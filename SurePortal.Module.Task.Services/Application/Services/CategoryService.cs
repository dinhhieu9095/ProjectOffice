using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SurePortal.Core.Kernel;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Firebase.Models;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Module.Task.Entities;

namespace SurePortal.Module.Task.Services
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
                            string query = item.Replace("GO", "");
                            await _projectCategoryRepository.SqlQueryAsync(typeof(bool), item);
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
            if (currentVersion < 20210204)
            {
                newVersion = 20210204;
                queries.Add(@"ALTER TABLE [Task].[ProjectCategory]
ADD ProjectId uniqueidentifier;");
            }
            if (currentVersion < 20210223)
            {
                newVersion = 20210223;
                queries.Add(@"ALTER TABLE Task.Project ADD
	IsAuto bit NOT NULL CONSTRAINT DF_Project_IsAuto DEFAULT 0,
	IsLinked bit NOT NULL CONSTRAINT DF_Project_IsLinked DEFAULT 0");
                queries.Add(@"ALTER TABLE Task.TaskItem ADD
	IsAuto bit NOT NULL CONSTRAINT DF_TaskItem_IsAuto DEFAULT 0,
	TaskGroupType int NOT NULL CONSTRAINT DF_TaskItem_TaskGroupType DEFAULT 0");
            }

            if (currentVersion < 20210302)
            {
                newVersion = 20210302;
                queries.Add(@"ALTER TABLE Task.TaskItemAssign ADD
	IsDeleted bit NOT NULL CONSTRAINT DF_TaskItemAssign_IsDeleted DEFAULT 0");
            }

            if (currentVersion < 20210312)
            {
                newVersion = 20210312;
                queries.Add(@"ALTER TABLE Task.Report
ADD Permission nvarchar(max) null");
            }
            if (currentVersion < 20210315)
            {
                newVersion = 20210315;
                queries.Add(@"ALTER TABLE Task.TaskItemAssign ADD
	IsExtend bit NOT NULL CONSTRAINT DF_TaskItemAssign_IsExtend DEFAULT 0");
            }
            
            if (currentVersion < 20210318)
            {
                newVersion = 20210318;
                queries.Add(@"
create or Alter PROCEDURE [dbo].[SP_Select_Projects_With_Filter] 
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
	@EndDate		VARCHAR(50)					= ''
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
								OR TIAssign.AssignFollow = ''' + @CurrentUserId + '''))) ';
	else
		SET @ProTaskSqlTrack =
			'INSERT INTO #ProTaskTbl
			SELECT TIAssign.Id, TItem.Id, Pro.Id AS ProjectId 
			FROM [Task].[TaskItemAssign] TIAssign
			FULL OUTER JOIN [Task].[TaskItem] TItem ON TIAssign.TaskItemId = TItem.Id
 			FULL OUTER JOIN [Task].[Project] Pro ON TItem.ProjectId = Pro.Id
			WHERE
				Pro.IsActive = 1 '
	print @ProTaskSqlTrack
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (
								--Pro.Summary like N''%' + @Keyword + '%'' OR 
								TItem.TaskName like N''%' + @Keyword + '%'')';
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
								' AND TIAssign.AssignTo = ''' + @CurrentUserId + ''' ';
		else
			SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND TIAssign.AssignTo = ''' + @AssignTo + ''' ';
		
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
								' AND (TIAssign.FromDate >= ''' + @FromDate + ''' 
										OR TItem.FromDate >= ''' + @FromDate + '''
										OR Pro.FromDate >= ''' + @FromDate + ''')';
	END

	-- ToDate Condition
	IF (@ToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @ToDate + '''
										OR TItem.ToDate <= ''' + @ToDate + '''
										OR Pro.ToDate <= ''' + @ToDate + ''')';
	END

	-- so sánh với fromdate của công việc
	IF (@TaskFromDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate >= ''' + @TaskFromDateOfFromDate + ''' 
										OR TItem.FromDate >= ''' + @TaskFromDateOfFromDate + '''
										OR Pro.FromDate >= ''' + @TaskFromDateOfFromDate + ''')';
	END

	IF (@TaskFromDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR TItem.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR Pro.FromDate <= ''' + @TaskFromDateOfToDate + ''')';
	END
	-- so sánh với todate của công việc
	IF (@TaskToDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate >= ''' + @TaskToDateOfFromDate + ''' 
										OR TItem.ToDate >= ''' + @TaskToDateOfFromDate + '''
										OR Pro.ToDate >= ''' + @TaskToDateOfFromDate + ''')';
	END

	IF (@TaskToDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @TaskToDateOfToDate + '''
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
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	dateadd(DAY, 2, getdate())) = 1
									AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	getdate()) = 0 ';
	END
	-- quá hạn
	IF (@StatusTime = 2)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate,
																	TIAssign.TaskItemStatusId, 
																	getdate()) = 1 ';
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
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
	END

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
	print(@ProTaskSqlTrack);
	EXEC sp_executesql @ProTaskSqlTrack;

	-- Neu lay project
	IF (@ParentId = '')
	BEGIN

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
						' ORDER BY TItem.[Order], TItem.CreatedDate';
		Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;

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
			TempPro.[Status] AS [Status],
				pro.PercentFinish AS [PercentFinish],
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
	ELSE IF @ParentId = ''
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
			Pro.ManagerId as UsersPrimary,
			pro.UserViews as UsersSecond,
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
GO
--{EXEC [dbo].[SP_Select_Projects_With_Filter]  @CurrentUserId = 'f7033d7c-d52d-4eaf-8227-a9d1d185a6b8' , @Keyword = N'', @ParentId = '5f2a606c-bf3c-4425-a0d0-a9ff3a291442'
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create or Alter PROCEDURE Task.SP_Move_Item_In_Table 
	-- Add the parameters for the stored procedure here
	@CurrentUserId		NVARCHAR(200),
	@IdSource NVARCHAR(200),
	@IdDestination NVARCHAR(200),
	@TaskType  NVARCHAR(200) ='TASK',
	@MoveType  NVARCHAR(200) ='MOVE'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Create variable
	Declare @ParentIdS  NVARCHAR(200)
			, @ParentIdD  NVARCHAR(200)
			, @UserAssignByD   NVARCHAR(200)
			, @UserCreatedD   NVARCHAR(200)
			,@Message NVARCHAR(200)='Success'
    -- Insert statements for procedure here
	select @ParentIdS =ParentId from Task.TaskItem where Id=@IdSource
	select @ParentIdD =ParentId, @UserAssignByD =AssignBy,@UserCreatedD=CreatedBy  from Task.TaskItem where Id=@IdDestination
	
	if @ParentIdD is NULL
	begin
		update Task.TaskItem set ParentId ='00000000-0000-0000-0000-000000000000' where Id= @IdSource 
	end
	else
	begin
		if(@UserAssignByD=@CurrentUserId OR @UserCreatedD =@CurrentUserId)	
		begin
			update Task.TaskItem set ParentId =@IdDestination where Id= @IdSource 
			  set @Message='Success';
		end
		else
		begin
			set @Message='Permission';
		end
	end
	select @Message as [Message],@ParentIdD as ParentId
	

END
GO

/****** Object:  UserDefinedFunction [Task].[Udf_Get_Process_Class_Grantt_View_By_Task]    Script Date: 2/25/2021 10:57:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		ntchien
-- Create date: 2021-02-24
-- Description:	Get UserId
-- =============================================
Alter FUNCTION [Task].[Udf_Get_Process_Class_Grantt_View_By_Task] 
(
	-- Add the parameters for the function here
	@ProjectId uniqueidentifier,
	@TaskItemId uniqueidentifier
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultL nvarchar(50),
			@ResultR nvarchar(50),
			@ProjectFromDate datetime, 
	        @ProjectToDate datetime,
			@TaskFromDate datetime ,
	        @TaskToDate datetime,
			@PercentLeftTemp float =0.0,
			@PercentRightTemp float =0.0 ,
			@PercentLeft nvarchar(50),
		    @PercentRight nvarchar(50),
			@TotalDay int;
				
		SELECT  @ProjectFromDate =FromDate, @ProjectToDate= ToDate, @TotalDay =DATEDIFF(day,FromDate,ToDate) from Task.Project where Id=@ProjectId ;
		SELECT  @TaskFromDate =FromDate, @TaskToDate= ToDate from Task.TaskItem where Id=@TaskItemId ;
		--SET @ResultL =10 ;
		if (@TotalDay <> 0 and @TaskFromDate IS NOT NULL and  @TaskToDate IS NOT NULL)
		begin
			set @PercentLeftTemp = (DATEDIFF(day,@ProjectFromDate,@TaskFromDate)*100 /@TotalDay);
			set @PercentRightTemp = (DATEDIFF(day,@TaskToDate,@ProjectToDate) *100/@TotalDay);
			--set @PercentLeftTemp= DATEDIFF(day,@ProjectFromDate,@TaskFromDate) ;
			--set @ResultL=DATEDIFF(day,@ProjectFromDate,@TaskFromDate);
			--set @PercentLeftTemp=  800/191;
		--	set @ResultL=@PercentLeftTemp;
		--set @ResultL=@PercentLeftTemp;
		--set @ResultR=  @PercentRightTemp;
			if @PercentLeftTemp <> 0 and @TotalDay <>0
			begin
				set @ResultL= CASE
							WHEN @PercentLeftTemp >0 and @PercentLeftTemp <=5  THEN  '5'
							WHEN @PercentLeftTemp >5 and @PercentLeftTemp <=10  THEN  '10'
							WHEN @PercentLeftTemp >10 and @PercentLeftTemp <=15  THEN  '15'
							WHEN @PercentLeftTemp >15 and @PercentLeftTemp <=20  THEN  '20'
								WHEN @PercentLeftTemp >20 and @PercentLeftTemp <=25  THEN  '25'
									WHEN @PercentLeftTemp >25 and @PercentLeftTemp <=30  THEN  '30'
										WHEN @PercentLeftTemp >30 and @PercentLeftTemp <=35  THEN  '35'
											WHEN @PercentLeftTemp >35 and @PercentLeftTemp <=40  THEN  '40'
												WHEN @PercentLeftTemp >40 and @PercentLeftTemp <=45  THEN  '45'
													WHEN @PercentLeftTemp >45 and @PercentLeftTemp <=50  THEN  '50'
														WHEN @PercentLeftTemp >50 and @PercentLeftTemp <=55  THEN  '55'
															WHEN @PercentLeftTemp >55 and @PercentLeftTemp <=60  THEN  '60'
																WHEN @PercentLeftTemp >60 and @PercentLeftTemp <=65  THEN  '65'
																WHEN @PercentLeftTemp >65 and @PercentLeftTemp <=70  THEN  '70'
																WHEN @PercentLeftTemp >70 and @PercentLeftTemp <=75  THEN  '75'
																WHEN @PercentLeftTemp >75 and @PercentLeftTemp <=80  THEN  '80'
																WHEN @PercentLeftTemp >80 and @PercentLeftTemp <=85  THEN  '85'
																WHEN @PercentLeftTemp >85 and @PercentLeftTemp <=90  THEN  '90'
																WHEN @PercentLeftTemp >90 and @PercentLeftTemp <=95  THEN  '95'
																WHEN @PercentLeftTemp >95 and @PercentLeftTemp <=100  THEN  '100'

									ELSE '00'
							END
					
			end
		if @PercentRightTemp <> 0 and @TotalDay <>0
			begin
				set @ResultR= CASE
							WHEN @PercentRightTemp >0 and @PercentRightTemp <=5  THEN  '5'
							WHEN @PercentRightTemp >5 and @PercentRightTemp <=10  THEN  '10'
							WHEN @PercentRightTemp >10 and @PercentRightTemp <=15  THEN  '15'
							WHEN @PercentRightTemp >15 and @PercentRightTemp <=20  THEN  '20'
								WHEN @PercentRightTemp >20 and @PercentRightTemp <=25  THEN  '25'
									WHEN @PercentRightTemp >25 and @PercentRightTemp <=30  THEN  '30'
										WHEN @PercentRightTemp >30 and @PercentRightTemp <=35  THEN  '35'
											WHEN @PercentRightTemp >35 and @PercentRightTemp <=40  THEN  '40'
												WHEN @PercentRightTemp >40 and @PercentRightTemp <=45  THEN  '45'
													WHEN @PercentRightTemp >45 and @PercentRightTemp <=50  THEN  '50'
														WHEN @PercentRightTemp >50 and @PercentRightTemp <=55  THEN  '55'
															WHEN @PercentRightTemp >55 and @PercentRightTemp <=60  THEN  '60'
																WHEN @PercentRightTemp >60 and @PercentRightTemp <=65  THEN  '65'
																WHEN @PercentRightTemp >65 and @PercentRightTemp <=70  THEN  '70'
																WHEN @PercentRightTemp >70 and @PercentRightTemp <=75  THEN  '75'
																WHEN @PercentRightTemp >75 and @PercentRightTemp <=80  THEN  '80'
																WHEN @PercentRightTemp >80 and @PercentRightTemp <=85  THEN  '85'
																WHEN @PercentRightTemp >85 and @PercentRightTemp <=90  THEN  '90'
																WHEN @PercentRightTemp >90 and @PercentRightTemp <=95  THEN  '95'
																WHEN @PercentRightTemp >95 and @PercentRightTemp <=100  THEN  '100'

									ELSE '0'
							END
					
					--print @ResultL ;
			
			end
				
		end
	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN 'L'+@ResultL+' '+'R'+@ResultR

END
GO
/****** Object:  StoredProcedure [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]    Script Date: 3/4/2021 5:39:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]
	@parentId NVARCHAR(200)  null
AS
BEGIN
	SET NOCOUNT ON;
	if (@parentId is null) set @parentId='00000000-0000-0000-0000-000000000000' ;
	WITH NodeOrg AS
			(
				SELECT N.Id, N.TaskName,n.ParentId,N.ProjectId, 1 as STT FROM [Task].[TaskItem] AS N WHERE N.Id = @parentId and N.IsDeleted =0 --and ParentId='00000000-0000-0000-0000-000000000000' --and IsGroupLabel =1
				UNION ALL 
				SELECT N.Id, N.TaskName, n.ParentId,N.ProjectId, (T.STT -1) as STT FROM [Task].[TaskItem] AS N 
				INNER JOIN NodeOrg AS T ON T.ParentId = N.Id  WHERE N.IsGroupLabel =1 and N.IsDeleted =0 -- and N.ParentId ='7764ebf5-b182-4049-983c-0ce8fc9208c9'
			)
				SELECT Id,TaskName as Name, ProjectId,STT  from NodeOrg
				union all 
				select Id, Summary as Name, Id as ProjectId, -100 as STT  from Task.Project where Id in (SELECT ProjectId from [Task].[TaskItem] where  Id= @parentId ) or Id =@parentId --'043e7d9f-3627-4048-8676-33ee640a6f59'
END




GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter FUNCTION [Task].[Udf_Get_UserIdAssign_By_TaskType] 
(
	@TypeId int,
	@TaskItemId uniqueidentifier,
		@ProjectId uniqueidentifier
)
RETURNS nvarchar(4000)
AS
BEGIN
	DECLARE @Result nvarchar(4000)
	select @Result =  SUBSTRING(
        ( SELECT ';'+ CAST(t1.AssignTo AS NVARCHAR(225))  AS [text()]
            FROM Task.TaskItemAssign t1
            WHERE t1.TaskType =@TypeId and t1.TaskItemId= @TaskItemId and ProjectId =@ProjectId
            --ORDER BY ST1.SubjectID
            FOR XML PATH ('')) , 2, 4000) 

 return @Result ;
END




GO");
            }
			if (currentVersion < 20210322)
			{
				newVersion = 20210322;
				queries.Add(@"create or ALTER PROCEDURE [dbo].[SP_UPDATE_TASK_RANGE_DATE]
	@ProjectId uniqueidentifier,
	@TaskId uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
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
	select distinct Id from tbParent;
	if(Exists(select * from [Task].[Project] 
	where Id = @ProjectId and IsAuto = 1))
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
	drop table #temp;
	
END
");
				queries.Add(@"insert into [dbo].[Settings]
  values(N'Dữ liệu chọn hiển thị điểm tiến độ công việc',	'Task.DataSourceProcesTaskCombobox',
  '10%:10;25%:25;50%:50;75%:75;100%:100'),
(N'Loại hiện thị tiến độ công việc(input: tự nhập, combobox: chọn)',	'Task.TypeBarProcesTask',	'combobox')");
			}
			if(currentVersion < 20210325)
			{
				newVersion = 20210325;
				queries.Add(@"---USE [SurePortal_DEV]
GO

/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter]    Script Date: 1/27/2021 10:13:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create or Alter PROCEDURE [dbo].[SP_Select_Projects_With_Filter] 
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
	@EndDate		VARCHAR(50)					= ''
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
								OR TIAssign.AssignFollow = ''' + @CurrentUserId + '''))) ';
	else
		SET @ProTaskSqlTrack =
			'INSERT INTO #ProTaskTbl
			SELECT TIAssign.Id, TItem.Id, Pro.Id AS ProjectId 
			FROM [Task].[TaskItemAssign] TIAssign
			FULL OUTER JOIN [Task].[TaskItem] TItem ON TIAssign.TaskItemId = TItem.Id
 			FULL OUTER JOIN [Task].[Project] Pro ON TItem.ProjectId = Pro.Id
			WHERE
				Pro.IsActive = 1 '
	print @ProTaskSqlTrack
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (
								--Pro.Summary like N''%' + @Keyword + '%'' OR 
								TItem.TaskName like N''%' + @Keyword + '%'')';
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
								' AND TIAssign.AssignTo = ''' + @CurrentUserId + ''' ';
		else
			SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND TIAssign.AssignTo = ''' + @AssignTo + ''' ';
		
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
								' AND (TIAssign.FromDate >= ''' + @FromDate + ''' 
										OR TItem.FromDate >= ''' + @FromDate + '''
										OR Pro.FromDate >= ''' + @FromDate + ''')';
	END

	-- ToDate Condition
	IF (@ToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @ToDate + '''
										OR TItem.ToDate <= ''' + @ToDate + '''
										OR Pro.ToDate <= ''' + @ToDate + ''')';
	END

	-- so sánh với fromdate của công việc
	IF (@TaskFromDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate >= ''' + @TaskFromDateOfFromDate + ''' 
										OR TItem.FromDate >= ''' + @TaskFromDateOfFromDate + '''
										OR Pro.FromDate >= ''' + @TaskFromDateOfFromDate + ''')';
	END

	IF (@TaskFromDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR TItem.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR Pro.FromDate <= ''' + @TaskFromDateOfToDate + ''')';
	END
	-- so sánh với todate của công việc
	IF (@TaskToDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate >= ''' + @TaskToDateOfFromDate + ''' 
										OR TItem.ToDate >= ''' + @TaskToDateOfFromDate + '''
										OR Pro.ToDate >= ''' + @TaskToDateOfFromDate + ''')';
	END

	IF (@TaskToDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @TaskToDateOfToDate + '''
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
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	dateadd(DAY, 2, getdate())) = 1
									AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	getdate()) = 0 ';
	END
	-- quá hạn
	IF (@StatusTime = 2)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate,
																	TIAssign.TaskItemStatusId, 
																	getdate()) = 1 ';
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
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
	END

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
	print(@ProTaskSqlTrack);
	EXEC sp_executesql @ProTaskSqlTrack;

	-- Neu lay project
	IF (@ParentId = '')
	BEGIN

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
						' ORDER BY TItem.[Order], TItem.CreatedDate';
		Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;

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
	ELSE IF @ParentId = ''
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
GO
--{EXEC [dbo].[SP_Select_Projects_With_Filter]  @CurrentUserId = 'f7033d7c-d52d-4eaf-8227-a9d1d185a6b8' , @Keyword = N'', @ParentId = '5f2a606c-bf3c-4425-a0d0-a9ff3a291442'
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create or Alter PROCEDURE Task.SP_Move_Item_In_Table 
	-- Add the parameters for the stored procedure here
	@CurrentUserId		NVARCHAR(200),
	@IdSource NVARCHAR(200),
	@IdDestination NVARCHAR(200),
	@TaskType  NVARCHAR(200) ='TASK',
	@MoveType  NVARCHAR(200) ='MOVE'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Create variable
	Declare @ParentIdS  NVARCHAR(200)
			, @ParentIdD  NVARCHAR(200)
			, @UserAssignByD   NVARCHAR(200)
			, @UserCreatedD   NVARCHAR(200)
			,@Message NVARCHAR(200)='Success'
    -- Insert statements for procedure here
	select @ParentIdS =ParentId from Task.TaskItem where Id=@IdSource
	select @ParentIdD =ParentId, @UserAssignByD =AssignBy,@UserCreatedD=CreatedBy  from Task.TaskItem where Id=@IdDestination
	
	if @ParentIdD is NULL
	begin
		update Task.TaskItem set ParentId ='00000000-0000-0000-0000-000000000000' where Id= @IdSource 
	end
	else
	begin
		if(@UserAssignByD=@CurrentUserId OR @UserCreatedD =@CurrentUserId)	
		begin
			update Task.TaskItem set ParentId =@IdDestination where Id= @IdSource 
			  set @Message='Success';
		end
		else
		begin
			set @Message='Permission';
		end
	end
	select @Message as [Message],@ParentIdD as ParentId
	

END
GO

/****** Object:  UserDefinedFunction [Task].[Udf_Get_Process_Class_Grantt_View_By_Task]    Script Date: 2/25/2021 10:57:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		ntchien
-- Create date: 2021-02-24
-- Description:	Get UserId
-- =============================================
Alter FUNCTION [Task].[Udf_Get_Process_Class_Grantt_View_By_Task] 
(
	-- Add the parameters for the function here
	@ProjectId uniqueidentifier,
	@TaskItemId uniqueidentifier
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultL nvarchar(50),
			@ResultR nvarchar(50),
			@ProjectFromDate datetime, 
	        @ProjectToDate datetime,
			@TaskFromDate datetime ,
	        @TaskToDate datetime,
			@PercentLeftTemp float =0.0,
			@PercentRightTemp float =0.0 ,
			@PercentLeft nvarchar(50),
		    @PercentRight nvarchar(50),
			@TotalDay int;
				
		SELECT  @ProjectFromDate =FromDate, @ProjectToDate= ToDate, @TotalDay =DATEDIFF(day,FromDate,ToDate) from Task.Project where Id=@ProjectId ;
		SELECT  @TaskFromDate =FromDate, @TaskToDate= ToDate from Task.TaskItem where Id=@TaskItemId ;
		--SET @ResultL =10 ;
		if (@TotalDay <> 0 and @TaskFromDate IS NOT NULL and  @TaskToDate IS NOT NULL)
		begin
			set @PercentLeftTemp = (DATEDIFF(day,@ProjectFromDate,@TaskFromDate)*100 /@TotalDay);
			set @PercentRightTemp = (DATEDIFF(day,@TaskToDate,@ProjectToDate) *100/@TotalDay);
			--set @PercentLeftTemp= DATEDIFF(day,@ProjectFromDate,@TaskFromDate) ;
			--set @ResultL=DATEDIFF(day,@ProjectFromDate,@TaskFromDate);
			--set @PercentLeftTemp=  800/191;
		--	set @ResultL=@PercentLeftTemp;
		--set @ResultL=@PercentLeftTemp;
		--set @ResultR=  @PercentRightTemp;
			if @PercentLeftTemp <> 0 and @TotalDay <>0
			begin
				set @ResultL= CASE
							WHEN @PercentLeftTemp >0 and @PercentLeftTemp <=5  THEN  '5'
							WHEN @PercentLeftTemp >5 and @PercentLeftTemp <=10  THEN  '10'
							WHEN @PercentLeftTemp >10 and @PercentLeftTemp <=15  THEN  '15'
							WHEN @PercentLeftTemp >15 and @PercentLeftTemp <=20  THEN  '20'
								WHEN @PercentLeftTemp >20 and @PercentLeftTemp <=25  THEN  '25'
									WHEN @PercentLeftTemp >25 and @PercentLeftTemp <=30  THEN  '30'
										WHEN @PercentLeftTemp >30 and @PercentLeftTemp <=35  THEN  '35'
											WHEN @PercentLeftTemp >35 and @PercentLeftTemp <=40  THEN  '40'
												WHEN @PercentLeftTemp >40 and @PercentLeftTemp <=45  THEN  '45'
													WHEN @PercentLeftTemp >45 and @PercentLeftTemp <=50  THEN  '50'
														WHEN @PercentLeftTemp >50 and @PercentLeftTemp <=55  THEN  '55'
															WHEN @PercentLeftTemp >55 and @PercentLeftTemp <=60  THEN  '60'
																WHEN @PercentLeftTemp >60 and @PercentLeftTemp <=65  THEN  '65'
																WHEN @PercentLeftTemp >65 and @PercentLeftTemp <=70  THEN  '70'
																WHEN @PercentLeftTemp >70 and @PercentLeftTemp <=75  THEN  '75'
																WHEN @PercentLeftTemp >75 and @PercentLeftTemp <=80  THEN  '80'
																WHEN @PercentLeftTemp >80 and @PercentLeftTemp <=85  THEN  '85'
																WHEN @PercentLeftTemp >85 and @PercentLeftTemp <=90  THEN  '90'
																WHEN @PercentLeftTemp >90 and @PercentLeftTemp <=95  THEN  '95'
																WHEN @PercentLeftTemp >95 and @PercentLeftTemp <=100  THEN  '100'

									ELSE '00'
							END
					
			end
		if @PercentRightTemp <> 0 and @TotalDay <>0
			begin
				set @ResultR= CASE
							WHEN @PercentRightTemp >0 and @PercentRightTemp <=5  THEN  '5'
							WHEN @PercentRightTemp >5 and @PercentRightTemp <=10  THEN  '10'
							WHEN @PercentRightTemp >10 and @PercentRightTemp <=15  THEN  '15'
							WHEN @PercentRightTemp >15 and @PercentRightTemp <=20  THEN  '20'
								WHEN @PercentRightTemp >20 and @PercentRightTemp <=25  THEN  '25'
									WHEN @PercentRightTemp >25 and @PercentRightTemp <=30  THEN  '30'
										WHEN @PercentRightTemp >30 and @PercentRightTemp <=35  THEN  '35'
											WHEN @PercentRightTemp >35 and @PercentRightTemp <=40  THEN  '40'
												WHEN @PercentRightTemp >40 and @PercentRightTemp <=45  THEN  '45'
													WHEN @PercentRightTemp >45 and @PercentRightTemp <=50  THEN  '50'
														WHEN @PercentRightTemp >50 and @PercentRightTemp <=55  THEN  '55'
															WHEN @PercentRightTemp >55 and @PercentRightTemp <=60  THEN  '60'
																WHEN @PercentRightTemp >60 and @PercentRightTemp <=65  THEN  '65'
																WHEN @PercentRightTemp >65 and @PercentRightTemp <=70  THEN  '70'
																WHEN @PercentRightTemp >70 and @PercentRightTemp <=75  THEN  '75'
																WHEN @PercentRightTemp >75 and @PercentRightTemp <=80  THEN  '80'
																WHEN @PercentRightTemp >80 and @PercentRightTemp <=85  THEN  '85'
																WHEN @PercentRightTemp >85 and @PercentRightTemp <=90  THEN  '90'
																WHEN @PercentRightTemp >90 and @PercentRightTemp <=95  THEN  '95'
																WHEN @PercentRightTemp >95 and @PercentRightTemp <=100  THEN  '100'

									ELSE '0'
							END
					
					--print @ResultL ;
			
			end
				
		end
	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN 'L'+@ResultL+' '+'R'+@ResultR

END
GO
/****** Object:  StoredProcedure [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]    Script Date: 3/4/2021 5:39:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create or Alter PROCEDURE [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]
	@parentId NVARCHAR(200)  null
AS
BEGIN
	SET NOCOUNT ON;
	if (@parentId is null) set @parentId='00000000-0000-0000-0000-000000000000' ;
	WITH NodeOrg AS
			(
				SELECT N.Id, N.TaskName,n.ParentId,N.ProjectId, 1 as STT FROM [Task].[TaskItem] AS N WHERE N.Id = @parentId and N.IsDeleted =0 --and ParentId='00000000-0000-0000-0000-000000000000' --and IsGroupLabel =1
				UNION ALL 
				SELECT N.Id, N.TaskName, n.ParentId,N.ProjectId, (T.STT -1) as STT FROM [Task].[TaskItem] AS N 
				INNER JOIN NodeOrg AS T ON T.ParentId = N.Id  WHERE N.IsGroupLabel =1 and N.IsDeleted =0 -- and N.ParentId ='7764ebf5-b182-4049-983c-0ce8fc9208c9'
			)
				SELECT Id,TaskName as Name, ProjectId,STT  from NodeOrg
				union all 
				select Id, Summary as Name, Id as ProjectId, -100 as STT  from Task.Project where Id in (SELECT ProjectId from [Task].[TaskItem] where  Id= @parentId ) or Id =@parentId --'043e7d9f-3627-4048-8676-33ee640a6f59'
END




GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter FUNCTION [Task].[Udf_Get_UserIdAssign_By_TaskType] 
(
	@TypeId int,
	@TaskItemId uniqueidentifier,
		@ProjectId uniqueidentifier
)
RETURNS nvarchar(4000)
AS
BEGIN
	DECLARE @Result nvarchar(4000)
	select @Result =  SUBSTRING(
        ( SELECT ';'+ CAST(t1.AssignTo AS NVARCHAR(225))  AS [text()]
            FROM Task.TaskItemAssign t1
            WHERE t1.TaskType =@TypeId and t1.TaskItemId= @TaskItemId and ProjectId =@ProjectId and t1.IsDeleted=0
            --ORDER BY ST1.SubjectID
            FOR XML PATH ('')) , 2, 4000) 

 return @Result ;
END




GO");
				queries.Add(@"
create or ALTER PROCEDURE [dbo].[SP_UPDATE_TASK_RANGE_DATE]
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
	where Id = @ProjectId and IsAuto = 1))
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
	
END
");
			}
			if(currentVersion < 20210402)
            {
				newVersion = 20210402;
				queries.Add(@"
create or ALTER PROCEDURE [dbo].[SP_Select_Projects_With_Filter] 
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
	@ProjectId nvarchar(50) = ''
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
	print @ProTaskSqlTrack
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (
								--Pro.Summary like N''%' + @Keyword + '%'' OR 
								TItem.TaskName like N''%' + @Keyword + '%'')';
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
								' AND (TIAssign.FromDate >= ''' + @FromDate + ''' 
										OR TItem.FromDate >= ''' + @FromDate + '''
										OR Pro.FromDate >= ''' + @FromDate + ''')';
	END

	-- ToDate Condition
	IF (@ToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @ToDate + '''
										OR TItem.ToDate <= ''' + @ToDate + '''
										OR Pro.ToDate <= ''' + @ToDate + ''')';
	END

	-- so sánh với fromdate của công việc
	IF (@TaskFromDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate >= ''' + @TaskFromDateOfFromDate + ''' 
										OR TItem.FromDate >= ''' + @TaskFromDateOfFromDate + '''
										OR Pro.FromDate >= ''' + @TaskFromDateOfFromDate + ''')';
	END

	IF (@TaskFromDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR TItem.FromDate <= ''' + @TaskFromDateOfToDate + '''
										OR Pro.FromDate <= ''' + @TaskFromDateOfToDate + ''')';
	END
	-- so sánh với todate của công việc
	IF (@TaskToDateOfFromDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate >= ''' + @TaskToDateOfFromDate + ''' 
										OR TItem.ToDate >= ''' + @TaskToDateOfFromDate + '''
										OR Pro.ToDate >= ''' + @TaskToDateOfFromDate + ''')';
	END

	IF (@TaskToDateOfToDate != '')
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TIAssign.ToDate <= ''' + @TaskToDateOfToDate + '''
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
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	dateadd(DAY, 2, getdate())) = 1
									AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate, 
																	TIAssign.TaskItemStatusId, 
																	getdate()) = 0 
									 AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
	END
	-- quá hạn
	IF (@StatusTime = 2)
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	TIAssign.FinishedDate,
																	TIAssign.TaskItemStatusId, 
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
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
	END

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
	print(@ProTaskSqlTrack);
	EXEC sp_executesql @ProTaskSqlTrack;

	-- Neu lay project
	IF (@ParentId = '')
	BEGIN

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
						' ORDER BY TItem.[Order], TItem.CreatedDate';
		Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;

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
	ELSE IF @ParentId = ''
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

				queries.Add(@"ALTER PROCEDURE [dbo].[SP_Select_Projects_MultiFilters]
--DECLARE
	@CurrentDate	DATETIME		= '',					--getdate(),
	@CurrentUser	NVARCHAR(200)	= '',					-- .Func_GetUserIDByLoginName('benthanh\nguyenminhphuong'), -- '',
	@TrackStatus	NVARCHAR(20)	= '',					--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
	@DocStatus		NVARCHAR(20)	= '',					-- mã tình trạng văn bản
	@ProcessType	INT				= 0,					--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	@TrackType		NVARCHAR(20)	= '',					--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	@DocType		NVARCHAR(20)	= '1',					-- 0: Vb đi, 1: vb đến, 2: cong viec, 3: cong viec dinh ky
	@ExternalType	INT				= 0,					-- 0: tat ca, 1: ben ngoai, 2: noi bo
	@UserType		INT				= 0,					--/ 0: assignto, 1: approved, 2: assignedby
	@UserDelegate   NVARCHAR(200)   = NULL,					-- lấy dữ liệu theo người ủy quyền
	@PrivateFolder	NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo folder cá nhân
	@Category		NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo danh mục phân loại
	@TimeFilter		NVARCHAR(200)	= NULL,					-- filter thời gian theo ngày tạo văn bản   				
    @KeyWord		NVARCHAR(200)	= '',
	@Page			INT				= 1,
	@PageSize		INT				= 10,
    @IsCount		BIT				= 0,
	@OrderBy		NVARCHAR(50)    = 'CreatedDate DESC',
	@FromDate		NVARCHAR(10)	= '',
	@ToDate		NVARCHAR(10)	= '',
	@TaskFromDateOfFromDate			VARCHAR(50)			= '',
	@TaskFromDateOfToDate				VARCHAR(50)			= '',
	@TaskToDateOfFromDate			VARCHAR(50)			= '',
	@TaskToDateOfToDate				VARCHAR(50)			= '',
	@DepartmentId	NVARCHAR(36)	= '',
	@UserId		NVARCHAR(36)	= '',
	@UserHandoverId NVARCHAR(36) = '',
	@StatusTime int = 0,
	@TaskItemStatusId nvarchar(20) = '',
	@AssignTo nvarchar(36) = '',
	@AssignBy nvarchar (36) ='',
	@IsFullControl int = 0,
	@TaskItemPriorityId VARCHAR(20)			= null,
	@TaskItemNatureId nvarchar(20)			= null,
	@ProjectHashtag	NVARCHAR(255)		= '',
	@TaskHashtag	NVARCHAR(255)		= '',
	@IsReport			VARCHAR(10)			= '',
	@IsWeirdo			VARCHAR(10)			= '',
	@TaskType			VARCHAR(10)			= '',
	@IsExtend		VARCHAR(50)					= '',
	@ProjectId nvarchar(50) = ''
AS

--SELECt @CurrentDate = '2019-12-16 08:17', @KeyWord = N'',
--@CurrentUser = dbo.Func_GetUserIDByLoginName('dxg\thaidt'),
--@OrderBy = 'CreatedDate DESC', @IsCount = '0', @Page = '1', 
--@PageSize = '15' ,@TrackStatus = '6' ,@DocType = '2', @UserType = '2'
BEGIN TRY
	 DECLARE @sql nvarchar(max) = ''
	 -- Lay danh project ban dau
	if @IsFullControl = 1
		Set @sql = 'Select Distinct TItem.Id from task.TaskItemAssign TIAssign 
					FULL OUTER JOIN Task.TaskItem TItem on TItem.Id = TIAssign.TaskItemId
					FULL OUTER JOIN Task.Project Pro on Pro.Id = TItem.ProjectId
					where Pro.IsActive = 1  AND TItem.IsDeleted = 0 AND (TItem.IsGroupLabel <> 1 OR TItem.IsGroupLabel IS NULL) '
	else
		Set @sql = 'Select Distinct TItem.Id from task.TaskItemAssign TIAssign 
				FULL OUTER JOIN Task.TaskItem TItem on TItem.Id = TIAssign.TaskItemId
				FULL OUTER JOIN Task.Project Pro on Pro.Id = TItem.ProjectId
				where Pro.IsActive = 1 AND TItem.IsDeleted = 0 AND (TItem.IsGroupLabel <> 1 OR TItem.IsGroupLabel IS NULL)
				AND (Pro.ApprovedBy = ''' + @CurrentUser + ''' 
					OR Pro.CreatedBy = ''' + @CurrentUser + '''
					OR Pro.UserViews like N''%' + @CurrentUser + '%''
					OR Pro.ManagerId like N''%' + @CurrentUser + '%''
					OR TItem.AssignBy = ''' + @CurrentUser + '''
					OR (TIAssign.TaskItemId IS NOT NULL 
						AND TIAssign.TaskItemId != ''00000000-0000-0000-0000-000000000000''
						AND (TIAssign.AssignTo = ''' + @CurrentUser + '''
								OR TIAssign.AssignFollow = ''' + @CurrentUser + '''))) '
	
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @sql = @sql +
								' AND (Pro.Summary like N''%' + @Keyword + '%''
										OR TItem.TaskName like N''%' + @Keyword + '%'')';
	END
	 
	-- Keyword Condition
	IF (@ProjectId != '') 
	BEGIN
		SET @sql = @sql + ' AND Pro.Id = ''' + @ProjectId + '''';
	END

	-- AssignBy Condition
	IF (@AssignBy != '') 
	BEGIN
		if @AssignBy = 'CurentUser'
			SET @sql = @sql + ' AND TItem.AssignBy = ''' + @CurrentUser + ''' ';
		else
			SET @sql = @sql + ' AND TItem.AssignBy = ''' + @AssignBy + ''' ';
	END
	
	-- TaskItemStatusId Condition
	IF (@TaskItemStatusId != '')
	BEGIN
		SET @sql = @sql + ' AND  TItem.TaskItemStatusId In (' + @TaskItemStatusId + ') AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
	END
	
	-- AssignTo Condition
	IF (@AssignTo != '')
	BEGIN
		if @AssignTo = 'CurentUser'
			SET @sql = @sql + ' AND TIAssign.AssignTo = ''' + @CurrentUser + ''' ';
		else
			SET @sql = @sql + ' AND TIAssign.AssignTo = ''' + @AssignTo + ''' ';
		
	END

	-- TaskType Condition
	IF (@TaskType != '')
	BEGIN
		SET @sql = @sql + ' AND TIAssign.TaskType in (' + @TaskType + ') ';
	END
	print '#12312'
	-- mặc định
	IF (@StatusTime = 0)
	BEGIN
		IF (@TaskFromDateOfFromDate != '')
		BEGIN
			declare @fromDateOfFromDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskFromDateOfFromDate, 103)))
			SET @sql = @sql + ' AND (TIAssign.FromDate >= ''' + @fromDateOfFromDate + ''' 
											OR TItem.FromDate >= ''' + @fromDateOfFromDate + '''
											OR Pro.FromDate >= ''' + @fromDateOfFromDate + ''')';
		END
		print '#12312'
		IF (@TaskFromDateOfToDate != '')
		BEGIN
			declare @fromDateOfToDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskFromDateOfToDate, 103)))
			SET @sql = @sql + ' AND (TIAssign.FromDate <= ''' + @fromDateOfToDate + '''
											OR TItem.FromDate <= ''' + @fromDateOfToDate + '''
											OR Pro.FromDate <= ''' + @fromDateOfToDate + ''')';
		END
		print '#12312'
		-- so sánh với todate của công việc
		IF (@TaskToDateOfFromDate != '')
		BEGIN
		declare @toDateOfFromDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskToDateOfFromDate, 103)))
			SET @sql = @sql + ' AND (TIAssign.ToDate >= ''' + @toDateOfFromDate + ''' 
											OR TItem.ToDate >= ''' + @toDateOfFromDate + '''
											OR Pro.ToDate >= ''' + @toDateOfFromDate + ''')';
		END
		print '#12312'
		IF (@TaskToDateOfToDate != '')
		BEGIN
			declare @toDateOfToDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskToDateOfToDate, 103)))
			SET @sql = @sql + ' AND (TIAssign.ToDate <= ''' + @toDateOfToDate + '''
											OR TItem.ToDate <= ''' + @toDateOfToDate + '''
											OR Pro.ToDate <= ''' + @toDateOfToDate + ''')';

		END
		print '#12312'
		-- FromDate Condition
		IF (@FromDate != '')
		BEGIN
			DECLARE @fDate nvarchar(255) = (SELECT convert(datetime, @FromDate, 103))
			SET @sql = @sql + ' AND (TIAssign.FromDate >= ''' + @fDate + ''' 
											OR TItem.FromDate >= ''' + @fDate + '''
											OR Pro.FromDate >= ''' + @fDate + ''')';
		END

		-- ToDate Condition
		IF (@ToDate != '')
		BEGIN
		DECLARE @tDate nvarchar(255) = (SELECT convert(datetime, @ToDate, 103))
			SET @sql = @sql + ' AND (TIAssign.ToDate <= ''' + @tDate + '''
											OR TItem.ToDate <= ''' + @tDate + '''
											OR Pro.ToDate <= ''' + @tDate + ''')';
		END
	END
	ELSE
	BEGIN
	-- sắp hết hạn
		IF (@StatusTime = 1)
		BEGIN
			SET @sql = @sql + ' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate, 
																	  TIAssign.TaskItemStatusId, 
																	  dateadd(DAY, 2, getdate())) = 1
									  AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate, 
																	  TIAssign.TaskItemStatusId, 
																	  getdate()) = 0 ';
		END
		-- quá hạn
		IF (@StatusTime = 2)
		BEGIN
			SET @sql = @sql + ' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate,
																	  TIAssign.TaskItemStatusId, 
																	  getdate()) = 1 ';
		END
	END

	-- gia hạn
	IF (@IsExtend = '1')
	BEGIN
		SET @sql = @sql + ' AND (TIAssign.IsExtend = 1) '
								 
	END

	-- Priority
	IF (@TaskItemPriorityId IS NOT NULL)
	BEGIN
		SET @sql = @sql + ' AND (TItem.TaskItemPriorityId In (' + @TaskItemPriorityId + '))';
	END

	--natureTask
	IF @TaskItemNatureId IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND (TItem.NatureTaskId In (' + @TaskItemNatureId + '))';
	END


	-- Hashtag
	-- khánh nói
	IF (@ProjectHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @sql = @sql + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @sql = @sql + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
	END

	-- IsReport
	IF (@IsReport != '')
	BEGIN
		SET @sql = @sql + ' AND (TItem.IsReport = ' + @IsReport + ')';
	END

	IF (@IsWeirdo != '')
	BEGIN
		SET @sql = @sql + ' AND (TItem.IsReport = ' + @IsWeirdo + ')';
	END

	set @sql = 'Select Count(*) As TotalRow from (' + @sql + ') As X'
	print @sql
	EXEC sys.sp_executesql @Sql;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH");

				queries.Add(@"IF COL_LENGTH('task.report','IsUser') IS NULL
BEGIN
alter table task.report add IsUser bit NOT NULL
CONSTRAINT IsUser DEFAULT 0;
END");
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
