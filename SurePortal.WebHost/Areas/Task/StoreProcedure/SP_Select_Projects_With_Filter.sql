---USE [SurePortal_DEV]
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




GO