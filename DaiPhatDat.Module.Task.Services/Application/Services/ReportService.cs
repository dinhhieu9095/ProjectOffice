using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public class ReportService : IReportService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ITaskItemRepository _objectRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IUserServices _userServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ReportService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, ITaskItemRepository objectRepository, IReportRepository reportRepository, IUserServices userServices, IUserDepartmentServices userDepartmentServices, IDepartmentServices departmentServices, ICategoryService categoryService)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _objectRepository = objectRepository;
            _reportRepository = reportRepository;
            _mapper = mapper;
            _userDepartmentServices = userDepartmentServices;
            _userServices = userServices;
            _departmentServices = departmentServices;
            _categoryService = categoryService;
        }
        public List<SunburstReportDto> BaoCaoTienDo(ReportFilterDto filter)
        {
            List<SunburstReportDto> dtos = new List<SunburstReportDto>();
            return dtos;
        }

        public async Task<bool> CreateAsync(ReportDto item)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var entity = _mapper.Map<Report>(item);
                _reportRepository.Add(entity);

                await scope.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> DeleteAsync(ReportDto model)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                // update user
                var entity = await _reportRepository.FindAsync(p => p.Id == (model.Id));
                if (entity == null)
                {
                    throw new Exception("Document template not found is delete !");
                }
                else
                {
                    entity.IsActive = false;
                    _reportRepository.Delete(entity);

                    await scope.SaveChangesAsync();
                    return true;
                }
            }
        }

        public async Task<Pagination<ReportDto>> GetPaginAsync(string keyword, int pageIndex, int pageSize, Guid userId, bool isAdmin)
        {
            var _lists = new List<ReportDto>();
            var result = new Pagination<ReportDto>(0, _lists);
            string guidStr = userId.ToString();
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var queryable = _reportRepository.GetAll();
                //queryable = queryable.Where(p => p.IsActive == true);

                if (!string.IsNullOrEmpty(keyword))
                {
                    queryable = queryable.Where(e => e.Name.Contains(keyword));
                }
                if (!isAdmin)
                    queryable = queryable.Where(e=>e.IsActive).Where(e => string.IsNullOrEmpty(e.Permission) || (!string.IsNullOrEmpty(e.Permission) && e.Permission.Contains(guidStr)));
                result.Count = queryable.Count();
                queryable = queryable.OrderBy(e => e.Name).Skip(pageSize * (pageIndex - 1)).Take(pageSize);

                _lists = queryable.Select(e => new ReportDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Link = e.Link,
                    LinkDesktop = e.LinkDesktop,
                    Icon = e.Icon,
                    CssClass = e.CssClass,
                    FileName = e.FileName,
                    IsActive = e.IsActive,
                    Type = e.Type
                }).ToList();
                result.Result = _lists;
            }
            return result;
        }

        public async Task<ReportDto> GetAsync(int id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var queryable = _reportRepository.GetAll();
                queryable = queryable.Where(p => p.Id == id);

                var reportDto = queryable.Select(e => new ReportDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Link = e.Link,
                    LinkDesktop = e.LinkDesktop,
                    Icon = e.Icon,
                    CssClass = e.CssClass,
                    FileName = e.FileName,
                    IsActive = e.IsActive,
                    Type = e.Type,
                    IsUser = e.IsUser,
                    Permission = e.Permission
                }).FirstOrDefault();

                if (!string.IsNullOrEmpty(reportDto.Permission))
                {
                    var userIds = reportDto.Permission.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList();
                    foreach (var userId in userIds)
                    {
                        var user = _userDepartmentServices.GetCachedUserDepartmentsByUser(userId).FirstOrDefault();
                        user.FullName = user.FullName + " - " + user.JobTitleName;
                        reportDto.Users.Add(user);
                    }
                }

                return reportDto;
            }
        }

        public async Task<ReportDto> GetAsyncByCode(string code)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var queryable = _reportRepository.GetAll();
                queryable = queryable.Where(p => p.Type == code);

                var reportDto = queryable.Select(e => new ReportDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Link = e.Link,
                    LinkDesktop = e.LinkDesktop,
                    Icon = e.Icon,
                    CssClass = e.CssClass,
                    FileName = e.FileName,
                    IsActive = e.IsActive,
                    Type = e.Type,
                    FileContent = e.FileContent
                }).FirstOrDefault();

                return reportDto;
            }
        }


        public async Task<ReportDto> GetFileAsync(int id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var queryable = _reportRepository.GetAll();
                queryable = queryable.Where(p => p.Id == id && p.IsActive == true);

                var report = queryable.FirstOrDefault();

                return _mapper.Map<ReportDto>(report);
            }
        }

        public async Task<bool> UpdateAsync(ReportDto item)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                // update user
                var entity = await _reportRepository.FindAsync(p => p.Id == (item.Id));
                if (entity == null)
                {
                    entity = new Report();
                    entity.Name = item.Name;
                    entity.LinkDesktop = item.LinkDesktop;
                    entity.Icon = item.Icon;
                    entity.CssClass = item.CssClass;
                    entity.Link = item.Link;
                    entity.IsActive = item.IsActive;
                    entity.Type = item.Type;
                    entity.Permission = item.Permission;
                    entity.IsUser = item.IsUser;

                    if (item.FileContent.Length > 0)
                    {
                        entity.FileName = item.FileName;
                        entity.FileContent = item.FileContent;
                    }
                    _reportRepository.Add(entity);
                }
                else
                {
                    entity.Name = item.Name;
                    entity.LinkDesktop = item.LinkDesktop;
                    entity.Icon = item.Icon;
                    entity.CssClass = item.CssClass;
                    entity.Link = item.Link;
                    entity.IsActive = item.IsActive;
                    entity.Type = item.Type;
                    entity.Permission = item.Permission;
                    entity.IsUser = item.IsUser;
                    if (item.FileContent.Length > 0)
                    {
                        entity.FileName = item.FileName;
                        entity.FileContent = item.FileContent;
                    }
                    _reportRepository.Modify(entity);
                }
                await scope.SaveChangesAsync();
                return true;

            }
        }

        public async Task<List<ReportResultDto>> ProjectReportWeeklyAsync(
            ReportFilterDto userReportQuery,
            CancellationToken cancellationToken = default)
        {
            List<ReportResultDto> userReportResults = new List<ReportResultDto>();
            DateTime baseDate = userReportQuery.FromDate ?? DateTime.Now;
            bool isNextWeek = true;
            var today = baseDate;
            int dayOfWeek = (int)baseDate.DayOfWeek == 0 ? 0 : (int)baseDate.DayOfWeek;
            var thisWeekStart = baseDate.AddDays(-dayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7);
            var nextWeekStart = thisWeekEnd.AddDays(1);
            var nextWeekEnd = nextWeekStart.AddDays(7);

            var departmentId = userReportQuery.DepartmentId;
            var departments = await _departmentServices.GetDepartmentsAsync();
            var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
            var deparmentIds = departmentArgs.Select(e => e.Id).ToList();
            deparmentIds.Add(departmentId.Value);
            var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

            var userIds = new List<Guid>();
            if (userReportQuery.UserOfDepartmentId == null)
            {
                userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();
            }
            else
            {
                userIds.Add(userReportQuery.UserOfDepartmentId.Value);
            }

            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemPredicateBuilder = PredicateBuilder.New<TaskItem>(x => true);
                var taskItemAssignPredicateBuilder = PredicateBuilder.New<TaskItemAssign>(x => true);

                var fromDate = thisWeekStart;
                var toDate = thisWeekEnd;
                var isReport = userReportQuery.IsReport;
                var isAssignBy = userReportQuery.IsAssignBy;
                var isAssignTo = userReportQuery.IsAssignTo;
                var trackingProgress = userReportQuery.TrackingProgress;
                var trackingStatus = userReportQuery.TrackingStatus;
                var trackingCrucial = userReportQuery.TrackingCrucial;
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.Project.IsActive == true && !x.IsDeleted && x.IsGroupLabel != true);
                if (userReportQuery.ProjectId.HasValue && userReportQuery.ProjectId != Guid.Empty)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.ProjectId == userReportQuery.ProjectId);
                }
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                    x => DbFunctions.TruncateTime(x.FromDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(toDate));

                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value) || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && userIds.Contains(s.AssignTo.Value))));
                if (isReport == true)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.IsReport == true);
                    //if (userId != null)
                    //{
                    //    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && x.AssignBy.Value == userId || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && s.AssignTo.Value == userId)));
                    //}
                    //else
                    //    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value) || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && userIds.Contains(s.AssignTo.Value))));
                }

                if (isAssignBy == true)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value));
                }

                if (isAssignTo == true)
                {
                    taskItemAssignPredicateBuilder = taskItemAssignPredicateBuilder.And(x => x.AssignTo.HasValue && userIds.Contains(x.AssignTo.Value));
                }

                if (trackingProgress != null &&
                    trackingProgress != -1)
                {
                    if (trackingProgress == 0)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate <= x.ToDate : x.ToDate > DateTime.Now);

                    if (trackingProgress == 1)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate > x.ToDate : x.ToDate <= DateTime.Now);
                }

                if (trackingStatus != null &&
                    trackingStatus != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemStatusId == (TaskItemStatusId)trackingStatus);

                if (trackingCrucial != null &&
                    trackingCrucial != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemPriorityId == (TaskItemPriorityId)trackingCrucial);
                //var projectKinds = dbContextReadOnlyScope.DbContexts
                //    .Get<TaskContext>()
                //    .Set<ProjectKind>()
                var taskItems = (await dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .Include(x => x.Project)
                    .Include(x => x.TaskItemPriority)
                    .Include(x => x.TaskItemStatus)
                    .Include(x => x.TaskItemAssigns)
                    .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemProcessHistories))
                    .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemStatus))
                    .Where(taskItemPredicateBuilder)
                    .OrderBy(x => x.Order).ThenBy(x => x.CreatedDate)
                    .ThenBy(x => x.TaskName)
                    .ToListAsync(cancellationToken))
                    .Where(x => x.TaskItemAssigns.Any())
                    .Select(x => new TaskItemDto
                    {
                        Id = x.Id,
                        ProjectId = x.ProjectId,
                        ParentId = x.ParentId,
                        ProjectPercentFinish = x.Project.PercentFinish,
                        TaskName = x.TaskName,
                        TaskItemStatus = new TaskItemStatusDto()
                        {
                            Name = x.TaskItemStatus.Name
                        },
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        AssignBy = x.AssignBy,
                        IsGroupLabel = x.IsGroupLabel,
                        DepartmentId = x.DepartmentId,
                        NatureTaskId = x.NatureTaskId,
                        PercentFinish = x.PercentFinish,
                        TaskItemPriority = new TaskItemPriorityDto
                        {
                            Name = x.TaskItemPriority.Name
                        },
                        TaskItemAssigns = x.TaskItemAssigns.Where(taskItemAssignPredicateBuilder)
                            .OrderBy(y => y.TaskType)
                            .Select(y => new TaskItemAssignDto
                            {
                                Id = y.Id,
                                AssignTo = y.AssignTo,
                                DepartmentId = y.DepartmentId,
                                PercentFinish = y.PercentFinish ?? 0,
                                AppraisePercentFinish = y.AppraisePercentFinish ?? 0,
                                AppraiseProcess = y.AppraiseProcess,
                                LastResult = y.LastResult,
                                ExtendDescription = y.ExtendDescription,
                                Problem = y.Problem,
                                Solution = y.Solution,
                                StatusName = y.TaskItemStatusId.HasValue ? y.TaskItemStatus.Name : string.Empty,
                                FinishedDate = y.FinishedDate,
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                TaskType = y.TaskType,
                                AppraiseResult = y.AppraiseResult,

                            }).ToList()
                    })
                    .ToList();

                var taskItemDtos = new List<ReportItemDto>();

                for (var i = 0; i < taskItems.Count; i++)
                {
                    var x = taskItems.ElementAt(i);

                    var taskItemDto = new ReportItemDto
                    {
                        ShowType = x.IsGroupLabel == true
                            ? ShowType.Group
                            : ShowType.TaskItem,
                        Id = x.Id.ToString(),
                        ProjectId = x.ProjectId.ToString(),
                        DepartmentId = x.DepartmentId.GetValueOrDefault().ToString(),
                        ParentId = x.ParentId.HasValue && x.ParentId.Value != Guid.Empty ? x.ParentId.ToString() : x.ProjectId.ToString(),
                        Name = x.TaskName,
                    };

                    if (taskItemDto.ShowType == ShowType.Group)
                        taskItemDtos.Add(taskItemDto);

                    if (x.TaskItemAssigns != null && x.TaskItemAssigns.Any())
                    {
                        int assignCount = x.TaskItemAssigns.Count();
                        for (var j = 0; j < assignCount; j++)
                        {
                            var assign = string.Empty;
                            var lastResult = string.Empty;
                            var extendDescription = string.Empty;
                            var problem = string.Empty;
                            var solution = string.Empty;

                            var taskItemAssign = x.TaskItemAssigns.ElementAt(j);

                            var userDepartment =
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == taskItemAssign.AssignTo &&
                                         y.DeptID == taskItemAssign.DepartmentId) ??
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == taskItemAssign.AssignTo);

                            if (userDepartment == null)
                                continue;

                            assign = $"{userDepartment.FullName} ({userDepartment.JobTitleName})";

                            if (!string.IsNullOrEmpty(taskItemAssign.LastResult))
                                lastResult = $"{taskItemAssign.LastResult}";

                            if (!string.IsNullOrEmpty(taskItemAssign.ExtendDescription))
                                extendDescription = $"{taskItemAssign.ExtendDescription}";

                            if (!string.IsNullOrEmpty(taskItemAssign.Problem))
                                problem = $"{taskItemAssign.Problem}";

                            if (!string.IsNullOrEmpty(taskItemAssign.Solution))
                                solution = $"{taskItemAssign.Solution}";

                            var taskItemAssignDto = new ReportItemDto()
                            {
                                Id = taskItemAssign.TaskType == TaskType.Primary ? taskItemDto.Id : taskItemAssign.Id.ToString(),
                                ShowType = ShowType.TaskItemAssign,
                                ProjectId = taskItemDto.ProjectId,
                                ParentId = taskItemDto.ParentId,
                                Name = taskItemDto.Name, // Nội dung CV
                                Assign = assign, // người thực hiện
                                AssignBy = taskItemDto.AssignBy,
                                LastResult = lastResult, //diễn giải

                                FromDate = taskItemAssign.FromDate, //Từ ngày
                                ToDate = taskItemAssign.ToDate, //Đến ngày
                                FinishedDate = taskItemAssign.FinishedDate,
                                PercentFinish = taskItemAssign.PercentFinish ?? 0, // tỷ lệ hoàn thành
                                AppraisePercentFinish = taskItemAssign.AppraisePercentFinish ?? 0,
                                AppraiseProcess = taskItemAssign.AppraiseProcess, //Điểm tiến độ CV
                                StatusName = taskItemAssign.StatusName, //Điểm tiến độ CV
                                ContentAppraise = taskItemAssign.AppraiseResult, // Ý kiến đánh giá
                                DepartmentId = taskItemAssign.DepartmentId.GetValueOrDefault().ToString() // Ý kiến đánh giá
                            };
                            List<string> notes = new List<string>();
                            string tile = string.Format(@"Tỉ lệ hoàn thành: {0}", taskItemAssignDto.AppraisePercentFinish ?? 0);
                            string diem = string.Format(@"Điểm tiến độ: {0}", !string.IsNullOrEmpty(taskItemAssignDto.AppraiseProcess) ? taskItemAssignDto.AppraiseProcess : "0");
                            string status = string.Format(@"Tình trạng: {0}", taskItemAssignDto.StatusName);
                            string yKien = string.Format(@"Đánh giá: {0}", taskItemAssignDto.ContentAppraise);
                            notes.Add(tile);
                            notes.Add(diem);
                            notes.Add(status);
                            notes.Add(yKien);
                            taskItemAssignDto.Note = string.Join("<br />", notes);
                            taskItemAssignDto.NoteExcel = string.Join("\r\n", notes);
                            taskItemDtos.Add(taskItemAssignDto);
                        }
                    }

                }

                taskItemDtos.InsertRange(0, await ToUserProjectTaskItemWeeklys(taskItemDtos));

                userReportResults.Add(new ReportResultDto
                {
                    Result = FlattenTaskItem(HierarchicalTaskItem(taskItemDtos)).ToList()
                });
            }
            if (isNextWeek)
            {
                using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var taskItemPredicateBuilder = PredicateBuilder.New<TaskItem>(x => true);
                    var taskItemAssignPredicateBuilder = PredicateBuilder.New<TaskItemAssign>(x => true);

                    var fromDate = nextWeekStart;
                    var toDate = nextWeekEnd;
                    var isReport = userReportQuery.IsReport;
                    var isAssignBy = userReportQuery.IsAssignBy;
                    var isAssignTo = userReportQuery.IsAssignTo;
                    var trackingProgress = userReportQuery.TrackingProgress;
                    var trackingStatus = userReportQuery.TrackingStatus;
                    var trackingCrucial = userReportQuery.TrackingCrucial;
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.Project.IsActive == true && !x.IsDeleted && x.IsGroupLabel != true);
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => DbFunctions.TruncateTime(x.FromDate) >= DbFunctions.TruncateTime(fromDate) &&
                             DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(toDate) ||
                             DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(fromDate) &&
                             DbFunctions.TruncateTime(x.ToDate) <= DbFunctions.TruncateTime(toDate) ||
                             DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(fromDate) &&
                             DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(toDate));

                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value) || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && userIds.Contains(s.AssignTo.Value))));

                    if (userReportQuery.ProjectId.HasValue && userReportQuery.ProjectId != Guid.Empty)
                    {
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.ProjectId == userReportQuery.ProjectId);
                    }
                    if (isReport == true)
                    {
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.IsReport == true);
                    }

                    if (isAssignBy == true)
                    {
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value));
                    }

                    if (isAssignTo == true)
                    {
                        taskItemAssignPredicateBuilder = taskItemAssignPredicateBuilder.And(x => x.AssignTo.HasValue && userIds.Contains(x.AssignTo.Value));
                    }

                    if (trackingProgress != null &&
                        trackingProgress != -1)
                    {
                        if (trackingProgress == 0)
                            taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                                x => x.FinishedDate != null ? x.FinishedDate <= x.ToDate : x.ToDate > DateTime.Now);

                        if (trackingProgress == 1)
                            taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                                x => x.FinishedDate != null ? x.FinishedDate > x.ToDate : x.ToDate <= DateTime.Now);
                    }

                    if (trackingStatus != null &&
                        trackingStatus != -1)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.TaskItemStatusId == (TaskItemStatusId)trackingStatus);

                    if (trackingCrucial != null &&
                        trackingCrucial != -1)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.TaskItemPriorityId == (TaskItemPriorityId)trackingCrucial);
                    //var projectKinds = dbContextReadOnlyScope.DbContexts
                    //    .Get<TaskContext>()
                    //    .Set<ProjectKind>()
                    var taskItems = (await dbContextReadOnlyScope.DbContexts
                        .Get<TaskContext>()
                        .Set<TaskItem>()
                        .Include(x => x.Project)
                        .Include(x => x.TaskItemPriority)
                        .Include(x => x.TaskItemStatus)
                        .Include(x => x.TaskItemAssigns)
                        .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemProcessHistories))
                        .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemStatus))
                        .Where(taskItemPredicateBuilder)
                        .OrderBy(x => x.CreatedDate)
                        .ThenBy(x => x.TaskName)
                        .ToListAsync(cancellationToken))
                        .Where(x => x.TaskItemAssigns.Any())
                        .Select(x => new TaskItemDto
                        {
                            Id = x.Id,
                            ProjectId = x.ProjectId,
                            ParentId = x.ParentId,
                            ProjectPercentFinish = x.Project.PercentFinish,
                            TaskName = x.TaskName,
                            TaskItemStatus = new TaskItemStatusDto()
                            {
                                Name = x.TaskItemStatus.Name
                            },
                            FromDate = x.FromDate,
                            ToDate = x.ToDate,
                            FinishedDate = x.FinishedDate,
                            AssignBy = x.AssignBy,
                            IsGroupLabel = x.IsGroupLabel,
                            DepartmentId = x.DepartmentId,
                            NatureTaskId = x.NatureTaskId,
                            PercentFinish = x.PercentFinish,
                            TaskItemPriority = new TaskItemPriorityDto
                            {
                                Name = x.TaskItemPriority.Name
                            },
                            TaskItemAssigns = x.TaskItemAssigns.Where(taskItemAssignPredicateBuilder)
                                .OrderBy(y => y.TaskType)
                                .Select(y => new TaskItemAssignDto
                                {
                                    Id = y.Id,
                                    AssignTo = y.AssignTo,
                                    DepartmentId = y.DepartmentId,
                                    PercentFinish = y.PercentFinish ?? 0,
                                    AppraisePercentFinish = y.AppraisePercentFinish ?? 0,
                                    AppraiseProcess = y.AppraiseProcess,
                                    LastResult = y.LastResult,
                                    ExtendDescription = y.ExtendDescription,
                                    Problem = y.Problem,
                                    Solution = y.Solution,
                                    StatusName = y.TaskItemStatusId.HasValue ? y.TaskItemStatus.Name : string.Empty,
                                    FinishedDate = y.FinishedDate,
                                    FromDate = y.FromDate,
                                    ToDate = y.ToDate,
                                    TaskType = y.TaskType,
                                    AppraiseResult = y.AppraiseResult,

                                }).ToList()
                        })
                        .ToList();

                    var taskItemDtos = new List<ReportItemDto>();

                    for (var i = 0; i < taskItems.Count(); i++)
                    {
                        var x = taskItems.ElementAt(i);

                        var taskItemDto = new ReportItemDto
                        {
                            ShowType = x.IsGroupLabel == true
                                ? ShowType.Group
                                : ShowType.TaskItem,
                            Id = x.Id.ToString(),
                            ProjectId = x.ProjectId.ToString(),
                            ParentId = x.ParentId.HasValue && x.ParentId.Value != Guid.Empty ? x.ParentId.ToString() : x.ProjectId.ToString(),
                            Name = x.TaskName,
                        };
                        if (taskItemDto.ShowType == ShowType.Group)
                            taskItemDtos.Add(taskItemDto);
                        if (x.TaskItemAssigns != null && x.TaskItemAssigns.Any())
                        {
                            int assignCount = x.TaskItemAssigns.Count();
                            for (var j = 0; j < assignCount; j++)
                            {
                                var assign = string.Empty;
                                var lastResult = string.Empty;
                                var extendDescription = string.Empty;
                                var problem = string.Empty;
                                var solution = string.Empty;

                                var taskItemAssign = x.TaskItemAssigns.ElementAt(j);

                                var userDepartment =
                                    userDepartments.FirstOrDefault(
                                        y => y.UserID == taskItemAssign.AssignTo &&
                                             y.DeptID == taskItemAssign.DepartmentId) ??
                                    userDepartments.FirstOrDefault(
                                        y => y.UserID == taskItemAssign.AssignTo);

                                if (userDepartment == null)
                                    continue;

                                assign = $"{userDepartment.FullName} ({userDepartment.JobTitleName})";

                                if (!string.IsNullOrEmpty(taskItemAssign.LastResult))
                                    lastResult = $"{taskItemAssign.LastResult}";

                                if (!string.IsNullOrEmpty(taskItemAssign.ExtendDescription))
                                    extendDescription = $"{taskItemAssign.ExtendDescription}";

                                if (!string.IsNullOrEmpty(taskItemAssign.Problem))
                                    problem = $"{taskItemAssign.Problem}";

                                if (!string.IsNullOrEmpty(taskItemAssign.Solution))
                                    solution = $"{taskItemAssign.Solution}";

                                var taskItemAssignDto = new ReportItemDto
                                {
                                    Id = taskItemAssign.TaskType == TaskType.Primary ? taskItemDto.Id : taskItemAssign.Id.ToString(),
                                    ShowType = ShowType.TaskItemAssign,
                                    ProjectId = taskItemDto.ProjectId,
                                    ParentId = taskItemDto.ParentId,
                                    Name = taskItemDto.Name, // Nội dung CV
                                    Assign = assign, // người thực hiện
                                    AssignBy = taskItemDto.AssignBy,
                                    LastResult = lastResult, //diễn giải

                                    FromDate = taskItemAssign.FromDate, //Từ ngày
                                    ToDate = taskItemAssign.ToDate, //Đến ngày
                                    FinishedDate = taskItemAssign.FinishedDate,
                                    PercentFinish = taskItemAssign.PercentFinish ?? 0, // tỷ lệ hoàn thành
                                    AppraisePercentFinish = taskItemAssign.AppraisePercentFinish ?? 0,
                                    AppraiseProcess = taskItemAssign.AppraiseProcess, //Điểm tiến độ CV
                                    StatusName = taskItemAssign.StatusName, //Điểm tiến độ CV
                                    ContentAppraise = taskItemAssign.AppraiseResult // Ý kiến đánh giá
                                };
                                List<string> notes = new List<string>();
                                string tile = string.Format(@"Tỉ lệ hoàn thành: {0}", taskItemAssignDto.AppraisePercentFinish ?? 0);
                                string diem = string.Format(@"Điểm tiến độ: {0}", !string.IsNullOrEmpty(taskItemAssignDto.AppraiseProcess) ? taskItemAssignDto.AppraiseProcess : "0");
                                string status = string.Format(@"Tình trạng: {0}", taskItemAssignDto.StatusName);
                                string yKien = string.Format(@"Đánh giá: {0}", taskItemAssignDto.ContentAppraise);
                                notes.Add(tile);
                                notes.Add(diem);
                                notes.Add(status);
                                notes.Add(yKien);
                                taskItemAssignDto.Note = string.Join("<br />", notes);
                                taskItemAssignDto.NoteExcel = string.Join("\r\n", notes);
                                taskItemDtos.Add(taskItemAssignDto);
                            }
                        }

                    }

                    taskItemDtos.InsertRange(0, await ToUserProjectTaskItemWeeklys(taskItemDtos));

                    userReportResults.Add(new ReportResultDto
                    {
                        Result = FlattenTaskItem(HierarchicalTaskItem(taskItemDtos)).ToList()
                    });
                }

            }
            return userReportResults;
        }

        public async Task<ReportResultDto> ProjectReportAsync(
            ReportFilterDto userReportQuery,
            CancellationToken cancellationToken = default)
        {
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemPredicateBuilder = PredicateBuilder.New<TaskItem>(x => true);
                var taskItemAssignPredicateBuilder = PredicateBuilder.New<TaskItemAssign>(x => true);

                var departmentId = userReportQuery.DepartmentId;
                var departments = await _departmentServices.GetDepartmentsAsync();
                var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
                var deparmentIds = departmentArgs.Select(e => e.Id).ToList();
                if (departmentId != null)
                    deparmentIds.Add(departmentId.Value);
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var userIds = new List<Guid>();
                if (userReportQuery.UserOfDepartmentId.HasValue)
                {
                    userIds.Add(userReportQuery.UserOfDepartmentId.Value);
                }
                else
                    userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();

                var fromDate = userReportQuery.FromDate;
                var toDate = userReportQuery.ToDate;
                var isReport = userReportQuery.IsReport;
                var isAssignBy = userReportQuery.IsAssignBy;
                var isAssignTo = userReportQuery.IsAssignTo;
                var trackingProgress = userReportQuery.TrackingProgress;
                var trackingStatus = userReportQuery.TrackingStatus;
                var trackingCrucial = userReportQuery.TrackingCrucial;
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.Project.IsActive == true && !x.IsDeleted);
                if (userReportQuery.ProjectId.HasValue && userReportQuery.ProjectId != Guid.Empty)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.ProjectId == userReportQuery.ProjectId.Value);
                }
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                    x => DbFunctions.TruncateTime(x.FromDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(toDate));

                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value) || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && userIds.Contains(s.AssignTo.Value))));

                if (isReport == true)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.IsReport == true);
                }

                if (isAssignBy == true)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value));

                if (isAssignTo == true)
                    taskItemAssignPredicateBuilder = taskItemAssignPredicateBuilder.And(x => x.AssignTo.HasValue && userIds.Contains(x.AssignTo.Value));

                if (trackingProgress != null &&
                    trackingProgress != -1)
                {
                    if (trackingProgress == 0)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate <= x.ToDate : x.ToDate > DateTime.Now);

                    if (trackingProgress == 1)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate > x.ToDate : x.ToDate <= DateTime.Now);
                }

                if (trackingStatus != null &&
                    trackingStatus != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemStatusId == (TaskItemStatusId)trackingStatus);

                if (trackingCrucial != null &&
                    trackingCrucial != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemPriorityId == (TaskItemPriorityId)trackingCrucial);
                //var projectKinds = dbContextReadOnlyScope.DbContexts
                //    .Get<TaskContext>()
                //    .Set<ProjectKind>()
                var taskItems = (await dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .Include(x => x.Project)
                    .Include(x => x.TaskItemPriority)
                    .Include(x => x.TaskItemStatus)
                    .Include(x => x.TaskItemAssigns)
                    //.IncludeOptimized(x => x.TaskItemAssigns.Select(y => y.TaskItemStatus))
                    .Where(taskItemPredicateBuilder)
                    .OrderBy(x => x.CreatedDate)
                    .ThenBy(x => x.TaskName)
                    .ToListAsync(cancellationToken))
                    .Where(x => x.TaskItemAssigns.Any() && x.IsDeleted == false && x.Project.IsActive == true)
                    .Select(x => new TaskItemDto
                    {
                        Id = x.Id,
                        ProjectId = x.ProjectId,
                        ParentId = x.ParentId,
                        TaskName = x.TaskName,
                        TaskItemStatus = new TaskItemStatusDto()
                        {
                            Name = x.TaskItemStatus.Name
                        },
                        TaskItemStatusId = x.TaskItemStatus.Id,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        AssignBy = x.AssignBy,
                        IsGroupLabel = x.IsGroupLabel,
                        DepartmentId = x.DepartmentId,
                        NatureTaskId = x.NatureTaskId,
                        PercentFinish = x.TaskItemAssigns.FirstOrDefault(y => y.TaskType == TaskType.Primary)?.PercentFinish,
                        AppraisePercentFinish = x.IsGroupLabel == true ? null : (x.PercentFinish == null || x.PercentFinish == 0 ? (x.TaskItemAssigns.FirstOrDefault(y => y.TaskType == TaskType.Primary)?.AppraisePercentFinish) : x.PercentFinish),
                        TaskItemPriority = new TaskItemPriorityDto
                        {
                            Name = x.TaskItemPriority?.Name
                        }
                        ,
                        TaskItemAssigns = x.TaskItemAssigns.Where(taskItemAssignPredicateBuilder)
                            .OrderBy(y => y.TaskType)
                            .Select(y => new TaskItemAssignDto
                            {
                                Id = y.Id,
                                AssignTo = y.AssignTo,
                                DepartmentId = y.DepartmentId,
                                PercentFinish = y.PercentFinish,
                                AppraisePercentFinish = y.AppraisePercentFinish == null ? 0 : y.AppraisePercentFinish,
                                AppraiseProcess = y.AppraiseProcess == null ? "0" : y.AppraiseProcess,
                                LastResult = y.LastResult,
                                ExtendDescription = y.ExtendDescription,
                                TaskItemStatusId = y.TaskItemStatusId,
                                Problem = y.Problem,
                                Solution = y.Solution,
                                FinishedDate = y.FinishedDate,
                                FromDate = y.FromDate,
                                ToDate = y.ToDate
                            }).ToList()
                    })
                    .ToList();

                var taskItemDtos = new List<ReportItemDto>();

                var statuses = await _categoryService.GetAllTaskItemStatuses();

                for (var i = 0; i < taskItems.Count; i++)
                {
                    var x = taskItems.ElementAt(i);

                    var taskItemDto = new ReportItemDto
                    {
                        ShowType = x.IsGroupLabel == true
                            ? ShowType.Group
                            : ShowType.TaskItem,
                        Id = x.Id.ToString(),
                        ProjectId = x.ProjectId.ToString(),
                        ParentId = x.ParentId.HasValue && x.ParentId.Value != Guid.Empty ? x.ParentId.ToString() : x.ProjectId.ToString(),
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        AppraisePercentFinish = x.AppraisePercentFinish,
                        PercentFinish = x.PercentFinish,
                        Name = x.TaskName,
                        StatusName = x.TaskItemStatus.Name,
                        TaskStatusId = (int)x.TaskItemStatusId,
                        NatureTask = x.NatureTaskId == 0 ? "Thường xuyên" : (x.NatureTaskId == 1 ? "Kế hoạch" : x.NatureTaskId == 2 ? "Đột xuất" : string.Empty),
                        BlueprintTime = ConvertToStringExtensions.DateToStringLocalNewLine(x.FromDate, x.ToDate, "f"),
                        ActualTime = ConvertToStringExtensions.DateToStringLocalNewLine(x.FromDate, x.FinishedDate, "f"),
                        TaskItemPriorityName = x.TaskItemPriority.Name,
                        TaskItemCategory = x.TaskItemCategory,
                    };

                    if (x.IsGroupLabel != true)
                    {
                        if (x.FinishedDate.HasValue)
                            taskItemDto.Progress = x.FinishedDate <= x.ToDate ? "Trong hạn" : "Quá hạn";
                        else if (x.ToDate > DateTime.Now)
                            taskItemDto.Progress = "Trong hạn";
                        else
                            taskItemDto.Progress = "Quá hạn";
                    }

                    var userDepartmentAssignBy =
                        userDepartments.FirstOrDefault(
                            y => y.UserID == x.AssignBy &&
                                    y.DeptID == x.DepartmentId) ??
                        userDepartments.FirstOrDefault(
                            y => y.UserID == x.AssignBy);

                    if (userDepartmentAssignBy != null)
                        taskItemDto.AssignBy = $"{userDepartmentAssignBy.FullName} ({userDepartmentAssignBy.JobTitleName})";
                    taskItemDtos.Add(taskItemDto);
                    if (x.TaskItemAssigns.Any())
                    {
                        int assignCount = x.TaskItemAssigns.Count();
                        for (var j = 0; j < assignCount; j++)
                        {
                            var assign = string.Empty;
                            var lastResult = string.Empty;
                            var extendDescription = string.Empty;
                            var problem = string.Empty;
                            var solution = string.Empty;

                            var taskItemAssign = x.TaskItemAssigns.ElementAt(j);

                            var userDepartment =
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == taskItemAssign.AssignTo &&
                                         y.DeptID == taskItemAssign.DepartmentId) ??
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == taskItemAssign.AssignTo);

                            if (userDepartment == null)
                                continue;

                            assign = $"{userDepartment.FullName} ({userDepartment.JobTitleName})";

                            if (!string.IsNullOrEmpty(taskItemAssign.LastResult))
                                lastResult = $"{taskItemAssign.LastResult}";

                            if (!string.IsNullOrEmpty(taskItemAssign.ExtendDescription))
                                extendDescription = $"{taskItemAssign.ExtendDescription}";

                            if (!string.IsNullOrEmpty(taskItemAssign.Problem))
                                problem = $"{taskItemAssign.Problem}";

                            if (!string.IsNullOrEmpty(taskItemAssign.Solution))
                                solution = $"{taskItemAssign.Solution}";
                            string progress = "";
                            if (x.FinishedDate.HasValue)
                                progress = taskItemAssign.FinishedDate <= taskItemAssign.ToDate ? "Trong hạn" : "Quá hạn";
                            else if (taskItemAssign.ToDate > DateTime.Now)
                                progress = "Trong hạn";
                            else
                                progress = "Quá hạn";

                            var taskItemAssignDto = new ReportItemDto
                            {
                                Id = taskItemAssign.Id.ToString(),
                                ShowType = ShowType.TaskItemAssign,
                                ProjectId = taskItemDto.ProjectId,
                                ParentId = taskItemDto.Id,
                                Assign = assign,
                                AssignBy = taskItemDto.AssignBy,
                                LastResult = lastResult,
                                ExtendDescription = extendDescription,
                                Problem = problem,
                                Solution = solution,
                                FromDate = taskItemAssign.FromDate,
                                ToDate = taskItemAssign.ToDate,
                                FinishedDate = taskItemAssign.FinishedDate,
                                StatusName = statuses.FirstOrDefault(e => taskItemAssign.TaskItemStatusId == e.Id)?.Name,
                                Progress = progress,
                                PercentFinish = taskItemAssign.PercentFinish == null ? 0 : taskItemAssign.PercentFinish,
                                AppraisePercentFinish = taskItemAssign.AppraisePercentFinish == null ? 0 : taskItemAssign.AppraisePercentFinish,
                                AppraiseProcess = taskItemAssign.AppraiseProcess == null ? "0" : taskItemAssign.AppraiseProcess,
                            };

                            taskItemDtos.Add(taskItemAssignDto);
                        }
                    }

                }

                var taskItemProjectIds = taskItemDtos.Select(x => Guid.Parse(x.ProjectId)).ToList();
                taskItemDtos.InsertRange(0, await ToUserProjectTaskItems(taskItemProjectIds));

                return new ReportResultDto
                {
                    Result = FlattenTaskItem(HierarchicalTaskItem(taskItemDtos)).ToList()
                };
            }
        }

        public async Task<byte[]> ExportReportOnepage(byte[] template, ReportFilterDto reportFilter)
        {
            byte[] fileResult = null;
            var stream = new MemoryStream(template);
            using (var package = new ExcelPackage(stream))
            {
                EplusExtension eplusExtension = new EplusExtension();
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(e => e.Name == reportFilter.ReportType);

                var worksheetOther = package.Workbook.Worksheets.Where(e => e.Name != reportFilter.ReportType).ToList();
                foreach (var item in worksheetOther)
                {
                    item.Hidden = eWorkSheetHidden.Hidden;
                }

                if (reportFilter.ReportType == "week")
                {
                    List<ReportOnepageWeekly> reports = await ReportOnepageWeeklyAsync(reportFilter);
                    if (!reports.Any())
                    {
                        reports.Add(new ReportOnepageWeekly());
                    }
                    FillData(reports, worksheet);
                }
                else if (reportFilter.ReportType == "month")
                {
                    List<ReportOnepageMonthly> reports = await ReportOnepageMonthlyAsync(reportFilter);
                    if (!reports.Any())
                    {
                        reports.Add(new ReportOnepageMonthly());
                    }
                    FillData(reports, worksheet);
                }
                else if (reportFilter.ReportType == "quarter")
                {
                    List<ReportOnepageQuaterly> reports = await ReportOnepageQuaterlyAsync(reportFilter);
                    if (!reports.Any())
                    {
                        reports.Add(new ReportOnepageQuaterly());
                    }
                    FillData<ReportOnepageQuaterly>(reports, worksheet);
                }
                else if (reportFilter.ReportType == "year")
                {
                    List<ReportOnepageAnnual> reports = await ReportOnepageAnnualAsync(reportFilter);
                    if (!reports.Any())
                    {
                        reports.Add(new ReportOnepageAnnual());
                    }
                    FillData(reports, worksheet);
                }

                fileResult = package.GetAsByteArray();
            }
            return fileResult;
        }

        public async Task<List<ReportOnepageWeekly>> ReportOnepageWeeklyAsync(
           ReportFilterDto userReportQuery,
           CancellationToken cancellationToken = default)
        {
            List<ReportOnepageWeekly> userReportResults = new List<ReportOnepageWeekly>();
            DateTime baseDate = userReportQuery.FromDate ?? DateTime.Now;
            var today = baseDate;
            int dayOfWeek = (int)baseDate.DayOfWeek == 0 ? 0 : (int)baseDate.DayOfWeek;
            var thisWeekStart = baseDate.AddDays(-dayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7);
            var nextWeekStart = thisWeekEnd.AddDays(1);
            var nextWeekEnd = nextWeekStart.AddDays(7);
            if (userReportQuery.ToDate.HasValue)
            {

            }
            var departmentId = userReportQuery.DepartmentId;
            var departments = await _departmentServices.GetDepartmentsAsync();
            var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            List<Guid> deparmentIds = new List<Guid>();
            var userIds = new List<Guid>();
            if (userReportQuery.UserOfDepartmentId == null)
            {
                var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
                deparmentIds = departmentArgs.Select(e => e.Id).ToList();
                deparmentIds.Add(departmentId.Value);
                userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();
            }
            else
            {

                userIds.Add(userReportQuery.UserOfDepartmentId.Value);
            }

            var taskItemFlats = new List<ReportItemDto>();
            var fromDate = thisWeekStart;
            var toDate = thisWeekEnd;
            taskItemFlats = await GetReportTaskItemFlats(userReportQuery, fromDate, toDate, userIds, cancellationToken, userDepartments);
            if (userReportQuery.ReportType == "week")
            {
                foreach (var task in taskItemFlats)
                {

                    List<string> week = new List<string>();
                    if (task.ShowType == ShowType.TaskItem)
                    {
                        for (DateTime i = fromDate.Date.AddDays(-1); i < fromDate.Date.AddDays(8); i = i.AddDays(1))
                        {
                            string checkInWeek = "";
                            if (task.FromDate.HasValue && task.ToDate.HasValue && task.FromDate.Value.Date <= i && task.ToDate.Value.Date >= i)
                            {
                                if (task.IsLate)
                                {
                                    checkInWeek = "#e7505a";
                                }
                                else
                                {
                                    checkInWeek = "#49a942";
                                }
                            }
                            week.Add(checkInWeek);
                        }
                        ReportOnepageWeekly report = new ReportOnepageWeekly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FromDate = task.FromDate,
                            FinishDateText = task.FinishedDateText,
                            ToDate = task.ToDate,
                            AssignName = task.AssignName,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                            DayBefore = week[0],
                            T2 = week[1],
                            T3 = week[2],
                            T4 = week[3],
                            T5 = week[4],
                            T6 = week[5],
                            T7 = week[6],
                            CN = week[7],
                            DayAfter = week[8],
                        };
                        userReportResults.Add(report);
                    }
                    else
                    {
                        ReportOnepageWeekly report = new ReportOnepageWeekly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                        };
                        userReportResults.Add(report);
                    }

                }
            }

            return userReportResults;
        }

        public async Task<List<ReportOnepageMonthly>> ReportOnepageMonthlyAsync(
         ReportFilterDto userReportQuery,
         CancellationToken cancellationToken = default)
        {
            List<ReportOnepageMonthly> userReportResults = new List<ReportOnepageMonthly>();
            DateTime baseDate = userReportQuery.FromDate ?? DateTime.Now;
            var today = baseDate;

            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var thisMonthEnd = thisMonthStart.AddMonths(1);
            if (userReportQuery.ToDate.HasValue)
            {

            }
            var departmentId = userReportQuery.DepartmentId;
            var departments = await _departmentServices.GetDepartmentsAsync();
            IReadOnlyList<UserDepartmentDto> userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            List<Guid> deparmentIds = new List<Guid>();
            var userIds = new List<Guid>();
            if (userReportQuery.UserOfDepartmentId == null)
            {
                var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
                deparmentIds = departmentArgs.Select(e => e.Id).ToList();
                deparmentIds.Add(departmentId.Value);
                userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();
            }
            else
            {

                userIds.Add(userReportQuery.UserOfDepartmentId.Value);
            }

            List<ReportItemDto> taskItemFlats = new List<ReportItemDto>();
            var fromDate = thisMonthStart;
            var toDate = thisMonthEnd;
            taskItemFlats = await GetReportTaskItemFlats(userReportQuery, fromDate, toDate, userIds, cancellationToken, userDepartments);
            if (userReportQuery.ReportType == "month")
            {
                foreach (var task in taskItemFlats)
                {

                    List<string> week = new List<string>();
                    if (task.ShowType == ShowType.TaskItem)
                    {
                        for (DateTime i = fromDate.AddDays(-1); i <= toDate.AddDays(1); i = i.AddDays(1))
                        {
                            string checkInWeek = "";
                            if (task.FromDate.HasValue && task.ToDate.HasValue && task.FromDate.Value.Date <= i && task.ToDate.Value.Date >= i)
                            {
                                if (task.IsLate)
                                {
                                    checkInWeek = "#e7505a";
                                }
                                else
                                {
                                    checkInWeek = "#49a942";
                                }
                            }
                            week.Add(checkInWeek);
                        }
                        ReportOnepageMonthly report = new ReportOnepageMonthly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            AssignName = task.AssignName,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                            DayBefore = week[0],
                            D1 = week[1],
                            D2 = week[2],
                            D3 = week[3],
                            D4 = week[4],
                            D5 = week[5],
                            D6 = week[6],
                            D7 = week[7],
                            D8 = week[8],
                            D9 = week[9],
                            D10 = week[10],
                            D11 = week[11],
                            D12 = week[12],
                            D13 = week[13],
                            D14 = week[14],
                            D15 = week[15],
                            D16 = week[16],
                            D17 = week[17],
                            D18 = week[18],
                            D19 = week[19],
                            D20 = week[20],
                            D21 = week[21],
                            D22 = week[22],
                            D23 = week[23],
                            D24 = week[24],
                            D25 = week[25],
                            D26 = week[26],
                            D27 = week[27],
                            D28 = week[28],
                            D29 = week[29],
                            D30 = week[30],
                            D31 = week[31],
                            DayAfter = week[32],
                        };
                        userReportResults.Add(report);
                    }
                    else
                    {
                        ReportOnepageMonthly report = new ReportOnepageMonthly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            AssignName = task.AssignName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                        };
                        userReportResults.Add(report);
                    }

                }
            }

            return userReportResults;
        }

        private async Task<List<ReportItemDto>> GetReportTaskItemFlats(ReportFilterDto userReportQuery, DateTime fromDate, DateTime toDate, List<Guid> userIds, CancellationToken cancellationToken, IReadOnlyList<UserDepartmentDto> userDepartments)
        {
            List<ReportItemDto> taskItemFlats = new List<ReportItemDto>();
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemPredicateBuilder = PredicateBuilder.New<TaskItem>(x => true);
                var taskItemAssignPredicateBuilder = PredicateBuilder.New<TaskItemAssign>(x => true);


                var isReport = userReportQuery.IsReport;
                var isAssignBy = userReportQuery.IsAssignBy;
                var isAssignTo = userReportQuery.IsAssignTo;
                var trackingProgress = userReportQuery.TrackingProgress;
                var trackingStatus = userReportQuery.TrackingStatus;
                var trackingCrucial = userReportQuery.TrackingCrucial;
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.Project.IsActive == true && !x.IsDeleted && x.IsGroupLabel == false);
                if (userReportQuery.ProjectId.HasValue && userReportQuery.ProjectId != Guid.Empty)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.ProjectId == userReportQuery.ProjectId);
                }
                taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value) || (x.TaskItemAssigns.Any(s => s.AssignTo.HasValue && userIds.Contains(s.AssignTo.Value))));

                taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                    x => DbFunctions.TruncateTime(x.FromDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) <= DbFunctions.TruncateTime(toDate) ||
                         DbFunctions.TruncateTime(x.FromDate) <= DbFunctions.TruncateTime(fromDate) &&
                         DbFunctions.TruncateTime(x.ToDate) >= DbFunctions.TruncateTime(toDate));

                if (isReport == true)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.IsReport == true);
                }

                if (isAssignBy == true)
                {
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(x => x.AssignBy.HasValue && userIds.Contains(x.AssignBy.Value));
                }

                if (isAssignTo == true)
                {
                    taskItemAssignPredicateBuilder = taskItemAssignPredicateBuilder.And(x => x.AssignTo.HasValue && userIds.Contains(x.AssignTo.Value));
                }

                if (trackingProgress != null &&
                    trackingProgress != -1)
                {
                    if (trackingProgress == 0)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate <= x.ToDate : x.ToDate > DateTime.Now);

                    if (trackingProgress == 1)
                        taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                            x => x.FinishedDate != null ? x.FinishedDate > x.ToDate : x.ToDate <= DateTime.Now);
                }

                if (trackingStatus != null &&
                    trackingStatus != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemStatusId == (TaskItemStatusId)trackingStatus);

                if (trackingCrucial != null &&
                    trackingCrucial != -1)
                    taskItemPredicateBuilder = taskItemPredicateBuilder.And(
                        x => x.TaskItemPriorityId == (TaskItemPriorityId)trackingCrucial);
                //var projectKinds = dbContextReadOnlyScope.DbContexts
                //    .Get<TaskContext>()
                //    .Set<ProjectKind>()
                var taskItems = (await dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .Include(x => x.Project)
                    .Include(x => x.TaskItemPriority)
                    .Include(x => x.TaskItemStatus)
                    .Include(x => x.TaskItemAssigns)
                    .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemProcessHistories))
                    .Include(x => x.TaskItemAssigns.Select(t => t.TaskItemStatus))
                    .Where(taskItemPredicateBuilder)
                    .OrderBy(x => x.Order).ThenBy(x => x.CreatedDate)
                    .ThenBy(x => x.TaskName)
                    .ToListAsync(cancellationToken))
                    .Where(x => x.TaskItemAssigns.Any())
                    .Select(x => new TaskItemDto
                    {
                        Id = x.Id,
                        IsDeleted = x.IsDeleted,
                        ProjectId = x.ProjectId,
                        ParentId = x.ParentId,
                        ProjectPercentFinish = x.Project.PercentFinish,
                        TaskName = x.TaskName,
                        TaskItemStatus = new TaskItemStatusDto()
                        {
                            Name = x.TaskItemStatus.Name
                        },
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        FinishedDate = x.FinishedDate,
                        AssignBy = x.AssignBy,
                        IsGroupLabel = x.IsGroupLabel,
                        DepartmentId = x.DepartmentId,
                        NatureTaskId = x.NatureTaskId,
                        PercentFinish = x.PercentFinish,
                        IsLate = (x.TaskItemStatusId == TaskItemStatusId.Finished && x.FinishedDate <= x.ToDate)
                            || (x.TaskItemStatusId != TaskItemStatusId.Finished && x.ToDate >= DateTime.Now) ? false : true,
                        TaskItemPriority = new TaskItemPriorityDto
                        {
                            Name = x.TaskItemPriority.Name
                        },
                        TaskItemAssigns = x.TaskItemAssigns.Where(taskItemAssignPredicateBuilder)
                            .OrderBy(y => y.TaskType)
                            .Select(y => new TaskItemAssignDto
                            {
                                Id = y.Id,
                                AssignTo = y.AssignTo,
                                DepartmentId = y.DepartmentId,

                                StatusName = y.TaskItemStatusId.HasValue ? y.TaskItemStatus.Name : string.Empty,
                                FinishedDate = y.FinishedDate,
                                FromDate = y.FromDate,
                                ToDate = y.ToDate,
                                TaskType = y.TaskType,
                                AppraiseResult = y.AppraiseResult,
                                PercentFinish = y.PercentFinish,
                                AssignToFullName = userDepartments.Where(e => e.UserID == y.AssignTo).FirstOrDefault() != null ? userDepartments.Where(e => e.UserID == y.AssignTo).FirstOrDefault().FullName : string.Empty
                            }).ToList()
                    })
                    .ToList();

                var taskItemDtos = new List<ReportItemDto>();

                for (var i = 0; i < taskItems.Count; i++)
                {
                    var x = taskItems.ElementAt(i);

                    var taskItemDto = new ReportItemDto
                    {
                        ShowType = x.IsGroupLabel == true
                            ? ShowType.Group
                            : ShowType.TaskItem,
                        Id = x.Id.ToString(),
                        ProjectId = x.ProjectId.ToString(),
                        DepartmentId = x.DepartmentId.GetValueOrDefault().ToString(),
                        ParentId = x.ParentId.HasValue && x.ParentId.Value != Guid.Empty ? x.ParentId.ToString() : x.ProjectId.ToString(),
                        Name = x.TaskName,
                        StatusName = x.TaskItemStatus.Name,
                        FinishedDate = x.FinishedDate,
                        IsLate = x.IsLate,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        PercentFinish = x.PercentFinish
                    };
                    if (!taskItemDto.PercentFinish.HasValue || taskItemDto.PercentFinish == 0)
                    {
                        var assignPrimer = x.TaskItemAssigns.Where(e => e.TaskType == TaskType.Primary).FirstOrDefault();
                        if (assignPrimer != null)
                        {
                            taskItemDto.PercentFinish = assignPrimer.PercentFinish;
                        }
                    }
                    string assignName = string.Join(", ", x.TaskItemAssigns.Select(e => e.AssignToFullName).ToList());
                    taskItemDto.AssignName = assignName;
                    taskItemDtos.Add(taskItemDto);
                }

                taskItemDtos.AddRange(await GetReportTaskGroup(taskItemDtos));
                taskItemDtos.InsertRange(0, await ToUserProjectTaskItemWeeklys(taskItemDtos));
                var childsHash = taskItemDtos.ToLookup(cat => cat.ParentId);

                foreach (var cat in taskItemDtos)
                {
                    cat.children = childsHash[cat.Id].ToList();
                    cat.ChildrenCount = cat.children.Count;
                }
                taskItemDtos = taskItemDtos.Where(e => e.ParentId == null || e.ParentId == Guid.Empty.ToString()).ToList();

                taskItemFlats = UnTree(taskItemDtos, 1);
                if (userReportQuery.Level.HasValue)
                {
                    taskItemFlats = taskItemFlats.Where(e => e.Level == userReportQuery.Level).ToList();
                }

            }
            return taskItemFlats;
        }

        private List<ReportItemDto> UnTree(IList<ReportItemDto> reportItemDto, int level, string index = "")
        {
            List<ReportItemDto> taskItemFlats = new List<ReportItemDto>();
            int i = 0;
            foreach (var item in reportItemDto)
            {
                if (item.ShowType != ShowType.TaskItem && !item.children.Where(e => e.ShowType == ShowType.TaskItem).Any())
                {
                    int child = CountChild(item.children.Where(e => e.ShowType != ShowType.TaskItem).ToList());
                    if (child <= 0)
                    {
                        continue;
                    }
                }
                item.NumberOf = string.IsNullOrEmpty(index) ? $"{++i}" : $"{index}.{++i}";
                taskItemFlats.Add(new ReportItemDto()
                {
                    ShowType = item.ShowType,
                    Id = item.Id,
                    ProjectId = item.ProjectId,
                    DepartmentId = item.DepartmentId,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    StatusName = item.StatusName,
                    FinishedDate = item.FinishedDate,
                    PercentFinish = item.PercentFinish,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                    Level = level,
                    NumberOf = item.NumberOf,
                    AssignName = item.AssignName,
                    IsLate = item.IsLate
                });
                if (item.children.Any())
                {
                    taskItemFlats.AddRange(UnTree(item.children, level + 1, item.NumberOf));
                }
            }
            return taskItemFlats;
        }

        private int CountChild(IList<ReportItemDto> collection)
        {
            int count = 0;
            foreach (var item in collection)
            {
                count += item.children.Where(e => e.ShowType == ShowType.TaskItem).Count();
                if (count == 0)
                {
                    count += CountChild(item.children);
                }
            }
            return count;
        }


        private async Task<IReadOnlyList<ReportItemDto>> GetReportTaskGroup(List<ReportItemDto> taskItemDtos)
        {
            var projectIds = taskItemDtos.Select(x => Guid.Parse(x.ProjectId)).ToList();
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var taskItemOfProject = dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<TaskItem>()
                    .AsNoTracking()
                    .Where(x => projectIds.Contains(x.ProjectId.Value) && x.IsGroupLabel == true && x.IsDeleted == false)
                    .OrderBy(x => x.Order).ThenBy(x => x.CreatedDate)
                    .ThenBy(x => x.TaskName)
                    .ToList()
                    .Select(x =>
                    {
                        var taskItemDto = new ReportItemDto
                        {
                            ShowType = ShowType.Group,
                            Id = x.Id.ToString(),
                            IsDeleted = x.IsDeleted,
                            ParentId = x.ParentId.HasValue && x.ParentId.Value != Guid.Empty ? x.ParentId.ToString() : x.ProjectId.ToString(),
                            ProjectId = x.ProjectId.ToString(),
                            Name = x.TaskName, // Nội dung
                            FromDate = null, // Từ ngày
                            ToDate = null, // đến ngày
                            StatusName = "", // trạng thái
                            PercentFinish = x.PercentFinish ?? 0, // tỉ lệ hoàn thành
                            ContentAppraise = "" //Nhận xét
                        };
                        List<string> notes = new List<string>();
                        string tile = string.Format(@"Tỉ lệ hoàn thành: {0}", taskItemDto.PercentFinish);
                        string status = string.Format(@"Tình trạng: {0}", taskItemDto.StatusName);
                        string yKien = string.Format(@"Nội dụng: {0}", taskItemDto.ContentAppraise);
                        notes.Add(tile);
                        notes.Add(status);
                        notes.Add(yKien);
                        taskItemDto.Note = string.Join("<br />", notes);
                        taskItemDto.NoteExcel = string.Join("\r\n", notes);
                        return taskItemDto;
                    })
                    .ToList();


                return taskItemOfProject;
            }
        }

        private async Task<IReadOnlyList<ReportItemDto>> ToUserProjectTaskItemWeeklys(List<ReportItemDto> taskItemDtos)
        {
            var projectIds = taskItemDtos.Select(x => Guid.Parse(x.ProjectId)).ToList();
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                var taskItemOfProject = dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<Project>()
                    .AsNoTracking()
                    .Include(x => x.ProjectStatus)
                    .Include(x => x.ProjectPriority)
                    .Where(x => projectIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreatedDate).ThenBy(x => x.Summary)
                    .ThenBy(x => x.Summary)
                    .ToList()
                    .Select(x =>
                    {
                        var taskItemDto = new ReportItemDto
                        {
                            ShowType = ShowType.Project,
                            Id = x.Id.ToString(),
                            ParentId = Guid.Empty.ToString(),
                            ProjectId = x.Id.ToString(),
                            IsLate = (x.ProjectStatusId == ProjectStatusId.Finished && x.FinishedDate <= x.ToDate)
                            || (x.ProjectStatusId != ProjectStatusId.Finished && x.ToDate >= DateTime.Now) ? false : true,
                            Name = x.Summary, // Nội dung
                            FinishedDate = x.FinishedDate, // ngày kết thúc
                            FromDate = x.FromDate, // Từ ngày
                            ToDate = x.ToDate, // đến ngày
                            StatusName = x.ProjectStatus.Name, // trạng thái
                            PercentFinish = x.PercentFinish ?? 0, // tỉ lệ hoàn thành
                            ContentAppraise = x.AppraiseResult //Nhận xét
                        };
                        List<string> notes = new List<string>();
                        string tile = string.Format(@"Tỉ lệ hoàn thành: {0}", taskItemDto.PercentFinish);
                        string status = string.Format(@"Tình trạng: {0}", taskItemDto.StatusName);
                        string yKien = string.Format(@"Nội dụng: {0}", taskItemDto.ContentAppraise);
                        notes.Add(tile);
                        notes.Add(status);
                        notes.Add(yKien);
                        taskItemDto.Note = string.Join("<br />", notes);
                        taskItemDto.NoteExcel = string.Join("\r\n", notes);
                        return taskItemDto;
                    })
                    .ToList();


                return taskItemOfProject;
            }
        }

        private IEnumerable<ReportItemDto>
            FlattenTaskItem(IReadOnlyList<ReportItemDto> taskItems)
        {
            for (var i = 0; i < taskItems.Count; i++)
            {
                var taskItem = taskItems.ElementAt(i);

                var stacks = new Stack<ReportItemDto>();
                stacks.Push(taskItem);

                while (stacks.Count > 0)
                {
                    var node = stacks.Pop();
                    yield return node;

                    for (var j = node.children.Count - 1; j >= 0; j--)
                    {
                        stacks.Push(node.children[j]);
                        node.children.RemoveAt(j);
                    }
                }
            }
        }

        private static IReadOnlyList<ReportItemDto>
            HierarchicalTaskItem(IEnumerable<ReportItemDto> taskItems)
        {
            var lookup = new Dictionary<string, ReportItemDto>();
            var list = new List<ReportItemDto>();
            var index = 0;

            foreach (var item in taskItems)
            {
                if (lookup.ContainsKey(item.ParentId) ||
                    item.ParentId != Guid.Empty.ToString())
                {
                    if (!lookup.TryGetValue(item.ParentId, out var parent))
                        if (lookup.TryGetValue(item.ProjectId, out parent))
                            item.ParentId = parent.Id;

                    if (item.ShowType != ShowType.TaskItemAssign)
                        item.NumberOf = $"{parent.NumberOf}.{++parent.ChildrenCount}";
                    parent.children.Add(item);
                }
                else
                {
                    item.NumberOf = (++index).ToString();
                    list.Add(item);
                }

                if (!lookup.ContainsKey(item.Id))
                    lookup.Add(item.Id, item);
            }

            return list;
        }

        private async Task<IReadOnlyList<ReportItemDto>> ToUserProjectTaskItems(IEnumerable<Guid> projectIds)
        {
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();

                return dbContextReadOnlyScope.DbContexts
                    .Get<TaskContext>()
                    .Set<Project>()
                    .AsNoTracking()
                    .Include(x => x.ProjectStatus)
                    .Include(x => x.ProjectPriority)
                    .Where(x => projectIds.Contains(x.Id))
                    .OrderBy(x => x.CreatedDate)
                    .ThenBy(x => x.Summary)
                    .ToList()
                    .Select(x =>
                    {
                        var taskItemDto = new ReportItemDto
                        {
                            ShowType = ShowType.Project,
                            Id = x.Id.ToString(),
                            ParentId = Guid.Empty.ToString(),
                            Name = x.Summary, // Nội dung
                            FinishedDate = x.FinishedDate,
                            FromDate = x.FromDate, // Từ ngày
                            ToDate = x.ToDate, // đến ngày
                            TaskItemCategory = x.ProjectKindId.HasValue && x.ProjectKindId == (int)ProjectKindId.Project ? "Dự án" : "Không phải dự án",
                            StatusName = x.ProjectStatus.Name, // trạng thái
                            ProjectStatusId = (int)x.ProjectStatus.Id, // trạng thái
                            ProjectPercentFinish = x.PercentFinish == null ? 0 : x.PercentFinish, // tỉ lệ hoàn thành
                            BlueprintTime = ConvertToStringExtensions.DateToStringLocalNewLine(x.FromDate, x.ToDate, "f"),
                            ActualTime = ConvertToStringExtensions.DateToStringLocalNewLine(x.FromDate, x.FinishedDate, "f"),
                            ContentAppraise = x.ProjectHistories != null ? x.ProjectHistories.Where(p => p.ActionId == ActionId.Finish).Select(e => e.Summary).FirstOrDefault() : string.Empty
                        };

                        var userDepartment =
                            userDepartments.FirstOrDefault(
                                y => y.UserID == x.ApprovedBy &&
                                        y.DeptID == x.DepartmentId) ??
                            userDepartments.FirstOrDefault(
                                y => y.UserID == x.ApprovedBy);

                        if (userDepartment == null)
                            userDepartment =
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == x.CreatedBy &&
                                            y.DeptID == x.DepartmentId) ??
                                userDepartments.FirstOrDefault(
                                    y => y.UserID == x.ApprovedBy);

                        if (userDepartment != null)
                            taskItemDto.AssignBy = $"{userDepartment.FullName} ({userDepartment.JobTitleName})";

                        if (x.FinishedDate.HasValue)
                            if (x.FinishedDate <= x.ToDate)
                                taskItemDto.Progress = "Trong hạn";
                            else
                                taskItemDto.Progress = "Quá hạn";
                        else if (x.ToDate > DateTime.Now)
                            taskItemDto.Progress = "Trong hạn";
                        else
                            taskItemDto.Progress = "Quá hạn";

                        return taskItemDto;
                    })
                    .ToList();
            }
        }

        public async Task<List<ReportOnepageQuaterly>> ReportOnepageQuaterlyAsync(
           ReportFilterDto userReportQuery,
           CancellationToken cancellationToken = default)
        {
            List<ReportOnepageQuaterly> userReportResults = new List<ReportOnepageQuaterly>();
            DateTime baseDate = userReportQuery.FromDate ?? DateTime.Now;
            var today = baseDate;
            int quarterNumber = (today.Month - 1) / 3 + 1;
            DateTime firstDayOfQuarter = new DateTime(today.Year, (quarterNumber - 1) * 3 + 1, 1);
            DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);
            if (userReportQuery.ToDate.HasValue)
            {

            }
            var departmentId = userReportQuery.DepartmentId;
            var departments = await _departmentServices.GetDepartmentsAsync();
            IReadOnlyList<UserDepartmentDto> userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            List<Guid> deparmentIds = new List<Guid>();

            var userIds = new List<Guid>();
            if (userReportQuery.UserOfDepartmentId == null)
            {
                var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
                deparmentIds = departmentArgs.Select(e => e.Id).ToList();
                deparmentIds.Add(departmentId.Value);
                userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();
            }
            else
            {

                userIds.Add(userReportQuery.UserOfDepartmentId.Value);
            }

            List<ReportItemDto> taskItemFlats = new List<ReportItemDto>();
            var fromDate = firstDayOfQuarter;
            var toDate = lastDayOfQuarter;
            taskItemFlats = await GetReportTaskItemFlats(userReportQuery, fromDate, toDate, userIds, cancellationToken, userDepartments);
            if (userReportQuery.ReportType == "quarter")
            {
                foreach (var task in taskItemFlats)
                {

                    List<string> week = new List<string>();
                    if (task.ShowType == ShowType.TaskItem)
                    {
                        var before = fromDate.Date.AddDays(-1);
                        string checkBefore = "";
                        string color = task.IsLate ? "#e7505a" : "#49a942";
                        if (task.ToDate >= before && task.FromDate < before)
                        {
                            checkBefore = color;
                        }
                        week.Add(checkBefore);
                        for (DateTime i = fromDate.Date; i < toDate.Date; i = i.AddMonths(1))
                        {
                            DateTime endI = i.AddMonths(1).AddDays(-1);
                            string checkInWeek = "";
                            if (!(task.ToDate < i || task.FromDate > endI))
                            {
                                checkInWeek = color;
                            }
                            week.Add(checkInWeek);
                        }
                        var after = toDate.Date.AddDays(-1);
                        string checkAfter = "";
                        if (task.ToDate >= after && task.FromDate < after)
                        {
                            checkAfter = color;
                        }
                        week.Add(checkAfter);
                        ReportOnepageQuaterly report = new ReportOnepageQuaterly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            AssignName = task.AssignName,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                            DayBefore = week[0],
                            T1 = week[1],
                            T2 = week[2],
                            T3 = week[3],
                            DayAfter = week[4],
                        };
                        userReportResults.Add(report);
                    }
                    else
                    {
                        ReportOnepageQuaterly report = new ReportOnepageQuaterly
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                        };
                        userReportResults.Add(report);
                    }

                }
            }

            return userReportResults;
        }

        public async Task<List<ReportOnepageAnnual>> ReportOnepageAnnualAsync(
          ReportFilterDto userReportQuery,
          CancellationToken cancellationToken = default)
        {
            List<ReportOnepageAnnual> userReportResults = new List<ReportOnepageAnnual>();

            DateTime baseDate = userReportQuery.FromDate ?? DateTime.Now;
            var today = baseDate;
            DateTime firstDayOfYear = new DateTime(today.Year, 1, 1);
            DateTime lastDayOfYear = new DateTime(today.Year, 12, 31);

            if (userReportQuery.ToDate.HasValue)
            {

            }
            var departmentId = userReportQuery.DepartmentId;
            var departments = await _departmentServices.GetDepartmentsAsync();
            IReadOnlyList<UserDepartmentDto> userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            List<Guid> deparmentIds = new List<Guid>();
            var userIds = new List<Guid>();
            if (userReportQuery.UserOfDepartmentId == null)
            {
                var departmentArgs = _departmentServices.GetChildren(departments, departmentId).ToList();
                deparmentIds = departmentArgs.Select(e => e.Id).ToList();
                deparmentIds.Add(departmentId.Value);
                userIds = userDepartments.Where(e => deparmentIds.Contains(e.DeptID)).Select(e => e.UserID).ToList();
            }
            else
            {

                userIds.Add(userReportQuery.UserOfDepartmentId.Value);
            }
            List<ReportItemDto> taskItemFlats = new List<ReportItemDto>();
            var fromDate = firstDayOfYear;
            var toDate = lastDayOfYear;
            taskItemFlats = await GetReportTaskItemFlats(userReportQuery, fromDate, toDate, userIds, cancellationToken, userDepartments);
            if (userReportQuery.ReportType == "year")
            {
                foreach (var task in taskItemFlats)
                {

                    List<string> week = new List<string>();
                    if (task.ShowType == ShowType.TaskItem)
                    {
                        var before = fromDate.Date.AddDays(-1);
                        string checkBefore = "";
                        string color = task.IsLate ? "#e7505a" : "#49a942";
                        if (task.ToDate >= before && task.FromDate < before)
                        {
                            checkBefore = color;
                        }
                        week.Add(checkBefore);
                        for (DateTime i = fromDate.Date; i < toDate.Date; i = i.AddMonths(1))
                        {
                            DateTime endI = i.AddMonths(1).AddDays(-1);
                            string checkInWeek = "";
                            if (!(task.ToDate < i || task.FromDate > endI))
                            {
                                checkInWeek = color;
                            }
                            week.Add(checkInWeek);
                        }
                        var after = toDate.Date.AddDays(-1);
                        string checkAfter = "";
                        if (task.ToDate >= after && task.FromDate < after)
                        {
                            checkAfter = color;
                        }
                        week.Add(checkAfter);
                        ReportOnepageAnnual report = new ReportOnepageAnnual
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            AssignName = task.AssignName,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                            DayBefore = week[0],
                            Th1 = week[1],
                            Th2 = week[2],
                            Th3 = week[3],
                            Th4 = week[4],
                            Th5 = week[5],
                            Th6 = week[6],
                            Th7 = week[7],
                            Th8 = week[8],
                            Th9 = week[9],
                            Th10 = week[10],
                            Th11 = week[11],
                            Th12 = week[12],
                            DayAfter = week[13],
                        };
                        userReportResults.Add(report);
                    }
                    else
                    {
                        ReportOnepageAnnual report = new ReportOnepageAnnual
                        {
                            Id = task.Id,
                            Name = task.Name,
                            NumberOf = task.NumberOf,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            ShowType = task.ShowType,
                            StatusId = task.StatusId,
                            StatusName = task.StatusName,
                            FinishDateText = task.FinishedDateText,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            PercentFinish = task.PercentFinish.HasValue ? task.PercentFinish + "%" : string.Empty,
                        };
                        userReportResults.Add(report);
                    }

                }
            }
            return userReportResults;
        }

        public ExcelWorksheet FillData<T>(List<T> entities, ExcelWorksheet excelWorksheet) where T : ReportOnepageBase
        {
            using (ExcelRange cells = excelWorksheet.Cells)
            {
                IList<FieldInfo> fieldInfos = GetParamenterTemplate(excelWorksheet).Fields.Where(f => f.Type == EplusExtension.KeyType_Field).ToArray();
                if (fieldInfos.Count > 0)
                {
                    int startRowIndex = fieldInfos.Min(e => e.ExcelRow);
                    int endRowIndex = entities.Count - 1;

                    excelWorksheet.InsertRow(startRowIndex + 1, endRowIndex, startRowIndex);
                    int rowIndex = startRowIndex;
                    foreach (T data in entities)
                    {
                        foreach (FieldInfo fieldInfo in fieldInfos)
                        {
                            var value = ReflectorUtility.FollowPropertyPath(data, fieldInfo.Name);
                            if (value != null && (value.ToString() == "#49a942" || value.ToString() == "#e7505a"))
                            {
                                cells[rowIndex, fieldInfo.ExcelColumn].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cells[rowIndex, fieldInfo.ExcelColumn].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(value.ToString()));
                            }
                            else
                            {
                                if (data.ShowType == ShowType.Group || data.ShowType == ShowType.Project)
                                {
                                    cells[rowIndex, fieldInfo.ExcelColumn].Value = value;
                                    cells[rowIndex, fieldInfo.ExcelColumn].Style.Font.Bold = true;
                                    //if (data.IsLate)
                                    //{
                                    //    cells[rowIndex, fieldInfo.ExcelColumn].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#e7505a"));

                                    //}
                                }
                                else
                                {
                                    cells[rowIndex, fieldInfo.ExcelColumn].Value = value;
                                }
                            }
                        }
                        rowIndex++;
                    }
                }
            }

            return excelWorksheet;
        }

        public ConfigInfo GetParamenterTemplate(ExcelWorksheet excelWorksheet)
        {
            ConfigInfo configInfo = new ConfigInfo();

            ExcelAddressBase dimension = excelWorksheet.Dimension;
            ExcelRange cells = excelWorksheet.Cells;
            for (int rowIndex = 1; rowIndex <= dimension.Rows; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex <= dimension.Columns; columnIndex++)
                {
                    ExcelRange cell = cells[rowIndex, columnIndex];
                    string text = cell.Text;

                    FieldInfo fieldInfo = ParseConfig(text);
                    if (fieldInfo != null)
                    {
                        fieldInfo.ExcelAddress = cell.Address;
                        fieldInfo.ExcelRow = rowIndex;
                        fieldInfo.ExcelColumn = columnIndex;
                        configInfo.Fields.Add(fieldInfo);
                    }
                }
            }
            return configInfo;
        }

        protected FieldInfo ParseConfig(string text)
        {
            FieldInfo fieldInfo = null;

            if (text.Contains(EplusExtension.Key_Start) && text.Contains(EplusExtension.Key_End))
            {
                string m_TextNoKey = text.Replace(EplusExtension.Key_Start, string.Empty).Replace(EplusExtension.Key_End, string.Empty);
                string[] m_TextNoKeyParts = m_TextNoKey.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (m_TextNoKeyParts.Length == 2)
                {
                    fieldInfo = new FieldInfo()
                    {
                        Type = m_TextNoKeyParts[0],
                        Name = m_TextNoKeyParts[1]
                    };
                }
            }

            return fieldInfo;
        }
    }
}
