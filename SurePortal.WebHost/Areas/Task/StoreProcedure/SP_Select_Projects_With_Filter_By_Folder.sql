USE [SurePortal]
GO

/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter_By_Folder]    Script Date: 1/27/2021 10:38:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-28
-- Description:	Lây danh sách project theo folder
-- =============================================
Alter   PROCEDURE [dbo].[SP_Select_Projects_With_Filter_By_Folder] 
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
									' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate, 
																	  TIAssign.TaskItemStatusId, 
																	  dateadd(DAY, 2, getdate())) = 1
									  AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate, 
																	  TIAssign.TaskItemStatusId, 
																	  getdate()) = 0 ';
		END

		IF (@StatusTime = 2)
		BEGIN
			SET @ProFolSqlTrack = @ProFolSqlTrack +
									' AND dbo.Func_Doc_CheckIsOverDue(TIAssign.ToDate, 
																	  TIAssign.FinishedDate,
																	  TIAssign.TaskItemStatusId, 
																	  getdate()) = 1 ';
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




GO


