using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Services;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    [RouteArea("Task")]
    [RoutePrefix("TaskItemAssign")]
    public class TaskItemAssignController : CoreController
    {
        private readonly ITaskItemAssignService _taskItemAssignService;
        private readonly IMapper _mapper;
        private readonly ISettingsService _settingsService;
        public TaskItemAssignController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, ITaskItemAssignService taskItemAssignService, IMapper mapper, ISettingsService settingsService) : base(loggerServices, userService, userDepartmentServices)
        {
            _taskItemAssignService = taskItemAssignService;
            _mapper = mapper;
            _settingsService = settingsService;
        }

        [Route("GetTaskItemAssign")]
        [HttpPost]
        public async Task<JsonResult> GetTaskItemAssign(Guid taskId, string action)
        {
            TaskItemAssignModel model = new TaskItemAssignModel();
            try
            {
                TaskItemAssignDto dto = null;
                if(action == "Process")
                {
                    dto = await _taskItemAssignService.GetByAssignTo(taskId, CurrentUser.Id);
                }
                if (action == "Appraise" || action == "Extend")
                {
                    dto = await _taskItemAssignService.GetByAssignBy(taskId, CurrentUser.Id);
                }
                if (dto == null)
                {
                    var rs = SendMessageResponse.CreateFailedResponse("AccessDenied");
                    return Json(rs, JsonRequestBehavior.AllowGet);
                }
                model = _mapper.Map<TaskItemAssignModel>(dto);
                model.TaskItemModel = _mapper.Map<TaskItemModel>(dto.TaskItem);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Cập nhật tình trạng task
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ProcessTaskItemAssign")]
        [HttpPost]
        public async Task<JsonResult> ProcessTaskItemAssign(TaskItemAssignModel model)
        {
            SendMessageResponse rs = null;
            try
            {
                TaskItemAssignDto dto = new TaskItemAssignDto();
                //check permission

                dto = _mapper.Map<TaskItemAssignDto>(model);
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
                            Source = Entities.Source.TaskItemAssign,
                        };
                        dto.Attachments.Add(attachmentDto);
                    }
                }
                rs = await _taskItemAssignService.UpdateProcessTaskAssign(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [Route("GetTaskItemAssignConfigPoint")]
        [HttpGet]
        public async Task<JsonResult> GetTaskItemAssignConfigPoint()
        {
            List<string> keys = new List<string>() { "Task.TypeBarProcesTask", "Task.DataSourceProcesTaskCombobox" };
            List<SettingsDto> models = await _settingsService.GetByKeys(keys);
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}