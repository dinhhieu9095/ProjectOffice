using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RefactorThis.GraphDiff;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Linq;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Module.Task.Entities;
using SystemTask = System.Threading.Tasks.Task;
namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _objectRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentServices _departmentServices;
        private readonly IAttachmentService _attachmentService;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IProjectHistoryRepository _projectHistoryRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUserServices _userServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        public ProjectService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IProjectRepository objectRepository, IUserServices userServices, ICategoryService categoryService, IDepartmentServices departmentServices, IProjectHistoryRepository projectHistoryRepository, IAttachmentRepository attachmentRepository, IProjectCategoryRepository projectCategoryRepository, IUserDepartmentServices userDepartmentServices, IAttachmentService attachmentService, ITaskItemRepository taskItemRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _categoryService = categoryService;
            _mapper = mapper;
            _objectRepository = objectRepository;
            _departmentServices = departmentServices;
            _attachmentService = attachmentService;
            _userServices = userServices;
            _userDepartmentServices = userDepartmentServices;
            _projectCategoryRepository = projectCategoryRepository;
            _projectHistoryRepository = projectHistoryRepository;
            _attachmentRepository = attachmentRepository;
            _taskItemRepository = taskItemRepository;
        }
        public async Task<ProjectDto> GetById(Guid id)
        {
            ProjectDto dto = new ProjectDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    Project entity = _objectRepository.GetAll().Include(e=>e.TaskItems).Where(p => p.IsActive == true && p.Id == id).FirstOrDefault();
                    var users = _userServices.GetUsers();
                    var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                    dto = _mapper.Map<ProjectDto>(entity);
                    if (!string.IsNullOrEmpty(dto.ProjectCategory))
                    {
                        dto.ProjectCategories = dto.ProjectCategory.Split(';').ToList();
                    }
                    if (!string.IsNullOrEmpty(dto.ManagerId))
                    {
                        List<string> managerIds = dto.ManagerId.Split(';').ToList();
                        foreach (string managerId in managerIds)
                        {
                            var userDept = userDepartments.Where(e => e.UserID.ToString() == managerId).FirstOrDefault();
                            if(userDept!= null)
                            {
                                ProjectMemberDto projectMember = new ProjectMemberDto
                                {
                                    Department = userDept.DeptName,
                                    JobTitle = userDept.JobTitleName,
                                    Role = "1",
                                    UserId = userDept.UserID,
                                    UserName = userDept.UserName,
                                    FullName = userDept.FullName,
                                };
                                dto.ProjectMembers.Add(projectMember);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(dto.UserViews))
                    {
                        List<string> userViews = dto.UserViews.Split(';').ToList();
                        foreach (string userView in userViews)
                        {
                            var userDept = userDepartments.Where(e => e.UserID.ToString() == userView).FirstOrDefault();
                            if (userDept != null)
                            {
                                ProjectMemberDto projectMember = new ProjectMemberDto
                                {
                                    Department = userDept.DeptName,
                                    JobTitle = userDept.JobTitleName,
                                    Role = "2",
                                    UserId = userDept.UserID,
                                    UserName = userDept.UserName,
                                    FullName = userDept.FullName,
                                };
                                dto.ProjectMembers.Add(projectMember);
                            }
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
                    if (dto.TaskItems.Any())
                    {
                        dto.MaxToDateText = dto.TaskItems.Max(e => e.ToDate).HasValue? dto.TaskItems.Max(e => e.ToDate).Value.ToString("dd/MM/yyyy"):string.Empty;
                        dto.MinFromDateText = dto.TaskItems.Min(e => e.FromDate).HasValue? dto.TaskItems.Min(e => e.FromDate).Value.ToString("dd/MM/yyyy"):string.Empty;
                    }
                    List<AttachmentDto> attachmentDtos = _attachmentRepository.GetAll().Where(e => e.ProjectId == entity.Id && !e.ItemId.HasValue).Select(e => new AttachmentDto() { 
                        Id = e.Id,
                        FileName = e.FileName,
                        FileExt = e.FileExt
                    }).ToList();
                    dto.Attachments = attachmentDtos;
                    dto.TaskItems = null;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public List<ItemActionDto> GetListItemAction(Guid projectId, Guid userId, bool isFullControl)
        {
            var itemActions = new List<ItemActionDto>();
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var project = _objectRepository.GetAll().Include(e => e.TaskItems).Where(p => p.IsActive == true && p.Id == projectId).FirstOrDefault();
                if (project != null)
                {
                    var userPermission = new List<string>();
                    try
                    {
                        bool hasApproveAction = false,
                            hasAssignAction = false,
                            hasAppraiseAction = false,
                            hasAppraiseExtendAction = false,
                            hasProcessAction = false,
                            hasEditAction = false,
                            hasDeleteAction = false,
                            hasJobApproveAction = false,
                            hasApproveReturnAction = false,
                            hasUpdateProcessProject = false;

                        if (project.ProjectStatusId == ProjectStatusId.WaitingApprove
                            && (project.ApprovedBy == userId
                            || isFullControl
                            || (!string.IsNullOrEmpty(project.ManagerId) && project.ManagerId.Contains(userId.ToString()))))
                        {
                            hasJobApproveAction = true;
                            hasApproveAction = true;
                        }
                        else
                        {
                            if (project.ApprovedBy == userId
                                || isFullControl
                                || (!string.IsNullOrEmpty(project.ManagerId) && project.ManagerId.Contains(userId.ToString())))
                            {
                                hasDeleteAction = true;
                                hasAssignAction = true;

                            }

                            //báo cáo trả lại
                            hasApproveReturnAction = project.TaskItems.Any(e => e.AssignBy == userId && e.TaskItemStatusId == TaskItemStatusId.ReportReturn);
                            hasAppraiseAction = project.TaskItems.Any(e => e.AssignBy == userId && e.TaskItemStatusId == TaskItemStatusId.Report);
                            hasAppraiseExtendAction = project.TaskItems.Any(e => e.AssignBy == userId && e.TaskItemStatusId == TaskItemStatusId.Extend);

                            hasProcessAction = project.TaskItems.Any(a => a.AssignBy == userId && (a.TaskItemStatusId == TaskItemStatusId.New ||
                                              a.TaskItemStatusId == TaskItemStatusId.InProcess ||
                                              a.TaskItemStatusId == TaskItemStatusId.Read));

                            if (project.ApprovedBy == userId
                                || isFullControl
                                || (!string.IsNullOrEmpty(project.ManagerId) && project.ManagerId.Contains(userId.ToString())))
                                hasEditAction = true;
                            if (hasEditAction)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.EditDoc.ToString(), Name = "Chỉnh sửa" });
                            //Xóa
                            if (hasDeleteAction && project.ProjectStatusId != ProjectStatusId.Finished)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.DeleteDoc.ToString(), Name = "Xóa" });
                            //cập nhật tiến độ
                            if ((project.ApprovedBy == userId
                                || isFullControl
                                || (!string.IsNullOrEmpty(project.ManagerId) && project.ManagerId.Contains(userId.ToString()))) && project.ProjectStatusId != ProjectStatusId.Finished)
                                hasUpdateProcessProject = true;

                        }
                        if (hasApproveAction || hasJobApproveAction)
                        {
                            if (hasApproveAction)
                                itemActions.Add(new ItemActionDto
                                {
                                    Code = ActionClick.Approve.ToString(),
                                    Name = "Duyệt"
                                });
                        }
                        else
                        {
                            if (hasAssignAction && project.ProjectStatusId != ProjectStatusId.Finished)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.Assign.ToString(), Name = "Giao việc" });
                            if (hasUpdateProcessProject)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.ProcessProject.ToString(), Name = "Cập nhật tiến độ" });
                            if (hasAppraiseAction)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.Appraise.ToString(), Name = "Duyệt báo cáo" });
                            if (hasAppraiseExtendAction)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.AppraiseExtend.ToString(), Name = "Duyệt Gia hạn" });
                            if (hasApproveReturnAction)
                                itemActions.Add(new ItemActionDto
                                { Code = ActionClick.ApproveReturn.ToString(), Name = "Duyệt trả lại" });
                            if (hasProcessAction)
                                itemActions.Add(new ItemActionDto { Code = ActionClick.Process.ToString(), Name = "Xử lý" });
                        }

                        if ((project.ApprovedBy == userId
                                || isFullControl
                                || (!string.IsNullOrEmpty(project.ManagerId) && project.ManagerId.Contains(userId.ToString()))) && project.ProjectStatusId != ProjectStatusId.Finished)
                        {
                            itemActions.Add(new ItemActionDto { Code = ActionClick.ImportMSProjectDoc.ToString(), Name = "Import MS Project" });
                            itemActions.Add(new ItemActionDto { Code = ActionClick.ImportDoc.ToString(), Name = "Import Excel" });


                        }
                        // Import Doc
                        //itemActions.Add(new ItemActionClass { Code = ActionClick.ExportMSProjectDoc.ToString(), Name = "Export MS Project" });
                        // Export Doc
                        itemActions.Add(new ItemActionDto { Code = ActionClick.ExportDoc.ToString(), Name = "Export Excel" });
                        //Chuyển vào thư mục    
                        itemActions.Add(new ItemActionDto
                        { Code = ActionClick.MoveToFolder.ToString(), Name = "Copy vào thư mục" });
                    }
                    catch (Exception ex)
                    {
                        _loggerServices.WriteError(ex.ToString());
                    }

                }
            }
            return itemActions;
        }
        
        public void SaveProjectCategory(Guid? projectId, Guid? userId, List<string> proJectCategories)
        {
            var categories = _projectCategoryRepository.Get(e => e.IsActive == true && e.ProjectId == projectId && proJectCategories.Contains(e.Name));
            var inSertCategories = proJectCategories.Where(e => !categories.Any(c => c.Name == e)).ToList();
            foreach (string cate in inSertCategories)
            {
                ProjectCategory projectCategory = new ProjectCategory
                {
                    Id = Guid.NewGuid(),
                    DateUseLast = DateTime.Now,
                    Name = cate,
                    IsActive = true,
                    UserId = userId,
                    ProjectId = projectId
                };
                _projectCategoryRepository.Add(projectCategory);
            }
        }
        public async Task<SendMessageResponse> UpdateStatusProjectAsync(ProjectDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    List<AttachmentDto> attachmentDtos = dto.Attachments.ToList();
                    dto.Attachments = null;
                    
                    Project entity = _objectRepository.GetAll().Where(e => e.Id == dto.Id && e.Id != Guid.Empty).FirstOrDefault();
                    if (entity != null)
                    {
                        if (!((dto.IsFullControl || entity.CreatedBy == dto.ModifiedBy || entity.ManagerId.Contains(dto.ModifiedBy.ToString())) && entity.ProjectStatusId != ProjectStatusId.Finished))
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                        entity.AppraiseResult = dto.AppraiseResult;
                        entity.ApprovedDate = dto.ApprovedDate;
                        if(dto.ProjectStatusId == ProjectStatusId.Finished)
                        {
                            entity.FinishedDate = dto.ApprovedDate;
                        }
                        entity.ModifiedBy= dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.PercentFinish= dto.PercentFinish;
                        entity.ProjectStatusId= dto.ProjectStatusId;
                        ProjectHistory projectHistory = new ProjectHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = entity.Id,
                            ActionId = ActionId.Appraise,
                            Created = dto.ModifiedDate,
                            CreatedBy = dto.ModifiedBy,
                            DepartmentId = Guid.Empty,
                            PercentFinish = dto.PercentFinish,
                            Summary = dto.AppraiseResult
                        };
                        
                        _objectRepository.Modify(entity);
                        _projectHistoryRepository.Add(projectHistory);
                        SaveProjectCategory(dto.Id ,dto.ModifiedBy, dto.ProjectCategories);
                        
                    }
                    else
                    {
                        sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                        return sendMessage;
                    }
                    foreach (AttachmentDto attachDto in attachmentDtos)
                    {
                        attachDto.ProjectId = entity.Id;
                        Attachment attach = _mapper.Map<Attachment>(attachDto);
                        _attachmentRepository.Add(attach);
                    }
                    if (dto.AttachDelIds!= null && dto.AttachDelIds.Any())
                    {
                        List<Attachment> attachDels = _attachmentRepository.GetAll().Where(e => dto.AttachDelIds.Contains(e.Id)).ToList();
                        _attachmentRepository.DeleteRange(attachDels);
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
         public async Task<SendMessageResponse> SaveAsync(ProjectDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    List<string> managerIds = dto.ProjectMembers.Where(e => e.Role == "1").Select(e => e.UserId.ToString()).ToList();
                    string managerId = string.Join(";", managerIds);
                    List<string> userViewIds = dto.ProjectMembers.Where(e => e.Role == "2").Select(e => e.UserId.ToString()).ToList();
                    string userViewId = string.Join(";", userViewIds);
                    dto.ManagerId = managerId;
                    dto.UserViews = userViewId;
                    List<AttachmentDto> attachmentDtos = dto.Attachments.ToList();
                    dto.Attachments = null;
                    if (dto.ProjectCategories!= null && dto.ProjectCategories.Any())
                    {
                        dto.ProjectCategory = string.Join(";", dto.ProjectCategories);
                    }
                    else
                    {
                        dto.ProjectCategory = null;
                    }
                    if (DateTime.TryParseExact(dto.FromDateText,"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out DateTime fromDate) )
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
                    Project entity = _objectRepository.GetAll().Where(e => e.Id == dto.Id && e.Id != Guid.Empty).FirstOrDefault();
                    if (entity != null)
                    {
                        if (!((dto.IsFullControl || entity.CreatedBy == dto.ModifiedBy || entity.ManagerId.Contains(dto.ModifiedBy.ToString())) && entity.ProjectStatusId != ProjectStatusId.Finished))
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                        entity.AppraiseResult = dto.AppraiseResult;
                        entity.ApprovedBy = dto.ApprovedBy;
                        entity.ApprovedDate= dto.ApprovedDate;
                        entity.FromDate = dto.FromDate;
                        entity.HasRecentActivity = dto.HasRecentActivity;
                        entity.IsAuto = dto.IsAuto;
                        entity.IsLinked = dto.IsLinked;
                        entity.ManagerId = dto.ManagerId;
                        entity.ModifiedBy= dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.PercentFinish= dto.PercentFinish;
                        entity.ProjectCategory= dto.ProjectCategory;
                        entity.ProjectContent= dto.ProjectContent;
                        entity.ProjectKindId= dto.ProjectKindId;
                        entity.ProjectPriorityId= dto.ProjectPriorityId;
                        entity.ProjectScopeId= dto.ProjectScopeId;
                        entity.ProjectSecretId= dto.ProjectSecretId;
                        entity.ProjectStatusId= dto.ProjectStatusId;
                        entity.ProjectTypeId= dto.ProjectTypeId;
                        entity.SerialNumber = dto.SerialNumber;
                        entity.Summary= dto.Summary;
                        entity.ToDate= dto.ToDate;
                        entity.UserViews= dto.UserViews;
                        ProjectHistory projectHistory = new ProjectHistory
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = entity.Id,
                            ActionId = ActionId.Update,
                            Created = dto.ModifiedDate,
                            CreatedBy = dto.ModifiedBy,
                            DepartmentId = Guid.Empty,
                            PercentFinish = dto.PercentFinish,
                            Summary = dto.Summary
                        };
                        
                        _objectRepository.Modify(entity);
                        _projectHistoryRepository.Add(projectHistory);
                        SaveProjectCategory(dto.Id ,dto.ModifiedBy, dto.ProjectCategories);
                        
                    }
                    else
                    {
                        dto.ProjectStatusId = ProjectStatusId.New;
                        dto.Id = Guid.NewGuid();
                        dto.CreatedBy = dto.ModifiedBy;
                        dto.CreatedDate = dto.ModifiedDate;
                        dto.IsActive = true;
                        entity = _mapper.Map<Project>(dto);
                        ProjectHistory projectHistory = new ProjectHistory
                        {
                            Id = Guid.NewGuid(),
                            ActionId = ActionId.Create,
                            Created = dto.ModifiedDate,
                            CreatedBy = dto.ModifiedBy,
                            DepartmentId = Guid.Empty,
                            PercentFinish = dto.PercentFinish,
                            Summary = dto.Summary
                        };
                        entity.ProjectHistories.Add(projectHistory);
                        _objectRepository.Add(entity);
                        SaveProjectCategory(dto.Id ,dto.ModifiedBy, dto.ProjectCategories);
                    }
                    foreach (AttachmentDto attachDto in attachmentDtos)
                    {
                        attachDto.ProjectId = entity.Id;
                        Attachment attach = _mapper.Map<Attachment>(attachDto);
                        _attachmentRepository.Add(attach);
                    }
                    if (dto.AttachDelIds!= null && dto.AttachDelIds.Any())
                    {
                        List<Attachment> attachDels = _attachmentRepository.GetAll().Where(e => dto.AttachDelIds.Contains(e.Id)).ToList();
                        _attachmentRepository.DeleteRange(attachDels);
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
        
        public async Task<SendMessageResponse> DeleteProject(ProjectDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                List<Guid> Ids = new List<Guid>();
                using (var scope = _dbContextScopeFactory.Create())
                {
                    Project entity = await _objectRepository.FindAsync(e => e.Id == dto.Id);
                    if (entity != null)
                    {
                        if (!((dto.IsFullControl || entity.CreatedBy == dto.ModifiedBy || entity.ManagerId.Contains(dto.ModifiedBy.ToString())) && entity.ProjectStatusId != ProjectStatusId.Finished))
                        {
                            sendMessage = SendMessageResponse.CreateFailedResponse("AccessDenied");
                            return sendMessage;
                        }
                    }
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                    entity.ModifiedDate = dto.ModifiedDate;
                    _objectRepository.Modify(entity, new List<Expression<Func<Project, object>>>() { p => p.ModifiedBy, p => p.ModifiedDate, p => p.IsActive });
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
        public List<ProjectFilterCounter> GetCountByFilters(List<string> lstParams, Guid userId, DateTime currentDate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TaskItemDto>> GetFlatTaskAsync(Guid id, Guid userId, int page = 1, int pageSize = 5, Guid? taskItemId = null, int viewType = 0, int viewStatus = -1)
        {
            throw new NotImplementedException();
        }

        public Pagination<FetchProjectsTasksResult> GetProjectsByFolderPaging(string keyWord, List<string> paramValues, int page = 1, int pageSize = 15, string orderBy = "CreatedDate DESC",  UserDto currentUser = default, bool isCount = false)
        {
            var dataResult = new Pagination<FetchProjectsTasksResult>(0, null);
            dataResult.Result = new List<FetchProjectsTasksResult>();
            try
            {
                if (string.IsNullOrEmpty(orderBy)) orderBy = "CreatedDate DESC";

                var sqlQuery = new StringBuilder("EXEC [dbo].[SP_Select_Projects_With_Filter_By_Folder] ");
                sqlQuery.AppendFormat(" @CurrentUserId = '{0}' ", currentUser.Id);
                sqlQuery.AppendFormat(", @Keyword = N'{0}'", keyWord);

                foreach (var param in paramValues)
                {
                    if (!string.IsNullOrEmpty(param))
                    {
                        var arrParams = param.Split(new[] { ':' }, 2);
                        var key = arrParams[0];
                        var value = key.Equals("@FromDate") || key.Equals("@ToDate")
                                        ? DateTime.ParseExact(arrParams[1],
                                                              "dd/MM/yyyy hh:mm",
                                                              null).ToString("yyyy/MM/dd HH:mm")
                                        : arrParams[1];

                        if (key.Equals("@ProjectHashtag"))
                        {
                            sqlQuery.AppendFormat(", {0} = N'{1}' ", key, value);
                        }
                        else
                        {
                            sqlQuery.AppendFormat(", {0} = '{1}' ", key, value);
                        }
                    }
                }

                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var dbContext = scope.DbContexts.Get<TaskContext>();
                    var dataEntity = dbContext.Database.SqlQuery<FetchProjectsTasksResult>(sqlQuery.ToString()).ToList();
                    dataResult.Count = dataEntity.Count();
                    dataResult.Skip = page;
                    dataResult.Take = pageSize;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return dataResult;
        }

        public async Task<MoveItemDto> GetMoveDataByTask(Dictionary<string, string> paramValues)
        {
            var sqlQuery = new StringBuilder("EXEC [Task].[SP_Move_Item_In_Table] ");
            string param = "";
            foreach (KeyValuePair<string, string> item in paramValues)
            {
                if (param != string.Empty) param += ", ";
                param += string.Format("{0} = '{1}' ", item.Key, item.Value);
            }
            if (param != string.Empty) sqlQuery.Append(param);
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<TaskContext>();
                MoveItemDto dataEntity = await dbContext.Database.SqlQuery<MoveItemDto>(sqlQuery.ToString()).FirstOrDefaultAsync();
                return dataEntity;
            }
        }
        public Task<IReadOnlyList<TaskItem>> GetTaskAsync(Guid id, Guid userId, Guid? taskItemId = null, int viewType = 0, int viewStatus = -1)
        {
            throw new NotImplementedException();
        }

        public Pagination<FetchProjectsTasksResult> GetTaskWithFilterPaging(string keyWord, List<string> paramValues, int page = 1, int pageSize = 15, string orderBy = "CreatedDate DESC", UserDto currentUser = default, bool isCount = false)
        {
            var dataResult = new Pagination<FetchProjectsTasksResult>(0, null); 
            dataResult.Result = new List<FetchProjectsTasksResult>();
            try
            {
                var isAdmin = false;
                if (currentUser.UserName.Contains("spadmin") || currentUser.Permissions.Contains("FullControl"))
                {
                    isAdmin = true;
                }
                if (string.IsNullOrEmpty(orderBy)) orderBy = "CreatedDate DESC";

                var sqlQuery = new StringBuilder("EXEC [dbo].[SP_Select_Projects_With_Filter] ");
                sqlQuery.AppendFormat(" @CurrentUserId = '{0}' ", currentUser.Id);
                sqlQuery.AppendFormat(", @Keyword = N'{0}'", keyWord);

                foreach (var param in paramValues)
                {
                    if (!string.IsNullOrEmpty(param))
                    {
                        var arrParams = param.Split(new[] { ':' }, 2);
                        var key = arrParams[0];
                        var value = key.Equals("@FromDate") || key.Equals("@ToDate") || key.Equals("@TaskFromDateOfFromDate") || key.Equals("@TaskFromDateOfToDate") || key.Equals("@TaskToDateOfFromDate") || key.Equals("@TaskToDateOfToDate")
                                        ? DateTime.ParseExact(arrParams[1],
                                                              "dd/MM/yyyy HH:mm",
                                                              null).ToString("yyyy/MM/dd HH:mm")
                                        : arrParams[1];
                        if (key.Equals("@StartDate") || key.Equals("@EndDate"))
                        {
                            value = System.Convert.ToDateTime(arrParams[1]).ToString("yyyy/MM/dd");
                        }
                        if (key.Equals("@ProjectHashtag") || key.Equals("@TaskHashtag"))
                        {
                            sqlQuery.AppendFormat(", {0} = N'{1}' ", key, value);
                        }
                        else
                        {
                            sqlQuery.AppendFormat(", {0} = '{1}' ", key, value);
                        }
                    }
                }

                if (isAdmin == true)
                {
                    sqlQuery.AppendFormat(", @IsFullControl = {0} ", 1);
                }
                if (isCount)
                {
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@Page", page);
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@PageSize", pageSize);
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@IsCount", 1);
                }
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var dbContext = scope.DbContexts.Get<TaskContext>();
                    var dataEntity = dbContext.Database.SqlQuery<FetchProjectsTasksResult>(sqlQuery.ToString()).ToList();
                    dataResult.Count = (dataEntity!=null && dataEntity.Count>0 ) ? dataEntity[0].TotalRecord : 0;
                    dataResult.Skip = page;
                    dataResult.Take = pageSize;
                    dataResult.Result = dataEntity;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return dataResult;
        }

        public async Task<ProjectDetailDto> RenderProject(Guid projectId, UserDto currentUser = default, bool isMobile = false)
        {
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var departments = await _departmentServices.GetDepartmentsAsync();
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var queryable = _objectRepository.GetAll();

                queryable = queryable.Where(e => e.Id == projectId)
                    .Include(x => x.ProjectStatus)
                    .Include(x => x.ProjectPriority)
                    .Include(x => x.ProjectKindId);
                var result = new ProjectDetailDto();
                if (isMobile)
                {
                    result = queryable.Where(x => x.Id == projectId)
                    .Select(x => new ProjectDetailDto
                    {
                        Id = x.Id,
                        Summary = x.Summary,
                        ProjectCategory = x.ProjectCategory,
                        ProjectStatusName = x.ProjectStatus.Name,
                        StatusId = x.ProjectStatusId,
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
                        CountHistory = x.ProjectHistories.Count(y => y.ActionId == ActionId.Process)
                    })
                    .FirstOrDefault();
                }
                else
                {
                    queryable = queryable.Include(x => x.ProjectHistories);
                    result = queryable.Where(x => x.Id == projectId)
                    .Select(x => new ProjectDetailDto
                    {
                        Id = x.Id,
                        Summary = x.Summary,
                        ProjectCategory = x.ProjectCategory,
                        ProjectStatusName = x.ProjectStatus.Name,
                        StatusId = x.ProjectStatusId,
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
                        ProjectHistories = x.ProjectHistories.OrderByDescending(y => y.Created)
                        .Where(y => y.ActionId == ActionId.Process).Select(e => new ProjectHistoryDto() { 
                            Id = e.Id,
                            Action = e.Action,
                            ActionId = e.ActionId,
                            CreatedBy = e.CreatedBy,
                            DepartmentId = e.DepartmentId,
                            PercentFinish = e.PercentFinish,
                            ProjectId = e.ProjectId,
                            Summary = e.Summary,
                            Created = e.Created
                        }).ToList()
                    })
                    .FirstOrDefault();
                }


                var checkUserViewInProject = result.CreatedBy == currentUser.Id || result.ApprovedBy == currentUser.Id
                        || (string.IsNullOrEmpty(result.UserViews) ? false : result.UserViews.Contains(currentUser.Id.ToString()));


                var projectPrioritys = await _categoryService.GetAllOfProjectPriorities();
                result.ProjectPriorityName = projectPrioritys.FirstOrDefault(e => e.Id == result.ProjectPriorityId).Name;

                if (result.ProjectKindId.HasValue)
                {
                    if (result.ProjectKindId == 1)
                    {
                        result.ProjectKindName = "Dự án";
                    }
                    if (result.ProjectKindId == 0)
                    {
                        result.ProjectKindName = "Không phải dự án";
                    }
                }

                //result.TaskItemRoots = dbContextReadOnlyScope.DbContexts
                //    .Get<TaskContext>()
                //    .Set<TaskItem>()
                //    .AsNoTracking()
                //    .Include(x => x.Children)
                //    .Include(x => x.TaskItemAssigns)
                //    .Where(x => x.ProjectId == projectId
                //        && x.ParentId == Guid.Empty && (x.AssignBy == currentUser.Id || x.CreatedBy == currentUser.Id || x.TaskItemAssigns.Any(y => y.AssignTo == currentUser.Id) || checkUserViewInProject))

                //    .OrderBy(x => x.CreatedDate)
                //    .Include(x => x.TaskItemStatus)
                //    .Select(x => new ProjectDetailDto.TaskItemRoot
                //    {
                //        UserId = x.TaskItemAssigns.OrderBy(y => y.TaskType).FirstOrDefault().AssignTo ?? null,
                //        AssignBy = x.AssignBy,
                //        FromDate = x.FromDate,
                //        Id = x.Id,
                //        Content = x.Conclusion,
                //        PercentFinish = x.PercentFinish,
                //        TaskItemStatusId = x.TaskItemStatusId,
                //        TaskName = x.TaskName,
                //        ToDate = x.ToDate,
                //        StatusName = x.TaskItemStatus.Name,
                //        DepartmentId = x.DepartmentId,
                //        IsGroupLabel = x.IsGroupLabel,
                //        CountChildren = x.Children.Count()
                //    }).ToList();

                var lstParams = new List<string>();
                lstParams.Add($"@ParentId:{projectId}");
                var pagingData = GetTaskWithFilterPaging(
                                         null,
                                          lstParams,
                                       1,
                                        20,
                                          " CreatedDate DESC ",
                                          currentUser,
                                          true);

                result.TaskItemRoots = pagingData.Result.Select(x => new ProjectDetailDto.TaskItemRoot()
                {
                    UserId = x.AssignTo,
                    AssignBy = x.UserId,
                    FromDate = x.FromDate,
                    Id = x.Id,
                    Content = x.Content,
                    PercentFinish = x.PercentFinish,
                    TaskItemStatusId = x.TaskItemStatusId,
                    TaskName = x.TaskName,
                    ToDate = x.ToDate,
                    FinishedDate = x.FinishedDate,
                    StatusName = x.StatusName,
                    DepartmentId = x.DepartmentId,
                    IsGroupLabel = x.IsGroupLabel,
                    CountChildren = x.CountTask

                }).ToList();

                result.DateTimeString = ConvertToStringExtensions.DateToString(result.FromDate, result.ToDate);

                result.DepartmentName = departments
                    .Where(x => x.Id == result.DepartmentId)
                    .Select(x => x.Name)
                    .FirstOrDefault();

                var attachmentHistories = await _attachmentService.GetAttachmentDtoHistoryProject(result.Id);

                if (result.ProjectHistories != null)
                    foreach (var history in result.ProjectHistories)
                    {
                        var createBy = await GetUserDeptDTO(history.CreatedBy, history.DepartmentId, userDepartments);
                        history.CreatedByFullName = createBy?.FullName;
                        history.CreatedByJobTitleName = createBy?.JobTitleName;
                        history.Attachments = attachmentHistories.Where(x => x.ItemId == history.Id).ToList();
                    }

                if (isMobile)
                {
                    var userDeparment = await GetUserDeptDTO(result.ApprovedBy, result.DepartmentId, userDepartments);

                    result.FullName = userDeparment?.FullName;
                    result.JobTitle = userDeparment?.JobTitleName;

                    result.Users = new List<UserDto>();
                    if (!string.IsNullOrEmpty(result.UserViews))
                    {
                        List<string> userDepts = result.UserViews.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string userDept in userDepts)
                        {
                            List<string> m_user = userDept.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            Guid m_userId = new Guid(m_user[0]);
                            Guid m_deptId = new Guid(m_user[1]);

                            var m_userDeparment = await GetUserDeptDTO(m_userId, m_deptId, userDepartments);

                            if (m_userDeparment != null)
                            {
                                var userDto = new UserDto();
                                userDto.Id = m_userDeparment.UserID;
                                userDto.FullName = m_userDeparment?.FullName;
                                userDto.JobTitle = m_userDeparment?.JobTitleName;

                                result.Users.Add(userDto);
                            }
                        }
                    }

                    var attachmentProjects = await _attachmentService.GetAttachments(result.Id, result.Id);

                    result.Attachments = attachmentProjects;
                }

                foreach (var taskItem in result.TaskItemRoots)
                {
                    var assignBy = await GetUserDeptDTO(taskItem.UserId, taskItem.DepartmentId, userDepartments);
                    taskItem.UserFullName = assignBy?.FullName;
                    taskItem.JobTitleName = assignBy?.JobTitleName;
                    taskItem.DateFormat = ConvertToStringExtensions.DateToString(taskItem.FromDate, taskItem.ToDate);


                    // là mobile
                    if (isMobile)
                    {
                        if (taskItem.IsGroupLabel == false)
                        {
                            //taskItem.CountComment = CommentService.Count(taskItem.Id);

                            //taskItem.CountAttachment = AsyncHelper.RunSync(() => AttachmentService.CountAttachments(result.Id, taskItem.Id, Source.TaskItem));
                        }
                    }
                }

                return result;
            }
        }

        public async Task<UserDepartmentDto> GetUserDeptDTO(Guid? userId, Guid? deptId, IReadOnlyList<UserDepartmentDto> userDeptDtos = null)
        {
            var rs = new UserDepartmentDto();
            if (userDeptDtos == null)
                userDeptDtos = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            var userDtos = _userServices.GetUsers();
            rs = userDeptDtos.FirstOrDefault(x => x.UserID == userId.GetValueOrDefault() && x.DeptID == deptId.GetValueOrDefault());
            if (rs == null)
                rs = userDeptDtos.OrderBy(x => x.DeptOrderNumber).FirstOrDefault(x => x.UserID == userId.GetValueOrDefault());
            if (rs == null)
            {
                rs = new UserDepartmentDto();
                var user = userDtos.Where(x => x.Id == userId.GetValueOrDefault()).FirstOrDefault();
                if (user != null)
                {
                    rs.FullName = user.FullName;
                }
            }
            return rs;
        }

        public async Task<IReadOnlyList<TaskItemDto>> AllTaskItemInProject(Guid Id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var userDepartDto = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var asyncTaskItem = await _taskItemRepository.GetAll()
                       .Include(x => x.TaskItemStatus)
                       .Where(x => x.ProjectId == Id && x.IsDeleted == false && (!x.IsGroupLabel.HasValue || x.IsGroupLabel == false))
                       .Select(e => new TaskItemDto() { 
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
                       })
                       .ToListAsync();

                foreach(var x in asyncTaskItem)
                {
                    var assignBy = await GetUserDeptDTO(x.AssignBy, x.DepartmentId, userDepartDto);
                    x.AssignByFullName = assignBy?.FullName;
                    x.DepartmentName = assignBy?.DeptName;
                    x.JobTitleName = assignBy?.JobTitleName;
                };

                return asyncTaskItem;
            }
        }

        public async Task<List<UserReportInProjectDto>> UserReportInProject(Guid id)
        {
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var rs = new List<UserReportInProjectDto>();
                var userDepartmentDto = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                var project = await GetById(id);
                if (project != null)
                {
                    var approvedBy = await GetUserDeptDTO(project.ApprovedBy, project.DepartmentId, userDepartmentDto);

                    var userApprovedBy = new UserReportInProjectDto()
                        .AddUser(project.ApprovedBy, project.DepartmentId, approvedBy?.FullName,
                        approvedBy?.JobTitleName, PositionInProject.Manager);

                    rs.Add(userApprovedBy);
                    if (!string.IsNullOrEmpty(project.ManagerId))
                    {
                        List<Guid?> managerIds = project.ManagerId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid?(new Guid(e))).ToList();
                        foreach (var managerId in managerIds)
                        {
                            var manager = await GetUserDeptDTO(managerId, null, userDepartmentDto);

                            var managerProject = new UserReportInProjectDto()
                        .AddUser(managerId, manager.DeptID, manager?.FullName,
                        manager?.JobTitleName, PositionInProject.Manager);
                            rs.Add(managerProject);

                        }
                    }
                    if (!string.IsNullOrEmpty(project.UserViews))
                    {
                        var listStrUser = project.UserViews.Split(';');
                        if (listStrUser.Any())
                        {
                            foreach (var strUser in listStrUser)
                            {
                                if (!string.IsNullOrEmpty(strUser))
                                {
                                    var str = strUser.Split('_');
                                    if (str.Any())
                                    {
                                        //var userViewDeptId = Guid.Parse(str[1]);
                                        var userId = Guid.Parse(str[0]);
                                        var userView = await GetUserDeptDTO(userId, null, userDepartmentDto);
                                        var userViewInProject = new UserReportInProjectDto()
                                        .AddUser(userId, null, userView?.FullName,
                                        userView?.JobTitleName, PositionInProject.Followers);

                                        rs.Add(userViewInProject);
                                    }
                                }
                            }
                        }
                    }

                    var taskAssigns = await dbContextReadOnlyScope.DbContexts
                       .Get<TaskContext>()
                       .Set<TaskItemAssign>()
                       .Where(x => x.ProjectId == id && !x.TaskItem.IsDeleted)
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
                                var userAssign = await GetUserDeptDTO(assign.AssignTo, assign.DepartmentId, userDepartmentDto);

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

        public async SystemTask MarkTaskItemAssignAsReadAsync(Guid id, Guid userId, Guid taskItemId)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var project = await _objectRepository.FindAsync(x => x.Id == id,
                    new IncludingQuery<Project>(new Expression<Func<Project, object>>[]
                    {
                        x => x.TaskItems,
                        x => x.TaskItems.Select(y => y.TaskItemAssigns),
                        x => x.TaskItems.Select(y => y.TaskItemAssigns.Select(z => z.TaskItemProcessHistories))
                    }));

                var task = project
                    .TaskItems.Where(x => x.Id == taskItemId).FirstOrDefault();
                var taskItemAssigns = project
                    .TaskItems.Where(x => x.Id == taskItemId)
                    .SelectMany(x => x.TaskItemAssigns)
                    .Where(x => x.AssignTo == userId && x.TaskItemStatusId == TaskItemStatusId.New)
                    .ToList();

                if (!taskItemAssigns.Any())
                    return;

                foreach (var taskItemAssign in taskItemAssigns)
                    taskItemAssign.MarkAsRead();
                if (taskItemAssigns.Any(e => e.TaskItemStatusId == TaskItemStatusId.Read && e.TaskType == TaskType.Primary) && task.TaskItemStatusId == TaskItemStatusId.New)
                {

                }
                _objectRepository.Modify(
                project,
                x => x.OwnedCollection(
                    y => y.TaskItems,
                    y => y.OwnedCollection(
                        z => z.TaskItemAssigns,
                        z => z.OwnedCollection(t => t.TaskItemProcessHistories))));

                if (taskItemAssigns.Any(e => e.TaskItemStatusId == TaskItemStatusId.Read && e.TaskType == TaskType.Primary) && task.TaskItemStatusId == TaskItemStatusId.New)
                {
                    task.TaskItemStatusId = TaskItemStatusId.Read;
                }
                _taskItemRepository.Modify(task);

                await dbContextScope.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<TaskItemForMSProjectDto>> GetTaskForTableExcelV2(Guid id, Guid userId, Guid? taskItemId, string numberIndex, int viewType = 0, int viewStatus = -1, bool isAll = false, IReadOnlyList<Guid> listParent = null, IReadOnlyList<UserDto> users = null, bool isRoot = false)
        {
            try
            {
                var lstTaskItemForTable = new List<TaskItemForMSProjectDto>();
                if (users == null)
                {
                    users = _userServices.GetUsers();
                }
                //điều kiện lấy tất cả công việc theo projectId
                IReadOnlyList<Guid> listTaskOfUserAssign = new List<Guid>();
                if (isAll == false)
                {
                    listTaskOfUserAssign = GetTaskOfUserAssign(id, userId, TaskType.Primary);
                    if (taskItemId.HasValue && listTaskOfUserAssign.Contains(taskItemId.Value))
                    {
                        isAll = true;
                    }
                    if (!isAll && listParent == null)
                    {
                        listParent = GetTaskParentOfUser(id, userId);
                    }
                }

                var lstTaskItem = await GetTaskAsyncV2(id, userId, taskItemId, users, isRoot);
                if (isAll == false)
                {
                    lstTaskItem = lstTaskItem.Where(e => listParent.Contains(e.Id) || e.AssignBy == userId).ToList();
                }
                if (lstTaskItem != null && lstTaskItem.Count > 0)
                {
                    int key = 1;

                    foreach (var item in lstTaskItem)
                    {
                        // int key = item.Order.HasValue ? item.Order.Value + 1 : 1;
                        var newNumberIndex = string.IsNullOrEmpty(numberIndex) ? key.ToString() : string.Concat(numberIndex, ".", key); ;
                        var user = _userServices.GetById(item.AssignBy.Value);
                        var hasAssignFollow = item.TaskItemAssigns.Where(p => p.AssignFollow != null).Count() > 0 ? true : false;

                        var taskItemForExcel = new TaskItemForMSProjectDto();
                        taskItemForExcel.Id = item.Id;
                        taskItemForExcel.DocId = item.ProjectId;
                        taskItemForExcel.ParentId = item.ParentId;
                        taskItemForExcel.IsGroupLabel = item.IsGroupLabel;
                        taskItemForExcel.PercentFinish = item.PercentFinish;
                        taskItemForExcel.No = newNumberIndex;
                        taskItemForExcel.AssignBy = item.AssignBy;
                        taskItemForExcel.AssignByName = user != null ? user.FullName : string.Empty;
                        taskItemForExcel.AssignByUsername = user != null ? user.UserName : string.Empty;
                        taskItemForExcel.AuthorId = item.CreatedBy;
                        taskItemForExcel.Name = item.TaskName;
                        taskItemForExcel.Content = item.Conclusion;
                        taskItemForExcel.StartTime = item.FromDate;
                        taskItemForExcel.FinishiDate = item.FinishedDate;
                        taskItemForExcel.EndTime = item.ToDate;
                        taskItemForExcel.HasReport = item.IsReport.HasValue ? item.IsReport.Value : false;
                        taskItemForExcel.HasSecurity = item.IsSecurity.HasValue ? item.IsSecurity.Value : false;
                        taskItemForExcel.HasWeirdo = item.IsWeirdo.HasValue ? item.IsWeirdo.Value : false;
                        taskItemForExcel.Status = item.TaskItemStatusId.HasValue ? (int)item.TaskItemStatusId.Value : 0;
                        taskItemForExcel.StatusName = item.TaskItemStatusId.HasValue ? GetStatusName(item.TaskItemStatusId.Value) : GetStatusName(TaskItemStatusId.New);
                        taskItemForExcel.HasAssignFollow = hasAssignFollow;
                        ///taskItemForExcel.IsAnyNewTaskItemAssign = item.IsAnyNewTaskItemAssign();
                        taskItemForExcel.IsOverDue = !TaskInDueDate.Main(item.ToDate, item.FinishedDate, item.TaskItemStatusId);
                        taskItemForExcel.TaskItemPriorityId = item.TaskItemPriorityId.HasValue ? (int)item.TaskItemPriorityId.Value : 0;
                        taskItemForExcel.TaskItemCategory = item.TaskItemCategory;
                        taskItemForExcel.NatureTask = item.NatureTaskId;
                        taskItemForExcel.Weight = item.Weight;
                        taskItemForExcel.MainWorkers = new List<UserExcelDto>();
                        taskItemForExcel.Supporters = new List<UserExcelDto>();
                        taskItemForExcel.WhoOnlyKnow = new List<UserExcelDto>();
                        taskItemForExcel.Visible = true;

                        if (item.IsSecurity == true && item.CreatedBy.HasValue && item.CreatedBy.Value != userId && !item.TaskItemAssigns.Any(p => p.AssignTo.HasValue && p.AssignTo.Value == userId) && !item.TaskItemAssigns.Any(p => p.AssignFollow.HasValue && p.AssignFollow.Value == userId))
                        {
                            taskItemForExcel.Visible = false;
                        }

                        if (item.TaskItemAssigns != null && item.TaskItemAssigns.Count > 0)
                        {
                            foreach (var subItem in item.TaskItemAssigns)
                            {
                                var userAssign = _userServices.GetById(subItem.AssignTo.Value);

                                if (userAssign != null)
                                {
                                    if (subItem.TaskType == TaskType.Primary)
                                    {
                                        taskItemForExcel.MainWorkers.Add(
                                            new UserExcelDto
                                            {
                                                ID = subItem.Id,
                                                Text = userAssign.FullName,
                                                Username = userAssign.UserName,
                                                CanProcess = subItem.TaskItemStatusId == TaskItemStatusId.New ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.InProcess ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.Read,
                                                PercentFinish = subItem.AppraisePercentFinish,
                                                FinishedDate = subItem.FinishedDate
                                            }
                                        );
                                    }
                                    else if (subItem.TaskType == TaskType.Support)
                                    {
                                        taskItemForExcel.Supporters.Add(
                                            new UserExcelDto
                                            {
                                                ID = subItem.Id,
                                                Text = userAssign.FullName,
                                                Username = userAssign.UserName,
                                                CanProcess = subItem.TaskItemStatusId == TaskItemStatusId.New ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.InProcess ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.Read,
                                                PercentFinish = subItem.AppraisePercentFinish,
                                                FinishedDate = subItem.FinishedDate
                                            }
                                        );
                                    }
                                    else if (subItem.TaskType == TaskType.ReadOnly)
                                    {
                                        taskItemForExcel.WhoOnlyKnow.Add(new UserExcelDto { ID = userAssign.Id, Text = userAssign.FullName, Username = userAssign.UserName });
                                    }
                                }

                            }
                        }


                        lstTaskItemForTable.Add(taskItemForExcel);

                        var lstTaskItemChildSub = await GetTaskForTableExcelV2(id, userId, item.Id, taskItemForExcel.No, viewType, viewStatus, isAll, listParent, users);
                        if (lstTaskItemChildSub.Any())
                        {
                            lstTaskItemForTable.AddRange(lstTaskItemChildSub);
                        }
                        key++;
                    }
                }

                return lstTaskItemForTable;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                throw;
            }
        }

        private async Task<IReadOnlyList<TaskItem>> GetTaskAsyncV2(Guid id, Guid userId,
           Guid? taskItemId = null, IReadOnlyList<UserDto> users = null, bool isRoot = false)
        {
            if (taskItemId != null && taskItemId != Guid.Empty)
                await MarkTaskItemAssignAsReadAsync(id, userId, taskItemId.Value);

            var taskItemList = new List<TaskItem>();

            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskContext = dbContextReadOnlyScope.DbContexts.Get<TaskContext>();
                var taskItems = _taskItemRepository.GetAll().Include(x => x.TaskItemStatus).Include(x => x.TaskItemAssigns.Select(y => y.TaskItemStatus)).Where(x => x.ProjectId == id && !x.IsDeleted);

                #region Filter TaskItem

                //nếu có taskItemId thì lọc ra những công việc và người xử lý thuộc taskItem đó
                if (taskItemId.GetValueOrDefault() != Guid.Empty)
                {
                    if (!isRoot)
                    {
                        taskItems = taskItems.Where(x => x.ParentId == taskItemId);

                    }
                    else
                    {
                        taskItems = taskItems.Where(x => x.Id == taskItemId);
                    }
                }
                else
                {
                    //ngược lại nếu không có taskItemId thì load những công việc root
                    taskItems = taskItems.Where(x => x.ParentId == null || x.ParentId == Guid.Empty);
                }

                var taskItemHasAssignRecursively = new Dictionary<Guid, bool>();

                //lấy Id người giao và người tạo của project
                
                FillTaskItemInformationV2(userId, taskItems, users);
                taskItemList = taskItems.ToList();
                #endregion
            }
            if (taskItemId != null && taskItemId != Guid.Empty)
                await RemoveRecentActivityAsync(taskItemId.Value, userId);

            return taskItemList.Any(x => x.HasRecentActivity == true)
                ? taskItemList
                    .OrderBy(x => x.Order).ThenBy(x => x.CreatedDate)
                    .ToList()
                : taskItemList.OrderBy(x => x.Order).ThenBy(x => x.CreatedDate)
                    .ToList();
        }

        private async SystemTask RemoveRecentActivityAsync(Guid id, Guid userId)
        {
            var projectIds = new List<Guid?>();

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var taskItem = await dbContextScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .Include(x => x.TaskItemAssigns)
                    .Where(x => x.Id == id)
                    .SingleAsync();

                if (taskItem.AssignBy != userId ||
                    taskItem.TaskItemAssigns.All(x => x.HasRecentActivity != true))
                    return;

                foreach (var taskItemAssign in taskItem.TaskItemAssigns)
                    taskItemAssign.HasRecentActivity = false;

                var taskItemIds = dbContextScope.DbContexts
                    .Get<TaskContext>().Database
                    .SqlQuery<Guid>(@"
                    ;WITH Temp 
                    AS
                    (
                        SELECT Ti.Id, Ti.ParentId
                        FROM [Task].[TaskItem] 
                        AS Ti
                        WHERE Ti.Id = @Id
                        UNION ALL 
                        SELECT Ti.Id, Ti.ParentId
                        FROM [Task].[TaskItem] AS Ti
                        INNER JOIN Temp 
                        AS T ON
                        T.ParentId = Ti.Id 
                    )
                    SELECT DISTINCT Id
                    FROM
                    (
                        SELECT T.Id 
                        FROM Temp 
                        AS T
                    ) 
                    AS Temp", new SqlParameter("Id", id))
                    .ToList();

                var graphTaskItems = await dbContextScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .Where(x => taskItemIds.Contains(x.Id))
                    .ToListAsync();

                projectIds
                    .AddRange(graphTaskItems
                        .Select(x => x.ProjectId)
                        .Distinct()
                        .ToList());

                foreach (var graphTaskItem in graphTaskItems)
                    graphTaskItem.HasRecentActivity = false;

                dbContextScope.SaveChanges();
            }

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var projects = await dbContextScope.DbContexts
                    .Get<TaskContext>()
                    .Set<Project>()
                    .Where(x => projectIds.Contains(x.Id))
                    .Include(x => x.TaskItems)
                    .ToListAsync();

                foreach (var project in projects
                    .Where(project => project.TaskItems
                        .All(x => x.HasRecentActivity != true)))
                    project.HasRecentActivity = false;

                dbContextScope.SaveChanges();
            }
        }

        private string GetStatusName(TaskItemStatusId taskItemStatusId)
        {
            var result = string.Empty;

            switch (taskItemStatusId)
            {
                case TaskItemStatusId.New:
                    result += "Mới";
                    break;
                case TaskItemStatusId.InProcess:
                    result += "Đang xử lý";
                    break;
                case TaskItemStatusId.Report:
                    result += "Báo cáo";
                    break;
                case TaskItemStatusId.Cancel:
                    result += "Hủy";
                    break;
                case TaskItemStatusId.Finished:
                    result += "Kết thúc";
                    break;
                case TaskItemStatusId.Extend:
                    result += "Gia hạn";
                    break;
                case TaskItemStatusId.ReportReturn:
                    result += "Báo cáo trả lại";
                    break;
                case TaskItemStatusId.EditTask:
                    result += "Chỉnh sửa công việc";
                    break;
                case TaskItemStatusId.Read:
                    result += "Đã xem";
                    break;
                default:
                    break;
            }

            return result;
        }

        private IReadOnlyList<Guid> GetTaskParentOfUser(Guid id, Guid userId)
        {
            IReadOnlyList<Guid> models = new List<Guid>();
            using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbCtxScope.DbContexts.Get<TaskContext>();
                models = dbContext.Database.SqlQuery<Guid>(
                  string.Format(@"
                EXEC dbo.SP_TASK_PARENT_OF_ASSIGNER
		            @CurrentUserId = '{0}',
		            @ProjectId = '{1}'", userId, id),
                          new object[] { }).ToList();
                return models;
            }

        }
            public IReadOnlyList<Guid> GetTaskOfUserAssign(Guid id, Guid userId, TaskType typeId)
        {
            IReadOnlyList<Guid> models = new List<Guid>();
            try
            {
                using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var taskContext = dbContextReadOnlyScope.DbContexts.Get<TaskContext>();
                    var taskItems = _taskItemRepository.Get(
                           e => e.ProjectId == id && e.AssignBy == userId && e.TaskItemStatusId != TaskItemStatusId.Cancel);
                    models = taskItems.Select(e => e.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return models;
        }
        private void FillTaskItemInformationV2(
           Guid userId,
           IEnumerable<TaskItem> taskItems,
           IReadOnlyList<UserDto> users)
        {

            taskItems.ForEach(x =>
            {

                x.AssignByFullName =
                    users
                        .Where(y => y.Id == x.AssignBy)
                        .Select(y => y.FullName)
                        .FirstOrDefault()
                    ?? string.Empty;
                x.TaskItemAssigns.ForEach(y =>
                {
                    y.AssignToFullName =
                        users
                            .Where(z => z.Id == y.AssignTo)
                            .Select(z => z.FullName)
                            .FirstOrDefault()
                        ?? string.Empty;
                });

                //order by
                x.TaskItemAssigns = x.TaskItemAssigns.Where(s=>!s.IsDeleted).OrderBy(y => y.TaskType).ThenBy(z => z.JobTitleOrderNumber).ToList();

            });
        }
        public async Task< List<TrackingBreadCrumbViewParentDTO> > GetViewBreadCrumbWithParent(Guid parentId)
        {
            // TODO; POSSIBLE SLOW ACTION QUERY
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                List<TrackingBreadCrumbViewParentDTO> rs = new List<TrackingBreadCrumbViewParentDTO>();
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var dbContext = scope.DbContexts.Get<TaskContext>();
                    rs =await dbContext.Database.SqlQuery<TrackingBreadCrumbViewParentDTO>(string.Format(@"EXEC dbo.SP_TASK_VIEW_BREADCRUMB_WITH_PARENT @parentId = '{0}'", parentId)).ToListAsync();
                }
                return rs;
            }
        }

        public bool CheckImportExcelPermission(Guid userId, Guid projectId, Guid? taskId)
        {
            bool isPermiss = false;
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var project = dbContextReadOnlyScope.DbContexts
                         .Get<TaskContext>()
                         .Set<Project>()
                         .Where(x => x.Id == projectId).FirstOrDefault();
                if (project.ProjectStatusId != ProjectStatusId.Finished)
                {
                    if (project.ApprovedBy == userId || project.ManagerId.Contains(userId.ToString()))
                    {
                        isPermiss = true;
                    }
                    else if (taskId.HasValue)
                    {
                        var task = dbContextReadOnlyScope.DbContexts
                        .Get<TaskContext>()
                        .Set<TaskItem>()
                        .Include(x => x.TaskItemAssigns)
                        .Where(x => x.Id == taskId).FirstOrDefault();
                        if (task.AssignBy == userId || task.TaskItemAssigns.Any(e => e.AssignTo == userId && e.TaskType == TaskType.Primary))
                        {
                            isPermiss = true;
                        }
                    }
                }

            }
            return isPermiss;
        }
        public bool UpdateMSProject(Guid Id)
        {
            try
            {
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    //update status project
                    var project = _objectRepository
                        .Get(
                            x => x.Id == Id,
                            new IncludingQuery<Project>(new List<Expression<Func<Project, object>>> {
                            x => x.TaskItems
                            })).FirstOrDefault();

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
                    _objectRepository.Modify(project);
                    dbContextScope.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return false;
        }
        public async Task<IReadOnlyList<TaskItemForMSProjectDto>> GetTaskForTableMSPAsync(Guid id, Guid userId, Guid? taskItemId, string numberIndex, int viewType = 0, int viewStatus = -1, bool isAll = false, IReadOnlyList<Guid> listParent = null, IReadOnlyList<UserDto> users = null)
        {
            var lstTaskItemForTable = new List<TaskItemForMSProjectDto>();
            try
            {
                if (users == null)
                {
                    users = _userServices.GetUsers();
                }
                //điều kiện lấy tất cả công việc theo projectId
                IReadOnlyList<Guid> listTaskOfUserAssign = new List<Guid>();
                if (isAll == false)
                {
                    var project = await GetById(id);
                    if (project.ManagerId.Contains(userId.ToString()))
                    {
                        isAll = true;
                    }
                    listTaskOfUserAssign = GetTaskOfUserAssign(id, userId, TaskType.Primary);
                    if (taskItemId.HasValue && listTaskOfUserAssign.Contains(taskItemId.Value))
                    {
                        isAll = true;
                    }
                    if (!isAll && listParent == null)
                    {
                        listParent = GetTaskParentOfUser(id, userId);
                    }
                }

                var lstTaskItem = await GetTaskAsyncV2(id, userId, taskItemId, users);
                if (isAll == false)
                {
                    lstTaskItem = lstTaskItem.Where(e => listParent.Contains(e.Id)).ToList();
                }
                if (lstTaskItem != null && lstTaskItem.Count > 0)
                {
                    int key = 1;

                    foreach (var item in lstTaskItem)
                    {
                        var newNumberIndex = string.IsNullOrEmpty(numberIndex) ? key.ToString() : string.Concat(numberIndex, ".", key); ;
                        var user = users.Where(e=>e.Id == item.AssignBy.Value).FirstOrDefault();
                        var hasAssignFollow = item.TaskItemAssigns.Where(p => p.AssignFollow != null).Count() > 0 ? true : false;

                        var taskItemForExcel = new TaskItemForMSProjectDto();
                        taskItemForExcel.Id = item.Id;
                        taskItemForExcel.DocId = item.ProjectId;
                        taskItemForExcel.ParentId = item.ParentId;
                        taskItemForExcel.IsGroupLabel = item.IsGroupLabel;
                        taskItemForExcel.PercentFinish = item.PercentFinish;
                        taskItemForExcel.No = newNumberIndex;
                        taskItemForExcel.AssignBy = item.AssignBy;
                        taskItemForExcel.AssignByName = user != null ? user.FullName : string.Empty;
                        taskItemForExcel.AssignByUsername = user != null ? user.UserName : string.Empty;
                        taskItemForExcel.AuthorId = item.CreatedBy;
                        taskItemForExcel.Name = item.TaskName;
                        taskItemForExcel.Content = item.Conclusion;
                        taskItemForExcel.StartTime = item.FromDate;
                        taskItemForExcel.FinishiDate = item.FinishedDate;
                        taskItemForExcel.EndTime = item.ToDate;
                        taskItemForExcel.HasReport = item.IsReport.HasValue ? item.IsReport.Value : false;
                        taskItemForExcel.HasSecurity = item.IsSecurity.HasValue ? item.IsSecurity.Value : false;
                        taskItemForExcel.HasWeirdo = item.IsWeirdo.HasValue ? item.IsWeirdo.Value : false;
                        taskItemForExcel.Status = item.TaskItemStatusId.HasValue ? (int)item.TaskItemStatusId.Value : 0;
                        taskItemForExcel.StatusName = item.TaskItemStatusId.HasValue ? GetStatusName(item.TaskItemStatusId.Value) : GetStatusName(TaskItemStatusId.New);
                        taskItemForExcel.HasAssignFollow = hasAssignFollow;
                        ///taskItemForExcel.IsAnyNewTaskItemAssign = item.IsAnyNewTaskItemAssign();
                        taskItemForExcel.IsOverDue = !TaskInDueDate.Main(item.ToDate, item.FinishedDate, item.TaskItemStatusId);
                        taskItemForExcel.TaskItemPriorityId = item.TaskItemPriorityId.HasValue ? (int)item.TaskItemPriorityId.Value : 0;
                        taskItemForExcel.TaskItemCategory = item.TaskItemCategory;
                        taskItemForExcel.Weight = item.Weight;
                        taskItemForExcel.MainWorkers = new List<UserExcelDto>();
                        taskItemForExcel.Supporters = new List<UserExcelDto>();
                        taskItemForExcel.WhoOnlyKnow = new List<UserExcelDto>();
                        taskItemForExcel.Visible = true;

                        if (item.IsSecurity == true && item.CreatedBy.HasValue && item.CreatedBy.Value != userId && !item.TaskItemAssigns.Any(p => p.AssignTo.HasValue && p.AssignTo.Value == userId) && !item.TaskItemAssigns.Any(p => p.AssignFollow.HasValue && p.AssignFollow.Value == userId))
                        {
                            taskItemForExcel.Visible = false;
                        }

                        if (item.TaskItemAssigns != null && item.TaskItemAssigns.Count > 0)
                        {
                            foreach (var subItem in item.TaskItemAssigns)
                            {
                                var userAssign = users.Where(e => e.Id == subItem.AssignTo.Value).FirstOrDefault(); 

                                if (userAssign != null)
                                {
                                    if (subItem.TaskType == TaskType.Primary)
                                    {
                                        taskItemForExcel.MainWorkers.Add(
                                            new UserExcelDto
                                            {
                                                ID = subItem.Id,
                                                Text = userAssign.FullName,
                                                Username = userAssign.UserName,
                                                CanProcess = subItem.TaskItemStatusId == TaskItemStatusId.New ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.InProcess ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.Read,
                                                PercentFinish = subItem.AppraisePercentFinish,
                                                FinishedDate = subItem.FinishedDate
                                            }
                                        );
                                    }
                                    else if (subItem.TaskType == TaskType.Support)
                                    {
                                        taskItemForExcel.Supporters.Add(
                                            new UserExcelDto
                                            {
                                                ID = subItem.Id,
                                                Text = userAssign.FullName,
                                                Username = userAssign.UserName,
                                                CanProcess = subItem.TaskItemStatusId == TaskItemStatusId.New ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.InProcess ||
                                                             subItem.TaskItemStatusId == TaskItemStatusId.Read,
                                                PercentFinish = subItem.AppraisePercentFinish,
                                                FinishedDate = subItem.FinishedDate
                                            }
                                        );
                                    }
                                    else if (subItem.TaskType == TaskType.ReadOnly)
                                    {
                                        taskItemForExcel.WhoOnlyKnow.Add(new UserExcelDto { ID = userAssign.Id, Text = userAssign.FullName, Username = userAssign.UserName });
                                    }
                                }

                            }
                        }



                        var lstTaskItemChildSub = await GetTaskForTableMSPAsync(id, userId, item.Id, taskItemForExcel.No, viewType, viewStatus, isAll, listParent, users);
                        taskItemForExcel.Childrens = lstTaskItemChildSub.ToList();
                        lstTaskItemForTable.Add(taskItemForExcel);
                        key++;
                    }
                }

                return lstTaskItemForTable;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return lstTaskItemForTable;
        }
    }
}
