using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using RefactorThis.GraphDiff;
using System.Configuration;
using DaiPhatDat.Core.Kernel.Notifications.Application;
using DaiPhatDat.Core.Kernel.Notifications.Application.Dto;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentServices _departmentServices;
        private readonly IUserServices _userServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        private readonly INotificationServices _notificationServices;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        private readonly ITaskItemRepository _objectRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ICommentService _commentService;
        private readonly IAttachmentService _attachmentService;
        private readonly ITaskItemProcessHistoryRepository _taskItemHistoryRepository;
        private readonly ITaskItemAssignRepository _taskItemAssignRepository;
        private readonly IActionRepository _actionRepository;
        public TaskItemService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices, ICategoryService categoryService, IDepartmentServices departmentServices, IUserDepartmentServices userDepartmentServices, ITaskItemRepository objectRepository, IProjectService projectService, ITaskItemProcessHistoryRepository taskItemHistoryRepository, ICommentService commentService, IAttachmentService attachmentService, IProjectRepository projectRepository, IAttachmentRepository attachmentRepository, IActionRepository actionRepository, ITaskItemAssignRepository taskItemAssignRepository, INotificationServices notificationServices)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _categoryService = categoryService;
            _mapper = mapper;
            _departmentServices = departmentServices;
            _userServices = userServices;
            _userDepartmentServices = userDepartmentServices;
            _objectRepository = objectRepository;
            _taskItemHistoryRepository = taskItemHistoryRepository;
            _projectService = projectService;
            _commentService = commentService;
            _attachmentService = attachmentService;
            _projectRepository = projectRepository;
            _attachmentRepository = attachmentRepository;
            _actionRepository = actionRepository;
            _taskItemAssignRepository = taskItemAssignRepository;
            _notificationServices = notificationServices;
        }
        public async Task<TaskItemDto> GetById(Guid id)
        {
            TaskItemDto dto = new TaskItemDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    TaskItem entity = _objectRepository.GetAll().Include(t => t.TaskItemAssigns).Include(t => t.Project).Where(p => p.IsDeleted == false && p.Id == id).FirstOrDefault();
                    entity.TaskItemAssigns = entity.TaskItemAssigns.Where(a => !a.IsDeleted).ToList();
                    var users = _userServices.GetUsers();
                    var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                    dto = _mapper.Map<TaskItemDto>(entity);
                    if(!dto.AdminCategoryId.HasValue)
                        dto.AdminCategoryId = Guid.Empty;
                    if (!string.IsNullOrEmpty(dto.TaskItemCategory))
                    {
                        dto.TaskItemCategories = dto.TaskItemCategory.Split(';').ToList();
                    }
                    foreach (TaskItemAssignDto assignee in dto.TaskItemAssigns)
                    {
                        var userDept = userDepartments.Where(e => e.UserID == assignee.AssignTo).FirstOrDefault();
                        if (userDept != null)
                        {
                            assignee.AssignToFullName = userDept.FullName;
                            assignee.AssignToJobTitleName = userDept.JobTitleName;
                            assignee.Department = userDept.DeptName;
                        }
                    }
                    dto.TaskItemAssigns = dto.TaskItemAssigns.OrderBy(e => e.AssignToFullName).ToList();
                    var assignBy = userDepartments.Where(e => e.UserID == dto.AssignBy).FirstOrDefault();
                    dto.AssignByFullName = assignBy.FullName;
                    if (dto.FromDate.HasValue)
                    {
                        dto.FromDateText = dto.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (dto.ToDate.HasValue)
                    {
                        dto.ToDateText = dto.ToDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (dto.ParentId.HasValue && dto.ParentId != Guid.Empty)
                    {
                        TaskItem parent = _objectRepository.GetAll().Where(p => p.IsDeleted == false && p.Id == dto.ParentId).FirstOrDefault();
                        dto.IsParentAuto = entity.Project.IsAuto;
                        dto.ParentToDateText = parent.ToDate.Value.ToString("dd/MM/yyyy");
                        dto.ParentFromDateText = parent.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        dto.IsParentAuto = entity.Project.IsAuto;
                        dto.ParentToDateText = entity.Project.ToDate.Value.ToString("dd/MM/yyyy");
                        dto.ParentFromDateText = entity.Project.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    dto.IsLinked = entity.Project.IsLinked;
                    dto.Project = null;
                    dto.AdminCategoryId = null;
                    List<AttachmentDto> attachmentDtos = _attachmentRepository.GetAll().Where(e => e.ProjectId == entity.ProjectId && e.ItemId.HasValue && e.ItemId == entity.Id && e.Source == Source.TaskItem).Select(e => new AttachmentDto()
                    {
                        Id = e.Id,
                        FileName = e.FileName,
                        FileExt = e.FileExt
                    }).ToList();
                    dto.Attachments = attachmentDtos;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public async Task<TaskItemDto> GetNewTask(TaskItemDto dto)
        {
            try
            {
                bool isLinked, isAuto = false;
                DateTime fromDate, toDate;
                ProjectDto project = await _projectService.GetById(dto.ProjectId.Value);
                dto.IsParentAuto = project.IsAuto;
                if (dto.ParentId.HasValue)
                {
                    TaskItemDto parent = await GetById(dto.ParentId.Value);
                    dto.IsAuto = parent.IsAuto;
                    isLinked = project.IsLinked;
                    isAuto = project.IsAuto;
                    fromDate = parent.FromDate.Value;
                    toDate = parent.ToDate.Value;
                }
                else
                {
                    dto.IsAuto = project.IsAuto;
                    isLinked = project.IsLinked;
                    isAuto = project.IsAuto;
                    toDate = project.ToDate.Value;
                    fromDate = project.FromDate.Value;
                }
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    List<TaskItemDto> taskItemDtos = _objectRepository.GetAll().Where(e => dto.ParentId.HasValue ? e.ParentId == dto.ParentId : (e.ProjectId == dto.ProjectId && (!e.ParentId.HasValue || e.ParentId == Guid.Empty))).Select(e => new TaskItemDto
                    {
                        FromDate = e.FromDate,
                        ToDate = e.ToDate
                    }).ToList();
                    if (taskItemDtos.Any())
                    {
                        DateTime toDateMax = taskItemDtos.Max(e => e.ToDate).Value;
                        if (!isAuto && toDateMax >= toDate)
                        {
                            dto.FromDate = toDateMax;
                            dto.ToDate = toDateMax;
                        }
                        else
                        {
                            dto.FromDate = toDateMax.AddDays(1);
                            dto.ToDate = toDateMax.AddDays(1);
                        }
                    }
                    else
                    {
                        dto.FromDate = fromDate;
                        dto.ToDate = fromDate;
                    }
                }
                if (dto.FromDate.HasValue)
                {
                    dto.FromDateText = dto.FromDate.Value.ToString("dd/MM/yyyy");
                }
                if (dto.ToDate.HasValue)
                {
                    dto.ToDateText = dto.ToDate.Value.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return null;
            }
            return dto;
        }
        public async Task<TaskItemDto> GetNewAdminTask(TaskItemDto dto)
        {
            try
            {
                bool isAuto = true;
                DateTime fromDate, toDate;
                
                dto.IsParentAuto = true;
                if (dto.ParentId.HasValue)
                {
                    TaskItemDto parent = await GetById(dto.ParentId.Value);
                    dto.IsAuto = parent.IsAuto;
                    isAuto = true;
                    fromDate = parent.FromDate.Value;
                    toDate = parent.ToDate.Value;
                }
                else
                {
                    dto.IsAuto = true;
                    isAuto = true;
                    toDate = DateTime.Now;
                    fromDate = DateTime.Now;
                }
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    List<TaskItemDto> taskItemDtos = _objectRepository.GetAll().Where(e => dto.ParentId.HasValue ? e.ParentId == dto.ParentId : (e.ProjectId == dto.ProjectId && (!e.ParentId.HasValue || e.ParentId == Guid.Empty))).Select(e => new TaskItemDto
                    {
                        FromDate = e.FromDate,
                        ToDate = e.ToDate
                    }).ToList();
                    if (taskItemDtos.Any())
                    {
                        DateTime toDateMax = taskItemDtos.Max(e => e.ToDate).Value;
                        if (!isAuto && toDateMax >= toDate)
                        {
                            dto.FromDate = toDateMax;
                            dto.ToDate = toDateMax;
                        }
                        else
                        {
                            dto.FromDate = toDateMax.AddDays(1);
                            dto.ToDate = toDateMax.AddDays(1);
                        }
                    }
                    else
                    {
                        dto.FromDate = fromDate;
                        dto.ToDate = fromDate;
                    }
                }
                if (dto.FromDate.HasValue)
                {
                    dto.FromDateText = dto.FromDate.Value.ToString("dd/MM/yyyy");
                }
                if (dto.ToDate.HasValue)
                {
                    dto.ToDateText = dto.ToDate.Value.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return null;
            }
            return dto;
        }
        public IReadOnlyList<Guid> GetTaskOfUserAssign(Guid id, Guid userId)
        {
            IReadOnlyList<Guid> models = new List<Guid>();
            try
            {
                using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var taskContext = dbContextReadOnlyScope.DbContexts.Get<TaskContext>();
                    var taskItems = _objectRepository.Get(
                           e => e.ProjectId == id && e.AssignBy == userId);
                    models = taskItems.Select(e => e.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return models;
        }
        public async Task<SendMessageResponse> SaveAsync(TaskItemDto dto, TaskItemStatusId taskItemStatusId = TaskItemStatusId.New)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                List<CreateNotificationDto> crtNoties = new List<CreateNotificationDto>();
                using (var scope = _dbContextScopeFactory.Create())
                {
                    List<TaskItemAssignDto> lstUserNoti = new List<TaskItemAssignDto>();
                    if (DateTime.TryParseExact(dto.FromDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate))
                    {
                        dto.FromDate = fromDate;
                    }
                    else
                    {
                        dto.FromDate = null;
                    }
                    if (DateTime.TryParseExact(dto.ToDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate))
                    {
                        dto.ToDate = toDate;
                    }
                    else
                    {
                        dto.ToDate = null;
                    }
                    List<AttachmentDto> attachmentDtos = dto.Attachments.ToList();
                    dto.Attachments = null;
                    TaskItem entity = _objectRepository.GetAll().Include(e => e.TaskItemAssigns).Where(e => e.Id == dto.Id).FirstOrDefault();
                    Project project = _projectRepository.GetAll().Where(e => e.Id == dto.ProjectId).FirstOrDefault();

                    if (entity != null)
                    {
                        if (!((dto.IsFullControl || entity.AssignBy == dto.ModifiedBy) && entity.TaskItemStatusId != TaskItemStatusId.Finished))
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                        
                        entity.AssignBy = dto.AssignBy;
                        entity.Conclusion = dto.Conclusion;
                        entity.DepartmentId = dto.DepartmentId;
                        entity.FinishedDate = dto.FinishedDate;
                        entity.FromDate = dto.FromDate;
                        entity.HasRecentActivity = dto.HasRecentActivity;
                        entity.IsAuto = dto.IsAuto;
                        entity.IsDeleted = dto.IsDeleted;
                        entity.IsAdminCategory = dto.IsAdminCategory;
                        entity.AdminCategoryId = dto.AdminCategoryId;
                        entity.IsGroupLabel = dto.IsGroupLabel;
                        entity.IsReport = dto.IsReport;
                        entity.IsSecurity = dto.IsSecurity;
                        entity.IsWeirdo = dto.IsWeirdo;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.NatureTaskId = dto.NatureTaskId;
                        entity.Order = dto.Order;
                        entity.ParentId = dto.ParentId;
                        entity.PercentFinish = dto.PercentFinish;
                        entity.ProjectId = dto.ProjectId;
                        entity.TaskGroupType = dto.TaskGroupType;
                        entity.TaskItemCategory = string.Join(";", dto.TaskItemCategories);
                        entity.TaskItemPriorityId = dto.TaskItemPriorityId;
                        entity.TaskItemStatusId = dto.TaskItemStatusId;
                        if(dto.TaskItemStatusId == TaskItemStatusId.Draft)
                        {
                            entity.TaskItemStatusId = taskItemStatusId;
                        }
                        entity.TaskName = dto.TaskName;
                        entity.TaskType = dto.TaskType;
                        entity.ToDate = dto.ToDate;
                        entity.Weight = dto.Weight;
                        entity.PercentFinish = dto.PercentFinish;
                        TaskItemProcessHistory taskHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = entity.ProjectId,
                            ActionId = ActionId.Update,
                            CreatedBy = dto.ModifiedBy,
                            PercentFinish = dto.PercentFinish,
                            CreatedDate = dto.ModifiedDate,
                            TaskItemId = entity.Id,
                            TaskItemAssignId = null,
                            TaskItemStatusId = entity.TaskItemStatusId,
                            ProcessResult = entity.TaskName
                        };
                        if (dto.TaskItemStatusId == TaskItemStatusId.Draft)
                        {
                            taskHistory.ActionId = ActionId.SaveDraft;
                        }
                        if (entity.TaskItemAssigns != null && entity.TaskItemAssigns.Any())
                        {
                            foreach (var assignentity in entity.TaskItemAssigns.Where(e => !e.IsDeleted))
                            {
                                var assigndto = dto.TaskItemAssigns.Where(e => e.AssignTo == assignentity.AssignTo).FirstOrDefault();
                                if (assigndto == null)
                                {
                                    assignentity.IsDeleted = true;
                                }
                                else
                                {
                                    assignentity.TaskType = assigndto.TaskType;
                                }
                                assignentity.ModifiedDate = DateTime.Now;
                            }
                        }
                        List<TaskItemAssignDto> assignAdds = dto.TaskItemAssigns.Where(e => !entity.TaskItemAssigns.Where(a => !a.IsDeleted).Any(t => t.AssignTo == e.AssignTo)).ToList();
                        foreach (var assign in assignAdds)
                        {
                            assign.Id = Guid.NewGuid();
                            assign.TaskItemId = dto.Id;
                            assign.ProjectId = dto.ProjectId;
                            assign.ModifiedDate = dto.ModifiedDate;
                            assign.TaskItemStatusId = TaskItemStatusId.New;
                            entity.TaskItemAssigns.Add(_mapper.Map<TaskItemAssign>(assign));
                        }
                        lstUserNoti.AddRange(assignAdds);
                        _objectRepository.Modify(entity);
                        _taskItemHistoryRepository.Add(taskHistory);
                    }
                    else
                    {
                        var managerInTask = new List<Guid?>();
                        managerInTask.Add(project.ApprovedBy);
                        if (!string.IsNullOrEmpty(project.ManagerId))
                        {
                            managerInTask.AddRange(project.ManagerId.Split(';').Select(e => new Guid?(new Guid(e))));
                        }

                        if (dto.ParentId.HasValue)
                        {
                            TaskItem parent = _objectRepository.GetAll().Where(p => p.IsDeleted == false && p.Id == dto.ParentId).FirstOrDefault();
                            if (!((managerInTask.Contains(dto.ModifiedBy)
                   || ListTaskParentHasUser(parent.Id, dto.ModifiedBy.Value) == 1 || dto.IsFullControl)
                   && parent.TaskItemStatusId != TaskItemStatusId.Finished
                   ))
                            {
                                sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                                return sendMessage;
                            }
                        }
                        else
                        {
                            if (!((managerInTask.Contains(dto.ModifiedBy) || dto.IsFullControl)
                  && project.ProjectStatusId != ProjectStatusId.Finished
                  ))
                            {
                                sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                                return sendMessage;
                            }
                        }

                        dto.TaskItemStatusId = taskItemStatusId;
                        dto.Id = Guid.NewGuid();
                        dto.CreatedBy = dto.ModifiedBy;
                        dto.CreatedDate = dto.ModifiedDate;
                        dto.TaskItemCategory = string.Join(";", dto.TaskItemCategories);
                        dto.IsDeleted = false;
                        foreach (var assign in dto.TaskItemAssigns)
                        {
                            assign.Id = Guid.NewGuid();
                            assign.TaskItemId = dto.Id;
                            assign.ProjectId = dto.ProjectId;
                            assign.ModifiedDate = dto.ModifiedDate;
                            assign.TaskItemStatusId = TaskItemStatusId.New;
                        }
                        lstUserNoti.AddRange(dto.TaskItemAssigns);
                        entity = _mapper.Map<TaskItem>(dto);
                        entity.Project = null;
                        TaskItemProcessHistory taskHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = entity.ProjectId,
                            ActionId = ActionId.Create,
                            CreatedBy = dto.ModifiedBy,
                            PercentFinish = dto.PercentFinish,
                            CreatedDate = dto.ModifiedDate,
                            TaskItemId = entity.Id,
                            TaskItemAssignId = null,
                            TaskItemStatusId = dto.TaskItemStatusId,
                            ProcessResult = entity.TaskName
                        };
                        if (dto.TaskItemStatusId == TaskItemStatusId.Draft)
                        {
                            taskHistory.ActionId = ActionId.SaveDraft;
                        }
                        entity.TaskItemProcessHistories.Add(taskHistory);
                        _objectRepository.Add(entity);
                    }
                    foreach (AttachmentDto attachDto in attachmentDtos)
                    {
                        attachDto.ProjectId = entity.ProjectId;
                        attachDto.ItemId = entity.Id;
                        Attachment attach = _mapper.Map<Attachment>(attachDto);
                        _attachmentRepository.Add(attach);
                    }
                    if (dto.AttachDelIds != null)
                    {
                        if (dto.AttachDelIds.Any())
                        {
                            List<Attachment> attachDels = _attachmentRepository.GetAll().Where(e => dto.AttachDelIds.Contains(e.Id)).ToList();
                            _attachmentRepository.DeleteRange(attachDels);
                        }
                    }
                    await scope.SaveChangesAsync();
                    if(taskItemStatusId != TaskItemStatusId.Draft)
                    {
                        var param = new List<SqlParameter>();
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@ProjectId",
                            IsNullable = false,
                            Value = dto.ProjectId
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@TaskId",
                            IsNullable = false,
                            Value = dto.Id
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@FromDate",
                            IsNullable = false,
                            Value = dto.FromDate
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@ToDate",
                            IsNullable = false,
                            Value = dto.ToDate
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.Bit,
                            ParameterName = "@IsUpdateStatus",
                            IsNullable = true,
                            Value = 1
                        });
                        await _objectRepository.SqlQueryAsync(typeof(ProjectDto), "[dbo].[SP_UPDATE_TASK_RANGE_DATE] @ProjectId, @TaskId, @FromDate, @ToDate, @IsUpdateStatus", param.ToArray());
                        
                        
                        foreach (var noti in lstUserNoti)
                        {
                            string subject = "";
                            if (dto.TaskItemStatusId == TaskItemStatusId.New)
                            {
                                subject = string.Format(ResourceManagement.GetResourceText("Task.Noti.NewTask", "Công việc {0} được tạo mới!", "A new task {0} has been created!"), dto.TaskName);
                            }
                            else
                            {
                                subject = string.Format(ResourceManagement.GetResourceText("Task.Noti.UpdateTask", "Công việc {0} đã được cập nhật!", "A task {0} has been updated!"), dto.TaskName);
                            }
                            var create = new CreateNotificationDto
                            {
                                ObjectId = noti.TaskItemId.Value,
                                Subject = subject,
                                ModuleCode = "Task",
                                Url = string.Format(@"/Task/Home/TaskItemDetailNotify?taskId={0}", dto.Id),
                                SenderId = dto.ModifiedBy,
                                RecipientId = noti.AssignTo.Value,
                            };
                            crtNoties.Add(create);
                        }

                    }
                    if (dto.AdminCategoryId.HasValue && dto.AdminCategoryId != Guid.Empty)
                    {
                        var param = new List<SqlParameter>();
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@AdminCategoryId",
                            IsNullable = false,
                            Value = dto.AdminCategoryId.Value
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@ParentId",
                            IsNullable = false,
                            Value = dto.Id
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@ProjectId",
                            IsNullable = false,
                            Value = dto.ProjectId
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.UniqueIdentifier,
                            ParameterName = "@CurrentUserId",
                            IsNullable = false,
                            Value = dto.ModifiedBy
                        });
                        await _objectRepository.SqlQueryAsync(typeof(TaskItemDto), "[dbo].[SP_ADMIN_CATEGORY_CLONE_TASK] @AdminCategoryId, @ProjectId, @ParentId, @CurrentUserId", param.ToArray());
                    }
                }
                if (crtNoties.Count > 0)
                {
                    bool nt = await _notificationServices.AddRangeAsync(crtNoties);
                }
                sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);
                return sendMessage;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }

        public async Task<SendMessageResponse> DeleteTaskItem(TaskItemDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                List<Guid> Ids = new List<Guid>();
                using (var scope = _dbContextScopeFactory.Create())
                {
                    TaskItem entity = await _objectRepository.FindAsync(e => e.Id == dto.Id);

                    if (entity != null)
                    {
                        if (!dto.IsFullControl && entity.AssignBy != dto.ModifiedBy)
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                    }
                    var param = new List<SqlParameter>();
                    param.Add(new SqlParameter()
                    {
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        ParameterName = "@parentId",
                        IsNullable = true,
                        Value = dto.Id
                    });
                    List<object> entities = await _objectRepository.SqlQueryAsync(typeof(Guid), "SP_TASK_BY_TASK_PARENT @parentId", param.ToArray());
                    if (entities.Any())
                    {
                        Ids = entities.Cast<Guid>().ToList();
                    }

                    List<TaskItem> taskItems = _objectRepository.GetAll().Where(e => e.Id == dto.Id).ToList();

                    entity.IsDeleted = true;
                    entity.ModifiedBy = dto.ModifiedBy;
                    entity.ModifiedDate = dto.ModifiedDate;
                    _objectRepository.Modify(entity, new List<Expression<Func<TaskItem, object>>>() { p => p.ModifiedBy, p => p.ModifiedDate, p => p.IsDeleted });
                    foreach (var item in taskItems)
                    {
                        item.IsDeleted = true;
                        item.ModifiedBy = dto.ModifiedBy;
                        item.ModifiedDate = dto.ModifiedDate;
                        _objectRepository.Modify(item, new List<Expression<Func<TaskItem, object>>>() { p => p.ModifiedBy, p => p.ModifiedDate, p => p.IsDeleted });
                    }
                    await scope.SaveChangesAsync();
                    sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);

                    return sendMessage;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }

        public async Task<SendMessageResponse> UpdateStatusTaskItem(TaskItemDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                List<Guid> Ids = new List<Guid>();
                using (var scope = _dbContextScopeFactory.Create())
                {
                    var param = new List<SqlParameter>();
                    param.Add(new SqlParameter()
                    {
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        ParameterName = "@parentId",
                        IsNullable = false,
                        Value = dto.Id
                    });
                    List<object> entities = await _objectRepository.SqlQueryAsync(typeof(Guid), "SP_TASK_BY_TASK_PARENT @parentId", param.ToArray());
                    if (entities.Any())
                    {
                        Ids = entities.Cast<Guid>().ToList();
                    }

                    List<TaskItem> taskItems = _objectRepository.GetAll().Where(e => e.Id == dto.Id).ToList();
                    TaskItem entity = await _objectRepository.FindAsync(e => e.Id == dto.Id);

                    if (entity != null)
                    {
                        if (!dto.IsFullControl && entity.CreatedBy != dto.ModifiedBy)
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                    }
                    ActionId? actionId = null;
                    if (dto.ActionText == "Finish")
                    {
                        actionId = ActionId.Finish;
                        dto.FinishedDate = dto.ModifiedDate;
                        dto.TaskItemStatusId = TaskItemStatusId.Finished;
                    }
                    if (dto.ActionText == "Evict")
                    {
                        actionId = ActionId.Evict;
                        dto.TaskItemStatusId = TaskItemStatusId.Cancel;
                    }

                    foreach (var item in taskItems)
                    {
                        item.TaskItemStatusId = dto.TaskItemStatusId;
                        item.ModifiedBy = dto.ModifiedBy;
                        item.ModifiedDate = dto.ModifiedDate;
                        item.FinishedDate = dto.FinishedDate;
                        TaskItemProcessHistory taskChildHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = item.ProjectId,
                            ActionId = actionId,
                            CreatedBy = dto.ModifiedBy,
                            CreatedDate = dto.ModifiedDate,
                            TaskItemId = item.Id,
                            TaskItemStatusId = item.TaskItemStatusId,
                            ProcessResult = dto.Description
                        };
                        _objectRepository.Modify(item, new List<Expression<Func<TaskItem, object>>>() { p => p.ModifiedBy, p => p.ModifiedDate, p => p.TaskItemStatusId });
                        _taskItemHistoryRepository.Add(taskChildHistory);
                    }
                    await scope.SaveChangesAsync();
                    sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);
                    return sendMessage;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }
        public async Task<TaskItemDetailDto> RenderProjectTask(Guid Id, UserDto currentUser = default)
        {
            var result = new TaskItemDetailDto();

            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var departments = await _departmentServices.GetDepartmentsAsync();
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var queryable = _objectRepository.GetAll();
                result = queryable
                    .Include(x => x.Project)
                    .Include(x => x.Parent)
                    .Include(x => x.Children)
                    .Include(x => x.TaskItemStatus)
                    .Include(x => x.TaskItemPriority)
                    .Include(x => x.TaskItemAssigns.Select(y => y.TaskItemStatus))
                    .Include(x => x.TaskItemAssigns.Select(y => y.TaskItemProcessHistories))
                    .Where(x => x.Id == Id)
                    .Select(x => new TaskItemDetailDto
                    {
                        ProjectId = x.ProjectId.ToString(),
                        TaskItemId = x.Id.ToString(),
                        TaskItemName = x.TaskName,
                        TaskItemFromDate = x.FromDate,
                        TaskItemToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        TaskItemStatus = x.TaskItemStatusId,
                        IsGroupLabel = x.IsGroupLabel,
                        AssignBy = x.AssignBy,
                        DepartmentId = x.DepartmentId,
                        TaskItemCategory = x.TaskItemCategory,
                        TaskItemConclusion = x.Conclusion,
                        ProjectSummary = x.Project.Summary,
                        TaskItemPriorityId = x.TaskItemPriorityId ?? TaskItemPriorityId.Normal,
                        TaskItemParentId = x.ParentId.HasValue ? x.ParentId.Value.ToString() : "",
                        TaskItemParentName = x.Parent != null ? x.Parent.TaskName : "",
                        TaskItemStatusName = x.TaskItemStatus != null ? x.TaskItemStatus.Name : "",
                        PercentFinish = x.PercentFinish,
                        IsSecurity = x.IsSecurity,
                        TaskItemPriorityName = x.TaskItemPriority.Name,
                        NatureTaskId = x.NatureTaskId,
                        CountHistory = x.TaskItemProcessHistories.Count(y => y.TaskItemAssignId == null || y.TaskItemAssignId == Guid.Empty),
                        TaskItemAssigns = x.TaskItemAssigns
                            .OrderBy(y => y.TaskType)
                            .Select(y => new TaskItemAssignDto()
                            {
                                Id = y.Id,
                                TaskType = y.TaskType,
                                AssignTo = y.AssignTo,
                                DepartmentId = y.DepartmentId,
                                ModifiedDate = y.ModifiedDate,
                                LastResult = y.LastResult,
                                TaskItemStatus = new TaskItemStatusDto()
                                {
                                    Id = y.TaskItemStatus == null ? 0 : y.TaskItemStatus.Id,
                                    Name = y.TaskItemStatus == null ? "" : y.TaskItemStatus.Name
                                },
                                FinishedDate = y.FinishedDate,
                                TaskItemStatusId = y.TaskItemStatusId,
                                ToDate = y.ToDate,
                                AppraisePercentFinish = y.AppraisePercentFinish,
                                PercentFinish = y.PercentFinish
                            }).ToList()

                    }).FirstOrDefault();

                result.CountComment = _commentService.Count(Id);
                result.Attachments = await _attachmentService.GetAttachments(new Guid(result.ProjectId), new Guid(result.TaskItemId), Source.TaskItem);
                result.AttachmentChildren = await _attachmentService.GetAllAttachmentChilds(new Guid(result.ProjectId), new Guid(result.TaskItemId));
                if (result.NatureTaskId != null)
                {
                    List<EnumNatureTask> EnumNatureTasks = Enum.GetValues(typeof(EnumNatureTask)).Cast<EnumNatureTask>().ToList();
                    //List<EnumNatureTask> EnumNatureTasks = new List<EnumNatureTask>();
                    var enumNatureTask = EnumNatureTasks.FirstOrDefault(e => (int)e == result.NatureTaskId.Value);
                    result.NatureTaskName = enumNatureTask.GetType().GetMember(enumNatureTask.ToString()).First().GetCustomAttribute<DisplayAttribute>()
                    .GetName();
                }

                result.TaskItemFromDateFormat = ConvertToStringExtensions.DateToString(result.TaskItemFromDate, result.TaskItemToDate);

                var userDeparmentForAssignBy = await _projectService.GetUserDeptDTO(result.AssignBy, result.DepartmentId, userDepartments);

                if (result.TaskItemStatus != TaskItemStatusId.Finished)
                {
                    var assignPrimary = result.TaskItemAssigns.FirstOrDefault(e => e.TaskType == TaskType.Primary);

                    if (assignPrimary != null && (!result.PercentFinish.HasValue || result.PercentFinish.Value <= 0) && result.PercentFinish <= 0)
                    {
                        result.PercentFinish = assignPrimary.PercentFinish > 0 ? assignPrimary.PercentFinish : assignPrimary.PercentFinish;
                    }
                }

                //result.TaskItemAssignPercentFinish = result.PercentFinish <= 0 ? "" : result.PercentFinish.Value.ToString() + "%";

                result.AssignByFullName = userDeparmentForAssignBy?.FullName;
                result.AssignByJobTitleName = userDeparmentForAssignBy?.JobTitleName;

                foreach (var taskItemAssign in result.TaskItemAssigns)
                {
                    taskItemAssign.ModifiedDateFormat = taskItemAssign.ModifiedDate?.ToString("dd/MM/yy");

                    var userDeparmentForAssignTo = await _projectService.GetUserDeptDTO(taskItemAssign.AssignTo, taskItemAssign.DepartmentId, userDepartments);

                    taskItemAssign.AssignToFullName = userDeparmentForAssignTo?.FullName;
                    taskItemAssign.AssignToJobTitleName = userDeparmentForAssignTo?.JobTitleName;
                    taskItemAssign.Attachments = _attachmentService.GetAllAttachments(new Guid(result.ProjectId), taskItemAssign.Id, Source.TaskItemAssign);
                    if(taskItemAssign.Attachments!=null && taskItemAssign.Attachments.Any())
                    {
                        if(result.Attachments == null)
                        {
                            result.Attachments = new List<AttachmentDto>();
                        }
                        result.Attachments.AddRange(taskItemAssign.Attachments);

                    }
                }

                result.HasFinishTaskAssignAction = result.TaskItemStatus != TaskItemStatusId.Cancel && result.TaskItemStatus != TaskItemStatusId.Finished;
                var lstParams = new List<string>();
                lstParams.Add($"@ParentId:{Id}");
                var pagingData = _projectService.GetTaskWithFilterPaging(
                                        null,
                                        lstParams,
                                        1,
                                        20,
                                        " CreatedDate DESC ",
                                        currentUser,
                                        true);

                result.Children = pagingData.Result.ToList();
                foreach (var child in result.Children)
                {
                    child.FromDateFormat = ConvertToStringExtensions.DateToString(child.FromDate, child.ToDate);
                    var userAssignBy = await _projectService.GetUserDeptDTO(child.AssignBy, child.DepartmentId, userDepartments);
                    child.FullName = userAssignBy?.FullName;
                    child.JobTitle= userAssignBy?.JobTitleName;

                    var userAssignTo = await _projectService.GetUserDeptDTO(child.AssignTo, child.AssignToDeparmentId, userDepartments);
                    child.AssignToFullName = userAssignTo?.FullName;
                    child.AssignToJobTitle = userAssignTo?.JobTitleName;
                }

            }

            await _projectService.MarkTaskItemAssignAsReadAsync(new Guid(result.ProjectId), currentUser.Id, new Guid(result.TaskItemId));
            return result;
        }
        public async Task<List<TaskItemProcessHistoryDto>> GetTaskHistories(QueryCommonDto query)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                var UserDto = _userServices.GetUsers();
                var actions = _actionRepository.GetAll().ToList();
                var attachments = await _attachmentRepository.GetAll().Where(e => e.ProjectId == query.ProjectId).Select(e => new AttachmentDto()
                {
                    Id = e.Id,
                    FileExt = e.FileExt,
                    ItemId = e.ItemId,
                    ProjectId = e.ProjectId,
                    Source = e.Source,
                    FileName = e.FileName
                }).ToListAsync();
                var taskItemProcessHistoryDtos = new List<TaskItemProcessHistoryDto>();
                var queryable = _taskItemHistoryRepository.GetAll();
                queryable = queryable.Where(e => e.ProjectId == query.ProjectId); // && e.TaskItemAssignId == null || e.TaskItemAssignId == Guid.Empty
                if (query.TaskItemId.HasValue)
                {
                    queryable = queryable.Where(e => e.TaskItemId == query.TaskItemId);
                }
                if (query.UserId != null)
                {
                    queryable = queryable.Where(e => e.CreatedBy == query.UserId);
                }
                if (query.FromDate != null)
                {
                    queryable = queryable.Where(e => e.CreatedDate >= query.FromDate);
                }

                if (query.ToDate != null)
                {
                    queryable = queryable.Where(e => e.CreatedDate <= query.ToDate);
                }

                queryable = queryable.OrderByDescending(e => e.CreatedDate);
                queryable = queryable.Skip(query.PageSize * (query.PageIndex - 1));
                queryable = queryable.Take(query.PageSize);
                var taskItemProcessHistorys = queryable.ToList();
                taskItemProcessHistoryDtos = _mapper.Map<List<TaskItemProcessHistoryDto>>(taskItemProcessHistorys);

                foreach (var item in taskItemProcessHistoryDtos)
                {
                    var user = await _projectService.GetUserDeptDTO(item.CreatedBy, null, userDepartments);
                    item.CreatedByFullName = user?.FullName;
                    item.CreatedByJobTitle = user?.JobDescription;
                    if (item.CreatedDate != null)
                        item.CreatedDateFormat = ConvertToStringExtensions.DateTimeToString(item.CreatedDate);
                    item.Attachments = attachments.Where(e => e.ItemId != null && e.ItemId == item.Id).ToList();
                    item.Action = new ActionDto();
                    if (item.ActionId.HasValue)
                        item.Action.Name = actions.FirstOrDefault(e => e.Id == item.ActionId.Value).Name;
                }

                return taskItemProcessHistoryDtos;
            }
        }
        public async Task<List<TaskItemAssignDto>> GetTaskItemAssignChildrens(Guid taskItemId)
        {
            List<TaskItemAssignDto> TaskItemAssigns = new List<TaskItemAssignDto>();
            try
            {
                using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var taskCtx = dbCtxScope.DbContexts.Get<TaskContext>();
                    var relatedIds = await taskCtx.Database.SqlQuery<Guid>(
                        string.Format(@"
                            WITH [TaskItemHierarchy]([TaskItemId],[ParentId])
                            AS
                            (
                               SELECT 
	                              Ti1.Id, Ti1.ParentId
                               FROM
                                  [Task].[TaskItem] Ti1
                               WHERE
                                  Ti1.Id = '{0}'
                               UNION ALL
                               SELECT
                                  Ti2.Id, Ti2.ParentId
                               FROM
                                  [Task].[TaskItem] Ti2
                                  INNER JOIN [TaskItemHierarchy] CTE ON CTE.TaskItemId = Ti2.ParentId
                            )
                            SELECT TaskItemId FROM [TaskItemHierarchy]", taskItemId),
                        new object[] { }).ToListAsync();
                    var models = await _taskItemAssignRepository.GetAsync(
                                    tr => tr.TaskItemId != null && relatedIds.Contains(tr.TaskItemId.Value),
                                   new IncludingQuery<TaskItemAssign>(new List<Expression<Func<TaskItemAssign, object>>>
                                   {
                                   i => i.TaskItem
                                   }));
                    TaskItemAssigns = _mapper.Map<List<TaskItemAssignDto>>(models.OrderBy(o => o.TaskItem.CreatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return TaskItemAssigns;
        }
        public async Task<List<ItemActionDto>> GetListItemActionTask(Guid projectId, Guid taskItemId, Guid userId, bool isFullControl)
        {
            var itemActions = new List<ItemActionDto>();

            var taskItem = await GetById(taskItemId);
            var project = await _projectService.GetById(projectId);
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.Create())
            {
                //assing task báo cáo, xử lý,
                var taskItemAssign = await dbContextReadOnlyScope.DbContexts
                                .Get<TaskContext>()
                                .Set<TaskItemAssign>()
                                .Where(x => x.TaskItemId == taskItemId && !x.IsDeleted && x.TaskItemStatusId != TaskItemStatusId.Cancel)
                                .ToListAsync();
                var managerInTask = new List<Guid?>();
                managerInTask.Add(project.ApprovedBy);
                if (!string.IsNullOrEmpty(project.ManagerId))
                {
                    managerInTask.AddRange(project.ManagerId.Split(';').Select(e => new Guid?(new Guid(e))));
                }
                // giao việc
                if ((managerInTask.Contains(userId)
                    || ListTaskParentHasUser(taskItemId, userId) == 1 || isFullControl)
                    && taskItem.TaskItemStatusId.GetValueOrDefault() != TaskItemStatusId.Finished
                    )
                {
                    itemActions.Add(new ItemActionDto { Code = ActionClick.Assign.ToString(), Name = "Giao việc" });
                }
                // duyệt báo cáo 
                if (taskItemAssign.Any(x => x.TaskItemStatusId == TaskItemStatusId.Report) && taskItem.IsGroupLabel != true)
                {
                    var list = CheckUserAssignTask(taskItemId, userId);
                    if (list.Any() || isFullControl)
                    {
                        itemActions.Add(new ItemActionDto { Code = ActionClick.Appraise.ToString(), Name = "Duyệt báo cáo" });
                    }
                }

                //duyệt gian hạn
                if (taskItemAssign.Any(x => x.IsExtend == true) && taskItem.IsGroupLabel != true)
                {
                    var list = CheckUserAssignTask(taskItemId, userId);
                    if (list.Any() || isFullControl)
                    {
                        itemActions.Add(new ItemActionDto { Code = ActionClick.AppraiseExtend.ToString(), Name = "Duyệt Gia hạn" });
                    }
                }

                // duyệt trả lại
                if (taskItemAssign.Any(x => x.TaskItemStatusId == TaskItemStatusId.ReportReturn) && taskItem.IsGroupLabel != true)
                {
                    var list = CheckUserAssignTask(taskItemId, userId);
                    if (list.Any() || isFullControl)
                    {
                        itemActions.Add(new ItemActionDto { Code = ActionClick.ApproveReturn.ToString(), Name = "Duyệt trả lại" });
                    }
                }

                //xử lý
                if (taskItemAssign.Any(x => x.AssignTo == userId
                 && (x.TaskItemStatusId == TaskItemStatusId.New
                     || x.TaskItemStatusId == TaskItemStatusId.InProcess
                     || x.TaskItemStatusId == TaskItemStatusId.Read)) && taskItem.IsGroupLabel != true && taskItem.TaskItemStatusId.GetValueOrDefault() != TaskItemStatusId.Draft)
                    itemActions.Add(new ItemActionDto { Code = ActionClick.Process.ToString(), Name = "Xử lý" });

                //chỉnh sửa
                if ((taskItem.AssignBy == userId || isFullControl) && taskItem.TaskItemStatusId != TaskItemStatusId.Finished)
                {
                    itemActions.Add(new ItemActionDto { Code = ActionClick.EditDoc.ToString(), Name = "Chỉnh sửa" });
                    itemActions.Add(new ItemActionDto { Code = ActionClick.DeleteDoc.ToString(), Name = "Xóa" });
                    if (project.ProjectStatusId != ProjectStatusId.Finished)
                    {
                        itemActions.Add(new ItemActionDto { Code = ActionClick.MoveTask.ToString(), Name = "Di chuyển" });
                    }
                }

                //thu hồi
                if (taskItemAssign.Any(x => x.TaskItemStatusId == TaskItemStatusId.New) && (taskItem.AssignBy == userId || isFullControl)/* && taskItem.IsGroupLabel != true*/)
                    itemActions.Add(new ItemActionDto { Code = ActionClick.Revoke.ToString(), Name = "Thu hồi" });
                //Bàn giao
                if (taskItemAssign.Any(x => x.AssignTo == userId
                 && (x.TaskItemStatusId == TaskItemStatusId.New
                     || x.TaskItemStatusId == TaskItemStatusId.InProcess
                     || x.TaskItemStatusId == TaskItemStatusId.Read))/* && taskItem.IsGroupLabel != true*/)
                    itemActions.Add(new ItemActionDto { Code = ActionClick.HandOverTask.ToString(), Name = "Bàn giao" });

                // Import Excel
                if ((managerInTask.Contains(userId)
                    || ListTaskParentHasUser(taskItemId, userId) == 1 || isFullControl)
                    && taskItem.TaskItemStatusId.GetValueOrDefault() != TaskItemStatusId.Finished
                    )
                {
                    itemActions.Add(new ItemActionDto { Code = ActionClick.ImportDoc.ToString(), Name = "Import Excel" });
                }
            }
            //Chuyển vào thư mục    
            itemActions.Add(new ItemActionDto
            {
                Code = ActionClick.ExportDoc.ToString(),
                Name = "Xuất excel"
            });
            //Chuyển vào thư mục    
            itemActions.Add(new ItemActionDto
            {
                Code = ActionClick.MoveToFolder.ToString(),
                Name = "Copy vào thư mục"
            });

            return itemActions;
        }
        private int ListTaskParentHasUser(Guid taskId, Guid userId)
        {
            List<int> rs = new List<int>();
            using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbCtxScope.DbContexts.Get<TaskContext>();
                rs = dbContext.Database.SqlQuery<int>(string.Format(@"
                EXEC dbo.SP_CHECK_USER_IS_PARENT_TASK_BY_TASK_ID
		            @CurrentUserId = '{0}',
		            @TaskId = '{1}'", userId, taskId)).ToList();
            }
            return rs.FirstOrDefault();
        }
        private List<Guid> CheckUserAssignTask(Guid taskId, Guid userId)
        {
            List<Guid> models = new List<Guid>();
            using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbCtxScope.DbContexts.Get<TaskContext>();

                models = dbContext.Database.SqlQuery<Guid>(
                  string.Format(@"
                EXEC dbo.SP_CHECK_USER_ASSIGN_TASK
		            @CurrentUserId = '{0}',
		            @TaskId = '{1}'", userId, taskId),
                          new object[] { }).ToList();
            }
            return models;
        }

        public async Task<List<TaskItemDto>> AllTaskItemChildren(Guid Id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<TaskContext>();
                IReadOnlyList<Guid> taskItemIds = new List<Guid>();
                taskItemIds = dbContext.Database.SqlQuery<Guid>(
                  string.Format(@"
                EXEC dbo.SP_Select_TaskItem_Children
		            @Id= '{0}'", Id),
                          new object[] { }).ToList();


                var userDepartDto = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var asyncTaskItem = await _objectRepository.GetAll()
                       .Include(x => x.TaskItemStatus)
                       .Where(x => taskItemIds.Contains(x.Id) && x.IsGroupLabel != true && !x.IsDeleted)
                       .Select(e => new TaskItemDto()
                       {
                           Id = e.Id,
                           TaskName = e.TaskName,
                           ProjectId = e.ProjectId,
                           FromDate = e.FromDate,
                           ToDate = e.ToDate,
                           FinishedDate = e.FinishedDate,
                           TaskItemStatusId = e.TaskItemStatusId,
                           CreatedDate = e.CreatedDate,
                           CreatedBy = e.CreatedBy,
                           AssignBy = e.AssignBy,
                           ParentId = e.ParentId,
                           PercentFinish = e.PercentFinish,
                           TaskType = e.TaskType,
                           IsReport = e.IsReport,
                           ModifiedDate = e.ModifiedDate,
                           ModifiedBy = e.ModifiedBy,
                           Conclusion = e.Conclusion,
                           TaskItemPriorityId = e.TaskItemPriorityId,
                           DepartmentId = e.DepartmentId,
                           TaskItemCategory = e.TaskItemCategory,
                           IsSecurity = e.IsSecurity,
                           IsWeirdo = e.IsWeirdo,
                           HasRecentActivity = e.HasRecentActivity,
                           Weight = e.Weight,
                           IsGroupLabel = e.IsGroupLabel,
                           IsDeleted = e.IsDeleted,
                           NatureTaskId = e.NatureTaskId,
                           Order = e.Order,
                           StatusName = e.TaskItemStatus.Name
                       }).ToListAsync();

                var taskItemDtos = _mapper.Map<List<TaskItemDto>>(asyncTaskItem);
                foreach (var x in taskItemDtos)
                {
                    var assignBy = await _projectService.GetUserDeptDTO(x.AssignBy, x.DepartmentId, userDepartDto);
                    x.AssignByFullName = assignBy?.FullName;
                    x.DepartmentName = assignBy?.DeptName;
                    x.JobTitleName = assignBy?.JobTitleName;
                }

                return taskItemDtos;
            }
        }

        public async Task<List<UserReportInProjectDto>> UserReportInTask(Guid id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var rs = new List<UserReportInProjectDto>();
                var userDepartmentDto = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                IReadOnlyList<Guid> taskItemIds = new List<Guid>();
                var dbContext = scope.DbContexts.Get<TaskContext>();
                taskItemIds = await dbContext.Database.SqlQuery<Guid>(
                  string.Format(@"
                EXEC dbo.SP_Select_TaskItem_Children
		            @Id= '{0}'", id),
                          new object[] { }).ToListAsync();

                var taskRoot = await _objectRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
                if (taskRoot != null)
                {
                    var assignBy = await _projectService.GetUserDeptDTO(taskRoot.AssignBy, null, userDepartmentDto);

                    var userAssignBy = new UserReportInProjectDto()
                        .AddUser(taskRoot.AssignBy, null, assignBy?.FullName,
                        assignBy?.JobTitleName, PositionInProject.Manager);

                    rs.Add(userAssignBy);

                    var taskAssigns = await dbContext.Set<TaskItemAssign>()
                       .Where(x => taskItemIds.Contains(x.TaskItem.Id) && (!x.TaskItem.IsGroupLabel.HasValue || x.TaskItem.IsGroupLabel == false) && !x.TaskItem.IsDeleted)
                       .ToListAsync();

                    if (taskAssigns.Any())
                    {
                        foreach (var assign in taskAssigns)
                        {
                            var userInProject = new UserReportInProjectDto();
                            switch (assign.TaskItemStatusId)
                            {
                                case TaskItemStatusId.New:
                                    userInProject.New = 1;
                                    break;
                                case TaskItemStatusId.Read:
                                    userInProject.Read = 1;
                                    break;
                                case TaskItemStatusId.Report:
                                case TaskItemStatusId.ReportReturn:
                                case TaskItemStatusId.Extend:
                                    userInProject.Report = 1;
                                    break;
                                case TaskItemStatusId.InProcess:
                                    userInProject.Process = 1;
                                    break;
                                case TaskItemStatusId.Finished:
                                case TaskItemStatusId.Cancel:
                                    userInProject.Finsh = 1;
                                    break;
                            }
                            if (TaskInDueDate.Main(assign.ToDate, assign.FinishedDate, assign.TaskItemStatusId))
                                userInProject.InDueDate = 1;
                            else userInProject.OutOfDate = 1;

                            if (rs.Any(x => x.UserId == assign.AssignTo))
                            {
                                var current = rs.FirstOrDefault(x => x.UserId == assign.AssignTo);
                                current.InDueDate += userInProject.InDueDate;
                                current.OutOfDate += userInProject.OutOfDate;
                                current.New += userInProject.New;
                                current.Process += userInProject.Process;
                                current.Read += userInProject.Read;
                                current.Report += userInProject.Report;
                                current.Finsh += userInProject.Finsh;
                            }
                            else
                            {
                                var userAssign = await _projectService.GetUserDeptDTO(assign.AssignTo, assign.DepartmentId, userDepartmentDto);

                                userInProject.UserId = assign.AssignTo;
                                userInProject.DepartmentId = assign.DepartmentId;
                                userInProject.UserName = userAssign?.FullName;
                                userInProject.JobTitleName = userAssign?.JobTitleName;
                                userInProject.Type = PositionInProject.AssignTo;
                                rs.Add(userInProject);
                            }
                        }
                    }
                }
                return rs;
            }
        }

        public async Task<bool> ReturnTaskItemAssign(Guid taskItemAssignId, Guid userId)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var task = _taskItemAssignRepository.GetAll().FirstOrDefault(e => e.Id == taskItemAssignId && e.TaskItem.AssignBy == userId);
                task.TaskItemStatusId = TaskItemStatusId.Cancel;
                _taskItemAssignRepository.Modify(task);
                scope.SaveChanges();
                return true;
            }
        }
        public async Task<TaskItemDto> GetDateRangeForTask(Guid projectId, Guid? taskId, IReadOnlyList<TaskItemDto> taskItems = null)
        {
            TaskItemDto task = new TaskItemDto();
            try
            {
                using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    if (!taskId.HasValue || taskId == Guid.Empty)
                    {
                        var project = await _projectService.GetById(projectId);
                        task.FromDate = project.FromDate;
                        task.ToDate = project.ToDate;
                        return task;
                    }
                    if (taskItems == null)
                    {
                        var taskItemEntities = _objectRepository.Get(e => e.ProjectId == projectId);
                        taskItems = _mapper.Map<List<TaskItemDto>>(taskItemEntities);
                    }
                    task = taskItems.Where(e => e.Id == taskId).FirstOrDefault();
                    if (!task.IsGroupLabel.HasValue || !task.IsGroupLabel.Value)
                    {
                        return task;
                    }
                    else
                    {
                        task = await GetDateRangeForTask(projectId, task.ParentId, taskItems);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return task;
        }
        /// <summary>
        /// Import excel
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> ImportExcelV2Action(List<TaskItemForMSProjectDto> tasks, Guid? projectId, Guid userId, Guid? taskId)
        {
            bool rs = false;
            try
            {
                var cultureName = Thread.CurrentThread.CurrentUICulture.Name;
                var UserDtos = _userServices.GetUsers();
                var deptDTOs = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    IReadOnlyList<TaskItem> taskItems = new List<TaskItem>();
                    IReadOnlyList<TaskItem> taskItemAlls = new List<TaskItem>();
                    if (!taskId.HasValue)
                    {
                        taskItems = _objectRepository
                        .Get(
                            x => x.ProjectId == projectId,
                            new IncludingQuery<TaskItem>(new List<Expression<Func<TaskItem, object>>> {
                            x => x.TaskItemAssigns
                            }));
                    }
                    else
                    {
                        List<Guid> taskIds = GetTaskByParents(taskId.Value);
                        if (taskIds.Any())
                        {
                            taskItemAlls = _objectRepository
                        .Get(
                            x => x.ProjectId == projectId,
                            new IncludingQuery<TaskItem>(new List<Expression<Func<TaskItem, object>>> {
                            x => x.TaskItemAssigns
                            }));
                            taskItems = taskItemAlls.Where(x => taskIds.Contains(x.Id)).ToList();
                        }
                    }
                    List<Guid> ids = taskItems.Select(e => e.Id).ToList();
                    List<Guid> idAlls = taskItemAlls.Select(e => e.Id).ToList();
                    List<Guid> idDtos = tasks.Select(e => e.Id).ToList();
                    List<TaskItemForMSProjectDto> adds = new List<TaskItemForMSProjectDto>();
                    List<TaskItem> taskUpdates = new List<TaskItem>();
                    List<TaskItem> TaskAdds = new List<TaskItem>();

                    taskUpdates = taskItems.Where(e => idDtos.Any(i => i == e.Id)).ToList();
                    if (taskId.HasValue)
                    {
                        taskUpdates = taskUpdates.Where(e => e.AssignBy == userId/* || e.TaskItemAssigns.Any(a => a.AssignTo == userId && a.TaskType == TaskType.Primary)*/).ToList();
                    }
                    if (taskId.HasValue)
                    {
                        adds = tasks.Where(e => !idAlls.Any(i => i == e.Id)).ToList();
                    }
                    else
                    {
                        adds = tasks.Where(e => !ids.Any(i => i == e.Id)).ToList();
                    }

                    foreach (var item in taskUpdates)
                    {
                        var taskAssignHistories = new List<TaskItemProcessHistory>();

                        string history = "";
                        string descriptHistoryRemoveUser = "";
                        TaskItemForMSProjectDto taskDto = tasks.Where(e => e.Id == item.Id).FirstOrDefault();
                        item.TaskName = taskDto.Name;
                        item.Conclusion = taskDto.Content;
                        item.ModifiedBy = userId;
                        item.FromDate = taskDto.StartTime;
                        if (!taskId.HasValue || taskDto.ParentId != Guid.Empty)
                        {
                            item.ParentId = taskDto.ParentId;
                        }
                        item.ToDate = taskDto.EndTime;
                        item.Order = taskDto.Order;
                        item.NatureTaskId = taskDto.NatureTask;
                        UserDto assigner = null;
                        if (!string.IsNullOrEmpty(taskDto.AssignByUsername))
                        {
                            assigner = UserDtos.Where(p => p.UserName.ToLower().Contains(taskDto.AssignByUsername.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                        }

                        if (assigner != null)
                        {
                            item.AssignBy = assigner.Id;
                        }
                        item.TaskItemPriorityId = (TaskItemPriorityId?)taskDto.TaskItemPriorityId;
                        if (!taskDto.IsGroupLabel.HasValue || !taskDto.IsGroupLabel.Value)
                        {
                            item.IsReport = taskDto.HasReport;

                            var workerAdds = taskDto.MainWorkers;
                            List<TaskItemAssign> assigerAdds = new List<TaskItemAssign>();
                            List<TaskItemAssign> assigerUpdates = _taskItemAssignRepository.Get(e => e.TaskItemId == item.Id, null, null, null).ToList();
                            foreach (var work in workerAdds)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();

                                    TaskItemAssign taskItemAssign = new TaskItemAssign();
                                    if (assigerUpdates.Any(e => e.AssignTo == user.Id && e.TaskType == TaskType.Primary))
                                    {
                                        taskItemAssign = assigerUpdates.Where(e => e.AssignTo == user.Id && e.TaskType == TaskType.Primary).FirstOrDefault();
                                        taskItemAssign.ProjectId = projectId;
                                        taskItemAssign.TaskItemId = item.Id;
                                        taskItemAssign.DepartmentId = dept != null ? (Guid?)dept.DeptID : null;
                                        taskItemAssign.FromDate = item.FromDate;
                                        taskItemAssign.ToDate = item.ToDate;
                                        taskItemAssign.FinishedDate = work.FinishedDate;
                                        taskItemAssign.TaskType = TaskType.Primary;
                                        taskItemAssign.ModifiedDate = DateTime.Now;


                                    }
                                    else
                                    {
                                        taskItemAssign = new TaskItemAssign()
                                        {
                                            AssignTo = user.Id,
                                            ProjectId = projectId,
                                            TaskItemId = item.Id,
                                            Id = Guid.NewGuid(),
                                            DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                            FromDate = item.FromDate,
                                            ToDate = item.ToDate,
                                            FinishedDate = work.FinishedDate,
                                            TaskType = TaskType.Primary,
                                            TaskItemStatusId = (TaskItemStatusId.New),
                                            ModifiedDate = DateTime.Now
                                        };
                                        if (!assigerAdds.Any())
                                        {
                                            taskAssignHistories.Add(new TaskItemProcessHistory()
                                            {
                                                Id = Guid.NewGuid(),
                                                ProjectId = projectId,
                                                TaskItemId = item.Id,
                                                TaskItemAssignId = taskItemAssign.Id,
                                                ProcessResult = "",
                                                CreatedDate = DateTime.Now,
                                                CreatedBy = userId,
                                                TaskItemStatusId = taskItemAssign.TaskItemStatusId,
                                                ActionId = ActionId.Import
                                            });

                                        }
                                    }
                                    assigerAdds.Add(taskItemAssign);
                                }
                            }
                            if (assigerAdds.Count > 1)
                            {
                                assigerAdds = new List<TaskItemAssign>()
                                {
                                    assigerAdds.FirstOrDefault()
                                };
                            }

                            var workerSups = taskDto.Supporters;
                            foreach (var work in workerSups)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign();
                                    if (assigerUpdates.Any(e => e.AssignTo == user.Id && e.TaskType == TaskType.Support))
                                    {
                                        taskItemAssign = assigerUpdates.Where(e => e.AssignTo == user.Id && e.TaskType == TaskType.Support).FirstOrDefault();
                                        taskItemAssign.ProjectId = projectId;
                                        taskItemAssign.TaskItemId = item.Id;
                                        taskItemAssign.DepartmentId = dept != null ? (Guid?)dept.DeptID : null;
                                        taskItemAssign.FromDate = item.FromDate;
                                        taskItemAssign.ToDate = item.ToDate;
                                        taskItemAssign.FinishedDate = work.FinishedDate;
                                        taskItemAssign.TaskType = TaskType.Support;
                                        taskItemAssign.ModifiedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        taskItemAssign = new TaskItemAssign()
                                        {
                                            AssignTo = user.Id,
                                            ProjectId = projectId,
                                            TaskItemId = item.Id,
                                            Id = Guid.NewGuid(),
                                            DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                            FromDate = item.FromDate,
                                            ToDate = item.ToDate,
                                            FinishedDate = work.FinishedDate,
                                            TaskType = TaskType.Support,
                                            TaskItemStatusId = (TaskItemStatusId.New),
                                            ModifiedDate = DateTime.Now
                                        };
                                        taskAssignHistories.Add(new TaskItemProcessHistory()
                                        {
                                            Id = Guid.NewGuid(),
                                            ProjectId = projectId,
                                            TaskItemId = item.Id,
                                            TaskItemAssignId = taskItemAssign.Id,
                                            ProcessResult = "",
                                            CreatedDate = DateTime.Now,
                                            CreatedBy = userId,
                                            TaskItemStatusId = taskItemAssign.TaskItemStatusId,
                                            ActionId = ActionId.Import
                                        });
                                    }
                                    assigerAdds.Add(taskItemAssign);
                                }
                            }


                            ///////
                            var workerKnows = taskDto.WhoOnlyKnow;
                            foreach (var work in workerKnows)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign();
                                    if (assigerUpdates.Any(e => e.AssignTo == user.Id && e.TaskType == TaskType.ReadOnly))
                                    {
                                        taskItemAssign = assigerUpdates.Where(e => e.AssignTo == user.Id && e.TaskType == TaskType.ReadOnly).FirstOrDefault();
                                        taskItemAssign.ProjectId = projectId;
                                        taskItemAssign.TaskItemId = item.Id;
                                        taskItemAssign.DepartmentId = dept != null ? (Guid?)dept.DeptID : null;
                                        taskItemAssign.FromDate = item.FromDate;
                                        taskItemAssign.ToDate = item.ToDate;
                                        taskItemAssign.FinishedDate = work.FinishedDate;
                                        taskItemAssign.TaskType = TaskType.ReadOnly;
                                        taskItemAssign.ModifiedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        taskItemAssign = new TaskItemAssign()
                                        {
                                            AssignTo = user.Id,
                                            ProjectId = projectId,
                                            TaskItemId = item.Id,
                                            Id = Guid.NewGuid(),
                                            DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                            FromDate = item.FromDate,
                                            ToDate = item.ToDate,
                                            FinishedDate = work.FinishedDate,
                                            TaskType = TaskType.ReadOnly,
                                            TaskItemStatusId = (TaskItemStatusId.New),
                                            ModifiedDate = DateTime.Now
                                        };
                                        taskAssignHistories.Add(new TaskItemProcessHistory()
                                        {
                                            Id = Guid.NewGuid(),
                                            ProjectId = projectId,
                                            TaskItemId = item.Id,
                                            TaskItemAssignId = taskItemAssign.Id,
                                            ProcessResult = "",
                                            CreatedDate = DateTime.Now,
                                            CreatedBy = userId,
                                            TaskItemStatusId = taskItemAssign.TaskItemStatusId,
                                            ActionId = ActionId.Import
                                        });
                                    }
                                    assigerAdds.Add(taskItemAssign);
                                }
                            }

                            if (assigerAdds != null && assigerAdds.Any())
                            {
                                var taskDelete = item.TaskItemAssigns.Where(e => !assigerAdds.Select(a => a.Id).Any(i => i == e.Id)).ToList();
                                foreach (var dele in taskDelete)
                                {
                                    var user = UserDtos.Where(p => p.Id == dele.AssignTo).FirstOrDefault();
                                    _taskItemAssignRepository.Delete(dele, x => x.OwnedCollection(y => y.TaskItemAppraiseHistories).OwnedCollection(y => y.TaskItemProcessHistories));
                                }
                                item.TaskItemAssigns = assigerAdds;
                            }
                        }
                        history = history == string.Empty ? descriptHistoryRemoveUser : history + "<br />" + descriptHistoryRemoveUser;
                        var taskItemHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = projectId,
                            TaskItemId = item.Id,
                            ProcessResult = history != string.Empty ? history : item.TaskName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = userId,
                            TaskItemStatusId = item.TaskItemStatusId,
                            ActionId = ActionId.Import
                        };

                        item.TaskItemProcessHistories.Add(taskItemHistory);
                        //foreach (var assHis in taskAssignHistories)
                        //{
                        //    item.TaskItemProcessHistories.Add(assHis);
                        //}

                        _objectRepository.Modify(item);
                    }
                    foreach (var taskDto in adds)
                    {
                        var taskAssignHistories = new List<TaskItemProcessHistory>();
                        TaskItem item = new TaskItem();
                        UserDto assigner = null;
                        if (!string.IsNullOrEmpty(taskDto.AssignByUsername))
                        {
                            assigner = UserDtos.Where(p => p.UserName.ToLower().Contains(taskDto.AssignByUsername.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                        }
                        if (assigner != null)
                        {
                            item.AssignBy = assigner.Id;
                        }
                        else
                        {
                            item.AssignBy = userId;
                        }
                        item.TaskName = taskDto.Name;
                        item.Id = taskDto.Id;
                        item.Order = taskDto.Order;
                        item.IsGroupLabel = taskDto.IsGroupLabel;
                        item.CreatedBy = userId;
                        item.Conclusion = taskDto.Content;
                        item.CreatedDate = DateTime.Now;
                        item.ProjectId = projectId;
                        item.TaskItemStatusId = (TaskItemStatusId.New);
                        item.ParentId = taskDto.ParentId;
                        item.FromDate = taskDto.StartTime;
                        item.ToDate = taskDto.EndTime;
                        item.PercentFinish = taskDto.PercentFinish;
                        item.FinishedDate = taskDto.FinishiDate;
                        item.NatureTaskId = taskDto.NatureTask;
                        item.TaskItemPriorityId = (TaskItemPriorityId?)taskDto.TaskItemPriorityId;
                        if (!taskDto.IsGroupLabel.HasValue || !taskDto.IsGroupLabel.Value)
                        {
                            item.IsReport = taskDto.HasReport;
                            var workerAdds = taskDto.MainWorkers;
                            List<TaskItemAssign> assigerAdds = new List<TaskItemAssign>();
                            foreach (var work in workerAdds)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign()
                                    {
                                        AppraisePercentFinish = work.PercentFinish,
                                        PercentFinish = work.PercentFinish,
                                        AssignTo = user.Id,
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        Id = Guid.NewGuid(),
                                        DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                        FromDate = item.FromDate,
                                        ToDate = item.ToDate,
                                        FinishedDate = work.FinishedDate,
                                        TaskType = TaskType.Primary,
                                        TaskItemStatusId = (TaskItemStatusId.New),
                                        ModifiedDate = DateTime.Now,
                                    };

                                    assigerAdds.Add(taskItemAssign);
                                }
                            }
                            if (assigerAdds != null && assigerAdds.Any())
                            {
                                if (assigerAdds.Count > 1)
                                {
                                    assigerAdds = new List<TaskItemAssign>()
                                    {
                                        assigerAdds.FirstOrDefault()
                                    };

                                }
                                taskAssignHistories.Add(new TaskItemProcessHistory()
                                {
                                    Id = Guid.NewGuid(),
                                    ProjectId = projectId,
                                    TaskItemId = item.Id,
                                    TaskItemAssignId = assigerAdds.FirstOrDefault().Id,
                                    ProcessResult = "",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = userId,
                                    TaskItemStatusId = assigerAdds.FirstOrDefault().TaskItemStatusId,
                                    ActionId = ActionId.Import
                                });
                                item.TaskItemAssigns = item.TaskItemAssigns.Concat(assigerAdds).ToList();
                            }

                            var workerSups = taskDto.Supporters;
                            List<TaskItemAssign> assigerSups = new List<TaskItemAssign>();
                            foreach (var work in workerSups)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign()
                                    {
                                        AppraisePercentFinish = work.PercentFinish,
                                        PercentFinish = work.PercentFinish,
                                        AssignTo = user.Id,
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        Id = Guid.NewGuid(),
                                        DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                        FromDate = item.FromDate,
                                        ToDate = item.ToDate,
                                        FinishedDate = work.FinishedDate,
                                        TaskType = TaskType.Support,
                                        TaskItemStatusId = (TaskItemStatusId.New),
                                        ModifiedDate = DateTime.Now,
                                    };
                                    taskAssignHistories.Add(new TaskItemProcessHistory()
                                    {
                                        Id = Guid.NewGuid(),
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        TaskItemAssignId = taskItemAssign.Id,
                                        ProcessResult = "",
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = userId,
                                        TaskItemStatusId = taskItemAssign.TaskItemStatusId,
                                        ActionId = ActionId.Import
                                    });
                                    assigerSups.Add(taskItemAssign);
                                }
                            }
                            if (workerSups != null && workerSups.Any())
                            {
                                item.TaskItemAssigns = item.TaskItemAssigns.Concat(assigerSups).ToList();
                            }


                            var workerKnows = taskDto.WhoOnlyKnow;
                            List<TaskItemAssign> assigerKnows = new List<TaskItemAssign>();
                            foreach (var work in workerKnows)
                            {
                                UserDto user = null;
                                if (!string.IsNullOrEmpty(work.Username))
                                {
                                    user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Username.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user == null)
                                {
                                    if (!string.IsNullOrEmpty(work.Text))
                                    {
                                        user = UserDtos.Where(p => p.UserName.ToLower().Contains(work.Text.Trim().ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                    }
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign()
                                    {
                                        AppraisePercentFinish = work.PercentFinish,
                                        PercentFinish = work.PercentFinish,
                                        AssignTo = user.Id,
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        Id = Guid.NewGuid(),
                                        DepartmentId = dept != null ? (Guid?)dept.DeptID : null,
                                        FromDate = item.FromDate,
                                        ToDate = item.ToDate,
                                        FinishedDate = work.FinishedDate,
                                        TaskType = TaskType.ReadOnly,
                                        TaskItemStatusId = (TaskItemStatusId.New),
                                        ModifiedDate = DateTime.Now,
                                    };
                                    taskAssignHistories.Add(new TaskItemProcessHistory()
                                    {
                                        Id = Guid.NewGuid(),
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        TaskItemAssignId = taskItemAssign.Id,
                                        ProcessResult = "",
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = userId,
                                        TaskItemStatusId = taskItemAssign.TaskItemStatusId,
                                        ActionId = ActionId.Import
                                    });
                                    assigerKnows.Add(taskItemAssign);
                                }
                            }
                            if (workerKnows != null && workerKnows.Any())
                            {
                                item.TaskItemAssigns = item.TaskItemAssigns.Concat(assigerKnows).ToList();
                            }
                        }
                        var taskItemHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = projectId,
                            TaskItemId = item.Id,
                            ProcessResult = item.TaskName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = item.AssignBy,
                            TaskItemStatusId = TaskItemStatusId.New,
                            ActionId = ActionId.Import
                        };

                        item.TaskItemProcessHistories.Add(taskItemHistory);
                        //foreach (var assHis in taskAssignHistories)
                        //{
                        //    item.TaskItemProcessHistories.Add(assHis);
                        //}

                        _objectRepository.Add(item);
                    }
                    //save taskItem
                    dbContextScope.SaveChanges();
                }
                _projectService.UpdateMSProject(projectId.Value);
                rs = true;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return rs;
        }
        public List<Guid> GetTaskByParents(Guid taskId)
        {
            List<Guid> rs = new List<Guid>();
            using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbCtxScope.DbContexts.Get<TaskContext>();
                rs = dbContext.Database.SqlQuery<Guid>(string.Format(@"
                EXEC dbo.SP_TASK_BY_TASK_PARENT
		            @parentId = '{0}'", taskId)).ToList();
            }
            return rs;
        }
        /// <summary>
        /// Import ms project
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="projectId"></param>
        /// <param name="isAll"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ImportMSProject(List<TaskItemForMSProjectDto> tasks, Guid? projectId, bool isAll, Guid userId)
        {
            bool result = false;
            try
            {
                var userDTOs = _userServices.GetUsers();
                var deptDTOs = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                string domain = ConfigurationManager.AppSettings["DomainName"].ToString();
                List<string> domains = domain.Split(';').ToList();
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    var taskItems = _objectRepository
                        .Get(
                            x => x.ProjectId == projectId,
                            new IncludingQuery<TaskItem>(new List<Expression<Func<TaskItem, object>>> {
                            x => x.TaskItemAssigns
                            }));
                    List<Guid> ids = taskItems.Select(e => e.Id).ToList();
                    List<Guid> idDtos = tasks.Select(e => e.Id).ToList();
                    List<TaskItemForMSProjectDto> adds = new List<TaskItemForMSProjectDto>();
                    List<TaskItem> taskUpdates = new List<TaskItem>();
                    List<TaskItem> TaskAdds = new List<TaskItem>();

                    taskUpdates = taskItems.Where(e => idDtos.Any(i => i == e.Id)).ToList();
                    adds = tasks.Where(e => !ids.Any(i => i == e.Id)).ToList();
                    foreach (var item in taskUpdates)
                    {
                        TaskItemForMSProjectDto taskDto = tasks.Where(e => e.Id == item.Id).FirstOrDefault();
                        item.TaskName = taskDto.Name;
                        item.ModifiedBy = userId;
                        item.FromDate = taskDto.StartTime;
                        item.ParentId = taskDto.ParentId;
                        item.ToDate = taskDto.EndTime;
                        item.PercentFinish = taskDto.PercentFinish;
                        item.FinishedDate = taskDto.FinishiDate;
                        item.Order = taskDto.Order;
                        if (!string.IsNullOrEmpty(taskDto.AssignByUsername))
                        {
                            string assignName = taskDto.AssignByUsername.Substring(taskDto.AssignByUsername.IndexOf("\\") + 1);
                            List<string> assignNames = domains.Select(e => (domain + "\\" + assignName).Trim().ToLower()).ToList();
                            var assigner = userDTOs.Where(p => assignNames.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                            if (assigner != null)
                            {
                                item.AssignBy = assigner.Id;
                            }

                        }
                        if (!taskDto.IsGroupLabel.HasValue || !taskDto.IsGroupLabel.Value)
                        {
                            var workerAdds = taskDto.MainWorkers.Where(a => !a.ID.HasValue);
                            var workerUpdates = taskDto.MainWorkers.Where(a => a.ID.HasValue);
                            List<TaskItemAssign> assigerAdds = new List<TaskItemAssign>();
                            foreach (var work in workerAdds)
                            {
                                UserDto user = null;
                                string username = work.Username.Substring(work.Username.IndexOf("\\") + 1);
                                List<string> userNames = domains.Select(e => (domain + "\\" + username).Trim().ToLower()).ToList();
                                user = userDTOs.Where(p => userNames.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();

                                if (user == null)
                                {
                                    string text = work.Text.Substring(work.Username.IndexOf("\\") + 1);
                                    List<string> userTexts = domains.Select(e => (domain + "\\" + text).Trim().ToLower()).ToList();
                                    user = userDTOs.Where(p => userTexts.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign()
                                    {
                                        AppraisePercentFinish = work.PercentFinish,
                                        PercentFinish = work.PercentFinish,
                                        AssignTo = user.Id,
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        Id = Guid.NewGuid(),
                                        DepartmentId = dept.DeptID,
                                        FromDate = item.FromDate,
                                        ToDate = item.ToDate,
                                        FinishedDate = work.FinishedDate,
                                        TaskType = TaskType.Primary,
                                        TaskItemStatusId = (TaskItemStatusId.New),
                                        ModifiedDate = DateTime.Now
                                    };
                                    assigerAdds.Add(taskItemAssign);
                                }
                            }
                            var ListAssignUpdateIds = workerUpdates.Select(e => e.ID.Value).ToList();
                            List<TaskItemAssign> assigerUpdates = _taskItemAssignRepository.Get(e => ListAssignUpdateIds.Contains(e.Id), null, null, null).ToList();
                            assigerUpdates.ForEach(delegate (TaskItemAssign tas)
                            {
                                var update = workerUpdates.Where(e => e.ID == tas.Id).FirstOrDefault();
                                tas.AppraisePercentFinish = update.PercentFinish;
                                tas.PercentFinish = update.PercentFinish;
                                tas.ProjectId = projectId;
                                tas.TaskItemId = item.Id;
                                tas.FromDate = item.FromDate;
                                tas.ToDate = item.ToDate;
                                tas.FinishedDate = update.FinishedDate;
                                tas.ModifiedDate = DateTime.Now;
                            });
                            if (workerAdds != null && workerAdds.Any())
                            {
                                item.TaskItemAssigns = assigerAdds;
                            }
                            if (workerUpdates != null && workerUpdates.Any())
                            {
                                item.TaskItemAssigns = item.TaskItemAssigns.Concat(assigerUpdates).ToList();
                            }
                            foreach (var ass in assigerUpdates)
                            {
                                _taskItemAssignRepository.Modify(ass);
                            }
                        }
                        var taskItemHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = projectId,
                            TaskItemId = item.Id,
                            ProcessResult = item.TaskName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = item.AssignBy,
                            TaskItemStatusId = item.TaskItemStatusId,
                            ActionId = ActionId.Update
                        };

                        item.TaskItemProcessHistories.Add(taskItemHistory);
                        _objectRepository.Modify(item);
                    }
                    foreach (var taskDto in adds)
                    {
                        TaskItem item = new TaskItem();
                        item.TaskName = taskDto.Name;
                        item.Id = taskDto.Id;
                        item.IsGroupLabel = taskDto.IsGroupLabel;
                        item.Order = taskDto.Order;
                        if (!string.IsNullOrEmpty(taskDto.AssignByUsername))
                        {
                            string assignername = taskDto.AssignByUsername.Substring(taskDto.AssignByUsername.IndexOf("\\") + 1);
                            List<string> userNames = domains.Select(e => (domain + "\\" + assignername).Trim().ToLower()).ToList();
                            var assigner = userDTOs.Where(p => userNames.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();

                            if (assigner != null)
                            {
                                item.AssignBy = assigner.Id;
                            }
                            else
                            {
                                item.AssignBy = userId;
                            }
                        }
                        else
                        {
                            item.AssignBy = userId;
                        }

                        item.CreatedBy = userId;
                        item.CreatedDate = DateTime.Now;
                        item.ProjectId = projectId;
                        item.TaskItemStatusId = (TaskItemStatusId.New);
                        item.ParentId = taskDto.ParentId;
                        item.FromDate = taskDto.StartTime;
                        item.ToDate = taskDto.EndTime;
                        item.PercentFinish = taskDto.PercentFinish;
                        item.FinishedDate = taskDto.FinishiDate;
                        item.NatureTaskId = (int)EnumNatureTask.Plan;
                        item.TaskItemPriorityId = (int)TaskItemPriorityId.Normal;
                        if (!taskDto.IsGroupLabel.HasValue || !taskDto.IsGroupLabel.Value)
                        {
                            item.IsReport = true;
                            var workerAdds = taskDto.MainWorkers;
                            List<TaskItemAssign> assigerAdds = new List<TaskItemAssign>();
                            foreach (var work in workerAdds)
                            {
                                UserDto user = null;
                                string username = work.Username.Substring(work.Username.IndexOf("\\") + 1);
                                List<string> userNames = domains.Select(e => (domain + "\\" + username).Trim().ToLower()).ToList();
                                user = userDTOs.Where(p => userNames.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();

                                if (user == null)
                                {
                                    string text = work.Text.Substring(work.Username.IndexOf("\\") + 1);
                                    List<string> userTexts = domains.Select(e => (domain + "\\" + text).Trim().ToLower()).ToList();
                                    user = userDTOs.Where(p => userTexts.Contains(p.UserName.ToLower())).OrderBy(p => p.UserName).FirstOrDefault();
                                }
                                if (user != null)
                                {
                                    var dept = deptDTOs.Where(e => e.UserID == user.Id).FirstOrDefault();
                                    TaskItemAssign taskItemAssign = new TaskItemAssign()
                                    {
                                        AppraisePercentFinish = work.PercentFinish,
                                        PercentFinish = work.PercentFinish,
                                        AssignTo = user.Id,
                                        ProjectId = projectId,
                                        TaskItemId = item.Id,
                                        Id = Guid.NewGuid(),
                                        DepartmentId = dept.DeptID,
                                        FromDate = item.FromDate,
                                        ToDate = item.ToDate,
                                        FinishedDate = work.FinishedDate,
                                        TaskType = TaskType.Primary,
                                        TaskItemStatusId = (TaskItemStatusId.New),
                                        ModifiedDate = DateTime.Now,
                                    };
                                    assigerAdds.Add(taskItemAssign);
                                }
                            }
                            if (workerAdds != null && workerAdds.Any())
                            {
                                item.TaskItemAssigns = assigerAdds;
                            }
                        }
                        var taskItemHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = projectId,
                            TaskItemId = item.Id,
                            ProcessResult = item.TaskName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = item.AssignBy,
                            TaskItemStatusId = TaskItemStatusId.New,
                            ActionId = ActionId.Import
                        };

                        item.TaskItemProcessHistories.Add(taskItemHistory);
                        _objectRepository.Add(item);
                    }
                    //update status project
                    var project = _projectRepository
                        .Get(
                            x => x.Id == projectId,
                            new IncludingQuery<Project>(new List<Expression<Func<Project, object>>> {
                            x => x.TaskItems
                            })).FirstOrDefault();

                    //udpate datetime
                    if (project.TaskItems != null)
                    {
                        project.FromDate = project.TaskItems.Where(e => e.FromDate.HasValue).Select(x => x.FromDate.GetValueOrDefault()).Min();
                        project.ToDate = project.TaskItems.Where(e => e.ToDate.HasValue).Select(x => x.ToDate.GetValueOrDefault()).Max();
                    }
                    if (project.ToDate == DateTime.MinValue)
                    {
                        project.ToDate = null;
                    }
                    if (project.FromDate == DateTime.MinValue)
                    {
                        project.FromDate = null;
                    }

                    _projectRepository.Modify(project, new List<Expression<Func<Project, object>>>() { p => p.FromDate, p => p.ToDate });
                    dbContextScope.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }

            return result;
        }
        #region Admin
        public async Task<SendMessageResponse> SaveAdminTaskAsync(TaskItemDto dto, TaskItemStatusId taskItemStatusId = TaskItemStatusId.New)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    if (DateTime.TryParseExact(dto.FromDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate))
                    {
                        dto.FromDate = fromDate;
                    }
                    else
                    {
                        dto.FromDate = null;
                    }
                    if (DateTime.TryParseExact(dto.ToDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate))
                    {
                        dto.ToDate = toDate;
                    }
                    else
                    {
                        dto.ToDate = null;
                    }
                    List<AttachmentDto> attachmentDtos = dto.Attachments.ToList();
                    dto.Attachments = null;
                    TaskItem entity = _objectRepository.GetAll().Include(e => e.TaskItemAssigns).Where(e => e.Id == dto.Id).FirstOrDefault();
                    
                    if (entity != null)
                    {
                        if (!((dto.IsFullControl || entity.AssignBy == dto.ModifiedBy) && entity.TaskItemStatusId != TaskItemStatusId.Finished))
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }

                        entity.AssignBy = dto.AssignBy;
                        entity.Conclusion = dto.Conclusion;
                        entity.DepartmentId = dto.DepartmentId;
                        entity.FinishedDate = dto.FinishedDate;
                        entity.FromDate = dto.FromDate;
                        entity.HasRecentActivity = dto.HasRecentActivity;
                        entity.IsAuto = dto.IsAuto;
                        entity.IsDeleted = dto.IsDeleted;
                        entity.IsAdminCategory = true;
                        entity.IsGroupLabel = dto.IsGroupLabel;
                        entity.IsReport = dto.IsReport;
                        entity.IsSecurity = dto.IsSecurity;
                        entity.IsWeirdo = dto.IsWeirdo;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.NatureTaskId = dto.NatureTaskId;
                        entity.Order = dto.Order;
                        entity.ParentId = dto.ParentId;
                        entity.PercentFinish = dto.PercentFinish;
                        entity.ProjectId = dto.ProjectId;
                        entity.TaskGroupType = dto.TaskGroupType;
                        entity.TaskItemCategory = string.Join(";", dto.TaskItemCategories);
                        entity.TaskItemPriorityId = dto.TaskItemPriorityId;
                        entity.TaskItemStatusId = dto.TaskItemStatusId;
                        entity.TaskName = dto.TaskName;
                        entity.TaskType = dto.TaskType;
                        entity.ToDate = dto.ToDate;
                        entity.Weight = dto.Weight;
                        entity.PercentFinish = dto.PercentFinish;

                        if (entity.TaskItemAssigns != null && entity.TaskItemAssigns.Any())
                        {
                            foreach (var assignentity in entity.TaskItemAssigns.Where(e => !e.IsDeleted))
                            {
                                var assigndto = dto.TaskItemAssigns.Where(e => e.AssignTo == assignentity.AssignTo).FirstOrDefault();
                                if (assigndto == null)
                                {
                                    assignentity.IsDeleted = true;
                                }
                                else
                                {
                                    assignentity.TaskType = assigndto.TaskType;
                                }
                                assignentity.ModifiedDate = DateTime.Now;
                            }
                        }
                        List<TaskItemAssignDto> assignAdds = dto.TaskItemAssigns.Where(e => !entity.TaskItemAssigns.Where(a => !a.IsDeleted).Any(t => t.AssignTo == e.AssignTo)).ToList();
                        foreach (var assign in assignAdds)
                        {
                            assign.Id = Guid.NewGuid();
                            assign.TaskItemId = dto.Id;
                            assign.ProjectId = dto.ProjectId;
                            assign.ModifiedDate = dto.ModifiedDate;
                            assign.TaskItemStatusId = TaskItemStatusId.New;
                            entity.TaskItemAssigns.Add(_mapper.Map<TaskItemAssign>(assign));
                        }
                        _objectRepository.Modify(entity);
                    }
                    else
                    {
                        dto.TaskItemStatusId = taskItemStatusId;
                        dto.Id = Guid.NewGuid();
                        dto.CreatedBy = dto.ModifiedBy;
                        dto.CreatedDate = dto.ModifiedDate;
                        dto.TaskItemCategory = string.Join(";", dto.TaskItemCategories);
                        dto.IsDeleted = false;
                        dto.IsAdminCategory = true;
                        foreach (var assign in dto.TaskItemAssigns)
                        {
                            assign.Id = Guid.NewGuid();
                            assign.TaskItemId = dto.Id;
                            assign.ProjectId = dto.ProjectId;
                            assign.ModifiedDate = dto.ModifiedDate;
                            assign.TaskItemStatusId = taskItemStatusId;
                        }
                        entity = _mapper.Map<TaskItem>(dto);
                        entity.Project = null;
                        _objectRepository.Add(entity);
                    }
                    foreach (AttachmentDto attachDto in attachmentDtos)
                    {
                        //attachDto.ProjectId = entity.ProjectId;
                        attachDto.ItemId = entity.Id;
                        Attachment attach = _mapper.Map<Attachment>(attachDto);
                        _attachmentRepository.Add(attach);
                    }
                    if (dto.AttachDelIds != null)
                    {
                        if (dto.AttachDelIds.Any())
                        {
                            List<Attachment> attachDels = _attachmentRepository.GetAll().Where(e => dto.AttachDelIds.Contains(e.Id)).ToList();
                            _attachmentRepository.DeleteRange(attachDels);
                        }
                    }
                    await scope.SaveChangesAsync();
                }
                sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);
                return sendMessage;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }
        public async Task<TaskItemDto> GetAdminTaskById(Guid id)
        {
            TaskItemDto dto = new TaskItemDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    TaskItem entity = _objectRepository.GetAll().Include(t => t.TaskItemAssigns).Where(p => p.IsDeleted == false && p.Id == id).FirstOrDefault();
                    entity.TaskItemAssigns = entity.TaskItemAssigns.Where(a => !a.IsDeleted).ToList();
                    var users = _userServices.GetUsers();
                    var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                    dto = _mapper.Map<TaskItemDto>(entity);
                    if (!string.IsNullOrEmpty(dto.TaskItemCategory))
                    {
                        dto.TaskItemCategories = dto.TaskItemCategory.Split(';').ToList();
                    }
                    foreach (TaskItemAssignDto assignee in dto.TaskItemAssigns)
                    {
                        var userDept = userDepartments.Where(e => e.UserID == assignee.AssignTo).FirstOrDefault();
                        if (userDept != null)
                        {
                            assignee.AssignToFullName = userDept.FullName;
                            assignee.AssignToJobTitleName = userDept.JobTitleName;
                            assignee.Department = userDept.DeptName;
                        }
                    }
                    dto.TaskItemAssigns = dto.TaskItemAssigns.OrderBy(e => e.AssignToFullName).ToList();
                    var assignBy = userDepartments.Where(e => e.UserID == dto.AssignBy).FirstOrDefault();
                    dto.AssignByFullName = assignBy.FullName;
                    if (dto.FromDate.HasValue)
                    {
                        dto.FromDateText = dto.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (dto.ToDate.HasValue)
                    {
                        dto.ToDateText = dto.ToDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (dto.ParentId.HasValue && dto.ParentId != Guid.Empty)
                    {
                        TaskItem parent = _objectRepository.GetAll().Where(p => p.IsDeleted == false && p.Id == dto.ParentId).FirstOrDefault();
                        dto.IsParentAuto = true;
                        dto.ParentToDateText = parent.ToDate.Value.ToString("dd/MM/yyyy");
                        dto.ParentFromDateText = parent.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        dto.IsParentAuto = true;
                    }
                    dto.IsLinked = true;
                    dto.Project = null;
                    List<AttachmentDto> attachmentDtos = _attachmentRepository.GetAll().Where(e => e.ItemId.HasValue && e.ItemId == entity.Id && e.Source == Source.TaskItem).Select(e => new AttachmentDto()
                    {
                        Id = e.Id,
                        FileName = e.FileName,
                        FileExt = e.FileExt
                    }).ToList();
                    dto.Attachments = attachmentDtos;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        #endregion
    }
}
