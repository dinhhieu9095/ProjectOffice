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
    [RoutePrefix("Project")]
    public partial class ProjectController : CoreController
    {
        public ProjectController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IProjectService projectService,
            IMapper mapper, ICategoryService categoryService, IAttachmentService attachmentService, ITaskItemService taskItemService) : base(loggerServices, userService, userDepartmentServices)
        {
            _projectService = projectService;
            _taskItemService = taskItemService;
            _mapper = mapper;
            _categoryService = categoryService;
            _attachmentService = attachmentService;
        }
        #region properties
        private IProjectService _projectService;
        private ITaskItemService _taskItemService;
        private ICategoryService _categoryService;
        private IAttachmentService _attachmentService;
        private IMapper _mapper;
        #endregion
        [HttpPost]
        public async Task<JsonResult> GetProject(Guid? id)
        {
            ProjectModel model = new ProjectModel();
            try
            {
                ProjectDto dto = new ProjectDto();
                if (id.HasValue)
                {
                    dto = await _projectService.GetById(id.Value);
                    if (dto == null)
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("NotExist");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                    if (dto.CreatedBy != CurrentUser.Id && !dto.ManagerId.Contains(CurrentUser.Id.ToString()) && !CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("AccessDenied");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dto.ProjectMembers.Add(new ProjectMemberDto()
                    {
                        Department = CurrentUser.Departments!=null ? CurrentUser.Departments[0].Name:string.Empty,
                        JobTitle = CurrentUser.Departments != null ? CurrentUser.Departments[0].JobTitle : string.Empty,
                        Role = "1",
                        UserId = CurrentUser.Id,
                        UserName = CurrentUser.UserName,
                        FullName = CurrentUser.FullName,
                    });
                }
                

                model = _mapper.Map<ProjectModel>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchProjectMember(string userName, List<ProjectMemberModel> projectMembers)
        {
            List<ProjectMemberModel> assigns = new List<ProjectMemberModel>();
            try
            {
                if (!string.IsNullOrEmpty(userName) && userName.Length > 2)
                {
                    var users = _userService.GetUsers();
                    users = users.Where(e => e.IsActive && (e.UserName.ToLower().Contains(userName.ToLower()) || e.FullName.ToLower().Contains(userName.ToLower())) && (projectMembers == null || !projectMembers.Any(t => t.UserId == e.Id.ToString()))).ToList();
                    assigns = users.Select(e => new ProjectMemberModel
                    {
                        FullName = e.FullName,
                        JobTitle = e.Departments != null ? e.Departments[0].JobTitle : string.Empty,
                        Department = e.Departments != null ? e.Departments[0].Name : string.Empty,
                        UserId = e.Id.ToString(),
                        Role = "2",
                        UserName = e.UserName
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
                rs = await _projectService.SaveAsync(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatusProject(ProjectModel model)
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
                            ProjectId = dto.Id
                        };
                        dto.Attachments.Add(attachmentDto);
                    }
                }
                rs = await _projectService.UpdateStatusProjectAsync(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteProject(ProjectDto model)
        {
            SendMessageResponse rs = null;
            try
            {
                ProjectDto dto = new ProjectDto();
                dto = _mapper.Map<ProjectDto>(model);
                dto.IsFullControl = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    dto.IsFullControl = true;
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                rs = await _projectService.DeleteProject(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetProjectStatuses()
        {
            List<ProjectStatusDto> model = new List<ProjectStatusDto>();
            try
            {
                model = await _categoryService.GetAllProjectStatuses();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetProjectTypes()
        {
            List<ProjectTypeDto> model = new List<ProjectTypeDto>();
            try
            {
                model = await _categoryService.GetAllOfProjectTypes();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetProjectCategories(Guid projectId,Guid? taskId = null)
        {
            List<string> model = new List<string>();
            try
            {
                model = await _categoryService.GetProjectCategoriesByProjectId(projectId, taskId);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetProjectPriorities()
        {
            List<ProjectPriorityDto> model = new List<ProjectPriorityDto>();
            try
            {
                model = await _categoryService.GetAllOfProjectPriorities();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> PostTrackingUpdateDB()
        {
            try
            {
                bool rs = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    rs = await _categoryService.PostTrackingUpdateDB();
                }
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TrackingUpdateDatabase()
        {
            if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
            {
                return View();
            }
            return RedirectToAction(HomeController.AccessDeniedAction, HomeController.ControllerName);
        }

        [HttpGet]
        public async Task<JsonResult> ProjectDetail(Guid Id)
        {
            var result = new ProjectDetailModel();
            try
            {
                var x = await _projectService.RenderProject(Id, CurrentUser, false);
                if (x != null)
                {
                    result = new ProjectDetailModel()
                    {
                        Id = x.Id,
                        Attachments = x.Attachments.Select(y => new AttachmentModel()
                        {
                            Id = y.Id,
                            CreateByFullName = y.CreateByFullName,
                            CreatedBy = y.CreatedBy,
                            CreatedDate = y.CreatedDate,
                            FileExt = y.FileExt,
                            FileName = y.FileName,
                            ProjectId = y.ProjectId,
                            ItemId = y.ItemId,
                            DateFormat = y.CreatedDate.Value.ToString("dd/MM/yyyy")
                        }).ToList(),
                        Summary = x.Summary,
                        ProjectCategory = x.ProjectCategory,
                        ProjectStatusName = x.ProjectStatusName,
                        StatusId = x.StatusId,
                        Finished = x.FinishedDate,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        ProjectPriorityId = x.ProjectPriorityId,
                        ApprovedBy = x.ApprovedBy,
                        CreatedBy = x.CreatedBy,
                        DepartmentId = x.DepartmentId,
                        ProjectContent = x.ProjectContent,
                        UserViews = x.UserViews,
                        PercentFinish = x.PercentFinish,
                        ProjectKindId = x.ProjectKindId,
                        ProjectKindName = x.ProjectKindName,
                        ProjectHistories = x.ProjectHistories.Select(e => new ProjectHistoryModel()
                        {
                            Id = e.Id,
                            Action = e.Action,
                            ActionId = e.ActionId,
                            CreatedBy = e.CreatedBy,
                            CreatedByFullName = e.CreatedByFullName,
                            CreatedByJobTitleName = e.CreatedByJobTitleName,
                            Attachments = e.Attachments.Select(y => new AttachmentModel()
                            {
                                Id = y.Id,
                                CreateByFullName = y.CreateByFullName,
                                CreatedBy = y.CreatedBy,
                                CreatedDate = y.CreatedDate,
                                FileExt = y.FileExt,
                                FileName = y.FileName,
                                ProjectId = y.ProjectId,
                                ItemId = y.ItemId,
                                DateFormat = y.CreatedDate.Value.ToString("dd/MM/yyyy")
                            }).ToList(),
                            DepartmentId = e.DepartmentId,
                            PercentFinish = e.PercentFinish,
                            ProjectId = e.ProjectId,
                            Summary = e.Summary,
                            Created = e.Created,
                            DateFormat = e.DateFormat
                        }).ToList(),
                        TaskItemRoots = x.TaskItemRoots.Select(e => new ProjectDetailModel.TaskItemRoot
                        {
                            UserId = e.UserId,
                            AssignBy = e.AssignBy,
                            FromDate = e.FromDate,
                            Id = e.Id,
                            Content = e.Content,
                            PercentFinish = e.PercentFinish,
                            TaskItemStatusId = e.TaskItemStatusId,
                            TaskName = e.TaskName,
                            ToDate = e.ToDate,
                            StatusName = e.StatusName,
                            DepartmentId = e.DepartmentId,
                            IsGroupLabel = e.IsGroupLabel,
                            CountChildren = e.CountChildren,
                            UserFullName = e.UserFullName,
                            JobTitleName = e.JobTitleName,
                            DateFormat = e.DateFormat,
                            FinishedDate = e.FinishedDate
                        }).ToList(),
                        TaskItems = x.TaskItemRoots.Select(e => new TaskDetailModel
                        {
                            Id = e.Id,
                            IsGroupLabel = e.IsGroupLabel,
                            Attachments = e.Attachments.Select(y => new AttachmentModel()
                            {
                                Id = y.Id,
                                CreateByFullName = y.CreateByFullName,
                                CreatedBy = y.CreatedBy,
                                CreatedDate = y.CreatedDate,
                                FileExt = y.FileExt,
                                FileName = y.FileName,
                                FileSize = y.FileSize,
                                ProjectId = y.ProjectId,
                                ItemId = y.ItemId,
                                DateFormat = y.CreatedDate.Value.ToString("dd/MM/yyyy")
                            }).ToList(),
                            TaskName = e.TaskName,
                            Content = e.Content,
                            FromDate = e.FromDate?.ToString("dd/MM/yy"),
                            ToDate = e.ToDate,
                            ToDateFormat = e.ToDate?.ToString("dd/MM/yy"),
                            FinishedDate = e.FinishedDate?.ToString("dd/MM/yy"),
                            AssignByID = e.UserId,
                            AssignByJobTitle = e.JobTitleName,
                            AssignByFullName = e.UserFullName,
                            AssignToID = e.UserAssignId,
                            AssignToJobTitle = e.UserAssignJobTitleName,
                            AssignToFullName = e.UserAssignFullName,
                            PercentFinish = e.PercentFinish,
                            StatusName = e.StatusName,
                            TaskItemStatusId = e.TaskItemStatusId,
                            DateFormat = e.DateFormat,
                        }).ToList(),
                        DateTimeString = x.DateTimeString,
                        DepartmentName = x.DepartmentName,
                    };
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> AttachmentProject(Guid Id)
        {
            var result = new List<AttachmentModel>();
            try
            {
                var attachmentDtos = await _attachmentService.GetAttachments(Id, Id);
                result = _mapper.Map<List<AttachmentModel>>(attachmentDtos);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<System.Web.Mvc.FileResult> DownloadFileTrackingWorkflowDocument(Guid fileTrackingWorkflowDocumentID, Guid deptID = default(Guid))
        {
            try
            {
                var file = await _attachmentService.GetById(fileTrackingWorkflowDocumentID);

                return File(file.FileContent, WebUtils.GetContentType(file.FileExt), file.FileName);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return null;
        }

        [HttpGet]
        public async Task<JsonResult> ReportProject(Guid Id)
        {
            var result = new ReportProjectModel();
            try
            {
                var dtos = await _projectService.AllTaskItemInProject(Id);
                var model = _mapper.Map<List<TaskItemModel>>(dtos);
                result = new ReportProjectModel()
                {
                    ProjectId = Id,
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
        public async Task<JsonResult> UserInProject(Guid Id)
        {
            var model = new List<UserReportInProjectModel>();
            try
            {
               var dto = await _projectService.UserReportInProject(Id);
                model = _mapper.Map<List<UserReportInProjectModel>>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckActionsProject(Guid projectId)
        {
            var model = new List<ItemActionDto>();
            try
            {
                var userId = CurrentUser.Id;
                bool isFullControl = CurrentUser.HavePermission((EnumModulePermission.Task_FullControl));
                model = _projectService.GetListItemAction(projectId, userId, isFullControl);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
    
}