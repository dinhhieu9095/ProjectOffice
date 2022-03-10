using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    [RouteArea("Task")]
    [RoutePrefix("Admin")]
    public class AdminController : CoreController
    {
        public AdminController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IMapper mapper) : base(loggerServices, userService, userDepartmentServices)
        {
            _mapper = mapper;
        }
        #region properties
        private IMapper _mapper;
        #endregion

        public ActionResult Category()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SaveProject(ProjectModel model)
        {
            SendMessageResponse rs = null;
            try
            {

                ProjectDto dto = new ProjectDto();
                //check permission
                dto = _mapper.Map<ProjectDto>(model);
                dto.IsFullControl = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    dto.IsFullControl = true;
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                dto.Attachments = new List<AttachmentDto>();
                if (Request.Files.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {

                        var fileContent = Request.Files[file];
                        byte[] document = Utility.ReadAllBytes(fileContent);
                        string ext = Path.GetExtension(fileContent.FileName).Replace(".", "");

                        AttachmentDto attachmentDto = new AttachmentDto()
                        {
                            Id = Guid.NewGuid(),
                            CreateByFullName = CurrentUser.FullName,
                            CreatedBy = CurrentUser.Id,
                            CreatedDate = DateTime.Now,
                            FileExt = ext,
                            FileContent = document,
                            FileName = fileContent.FileName,
                            FileSize = fileContent.ContentLength,
                            ProjectId = dto.Id,
                            Source = Source.Project
                        };
                        dto.Attachments.Add(attachmentDto);
                    }
                }
                //rs = await _projectService.SaveAsync(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}
