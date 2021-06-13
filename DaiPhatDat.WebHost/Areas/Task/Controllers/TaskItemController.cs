using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    public class TaskItemController : BaseTaskController
    {
        public TaskItemController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, ITaskItemService taskItemService, IMapper mapper, ICategoryService categoryService, IAttachmentService attachmentService, IProjectService projectService) : base(loggerServices, userService, userDepartmentServices)
        {
            _attachmentService = attachmentService;
            _projectService = projectService;
            _mapper = mapper;
            _categoryService = categoryService;
            _taskItemService = taskItemService;
        }
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;
        private readonly IAttachmentService _attachmentService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        [HttpPost]
        public async Task<JsonResult> GetTaskItem(Guid? id)
        {
            TaskItemModel model = new TaskItemModel();
            try
            {
                TaskItemDto dto = null;
                if (id.HasValue)
                {
                    dto = await _taskItemService.GetById(id.Value);
                    if (dto == null)
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("NotExist");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                    if (dto.CreatedBy != CurrentUser.Id && !CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("AccessDenied");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dto = new TaskItemDto { AssignBy = CurrentUser.Id, AssignByFullName = CurrentUser.FullName };
                    dto.TaskItemAssigns.Add(new TaskItemAssignDto
                    {
                         AssignToFullName = CurrentUser.FullName,
                        AssignToJobTitleName = CurrentUser.Departments != null ? CurrentUser.Departments[0].JobTitle : string.Empty,
                        Department = CurrentUser.Departments != null ? CurrentUser.Departments[0].Name : string.Empty,
                        AssignTo = CurrentUser.Id,
                        TaskType = Entities.TaskType.Primary
                    });
                }
                model = _mapper.Map<TaskItemModel>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetNewTaskItem(TaskItemModel model)
        {
            try
            {
                TaskItemDto dto = _mapper.Map<TaskItemDto>(model);
                dto = await _taskItemService.GetNewTask(dto);
                dto.TaskItemAssigns.Add(new TaskItemAssignDto
                {
                    AssignToFullName = CurrentUser.FullName,
                    AssignToJobTitleName = CurrentUser.Departments != null ? CurrentUser.Departments[0].JobTitle : string.Empty,
                    Department = CurrentUser.Departments != null ? CurrentUser.Departments[0].Name : string.Empty,
                    AssignTo = CurrentUser.Id,
                    TaskType = Entities.TaskType.Primary
                });
                dto.AssignBy = CurrentUser.Id; dto.AssignByFullName = CurrentUser.FullName;
                model = _mapper.Map<TaskItemModel>(dto);
                model.TaskItemAssignIds = model.TaskItemAssigns.Select(e => e.AssignTo).ToList();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchTaskAssign(string userName, List<TaskItemAssignModel> taskItemAssigns)
        {
            List<TaskItemAssignModel> assigns = new List<TaskItemAssignModel>();
            try
            {
                if(!string.IsNullOrEmpty(userName) && userName.Length > 2)
                {
                    var users = _userService.GetUsers();
                    users = users.Where(e => e.IsActive && (e.UserName.ToLower().Contains(userName.ToLower()) || e.FullName.ToLower().Contains(userName.ToLower())) && (taskItemAssigns== null || !taskItemAssigns.Any(t=>t.AssignTo == e.Id))).ToList();
                    assigns = users.Select(e => new TaskItemAssignModel {
                        AssignToFullName = e.FullName,
                        AssignToJobTitleName = e.Departments!=null ? e.Departments[0].JobTitle : string.Empty,
                        Department = e.Departments != null ? e.Departments[0].Name : string.Empty,
                        AssignTo = e.Id,
                        TaskType = Entities.TaskType.Support,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(assigns, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> SaveTaskItem(TaskItemModel model)
        {
            _loggerServices.WriteError("test log");
            SendMessageResponse rs = null;
            try
            {
                TaskItemDto dto = new TaskItemDto();
                //check permission
                dto = _mapper.Map<TaskItemDto>(model);
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
                            Source = Entities.Source.TaskItem,
                        };
                        dto.Attachments.Add(attachmentDto);
                    }
                }
                rs = await _taskItemService.SaveAsync(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTaskItem(TaskItemDto model)
        {
            SendMessageResponse rs = null;
            try
            {
                TaskItemDto dto = new TaskItemDto();
                dto = _mapper.Map<TaskItemDto>(model);
                dto.IsFullControl = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    dto.IsFullControl = true;
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                rs = await _taskItemService.DeleteTaskItem(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateStatusTaskItem(TaskItemModel model)
        {
            SendMessageResponse rs = null;
            try
            {
                TaskItemDto dto = new TaskItemDto();
                dto = _mapper.Map<TaskItemDto>(model);
                dto.IsFullControl = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    dto.IsFullControl = true;
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                rs = await _taskItemService.UpdateStatusTaskItem(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> TaskItemDetail(Guid Id)
        {
            var result = new TaskItemDetailModel();
            try
            {
                var dto = await _taskItemService.RenderProjectTask(Id, CurrentUser);
                result = _mapper.Map<TaskItemDetailModel>(dto);
                result.TaskDetail = dto.Children.Select(e => new TaskDetailModel()
                {
                    Id = e.Id,
                    IsGroupLabel = e.IsGroupLabel,
                    TaskName = e.TaskName,
                    Content = e.Content,
                    FromDate = e.FromDate?.ToString("dd/MM/yy"),
                    ToDate = e.ToDate,
                    ToDateFormat = e.ToDate?.ToString("dd/MM/yy"),
                    FinishedDate = e.FinishedDate?.ToString("dd/MM/yy"),
                    AssignByID = e.UserId,
                    AssignByJobTitle = e.JobTitleName,
                    AssignByFullName = e.UserFullName,
                    AssignToJobTitle = dto.AssignByFullName,
                    AssignToFullName = dto.AssignByJobTitleName,
                    PercentFinish = e.PercentFinish,
                    StatusName = e.StatusName,
                    TaskItemStatusId = e.TaskItemStatusId,
                    DateFormat = e.DateFormat,
                }).ToList();
                result.TaskItemAssigns.ToList().ForEach(item =>
                {
                    string imgSrc = DefaultImageBase64;
                    var isImg = System.IO.File.Exists(Server.MapPath(string.Concat("~", AVARTAR_URL, "/", item.Id)));
                    if (isImg)
                    {
                        imgSrc = string.Concat(AVARTAR_URL, "/", item.Id);
                    }
                    item.AvatarUrl = imgSrc;
                });
                result.CurrentUserID = CurrentUser.Id;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Attachments(Guid projectId, Guid taskItemId)
        {

            var result = new List<AttachmentModel>();
            try
            {
                var dto = await _attachmentService.GetAttachments(projectId, taskItemId,Entities.Source.TaskItem);
                result = _mapper.Map<List<AttachmentModel>>(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Histories(QueryCommonModel model)
        {

            var result = new List<TaskItemProcessHistoryModel>();
            try
            {
                var filter = _mapper.Map<QueryCommonDto>(model);
                var dto = await _taskItemService.GetTaskHistories(filter);
                result = _mapper.Map<List<TaskItemProcessHistoryModel>>(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetTaskPriorities()
        {
            List<TaskItemPriorityDto> model = new List<TaskItemPriorityDto>();
            try
            {
                model = await _categoryService.GetAllTaskItemPriorities();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách quyền thao tác trên công việc của user
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CheckActionsTask(Guid projectId, Guid taskId)
        {
            var model = new List<ItemActionDto>();
            try
            {
                var userId = CurrentUser.Id;
                bool isFullControl = CurrentUser.HavePermission((EnumModulePermission.Task_FullControl));
                model = await _taskItemService.GetListItemActionTask(projectId, taskId, userId, isFullControl);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ReportTask(Guid TaskItemId)
        {
            var result = new ReportProjectModel();
            try
            {
                var taskItems = await _taskItemService.AllTaskItemChildren(TaskItemId);
                var model = _mapper.Map<List<TaskItemModel>>(taskItems);
                result = new ReportProjectModel()
                {
                    ProjectId = TaskItemId,
                    TaskItems = model
                };
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> UserReportInTask(Guid TaskItemId)
        {
            var model = new List<UserReportInProjectModel>();
            try
            {
                var dto = await _taskItemService.UserReportInTask(TaskItemId);
                model = _mapper.Map<List<UserReportInProjectModel>>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> ReturnTaskItemAssign(Guid Id)
        {
            try
            {
                bool isSuccess = await _taskItemService.ReturnTaskItemAssign(Id, CurrentUser.Id);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

      
    }
}