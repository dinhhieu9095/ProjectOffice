using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using System.Globalization;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using System.Data.SqlClient;
using System.Data;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemAssignService : ITaskItemAssignService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        private readonly ITaskItemAssignRepository _objectRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ITaskItemProcessHistoryRepository _taskItemProcessHistoryRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public TaskItemAssignService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, ITaskItemAssignRepository taskItemAssignRepository, ITaskItemRepository taskItemRepository, IUserServices userServices, IUserDepartmentServices userDepartmentServices, ITaskItemProcessHistoryRepository taskItemProcessHistoryRepository, IAttachmentRepository attachmentRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _objectRepository = taskItemAssignRepository;
            _userServices = userServices;
            _userDepartmentServices = userDepartmentServices;
            _taskItemProcessHistoryRepository = taskItemProcessHistoryRepository;
            _attachmentRepository = attachmentRepository;
            _taskItemRepository = taskItemRepository;
        }
        public async Task<TaskItemAssignDto> GetByAssignTo(Guid taskId, Guid userId)
        {
            TaskItemAssignDto dto = null;
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    TaskItemAssign entity = _objectRepository.GetAll().Include(t => t.TaskItemProcessHistories).Include(t => t.TaskItem).Include(t=>t.TaskItemStatus).Include(t => t.TaskItem.TaskItemStatus).Where(p => p.IsDeleted == false && p.TaskItemId == taskId && p.AssignTo == userId).FirstOrDefault();
                    if(entity != null)
                    {
                        var users = _userServices.GetUsers();
                        var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                        dto = _mapper.Map<TaskItemAssignDto>(entity);

                        var assignTo = userDepartments.Where(e => e.UserID == entity.AssignTo).FirstOrDefault();
                        dto.AssignToFullName = assignTo.FullName;
                        var assignBy = userDepartments.Where(e => e.UserID == entity.TaskItem.AssignBy).FirstOrDefault();
                        dto.TaskItem.AssignByFullName = assignBy.FullName;
                        if (dto.TaskItem.FromDate.HasValue)
                        {
                            dto.TaskItem.FromDateText = dto.TaskItem.FromDate.Value.ToString("dd/MM/yyyy");
                        }
                        if (dto.TaskItem.ToDate.HasValue)
                        {
                            dto.TaskItem.ToDateText = dto.TaskItem.ToDate.Value.ToString("dd/MM/yyyy");
                        }
                        if(dto.TaskType != TaskType.Primary)
                        {
                            dto.TaskItem.IsReport = false;
                        }
                        if (dto.ExtendDate.HasValue)
                        {
                            dto.ExtendDateText = dto.ExtendDate.Value.ToString("dd/MM/yyyy");
                        }
                        var isFinished = dto.TaskItem.TaskItemStatusId == TaskItemStatusId.Finished || dto.TaskItem.TaskItemStatusId == TaskItemStatusId.Cancel;
                        if ((!isFinished && dto.TaskItem.ToDate.Value.Date < DateTime.Now.Date) || (isFinished && dto.TaskItem.FinishedDate.HasValue && dto.TaskItem.ToDate.Value.Date < dto.TaskItem.FinishedDate.Value.Date))
                        {
                            dto.TaskItem.IsLate = true;
                            dto.TaskItem.Process = "out-of-date";
                        }
                        else if(!isFinished && dto.TaskItem.ToDate.Value.Date <= DateTime.Now.AddDays(2).Date)
                        {
                            dto.TaskItem.IsLate = false;
                            dto.TaskItem.Process = "near-of-date";
                        }
                        else
                        {
                            dto.TaskItem.IsLate = false;
                            dto.TaskItem.Process = "in-due-date";
                        }
                        dto.IsExtend = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public async Task<TaskItemAssignDto> GetByAssignBy(Guid taskId, Guid userId)
        {
            TaskItemAssignDto dto = new TaskItemAssignDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    TaskItemAssign entity = _objectRepository.GetAll().Include(t => t.TaskItemProcessHistories).Include(t => t.TaskItem).Include(t => t.TaskItemStatus).Where(p => p.IsDeleted == false && p.TaskItemId == taskId && p.TaskItem.AssignBy == userId && p.TaskType == TaskType.Primary).FirstOrDefault();
                    if(entity!= null)
                    {
                        var users = _userServices.GetUsers();
                        var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                        dto = _mapper.Map<TaskItemAssignDto>(entity);

                        var assignBy = userDepartments.Where(e => e.UserID == dto.AssignTo).FirstOrDefault();
                        dto.AssignToFullName = assignBy.FullName;
                        if (dto.TaskItem.FromDate.HasValue)
                        {
                            dto.TaskItem.FromDateText = dto.TaskItem.FromDate.Value.ToString("dd/MM/yyyy");
                        }
                        if (dto.TaskItem.ToDate.HasValue)
                        {
                            dto.TaskItem.ToDateText = dto.TaskItem.ToDate.Value.ToString("dd/MM/yyyy");
                        }
                        if (dto.ExtendDate.HasValue)
                        {
                            dto.ExtendDateText = dto.ExtendDate.Value.ToString("dd/MM/yyyy");
                            dto.ExtendDateTotal = (dto.ExtendDate - dto.TaskItem.ToDate).Value.Days;
                        }
                        var isFinished = dto.TaskItem.TaskItemStatusId == TaskItemStatusId.Finished || dto.TaskItem.TaskItemStatusId == TaskItemStatusId.Cancel;
                        if ((!isFinished && dto.TaskItem.ToDate < DateTime.Now.Date) || (isFinished && dto.TaskItem.FinishedDate.HasValue && dto.TaskItem.ToDate < dto.TaskItem.FinishedDate.Value.Date))
                        {
                            dto.TaskItem.IsLate = true;
                        }
                        else
                        {
                            dto.TaskItem.IsLate = false;
                        }
                        dto.AppraisePercentFinish = dto.PercentFinish;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public async Task<TaskItemAssignDto> GetById(Guid id)
        {
            TaskItemAssignDto dto = new TaskItemAssignDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    TaskItemAssign entity = _objectRepository.GetAll().Include(t => t.TaskItemProcessHistories).Include(t => t.TaskItem).Where(p => p.IsDeleted == false && p.Id == id).FirstOrDefault();
                    
                    var users = _userServices.GetUsers();
                    var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                    dto = _mapper.Map<TaskItemAssignDto>(entity);
                    
                    var assignBy = userDepartments.Where(e => e.UserID == dto.AssignTo).FirstOrDefault();
                    dto.AssignToFullName = assignBy.FullName;
                    if (dto.TaskItem.FromDate.HasValue)
                    {
                        dto.TaskItem.FromDateText = dto.TaskItem.FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    if (dto.TaskItem.ToDate.HasValue)
                    {
                        dto.TaskItem.ToDateText = dto.TaskItem.ToDate.Value.ToString("dd/MM/yyyy");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public async Task<SendMessageResponse> UpdateProcessTaskAssign(TaskItemAssignDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    List<AttachmentDto> attachmentDtos = dto.Attachments.ToList();
                    dto.Attachments = null;
                    TaskItemAssign entity = _objectRepository.GetAll().Where(e => e.Id == dto.Id).FirstOrDefault();
                    if (entity != null)
                    {
                        TaskItem taskEntity = _taskItemRepository.GetAll().Where(e => e.Id == entity.TaskItemId).FirstOrDefault();
                        if (!dto.IsFullControl && taskEntity.AssignBy != dto.ModifiedBy && entity.AssignTo != dto.ModifiedBy)
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                        if (DateTime.TryParseExact(dto.ExtendDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime extendDate))
                        {
                            dto.ExtendDate = extendDate;
                        }
                        else
                        {
                            dto.ExtendDate = null;
                        }
                        entity.AppraiseProcess = dto.AppraiseProcess;
                        entity.AppraiseStatus = dto.AppraiseStatus;
                        entity.FinishedDate = dto.FinishedDate;
                        entity.FromDate = dto.FromDate;
                        entity.IsDeleted = dto.IsDeleted;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.Problem = dto.Problem;
                        entity.Solution = dto.Solution;
                        entity.ToDate = dto.ToDate;
                        if (entity.AssignTo == dto.ModifiedBy && !dto.IsAssignBy)
                        {
                            switch (dto.ActionText)
                            {
                                case "Process":
                                    dto.TaskItemStatusId = TaskItemStatusId.InProcess;
                                    dto.ActionId = ActionId.Process;
                                    entity.PercentFinish = dto.PercentFinish;
                                    break;
                                case "Report":
                                    if(taskEntity.IsReport == false)
                                    {
                                        dto.TaskItemStatusId = TaskItemStatusId.Finished;
                                        dto.ActionId = ActionId.Finish;
                                    }
                                    else
                                    {
                                        dto.TaskItemStatusId = TaskItemStatusId.Report;
                                        dto.ActionId = ActionId.Report;
                                    }
                                    entity.PercentFinish = dto.PercentFinish;
                                    break;
                                case "Extend":
                                    entity.IsExtend = true;
                                    entity.ExtendDate = dto.ExtendDate;
                                    dto.ActionId = ActionId.Extend;
                                    entity.PercentFinish = dto.PercentFinish;

                                    break;
                                case "ReturnReport":
                                    dto.TaskItemStatusId = TaskItemStatusId.ReportReturn;
                                    dto.ActionId = ActionId.Return;
                                    break;
                                default:
                                    break;
                            }
                            if (entity.TaskType != TaskType.Primary)
                            {
                                dto.TaskItemStatusId = TaskItemStatusId.Read;
                            }
                            entity.LastResult = dto.Description;
                            //entity.IsExtend = dto.IsExtend;
                            //entity.ExtendDate = dto.ExtendDate;
                        }
                        if (taskEntity.AssignBy == dto.ModifiedBy && dto.IsAssignBy)
                        {
                            switch (dto.ActionText)
                            {
                                case "Appraise":
                                    dto.ActionId = ActionId.Appraise;

                                    if (entity.TaskItemStatusId == TaskItemStatusId.Extend)
                                    {
                                        dto.TaskItemStatusId = TaskItemStatusId.InProcess;
                                    }
                                    else if (entity.TaskItemStatusId == TaskItemStatusId.Report)
                                    {
                                        dto.TaskItemStatusId = TaskItemStatusId.Finished;
                                        entity.AppraisePercentFinish = dto.AppraisePercentFinish;
                                        entity.PercentFinish = dto.AppraisePercentFinish;
                                        dto.ActionId = ActionId.Finish;
                                    }
                                    else if (entity.TaskItemStatusId == TaskItemStatusId.ReportReturn)
                                    {
                                        dto.TaskItemStatusId = TaskItemStatusId.Cancel;
                                    }
                                    break;
                                case "Return":
                                    dto.TaskItemStatusId = TaskItemStatusId.InProcess;
                                    entity.AppraisePercentFinish = dto.AppraisePercentFinish;
                                    entity.PercentFinish = dto.AppraisePercentFinish;
                                    dto.ActionId = ActionId.Return;
                                    break;
                                case "AppraiseExtend":
                                    entity.ExtendDate = dto.ExtendDate;
                                    taskEntity.ToDate = dto.ExtendDate;
                                    entity.IsExtend = false;
                                    dto.ActionId = ActionId.Appraise;
                                    break;
                                case "ReturnExtend":
                                    entity.ExtendDate = null;
                                    entity.IsExtend = false;
                                    dto.ActionId = ActionId.Return;
                                    break;
                                default:
                                    break;
                            }
                            entity.AppraiseResult = dto.Description;
                        }

                        entity.TaskItemStatusId = dto.TaskItemStatusId;
                        TaskItemProcessHistory taskAssignHistory = new TaskItemProcessHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = entity.ProjectId,
                            ActionId = dto.ActionId,
                            CreatedBy = dto.ModifiedBy,
                            PercentFinish = entity.PercentFinish,
                            CreatedDate = dto.ModifiedDate,
                            TaskItemId = entity.TaskItemId,
                            TaskItemAssignId = entity.Id,
                            TaskItemStatusId = entity.TaskItemStatusId,
                            ProcessResult = dto.Description
                        };
                        if(entity.TaskType == TaskType.Primary || dto.IsAssignBy)
                        {
                            //TaskItemProcessHistory taskHistory = new TaskItemProcessHistory
                            //{
                            //    Id = Guid.NewGuid(),
                            //    ProjectId = entity.ProjectId,
                            //    ActionId = dto.ActionId,
                            //    CreatedBy = dto.ModifiedBy,
                            //    PercentFinish = dto.PercentFinish,
                            //    CreatedDate = dto.ModifiedDate,
                            //    TaskItemId = entity.TaskItemId,
                            //    TaskItemStatusId = entity.TaskItemStatusId,
                            //    ProcessResult = dto.Description
                            //};
                            taskEntity.TaskItemStatusId = entity.TaskItemStatusId;
                            taskEntity.PercentFinish = entity.PercentFinish;
                            //_taskItemProcessHistoryRepository.Add(taskHistory);

                        }

                        _objectRepository.Modify(entity);
                        //_taskItemRepository.Modify(taskEntity);
                        _taskItemProcessHistoryRepository.Add(taskAssignHistory);
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
                            Value = taskEntity.Id
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@FromDate",
                            IsNullable = false,
                            Value = taskEntity.FromDate
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@ToDate",
                            IsNullable = false,
                            Value = taskEntity.ToDate
                        });
                        param.Add(new SqlParameter()
                        {
                            SqlDbType = SqlDbType.Bit,
                            ParameterName = "@IsUpdateStatus",
                            IsNullable = true,
                            Value = 1
                        });
                        await _objectRepository.SqlQueryAsync(typeof(ProjectDto), "[dbo].[SP_UPDATE_TASK_RANGE_DATE] @ProjectId, @TaskId, @FromDate, @ToDate, @IsUpdateStatus", param.ToArray());
                    }
                    
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

    }
}
