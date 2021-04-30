using AutoMapper;
using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurePortal.Module.Task.Web.Controllers
{
    public class ApisController : CoreController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IUserDepartmentServices _userdepartmentServices;
        public ApisController(ILoggerServices loggerServices
            , IUserServices userService
            , IUserDepartmentServices userDepartmentServices
            , IProjectService projectService
            , IMapper mapper
            ) : base(loggerServices, userService, userDepartmentServices)
        {
            _projectService = projectService;
            _mapper = mapper;
            _userdepartmentServices = userDepartmentServices;
        }

        // GET: Apis
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Route("FetchProjectsTasks")]
        public JsonResult FetchProjectsTasks(FetchProjectsTasksQuery fetchProjectsTasksQuery)
        {
            try
            {
                var fetchTaskItemsResult = new List<FetchProjectsTasksResult>();
                var lstParams = new List<string>();
                lstParams.Add($"@ParentId:{fetchProjectsTasksQuery.ParentId}");

                if (!string.IsNullOrEmpty(fetchProjectsTasksQuery.OtherParams))
                {
                    var arrStr = fetchProjectsTasksQuery.OtherParams.Split(';');
                    lstParams.AddRange(arrStr.ToList());
                }

                Pagination<FetchProjectsTasksResult> pagingData = null;

                if (!string.IsNullOrEmpty(fetchProjectsTasksQuery.OtherParams) && fetchProjectsTasksQuery.OtherParams.Contains("@PrivateFolder"))
                {
                  pagingData = _projectService.GetProjectsByFolderPaging(
                                        fetchProjectsTasksQuery.Search,
                                        lstParams,
                                        -1,
                                        15,
                                        " CreatedDate DESC ",
                                        CurrentUser,
                                        false);
                }
                else
                {
                    pagingData = _projectService.GetTaskWithFilterPaging( fetchProjectsTasksQuery.Search,lstParams,-1,15," CreatedDate DESC ",CurrentUser, false);
                }

                if (pagingData != null && pagingData.Result != null && pagingData.Result.Count() > 0)
                {
                    var data = pagingData.Result;
                   var userDepartments = _userDepartmentServices.GetCachedUserDepartmentDtos();

                    fetchTaskItemsResult = data
                        .Select(x =>
                        {
                            x.FromDateFormat = x.FromDate?.ToString("o");
                            x.ToDateFormat = x.ToDate?.ToString("o");

                            if (x.Type == "project")
                            {
                                x.UserId = x.AssignBy;
                            }

                            if (string.IsNullOrEmpty(x.Type))
                            {
                                x.Type = x.HasChildren == true ? "tasks" : "task";
                            }
                            if (x.UserId != null)
                            {
                                var userDeparment = userDepartments.Result.Where(y=>y.Equals(x.Type.Equals("project") ? x.ApprovedBy : x.UserId) && y.DeptID.Equals(x.DepartmentId)).FirstOrDefault();

                                x.FullName = userDeparment?.FullName;
                                x.JobTitle = userDeparment?.JobTitleName;

                            }
                            return x;
                        })
                        .ToList();
                }

                return Json(fetchTaskItemsResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message.ToString());
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}