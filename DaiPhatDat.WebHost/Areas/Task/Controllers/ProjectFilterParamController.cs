using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    [RouteArea("Task")]
    [RoutePrefix("ProjectFilterParam")]
    public class ProjectFilterParamController : CoreController
    {
        private IProjectFilterParamService _projectFilterParamService;
        private ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProjectFilterParamController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IProjectFilterParamService projectFilterParamService, IMapper mapper, ICategoryService categoryService) : base(loggerServices, userService, userDepartmentServices)
        {
            _projectFilterParamService = projectFilterParamService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get GetAdvancedSearch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAdvancedSearch")]
        public async Task<JsonResult> GetAdvancedSearch(Guid? id)
        {
            var model = new ResponseProjectFilterParamModel();
            model.ProjectFilterParam = new ProjectFilterParamModel();
            try
            {
                if (id != null)
                {
                    model.ProjectFilterParam = _mapper.Map<ProjectFilterParamModel>(_projectFilterParamService.GetById(id.Value));
                }
                model.ProjectCategories = _mapper.Map<List<ProjectCategoryModel>>(await _categoryService.GetAllOfProjectCategories());
                model.TaskItemCategories = _mapper.Map<List<TaskItemCategoryModel>>(await _categoryService.GetAllTaskItemCategories());
                model.TaskItemStatuses = _mapper.Map<List<TaskItemStatusModel>>(await _categoryService.GetAllTaskItemStatuses());
                model.TaskItemPriorites = _mapper.Map<List<TaskItemPriorityModel>>(await _categoryService.GetAllTaskItemPriorities());
                model.NatureTasks = _mapper.Map<List<NatureTaskModel>>(await _categoryService.GetAllNatureTasks());
                model.ProjectFilterParams = _mapper.Map<List<ProjectFilterParamModel>>(_projectFilterParamService.GetProjectFilterParam("TASK", CurrentUser.Id, false));
                model.IsAdmin = CurrentUser.AccountName == "spadmin";
                var users = _userService.GetUsers();
                model.UserInfos = users.Select(e => new UserModel()
                {
                    FullName = e.FullName,
                    UserName = e.UserName,
                    ID = e.Id,
                }).ToList();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Add or Edit projectFilterParam
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveAdvancedSearch")]
        public JsonResult SaveAdvancedSearch(ProjectFilterParamModel model)
        {
            try
            { 
                var dto = _mapper.Map<ProjectFilterParamDto>(model);
                _projectFilterParamService.Save(dto, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(Guid Id)
        {
            try
            {
                _projectFilterParamService.Delete(Id);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }    
    }
}