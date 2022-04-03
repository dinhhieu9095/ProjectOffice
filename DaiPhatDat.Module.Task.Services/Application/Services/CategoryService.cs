using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Linq;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Module.Task.Entities;

namespace DaiPhatDat.Module.Task.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly ITaskItemCategoryRepository _taskItemCategoryRepository;
        private readonly ITaskItemStatusRepository _taskItemStatusRepository;
        private readonly ITaskItemPriorityRepository _taskItemPriorityRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly INatureTaskRepository _natureTaskRepository;
        private readonly IProjectPriorityRepository _projectPriorityRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly ISettingsService _settingsService;

        public CategoryService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, ITaskItemCategoryRepository taskItemCategoryRepository, IUserServices userServices, IProjectCategoryRepository projectCategoryRepository, ITaskItemStatusRepository taskItemStatusRepository, ITaskItemPriorityRepository taskItemPriorityRepository, INatureTaskRepository natureTaskRepository, IProjectPriorityRepository projectPriorityRepository, IProjectTypeRepository projectTypeRepository, ISettingsService settingsService, ITaskItemRepository taskItemRepository, IProjectStatusRepository projectStatusRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _projectCategoryRepository = projectCategoryRepository;
            _taskItemCategoryRepository = taskItemCategoryRepository;
            _taskItemStatusRepository = taskItemStatusRepository;
            _taskItemPriorityRepository = taskItemPriorityRepository;
            _natureTaskRepository = natureTaskRepository;
            _projectPriorityRepository = projectPriorityRepository;
            _projectTypeRepository = projectTypeRepository;
            _mapper = mapper;
            _userServices = userServices;
            _settingsService = settingsService;
            _taskItemRepository = taskItemRepository;
            _projectStatusRepository = projectStatusRepository;
        }


        #region ProjectCategory
        public async Task<List<ProjectCategoryDto>> GetProjectCategories(Guid userId)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectCategories = await _projectCategoryRepository.GetAsync(e => e.UserId == userId && e.IsActive == true);

                return _mapper.Map<List<ProjectCategoryDto>>(projectCategories);
            }
        }
        public async Task<List<string>> GetProjectCategoriesByProjectId(Guid projectId, Guid? taskId = null)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                IReadOnlyList<ProjectCategory> projectCategories = await _projectCategoryRepository.GetAsync(e => e.ProjectId == projectId && e.IsActive == true);
                List<string> categories = projectCategories.Select(group => group.Name).ToList();
                if (taskId.HasValue)
                {
                    var taskItem = _taskItemRepository.GetAll().Where(e => e.Id == taskId && e.IsDeleted == false).FirstOrDefault();
                    string taskItemCategory = string.Empty;
                    if (taskItem != null)
                    {
                        taskItemCategory = taskItem.TaskItemCategory;
                    }
                    if (!string.IsNullOrEmpty(taskItemCategory))
                    {
                        categories.AddRange(taskItemCategory.Split(';').ToList());
                    }
                }
                categories = categories.Distinct().ToList();
                return categories;
            }
        }

        public async Task<List<ProjectCategoryDto>> GetAllOfProjectCategories()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectCategories = await _projectCategoryRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<ProjectCategoryDto>>(projectCategories);
            }
        }
        #endregion
        #region ProjectPriority
        public async Task<List<ProjectPriorityDto>> GetAllOfProjectPriorities()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectPriorities = await _projectPriorityRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectPriorityDto>>(projectPriorities);
            }
        }
        #endregion
        #region ProjectStatus

        public async Task<List<ProjectStatusDto>> GetAllProjectStatuses()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemStatuses = await _projectStatusRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectStatusDto>>(taskItemStatuses);
            }
        }

        #endregion
        #region ProjectType
        public async Task<List<ProjectTypeDto>> GetAllOfProjectTypes()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectTypes = await _projectTypeRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectTypeDto>>(projectTypes);
            }
        }
        #endregion
        #region TaskItemCategory
        public async Task<List<TaskItemCategoryDto>> GetTaskItemCategories(Guid userId)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemCategories = await _taskItemCategoryRepository.GetAsync(e => e.UserId == userId);

                return _mapper.Map<List<TaskItemCategoryDto>>(taskItemCategories);
            }
        }

        public async Task<List<TaskItemCategoryDto>> GetAllTaskItemCategories()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemCategories = await _taskItemCategoryRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<TaskItemCategoryDto>>(taskItemCategories);
            }
        }
        #endregion

        #region TaskItemStatus

        public async Task<List<TaskItemStatusDto>> GetAllTaskItemStatuses()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemStatuses = await _taskItemStatusRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<TaskItemStatusDto>>(taskItemStatuses);
            }
        }

        #endregion

        #region TaskItemPriority

        public async Task<List<TaskItemPriorityDto>> GetAllTaskItemPriorities()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemPriorities = await _taskItemPriorityRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<TaskItemPriorityDto>>(taskItemPriorities);
            }
        }
        #endregion


        #region NatureTask

        public async Task<List<NatureTaskDto>> GetAllNatureTasks()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var natureTaskDtos = await _natureTaskRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<NatureTaskDto>>(natureTaskDtos);
            }
        }
        #endregion
        public async Task<bool> PostTrackingUpdateDB()
        {
            try
            {
                using (var dbCtxScope = _dbContextScopeFactory.Create())
                {
                    SettingsDto versionTask = _settingsService
                           .GetByKey("CurrentTaskVersion");
                    string currentVersion = versionTask.Value;
                    int version = Int32.Parse(currentVersion);
                    List<string> queries = GetQueryUpdate(version);
                    foreach (string item in queries)
                    {
                        try
                        {
                            string pattern = @"\bGO\b";
                            string query = Regex.Replace(item, pattern, "");
                            await _projectCategoryRepository.SqlQueryAsync(typeof(bool), query);
                        }
                        catch (Exception ex)
                        {
                            _loggerServices.WriteError("PostTrackingUpdateDB at: " + item + " message: " + ex.ToString());
                            continue;
                        }

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return false;
            }
        }
        public List<string> GetQueryUpdate(int currentVersion)
        {
            List<string> queries = new List<string>();
            int newVersion = currentVersion;
            DateTime now = DateTime.Now;
            string cVersion = now.ToString("yyyyMMdd");
            int version = Int32.Parse(cVersion);
            
            if (newVersion < 20210503)
            {
                newVersion = 20210503;
                queries.Add(@"
 insert into  [dbo].[Roles] values ('3A9EC51A-6EBC-4964-9297-4C97B827DB42',	N'Quản trị hệ thống',	N'Quản trị hệ thống',	'AABA4C69-D2FA-4BAF-97B5-BE6C17742A3B')");
                queries.Add(@"
  insert into [dbo].[Permissions] values ('F90744BA-9090-400B-89FD-A4D12FF7697A',	'AABA4C69-D2FA-4BAF-97B5-BE6C17742A3B',	'TaskFullControl',	N'Toàn quyền quản lý công việc')");
                queries.Add(@"
  insert into [dbo].[RolePermissions] values ('3A9EC51A-6EBC-4964-9297-4C97B827DB42',	'F90744BA-9090-400B-89FD-A4D12FF7697A',	N'Quản trị hệ thống')");

            }
            if(newVersion < 20210504)
            {
                newVersion = 20210504;
                queries.Add(@"delete from  [Task].[Action];
INSERT INTO [Task].[Action]
           ([Id]
           ,[Name]
           ,[IsActive])
     VALUES
           (0,	N'Tạo mới',	1),
(1,	N'Giao việc',	1),
(2,	N'Chỉnh sửa',	1),
(3,	N'Xử lý',	1),
(4,	N'Kết thúc',	1),
(5,	N'Trả lại',	1),
(6,	N'Báo cáo',	1),
(7,	N'Gia hạn',	1),
(8,	N'Ðánh giá',	1),
(9,	N'Ðã xem',	1),
(10,	N'Import',	1),
(11,	N'Duyệt gia hạn',	1),
(12,	N'Thu hồi',	1)");
                queries.Add(@"delete from  [Task].[NatureTask];
INSERT INTO [Task].[NatureTask]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (0,	N'Thường xuyên',	1),
(1,	N'Kế hoạch',	1),
(2,	N'Đột xuất',	1)");
                queries.Add(@"delete from  [Task].[ProjectKind];
INSERT INTO [Task].[ProjectKind]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Không phải dự án',	1),
(1,	N'Dự án',	1)");
                queries.Add(@"delete from  [Task].[ProjectPriority];
INSERT INTO [Task].[ProjectPriority]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Bình thuờng',	1),
(1,	N'Quan trọng',	1),
(2,	N'Rất quan trọng',	1)");
                queries.Add(@"delete from  [Task].[ProjectSecret];
INSERT INTO [Task].[ProjectSecret]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Bình thuờng',	1),
(1,	N'Mật',	1),
(2,	N'Tuyệt mật',	1),
(4,	N'Tối mật',	1)");
                queries.Add(@"delete from  [Task].[ProjectStatus];
INSERT INTO [Task].[ProjectStatus]
           ([Id]
           ,[Name]
           ,[Code]
           ,[IsActive])
     VALUES
           (
-1,N'Ðịnh kỳ',	'DINHKY',	1),
(0	,N'Mới',	'MOI',	1),
(1	,N'Ðang chờ duyệt',	'CHODUYET',	1),
(3	,N'Ðang xử lý',	'DANGXULY',	1),
(4	,N'Kết thúc',	'KETTHUC',	1),
(5	,N'Ðã duyệt',	'DADUYET',	1),
(12	,N'Ðang soạn',	'DANGSOAN',	1)");
                queries.Add(@"delete from  [Task].[ProjectType];
INSERT INTO [Task].[ProjectType]
           ([Id]
           ,[Name]
           ,[Code]
           ,[OrderNumber]
           ,[IsActive])
     VALUES
           (0,N'Kế hoạch công việc','PlanningApprove',	0,	1),
(1,N'Công việc giao ban','Planning',	1,	1),
(2,N'Công việc định kỳ','Scheduling',	2,	1),
(3,N'Công việc phát sinh','Unplanned',	3,	1)
GO
");
                queries.Add(@"delete from  [Task].[TaskItemPriority];
INSERT INTO [Task].[TaskItemPriority]
           ([Id]
           ,[Name]
           ,[Density]
           ,[IsActive])
     VALUES
           (0,N'Bình thuờng',	1,	1),(
1,N'Quan trọng',	1,	1),(
2,N'Rất quan trọng',	1,	1),(
3,N'Không quan trọng',	1,	1),(
4,N'Thiết yếu',	1,	1)");
                queries.Add(@"delete from  [Task].[TaskItemStatus];
INSERT INTO [Task].[TaskItemStatus]
           ([Id]
           ,[Name]
           ,[Code]
           ,[IsActive])
     VALUES
           (-1,N'Tất cả','ALL',	1),(
0,N'Mới','NEW',	1),(
1,N'Ðang xử lý','INPROCESS',	1),(
2,N'Báo cáo','REPORT',	1),(
3,N'Hủy','CANCEL',	1),(
4,N'Kết thúc','FINISH',	1),(
5,N'Gia hạn','EXTEND',	1),(
6,N'Trả lại','REPORT_RETURN',	1),(
8,N'Ðã xem','READ',	1)");
            }
            if(newVersion < 20210514)
            {
                newVersion = 20210514;
                queries.Add(@"

ALTER PROCEDURE [dbo].[SP_Select_Projects_With_Filter] 
	-- Add the parameters for the stored procedure here
	@CurrentUserId		NVARCHAR(200),					-- Func_GetUserIDByLoginName('benthanh\nguyenminhphuong')
	@CurrentDate		DATETIME			= '',
	@Keyword			NVARCHAR(200)		= '', 
	@ParentId			NVARCHAR(200)		= '',
	@IsSearching		BIT					= 0,
	@AssignBy			NVARCHAR(200)		= '',
	@TaskItemStatusId	VARCHAR(10)			= '',
	@AssignTo			NVARCHAR(200)		= '',
	@TaskType			VARCHAR(10)			= '',
	@FromDate			VARCHAR(50)			= '',
	@ToDate				VARCHAR(50)			= '',
	@TaskFromDateOfFromDate			VARCHAR(50)			= '',
	@TaskFromDateOfToDate				VARCHAR(50)			= '',
	@TaskToDateOfFromDate			VARCHAR(50)			= '',
	@TaskToDateOfToDate				VARCHAR(50)			= '',
	@StatusTime			INT					= 0,
	@TaskItemPriorityId VARCHAR(50)			= null,
	@TaskItemNatureId VARCHAR(50) = null,
	@ProjectHashtag	NVARCHAR(255)		= '',
	@TaskHashtag	NVARCHAR(255)		= '',
	@IsReport			VARCHAR(10)			= '',
	@IsWeirdo			VARCHAR(10)			= '',
	@Page				INT					= 1,
	@PageSize			INT					= 10,
	@OrderBy	  		NVARCHAR(50)			= 'CreatedDate DESC',
	@IsCount			BIT					= 0,
	@IsFullControl		BIT					= 0,
	@StartDate		VARCHAR(50)					= '',
	@EndDate		VARCHAR(50)					= '',
	@IsExtend		VARCHAR(50)					= '',
	@ProjectId nvarchar(50) = '',
	@IsAllLevel		BIT					= 0
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;

    -- Insert statements for procedure here
	
	-- Create temp table
	CREATE TABLE #TempOneTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TempTwoTItemTbl (TaskItemId uniqueidentifier);
	
	CREATE TABLE #TempTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TItemTbl (TaskItemId uniqueidentifier);


	CREATE TABLE #ProTaskTbl (TaskItemAssignId uniqueidentifier, TaskItemId uniqueidentifier, ProjectId uniqueidentifier);
	CREATE TABLE #ProTbl (ProjectId uniqueidentifier);
	
	CREATE TABLE #DocTbl 
	(
		[Id] uniqueidentifier,
		[ProjectId] uniqueidentifier,
		[ParentId] uniqueidentifier,
		[Name] NVARCHAR(MAX),
		[Type] VARCHAR(10),
		[Status] VARCHAR(20),
		[Process] VARCHAR(20),
		[HasChildren] BIT,
		[FromDate] DATETIME,
		[ToDate] DATETIME,
		[ApprovedBy] uniqueidentifier,
		[AssignBy] uniqueidentifier,
		[UserId] uniqueidentifier,
		[DepartmentId] uniqueidentifier,
		[StatusName] NVARCHAR(50)
	);
	
	-- Create variable
	DECLARE 
			@ProTaskSqlTrack nvarchar(MAX),
			@ProSqlTrack nvarchar(MAX),
			@TaskSqlTrack nvarchar(MAX),
			@SqlTrack nvarchar(MAX),
			@SqlFilter nvarchar(200),
			@Sql nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int,
			@FilerTrackType nvarchar(100) = '',
			@IsParentInProject bit = 0;


	-- AFTER CHANGING
	
	-- Lay danh project ban dau
	if @IsFullControl = 0
	-- lấy all vào #ProTaskTbl
	SET @ProTaskSqlTrack =
			'INSERT INTO #ProTaskTbl
			SELECT TIAssign.Id, TItem.Id, Pro.Id AS ProjectId 
			FROM [Task].[TaskItemAssign] TIAssign
			FULL OUTER JOIN [Task].[TaskItem] TItem ON TIAssign.TaskItemId = TItem.Id
 			FULL OUTER JOIN [Task].[Project] Pro ON TItem.ProjectId = Pro.Id
			WHERE
				Pro.IsActive = 1 
				AND (Pro.ApprovedBy = ''' + @CurrentUserId + ''' 
					OR Pro.CreatedBy = ''' + @CurrentUserId + '''
					OR Pro.UserViews like N''%' + @CurrentUserId + '%''
					OR Pro.ManagerId like ''%'+@CurrentUserId+'%''
					OR TItem.AssignBy = ''' + @CurrentUserId + '''
					OR (TIAssign.TaskItemId IS NOT NULL 
						AND TIAssign.TaskItemId != ''00000000-0000-0000-0000-000000000000''
						AND (TIAssign.AssignTo = ''' + @CurrentUserId + '''
								OR TIAssign.AssignFollow = ''' + @CurrentUserId + ''') AND TIAssign.TaskItemStatusId != 3)) ';
	else
		SET @ProTaskSqlTrack =
			'INSERT INTO #ProTaskTbl
			SELECT TIAssign.Id, TItem.Id, Pro.Id AS ProjectId 
			FROM [Task].[TaskItemAssign] TIAssign
			FULL OUTER JOIN [Task].[TaskItem] TItem ON TIAssign.TaskItemId = TItem.Id
 			FULL OUTER JOIN [Task].[Project] Pro ON TItem.ProjectId = Pro.Id
			WHERE
				Pro.IsActive = 1 '
	--print @ProTaskSqlTrack
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (
								--Pro.Summary like N''%' + @Keyword + '%'' OR 
								TItem.TaskName like N''%' + @Keyword + '%'')';
	END
	IF (@ParentId!= '' and EXISTS (SELECT Id FROM [Task].[Project] WHERE Id = @ParentId))
		BEGIN
			SET @ProjectId = @ParentId;
		END
	IF (@ProjectId != '') 
	BEGIN
		
			SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND Pro.Id = ''' + @ProjectId + '''';
	END

	-- AssignBy Condition
	IF (@AssignBy != '') 
	BEGIN
		if @AssignBy = 'CurentUser'
			SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND TItem.AssignBy = ''' + @CurrentUserId + ''' ';
		else
			SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND TItem.AssignBy = ''' + @AssignBy + ''' ';
	END

	-- TaskItemStatusId Condition
	IF (@TaskItemStatusId != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND  TItem.TaskItemStatusId In (' + @TaskItemStatusId + ') AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
	END

	-- AssignTo Condition
	IF (@AssignTo != '')
	BEGIN
		if @AssignTo = 'CurentUser'
			SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND TIAssign.AssignTo = ''' + @CurrentUserId + ''' AND TIAssign.TaskItemStatusId != 3';
		else
			SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND TIAssign.AssignTo = ''' + @AssignTo + ''' AND TIAssign.TaskItemStatusId != 3';
		
	END

	-- TaskType Condition
	IF (@TaskType != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND TIAssign.TaskType In (' + @TaskType + ')';
	END

	-- FromDate Condition
	IF (@FromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.FromDate >= ''' + @FromDate + ''' 
										OR TItem.FromDate >= ''' + @FromDate + '''
										OR Pro.FromDate >= ''' + @FromDate + ''')';
	END

	-- ToDate Condition
	IF (@ToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.ToDate <= ''' + @ToDate + '''
										OR TItem.ToDate <= ''' + @ToDate + '''
										OR Pro.ToDate <= ''' + @ToDate + ''')';
	END

	-- so sánh với fromdate của công việc
	IF (@TaskFromDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.FromDate >= ''' + @TaskFromDateOfFromDate + ''' 
										OR TItem.FromDate >= ''' + @TaskFromDateOfFromDate + '''
										OR Pro.FromDate >= ''' + @TaskFromDateOfFromDate + ''')';
	END

	IF (@TaskFromDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR TItem.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR Pro.FromDate <= ''' + @TaskFromDateOfToDate + ''')';
	END
	-- so sánh với todate của công việc
	IF (@TaskToDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.ToDate >= ''' + @TaskToDateOfFromDate + ''' 
										OR TItem.ToDate >= ''' + @TaskToDateOfFromDate + '''
										OR Pro.ToDate >= ''' + @TaskToDateOfFromDate + ''')';
	END

	IF (@TaskToDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.ToDate <= ''' + @TaskToDateOfToDate + '''
										OR TItem.ToDate <= ''' + @TaskToDateOfToDate + '''
										OR Pro.ToDate <= ''' + @TaskToDateOfToDate + ''')';
	END

	-- mặc định
	--IF (@StatusTime = 0)
	--BEGIN
		
	--END
	-- sắp hết hạn
	IF (@StatusTime = 1)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	TItem.FinishedDate, 
																	TItem.TaskItemStatusId, 
																	dateadd(DAY, 2, getdate())) = 1
									AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	TItem.FinishedDate, 
																	TItem.TaskItemStatusId, 
																	getdate()) = 0 
									 AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
	END
	-- quá hạn
	IF (@StatusTime = 2)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	TItem.FinishedDate,
																	TItem.TaskItemStatusId, 
																	getdate()) = 1 
								 AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 )';
	END

	
	-- gia hạn
	IF (@IsExtend = '1')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TIAssign.IsExtend = 1)'
								 
	END
 
	-- Priority
	IF (@TaskItemPriorityId IS NOT NULL)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.TaskItemPriorityId In (' + @TaskItemPriorityId + '))';
	END

	--natureTask
	IF @TaskItemNatureId IS NOT NULL 
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.NatureTaskId In (' + @TaskItemNatureId + '))';
	END


	-- Hashtag
	-- khánh nói
	IF (@ProjectHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (EXISTS  (SELECT 1
													FROM dbo.Func_SplitTextToTable(N'''+@ProjectHashtag+''','','') where items in (select *  from dbo.Func_SplitTextToTable(Pro.ProjectCategory,'';''))))';
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (EXISTS  (SELECT 1
													FROM dbo.Func_SplitTextToTable(N'''+@TaskHashtag+''','','') where items in (select *  from dbo.Func_SplitTextToTable(TaskItemCategory,'';''))))';
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
	END
	--print @ProTaskSqlTrack
	-- IsReport
	IF (@IsReport != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.IsReport = ' + @IsReport + ')';
	END

	IF (@IsWeirdo != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.IsReport = ' + @IsWeirdo + ')';
	END
			-- FromDate Condition
	IF (@StartDate!='' or @EndDate !='' )
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack +
		--						' AND ((TItem.FromDate <= ''' + @EndDate + ''' And TItem.FromDate >=''' + @StartDate + ''') or (TItem.ToDate <= ''' + @EndDate + ''' And TItem.ToDate >=''' + @StartDate + ''') or (TItem.FromDate <= ''' + @StartDate  + ''' And TItem.ToDate >=''' + @EndDate + '''))' ;
								SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND ((TItem.FromDate < ''' + @EndDate + ''' And TItem.ToDate >''' + @StartDate + '''))' ;

	END
	--print(@ProTaskSqlTrack);
	-- lấy all vào #ProTaskTbl
	EXEC sp_executesql @ProTaskSqlTrack;
	--print @IsAllLevel;
	-- Neu lay project
	IF (@ParentId = '' and @IsAllLevel = 0)
	BEGIN
	-- lấy projectId vào #ProTbl
		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				select ProjectId from (SELECT ProjectId
				FROM #ProTaskTbl) AS T3
GROUP BY ProjectId'
				;

		EXEC sp_executesql @ProSqlTrack;

		-- Get info Project from #ProTbl
		SET @SqlTrack = 
				'INSERT INTO #DocTbl
				SELECT 
					Pro.Id AS [Id],
					Pro.Id As [ProjectId],
					''00000000-0000-0000-0000-000000000000'' AS [ParentId],
					Pro.Summary AS [Name], 
					''project'' AS [Type],
					ProStatus.Code AS [Status],
					CASE 
						When dbo.Func_Doc_CheckIsOverDue(Pro.ToDate, 
																	Pro.FinishedDate, 
																	Pro.ProjectStatusId, 
																	dateadd(DAY, 2, getdate()))=1 
																		and dbo.Func_Doc_CheckIsOverDue(Pro.ToDate, 
																	Pro.FinishedDate, 
																	Pro.ProjectStatusId, 
																	getdate()) = 0  Then ''near-of-date''
						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate <= Pro.ToDate THEN ''in-due-date''
						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate >  Pro.ToDate THEN ''out-of-date''
						WHEN Pro.ProjectStatusId != 4 AND Pro.ToDate       >= GETDATE()  THEN ''in-due-date''
					ELSE ''out-of-date'' 
					END AS [Process],
					[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
					Pro.FromDate AS [FromDate],
					Pro.ToDate AS [ToDate],
					Pro.ApprovedBy AS [ApprovedBy],
					Pro.ApprovedBy AS [AssignBy],
					NULL AS [UserId],
					Pro.DepartmentId AS [DepartmentId],
					ProStatus.Name AS [StatusName]
				FROM #ProTbl TempTbl
				LEFT JOIN [Task].[Project] Pro ON TempTbl.ProjectId = Pro.Id
				LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
				ORDER BY Pro.CreatedDate DESC, Pro.Summary';

		EXEC sp_executesql @SqlTrack;

	END
	-- Neu lay task
	ELSE
	BEGIN

		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				SELECT DISTINCT ProjectId
				FROM #ProTaskTbl';

		EXEC sp_executesql @ProSqlTrack;

		SET @TaskSqlTrack = 
				'INSERT INTO #TempTItemTbl
				SELECT DISTINCT TaskItemId
				FROM #ProTaskTbl';

		EXEC sp_executesql @TaskSqlTrack;
		
		-- Insert vào @TaskItemId danh sách TaskItemId có quan hệ
		DECLARE @TaskItemId NVARCHAR(200);
		DECLARE MY_CURSOR CURSOR
		LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR
		SELECT TaskItemId FROM #TempTItemTbl

		OPEN MY_CURSOR
		FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			--Do something with Id here
			WITH tbParent AS
			(
				SELECT Id, ParentId 
				FROM [Task].[TaskItem]
				WHERE Id = @TaskItemId  and IsDeleted = 0
				
				UNION ALL

				SELECT TItem.Id, TItem.ParentId
				FROM [Task].[TaskItem] TItem
				JOIN tbParent 
				ON TItem.Id = tbParent.ParentId
				
			)

			INSERT INTO #TempTwoTItemTbl
			SELECT Id AS Id
			FROM tbParent

			FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR

		-- Đưa TaskItemId đã được lọc duplicate vào trong #TItemTbl
		INSERT INTO #TItemTbl
		SELECT DISTINCT *
		FROM #TempTwoTItemTbl
		SET @SqlTrack =
			'INSERT INTO #DocTbl
				SELECT TItem.Id AS [Id],
					TItem.ProjectId AS [ProjectId],
					TItem.ParentId AS [ParentId],
					TItem.TaskName AS [Name],
					CASE 
						WHEN TItem.IsGroupLabel = 1 THEN ''group''
						ELSE ''''
						END AS [Type],
					TIStatus.Code AS [Status],
					CASE
									When dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	TItem.FinishedDate, 
																	TItem.TaskItemStatusId, 
																	dateadd(DAY, 2, getdate()))=1 
																		And dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	TItem.FinishedDate, 
																	TItem.TaskItemStatusId, 
																	getdate()) = 0  Then ''near-of-date''
						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate <= TItem.ToDate THEN ''in-due-date''
						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate >  TItem.ToDate THEN ''out-of-date''
						WHEN TItem.TaskItemStatusId != 4 AND TItem.ToDate       >= GETDATE()    THEN ''in-due-date''
						ELSE ''out-of-date''
						END AS [Process],
					[dbo].[Func_CheckChildrenOfTaskItem](TItem.Id) AS [HasChildren],
					TItem.FromDate AS [FromDate],
					TItem.ToDate AS [ToDate],
					''00000000-0000-0000-0000-000000000000'' AS [ApprovedBy],
					TItem.AssignBy AS [AssignBy],
					[dbo].[Func_GetPriorityUserIdByTaskItemId](TItem.Id) AS [UserId],
					TItem.DepartmentId AS [DepartmentId],
					TIStatus.Name AS [StatusName]
				FROM [Task].[TaskItem] AS TItem
				LEFT JOIN [Task].[TaskItemStatus] AS TIStatus
				ON TItem.TaskItemStatusId = TIStatus.Id 
				WHERE TItem.TaskName IS NOT NULL ';

		IF (@ParentId != '' and EXISTS (SELECT Id FROM [Task].[Project] WHERE Id = @ParentId))
		BEGIN
			if(@IsAllLevel = 0)
			SET @SqlTrack = @SqlTrack +
							' AND (TItem.ProjectId = ''' + @ParentId + ''' 
								AND (TItem.ParentId IS NULL OR TItem.ParentId = ''00000000-0000-0000-0000-000000000000'')) ';
			else
			SET @SqlTrack = @SqlTrack +
							' AND (TItem.ProjectId = ''' + @ParentId + ''' and TItem.IsGroupLabel != 1 ) ';
		END
		ELSE
		BEGIN
			if(@IsAllLevel = 1)
			SET @SqlTrack = @SqlTrack +
							' AND (TItem.IsGroupLabel != 1 ) ';
			if(@IsAllLevel = 0 and @ParentId!='')
			SET @SqlTrack = @SqlTrack + 
							' AND (TItem.ParentId = ''' + @ParentId + ''')';
		END

		SET @SqlTrack = @SqlTrack + 
				' AND TItem.Id IN (SELECT TaskItemId FROM #TItemTbl)';

	
		SET @SqlTrack = @SqlTrack +
						' ORDER BY TItem.[Order], TItem.CreatedDate';
		--Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;

	END

	-- END: AFTER CHANGING

	-- lấy số trang
	SET @TotalRecord = (SELECT COUNT(*) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);

	-- lấy dữ liệu phân trang
	IF (@IsCount = 1 AND @ParentId = '' and @IsAllLevel=0)
	BEGIN
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
		SELECT Pro.Id AS [Id],
			Pro.Summary AS [Name], 
			Pro.Id As [ProjectId],
			'project' AS [Type],
			TempPro.[Status] AS [Status],
				pro.PercentFinish AS [PercentFinish],
				TempPro.Process  as Process,
			[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
			Pro.FromDate AS [FromDate],
			Pro.ToDate AS [ToDate],
			Pro.ApprovedBy AS [ApprovedBy],
			Pro.DepartmentId AS [DepartmentId],
			ProStatus.Name AS [StatusName],
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			Pro.CreatedBy,
			'' as ProcessClass,
			Pro.ManagerId as UsersPrimary,
			pro.UserViews as UsersSecond,
			'' as UsersThird 

		FROM #DocTbl TempPro
		LEFT JOIN [Task].[Project] Pro ON TempPro.Id = Pro.Id
		LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
		ORDER BY Pro.CreatedDate DESC, Pro.Summary
		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
		FETCH NEXT @PageSize ROWS ONLY;

	END
	ELSE IF @ParentId = '' and @IsAllLevel=0
	BEGIN
		SELECT 
			Doc.*,
			p.PercentFinish AS [PercentFinish],
			p.UserViews [UserViews],
			(SELECT Count(ti.Id) FROM Task.TaskItem ti
				WHERE ti.ProjectId = Doc.Id AND (ti.IsGroupLabel is null Or ti.IsGroupLabel = 0) AND (ti.AssignBy = @CurrentUserId Or (select Count(tia.Id) from Task.TaskItemAssign tia where tia.ProjectId = ti.Id AND tia.AssignTo = @CurrentUserId) > 0)) As CountTask,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
				'' as ProcessClass,
			p.ManagerId as UsersPrimary,
			p.UserViews as UsersSecond,
			'' as UsersThird 
		FROM #DocTbl Doc INNER JOIN Task.Project p ON p.Id = Doc.Id
		ORDER BY p.CreatedDate DESC, p.Summary
	END
	ELSE
	BEGIN
	if (@IsCount = 1)
		begin
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
			SELECT 
			Doc.*,
			p.IsGroupLabel,
			CASE
				WHEN p.PercentFinish <= 0 THEN (Select top 1 PercentFinish From Task.TaskItemAssign tia where tia.TaskType = 1 AND tia.TaskItemId = Doc.Id)
				ELSE p.PercentFinish
			END as PercentFinish,
			p.Conclusion as Content,
			(SELECT Count(ti.Id) FROM Task.TaskItem ti
				WHERE ti.ParentId = Doc.Id) As CountTask,
			(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
			(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
			[Task].[Udf_Get_Process_Class_Grantt_View_By_Task](p.ProjectId,p.Id) as ProcessClass,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](1,p.Id,p.ProjectId) as UsersPrimary,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](3,p.Id,p.ProjectId) as UsersSecond,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](7,p.Id,p.ProjectId) as UsersThird

		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		where p.IsDeleted = 0
		ORDER BY p.[Order], p.CreatedDate
		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
		FETCH NEXT @PageSize ROWS ONLY;
		end
	else 
	begin
		SELECT 
			Doc.*,
			p.IsGroupLabel,
			CASE
				WHEN p.PercentFinish <= 0 THEN (Select top 1 PercentFinish From Task.TaskItemAssign tia where tia.TaskType = 1 AND tia.TaskItemId = Doc.Id)
				ELSE p.PercentFinish
			END as PercentFinish,
			p.Conclusion as Content,
			(SELECT Count(ti.Id) FROM Task.TaskItem ti
				WHERE ti.ParentId = Doc.Id) As CountTask,
			(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
			(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
			[Task].[Udf_Get_Process_Class_Grantt_View_By_Task](p.ProjectId,p.Id) as ProcessClass,
				[Task].[Udf_Get_UserIdAssign_By_TaskType](1,p.Id,p.ProjectId) as UsersPrimary,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](3,p.Id,p.ProjectId) as UsersSecond,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](7,p.Id,p.ProjectId) as UsersThird
		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		where p.IsDeleted = 0
		ORDER BY p.[Order], p.CreatedDate
		end
	END
	DROP TABLE #TempOneTItemTbl;
	DROP TABLE #TempTwoTItemTbl;
	DROP TABLE #TempTItemTbl;
	DROP TABLE #TItemTbl;
	DROP TABLE #ProTaskTbl;
	DROP TABLE #ProTbl;
	DROP TABLE #DocTbl;

END TRY
BEGIN CATCH
	-- Whoops, there was an error
	IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
	
	-- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), 
			@ErrSeverity int;

	SELECT @ErrMsg = ERROR_MESSAGE(),
		   @ErrSeverity = ERROR_SEVERITY();

	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
");
            }
            if(newVersion < 20210613)
            {
                newVersion = 20210613;
                queries.Add(@"ALTER   PROCEDURE [dbo].[SP_UPDATE_TASK_RANGE_DATE]
	@ProjectId uniqueidentifier,
	@TaskId uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime,
	@IsUpdateStatus bit null
AS
BEGIN
	SET NOCOUNT ON;
	create table #temp (id uniqueidentifier);
WITH tbParent AS
(
	SELECT Id, ParentId
	FROM [Task].[TaskItem]
	WHERE Id = @TaskId AND IsDeleted = 0
	UNION ALL
	SELECT TItem.Id, TItem.ParentId
	FROM [Task].[TaskItem] TItem
	JOIN tbParent 
	ON TItem.Id = tbParent.ParentId where TItem.IsDeleted = 0
)			
	insert into #temp
	select distinct Id from tbParent where id != @TaskId;
	if(Exists(select * from [Task].[Project] 
	where Id = @ProjectId ))
	begin
		update [Task].[TaskItem] set FromDate = @FromDate
		where FromDate > @FromDate and Id in (select id from #temp)
		update [Task].[TaskItem] set ToDate = @ToDate
		where ToDate < @ToDate and Id in (select id from #temp)

		update [Task].[Project] set FromDate = @FromDate
		where FromDate > @FromDate and Id = @ProjectId
		update [Task].[Project] set ToDate = @ToDate
		where ToDate < @ToDate and Id = @ProjectId
	end
	if(@IsUpdateStatus = 1)
	begin 
		update [Task].[TaskItem] set TaskItemStatusId = 1
			where IsGroupLabel!= 1 and TaskItemStatusId = 0 and Id in (select id from #temp);
		update [Task].[Project] set ProjectStatusId = 3
			where ProjectStatusId = 0 and Id = @ProjectId;
	end
	
	drop table #temp;
	
END");
            }
            if (newVersion < 20210622)
            {
                newVersion = 20210622;
                queries.Add(@"Insert into [Task].[Action]
  values (13, N'Từ chối gia hạn',1),(14, N'Báo cáo trả lại',1)");
            }
            if (newVersion < version)
            {
                newVersion = version;

                queries.Add(@"ALTER TABLE Core.Notifications ALTER COLUMN NotificationTypeId uniqueidentifier NULL;");
                queries.Add(@"insert into [Task].[TaskItemStatus]
  Values(9, N'Nháp', 'DRAFT', 1)");
                #region 6
                queries.Add(@"CREATE TABLE [Task].[AdminCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Summary] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_AdminCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO");
                #endregion
                #region 5
                queries.Add(@"
ALTER   PROCEDURE [dbo].[SP_Select_Projects_With_Filter_By_Folder] 
	-- Add the parameters for the stored procedure here
	@CurrentUserId		NVARCHAR(200),
	@PrivateFolder		VARCHAR(50),
	@Keyword			NVARCHAR(200)		= '',
	@ParentId			NVARCHAR(200)		= '',
	@IsSearching		BIT					= 0,
	@AssignBy			NVARCHAR(200)		= '',
	@TaskItemStatusId	VARCHAR(10)			= '',
	@AssignTo			NVARCHAR(200)		= '',
	@TaskType			VARCHAR(10)			= '',
	@FromDate			VARCHAR(50)			= '',
	@ToDate				VARCHAR(50)			= '',
	@StatusTime			INT					= 0,
	@TaskItemPriorityId VARCHAR(10)			= '',
	@ProjectHashtag	NVARCHAR(255)		= '',
	@IsReport			VARCHAR(10)			= '',
	@IsWeirdo			VARCHAR(10)			= '',
	@Page				INT					= 1,
	@PageSize			INT					= 10,
	@IsCount			BIT					= 0
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;

    -- Insert statements for procedure here
	DECLARE @ProFolSqlTrack nvarchar(MAX),
			@ProSqlTrack nvarchar(MAX),
			@TaskSqlTrack nvarchar(MAX),
			@SqlTrack nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int;

	CREATE TABLE #TempOneTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TempTwoTItemTbl (TaskItemId uniqueidentifier);

	CREATE TABLE #TempTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TItemTbl (TaskItemId uniqueidentifier);

	CREATE TABLE #ProFolTbl (FolderId uniqueidentifier, ProjectId uniqueidentifier, TaskItemId uniqueidentifier, TaskItemAssignId uniqueidentifier);
	CREATE TABLE #ProTbl (ProjectId uniqueidentifier);

	CREATE TABLE #DocTbl 
	(
		[Id] uniqueidentifier,
		[ProjectId] uniqueidentifier,
		[ParentId] uniqueidentifier,
		[Name] NVARCHAR(MAX),
		[Type] VARCHAR(10),
		[Status] VARCHAR(20),
		[Process] VARCHAR(20),
		[HasChildren] BIT,
		[FromDate] DATETIME,
		[ToDate] DATETIME,
		[ApprovedBy] uniqueidentifier,
		[AssignBy] uniqueidentifier,
		[UserId] uniqueidentifier,
		[DepartmentId] uniqueidentifier,
		[StatusName] NVARCHAR(50)
	);

	SET @ProFolSqlTrack = 
			'INSERT INTO #ProFolTbl
			SELECT Pfd.FolderId, Pro.Id, TItem.Id, TIAssign.Id
			FROM [Task].[ProjectFolderDetail] AS Pfd
			LEFT JOIN [Task].[Project] AS Pro ON Pfd.ProjectId = Pro.Id
			RIGHT JOIN [Task].[TaskItem] AS TItem ON Pro.Id = TItem.ProjectId
			RIGHT JOIN [Task].[TaskItemAssign] AS TIAssign ON TItem.Id = TIAssign.TaskItemId
			WHERE Pro.IsActive = 1 
				  AND Pfd.FolderId = ''' + @PrivateFolder + ''' 
				  AND (Pro.ApprovedBy = ''' + @CurrentUserId + ''' 
					OR Pro.CreatedBy = ''' + @CurrentUserId + '''
					OR TItem.AssignBy = ''' + @CurrentUserId + '''
					OR (TIAssign.TaskItemId IS NOT NULL 
						AND TIAssign.TaskItemId != ''00000000-0000-0000-0000-000000000000''
						AND (TIAssign.AssignTo = ''' + @CurrentUserId + '''
								OR TIAssign.AssignFollow = ''' + @CurrentUserId + '''))) ';

	IF (@Keyword != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND (Pro.Summary like N''%' + @Keyword + '%''
										OR TItem.TaskName like N''%' + @Keyword + '%'')';
	END

	-- AssignBy Condition
	IF (@AssignBy != '') 
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND TItem.AssignBy = ''' + @AssignBy + ''' ';
	END

	-- TaskItemStatusId Condition
	IF (@TaskItemStatusId != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND TItem.TaskItemStatusId = ' + @TaskItemStatusId + ' ';
	END

	-- AssignTo Condition
	IF (@AssignTo != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND TIAssign.AssignTo = ''' + @AssignTo + ''' ';
	END

	-- TaskType Condition
	IF (@TaskType != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND TIAssign.TaskType = ' + @TaskType + ' ';
	END

	IF (@StatusTime = 1)
	BEGIN
		-- FromDate Condition
		IF (@FromDate != '')
		BEGIN
			SET @ProFolSqlTrack = @ProFolSqlTrack +
									' AND (TIAssign.FromDate >= ''' + @FromDate + ''' 
											OR TItem.FromDate >= ''' + @FromDate + '''
											OR Pro.FromDate >= ''' + @FromDate + ''')';
		END

		-- ToDate Condition
		IF (@ToDate != '')
		BEGIN
			SET @ProFolSqlTrack = @ProFolSqlTrack +
									' AND (TIAssign.ToDate <= ''' + @ToDate + '''
											OR TItem.ToDate <= ''' + @ToDate + '''
											OR Pro.ToDate <= ''' + @ToDate + ''')';
		END
	END
	ELSE
	BEGIN
		IF (@StatusTime = 1)
		BEGIN
			SET @ProFolSqlTrack = @ProFolSqlTrack +
									' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	  TItem.FinishedDate, 
																	  TItem.TaskItemStatusId, 
																	  dateadd(DAY, 2, getdate())) = 1
									  AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	  TItem.FinishedDate, 
																	  TItem.TaskItemStatusId, 
																	  getdate()) = 0 
																	  AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 )';
		END

		IF (@StatusTime = 2)
		BEGIN
			SET @ProFolSqlTrack = @ProFolSqlTrack +
									' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	  TItem.FinishedDate,
																	  TItem.TaskItemStatusId, 
																	  getdate()) = 1 
																	  AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 )';
		END
	END

	-- Priority
	IF (@TaskItemPriorityId != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND (TItem.TaskItemPriorityId = ' + @TaskItemPriorityId + ')';
	END

	-- Hashtag
	IF (@ProjectHashtag != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	-- IsReport
	IF (@IsReport != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND (TItem.IsReport = ' + @IsReport + ')';
	END

	IF (@IsWeirdo != '')
	BEGIN
		SET @ProFolSqlTrack = @ProFolSqlTrack +
								' AND (TItem.IsReport = ' + @IsWeirdo + ')';
	END

	EXEC sp_executesql @ProFolSqlTrack;

	-- Neu lay project
	IF (@ParentId = '')
	BEGIN
		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				SELECT DISTINCT ProjectId
				FROM #ProFolTbl';

		EXEC sp_executesql @ProSqlTrack;

		-- Get info Project from #ProTbl
		SET @SqlTrack = 
				'INSERT INTO #DocTbl
				SELECT 
					Pro.Id AS [Id],
					Pro.Id As [ProjectId],
					''00000000-0000-0000-0000-000000000000'' AS [ParentId],
					Pro.Summary AS [Name], 
					''project'' AS [Type],
					ProStatus.Code AS [Status],
					CASE 
						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate <= Pro.ToDate THEN ''in-due-date''
						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate >  Pro.ToDate THEN ''out-of-date''
						WHEN Pro.ProjectStatusId != 4 AND Pro.ToDate       >= GETDATE()  THEN ''in-due-date''
					ELSE ''out-of-date'' 
					END AS [Process],
					[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
					Pro.FromDate AS [FromDate],
					Pro.ToDate AS [ToDate],
					Pro.ApprovedBy AS [ApprovedBy],
					''00000000-0000-0000-0000-000000000000'' AS [AssignBy],
					NULL AS [UserId],
					Pro.DepartmentId AS [DepartmentId],
					ProStatus.Name AS [StatusName]
				FROM #ProTbl TempTbl
				LEFT JOIN [Task].[Project] Pro ON TempTbl.ProjectId = Pro.Id
				LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
				ORDER BY Pro.CreatedDate DESC, Pro.Summary';

		EXEC sp_executesql @SqlTrack;
	END
	ELSE
	BEGIN
		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				SELECT DISTINCT ProjectId
				FROM #ProFolTbl';

		EXEC sp_executesql @ProSqlTrack;

		SET @TaskSqlTrack = 
				'INSERT INTO #TempTItemTbl
				SELECT DISTINCT TaskItemId
				FROM #ProFolTbl';

		EXEC sp_executesql @TaskSqlTrack;

		-- Insert vào @TaskItemId danh sách TaskItemId có quan hệ
		DECLARE @TaskItemId NVARCHAR(200);
		DECLARE MY_CURSOR CURSOR
		LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR
		SELECT TaskItemId FROM #TempTItemTbl

		OPEN MY_CURSOR
		FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			--Do something with Id here
			WITH tbParent AS
			(
				SELECT Id, ParentId 
				FROM [Task].[TaskItem]
				WHERE Id = @TaskItemId
				
				UNION ALL

				SELECT TItem.Id, TItem.ParentId
				FROM [Task].[TaskItem] TItem
				JOIN tbParent 
				ON TItem.Id = tbParent.ParentId
			)

			INSERT INTO #TempTwoTItemTbl
			SELECT Id AS Id
			FROM tbParent

			FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR

		-- Đưa TaskItemId đã được lọc duplicate vào trong #TItemTbl
		INSERT INTO #TItemTbl
		SELECT DISTINCT *
		FROM #TempTwoTItemTbl
		SET @SqlTrack =
			'INSERT INTO #DocTbl
				SELECT TItem.Id AS [Id],
					TItem.ProjectId AS [ProjectId],
					TItem.ParentId AS [ParentId],
					TItem.TaskName AS [Name],
					CASE 
						WHEN TItem.IsGroupLabel = 1 THEN ''group''
						ELSE ''''
						END AS [Type],
					TIStatus.Code AS [Status],
					CASE
						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate <= TItem.ToDate THEN ''in-due-date''
						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate >  TItem.ToDate THEN ''out-of-date''
						WHEN TItem.TaskItemStatusId != 4 AND TItem.ToDate       >= GETDATE()    THEN ''in-due-date''
						ELSE ''out-of-date''
						END AS [Process],
					[dbo].[Func_CheckChildrenOfTaskItem](TItem.Id) AS [HasChildren],
					TItem.FromDate AS [FromDate],
					TItem.ToDate AS [ToDate],
					''00000000-0000-0000-0000-000000000000'' AS [ApprovedBy],
					TItem.AssignBy AS [AssignBy],
					[dbo].[Func_GetPriorityUserIdByTaskItemId](TItem.Id) AS [UserId],
					TItem.DepartmentId AS [DepartmentId],
					TIStatus.Name AS [StatusName]
				FROM [Task].[TaskItem] AS TItem
				LEFT JOIN [Task].[TaskItemStatus] AS TIStatus
				ON TItem.TaskItemStatusId = TIStatus.Id 
				WHERE TItem.TaskName IS NOT NULL ';

		IF (EXISTS (SELECT Id FROM [Task].[Project] WHERE Id = @ParentId))
		BEGIN
			SET @SqlTrack = @SqlTrack +
							' AND (TItem.ProjectId = ''' + @ParentId + ''' 
								AND (TItem.ParentId IS NULL OR TItem.ParentId = ''00000000-0000-0000-0000-000000000000'')) ';
		END
		ELSE
		BEGIN
			SET @SqlTrack = @SqlTrack + 
							' AND (TItem.ParentId = ''' + @ParentId + ''')';
		END

		SET @SqlTrack = @SqlTrack + 
				' AND TItem.Id IN (SELECT TaskItemId FROM #TItemTbl)';

		
		SET @SqlTrack = @SqlTrack +
						' ORDER BY TItem.CreatedDate, TItem.TaskName';

		EXEC sp_executesql @SqlTrack;
	END

	-- lấy số trang
	SET @TotalRecord = (SELECT COUNT(*) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);

	-- lấy dữ liệu phân trang
	IF (@IsCount = 1)
	BEGIN
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;

		SELECT Pro.Id AS [Id],
			Pro.Summary AS [Name], 
			Pro.Id As [ProjectId],
			'project' AS [Type],
			CASE 
				WHEN ProjectStatusId = 4 THEN 'ProjectStatusId' 
				ELSE 'not-finish' END AS [Status],
			[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
			Pro.FromDate AS [FromDate],
			Pro.ToDate AS [ToDate],
			Pro.ApprovedBy AS [ApprovedBy],
			Pro.DepartmentId AS [DepartmentId],
			ProStatus.Name AS [StatusName],
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord]
		FROM #DocTbl TempPro
		LEFT JOIN [Task].[Project] Pro ON TempPro.Id = Pro.Id
		LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
		ORDER BY Pro.CreatedDate DESC, Pro.Summary
		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
		FETCH NEXT @PageSize ROWS ONLY;

	END
	ELSE
	BEGIN
		SELECT 
			Doc.*,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord]
		FROM #DocTbl Doc
	END

	DROP TABLE #TempOneTItemTbl;
	DROP TABLE #TempTwoTItemTbl;
	DROP TABLE #TempTItemTbl;
	DROP TABLE #TItemTbl;
	DROP TABLE #ProFolTbl;
	DROP TABLE #ProTbl;
	DROP TABLE #DocTbl;

END TRY
BEGIN CATCH
	-- Whoops, there was an error
	IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
	
	-- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), 
			@ErrSeverity int;

	SELECT @ErrMsg = ERROR_MESSAGE(),
		   @ErrSeverity = ERROR_SEVERITY();

	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH



");
                #endregion
                #region 3
                queries.Add(@"ALTER TABLE Task.TaskItem ADD
	IsAdminCategory bit NOT NULL CONSTRAINT DF_TaskItem_IsAdminCategory DEFAULT 0");
                queries.Add(@"ALTER TABLE Task.Project ADD
	AdminCategoryId uniqueidentifier NULL");
                queries.Add(@"ALTER TABLE Task.TaskItem ADD
	AdminCategoryId uniqueidentifier NULL");
                #endregion
                #region 1
                queries.Add(@"
Create  PROCEDURE [dbo].[SP_ADMIN_CATEGORY_CLONE_TASK]
	@AdminCategoryId uniqueidentifier,
	@ProjectId uniqueidentifier,
	@ParentId uniqueidentifier = null,
	@CurrentUserId	uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @temp TABLE(id int identity(1,1), newAttrID uniqueidentifier, oldAttrID uniqueidentifier null)
	insert into Task.TaskItem
	output inserted.Id, null into @temp
	select 
	NEWID(),
	@ProjectId
	,[TaskName]
      ,[FromDate]
      ,[ToDate]
      ,[FinishedDate]
      ,9
      ,getdate()
      ,@CurrentUserId
      ,@CurrentUserId
      ,[ParentId]
      ,[PercentFinish]
      ,[TaskType]
      ,[IsReport]
      ,getdate()
      ,[ParentId]
      ,[Conclusion]
      ,[TaskItemPriorityId]
      ,[DepartmentId]
      ,[TaskItemCategoryId]
      ,[IsSecurity]
      ,[IsWeirdo]
      ,[HasRecentActivity]
      ,[Weight]
      ,[IsDeleted]
      ,[IsGroupLabel]
      ,[TaskItemCategory]
      ,[NatureTaskId]
      ,[Order]
      ,[IsAuto]
      ,[TaskGroupType]
      ,[IsExtend]
      ,[ExtendDate]
      ,0, null from Task.TaskItem as t
	  where t.ProjectId = @AdminCategoryId
	  ORDER BY t.Id ASC

	   ;WITH CTE AS
    (
        SELECT t.Id,
        --Use ROW_NUMBER to get matching id which is same as the one generated in @temp
        ROW_NUMBER()OVER(ORDER BY t.Id ASC) idx
        FROM Task.TaskItem t
        WHERE t.ProjectId = @AdminCategoryId
    )
	 UPDATE T
    SET oldAttrID = CTE.Id
    FROM @temp T
    INNER JOIN CTE ON T.id = CTE.idx;

	update tk
	set tk.ParentId = Case
	when tk.ParentId is null then @ParentId
	else T.newAttrID end
	from Task.TaskItem as tk left join @temp as T
	on tk.ParentId = T.oldAttrID
	where tk.ProjectId = @ProjectId and tk.Id in (select newAttrID from @temp);

	INSERT INTO Task.TaskItemAssign (
	 [Id]
      ,[TaskItemId]
      ,[ProjectId]
      ,[AssignTo]
      ,[LastResult]
      ,[TaskItemStatusId]
      ,[ModifiedDate]
      ,[PercentFinish]
      ,[FromDate]
      ,[ToDate]
      ,[FinishedDate]
      ,[TaskType]
	)
        SELECT 
			NEWID(),
            T.NewAttrID
			,@ProjectId
			,[AssignTo]
			,[LastResult]
			,[TaskItemStatusId]
			,[ModifiedDate]
			,[PercentFinish]
			,[FromDate]
			,[ToDate]
			,[FinishedDate]
			,[TaskType]
        FROM Task.TaskItemAssign tas
        INNER JOIN @temp T
        ON T.oldAttrID = tas.TaskItemId

	INSERT INTO Task.Attachment
        SELECT 
			NEWID()
			,@ProjectId
            ,T.NewAttrID
			,[Source]
			,[FileName]
			,[FileExt]
			,[FileContent]
			,GETDATE()
			,@CurrentUserId
        FROM Task.Attachment at
        INNER JOIN @temp T
        ON T.oldAttrID = at.ItemId
END
");
                #endregion
                #region 2
                queries.Add(@"
Create PROCEDURE [dbo].[SP_Select_AdminCategories] 
	-- Add the parameters for the stored procedure here
	@ParentId			NVARCHAR(200)		= '',
	@Page				INT					= 1,
	@PageSize			INT					= 10,
	@OrderBy	  		NVARCHAR(50)			= 'CreatedDate DESC',
	@ProjectId nvarchar(50) = '',
	@IsCount			BIT					= 0
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;
    -- Insert statements for procedure here
	-- Create temp table
	CREATE TABLE #TempOneTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TempTwoTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TempTItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #TItemTbl (TaskItemId uniqueidentifier);
	CREATE TABLE #ProTaskTbl (TaskItemId uniqueidentifier, ProjectId uniqueidentifier);
	CREATE TABLE #ProTbl (ProjectId uniqueidentifier);
	CREATE TABLE #DocTbl 
	(
		[Id] uniqueidentifier,
		[ProjectId] uniqueidentifier,
		[ParentId] uniqueidentifier,
		[Name] NVARCHAR(MAX),
		[Type] VARCHAR(10),
		[Status] VARCHAR(20),
		[Process] VARCHAR(20),
		[HasChildren] BIT,
		[FromDate] DATETIME,
		[ToDate] DATETIME,
		[ApprovedBy] uniqueidentifier,
		[AssignBy] uniqueidentifier,
		[UserId] uniqueidentifier,
		[DepartmentId] uniqueidentifier,
		[StatusName] NVARCHAR(50)
	);
	-- Create variable
	DECLARE 
			@ProTaskSqlTrack nvarchar(MAX),
			@ProSqlTrack nvarchar(MAX),
			@TaskSqlTrack nvarchar(MAX),
			@SqlTrack nvarchar(MAX),
			@SqlFilter nvarchar(200),
			@Sql nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int,
			@FilerTrackType nvarchar(100) = '',
			@IsParentInProject bit = 0;
	-- AFTER CHANGING
	-- Lay danh project ban dau
	SET @ProTaskSqlTrack =
			'INSERT INTO #ProTaskTbl
			SELECT TItem.Id, Pro.Id AS ProjectId 
			FROM [Task].[TaskItem] TItem
 			FULL OUTER JOIN [Task].[AdminCategory] Pro ON TItem.ProjectId = Pro.Id
			WHERE
				Pro.IsActive = 1 ';
	IF (@ParentId!= '' and EXISTS (SELECT Id FROM [Task].[AdminCategory] WHERE Id = @ParentId))
		BEGIN
			SET @ProjectId = @ParentId;
		END
	IF (@ProjectId != '') 
	BEGIN
			SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND Pro.Id = ''' + @ProjectId + '''';
	END
	-- lấy all vào #ProTaskTbl
	EXEC sp_executesql @ProTaskSqlTrack;
	--print @IsAllLevel;
	-- Neu lay project
	IF (@ParentId = '')
	BEGIN
	-- lấy projectId vào #ProTbl
		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				select ProjectId from (SELECT ProjectId
				FROM #ProTaskTbl) AS T3
GROUP BY ProjectId'
				;
		EXEC sp_executesql @ProSqlTrack;
		-- Get info Project from #ProTbl
		SET @SqlTrack = 
				'INSERT INTO #DocTbl
				SELECT 
					Pro.Id AS [Id],
					Pro.Id As [ProjectId],
					''00000000-0000-0000-0000-000000000000'' AS [ParentId],
					Pro.Summary AS [Name], 
					''project'' AS [Type],
					'''' as [Status],
					'''' as [Process],
					[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
					NULL AS [FromDate],
					NULL AS [ToDate],
					NULL AS [ApprovedBy],
					NULL AS [AssignBy],
					NULL AS [UserId],
					NULL [DepartmentId],
					NULL AS [StatusName]
				FROM #ProTbl TempTbl
				LEFT JOIN [Task].[AdminCategory] Pro ON TempTbl.ProjectId = Pro.Id
				ORDER BY Pro.CreatedDate DESC, Pro.Summary';
		EXEC sp_executesql @SqlTrack;
	END
	-- Neu lay task
	ELSE
	BEGIN
		SET @ProSqlTrack = 
				'INSERT INTO #ProTbl
				SELECT DISTINCT ProjectId
				FROM #ProTaskTbl';
		EXEC sp_executesql @ProSqlTrack;
		SET @TaskSqlTrack = 
				'INSERT INTO #TempTItemTbl
				SELECT DISTINCT TaskItemId
				FROM #ProTaskTbl';
		EXEC sp_executesql @TaskSqlTrack;
		-- Insert vào @TaskItemId danh sách TaskItemId có quan hệ
		DECLARE @TaskItemId NVARCHAR(200);
		DECLARE MY_CURSOR CURSOR
		LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR
		SELECT TaskItemId FROM #TempTItemTbl
		OPEN MY_CURSOR
		FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			--Do something with Id here
			WITH tbParent AS
			(
				SELECT Id, ParentId 
				FROM [Task].[TaskItem]
				WHERE Id = @TaskItemId  and IsDeleted = 0
				UNION ALL
				SELECT TItem.Id, TItem.ParentId
				FROM [Task].[TaskItem] TItem
				JOIN tbParent 
				ON TItem.Id = tbParent.ParentId
			)
			INSERT INTO #TempTwoTItemTbl
			SELECT Id AS Id
			FROM tbParent
			FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR
		-- Đưa TaskItemId đã được lọc duplicate vào trong #TItemTbl
		INSERT INTO #TItemTbl
		SELECT DISTINCT *
		FROM #TempTwoTItemTbl
		SET @SqlTrack =
			'INSERT INTO #DocTbl
				SELECT TItem.Id AS [Id],
					TItem.ProjectId AS [ProjectId],
					TItem.ParentId AS [ParentId],
					TItem.TaskName AS [Name],
					CASE 
						WHEN TItem.IsGroupLabel = 1 THEN ''group''
						ELSE ''''
						END AS [Type],
					'''' as [Status],
					'''' as [Process],
					[dbo].[Func_CheckChildrenOfTaskItem](TItem.Id) AS [HasChildren],
					TItem.FromDate AS [FromDate],
					TItem.ToDate AS [ToDate],
					NULL AS [ApprovedBy],
					NULL AS [AssignBy],
					NULL AS [UserId],
					NULL [DepartmentId],
					NULL AS [StatusName]
				FROM [Task].[TaskItem] AS TItem
				WHERE TItem.TaskName IS NOT NULL ';
		IF (@ParentId != '' and EXISTS (SELECT Id FROM [Task].[AdminCategory] WHERE Id = @ParentId))
		BEGIN
			SET @SqlTrack = @SqlTrack +
							' AND (TItem.ProjectId = ''' + @ParentId + ''' 
								AND (TItem.ParentId IS NULL OR TItem.ParentId = ''00000000-0000-0000-0000-000000000000'')) ';
		END
		ELSE
		BEGIN
			SET @SqlTrack = @SqlTrack + 
							' AND (TItem.ParentId = ''' + @ParentId + ''')';
		END
		SET @SqlTrack = @SqlTrack + 
				' AND TItem.Id IN (SELECT TaskItemId FROM #TItemTbl)';
		SET @SqlTrack = @SqlTrack +
						' ORDER BY TItem.[Order], TItem.CreatedDate';
						Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;
		Print 2;

	END
	-- END: AFTER CHANGING
	-- lấy số trang
	SET @TotalRecord = (SELECT COUNT(*) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	-- lấy dữ liệu phân trang
	IF (@IsCount = 1 AND @ParentId = '')
	BEGIN
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
		SELECT Pro.Id AS [Id],
			Pro.Summary AS [Name], 
			Pro.Id As [ProjectId],
			'project' AS [Type],
			[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			Pro.CreatedBy,
			'' as ProcessClass,
			'' as UsersThird 
		FROM #DocTbl TempPro
		LEFT JOIN [Task].[AdminCategory] Pro ON TempPro.Id = Pro.Id
		ORDER BY Pro.CreatedDate DESC, Pro.Summary
		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
		FETCH NEXT @PageSize ROWS ONLY;
	END
	ELSE IF @ParentId = ''
	BEGIN
		SELECT 
			Doc.*,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
				'' as ProcessClass,
			'' as UsersThird 
		FROM #DocTbl Doc INNER JOIN Task.AdminCategory p ON p.Id = Doc.Id
		ORDER BY p.CreatedDate DESC, p.Summary
	END
	ELSE
	BEGIN
	if (@IsCount = 1)
		begin
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
			SELECT 
			Doc.*,
			p.IsGroupLabel,
			p.Conclusion as Content,
			(SELECT Count(ti.Id) FROM Task.TaskItem ti
				WHERE ti.ParentId = Doc.Id) As CountTask,
			(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
			(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](1,p.Id,p.ProjectId) as UsersPrimary,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](3,p.Id,p.ProjectId) as UsersSecond,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](7,p.Id,p.ProjectId) as UsersThird
		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		where p.IsDeleted = 0
		ORDER BY p.[Order], p.CreatedDate
		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
		FETCH NEXT @PageSize ROWS ONLY;
		end
	else 
	begin
		SELECT 
			Doc.*,
			p.IsGroupLabel,
			p.Conclusion as Content,
			(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
			(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord],
			@Page as CurrentPage,
			@PageSize as PageSize,
			p.CreatedBy,
				[Task].[Udf_Get_UserIdAssign_By_TaskType](1,p.Id,p.ProjectId) as UsersPrimary,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](3,p.Id,p.ProjectId) as UsersSecond,
			[Task].[Udf_Get_UserIdAssign_By_TaskType](7,p.Id,p.ProjectId) as UsersThird
		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		where p.IsDeleted = 0
		ORDER BY p.[Order], p.CreatedDate
		end
	END
	DROP TABLE #TempOneTItemTbl;
	DROP TABLE #TempTwoTItemTbl;
	DROP TABLE #TempTItemTbl;
	DROP TABLE #TItemTbl;
	DROP TABLE #ProTaskTbl;
	DROP TABLE #ProTbl;
	DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
	-- Whoops, there was an error
	IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
	-- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), 
			@ErrSeverity int;
	SELECT @ErrMsg = ERROR_MESSAGE(),
		   @ErrSeverity = ERROR_SEVERITY();
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH");
                #endregion

            }
            if (newVersion != currentVersion)
            {
                queries.Add(string.Format(@"if (Not Exists(select * from [dbo].[Settings] where Code = 'CurrentTaskVersion'))
  begin insert into [dbo].[Settings] values ('CurrentTaskVersion','CurrentTaskVersion','{0}')
  end
  else
  begin update [dbo].[Settings] set Value = '{0}' where Code = 'CurrentTaskVersion'
  end", newVersion));
            }

            return queries;
        }
    }
}
