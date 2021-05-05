USE [master]
GO
/****** Object:  Database [SurePortal_DEV]    Script Date: 04-05-2021 7:51:19 PM ******/
CREATE DATABASE [SurePortal_DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SurePortal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SurePortal_DEV.mdf' , SIZE = 478208KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ), 
 FILEGROUP [FILESTREAM] 
( NAME = N'SurePortal_Filestream', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SurePortal_DEV_Filestream' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ), 
 FILEGROUP [FULLTEXTINDEX] 
( NAME = N'SurePortal_FullTextIndex', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SurePortal_DEV_FulltextIndex' , SIZE = 6464KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SurePortal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SurePortal_DEV_log.ldf' , SIZE = 3220480KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SurePortal_DEV] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SurePortal_DEV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SurePortal_DEV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET ARITHABORT OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SurePortal_DEV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SurePortal_DEV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SurePortal_DEV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SurePortal_DEV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET RECOVERY FULL 
GO
ALTER DATABASE [SurePortal_DEV] SET  MULTI_USER 
GO
ALTER DATABASE [SurePortal_DEV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SurePortal_DEV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SurePortal_DEV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SurePortal_DEV] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SurePortal_DEV] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SurePortal_DEV]
GO
/****** Object:  User [lacviet]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE USER [lacviet] FOR LOGIN [lacviet] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [Authenticate]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Authenticate]
GO
/****** Object:  Schema [BusinessWorkflow]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [BusinessWorkflow]
GO
/****** Object:  Schema [Chat]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Chat]
GO
/****** Object:  Schema [Contract]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Contract]
GO
/****** Object:  Schema [Core]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [nav]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [nav]
GO
/****** Object:  Schema [SmartOTP]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [SmartOTP]
GO
/****** Object:  Schema [Task]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Task]
GO
/****** Object:  Schema [Workspace]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE SCHEMA [Workspace]
GO
/****** Object:  FullTextCatalog [BW_Document]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE FULLTEXT CATALOG [BW_Document]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  FullTextCatalog [Customer_Catalog]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE FULLTEXT CATALOG [Customer_Catalog]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  FullTextCatalog [Document_Catalog]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE FULLTEXT CATALOG [Document_Catalog]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  FullTextCatalog [Task_Fulltext]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE FULLTEXT CATALOG [Task_Fulltext]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  FullTextCatalog [User_Catalog]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE FULLTEXT CATALOG [User_Catalog]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  UserDefinedTableType [dbo].[DeptIDs]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE TYPE [dbo].[DeptIDs] AS TABLE(
	[DeptID] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[DecodeUTF8String]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
                            CREATE FUNCTION [dbo].[DecodeUTF8String] (@value varchar(max))
                            RETURNS nvarchar(max)
                            AS
                            BEGIN
                                -- Transforms a UTF-8 encoded varchar string into Unicode
                                -- By Anthony Faull 2014-07-31
                                DECLARE @result nvarchar(max);

                                -- If ASCII or null there's no work to do
                                IF (@value IS NULL
                                    OR @value NOT LIKE '%[^ -~]%' COLLATE Latin1_General_BIN
                                )
                                    RETURN @value;

                                -- Generate all integers from 1 to the length of string
                                WITH e0(n) AS (SELECT TOP(POWER(2,POWER(2,0))) NULL FROM (VALUES (NULL),(NULL)) e(n))
                                    , e1(n) AS (SELECT TOP(POWER(2,POWER(2,1))) NULL FROM e0 CROSS JOIN e0 e)
                                    , e2(n) AS (SELECT TOP(POWER(2,POWER(2,2))) NULL FROM e1 CROSS JOIN e1 e)
                                    , e3(n) AS (SELECT TOP(POWER(2,POWER(2,3))) NULL FROM e2 CROSS JOIN e2 e)
                                    , e4(n) AS (SELECT TOP(POWER(2,POWER(2,4))) NULL FROM e3 CROSS JOIN e3 e)
                                    , e5(n) AS (SELECT TOP(POWER(2.,POWER(2,5)-1)-1) NULL FROM e4 CROSS JOIN e4 e)
                                , numbers(position) AS
                                (
                                    SELECT TOP(DATALENGTH(@value)) ROW_NUMBER() OVER (ORDER BY (SELECT NULL))
                                    FROM e5
                                ) 
                                -- For each octet, count the high-order one bits, and extract the data bits.
                                , octets AS
                                (
                                    SELECT position, highorderones, partialcodepoint
                                    FROM numbers a
                                    -- Split UTF8 string into rows of one octet each.
                                    CROSS APPLY (SELECT octet = ASCII(SUBSTRING(@value, position, 1))) b
                                    -- Count the number of leading one bits
                                    CROSS APPLY (SELECT highorderones = 8 - FLOOR(LOG( ~CONVERT(tinyint, octet) * 2 + 1)/LOG(2))) c
                                    CROSS APPLY (SELECT databits = 7 - highorderones) d
                                    CROSS APPLY (SELECT partialcodepoint = octet % POWER(2, databits)) e
                                )
                                -- Compute the Unicode codepoint for each sequence of 1 to 4 bytes
                                , codepoints AS
                                (
                                    SELECT position, codepoint
                                    FROM
                                    (
                                        -- Get the starting octect for each sequence (i.e. exclude the continuation bytes)
                                        SELECT position, highorderones, partialcodepoint
                                        FROM octets
                                        WHERE highorderones <> 1
                                    ) lead
                                    CROSS APPLY (SELECT sequencelength = CASE WHEN highorderones in (1,2,3,4) THEN highorderones ELSE 1 END) b
                                    CROSS APPLY (SELECT endposition = position + sequencelength - 1) c
                                    CROSS APPLY
                                    (
                                        -- Compute the codepoint of a single UTF-8 sequence
                                        SELECT codepoint = SUM(POWER(2, shiftleft) * partialcodepoint)
                                        FROM octets
                                        CROSS APPLY (SELECT shiftleft = 6 * (endposition - position)) b
                                        WHERE position BETWEEN lead.position AND endposition
                                    ) d
                                )
                                -- Concatenate the codepoints into a Unicode string
                                SELECT @result = CONVERT(xml,
                                    (
                                        SELECT NCHAR(codepoint)
                                        FROM codepoints
                                        ORDER BY position
                                        FOR XML PATH('')
                                    )).value('.', 'nvarchar(max)');

                                RETURN @result;
                            END

GO
/****** Object:  UserDefinedFunction [dbo].[FN_REPLACE_FIRST]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   FUNCTION [dbo].[FN_REPLACE_FIRST](@X NVARCHAR(MAX), @F NVARCHAR(MAX), @R NVARCHAR(MAX)) RETURNS NVARCHAR(MAX) AS BEGIN
RETURN STUFF(@X, CHARINDEX(@F, @X), LEN(@F), @R)
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_BuildKeywordTable]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Func_BuildKeywordTable]
(	
	@KeyWord nvarchar(1000)
)
RETURNS @TempKeyword TABLE (Idx int, Keyword nvarchar(200), OrderRank int) 
AS
BEGIN 
	DECLARE @ResultVar nvarchar(1000) = '', @Idx int = 1 , @Rank int = 1, 
			@Max int = 0, @Word nvarchar(50), @IsComplexWord bit = 0;
	
	DECLARE @TempQuery TABLE  (Idx int, Word nvarchar(50))	
	INSERT INTO @TempQuery
		SELECT * FROM dbo.Func_SplitTextToTable_WithRowNumber(@KeyWord, ' ');
	
	SELECT @Max = Max(Idx) FROM @TempQuery;

	-- thêm toàn bộ vào
	INSERT INTO @TempKeyword
		SELECT 1, '"' +  @KeyWord + '"', @Rank
	SET @Rank = @Rank + 1;
	WHILE (@Idx < @Max AND @Max > 2)
	BEGIN
		SET @ResultVar = '"';
		SELECT @ResultVar = @ResultVar + Word + ' '
		FROM @TempQuery
		WHERE Idx <= @Max - @Idx
		
		SET @ResultVar = RTRIM(@ResultVar) ;

		IF (@Idx > 1)
		BEGIN
			SET @ResultVar =@ResultVar + '" AND "';

			SELECT @ResultVar = @ResultVar + Word + ' '
			FROM @TempQuery
			WHERE Idx > @Max - @Idx

			SET @ResultVar = RTRIM(@ResultVar) + '" '

			INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar, @Rank
			SET @Rank = @Rank  + 1

			SET @ResultVar = '"';
			SELECT @ResultVar = @ResultVar + Word + ' '
			FROM @TempQuery
			WHERE Idx <= @Max - @Idx
		
			SET @ResultVar = RTRIM(@ResultVar) ;

			IF (LEN(@ResultVar) -LEN(REPLACE(@ResultVar, ' ', '')) >= 2)
			BEGIN
				INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar + '"', @Rank
				SET @Rank = @Rank  + 1
			END

			SET @ResultVar =@ResultVar + '" OR "';

			SELECT @ResultVar = @ResultVar + Word + ' '
			FROM @TempQuery
			WHERE Idx > @Max - @Idx

			SET @ResultVar = RTRIM(@ResultVar) + '" '

			INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar, @Rank
			SET @Rank = @Rank  + 1

		END
		ELSE
		BEGIN
			SET @ResultVar = RTRIM(@ResultVar) + '" '

			INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar, @Rank
			SET @Rank = @Rank  + 1
		END

		SET @Idx = @Idx + 1;
	END 

	SET @ResultVar = '';
	SELECT @ResultVar = @ResultVar + Word + ' OR '
		FROM @TempQuery
		WHERE Idx <= ( @Max  - 1)
	SELECT @ResultVar = @ResultVar + Word 
		FROM @TempQuery
		WHERE Idx = @Max


	INSERT INTO @TempKeyword
			SELECT @Rank, @ResultVar, @Rank 

	DECLARE @maxAndRank int = 0, @MinORRank int = 0;
	SELECT @maxAndRank = Max(ORDERRANK) FROM @TempKeyword where Keyword like N'% AND %'
	SELECT @MinORRank = COUNT(ORDERRANK) FROM @TempKeyword where Keyword like N'% OR %'

	update @TempKeyword
	set OrderRank = @maxAndRank  - @MinORRank + 1 + OrderRank
	where Keyword like N'% OR %'

	RETURN;
END


GO
/****** Object:  UserDefinedFunction [dbo].[Func_BuildKeywordTable_V10]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[Func_BuildKeywordTable_V10]
(
	@KeyWord nvarchar(1000) 
)
RETURNS 
	@TempKeyword TABLE (Idx int, Keyword nvarchar(200), OrderRank int) 

BEGIN 
	DECLARE @ResultVar nvarchar(1000) = '', @Idx int = 1 , @Rank int = 1, 
			@Max int = 0, @Word nvarchar(50), @IsComplexWord bit = 0;
	
	DECLARE @TempQuery TABLE  (Idx int, Word nvarchar(50))	
	INSERT INTO @TempQuery
		SELECT * FROM dbo.Func_SplitTextToTable_WithRowNumber(@KeyWord, ' ');
	
	SELECT @Max = Max(Idx) FROM @TempQuery;

	-- thêm toàn bộ vào
	INSERT INTO @TempKeyword
		SELECT 1, '"' +  @KeyWord + '"', @Rank
	SET @Rank = @Rank + 1;
	WHILE (@Idx < @Max AND @Max > 2)
	BEGIN
		SET @ResultVar = '"';
		SELECT @ResultVar = @ResultVar + Word + ' '
		FROM @TempQuery
		WHERE Idx <= @Max - @Idx
		
		SET @ResultVar = RTRIM(@ResultVar) ;

		IF (@Idx > 1)
		BEGIN
			SET @ResultVar =@ResultVar + '" AND "';

			SELECT @ResultVar = @ResultVar + Word + ' '
			FROM @TempQuery
			WHERE Idx > @Max - @Idx

			SET @ResultVar = RTRIM(@ResultVar) + '" '

			INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar, @Rank
			SET @Rank = @Rank  + 1

			SET @ResultVar = '"';
			SELECT @ResultVar = @ResultVar + Word + ' '
			FROM @TempQuery
			WHERE Idx <= @Max - @Idx
		
			SET @ResultVar = RTRIM(@ResultVar) ;

			IF (LEN(@ResultVar) -LEN(REPLACE(@ResultVar, ' ', '')) >= 2)
			BEGIN
				INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar + '"', @Rank
				SET @Rank = @Rank  + 1
			END

			

		END
		ELSE
		BEGIN
			SET @ResultVar = RTRIM(@ResultVar) + '" '

			INSERT INTO @TempKeyword
				SELECT @Rank, @ResultVar, @Rank
			SET @Rank = @Rank  + 1
		END

		SET @Idx = @Idx + 1;
	END 

	DECLARE @maxAndRank int = 0, @MinORRank int = 0;
	SELECT @maxAndRank = Max(ORDERRANK) FROM @TempKeyword where Keyword like N'% AND %'
	SELECT @MinORRank = COUNT(ORDERRANK) FROM @TempKeyword where Keyword like N'% OR %'


	RETURN;

END


GO
/****** Object:  UserDefinedFunction [dbo].[Func_BuildLevelPath]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		tmquan
-- Create date: 2015-05-28
-- Description:	hàm xây dựng đường đánh dấu level
-- =============================================
CREATE FUNCTION [dbo].[Func_BuildLevelPath]
(	
--DECLARE
	@DepthTree int = 8, -- chỉ định độ sâu của cây,
	@ParentLevelPath nvarchar(20), -- buildlevelPath của cha
	@CurrentLevel int , -- thứ tự hiện tại,
	@CurrentDepth int -- độ sâu hiện tại
)
RETURNS nvarchar(20)
AS
BEGIN
	DECLARE @Result nvarchar(20) = '', @LenghtPath int = @DepthTree * 2;

	SET @ParentLevelPath = LEFT(@ParentLevelPath, @CurrentDepth * 2);
	IF (@CurrentLevel <= 9)
	BEGIN
		SET @Result = @ParentLevelPath + '0' + CAST(@CurrentLevel AS nvarchar);
	END
	ELSE
	BEGIN
		SET @Result = @ParentLevelPath + CAST(@CurrentLevel AS nvarchar);
	END

	SET @Result = @Result + SPACE(@LenghtPath - LEN(@Result));
	SET @Result = REPLACE(@Result, ' ', '0');
	RETURN @Result
END




GO
/****** Object:  UserDefinedFunction [dbo].[Func_CalculateKPI_ByType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_CalculateKPI_ByType]
(
	@TrackingID uniqueidentifier,
	@ColumnType nvarchar(200)
)
RETURNS varchar(10)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar varchar(10) = '0', @TrackingWorkFlowID uniqueidentifier = NULL;

	SELECT TOP 1
		@TrackingWorkFlowID = TWF.ID
	FROM TrackingWorkflowDocuments TWF
	WHERE TWF.TrackingDocumentID = @TrackingID
	ORDER BY TWF.Created DESC;

	IF (@ColumnType = 'AssignToAccomplishment')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignToAccomplishment
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC --, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AssignToAchievement')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignToAchievement
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
	--	AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AssignByAccomplishment')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignByAccomplishment
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC --, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AssignByAchievement')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignByAchievement
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AdjustmentFactor')
	BEGIN
		SELECT TOP 1
			@ResultVar = AdjustmentFactor
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AdjustmentDensity')
	BEGIN
		SELECT TOP 1
			@ResultVar = AdjustmentDensity
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AssignToAverage')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignToAverage
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AssignByAverage')
	BEGIN
		SELECT TOP 1
			@ResultVar = AssignByAverage
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'AdjustmentAverage')
	BEGIN
		SELECT TOP 1
			@ResultVar = AdjustmentAverage
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END
	ELSE
	IF (@ColumnType = 'DensityAverage')
	BEGIN
		SELECT TOP 1
			@ResultVar = DensityAverage
		FROM TrackingKpi
		WHERE TrackingDocumentId = @TrackingID
		--AND TrackingWorkflowDocumentId = @TrackingWorkFlowID
		ORDER BY AssignByModified DESC--, AssignByModified DESC;
	END

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_CheckChildrenOfProject]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-14
-- Description:	Kiem tra Children cua project
-- SELECT [dbo].[Func_CheckChildrenOfProject]('48937DDA-AE63-415A-9F8C-EC928CFC2CEE')
-- =============================================
CREATE FUNCTION [dbo].[Func_CheckChildrenOfProject] 
(
	-- Add the parameters for the function here
	@ProjectId NVARCHAR(200)
)
RETURNS BIT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIT;

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = (SELECT CASE 
								WHEN EXISTS (SELECT ProjectId FROM [Task].[TaskItem] WHERE ProjectId = @ProjectId  and IsDeleted = 0) 
								THEN 1 
								ELSE 0 
							END);

	-- Return the result of the function
	RETURN @Result;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_CheckChildrenOfTaskItem]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-14
-- Description:	Lấy số lượng record cha cua taskitem 
-- SELECT [dbo].[Func_GetCountParentTaskItem]('48937DDA-AE63-415A-9F8C-EC928CFC2CEE')
-- =============================================
CREATE FUNCTION [dbo].[Func_CheckChildrenOfTaskItem]
(
	-- Add the parameters for the function here
	@TaskItemId NVARCHAR(200)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Count INT = 0;
	DECLARE @Result BIT = 0;

	WITH tbParent AS 
	(
		SELECT *  
		FROM [Task].[TaskItem]  
		WHERE Id = @TaskItemId and IsDeleted = 0

		UNION ALL 

		SELECT TItem.*  
		FROM [Task].[TaskItem] TItem   
		JOIN tbParent   
		ON TItem.ParentId = tbParent.Id 
		where TItem.IsDeleted = 0
	)

	SELECT @Count = (SELECT COUNT(*) FROM  tbParent);
	
	IF @Count > 1
	BEGIN
		SET @Result = 1
	END

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_ConvertNoSign]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		tmquan
-- Create date: 15/11/2012
-- Description:	Ham chuyen doi tieng viet tu co dau thanh khong dau
-- =============================================
CREATE FUNCTION [dbo].[Func_ConvertNoSign]
(
	@Text nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN	
	DECLARE @Result nvarchar(max)
	SET @Result = @Text;
	-- Unicode - TCVN 6909
    SET @Result = REPLACE(@Result,N'ắ', N'a');
    SET @Result = REPLACE(@Result,N'ẳ', N'a');
    SET @Result = REPLACE(@Result,N'ẵ', N'a');
    SET @Result = REPLACE(@Result,N'ằ', N'a');
    SET @Result = REPLACE(@Result,N'ặ', N'a');
    SET @Result = REPLACE(@Result,N'ă', N'a');

    SET @Result = REPLACE(@Result,N'ấ', N'a');
    SET @Result = REPLACE(@Result,N'ẩ', N'a');
    SET @Result = REPLACE(@Result,N'ẫ', N'a');
    SET @Result = REPLACE(@Result,N'ầ', N'a');
    SET @Result = REPLACE(@Result,N'ậ', N'a');
    SET @Result = REPLACE(@Result,N'â', N'a');

    SET @Result = REPLACE(@Result,N'á', N'a');
    SET @Result = REPLACE(@Result,N'ả', N'a');
    SET @Result = REPLACE(@Result,N'ã', N'a');
    SET @Result = REPLACE(@Result,N'à', N'a');
    SET @Result = REPLACE(@Result,N'ạ', N'a');

    SET @Result = REPLACE(@Result,N'đ', N'd');

    SET @Result = REPLACE(@Result,N'ế', N'e');
    SET @Result = REPLACE(@Result,N'ể', N'e');
    SET @Result = REPLACE(@Result,N'ễ', N'e');
    SET @Result = REPLACE(@Result,N'ề', N'e');
    SET @Result = REPLACE(@Result,N'ệ', N'e');
    SET @Result = REPLACE(@Result,N'ê', N'e');

    SET @Result = REPLACE(@Result,N'é', N'e');
    SET @Result = REPLACE(@Result,N'ẻ', N'e');
    SET @Result = REPLACE(@Result,N'ẽ', N'e');
    SET @Result = REPLACE(@Result,N'è', N'e');
    SET @Result = REPLACE(@Result,N'ẹ', N'e');

    SET @Result = REPLACE(@Result,N'í', N'i');
    SET @Result = REPLACE(@Result,N'ỉ', N'i');
    SET @Result = REPLACE(@Result,N'ĩ', N'i');
    SET @Result = REPLACE(@Result,N'ì', N'i');
    SET @Result = REPLACE(@Result,N'ị', N'i');

    SET @Result = REPLACE(@Result,N'ố', N'o');
    SET @Result = REPLACE(@Result,N'ổ', N'o');
    SET @Result = REPLACE(@Result,N'ỗ', N'o');
    SET @Result = REPLACE(@Result,N'ồ', N'o');
    SET @Result = REPLACE(@Result,N'ộ', N'o');
    SET @Result = REPLACE(@Result,N'ô', N'o');

    SET @Result = REPLACE(@Result,N'ớ', N'o');
    SET @Result = REPLACE(@Result,N'ở', N'o');
    SET @Result = REPLACE(@Result,N'ỡ', N'o');
    SET @Result = REPLACE(@Result,N'ờ', N'o');
    SET @Result = REPLACE(@Result,N'ợ', N'o');
    SET @Result = REPLACE(@Result,N'ơ', N'o');

    SET @Result = REPLACE(@Result,N'ó', N'o');
    SET @Result = REPLACE(@Result,N'ỏ', N'o');
    SET @Result = REPLACE(@Result,N'õ', N'o');
    SET @Result = REPLACE(@Result,N'ò', N'o');
    SET @Result = REPLACE(@Result,N'ọ', N'o');

    SET @Result = REPLACE(@Result,N'ứ', N'u');
    SET @Result = REPLACE(@Result,N'ử', N'u');
    SET @Result = REPLACE(@Result,N'ữ', N'u');
    SET @Result = REPLACE(@Result,N'ừ', N'u');
    SET @Result = REPLACE(@Result,N'ự', N'u');
    SET @Result = REPLACE(@Result,N'ư', N'u');

    SET @Result = REPLACE(@Result,N'ú', N'u');
    SET @Result = REPLACE(@Result,N'ủ', N'u');
    SET @Result = REPLACE(@Result,N'ũ', N'u');
    SET @Result = REPLACE(@Result,N'ù', N'u');
    SET @Result = REPLACE(@Result,N'ụ', N'u');

    SET @Result = REPLACE(@Result,N'ý', N'y');
    SET @Result = REPLACE(@Result,N'ỷ', N'y');
    SET @Result = REPLACE(@Result,N'ỹ', N'y');
    SET @Result = REPLACE(@Result,N'ỳ', N'y');
    SET @Result = REPLACE(@Result,N'ỵ', N'y');

    SET @Result = REPLACE(@Result,N'Ắ', N'A');
    SET @Result = REPLACE(@Result,N'Ẳ', N'A');
    SET @Result = REPLACE(@Result,N'Ẵ', N'A');
    SET @Result = REPLACE(@Result,N'Ằ', N'A');
    SET @Result = REPLACE(@Result,N'Ặ', N'A');
    SET @Result = REPLACE(@Result,N'Ă', N'A');

    SET @Result = REPLACE(@Result,N'Ấ', N'A');
    SET @Result = REPLACE(@Result,N'Ẩ', N'A');
    SET @Result = REPLACE(@Result,N'Ẫ', N'A');
    SET @Result = REPLACE(@Result,N'Ầ', N'A');
    SET @Result = REPLACE(@Result,N'Ậ', N'A');
    SET @Result = REPLACE(@Result,N'Â', N'A');

    SET @Result = REPLACE(@Result,N'Á', N'A');
    SET @Result = REPLACE(@Result,N'Ả', N'A');
    SET @Result = REPLACE(@Result,N'Ã', N'A');
    SET @Result = REPLACE(@Result,N'À', N'A');
    SET @Result = REPLACE(@Result,N'Ạ', N'A');

    SET @Result = REPLACE(@Result,N'Đ', N'D');

    SET @Result = REPLACE(@Result,N'Ế', N'E');
    SET @Result = REPLACE(@Result,N'Ể', N'E');
    SET @Result = REPLACE(@Result,N'Ễ', N'E');
    SET @Result = REPLACE(@Result,N'Ề', N'E');
    SET @Result = REPLACE(@Result,N'Ệ', N'E');
    SET @Result = REPLACE(@Result,N'Ê', N'E');

    SET @Result = REPLACE(@Result,N'É', N'E');
    SET @Result = REPLACE(@Result,N'Ẻ', N'E');
    SET @Result = REPLACE(@Result,N'Ẽ', N'E');
    SET @Result = REPLACE(@Result,N'È', N'E');
    SET @Result = REPLACE(@Result,N'Ẹ', N'E');

    SET @Result = REPLACE(@Result,N'Í', N'I');
    SET @Result = REPLACE(@Result,N'Ỉ', N'I');
    SET @Result = REPLACE(@Result,N'Ĩ', N'I');
    SET @Result = REPLACE(@Result,N'Ì', N'I');
    SET @Result = REPLACE(@Result,N'Ị', N'I');

    SET @Result = REPLACE(@Result,N'Ố', N'O');
    SET @Result = REPLACE(@Result,N'Ổ', N'O');
    SET @Result = REPLACE(@Result,N'Ỗ', N'O');
    SET @Result = REPLACE(@Result,N'Ồ', N'O');
    SET @Result = REPLACE(@Result,N'Ộ', N'O');
    SET @Result = REPLACE(@Result,N'Ô', N'O');

    SET @Result = REPLACE(@Result,N'Ớ', N'O');
    SET @Result = REPLACE(@Result,N'Ở', N'O');
    SET @Result = REPLACE(@Result,N'Ỡ', N'O');
    SET @Result = REPLACE(@Result,N'Ờ', N'O');
    SET @Result = REPLACE(@Result,N'Ợ', N'O');
    SET @Result = REPLACE(@Result,N'Ơ', N'O');

    SET @Result = REPLACE(@Result,N'Ó', N'O');
    SET @Result = REPLACE(@Result,N'Ỏ', N'O');
    SET @Result = REPLACE(@Result,N'Õ', N'O');
    SET @Result = REPLACE(@Result,N'Ò', N'O');
    SET @Result = REPLACE(@Result,N'Ọ', N'O');

    SET @Result = REPLACE(@Result,N'Ứ', N'U');
    SET @Result = REPLACE(@Result,N'Ử', N'U');
    SET @Result = REPLACE(@Result,N'Ữ', N'U');
    SET @Result = REPLACE(@Result,N'Ừ', N'U');
    SET @Result = REPLACE(@Result,N'Ự', N'U');
    SET @Result = REPLACE(@Result,N'Ư', N'U');

    SET @Result = REPLACE(@Result,N'Ú', N'U');
    SET @Result = REPLACE(@Result,N'Ủ', N'U');
    SET @Result = REPLACE(@Result,N'Ũ', N'U');
    SET @Result = REPLACE(@Result,N'Ù', N'U');
    SET @Result = REPLACE(@Result,N'Ụ', N'U');

    SET @Result = REPLACE(@Result,N'Ý', N'Y');
    SET @Result = REPLACE(@Result,N'Ỷ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỹ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỳ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỵ', N'Y');

    --- Unicode tổ hợp - Unicode vietnamese
    SET @Result = REPLACE(@Result,N'ắ', N'a');
    SET @Result = REPLACE(@Result,N'ẳ', N'a');
    SET @Result = REPLACE(@Result,N'ẵ', N'a');
    SET @Result = REPLACE(@Result,N'ằ', N'a');
    SET @Result = REPLACE(@Result,N'ặ', N'a');
    SET @Result = REPLACE(@Result,N'ă', N'a');

    SET @Result = REPLACE(@Result,N'ấ', N'a');
    SET @Result = REPLACE(@Result,N'ẩ', N'a');
    SET @Result = REPLACE(@Result,N'ẫ', N'a');
    SET @Result = REPLACE(@Result,N'ầ', N'a');
    SET @Result = REPLACE(@Result,N'ậ', N'a');
    SET @Result = REPLACE(@Result,N'â', N'a');

    SET @Result = REPLACE(@Result,N'á', N'a');
    SET @Result = REPLACE(@Result,N'ả', N'a');
    SET @Result = REPLACE(@Result,N'ã', N'a');
    SET @Result = REPLACE(@Result,N'à', N'a');
    SET @Result = REPLACE(@Result,N'ạ', N'a');

    SET @Result = REPLACE(@Result,N'đ', N'd');

    SET @Result = REPLACE(@Result,N'ế', N'e');
    SET @Result = REPLACE(@Result,N'ể', N'e');
    SET @Result = REPLACE(@Result,N'ễ', N'e');
    SET @Result = REPLACE(@Result,N'ề', N'e');
    SET @Result = REPLACE(@Result,N'ệ', N'e');
    SET @Result = REPLACE(@Result,N'ê', N'e');

    SET @Result = REPLACE(@Result,N'é', N'e');
    SET @Result = REPLACE(@Result,N'ẻ', N'e');
    SET @Result = REPLACE(@Result,N'ẽ', N'e');
    SET @Result = REPLACE(@Result,N'è', N'e');
    SET @Result = REPLACE(@Result,N'ẹ', N'e');

    SET @Result = REPLACE(@Result,N'í', N'i');
    SET @Result = REPLACE(@Result,N'ỉ', N'i');
    SET @Result = REPLACE(@Result,N'ĩ', N'i');
    SET @Result = REPLACE(@Result,N'ì', N'i');
    SET @Result = REPLACE(@Result,N'ị', N'i');

    SET @Result = REPLACE(@Result,N'ố', N'o');
    SET @Result = REPLACE(@Result,N'ổ', N'o');
    SET @Result = REPLACE(@Result,N'ỗ', N'o');
    SET @Result = REPLACE(@Result,N'ồ', N'o');
    SET @Result = REPLACE(@Result,N'ộ', N'o');
    SET @Result = REPLACE(@Result,N'ô', N'o');

    SET @Result = REPLACE(@Result,N'ớ', N'o');
    SET @Result = REPLACE(@Result,N'ở', N'o');
    SET @Result = REPLACE(@Result,N'ỡ', N'o');
    SET @Result = REPLACE(@Result,N'ờ', N'o');
    SET @Result = REPLACE(@Result,N'ợ', N'o');
    SET @Result = REPLACE(@Result,N'ơ', N'o');

    SET @Result = REPLACE(@Result,N'ó', N'o');
    SET @Result = REPLACE(@Result,N'ỏ', N'o');
    SET @Result = REPLACE(@Result,N'õ', N'o');
    SET @Result = REPLACE(@Result,N'ò', N'o');
    SET @Result = REPLACE(@Result,N'ọ', N'o');

    SET @Result = REPLACE(@Result,N'ứ', N'u');
    SET @Result = REPLACE(@Result,N'ử', N'u');
    SET @Result = REPLACE(@Result,N'ữ', N'u');
    SET @Result = REPLACE(@Result,N'ừ', N'u');
    SET @Result = REPLACE(@Result,N'ự', N'u');
    SET @Result = REPLACE(@Result,N'ư', N'u');

    SET @Result = REPLACE(@Result,N'ú', N'u');
    SET @Result = REPLACE(@Result,N'ủ', N'u');
    SET @Result = REPLACE(@Result,N'ũ', N'u');
    SET @Result = REPLACE(@Result,N'ù', N'u');
    SET @Result = REPLACE(@Result,N'ụ', N'u');

    SET @Result = REPLACE(@Result,N'ý', N'y');
    SET @Result = REPLACE(@Result,N'ỷ', N'y');
    SET @Result = REPLACE(@Result,N'ỹ', N'y');
    SET @Result = REPLACE(@Result,N'ỳ', N'y');
    SET @Result = REPLACE(@Result,N'ỵ', N'y');

    SET @Result = REPLACE(@Result,N'Ắ', N'A');
    SET @Result = REPLACE(@Result,N'Ẳ', N'A');
    SET @Result = REPLACE(@Result,N'Ẵ', N'A');
    SET @Result = REPLACE(@Result,N'Ằ', N'A');
    SET @Result = REPLACE(@Result,N'Ặ', N'A');
    SET @Result = REPLACE(@Result,N'Ă', N'A');

    SET @Result = REPLACE(@Result,N'Ấ', N'A');
    SET @Result = REPLACE(@Result,N'Ẩ', N'A');
    SET @Result = REPLACE(@Result,N'Ẫ', N'A');
    SET @Result = REPLACE(@Result,N'Ầ', N'A');
    SET @Result = REPLACE(@Result,N'Ậ', N'A');
    SET @Result = REPLACE(@Result,N'Â', N'A');

    SET @Result = REPLACE(@Result,N'Á', N'A');
    SET @Result = REPLACE(@Result,N'Ả', N'A');
    SET @Result = REPLACE(@Result,N'Ã', N'A');
    SET @Result = REPLACE(@Result,N'À', N'A');
    SET @Result = REPLACE(@Result,N'Ạ', N'A');

    SET @Result = REPLACE(@Result,N'Đ', N'D');

    SET @Result = REPLACE(@Result,N'Ế', N'E');
    SET @Result = REPLACE(@Result,N'Ể', N'E');
    SET @Result = REPLACE(@Result,N'Ễ', N'E');
    SET @Result = REPLACE(@Result,N'Ề', N'E');
    SET @Result = REPLACE(@Result,N'Ệ', N'E');
    SET @Result = REPLACE(@Result,N'Ê', N'E');

    SET @Result = REPLACE(@Result,N'É', N'E');
    SET @Result = REPLACE(@Result,N'Ẻ', N'E');
    SET @Result = REPLACE(@Result,N'Ẽ', N'E');
    SET @Result = REPLACE(@Result,N'È', N'E');
    SET @Result = REPLACE(@Result,N'Ẹ', N'E');

    SET @Result = REPLACE(@Result,N'Í', N'I');
    SET @Result = REPLACE(@Result,N'Ỉ', N'I');
    SET @Result = REPLACE(@Result,N'Ĩ', N'I');
    SET @Result = REPLACE(@Result,N'Ì', N'I');
    SET @Result = REPLACE(@Result,N'Ị', N'I');

    SET @Result = REPLACE(@Result,N'Ố', N'O');
    SET @Result = REPLACE(@Result,N'Ổ', N'O');
    SET @Result = REPLACE(@Result,N'Ỗ', N'O');
    SET @Result = REPLACE(@Result,N'Ồ', N'O');
    SET @Result = REPLACE(@Result,N'Ộ', N'O');
    SET @Result = REPLACE(@Result,N'Ô', N'O');

    SET @Result = REPLACE(@Result,N'Ớ', N'O');
    SET @Result = REPLACE(@Result,N'Ở', N'O');
    SET @Result = REPLACE(@Result,N'Ỡ', N'O');
    SET @Result = REPLACE(@Result,N'Ờ', N'O');
    SET @Result = REPLACE(@Result,N'Ợ', N'O');
    SET @Result = REPLACE(@Result,N'Ơ', N'O');

    SET @Result = REPLACE(@Result,N'Ó', N'O');
    SET @Result = REPLACE(@Result,N'Ỏ', N'O');
    SET @Result = REPLACE(@Result,N'Õ', N'O');
    SET @Result = REPLACE(@Result,N'Ò', N'O');
    SET @Result = REPLACE(@Result,N'Ọ', N'O');

    SET @Result = REPLACE(@Result,N'Ứ', N'U');
    SET @Result = REPLACE(@Result,N'Ử', N'U');
    SET @Result = REPLACE(@Result,N'Ữ', N'U');
    SET @Result = REPLACE(@Result,N'Ừ', N'U');
    SET @Result = REPLACE(@Result,N'Ự', N'U');
    SET @Result = REPLACE(@Result,N'Ư', N'U');

    SET @Result = REPLACE(@Result,N'Ú', N'U');
    SET @Result = REPLACE(@Result,N'Ủ', N'U');
    SET @Result = REPLACE(@Result,N'Ũ', N'U');
    SET @Result = REPLACE(@Result,N'Ù', N'U');
    SET @Result = REPLACE(@Result,N'Ụ', N'U');

    SET @Result = REPLACE(@Result,N'Ý', N'Y');
    SET @Result = REPLACE(@Result,N'Ỷ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỹ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỳ', N'Y');
    SET @Result = REPLACE(@Result,N'Ỵ', N'Y');
       

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_Doc_Build_NEAR_Keyword]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Func_Doc_Build_NEAR_Keyword]
(
	@KeyWord nvarchar(1000) 
)
RETURNS nvarchar(1000)
BEGIN 
	DECLARE @ResultVar nvarchar(1000) = '';
	IF ((SELECT COUNT(*)
		FROM dbo.Func_SplitTextToTable_WithRowNumber(@KeyWord, ' ')) > 1)
	BEGIN
		SET @ResultVar = 'NEAR(';
	
		SELECT @ResultVar = @ResultVar + items + ','
		FROM dbo.Func_SplitTextToTable_WithRowNumber(@KeyWord, ' ');
	
		SET @ResultVar = SUBSTRING(@ResultVar,0, LEN(@ResultVar)) + ')';
	END
	ELSE
	BEGIN
		SET @ResultVar = @KeyWord;
	END

	RETURN @ResultVar;

END




GO
/****** Object:  UserDefinedFunction [dbo].[Func_Doc_CheckIsOverDue]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_Doc_CheckIsOverDue]
(
	@ToDate datetime,
	@FinishDate  datetime,
	@Status int,
	@CheckDate datetime
)
RETURNS bit
AS
BEGIN
	DECLARE @IsOverDue bit = 0

	IF (@Status <> 4)
	BEGIN
		IF (@CheckDate > @ToDate)
		BEGIN
			SET @IsOverDue = 1;
		END
	END
	ELSE
	BEGIN
		IF (@Status = 4 AND @FinishDate > @ToDate)
		BEGIN
			SET @IsOverDue = 1;
		END
	END	
	
	-- Return the result of the function
	RETURN @IsOverDue;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_Firsties]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


		  CREATE FUNCTION [dbo].[Func_Firsties] ( @str NVARCHAR(4000) )
RETURNS NVARCHAR(2000)
AS
BEGIN
    DECLARE @retval NVARCHAR(2000);

    SET @str=RTRIM(LTRIM(@str));
    SET @retval=LEFT(@str,1);

    WHILE CHARINDEX(' ',@str,1)>0 BEGIN
        SET @str=LTRIM(RIGHT(@str,LEN(@str)-CHARINDEX(' ',@str,1)));
        SET @retval+=LEFT(@str,1);
    END

    RETURN @retval;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetAttachInfos]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetAttachInfos]
(
	@DocId Uniqueidentifier
)
RETURNS NVARCHAR(3000)
AS
BEGIN 
	DECLARE @ListAttachs NVARCHAR(3000) = '';

	SELECT @ListAttachs = @ListAttachs +
	CAST(FD.ID AS NVARCHAR(36)) + ';' + FD.Name + '|'
	FROM FileDocuments FD
	WHERE FD.DocID = @DocId;

	RETURN @ListAttachs;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetDeptIdOfTaskItem]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetDeptIdOfTaskItem]
(
	@DeptIds nvarchar(max),
	@AssignTo uniqueidentifier null,
	@TaskItemId uniqueidentifier
)
RETURNS uniqueidentifier
AS
BEGIN
	DECLARE @ResultVar uniqueidentifier = null;
	 
	IF (@AssignTo IS NOT NULL AND @AssignTo != CAST(0x0 AS uniqueidentifier))
	BEGIN
		SELECT
			@ResultVar = DepartmentId
		FROM 
			Task.TaskItemAssign
		WHERE 
			TaskItemId = @TaskItemId AND @AssignTo = AssignTo;
	END
	ELSE
	BEGIN
		IF (@DeptIds IS NOT NULL AND @DeptIds != '')
		BEGIN
			SELECT
				@ResultVar = DepartmentId
			FROM 
				Task.TaskItemAssign
			WHERE 
				TaskItemId = @TaskItemId AND DepartmentId IN 
				(SELECT items FROM dbo.Func_SplitTextToTable(@DeptIds, ';'))
				AND DepartmentId != (SELECT DepartmentId FROM Task.TaskItem WHERE Id = @TaskItemId);
		END
	END

	RETURN @ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetDescTrackingWorkFlowID]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE FUNCTION [dbo].[Func_GetDescTrackingWorkFlowID]
(
	@TrackingID uniqueidentifier
)
RETURNS uniqueidentifier
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar uniqueidentifier = NULL;

	SELECT TOP 1
		@ResultVar = TWF.ID
	FROM TrackingWorkflowDocuments TWF
	WHERE TWF.TrackingDocumentID = @TrackingID
	ORDER BY TWF.Created DESC;

	RETURN @ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetFullNameByUserId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetFullNameByUserId]
(
	@UserId uniqueidentifier
)
RETURNS nvarchar(200)
AS
BEGIN
	DECLARE @ResultVar nvarchar(200) = ''

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = FullName
	FROM dbo.Users
	WHERE ID = @UserId

	-- Return the result of the function
	RETURN @ResultVar

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetLinkDeptID]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetLinkDeptID] 
(
	@DocId uniqueidentifier
)
RETURNS uniqueidentifier
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar uniqueidentifier = NULL;
	
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM PVGAS_SurePortal_Doc_TCT.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_KVT.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_KDN.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_KDK.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_KCM.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_DVK.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_DNB.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_DAK.dbo.Documents 
		WHERE ID = @DocId;
	END
	IF (@ResultVar IS NULL)
	BEGIN
		SELECT @ResultVar = EditDepartment FROM SurePortal_Doc_BCM.dbo.Documents 
		WHERE ID = @DocId;
	END
	-- Return the result of the function
	RETURN @ResultVar

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetLoginNameByID]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetLoginNameByID]
(
	@ID uniqueidentifier
)
RETURNS nvarchar(200)
AS
BEGIN
	DECLARE @LoginName nvarchar(200) = ''

	SELECT @LoginName = UserName FROM Users WHERE ID = @ID

	-- Return the result of the function
	RETURN @LoginName

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetNoOrderFromLevelString]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SELECT SUBSTRING('101100', 1, 2)
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetNoOrderFromLevelString]
(
--DECLARE
	@LevelString nvarchar(50) 
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar nvarchar(50) = '',
			@RunVar nvarchar(50) = @LevelString,
			@Idx int = 1;
			
	WHILE (@Idx <= LEN(@LevelString))
	BEGIN
		IF (LEN(@RunVar) - @Idx > 0)
		BEGIN
			SET @ResultVar = @ResultVar + SUBSTRING(@RunVar, @Idx, 2);
			IF (LEN(@RunVar) - @Idx - 2 > 0)
				SET @ResultVar = @ResultVar + '.';

			SET @RunVar = SUBSTRING(@LevelString, @Idx, LEN(@LevelString) - @Idx);
		END
		SET @Idx = @Idx + 2;
	END

	 
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetPlanningType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetPlanningType]
( 
	@ExtensiveInfo XML
)
RETURNS NVARCHAR(200)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(200);

	SET @ResultVar = @ExtensiveInfo.value ('(/Columns/Column[@name="PlanningType"])[1]', 'NVARCHAR(200)');

	IF (@ResultVar = 'Planning')
	BEGIN
		SET @ResultVar = '1-Planning';
	END
	ELSE
	IF (@ResultVar = 'Unplanned')
	BEGIN
		SET @ResultVar = '2-Unplanned';
	END
	ELSE
	IF (@ResultVar = 'PlanningApprove')
	BEGIN
		SET @ResultVar = '5-PlanningApprove_Scheduling';
	END
	ELSE
	IF (@ResultVar = 'Scheduling')
	BEGIN
		SET @ResultVar = '4-Scheduling';
	END
	ELSE
	BEGIN
		SET @ResultVar = '3-Document_Unplanned';
	END
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetPriorityUserIdByTaskItemId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-15
-- Description:	Get UserId
-- =============================================
CREATE   FUNCTION [dbo].[Func_GetPriorityUserIdByTaskItemId] 
(
	-- Add the parameters for the function here
	@TaskItemId uniqueidentifier
)
RETURNS uniqueidentifier
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result VARCHAR(50);

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = (SELECT TOP(1) AssignTo FROM [Task].[TaskItemAssign] WHERE TaskItemId = @TaskItemId ORDER BY [TaskType]);

	-- Return the result of the function
	RETURN @Result

END





GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetSignedIDByUserName]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetSignedIDByUserName]
(
	@UserNames nvarchar(max) 
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @ResultVar nvarchar(max) = '';

	SELECT @ResultVar = @ResultVar + CAST( dbo.Func_GetUserIDByLoginName(items) AS NVARCHAR(36) ) + ','
	FROM 
		dbo.Func_SplitTextToTable(@UserNames, ';');


	RETURN @ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetStringTaskItemStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetStringTaskItemStatus]
(
--DECLARE
	@ProjectId uniqueidentifier,
	@AssignTo uniqueidentifier 
)
RETURNS nvarchar(100)
AS
BEGIN
	DECLARE @ResultVar nvarchar(100) = '';

	SELECT TOP 1 @ResultVar = CAST(dt2.TaskItemStatusId as NVARCHAR(2)) + '-TRACKING;' + 
				CAST(dt2.TaskType as nvarchar(2)) + '-TRACKTYPE;'
	FROM 
		[Task].TaskItemAssign dt2
	WHERE 
		dt2.ProjectId = @ProjectId AND AssignTo = @AssignTo
	ORDER BY
		dt2.TaskItemStatusId ASC; 
	
	SELECT TOP 1 @ResultVar = @ResultVar + ' overdue'
	FROM 
		[Task].TaskItemAssign
	WHERE 
		ProjectId = @ProjectId AND AssignTo = @AssignTo 
		AND dbo.Func_Doc_CheckIsOverDue(ToDate, FinishedDate, TaskItemStatusId, getdate()) = 1;

	IF (@ResultVar = '')
	BEGIN
		IF (NOT EXISTS(
			SELECT Id
			FROM Task.TaskItemAssign
			WHERE TaskItemId IN
			(
				SELECT Id FROM [Task].TaskItem WHERE ProjectId = @ProjectId 
				  AND ParentId = Cast(0x0 as UNIQUEIDENTIFIER)
			
			) AND TaskItemStatusId != 0 AND TaskType = 1))
		BEGIN
			SET @ResultVar = '0-TRACKING'
		END
		ELSE
		IF (NOT EXISTS(
			SELECT Id
			FROM Task.TaskItemAssign
			WHERE TaskItemId IN
			(
				SELECT Id FROM [Task].TaskItem WHERE ProjectId = @ProjectId 
				  AND ParentId = Cast(0x0 as UNIQUEIDENTIFIER)
			
			) AND TaskItemStatusId != 4 AND TaskType = 1))
		BEGIN
			SET @ResultVar = '4-TRACKING'
		END
		ELSE
		BEGIN
			SET @ResultVar = '1-TRACKING';
		END
		--SET @ResultVar = '1-TRACKING'
	END

	RETURN @ResultVar

	--SELECT
	--	@ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetStringTrackingStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetStringTrackingStatus]
(
--DECLARE
	@DocID uniqueidentifier,
	@AssignTo uniqueidentifier 
)
RETURNS nvarchar(100)
AS
BEGIN
	DECLARE @ResultVar nvarchar(100) = '';

	SELECT TOP 1 @ResultVar = CAST(Status as NVARCHAR(2)) + '-TRACKING;' + 
				CAST(dt2.Type as nvarchar(2)) + '-TRACKTYPE;'
	FROM 
		TrackingDocuments dt2
	WHERE 
		DocID = @DocID AND AssignTo = @AssignTo
	ORDER BY
		Status ASC;

	SELECT  TOP 1 @ResultVar = @ResultVar + 'isread;'
	FROM TransferDocuments TF
	WHERE 
		TF.DocumentID = @DocID
		AND TF.Receiver = @AssignTo
		AND TF.ReadTime IS NOT NULL

	--SET @ResultVar = @ResultVar + '-TRACKING';
	
	
	SELECT TOP 1 @ResultVar = @ResultVar + ' overdue'
	FROM 
		TrackingDocuments
	WHERE 
		DocID = @DocID AND AssignTo = @AssignTo 
		AND dbo.Func_Doc_CheckIsOverDue(ToDate, FinishedDate, Status, getdate()) = 1;

	RETURN @ResultVar

	--SELECT
	--	@ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetStringTransferStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetStringTransferStatus]
(
--DECLARE
	@DocID uniqueidentifier,
	@AssignTo uniqueidentifier 
)
RETURNS nvarchar(100)
AS
BEGIN
	DECLARE @ResultVar nvarchar(100) = '';

	SELECT TOP 1 @ResultVar = @ResultVar + CAST(Trans2.Status as NVARCHAR(10)) + '-TRANSFER'
	FROM TransferDocuments Trans2 
	 WHERE Trans2.DocumentID = @DocID  
	 AND Trans2.Receiver = @AssignTo 
	ORDER BY Trans2.Status DESC;
		 

	SELECT  TOP 1 @ResultVar = @ResultVar + ';isread;'
	FROM TransferDocuments TF
	WHERE 
		TF.DocumentID = @DocID
		AND TF.Receiver = @AssignTo
		AND TF.ReadTime IS NOT NULL
		 
	RETURN @ResultVar

	--SELECT
	--	@ResultVar;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTaskAssignFullNameToByDocId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  
CREATE   FUNCTION [dbo].[Func_GetTaskAssignFullNameToByDocId]
( 
	@ProjectId UNIQUEIDENTIFIER, @Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	SELECT @ResultVar = @ResultVar +
	 ISNULL(U.FullName,'') + '; '
	FROM 
		[Task].[TaskItemAssign] TIA
		LEFT JOIN Users U ON U.ID = TIA.AssignTo
	WHERE 
		ProjectId = @ProjectId  AND TaskType = @Type
	ORDER BY FromDate DESC

	-- Return the result of the function
	RETURN @ResultVar;

END
                        


GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTaskAssignToByDocId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
CREATE   FUNCTION [dbo].[Func_GetTaskAssignToByDocId]
(
	@ProjectId UNIQUEIDENTIFIER,
	@Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	SELECT @ResultVar = @ResultVar + Cast( AssignTo as NVARCHAR(36)) + ';'
	FROM [Task].[TaskItemAssign]
	WHERE 
		ProjectId =@ProjectId  AND TaskType = @Type
	ORDER BY FromDate DESC

	-- Return the result of the function
	RETURN @ResultVar;

END
                        




GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTotalPage]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[Func_GetTotalPage]
(
	@totalRecord int,
	@pagesize int
)      
returns int  
as      
begin      
    declare @totalPage int ; 		
            if (@totalRecord = 0)
                set @totalPage = 1;
            else
				begin
					set @totalPage = @totalRecord / @pagesize;
					if (@totalRecord % @pagesize > 0) set @totalPage = @totalPage +1 ;
                end
return  @totalPage    
end


GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackAssignToByDocId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
                        CREATE FUNCTION [dbo].[Func_GetTrackAssignToByDocId]
                        (
	                        @DocID UNIQUEIDENTIFIER,
	                        @Type INT
                        )
                        RETURNS NVARCHAR(MAX)
                        AS
                        BEGIN
	                        DECLARE @ResultVar NVARCHAR(MAX) = '';

	                        SELECT @ResultVar = @ResultVar + Cast( AssignTo as NVARCHAR(36)) + ';'
	                        FROM TrackingDocuments
	                        WHERE 
		                        DocID =@DocID  AND Type = @Type 
		                        AND 
		                        ParentID = Cast(0x0 AS UNIQUEIDENTIFIER)
	                        ORDER BY Created DESC

	                        -- Return the result of the function
	                        RETURN @ResultVar;

                        END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackAssignToByType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Func_GetTrackAssignToByType]
(
	@ParentID UNIQUEIDENTIFIER,
	@Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	SELECT @ResultVar = @ResultVar + Cast( AssignTo as NVARCHAR(36)) + ';'
	FROM TrackingDocuments
	WHERE ParentID = @ParentID AND Type = @Type
	ORDER BY Created DESC

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackAssignToByType2]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE FUNCTION [dbo].[Func_GetTrackAssignToByType2]
(
	@ParentID UNIQUEIDENTIFIER,
	@Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';
	IF (@ParentID != CAST(0x0 As uniqueidentifier))
	BEGIN
		SELECT @ResultVar = @ResultVar + Cast( AssignTo as NVARCHAR(36)) + '_' + CAST(ID as NVARCHAR(36)) + ';'
		FROM TrackingDocuments
		WHERE ParentID = @ParentID AND Type = @Type
		ORDER BY Created DESC
	END
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackAssignToFullNameByType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
CREATE FUNCTION [dbo].[Func_GetTrackAssignToFullNameByType]
(
	@TaskItemId UNIQUEIDENTIFIER,
	@Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	SELECT 
		@ResultVar = @ResultVar + U.FullName + ';'
	FROM 
		Task.TaskItemAssign	TIA
		LEFT JOIN Users U ON U.ID = TIA.AssignTo		
	WHERE 
		TaskItemId = @TaskItemId AND TaskType = @Type
	ORDER BY ModifiedDate

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackAssignToStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTrackAssignToStatus]
(
	 @ParentID UNIQUEIDENTIFIER,
	 @DocID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @ResultVar INT;
	 
	-- kiem tra chưa triển khai
	IF (EXISTS(
		SELECT TOP 1
			ID
		FROM TrackingDocuments
		WHERE DocID = @DocID AND ParentID = @ParentID
		AND [Status] != 0 
		AND [Type] IN (1,6)) )
	BEGIN
		-- kiem tra dang xu ly
		IF (EXISTS(
			SELECT TOP 1
				ID
			FROM TrackingDocuments
			WHERE DocID = @DocID  AND ParentID = @ParentID
				AND [Status] in (0,1,2,5) 
				AND [Type] IN (1,6)) 
			)
		BEGIN
			SET @ResultVar = 1;
		END
		ELSE
		BEGIN
			SET @ResultVar = 4;
		END
	END
	ELSE
	BEGIN
		SET @ResultVar = 0;
	END

	RETURN
		@ResultVar;

	-- Return the result of the function
	RETURN @ResultVar

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackingSeverityTrackValue]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTrackingSeverityTrackValue]
(
	@Severity int
)
RETURNS real
AS
BEGIN
	DECLARE @ResultVar real = 0.0;

	SELECT @ResultVar = TS.Density
	FROM 
		SurePortal_DCM.dbo.TrackingSeverity TS
	WHERE 
		TS.Id = @Severity

	IF (@ResultVar = 0.0)
	BEGIN
		SELECT TOP 1 @ResultVar = TS.Density
		FROM 
			SurePortal_DCM.dbo.TrackingSeverity TS
		ORDER BY 
			TS.Density ASC;
	END
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackingSeverityValue]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTrackingSeverityValue]
(
	@PriorityID uniqueidentifier
)
RETURNS real
AS
BEGIN
	DECLARE @ResultVar real = 0.0;

	SELECT @ResultVar = TS.Density
	FROM 
		SurePortal_DCM.dbo.TrackingSeverity TS
		INNER JOIN SurePortal_DCM.dbo.PriorityDocuments P ON P.MapingID = TS.Id
	WHERE 
		P.IsActive = 1 AND  P.ID = @PriorityID;

	IF (@ResultVar = 0.0)
	BEGIN
		SELECT TOP 1 @ResultVar = TS.Density
		FROM 
			SurePortal_DCM.dbo.TrackingSeverity TS
		ORDER BY 
			TS.Density ASC;
	END
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackingType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTrackingType]
( 
	@ExtensiveInfo XML,
	@DocID uniqueidentifier,
	@DeptID nvarchar(4000)
)
RETURNS int
AS
BEGIN
	DECLARE @ResultVar int = 0, @Result nvarchar(20);

	SET @Result = @ExtensiveInfo.value ('(/Columns/Column[@name="TrackingType"])[1]', 'NVARCHAR(10)');

	IF (@Result = '1')
	BEGIN
		SET @ResultVar = 1;
	END
	ELSE
	IF (@Result = '3')
	BEGIN
		SET @ResultVar = 3;
	END
	ELSE
	IF (@Result = '7')
	BEGIN
		SET @ResultVar = 7;
	END
	ELSE
	BEGIN
		SELECT TOP 1
			@ResultVar =  Type  
		FROM TrackingDocuments
		WHERE DocID = @DocID
		
		AND DeptID IN (SELECT
				items
			FROM dbo.Func_SplitTextToTable(@DeptID, ';'))
		ORDER BY Type
	END
	IF (@ResultVar = 0)
		SET @ResultVar = 1;
	
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackLastResultByType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTrackLastResultByType]
(
	@ParentID UNIQUEIDENTIFIER,
	@Type INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	SELECT @ResultVar = @ResultVar + LastResult + '; '
	FROM TrackingDocuments
	WHERE ParentID = @ParentID AND LastResult IS NOT NULL
	--AND LastResult != Name
	 AND Type = @Type
	ORDER BY Modified DESC

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTrackWFByType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[Func_GetTrackWFByType]
(
	@TrackParentID UNIQUEIDENTIFIER,
	@Type NVARCHAR(20)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @ResultVar NVARCHAR(MAX) = '';

	IF (@Type = 'ExtendReason')
	BEGIN
		SELECT @ResultVar = @ResultVar + ExtendReason + '; '
		FROM TrackingWorkflowDocuments
		WHERE TrackingDocumentID IN 
		(
			SELECT TD.ID
			FROM TrackingDocuments TD
			WHERE 
			ParentID = @TrackParentID 
			AND TD.Type = 1
		)
		AND ExtendReason IS NOT NULL
		ORDER BY Created DESC;
	END

	IF (@Type = 'Solution')
	BEGIN
		SELECT @ResultVar = @ResultVar + Solution + '; '
		FROM TrackingWorkflowDocuments
		WHERE TrackingDocumentID IN 
		(
			SELECT TD.ID
			FROM TrackingDocuments TD
			WHERE 
			ParentID = @TrackParentID 
			AND TD.Type = 1
		)
		AND Solution IS NOT NULL
		ORDER BY Created DESC;
	END

	IF (@Type = 'Problem')
	BEGIN
		SELECT @ResultVar = @ResultVar + Problem + '; '
		FROM TrackingWorkflowDocuments
		WHERE TrackingDocumentID IN 
		(
			SELECT TD.ID
			FROM TrackingDocuments TD
			WHERE 
			ParentID = @TrackParentID 
			AND TD.Type = 1
		)
		AND Problem IS NOT NULL
		ORDER BY Created DESC;
	END

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTWFDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTWFDate]
(
	@TrackingID uniqueidentifier,
	@Type nvarchar(20)
	
)
RETURNS DATETIME
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar DATETIME = NULL;

	SELECT TOP 1 @ResultVar =  Created
	FROM TrackingWorkflowDocuments
	WHERE TrackingDocumentID = @TrackingID
	ORDER BY Created

	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetTWFDescriptionByTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetTWFDescriptionByTask]
(
	@TrackingDocumentID uniqueidentifier,
	@AssignTo uniqueidentifier,
	@IsExtend bit 
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar nvarchar(max);
	IF (@IsExtend = 1)
	BEGIN
		SELECT TOP 1 @ResultVar = [Description]
		FROM TrackingWorkflowDocuments
		WHERE TrackingDocumentID = @TrackingDocumentID
		AND @AssignTo = AuthorID
		AND Status  = 5
		ORDER BY Created DESC
	END
	ELSE
	BEGIN
		SELECT TOP 1 @ResultVar = [Description]
		FROM TrackingWorkflowDocuments
		WHERE TrackingDocumentID = @TrackingDocumentID
		AND @AssignTo = AuthorID
		AND Status != 5
		ORDER BY Created DESC
	END

	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetUserIDByLoginName]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetUserIDByLoginName]
(
	@LoginName  nvarchar(200)
)
RETURNS uniqueidentifier
AS
BEGIN
	DECLARE @ID uniqueidentifier = NULL

	SELECT @ID = ID FROM Users WHERE UserName = @LoginName

	-- Return the result of the function
	RETURN @ID;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_IsHavePermission]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_IsHavePermission]
(
	 @PName nvarchar(200),
	 @UserID uniqueidentifier
)
RETURNS int
AS
BEGIN
	DECLARE @Exists bit = 0;

	IF (
	EXISTS(
		SELECT *
		FROM 
		(
			SELECT P.ID
			FROM 
				UserRoles UR
				INNER JOIN RolePermissions RP ON RP.RoleID = UR.RoleID
				INNER JOIN [Permissions] P ON P.ID = RP.PermissionID
			WHERE 
				UR.UserID = @UserID
				AND P.Name = @PName
			UNION
			SELECT P.ID
			FROM 
				UserGroups UG
				INNER JOIN GroupRoles GR ON GR.GroupID = UG.GroupID
				INNER JOIN RolePermissions RP ON RP.RoleID = GR.RoleID
				INNER JOIN [Permissions] P ON P.ID = RP.PermissionID
			WHERE 
				UG.UserID = @UserID
				AND P.Name = @PName
		) TB
	) )
	BEGIN
		SET @Exists = 1;
	END

	RETURN
		@Exists;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_ReBuildPathLevel]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 --SELECT dbo.Func_ReBuildPathLevel('0301010000000000', 2, 2)
									 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_ReBuildPathLevel]
(
--DECLARE
	 @src NVARCHAR(50),-- = '0301010000000000',
	 @len int = 2,
	 @repeatsize int = 2
)
RETURNS NVARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(50);
SET @ResultVar =
CONCAT(
CONCAT(
SUBSTRING(@src, 1, 2),
SUBSTRING(@src, @len * @repeatsize + 1, LEN(@src) - (@len * @repeatsize + 1))),
REPLICATE('0', (@len -1) * @repeatsize + 1)
);

	-- Return the result of the function
	RETURN @ResultVar;
	--SELECT @ResultVar

	--SELECT SUBSTRING(@src, 1, 2)
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_ReplaceParamFilter]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_ReplaceParamFilter]
(
	@Param nvarchar(2000)
)
RETURNS  nvarchar(2000)
AS
BEGIN
	DECLARE @Result nvarchar(2000) = '';
	SET @Result = @Param;
	IF (@Result IS NOT NULL AND @Result != '')
	BEGIN
		SET @Result = REPLACE(@Result, ';', ',');
	END 

	RETURN @Result;

END


GO
/****** Object:  UserDefinedFunction [dbo].[Func_SearchLinkDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_SearchLinkDocument]
(
	@DocID uniqueidentifier,
	@DeptID uniqueidentifier,
	@KeyWord nvarchar(400)
)
RETURNS int
AS
BEGIN
	DECLARE @ResultVar int = 0 ;

	SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(LTRIM(RTRIM(@KeyWord))) ;

	DECLARE @DBName nvarchar(200) = '', @Sql nvarchar(1000) = '';

	SELECT
		@DBName = DatabaseName
	FROM Departments
	WHERE ID = @DeptID;

	IF (@DBName = 'SurePortal_Doc_BCM')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_BCM].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR 			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_DAK')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_DAK].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_DNB')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_DNB].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_DVK')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_DVK].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_KCM')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_KCM].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_KDK')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_KDK].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_KDN')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_KDN].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_KVT')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_KVT].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	ELSE
	IF (@DBName = 'SurePortal_Doc_TCT')
	BEGIN
		SELECT TOP 1 @ResultVar = 1
		FROM [SurePortal_Doc_TCT].dbo.Documents
		WHERE	
			ID = @DocID
			AND (
			CONTAINS(Summary, @KeyWord) OR
			CONTAINS(SerialNumber, @KeyWord) OR
			CONTAINS(Sender, @KeyWord) 
			--OR			CAST(DocNumber as nvarchar(20)) = @KeyWord
			);
	END
	


	RETURN @ResultVar;

END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_Select_Item_CoreSchedule]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2018/08/30
-- Description:	Lấy danh sách lặp item theo lịch
-- và module
-- =============================================
CREATE FUNCTION [dbo].[Func_Select_Item_CoreSchedule]
(
	@CurrentDate date,
	@ModuleCode nvarchar(200)
)
RETURNS 
@Table_Result TABLE 
(
	ObjectId nvarchar(128), 
	ScheduleType int,
	ScheduleTypeCode nvarchar(50)
)
AS
BEGIN
	
	
	-- ScheduleType
	-- 0 - Daily
	-- 1 - Weekly
	-- 2 - Monthly
	-- 3 - Yearly
	INSERT INTO @Table_Result
	SELECT	
		Results.ObjectId,
		Results.ScheduleType,
		CASE Results.ScheduleType
			WHEN 0 THEN 'Daily'
			WHEN 1 THEN 'Weekly'
			WHEN 2 THEN 'Monthly'
			WHEN 3 THEN 'Yearly'
		END AS ScheduleTypeCode
	FROM 
	(
		---------------------------------------------------------------------
		-- repeat daily
		--PRINT 'repeat daily - Every weekday'
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 0 AND Sch.IntervalInWeekday = 1		-- in case weekday
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			AND (DATEPART(weekday, @CurrentDate) in (1,7) )			-- in case SA, SUN
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)
		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat daily - Every on 1/day(s)'
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 0 AND Sch.IntervalInWeekday = 0		-- in case day
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberDay(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(DAY, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)
		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat weekly - every 2 weeks on  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 1									-- in case week
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberWeek(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(WEEK, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			-- in case WeekDay in selected
			AND  (	DATEPART(WEEKDAY, @CurrentDate) 
					in (
						SELECT items FROM dbo.Func_SplitTextToTable(Sch.IntervalInDayOfWeek, ';')
					) 
				  )	
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat monthly - every on the day  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 2									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NOT NULL
			AND Sch.IntervalInDateOfMonth = DATEPART(DAY, @CurrentDate)
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat monthly - every on the first,second,third,fourth, last TuesDay  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 2									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NULL
			AND ( 
				(IntervalOrdinalNumber != -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 = Sch.IntervalOrdinalNumber )
				OR 
				(IntervalOrdinalNumber = -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 >= 4 )
			)
			AND DATEPART(WEEKDAY, @CurrentDate) = Sch.IntervalOrdinalNumberInDayOfWeek
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat yearly - every on the day  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 3									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate 
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInMonthOfYear = MONTH(@CurrentDate)
			AND Sch.IntervalInDateOfMonth IS NOT NULL
			AND Sch.IntervalInDateOfMonth = DATEPART(DAY, @CurrentDate)
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat yearly - every on the first,second,third,fourth, last TuesDay  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 3								-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate 
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NULL
			AND Sch.IntervalInMonthOfYear = MONTH(@CurrentDate)
			AND ( 
				(IntervalOrdinalNumber != -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 = Sch.IntervalOrdinalNumber )
				OR 
				(IntervalOrdinalNumber = -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 >= 4 )
			)
			AND DATEPART(WEEKDAY, @CurrentDate) = Sch.IntervalOrdinalNumberInDayOfWeek
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		-------------------------------------------------------------------------------------------------
	) AS Results

	RETURN 
END

GO
/****** Object:  UserDefinedFunction [dbo].[Func_SplitTextToTable]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 11/8/2011
-- Description:	Cắt chuỗi đầu vào với ký tự cắt ra thành một bảng dữ liệu tương ứng
-- =============================================
CREATE FUNCTION [dbo].[Func_SplitTextToTable]
(
	-- chuỗi cần cắt
	@String nvarchar(MAX), 
	-- ký tự cắt
	@Delimiter char(1)
)      
returns @temptable TABLE (items nvarchar(4000))      
as      
begin      
    declare @idx int      
    declare @slice nvarchar(4000)      
      
    select @idx = 1      
        if len(@String)<1 or @String is null  return      
      
    while @idx!= 0      
    begin      
        set @idx = charindex(@Delimiter,@String)      
        if @idx!=0      
            set @slice = left(@String,@idx - 1)      
        else      
            set @slice = @String      
          
        if(len(@slice)>0) 
            insert into @temptable(Items) values(@slice)      
  
        set @String = right(@String,len(@String) - @idx)      
        if len(@String) = 0 break      
    end  
return      
end





GO
/****** Object:  UserDefinedFunction [dbo].[Func_SplitTextToTable_WithRowNumber]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[Func_SplitTextToTable_WithRowNumber]
(
	-- chuỗi cần cắt
	@String nvarchar(MAX), 
	-- ký tự cắt
	@Delimiter char(1)
)      
returns @temptable TABLE (STT int,items nvarchar(4000))      
as      
begin      
    declare @idx int      
    declare @slice nvarchar(4000)      
      
    select @idx = 1      
        if len(@String)<1 or @String is null  return      
      
    while @idx!= 0      
    begin      
        set @idx = charindex(@Delimiter,@String)      
        if @idx!=0      
            set @slice = left(@String,@idx - 1)      
        else      
            set @slice = @String      
          
        if(len(@slice)>0) 
			begin
				declare @Count int  = (select Count(Items) from @temptable);
				insert into @temptable( STT,Items) values(@Count +1 , @slice)      
            end
  
        set @String = right(@String,len(@String) - @idx)      
        if len(@String) = 0 break      
    end  
return      
end


GO
/****** Object:  UserDefinedFunction [dbo].[Func_StripHTMLV2]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Func_StripHTMLV2] (@HTMLText NVARCHAR(MAX))
RETURNS NVARCHAR(MAX) AS
BEGIN
    DECLARE @Start INT
    DECLARE @End INT
    DECLARE @Length INT
    SET @Start = CHARINDEX('<',@HTMLText)
    SET @End = CHARINDEX('>',@HTMLText,CHARINDEX('<',@HTMLText))
    SET @Length = (@End - @Start) + 1
    WHILE @Start > 0 AND @End > 0 AND @Length > 0
    BEGIN
        SET @HTMLText = STUFF(@HTMLText,@Start,@Length,'')
        SET @Start = CHARINDEX('<',@HTMLText)
        SET @End = CHARINDEX('>',@HTMLText,CHARINDEX('<',@HTMLText))
        SET @Length = (@End - @Start) + 1
    END
	DECLARE @ResultVar NVARCHAR(MAX)  = LTRIM(RTRIM(REPLACE(REPLACE(ISNULL(@HTMLText, ''), CHAR(13), ''), CHAR(10)+CHAR(10), ' ')));
	
	SET @ResultVar = REPLACE(@ResultVar, N'&#273;', N'đ');
	SET @ResultVar = REPLACE(@ResultVar, N'&agrave;', N'à');
	SET @ResultVar = REPLACE(@ResultVar, N'&egrave;', N'è');
	SET @ResultVar = REPLACE(@ResultVar, N'&eacute;', N'é');
	SET @ResultVar = REPLACE(@ResultVar, N'&iacute;', N'í');
	SET @ResultVar = REPLACE(@ResultVar, N'&ograve;', N'ò');
	SET @ResultVar = REPLACE(@ResultVar, N'&oacute;', N'ó');
	SET @ResultVar = REPLACE(@ResultVar, N'&uacute;', N'ú');
	SET @ResultVar = REPLACE(@ResultVar, N'&aacute;', N'á');
	SET @ResultVar = REPLACE(@ResultVar, N'&yacute;', N'ý');
	SET @ResultVar = REPLACE(@ResultVar, N'&otilde;', N'õ');
	SET @ResultVar = REPLACE(@ResultVar, N'&acirc;', N'â');
	SET @ResultVar = REPLACE(@ResultVar, N'&ecirc;', N'ê');
	SET @ResultVar = REPLACE(@ResultVar, N'&ocirc;', N'ô');
	SET @ResultVar = REPLACE(@ResultVar, N'&atilde;', N'ã');
	SET @ResultVar = REPLACE(@ResultVar, N'&igrave;', N'ì');

	SET @ResultVar = REPLACE(@ResultVar, N'&#272;', N'Đ');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Agrave;', N'À');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Egrave;', N'È');
	SET @ResultVar = REPLACE(@ResultVar, N'&Eacute;', N'É');
	SET @ResultVar = REPLACE(@ResultVar, N'&Iacute;', N'Í');
	SET @ResultVar = REPLACE(@ResultVar, N'&Ograve;', N'Ò');
	SET @ResultVar = REPLACE(@ResultVar, N'&Oacute;', N'Ó');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Uacute;', N'Ú');	
	SET @ResultVar = REPLACE(@ResultVar, N'&middot;', N'·');
	SET @ResultVar = REPLACE(@ResultVar, N'&Aacute;', N'Á');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Yacute;', N'Ý');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Otilde;', N'Õ');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Acirc;', N'Â');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Ecirc;', N'Ê');	
	SET @ResultVar = REPLACE(@ResultVar, N'&Ocirc;', N'Ô');	
	SET @ResultVar = REPLACE(@ResultVar, N'&#296;', N'Ĩ');
	SET @ResultVar = REPLACE(@ResultVar, N'&Atilde;', N'Ã');
	SET @ResultVar = REPLACE(@ResultVar, N'&nbsp;', N' ');
	SET @ResultVar = REPLACE(@ResultVar, N'&amp;', N'&');
	SET @ResultVar = REPLACE(@ResultVar, N'&lt;', N'<');
	SET @ResultVar = REPLACE(@ResultVar, N'&gt;', N'>');
	SET @ResultVar = REPLACE(@ResultVar, N'&Igrave;', N'Ì');


    RETURN @ResultVar


END

GO
/****** Object:  UserDefinedFunction [dbo].[NullToEmpty]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[NullToEmpty]
(
	@Value NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @RetValues NVARCHAR(MAX) = @Value;
	IF (@RetValues IS NULL)
		SET	@RetValues = '';
	
	RETURN @RetValues;
END

GO
/****** Object:  UserDefinedFunction [dbo].[Udf_Get_Process_Class_Grantt_View_By_Task]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ntchien
-- Create date: 2020-07-15
-- Description:	Get UserId
-- =============================================
CREATE   FUNCTION [dbo].[Udf_Get_Process_Class_Grantt_View_By_Task] 
(
	-- Add the parameters for the function here
	@ProjectId uniqueidentifier,
	@ProjectFromDate datetime, 
	@ProjectToDate datetime,
	@TaskFromDate datetime ,
	 @TaskToDate datetime,
	@TaskItemId uniqueidentifier
)
RETURNS uniqueidentifier
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultL VARCHAR(50) ='0',
			@ResultR VARCHAR(50) ='0',
			@PercentLeftTemp nvarchar(50),
			@PercentRightTemp nvarchar(50),
			@PercentLeft nvarchar(50),
		    @PercentRight nvarchar(50),
			@TotalDay int;

	if( @TaskFromDate <> null and @TaskToDate <> null )	
	begin	
		SELECT @TotalDay =DATEDIFF(day,@ProjectToDate,@ProjectFromDate);

		if @TotalDay <> 0
		begin
			SELECT @PercentLeftTemp =((DATEDIFF(day,@ProjectFromDate,@TaskFromDate)) /@TotalDay) * 100;
			SELECT @PercentRightTemp = ((DATEDIFF(day,@ProjectToDate,@TaskToDate))/@TotalDay) *100;
		End

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

									ELSE '0'
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
					

			end

		end
	-- Add the T-SQL statements to compute the return value here

	-- Return the result of the function
	RETURN 'L'+@ResultL+'R'+@ResultR

END






GO
/****** Object:  UserDefinedFunction [Task].[Udf_Get_Process_Class_Grantt_View_By_Task]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		ntchien
-- Create date: 2021-02-24
-- Description:	Get UserId
-- =============================================
CREATE FUNCTION [Task].[Udf_Get_Process_Class_Grantt_View_By_Task] 
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
/****** Object:  UserDefinedFunction [Task].[Udf_Get_UserIdAssign_By_TaskType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Task].[Udf_Get_UserIdAssign_By_TaskType] 
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





GO
/****** Object:  Table [dbo].[Departments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[NoUser] [int] NULL,
	[OrderNumber] [int] NULL,
	[ParentID] [uniqueidentifier] NULL,
	[Path] [nvarchar](260) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[AuthorID] [uniqueidentifier] NOT NULL,
	[IsShow] [bit] NOT NULL,
	[DeptTypeID] [uniqueidentifier] NOT NULL,
	[EditorID] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[ServerAddress] [nvarchar](100) NOT NULL,
	[DatabaseName] [nvarchar](100) NULL,
	[UserAccess] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Code] [nvarchar](100) NULL,
	[RootDBID] [uniqueidentifier] NULL,
	[IsPrint] [bit] NULL,
	[Supervisor] [nvarchar](max) NULL,
	[DeptIndex] [varchar](20) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[Func_SelectRecursiveDeptId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Func_SelectRecursiveDeptId]
(	
	@ParentId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	  WITH  CTE_Dept
                        AS
                        (
	                        SELECT ID, Name
	                        FROM Departments
	                        WHERE 
		                        ID = @ParentId 
		                        AND IsActive = 1  
		                        AND IsShow = 1
	                        UNION ALL
	                        SELECT D.ID, D.Name
	                        FROM
		                         Departments D
		                        INNER JOIN CTE_Dept CD ON CD.ID = D.ParentID
	                        WHERE
		                        D.IsActive = 1
		                        AND D.IsShow = 1

                        )

                        SELECT ID
                        FROM CTE_Dept
)

GO
/****** Object:  UserDefinedFunction [dbo].[Func_SelectRecursiveReversveDeptId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Func_SelectRecursiveReversveDeptId]
(	
	@ParentId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	  WITH  CTE_Dept
    AS
    (
	    SELECT ID, Name, ParentID
	    FROM Departments
	    WHERE 
		    ID = @ParentId 
		    AND IsActive = 1  
		    AND IsShow = 1
	    UNION ALL
	    SELECT D.ID, D.Name, D.ParentID
	    FROM
		        Departments D
		    INNER JOIN CTE_Dept CD ON CD.ParentID = D.ID
	    WHERE
		    D.IsActive = 1
		    AND D.IsShow = 1

    )
    SELECT DISTINCT ID
    FROM CTE_Dept
)

GO
/****** Object:  Table [dbo].[JobTitles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitles](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[OrderNumber] [int] NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_JobTitles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDepartments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDepartments](
	[UserID] [uniqueidentifier] NOT NULL,
	[DeptID] [uniqueidentifier] NOT NULL,
	[JobDescription] [nvarchar](150) NULL,
	[OrderNumber] [int] NULL,
	[JobTitleID] [uniqueidentifier] NULL,
	[IsManager] [bit] NULL,
 CONSTRAINT [PK_UserDepartments] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[FullName] [nvarchar](300) NULL,
	[FirstName] [nvarchar](150) NULL,
	[LastName] [nvarchar](150) NULL,
	[Gender] [int] NULL,
	[Email] [nvarchar](150) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[HomePhone] [nvarchar](50) NULL,
	[Ext] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[UserCode] [nvarchar](100) NULL,
	[Avartar] [varbinary](max) NULL,
	[LanguageCulture] [nvarchar](10) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)),
	[UserIndex] [varchar](20) NULL,
	[URL] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[AccountName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[View_UserDept]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_UserDept]
AS
SELECT        UD.DeptID, D.Name AS DeptName, UD.UserID, U.UserName, U.FullName, JT.ID AS JtID, JT.Name AS JtName, UD.JobDescription, D.DatabaseName, D.OrderNumber, UD.OrderNumber AS Expr1
FROM            dbo.Departments AS D INNER JOIN
                         dbo.UserDepartments AS UD ON UD.DeptID = D.ID INNER JOIN
                         dbo.Users AS U ON U.ID = UD.UserID LEFT OUTER JOIN
                         dbo.JobTitles AS JT ON JT.ID = UD.JobTitleID

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[ID] [uniqueidentifier] NOT NULL,
	[DocNumber] [int] NULL,
	[SerialNumber] [nvarchar](150) NULL,
	[Summary] [nvarchar](max) NULL,
	[Content] [ntext] NULL,
	[DocDate] [datetime] NULL,
	[SignedBy] [nvarchar](250) NULL,
	[Sender] [nvarchar](2000) NULL,
	[Receiver] [nvarchar](max) NULL,
	[InternalReceiver] [xml] NULL,
	[CarbonCopy] [xml] NULL,
	[Pages] [int] NULL,
	[Appendix] [int] NULL,
	[Status] [int] NULL,
	[IsInComing] [bit] NULL,
	[IsExternal] [bit] NULL,
	[ExpiredDate] [datetime] NULL,
	[Approved] [datetime] NULL,
	[ApprovedBy] [uniqueidentifier] NULL,
	[PublishedDate] [datetime] NULL,
	[PublishedBy] [uniqueidentifier] NULL,
	[ReceivedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[DocType] [uniqueidentifier] NULL,
	[Secret] [uniqueidentifier] NULL,
	[Priority] [uniqueidentifier] NULL,
	[SendProvider] [int] NULL,
	[DocBook] [uniqueidentifier] NULL,
	[EditDepartment] [uniqueidentifier] NULL,
	[WorkFlowStepID] [uniqueidentifier] NULL,
	[WorkFlowInfos] [xml] NULL,
	[IsBroadCast] [nchar](10) NULL,
	[ExtensiveInfo] [xml] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Scope] [int] NULL,
	[IsJob] [bit] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriorityDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriorityDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MapingID] [int] NOT NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PriorityDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SecretDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecretDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MapingID] [int] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SecretDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusDocuments](
	[ID] [int] NOT NULL,
	[Name] [nchar](50) NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_StatusDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[View_ListDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_ListDocument]
AS
SELECT        dbo.Documents.ID AS DocID, dbo.Documents.DocNumber, dbo.Documents.SerialNumber, dbo.Documents.Summary, dbo.Documents.DocDate, dbo.Documents.SignedBy, dbo.Documents.Sender, dbo.Documents.Status, 
                         dbo.Documents.IsInComing, dbo.Documents.IsExternal, dbo.Documents.ApprovedBy, dbo.Documents.Approved, dbo.PriorityDocuments.MapingID AS PriorityMaping, dbo.SecretDocuments.MapingID AS SecretMaping, 
                         dbo.PriorityDocuments.Name AS PriorityName, dbo.SecretDocuments.Name AS SecretName, dbo.StatusDocuments.Code AS StatusCode, dbo.StatusDocuments.ID AS StatusID, dbo.Documents.Created, 
                         dbo.Documents.Modified
FROM            dbo.Documents INNER JOIN
                         dbo.StatusDocuments ON dbo.Documents.Status = dbo.StatusDocuments.ID INNER JOIN
                         dbo.PriorityDocuments ON dbo.Documents.Priority = dbo.PriorityDocuments.ID INNER JOIN
                         dbo.SecretDocuments ON dbo.Documents.Secret = dbo.SecretDocuments.ID

GO
/****** Object:  UserDefinedFunction [dbo].[Func_GetColumnPlanningType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Func_GetColumnPlanningType]
( 
	@ExtensiveInfo XML
)
RETURNS NVARCHAR(100)
WITH SCHEMABINDING
BEGIN
	DECLARE @ResultVar NVARCHAR(100);

	SET @ResultVar = @ExtensiveInfo.value ('(/Columns/Column[@name="PlanningType"])[1]', 'NVARCHAR(100)');
 
	-- Return the result of the function
	RETURN @ResultVar;

END

GO
/****** Object:  Table [BusinessWorkflow].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[Annex]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[Annex](
	[Id] [uniqueidentifier] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Document_Id] [uniqueidentifier] NOT NULL,
	[Revision] [int] NOT NULL,
	[HistoryId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[DeleteHistoryId] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[Action] [int] NOT NULL,
	[AttachmentId] [uniqueidentifier] NULL,
	[Deleted] [datetime] NULL,
	[IsSignCa] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.Annex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[Attachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[Attachment](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Revision] [int] NULL,
	[Path] [nvarchar](max) NULL,
	[Document_Id] [uniqueidentifier] NOT NULL,
	[Content] [varbinary](max) NOT NULL,
	[AnnexSortOrder] [int] NULL,
	[AuthorId] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[EditorId] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[Size] [bigint] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[Book]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[Book](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Issuers] [nvarchar](max) NULL,
	[Authorize] [nvarchar](max) NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[IsReset] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[BookDocumentType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[BookDocumentType](
	[Id] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NULL,
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
	[SerialFormat] [nvarchar](max) NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.BookDocumentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate](
	[BookDocumentType_Id] [uniqueidentifier] NOT NULL,
	[WorkflowTemplate_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.BookDocumentTypeWorkflowTemplate] PRIMARY KEY CLUSTERED 
(
	[BookDocumentType_Id] ASC,
	[WorkflowTemplate_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[BookNumber]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[BookNumber](
	[Id] [uniqueidentifier] NOT NULL,
	[Year] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Book_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.BookNumber] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[Document]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[Document](
	[Id] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NULL,
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Serial] [nvarchar](100) NOT NULL,
	[CurrentStepId] [uniqueidentifier] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[IsPromulgate] [bit] NULL,
	[InternalReceiver] [nvarchar](max) NOT NULL,
	[DocSetId] [uniqueidentifier] NULL,
	[ExtensiveFields] [nvarchar](max) NULL,
	[OrderNumber] [nvarchar](256) NULL,
	[OrderDate] [datetime] NULL,
	[SendOrderNumber] [nvarchar](max) NULL,
	[Revision] [int] NULL,
	[LinkedDocuments] [varchar](max) NULL,
	[CurrentStepOrder] [int] NOT NULL,
	[CurrentStepName] [nvarchar](255) NULL,
	[HasSignCaAttachment] [bit] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[PublishDocumentUrl] [nvarchar](500) NULL,
	[AllowComment] [bit] NULL,
 CONSTRAINT [PK_BusinessWorkflow.Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[DocumentLevel]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentLevel](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.DocumentLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[DocumentTemplate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentTemplate](
	[Id] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[BookDocumentType_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.DocumentTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[DocumentType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[PermissionForDepartmentIds] [varchar](max) NULL,
	[OrderNumber] [int] NULL,
 CONSTRAINT [PK_BusinessWorkflow.DocumentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[DocumentTypeAttachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentTypeAttachment](
	[Id] [uniqueidentifier] NOT NULL,
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DefaultName] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [varbinary](max) NULL,
	[UniqueName] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[Size] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentTypeDetail_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[DocumentTypeField]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentTypeField](
	[Id] [uniqueidentifier] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[DefaultValue] [nvarchar](max) NULL,
	[ShowOnList] [bit] NOT NULL,
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.DocumentTypeField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[DocumentTypeSupervisor]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[DocumentTypeSupervisor](
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
	[SupervisorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.DocumentTypeSupervisor] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeId] ASC,
	[SupervisorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[FilterParam]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[FilterParam](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NOT NULL,
	[ParamValue] [nvarchar](max) NULL,
	[IsCount] [bit] NOT NULL,
	[ShowMobile] [bit] NOT NULL,
	[IsPersonal] [bit] NOT NULL,
	[NoOrder] [int] NOT NULL,
	[Permissions] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
	[IsPrivateFolder] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.FilterParam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[FilterParamPermissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[FilterParamPermissions](
	[Id] [uniqueidentifier] NOT NULL,
	[FilterParamId] [uniqueidentifier] NOT NULL,
	[UserOrRoleId] [uniqueidentifier] NOT NULL,
	[IsAllow] [bit] NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.FilterParamPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[FolderDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[FolderDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[FolderID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[AuthorID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.FolderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[History]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[History](
	[Id] [uniqueidentifier] NOT NULL,
	[DocumentId] [uniqueidentifier] NOT NULL,
	[WorkflowStepId] [uniqueidentifier] NOT NULL,
	[WorkflowTaskId] [uniqueidentifier] NOT NULL,
	[Log] [nvarchar](max) NULL,
	[Reason] [nvarchar](max) NULL,
	[Action] [int] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Revision] [int] NULL,
 CONSTRAINT [PK_BusinessWorkflow.History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[Logger]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[Logger](
	[Id] [uniqueidentifier] NOT NULL,
	[Time] [datetime] NULL,
	[Url] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](max) NULL,
	[Browser] [nvarchar](max) NULL,
	[IsMobileDevice] [bit] NULL,
	[Controller] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[Data] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[ObjectId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BusinessWorkflow.Logger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[Otp]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[Otp](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](4) NOT NULL,
	[Created] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[Role]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Code] [nvarchar](300) NULL,
 CONSTRAINT [PK_BusinessWorkflow.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[SignArea]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[SignArea](
	[Id] [uniqueidentifier] NOT NULL,
	[SignerId] [uniqueidentifier] NOT NULL,
	[Page] [int] NOT NULL,
	[HasSignature] [bit] NOT NULL,
	[HasStamp] [bit] NOT NULL,
	[HasInformation] [bit] NOT NULL,
	[CoordinateX] [int] NOT NULL,
	[CoordinateY] [int] NOT NULL,
	[Attachment_Id] [uniqueidentifier] NOT NULL,
	[Operation] [int] NULL,
 CONSTRAINT [PK_BusinessWorkflow.SignArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[UserRole]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[UserRole](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[AssignToId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowStep]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowStep](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Protocol] [int] NOT NULL,
	[ReturnToStep] [uniqueidentifier] NOT NULL,
	[NextStep] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Document_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowStep] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTask](
	[Id] [uniqueidentifier] NOT NULL,
	[AssignTo] [uniqueidentifier] NOT NULL,
	[Operation] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[IsSignCa] [bit] NOT NULL,
	[Order] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[WorkflowStep_Id] [uniqueidentifier] NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[ParentTask] [uniqueidentifier] NOT NULL,
	[IsWaitSecretary] [bit] NOT NULL,
	[Processed] [datetime] NULL,
	[AssignById] [uniqueidentifier] NULL,
	[StartDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[Action] [int] NULL,
	[DelegatedUserId] [uniqueidentifier] NULL,
	[HasAttachment] [bit] NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTaskAttachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTaskAttachment](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[Content] [varbinary](max) NOT NULL,
	[WorkflowTask_Id] [uniqueidentifier] NOT NULL,
	[Size] [bigint] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTaskAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTemplate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTemplate](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Editor] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[IsPersonal] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTemplateAssociation]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTemplateAssociation](
	[Id] [uniqueidentifier] NOT NULL,
	[WorkflowTemplateId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[DocumentTypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTemplateAssociation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTemplateSecretary]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTemplateSecretary](
	[WorkflowTemplateTaskId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTemplateSecretary] PRIMARY KEY CLUSTERED 
(
	[WorkflowTemplateTaskId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTemplateStep]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTemplateStep](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Protocol] [int] NOT NULL,
	[ReturnToStep] [uniqueidentifier] NOT NULL,
	[NextStep] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[WorkflowTemplate_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTemplateStep] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [BusinessWorkflow].[WorkflowTemplateTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BusinessWorkflow].[WorkflowTemplateTask](
	[Id] [uniqueidentifier] NOT NULL,
	[AssignToMode] [int] NULL,
	[AssignTo] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[Operation] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[IsSignCa] [bit] NOT NULL,
	[Order] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[WorkflowTemplateStep_Id] [uniqueidentifier] NOT NULL,
	[AssignToType] [int] NOT NULL,
 CONSTRAINT [PK_BusinessWorkflow.WorkflowTemplateTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Chat].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Chat].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Chat.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Chat].[Attachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Chat].[Attachment](
	[Id] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[Content] [varbinary](max) NULL,
	[Size] [bigint] NOT NULL,
	[FileExt] [nvarchar](10) NULL,
	[Path] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat.Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Chat].[Chat]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Chat](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[BeforeChange] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[TypeData] [int] NOT NULL,
 CONSTRAINT [PK_Chat.Chat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Chat].[Interactive]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Interactive](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NULL,
	[Action] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat.Interactive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Chat].[Member]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Member](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[TimeDueDisableNotification] [datetime] NULL,
	[NumberOfNotices] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat.Member] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Chat].[Room]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Room](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat.Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Chat].[RoomSetting]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[RoomSetting](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat.RoomSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Action]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Action](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](4000) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Action] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Alert]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Alert](
	[Id] [uniqueidentifier] NOT NULL,
	[StartTime] [datetime] NULL,
	[AlertTime] [datetime] NULL,
	[AlertStatusId] [uniqueidentifier] NULL,
	[FolderId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Alert] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[AlertStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[AlertStatus](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.AlertStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Category]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[ConfigColumnType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[ConfigColumnType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [datetime] NULL,
	[URL] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Contract.ConfigColumnType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Contract].[ConfigFolderCustom]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[ConfigFolderCustom](
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NULL,
	[Name] [nvarchar](4000) NULL,
	[ColumnName] [nvarchar](4000) NULL,
	[DisplayName] [nvarchar](4000) NULL,
	[ColumnTypeId] [uniqueidentifier] NULL,
	[TableRefName] [nvarchar](max) NULL,
	[HaveRef] [bit] NULL,
	[HtmlView] [int] NULL,
	[HtmlAdd] [int] NULL,
	[HtmlEdit] [int] NULL,
	[Number] [int] NULL,
	[IsActive] [bit] NULL,
	[URL] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Contract.ConfigFolderCustom] PRIMARY KEY CLUSTERED 
(
	[ActiveFag] ASC,
	[CreatedBy] ASC,
	[CreatedDate] ASC,
	[ModifiedBy] ASC,
	[ModifiedDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Document]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Document](
	[Id] [uniqueidentifier] NOT NULL,
	[FolderCategoryId] [uniqueidentifier] NULL,
	[DocumentTypeId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[DocumentFile]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Contract].[DocumentFile](
	[FileContent] [varbinary](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Extension] [nvarchar](255) NULL,
	[DocumentId] [uniqueidentifier] NULL,
	[SiteURL] [nvarchar](4000) NULL,
	[IsOnline] [bit] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.DocumentFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Contract].[DocumentProcess]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[DocumentProcess](
	[Id] [uniqueidentifier] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[ActionId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[DocumentId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](255) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[URL] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Contract.DocumentProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Contract].[DocumentTemplate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Contract].[DocumentTemplate](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](4000) NULL,
	[FileName] [nvarchar](255) NULL,
	[FileContent] [varbinary](max) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.DocumentTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Contract].[DocumentType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[DocumentType](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](255) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.DocumentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Folder]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Folder](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[FolderBiddingFiledId] [uniqueidentifier] NULL,
	[FolderFormBiddingId] [uniqueidentifier] NULL,
	[FolderFormSelectionId] [uniqueidentifier] NULL,
	[FolderScopePerformId] [uniqueidentifier] NULL,
	[FolderTypeId] [uniqueidentifier] NULL,
	[SoQuyetDinhDuToan] [nvarchar](4000) NULL,
	[NgayQuyetDinhDuToan] [datetime] NULL,
	[GiaDuToan] [money] NULL,
	[SoQuyetDinhKQ] [nvarchar](4000) NULL,
	[NgayQuyetDinhKQ] [datetime] NULL,
	[GiaTrungThau] [money] NULL,
	[GiaChenhLech] [money] NULL,
	[TenNhaThau] [nvarchar](4000) NULL,
	[NgayKyHD] [datetime] NULL,
	[ThoiGianTHHD] [int] NULL,
	[NgayKetThucHD] [datetime] NULL,
	[GiaTriPhuLucHD] [nvarchar](4000) NULL,
	[SoBBNghiemThu] [nvarchar](4000) NULL,
	[NgayKyBBNghiemThu] [datetime] NULL,
	[NgayKyBBThanhLy] [datetime] NULL,
	[SoBBThanhLy] [nvarchar](255) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Folder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderBiddingFiled]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderBiddingFiled](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](255) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderBiddingFiled] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderCategory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[OrderNumber] [int] NOT NULL,
	[FolderId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderCustom]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderCustom](
	[Id] [uniqueidentifier] NOT NULL,
	[Backup1] [nvarchar](max) NULL,
	[Backup2] [nvarchar](max) NULL,
	[Backup3] [nvarchar](max) NULL,
	[Backup4] [nvarchar](max) NULL,
	[Backup5] [nvarchar](max) NULL,
	[Backup6] [nvarchar](max) NULL,
	[Backup7] [nvarchar](max) NULL,
	[Backup8] [nvarchar](max) NULL,
	[Backup9] [nvarchar](max) NULL,
	[Backup10] [nvarchar](max) NULL,
	[Backup11] [nvarchar](max) NULL,
	[Backup12] [nvarchar](max) NULL,
	[Backup13] [nvarchar](max) NULL,
	[Backup14] [nvarchar](max) NULL,
	[Backup15] [nvarchar](max) NULL,
	[Backup16] [nvarchar](max) NULL,
	[Backup17] [nvarchar](max) NULL,
	[Backup18] [nvarchar](max) NULL,
	[Backup19] [nvarchar](max) NULL,
	[Backup20] [nvarchar](max) NULL,
	[Folder_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Contract.FolderCustom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderFormBidding]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderFormBidding](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](255) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderFormBidding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderFormSelection]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderFormSelection](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderFormSelection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderGroup]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Icon] [nvarchar](255) NULL,
	[Version] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderScopePerform]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderScopePerform](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](255) NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderScopePerform] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[FolderType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[FolderType](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.FolderType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Permission]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Permission](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NULL,
	[Code] [nvarchar](4000) NULL,
	[Description] [nvarchar](4000) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[URL] [nvarchar](4000) NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](255) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Project]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[FolderGroupId] [uniqueidentifier] NULL,
	[ProjectTypeId] [uniqueidentifier] NULL,
	[ProjectFormatId] [uniqueidentifier] NULL,
	[Version] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[ProjectFormat]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[ProjectFormat](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.ProjectFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[ProjectType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[ProjectType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.ProjectType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[Status]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[Status](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Contract].[TrackingUpdateDB]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Contract].[TrackingUpdateDB](
	[EventDate] [datetime] NOT NULL,
	[EventType] [nvarchar](100) NULL,
	[EventDDL] [nvarchar](max) NULL,
	[DatabaseName] [nvarchar](255) NULL,
	[SchemaName] [nvarchar](255) NULL,
	[ObjectName] [nvarchar](255) NULL,
	[HostName] [nvarchar](255) NULL,
	[IPAddress] [varchar](32) NULL,
	[ProgramName] [nvarchar](255) NULL,
	[LoginName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Contract.TrackingUpdateDB] PRIMARY KEY CLUSTERED 
(
	[EventDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Contract].[UserPermission]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Contract].[UserPermission](
	[Id] [uniqueidentifier] NOT NULL,
	[FolderGroupId] [uniqueidentifier] NULL,
	[FolderId] [uniqueidentifier] NULL,
	[FolderCategoryId] [uniqueidentifier] NULL,
	[DocumentId] [uniqueidentifier] NULL,
	[UserName] [nvarchar](255) NULL,
	[PermissionId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contract.UserPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Core].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Core].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Core.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Core].[DocumentSetDetails]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[DocumentSetDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[DocumentSetId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[ModuleCode] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DocumentSetDetails_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[DocumentSets]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[DocumentSets](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Created] [datetime] NOT NULL,
	[Author] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[SerialNumber] [nvarchar](50) NOT NULL,
	[Number] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DocumentSets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[Notifications]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Core].[Notifications](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleCode] [nvarchar](100) NOT NULL,
	[AdditionalData] [varbinary](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[NotificationTypeId] [uniqueidentifier] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[RecipientId] [uniqueidentifier] NOT NULL,
	[SenderId] [uniqueidentifier] NULL,
	[Url] [nvarchar](2000) NULL,
	[Subject] [nvarchar](2000) NULL,
	[Body] [nvarchar](max) NULL,
	[SenderEmail] [nvarchar](300) NULL,
	[RecipientEmail] [nvarchar](3000) NULL,
	[CcEmail] [nvarchar](3000) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_core.Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Core].[NotificationSettings]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[NotificationSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsUrgent] [bit] NOT NULL,
	[NotificationTypeId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_core.NotificationSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[NotificationTypes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[NotificationTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Description] [nvarchar](2000) NULL,
	[Template] [nvarchar](max) NULL,
	[ModuleCode] [nvarchar](100) NOT NULL,
	[ActionType] [int] NOT NULL,
	[ActionTypeName] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_core.NotificationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Core].[Schedule]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[Schedule](
	[ModuleId] [uniqueidentifier] NOT NULL,
	[ObjectId] [nvarchar](128) NOT NULL,
	[ScheduleType] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[IntervalFrequently] [int] NOT NULL,
	[IntervalInWeekday] [bit] NOT NULL,
	[IntervalInDayOfWeek] [nvarchar](128) NULL,
	[IntervalInDateOfMonth] [int] NULL,
	[IntervalInMonthOfYear] [int] NULL,
	[IntervalOrdinalNumber] [int] NULL,
	[IntervalOrdinalNumberInDayOfWeek] [int] NULL,
 CONSTRAINT [PK_Core.Schedule] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC,
	[ObjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[SignAreas]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[SignAreas](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Template] [nvarchar](500) NULL,
	[CoordinateX] [int] NULL,
	[CoordinateY] [int] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[OrderNumber] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK__SignArea__3214EC073F457C07] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[Signature]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Core].[Signature](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Certificate] [varbinary](max) NULL,
	[SignatureImage] [varbinary](max) NULL,
	[SignatureImageType] [nvarchar](50) NULL,
	[StampImage] [varbinary](max) NULL,
	[StampImageType] [nvarchar](50) NULL,
	[Email] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Otp] [nvarchar](4) NULL,
	[OtpCreated] [datetime] NULL,
	[SignServerId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CertificateValidFrom] [datetime] NULL,
	[CertificateValidTo] [datetime] NULL,
	[CertificateSerialNumber] [varchar](max) NULL,
	[IsOwnedByPersonal] [bit] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[UserIdsInDepartment] [varchar](max) NULL,
	[IsValid] [bit] NULL,
	[Status] [tinyint] NULL,
	[URL] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[SignatureImageSmall] [varbinary](max) NULL,
	[SignatureImageSmallType] [nvarchar](50) NULL,
	[Type] [tinyint] NULL,
	[IsAync] [bit] NULL,
 CONSTRAINT [PK__Signatur__3214EC07FF23A0A5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Core].[SignServer]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[SignServer](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Url] [nvarchar](500) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[ClientId] [nvarchar](500) NULL,
	[ClientSecret] [nvarchar](500) NULL,
	[OtpRequired] [bit] NULL,
	[OtpInterval] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Core].[UserOtp]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[UserOtp](
	[UserId] [uniqueidentifier] NOT NULL,
	[OtpType] [nchar](10) NOT NULL,
	[PIN] [nvarchar](50) NULL,
	[OtpTimeLife] [int] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[OtpCode] [nvarchar](10) NULL,
	[OtpExpired] [datetime] NULL,
	[OtpSendStatus] [int] NULL,
	[OtpSendTime] [datetime] NULL,
	[UserChangeOtpType] [bit] NULL,
 CONSTRAINT [PK_UserOtp] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[ID] [uniqueidentifier] NOT NULL,
	[ItemID] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](300) NULL,
	[FileExt] [nvarchar](10) NULL,
	[FileContent] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Source] [int] NULL,
 CONSTRAINT [PK_Attachment_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AudienceNav]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AudienceNav](
	[ID] [uniqueidentifier] NOT NULL,
	[NavNodeID] [uniqueidentifier] NOT NULL,
	[AudienceID] [uniqueidentifier] NOT NULL,
	[AudienceType] [varchar](10) NOT NULL,
 CONSTRAINT [PK_AudienceNav] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AuditWebAccess]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AuditWebAccess](
	[DBName] [nvarchar](50) NULL,
	[OrgName] [nvarchar](250) NULL,
	[FullName] [nvarchar](150) NULL,
	[UserName] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[DeviceName] [nvarchar](50) NULL,
	[Version] [varchar](50) NULL,
	[Url] [nvarchar](2000) NULL,
	[DeviceType] [nvarchar](100) NULL,
	[OLevel] [bigint] NULL,
	[ULevel] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[OrgID] [uniqueidentifier] NOT NULL,
	[MainTitle] [nvarchar](500) NULL,
	[SubTitle] [nvarchar](500) NULL,
	[Template] [nvarchar](max) NULL,
 CONSTRAINT [PK_BillDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookDocTypeDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookDocTypeDocuments](
	[BookID] [uniqueidentifier] NOT NULL,
	[DocumentTypeID] [uniqueidentifier] NOT NULL,
	[SerialNumberFormat] [nvarchar](100) NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_BookDocTypeDocuments] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[DocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BookDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Dept] [uniqueidentifier] NULL,
	[Name] [nvarchar](350) NULL,
	[Type] [bit] NULL,
	[IsExternal] [bit] NULL,
	[NoOrder] [int] NULL,
	[Permission] [nvarchar](2500) NULL,
	[IsActive] [bit] NULL,
	[PublishedBy] [nvarchar](2500) NULL,
	[LinkOrg] [varchar](max) NULL,
 CONSTRAINT [PK_BookDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookNumberDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookNumberDocuments](
	[BookID] [uniqueidentifier] NOT NULL,
	[BookYear] [int] NOT NULL,
	[BookNumber] [int] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BookNumberDocuments] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[BookYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookTrackingDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTrackingDocuments](
	[BookID] [uniqueidentifier] NOT NULL,
	[DocID] [uniqueidentifier] NOT NULL,
	[SerialNumber] [nvarchar](150) NULL,
	[DocNumber] [int] NULL,
	[YearInBook] [int] NULL,
	[BookType] [bit] NULL,
	[IsExternal] [bit] NULL,
 CONSTRAINT [PK_BookTrackingDocuments] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[DocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookWorkFlowDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookWorkFlowDocuments](
	[BookID] [uniqueidentifier] NOT NULL,
	[WorkFlowID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookWorkFlowDocuments] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[WorkFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Calendars]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendars](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Content] [nvarchar](500) NULL,
	[Location] [nvarchar](500) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AllDayEvent] [bit] NULL,
	[Organizer] [nvarchar](500) NULL,
	[RequiredAttandee] [nvarchar](1000) NULL,
	[OptionalAttandee] [nvarchar](1000) NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UserID] [uniqueidentifier] NULL,
	[DeptID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Calendars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CatalogActionDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogActionDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Keyword] [nvarchar](50) NULL,
	[JobTitleID] [uniqueidentifier] NULL,
	[Value] [nvarchar](150) NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CatalogActionDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategorizeDetailDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategorizeDetailDocuments](
	[CategorizeID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[LinkDocLib] [nvarchar](500) NULL,
	[IsMoveToDocLib] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CategorizeDetailDocuments] PRIMARY KEY CLUSTERED 
(
	[CategorizeID] ASC,
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategorizeDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategorizeDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[LinkDocLib] [nvarchar](500) NULL,
	[Permissions] [nvarchar](max) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[DocType] [int] NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CategorizeDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[ObjectID] [uniqueidentifier] NULL,
	[ModuleCode] [nvarchar](50) NULL,
	[Content] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsChange] [bit] NULL,
	[HistoryContent] [nvarchar](max) NULL,
	[TagUsers] [nvarchar](max) NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Email] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[IsContentRequired] [bit] NULL,
	[CustomerIndex] [nvarchar](10) NULL,
 CONSTRAINT [PK_CustomerDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeleteTransaction]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleteTransaction](
	[ID] [uniqueidentifier] NOT NULL,
	[DeleteDate] [datetime] NOT NULL,
	[ItemType] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DeleteTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepartmentTypes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_DepartmentTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeptPublishDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeptPublishDocuments](
	[DeptID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DeptPublishDocuments] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentConfigBackup]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentConfigBackup](
	[DBName] [nvarchar](200) NOT NULL,
	[Time] [int] NULL,
	[Login] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Server] [nvarchar](500) NULL,
	[BackupDBName] [nvarchar](200) NULL,
 CONSTRAINT [PK_DocumentConfigBackup] PRIMARY KEY CLUSTERED 
(
	[DBName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentStatistics]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentStatistics](
	[UserID] [nvarchar](50) NOT NULL,
	[Type] [nchar](10) NOT NULL,
	[TypeKey] [nvarchar](50) NOT NULL,
	[TotalDocs] [int] NULL,
 CONSTRAINT [PK_DocumentStatistics] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[Type] ASC,
	[TypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentTemplates]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumentTemplates](
	[ID] [uniqueidentifier] NOT NULL,
	[DocumentTypeID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[FileName] [nvarchar](500) NOT NULL,
	[FileType] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DocumentTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocumentTypes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NOT NULL,
	[EditorID] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NoDayProcess] [int] NULL,
 CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileComments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileComments](
	[ID] [uniqueidentifier] NOT NULL,
	[CommentID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Ext] [nvarchar](10) NULL,
	[Content] [varbinary](max) NULL,
 CONSTRAINT [PK_FileComments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileTrackingWorkflowDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileTrackingWorkflowDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[FileContent] [varbinary](max) NULL,
	[FileExt] [nvarchar](10) NULL,
	[FileName] [nvarchar](500) NULL,
	[TrackingWorkflowDocumentID] [uniqueidentifier] NULL,
	[TrackingDocumentID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_FileTrackingWorkflowDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FolderDetailDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolderDetailDocuments](
	[FolderID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_FolderDetailDocuments] PRIMARY KEY CLUSTERED 
(
	[FolderID] ASC,
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FolderDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolderDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](350) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
	[IsPersonal] [bit] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Permission] [nvarchar](max) NULL,
 CONSTRAINT [PK_FolderDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupEmail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupEmail](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Email] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_GroupEmail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupRoles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupRoles](
	[GroupID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_GroupRoles] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[RoleID] ASC,
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DepartmentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoryDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[DocID] [uniqueidentifier] NULL,
	[Action] [nvarchar](50) NULL,
	[Summary] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_HistoryDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[SelectedDate] [date] NOT NULL,
	[Created] [datetime] NOT NULL,
	[AuthorID] [uniqueidentifier] NOT NULL,
	[EditorID] [uniqueidentifier] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[RecurrencyType] [int] NOT NULL,
	[RecurrencyEndDate] [date] NOT NULL,
 CONSTRAINT [PK_Holidays] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LanguageCulture] [nvarchar](20) NOT NULL,
	[FlagImageFileName] [nvarchar](50) NULL,
	[RightToLeft] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveOfAbsences]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveOfAbsences](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[NumberAnnualLeave] [float] NOT NULL,
	[UsedLeave] [float] NOT NULL,
	[RemainingLeave] [float] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[EditorId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LeaveOfAbsences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveRegistrationDetails]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRegistrationDetails](
	[ID] [uniqueidentifier] NOT NULL,
	[StartDate] [date] NOT NULL,
	[LeaveRegistrationID] [uniqueidentifier] NOT NULL,
	[LeaveTime] [int] NOT NULL,
	[DueDate] [date] NOT NULL,
	[LeaveNumber] [float] NOT NULL,
	[RealityLeaveNumber] [float] NOT NULL,
	[LeaveTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LeaveRegistrationDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveRegistrations]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRegistrations](
	[ID] [uniqueidentifier] NOT NULL,
	[AuthorID] [uniqueidentifier] NOT NULL,
	[DepartmentID] [uniqueidentifier] NOT NULL,
	[CurrentLeaveNumber] [float] NOT NULL,
	[LeaveNumber] [float] NOT NULL,
	[RealityLeaveNumber] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[DeleteTransactionId] [uniqueidentifier] NULL,
	[Created] [datetime] NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Approved] [datetime] NULL,
	[WorkflowID] [uniqueidentifier] NULL,
	[Assigners] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Leave] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveType](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LeaveType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LinkAttachmentDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LinkAttachmentDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[LinkDocumentID] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[FileExt] [nvarchar](10) NULL,
	[FileContent] [varbinary](max) NULL,
	[LinkDeptID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LinkAttachmentDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LinkDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[ReceivedDate] [datetime] NULL,
	[UserDistribute] [uniqueidentifier] NULL,
	[DistributeDate] [datetime] NULL,
	[DocumentID] [uniqueidentifier] NULL,
	[Type] [int] NULL,
	[NewDocID] [uniqueidentifier] NULL,
	[History] [nvarchar](max) NULL,
	[Sender] [nvarchar](200) NULL,
	[SenderTime] [datetime] NULL,
	[IsBranch] [bit] NULL,
	[ReadTime] [datetime] NULL,
	[CancelTime] [datetime] NULL,
	[DeptID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LinkDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocaleResourceKey]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocaleResourceKey](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.LocaleResourceKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocaleStringResource]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocaleStringResource](
	[Id] [uniqueidentifier] NOT NULL,
	[ResourceValue] [nvarchar](1000) NOT NULL,
	[LocaleResourceKey_Id] [uniqueidentifier] NOT NULL,
	[Language_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.LocaleStringResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuPermissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuPermissions](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
	[NavNodeId] [uniqueidentifier] NOT NULL,
	[URL] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ActiveFag] [tinyint] NOT NULL,
 CONSTRAINT [PK_MenuPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleDetails]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleDetails](
	[ID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[Code] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ModuleDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modules]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[ID] [uniqueidentifier] NOT NULL,
	[Area] [nvarchar](255) NULL,
	[Controller] [nvarchar](255) NULL,
	[NameResourceKey] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleUrl]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleUrl](
	[ModuleID] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_ModuleUrl_1] PRIMARY KEY CLUSTERED 
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NavNodePermissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavNodePermissions](
	[NavNodeID] [uniqueidentifier] NOT NULL,
	[UserGroupID] [uniqueidentifier] NOT NULL,
	[IsUser] [bit] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_NavNodePermissions] PRIMARY KEY CLUSTERED 
(
	[NavNodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NavNodes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NavNodes](
	[ID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[NameResourceKey] [nvarchar](256) NULL,
	[Icon] [nvarchar](100) NULL,
	[IconImage] [varchar](max) NULL,
	[Url] [nvarchar](260) NULL,
	[FriendlyUrl] [nvarchar](260) NULL,
	[ElementType] [nvarchar](10) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[NoOrder] [int] NULL,
	[HTMLContent] [nvarchar](max) NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Code] [nvarchar](50) NULL,
	[IsViewMobile] [bit] NULL,
	[Type] [tinyint] NULL,
	[Layout] [tinyint] NULL,
 CONSTRAINT [PK_NavNodes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NotificationLogs]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationLogs](
	[ID] [uniqueidentifier] NOT NULL,
	[Sender] [uniqueidentifier] NULL,
	[Receiver] [uniqueidentifier] NULL,
	[SenderType] [nchar](10) NULL,
	[Name] [nvarchar](250) NULL,
	[Content] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Status] [int] NULL,
	[NotificationID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_NotificationLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[ID] [uniqueidentifier] NOT NULL,
	[NotificationTypeID] [int] NULL,
	[Sender] [uniqueidentifier] NULL,
	[Receiver] [uniqueidentifier] NULL,
	[IsRead] [bit] NULL,
	[IsActive] [bit] NULL,
	[Url] [nvarchar](250) NULL,
	[Created] [datetime] NULL,
	[ModuleCode] [nvarchar](50) NULL,
	[ObjectID] [nvarchar](50) NULL,
	[Action] [nvarchar](150) NULL,
	[Subject] [nvarchar](300) NULL,
	[Body] [nvarchar](max) NULL,
	[NotifyType] [int] NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotificationTypes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Template] [nvarchar](max) NULL,
	[SendType] [nvarchar](10) NULL,
	[ModuleCode] [nvarchar](50) NULL,
	[UrgentLevel] [int] NULL,
	[TemplateCode] [nvarchar](50) NULL,
	[Action] [nvarchar](500) NULL,
 CONSTRAINT [PK_NotificationTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineUsers](
	[ServerName] [nvarchar](250) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[LastActivityDate] [datetime] NULL,
	[UserName] [nvarchar](200) NULL,
 CONSTRAINT [PK_OnlineUsers] PRIMARY KEY CLUSTERED 
(
	[ServerName] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[ID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonalConfiguration]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalConfiguration](
	[UserID] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](250) NOT NULL,
	[Value] [nvarchar](50) NULL,
	[ModuleCode] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureAttachments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcedureAttachments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Ext] [nvarchar](10) NULL,
	[Size] [nvarchar](50) NULL,
	[FileContent] [varbinary](max) NULL,
	[ProcedureID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_ProcedureAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProcedureCategories]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureCategories](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[Author] [uniqueidentifier] NULL,
	[Editor] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_ProcedureCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Procedures]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procedures](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Department] [uniqueidentifier] NULL,
	[ProcedureType] [uniqueidentifier] NOT NULL,
	[ProcedureDate] [datetime] NULL,
	[Secret] [uniqueidentifier] NULL,
	[Priority] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[Author] [uniqueidentifier] NULL,
	[Editor] [uniqueidentifier] NULL,
	[WorkflowID] [uniqueidentifier] NULL,
	[Status] [int] NULL,
	[Summary] [nvarchar](max) NULL,
 CONSTRAINT [PK_Procedures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureTypes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[Author] [uniqueidentifier] NULL,
	[Editor] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ProcedureCategoryID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProcedureTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowHistories]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowHistories](
	[ID] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[Action] [int] NULL,
	[StartDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Occurred] [datetime] NULL,
	[WorkflowTaskID] [uniqueidentifier] NULL,
	[Version] [int] NULL,
	[WorkflowID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProcedureWorkflowHistories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflows]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflows](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[CurrentStepID] [uniqueidentifier] NULL,
	[IsCompleted] [bit] NULL,
 CONSTRAINT [PK_ProcedureWorkflows] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowSteps]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowSteps](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[IsFirst] [bit] NULL,
	[WorkflowID] [uniqueidentifier] NULL,
	[NextStepID] [uniqueidentifier] NULL,
	[ReturnToStepID] [uniqueidentifier] NULL,
	[SortOrder] [int] NULL,
	[ProcessType] [int] NULL,
 CONSTRAINT [PK_ProcedureWorkflowSteps] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowTasks]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowTasks](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[SignType] [int] NULL,
	[Created] [datetime] NULL,
	[Duration] [float] NULL,
	[SortOrder] [int] NULL,
	[WorkflowStepID] [uniqueidentifier] NULL,
	[IsCompleted] [bit] NULL,
 CONSTRAINT [PK_ProcessWorkflowTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowTemplates]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowTemplates](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Author] [uniqueidentifier] NULL,
	[Editor] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[ProcedureType] [uniqueidentifier] NULL,
	[IsPersonal] [bit] NULL,
 CONSTRAINT [PK_ProcessWorkflowTemplates_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowTemplateSteps]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowTemplateSteps](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[IsFirst] [bit] NULL,
	[WorkflowTemplateID] [uniqueidentifier] NULL,
	[NextTemplateStepID] [uniqueidentifier] NULL,
	[SortOrder] [int] NULL,
	[ReturnToTemplateStepID] [uniqueidentifier] NULL,
	[ProcessType] [int] NULL,
 CONSTRAINT [PK_ProcessWorkflowTemplateSteps] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureWorkflowTemplateTasks]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureWorkflowTemplateTasks](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[UserName] [nvarchar](500) NULL,
	[SignType] [int] NULL,
	[Duration] [float] NULL,
	[SortOrder] [int] NULL,
	[WorkflowTemplateStepID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProcedureWorkflowTemplateTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RelatedDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelatedDocuments](
	[FromDocID] [uniqueidentifier] NOT NULL,
	[ToDocID] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RelatedDocuments] PRIMARY KEY CLUSTERED 
(
	[FromDocID] ASC,
	[ToDocID] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resources]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resources](
	[Key] [nvarchar](256) NOT NULL,
	[Culture] [varchar](32) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Client] [varchar](64) NULL,
	[CreatedDate] [datetime] NULL,
	[Dirty] [bit] NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Culture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleID] [uniqueidentifier] NOT NULL,
	[PermissionID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[ModuleId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Salary]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salary](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[PayrollPeriod] [int] NOT NULL,
	[PerYear] [int] NOT NULL,
 CONSTRAINT [PK_Salary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalarySub]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalarySub](
	[Id] [uniqueidentifier] NOT NULL,
	[SalaryId] [uniqueidentifier] NOT NULL,
	[NameField] [nvarchar](500) NOT NULL,
	[Value] [nvarchar](2000) NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_SalarySub] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScopeDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScopeDocuments](
	[Name] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_ScopeDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SendMailDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SendMailDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](500) NULL,
	[Receivers] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[DocumentID] [uniqueidentifier] NULL,
	[Status] [int] NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[Created] [datetime] NULL,
	[EmailType] [int] NULL,
	[Error] [nvarchar](max) NULL,
 CONSTRAINT [PK_SendMailDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SendProviderDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SendProviderDocuments](
	[Name] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_SendProviderDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SentLinkDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SentLinkDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[Receivers] [nvarchar](255) NULL,
	[CC] [nvarchar](255) NULL,
	[SentService] [int] NULL,
	[Status] [int] NULL,
	[UserCreate] [nvarchar](50) NULL,
	[ConfirmDate] [datetime] NULL,
	[Delivered] [int] NULL,
	[SentHistory] [ntext] NULL,
	[CreateDate] [datetime] NULL,
	[NumRetry] [int] NULL,
	[Error] [nvarchar](max) NULL,
 CONSTRAINT [PK_SentLinkDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Name] [nvarchar](500) NOT NULL,
	[Code] [nvarchar](350) NOT NULL,
	[Value] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Settings_1] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShortLinkFileDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShortLinkFileDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Link] [nvarchar](500) NULL,
	[UserID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[RootDBID] [uniqueidentifier] NULL,
	[DocID] [uniqueidentifier] NULL,
	[FileID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ShortLinkDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Signatures]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Signatures](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Name] [nvarchar](500) NULL,
	[CertificateFile] [varbinary](max) NULL,
	[CertificateName] [nvarchar](500) NULL,
	[CertificatePass] [nvarchar](500) NULL,
	[Checksum] [nvarchar](500) NULL,
	[SignatureImageData] [varbinary](max) NULL,
	[SignatureImageFileName] [nvarchar](500) NULL,
	[SignatureImageFileType] [nvarchar](500) NULL,
	[StampImageData] [varbinary](max) NULL,
	[StampImageFileName] [nvarchar](500) NULL,
	[StampImageFileType] [nvarchar](500) NULL,
 CONSTRAINT [PK_Signatures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SiteModules]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteModules](
	[Id] [uniqueidentifier] NOT NULL,
	[SiteId] [uniqueidentifier] NOT NULL,
	[ModuleId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ActiveFag] [tinyint] NOT NULL,
 CONSTRAINT [PK_SiteModules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sites]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DomainName] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ActiveFag] [tinyint] NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLogDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLogDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Action] [nvarchar](200) NULL,
	[Object] [nvarchar](max) NULL,
	[Computer] [nvarchar](100) NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SystemLogDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLogs]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Action] [nvarchar](200) NULL,
	[Object] [nvarchar](max) NULL,
	[Computer] [nvarchar](100) NULL,
	[Module] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[Author] [nvarchar](100) NULL,
	[Key] [varchar](150) NULL,
	[URL] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[ActiveFag] [tinyint] NOT NULL,
 CONSTRAINT [PK_SystemLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[TenantID] [uniqueidentifier] NOT NULL,
	[TenantName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[Domain] [nvarchar](256) NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Applicat__30910331FB364C8F] UNIQUE NONCLUSTERED 
(
	[TenantName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrackingDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[DeptID] [uniqueidentifier] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[AssignBy] [uniqueidentifier] NULL,
	[AssignTo] [uniqueidentifier] NULL,
	[PercentFinish] [float] NULL,
	[Type] [int] NULL,
	[LastResult] [nvarchar](max) NULL,
	[DocID] [uniqueidentifier] NULL,
	[IsRecursive] [bit] NULL,
	[RecursiveType] [int] NULL,
	[Status] [int] NULL,
	[IsReport] [bit] NULL,
	[Rating] [int] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[IsNotice] [bit] NULL,
	[Severity] [int] NULL,
 CONSTRAINT [PK_TrackingDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrackingKpi]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingKpi](
	[TrackingDocumentId] [uniqueidentifier] NOT NULL,
	[TrackingWorkflowDocumentId] [uniqueidentifier] NOT NULL,
	[AssignToAccomplishment] [int] NULL,
	[AssignToAchievement] [int] NULL,
	[AssignToModified] [datetime] NULL,
	[AssignByAccomplishment] [int] NULL,
	[AssignByAchievement] [int] NULL,
	[AssignByModified] [datetime] NULL,
	[AdjustmentFactor] [real] NULL,
	[AdjustmentDensity] [real] NULL,
	[AssignToAverage]  AS ([AssignToAccomplishment]*(0.5)+[AssignToAchievement]*(0.5)),
	[AssignByAverage]  AS ([AssignByAccomplishment]*(0.5)+[AssignByAchievement]*(0.5)),
	[AdjustmentAverage]  AS (([AssignByAchievement]*(0.5)+[AssignByAccomplishment]*(0.5))*[AdjustmentFactor]),
	[DensityAverage]  AS ((([AssignByAchievement]*(0.5)+[AssignByAccomplishment]*(0.5))*[AdjustmentFactor])*[AdjustmentDensity]),
 CONSTRAINT [PK_TrackingKpi] PRIMARY KEY CLUSTERED 
(
	[TrackingDocumentId] ASC,
	[TrackingWorkflowDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrackingStatusDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingStatusDocument](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_TrackingStatusDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrackingWorkflowDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingWorkflowDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[TrackingDocumentID] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[ApprovedBy] [uniqueidentifier] NULL,
	[PercentFinish] [int] NULL,
	[Solution] [nvarchar](max) NULL,
	[Problem] [nvarchar](max) NULL,
	[ExtendReason] [nvarchar](max) NULL,
 CONSTRAINT [PK_TrackingWorkflowDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransferDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NULL,
	[Sender] [uniqueidentifier] NULL,
	[Receiver] [uniqueidentifier] NULL,
	[TransferTime] [datetime] NULL,
	[DeptID] [uniqueidentifier] NULL,
	[Status] [int] NULL,
	[ReadTime] [datetime] NULL,
 CONSTRAINT [PK_TransferDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TreeFilterDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeFilterDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[IconLink] [nvarchar](350) NULL,
	[ParamNames] [nvarchar](2500) NULL,
	[ParamValues] [nvarchar](2500) NULL,
	[IsCount] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[NoOrder] [int] NULL,
	[Permission] [nvarchar](2500) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[TypeShow] [nvarchar](50) NULL,
 CONSTRAINT [PK_TreeFilterDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TreeFilterPermissions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeFilterPermissions](
	[TreeFilterID] [uniqueidentifier] NOT NULL,
	[UserGroupID] [uniqueidentifier] NOT NULL,
	[IsUser] [bit] NULL,
 CONSTRAINT [PK_TreeFilterPermission] PRIMARY KEY CLUSTERED 
(
	[TreeFilterID] ASC,
	[UserGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSConfigDateOff]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSConfigDateOff](
	[ID] [uniqueidentifier] NOT NULL,
	[ConfigTimeShiftID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsRepeat] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSConfigDateOff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSConfigLeaveDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSConfigLeaveDate](
	[ID] [uniqueidentifier] NOT NULL,
	[NumDate] [int] NOT NULL,
	[IsSum] [bit] NOT NULL,
	[MonthReset] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSConfigLeaveDate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSConfigTimeDay]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSConfigTimeDay](
	[ID] [uniqueidentifier] NOT NULL,
	[ConfigTimeShiftID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[DayValue] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSConfigTimeDay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSConfigTimeShift]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSConfigTimeShift](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[OrderBy] [int] NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSConfigTimeShift] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TSDocument](
	[ID] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](1000) NOT NULL,
	[Content] [varbinary](max) NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[ObjectTypeID] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TSLeaveProcess]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TSLeaveProcess](
	[ID] [uniqueidentifier] NOT NULL,
	[LeaveRequestID] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Note] [nvarchar](1000) NULL,
	[WorkFlowStatusID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSLeaveProcess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TSLeaveRequest]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TSLeaveRequest](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[WorkFlowStatusID] [uniqueidentifier] NOT NULL,
	[OrgID] [uniqueidentifier] NULL,
	[NextAppover] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSLeaveRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TSLeaveRequestDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSLeaveRequestDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[LeaveRequestID] [uniqueidentifier] NOT NULL,
	[TSConfigTimeShiftID] [uniqueidentifier] NULL,
	[LeaveTypeID] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSLeaveRequestDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSLeaveType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSLeaveType](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSLeaveType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSObjectDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TSObjectDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[WorkFlowDetailID] [uniqueidentifier] NULL,
	[JobTitle] [nvarchar](250) NULL,
	[RoleName] [nvarchar](250) NULL,
	[FullName] [nvarchar](250) NULL,
	[UserID] [uniqueidentifier] NULL,
	[UserName] [varchar](250) NULL,
	[Code] [nvarchar](250) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSObjectDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TSObjectType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSObjectType](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[TSQL] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSObjectType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSScope]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSScope](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[TSQL] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSScope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSSummary]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TSSummary](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[TotalDateOff] [float] NOT NULL,
	[TotalDay] [float] NOT NULL,
	[Year] [int] NOT NULL,
	[IsReSet] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSSummary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TSWorkFlowAttributes]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowAttributes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowAttributes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSWorkFlowCondition]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowCondition](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[AttributeID] [uniqueidentifier] NOT NULL,
	[OperationID] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowCondition] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSWorkFlowDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[WorkFlowConditionID] [uniqueidentifier] NULL,
	[ObjectTypeID] [uniqueidentifier] NULL,
	[WorkFlowStatusID] [uniqueidentifier] NULL,
	[WorkFlowInfoID] [uniqueidentifier] NULL,
	[SLA] [int] NULL,
	[Step] [int] NULL,
	[ExpiredDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSWorkFlowInfo]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowInfo](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[TimeSheetScopeID] [uniqueidentifier] NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSWorkFlowOperation]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowOperation](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSWorkFlowStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSWorkFlowStatus](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TSWorkFlowStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDelegations]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDelegations](
	[ID] [uniqueidentifier] NOT NULL,
	[FromUserID] [uniqueidentifier] NULL,
	[ToUserID] [uniqueidentifier] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[EditorID] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[CcUserID] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserDelegations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[UserID] [uniqueidentifier] NOT NULL,
	[GroupID] [uniqueidentifier] NOT NULL,
	[IsManager] [bit] NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserHandovers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHandovers](
	[ID] [uniqueidentifier] NOT NULL,
	[FromUserID] [uniqueidentifier] NULL,
	[ToUserID] [uniqueidentifier] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AuthorID] [uniqueidentifier] NULL,
	[CcUserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserHandovers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[ItemKey] [nvarchar](100) NOT NULL,
	[ItemValue] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserRoles_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSiteUrl]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSiteUrl](
	[UserID] [nvarchar](200) NULL,
	[Site] [nvarchar](150) NULL,
	[Token] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Versions]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Versions](
	[Id] [uniqueidentifier] NOT NULL,
	[CurrentVersion] [nvarchar](50) NOT NULL,
	[LastVersion] [nvarchar](50) NOT NULL,
	[LastUpdateResult] [nvarchar](max) NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_Versions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WebAnalysis](
	[ID] [uniqueidentifier] NOT NULL,
	[RequestDateTime] [datetime] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[IP] [nvarchar](50) NOT NULL,
	[Browser] [nvarchar](50) NULL,
	[IsMobileDevice] [bit] NULL,
	[UserID] [uniqueidentifier] NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[Controller] [varchar](50) NULL,
	[Action] [varchar](100) NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_WebAnalysis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WebAnalysis_Date]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Date](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Date] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[RequestTotal] [bigint] NULL,
	[MobileRequestTotal] [bigint] NULL,
	[UserID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WebAnalysis_Month] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC,
	[Date] ASC,
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Date_Module]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Date_Module](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Date] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[ModuleID] [uniqueidentifier] NOT NULL,
	[RequestTotal] [bigint] NULL,
	[UserID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WebAnalysis_Date_Module] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC,
	[Date] ASC,
	[UserName] ASC,
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Date_Module_OrgUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Date_Module_OrgUser](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Date] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[ModuleID] [uniqueidentifier] NOT NULL,
	[OrgID] [uniqueidentifier] NOT NULL,
	[RequestTotal] [bigint] NULL,
	[UserID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WebAnalysis_Date_Module_Org] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC,
	[Date] ASC,
	[UserName] ASC,
	[ModuleID] ASC,
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Date_Org]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Date_Org](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Date] [int] NOT NULL,
	[OrgID] [uniqueidentifier] NOT NULL,
	[ParentID] [uniqueidentifier] NULL,
	[RequestTotal] [bigint] NULL,
	[MobileRequestTotal] [bigint] NULL,
	[OnlineTotal] [bigint] NULL,
 CONSTRAINT [PK_WebAnalysis_Date_Org_1] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC,
	[Date] ASC,
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Date_OrgUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Date_OrgUser](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Date] [int] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[OrgID] [uniqueidentifier] NOT NULL,
	[RequestTotal] [bigint] NULL,
	[MobileRequestTotal] [bigint] NULL,
	[UserID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WebAnalysis_Date_OrgUser] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC,
	[Date] ASC,
	[UserName] ASC,
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Month]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Month](
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[RequestTotal] [bigint] NULL,
	[MobileRequestTotal] [bigint] NULL,
 CONSTRAINT [PK_WebAnalysis_Year] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Month] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebAnalysis_Year]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAnalysis_Year](
	[Year] [int] NOT NULL,
	[RequestTotal] [bigint] NULL,
	[MobileRequestTotal] [bigint] NULL,
 CONSTRAINT [PK_WebAnalysis_Year_1] PRIMARY KEY CLUSTERED 
(
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WFRoles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WFRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[ModuleCode] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_dbo.WFRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WFUserRoles]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFUserRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[AssignToId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.WFRoleAssignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkFlowDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlowDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Type] [int] NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_WorkFlowDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkFlowStepDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlowStepDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[WorkFlowID] [uniqueidentifier] NULL,
	[WorkFlowTypeID] [uniqueidentifier] NULL,
	[ObjectAction] [nvarchar](max) NULL,
	[IsJobTitle] [bit] NULL,
	[NextStep] [uniqueidentifier] NULL,
	[BackStep] [uniqueidentifier] NULL,
	[Sequences] [int] NULL,
	[IsEndWorkFlow] [bit] NULL,
	[IsSignCA] [bit] NULL,
	[IsMoveToStep] [bit] NULL,
	[CCUser] [nvarchar](max) NULL,
	[IsAllowAssign] [bit] NULL,
	[IsCCUserJobTitle] [bit] NULL,
 CONSTRAINT [PK_WorkFlowStepDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkFlowTypeDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlowTypeDocuments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Code] [nvarchar](50) NULL,
	[OrderNumber] [int] NULL,
 CONSTRAINT [PK_WorkFlowTypeDocuments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkingDays]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingDays](
	[DayWork] [int] NULL,
	[FromMorningHour] [int] NULL,
	[FromMorningMinute] [int] NULL,
	[ToMorningHour] [int] NULL,
	[ToMorningMinute] [int] NULL,
	[FromAfternoonHour] [int] NULL,
	[FromAfternoonMinute] [int] NULL,
	[ToAfternoonHour] [int] NULL,
	[ToAfternoonMinute] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [nav].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [nav].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_nav.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [nav].[Menu]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [nav].[Menu](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[NavNodeId] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ModuleId] [uniqueidentifier] NULL,
	[TypeModule] [int] NULL,
	[Layout] [nvarchar](max) NULL,
	[Status] [tinyint] NOT NULL,
	[Target] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[Order] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Roles] [nvarchar](max) NULL,
	[GroupOrUsers] [nvarchar](max) NULL,
 CONSTRAINT [PK_nav.Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [nav].[MenuRole]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [nav].[MenuRole](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[MenuId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_nav.MenuRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [nav].[NavNode]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [nav].[NavNode](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Areas] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[Params] [nvarchar](max) NULL,
	[ResourceId] [nvarchar](max) NULL,
	[NameEN] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Method] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_nav.NavNode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [SmartOTP].[UserOtp]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SmartOTP].[UserOtp](
	[UserId] [uniqueidentifier] NOT NULL,
	[OtpType] [int] NOT NULL,
	[Pin] [nvarchar](50) NULL,
	[OtpTimeLife] [int] NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[Created] [datetime] NULL,
	[OtpCode] [nvarchar](10) NULL,
	[OtpExpired] [datetime] NULL,
	[OtpSendStatus] [int] NULL,
	[OtpSendTime] [datetime] NULL,
	[UserChangeOtpType] [bit] NULL,
	[IsValid] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [SmartOTP].[UserOtpLog]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SmartOTP].[UserOtpLog](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Action] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[Action]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[Action](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__Action__3214EC076C0E90E8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[Attachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Task].[Attachment](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[ItemId] [uniqueidentifier] NULL,
	[Source] [int] NULL,
	[FileName] [nvarchar](300) NULL,
	[FileExt] [nvarchar](10) NULL,
	[FileContent] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Task].[NatureTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[NatureTask](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Task.NatureTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[Project]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[SerialNumber] [nvarchar](150) NULL,
	[Summary] [nvarchar](max) NULL,
	[ProjectContent] [ntext] NULL,
	[ProjectStatusId] [int] NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[ProjectTypeId] [int] NULL,
	[ProjectSecretId] [int] NULL,
	[ProjectPriorityId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ProjectScopeId] [int] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[HasRecentActivity] [bit] NULL,
	[UserViews] [nvarchar](max) NULL,
	[PercentFinish] [float] NULL,
	[ProjectCategory] [nvarchar](500) NULL,
	[ProjectKindId] [int] NULL,
	[ManagerId] [nvarchar](max) NULL,
	[AppraiseResult] [nvarchar](max) NULL,
	[IsAuto] [bit] NOT NULL,
	[IsLinked] [bit] NOT NULL,
	[ReferenceId] [uniqueidentifier] NULL,
	[Module] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectCategory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[UserId] [uniqueidentifier] NULL,
	[DateUseLast] [datetime] NULL,
	[ProjectId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProjectCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectFilterParam]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectFilterParam](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Code] [nvarchar](50) NULL,
	[ParamValue] [nvarchar](2500) NULL,
	[IsCount] [bit] NULL,
	[IsPrivate] [bit] NULL,
	[IsActive] [bit] NULL,
	[NoOrder] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ParentID] [uniqueidentifier] NULL,
	[TypeShow] [nvarchar](80) NULL,
	[Icon] [nvarchar](80) NULL,
	[IsLable] [bit] NOT NULL,
	[Roles] [nvarchar](500) NULL,
	[Users] [nvarchar](500) NULL,
	[Test] [nvarchar](50) NULL,
 CONSTRAINT [PK__ProjectF__3214EC07A2399642] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectFolder]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectFolder](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](350) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[IsPersonal] [bit] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[Permission] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectFolderDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectFolderDetail](
	[FolderId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProjectFolderDetail] PRIMARY KEY CLUSTERED 
(
	[FolderId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectFollow]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectFollow](
	[UserId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProjectFollow] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[Action] [nvarchar](50) NULL,
	[Summary] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ActionId] [int] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[PercentFinish] [float] NULL,
 CONSTRAINT [PK_ProjectHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectKind]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectKind](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectK__3214EC074C47CDA0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectPriority]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectPriority](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectP__3214EC07B95AA391] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectScope]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectScope](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectS__3214EC077CE8AD31] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectSecret]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectSecret](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectS__3214EC07B1D086AB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Code] [nvarchar](64) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectS__3214EC07BF97F345] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectType]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Code] [nvarchar](64) NULL,
	[OrderNumber] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__ProjectT__3214EC0720FC79DC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[ProjectUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[ProjectUser](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Action] [int] NULL,
 CONSTRAINT [PK__ProjectU__3214EC0735A3C168] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[Report]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Task].[Report](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](4000) NULL,
	[Icon] [nvarchar](128) NULL,
	[Link] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[LinkDesktop] [nvarchar](500) NULL,
	[Type] [nvarchar](50) NULL,
	[CssClass] [nvarchar](255) NULL,
	[FileName] [nvarchar](250) NULL,
	[FileContent] [varbinary](max) NULL,
	[Permission] [nvarchar](max) NULL,
	[IsUser] [bit] NOT NULL,
 CONSTRAINT [PK__Report__3214EC07F41FFB06] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Task].[TaskItem]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItem](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[TaskName] [nvarchar](max) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[TaskItemStatusId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[AssignBy] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[PercentFinish] [float] NULL,
	[TaskType] [int] NULL,
	[IsReport] [bit] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[Conclusion] [nvarchar](max) NULL,
	[TaskItemPriorityId] [int] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[TaskItemCategoryId] [int] NULL,
	[IsSecurity] [bit] NULL,
	[IsWeirdo] [bit] NULL,
	[HasRecentActivity] [bit] NULL,
	[Weight] [float] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsGroupLabel] [bit] NULL,
	[TaskItemCategory] [nvarchar](500) NULL,
	[NatureTaskId] [int] NULL,
	[Order] [int] NULL,
	[IsAuto] [bit] NOT NULL,
	[TaskGroupType] [int] NOT NULL,
	[IsExtend] [bit] NULL,
	[ExtendDate] [datetime] NULL,
 CONSTRAINT [PK_TaskItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemAppraiseHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemAppraiseHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskItemAssignId] [uniqueidentifier] NULL,
	[TaskItemId] [uniqueidentifier] NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[AppraiseResult] [nvarchar](max) NULL,
	[TaskItemStatusId] [int] NULL,
	[ExtendDate] [datetime] NULL,
	[PropertyExt] [nvarchar](max) NULL,
	[ActionId] [int] NULL,
 CONSTRAINT [PK_TaskItemAppraiseHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemAssign]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemAssign](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskItemId] [uniqueidentifier] NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[AssignTo] [uniqueidentifier] NULL,
	[LastResult] [nvarchar](max) NULL,
	[TaskItemStatusId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[PercentFinish] [float] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[FinishedDate] [datetime] NULL,
	[TaskType] [int] NULL,
	[AppraiseResult] [nvarchar](max) NULL,
	[AppraiseStatus] [int] NULL,
	[ExtendDate] [datetime] NULL,
	[PropertyExt] [xml] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[AssignFollow] [uniqueidentifier] NULL,
	[HasRecentActivity] [bit] NULL,
	[ExtendDescription] [nvarchar](max) NULL,
	[Solution] [nvarchar](max) NULL,
	[Problem] [nvarchar](max) NULL,
	[UserHandoverId] [uniqueidentifier] NULL,
	[AppraisePercentFinish] [float] NULL,
	[AppraiseProcess] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsExtend] [bit] NOT NULL,
 CONSTRAINT [PK_TaskItemAssign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemCategory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[DateUseLast] [datetime] NULL,
 CONSTRAINT [PK_TaskItemCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemGroup]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__TaskItem__3214EC07A9B3D3A6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemKpi]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemKpi](
	[TaskItemId] [uniqueidentifier] NOT NULL,
	[TaskItemAssignId] [uniqueidentifier] NOT NULL,
	[AssignToAccomplishment] [int] NULL,
	[AssignToAchievement] [int] NULL,
	[AssignToModified] [datetime] NULL,
	[AssignByAccomplishment] [int] NULL,
	[AssignByAchievement] [int] NULL,
	[AssignByModified] [datetime] NULL,
	[AdjustmentFactor] [real] NULL,
	[AdjustmentDensity] [real] NULL,
	[AssignToAverage]  AS ([AssignToAccomplishment]*(0.4)+[AssignToAchievement]*(0.6)),
	[AssignByAverage]  AS ([AssignByAccomplishment]*(0.4)+[AssignByAchievement]*(0.6)),
	[AdjustmentAverage]  AS (([AssignByAchievement]*(0.5)+[AssignByAccomplishment]*(0.5))*[AdjustmentFactor]),
	[DensityAverage]  AS ((([AssignByAchievement]*(0.5)+[AssignByAccomplishment]*(0.5))*[AdjustmentFactor])*[AdjustmentDensity]),
 CONSTRAINT [PK_TaskItemKpi] PRIMARY KEY CLUSTERED 
(
	[TaskItemId] ASC,
	[TaskItemAssignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemPriority]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemPriority](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Density] [real] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__TaskItem__3214EC07660EB282] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemProcessHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemProcessHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskItemAssignId] [uniqueidentifier] NULL,
	[TaskItemId] [uniqueidentifier] NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ProcessResult] [nvarchar](max) NULL,
	[PercentFinish] [float] NULL,
	[TaskItemStatusId] [int] NULL,
	[PropertyExt] [nvarchar](max) NULL,
	[ActionId] [int] NULL,
	[IsPrivacyReport] [bit] NULL,
 CONSTRAINT [PK_TaskItemProcessHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Task].[TaskItemStatus]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Task].[TaskItemStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Code] [nvarchar](64) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__TaskItem__3214EC079C31E5D2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Workspace].[__MigrationHistory]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Workspace].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Workspace.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Workspace].[Attachment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Workspace].[Attachment](
	[Id] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[Content] [varbinary](max) NULL,
	[Size] [bigint] NOT NULL,
	[FileExt] [nvarchar](10) NULL,
	[Path] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Workspace.Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Workspace].[MyJob]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workspace].[MyJob](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleCode] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[Parameters] [nvarchar](max) NULL,
	[Method] [int] NOT NULL,
 CONSTRAINT [PK_Workspace.MyJob] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Workspace].[Permission]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workspace].[Permission](
	[Id] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[Item] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Workspace.Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Workspace].[Post]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workspace].[Post](
	[Id] [uniqueidentifier] NOT NULL,
	[Limit] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ActiveFag] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Workspace.Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_AttachmentId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_AttachmentId] ON [BusinessWorkflow].[Annex]
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Idx_BWF_Doc_Status_Id_Step_Author_Internal]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_BWF_Doc_Status_Id_Step_Author_Internal] ON [BusinessWorkflow].[Document]
(
	[Status] ASC
)
INCLUDE ( 	[Id],
	[CurrentStepId],
	[Author],
	[InternalReceiver]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookId] ON [BusinessWorkflow].[Document]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentTypeId] ON [BusinessWorkflow].[Document]
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentTypeId] ON [BusinessWorkflow].[DocumentTypeAttachment]
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AssignToId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_AssignToId] ON [BusinessWorkflow].[UserRole]
(
	[AssignToId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentId] ON [BusinessWorkflow].[UserRole]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [BusinessWorkflow].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_BW_WFTask_AssignTo]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_BW_WFTask_AssignTo] ON [BusinessWorkflow].[WorkflowTask]
(
	[AssignTo] ASC
)
INCLUDE ( 	[WorkflowStep_Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_BWF_Task_AssignTo_StepId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_BWF_Task_AssignTo_StepId] ON [BusinessWorkflow].[WorkflowTask]
(
	[AssignTo] ASC,
	[WorkflowStep_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentId] ON [BusinessWorkflow].[WorkflowTemplateAssociation]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentTypeId] ON [BusinessWorkflow].[WorkflowTemplateAssociation]
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkflowTemplateId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkflowTemplateId] ON [BusinessWorkflow].[WorkflowTemplateAssociation]
(
	[WorkflowTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChatId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChatId] ON [Chat].[Attachment]
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [Chat].[Chat]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChatId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChatId] ON [Chat].[Interactive]
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [Chat].[Interactive]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [Chat].[Member]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [Chat].[RoomSetting]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AlertStatusId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_AlertStatusId] ON [Contract].[Alert]
(
	[AlertStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColumnTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ColumnTypeId] ON [Contract].[ConfigFolderCustom]
(
	[ColumnTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentTypeId] ON [Contract].[Document]
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderCategoryId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderCategoryId] ON [Contract].[Document]
(
	[FolderCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentId] ON [Contract].[DocumentFile]
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderBiddingFiledId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderBiddingFiledId] ON [Contract].[Folder]
(
	[FolderBiddingFiledId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderFormBiddingId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderFormBiddingId] ON [Contract].[Folder]
(
	[FolderFormBiddingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderFormSelectionId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderFormSelectionId] ON [Contract].[Folder]
(
	[FolderFormSelectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderScopePerformId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderScopePerformId] ON [Contract].[Folder]
(
	[FolderScopePerformId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderTypeId] ON [Contract].[Folder]
(
	[FolderTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProjectId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProjectId] ON [Contract].[Folder]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Folder_Id]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_Folder_Id] ON [Contract].[FolderCustom]
(
	[Folder_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FolderGroupId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FolderGroupId] ON [Contract].[Project]
(
	[FolderGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProjectFormatId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProjectFormatId] ON [Contract].[Project]
(
	[ProjectFormatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProjectTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProjectTypeId] ON [Contract].[Project]
(
	[ProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_PermissionId] ON [Contract].[UserPermission]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationTypeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationTypeId] ON [Core].[Notifications]
(
	[NotificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Doc_InComing_IsActive]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IDX_Doc_InComing_IsActive] ON [dbo].[Documents]
(
	[IsInComing] ASC,
	[IsActive] ASC
)
INCLUDE ( 	[ID],
	[EditDepartment],
	[Created]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Notification]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_Notification] ON [dbo].[Notifications]
(
	[Receiver] ASC,
	[IsActive] ASC,
	[Created] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Resources]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_Resources] ON [dbo].[Resources]
(
	[Culture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Track_AssignTo_DocID]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IDX_Track_AssignTo_DocID] ON [dbo].[TrackingDocuments]
(
	[AssignTo] ASC,
	[DocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Track_DocID_Created]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IDX_Track_DocID_Created] ON [dbo].[TrackingDocuments]
(
	[AssignTo] ASC
)
INCLUDE ( 	[DocID],
	[Created]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_TrackDoc_Assignto]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IDX_TrackDoc_Assignto] ON [dbo].[TrackingDocuments]
(
	[DocID] ASC
)
INCLUDE ( 	[AssignTo]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Trans_DocID_Transfertime]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_Trans_DocID_Transfertime] ON [dbo].[TransferDocuments]
(
	[Receiver] ASC
)
INCLUDE ( 	[DocumentID],
	[TransferTime]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Trans_Receiver_DocID]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IDX_Trans_Receiver_DocID] ON [dbo].[TransferDocuments]
(
	[Receiver] ASC,
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_ud_userid_ordernumber]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_ud_userid_ordernumber] ON [dbo].[UserDepartments]
(
	[DeptID] ASC
)
INCLUDE ( 	[UserID],
	[OrderNumber]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_WebAnalysis_Requestdatetime]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_WebAnalysis_Requestdatetime] ON [dbo].[WebAnalysis]
(
	[RequestDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Idx_WebAnalySis_Url_User_Date]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [Idx_WebAnalySis_Url_User_Date] ON [dbo].[WebAnalysis]
(
	[UserName] ASC,
	[RequestDateTime] ASC
)
INCLUDE ( 	[Url]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NavNodeId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_NavNodeId] ON [nav].[Menu]
(
	[NavNodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostId] ON [Workspace].[Attachment]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostId]    Script Date: 04-05-2021 7:51:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostId] ON [Workspace].[Permission]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [BusinessWorkflow].[Annex] ADD  DEFAULT ((0)) FOR [Revision]
GO
ALTER TABLE [BusinessWorkflow].[Annex] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [HistoryId]
GO
ALTER TABLE [BusinessWorkflow].[Annex] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [BusinessWorkflow].[Annex] ADD  DEFAULT ((0)) FOR [Action]
GO
ALTER TABLE [BusinessWorkflow].[Annex] ADD  DEFAULT ((0)) FOR [IsSignCa]
GO
ALTER TABLE [BusinessWorkflow].[Attachment] ADD  DEFAULT ((0)) FOR [AnnexSortOrder]
GO
ALTER TABLE [BusinessWorkflow].[Attachment] ADD  DEFAULT ((0)) FOR [Size]
GO
ALTER TABLE [BusinessWorkflow].[Book] ADD  DEFAULT ((1)) FOR [IsReset]
GO
ALTER TABLE [BusinessWorkflow].[Document] ADD  CONSTRAINT [DF__tmp_ms_xx__Statu__1229A90A]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [BusinessWorkflow].[Document] ADD  CONSTRAINT [DF__tmp_ms_xx__Inter__131DCD43]  DEFAULT ('') FOR [InternalReceiver]
GO
ALTER TABLE [BusinessWorkflow].[Document] ADD  CONSTRAINT [DF__tmp_ms_xx__Revis__1411F17C]  DEFAULT ((0)) FOR [Revision]
GO
ALTER TABLE [BusinessWorkflow].[Document] ADD  CONSTRAINT [DF__tmp_ms_xx__Curre__150615B5]  DEFAULT ((0)) FOR [CurrentStepOrder]
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeAttachment] ADD  DEFAULT ((0)) FOR [Size]
GO
ALTER TABLE [BusinessWorkflow].[History] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Author]
GO
ALTER TABLE [BusinessWorkflow].[History] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [Created]
GO
ALTER TABLE [BusinessWorkflow].[History] ADD  DEFAULT ((0)) FOR [Revision]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTask] ADD  DEFAULT ((0)) FOR [IsProcessed]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTask] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ParentTask]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTask] ADD  DEFAULT ((0)) FOR [IsWaitSecretary]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTaskAttachment] ADD  DEFAULT ((0)) FOR [Size]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplate] ADD  DEFAULT ((0)) FOR [IsPersonal]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateTask] ADD  DEFAULT ((0)) FOR [AssignToType]
GO
ALTER TABLE [Chat].[Chat] ADD  CONSTRAINT [DF__Chat__TypeData__55766BDC]  DEFAULT ((0)) FOR [TypeData]
GO
ALTER TABLE [dbo].[AudienceNav] ADD  CONSTRAINT [DF_AudienceNav_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Departments] ADD  CONSTRAINT [DF_Departments_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Departments] ADD  CONSTRAINT [DF_Departments_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Departments] ADD  CONSTRAINT [DF_Departments_IsShow]  DEFAULT ((1)) FOR [IsShow]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[LocaleResourceKey] ADD  CONSTRAINT [DF_LocaleResourceKey_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[LocaleStringResource] ADD  CONSTRAINT [DF_LocaleStringResource_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NavNodes] ADD  CONSTRAINT [DF_NavNodes_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[NavNodes] ADD  CONSTRAINT [DF_NavNodes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [DF_Permissions_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Tenants] ADD  CONSTRAINT [DF__Applicati__Appli__17AD7836]  DEFAULT (newid()) FOR [TenantID]
GO
ALTER TABLE [dbo].[TSConfigDateOff] ADD  CONSTRAINT [DF_TSConfigDateOff_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSConfigDateOff] ADD  CONSTRAINT [DF_TSConfigDateOff_IsRepeat]  DEFAULT ((0)) FOR [IsRepeat]
GO
ALTER TABLE [dbo].[TSConfigDateOff] ADD  CONSTRAINT [DF_TSConfigDateOff_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSConfigLeaveDate] ADD  CONSTRAINT [DF_TSConfigLeaveDate_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSConfigLeaveDate] ADD  CONSTRAINT [DF_TSConfigLeaveDate_IsSum]  DEFAULT ((0)) FOR [IsSum]
GO
ALTER TABLE [dbo].[TSConfigLeaveDate] ADD  CONSTRAINT [DF_TSConfigLeaveDate_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSConfigTimeDay] ADD  CONSTRAINT [DF_TSConfigTimeDay_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSConfigTimeDay] ADD  CONSTRAINT [DF_TSConfigTimeDay_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSConfigTimeShift] ADD  CONSTRAINT [DF_TSConfigTimeShift_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSConfigTimeShift] ADD  CONSTRAINT [DF_TSConfigTimeShift_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSDocument] ADD  CONSTRAINT [DF_TSDocument_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSDocument] ADD  CONSTRAINT [DF_TSDocument_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSLeaveProcess] ADD  CONSTRAINT [DF_TSLeaveProcess_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSLeaveProcess] ADD  CONSTRAINT [DF_TSLeaveProcess_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSLeaveRequest] ADD  CONSTRAINT [DF_TSLeaveRequest_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail] ADD  CONSTRAINT [DF_TSLeaveRequestDetail_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail] ADD  CONSTRAINT [DF_TSLeaveRequestDetail_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSLeaveType] ADD  CONSTRAINT [DF_TSLeaveType_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSLeaveType] ADD  CONSTRAINT [DF_TSLeaveType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSObjectDetail] ADD  CONSTRAINT [DF_TSObjectDetail_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSObjectDetail] ADD  CONSTRAINT [DF_TSObjectDetail_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSObjectType] ADD  CONSTRAINT [DF_TSObjectType_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSObjectType] ADD  CONSTRAINT [DF_TSObjectType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSScope] ADD  CONSTRAINT [DF_TSScope_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSScope] ADD  CONSTRAINT [DF_TSScope_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSSummary] ADD  CONSTRAINT [DF_TSSummary_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSSummary] ADD  CONSTRAINT [DF_TSSummary_IsReSet]  DEFAULT ((0)) FOR [IsReSet]
GO
ALTER TABLE [dbo].[TSSummary] ADD  CONSTRAINT [DF_TSSummary_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowAttributes] ADD  CONSTRAINT [DF_TSWorkFlowAttributes_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowAttributes] ADD  CONSTRAINT [DF_TSWorkFlowAttributes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowCondition] ADD  CONSTRAINT [DF_TSWorkFlowCondition_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowCondition] ADD  CONSTRAINT [DF_TSWorkFlowCondition_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] ADD  CONSTRAINT [DF_TSWorkFlowDetail_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] ADD  CONSTRAINT [DF_TSWorkFlowDetail_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowInfo] ADD  CONSTRAINT [DF_TSWorkFlowInfo_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowInfo] ADD  CONSTRAINT [DF_TSWorkFlowInfo_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowOperation] ADD  CONSTRAINT [DF_TSWorkFlowOperation_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowOperation] ADD  CONSTRAINT [DF_TSWorkFlowOperation_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TSWorkFlowStatus] ADD  CONSTRAINT [DF_TSWorkFlowStatus_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TSWorkFlowStatus] ADD  CONSTRAINT [DF_TSWorkFlowStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[UserProfiles] ADD  CONSTRAINT [DF_UserProfiles_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[WFRoles] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [Task].[Project] ADD  CONSTRAINT [DF_Project_IsAuto]  DEFAULT ((0)) FOR [IsAuto]
GO
ALTER TABLE [Task].[Project] ADD  CONSTRAINT [DF_Project_IsLinked]  DEFAULT ((0)) FOR [IsLinked]
GO
ALTER TABLE [Task].[Report] ADD  CONSTRAINT [DF_Report_IsUser]  DEFAULT ((0)) FOR [IsUser]
GO
ALTER TABLE [Task].[TaskItem] ADD  CONSTRAINT [DF_TaskItem_IsAuto]  DEFAULT ((0)) FOR [IsAuto]
GO
ALTER TABLE [Task].[TaskItem] ADD  CONSTRAINT [DF_TaskItem_TaskGroupType]  DEFAULT ((0)) FOR [TaskGroupType]
GO
ALTER TABLE [Task].[TaskItemAssign] ADD  CONSTRAINT [DF_TaskItemAssign_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [Task].[TaskItemAssign] ADD  CONSTRAINT [DF_TaskItemAssign_IsExtend]  DEFAULT ((0)) FOR [IsExtend]
GO
ALTER TABLE [Workspace].[MyJob] ADD  CONSTRAINT [DF__MyJob__Method__28A3C565]  DEFAULT ((0)) FOR [Method]
GO
ALTER TABLE [BusinessWorkflow].[Annex]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.Annex_BusinessWorkflow.Attachment_AttachmentId] FOREIGN KEY([AttachmentId])
REFERENCES [BusinessWorkflow].[Attachment] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[Annex] CHECK CONSTRAINT [FK_BusinessWorkflow.Annex_BusinessWorkflow.Attachment_AttachmentId]
GO
ALTER TABLE [BusinessWorkflow].[Annex]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.Annex_BusinessWorkflow.Document_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [BusinessWorkflow].[Document] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[Annex] CHECK CONSTRAINT [FK_BusinessWorkflow.Annex_BusinessWorkflow.Document_Document_Id]
GO
ALTER TABLE [BusinessWorkflow].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.Attachment_BusinessWorkflow.Document_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [BusinessWorkflow].[Document] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[Attachment] CHECK CONSTRAINT [FK_BusinessWorkflow.Attachment_BusinessWorkflow.Document_Document_Id]
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentType]  WITH NOCHECK ADD  CONSTRAINT [FK_BookDocumentType_Book] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[Book] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentType] NOCHECK CONSTRAINT [FK_BookDocumentType_Book]
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentType]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.BookDocumentType_BusinessWorkflow.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[DocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentType] CHECK CONSTRAINT [FK_BusinessWorkflow.BookDocumentType_BusinessWorkflow.DocumentType_DocumentTypeId]
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.BookDocumentTypeWorkflowTemplate_BusinessWorkflow.BookDocumentType_BookDocumentType_Id] FOREIGN KEY([BookDocumentType_Id])
REFERENCES [BusinessWorkflow].[BookDocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate] CHECK CONSTRAINT [FK_BusinessWorkflow.BookDocumentTypeWorkflowTemplate_BusinessWorkflow.BookDocumentType_BookDocumentType_Id]
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.BookDocumentTypeWorkflowTemplate_BusinessWorkflow.WorkflowTemplate_WorkflowTemplate_Id] FOREIGN KEY([WorkflowTemplate_Id])
REFERENCES [BusinessWorkflow].[WorkflowTemplate] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[BookDocumentTypeWorkflowTemplate] CHECK CONSTRAINT [FK_BusinessWorkflow.BookDocumentTypeWorkflowTemplate_BusinessWorkflow.WorkflowTemplate_WorkflowTemplate_Id]
GO
ALTER TABLE [BusinessWorkflow].[BookNumber]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.BookNumber_BusinessWorkflow.Book_Book_Id] FOREIGN KEY([Book_Id])
REFERENCES [BusinessWorkflow].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[BookNumber] CHECK CONSTRAINT [FK_BusinessWorkflow.BookNumber_BusinessWorkflow.Book_Book_Id]
GO
ALTER TABLE [BusinessWorkflow].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Document] FOREIGN KEY([Id])
REFERENCES [BusinessWorkflow].[Document] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[Document] CHECK CONSTRAINT [FK_Document_Document]
GO
ALTER TABLE [BusinessWorkflow].[DocumentTemplate]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.DocumentTemplate_BusinessWorkflow.BookDocumentType_BookDocumentType_Id] FOREIGN KEY([BookDocumentType_Id])
REFERENCES [BusinessWorkflow].[BookDocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[DocumentTemplate] CHECK CONSTRAINT [FK_BusinessWorkflow.DocumentTemplate_BusinessWorkflow.BookDocumentType_BookDocumentType_Id]
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeAttachment]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.DocumentTypeAttachment_BusinessWorkflow.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[DocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeAttachment] CHECK CONSTRAINT [FK_BusinessWorkflow.DocumentTypeAttachment_BusinessWorkflow.DocumentType_DocumentTypeId]
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeField]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.DocumentTypeField_BusinessWorkflow.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[DocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeField] CHECK CONSTRAINT [FK_BusinessWorkflow.DocumentTypeField_BusinessWorkflow.DocumentType_DocumentTypeId]
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.DocumentTypeSupervisor_BusinessWorkflow.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[DocumentType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[DocumentTypeSupervisor] CHECK CONSTRAINT [FK_BusinessWorkflow.DocumentTypeSupervisor_BusinessWorkflow.DocumentType_DocumentTypeId]
GO
ALTER TABLE [BusinessWorkflow].[SignArea]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.SignArea_BusinessWorkflow.Attachment_Attachment_Id] FOREIGN KEY([Attachment_Id])
REFERENCES [BusinessWorkflow].[Attachment] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[SignArea] CHECK CONSTRAINT [FK_BusinessWorkflow.SignArea_BusinessWorkflow.Attachment_Attachment_Id]
GO
ALTER TABLE [BusinessWorkflow].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.UserRole_BusinessWorkflow.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [BusinessWorkflow].[Role] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[UserRole] CHECK CONSTRAINT [FK_BusinessWorkflow.UserRole_BusinessWorkflow.Role_RoleId]
GO
ALTER TABLE [BusinessWorkflow].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.UserRole_dbo.Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [BusinessWorkflow].[UserRole] CHECK CONSTRAINT [FK_BusinessWorkflow.UserRole_dbo.Departments_DepartmentId]
GO
ALTER TABLE [BusinessWorkflow].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.UserRole_dbo.Users_AssignToId] FOREIGN KEY([AssignToId])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [BusinessWorkflow].[UserRole] CHECK CONSTRAINT [FK_BusinessWorkflow.UserRole_dbo.Users_AssignToId]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowStep]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowStep_BusinessWorkflow.Document_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [BusinessWorkflow].[Document] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[WorkflowStep] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowStep_BusinessWorkflow.Document_Document_Id]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTask]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTask_BusinessWorkflow.WorkflowStep_WorkflowStep_Id] FOREIGN KEY([WorkflowStep_Id])
REFERENCES [BusinessWorkflow].[WorkflowStep] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTask] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTask_BusinessWorkflow.WorkflowStep_WorkflowStep_Id]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTaskAttachment]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTaskAttachment_BusinessWorkflow.WorkflowTask_WorkflowTask_Id] FOREIGN KEY([WorkflowTask_Id])
REFERENCES [BusinessWorkflow].[WorkflowTask] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTaskAttachment] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTaskAttachment_BusinessWorkflow.WorkflowTask_WorkflowTask_Id]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_BusinessWorkflow.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [BusinessWorkflow].[DocumentType] ([Id])
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_BusinessWorkflow.DocumentType_DocumentTypeId]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_BusinessWorkflow.WorkflowTemplate_WorkflowTemplateId] FOREIGN KEY([WorkflowTemplateId])
REFERENCES [BusinessWorkflow].[WorkflowTemplate] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_BusinessWorkflow.WorkflowTemplate_WorkflowTemplateId]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_dbo.Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateAssociation] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateAssociation_dbo.Departments_DepartmentId]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateSecretary]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateSecretary_BusinessWorkflow.WorkflowTemplateTask_WorkflowTemplateTaskId] FOREIGN KEY([WorkflowTemplateTaskId])
REFERENCES [BusinessWorkflow].[WorkflowTemplateTask] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateSecretary] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateSecretary_BusinessWorkflow.WorkflowTemplateTask_WorkflowTemplateTaskId]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateStep]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateStep_BusinessWorkflow.WorkflowTemplate_WorkflowTemplate_Id] FOREIGN KEY([WorkflowTemplate_Id])
REFERENCES [BusinessWorkflow].[WorkflowTemplate] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateStep] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateStep_BusinessWorkflow.WorkflowTemplate_WorkflowTemplate_Id]
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateTask]  WITH CHECK ADD  CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateTask_BusinessWorkflow.WorkflowTemplateStep_WorkflowTemplateStep_Id] FOREIGN KEY([WorkflowTemplateStep_Id])
REFERENCES [BusinessWorkflow].[WorkflowTemplateStep] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [BusinessWorkflow].[WorkflowTemplateTask] CHECK CONSTRAINT [FK_BusinessWorkflow.WorkflowTemplateTask_BusinessWorkflow.WorkflowTemplateStep_WorkflowTemplateStep_Id]
GO
ALTER TABLE [Chat].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Chat.Attachment_Chat.Chat_ChatId] FOREIGN KEY([ChatId])
REFERENCES [Chat].[Chat] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Chat].[Attachment] CHECK CONSTRAINT [FK_Chat.Attachment_Chat.Chat_ChatId]
GO
ALTER TABLE [Chat].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat.Chat_Chat.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Chat].[Room] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Chat].[Chat] CHECK CONSTRAINT [FK_Chat.Chat_Chat.Room_RoomId]
GO
ALTER TABLE [Chat].[Interactive]  WITH CHECK ADD  CONSTRAINT [FK_Chat.Interactive_Chat.Chat_ChatId] FOREIGN KEY([ChatId])
REFERENCES [Chat].[Chat] ([Id])
GO
ALTER TABLE [Chat].[Interactive] CHECK CONSTRAINT [FK_Chat.Interactive_Chat.Chat_ChatId]
GO
ALTER TABLE [Chat].[Interactive]  WITH CHECK ADD  CONSTRAINT [FK_Chat.Interactive_Chat.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Chat].[Room] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Chat].[Interactive] CHECK CONSTRAINT [FK_Chat.Interactive_Chat.Room_RoomId]
GO
ALTER TABLE [Chat].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Chat.Member_Chat.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Chat].[Room] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Chat].[Member] CHECK CONSTRAINT [FK_Chat.Member_Chat.Room_RoomId]
GO
ALTER TABLE [Chat].[RoomSetting]  WITH CHECK ADD  CONSTRAINT [FK_Chat.RoomSetting_Chat.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Chat].[Room] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Chat].[RoomSetting] CHECK CONSTRAINT [FK_Chat.RoomSetting_Chat.Room_RoomId]
GO
ALTER TABLE [Contract].[Alert]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Alert_Contract.AlertStatus_AlertStatusId] FOREIGN KEY([AlertStatusId])
REFERENCES [Contract].[AlertStatus] ([Id])
GO
ALTER TABLE [Contract].[Alert] CHECK CONSTRAINT [FK_Contract.Alert_Contract.AlertStatus_AlertStatusId]
GO
ALTER TABLE [Contract].[ConfigFolderCustom]  WITH CHECK ADD  CONSTRAINT [FK_Contract.ConfigFolderCustom_Contract.ConfigColumnType_ColumnTypeId] FOREIGN KEY([ColumnTypeId])
REFERENCES [Contract].[ConfigColumnType] ([Id])
GO
ALTER TABLE [Contract].[ConfigFolderCustom] CHECK CONSTRAINT [FK_Contract.ConfigFolderCustom_Contract.ConfigColumnType_ColumnTypeId]
GO
ALTER TABLE [Contract].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Document_Contract.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [Contract].[DocumentType] ([Id])
GO
ALTER TABLE [Contract].[Document] CHECK CONSTRAINT [FK_Contract.Document_Contract.DocumentType_DocumentTypeId]
GO
ALTER TABLE [Contract].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Document_Contract.FolderCategory_FolderCategoryId] FOREIGN KEY([FolderCategoryId])
REFERENCES [Contract].[FolderCategory] ([Id])
GO
ALTER TABLE [Contract].[Document] CHECK CONSTRAINT [FK_Contract.Document_Contract.FolderCategory_FolderCategoryId]
GO
ALTER TABLE [Contract].[DocumentFile]  WITH CHECK ADD  CONSTRAINT [FK_Contract.DocumentFile_Contract.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [Contract].[Document] ([Id])
GO
ALTER TABLE [Contract].[DocumentFile] CHECK CONSTRAINT [FK_Contract.DocumentFile_Contract.Document_DocumentId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.FolderBiddingFiled_FolderBiddingFiledId] FOREIGN KEY([FolderBiddingFiledId])
REFERENCES [Contract].[FolderBiddingFiled] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.FolderBiddingFiled_FolderBiddingFiledId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.FolderFormBidding_FolderFormBiddingId] FOREIGN KEY([FolderFormBiddingId])
REFERENCES [Contract].[FolderFormBidding] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.FolderFormBidding_FolderFormBiddingId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.FolderFormSelection_FolderFormSelectionId] FOREIGN KEY([FolderFormSelectionId])
REFERENCES [Contract].[FolderFormSelection] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.FolderFormSelection_FolderFormSelectionId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.FolderScopePerform_FolderScopePerformId] FOREIGN KEY([FolderScopePerformId])
REFERENCES [Contract].[FolderScopePerform] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.FolderScopePerform_FolderScopePerformId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.FolderType_FolderTypeId] FOREIGN KEY([FolderTypeId])
REFERENCES [Contract].[FolderType] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.FolderType_FolderTypeId]
GO
ALTER TABLE [Contract].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Folder_Contract.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [Contract].[Project] ([Id])
GO
ALTER TABLE [Contract].[Folder] CHECK CONSTRAINT [FK_Contract.Folder_Contract.Project_ProjectId]
GO
ALTER TABLE [Contract].[FolderCustom]  WITH CHECK ADD  CONSTRAINT [FK_Contract.FolderCustom_Contract.Folder_Folder_Id] FOREIGN KEY([Folder_Id])
REFERENCES [Contract].[Folder] ([Id])
GO
ALTER TABLE [Contract].[FolderCustom] CHECK CONSTRAINT [FK_Contract.FolderCustom_Contract.Folder_Folder_Id]
GO
ALTER TABLE [Contract].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Project_Contract.FolderGroup_FolderGroupId] FOREIGN KEY([FolderGroupId])
REFERENCES [Contract].[FolderGroup] ([Id])
GO
ALTER TABLE [Contract].[Project] CHECK CONSTRAINT [FK_Contract.Project_Contract.FolderGroup_FolderGroupId]
GO
ALTER TABLE [Contract].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Project_Contract.ProjectFormat_ProjectFormatId] FOREIGN KEY([ProjectFormatId])
REFERENCES [Contract].[ProjectFormat] ([Id])
GO
ALTER TABLE [Contract].[Project] CHECK CONSTRAINT [FK_Contract.Project_Contract.ProjectFormat_ProjectFormatId]
GO
ALTER TABLE [Contract].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Contract.Project_Contract.ProjectType_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [Contract].[ProjectType] ([Id])
GO
ALTER TABLE [Contract].[Project] CHECK CONSTRAINT [FK_Contract.Project_Contract.ProjectType_ProjectTypeId]
GO
ALTER TABLE [Contract].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_Contract.UserPermission_Contract.Permission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [Contract].[Permission] ([Id])
GO
ALTER TABLE [Contract].[UserPermission] CHECK CONSTRAINT [FK_Contract.UserPermission_Contract.Permission_PermissionId]
GO
ALTER TABLE [Core].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_core.Notifications_core.NotificationTypes_NotificationTypeId] FOREIGN KEY([NotificationTypeId])
REFERENCES [Core].[NotificationTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Core].[Notifications] CHECK CONSTRAINT [FK_core.Notifications_core.NotificationTypes_NotificationTypeId]
GO
ALTER TABLE [Core].[UserOtp]  WITH CHECK ADD  CONSTRAINT [FK_UserOtp_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [Core].[UserOtp] CHECK CONSTRAINT [FK_UserOtp_Users]
GO
ALTER TABLE [dbo].[AudienceNav]  WITH CHECK ADD  CONSTRAINT [FK_AudienceNav_NavNodes] FOREIGN KEY([NavNodeID])
REFERENCES [dbo].[NavNodes] ([ID])
GO
ALTER TABLE [dbo].[AudienceNav] CHECK CONSTRAINT [FK_AudienceNav_NavNodes]
GO
ALTER TABLE [dbo].[BookDocTypeDocuments]  WITH CHECK ADD  CONSTRAINT [FK_BookDocTypeDocuments_BookDocuments] FOREIGN KEY([BookID])
REFERENCES [dbo].[BookDocuments] ([ID])
GO
ALTER TABLE [dbo].[BookDocTypeDocuments] CHECK CONSTRAINT [FK_BookDocTypeDocuments_BookDocuments]
GO
ALTER TABLE [dbo].[BookNumberDocuments]  WITH CHECK ADD  CONSTRAINT [FK_BookNumberDocuments_BookDocuments] FOREIGN KEY([BookID])
REFERENCES [dbo].[BookDocuments] ([ID])
GO
ALTER TABLE [dbo].[BookNumberDocuments] CHECK CONSTRAINT [FK_BookNumberDocuments_BookDocuments]
GO
ALTER TABLE [dbo].[BookTrackingDocuments]  WITH CHECK ADD  CONSTRAINT [FK_BookTrackingDocuments_Documents] FOREIGN KEY([DocID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[BookTrackingDocuments] CHECK CONSTRAINT [FK_BookTrackingDocuments_Documents]
GO
ALTER TABLE [dbo].[BookWorkFlowDocuments]  WITH CHECK ADD  CONSTRAINT [FK_BookWorkFlowDocuments_BookDocuments] FOREIGN KEY([BookID])
REFERENCES [dbo].[BookDocuments] ([ID])
GO
ALTER TABLE [dbo].[BookWorkFlowDocuments] CHECK CONSTRAINT [FK_BookWorkFlowDocuments_BookDocuments]
GO
ALTER TABLE [dbo].[BookWorkFlowDocuments]  WITH CHECK ADD  CONSTRAINT [FK_BookWorkFlowDocuments_WorkFlowDocuments] FOREIGN KEY([WorkFlowID])
REFERENCES [dbo].[WorkFlowDocuments] ([ID])
GO
ALTER TABLE [dbo].[BookWorkFlowDocuments] CHECK CONSTRAINT [FK_BookWorkFlowDocuments_WorkFlowDocuments]
GO
ALTER TABLE [dbo].[CategorizeDetailDocuments]  WITH CHECK ADD  CONSTRAINT [FK_CategorizeDetailDocuments_CategorizeDocuments] FOREIGN KEY([CategorizeID])
REFERENCES [dbo].[CategorizeDocuments] ([ID])
GO
ALTER TABLE [dbo].[CategorizeDetailDocuments] CHECK CONSTRAINT [FK_CategorizeDetailDocuments_CategorizeDocuments]
GO
ALTER TABLE [dbo].[CategorizeDetailDocuments]  WITH CHECK ADD  CONSTRAINT [FK_CategorizeDetailDocuments_Documents] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[CategorizeDetailDocuments] CHECK CONSTRAINT [FK_CategorizeDetailDocuments_Documents]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Authors] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Authors]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_DepartmentTypes] FOREIGN KEY([DeptTypeID])
REFERENCES [dbo].[DepartmentTypes] ([ID])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_DepartmentTypes]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Editors] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Editors]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_BookDocuments] FOREIGN KEY([DocBook])
REFERENCES [dbo].[BookDocuments] ([ID])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_BookDocuments]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_DocumentTypes] FOREIGN KEY([DocType])
REFERENCES [dbo].[DocumentTypes] ([ID])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_DocumentTypes]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_PriorityDocuments] FOREIGN KEY([Priority])
REFERENCES [dbo].[PriorityDocuments] ([ID])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_PriorityDocuments]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_SecretDocuments] FOREIGN KEY([Secret])
REFERENCES [dbo].[SecretDocuments] ([ID])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_SecretDocuments]
GO
ALTER TABLE [dbo].[DocumentTemplates]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTemplates_DocumentTypes] FOREIGN KEY([DocumentTypeID])
REFERENCES [dbo].[DocumentTypes] ([ID])
GO
ALTER TABLE [dbo].[DocumentTemplates] CHECK CONSTRAINT [FK_DocumentTemplates_DocumentTypes]
GO
ALTER TABLE [dbo].[FileComments]  WITH CHECK ADD  CONSTRAINT [FK_FileComments_Comments] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comments] ([ID])
GO
ALTER TABLE [dbo].[FileComments] CHECK CONSTRAINT [FK_FileComments_Comments]
GO
ALTER TABLE [dbo].[FileTrackingWorkflowDocuments]  WITH CHECK ADD  CONSTRAINT [FK_FileTrackingWorkflowDocuments_TrackingDocuments] FOREIGN KEY([TrackingDocumentID])
REFERENCES [dbo].[TrackingDocuments] ([ID])
GO
ALTER TABLE [dbo].[FileTrackingWorkflowDocuments] CHECK CONSTRAINT [FK_FileTrackingWorkflowDocuments_TrackingDocuments]
GO
ALTER TABLE [dbo].[FileTrackingWorkflowDocuments]  WITH CHECK ADD  CONSTRAINT [FK_FileTrackingWorkflowDocuments_TrackingWorkflowDocuments] FOREIGN KEY([TrackingWorkflowDocumentID])
REFERENCES [dbo].[TrackingWorkflowDocuments] ([ID])
GO
ALTER TABLE [dbo].[FileTrackingWorkflowDocuments] CHECK CONSTRAINT [FK_FileTrackingWorkflowDocuments_TrackingWorkflowDocuments]
GO
ALTER TABLE [dbo].[FolderDetailDocuments]  WITH CHECK ADD  CONSTRAINT [FK_FolderDetailDocuments_Documents] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[FolderDetailDocuments] CHECK CONSTRAINT [FK_FolderDetailDocuments_Documents]
GO
ALTER TABLE [dbo].[FolderDetailDocuments]  WITH CHECK ADD  CONSTRAINT [FK_FolderDetailDocuments_FolderDocuments] FOREIGN KEY([FolderID])
REFERENCES [dbo].[FolderDocuments] ([ID])
GO
ALTER TABLE [dbo].[FolderDetailDocuments] CHECK CONSTRAINT [FK_FolderDetailDocuments_FolderDocuments]
GO
ALTER TABLE [dbo].[FolderDocuments]  WITH CHECK ADD  CONSTRAINT [FK_FolderDocuments_FolderDocuments] FOREIGN KEY([ParentID])
REFERENCES [dbo].[FolderDocuments] ([ID])
GO
ALTER TABLE [dbo].[FolderDocuments] CHECK CONSTRAINT [FK_FolderDocuments_FolderDocuments]
GO
ALTER TABLE [dbo].[GroupEmail]  WITH CHECK ADD  CONSTRAINT [FK_GroupEmail_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[GroupEmail] CHECK CONSTRAINT [FK_GroupEmail_Users]
GO
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[GroupRoles] CHECK CONSTRAINT [FK_GroupRoles_Groups]
GO
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Modules] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ID])
GO
ALTER TABLE [dbo].[GroupRoles] CHECK CONSTRAINT [FK_GroupRoles_Modules]
GO
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[GroupRoles] CHECK CONSTRAINT [FK_GroupRoles_Roles]
GO
ALTER TABLE [dbo].[HistoryDocuments]  WITH CHECK ADD  CONSTRAINT [FK_HistoryDocuments_Documents] FOREIGN KEY([DocID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[HistoryDocuments] CHECK CONSTRAINT [FK_HistoryDocuments_Documents]
GO
ALTER TABLE [dbo].[Holidays]  WITH CHECK ADD  CONSTRAINT [FK_Holidays_Authors] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Holidays] CHECK CONSTRAINT [FK_Holidays_Authors]
GO
ALTER TABLE [dbo].[Holidays]  WITH CHECK ADD  CONSTRAINT [FK_Holidays_Editors] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Holidays] CHECK CONSTRAINT [FK_Holidays_Editors]
GO
ALTER TABLE [dbo].[LeaveRegistrationDetails]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRegistrationDetails_LeaveRegistrations] FOREIGN KEY([LeaveRegistrationID])
REFERENCES [dbo].[LeaveRegistrations] ([ID])
GO
ALTER TABLE [dbo].[LeaveRegistrationDetails] CHECK CONSTRAINT [FK_LeaveRegistrationDetails_LeaveRegistrations]
GO
ALTER TABLE [dbo].[LeaveRegistrationDetails]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRegistrationDetails_LeaveType] FOREIGN KEY([LeaveTypeID])
REFERENCES [dbo].[LeaveType] ([ID])
GO
ALTER TABLE [dbo].[LeaveRegistrationDetails] CHECK CONSTRAINT [FK_LeaveRegistrationDetails_LeaveType]
GO
ALTER TABLE [dbo].[LeaveRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRegistrations_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [dbo].[LeaveRegistrations] CHECK CONSTRAINT [FK_LeaveRegistrations_Departments]
GO
ALTER TABLE [dbo].[LinkAttachmentDocuments]  WITH CHECK ADD  CONSTRAINT [FK_LinkAttachmentDocuments_LinkDocuments] FOREIGN KEY([LinkDocumentID])
REFERENCES [dbo].[LinkDocuments] ([ID])
GO
ALTER TABLE [dbo].[LinkAttachmentDocuments] CHECK CONSTRAINT [FK_LinkAttachmentDocuments_LinkDocuments]
GO
ALTER TABLE [dbo].[LocaleStringResource]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LocaleStringResource_dbo.Language_Language_Id] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[LocaleStringResource] CHECK CONSTRAINT [FK_dbo.LocaleStringResource_dbo.Language_Language_Id]
GO
ALTER TABLE [dbo].[LocaleStringResource]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LocaleStringResource_dbo.LocaleResourceKey_LocaleResourceKey_Id] FOREIGN KEY([LocaleResourceKey_Id])
REFERENCES [dbo].[LocaleResourceKey] ([Id])
GO
ALTER TABLE [dbo].[LocaleStringResource] CHECK CONSTRAINT [FK_dbo.LocaleStringResource_dbo.LocaleResourceKey_LocaleResourceKey_Id]
GO
ALTER TABLE [dbo].[NavNodes]  WITH CHECK ADD  CONSTRAINT [FK_NavNodes_Modules] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ID])
GO
ALTER TABLE [dbo].[NavNodes] CHECK CONSTRAINT [FK_NavNodes_Modules]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_NotificationTypes] FOREIGN KEY([NotificationTypeID])
REFERENCES [dbo].[NotificationTypes] ([ID])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_NotificationTypes]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Modules] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ID])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Modules]
GO
ALTER TABLE [dbo].[PersonalConfiguration]  WITH CHECK ADD  CONSTRAINT [FK__PersonalC__UserI__10B661E4] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PersonalConfiguration] CHECK CONSTRAINT [FK__PersonalC__UserI__10B661E4]
GO
ALTER TABLE [dbo].[ProcedureAttachments]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureAttachments_Procedures] FOREIGN KEY([ProcedureID])
REFERENCES [dbo].[Procedures] ([ID])
GO
ALTER TABLE [dbo].[ProcedureAttachments] CHECK CONSTRAINT [FK_ProcedureAttachments_Procedures]
GO
ALTER TABLE [dbo].[Procedures]  WITH CHECK ADD  CONSTRAINT [FK_Procedures_ProcedureTypes] FOREIGN KEY([ProcedureType])
REFERENCES [dbo].[ProcedureTypes] ([ID])
GO
ALTER TABLE [dbo].[Procedures] CHECK CONSTRAINT [FK_Procedures_ProcedureTypes]
GO
ALTER TABLE [dbo].[Procedures]  WITH CHECK ADD  CONSTRAINT [FK_Procedures_ProcedureWorkflows] FOREIGN KEY([WorkflowID])
REFERENCES [dbo].[ProcedureWorkflows] ([ID])
GO
ALTER TABLE [dbo].[Procedures] CHECK CONSTRAINT [FK_Procedures_ProcedureWorkflows]
GO
ALTER TABLE [dbo].[ProcedureTypes]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureTypes_ProcedureCategories] FOREIGN KEY([ProcedureCategoryID])
REFERENCES [dbo].[ProcedureCategories] ([ID])
GO
ALTER TABLE [dbo].[ProcedureTypes] CHECK CONSTRAINT [FK_ProcedureTypes_ProcedureCategories]
GO
ALTER TABLE [dbo].[ProcedureWorkflowSteps]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureWorkflowSteps_ProcedureWorkflows] FOREIGN KEY([WorkflowID])
REFERENCES [dbo].[ProcedureWorkflows] ([ID])
GO
ALTER TABLE [dbo].[ProcedureWorkflowSteps] CHECK CONSTRAINT [FK_ProcedureWorkflowSteps_ProcedureWorkflows]
GO
ALTER TABLE [dbo].[ProcedureWorkflowTasks]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureWorkflowTasks_ProcedureWorkflowSteps] FOREIGN KEY([WorkflowStepID])
REFERENCES [dbo].[ProcedureWorkflowSteps] ([ID])
GO
ALTER TABLE [dbo].[ProcedureWorkflowTasks] CHECK CONSTRAINT [FK_ProcedureWorkflowTasks_ProcedureWorkflowSteps]
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplates]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureWorkflowTemplates_ProcedureTypes] FOREIGN KEY([ProcedureType])
REFERENCES [dbo].[ProcedureTypes] ([ID])
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplates] CHECK CONSTRAINT [FK_ProcedureWorkflowTemplates_ProcedureTypes]
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplateSteps]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureWorkflowTemplateSteps_ProcedureWorkflowTemplates] FOREIGN KEY([WorkflowTemplateID])
REFERENCES [dbo].[ProcedureWorkflowTemplates] ([ID])
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplateSteps] CHECK CONSTRAINT [FK_ProcedureWorkflowTemplateSteps_ProcedureWorkflowTemplates]
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplateTasks]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureWorkflowTemplateTasks_ProcedureWorkflowTemplateSteps] FOREIGN KEY([WorkflowTemplateStepID])
REFERENCES [dbo].[ProcedureWorkflowTemplateSteps] ([ID])
GO
ALTER TABLE [dbo].[ProcedureWorkflowTemplateTasks] CHECK CONSTRAINT [FK_ProcedureWorkflowTemplateTasks_ProcedureWorkflowTemplateSteps]
GO
ALTER TABLE [dbo].[RelatedDocuments]  WITH CHECK ADD  CONSTRAINT [FK_RelatedDocuments_Documents] FOREIGN KEY([FromDocID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[RelatedDocuments] CHECK CONSTRAINT [FK_RelatedDocuments_Documents]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permissions] ([ID])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permissions]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles]
GO
ALTER TABLE [dbo].[SalarySub]  WITH CHECK ADD  CONSTRAINT [FK_SalarySub_Salary] FOREIGN KEY([SalaryId])
REFERENCES [dbo].[Salary] ([Id])
GO
ALTER TABLE [dbo].[SalarySub] CHECK CONSTRAINT [FK_SalarySub_Salary]
GO
ALTER TABLE [dbo].[TrackingDocuments]  WITH CHECK ADD  CONSTRAINT [FK_TrackingDocuments_Documents] FOREIGN KEY([DocID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[TrackingDocuments] CHECK CONSTRAINT [FK_TrackingDocuments_Documents]
GO
ALTER TABLE [dbo].[TrackingDocuments]  WITH CHECK ADD  CONSTRAINT [FK_TrackingDocuments_TrackingStatusDocument] FOREIGN KEY([Status])
REFERENCES [dbo].[TrackingStatusDocument] ([ID])
GO
ALTER TABLE [dbo].[TrackingDocuments] CHECK CONSTRAINT [FK_TrackingDocuments_TrackingStatusDocument]
GO
ALTER TABLE [dbo].[TrackingWorkflowDocuments]  WITH CHECK ADD  CONSTRAINT [FK_TrackingWorkflowDocuments_TrackingDocuments] FOREIGN KEY([TrackingDocumentID])
REFERENCES [dbo].[TrackingDocuments] ([ID])
GO
ALTER TABLE [dbo].[TrackingWorkflowDocuments] CHECK CONSTRAINT [FK_TrackingWorkflowDocuments_TrackingDocuments]
GO
ALTER TABLE [dbo].[TransferDocuments]  WITH CHECK ADD  CONSTRAINT [FK_TransferDocuments_Documents] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Documents] ([ID])
GO
ALTER TABLE [dbo].[TransferDocuments] CHECK CONSTRAINT [FK_TransferDocuments_Documents]
GO
ALTER TABLE [dbo].[TreeFilterDocuments]  WITH NOCHECK ADD  CONSTRAINT [FK_TreeFilterDocuments_TreeFilterDocuments] FOREIGN KEY([ParentID])
REFERENCES [dbo].[TreeFilterDocuments] ([ID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TreeFilterDocuments] NOCHECK CONSTRAINT [FK_TreeFilterDocuments_TreeFilterDocuments]
GO
ALTER TABLE [dbo].[TSConfigDateOff]  WITH NOCHECK ADD  CONSTRAINT [FK_TSConfigDateOff_TSConfigTimeShift] FOREIGN KEY([ConfigTimeShiftID])
REFERENCES [dbo].[TSConfigTimeShift] ([ID])
GO
ALTER TABLE [dbo].[TSConfigDateOff] CHECK CONSTRAINT [FK_TSConfigDateOff_TSConfigTimeShift]
GO
ALTER TABLE [dbo].[TSConfigTimeDay]  WITH NOCHECK ADD  CONSTRAINT [FK_TSConfigTimeDay_TSConfigTimeShift] FOREIGN KEY([ConfigTimeShiftID])
REFERENCES [dbo].[TSConfigTimeShift] ([ID])
GO
ALTER TABLE [dbo].[TSConfigTimeDay] CHECK CONSTRAINT [FK_TSConfigTimeDay_TSConfigTimeShift]
GO
ALTER TABLE [dbo].[TSLeaveProcess]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveProcess_TSLeaveRequest] FOREIGN KEY([LeaveRequestID])
REFERENCES [dbo].[TSLeaveRequest] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveProcess] CHECK CONSTRAINT [FK_TSLeaveProcess_TSLeaveRequest]
GO
ALTER TABLE [dbo].[TSLeaveProcess]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveProcess_TSWorkFlowStatus] FOREIGN KEY([WorkFlowStatusID])
REFERENCES [dbo].[TSWorkFlowStatus] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveProcess] CHECK CONSTRAINT [FK_TSLeaveProcess_TSWorkFlowStatus]
GO
ALTER TABLE [dbo].[TSLeaveRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveRequest_TSWorkFlowStatus] FOREIGN KEY([WorkFlowStatusID])
REFERENCES [dbo].[TSWorkFlowStatus] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveRequest] CHECK CONSTRAINT [FK_TSLeaveRequest_TSWorkFlowStatus]
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveRequestDetail_TSConfigTimeShift] FOREIGN KEY([TSConfigTimeShiftID])
REFERENCES [dbo].[TSConfigTimeShift] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail] CHECK CONSTRAINT [FK_TSLeaveRequestDetail_TSConfigTimeShift]
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveRequestDetail_TSLeaveRequest] FOREIGN KEY([LeaveRequestID])
REFERENCES [dbo].[TSLeaveRequest] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail] CHECK CONSTRAINT [FK_TSLeaveRequestDetail_TSLeaveRequest]
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSLeaveRequestDetail_TSLeaveType] FOREIGN KEY([LeaveTypeID])
REFERENCES [dbo].[TSLeaveType] ([ID])
GO
ALTER TABLE [dbo].[TSLeaveRequestDetail] CHECK CONSTRAINT [FK_TSLeaveRequestDetail_TSLeaveType]
GO
ALTER TABLE [dbo].[TSObjectDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSObjectDetail_TSWorkFlowDetail] FOREIGN KEY([WorkFlowDetailID])
REFERENCES [dbo].[TSWorkFlowDetail] ([ID])
GO
ALTER TABLE [dbo].[TSObjectDetail] CHECK CONSTRAINT [FK_TSObjectDetail_TSWorkFlowDetail]
GO
ALTER TABLE [dbo].[TSWorkFlowCondition]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowCondition_TSWorkFlowAttributes] FOREIGN KEY([AttributeID])
REFERENCES [dbo].[TSWorkFlowAttributes] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowCondition] CHECK CONSTRAINT [FK_TSWorkFlowCondition_TSWorkFlowAttributes]
GO
ALTER TABLE [dbo].[TSWorkFlowCondition]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowCondition_TSWorkFlowOperation] FOREIGN KEY([OperationID])
REFERENCES [dbo].[TSWorkFlowOperation] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowCondition] CHECK CONSTRAINT [FK_TSWorkFlowCondition_TSWorkFlowOperation]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowDetail_TSObjectType] FOREIGN KEY([ObjectTypeID])
REFERENCES [dbo].[TSObjectType] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] CHECK CONSTRAINT [FK_TSWorkFlowDetail_TSObjectType]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowCondition] FOREIGN KEY([WorkFlowConditionID])
REFERENCES [dbo].[TSWorkFlowCondition] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] CHECK CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowCondition]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowInfo] FOREIGN KEY([WorkFlowInfoID])
REFERENCES [dbo].[TSWorkFlowInfo] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] CHECK CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowInfo]
GO
ALTER TABLE [dbo].[TSWorkFlowDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowStatus] FOREIGN KEY([WorkFlowStatusID])
REFERENCES [dbo].[TSWorkFlowStatus] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowDetail] CHECK CONSTRAINT [FK_TSWorkFlowDetail_TSWorkFlowStatus]
GO
ALTER TABLE [dbo].[TSWorkFlowInfo]  WITH NOCHECK ADD  CONSTRAINT [FK_TSWorkFlowInfo_TSScope] FOREIGN KEY([TimeSheetScopeID])
REFERENCES [dbo].[TSScope] ([ID])
GO
ALTER TABLE [dbo].[TSWorkFlowInfo] CHECK CONSTRAINT [FK_TSWorkFlowInfo_TSScope]
GO
ALTER TABLE [dbo].[UserDepartments]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartments_Departments] FOREIGN KEY([DeptID])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [dbo].[UserDepartments] CHECK CONSTRAINT [FK_UserDepartments_Departments]
GO
ALTER TABLE [dbo].[UserDepartments]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartments_JobTitles] FOREIGN KEY([JobTitleID])
REFERENCES [dbo].[JobTitles] ([ID])
GO
ALTER TABLE [dbo].[UserDepartments] CHECK CONSTRAINT [FK_UserDepartments_JobTitles]
GO
ALTER TABLE [dbo].[UserDepartments]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartments_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserDepartments] CHECK CONSTRAINT [FK_UserDepartments_Users]
GO
ALTER TABLE [dbo].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Groups]
GO
ALTER TABLE [dbo].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Users]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[WorkFlowStepDocuments]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStepDocuments_WorkFlowDocuments] FOREIGN KEY([WorkFlowID])
REFERENCES [dbo].[WorkFlowDocuments] ([ID])
GO
ALTER TABLE [dbo].[WorkFlowStepDocuments] CHECK CONSTRAINT [FK_WorkFlowStepDocuments_WorkFlowDocuments]
GO
ALTER TABLE [dbo].[WorkFlowStepDocuments]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStepDocuments_WorkFlowTypeDocuments] FOREIGN KEY([WorkFlowTypeID])
REFERENCES [dbo].[WorkFlowTypeDocuments] ([ID])
GO
ALTER TABLE [dbo].[WorkFlowStepDocuments] CHECK CONSTRAINT [FK_WorkFlowStepDocuments_WorkFlowTypeDocuments]
GO
ALTER TABLE [nav].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_nav.Menu_nav.NavNode_NavNodeId] FOREIGN KEY([NavNodeId])
REFERENCES [nav].[NavNode] ([Id])
GO
ALTER TABLE [nav].[Menu] CHECK CONSTRAINT [FK_nav.Menu_nav.NavNode_NavNodeId]
GO
ALTER TABLE [Task].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Project] FOREIGN KEY([ProjectId])
REFERENCES [Task].[Project] ([Id])
GO
ALTER TABLE [Task].[Attachment] CHECK CONSTRAINT [FK_Attachment_Project]
GO
ALTER TABLE [Task].[ProjectCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategory_ProjectCategory1] FOREIGN KEY([ParentId])
REFERENCES [Task].[ProjectCategory] ([Id])
GO
ALTER TABLE [Task].[ProjectCategory] CHECK CONSTRAINT [FK_ProjectCategory_ProjectCategory1]
GO
ALTER TABLE [Task].[ProjectFolderDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFolderDetail_Project] FOREIGN KEY([ProjectId])
REFERENCES [Task].[Project] ([Id])
GO
ALTER TABLE [Task].[ProjectFolderDetail] CHECK CONSTRAINT [FK_ProjectFolderDetail_Project]
GO
ALTER TABLE [Task].[ProjectFollow]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFollow_Project] FOREIGN KEY([ProjectId])
REFERENCES [Task].[Project] ([Id])
GO
ALTER TABLE [Task].[ProjectFollow] CHECK CONSTRAINT [FK_ProjectFollow_Project]
GO
ALTER TABLE [Task].[ProjectHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectHistory_Project] FOREIGN KEY([ProjectId])
REFERENCES [Task].[Project] ([Id])
GO
ALTER TABLE [Task].[ProjectHistory] CHECK CONSTRAINT [FK_ProjectHistory_Project]
GO
ALTER TABLE [Task].[TaskItemAppraiseHistory]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemAppraiseHistory_TaskItemAssign] FOREIGN KEY([TaskItemAssignId])
REFERENCES [Task].[TaskItemAssign] ([Id])
GO
ALTER TABLE [Task].[TaskItemAppraiseHistory] CHECK CONSTRAINT [FK_TaskItemAppraiseHistory_TaskItemAssign]
GO
ALTER TABLE [Task].[TaskItemCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_TaskItemCategory_TaskItemCategory] FOREIGN KEY([ParentId])
REFERENCES [Task].[TaskItemCategory] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [Task].[TaskItemCategory] CHECK CONSTRAINT [FK_TaskItemCategory_TaskItemCategory]
GO
ALTER TABLE [Task].[TaskItemKpi]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemKpi_TaskItemAssign] FOREIGN KEY([TaskItemAssignId])
REFERENCES [Task].[TaskItemAssign] ([Id])
GO
ALTER TABLE [Task].[TaskItemKpi] CHECK CONSTRAINT [FK_TaskItemKpi_TaskItemAssign]
GO
ALTER TABLE [Task].[TaskItemProcessHistory]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemProcessHistory_TaskItemAssign] FOREIGN KEY([TaskItemAssignId])
REFERENCES [Task].[TaskItemAssign] ([Id])
GO
ALTER TABLE [Task].[TaskItemProcessHistory] CHECK CONSTRAINT [FK_TaskItemProcessHistory_TaskItemAssign]
GO
ALTER TABLE [Workspace].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Workspace.Attachment_Workspace.Post_PostId] FOREIGN KEY([PostId])
REFERENCES [Workspace].[Post] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Workspace].[Attachment] CHECK CONSTRAINT [FK_Workspace.Attachment_Workspace.Post_PostId]
GO
ALTER TABLE [Workspace].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Workspace.Permission_Workspace.Post_PostId] FOREIGN KEY([PostId])
REFERENCES [Workspace].[Post] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Workspace].[Permission] CHECK CONSTRAINT [FK_Workspace.Permission_Workspace.Post_PostId]
GO
/****** Object:  StoredProcedure [dbo].[Report_Document]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Report_Document]
	@FromDate datetime,
	@ToDate datetime,
	@departmentId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #DepartmentSigns (DepartmentId UNIQUEIDENTIFIER,
									DepartmentName NVARCHAR(MAX),
									DelayTime INT,
									TotalSign INT,
									SignSlowly INT);
	-- LẤY BÁO CÁO PHÒNG BANG CON 
	DECLARE @Check bit=1,
				@DeptId UNIQUEIDENTIFIER = CAST(0x0 AS UNIQUEIDENTIFIER),
				@DeptName NVARCHAR(MAX),
				@DeptParentId UNIQUEIDENTIFIER;

	CREATE TABLE #DeptTemp (Id UNIQUEIDENTIFIER);
	CREATE TABLE #DepartmentRoot (Id UNIQUEIDENTIFIER, [Name] NVARCHAR(MAX));
	INSERT INTO #DepartmentRoot SELECT D.ID, D.[Name] FROM Departments D WHERE D.ID=@departmentId;
	INSERT INTO #DepartmentRoot SELECT D.ID, D.[Name] FROM Departments D WHERE D.ParentID=@departmentId;
	WHILE(@Check = 1)
	BEGIN 
			SET @DeptId = (SELECT TOP 1 Id FROM #DepartmentRoot WHERE Id NOT IN (SELECT * FROM #DeptTemp));
			IF(@DeptId IS NULL OR @DeptId =CAST(0x0 AS UNIQUEIDENTIFIER))
			BEGIN
				SET @Check =0;
				BREAK;
			END

			SET @DeptName =(SELECT TOP 1 Name FROM #DepartmentRoot WHERE Id = @DeptId);
			INSERT INTO #DeptTemp(Id) values(@DeptId);
			-- Lấy tất cả các phòng ban con trức thuộc
			CREATE TABLE #DepartmentTemp(Id UNIQUEIDENTIFIER);
			IF(@DeptId != @departmentId)
			BEGIN
				WITH Department (Id)
				AS
				( SELECT Id
				  FROM Departments
				  WHERE ID =@DeptId
				  UNION ALL
					SELECT e.ID
					FROM Departments e
					INNER JOIN Department c ON e.ParentID = c.Id)
				INSERT INTO #DepartmentTemp SELECT Id FROM Department
			END
			ELSE
			BEGIN
				INSERT INTO #DepartmentTemp values(@DeptId);
			END
			-- LẤY BÁO CÁO THEO PHÒNG BAN
			INSERT INTO #DepartmentSigns(DepartmentId,DelayTime,TotalSign,SignSlowly) SELECT 
				@DeptId,
				COUNT(CASE 
						WHEN T.Processed IS NULL AND GETDATE () > T.DueDate THEN DATEDIFF(HOUR,T.DueDate,GETDATE()) 
						WHEN T.Processed IS NOT NULL AND T.Processed > T.DueDate THEN DATEDIFF(HOUR,T.DueDate,T.Processed)
						ELSE NULL END) AS DelayTime,
				COUNT(DISTINCT D.Id) AS TotalDocument,
				COUNT(DISTINCT (CASE WHEN (T.Processed IS NULL AND GETDATE () > T.DueDate) OR (T.Processed > T.DueDate) THEN D.Id ELSE NULL END)) AS TotalSlowly
			FROM BusinessWorkflow.Document D
			INNER JOIN BusinessWorkflow.WorkflowStep S ON S.Document_Id = D.Id
			INNER JOIN BusinessWorkflow.WorkflowTask T ON T.WorkflowStep_Id = S.Id
			WHERE D.Created > @FromDate
				  AND D.Created < @ToDate
				  AND T.ParentTask = CAST(0x0 AS UNIQUEIDENTIFIER)
				  AND D.Status NOT IN (-1,0,5,6)
				  AND T.DueDate IS NOT NULL
				  AND T.AssignTo IN (SELECT UD.UserID FROM UserDepartments UD WHERE UD.DeptID IN 
						(SELECT Id FROM #DepartmentTemp));
			UPDATE #DepartmentSigns SET DepartmentName = @DeptName WHERE DepartmentId = @DeptId;
			DROP TABLE #DepartmentTemp;
		END
	SELECT * FROM #DepartmentSigns WHERE TotalSign > 0
	--XÓA BẢNG
	DROP TABLE #DeptTemp;
	DROP TABLE #DepartmentRoot;
	DROP TABLE #DepartmentSigns;
END

GO
/****** Object:  StoredProcedure [dbo].[Report_DocumentByDepartment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Report_DocumentByDepartment]
	@FromDate datetime,
	@ToDate datetime,
	@DepartmentId UNIQUEIDENTIFIER NULL,
	@IsRoot bit = 1,
	@isDeptRoot bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #DepartmentSigns (DepartmentId UNIQUEIDENTIFIER,
									ParentId UNIQUEIDENTIFIER,
									DepartmentName NVARCHAR(MAX),
									TotalSign INT, 
									SignSlowly INT,
									SignDone INT,
									UserId UNIQUEIDENTIFIER NULL,
									UserFullName NVARCHAR(MAX) NULL);
	
	-- LẤY BÁO CÁO PHÒNG BANG CHỈ ĐỊNH GROUP BY THEO PHÒNG BAN
	IF(@IsRoot = 1)
	BEGIN
		DECLARE @Check bit=1,
				@DeptId UNIQUEIDENTIFIER = CAST(0x0 AS UNIQUEIDENTIFIER),
				@DeptName NVARCHAR(MAX),
				@DeptParentId UNIQUEIDENTIFIER;

		CREATE TABLE #DeptTemp (Id UNIQUEIDENTIFIER);
		CREATE TABLE #DepartmentRoot (Id UNIQUEIDENTIFIER, [Name] NVARCHAR(MAX),ParentId UNIQUEIDENTIFIER);
		INSERT INTO #DepartmentRoot SELECT D.ID, D.[Name],D.ParentID FROM Departments D WHERE D.ID = @DepartmentId;
		IF(@isDeptRoot = 0)
		BEGIN
			INSERT INTO #DepartmentRoot SELECT C.ID, C.[Name],C.ParentID FROM Departments D INNER JOIN Departments C ON C.ParentID = D.ID 
				WHERE D.ID=@DepartmentId;
		END
		WHILE(@Check = 1)
		BEGIN 
			SET @DeptId = (SELECT TOP 1 Id FROM #DepartmentRoot WHERE Id NOT IN (SELECT * FROM #DeptTemp));
			IF(@DeptId IS NULL OR @DeptId =CAST(0x0 AS UNIQUEIDENTIFIER))
			BEGIN
				SET @Check =0;
				BREAK;
			END

			SET @DeptName =(SELECT TOP 1 Name FROM #DepartmentRoot WHERE Id = @DeptId);
			SET @DeptParentId = (SELECT TOP 1 ParentId FROM #DepartmentRoot WHERE Id = @DeptId);
			INSERT INTO #DeptTemp SELECT TOP 1 Id FROM #DepartmentRoot WHERE Id = @DeptId;

			-- LẤY BÁO CÁO THEO PHÒNG BAN
			IF(@DeptId = @DepartmentId)
			BEGIN
				INSERT INTO #DepartmentSigns(DepartmentId,TotalSign,SignSlowly,SignDone) SELECT 
				@DeptId,
				COUNT(D.Id) AS TotalTaskDone,
				COUNT(CASE WHEN  (T.Processed IS NULL AND GETDATE () > T.DueDate) OR (T.Processed > T.DueDate) THEN 1 ELSE NULL END) AS SignSlowly,
				COUNT(CASE WHEN (T.Processed IS NULL AND GETDATE () < T.DueDate) OR (T.Processed < T.DueDate)  THEN 1 ELSE NULL END) 
				AS SignDone
			FROM BusinessWorkflow.Document D
				INNER JOIN BusinessWorkflow.WorkflowStep S ON S.Document_Id = D.Id
				INNER JOIN BusinessWorkflow.WorkflowTask T ON T.WorkflowStep_Id = S.Id
			WHERE D.Created > @FromDate
				  AND D.Created < @ToDate
				  AND D.Status NOT IN (-1,0,5,6)
				  AND T.DueDate IS NOT NULL
				  AND T.ParentTask = CAST(0x0 AS UNIQUEIDENTIFIER)
				  AND T.AssignTo IN (SELECT UD.UserID FROM UserDepartments UD WHERE UD.DeptID = @DeptId);
			END
			ELSE
			BEGIN
				INSERT INTO #DepartmentSigns(DepartmentId,TotalSign,SignSlowly,SignDone) SELECT 
				@DeptId,
				COUNT(D.Id) AS TotalTaskDone,
				COUNT(CASE WHEN  (T.Processed IS NULL AND GETDATE () > T.DueDate) OR (T.Processed > T.DueDate) THEN 1 ELSE NULL END) AS SignSlowly,
				COUNT(CASE WHEN (T.Processed IS NULL AND GETDATE () < T.DueDate) OR (T.Processed < T.DueDate)  THEN 1 ELSE NULL END) 
				AS SignDone
			FROM BusinessWorkflow.Document D
				INNER JOIN BusinessWorkflow.WorkflowStep S ON S.Document_Id = D.Id
				INNER JOIN BusinessWorkflow.WorkflowTask T ON T.WorkflowStep_Id = S.Id
			WHERE D.Created > @FromDate
				  AND D.Created < @ToDate
				  AND D.Status NOT IN (-1,0,5,6)
				  AND T.AssignTo IN (SELECT UD.UserID FROM UserDepartments UD WHERE UD.DeptID IN 
						(SELECT
						CASE 
							WHEN C2.ID IS NOT NULL THEN C2.ID
							WHEN C1.ID IS NOT NULL THEN C1.ID
							WHEN C.ID IS NOT NULL THEN C.ID
							ELSE D.ID
						END AS Id
						FROM Departments D
							LEFT JOIN Departments C on C.ParentID = D.ID
							LEFT JOIN Departments C1 ON C1.ParentID = C.ID
							LEFT JOIN Departments C2 ON C2.ParentID = C1.ID
						WHERE D.ID=@DeptId));
			END
			UPDATE #DepartmentSigns SET DepartmentName =@DeptName, ParentId = @DeptParentId WHERE DepartmentId = @DeptId;
		END
		SELECT * FROM #DepartmentSigns WHERE TotalSign > 0
		DROP TABLE #DeptTemp;
		DROP TABLE #DepartmentRoot;
	END
	-- LẤY BÁO CÁO THEO USER PHÒNG BAN CHỈ ĐỊNH
	ELSE
	BEGIN
		CREATE TABLE #DepartmentRoot1 (Id UNIQUEIDENTIFIER, [Name] NVARCHAR(MAX),ParentId UNIQUEIDENTIFIER,IdRoot UNIQUEIDENTIFIER);
		INSERT INTO #DepartmentRoot1 SELECT D.ID, D.[Name],D.ParentID,D.ID FROM Departments D WHERE D.ID = @DepartmentId;
		IF(@isDeptRoot = 0)
		BEGIN
			INSERT INTO #DepartmentRoot1  SELECT
										CASE 
											WHEN C2.ID IS NOT NULL THEN C2.ID
											WHEN C1.ID IS NOT NULL THEN C1.ID
											WHEN C.ID IS NOT NULL THEN C.ID
											ELSE D.ID
										END AS Id,
										CASE 
											WHEN C2.ID IS NOT NULL THEN C2.Name
											WHEN C1.ID IS NOT NULL THEN C1.Name
											WHEN C.ID IS NOT NULL THEN C.Name
											ELSE D.Name
										END AS DeptName,
										CASE 
											WHEN C2.ID IS NOT NULL THEN C2.ParentID
											WHEN C1.ID IS NOT NULL THEN C1.ParentID
											WHEN C.ID IS NOT NULL THEN C.ParentID
											ELSE D.ParentID
										END AS ParentId,
										D.Id AS IdRoot
									FROM Departments D
											LEFT JOIN Departments C on C.ParentID = D.ID
											LEFT JOIN Departments C1 ON C1.ParentID = C.ID
											LEFT JOIN Departments C2 ON C2.ParentID = C1.ID
									WHERE D.ParentID = @DepartmentId;
		END
		INSERT INTO #DepartmentSigns
		SELECT 
				USERJOIN.DeptId,
				USERJOIN.ParentId,
				USERJOIN.Name,
				COUNT(D.Id) AS TotalTaskDone,
				COUNT(CASE WHEN  (T.Processed IS NULL AND GETDATE () > T.DueDate) OR (T.Processed > T.DueDate) THEN 1 ELSE NULL END) AS SignSlowly,
				COUNT(CASE WHEN  (T.Processed IS NULL AND GETDATE () < T.DueDate) OR (T.Processed < T.DueDate) THEN 1 ELSE NULL END) AS SignDone,
				USERJOIN.UserID,
				USERJOIN.FullName
		FROM BusinessWorkflow.Document D
			INNER JOIN BusinessWorkflow.WorkflowStep S ON S.Document_Id = D.Id
			INNER JOIN BusinessWorkflow.WorkflowTask T ON T.WorkflowStep_Id = S.Id
			INNER JOIN (SELECT UD.UserID ,U.FullName,DJOIN.Id AS DeptId,DJOIN.ParentId , DJOIN.[Name],DJOIN.IdRoot AS IdInput 
						FROM UserDepartments UD 
						INNER JOIN #DepartmentRoot1 DJOIN ON DJOIN.Id = UD.DeptID
						LEFT JOIN Users U ON U.ID = UD.UserID) USERJOIN
						ON USERJOIN.UserID = T.AssignTo
			WHERE D.Created > @FromDate
				  AND D.Created < @ToDate
				  AND D.Status NOT IN (-1,0,5,6)
				  AND T.DueDate IS NOT NULL
				  AND T.ParentTask = CAST(0x0 AS UNIQUEIDENTIFIER)
				 --AND USERJOIN.IdInput =  @DepartmentId
			GROUP BY USERJOIN.DeptId,USERJOIN.ParentId, USERJOIN.Name,USERJOIN.UserID, USERJOIN.FullName;

		SELECT * FROM #DepartmentSigns WHERE  TotalSign > 0 
	END
	--XÓA BẢNG
	DROP TABLE #DepartmentSigns;
END

GO
/****** Object:  StoredProcedure [dbo].[Select_OnlineUser_Date_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Select_OnlineUser_Date_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;	
    Select  
		tbDateOrg.Year,
	    tbDateOrg.Month,
		tbDateOrg.Date,
		tbDateOrg.OrgID,
		D.Name AS OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'
	from 
		WebAnalysis_Date_Org tbDateOrg 
		inner join Departments D on tbDateOrg.OrgID = D.ID  
	where 
		D.ParentID = @OrgParentID
		And D.IsActive = 1
		And @FromDate <= DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate > DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.Date,tbDateOrg.OrgID, D.Name, D.OrderNumber
	order by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.Date, D.OrderNumber
END


GO
/****** Object:  StoredProcedure [dbo].[Select_OnlineUser_Month_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Select_OnlineUser_Month_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    Select  
		tbDateOrg.Year,
	    tbDateOrg.Month,
		tbDateOrg.OrgID,
		tbOrg.Name AS OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'
	from 
		WebAnalysis_Date_Org tbDateOrg
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID  
	where 
		tbOrg.ParentID=@OrgParentID
		And tbOrg.IsActive=1
		And @FromDate<=DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate>DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.OrgID,tbOrg.Name,tbOrg.OrderNumber
	order by 
		tbDateOrg.Year,tbDateOrg.Month , tbOrg.OrderNumber
END


GO
/****** Object:  StoredProcedure [dbo].[Select_OnlineUser_Quarter_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_OnlineUser_Quarter_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	
    Select 
		tbDateOrg.Year,
	    Case 
			When (tbDateOrg.Month <= 3) Then 1 
			When (tbDateOrg.Month > 3 and tbDateOrg.Month <= 6 ) Then 2 
			When (tbDateOrg.Month > 6 and tbDateOrg.Month <= 9 ) Then 3 
			Else 4 
		End  as'Quarter' , 
		tbDateOrg.OrgID,
		tbOrg.Name OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'

	from 
		WebAnalysis_Date_Org tbDateOrg
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID  
	where 
		tbOrg.ParentID = @OrgParentID
		And tbOrg.IsActive=1
		And @FromDate <= DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate > DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.OrgID,tbOrg.Name,tbOrg.OrderNumber
	order by 
		tbDateOrg.Year,tbDateOrg.Month , tbOrg.OrderNumber

END


GO
/****** Object:  StoredProcedure [dbo].[Select_OnlineUser_Week_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_OnlineUser_Week_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
    Select  
		DATEPART(wk,DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)) as 'Week', 
		tbDateOrg.OrgID,
		tbOrg.Name OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'

	from 
		WebAnalysis_Date_Org tbDateOrg
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID  
	where 
		tbOrg.ParentID=@OrgParentID
		And 
		tbOrg.IsActive=1
		And @FromDate <= DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate > DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date, tbDateOrg.OrgID, tbOrg.Name,tbOrg.OrderNumber
	order by 
		tbOrg.OrderNumber

END


GO
/****** Object:  StoredProcedure [dbo].[Select_OnlineUser_Year_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_OnlineUser_Year_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
    Select 
		tbDateOrg.Year,
		tbDateOrg.OrgID,
		tbOrg.Name OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'
	from 
		WebAnalysis_Date_Org tbDateOrg
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID
	where 
		tbOrg.ParentID = @OrgParentID
		And 
		tbOrg.IsActive = 1
		And @FromDate <= DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate > DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.OrgID,tbOrg.Name,tbOrg.OrderNumber
	order by
		tbDateOrg.Year , tbOrg.OrderNumber
  
END


GO
/****** Object:  StoredProcedure [dbo].[Select_RequestTotal_Module_UserNameFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_RequestTotal_Module_UserNameFromDateToDate]
	@UserName nvarchar(200),
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
 
	Select 
		tbModuleOrgUser.ModuleID, 
		tbModule.Description ModuleName,
		SUM(tbModuleOrgUser.RequestTotal) as 'RequestTotal'
	From
	   WebAnalysis_Date_Module_OrgUser tbModuleOrgUser 
	   left join Modules tbModule on tbModuleOrgUser.ModuleID = tbModule.ID
	where 
	   tbModuleOrgUser.UserName = @UserName
	   And @FromDate <= DATEFROMPARTS(tbModuleOrgUser.Year,tbModuleOrgUser.Month,tbModuleOrgUser.Date)
	   And @ToDate > DATEFROMPARTS(tbModuleOrgUser.Year,tbModuleOrgUser.Month,tbModuleOrgUser.Date)
	Group by  
		tbModuleOrgUser.ModuleID, tbModule.Description
	order by 
		RequestTotal desc
	
END


GO
/****** Object:  StoredProcedure [dbo].[Select_RequestTotal_User_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_RequestTotal_User_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
    Select 
		tbUserInfo.UserName,
		tbUserInfo.FullName,
		SUM(tbOrgUser.RequestTotal) as 'RequestTotal'
	from 
	    WebAnalysis_Date_OrgUser tbOrgUser 
		inner join Users tbUserInfo on tbOrgUser.UserName = tbUserInfo.UserName 
		inner join Departments tbOrg on tbOrgUser.OrgID = tbOrg.ID
	where 
		tbOrgUser.OrgID = @OrgParentID
		And 
		tbOrg.IsActive = 1
		And @FromDate<=DATEFROMPARTS(tbOrgUser.Year,tbOrgUser.Month,tbOrgUser.Date)
		And @ToDate>DATEFROMPARTS(tbOrgUser.Year,tbOrgUser.Month,tbOrgUser.Date)
	Group by 
		tbUserInfo.UserName,tbUserInfo.FullName
END


GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessWorkflow_ReportDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BusinessWorkflow_ReportDocument]
--DECLARE
	@ViewUserIds	NVARCHAR(MAX)	= NULL, -- Xem báo cáo phân quyền theo phòng ban user login
	@KeyWord		NVARCHAR(200)	= NULL, -- TỪ KHÓA
	@Author 		NVARCHAR(36)	= NULL, -- NGƯỜI TẠO
	@FromCreateDate		DATETIME	= NULL,	-- THỜI GIAN TẠO TỪ
	@ToCreateDate		DATETIME	= NULL, -- THỜI GIAN TẠO TỚI
	@AssignTo 		NVARCHAR(36)	= NULL, -- NGƯỜI XỬ LÝ
	@FromProcessDate    DATETIME    = NULL, -- THỜI GIAN XỬ LÝ TỪ
	@ToProcessDate      DATETIME    = NULL, -- THỜI GIAN XỬ LÝ ĐẾN
	@Status				INT			= -1,	-- TRẠNG THÁI VĂN BẢN
	@UserDepartmentIds NVARCHAR(MAX)= NULL, -- LIST USER THUỘC PHÒNG BAN (;)
	@DocumentTypeId NVARCHAR(36)	= NULL, -- LOẠI VĂN BẢN
	@OperationId        INT			= NULL  -- LOẠI XỬ LÝ
AS
BEGIN TRY
	SET NOCOUNT ON;
	--Khai báo ------------------------------------------------------------------------
	DECLARE @SqlFilter nvarchar(MAX),
			@SqlFrom nvarchar(1000),
			@Sql nvarchar(MAX),
			@IsCurrentStepFilter int,
			@IsUserFilter int,
			@AuthorViewFilter nvarchar(1000),
			@SupervisorViewFilter nvarchar(1000),
			@ShareViewFilter nvarchar(1000),
			@AssignToViewFilter nvarchar(1000)
	--Hết khai báo ------------------------------------------------------------------------
	SET @SqlFrom='from BusinessWorkflow.Document doc '
	SET @IsCurrentStepFilter=0
	SET @IsUserFilter=0
	SET @SqlFilter ='where doc.Status <> -1 ';	
	SET @AuthorViewFilter= 'doc.Author = ''' + @AssignTo + ''' '
	SET @SupervisorViewFilter= 'dts.SupervisorId = ''' + @AssignTo + ''' '
	SET @ShareViewFilter= 'doc.InternalReceiver LIKE  ''%' + @AssignTo + '%'' '
	SET @AssignToViewFilter= '(doc.CurrentStepOrder> = step.[Order] And (task.AssignTo=''' + @AssignTo + ''' or task.DelegatedUserId=''' + @AssignTo + ''')) '	

	IF (@Author IS NOT NULL)
	BEGIN
		SET @SqlFilter = @SqlFilter + 'And doc.Author = ''' + @Author + ''' ';		
	END

	IF (@AssignTo IS NOT NULL)
	BEGIN
		SET @IsUserFilter=1
		--SET @SqlFilter = @SqlFilter + 'And (task.AssignTo = ''' + @AssignTo + ''' or task.DelegatedUserId = ''' + @AssignTo + ''')';	
		SET @SqlFilter = @SqlFilter + 'And ('+ @AuthorViewFilter +
			' or '+ @ShareViewFilter +
			' or '+ @SupervisorViewFilter +
			' or ' +@AssignToViewFilter +') ';	
	END

	IF (@FromCreateDate IS NOT NULL)
	BEGIN
		 SET @SqlFilter = @SqlFilter + 'And CAST(doc.Created AS date) >= ''' + CONVERT(NVARCHAR(20), @FromCreateDate, 120) + ''' ';	
	END

	IF (@ToCreateDate IS NOT NULL)
	BEGIN
		 SET @SqlFilter = @SqlFilter + 'And CAST(doc.Created AS date) <= ''' + CONVERT(NVARCHAR(20), @ToCreateDate, 120) + ''' ';	
	END
	
	IF (@DocumentTypeId IS NOT NULL)
	BEGIN
		SET @SqlFilter = @SqlFilter + 'And doc.DocumentTypeId = ''' + @DocumentTypeId + ''' ';		
	END
	
	IF (@KeyWord IS NOT NULL And @KeyWord != '')
	 BEGIN
		SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
		SET @SqlFilter = @SqlFilter + ' 
		And (CONTAINS(doc.Serial, N''' + @KeyWord +''') 
		OR CONTAINS(doc.Title, N''' + @KeyWord +''') 
		OR CONTAINS(doc.Subject, N''' + @KeyWord +''') 
		OR CONTAINS(doc.ExtensiveFields, N''' + @KeyWord +''')) ';
	 END

	IF (@UserDepartmentIds IS NOT NULL) -- Lọc theo đơn vị trình
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter  + ' 
		And doc.Author IN (SELECT CAST(value as uniqueidentifier) FROM STRING_SPLIT('''+@UserDepartmentIds +''', '';'')) ';
	END

	IF(@ViewUserIds IS NOT NULL) -- LỌC THEO PHÂN QUYỀN PHÒNG BAN THẤY THẤY NHỮNG NGƯỜI CÙNG PHÒNG VÀ NHỮNG PHÒNG BAN CON
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter  + ' 
		And task.AssignTo IN (SELECT CAST(value as uniqueidentifier) FROM STRING_SPLIT('''+@ViewUserIds +''', '';'')) ';
	END
	 -- -1: Đã xóa
	 -- 1: Chờ tôi ký
	 -- 0: Bản nháp
	 -- 1; Chờ tôi ký
	 -- 2: Đang xử lý
	 -- 3: Đã ký
	 -- 4: Bị trả lại
	 -- 5: Bị từ chối
	 -- 6: Công việc cần xử lý
	 -- 7: Đã giao
	 -- 8: Báo cáo
	
	IF (@Status IS NULL OR @Status in (0,2,3,4,5))
	BEGIN
		SET @SqlFilter = @SqlFilter + 'And doc.Status = '+Cast(@Status as nvarchar(2));
	END

	IF (@Status in (1,7,8,9,10)) 
	BEGIN
		SET @IsCurrentStepFilter=1
	END

	IF (@Status = 1) -- Chờ tôi ký
	BEGIN
		SET @SqlFilter = @SqlFilter + '
		And doc.Status = 2
		And task.ParentTask = Cast(0x0 as uniqueidentifier) 
		And task.StartDate is not null 
		And task.IsWaitSecretary <> 1 
		And task.IsProcessed <> 1 
		And task.DelegatedUserId IS NULL
		And task.AssignTo = ''' + @AssignTo + ''' '	
	END

	IF (@Status = 7) -- Đã giao
	BEGIN
		SET @SqlFilter = @SqlFilter + '
		And doc.Status = 2
		And task.AssignById = ''' + @AssignTo + ''' ';	
	END

	IF (@Status = 8) -- Báo cáo
	BEGIN
	SET @SqlFilter = @SqlFilter + ' 
		And doc.Status = 2
		And task.IsProcessed = 1 
		And task.AssignById = ''' + @AssignTo + ''' ';	
	END

	IF (@Status = 9) -- Đã ủy quyền
	BEGIN
		SET @SqlFilter = @SqlFilter + '
		And doc.Status = 2
		And task.ParentTask = Cast(0x0 as uniqueidentifier) 
		And task.StartDate is not null 
		And task.IsWaitSecretary <> 1 
		And task.IsProcessed <> 1 
		And task.DelegatedUserId IS NOT NULL '
		IF(@AssignTo IS NOT NULL)
		BEGIN
			SET @SqlFilter = @SqlFilter +'And task.AssignTo = ''' + @AssignTo + ''' '
		END
	END

	IF (@Status = 10) -- Được ủy quyền
	BEGIN
		SET @SqlFilter = @SqlFilter + '
		And doc.Status = 2
		And task.ParentTask = Cast(0x0 as uniqueidentifier) 
		And task.StartDate is not null 
		And task.IsWaitSecretary <> 1 
		And task.IsProcessed <> 1
		AND task.DelegatedUserId IS NOT NULL '
		IF(@AssignTo IS NOT NULL)
		BEGIN
			SET @SqlFilter = @SqlFilter +'And task.DelegatedUserId = ''' + @AssignTo + ''' '
		END
	END

	IF (@Status = 6) -- Công việc cần xử lý
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter+ '
		And (doc.Status = 2 Or doc.Status = 3)
		And doc.CurrentStepOrder> = step.[Order]
		And task.ParentTask <> Cast(0x0 as uniqueidentifier) 
		And task.IsProcessed <> 1 
		And task.AssignTo = ''' + @AssignTo + ''' ';	
	END

	IF (@FromProcessDate IS NOT NULL) -- Lọc thời gian ký từ
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter + '
		And CAST(task.Processed AS date) >= ''' + CONVERT(NVARCHAR(20), @FromProcessDate, 120)+ ''' ';
	END

	IF (@ToProcessDate IS NOT NULL) -- Lọc thời gian tới
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter + '
		And CAST(task.Processed AS date) <= ''' + CONVERT(NVARCHAR(20), @ToProcessDate, 120)+ ''' ';
	END

	IF (@OperationId IS NOT NULL) -- Loại xử lý 
	BEGIN
		SET @IsUserFilter=1
		SET @SqlFilter = @SqlFilter + '
		And task.Operation ='+ Cast(@OperationId as nvarchar(2));
	END

	IF (@IsCurrentStepFilter = 1) 
	BEGIN
		  SET @SqlFrom = @SqlFrom +' 
		   left join BusinessWorkflow.WorkflowStep step on step.Id=doc.CurrentStepId
		   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
	END
	ELSE
	BEGIN
		IF (@IsUserFilter = 1) 
		BEGIN
			SET @SqlFrom = @SqlFrom +' 
		   left join BusinessWorkflow.DocumentTypeSupervisor dts on doc.DocumentTypeId = dts.DocumentTypeId
		   left join BusinessWorkflow.WorkflowStep step on step.Document_ID=doc.Id
		   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
		END		
	END

	-- END FILTER
	CREATE TABLE #DocIdTb (Id uniqueidentifier);				
	
	SET @Sql = '
	INSERT INTO #DocIdTb
	SELECT Distinct doc.Id ' + @SqlFrom + @SqlFilter;

	PRINT @Sql;
	EXEC sys.sp_executesql @Sql;


    SELECT 
		doc.Id AS Id,
		Author AS AuthorAuthorId,
		Created AS AuthorCreated,
		Serial AS SerialValue,
		[Status] AS [Status],
		Title AS TitleValue,
		[Subject] AS SubjectValue
	FROM 
		BusinessWorkflow.Document doc
		INNER JOIN #DocIdTb result ON result.Id = doc.Id
	ORDER BY 
		doc.Created DESC;

	DROP TABLE #DocIdTb;
END 


TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 	DROP TABLE #DocIdTb;
	--DROP TABLE #ResultTb;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessWorkflow_SearchDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_BusinessWorkflow_SearchDocument]
--DECLARE
	@Status		int	= NULL,	
	@Author 		NVARCHAR(36)	= NULL,
	@AssignTo 		NVARCHAR(36)	= NULL,	
	@DocumentTypeId 		NVARCHAR(36)	= NULL,	
	@DocumentTypeCode 		NVARCHAR(50)	= NULL,	
	@FromDate		DATETIME	=NULL,	
	@ToDate		DATETIME	= NULL,
	@DocSetId		NVARCHAR(36)	= NULL,	
    @KeyWord		NVARCHAR(200)	= NULL,
	@Page			INT				= 1,
	@PageSize		INT				= 10,
	@UserId	NVARCHAR(36)	= '',
	@IsAdmin		BIT				= 0,
    @IsCount		BIT				= 0,
	@IsPrivateFolder	BIT			= 0, --20200508: Thêmn điều kiện lọc thư mục cá nhân
	@FolderId	NVARCHAR(36)	= NULL,	--20200508: Thêmn tham số lọc thư mục cá nhân
	@DepartmentRootId	NVARCHAR(36) = '',--20201125: Thêmn tham số lọc theo department root
	@OrderBy		NVARCHAR(50)    = 'Modified DESC'		
AS
BEGIN TRY
	SET NOCOUNT ON;

	--Khai báo ------------------------------------------------------------------------
	
	DECLARE @SqlFilter nvarchar(MAX),
			@SqlFrom nvarchar(1000),
			@Sql nvarchar(MAX),
			@AuthorFilter nvarchar(1000),
			@TotalRecord int,
            @TotalPage int,
			@IsCurrentStepFilter int,
			@IsUserFilter int,
			@AuthorViewFilter nvarchar(1000),
			@SupervisorViewFilter nvarchar(1000),
			@ShareViewFilter nvarchar(1000),
			@AssignToViewFilter nvarchar(1000),
			@DeparmentRootName nvarchar(500),
			@TmpDocumentTypeId 		NVARCHAR(36)

	CREATE TABLE #DocIdTb (Id uniqueidentifier);	
	CREATE TABLE #ResultTb (Idx int, Id uniqueidentifier);
	CREATE TABLE #DepartmentChildTb (Id uniqueidentifier);

	--Hết khai báo ------------------------------------------------------------------------
	SET @SqlFrom='from BusinessWorkflow.Document doc '
	SET @IsCurrentStepFilter=0
	SET @IsUserFilter=0
	SET @AuthorViewFilter= 'doc.Author = ''' + @UserId + ''' '
	SET @SupervisorViewFilter= 'dts.SupervisorId = ''' + @UserId + ''' '
	SET @ShareViewFilter= 'doc.InternalReceiver LIKE  ''%' + @UserId + '%'' '
	SET @AssignToViewFilter= '(doc.CurrentStepOrder> = step.[Order] And (task.AssignTo=''' + @UserId + ''' or task.DelegatedUserId=''' + @UserId + ''')) '
	SET @SqlFilter ='where doc.Status <> -1 ';	

	
	INSERT INTO #DepartmentChildTb	SELECT DP.ID
								FROM UserDepartments  UDP
								INNER JOIN Departments DP ON DP.ID = UDP.DeptID								
								WHERE UDP.UserID =@UserId AND DP.RootDBID=@DepartmentRootId
	INSERT INTO #DepartmentChildTb	 select @DepartmentRootId		 
	
	SET	@DeparmentRootName = (SELECT top 1 DP.[Name] from Departments DP where DP.ID = @DepartmentRootId)		
	IF @IsPrivateFolder=0
		BEGIN				
			IF (@Author IS NOT NULL)
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.Author = ''' + @Author + ''' ';		
			END
			IF (@AssignTo IS NOT NULL)
			BEGIN
				SET @IsUserFilter=1
				SET @SqlFilter = @SqlFilter + 'And (task.AssignTo = ''' + @AssignTo + ''' or task.DelegatedUserId = ''' + @AssignTo + ''')';		
			END
			IF (@FromDate IS NOT NULL)
			BEGIN
				 SET @SqlFilter = @SqlFilter + 'And doc.Created >= ''' + CONVERT(NVARCHAR(20), @FromDate, 120) + ''' ';	
			END
			IF (@ToDate IS NOT NULL)
			BEGIN
				 SET @SqlFilter = @SqlFilter + 'And doc.Created <= ''' + CONVERT(NVARCHAR(20), @ToDate, 120) + ''' ';	
			END	

			IF (@DocumentTypeId IS NOT NULL)
				BEGIN
					SET @SqlFilter = @SqlFilter + 'And doc.DocumentTypeId = ''' + @DocumentTypeId + ''' ';		
				END
			ELSE
					BEGIN
						IF(@DocumentTypeCode IS NOT NULL)
						BEGIN
						SET @TmpDocumentTypeId = (select Id from BusinessWorkflow.DocumentType where Code=@DocumentTypeCode)
							IF (@TmpDocumentTypeId is not null)
							BEGIN
									SET @SqlFilter = @SqlFilter + 'And doc.DocumentTypeId = ''' + @TmpDocumentTypeId + ''' ';								
							END 					
						END
					END
			IF (@DocSetId IS NOT NULL)
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.DocSetId = ''' + @DocSetId + ''' ';		
			END	
			IF (@KeyWord IS NOT NULL And @KeyWord != '')
			 BEGIN
				SET @AuthorFilter='(doc.Author in (select id from Users where FullName like N''%'+@KeyWord+'%''))'
				SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
				SET @SqlFilter = @SqlFilter + ' 
				And (CONTAINS(doc.Serial, N''' + @KeyWord +''') 
				OR CONTAINS(doc.Title, N''' + @KeyWord +''') 
				OR CONTAINS(doc.Subject, N''' + @KeyWord +''') 
				OR CONTAINS(doc.ExtensiveFields, N''' + @KeyWord +''')  
				OR '+@AuthorFilter+') ';
			 END
			 -- -1: Đã xóa
			 -- 1: Chờ tôi ký
			 -- 0: Bản nháp
			 -- 1; Chờ tôi ký
			 -- 2: Đang xử lý
			 -- 3: Đã ký
			 -- 4: Bị trả lại
			 -- 5: Bị từ chối
			 -- 6: Công việc cần xử lý
			 -- 7: Đã giao
			 -- 8: Báo cáo
			 -- 9: Đã ủy quyền
			 -- 10: Được ủy quyền
			 -- 11: Chờ ban hành
			 -- 12: Đã ban hành
	
			IF (@Status IS NULL OR @Status in (0,2,4,5))
			BEGIN
				IF(@IsAdmin!=1)
				BEGIN 
					SET @IsUserFilter=1
					SET @SqlFilter = @SqlFilter + 'And ('+ @AuthorViewFilter +
					' or '+ @ShareViewFilter +
					' or '+ @SupervisorViewFilter +
					' or ' +@AssignToViewFilter +') ';	
				END
				IF (@Status in (0,2,4,5)) 
				BEGIN
					SET @SqlFilter = @SqlFilter + 'And doc.Status = '+Cast(@Status as nvarchar(2));
				END
				
			END
			-- Đã ký
			IF (@Status = 3) 
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And (doc.Status = 3 Or doc.Status = 12)';
			END
			IF (@Status = 6) -- Công việc cần xử lý
			BEGIN
				SET @IsUserFilter=1
				SET @SqlFilter = @SqlFilter+ '
				And (doc.Status = 2 Or doc.Status = 3)
				And doc.CurrentStepOrder> = step.[Order]
				And task.ParentTask <> Cast(0x0 as uniqueidentifier) 
				And task.IsProcessed <> 1 
				And task.AssignTo = ''' + @UserId + ''' ';	
			END

			IF (@Status in (1,7,8,9,10,11,12)) 
			BEGIN
				SET @IsCurrentStepFilter=1
			END
			IF (@Status = 1) -- Chờ tôi ký
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.DelegatedUserId IS NULL
				And task.Operation <> 7
				And task.AssignTo = ''' + @UserId + ''' '	
			END
			IF (@Status = 7) -- Đã giao
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.AssignById = ''' + @UserId + ''' ';	
			END

			IF (@Status = 8) -- Báo cáo
			BEGIN
			SET @SqlFilter = @SqlFilter + ' 
				And doc.Status = 2
				And task.IsProcessed = 1 
				And task.AssignById = ''' + @UserId + ''' ';	
			END
			IF (@Status = 9) -- Đã ủy quyền
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.DelegatedUserId IS NOT NULL
				And task.AssignTo = ''' + @UserId + ''' '	
			END
			IF (@Status = 10) -- Được ủy quyền
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.DelegatedUserId = ''' + @UserId + ''' '	
			END
			
			IF (@Status = 11) -- Chờ ban hành
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.Operation = 7
				And task.AssignTo = ''' + @UserId + ''' '	
			END

			IF (@Status = 12) -- Đã ban hành
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.PublishDocumentUrl IS NOT NULL
				And doc.Status = 12 
				AND (doc.Author = ''' + @UserId + ''' OR doc.Editor = ''' + @UserId + ''')'	
			END
			

			IF (@IsCurrentStepFilter = 1) 
			BEGIN
				  SET @SqlFrom = @SqlFrom +' 
				   left join BusinessWorkflow.WorkflowStep step on step.Id=doc.CurrentStepId
				   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
			END
			ELSE
			BEGIN
				IF (@IsUserFilter = 1) 
				BEGIN
					SET @SqlFrom = @SqlFrom +' 
				   left join BusinessWorkflow.DocumentTypeSupervisor dts on doc.DocumentTypeId = dts.DocumentTypeId
				   left join BusinessWorkflow.WorkflowStep step on step.Document_ID=doc.Id
				   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
				END		
			END

			SET @Sql = '
						INSERT INTO #DocIdTb
						SELECT Distinct doc.Id ' + @SqlFrom + @SqlFilter;
			PRINT @Sql;
			EXEC sys.sp_executesql @Sql;
		END

	ELSE

		BEGIN	
			DECLARE @SubSql nvarchar(MAX),
					@SubSqlKeyword nvarchar(MAX),
					@SubSqlDocument nvarchar(MAX),
					@SubSqlDocumentWhere nvarchar(MAX)	

					SET @SubSqlKeyword = '';
					SET @SubSqlDocument = '';
					SET @SubSqlDocumentWhere = '';
				IF (@FolderId IS NOT NULL And @FolderId != '')
					 BEGIN	
						SET @SubSql = '
							INSERT INTO #DocIdTb
							SELECT 
								Distinct fdt.DocumentID										
							FROM BusinessWorkflow.FolderDetail as fdt  
							'
							SET @SubSqlDocumentWhere = ' WHERE  fdt.FolderID = ''' + @FolderId + ''' '
					 END
					
				IF (@KeyWord IS NOT NULL And @KeyWord != '')
					 BEGIN
						SET @AuthorFilter='(doc.Author in (select id from Users where FullName like N''%'+@KeyWord+'%''))'
						SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
						print @KeyWord;
						SET @SubSqlKeyword =' 
						AND (CONTAINS(doc.Serial, N''' + @KeyWord +''') 
						OR CONTAINS(doc.Title, N''' + @KeyWord +''') 
						OR CONTAINS(doc.Subject, N''' + @KeyWord +''') 
						OR CONTAINS(doc.ExtensiveFields, N''' + @KeyWord +''')  
						OR '+@AuthorFilter+') ';

						SET @SubSqlDocument = ' INNER JOIN BusinessWorkflow.Document doc on doc.Id = fdt.DocumentID '
					END
			
			SET @SubSql = @SubSql + @SubSqlDocument + @SubSqlDocumentWhere + @SubSqlKeyword;
			PRINT @SubSql;
			EXEC sys.sp_executesql  @SubSql;
		END

-- lấy dữ liệu phân trang
	IF (@IsCount = 0)
		BEGIN		
		SET @Sql = '
		INSERT INTO #ResultTb
		SELECT
			ROW_NUMBER() OVER (ORDER BY doc.' + @OrderBy + '),
			doc.Id
		FROM 
			BusinessWorkflow.Document doc 
			INNER JOIN #DocIdTb idDoc ON idDoc.Id = doc.Id AND doc.DepartmentId in (select id from #DepartmentChildTb)';
	
		EXEC sys.sp_executesql @Sql;
		SET @TotalRecord = (SELECT MAX(Idx) FROM #ResultTb);
		Select 
		doc.Id,
		Author,
		doc.Created,
		Serial,
		[Status],
		Title,
		CurrentStepName,
		ExtensiveFields,
		doc.OrderNumber,
		doc.DepartmentId,
		 CONVERT(uniqueidentifier, @DepartmentRootId) AS RootDBID,
		@DeparmentRootName AS RootDeptName,
		--JD.RootDBID,		
		--(CASE WHEN JD.RootDBID IS NULL THEN NULL			
		--	ELSE (SELECT TOP 1 JDTemp.Code
		--		FROM dbo.Departments JDTemp 
		--		WHERE JDTemp.ID=JD.RootDBID)
		--END) AS RootDeptName,	
		@TotalRecord as TotalRecord
		from 
			BusinessWorkflow.Document doc
			INNER JOIN #ResultTb result on result.Id = doc.Id
			--LEFT JOIN dbo.Departments JD
			--ON JD.ID=doc.DepartmentId
		WHERE 
			result.Idx BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
		ORDER BY 
		result.Idx;		
			
		END 
	ELSE
		BEGIN
			SELECT Count(id) from #DocIdTb
		END 		
	DROP TABLE #DocIdTb;
	DROP TABLE #ResultTb;
	DROP TABLE #DepartmentChildTb;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 --	DROP TABLE #DocIdTb;
	--DROP TABLE #ResultTb;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessWorkflow_SearchDocumentByDept]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BusinessWorkflow_SearchDocumentByDept]
--DECLARE
	@Status		int	= NULL,	
	@Author 		NVARCHAR(36)	= NULL,
	@AssignTo 		NVARCHAR(36)	= NULL,	
	@DocumentTypeId 		NVARCHAR(36)	= NULL,	
	@FromDate		DATETIME	=NULL,	
	@ToDate		DATETIME	= NULL,
	@DocSetId		NVARCHAR(36)	= NULL,	
    @KeyWord		NVARCHAR(200)	= NULL,
	@Page			INT				= 1,
	@PageSize		INT				= 10,
	@UserId	NVARCHAR(36)	= '',
	@IsAdmin		BIT				= 0,
    @IsCount		BIT				= 0,
	@IsPrivateFolder	BIT			= 0,
	@FolderId	NVARCHAR(36)	= NULL,
	@DepartmentRootId		NVARCHAR(36) = NULL,--20201125: Thêmn tham số lọc theo department root
	@OrderBy		NVARCHAR(50)    = 'Modified DESC'		
AS
BEGIN TRY
	SET NOCOUNT ON;

	--Khai báo ------------------------------------------------------------------------
	
	DECLARE @SqlFilter nvarchar(MAX),
			@SqlFrom nvarchar(1000),
			@Sql nvarchar(MAX),
			@AuthorFilter nvarchar(1000),
			@TotalRecord int,
            @TotalPage int,
			@IsCurrentStepFilter int,
			@IsUserFilter int,
			@AuthorViewFilter nvarchar(1000),
			@SupervisorViewFilter nvarchar(1000),
			@ShareViewFilter nvarchar(1000),
			@AssignToViewFilter nvarchar(1000)

	CREATE TABLE #DocIdTb (Id uniqueidentifier);	
	CREATE TABLE #ResultTb (Idx int, Id uniqueidentifier);
	--Khai báo kiểm tra và get quyền truy cập theo đơn vị---------------------------------------------------
	DECLARE @DeptData TABLE(TempID uniqueidentifier);
	CREATE TABLE #DeptDataFull (TempID uniqueidentifier);
	DECLARE @IsAllow int;
	SET @IsAllow=(SELECT COUNT(JP.Name) AS Code 
				FROM dbo.UserRoles JUR
				INNER JOIN dbo.RolePermissions JRP
				ON JUR.RoleID=JRP.RoleID
				INNER JOIN dbo.[Permissions] JP
				ON JRP.PermissionID=JP.ID                            
				WHERE JUR.UserID=@UserId 
					AND UPPER(JP.Name)= 'ALLLISTDATABYDEPT'
					AND JP.Name <> ''
					AND JP.Name IS NOT NULL);
	if(@IsAllow>0)
		BEGIN	
			INSERT INTO @DeptData SELECT DISTINCT DeptID FROM dbo.UserDepartments WHERE UserID=@UserId;		
			WITH DepartmentTree (Id,Name, [Level], location)
			AS (SELECT  Id,
						CAST(Name AS NVARCHAR(MAX)),          
						0 AS [Level],
						CAST(Id AS NVARCHAR(MAX)) AS Location                                          
				FROM Departments
				WHERE ID IN(SELECT TempID FROM @DeptData)
				UNION ALL
				SELECT child.Id,
						CAST(CONCAT(SPACE(parent.[Level] * 5), '|__', child.Name) AS NVARCHAR(MAX)),           
						parent.Level + 1,
						CAST(CONCAT(parent.location, ',', child.Id) AS NVARCHAR(MAX)) AS Location                                          
				FROM Departments child
					INNER JOIN DepartmentTree parent
						ON child.ParentID = parent.Id)
			INSERT INTO #DeptDataFull SELECT DISTINCT Id FROM DepartmentTree;
			--SELECT * FROM @DeptDataFull ORDER BY location;     
		END
	--Hết khai báo ------------------------------------------------------------------------
	SET @SqlFrom='from BusinessWorkflow.Document doc '
	SET @IsCurrentStepFilter=0
	SET @IsUserFilter=0
	SET @AuthorViewFilter= 'doc.Author = ''' + @UserId + ''' '
	SET @SupervisorViewFilter= 'dts.SupervisorId = ''' + @UserId + ''' '
	SET @ShareViewFilter= 'doc.InternalReceiver LIKE  ''%' + @UserId + '%'' '
	SET @AssignToViewFilter= '(doc.CurrentStepOrder> = step.[Order] And (task.AssignTo=''' + @UserId + ''' or task.DelegatedUserId=''' + @UserId + ''')) '
	SET @SqlFilter ='where doc.Status <> -1 ';	

	IF @IsPrivateFolder=0
		BEGIN	
			---- Search theo quyền đơn vị------------------
			SET @SqlFilter = @SqlFilter +' AND doc.DepartmentId IN(SELECT TempID From #DeptDataFull)' ;
			---- Search người tạo--------------------------			
			IF (@Author IS NOT NULL)
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.Author = ''' + @Author + ''' ';		
			END
			---- Search người tạo--------------------------			
			IF (@AssignTo IS NOT NULL)
			BEGIN
				SET @IsUserFilter=1
				SET @SqlFilter = @SqlFilter + 'And (task.AssignTo = ''' + @AssignTo + ''' or task.DelegatedUserId = ''' + @AssignTo + ''')';		
			END
			---- Search người tạo--------------------------			
			IF (@FromDate IS NOT NULL)
			BEGIN
				 SET @SqlFilter = @SqlFilter + 'And doc.Created >= ''' + CONVERT(NVARCHAR(20), @FromDate, 120) + ''' ';	
			END
			---- Search người tạo--------------------------			
			IF (@ToDate IS NOT NULL)
			BEGIN
				 SET @SqlFilter = @SqlFilter + 'And doc.Created <= ''' + CONVERT(NVARCHAR(20), @ToDate, 120) + ''' ';	
			END	
			---- Search người tạo--------------------------			
			IF (@DocumentTypeId IS NOT NULL)
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.DocumentTypeId = ''' + @DocumentTypeId + ''' ';		
			END
			---- Search người tạo--------------------------			
			IF (@DocSetId IS NOT NULL)
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.DocSetId = ''' + @DocSetId + ''' ';		
			END	
			---- Search người tạo--------------------------			
			IF (@KeyWord IS NOT NULL And @KeyWord != '')
			 BEGIN
				SET @AuthorFilter='(doc.Author in (select id from Users where FullName like N''%'+@KeyWord+'%''))'
				SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
				SET @SqlFilter = @SqlFilter + ' 
				And (CONTAINS(doc.Serial, N''' + @KeyWord +''') 
				OR CONTAINS(doc.Title, N''' + @KeyWord +''') 
				OR CONTAINS(doc.Subject, N''' + @KeyWord +''') 
				OR CONTAINS(doc.ExtensiveFields, N''' + @KeyWord +''')  
				OR '+@AuthorFilter+') ';
			 END
			 -- -1: Đã xóa
			 -- 1: Chờ tôi ký
			 -- 0: Bản nháp
			 -- 1; Chờ tôi ký
			 -- 2: Đang xử lý
			 -- 3: Đã ký
			 -- 4: Bị trả lại
			 -- 5: Bị từ chối
			 -- 6: Công việc cần xử lý
			 -- 7: Đã giao
			 -- 8: Báo cáo
			 -- 9: Đã ủy quyền
			 -- 10: Được ủy quyền
			 -- 11: Chờ ban hành
			 -- 12: Đã ban hành
			
			IF (@Status in (0,2,4,5))
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And doc.Status = '+Cast(@Status as nvarchar(2));
			END
			-- Đã ký
			IF (@Status = 3) 
			BEGIN
				SET @SqlFilter = @SqlFilter + 'And (doc.Status = 3 Or doc.Status = 12)';
			END
			IF (@Status = 6) -- Công việc cần xử lý
			BEGIN
				SET @IsUserFilter=1;
				SET @SqlFilter = @SqlFilter+ '
				And (doc.Status = 2 Or doc.Status = 3)
				And doc.CurrentStepOrder> = step.[Order]
				And task.ParentTask <> Cast(0x0 as uniqueidentifier) 
				And task.IsProcessed <> 1 ';	
			END

			IF (@Status in (1,7,8,9,10,11,12)) 
			BEGIN
				SET @IsCurrentStepFilter=1
			END
			IF (@Status = 1) -- Chờ tôi ký
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.DelegatedUserId IS NULL
				And task.Operation <> 7 '	
			END
			IF (@Status = 7) -- Đã giao
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2 ';	
			END

			IF (@Status = 8) -- Báo cáo
			BEGIN
			SET @SqlFilter = @SqlFilter + ' 
				And doc.Status = 2
				And task.IsProcessed = 1 ';	
			END
			IF (@Status = 9) -- Đã ủy quyền
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.DelegatedUserId IS NOT NULL '	
			END
			IF (@Status = 10) -- Được ủy quyền
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 '	
			END
			
			IF (@Status = 11) -- Chờ ban hành
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.Status = 2
				And task.ParentTask = Cast(0x0 as uniqueidentifier) 
				And task.StartDate is not null 
				And task.IsWaitSecretary <> 1 
				And task.IsProcessed <> 1 
				And task.Operation = 7 '	
			END

			IF (@Status = 12) -- Đã ban hành
			BEGIN
				SET @SqlFilter = @SqlFilter + '
				And doc.PublishDocumentUrl IS NOT NULL
				And doc.Status = 12 '	
			END
			

			IF (@IsCurrentStepFilter = 1) 
			BEGIN
				  SET @SqlFrom = @SqlFrom +' 
				   left join BusinessWorkflow.WorkflowStep step on step.Id=doc.CurrentStepId
				   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
			END
			ELSE
			BEGIN
				IF (@IsUserFilter = 1) 
				BEGIN
					SET @SqlFrom = @SqlFrom +' 
				   left join BusinessWorkflow.DocumentTypeSupervisor dts on doc.DocumentTypeId = dts.DocumentTypeId
				   left join BusinessWorkflow.WorkflowStep step on step.Document_ID=doc.Id
				   left join BusinessWorkflow.WorkflowTask task on task.WorkflowStep_Id=step.Id ';
				END		
			END

			SET @Sql = '
						INSERT INTO #DocIdTb
						SELECT Distinct doc.Id ' + @SqlFrom + @SqlFilter;
			PRINT @Sql;
			EXEC sys.sp_executesql @Sql;
		END

	ELSE

		BEGIN	
			DECLARE @SubSql nvarchar(MAX),
					@SubSqlKeyword nvarchar(MAX),
					@SubSqlDocument nvarchar(MAX),
					@SubSqlDocumentWhere nvarchar(MAX)	

					SET @SubSqlKeyword = '';
					SET @SubSqlDocument = '';
					SET @SubSqlDocumentWhere = '';
				IF (@FolderId IS NOT NULL And @FolderId != '')
					 BEGIN	
						SET @SubSql = '
							INSERT INTO #DocIdTb
							SELECT 
								Distinct fdt.DocumentID										
							FROM BusinessWorkflow.FolderDetail as fdt  
							'
							SET @SubSqlDocumentWhere = ' WHERE  fdt.FolderID = ''' + @FolderId + ''' '
					 END
					
				IF (@KeyWord IS NOT NULL And @KeyWord != '')
					 BEGIN
						SET @AuthorFilter='(doc.Author in (select id from Users where FullName like N''%'+@KeyWord+'%''))'
						SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
						print @KeyWord;
						SET @SubSqlKeyword =' 
						AND (CONTAINS(doc.Serial, N''' + @KeyWord +''') 
						OR CONTAINS(doc.Title, N''' + @KeyWord +''') 
						OR CONTAINS(doc.Subject, N''' + @KeyWord +''') 
						OR CONTAINS(doc.ExtensiveFields, N''' + @KeyWord +''')  
						OR '+@AuthorFilter+') ';

						SET @SubSqlDocument = ' INNER JOIN BusinessWorkflow.Document doc on doc.Id = fdt.DocumentID '
					END
			
			SET @SubSql = @SubSql + @SubSqlDocument + @SubSqlDocumentWhere + @SubSqlKeyword;
			PRINT @SubSql;
			EXEC sys.sp_executesql  @SubSql;
		END

-- lấy dữ liệu phân trang
	IF (@IsCount = 0)
		BEGIN		
		SET @Sql = '
		INSERT INTO #ResultTb
		SELECT
			ROW_NUMBER() OVER (ORDER BY doc.' + @OrderBy + '),
			doc.Id
		FROM 
			BusinessWorkflow.Document doc 
			INNER JOIN #DocIdTb idDoc ON idDoc.Id = doc.Id ';
		EXEC sys.sp_executesql @Sql;
		SET @TotalRecord = (SELECT MAX(Idx) FROM #ResultTb);
		Select 
		doc.Id,
		Author,
		doc.Created,
		Serial,
		[Status],
		Title,
		CurrentStepName,
		ExtensiveFields,
		doc.OrderNumber,
		doc.DepartmentId,
		JD.RootDBID,		
		(CASE WHEN JD.RootDBID IS NULL THEN NULL			
			ELSE (SELECT TOP 1 JDTemp.Code
				FROM dbo.Departments JDTemp 
				WHERE JDTemp.ID=JD.RootDBID)
		END) AS RootDeptName,
		@TotalRecord as TotalRecord
		from 
			BusinessWorkflow.Document doc
			INNER JOIN #ResultTb result on result.Id = doc.Id
			LEFT JOIN dbo.Departments JD
			ON JD.ID=doc.DepartmentId
		WHERE 
			result.Idx BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
		ORDER BY 
		result.Idx;		
			
		END 
	ELSE
		BEGIN
			SELECT Count(id) from #DocIdTb
		END 		
	DROP TABLE #DocIdTb;
	DROP TABLE #ResultTb;
	DROP TABLE #DeptDataFull;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 --	DROP TABLE #DocIdTb;
	--DROP TABLE #ResultTb;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_TASK_ASSIGNBY_USER]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CHECK_TASK_ASSIGNBY_USER]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@TaskId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	create table #temp (id uniqueidentifier);
    WITH tbChildren AS
			(
				SELECT Id
				FROM [Task].[TaskItem]
				WHERE Id = @TaskId
				
				UNION ALL

				SELECT TItem.Id
				FROM [Task].[TaskItem] TItem
				JOIN tbChildren 
				ON TItem.ParentId = tbChildren.Id
			)
			insert into #temp
			select distinct Id from tbChildren;
	select Id from Task.TaskItem 
	where Id in (select id from #temp) and 
	AssignBy = @CurrentUserId AND IsDeleted = 0
	;
	drop table #temp;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_USER_ASSIGN_TASK]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CHECK_USER_ASSIGN_TASK]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@TaskId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select Id from Task.TaskItem
	where AssignBy = @CurrentUserId and Id = @TaskId AND IsDeleted = 0
	;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_USER_IS_PARENT_TASK]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CHECK_USER_IS_PARENT_TASK]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@ProjectId uniqueidentifier,
	@StatusId int null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   select Id from Task.TaskItem 
	where ProjectId = @ProjectId and AssignBy = @CurrentUserId AND IsDeleted = 0
	and TaskItemStatusId = @StatusId;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_USER_IS_PARENT_TASK_BY_TASK_ID]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CHECK_USER_IS_PARENT_TASK_BY_TASK_ID]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@TaskId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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
	if(EXISTS(select distinct TaskItemId from Task.TaskItemAssign 
	where TaskItemId in (select id from #temp) and 
	AssignTo = @CurrentUserId and TaskType = 1) 
	or EXISTS(select distinct ID from Task.TaskItem 
	where AssignBy = @CurrentUserId and ID in (select id from #temp)))
	begin 
		select 1;
	end
	else
	begin
		select 0;
	end
	drop table #temp;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CheckResxDepartment]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckResxDepartment]
	@Culture nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Resources
	SELECT 
		N'Data.Department.Name.' + CAST(ID AS NVARCHAR(36)) + N'.' + Name ,
		@Culture, 
		CASE
			WHEN @Culture = 'vi-VN' THEN	 Name
			ELSE ''
		END
		, 'Web', getdate(), 0
	FROM Departments
	WHERE 
		NOT EXISTS (
			SELECT TOP 1 Resx.[Key]
			FROM Resources Resx
			WHERE Resx.Culture = @Culture
			AND Resx.[Key] = N'Data.Department.Name.' + CAST(ID AS NVARCHAR(36)) + N'.' + Name		
		)

END

GO
/****** Object:  StoredProcedure [dbo].[SP_CheckResxJobTitle]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckResxJobTitle]
	@Culture nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Resources
	SELECT 
		N'Data.JobTitle.Name.' + CAST(ID AS NVARCHAR(36)) + N'.' + Name ,
		@Culture, 
		CASE
			WHEN @Culture = 'vi-VN' THEN	 Name
			ELSE ''
		END
		, 'Web', getdate(), 0
	FROM JobTitles
	WHERE 
		NOT EXISTS (
			SELECT TOP 1 Resx.[Key]
			FROM Resources Resx
			WHERE Resx.Culture = @Culture
			AND Resx.[Key] = N'Data.JobTitle.Name.' + CAST(ID AS NVARCHAR(36)) + N'.' + Name		
		)

END

GO
/****** Object:  StoredProcedure [dbo].[SP_Count_ByServer_OnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Count_ByServer_OnlineUsers]
	@ServerName nvarchar(150)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		COUNT(*) AS Numbers
	FROM 
		OnlineUsers
	WHERE 
		ServerName = @ServerName
END
		

GO
/****** Object:  StoredProcedure [dbo].[SP_Count_OnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Count_OnlineUsers]
AS
BEGIN
	SET NOCOUNT ON;	
	SELECT
		ServerName
		,COUNT(UserID) AS Numbers
	FROM 
		OnlineUsers
	GROUP BY 
		ServerName

END

GO
/****** Object:  StoredProcedure [dbo].[SP_Count_Select_Projects_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Count_Select_Projects_MultiFilters]
--DECLARE
	@CurrentDate	DATETIME, 
	@CurrentUserID	NVARCHAR(200),
	@ListParamsName nvarchar(Max),
	@IsFullControl nvarchar(10) = '0'
AS 
--SELECT @CurrentDate	= getdate(),
--@CurrentUserID = dbo.Func_GetUserIDByLoginName('datxanh\thaidt'),
--	@ListParamsName = '@DocType:2|@DocType:2;@TrackStatus:0|@DocType:2;@TrackStatus:1' 
BEGIN TRY
	SET NOCOUNT ON;
	SET ARITHABORT ON;
	DECLARE @TbCount TABLE (ParamName nvarchar(max), NoDoc int, Idx int );
	DELETE @TbCount;
	INSERT INTO @TbCount
		SELECT items, 0, STT
		FROM dbo.Func_SplitTextToTable_WithRowNumber(@ListParamsName, '|')
		WHERE items != '' AND items IS NOT NULL;

	CREATE TABLE #TempCount (TotalRow int);
	DECLARE @Sql NVARCHAR(max) = '', @Idx int = 1, @Max int = 0, @Param NVARCHAR(max) = '';
	SELECT @Max = MAX(Idx) FROM @TbCount;
	WHILE (@Idx <= @Max)
	BEGIN
		DELETE #TempCount;
		SELECT @Param = ParamName FROM @TbCount WHERE Idx = @Idx;
		SET @Sql = 'INSERT INTO #TempCount EXEC SP_Select_Projects_MultiFilters  @CurrentDate = ''' + CONVERT(NVARCHAR, @CurrentDate, 101) +''', @IsCount = 1, @IsFullControl = ' + @IsFullControl;
		SET @Sql = @Sql	 + ',@CurrentUser = ''' + @CurrentUserID + ''' ';
		SELECT @Sql = @Sql + ',' + DBO.FN_REPLACE_FIRST(items, ':', '=''') + ''' '
		FROM dbo.Func_SplitTextToTable(@Param, ';');
		PRINT @Sql;
		EXEC sys.sp_executesql @Sql; 

		UPDATE @TbCount
		SET NoDoc = (SELECT TOP 1 TotalRow FROM #TempCount)
		WHERE Idx = @Idx;

		SET @Idx = @Idx + 1;
	END
	--DROP TABLE #TempCount;

	UPDATE @TbCount SET NoDoc = 0 WHERE NoDoc IS NULL;
	SELECT *
	FROM @TbCount; 
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 --ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 
	--DROP TABLE #TempCount;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Departments_ByID]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_Departments_ByID]
	@DeptID uniqueidentifier
AS
BEGIN TRY
	SET NOCOUNT ON;

	BEGIN TRANSACTION;

	
	DELETE UserDepartments
	WHERE DeptID = @DeptID;

	DELETE BookDocTypeDocuments
	WHERE BookID IN (SELECT
				ID
			FROM BookDocuments
			WHERE Dept = @DeptID);


	DELETE BookWorkFlowDocuments
	WHERE BookID IN (SELECT
				ID
			FROM BookDocuments
			WHERE Dept = @DeptID);

	DELETE BookNumberDocuments
	WHERE BookID IN (SELECT
				ID
			FROM BookDocuments
			WHERE Dept = @DeptID);


	DELETE BookDocuments
	WHERE Dept = @DeptID;

	DELETE Departments
	WHERE IsActive = 0;

	COMMIT;
END TRY
BEGIN CATCH
	IF (@@error > 0)
	BEGIN
		ROLLBACK;
	END
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_OnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_OnlineUsers]
	@ServerName nvarchar(150),
	@UserID nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE OnlineUsers
	WHERE ServerName = @ServerName AND UserID = @UserID;

END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrgUser_ByListUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOrgUser_ByListUser]
--DECLARE
	@ListUser nvarchar(max)
AS
BEGIN	
	SET NOCOUNT ON;
	
	--DECLARE @TbUser TABLE (Idx int, UserID nvarchar(200))
	--DECLARE @TOrg TABLE (OrgID uniqueidentifier, ParentID uniqueidentifier, OrgName nvarchar(500), UserName nvarchar(200), FullName nvarchar(200), Level bigint)

	--INSERT INTO @TbUser
	--	SELECT STT, items
	--	FROM dbo.Func_SplitTextToTable_WithRowNumber(@ListUser, ';')

	--INSERT INTO @TOrg
	--SELECT O.OrgID, O.ParentID, O.OrgName, OU.UserName, UI.FullName, O.Level
	--FROM Orgs O
	--	INNER JOIN OrgUser OU ON OU.OrgID = O.OrgID
	--	INNER JOIN @TbUser TU ON TU.UserID = OU.UserName
	--	INNER JOIN UserInfos UI ON UI.UserName = OU.UserName

	--INSERT INTO @TOrg
	--SELECT DISTINCT O.OrgID, O.ParentID, O.OrgName, NULL, NULL, O.Level
	--FROM Orgs O
	--	INNER JOIN @TOrg T ON T.ParentID = O.OrgID


	--INSERT INTO @TOrg
	--SELECT DISTINCT O.OrgID, O.ParentID, O.OrgName, NULL, NULL, O.Level
	--FROM Orgs O
	--	INNER JOIN @TOrg T ON T.ParentID = O.OrgID
	--WHERE T.ParentID
	--	NOT IN (SELECT T2.OrgID FROM @TOrg T2)


	--INSERT INTO @TOrg
	--SELECT DISTINCT O.OrgID, O.ParentID, O.OrgName, NULL, NULL, O.Level
	--FROM Orgs O
	--	INNER JOIN @TOrg T ON T.ParentID = O.OrgID
	--WHERE T.ParentID
	--	NOT IN (SELECT T2.OrgID FROM @TOrg T2)


	

	--;WITH TbTemp 
	--	AS
	--	(
	--		SELECT O.OrgID, 
	--			   CAST(O.OrgName as nvarchar) AS Name, 
	--			   1 AS OrgLevel,
	--			   CAST('' as nvarchar) AS SpacePath,
	--			   O.Level AS OrgPath,			
	--			   dbo.Func_BuildLevelPath(5,'', 1, 1) as PathLevel
	--		FROM Orgs AS O    		
	--		WHERE (O.ParentID IS NULL OR O.ParentID = '00000000-0000-0000-0000-000000000000'  )
	--			  AND O.IsActive = 1 			  
	--		UNION ALL 			
	--		SELECT  O.OrgID, 
	--			CAST(( CAST(DP.SpacePath as nvarchar) + '+ ' + O.OrgName) as nvarchar) as Name, 
	--			DP.OrgLevel + 1 AS OrgLevel, 				
	--			CAST((CAST(DP.SpacePath as nvarchar) + '--') as nvarchar)   AS SpacePath,
	--			O.Level as OrgPath,
	--			     dbo.Func_BuildLevelPath(5,DP.PathLevel, ROW_NUMBER() OVER(ORDER BY O.Level, O.OrgName), DP.OrgLevel) as PathLevel
	--		FROM Orgs AS O
	--		INNER JOIN TbTemp AS DP ON DP.OrgID = O.ParentID		    
	--		WHERE  O.IsActive = 1 
	--	)
	
	--SELECT TB.OrgID, TB.ParentID, TB.OrgName, TB.UserName, TB.FullName, cast ( TB.Level as bigint ) AS Level 
	--FROM
	--(
	--	SELECT T1.OrgID, T1.ParentID, T1.OrgName, T1.UserName, T1.FullName,  T2.PathLevel AS Level 
	--	FROM @TOrg T1
	--		LEFT JOIN TbTemp T2 ON T1.OrgID = T2.OrgID

	--	UNION ALL
	--	SELECT NEWID(), '00000000-0000-0000-0000-000000000000', N'Người dùng khác', UserID, REPLACE(UserID, 'AICVN\', '') as FullName, '9000000000' as Level
	--	FROM @TbUser TU
	--	WHERE NOT EXISTS(
	--		SELECT [@TOrg].UserName FROM @TOrg WHERE TU.UserID = [@TOrg].UserName
	--	)
	--) TB
	--ORDER BY TB.Level

END


GO
/****** Object:  StoredProcedure [dbo].[SP_GetRecursiveChild_Dept]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetRecursiveChild_Dept]
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    

	WITH cteorg
	AS
	(
		select d.ID, d.ParentID
		from Departments d
			INNER JOIN UserDepartments ud on ud.DeptID = d.ID and d.ID != d.RootDBID
		where d.IsActive = 1  and ud.UserID = @UserID -- and d.RootDBID != ud.DeptID
		union all
		select d.ID, d.ParentID
		from Departments d
			INNER JOIN cteorg on cteorg.ID = d.ParentID and d.IsActive = 1

	)

	select id
	from cteorg 
	
	--SELECT
	--	id
	--FROM Departments
	--WHERE id = NULL;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Mobile_CountDashboard]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Mobile_CountDashboard]
--DECLARE
    @FromDate nvarchar(50), -- = '2016-10-01 00:00',
    @ToDate nvarchar(50),-- = '2018-03-16 23:59',
	@UserID nvarchar(36)-- = '2BF9B4DC-6776-4AC2-8440-BB8FBB647392'
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #CountTable (DocID UNIQUEIDENTIFIER, TStatus int);
	
	INSERT INTO #CountTable
		SELECT
			T.DocID
			,(
				SELECT TOP 1 T2.Status
				FROM 
					TrackingDocuments T2
				WHERE 
					T2.DocID = T.DocID
					AND T2.AssignTo = T.AssignTo
				ORDER BY T2.Status
			) TrackingStatus
		FROM 
			TrackingDocuments T
			INNER JOIN Documents D ON D.ID = T.DocID
		WHERE 
			D.IsActive = 1
			AND T.AssignTo = @UserID
			AND D.Created BETWEEN @FromDate AND @ToDate;
					
	INSERT INTO #CountTable
		SELECT
			DT.DocumentID
			,(
			CASE
				WHEN DT.Status = 1 THEN 4
				ELSE DT.Status
			END
			) TransferStatus
		FROM 
			TransferDocuments DT
			INNER JOIN Documents D ON D.ID = DT.DocumentID
		WHERE 
			D.IsActive = 1
			AND DT.Receiver = @UserID
			AND DT.TransferTime BETWEEN @FromDate AND @ToDate
			AND NOT EXISTS (SELECT TOP 1
					CT.TStatus
				FROM #CountTable CT
				WHERE CT.DocID = D.ID);
		

	SELECT 
		CT.TStatus, COUNT(CT.DocID) TCount
	FROM 
		#CountTable CT

	GROUP BY
		CT.TStatus
	

	DROP TABLE #CountTable;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Mobile_DocumentStatistics_Calculator]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Mobile_DocumentStatistics_Calculator]
AS
BEGIN
	SET NOCOUNT ON;
	-- khai báo điều kiện tính toán
	DECLARE @CurrentDate date = getdate();
	DECLARE @FromDate nvarchar(30) =  DATEADD(DAY, -6, @CurrentDate);
	DECLARE @ToDate  nvarchar(30) =  @CurrentDate;
	DECLARE @FromMonth datetime = DATEADD(DAY, -180, @CurrentDate);
	DECLARE @ToMonth datetime = @CurrentDate
	DECLARE @FromYear int = Year(@FromMonth);
	DECLARE @ToYear int = Year(@ToMonth);
	-- quét danh sách user cần tính toán
	
	CREATE TABLE #CountTable (DocID UNIQUEIDENTIFIER, UserID nvarchar(50),  Created datetime);

	INSERT INTO #CountTable
		SELECT 
			DISTINCT Track.DocID, Track.AssignTo,
			(SELECT TOP 1 T2.Created 
			FROM TrackingDocuments T2 
			WHERE T2.DocID = Track.DocID AND T2.AssignTo = Track.AssignTo
			ORDER BY T2.Created )
		FROM 
			TrackingDocuments Track
			INNER JOIN Documents D ON D.ID = Track.DocID
		WHERE 
			YEAR(Track.Created) BETWEEN @FromYear AND @ToYear;

	INSERT INTO #CountTable
		SELECT 
			DISTINCT Trans.DocumentID, Trans.Receiver,
			(SELECT TOP 1 T2.TransferTime 
			FROM TransferDocuments T2 
			WHERE T2.DocumentID = Trans.DocumentID AND T2.Receiver = Trans.Receiver
			ORDER BY T2.TransferTime )
		FROM 
			TransferDocuments Trans
			INNER JOIN Documents D ON D.ID = Trans.DocumentID
		WHERE 
			YEAR(Trans.TransferTime) BETWEEN @FromYear AND @ToYear
			AND NOT EXISTS 
			(
				SELECT TOP 1 CT.Created FROM #CountTable CT WHERE CT.DocID = Trans.DocumentID AND Ct.UserID = Trans.Receiver
			);
	
	
	--	-- count by from date - to date
	DELETE DocumentStatistics
	WHERE [Type] = 'date'
		AND [TypeKey] BETWEEN @FromDate AND @ToDate;

	INSERT INTO DocumentStatistics
		SELECT
			UserID, 
			'date' [Type],
			CAST(Created AS DATE ) [TypeKey],
			Count(DocID) TotalDocs
		FROM
			#CountTable
		WHERE
			Created BETWEEN @FromDate AND @ToDate
		GROUP BY 
			CAST(Created AS DATE), UserID;

	-- count by month
	DELETE DocumentStatistics
	WHERE 
		 [Type] = 'month'
		AND [TypeKey] BETWEEN LEFT( CONVERT(DATE, @FromMonth,120),7) AND LEFT( CONVERT(DATE, @ToMonth,120),7);

	INSERT INTO DocumentStatistics
		SELECT
			UserID UserName, 
			'month' [Type],
			LEFT( CONVERT(DATE, Created,120),7) [TypeKey], 
			Count(DocID) TotalDocs
		FROM #CountTable
		WHERE
			Created BETWEEN @FromMonth AND @ToMonth
		GROUP BY 
			LEFT( CONVERT(DATE, Created,120),7), UserID;

	--count by year
	DELETE DocumentStatistics
	WHERE  
		[Type] = 'year'
		AND [TypeKey] = @ToYear;

	INSERT INTO DocumentStatistics
	SELECT
		UserID UserName, 
		'year' [Type],
		Year(Created) [TypeKey], 
		Count(DocID) TotalDocs
	FROM #CountTable
	WHERE
		Year(Created) = @ToYear
	GROUP BY 
		Year(Created), UserID;
		
			

	DROP TABLE #CountTable;

END

GO
/****** Object:  StoredProcedure [dbo].[SP_Report_MonthlyDeptTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Report_MonthlyDeptTask]
--DECLARE 
	@DeptID nvarchar(4000),
	@FromDate datetime,
	@ToDate datetime
AS
--SELECT	@DeptID = '075BDABA-86CA-4A3A-8D41-A6718D8E51C5;',
--		@FromDate = '2018-01-01 00:00',
--		@ToDate = '2018-08-31 23:59';
BEGIN
	SET NOCOUNT ON;

    WITH CTE_MYTASK
	AS
	(
		-- query document object
		SELECT 
			--- For Order -------------------------------------------------------------------------
			1								AS DepthLevel
			, dbo.Func_BuildLevelPath(3,'', ROW_NUMBER() OVER(ORDER BY dbo.Func_GetPlanningType(D.ExtensiveInfo), D.Created ASC), 1) 
											AS OLevel
			---------------------------------------------------------------------------------------
			, D.ID							AS TaskID
			, D.ID							AS DocID
			, D.Summary						AS TaskName
			, CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER)
											AS AssignTo
			, D.FromDate					AS PlanFromDate
			, D.ToDate						AS PlanToDate
			, 0								AS PlanRoleType
			, CAST('' AS NVARCHAR(100))	    AS ActualLastResult
			, D.ToDate						AS ActualToDate
			, CAST('' AS NVARCHAR(2000))	AS ActualTaskName
			, CAST('' AS NVARCHAR(MAX))		AS ActualReason
			, CAST('' AS NVARCHAR(10))
					   						AS PercentFinish
			, CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER)
											AS ParentID
			, dbo.Func_GetPlanningType(D.ExtensiveInfo)	
											AS PlanningType 
		FROM 
			Documents D 
		WHERE 
			D.[IsActive] = 1 AND ( LOWER(RTRIM(D.IsBroadCast)) = 'false' OR D.IsBroadCast IS NULL)
			AND D.ID IN 
			(SELECT DISTINCT 
				MTD.DocID 
			 FROM TrackingDocuments MTD 
				INNER JOIN dbo.Func_SplitTextToTable(@DeptID, ';') TB ON TB.items = MTD.DeptID
			 WHERE			
				--MTD.Created BETWEEN @FromDate AND @ToDate
				(MTD.FromDate BETWEEN @FromDate AND @ToDate)
				OR
				(MTD.ToDate BETWEEN @FromDate AND @ToDate)
				OR
				(MTD.FromDate <= @FromDate AND @ToDate <= MTD.ToDate)
			  )
			  AND D.Scope IN (2,1)

		-- query task
		UNION ALL
		SELECT 
			--- For Order -----------------------------------------------------------------------------------------------
			CTE.DepthLevel + 1				AS DepthLevel
			, dbo.Func_BuildLevelPath(3, CTE.OLevel, ROW_NUMBER() OVER(ORDER BY TD.Created ASC), CTE.DepthLevel) 
											AS OLevel
			-------------------------------------------------------------------------------------------------------------
			, TD.ID							AS TaskID
			, TD.DocID						AS DocID
			, TD.Name						AS TaskName
			, TD.AssignTo					AS AssignTo
			, TD.FromDate					AS PlanFromDate
			, TD.ToDate						AS PlanToDate
			, TD.[Type]						AS PlanRoleType
			, CAST( (CASE
				 WHEN TD.Status = 4 THEN N'Hoàn thành'
				 --WHEN TD.Status = 2 THEN N'Hoàn thành'
				 ELSE N'Chưa hoàn thành'
			  END)	 AS NVARCHAR(100))		AS ActualLastResult
			, TD.FinishedDate				AS ActualToDate
			, CAST(
			(CASE
				 WHEN TD.Status IN (0,1) THEN
				 dbo.Func_GetTWFDescriptionByTask(TD.ID, TD.AssignTo, 0)
				 ELSE N''
			 END)	
			AS NVARCHAR(2000))				AS ActualTaskName
			,CAST(
			 dbo.Func_GetTWFDescriptionByTask(TD.ID, TD.AssignTo, 1)	
			 AS NVARCHAR(MAX))				AS ActualReason
			, CAST(TD.PercentFinish AS NVARCHAR(10))
											AS PercentFinish
			, TD.DocID						AS ParentID
			, CTE.PlanningType				AS PlanningType 
		FROM 
			TrackingDocuments TD 
			INNER JOIN CTE_MYTASK CTE ON CTE.TaskID = TD.DocID
			INNER JOIN dbo.Func_SplitTextToTable(@DeptID, ';') TB ON TB.items = TD.DeptID
	)

	--INSERT INTO #TempReport
	SELECT 
		REPLACE( REPLACE(RTRIM(LTRIM(REPLACE(OLevel, '0', ' '))),' ','.'), '..', '0.') AS NoOrder,
		TaskName,
		AssignTo,
		PlanFromDate,
		PlanToDate,
		PlanRoleType,
		ActualLastResult,
		ActualToDate,
		ActualTaskName,
		ActualReason,
		PercentFinish,
		DepthLevel, 
		OLevel,
		DocID, 
		TaskID,
		PlanningType
	FROM CTE_MYTASK
	ORDER BY  OLevel;
	
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Report_MonthlyUserTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Report_MonthlyUserTask]
	@UserID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;

    WITH CTE_MYTASK
	AS
	(
		-- query document object
		SELECT 
			--- For Order -------------------------------------------------------------------------
			1								AS DepthLevel
			, dbo.Func_BuildLevelPath(3,'', ROW_NUMBER() OVER(ORDER BY D.Created ASC), 1) 
											AS OLevel
			---------------------------------------------------------------------------------------
			, D.ID							AS TaskID
			, D.ID							AS DocID
			, D.Summary						AS TaskName
			, D.FromDate					AS PlanFromDate
			, D.ToDate						AS PlanToDate
			, 0								AS PlanRoleType
			, D.FromDate					AS ActualFromDate
			, D.ToDate						AS ActualToDate
			, 0								AS ActualRoleType
			, -1							AS StatusResult
			,CAST('' AS NVARCHAR(MAX))		AS LastResult
			,CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER)
											AS ParentID
		FROM 
			Documents D 
		WHERE 
			D.[IsActive] = 1  AND ( D.IsBroadCast = 0 OR D.IsBroadCast IS NULL)
		
			AND D.ID IN 
			(SELECT DISTINCT MTD.DocID FROM TrackingDocuments MTD WHERE MTD.AssignTo = @UserID 	
			AND
			(
			(MTD.FromDate BETWEEN @FromDate AND @ToDate)
				OR
				(MTD.ToDate BETWEEN @FromDate AND @ToDate)
				OR
				(MTD.FromDate <= @FromDate AND @ToDate <= MTD.ToDate) ) )
			  AND D.Scope IN (2,0)
		-- query task
		UNION ALL
		SELECT 
			--- For Order -----------------------------------------------------------------------------------------------
			CTE.DepthLevel + 1				AS DepthLevel
			, dbo.Func_BuildLevelPath(3, CTE.OLevel, ROW_NUMBER() OVER(ORDER BY TD.Created ASC), CTE.DepthLevel) 
											AS OLevel
			-------------------------------------------------------------------------------------------------------------
			, TD.ID							AS TaskID
			, TD.DocID						AS DocID
			, TD.Name						AS TaskName
			, TD.FromDate					AS PlanFromDate
			, TD.ToDate						AS PlanToDate
			, TD.[Type]						AS PlanRoleType
			, TD.FromDate					AS ActualFromDate
			, TD.FinishedDate				AS ActualToDate
			, TD.[Type]						AS ActualRoleType
			, TD.[Status]					AS StatusResult
			, TD.LastResult					AS LastResult
			, TD.DocID						AS ParentID
		FROM 
			TrackingDocuments TD 
			INNER JOIN CTE_MYTASK CTE ON CTE.TaskID = TD.DocID
		WHERE 
			TD.AssignTo = @UserID
	)

	--INSERT INTO #TempReport
	SELECT 
		REPLACE( REPLACE(RTRIM(LTRIM(REPLACE(OLevel, '0', ' '))),' ','.'), '..', '0.') 
		TaskName,
		PlanFromDate,
		PlanToDate,
		PlanRoleType,
		ActualFromDate,
		ActualToDate,
		ActualRoleType, 
		StatusResult,
		LastResult,
		DepthLevel, 
		OLevel
	FROM CTE_MYTASK
	ORDER BY  OLevel;
	
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Report_TaskItemGenernalDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_TaskItemGenernalDetail]
--DECLARE 	
	@DepartmentIds						NVARCHAR(MAX) = NULL,		-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	@ParentDepartmentIds				NVARCHAR(MAX) = NULL,		-- lọc theo cha phòng ban chỉ định, danh sách cha phòng ban, cách nhau bằng dấu ';'
	@FromDate							DATETIME = NULL,			-- lọc theo từ ngày
	@ToDate								DATETIME = NULL,			-- lọc theo đến ngày
	@ReportTrackingDepartmentType		NVARCHAR(20) = 'Detail',	-- lọc theo chi tiết General/Detail - chọn 1
	@ReportTrackingDepartmentStatus		NVARCHAR(20) = 'All',		-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	@ReportTrackingDocumentScope		NVARCHAR(20) = 'All',		-- lọc theo phạm vi của công việc Document.isjob = 1 and Document.Scope == (int) 
	@UserOfDepartmentId					UNIQUEIDENTIFIER = NULL,	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	@IsAssignTo							BIT = 1,					-- lọc dữ liệu theo người xử lý,
	@IsAssignBy							BIT = 0,					-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy =1 lấy theo đk này 
	@IsReport							BIT = 0,					-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	@TrackingStatus						INT = -1,					-- lọc theo tình trạng công việc
	@TrackingCrucial					INT = -1,					-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	@TrackingPlanningTypeSelected		NVARCHAR(500) = 'All',		-- lọc theo loại công việc kế hoạch, giao ban, định kỳ, phát sinh -- document.extentioninfo -> planningtype, chọn nhiều
	@CategorizeTrackingDocumentId		INT = -1					-- lọc theo loại công việc
AS
BEGIN
	SET NOCOUNT ON;
	-- tạo bảng tạm lưu trữ
	CREATE TABLE #ReportTrack (Id UNIQUEIDENTIFIER NULL, ParentId UNIQUEIDENTIFIER NULL, ProjectId UNIQUEIDENTIFIER NULL);
	DECLARE 
		@Sql				NVARCHAR(MAX) = '',
		@ParamDefines		NVARCHAR(2000) = '';

	-- khởi tạo câu query
	SET @Sql = @Sql + ' INSERT INTO #ReportTrack ';
	SET @Sql = @Sql + '		SELECT Track.Id, Track.ParentId, Track.ProjectId ';
	SET @Sql = @Sql + '		FROM Task.TaskItem Track ';
	SET @Sql = @Sql + '			INNER JOIN Task.Project D ON D.Id = Track.ProjectId '
	SET @Sql = @Sql + '		WHERE D.IsActive = 1 ';

	-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	IF (@DepartmentIds IS NOT NULL AND @DepartmentIds != '' AND @IsAssignBy = 0 AND @TrackingPlanningTypeSelected != 'All')
	BEGIN
		SET @Sql = @Sql + '		AND Track.DepartmentId IN ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'') ) ';		
	END
	
	IF (@TrackingPlanningTypeSelected like '%Weirdo%')
	BEGIN
		SET @TrackingPlanningTypeSelected = 'All'
		SET @Sql = @Sql + '		AND Track.IsWeirdo = 1 ';
	END

	--lọc theo loại công việc
	IF (@CategorizeTrackingDocumentId != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemCategoryId = @CategorizeTrackingDocumentId ';
	END

	-- lọc theo từ ngày
	IF (@FromDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '		AND Track.FromDate >= @FromDate ';		
	END
	
	-- lọc theo đến ngày
	IF (@ToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '		AND Track.FromDate <= @ToDate ';		
	END

	-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	IF (@ReportTrackingDepartmentStatus = 'InDueDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
	END
	ELSE
	-- lọc theo công việc quá hạn
	IF (@ReportTrackingDepartmentStatus = 'OutOfDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 '		
	END 

	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	IF (@UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER))
	BEGIN
		-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy = 1 lấy theo đk này 
		-- lọc dữ liệu theo người giao việc
		IF (@IsAssignBy = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.AssignBy = @UserOfDepartmentId ';
		END
		ELSE
		IF (@IsAssignTo = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.Id in (SELECT TaskItemId FROM Task.TaskItemAssign WHERE AssignTo = @UserOfDepartmentId )'; 
		END
	END

	-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	IF (@IsReport = 1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.IsReport = 1 '; 
	END
	
	-- lọc theo tình trạng công việc
	IF (@TrackingStatus != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemStatusId = @TrackingStatus ';		
	END

	-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	IF (@TrackingCrucial != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemPriorityId = '+@TrackingCrucial ;		
	END

	SET @ParamDefines = ' @DepartmentIds NVARCHAR(MAX), @FromDate DATETIME, @ToDate	DATETIME, @UserOfDepartmentId	UNIQUEIDENTIFIER, '
		+ ' @IsAssignTo BIT, @IsAssignBy BIT, @IsReport BIT, @TrackingStatus INT, @TrackingCrucial INT, @TrackingPlanningTypeSelected NVARCHAR(500),@CategorizeTrackingDocumentId INT  ';
	Print @sql;
	EXEC sys.sp_executesql @Sql, @ParamDefines,
		@DepartmentIds = @DepartmentIds, @FromDate = @FromDate, @ToDate = @ToDate,
		@UserOfDepartmentId = @UserOfDepartmentId, @IsAssignTo = @IsAssignTo, @IsAssignBy = @IsAssignBy,
		@IsReport = @IsReport, @TrackingStatus = @TrackingStatus, @TrackingCrucial = @TrackingCrucial,
		@TrackingPlanningTypeSelected = @TrackingPlanningTypeSelected,@CategorizeTrackingDocumentId = @CategorizeTrackingDocumentId;

	-- detail
	IF (@ReportTrackingDepartmentType = 'Detail')
	BEGIN
		IF (@IsAssignTo = 1 AND @UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER) )
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignToId,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemAssignId = TA.Id ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id AND TA.AssignTo=@UserOfDepartmentId
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			ORDER BY
				Track.CreatedDate;
		END
		ELSE
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignToId,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemId = Track.ID ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			ORDER BY
				Track.CreatedDate;
		END
	END
	ELSE
	BEGIN
		DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT); 
		DECLARE @GenralTbls TABLE (DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, AssignTo UNIQUEIDENTIFIER,
								   TotalTask INT, TotalFinishedTask INT, TotalNotFinishedTask INT, TotalOverDays INT,
								   DeptLevel INT); 
		DECLARE @SelectedDeptId UNIQUEIDENTIFIER = NULL;
		DECLARE @CurrentDate DATETIME = GETDATE(), @IdxLv INT = 1, @MaxLv INT = -1;

		DELETE @Depts;
		DELETE @GenralTbls;
		
		INSERT INTO @Depts
			SELECT tb1.STT, tb1.items, tb2.items, -1
			FROM 
				dbo.Func_SplitTextToTable_WithRowNumber(@DepartmentIds, ';') Tb1
				INNER JOIN dbo.Func_SplitTextToTable_WithRowNumber(@ParentDepartmentIds, ';') Tb2 ON Tb2.STT = Tb1.STT;
		
		SELECT @SelectedDeptId = DeptId FROM @Depts WHERE Idx = 1;

		-- build deptlevel
		WITH #TbDeptLevel
		AS
		(
			SELECT D.DeptId, 1 AS DeptLevel
			FROM @Depts D
			WHERE D.Idx = 1
			UNION ALL
			SELECT D.DeptId, DL.DeptLevel + 1 AS DeptLevel
			FROM @Depts D
				INNER JOIN #TbDeptLevel DL ON DL.DeptId = D.ParentId			
		)

		UPDATE @Depts
		SET DeptLevel = DL.DeptLevel
		FROM @Depts D 
			INNER JOIN #TbDeptLevel DL ON DL.DeptId = D.DeptId;
		
		INSERT INTO @GenralTbls
			SELECT 
				Track.DepartmentId,
				D.ParentId,
				TRY_CAST(0x0 as UNIQUEIDENTIFIER) As AssignTo,
				COUNT(Track.ID) AS TotalTask,
				SUM(CASE WHEN  Track.TaskItemStatusId = 4  THEN 1 ELSE 0 END ) AS TotalFinishedTask,
				SUM(CASE WHEN  Track.TaskItemStatusId != 4  THEN 1 ELSE 0 END ) AS TotalNotFinishedTask,
				SUM(CASE 
					WHEN TRY_CAST(Track.ToDate AS DATE ) < TRY_CAST(@CurrentDate AS DATE) THEN DATEDIFF(DAY, Track.ToDate, @CurrentDate)
					ELSE 0
					 END) TotalOverDays,
				D.DeptLevel
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN @Depts D ON Track.DepartmentId = D.DeptId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId= Track.Id 
			GROUP BY
				Track.DepartmentId, TA.AssignTo, D.ParentId, D.DeptLevel;
				
		SELECT
			@MaxLv = MAX(DeptLevel)
		FROM @Depts;
		-- for each row to sum up
		WHILE (@IdxLv <= @MaxLv)
		BEGIN
			INSERT INTO @GenralTbls
				SELECT	
					GT.DeptId, 
					GT.ParentId, 
					TRY_CAST(0x0 as UNIQUEIDENTIFIER) As AssignTo,
					SUM(GT.TotalTask) AS TotalTask,
					SUM(GT.TotalFinishedTask) AS TotalFinishedTask,
					SUM(GT.TotalNotFinishedTask) AS TotalNotFinishedTask,
					SUM(GT.TotalOverDays) AS TotalOverDays,
					GT.DeptLevel
				FROM 
					@GenralTbls GT
				WHERE 
					GT.DeptLevel = @MaxLv
				GROUP BY
					GT.DeptId, GT.ParentId, GT.DeptLevel;
			
			UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + (SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = TRY_CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalFinishedTask =  TotalFinishedTask +
							(SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = TRY_CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalNotFinishedTask =  TotalNotFinishedTask +
							(SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = TRY_CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalOverDays = TotalOverDays +
							(SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = TRY_CAST(0x0 AS UNIQUEIDENTIFIER) )
			WHERE AssignTo = TRY_CAST(0x0 as UNIQUEIDENTIFIER) AND DeptLevel = @MaxLv
			AND DeptId IN 
			(SELECT ParentId FROM @GenralTbls )

			SET @MaxLv = @MaxLv - 1;
		END
		
		SELECT 
			DeptId						AS DepartmentId,
			ParentId					AS DepartmentParentId,
			DeptLevel					AS DepartmentLevel,
			SUM(TotalTask)				AS TrackingCount,
			SUM(TotalFinishedTask)		AS TrackingFinishedCount,
			SUM(TotalNotFinishedTask)	AS TrackingNotFinishedCount,
			SUM(TotalOverDays)			AS TrackingTotalOverDays,
			AssignTo,
			'0'							AS TrackingStatus
		FROM @GenralTbls
		WHERE
			@SelectedDeptId = DeptId OR  
			( ParentId = @SelectedDeptId AND AssignTo = TRY_CAST(0x0 as UNIQUEIDENTIFIER)  )
		GROUP BY
			DeptId, ParentId, DeptLevel, TotalOverDays, AssignTo
		ORDER BY DeptLevel, DeptId, AssignTo;		 
	END

	DROP TABLE #ReportTrack;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Report_TaskItemGenernalDetail_V2]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_TaskItemGenernalDetail_V2]
--DECLARE 	
	@DepartmentIds						NVARCHAR(MAX) = NULL,		-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	@ParentDepartmentIds				NVARCHAR(MAX) = NULL,		-- lọc theo cha phòng ban chỉ định, danh sách cha phòng ban, cách nhau bằng dấu ';'
	@FromDate							DATETIME = NULL,			-- lọc theo từ ngày
	@ToDate								DATETIME = NULL,			-- lọc theo đến ngày
	@ReportTrackingDepartmentType		NVARCHAR(20) = 'Detail',	-- lọc theo chi tiết General/Detail - chọn 1
	@ReportTrackingDepartmentStatus		NVARCHAR(20) = 'All',		-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	@ReportTrackingDocumentScope		NVARCHAR(20) = 'All',		-- lọc theo phạm vi của công việc Document.isjob = 1 and Document.Scope == (int) 
	@UserOfDepartmentId					UNIQUEIDENTIFIER = NULL,	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	@IsAssignTo							BIT = 1,					-- lọc dữ liệu theo người xử lý,
	@IsAssignBy							BIT = 0,					-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy =1 lấy theo đk này 
	@IsReport							BIT = 0,					-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	@TrackingStatus						INT = -1,					-- lọc theo tình trạng công việc
	@TrackingCrucial					INT = -1,					-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	@TrackingPlanningTypeSelected		NVARCHAR(500) = 'All',		-- lọc theo loại công việc kế hoạch, giao ban, định kỳ, phát sinh -- document.extentioninfo -> planningtype, chọn nhiều
	@CategorizeTrackingDocumentId		INT = -1					-- lọc theo loại công việc
AS
--SELECT 
--@DepartmentIds=N'4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;49332ea7-9baa-49cc-b4f3-12344cf75199;64e4bd71-8cf5-454e-8111-571096185486;1b5a83ce-78a9-4e28-82c6-a9637b119560;',@ParentDepartmentIds=N'fc7e9feb-d476-48ea-901c-c6e4bc77444f;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;',@FromDate='2020-01-01 00:00:00',@ToDate='2020-01-31 00:00:00',@ReportTrackingDepartmentType=N'General',@ReportTrackingDepartmentStatus=N'All',@ReportTrackingDocumentScope=N'All',@UserOfDepartmentId='00000000-0000-0000-0000-000000000000',@IsAssignTo=1,@IsAssignBy=0,@IsReport=0,@TrackingStatus=-1,@TrackingCrucial=-1,@TrackingPlanningTypeSelected=N'All',@CategorizeTrackingDocumentId=-1
--@DepartmentIds=N'cd912ef6-675a-4aea-90c4-6f6afa43c9e0;2de3cd87-45d6-452d-95a0-7eb639863964;fc7e9feb-d476-48ea-901c-c6e4bc77444f;6301d06f-8737-4f1c-9b36-038872bccd32;16802de4-e4b6-460b-9620-1c982b0fb3cd;56cd00bc-fdf9-4b88-a70d-252a04f84da3;a2c5717d-874c-4e2a-b23c-2bd99df5731b;e62077d3-51f3-4df6-81d2-41b2bf36e320;5984c23f-5c7c-4b17-9680-5b3488d1297d;4da9f0bf-aad3-4861-83fc-847eab21c623;53e4cb14-f86f-47a2-8020-896d0daf79a6;b63e9a6c-f1b5-4c30-8b56-c7903bf2f3ff;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4a175fff-7b4e-45f6-a907-cb2379a3eb8f;551c4833-66b2-4f70-a72f-d7085958f239;ccbd45fb-6ab4-494b-88d1-1bba061915bf;6aec259a-6220-4a41-ba25-32462cb3180e;27b17a52-f3e3-4b78-b837-538b465e56b1;23176518-745d-4602-b88e-68a5b2a2613b;48160875-d0aa-452d-86d8-0f1b6a3f4ed3;e902ff36-131a-409c-a908-3e9694376a30;02e1f7c9-772f-4b89-8e34-c785a6ccd5fd;e8bdb53b-81f4-4b93-9860-ac0a0cd47260;5dffded3-9999-4003-8cd7-cbcc70c70315;3e54106c-4074-4806-a55b-0f471158cd94;fb38c5ba-a269-4782-9056-2ebd03b8691f;efc6635e-8622-4aaa-9011-79a8d292bf66;033e4728-2b5a-47c8-a83d-91b145296a50;2a243d3b-830a-4d2e-9192-93f90469efe8;3a9b15da-285e-45d3-b359-b375a5b51d6a;5d03851c-63e4-4365-936f-c0a9e95d3f15;1b59a597-2735-4a9b-a085-cfcd15aeab08;c5ef8ab2-da1a-4293-bfd2-d92e3c6fe35e;718b7f1d-fe3d-4e24-b86b-ffb322fa43c7;d675a5d0-e20c-4902-8c35-0a3d63fc6866;1769f9a2-765e-4e2c-86f0-47386de0c197;8d26f299-4ed9-42af-a0e6-996bd9370676;49332ea7-9baa-49cc-b4f3-12344cf75199;64e4bd71-8cf5-454e-8111-571096185486;1b5a83ce-78a9-4e28-82c6-a9637b119560;',@ParentDepartmentIds=N'00000000-0000-0000-0000-000000000000;cd912ef6-675a-4aea-90c4-6f6afa43c9e0;cd912ef6-675a-4aea-90c4-6f6afa43c9e0;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;a2c5717d-874c-4e2a-b23c-2bd99df5731b;a2c5717d-874c-4e2a-b23c-2bd99df5731b;a2c5717d-874c-4e2a-b23c-2bd99df5731b;e62077d3-51f3-4df6-81d2-41b2bf36e320;e62077d3-51f3-4df6-81d2-41b2bf36e320;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;53e4cb14-f86f-47a2-8020-896d0daf79a6;53e4cb14-f86f-47a2-8020-896d0daf79a6;53e4cb14-f86f-47a2-8020-896d0daf79a6;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;',@FromDate='2020-01-01 00:00:00',@ToDate='2020-01-31 00:00:00',@ReportTrackingDepartmentType=N'General',@ReportTrackingDepartmentStatus=N'All',@ReportTrackingDocumentScope=N'All',@UserOfDepartmentId='00000000-0000-0000-0000-000000000000',@IsAssignTo=1,@IsAssignBy=0,@IsReport=0,@TrackingStatus=-1,@TrackingCrucial=-1,@TrackingPlanningTypeSelected=N'All',@CategorizeTrackingDocumentId=-1

BEGIN
	SET NOCOUNT ON;
	-- tạo bảng tạm lưu trữ
	DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT, PathLevel NVARCHAR(100));
	
	DELETE @Depts;
		INSERT INTO @Depts
			SELECT tb1.STT, tb1.items, tb2.items, -1, ''
			FROM 
				dbo.Func_SplitTextToTable_WithRowNumber(@DepartmentIds, ';') Tb1
				INNER JOIN dbo.Func_SplitTextToTable_WithRowNumber(@ParentDepartmentIds, ';') Tb2 ON Tb2.STT = Tb1.STT
				INNER JOIN Departments D ON D.ID = tb1.items AND D.IsActive = 1;

	CREATE TABLE #ReportTrack (Id UNIQUEIDENTIFIER NULL, ParentId UNIQUEIDENTIFIER NULL, ProjectId UNIQUEIDENTIFIER NULL,
							   DeptId UNIQUEIDENTIFIER NULL);
	DECLARE 
		@Sql				NVARCHAR(MAX) = '',
		@ParamDefines		NVARCHAR(2000) = ''; 
	-- khởi tạo câu query
	SET @Sql = @Sql + ' INSERT INTO #ReportTrack ';
	SET @Sql = @Sql + '		SELECT Track.Id, Track.ParentId, Track.ProjectId,
	   ISNULL( dbo.Func_GetDeptIdOfTaskItem(@DepartmentIds, @UserOfDepartmentId, Track.Id ) , track.departmentid)';
	SET @Sql = @Sql + '		FROM Task.TaskItem Track ';
	SET @Sql = @Sql + '			INNER JOIN Task.Project D ON D.Id = Track.ProjectId '
	SET @Sql = @Sql + '		WHERE D.IsActive = 1 ';

	-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	IF (@DepartmentIds IS NOT NULL AND @DepartmentIds != '' AND @IsAssignBy = 0 )-- AND @TrackingPlanningTypeSelected != 'All')
	BEGIN
		SET @Sql = @Sql + '	AND
		(
		 Track.DepartmentId IN ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'') )
		 OR
		 EXISTS(SELECT Top 1 TiA.Id FROM Task.TaskItemAssign TiA WHERE
		  TiA.TaskItemId = Track.Id AND
		  TiA.DepartmentId IN  ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'')) 
		  AND Tia.TaskType != 7 )
		 )
		  ';		
	END
	
	IF (@TrackingPlanningTypeSelected like '%Weirdo%')
	BEGIN
		SET @TrackingPlanningTypeSelected = 'All'
		SET @Sql = @Sql + '		AND Track.IsWeirdo = 1 ';
	END

	--lọc theo loại công việc
	IF (@CategorizeTrackingDocumentId != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemCategoryId = @CategorizeTrackingDocumentId ';
	END

	-- lọc theo từ ngày đến ngày
	IF (@FromDate IS NOT NULL AND @ToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '	AND ( ';
		SET @Sql = @Sql + ' (Track.FromDate BETWEEN @FromDate AND @ToDate)  ';
		--SET @Sql = @Sql + ' OR (Track.ToDate BETWEEN @FromDate AND @ToDate)  ';
		--SET @Sql = @Sql + ' OR (Track.FromDate <= @FromDate AND @ToDate <= Track.ToDate) ';
		SET @Sql = @Sql + ' ) '; 

	END 

	-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	IF (@ReportTrackingDepartmentStatus = 'InDueDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
	END
	ELSE
	-- lọc theo công việc quá hạn
	IF (@ReportTrackingDepartmentStatus = 'OutOfDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 '		
	END 

	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	IF (@UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER))
	BEGIN
		-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy = 1 lấy theo đk này 
		-- lọc dữ liệu theo người giao việc
		IF (@IsAssignBy = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.AssignBy = @UserOfDepartmentId ';
		END
		ELSE
		IF (@IsAssignTo = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.Id in (SELECT TaskItemId FROM Task.TaskItemAssign WHERE AssignTo = @UserOfDepartmentId )'; 
		END
	END

	-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	IF (@IsReport = 1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.IsReport = 1 '; 
	END
	
	-- lọc theo tình trạng công việc
	IF (@TrackingStatus != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemStatusId = @TrackingStatus ';		
	END

	-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	IF (@TrackingCrucial != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemPriorityId = '+@TrackingCrucial ;		
	END

	SET @ParamDefines = ' @DepartmentIds NVARCHAR(MAX), @FromDate DATETIME, @ToDate	DATETIME, @UserOfDepartmentId	UNIQUEIDENTIFIER, '
		+ ' @IsAssignTo BIT, @IsAssignBy BIT, @IsReport BIT, @TrackingStatus INT, @TrackingCrucial INT, @TrackingPlanningTypeSelected NVARCHAR(500),@CategorizeTrackingDocumentId INT  ';
	Print @sql;
	EXEC sys.sp_executesql @Sql, @ParamDefines,
		@DepartmentIds = @DepartmentIds, @FromDate = @FromDate, @ToDate = @ToDate,
		@UserOfDepartmentId = @UserOfDepartmentId, @IsAssignTo = @IsAssignTo, @IsAssignBy = @IsAssignBy,
		@IsReport = @IsReport, @TrackingStatus = @TrackingStatus, @TrackingCrucial = @TrackingCrucial,
		@TrackingPlanningTypeSelected = @TrackingPlanningTypeSelected,@CategorizeTrackingDocumentId = @CategorizeTrackingDocumentId;
	 --select * from #ReportTrack
	-- detail
	IF (@ReportTrackingDepartmentType = 'Detail')
	BEGIN
		

		IF (@IsAssignTo = 1 AND @UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER) )
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignToId,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemAssignId = TA.Id ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
				, cast(track.Weight as NVARCHAR(4)) [Weight]
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id AND TA.AssignTo=@UserOfDepartmentId
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			--	INNER JOIN @Depts Dep ON Track.DepartmentId = Dep.DeptId
			ORDER BY
				Track.CreatedDate;
		END
		ELSE
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate, 
				 CONVERT(varchar, Track.FinishedDate, 103)      FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND TS.Id = 4) 
					THEN 0
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 --Track.AssignBy									AssignById,
				 dbo.Func_GetFullNameByUserId(track.AssignBy) AssignBy,
				 --TA.AssignTo	
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 1)	PrimaryAssignTo, 
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 3)	SupportAssignTo,
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 7)	ReadOnlyAssignTo,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemId = Track.ID ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
				, cast(track.Weight as NVARCHAR(4)) [Weight]
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				--INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
				--INNER JOIN @Depts Dep ON Track.DepartmentId = Dep.DeptId
			ORDER BY
				Track.CreatedDate;
		END
	END
	ELSE
	BEGIN
		--DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT); 
		DECLARE @GenralTbls TABLE (DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER,		
								   TotalTask INT, TotalFinishedTask INT, TotalNotFinishedTask INT, TotalOverDays INT,
								   DeptLevel INT, PathLevel nvarchar(100),
								   AssignTo UNIQUEIDENTIFIER); 
		DECLARE @SelectedDeptId UNIQUEIDENTIFIER = NULL;
		DECLARE @CurrentDate DATETIME = GETDATE(), @IdxLv INT = 1, @MaxLv INT = -1;
		 
		DELETE @GenralTbls;

		SELECT @SelectedDeptId = DeptId FROM @Depts WHERE Idx = 1;
		-- Phốt đầy
		--
		;WITH #TbOrgTemp 
		AS
		(
			SELECT D.ID DeptId, 
				CAST( D.Name  as nvarchar(100) )AS Name,
					D.Code, 
					CAST(D.Code as NVARCHAR(1000)) as PathLevel,
					cast(' ' as NVARCHAR(10)) as dashpath
					,1 AS DeptLevel
			FROM 
				Departments AS D    		
			WHERE
				D.IsActive = 1 AND
				(D.ParentID IS NULL OR D.ParentID = CAST(0x0 AS UNIQUEIDENTIFIER) )
			UNION ALL 			
			SELECT 
				D.ID DeptId, 
				CAST ( dp.dashpath + ' ' + D.Name  as nvarchar(100)) Name,
					D.Code, 
					CAST(DP.PathLevel + '\' + D.Code as NVARCHAR(1000)) as PathLevel,
					cast(dp.dashpath + '     ' as NVARCHAR(10)) as dashpath
					,dp.DeptLevel + 1 AS DeptLevel
			FROM 
				Departments AS D
				INNER JOIN #TbOrgTemp AS DP ON DP.DeptId = D.ParentId AND D.IsActive = 1	    
		) 
		
		UPDATE @Depts
		SET DeptLevel = DL.DeptLevel,  PathLevel  = DL.PathLevel
		FROM @Depts D 
			INNER JOIN #TbOrgTemp DL ON DL.DeptId = D.DeptId ;
	 

		INSERT INTO @GenralTbls
			SELECT 
				RT.DeptId DepartmentId,
				D.ParentId, 
				COUNT(Track.ID) AS TotalTask,
				SUM(CASE WHEN  Track.TaskItemStatusId = 4  THEN 1 ELSE 0 END ) AS TotalFinishedTask,
				SUM(CASE WHEN  Track.TaskItemStatusId != 4  THEN 1 ELSE 0 END ) AS TotalNotFinishedTask,
				SUM(CASE 
					WHEN Track.ToDate IS NULL THEN 0
					WHEN CAST(Track.ToDate AS DATE ) < CAST(@CurrentDate AS DATE) THEN DATEDIFF(DAY, Track.ToDate, @CurrentDate)
					ELSE 0
					 END) TotalOverDays,
				D.DeptLevel
				,D.PathLevel
				,CAST(0x0 as UNIQUEIDENTIFIER)
			FROM
				--@Depts D
				--LEFT JOIN Task.TaskItem Track  ON Track.DepartmentId = D.DeptId
				--LEFT JOIN #ReportTrack RT ON RT.Id = Track.Id
				#ReportTrack RT
				INNER JOIN Task.TaskItem Track ON Track.Id = RT.Id
				LEFT JOIN @Depts D ON D.DeptId = RT.DeptId 
			GROUP BY
				RT.DeptId, --TA.AssignTo,
				 D.ParentId, D.DeptLevel, d.PathLevel;	  

		SELECT
			@MaxLv = MAX(DISTINCT DeptLevel)
		FROM @GenralTbls;
		--select @MaxLv, @IdxLv
		-- for each row to sum up
		WHILE (@IdxLv <= @MaxLv)
		BEGIN 
			-- nếu tồn tại task phòng ban đang đứng
			-- thêm 1 phòng ban của nó để đánh dấu count ngược lên
			INSERT INTO @GenralTbls
				SELECT
					
					G.DeptId,
					G.ParentId, 
					0 AS TotalTask,
					0 AS TotalFinishedTask,
					0 AS TotalNotFinishedTask,
					0 TotalOverDays,
					G.DeptLevel,
					G.PathLevel,
					'11111111-1111-1111-1111-111111111111'
				FROM 
					@GenralTbls G
				WHERE 
					G.DeptLevel = @MaxLv AND G.TotalTask > 0;
			 
			UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + 
				ISNULL((SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0),
				TotalFinishedTask =  TotalFinishedTask +
				ISNULL((SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalNotFinishedTask =  TotalNotFinishedTask +
				ISNULL((SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalOverDays = TotalOverDays +
				ISNULL((SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0)
			WHERE 
				DeptLevel = @MaxLv AND TotalTask = 0;
				 
			SET @MaxLv = @MaxLv - 1;
		END

		--UNION ALL
		INSERT INTO @GenralTbls
			SELECT 
				dt.DeptId,
				dt.ParentId, 
				0,0,0,0,  
					dt.DeptLevel,
					dt.PathLevel,
					'11111111-1111-1111-1111-111111111111'
			FROM 
				@Depts dt
				inner join Departments d on d.ID = dt.DeptId
			WHERE 
				dt.DeptId NOT IN 
				(SELECT G.DeptId  FROM @GenralTbls  G)

		UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + 
				ISNULL((SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0),
				TotalFinishedTask =  TotalFinishedTask +
				ISNULL((SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalNotFinishedTask =  TotalNotFinishedTask +
				ISNULL((SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalOverDays = TotalOverDays +
				ISNULL((SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0)
			WHERE 
				DeptLevel = 1 AND TotalTask = 0;

		SELECT *
		FROM 
		(
			SELECT 
				DeptId						AS DepartmentId,
				G.ParentId					AS DepartmentParentId,
				DeptLevel					AS DepartmentLevel,
				SUM(TotalTask)				AS TrackingCount,
				SUM(TotalFinishedTask)		AS TrackingFinishedCount,
				SUM(TotalNotFinishedTask)	AS TrackingNotFinishedCount,
				SUM(TotalOverDays)			AS TrackingTotalOverDays,
				CAST(0x0 AS UNIQUEIDENTIFIER) AS AssignTo,
				'0'							AS TrackingStatus
				,D.Name
				,G.PathLevel
				--, dbo.Func_GetLoginNameByID(G.AssignTo)
			FROM @GenralTbls G
				INNER JOIN Departments D ON D.ID =  DeptId
			--WHERE
			--	 G.DeptLevel <= 3 
			GROUP BY
				DeptId, G.ParentId, AssignTo, DeptLevel, G.PathLevel --,  
				,D.Name
			--UNION ALL
			--SELECT 
			--	dt.DeptId,
			--	dt.ParentId,
			--	dt.DeptLevel,
			--	0,0,0,0,
			--	cast(0x0 as UNIQUEIDENTIFIER), '0',
			--	d.Name,
			--	dt.PathLevel
			--FROM 
			--	@Depts dt
			--	inner join Departments d on d.ID = dt.DeptId
			--WHERE 
			--	dt.DeptId NOT IN 
			--	(SELECT G.DeptId  FROM @GenralTbls G)
			
		) TB
		ORDER BY TB.PathLevel
	END
	--SELECT *
	--FROM @Depts
	DROP TABLE #ReportTrack;
END



GO
/****** Object:  StoredProcedure [dbo].[SP_Report_TaskItemGenernalDetail_V3]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[SP_Report_TaskItemGenernalDetail_V3]
--DECLARE 	
	@DepartmentIds						NVARCHAR(MAX) = NULL,		-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	@ParentDepartmentIds				NVARCHAR(MAX) = NULL,		-- lọc theo cha phòng ban chỉ định, danh sách cha phòng ban, cách nhau bằng dấu ';'
	@FromDate							DATETIME = NULL,			-- lọc theo từ ngày
	@ToDate								DATETIME = NULL,			-- lọc theo đến ngày
	@ReportTrackingDepartmentType		NVARCHAR(20) = 'Detail',	-- lọc theo chi tiết General/Detail - chọn 1
	@ReportTrackingDepartmentStatus		NVARCHAR(20) = 'All',		-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	@ReportTrackingDocumentScope		NVARCHAR(20) = 'All',		-- lọc theo phạm vi của công việc Document.isjob = 1 and Document.Scope == (int) 
	@UserOfDepartmentId					UNIQUEIDENTIFIER = NULL,	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	@IsAssignTo							BIT = 1,					-- lọc dữ liệu theo người xử lý,
	@IsAssignBy							BIT = 0,					-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy =1 lấy theo đk này 
	@IsReport							BIT = 0,					-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	@TrackingStatus						INT = -1,					-- lọc theo tình trạng công việc
	@TrackingCrucial					INT = -1,					-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	@TrackingPlanningTypeSelected		NVARCHAR(500) = 'All',		-- lọc theo loại công việc kế hoạch, giao ban, định kỳ, phát sinh -- document.extentioninfo -> planningtype, chọn nhiều
	@CategorizeTrackingDocumentId		INT = -1					-- lọc theo loại công việc
AS
--SELECT 
--@DepartmentIds=N'4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;49332ea7-9baa-49cc-b4f3-12344cf75199;64e4bd71-8cf5-454e-8111-571096185486;1b5a83ce-78a9-4e28-82c6-a9637b119560;',@ParentDepartmentIds=N'fc7e9feb-d476-48ea-901c-c6e4bc77444f;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;',@FromDate='2020-01-01 00:00:00',@ToDate='2020-01-31 00:00:00',@ReportTrackingDepartmentType=N'General',@ReportTrackingDepartmentStatus=N'All',@ReportTrackingDocumentScope=N'All',@UserOfDepartmentId='00000000-0000-0000-0000-000000000000',@IsAssignTo=1,@IsAssignBy=0,@IsReport=0,@TrackingStatus=-1,@TrackingCrucial=-1,@TrackingPlanningTypeSelected=N'All',@CategorizeTrackingDocumentId=-1
--@DepartmentIds=N'cd912ef6-675a-4aea-90c4-6f6afa43c9e0;2de3cd87-45d6-452d-95a0-7eb639863964;fc7e9feb-d476-48ea-901c-c6e4bc77444f;6301d06f-8737-4f1c-9b36-038872bccd32;16802de4-e4b6-460b-9620-1c982b0fb3cd;56cd00bc-fdf9-4b88-a70d-252a04f84da3;a2c5717d-874c-4e2a-b23c-2bd99df5731b;e62077d3-51f3-4df6-81d2-41b2bf36e320;5984c23f-5c7c-4b17-9680-5b3488d1297d;4da9f0bf-aad3-4861-83fc-847eab21c623;53e4cb14-f86f-47a2-8020-896d0daf79a6;b63e9a6c-f1b5-4c30-8b56-c7903bf2f3ff;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4a175fff-7b4e-45f6-a907-cb2379a3eb8f;551c4833-66b2-4f70-a72f-d7085958f239;ccbd45fb-6ab4-494b-88d1-1bba061915bf;6aec259a-6220-4a41-ba25-32462cb3180e;27b17a52-f3e3-4b78-b837-538b465e56b1;23176518-745d-4602-b88e-68a5b2a2613b;48160875-d0aa-452d-86d8-0f1b6a3f4ed3;e902ff36-131a-409c-a908-3e9694376a30;02e1f7c9-772f-4b89-8e34-c785a6ccd5fd;e8bdb53b-81f4-4b93-9860-ac0a0cd47260;5dffded3-9999-4003-8cd7-cbcc70c70315;3e54106c-4074-4806-a55b-0f471158cd94;fb38c5ba-a269-4782-9056-2ebd03b8691f;efc6635e-8622-4aaa-9011-79a8d292bf66;033e4728-2b5a-47c8-a83d-91b145296a50;2a243d3b-830a-4d2e-9192-93f90469efe8;3a9b15da-285e-45d3-b359-b375a5b51d6a;5d03851c-63e4-4365-936f-c0a9e95d3f15;1b59a597-2735-4a9b-a085-cfcd15aeab08;c5ef8ab2-da1a-4293-bfd2-d92e3c6fe35e;718b7f1d-fe3d-4e24-b86b-ffb322fa43c7;d675a5d0-e20c-4902-8c35-0a3d63fc6866;1769f9a2-765e-4e2c-86f0-47386de0c197;8d26f299-4ed9-42af-a0e6-996bd9370676;49332ea7-9baa-49cc-b4f3-12344cf75199;64e4bd71-8cf5-454e-8111-571096185486;1b5a83ce-78a9-4e28-82c6-a9637b119560;',@ParentDepartmentIds=N'00000000-0000-0000-0000-000000000000;cd912ef6-675a-4aea-90c4-6f6afa43c9e0;cd912ef6-675a-4aea-90c4-6f6afa43c9e0;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;fc7e9feb-d476-48ea-901c-c6e4bc77444f;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;16802de4-e4b6-460b-9620-1c982b0fb3cd;a2c5717d-874c-4e2a-b23c-2bd99df5731b;a2c5717d-874c-4e2a-b23c-2bd99df5731b;a2c5717d-874c-4e2a-b23c-2bd99df5731b;e62077d3-51f3-4df6-81d2-41b2bf36e320;e62077d3-51f3-4df6-81d2-41b2bf36e320;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;4da9f0bf-aad3-4861-83fc-847eab21c623;53e4cb14-f86f-47a2-8020-896d0daf79a6;53e4cb14-f86f-47a2-8020-896d0daf79a6;53e4cb14-f86f-47a2-8020-896d0daf79a6;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;',@FromDate='2020-01-01 00:00:00',@ToDate='2020-01-31 00:00:00',@ReportTrackingDepartmentType=N'General',@ReportTrackingDepartmentStatus=N'All',@ReportTrackingDocumentScope=N'All',@UserOfDepartmentId='00000000-0000-0000-0000-000000000000',@IsAssignTo=1,@IsAssignBy=0,@IsReport=0,@TrackingStatus=-1,@TrackingCrucial=-1,@TrackingPlanningTypeSelected=N'All',@CategorizeTrackingDocumentId=-1

--SELECT 
--	@DepartmentIds=N'64e4bd71-8cf5-454e-8111-571096185486;',@ParentDepartmentIds=N'4ccc9952-53b1-4eb5-b91e-cb07bf0a3706;',
--	@FromDate='2020-02-01 00:00:00',@ToDate='2020-02-29 00:00:00',@ReportTrackingDepartmentType=N'Detail',
--	@ReportTrackingDepartmentStatus=N'All',@ReportTrackingDocumentScope=N'All',
--	@UserOfDepartmentId='070C1F19-E840-4744-B58B-8C1BD21EA68D',@IsAssignTo=1,@IsAssignBy=0,@IsReport=0,@TrackingStatus=-1,
--	@TrackingCrucial=-1,@TrackingPlanningTypeSelected=N'All',@CategorizeTrackingDocumentId=-1

BEGIN
	SET NOCOUNT ON;
	-- tạo bảng tạm lưu trữ
	DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT, PathLevel NVARCHAR(100));
	
	DELETE @Depts;
		INSERT INTO @Depts
			SELECT tb1.STT, tb1.items, tb2.items, -1, ''
			FROM 
				dbo.Func_SplitTextToTable_WithRowNumber(@DepartmentIds, ';') Tb1
				INNER JOIN dbo.Func_SplitTextToTable_WithRowNumber(@ParentDepartmentIds, ';') Tb2 ON Tb2.STT = Tb1.STT
				INNER JOIN Departments D ON D.ID = tb1.items AND D.IsActive = 1;

	CREATE TABLE #ReportTrack (Id UNIQUEIDENTIFIER NULL, ParentId UNIQUEIDENTIFIER NULL, ProjectId UNIQUEIDENTIFIER NULL,
							   DeptId UNIQUEIDENTIFIER NULL);
	DECLARE 
		@Sql				NVARCHAR(MAX) = '',
		@ParamDefines		NVARCHAR(2000) = ''; 
	-- khởi tạo câu query
	SET @Sql = @Sql + ' INSERT INTO #ReportTrack ';
	SET @Sql = @Sql + '		SELECT Track.Id, Track.ParentId, Track.ProjectId,
	   ISNULL( dbo.Func_GetDeptIdOfTaskItem(@DepartmentIds, @UserOfDepartmentId, Track.Id ) , track.departmentid)';
	SET @Sql = @Sql + '		FROM Task.TaskItem Track ';
	SET @Sql = @Sql + '			INNER JOIN Task.Project D ON D.Id = Track.ProjectId '
	SET @Sql = @Sql + '		WHERE D.IsActive = 1 ';

	-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	IF (@DepartmentIds IS NOT NULL AND @DepartmentIds != '' AND @IsAssignBy = 0 )-- AND @TrackingPlanningTypeSelected != 'All')
	BEGIN
		SET @Sql = @Sql + '	AND
		(
		 Track.DepartmentId IN ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'') )
		 OR
		 EXISTS(SELECT Top 1 TiA.Id FROM Task.TaskItemAssign TiA WHERE
		  TiA.TaskItemId = Track.Id AND
		  TiA.DepartmentId IN  ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'')) 
		  AND Tia.TaskType != 7 )
		 )
		  ';		
	END
	
	IF (@TrackingPlanningTypeSelected like '%Weirdo%')
	BEGIN
		SET @TrackingPlanningTypeSelected = 'All'
		SET @Sql = @Sql + '		AND Track.IsWeirdo = 1 ';
	END

	--lọc theo loại công việc
	IF (@CategorizeTrackingDocumentId != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemCategoryId = @CategorizeTrackingDocumentId ';
	END

	-- lọc theo từ ngày đến ngày
	IF (@FromDate IS NOT NULL AND @ToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '	AND ( ';
		SET @Sql = @Sql + ' (Track.FromDate BETWEEN @FromDate AND @ToDate)  ';
		--SET @Sql = @Sql + ' OR (Track.ToDate BETWEEN @FromDate AND @ToDate)  ';
		--SET @Sql = @Sql + ' OR (Track.FromDate <= @FromDate AND @ToDate <= Track.ToDate) ';
		SET @Sql = @Sql + ' ) '; 

	END 

	-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	IF (@ReportTrackingDepartmentStatus = 'InDueDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
	END
	ELSE
	-- lọc theo công việc quá hạn
	IF (@ReportTrackingDepartmentStatus = 'OutOfDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 '		
	END 

	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	IF (@UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER))
	BEGIN
		-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy = 1 lấy theo đk này 
		-- lọc dữ liệu theo người giao việc
		IF (@IsAssignBy = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.AssignBy = @UserOfDepartmentId ';
		END
		ELSE
		IF (@IsAssignTo = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.Id in (SELECT TaskItemId FROM Task.TaskItemAssign WHERE AssignTo = @UserOfDepartmentId )'; 
		END
	END

	-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	IF (@IsReport = 1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.IsReport = 1 '; 
	END
	
	-- lọc theo tình trạng công việc
	IF (@TrackingStatus != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemStatusId = @TrackingStatus ';		
	END

	-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	IF (@TrackingCrucial != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemPriorityId = '+@TrackingCrucial ;		
	END

	SET @ParamDefines = ' @DepartmentIds NVARCHAR(MAX), @FromDate DATETIME, @ToDate	DATETIME, @UserOfDepartmentId	UNIQUEIDENTIFIER, '
		+ ' @IsAssignTo BIT, @IsAssignBy BIT, @IsReport BIT, @TrackingStatus INT, @TrackingCrucial INT, @TrackingPlanningTypeSelected NVARCHAR(500),@CategorizeTrackingDocumentId INT  ';
	Print @sql;
	EXEC sys.sp_executesql @Sql, @ParamDefines,
		@DepartmentIds = @DepartmentIds, @FromDate = @FromDate, @ToDate = @ToDate,
		@UserOfDepartmentId = @UserOfDepartmentId, @IsAssignTo = @IsAssignTo, @IsAssignBy = @IsAssignBy,
		@IsReport = @IsReport, @TrackingStatus = @TrackingStatus, @TrackingCrucial = @TrackingCrucial,
		@TrackingPlanningTypeSelected = @TrackingPlanningTypeSelected,@CategorizeTrackingDocumentId = @CategorizeTrackingDocumentId;
	 --select * from #ReportTrack
	-- detail
	IF (@ReportTrackingDepartmentType = 'Detail')
	BEGIN
		

		IF (@IsAssignTo = 1 AND @UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != TRY_CAST(0x0 AS UNIQUEIDENTIFIER) )
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignToId,
				  dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 1)	PrimaryAssignTo, 
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 3)	SupportAssignTo,
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 7)	ReadOnlyAssignTo,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemAssignId = TA.Id ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
				, cast(track.Weight as NVARCHAR(4)) [Weight]
				,Track.DepartmentId
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id AND TA.AssignTo=@UserOfDepartmentId
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			--	INNER JOIN @Depts Dep ON Track.DepartmentId = Dep.DeptId
			ORDER BY
				Track.CreatedDate;
		END
		ELSE
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate, 
				 CONVERT(varchar, Track.FinishedDate, 103)      FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND TS.Id = 4) 
					THEN 0
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 --Track.AssignBy									AssignById,
				 dbo.Func_GetFullNameByUserId(track.AssignBy) AssignBy,
				 --TA.AssignTo	
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 1)	PrimaryAssignTo, 
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 3)	SupportAssignTo,
				 dbo.[Func_GetTrackAssignToFullNameByType](Track.ID, 7)	ReadOnlyAssignTo,
				 Track.Conclusion,
				 TRY_CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemId = Track.ID ORDER BY TWF.CreatedDate DESC )
																LastResult,
				Track.Conclusion
				, cast(track.Weight as NVARCHAR(4)) [Weight]
				,Track.DepartmentId
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				--INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
				--INNER JOIN @Depts Dep ON Track.DepartmentId = Dep.DeptId
			ORDER BY
				Track.CreatedDate;
		END
	END
	ELSE
	BEGIN
		-- count total

		--DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT); 
		DECLARE @GenralTbls TABLE (DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER,		
								   TotalTask INT, TotalFinishedTask INT, TotalNotFinishedTask INT, TotalOverDays INT,
								   DeptLevel INT, PathLevel nvarchar(100),
								   AssignTo UNIQUEIDENTIFIER); 
		DECLARE @SelectedDeptId UNIQUEIDENTIFIER = NULL;
		DECLARE @CurrentDate DATETIME = GETDATE(), @IdxLv INT = 1, @MaxLv INT = -1;
		 
		DELETE @GenralTbls;

		SELECT @SelectedDeptId = DeptId FROM @Depts WHERE Idx = 1;
		-- Phốt đầy
		--
		;WITH #TbOrgTemp 
		AS
		(
			SELECT D.ID DeptId, 
				CAST( D.Name  as nvarchar(100) )AS Name,
					D.Code, 
					CAST(D.Code as NVARCHAR(1000)) as PathLevel,
					cast(' ' as NVARCHAR(10)) as dashpath
					,1 AS DeptLevel
			FROM 
				Departments AS D    		
			WHERE
				D.IsActive = 1 AND
				(D.ParentID IS NULL OR D.ParentID = CAST(0x0 AS UNIQUEIDENTIFIER) )
			UNION ALL 			
			SELECT 
				D.ID DeptId, 
				CAST ( dp.dashpath + ' ' + D.Name  as nvarchar(100)) Name,
					D.Code, 
					CAST(DP.PathLevel + '\' + D.Code as NVARCHAR(1000)) as PathLevel,
					cast(dp.dashpath + '     ' as NVARCHAR(10)) as dashpath
					,dp.DeptLevel + 1 AS DeptLevel
			FROM 
				Departments AS D
				INNER JOIN #TbOrgTemp AS DP ON DP.DeptId = D.ParentId AND D.IsActive = 1	    
		) 
		
		UPDATE @Depts
		SET DeptLevel = DL.DeptLevel,  PathLevel  = DL.PathLevel
		FROM @Depts D 
			INNER JOIN #TbOrgTemp DL ON DL.DeptId = D.DeptId ;
	 

		INSERT INTO @GenralTbls
			SELECT 
				RT.DeptId DepartmentId,
				D.ParentId, 
				COUNT(Track.ID) AS TotalTask,
				SUM(CASE WHEN  Track.TaskItemStatusId = 4  THEN 1 ELSE 0 END ) AS TotalFinishedTask,
				SUM(CASE WHEN  Track.TaskItemStatusId != 4  THEN 1 ELSE 0 END ) AS TotalNotFinishedTask,
				SUM(CASE 
					WHEN Track.ToDate IS NULL THEN 0
					WHEN CAST(Track.ToDate AS DATE ) < CAST(@CurrentDate AS DATE) THEN DATEDIFF(DAY, Track.ToDate, @CurrentDate)
					ELSE 0
					 END) TotalOverDays,
				D.DeptLevel
				,D.PathLevel
				,TrackAssign.AssignTo  --CAST(0x0 as UNIQUEIDENTIFIER)
			FROM
				--@Depts D
				--LEFT JOIN Task.TaskItem Track  ON Track.DepartmentId = D.DeptId
				--LEFT JOIN #ReportTrack RT ON RT.Id = Track.Id
				#ReportTrack RT
				INNER JOIN Task.TaskItem Track ON Track.Id = RT.Id
				INNER JOIN Task.TaskItemAssign TrackAssign ON TrackAssign.TaskItemId = Track.Id
				LEFT JOIN @Depts D ON D.DeptId = RT.DeptId 
			GROUP BY
				RT.DeptId, TrackAssign.AssignTo,
				 D.ParentId, D.DeptLevel, d.PathLevel;	  

		SELECT
			@MaxLv = MAX(DISTINCT DeptLevel)
		FROM @GenralTbls;
		--select @MaxLv, @IdxLv
		-- for each row to sum up
		WHILE (@IdxLv <= @MaxLv)
		BEGIN 
			-- nếu tồn tại task phòng ban đang đứng
			-- thêm 1 phòng ban của nó để đánh dấu count ngược lên
			INSERT INTO @GenralTbls
				SELECT
					
					G.DeptId,
					G.ParentId, 
					0 AS TotalTask,
					0 AS TotalFinishedTask,
					0 AS TotalNotFinishedTask,
					0 TotalOverDays,
					G.DeptLevel,
					G.PathLevel,
					'11111111-1111-1111-1111-111111111111'
				FROM 
					@GenralTbls G
				WHERE 
					G.DeptLevel = @MaxLv AND G.TotalTask > 0;
			 
			UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + 
				ISNULL((SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0),
				TotalFinishedTask =  TotalFinishedTask +
				ISNULL((SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalNotFinishedTask =  TotalNotFinishedTask +
				ISNULL((SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalOverDays = TotalOverDays +
				ISNULL((SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0)
			WHERE 
				DeptLevel = @MaxLv AND TotalTask = 0;
				 
			SET @MaxLv = @MaxLv - 1;
		END

		--UNION ALL
		INSERT INTO @GenralTbls
			SELECT 
				dt.DeptId,
				dt.ParentId, 
				0,0,0,0,  
					dt.DeptLevel,
					dt.PathLevel,
					'11111111-1111-1111-1111-111111111111'
			FROM 
				@Depts dt
				inner join Departments d on d.ID = dt.DeptId
			WHERE 
				dt.DeptId NOT IN 
				(SELECT G.DeptId  FROM @GenralTbls  G)

		UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + 
				ISNULL((SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0),
				TotalFinishedTask =  TotalFinishedTask +
				ISNULL((SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalNotFinishedTask =  TotalNotFinishedTask +
				ISNULL((SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId), 0),
				TotalOverDays = TotalOverDays +
				ISNULL((SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId ), 0)
			WHERE 
				DeptLevel = 1 AND TotalTask = 0;

		SELECT *
		FROM 
		(
			SELECT 
				DeptId						AS DepartmentId,
				G.ParentId					AS DepartmentParentId,
				DeptLevel					AS DepartmentLevel,
				SUM(TotalTask)				AS TrackingCount,
				SUM(TotalFinishedTask)		AS TrackingFinishedCount,
				SUM(TotalNotFinishedTask)	AS TrackingNotFinishedCount,
				SUM(TotalOverDays)			AS TrackingTotalOverDays,
				CASE
					WHEN G.AssignTo = '11111111-1111-1111-1111-111111111111' THEN '00000000-0000-0000-0000-000000000000'
					ELSE G.AssignTo
				END AssignTo,
				--CAST(0x0 AS UNIQUEIDENTIFIER) AS AssignTo,
				'0'							AS TrackingStatus
				,D.Name
				,G.PathLevel
				--, dbo.Func_GetLoginNameByID(G.AssignTo)
			FROM @GenralTbls G
				INNER JOIN Departments D ON D.ID =  DeptId
			--WHERE
			--	 G.DeptLevel <= 3 
			GROUP BY
				DeptId, G.ParentId, AssignTo, DeptLevel, G.PathLevel --,  
				,D.Name
			--UNION ALL
			--SELECT 
			--	dt.DeptId,
			--	dt.ParentId,
			--	dt.DeptLevel,
			--	0,0,0,0,
			--	cast(0x0 as UNIQUEIDENTIFIER), '0',
			--	d.Name,
			--	dt.PathLevel
			--FROM 
			--	@Depts dt
			--	inner join Departments d on d.ID = dt.DeptId
			--WHERE 
			--	dt.DeptId NOT IN 
			--	(SELECT G.DeptId  FROM @GenralTbls G)
			
		) TB
		ORDER BY TB.PathLevel
	END
	--SELECT *
	--FROM @Depts
	DROP TABLE #ReportTrack;
END



GO
/****** Object:  StoredProcedure [dbo].[SP_Report_TaskItemGernalDetail]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_TaskItemGernalDetail]
--DECLARE 	
	@DepartmentIds						NVARCHAR(MAX) = NULL,		-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	@ParentDepartmentIds				NVARCHAR(MAX) = NULL,		-- lọc theo cha phòng ban chỉ định, danh sách cha phòng ban, cách nhau bằng dấu ';'
	@FromDate							DATETIME = NULL,			-- lọc theo từ ngày
	@ToDate								DATETIME = NULL,			-- lọc theo đến ngày
	@ReportTrackingDepartmentType		NVARCHAR(20) = 'Detail',	-- lọc theo chi tiết General/Detail - chọn 1
	@ReportTrackingDepartmentStatus		NVARCHAR(20) = 'All',		-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	@ReportTrackingDocumentScope		NVARCHAR(20) = 'All',		-- lọc theo phạm vi của công việc Document.isjob = 1 and Document.Scope == (int) 
	@UserOfDepartmentId					UNIQUEIDENTIFIER = NULL,	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	@IsAssignTo							BIT = 1,					-- lọc dữ liệu theo người xử lý,
	@IsAssignBy							BIT = 0,					-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy =1 lấy theo đk này 
	@IsReport							BIT = 0,					-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	@TrackingStatus						INT = -1,					-- lọc theo tình trạng công việc
	@TrackingCrucial					INT = -1,					-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	@TrackingPlanningTypeSelected		NVARCHAR(500) = 'All',		-- lọc theo loại công việc kế hoạch, giao ban, định kỳ, phát sinh																				  -- document.extentioninfo -> planningtype, chọn nhiều
	@CategorizeTrackingDocumentId     INT = 0						-- lọc theo loại công việc
AS
BEGIN
	SET NOCOUNT ON;
	-- tạo bảng tạm lưu trữ
	CREATE TABLE #ReportTrack (Id UNIQUEIDENTIFIER NULL, ParentId UNIQUEIDENTIFIER NULL, ProjectId UNIQUEIDENTIFIER NULL);
	DECLARE 
		@Sql				NVARCHAR(MAX) = '',
		@ParamDefines		NVARCHAR(2000) = '';

	-- khởi tạo câu query
	SET @Sql = @Sql + ' INSERT INTO #ReportTrack ';
	SET @Sql = @Sql + '		SELECT Track.Id, Track.ParentId, Track.ProjectId ';
	SET @Sql = @Sql + '		FROM Task.TaskItem Track ';
	SET @Sql = @Sql + '			INNER JOIN Task.Project D ON D.Id = Track.ProjectId '
	SET @Sql = @Sql + '		WHERE D.IsActive = 1 ';

	-- lọc theo phòng ban chỉ định, danh sách phòng ban, cách nhau bằng dấu ';'
	IF (@DepartmentIds IS NOT NULL AND @DepartmentIds != '' AND @IsAssignBy = 0 AND @TrackingPlanningTypeSelected != 'All')
	BEGIN
		SET @Sql = @Sql + '		AND Track.DepartmentId IN ( SELECT items FROM dbo.Func_SplitTextToTable(@DepartmentIds, '';'') ) ';		
	END
	
	IF (@TrackingPlanningTypeSelected like '%Weirdo%')
	BEGIN
		SET @TrackingPlanningTypeSelected = 'All'
		SET @Sql = @Sql + '		AND Track.IsWeirdo = 1 ';
	END
	--lọc theo loại công việc
	IF (@CategorizeTrackingDocumentId IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemCategoryId = @CategorizeTrackingDocumentId ';
	END

	-- lọc theo từ ngày
	IF (@FromDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '		AND Track.FromDate >= @FromDate ';		
	END
	
	-- lọc theo đến ngày
	IF (@ToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + '		AND Track.FromDate <= @ToDate ';		
	END

	-- lọc theo hạn xử lý All/InDueDate/OutOfDate - chọn 1
	IF (@ReportTrackingDepartmentStatus = 'InDueDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
	END
	ELSE
	-- lọc theo công việc quá hạn
	IF (@ReportTrackingDepartmentStatus = 'OutOfDate')
	BEGIN
		SET @Sql = @Sql + '		AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 '		
	END 

	-- lọc theo AssignTo,AssignBy, nếu @UserOfDepartmentId not null lấy theo trường này
	IF (@UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != CAST(0x0 AS UNIQUEIDENTIFIER))
	BEGIN
		-- lọc dữ liệu theo người giao công việc, nếu IsAssignBy = 1 lấy theo đk này 
		-- lọc dữ liệu theo người giao việc
		IF (@IsAssignBy = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.AssignBy = @UserOfDepartmentId ';
		END
		ELSE
		IF (@IsAssignTo = 1)
		BEGIN
			SET @Sql = @Sql + '		AND Track.Id in (SELECT TaskItemId FROM Task.TaskItemAssign WHERE AssignTo = @UserOfDepartmentId )'; 
		END
	END

	-- lọc dữ liệu công việc có yêu cầu báo cáo không 
	IF (@IsReport = 1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.IsReport = 1 '; 
	END
	
	-- lọc theo tình trạng công việc
	IF (@TrackingStatus != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemStatusId = @TrackingStatus ';		
	END

	-- lọc theo track.severity All = -1/Normal = 0/Important = 1/Critical = 3 - chọn 1
	IF (@TrackingCrucial != -1)
	BEGIN
		SET @Sql = @Sql + '		AND Track.TaskItemPriorityId = '+@TrackingCrucial ;		
	END

	SET @ParamDefines = ' @DepartmentIds NVARCHAR(MAX), @FromDate DATETIME, @ToDate	DATETIME, @UserOfDepartmentId	UNIQUEIDENTIFIER, '
		+ ' @IsAssignTo BIT, @IsAssignBy BIT, @IsReport BIT, @TrackingStatus INT, @TrackingCrucial INT, @TrackingPlanningTypeSelected NVARCHAR(500),@CategorizeTrackingDocumentId INT  ';
	Print @sql;
	EXEC sys.sp_executesql @Sql, @ParamDefines,
		@DepartmentIds = @DepartmentIds, @FromDate = @FromDate, @ToDate = @ToDate,
		@UserOfDepartmentId = @UserOfDepartmentId, @IsAssignTo = @IsAssignTo, @IsAssignBy = @IsAssignBy,
		@IsReport = @IsReport, @TrackingStatus = @TrackingStatus, @TrackingCrucial = @TrackingCrucial,
		@TrackingPlanningTypeSelected = @TrackingPlanningTypeSelected,@CategorizeTrackingDocumentId = @CategorizeTrackingDocumentId;

	-- detail
	IF (@ReportTrackingDepartmentType = 'Detail')
	BEGIN
		IF (@IsAssignTo = 1 AND @UserOfDepartmentId IS NOT NULL AND @UserOfDepartmentId != CAST(0x0 AS UNIQUEIDENTIFIER) )
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignTo,
				 Track.Conclusion,
				 CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemAssignId = TA.Id ORDER BY TWF.CreatedDate DESC )
																Solution,
				Track.Conclusion
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id AND TA.AssignTo=@UserOfDepartmentId
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			ORDER BY
				Track.CreatedDate;
		END
		ELSE
		BEGIN
			SELECT 
				 ROW_NUMBER() OVER(ORDER BY Track.CreatedDate)		NumberOf,
				 D.SerialNumber									Number,
				 D.Summary										[Name],
				 Track.TaskName								    RootTrackingName,
				 CONVERT(varchar, Track.FromDate, 103)			FromDate,
				 CONVERT(varchar, Track.ToDate, 103)			ToDate,
				 CONVERT(varchar, Track.FinishedDate, 103)		FinishedDate, 
				 CASE
					WHEN (Track.FinishedDate IS NULL AND Track.ToDate < GETDATE())
					THEN  DATEDIFF(DAY, Track.ToDate, GETDATE())
					WHEN (Track.FinishedDate IS NOT NULL AND Track.FinishedDate > Track.ToDate)
					THEN DATEDIFF(DAY, Track.ToDate,  Track.FinishedDate)	
					ELSE 0
				 END											OverDueDaysNumber,
				 TS.Name										TrackingStatus,
				 Track.AssignBy									AssignById,
				 TA.AssignTo									AssignTo,
				 Track.Conclusion,
				 CAST(Track.PercentFinish AS NVARCHAR(4))		PercentFinish,
				(SELECT TOP 1 TWF.ProcessResult FROM Task.TaskItemProcessHistory TWF 
				  WHERE TWF.TaskItemId = Track.ID ORDER BY TWF.CreatedDate DESC )
																Solution,
				Track.Conclusion
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN Task.Project D ON D.Id = Track.ProjectId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId=Track.Id AND TA.ProjectId= D.Id
				LEFT JOIN Task.TaskItemStatus TS ON TS.Id = Track.TaskItemStatusId
			ORDER BY
				Track.CreatedDate;
		END
	END
	ELSE
	BEGIN
		DECLARE @Depts TABLE (Idx INT, DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, DeptLevel INT); 
		DECLARE @GenralTbls TABLE (DeptId UNIQUEIDENTIFIER, ParentId UNIQUEIDENTIFIER, AssignTo UNIQUEIDENTIFIER,
								   TotalTask INT, TotalFinishedTask INT, TotalNotFinishedTask INT, TotalOverDays INT,
								   DeptLevel INT); 
		DECLARE @SelectedDeptId UNIQUEIDENTIFIER = NULL;
		DECLARE @CurrentDate DATETIME = GETDATE(), @IdxLv INT = 1, @MaxLv INT = -1;

		DELETE @Depts;
		DELETE @GenralTbls;
		
		INSERT INTO @Depts
			SELECT tb1.STT, tb1.items, tb2.items, -1
			FROM 
				dbo.Func_SplitTextToTable_WithRowNumber(@DepartmentIds, ';') Tb1
				INNER JOIN dbo.Func_SplitTextToTable_WithRowNumber(@ParentDepartmentIds, ';') Tb2 ON Tb2.STT = Tb1.STT;
		
		SELECT @SelectedDeptId = DeptId FROM @Depts WHERE Idx = 1;

		-- build deptlevel
		WITH #TbDeptLevel
		AS
		(
			SELECT D.DeptId, 1 AS DeptLevel
			FROM @Depts D
			--WHERE D.Idx = 1
			UNION ALL
			SELECT D.DeptId, DL.DeptLevel + 1 AS DeptLevel
			FROM @Depts D
				INNER JOIN #TbDeptLevel DL ON DL.DeptId = D.ParentId			
		)

		UPDATE @Depts
		SET DeptLevel = DL.DeptLevel
		FROM @Depts D 
			INNER JOIN #TbDeptLevel DL ON DL.DeptId = D.DeptId;
		
		INSERT INTO @GenralTbls
			SELECT 
				Track.DepartmentId,
				D.ParentId,
				Cast(0x0 as UNIQUEIDENTIFIER) As AssignTo,
				COUNT(Track.ID) AS TotalTask,
				SUM(CASE WHEN  Track.TaskItemStatusId = 4  THEN 1 ELSE 0 END ) AS TotalFinishedTask,
				SUM(CASE WHEN  Track.TaskItemStatusId != 4  THEN 1 ELSE 0 END ) AS TotalNotFinishedTask,
				SUM(CASE 
					WHEN CAST(Track.ToDate AS DATE ) < CAST(@CurrentDate AS DATE) THEN DATEDIFF(DAY, Track.ToDate, @CurrentDate)
					ELSE 0
					 END) TotalOverDays,
				D.DeptLevel
			FROM
				Task.TaskItem Track
				INNER JOIN #ReportTrack RT ON RT.Id = Track.Id
				INNER JOIN @Depts D ON Track.DepartmentId = D.DeptId
				INNER JOIN Task.TaskItemAssign TA ON TA.TaskItemId= Track.Id 
			GROUP BY
				Track.DepartmentId, D.ParentId, TA.AssignTo, D.DeptLevel;
				
		SELECT
			@MaxLv = MAX(DeptLevel)
		FROM @Depts;
		-- for each row to sum up
		WHILE (@IdxLv <= @MaxLv)
		BEGIN
			INSERT INTO @GenralTbls
				SELECT	
					GT.DeptId, 
					GT.ParentId, 
					Cast(0x0 as UNIQUEIDENTIFIER) As AssignTo,
					SUM(GT.TotalTask) AS TotalTask,
					SUM(GT.TotalFinishedTask) AS TotalFinishedTask,
					SUM(GT.TotalNotFinishedTask) AS TotalNotFinishedTask,
					SUM(GT.TotalOverDays) AS TotalOverDays,
					GT.DeptLevel
				FROM 
					@GenralTbls GT
				WHERE 
					GT.DeptLevel = @MaxLv
				GROUP BY
					GT.DeptId, GT.ParentId, GT.DeptLevel;
			
			UPDATE @GenralTbls
			SET 
				TotalTask = TotalTask + (SELECT SUM(GT.TotalTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalFinishedTask =  TotalFinishedTask +
							(SELECT SUM(GT.TotalFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalNotFinishedTask =  TotalNotFinishedTask +
							(SELECT SUM(GT.TotalNotFinishedTask) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = CAST(0x0 AS UNIQUEIDENTIFIER) ),
				TotalOverDays = TotalOverDays +
							(SELECT SUM(GT.TotalOverDays) FROM @GenralTbls GT WHERE GT.ParentId = [@GenralTbls].DeptId 
							AND GT.AssignTo = CAST(0x0 AS UNIQUEIDENTIFIER) )
			WHERE AssignTo = Cast(0x0 as UNIQUEIDENTIFIER) AND DeptLevel = @MaxLv
			AND DeptId IN 
			(SELECT ParentId FROM @GenralTbls )

			SET @MaxLv = @MaxLv - 1;
		END
		
		SELECT 
			DeptId					AS DepartmentId,
			ParentId				AS DepartmentParentId,
			DeptLevel				AS DepartmentLevel,
			TotalTask				AS TrackingCount,
			TotalFinishedTask		AS TrackingFinishedCount,
			TotalNotFinishedTask	AS TrackingNotFinishedCount,
			CAST(TotalOverDays AS NVARCHAR(10))	AS TrackingStatus,
			AssignTo
		FROM @GenralTbls
		WHERE
			@SelectedDeptId = DeptId OR  
			( ParentId = @SelectedDeptId AND AssignTo = Cast(0x0 as UNIQUEIDENTIFIER)  )
		ORDER BY DeptLevel, DeptId, AssignTo;		 
	END

	DROP TABLE #ReportTrack;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_RollBack_ByAssignBy_TrackingDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_RollBack_ByAssignBy_TrackingDocuments]
--DECLARE
	@AssignBy uniqueidentifier,
	@DocID uniqueidentifier	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @DeleteTrackings TABLE (TrackingID uniqueidentifier);
	
	;with #temp
	AS
	(
		SELECT ID
		FROM TrackingDocuments
		WHERE AssignBy = @AssignBy
			AND DocID = @DocID
		UNION ALL
		SELECT  T.ID
		FROM TrackingDocuments T 
			INNER JOIN #temp TP ON TP.ID = T.ParentID
		WHERE T.DocID = @DocID
	)
		
	INSERT INTO @DeleteTrackings
		SELECT * FROM #temp
	
		
	DELETE TrackingDocuments
	WHERE ID in 
	(SELECT TrackingID FROM @DeleteTrackings)
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Documents_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Search_Documents_MultiFilters]
--DECLARE
	@CurrentDate	DATETIME		= NULL,
	@SearchMode		NVARCHAR(10)	= 'None',	-- None, Unit, DB
	@UnitDeptIDs	NVARCHAR(2000)	= NULL,	
	@UserID			NVARCHAR(36)	= NULL,--'0CDA755B-A4E8-4E97-B56D-77FE5E75A349',
	@PublishDeptID	NVARCHAR(36)	= NULL,
	@EditDeptID		NVARCHAR(36)	= NULL,
	@BookID			NVARCHAR(36)	= NULL,-- '4333B1AD-EA6B-4BF9-8585-75B6F1F2E174',
	@Sender			NVARCHAR(2000)	= NULL,
	@DocTypeID		NVARCHAR(36)	= NULL,
	@Receiver		NVARCHAR(2000)	= NULL,
	@SecretID		NVARCHAR(36)	= NULL,
	@PriorityID		NVARCHAR(36)	= NULL,
	@DocStatus		NVARCHAR(20)	= '',		-- mã tình trạng văn bản
	@FromDocDate	DATETIME		= NULL,--'2017-01-01 00:00',
	@ToDocDate		DATETIME		= NULL,-- '2018-01-01 00:00',
	@FromCreated	DATETIME		= NULL,
	@ToCreated		DATETIME		= NULL,	
	@TrackStatus	NVARCHAR(20)	= '',		--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc	
	@ProcessType	INT				= 0,		--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	@TrackType		NVARCHAR(20)	= '',		--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	@DocType		NVARCHAR(20)	= '0,1',	-- 0: Vb đi, 1: vb đến, 2: cong viec
	@ExternalType	INT				= 0,		-- 0: tat ca, 1: ben ngoai, 2: noi bo
	@UserType		INT				= '0',		-- 0: assignto, 2: assignedby
	@IsSearchFile	BIT				= 0,
	@Category		NVARCHAR(20)	= 'All',
	@KeyWord		NVARCHAR(200)	= '',
	@Page			INT				= 1,
	@PageSize		INT				= 20,
	@OrderBy		NVARCHAR(50)    = 'Created DESC',
	@SingedBy		NVARCHAR(200)	= '' 
AS

BEGIN TRY
	SET NOCOUNT ON;	

	--Khai báo ------------------------------------------------------------------------
	CREATE TABLE #DocTbl (Idx INT, DocID uniqueidentifier ); --, StatusString nvarchar(100), DeptID uniqueidentifier);
	CREATE TABLE #TempSearch (DocumentID uniqueidentifier, OrderRank int);
	DECLARE @Sql NVARCHAR(MAX) = '', @SqlFilter NVARCHAR(MAX) = '', @BaseQuery NVARCHAR(MAX) = '', 
			@SqlTrack NVARCHAR(MAX) = '', @SqlTrans NVARCHAR(MAX) = '',
			@TotalRecord int, @TotalPage int, @FilerTrackType nvarchar(100) = '',
			@FromYear int, @ToYear int, @LikeKeyWord NVARCHAR(200) = '%' + @KeyWord + '%';
	SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus);
	SET @DocStatus = dbo.Func_ReplaceParamFilter(@DocStatus);
	SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType);
	SET @DocType = dbo.Func_ReplaceParamFilter(@DocType);
	IF (@FromDocDate IS NOT NULL)
	BEGIN
		SET @FromYear = YEAR(@FromDocDate);
	END
	ELSE
	IF (@FromCreated IS NOT NULL)
	BEGIN
		SET @FromYear = YEAR(@FromCreated);
	END

	IF (@ToDocDate IS NOT NULL)
	BEGIN
		SET @ToYear = YEAR(@ToDocDate);
	END
	ELSE
	IF (@ToCreated IS NOT NULL)
	BEGIN
		SET @ToYear = YEAR(@ToCreated);
	END
	IF (@FromYear IS NULL)
	BEGIN
		SET @FromYear = YEAR(GETDATE()) - 1;
	END
	IF (@ToYear IS NULL)
	BEGIN
		SET @ToYear = YEAR(GETDATE());
	END
	--Hết khai báo ------------------------------------------------------------------------

	--Phần query theo DB ====================================================================================================================================	
	SET @BaseQuery = '
	FROM Documents D
	 ';
	-- Base Query Tracking ==================================================================================================================================
	IF (@SearchMode != 'DB')
	BEGIN
		IF (@UserID IS NULL) 
			SET @UserID = '';
		SET @SqlTrack = ' 
			SELECT 
				DISTINCT Track.DocID AS DocumentID
			FROM
				TrackingDocuments Track
			WHERE
				Year(Track.Created) BETWEEN ' + CAST(@FromYear AS NVARCHAR(4)) + ' AND ' + CAST(@ToYear AS NVARCHAR(4));
		SET @SqlTrans = '
			SELECT 
				DISTINCT Trans.DocumentID
			FROM 
				TransferDocuments Trans
			WHERE 
				Year(Trans.TransferTime) BETWEEN ' + CAST(@FromYear AS NVARCHAR(4)) + ' AND ' + CAST(@ToYear AS NVARCHAR(4));		
		-- Conditions @TrackType ==============================================================================================================================
		-- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
		IF (@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')
		BEGIN
			SET @FilerTrackType = '';
			IF (CHARINDEX('1',@TrackType) > 0 ) -- XLC
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
			END

			IF (CHARINDEX('2',@TrackType) > 0 ) -- PH
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '3,';	
			END

			IF (CHARINDEX('3',@TrackType) > 0 ) -- ĐB
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '7,';
			END
			SET @FilerTrackType = @FilerTrackType + '9'
			SET @SqlTrack = @SqlTrack + ' AND Track.Type IN (' + @FilerTrackType + ') ';
			SET @SqlTrans = '';
		END		
		-- End Conditions @TrackType ==========================================================================================================================
				 
		-- End Conditions @ProcessType ===========================================================================================================================
		-- Conditions @UserType ==================================================================================================================================
		-- 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
		IF (@ProcessType = -1) -- dung han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType = -2) -- tre han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType > 0) -- den han x ngay
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, dateadd(DAY, 2, getdate())) = 1 ';
			SET @SqlTrans = '';
		END
		-- End Conditions @ProcessType ==================================================================================================================================		
		-- Conditions @TrackStatus ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
		IF (@TrackStatus != '' AND @TrackStatus != '-1')
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.Status IN (' + @TrackStatus + ') ';	
			IF (@TrackStatus = '0')
			BEGIN
				SET @SqlTrans = @SqlTrans + ' AND Trans.Status = 0 ';
			END	
			ELSE
			BEGIN
				SET @SqlTrans = '';
			END	
		END
		-- End Conditions @TrackStatus ==========================================================================================================================
	
		IF (@SearchMode = 'None') -- tìm kiếm mặc định theo người dùng
		BEGIN
			-- Conditions @UserType =================================================================================================================================
			-- 0: assignto, 2: assignedby
			IF (@UserType = 0 OR @UserType IS NULL)
			BEGIN
				SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo = ''' + @UserID + ''' ';
				SET @SqlTrans = @SqlTrans + ' AND Trans.Receiver = ''' + @UserID + ''' ';
			END
			ELSE
			IF (@UserType = 2)
			BEGIN
				SET @SqlTrack = @SqlTrack + ' AND Track.AssignBy = ''' + @UserID + ''' ';
				SET @SqlTrans = '';
			END
		END
		ELSE
		IF (@SearchMode = 'Unit') -- tìm kiếm theo phòng ban người dùng
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.DeptID IN (SELECT item FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @UnitDeptIDs + ''', '';'')) ';
			SET @SqlTrans = @SqlTrans + ' AND Trans.DeptID IN (SELECT item FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @UnitDeptIDs + ''', '';'')) ';
		END

		IF (@SqlTrack != '' OR @SqlTrans != '')
		BEGIN
			SET @BaseQuery = @BaseQuery + ' INNER JOIN ( 
				SELECT DISTINCT  T1.DocumentID FROM ( 
				( ' + @SqlTrack + ' ) ';
			IF (@SqlTrans != '')
			BEGIN
				SET @BaseQuery = @BaseQuery + ' 
				UNION ALL 
				( ' + @SqlTrans + ') ';
			END
			SET @BaseQuery = @BaseQuery + ' ) 
			T1 ) 
			TB ON TB.DocumentID = D.ID ';
		END
	END

	--Build SearchQuery -------------------------------------------------------------------------------------------------------
	IF (@KeyWord <> '' AND @KeyWord IS NOT NULL)
	BEGIN			
		--DECLARE @TempKeyWord TABLE (Idx int, Keyword nvarchar(2000), OrderRank int);
		--DECLARE @Idx int = 1, @Max int = 0, @OrderRank nvarchar(2);			
		--INSERT INTO @TempKeyWord
		--	SELECT *  FROM dbo.[Func_BuildKeywordTable](@KeyWord);			
		--SELECT @Max = MAX(Idx) FROM @TempKeyWord;
		
		IF (@Category = 'All')
		BEGIN
			INSERT INTO #TempSearch
				EXEC [SP_FullTextSearchDocuments] @KeyWord
												 ,@Category
												 ,@FromYear
												 ,@ToYear;
		END			
		ELSE
		IF (@Category = 'DocNumber')
		BEGIN
			INSERT INTO #TempSearch
					SELECT D.ID, -1
					FROM Documents D
					WHERE YEAR(D.Created) BETWEEN @FromYear AND @ToYear
					AND D.DocNumber like @LikeKeyWord
		END
		ELSE
		BEGIN
			INSERT INTO #TempSearch
				EXEC [SP_FullTextSearchDocuments] @KeyWord
												 ,@Category
												 ,@FromYear
												 ,@ToYear;
		END
		SET @BaseQuery = '
		INSERT INTO #DocTbl 
			SELECT ROW_NUMBER() OVER(ORDER BY TS.OrderRank ASC, D.' + @OrderBy + '), D.ID ' + @BaseQuery;

		SET @BaseQuery = @BaseQuery + ' 
		INNER JOIN #TempSearch TS ON TS.DocumentID = D.ID '; 
	END
	ELSE
	BEGIN
		SET @BaseQuery = '
		INSERT INTO #DocTbl 
		SELECT ROW_NUMBER() OVER(ORDER BY D.' + @OrderBy + '), D.ID ' + @BaseQuery;
	END
	--Hết Build SearchQuery ---------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------------------------------------------------
	SET @BaseQuery = @BaseQuery + '	
	WHERE D.IsActive = 1 '; 

	--Filter @DocType ========================================================================================================================
	-- 0: di, 1: den, 2: cong viec
	SET @SqlFilter = ' ';
	IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0  AND CHARINDEX('2',@DocType) > 0 ) -- tat ca
	BEGIN
		SET @SqlFilter = ' ';
	END
	ELSE
	BEGIN
		IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0 ) -- den,di
		BEGIN
			SET @BaseQuery = @BaseQuery + '';
		END	 
		ELSE
		IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- di,congviec
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND (D.IsInComing = 0 )';-- OR D.IsJob = 1 ) ';
		END
		ELSE
		IF (CHARINDEX('1',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- den,congviec
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND (D.IsInComing = 1 )';--OR D.IsJob = 1 ) ';
		END
		ELSE
		IF (CHARINDEX('0',@DocType) > 0) -- di
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND (D.IsInComing = 0)';-- AND D.IsJob = 0)  ';
		END
		ELSE
		IF (CHARINDEX('1',@DocType) > 0) -- den
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND (D.IsInComing = 1)';-- AND D.IsJob = 0)  ';
		END
		ELSE
		IF (CHARINDEX('2',@DocType) > 0) -- cong viec
		BEGIN
			SET @BaseQuery = @BaseQuery + '';
		END
	END
	--Filter @ExternalType ========================================================================================================================
	-- 0: tat ca, 1: ben ngoai, 2: noi bo
	IF (@ExternalType != 0)
	BEGIN
		IF (@ExternalType = 1 ) -- tat ca
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND D.IsExternal = 1 ';
		END
		ELSE
		IF (@ExternalType = 2 ) -- tat ca
		BEGIN
			SET @BaseQuery = @BaseQuery + ' 
			AND D.IsExternal = 0 ';
		END
	END
	--Filter @DocStatus ========================================================================================================================	
	IF (@DocStatus != '' AND @DocStatus IS NOT NULL AND @DocStatus != '32' AND @DocStatus != '33' AND @DocStatus != '34'   )
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.Status IN (' + @DocStatus + ') ';
	END
	----Filter by @PublishDeptID --------------------------------------
	--IF (@PublishDeptID != '' AND @PublishDeptID IS NOT NULL)
	--BEGIN
	--	SET @BaseQuery = @BaseQuery + ' 
	--	AND D.EditDepartment = ''' + @PublishDeptID + ''' ';		
	--END
	----Filter by @EditDeptID --------------------------------------
	--IF (@EditDeptID != '' AND @EditDeptID IS NOT NULL)
	--BEGIN
	--	SET @BaseQuery = @BaseQuery + ' 
	--	AND D.EditDepartment = ''' + @EditDeptID + ''' ';		
	--END
	--Filter by @BookID --------------------------------------
	IF (@BookID != '' AND @BookID IS NOT NULL)
	BEGIN
		
		SET @BaseQuery = @BaseQuery + ' 
		AND D.DocBook = ''' + @BookID + ''' ';		
	END
	--Filter by @Sender --------------------------------------
	IF (@Sender != '' AND @Sender IS NOT NULL)
	BEGIN
		DECLARE @TempSender TABLE (Idx int, Keyword nvarchar(200));
		DELETE @TempSender;
		DECLARE @Idx int = 1, @Max int = 0, @KW NVARCHAR(200) = '';
		INSERT INTO @TempSender
			SELECT
				st.STT
			   ,st.items
			FROM dbo.Func_SplitTextToTable_WithRowNumber(@Sender, ',') st;
		SELECT @Max = MAX(idx) FROM @TempSender;
		SET @BaseQuery = @BaseQuery + ' AND ( ';
		WHILE (@Idx <= @Max)
		BEGIN
			SELECT @KW = REPLACE(Keyword,'_', ',' ) FROM @TempSender WHERE @Idx= Idx;

			SET @BaseQuery = @BaseQuery + ' 
			CONTAINS(D.Sender, N''"*' + @KW + '*"'') ';
			SET @Idx = @Idx + 1;
			IF (@Idx <= @Max)
			BEGIN
				SET @BaseQuery = @BaseQuery + ' OR ';
			END
		END
		SET @BaseQuery = @BaseQuery + ' ) ';
	END
	--Filter by @Receiver --------------------------------------
	IF (@Receiver != '' AND @Receiver IS NOT NULL)
	BEGIN
		DECLARE @TempReceiver TABLE (Idx int, Keyword nvarchar(200));
		DELETE @TempReceiver;
		SET @Idx = 1;
	--	DECLARE @Idx int = 1, @Max int = 0, @KW NVARCHAR(200) = '';
		INSERT INTO @TempReceiver
			SELECT
				st.STT
			   ,st.items
			FROM dbo.Func_SplitTextToTable_WithRowNumber(@Receiver, ',') st;
		SELECT @Max = MAX(idx) FROM @TempReceiver;
		SET @BaseQuery = @BaseQuery + ' AND ( ';
		WHILE (@Idx <= @Max)
		BEGIN
			SELECT @KW = REPLACE(Keyword,'_', ',' ) FROM @TempReceiver WHERE @Idx= Idx;
			SET @BaseQuery = @BaseQuery + ' 
			CONTAINS(D.Receiver, N''"*' + @KW + '*"'') ';
			SET @Idx = @Idx + 1;
			IF (@Idx <= @Max)
			BEGIN
				SET @BaseQuery = @BaseQuery + ' OR ';
			END
		END
		SET @BaseQuery = @BaseQuery + ' ) ';		
	END
	--Filter by @DocTypeID --------------------------------------
	IF (@DocTypeID != '' AND @DocTypeID IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.DocType = ''' + @DocTypeID + ''' ';		
	END
	----Filter by @Receiver --------------------------------------
	--IF (@Receiver != '' AND @Receiver IS NOT NULL)
	--BEGIN
	--	SET @BaseQuery = @BaseQuery + ' 
	--	AND CONTAINS(D.Receiver, N''"*' + @Receiver + '*"'') ';		
	--END
	--Filter by @FromDocDate/@ToDocDate --------------------------------------
	IF (@FromDocDate != '' AND @FromDocDate IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.DocDate BETWEEN ''' + CONVERT(NVARCHAR(20), @FromDocDate, 120) + ''' AND ''' + CONVERT(NVARCHAR(20), @ToDocDate, 120) + ''' ';		
	END
	--Filter by @FromCreated/@ToCreated --------------------------------------
	IF (@FromCreated != '' AND @ToCreated IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.Created BETWEEN ''' + CONVERT(NVARCHAR(20), @FromCreated, 120) + ''' AND ''' + CONVERT(NVARCHAR(20), @ToCreated, 120) + ''' ';		
	END
	--Filter by @SecretID --------------------------------------
	IF (@SecretID != '' AND @SecretID IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.Secret = ''' + @SecretID + ''' ';		
	END
	--Filter by @PriorityID --------------------------------------
	IF (@PriorityID != '' AND @PriorityID IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.Priority = ''' + @PriorityID + ''' ';		
	END
	--Filter by search File

	--Filter by Signed By
	IF (@SingedBy != '' AND @SingedBy IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.SignedBy = ''' + @SingedBy + ''' ';		
	END

	PRINT @BaseQuery;
	EXEC sys.sp_executesql @BaseQuery;
	
	 -- lấy số trang
	SET @TotalRecord = (SELECT MAX(Idx) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	

	-- lấy dữ liệu phân trang	
	SELECT
		DT.DocID			AS DocumentID,
		N''					AS TinhTrang,
		D.EditDepartment	AS DeptID,
		@TotalPage			AS TotalPage,
		@TotalRecord		AS TotalRecord,
		D.Sender,
		D.Receiver
		--DT.Idx,
		--D.Summary,
		--D.DocNumber,
		--D.SerialNumber
		
	FROM 
		#DocTbl DT
		INNER JOIN Documents D ON D.ID = DT.DocID
	WHERE 
		DT.Idx 
			BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
	ORDER BY 
		DT.Idx;

	
	DROP TABLE #tempSearch;
	DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION

  DROP TABLE #tempSearch;
	DROP TABLE #DocTbl;
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 
	
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Projects_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 CREATE   PROCEDURE [dbo].[SP_Search_Projects_MultiFilters]
--DECLARE
	@CurrentDate	DATETIME		= NULL,		-- getdate(), --NULL,
	@SearchMode		NVARCHAR(10)	= 'None',	-- None, Unit, DB
	@AssignTo		NVARCHAR(2000)	= NULL,
	@AssignToDept	NVARCHAR(2000)	= NULL,
	@AssignBy		NVARCHAR(2000)	= NULL,
	@AssignByDept	NVARCHAR(2000)	= NULL,
	@FromDate		DATETIME		= NULL,		--'2017-01-01 00:00',
	@ToDate			DATETIME		= NULL,		-- '2018-01-01 00:00',	 
	@TrackStatus	NVARCHAR(20)	= '',		--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc	
	@ProcessType	INT				= 0,		--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han 
	@TrackType		NVARCHAR(20)	= '',		--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	@JobType		NVARCHAR(200)	= '',		-- 0: Vb đi, 1: vb đến, 2: cong viec
	@Category		NVARCHAR(20)	= 'All',
	@KeyWord		NVARCHAR(200)	= '',
	@Page			INT				= 1,
	@PageSize		INT				= 20,
	@OrderBy		NVARCHAR(50)    = 'CreatedDate DESC',
	@SearchScope	NVARCHAR(50)	= 'All' -- All|Project|Task
AS
--SELECT 
--	 @FromDate = '2019-01-01'
--	 ,@ToDate = '2019-12-01'
--	 ,@SearchMode ='None' 
--	 ,@TrackType = '1,2,3'
--	 ,@AssignToDept = ''
--	 ,@AssignTo= ''
--	 ,@KeyWord = N''
--	 ,@SearchScope = 'Task'
--	 ,@JobType = '0'
BEGIN TRY
	SET NOCOUNT ON;		 


	IF (@JobType = 'Personal,Internal' OR @JobType = 'Internal,Personal')
	BEGIN
		SET @JobType = '0,1';
	END


	IF (@JobType = 'Personal')
	BEGIN
		SET @JobType = 0;
	END

	IF (@JobType = 'Internal')
	BEGIN
		SET @JobType = 1;
	END

	IF (@AssignTo != '')
	BEGIN
		SET @AssignTo = REPLACE(@AssignTo, ';', ',');
		--SET @AssignToDept = '';
	END 

	IF (@AssignToDept != '')
	BEGIN
		SET @AssignToDept = REPLACE(@AssignToDept, ';', ',');
		--SET @AssignToDept = '';
	END 
	--Khai báo ------------------------------------------------------------------------
	CREATE TABLE #DocTbl (Idx INT, DocID uniqueidentifier );
	CREATE TABLE #TempSearch (DocumentID uniqueidentifier, OrderRank int);
	DECLARE @Sql NVARCHAR(MAX) = '', @SqlFilter NVARCHAR(MAX) = '', @BaseQuery NVARCHAR(MAX) = '', 
			@SqlTrack NVARCHAR(MAX) = '',  
			@TotalRecord int, @TotalPage int, @FilerTrackType nvarchar(100) = '',
			@FromYear int, @ToYear int, @LikeKeyWord NVARCHAR(200) = '%' + @KeyWord + '%';
	SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus); 
	SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType); 
	IF (@FromDate IS NOT NULL)
	BEGIN
		SET @FromYear = YEAR(@FromDate);
	END 

	IF (@ToDate IS NOT NULL)
	BEGIN
		SET @ToYear = YEAR(@ToDate); 
	END 
	IF (@FromYear IS NULL)
	BEGIN
		SET @FromYear = YEAR(GETDATE()) - 1;
	END
	IF (@ToYear IS NULL)
	BEGIN
		SET @ToYear = YEAR(GETDATE());
	END
	--Hết khai báo ------------------------------------------------------------------------

	--Phần query theo DB ====================================================================================================================================	
	SET @BaseQuery = '
	FROM [Task].[Project] D
	    ';
	-- Base Query Tracking ==================================================================================================================================
	 
	SET @SqlTrack = ' 
		SELECT 
			DISTINCT Track.ProjectId AS DocumentID
		FROM
			[Task].[TaskItemAssign] Track
		LEFT JOIN 
			[Task].[TaskItem] TaskItem on TaskItem.Id = Track.TaskItemId
		WHERE
			Year(Track.FromDate) BETWEEN ' + CAST(@FromYear AS NVARCHAR(4)) + ' AND ' + CAST(@ToYear AS NVARCHAR(4));
		
	-- Conditions @TrackType ==============================================================================================================================
	-- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	IF (@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')
	BEGIN
		SET @FilerTrackType = '';
		IF (CHARINDEX('1',@TrackType) > 0 ) -- XLC
		BEGIN
			SET	@FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
		END

		IF (CHARINDEX('2',@TrackType) > 0 ) -- PH
		BEGIN
			SET	@FilerTrackType = @FilerTrackType + '3,';	
		END

		IF (CHARINDEX('3',@TrackType) > 0 ) -- ĐB
		BEGIN
			SET	@FilerTrackType = @FilerTrackType + '7,';
		END
		SET @FilerTrackType = @FilerTrackType + '9'
		SET @SqlTrack = @SqlTrack + ' AND Track.TaskType IN (' + @FilerTrackType + ') ';
	END		
	-- End Conditions @TrackType
	--==============================
	-- Conditions @ProcessType
	-- 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	IF (@ProcessType = -1) -- dung han
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, getdate()) = 0 ';
	END
	ELSE
	IF (@ProcessType = -2) -- tre han
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, getdate()) = 1 ';
	END
	ELSE
	IF (@ProcessType > 0) -- den han x ngay
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, dateadd(DAY, 2, getdate())) = 1 ';
	END
	-- End Conditions @ProcessType ==================================================================================================================================		
	-- Conditions @TrackStatus ==============================================================================================================================
	-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
	IF (@TrackStatus != '' AND @TrackStatus != '-1')
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND Track.TaskItemStatusId IN (' + @TrackStatus + ') ';	 	
	END
	-- End Conditions @TrackStatus  
	-- 0: assignto, 2: assignedby
	IF (@AssignTo != '' AND @AssignTo IS NOT NULL)
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignTo + ''', '','')) '; 
	END 

	IF (@AssignBy != '' AND @AssignBy IS NOT NULL)
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND TaskItem.AssignBy IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignBy + ''', '','')) ';
	END
			 
	IF (@AssignToDept != '' AND @AssignToDept IS NOT NULL) -- tìm kiếm theo phòng ban người dùng
	BEGIN
		SET @SqlTrack = @SqlTrack + ' AND Track.DepartmentId IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignToDept + ''', '','')) '; 
	END

	
	-- Filter by JobType
	IF (@JobType != '' AND @JobType IS NOT NULL)
	BEGIN
		SET @SqlTrack = @SqlTrack + ' 
		AND EXISTS
		(SELECT Ti.Id FROM Task.TaskItem Ti WHERE Ti.Id = Track.TaskItemId
			AND TaskItemCategoryId in( ' + @JobType + ') ) ';
	END

	IF (@SqlTrack != '')
	BEGIN
		SET @BaseQuery = @BaseQuery + ' INNER JOIN ( 
			SELECT DISTINCT  T1.DocumentID FROM ( 
			( ' + @SqlTrack + ' ) ';
		 
		SET @BaseQuery = @BaseQuery + ' ) 
		T1 ) 
		TB ON TB.DocumentID = D.ID ';
	END 


	--Build SearchQuery -------------------------------------------------------------------------------------------------------
	IF (@KeyWord <> '' AND @KeyWord IS NOT NULL)
	BEGIN			 
		IF (@Category = 'All')
		BEGIN
			INSERT INTO #TempSearch
				EXEC [SP_FullTextSearchProjects] @KeyWord,@Category,@FromYear,@ToYear;

			INSERT INTO #TempSearch
				SELECT DISTINCT tds.ProjectId, 2
				FROM [Task].TaskItem tds
				WHERE tds.TaskName like @LikeKeyWord AND YEAR(CreatedDate) Between @FromYear AND @ToYear;
		END			 
		ELSE
		IF (@Category  = 'Task')
		BEGIN
			INSERT INTO #TempSearch
				SELECT DISTINCT tds.ProjectId, 2
				FROM [Task].TaskItem tds
				WHERE tds.TaskName like @LikeKeyWord AND YEAR(CreatedDate) Between @FromYear AND @ToYear;
		END
		ELSE
		IF (@Category  = 'Project')
		BEGIN
			INSERT INTO #TempSearch
				EXEC [SP_FullTextSearchProjects] @KeyWord,@Category,@FromYear,@ToYear;
		END
		SET @BaseQuery = '
		INSERT INTO #DocTbl 
			SELECT ROW_NUMBER() OVER(ORDER BY TS.OrderRank ASC, D.' + @OrderBy + '), D.ID ' + @BaseQuery;

		SET @BaseQuery = @BaseQuery + ' 
		INNER JOIN #TempSearch TS ON TS.DocumentID = D.ID '; 
	END
	ELSE
	BEGIN
		SET @BaseQuery = '
		INSERT INTO #DocTbl 
		SELECT ROW_NUMBER() OVER(ORDER BY D.' + @OrderBy + '), D.ID ' + @BaseQuery;
	END
	--Hết Build SearchQuery ---------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------------------------------------------------
	SET @BaseQuery = @BaseQuery + '	
	WHERE 
		D.IsActive = 1  '; 

	--Filter @DocType ========================================================================================================================
	-- 0: di, 1: den, 2: cong viec
	SET @SqlFilter = ' ';	   
	--Filter by @FromDate/@ToDate --------------------------------------
	IF (@FromDate != '' AND @FromDate IS NOT NULL)
	BEGIN
		SET @BaseQuery = @BaseQuery + ' 
		AND D.CreatedDate BETWEEN ''' + CONVERT(NVARCHAR(20), @FromDate, 120) + ''' AND ''' + CONVERT(NVARCHAR(20), @ToDate, 120) + ''' ';		
	END 
	-- Filter by JobType
	--IF (@JobType != '' AND @JobType IS NOT NULL)
	--BEGIN
	--	SET @BaseQuery = @BaseQuery + ' 
	--	AND D.PlanningType IN 
	--	(SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @JobType + ''', '','')) ';
	--END
	 
	 
	PRINT @BaseQuery;
	EXEC sys.sp_executesql @BaseQuery;
	
	    -- lấy số trang
	SET @TotalRecord = (SELECT MAX(Idx) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	
	-- lấy dữ liệu phân trang	
	SELECT
		DT.DocID			AS DocumentID,		 
		D.DepartmentId		AS DeptID,
		DT.Idx				As Number,
		D.SerialNumber,
		D.Summary,		
		D.ApprovedBy,
		D.FromDate,
		D.ToDate, 
		dbo.[Func_GetTaskAssignToByDocId](D.ID, 1)	PrimaryAssignTo, 
		dbo.[Func_GetTaskAssignToByDocId](D.ID, 3)	SupportAssignTo,
		dbo.[Func_GetTaskAssignToByDocId](D.ID, 7)	ReadOnlyAssignTo,
		@TotalPage			AS TotalPage,
		@TotalRecord		AS TotalRecord
		,D.ProjectStatusId
		,PS.Name			ProjectStatusName
		,PS.Code			ProjectStatusCode
	FROM 
		#DocTbl DT
		INNER JOIN [Task].[Project] D ON D.ID = DT.DocID
		LEFT JOIN Task.ProjectStatus PS ON PS.Id = D.ProjectStatusId
	WHERE 
		DT.Idx 
			BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
	ORDER BY 
		DT.Idx;

	
	DROP TABLE #tempSearch;
	DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
-- Whoops, there was an error
    IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION

    DROP TABLE #tempSearch;
	DROP TABLE #DocTbl;
-- Raise an error with the details of the exception
    DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

    SELECT @ErrMsg = ERROR_MESSAGE(),
    @ErrSeverity = ERROR_SEVERITY()
 
	
    RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH 

GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Tasks_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
                         CREATE PROCEDURE [dbo].[SP_Search_Tasks_MultiFilters]
                        --DECLARE
	                        @CurrentDate	DATETIME		= NULL,		-- getdate(), --NULL,
	                        @SearchMode		NVARCHAR(10)	= 'None',	-- None, Unit, DB
	                        @AssignTo		NVARCHAR(2000)	= NULL,
	                        @AssignToDept	NVARCHAR(2000)	= NULL,
	                        @AssignBy		NVARCHAR(2000)	= NULL,
	                        @AssignByDept	NVARCHAR(2000)	= NULL,
	                        @FromDate		DATETIME		= NULL,		--'2017-01-01 00:00',
	                        @ToDate			DATETIME		= NULL,		-- '2018-01-01 00:00',	 
	                        @TrackStatus	NVARCHAR(20)	= '',		--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc	
	                        @ProcessType	INT				= 0,		--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han 
	                        @TrackType		NVARCHAR(20)	= '',		--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	                        @JobType		NVARCHAR(200)	= '',		-- 0: Vb đi, 1: vb đến, 2: cong viec
	                        @Category		NVARCHAR(20)	= 'All',
	                        @KeyWord		NVARCHAR(200)	= '',
	                        @Page			INT				= 1,
	                        @PageSize		INT				= 20,
	                        @OrderBy		NVARCHAR(50)    = 'Created DESC'
                        AS
                        --SELECT 
                        --	@FromDate = '2019-01-01'
                        --	,@ToDate = '2019-12-01'
                        --	,@SearchMode ='DB' 

                        BEGIN TRY
	                        SET NOCOUNT ON;		 

	                        --Khai báo ------------------------------------------------------------------------
	                        CREATE TABLE #DocTbl (Idx INT, DocID uniqueidentifier );
	                        CREATE TABLE #TempSearch (DocumentID uniqueidentifier, OrderRank int);
	                        DECLARE @Sql NVARCHAR(MAX) = '', @SqlFilter NVARCHAR(MAX) = '', @BaseQuery NVARCHAR(MAX) = '', 
			                        @SqlTrack NVARCHAR(MAX) = '', @SqlTrans NVARCHAR(MAX) = '',
			                        @TotalRecord int, @TotalPage int, @FilerTrackType nvarchar(100) = '',
			                        @FromYear int, @ToYear int, @LikeKeyWord NVARCHAR(200) = '%' + @KeyWord + '%';
	                        SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus); 
	                        SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType); 
	                        IF (@FromDate IS NOT NULL)
	                        BEGIN
		                        SET @FromYear = YEAR(@FromDate);
	                        END 

	                        IF (@ToDate IS NOT NULL)
	                        BEGIN
		                        SET @ToYear = YEAR(@ToDate); 
	                        END 
	                        IF (@FromYear IS NULL)
	                        BEGIN
		                        SET @FromYear = YEAR(GETDATE()) - 1;
	                        END
	                        IF (@ToYear IS NULL)
	                        BEGIN
		                        SET @ToYear = YEAR(GETDATE());
	                        END
	                        --Hết khai báo ------------------------------------------------------------------------

	                        --Phần query theo DB ====================================================================================================================================	
	                        SET @BaseQuery = '
	                        FROM Documents D
	                         ';
	                        -- Base Query Tracking ==================================================================================================================================
	 
	                        SET @SqlTrack = ' 
		                        SELECT 
			                        DISTINCT Track.DocID AS DocumentID
		                        FROM
			                        TrackingDocuments Track
		                        WHERE
			                        Year(Track.Created) BETWEEN ' + CAST(@FromYear AS NVARCHAR(4)) + ' AND ' + CAST(@ToYear AS NVARCHAR(4));
	                        SET @SqlTrans = '
		                        SELECT 
			                        DISTINCT Trans.DocumentID
		                        FROM 
			                        TransferDocuments Trans
		                        WHERE 
			                        Year(Trans.TransferTime) BETWEEN ' + CAST(@FromYear AS NVARCHAR(4)) + ' AND ' + CAST(@ToYear AS NVARCHAR(4));		
	                        -- Conditions @TrackType ==============================================================================================================================
	                        -- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	                        IF (@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')
	                        BEGIN
		                        SET @FilerTrackType = '';
		                        IF (CHARINDEX('1',@TrackType) > 0 ) -- XLC
		                        BEGIN
			                        SET	@FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
		                        END

		                        IF (CHARINDEX('2',@TrackType) > 0 ) -- PH
		                        BEGIN
			                        SET	@FilerTrackType = @FilerTrackType + '3,';	
		                        END

		                        IF (CHARINDEX('3',@TrackType) > 0 ) -- ĐB
		                        BEGIN
			                        SET	@FilerTrackType = @FilerTrackType + '7,';
		                        END
		                        SET @FilerTrackType = @FilerTrackType + '9'
		                        SET @SqlTrack = @SqlTrack + ' AND Track.Type IN (' + @FilerTrackType + ') ';
		                        SET @SqlTrans = '';
	                        END		
	                        -- End Conditions @TrackType
	                        --==============================
	                        -- Conditions @ProcessType
	                        -- 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	                        IF (@ProcessType = -1) -- dung han
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
		                        SET @SqlTrans = '';
	                        END
	                        ELSE
	                        IF (@ProcessType = -2) -- tre han
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 ';
		                        SET @SqlTrans = '';
	                        END
	                        ELSE
	                        IF (@ProcessType > 0) -- den han x ngay
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, dateadd(DAY, 2, getdate())) = 1 ';
		                        SET @SqlTrans = '';
	                        END
	                        -- End Conditions @ProcessType ==================================================================================================================================		
	                        -- Conditions @TrackStatus ==============================================================================================================================
	                        -- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
	                        IF (@TrackStatus != '' AND @TrackStatus != '-1')
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND Track.Status IN (' + @TrackStatus + ') ';	
		                        IF (@TrackStatus = '0')
		                        BEGIN
			                        SET @SqlTrans = @SqlTrans + ' AND Trans.Status = 0 ';
		                        END	
		                        ELSE
		                        BEGIN
			                        SET @SqlTrans = '';
		                        END	
	                        END
	                        -- End Conditions @TrackStatus  
	                        -- 0: assignto, 2: assignedby
	                        IF (@AssignTo != '' AND @AssignTo IS NOT NULL)
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignTo + ''', '','')) ';
		                        SET @SqlTrans = @SqlTrans + ' AND Trans.Receiver IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignTo + ''', '','')) ';;
	                        END 

	                        IF (@AssignBy != '' AND @AssignBy IS NOT NULL)
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND Track.AssignBy IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignBy + ''', '','')) ';
		                        SET @SqlTrans = '';
	                        END
			 
	                        IF (@AssignToDept != '' AND @AssignToDept IS NOT NULL) -- tìm kiếm theo phòng ban người dùng
	                        BEGIN
		                        SET @SqlTrack = @SqlTrack + ' AND Track.DeptID IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignToDept + ''', '','')) ';
		                        SET @SqlTrans = @SqlTrans + ' AND Trans.DeptID IN (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @AssignToDept + ''', '','')) ';
	                        END

	                        IF (@SqlTrack != '' OR @SqlTrans != '')
	                        BEGIN
		                        SET @BaseQuery = @BaseQuery + ' INNER JOIN ( 
			                        SELECT DISTINCT  T1.DocumentID FROM ( 
			                        ( ' + @SqlTrack + ' ) ';
		                        IF (@SqlTrans != '')
		                        BEGIN
			                        SET @BaseQuery = @BaseQuery + ' 
			                        UNION ALL 
			                        ( ' + @SqlTrans + ') ';
		                        END
		                        SET @BaseQuery = @BaseQuery + ' ) 
		                        T1 ) 
		                        TB ON TB.DocumentID = D.ID ';
	                        END 

	                        --Build SearchQuery -------------------------------------------------------------------------------------------------------
	                        IF (@KeyWord <> '' AND @KeyWord IS NOT NULL)
	                        BEGIN			 
		                        IF (@Category = 'All')
		                        BEGIN
			                        INSERT INTO #TempSearch
				                        EXEC [SP_FullTextSearchDocuments] @KeyWord
												                         ,@Category
												                         ,@FromYear
												                         ,@ToYear;
		                        END			 
		                        ELSE
		                        BEGIN
			                        INSERT INTO #TempSearch
				                        EXEC [SP_FullTextSearchDocuments] @KeyWord
												                         ,@Category
												                         ,@FromYear
												                         ,@ToYear;
		                        END
		                        SET @BaseQuery = '
		                        INSERT INTO #DocTbl 
			                        SELECT ROW_NUMBER() OVER(ORDER BY TS.OrderRank ASC, D.' + @OrderBy + '), D.ID ' + @BaseQuery;

		                        SET @BaseQuery = @BaseQuery + ' 
		                        INNER JOIN #TempSearch TS ON TS.DocumentID = D.ID '; 
	                        END
	                        ELSE
	                        BEGIN
		                        SET @BaseQuery = '
		                        INSERT INTO #DocTbl 
		                        SELECT ROW_NUMBER() OVER(ORDER BY D.' + @OrderBy + '), D.ID ' + @BaseQuery;
	                        END
	                        --Hết Build SearchQuery ---------------------------------------------------------------------------------------
	                        ---------------------------------------------------------------------------------------------------------------------------------------
	                        SET @BaseQuery = @BaseQuery + '	
	                        WHERE 
		                        D.IsActive = 1
		                        AND D.IsJob = 1
	                         '; 

	                        --Filter @DocType ========================================================================================================================
	                        -- 0: di, 1: den, 2: cong viec
	                        SET @SqlFilter = ' ';	   
	                        --Filter by @FromDate/@ToDate --------------------------------------
	                        IF (@FromDate != '' AND @FromDate IS NOT NULL)
	                        BEGIN
		                        SET @BaseQuery = @BaseQuery + ' 
		                        AND D.DocDate BETWEEN ''' + CONVERT(NVARCHAR(20), @FromDate, 120) + ''' AND ''' + CONVERT(NVARCHAR(20), @ToDate, 120) + ''' ';		
	                        END 
	                        -- Filter by JobType
	                        IF (@JobType != '' AND @JobType IS NOT NULL)
	                        BEGIN
		                        SET @BaseQuery = @BaseQuery + ' 
		                        AND D.PlanningType IN 
		                        (SELECT items FROM dbo.Func_SplitTextToTable_WithRowNumber(''' + @JobType + ''', '','')) ';
	                        END
	 
	 
	                        PRINT @BaseQuery;
	                        EXEC sys.sp_executesql @BaseQuery;
	
	                         -- lấy số trang
	                        SET @TotalRecord = (SELECT MAX(Idx) FROM #DocTbl);
	                        SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	
	                        -- lấy dữ liệu phân trang	
	                        SELECT
		                        DT.DocID			AS DocumentID,		 
		                        D.EditDepartment	AS DeptID,
		                        DT.Idx				As Number,
		                        D.SerialNumber,
		                        D.Summary,		
		                        D.ApprovedBy,
		                        D.FromDate,
		                        D.ToDate, 
		                        dbo.Func_GetTrackAssignToByDocId(D.ID, 1)	PrimaryAssignTo, 
		                        dbo.Func_GetTrackAssignToByDocId(D.ID, 3)	SupportAssignTo,
		                        dbo.Func_GetTrackAssignToByDocId(D.ID, 7)	ReadOnlyAssignTo,
		                        @TotalPage			AS TotalPage,
		                        @TotalRecord		AS TotalRecord
	                        FROM 
		                        #DocTbl DT
		                        INNER JOIN Documents D ON D.ID = DT.DocID
	                        WHERE 
		                        DT.Idx 
			                        BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
	                        ORDER BY 
		                        DT.Idx;

	
	                        DROP TABLE #tempSearch;
	                        DROP TABLE #DocTbl;
                        END TRY
                        BEGIN CATCH
                        -- Whoops, there was an error
                         IF @@TRANCOUNT > 0
                         ROLLBACK TRANSACTION

                          DROP TABLE #tempSearch;
	                        DROP TABLE #DocTbl;
                        -- Raise an error with the details of the exception
                         DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

                         SELECT @ErrMsg = ERROR_MESSAGE(),
                         @ErrSeverity = ERROR_SEVERITY()
 
	
                         RAISERROR(@ErrMsg, @ErrSeverity, 1)
                        END CATCH 

GO
/****** Object:  StoredProcedure [dbo].[SP_SearchLinkDocument]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SearchLinkDocument]
	@DocID uniqueidentifier,
	@DeptID uniqueidentifier,
	@KeyWord nvarchar(400)
AS
BEGIN
	SET NOCOUNT ON;

	SET @KeyWord = LTRIM(RTRIM(@KeyWord));

	DECLARE @DBName nvarchar(200) = '', @Sql nvarchar(1000) = '';

	SELECT
		@DBName = DatabaseName
	FROM Departments
	WHERE ID = @DeptID;

	SET @Sql = ' 
	SELECT TOP 1 ''True''
	FROM [' + @DBName + '].dbo.Documents
	WHERE	
		ID = ''' + CAST(@DocID AS NVARCHAR(36)) + '''
		AND (
		CONTAINS(Summary, N''' + @KeyWord + ''') OR
		CONTAINS(SerialNumber,  N''' + @KeyWord + ''') OR
		CONTAINS(Sender,  N''' + @KeyWord + ''') OR
		CAST(DocNumber as nvarchar(20)) =  N''' + @KeyWord + ''')
		)
	';
	EXEC sys.sp_executesql @Sql;
		

    
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_ByServer_ListOnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_ByServer_ListOnlineUsers] 
	@ServerName nvarchar(150)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @LstUserIDs nvarchar(max) = ''

	SELECT
		@LstUserIDs = @LstUserIDs + REPLACE(UserID, '0#.f|pvgas|', 'pvgas\') + ';'
	FROM 
		(SELECT DISTINCT
			UserID
		FROM OnlineUsers
		WHERE ServerName = @ServerName
		) TB

	EXEC SP_GetOrgUser_ByListUser @LstUserIDs

END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_ByViewType_TrackingDocuments]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_ByViewType_TrackingDocuments]
--DECLARE
	@ViewType nvarchar(50) = 'All',
	@Status int = -1,
	@DocID uniqueidentifier = '',
	@UserLoginName nvarchar(200) = '',
	@UserID uniqueidentifier = null,
	@DeptIDs nvarchar(500) = ''
AS
BEGIN
	SET NOCOUNT ON;
	--DECLARE @UserID uniqueidentifier = dbo.Func_GetUserIDByLoginName(@UserLoginName);
	--SET @UserID  = dbo.Func_GetUserIDByLoginName(@UserLoginName);

	IF (@ViewType = 'All')
	BEGIN
		SELECT
			TD.*
		FROM TrackingDocuments TD
		WHERE ( TD.ParentID = '00000000-0000-0000-0000-000000000000' OR TD.ParentID IS NULL )
		AND @DocID = TD.DocID
		AND ( TD.Status = @Status OR @Status = -1)
		ORDER BY TD.Type, TD.Created;
	END
	ELSE
	IF (@ViewType = 'Department')
	BEGIN
		--DECLARE @TableDept TABLE (ParentID UNIQUEIDENTIFIER)
		--DELETE @TableDept;

		--INSERT INTO @TableDept
		--	SELECT
		--		DISTINCT TD.ParentID
		--	FROM TrackingDocuments TD
		--	WHERE @DocID = TD.DocID
		--	AND (TD.Status = @Status
		--	OR @Status = -1)
		--	AND TD.DeptID IN (SELECT
		--			items
		--		FROM dbo.Func_SplitTextToTable(@DeptIDs, ';'));

		
		--SELECT
		--	Tr.*
		--FROM TrackingDocuments Tr
		--WHERE Tr.ID IN (SELECT
		--		TD.ParentID
		--	FROM @TableDept TD

		--	 )
		--and  tr.ParentID not IN
		--	(SELECT
		--		TD.ParentID
		--	FROM @TableDept TD
		--	WHERE TD.ParentID != '00000000-0000-0000-0000-000000000000')
		--ORDER BY Tr.Type, Tr.Created;


		SELECT
			TD.*
		FROM TrackingDocuments TD
		WHERE ( TD.ParentID = '00000000-0000-0000-0000-000000000000' OR TD.ParentID IS NULL )
		AND @DocID = TD.DocID
		AND ( TD.Status = @Status OR @Status = -1)
		AND TD.DeptID IN (SELECT
					items
				FROM dbo.Func_SplitTextToTable(@DeptIDs, ';'))
		ORDER BY TD.Type, TD.Created;

	END
	ELSE
	IF (@ViewType = 'User')
	BEGIN
		SELECT Tr.*
		FROM 
		(
			SELECT
				Tr.ID, Tr.Type, Tr.Created
			FROM TrackingDocuments Tr
			WHERE Tr.ID IN (
				SELECT
					TD.ParentID
				FROM TrackingDocuments TD
				WHERE TD.AssignTo = @UserID 
				AND (TD.ParentID != '00000000-0000-0000-0000-000000000000' AND TD.ParentID IS NOT NULL)
				AND @DocID = TD.DocID
				AND ( TD.Status = @Status OR @Status = -1)
				)
			UNION ALL
			SELECT
				TD.ID, TD.Type, TD.Created
			FROM TrackingDocuments TD
			WHERE TD.AssignTo = @UserID 
				AND (TD.ParentID = '00000000-0000-0000-0000-000000000000' OR TD.ParentID IS  NULL)
				AND @DocID = TD.DocID
				AND ( TD.Status = @Status OR @Status = -1)
		) TB
			INNER JOIN TrackingDocuments Tr ON Tr.ID = TB.ID
		ORDER BY TB.Type, TB.Created;
	END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Documents_Filters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_Select_Documents_Filters]
--DECLARE
	@CurrentDate	DATETIME		= '',
	@CurrentUser	NVARCHAR(200)	= '',
	@TrackStatus	NVARCHAR(20)	= '',				--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
	@DocStatus		NVARCHAR(20)	= '',					-- mã tình trạng văn bản
	@ProcessType	INT				= 0,					--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	@TrackType		NVARCHAR(20)	= '',				--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	@DocType		NVARCHAR(20)	= '1,2',				-- 0: Vb đi, 1: vb đến, 2: cong viec
	@ExternalType	INT				= 0,					-- 0: tat ca, 1: ben ngoai, 2: noi bo
	@UserType		INT				= 0,					--/ 0: assignto, 1: approved, 2: assignedby
	@UserDelegate   NVARCHAR(200)   = NULL,					-- lấy dữ liệu theo người ủy quyền
	@PrivateFolder	NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo folder cá nhân
	@Category		NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo danh mục phân loại
	@TimeFilter		NVARCHAR(200)	= '',					-- filter thời gian theo ngày tạo văn bản 
    @KeyWord		NVARCHAR(200)	= '',
	@Page			INT				= 1,
	@PageSize		INT				= 10,
    @IsCount		BIT				= 0,
	@OrderBy		NVARCHAR(50)    = 'Created DESC'	
AS
--SET @CurrentDate			= ''
--SET	@CurrentUser		= 'haugiang\vanthuudcnc'
--SET	@TrackStatus		= ''				--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
--SET	@DocStatus			= '33'				-- mã tình trạng văn bản
--SET	@ProcessType					= 0					--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
--SET	@TrackType			= ''			--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
--SET	@DocType		= '0'				-- 0: Vb đi, 1: vb đến, 2: cong viec
--SET	@ExternalType					= 0				-- 0: tat ca, 1: ben ngoai, 2: noi bo
--SET	@UserType						= 0					--/ 0: assignto, 1: approved, 2: assignedby
--SET	@UserDelegate     = NULL				-- lấy dữ liệu theo người ủy quyền
--SET	@PrivateFolder		= NULL				-- lấy dữ liệu theo folder cá nhân
--SET	@Category			= NULL					-- lấy dữ liệu theo danh mục phân loại
--SET	@TimeFilter			= ''					-- filter thời gian theo ngày tạo văn bản 
--  SET  @KeyWord			= ''
--SET	@Page							= 1 
--SET    @IsCount						= 0
--SET	@OrderBy		   = 'Created DESC'	
BEGIN TRY
	SET NOCOUNT ON;
	--SELECT
	--		V.*,
	--		(CASE
	--			--WHEN V.IsJob = 1 THEN 'CONGVIEC'
	--			WHEN V.IsInComing = 1 THEN 'VBDEN'
	--			ELSE 'VBDI'
	--		 END) AS loaivanban,
	--		' ' AS TinhTrang,
	--		1 TotalPage,
	--		2 TotalRecord
	--	FROM 
	--		dbo.View_ListDocument V

	DECLARE @CurrentUserID nvarchar(36) = '';
	SET @CurrentUserID = dbo.Func_GetUserIDByLoginName(@CurrentUser);
	SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus);
	SET @DocStatus = dbo.Func_ReplaceParamFilter(@DocStatus);
	SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType);
	SET @DocType = dbo.Func_ReplaceParamFilter(@DocType);
	SET @TimeFilter = dbo.Func_ReplaceParamFilter(@TimeFilter);

	-- kiểm tra, nếu người dùng truyền vào user delegate thì lấy dữ liệu theo user đó
	-- kiểm tra xem user delegate đó còn hiệu lực hay không
	IF (@UserDelegate != '' 
		AND @UserDelegate IS NOT NULL
		AND	EXISTS (SELECT
				TU.UserName
			FROM UserDelegations UD
				INNER JOIN Users FU ON FU.ID = UD.FromUserID
				INNER JOIN Users TU ON TU.ID = UD.ToUserID
			WHERE FU.UserName = @UserDelegate
			AND TU.UserName = @CurrentUser
			AND CAST(@CurrentDate AS DATE) BETWEEN UD.FromDate AND UD.ToDate)
	)
	BEGIN
		SET @CurrentUser = @UserDelegate;
	END


	--Khai báo ------------------------------------------------------------------------
	CREATE TABLE #DocTbl (DocID uniqueidentifier , StatusString nvarchar(100));	
	DECLARE @SqlTrack nvarchar(MAX), @SqlTrans nvarchar(MAX),
			@SqlFilter nvarchar(200),
			@Sql nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int,
			@FilerTrackType nvarchar(100) = '';
	--Hết khai báo ------------------------------------------------------------------------

	--Các trường hợp đặc biệt ==================================================================================================================================================================
	IF (@DocStatus = '12') -- Đang soạn
	BEGIN
		INSERT INTO #DocTbl
			SELECT 
				DOC.ID, '1-TRANSFER'
			FROM 
				Documents DOC
			WHERE 
				DOC.AuthorID = @CurrentUserID AND DOC.Status = 12;
	END
	ELSE
	IF (@DocStatus = '1') -- Đang chờ tôi duyệt
	BEGIN
		
		INSERT INTO #DocTbl
			SELECT 
				DOC.ID, '1-TRANSFER'
			FROM 
				Documents DOC				
			WHERE 
				DOC.Status = 1
				--AND CHARINDEX('<TransferInfo>', DOC.InternalReceiver) > 0
				AND DOC.ApprovedBy = @CurrentUserID 
				--and wft.Code != 'GetDocNumber'
				--and wfs.IsSignCA != 1
	END

	--Hết các trường hợp đặc biệt ==============================================================================================================================================================
	ELSE
	--Phần query theo DB ====================================================================================================================================================================
	BEGIN
		-- Base Query Tracking ==================================================================================================================================
		SET @SqlTrack = ' INSERT INTO #DocTbl
		SELECT 
			DISTINCT Track.DocID, CAST(
				(select top 1 dt2.Status 
				from TrackingDocuments dt2 
				where dt2.DocID = Track.DocID AND dt2.AssignTo = ''' + @CurrentUserID + ''' 
				order by dt2.Status) as varchar) + ''-TRACKING'' 
		FROM
			TrackingDocuments Track
		WHERE
			Track.DocID IS NOT NULL ';

		SET @SqlTrans = ' 	INSERT INTO #DocTbl
			SELECT
				Trans.DocumentID
			   ,CAST(Trans.Status AS NVARCHAR(10)) + ''-TRANSFER''
			FROM TransferDocuments Trans
			WHERE Trans.Receiver = ''' + @CurrentUserID + ''' 
			AND NOT EXISTS (SELECT
					DT1.StatusString
				FROM #DocTbl DT1
				WHERE DT1.DocID = Trans.DocumentID) ';
				
		-- Conditions @UserType =================================================================================================================================
		-- 0: assignto, 1: approved, 2: assignedby
		IF (@UserType = 0)
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo = ''' + @CurrentUserID + ''' ';						
		END
		ELSE
		IF (@UserType = 1)
		BEGIN
			SET @SqlTrack = '';
		END
		ELSE
		IF (@UserType = 2)
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.AssignBy = ''' + @CurrentUserID + ''' ';
			SET @SqlTrans = '';
		END
			-- Conditions @TrackType ==============================================================================================================================
		-- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
		IF (@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')
		BEGIN
			SET @FilerTrackType = '';
			IF (CHARINDEX('1',@TrackType) > 0 ) -- XLC
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
			END

			IF (CHARINDEX('2',@TrackType) > 0 ) -- PH
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '3,';	
			END

			IF (CHARINDEX('3',@TrackType) > 0 ) -- ĐB
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '7,';
			END
			SET @FilerTrackType = @FilerTrackType + '9'
			SET @SqlTrack = @SqlTrack + ' AND Track.Type IN (' + @FilerTrackType + ') ';
			SET @SqlTrans = '';
		END
		
		-- End Conditions @TrackType ==========================================================================================================================
				 
		-- End Conditions @ProcessType ===========================================================================================================================

		-- Conditions @UserType ==================================================================================================================================
		-- 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
		IF (@ProcessType = -1) -- dung han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType = -2) -- tre han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType > 0) -- den han x ngay
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, dateadd(DAY, 2, getdate())) = 1 ';
			SET @SqlTrans = '';

		END
		-- End Conditions @ProcessType ==================================================================================================================================		

		-- Conditions @TimeFilter ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
		IF (@TimeFilter != '')
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.Created IN (' + @TimeFilter + ') ';
		END
		-- End Conditions @TimeFilter ==========================================================================================================================
		-- Conditions @TrackStatus ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
		IF (@TrackStatus != '' AND @TrackStatus != '-1')
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.Status IN (' + @TrackStatus + ') ';	
			IF (@TrackStatus = '0')
			BEGIN
				SET @SqlTrans = @SqlTrans +  'AND Trans.Status = 0 ';
			END	
			ELSE
			BEGIN
				SET @SqlTrans = '';
			END	
		END
		-- End Conditions @TrackStatus ==========================================================================================================================
	END

	--select @SqlTrack;
	EXEC sp_executesql @SqlTrack;
	EXEC sp_executesql @SqlTrans;
				
	
					
	--Filter lại theo @DocType, @ExternalType, @DocStatus, @PrivateFolder, @Category
	CREATE TABLE #Results (Idx int, DocumentID uniqueidentifier, DocStatus nvarchar(50));
	SET @Sql = '
	INSERT INTO #Results
		SELECT
			ROW_NUMBER() OVER (ORDER BY Doc.' + @OrderBy + ')
		   ,Doc.ID
		   ,TDoc.StatusString
		FROM 
			Documents Doc
			INNER JOIN #DocTbl TDoc ON TDoc.DocID = Doc.ID ';
	 --Filter @PrivateFolder =====================================================================================================================
     IF (@PrivateFolder != '' AND @PrivateFolder IS NOT NULL )
	 BEGIN
		SET @Sql = @Sql + ' INNER JOIN FolderDetailDocuments PF ON PF.DocumentID = TDoc.DocID AND PF.FolderID = ''' + @PrivateFolder + ''' ';
	 END
	 --Filter @Category =====================================================================================================================
     IF (@Category != '' AND @Category IS NOT NULL )
	 BEGIN
		SET @Sql = @Sql + ' INNER JOIN CategorizeDetailDocuments CD ON CD.DocumentID = TDoc.DocID AND CD.CategorizeID = ''' + @Category + ''' ';
	 END

	 SET @Sql = @Sql + '	
		WHERE
			Doc.IsActive = 1 '; 
	 --Filter @KeyWord ========================================================================================================================
	 IF (@KeyWord IS NOT NULL AND @KeyWord != '')
	 BEGIN
		SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
		SET @Sql = @Sql + ' AND ( CONTAINS(Doc.Summary, N''' + @KeyWord +''') OR CONTAINS(Doc.Sender, N''' + @KeyWord +''') OR CONTAINS(Doc.SerialNumber, N''' + @KeyWord +''')  ) ';
	 END

	 --Filter @DocType ========================================================================================================================
	 -- 0: di, 1: den, 2: cong viec
	 SET @SqlFilter = ' ';
	 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0  AND CHARINDEX('2',@DocType) > 0 ) -- tat ca
	 BEGIN
		SET @SqlFilter = @SqlFilter + ' ';
	 END
	 ELSE
	 BEGIN
		 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0 ) -- den,di
		 BEGIN
			SET @SqlFilter = @SqlFilter + ''; -- AND (Doc.IsJob = 0)  OR Doc.IsJob IS NULL ) ';
		 END	 
		 ELSE
		 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- di,congviec
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 0 )';-- OR Doc.IsJob = 1 ) ';
		 END
		 ELSE
		 IF (CHARINDEX('1',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- den,congviec
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 1 )';--OR Doc.IsJob = 1 ) ';
		 END
		 ELSE
		 IF (CHARINDEX('0',@DocType) > 0) -- di
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 0)';-- AND Doc.IsJob = 0)  ';
		 END
		 IF (CHARINDEX('1',@DocType) > 0) -- den
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 1)';-- AND Doc.IsJob = 0)  ';
		 END
		 ELSE
		 IF (CHARINDEX('2',@DocType) > 0) -- cong viec
		 BEGIN
			SET @SqlFilter = @SqlFilter + '';-- AND Doc.IsJob = 1 ';
		 END
	 END
	 SET @Sql = @Sql + @SqlFilter;

	 --Filter @ExternalType ========================================================================================================================
	 -- 0: tat ca, 1: ben ngoai, 2: noi bo
	 SET @SqlFilter = ' ';
	 IF (@ExternalType = 1 ) -- tat ca
	 BEGIN
		SET @SqlFilter = @SqlFilter + ' AND Doc.IsExternal = 1 ';
	 END
	 IF (@ExternalType = 2 ) -- tat ca
	 BEGIN
		SET @SqlFilter = @SqlFilter + ' AND Doc.IsExternal = 0 ';
	 END
	 SET @Sql = @Sql + @SqlFilter;

	 --Filter @DocStatus ========================================================================================================================
	 SET @SqlFilter = ' ';
	 IF (@DocStatus != '' AND @DocStatus IS NOT NULL AND @DocStatus != '32' AND @DocStatus != '33' AND @DocStatus != '34'   )
	 BEGIN
		SET @SqlFilter = @SqlFilter + ' AND Doc.Status IN (' + @DocStatus + ') ';
	 END
	 ELSE
	 BEGIN
		IF ( @DocStatus = '32')
		BEGIN
			SET @SqlFilter = @SqlFilter + '  AND Doc.DocNumber IS NULL ';
		END
		IF ( @DocStatus = '33')
		BEGIN
			SET @SqlFilter = @SqlFilter + ' AND Doc.DocStatus IN (1) AND Doc.DocNumber IS NOT NULL ';
		END
		IF ( @DocStatus = '34')
		BEGIN
			SET @SqlFilter = @SqlFilter + ' ';
		END
	 END


	 SET @Sql = @Sql + @SqlFilter;
	-- PRINT @Sql;
	 EXEC sys.sp_executesql @Sql;

	 -- lấy số trang
	SET @TotalRecord = (
		SELECT MAX(Idx)
		FROM #Results);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	

	-- lấy dữ liệu phân trang
	IF (@IsCount = 0)
	BEGIN
		SELECT
			V.*,
			(CASE
				--WHEN V.IsJob = 1 THEN 'CONGVIEC'
				WHEN V.IsInComing = 1 THEN 'VBDEN'
				ELSE 'VBDI'
			 END) AS loaivanban,
			DT.DocStatus AS TinhTrang,
			@TotalPage TotalPage,
			@TotalRecord TotalRecord
		FROM 
			dbo.View_ListDocument V
			INNER JOIN #Results DT ON DT.DocumentID = V.DocID
		WHERE 
			DT.Idx BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
		ORDER BY 
			DT.Idx;
	END 
	ELSE
	BEGIN
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
		SELECT
		  CAST(	'00000000-0000-0000-0000-000000000000' as uniqueidentifier ) [DocID]
		   ,0 [DocNumber]
		   ,'' [SerialNumber]
		   ,'' [Summary]
		   ,NULL [DocDate]
		   ,NULL [SignedBy]
		   ,NULL [Sender]
		   ,NULL [Status]
		   ,NULL [IsInComing]
		   ,NULL [IsExternal]
		   ,NULL [ApprovedBy]
		   ,NULL [Approved]
		   ,0 [PriorityMaping]
		   ,NULL [SecretMaping]
		   ,NULL [PriorityName]
		   ,NULL [SecretName]
		   ,NULL [StatusCode]
		   ,0 [StatusID]
		   ,NULL [Created]
		   ,NULL [Modified]
		   ,'' AS loaivanban
		   ,'' AS TinhTrang
		   ,@TotalPage TotalPage
		   ,@TotalRecord TotalRecord;
		


	END                                                     
	

	DROP TABLE #Results
	DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 
	DROP TABLE #Results
	DROP TABLE #DocTbl;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Documents_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_Select_Documents_MultiFilters]
--DECLARE
	@CurrentDate	DATETIME		= '',
	@CurrentUser	NVARCHAR(200)	= '',
	@TrackStatus	NVARCHAR(20)	= '',				--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
	@DocStatus		NVARCHAR(20)	= '',					-- mã tình trạng văn bản
	@ProcessType	INT				= 0,					--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
	@TrackType		NVARCHAR(20)	= '',				--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
	@DocType		NVARCHAR(20)	= '1,2',				-- 0: Vb đi, 1: vb đến, 2: cong viec
	@ExternalType	INT				= 0,					-- 0: tat ca, 1: ben ngoai, 2: noi bo
	@UserType		INT				= 0,					--/ 0: assignto, 1: approved, 2: assignedby
	@UserDelegate   NVARCHAR(200)   = NULL,					-- lấy dữ liệu theo người ủy quyền
	@PrivateFolder	NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo folder cá nhân
	@Category		NVARCHAR(36)	= NULL,					-- lấy dữ liệu theo danh mục phân loại
	@TimeFilter		NVARCHAR(200)	= '',					-- filter thời gian theo ngày tạo văn bản 
    @KeyWord		NVARCHAR(200)	= '',
	@Page			INT				= 1,
	@PageSize		INT				= 10,
    @IsCount		BIT				= 0,
	@OrderBy		NVARCHAR(50)    = 'Created DESC',
	@LinkDocument	INT				= 0						-- 0: ko lấy từ link document, 1: lấy isbranch = 1, 2: lấy isbranch 
AS
--SET @CurrentDate			= ''
--SET	@CurrentUser		= ''
--SET	@TrackStatus		= ''				--/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
--SET	@DocStatus			= '33'				-- mã tình trạng văn bản
--SET	@ProcessType					= 0					--/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
--SET	@TrackType			= ''			--/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
--SET	@DocType		= '0'				-- 0: Vb đi, 1: vb đến, 2: cong viec
--SET	@ExternalType					= 0				-- 0: tat ca, 1: ben ngoai, 2: noi bo
--SET	@UserType						= 0					--/ 0: assignto, 1: approved, 2: assignedby
--SET	@UserDelegate     = NULL				-- lấy dữ liệu theo người ủy quyền
--SET	@PrivateFolder		= NULL				-- lấy dữ liệu theo folder cá nhân
--SET	@Category			= NULL					-- lấy dữ liệu theo danh mục phân loại
--SET	@TimeFilter			= ''					-- filter thời gian theo ngày tạo văn bản 
--  SET  @KeyWord			= ''
--SET	@Page							= 1 
--SET    @IsCount						= 0
--SET	@OrderBy		   = 'Created DESC'	
BEGIN TRY
	SET NOCOUNT ON;
	


	DECLARE @CurrentUserID nvarchar(36) = '';
	SET @CurrentUserID = @CurrentUser;-- dbo.Func_GetUserIDByLoginName(@CurrentUser);
	SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus);
	SET @DocStatus = dbo.Func_ReplaceParamFilter(@DocStatus);
	SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType);
	SET @DocType = dbo.Func_ReplaceParamFilter(@DocType);
	SET @TimeFilter = dbo.Func_ReplaceParamFilter(@TimeFilter);

	-- kiểm tra, nếu người dùng truyền vào user delegate thì lấy dữ liệu theo user đó
	-- kiểm tra xem user delegate đó còn hiệu lực hay không
	--IF (@UserDelegate != '' 
	--	AND @UserDelegate IS NOT NULL
	--	AND	EXISTS (SELECT
	--			TU.UserName
	--		FROM UserDelegations UD
	--			INNER JOIN Users FU ON FU.ID = UD.FromUserID
	--			INNER JOIN Users TU ON TU.ID = UD.ToUserID
	--		WHERE FU.UserName = @UserDelegate
	--		AND TU.UserName = @CurrentUser
	--		AND CAST(@CurrentDate AS DATE) BETWEEN UD.FromDate AND UD.ToDate)
	--)
	BEGIN
		SET @CurrentUser = @UserDelegate;
	END


	--Khai báo ------------------------------------------------------------------------
	CREATE TABLE #DocTbl (DocID uniqueidentifier , StatusString nvarchar(100), DeptID uniqueidentifier);	
	DECLARE @SqlTrack nvarchar(MAX), @SqlTrans nvarchar(MAX),
			@SqlFilter nvarchar(200),
			@Sql nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int,
			@FilerTrackType nvarchar(100) = '';
	--Hết khai báo ------------------------------------------------------------------------

	--Các trường hợp đặc biệt ==================================================================================================================================================================
	IF (@LinkDocument != 0)
	BEGIN
		IF (@LinkDocument = 1) -- lấy đơn vị
		BEGIN
			IF (@DocStatus = '')
			BEGIN
				INSERT INTO #DocTbl
					SELECT 
						LD.ID, CAST(LD.Status AS NVARCHAR(2) ) + '-LINKDOC', LD.DeptID
					FROM 
						LinkDocuments LD
					WHERE 
						LD.IsBranch = 1
			END
			ELSE
			BEGIN
				INSERT INTO #DocTbl
					SELECT 
						LD.ID, CAST(LD.Status AS NVARCHAR(2) ) + '-LINKDOC', LD.DeptID
					FROM 
						LinkDocuments LD
					WHERE 
						LD.IsBranch = 1 AND
						LD.Status = @DocStatus
			END
		END
		--ELSE
		--IF (@LinkDocument = 2) -- lấy liên thông mail
		--BEGIN
		--	INSERT INTO #DocTbl
		--		SELECT 
		--			LD.ID, CAST(LD.Status AS NVARCHAR(2) ) + '-LINKDOC', LD.DeptID
		--		FROM 
		--			LinkDocuments LD
		--		WHERE 
		--			LD.IsBranch = 0 AND
		--			LD.Status = @DocStatus
		--END
	END
	ELSE
	IF (@DocStatus = '12') -- Đang soạn
	BEGIN
		INSERT INTO #DocTbl
			SELECT 
				DOC.ID, '1-TRANSFER', DOC.EditDepartment
			FROM 
				Documents DOC
			WHERE 
				DOC.AuthorID = @CurrentUserID AND DOC.Status = 12;
	END
	ELSE
	IF (@DocStatus = '1') -- Đang chờ tôi duyệt
	BEGIN
		
		INSERT INTO #DocTbl
			SELECT 
				DOC.ID, '1-TRANSFER', DOC.EditDepartment
			FROM 
				Documents DOC				
			WHERE 
				DOC.Status = 1
				--AND CHARINDEX('<TransferInfo>', DOC.InternalReceiver) > 0
				AND DOC.ApprovedBy = @CurrentUserID 
				--and wft.Code != 'GetDocNumber'
				--and wfs.IsSignCA != 1
	END

	--Hết các trường hợp đặc biệt ==============================================================================================================================================================
	ELSE
	--Phần query theo DB ====================================================================================================================================================================
	BEGIN
		-- Base Query Tracking ==================================================================================================================================
		SET @SqlTrack = ' INSERT INTO #DocTbl
		SELECT 
			DISTINCT Track.DocID, CAST(
				(select top 1 dt2.Status 
				from TrackingDocuments dt2 
				where dt2.DocID = Track.DocID AND dt2.AssignTo = ''' + @CurrentUserID + ''' 
				order by dt2.Status) as varchar) + ''-TRACKING'', Track.DeptID
		FROM
			TrackingDocuments Track
		WHERE
			Track.DocID IS NOT NULL ';

		SET @SqlTrans = ' 	INSERT INTO #DocTbl
			SELECT
				Trans.DocumentID
			   ,CAST(Trans.Status AS NVARCHAR(10)) + ''-TRANSFER''
			   , Trans.DeptID
			FROM TransferDocuments Trans
			WHERE Trans.Receiver = ''' + @CurrentUserID + ''' 
			AND NOT EXISTS (SELECT
					DT1.StatusString
				FROM #DocTbl DT1
				WHERE DT1.DocID = Trans.DocumentID) ';
				
		-- Conditions @UserType =================================================================================================================================
		-- 0: assignto, 1: approved, 2: assignedby
		IF (@UserType = 0)
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo = ''' + @CurrentUserID + ''' ';						
		END
		ELSE
		IF (@UserType = 1)
		BEGIN
			SET @SqlTrack = '';
		END
		ELSE
		IF (@UserType = 2)
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.AssignBy = ''' + @CurrentUserID + ''' ';
			SET @SqlTrans = '';
		END
			-- Conditions @TrackType ==============================================================================================================================
		-- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
		IF (@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')
		BEGIN
			SET @FilerTrackType = '';
			IF (CHARINDEX('1',@TrackType) > 0 ) -- XLC
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
			END

			IF (CHARINDEX('2',@TrackType) > 0 ) -- PH
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '3,';	
			END

			IF (CHARINDEX('3',@TrackType) > 0 ) -- ĐB
			BEGIN
				SET	@FilerTrackType = @FilerTrackType + '7,';
			END
			SET @FilerTrackType = @FilerTrackType + '9'
			SET @SqlTrack = @SqlTrack + ' AND Track.Type IN (' + @FilerTrackType + ') ';
			SET @SqlTrans = '';
		END
		
		-- End Conditions @TrackType ==========================================================================================================================
				 
		-- End Conditions @ProcessType ===========================================================================================================================

		-- Conditions @UserType ==================================================================================================================================
		-- 0: tat ca, -1: trong han, -2: qua han, > 3 den han (processtype se ngay den han)
		IF (@ProcessType = -1) -- dung han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 0 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType = -2) -- tre han
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, getdate()) = 1 ';
			SET @SqlTrans = '';
		END
		ELSE
		IF (@ProcessType > 0) -- den han x ngay
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.Status, dateadd(DAY, 2, getdate())) = 1 ';
			SET @SqlTrans = '';

		END
		-- End Conditions @ProcessType ==================================================================================================================================		

		-- Conditions @TimeFilter ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
		IF (@TimeFilter != '')
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.Created IN (' + @TimeFilter + ') ';
		END
		-- End Conditions @TimeFilter ==========================================================================================================================
		-- Conditions @TrackStatus ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc
		IF (@TrackStatus != '' AND @TrackStatus != '-1')
		BEGIN
			SET @SqlTrack = @SqlTrack + ' AND Track.Status IN (' + @TrackStatus + ') ';	
			IF (@TrackStatus = '0')
			BEGIN
				SET @SqlTrans = @SqlTrans +  'AND Trans.Status = 0 ';
			END	
			ELSE
			BEGIN
				SET @SqlTrans = '';
			END	
		END
		-- End Conditions @TrackStatus ==========================================================================================================================
	END

	--print @SqlTrans

	--select @SqlTrack;
	EXEC sp_executesql @SqlTrack;
	EXEC sp_executesql @SqlTrans;
				
					
	--Filter lại theo @DocType, @ExternalType, @DocStatus, @PrivateFolder, @Category
	CREATE TABLE #Results (Idx int, DocumentID uniqueidentifier, DocStatus nvarchar(50), DeptID uniqueidentifier);
	IF (@LinkDocument != 0)
	BEGIN
		SET @Sql = '
		INSERT INTO #Results
			SELECT
				ROW_NUMBER() OVER (ORDER BY LD.ReceivedDate DESC)
			   ,LD.DocumentID
			   ,TDoc.StatusString
			   ,TDoc.DeptID
			FROM 
				LinkDocuments LD
				INNER JOIN #DocTbl TDoc ON TDoc.DocID = LD.ID ';
		Print @sql;
	END
	ELSE
	BEGIN
		SET @Sql = '
		INSERT INTO #Results
			SELECT
				ROW_NUMBER() OVER (ORDER BY Doc.' + @OrderBy + ')
			   ,Doc.ID
			   ,TDoc.StatusString
			   ,Doc.EditDepartment
			FROM 
				Documents Doc
				INNER JOIN #DocTbl TDoc ON TDoc.DocID = Doc.ID ';
		 --Filter @PrivateFolder =====================================================================================================================
		 IF (@PrivateFolder != '' AND @PrivateFolder IS NOT NULL )
		 BEGIN
			SET @Sql = @Sql + ' INNER JOIN FolderDetailDocuments PF ON PF.DocumentID = TDoc.DocID AND PF.FolderID = ''' + @PrivateFolder + ''' ';
		 END
		 --Filter @Category =====================================================================================================================
		 IF (@Category != '' AND @Category IS NOT NULL )
		 BEGIN
			SET @Sql = @Sql + ' INNER JOIN CategorizeDetailDocuments CD ON CD.DocumentID = TDoc.DocID AND CD.CategorizeID = ''' + @Category + ''' ';
		 END

		 SET @Sql = @Sql + '	
			WHERE
				Doc.IsActive = 1 '; 
		 --Filter @KeyWord ========================================================================================================================
		 IF (@KeyWord IS NOT NULL AND @KeyWord != '')
		 BEGIN
			SET @KeyWord = dbo.Func_Doc_Build_NEAR_Keyword(@KeyWord);
			SET @Sql = @Sql + ' AND ( CONTAINS(Doc.Summary, N''' + @KeyWord +''') OR CONTAINS(Doc.Sender, N''' + @KeyWord +''') OR CONTAINS(Doc.SerialNumber, N''' + @KeyWord +''')  ) ';
		 END

		 --Filter @DocType ========================================================================================================================
		 -- 0: di, 1: den, 2: cong viec
		 SET @SqlFilter = ' ';
		 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0  AND CHARINDEX('2',@DocType) > 0 ) -- tat ca
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' ';
		 END
		 ELSE
		 BEGIN
			 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('1',@DocType) > 0 ) -- den,di
			 BEGIN
				SET @SqlFilter = @SqlFilter + ''; -- AND (Doc.IsJob = 0)  OR Doc.IsJob IS NULL ) ';
			 END	 
			 ELSE
			 IF (CHARINDEX('0',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- di,congviec
			 BEGIN
				SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 0 )';-- OR Doc.IsJob = 1 ) ';
			 END
			 ELSE
			 IF (CHARINDEX('1',@DocType) > 0 AND CHARINDEX('2',@DocType) > 0 ) -- den,congviec
			 BEGIN
				SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 1 )';--OR Doc.IsJob = 1 ) ';
			 END
			 ELSE
			 IF (CHARINDEX('0',@DocType) > 0) -- di
			 BEGIN
				SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 0)';-- AND Doc.IsJob = 0)  ';
			 END
			 IF (CHARINDEX('1',@DocType) > 0) -- den
			 BEGIN
				SET @SqlFilter = @SqlFilter + ' AND (Doc.IsInComing = 1)';-- AND Doc.IsJob = 0)  ';
			 END
			 ELSE
			 IF (CHARINDEX('2',@DocType) > 0) -- cong viec
			 BEGIN
				SET @SqlFilter = @SqlFilter + '';-- AND Doc.IsJob = 1 ';
			 END
		 END
		 SET @Sql = @Sql + @SqlFilter;

		 --Filter @ExternalType ========================================================================================================================
		 -- 0: tat ca, 1: ben ngoai, 2: noi bo
		 SET @SqlFilter = ' ';
		 IF (@ExternalType = 1 ) -- tat ca
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND Doc.IsExternal = 1 ';
		 END
		 IF (@ExternalType = 2 ) -- tat ca
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND Doc.IsExternal = 0 ';
		 END
		 SET @Sql = @Sql + @SqlFilter;

		 --Filter @DocStatus ========================================================================================================================
		 SET @SqlFilter = ' ';
		 IF (@DocStatus != '' AND @DocStatus IS NOT NULL AND @DocStatus != '32' AND @DocStatus != '33' AND @DocStatus != '34'   )
		 BEGIN
			SET @SqlFilter = @SqlFilter + ' AND Doc.Status IN (' + @DocStatus + ') ';
		 END
		 ELSE
		 BEGIN
			IF ( @DocStatus = '32')
			BEGIN
				SET @SqlFilter = @SqlFilter + '  AND Doc.DocNumber IS NULL ';
			END
			IF ( @DocStatus = '33')
			BEGIN
				SET @SqlFilter = @SqlFilter + ' AND Doc.Status IN (1) AND Doc.DocNumber IS NOT NULL ';
			END
			IF ( @DocStatus = '34')
			BEGIN
				SET @SqlFilter = @SqlFilter + ' ';
			END
		 END


		 SET @Sql = @Sql + @SqlFilter;
	END
	--PRINT @Sql;
	EXEC sys.sp_executesql @Sql;

	 -- lấy số trang
	SET @TotalRecord = (
		SELECT MAX(Idx)
		FROM #Results);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);
	

	-- lấy dữ liệu phân trang
	IF (@IsCount = 0)
	BEGIN
		SELECT
			DT.DocumentID ,
			DT.DocStatus AS TinhTrang,
			DT.DeptID,
			@TotalPage TotalPage,
			@TotalRecord TotalRecord
		FROM 
			 #Results DT
		WHERE 
			DT.Idx BETWEEN (@Page - 1) * @PageSize + 1 AND (@Page - 1) * @PageSize + @PageSize
		ORDER BY 
			DT.Idx;
			
	END 
	ELSE
	BEGIN
		IF (@TotalRecord IS NULL)
			SET @TotalRecord = 0;
		IF (@TotalPage IS NULL)
			SET @TotalPage = 0;
		SELECT
		  CAST(	'00000000-0000-0000-0000-000000000000' as uniqueidentifier ) DocumentID
		   ,'' AS TinhTrang
		   ,  CAST(	'00000000-0000-0000-0000-000000000000' as uniqueidentifier ) DeptID
		   ,@TotalPage TotalPage
		   ,@TotalRecord TotalRecord;
		


	END                                                     
	
	
		DROP TABLE #Results
		DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()
 
	DROP TABLE #Results
	DROP TABLE #DocTbl;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Export_WebAnalysis_Date_Org_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Select_Export_WebAnalysis_Date_Org_OrgParentIDFromDateToDate]
--DECLARE
	@OrgParentID uniqueidentifier, -- = '00000000-0000-0000-0000-000000000000' ,
	@FromDate date ,--= '2017-01-03',
	@ToDay date  --= '2017-02-03'
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #RCV_Org (OrgID UNIQUEIDENTIFIER, OrgName NVARCHAR(400), OLevel bigint);

	;WITH TbTemp 
		AS
		(
			SELECT O.ID , 
				   O.Name, 
				   1 AS OrgLevel,
				   CAST('' as nvarchar) AS SpacePath,
				   O.OrderNumber AS OrgPath,			
				   dbo.Func_BuildLevelPath(8,'', 1, 1) as PathLevel
			FROM 
				Departments AS O    		
			WHERE 
				(O.ID = @OrgParentID  ) AND O.IsActive = 1 			  
			UNION ALL 			
			SELECT  
					O.ID, 
					O.Name, 
					DP.OrgLevel + 1 AS OrgLevel, 				
					CAST((CAST(DP.SpacePath as nvarchar) + '—') as nvarchar)   AS SpacePath,
					O.OrderNumber as OrgPath,
				    dbo.Func_BuildLevelPath(8,DP.PathLevel, ROW_NUMBER() OVER(ORDER BY O.OrderNumber asc, O.Name asc), DP.OrgLevel) as PathLevel
			FROM 
				Departments AS O
				INNER JOIN TbTemp AS DP ON DP.ID = O.ParentID		    
			WHERE  O.IsActive = 1 
		)
	INSERT INTO #RCV_Org
		SELECT T.ID, SpacePath + Name, cast ( T.PathLevel as bigint )
		FROM  TbTemp T

	IF (@OrgParentID IS NULL OR @OrgParentID = '00000000-0000-0000-0000-000000000000')
	BEGIN
		SELECT 
			Year(@ToDay) [Year],
			Month(@ToDay) [Month],
			DAY(@ToDay) [Day],	
			@OrgParentID ParentID,
			T2.OrgID,
			T2.OrgName,
			CASE WHEN MobileRequestTotal IS NULL THEN 0 ELSE R.[MobileRequestTotal] END [MobileRequestTotal],
			CASE WHEN [RequestTotal] IS NULL THEN 0 ELSE R.[RequestTotal] END [RequestTotal],
			CASE WHEN [OnlineTotal] IS NULL THEN 0 ELSE R.[OnlineTotal] END [OnlineTotal]
		FROM 
		(
			Select 
				t1.Year, t1.Month, t1.Date,
				t1.ParentID, t1.OrgID, t2.Name OrgName,
				t1.MobileRequestTotal,t1.RequestTotal,t1.OnlineTotal, R.OLevel
			from 
				WebAnalysis_Date_Org t1 
				join Departments t2 on t1.OrgID = t2.ID
				INNER JOIN #RCV_Org R ON R.OrgID = T2.ID
			where
				( t1.OrgID IN (SELECT t1.OrgID FROM #RCV_Org) )
				And t2.IsActive = 1
				And @FromDate <= DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And @ToDay > =DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And t1.RequestTotal > 0
		) R 
		RIGHT JOIN #RCV_Org T2 ON R.OrgID = T2.OrgID
		order by 
			R.OLevel
	END
	ELSE
	BEGIN
		SELECT 
			Year(@ToDay) [Year],
			Month(@ToDay) [Month],
			DAY(@ToDay) [Day],	
			@OrgParentID ParentID,
			T2.OrgID,
			T2.OrgName,
			CASE WHEN MobileRequestTotal IS NULL THEN 0 ELSE R.[MobileRequestTotal] END [MobileRequestTotal],
			CASE WHEN [RequestTotal] IS NULL THEN 0 ELSE R.[RequestTotal] END [RequestTotal],
			CASE WHEN [OnlineTotal] IS NULL THEN 0 ELSE R.[OnlineTotal] END [OnlineTotal]
		FROM 
		(
			Select 		
				Year(@ToDay) [Year],
				Month(@ToDay) [Month],
				DAY(@ToDay) [Day],		
				@OrgParentID ParentID,
				t1.OrgID,
				t2.Name OrgName,
				SUM(t1.MobileRequestTotal) AS [MobileRequestTotal],
				SUM(t1.RequestTotal) AS [RequestTotal],
				SUM(t1.OnlineTotal ) AS [OnlineTotal], R.OLevel
			from 
				WebAnalysis_Date_Org t1 
				join Departments t2 on t1.OrgID=t2.ID
				INNER JOIN #RCV_Org R ON R.OrgID = T2.ID
			where
				( t1.OrgID IN (SELECT t1.OrgID FROM #RCV_Org) )
				And t2.IsActive = 1
				And @FromDate <= DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And @ToDay >= DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And t1.RequestTotal > 0
			GROUP BY 
				t1.OrgID, t2.Name, R.OLevel
		) R
		right join #RCV_Org T2 ON R.OrgID = T2.OrgID
		order by 
			T2.OLevel
	END


	DROP TABLE #RCV_Org ;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Export_WebAnalysis_Date_OrgUser_OrgIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_Select_Export_WebAnalysis_Date_OrgUser_OrgIDFromDateToDate]
--DECLARE
	@OrgID uniqueidentifier  ,
	@FromDate date,
	@ToDay date
AS
BEGIN
	SET NOCOUNT ON;
	
	CREATE TABLE #RCV_Org (OrgID UNIQUEIDENTIFIER, OLevel bigint, UserID nvarchar(200), ULevel int, OrgName nvarchar(400));
	DECLARE @DBName nvarchar(200) =  (SELECT TOP 1 D.DatabaseName FROM Departments D WHERE D.ID = @OrgID);
	;WITH TbTemp 
		AS
		(
			SELECT O.ID, 
				O.Name, 
				1 AS OrgLevel,
				CAST('' as nvarchar) AS SpacePath,
				O.OrderNumber AS OrgPath,			
				dbo.Func_BuildLevelPath(8,'', 1, 1) as PathLevel
			FROM 
				Departments AS O    		
			WHERE 
				(O.ID = @OrgID  )  AND O.IsActive = 1  
				AND
				O.DatabaseName = @DBName
			UNION ALL 			
			SELECT  
				O.ID, 
				O.Name, 
				DP.OrgLevel + 1 AS OrgLevel, 				
				CAST((CAST(DP.SpacePath as nvarchar) + ' ') as nvarchar)   AS SpacePath,
				O.OrderNumber as OrgPath,
				dbo.Func_BuildLevelPath(8,DP.PathLevel, ROW_NUMBER() OVER(ORDER BY O.OrderNumber, O.Name), DP.OrgLevel) as PathLevel
			FROM
				Departments  AS O
				INNER JOIN TbTemp AS DP ON DP.ID = O.ParentID	    
			WHERE  
				O.IsActive = 1 
				AND
				O.DatabaseName = @DBName
		)
	INSERT INTO #RCV_Org
		SELECT 
			T.ID,  cast ( T.PathLevel as bigint ), U.UserName, OU.OrderNumber, SpacePath + T.Name
		FROM  
			TbTemp T 
			LEFT JOIN UserDepartments OU ON OU.DeptID = T.ID
			LEFT JOIN Users U ON U.ID = OU.UserID
				--WHERE OU.UserName Not like '%undefined%'
	SELECT 
		CASE WHEN TB2.OrgBookName  IS NULL THEN TB2.OrgBookName2 ELSE TB2.OrgBookName END [OrgBookName],
		CASE WHEN TB2.OrgName  IS NULL THEN TB2.OrgName2 ELSE TB2.OrgName END [OrgName],
		CASE WHEN TB2.FullName IS NULL THEN TB2.OrgName2 ELSE TB2.FullName END [FullName],
		CASE WHEN TB2.UserName IS NULL THEN TB2.UserID ELSE TB2.UserName END [UserName],
		CASE WHEN TB2.RequestTotal IS NULL THEN 0 ELSE TB2.RequestTotal END [RequestTotal],
		CASE WHEN TB2.MobileRequestTotal IS NULL THEN 0 ELSE TB2.MobileRequestTotal END [MobileRequestTotal],	
		Year(@ToDay) [Year],
		Month(@ToDay) [Month],
		DAY(@ToDay) [Date],
		
		CASE WHEN TB2.OrgID IS NULL THEN TB2.OrgID2 ELSE TB2.OrgID END [OrgID]
		
		
	FROM
	(
		SELECT 
			TB.*, O.OrgName OrgName2,O.OLevel, O.ULevel, O.UserID, O.OrgID OrgID2,
			(
				SELECT TOP 1 O2.Name FROM Departments O2 
				WHERE 
					O2.ID = (
						SELECT TOP 1 O3.RootDBID FROM Departments O3 WHERE O3.ID = O.OrgID
					)  
			) OrgBookName2
		FROM 
		(
			Select 
				Year(@ToDay) [Year],
				Month(@ToDay) [Month],
				DAY(@ToDay) [Date],		
				t1.UserName ,--+ '_' + t2.FullName UserName,		
				t2.FullName,
				SUM(t1.RequestTotal) AS [RequestTotal],
				SUM(t1.MobileRequestTotal) AS [MobileRequestTotal],
				t3.ID OrgID,
				t3.Name OrgName,
				(
					SELECT TOP 1 O2.Name FROM Departments O2 
					WHERE O2.ID = (
						SELECT TOP 1 O3.RootDBID FROM Departments O3 WHERE O3.ID = t3.ID
					)  
				) OrgBookName
				
			from 
				WebAnalysis_Date_OrgUser t1 
				join Users t2 on t1.UserName = t2.UserName 
				join Departments t3 on t1.OrgID = t3.ID
			where 
				(t1.OrgID  in (SELECT OrgID FROM #RCV_Org))
				And @FromDate <= DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And @ToDay >= DATEFROMPARTS(t1.Year,t1.Month,t1.Date)
				And t1.RequestTotal>0
			GROUP BY 
				t1.UserName, t2.FullName, t3.ID, t3.Name 
		) TB 
		right JOIN #RCV_Org O ON O.OrgID = TB.OrgID and o.UserID = tb.UserName
		
	--order by  t1.UserName
	) TB2
	ORDER BY  
		TB2.OLevel , TB2.ULevel

	DROP TABLE #RCV_Org ;

END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Item_CoreSchedule]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2018/08/30
-- Description:	Lấy danh sách lặp item theo lịch
-- và module
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_Item_CoreSchedule]
	@CurrentDate date,
	@ModuleCode nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

    
	-- ScheduleType
	-- 0 - Daily
	-- 1 - Weekly
	-- 2 - Monthly
	-- 3 - Yearly
	SELECT	
		Results.ObjectId,
		Results.ScheduleType,
		CASE Results.ScheduleType
			WHEN 0 THEN 'Daily'
			WHEN 1 THEN 'Weekly'
			WHEN 2 THEN 'Monthly'
			WHEN 3 THEN 'Yearly'
		END AS ScheduleTypeCode
	FROM 
	(
		---------------------------------------------------------------------
		-- repeat daily
		--PRINT 'repeat daily - Every weekday'
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 0 AND Sch.IntervalInWeekday = 1		-- in case weekday
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			AND (DATEPART(weekday, @CurrentDate) in (1,7) )			-- in case SA, SUN
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)
		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat daily - Every on 1/day(s)'
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 0 AND Sch.IntervalInWeekday = 0		-- in case day
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberDay(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(DAY, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)
		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat weekly - every 2 weeks on  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 1									-- in case week
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberWeek(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(WEEK, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			-- in case WeekDay in selected
			AND  (	DATEPART(WEEKDAY, @CurrentDate) 
					in (
						SELECT items FROM dbo.Func_SplitTextToTable(Sch.IntervalInDayOfWeek, ';')
					) 
				  )	
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat monthly - every on the day  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 2									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NOT NULL
			AND Sch.IntervalInDateOfMonth = DATEPART(DAY, @CurrentDate)
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat monthly - every on the first,second,third,fourth, last TuesDay  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 2									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NULL
			AND ( 
				(IntervalOrdinalNumber != -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 = Sch.IntervalOrdinalNumber )
				OR 
				(IntervalOrdinalNumber = -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 >= 4 )
			)
			AND DATEPART(WEEKDAY, @CurrentDate) = Sch.IntervalOrdinalNumberInDayOfWeek
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		---------------------------------------------------------------------------------------------------
		--PRINT 'repeat yearly - every on the day  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 3									-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate 
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInMonthOfYear = MONTH(@CurrentDate)
			AND Sch.IntervalInDateOfMonth IS NOT NULL
			AND Sch.IntervalInDateOfMonth = DATEPART(DAY, @CurrentDate)
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		-------------------------------------------------------------------------------------------------
		--PRINT 'repeat yearly - every on the first,second,third,fourth, last TuesDay  '
		UNION ALL
		SELECT 
			Sch.ObjectId, Sch.ScheduleType
		FROM
			Core.Schedule Sch
		WHERE
			Sch.ScheduleType = 3								-- in case month
			AND  (@CurrentDate BETWEEN Sch.StartDate AND Sch.EndDate)		-- in case fromdate - todate 
			----------------------------------------------------------------------------------------
			-- in case frequently by function NumberMONTH(StartDate,CurrentDate) mod frequently = 0
			AND DATEDIFF(MONTH, Sch.StartDate, @CurrentDate) % Sch.IntervalFrequently = 0
			AND Sch.IntervalInDateOfMonth IS NULL
			AND Sch.IntervalInMonthOfYear = MONTH(@CurrentDate)
			AND ( 
				(IntervalOrdinalNumber != -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 = Sch.IntervalOrdinalNumber )
				OR 
				(IntervalOrdinalNumber = -1 AND DATEDIFF(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, @CurrentDate), 0), @CurrentDate) + 1 >= 4 )
			)
			AND DATEPART(WEEKDAY, @CurrentDate) = Sch.IntervalOrdinalNumberInDayOfWeek
			AND Sch.ModuleId IN  (SELECT ID FROM Modules WHERE @ModuleCode = Area)

		-------------------------------------------------------------------------------------------------
	) AS Results
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_ListOnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_ListOnlineUsers] 
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @LstUserIDs nvarchar(max) = ''

	SELECT
		@LstUserIDs = @LstUserIDs + REPLACE(UserID, '0#.f|pvgas|', 'pvgas\') + ';'
	FROM 
		(SELECT DISTINCT
			UserID
		FROM OnlineUsers) TB

	EXEC SP_GetOrgUser_ByListUser @LstUserIDs

END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUser_Date_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Select_OnlineUser_Date_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	
    Select  tbDateOrg.Year,
	        tbDateOrg.Month,
			tbDateOrg.Date,
			tbDateOrg.OrgID,
			tbOrg.Name OrgName,
			Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'
	from 
		WebAnalysis_Date_Org tbDateOrg 
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID  
	where 
		tbOrg.ParentID=@OrgParentID
		And tbOrg.IsActive=1
		And @FromDate<=DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate>DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.Date,tbDateOrg.OrgID,tbOrg.Name,tbOrg.OrderNumber
	order by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.Date, tbOrg.OrderNumber
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUser_Month_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Select_OnlineUser_Month_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	
    Select  
		tbDateOrg.Year,
	    tbDateOrg.Month,
		tbDateOrg.OrgID,
		tbOrg.Name OrgName,
		Sum(tbDateOrg.OnlineTotal) as 'OnlineTotal'

	from 
		WebAnalysis_Date_Org tbDateOrg
		inner join Departments tbOrg on tbDateOrg.OrgID = tbOrg.ID  
	where 
		tbOrg.ParentID = @OrgParentID
		And tbOrg.IsActive=1
		And @FromDate<=DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
		And @ToDate>DATEFROMPARTS(tbDateOrg.Year,tbDateOrg.Month,tbDateOrg.Date)
	Group by 
		tbDateOrg.Year,tbDateOrg.Month ,tbDateOrg.OrgID,tbOrg.Name,tbOrg.OrderNumber
	order by 
		tbDateOrg.Year,tbDateOrg.Month , tbOrg.OrderNumber
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUser_Quarter_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_OnlineUser_Quarter_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		tbDateOrg.Year
	   ,CASE
			WHEN (tbDateOrg.Month <= 3) THEN 1
			WHEN (tbDateOrg.Month > 3 AND
				tbDateOrg.Month <= 6) THEN 2
			WHEN (tbDateOrg.Month > 6 AND
				tbDateOrg.Month <= 9) THEN 3
			ELSE 4
		END
		AS 'Quarter'
	   ,tbDateOrg.OrgID
	   ,tbOrg.Name OrgName
	   ,SUM(tbDateOrg.OnlineTotal) AS 'OnlineTotal' INTO #TemTable
	FROM 
		WebAnalysis_Date_Org tbDateOrg
		INNER JOIN Departments tbOrg ON tbDateOrg.OrgID = tbOrg.ID
	WHERE 
		tbOrg.ParentID = @OrgParentID
		AND tbOrg.IsActive = 1
		AND @FromDate <= DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
		AND @ToDate > DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
	GROUP BY tbDateOrg.Year
			,tbDateOrg.Month
			,tbDateOrg.OrgID
			,tbOrg.Name
			,tbOrg.OrderNumber
	ORDER BY 
		tbDateOrg.Year, tbDateOrg.Month, tbOrg.OrderNumber;

	SELECT
		Quarter
	   ,OrgID
	   ,OrgName
	   ,SUM(OnlineTotal) AS 'OnlineTotal'
	FROM #TemTable
	GROUP BY Quarter
			,OrgID
			,OrgName;

	DROP TABLE #TemTable;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUser_Week_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_OnlineUser_Week_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DATEPART(wk, DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)) AS 'Week'
	   ,tbDateOrg.OrgID
	   ,tbOrg.Name OrgName
	   ,SUM(tbDateOrg.OnlineTotal) AS 'OnlineTotal' INTO #TemTable
	FROM 
		WebAnalysis_Date_Org tbDateOrg
	    INNER JOIN Departments tbOrg ON tbDateOrg.OrgID = tbOrg.ID
	WHERE 
		tbOrg.ParentID = @OrgParentID
		AND tbOrg.IsActive = 1
		AND @FromDate <= DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
		AND @ToDate > DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
	GROUP BY tbDateOrg.Year
			,tbDateOrg.Month
			,tbDateOrg.Date
			,tbDateOrg.OrgID
			,tbOrg.Name
			,tbOrg.OrderNumber
	ORDER BY 
		tbOrg.OrderNumber;

	SELECT
		Week
	   ,OrgID
	   ,OrgName
	   ,SUM(OnlineTotal) AS 'OnlineTotal'
	FROM #TemTable
	GROUP BY Week
			,OrgID
			,OrgName;

	DROP TABLE #TemTable;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUser_Year_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Select_OnlineUser_Year_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		tbDateOrg.Year
	   ,tbDateOrg.OrgID
	   ,tbOrg.Name OrgName
	   ,SUM(tbDateOrg.OnlineTotal) AS 'OnlineTotal'
	FROM 
		WebAnalysis_Date_Org tbDateOrg
		INNER JOIN Departments tbOrg ON tbDateOrg.OrgID = tbOrg.ID
	WHERE 
		tbOrg.ParentID = @OrgParentID
		AND tbOrg.IsActive = 1
		AND @FromDate <= DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
		AND @ToDate > DATEFROMPARTS(tbDateOrg.Year, tbDateOrg.Month, tbDateOrg.Date)
	GROUP BY tbDateOrg.Year
			,tbDateOrg.OrgID
			,tbOrg.Name
			,tbOrg.OrderNumber
	ORDER BY 
		tbDateOrg.Year, tbOrg.OrderNumber;
  
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OnlineUsers]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		tmquan
-- Create date: 2016-08-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_OnlineUsers]
	@ServerName nvarchar(150) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@ServerName IS NOT NULL AND @ServerName != '')
	BEGIN
		SELECT *
		FROM OnlineUsers
		WHERE ServerName = @ServerName		
		and CAST(LastActivityDate AS DATE) = CAST(getdate() as DATE)
		ORDER BY ServerName, LastActivityDate
	END
	ELSE
	BEGIN
		SELECT *
		FROM OnlineUsers
		where ServerName = 'SRV-QLCV-APP' 
		and CAST(LastActivityDate AS DATE) = CAST(getdate() as DATE)
		and UserName is not null
		ORDER BY ServerName, LastActivityDate
	END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_Projects_MultiFilters]
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
			SET @sql = @sql + ' AND (TItem.FromDate >= ''' + @fromDateOfFromDate + ''' 
											OR TItem.FromDate >= ''' + @fromDateOfFromDate + '''
											OR Pro.FromDate >= ''' + @fromDateOfFromDate + ''')';
		END
		print '#12312'
		IF (@TaskFromDateOfToDate != '')
		BEGIN
			declare @fromDateOfToDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskFromDateOfToDate, 103)))
			SET @sql = @sql + ' AND (TItem.FromDate <= ''' + @fromDateOfToDate + '''
											OR TItem.FromDate <= ''' + @fromDateOfToDate + '''
											OR Pro.FromDate <= ''' + @fromDateOfToDate + ''')';
		END
		print '#12312'
		-- so sánh với todate của công việc
		IF (@TaskToDateOfFromDate != '')
		BEGIN
		declare @toDateOfFromDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskToDateOfFromDate, 103)))
			SET @sql = @sql + ' AND (TItem.ToDate >= ''' + @toDateOfFromDate + ''' 
											OR TItem.ToDate >= ''' + @toDateOfFromDate + '''
											OR Pro.ToDate >= ''' + @toDateOfFromDate + ''')';
		END
		print '#12312'
		IF (@TaskToDateOfToDate != '')
		BEGIN
			declare @toDateOfToDate nvarchar(50) = (SELECT CONVERT(nvarchar(50), convert(datetime, @TaskToDateOfToDate, 103)))
			SET @sql = @sql + ' AND (TItem.ToDate <= ''' + @toDateOfToDate + '''
											OR TItem.ToDate <= ''' + @toDateOfToDate + '''
											OR Pro.ToDate <= ''' + @toDateOfToDate + ''')';

		END
		print '#12312'
		-- FromDate Condition
		IF (@FromDate != '')
		BEGIN
			DECLARE @fDate nvarchar(255) = (SELECT convert(datetime, @FromDate, 103))
			SET @sql = @sql + ' AND (TItem.FromDate >= ''' + @fDate + ''' 
											OR TItem.FromDate >= ''' + @fDate + '''
											OR Pro.FromDate >= ''' + @fDate + ''')';
		END

		-- ToDate Condition
		IF (@ToDate != '')
		BEGIN
		DECLARE @tDate nvarchar(255) = (SELECT convert(datetime, @ToDate, 103))
			SET @sql = @sql + ' AND (TItem.ToDate <= ''' + @tDate + '''
											OR TItem.ToDate <= ''' + @tDate + '''
											OR Pro.ToDate <= ''' + @tDate + ''')';
		END
	END
	ELSE
	BEGIN
	-- sắp hết hạn
		IF (@StatusTime = 1)
		BEGIN
			SET @sql = @sql + ' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
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
			SET @sql = @sql + ' AND dbo.Func_Doc_CheckIsOverDue(TItem.ToDate, 
																	  TItem.FinishedDate,
																	  TItem.TaskItemStatusId, 
																	  getdate()) = 1 
								AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
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
	 
	IF (@ProjectHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @sql = @sql + ' AND (EXISTS  (SELECT 1
													FROM dbo.Func_SplitTextToTable(N'''+@ProjectHashtag+''','','') where items in (select *  from dbo.Func_SplitTextToTable(Pro.ProjectCategory,'';''))))';
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
	END

	IF (@TaskHashtag != '')
	BEGIN
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (Pro.ProjectCategory like N''%' + @ProjectHashtag + '%'')';
		SET @sql = @sql + ' AND (EXISTS  (SELECT 1
													FROM dbo.Func_SplitTextToTable(N'''+@TaskHashtag+''','','') where items in (select *  from dbo.Func_SplitTextToTable(TaskItemCategory,'';''))))';
		--SET @ProTaskSqlTrack = @ProTaskSqlTrack + ' AND (TItem.TaskItemCategory like N''%' + @TaskHashtag + '%'')';
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
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_MultiFilters_MyTask]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
--Author:		TRUNGNT
-- Create date: 2020 - 01 - 13
-- Description: Search project with current user has been assigned task
-- =============================================
CREATE   PROCEDURE[dbo].[SP_Select_Projects_MultiFilters_MyTask]
    --DECLARE

    @CurrentDate    DATETIME = '', --getdate(),
    @CurrentUser    NVARCHAR(200) = '', -- .Func_GetUserIDByLoginName('benthanh\nguyenminhphuong'), --'',
    @TrackStatus    NVARCHAR(20) = '', --/ 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc

    @DocStatus      NVARCHAR(20) = '', --mã tình trạng văn bản

    @ProcessType    INT = 0, --/ 0: tat ca, -1: trong han, -2: qua han, > 3 den han(processtype se ngay den han)

    @TrackType      NVARCHAR(20) = '', --/ '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
    @DocType        NVARCHAR(20) = '1', --0: Vb đi, 1: vb đến, 2: cong viec, 3: cong viec dinh ky

    @ExternalType   INT = 0, --0: tat ca, 1: ben ngoai, 2: noi bo

    @UserType       INT = 0, --/ 0: assignto, 1: approved, 2: assignedby

    @UserDelegate   NVARCHAR(200) = NULL, --lấy dữ liệu theo người ủy quyền

    @PrivateFolder  NVARCHAR(36) = NULL, --lấy dữ liệu theo folder cá nhân

    @Category       NVARCHAR(36) = NULL, --lấy dữ liệu theo danh mục phân loại

    @TimeFilter     NVARCHAR(200) = NULL, --filter thời gian theo ngày tạo văn bản
    @KeyWord        NVARCHAR(200) = '',
    @Page           INT = 1,
    @PageSize       INT = 10,
    @IsCount        BIT = 0,
    @OrderBy        NVARCHAR(50) = 'CreatedDate DESC'
    , @FromDate      NVARCHAR(10) = ''
    , @ToDate        NVARCHAR(10) = ''
    , @DepartmentId  NVARCHAR(36) = ''
    , @UserId        NVARCHAR(36) = ''
AS
BEGIN TRY
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    SET NOCOUNT ON;

                --Insert statements for procedure here
           
               set arithabort on;
                IF(@OrderBy = 'Created DESC')

    BEGIN
        SET @OrderBy = ' CreatedDate DESC ';
                END
                DECLARE @CurrentUserID nvarchar(36) = '', @IsFilterFollowOnly bit = 0;

                SET @CurrentUserID = @CurrentUser;
                SET @TrackStatus = dbo.Func_ReplaceParamFilter(@TrackStatus);
                SET @DocStatus = dbo.Func_ReplaceParamFilter(@DocStatus);
                SET @TrackType = dbo.Func_ReplaceParamFilter(@TrackType);
                SET @DocType = dbo.Func_ReplaceParamFilter(@DocType);
                SET @TimeFilter = dbo.Func_ReplaceParamFilter(@TimeFilter);

                IF(@UserDelegate != '')

    BEGIN
        SET @CurrentUser = @UserDelegate;
                SET @CurrentUserID = @UserDelegate

    END


    IF(@UserId IS NOT NULL AND @UserId != '' AND @UserId != CAST(0x0 AS UNIQUEIDENTIFIER))

    BEGIN
        SET @CurrentUserID = @UserId;
                END
                --Khai báo ------------------------------------------------------------------------
                CREATE TABLE #DocTbl (ProjectId uniqueidentifier , StatusString nvarchar(100), DeptID uniqueidentifier);	
	DECLARE @SqlTrack nvarchar(MAX), 
			@SqlFilter nvarchar(200),
			@Sql nvarchar(MAX),
			@TotalRecord int ,
            @TotalPage int,
            @FilerTrackType nvarchar(100) = '';
                --Hết khai báo ------------------------------------------------------------------------

                --Các trường hợp đặc biệt ==================================================================================================================================================================

                IF(@DocStatus = '12')-- Đang soạn

    BEGIN
        INSERT INTO #DocTbl
			SELECT

                Pro.Id, '1-TRANSFER', Pro.DepartmentId
            FROM
                [Task].[Project]
        Pro
            WHERE

                Pro.CreatedBy = @CurrentUserID AND Pro.ProjectStatusId = 12;
	END
    ELSE

    IF(@DocStatus = '1') -- Đang chờ tôi duyệt

    BEGIN

        INSERT INTO #DocTbl
			SELECT

                Pro.Id, '1-TRANSFER', Pro.DepartmentId
            FROM
                [Task].[Project]
        Pro
            WHERE

                Pro.ProjectStatusId = 1 
				AND(Pro.ApprovedBy = @CurrentUserID OR Pro.CreatedBy = @CurrentUserID)

    END
    ELSE

    IF(CHARINDEX('3', @DocType) > 0) -- cong viec dinh ky

    BEGIN
        INSERT INTO #DocTbl
			SELECT ObjectId, CAST(ScheduleType AS VARCHAR(2)) + '-Schedule', NULL
            FROM dbo.Func_Select_Item_CoreSchedule(getdate(),'Document');
	END
    ELSE

    IF(@TimeFilter IS NOT NULL AND CHARINDEX('IsFollowed', @TimeFilter) = 1 AND
        @TrackType = '' AND @TrackStatus = '' AND @DocStatus != '99' AND @DocStatus = '')

    BEGIN
        SET @IsFilterFollowOnly = 1;
		INSERT INTO #DocTbl
			SELECT
                Pro.Id, '1-TRANSFER', Pro.DepartmentId
            FROM

                [Task].[Project] Pro
                INNER JOIN[Task].[ProjectFollow] ProF ON ProF.ProjectId = Pro.Id

            WHERE
                Pro.IsActive = 1 AND ProF.UserId = @CurrentUserID;
        END 
	--Hết các trường hợp đặc biệt ==============================================================================================================================================================

    ELSE
	--Phần query theo DB ====================================================================================================================================================================

    BEGIN 
		-- Base Query Tracking ==================================================================================================================================
		SET @SqlTrack = ' INSERT INTO #DocTbl

        SELECT
            DISTINCT  Track.ProjectId, ''1-TRACK'', NUll
        FROM
            [Task].[TaskItemAssign]
        Track
            INNER JOIN[Task].[Project] Pro ON Pro.Id = Track.ProjectId AND Pro.IsActive = 1
		WHERE
            Track.ProjectId IS NOT NULL  ';


        IF (@FromDate != '' AND @ToDate != '' AND
            @FromDate IS NOT NULL AND @ToDate IS NOT NULL)

        BEGIN
            SET @SqlTrack = @SqlTrack + -- ' AND (Track.CreatedDate BETWEEN ''' + @FromDate + ''' AND '''+ @ToDate + ''' ) ';
			' AND (
				(Track.FromDate >= ''' + @FromDate + ''' AND Track.ToDate <= '''+ @ToDate + ''') OR
               (Track.ToDate >= ''' + @FromDate + ''' AND Track.ToDate <= '''+ @ToDate + ''') OR
              (Track.ToDate >= '''+ @ToDate + '''  AND Track.FromDate <= ''' + @FromDate + ''' )
			 ) '
		END
        ELSE

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND year(Track.FromDate) >= year(getdate()) - 1 ';
		END		

		-- Conditions @UserType =================================================================================================================================
		-- 0: assignto, 1: approved, 2: assignedby
        IF(@DepartmentId != '' AND @DepartmentId IS NOT NULL)

        BEGIN
            SET @SqlTrack = @SqlTrack +  ' AND Track.DepartmentID = ''' + @DepartmentId + ''' ';
		END

        IF(@UserId != '' AND @UserId IS NOT NULL)

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND Track.AssignTo = ''' + @UserId + ''' ';
		END
        ELSE

        IF(@UserType = 0 AND (@DepartmentId = '' OR @DepartmentId IS NULL) )
		BEGIN
            IF(@TrackStatus = '')

            BEGIN
                SET @SqlTrack = @SqlTrack + ' AND 
				(
                    Track.AssignTo = ''' + @CurrentUserID + '''
					OR EXISTS(SELECT TOP 1 Ti.Id FROM [Task].[TaskItem] Ti WHERE Ti.Id = Track.TaskItemId  AND Ti.AssignBy = ''' + @CurrentUserID + '''  )
				 )';
			END
            ELSE

            BEGIN
                SET @SqlTrack = @SqlTrack + ' AND 
				(
                    Track.AssignTo = ''' + @CurrentUserID + ''' 
				 )';
			END
        END

        ELSE
        IF(@UserType = 1 AND (@DepartmentId = '' OR @DepartmentId IS NULL))
		BEGIN
            SET @SqlTrack = '';
		END
        ELSE

        IF(@UserType = 2 AND (@DepartmentId = '' OR @DepartmentId IS NULL))
		BEGIN
            SET @SqlTrack = @SqlTrack + ' AND EXISTS
			(SELECT Top 1 TrackBy.Id FROM[Task].[TaskItem] TrackBy WHERE TrackBy.AssignBy = ''' + @CurrentUserID + ''' AND TrackBy.Id = Track.TaskItemId ';

           IF (@TrackStatus = '6')

            BEGIN
                SET @SqlTrack = @SqlTrack + ' AND ( TrackBY.AssignBy = Track.AssignFollow OR Track.AssignFollow IS NULL) ';
			END
            SET @SqlTrack = @SqlTrack + ' ) ';
		END
			-- Conditions @TrackType ==============================================================================================================================
		-- '1,2,3': tat ca, 1: XLC, 2: PH, 3: ĐB,
		IF(@TrackType != '' AND @TrackType != '-1' AND @TrackType != '1,2,3')

        BEGIN
            IF(CHARINDEX('4', @TrackType) > 0 ) -- BC
            BEGIN

                SET @SqlTrack = @SqlTrack + ' AND (Track.TaskType = 6 OR Track.IsReport = 1) ';
        END
        ELSE

            IF(CHARINDEX('5', @TrackType) > 0 ) -- dg
            BEGIN

                SET @SqlTrack = @SqlTrack + ' AND (Track.TaskItemStatusId = 2) ';
        END
        ELSE

            BEGIN
                SET @FilerTrackType = '';
				IF(CHARINDEX('1', @TrackType) > 0 ) -- XLC
                BEGIN

                    SET @FilerTrackType = @FilerTrackType + '1,2,6,0,5,';
        END

        IF(CHARINDEX('2', @TrackType) > 0 ) -- PH
        BEGIN

                    SET @FilerTrackType = @FilerTrackType + '3,';
        END

        IF(CHARINDEX('3', @TrackType) > 0 ) -- ĐB
        BEGIN

                    SET @FilerTrackType = @FilerTrackType + '7,';
        END
        SET @FilerTrackType = @FilerTrackType + '9'
				SET @SqlTrack = @SqlTrack + ' AND Track.TaskType IN (' + @FilerTrackType + ') ';

        END
    END
		
		-- End Conditions @TrackType ==========================================================================================================================
				 
		-- End Conditions @ProcessType ===========================================================================================================================
		  

		-- Conditions @UserType ==================================================================================================================================
		-- 0: tat ca, -1: trong han, -2: qua han, > 3 den han(processtype se ngay den han)

        IF(@ProcessType = -1) -- dung han

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, getdate()) = 0 ';
		END
        ELSE

        IF(@ProcessType = -2) -- tre han

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, getdate()) = 1 ';
		END
        ELSE

        IF(@ProcessType > 0) -- den han x ngay

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate ,Track.TaskItemStatusId, dateadd(DAY, 2, getdate())) = 1
			AND dbo.Func_Doc_CheckIsOverDue(Track.ToDate, Track.FinishedDate , Track.TaskItemStatusId, getdate()) = 0
			 ';
		END
		-- End Conditions @ProcessType ==================================================================================================================================		

		-- Conditions @TimeFilter ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc

        IF(@TimeFilter != '' AND CHARINDEX('priority', @TimeFilter) = 0 AND CHARINDEX('bookid', @TimeFilter) = 0
		 AND CHARINDEX('IsBroadCast', @TimeFilter) = 0 AND CHARINDEX('IsFollow', @TimeFilter) = 0)
		BEGIN
            SET @SqlTrack = @SqlTrack + ' AND Track.CreatedDate IN (' + @TimeFilter + ') ';
		END

        IF(@DocStatus = 99 AND CHARINDEX('IsFollow', @TimeFilter) > 0)
		BEGIN
            SET @SqlTrack = @SqlTrack + ' AND Track.TaskItemStatusId in (0,1) '
		END 

		-- End Conditions @TimeFilter ==========================================================================================================================
		-- Conditions @TrackStatus ==============================================================================================================================
		-- 0: moi, 1: dang xu ly, 2: bao cao, 3: huy, 4: ket thuc

        IF(@TrackStatus != '' AND @TrackStatus != '-1')

        BEGIN
            SET @SqlTrack = @SqlTrack + ' AND Track.TaskItemStatusId IN (' + @TrackStatus + ') '; 
		END
		-- End Conditions @TrackStatus ==========================================================================================================================
	END

	--print @SqlTrans
	--print @SqlTrack;

        IF(@DocType = '2')

    BEGIN

        IF(@TrackStatus = '6')

        BEGIN
            SET @SqlTrack = @SqlTrack + ' OR (Track.AssignFollow = ''' + @CurrentUserID + ''' AND Track.TaskItemStatusId = 6) ';
		END
    END

    print @sqlTRACK;
        EXEC sp_executesql @SqlTrack;

	--IF(@TrackStatus = '' AND @DocStatus = '' AND (CHARINDEX('IsFollowed', @TimeFilter) < 1 OR @TimeFilter IS NULL   ))
	--BEGIN
	--	INSERT INTO #DocTbl
	--		SELECT Pro.Id, '1-TRACK', NUll
	--		FROM
	--			[Task].[Project] Pro
	--		WHERE
	--			Pro.IsActive = 1
    --          AND (Pro.CreatedBy = @CurrentUserID OR Pro.ApprovedBy = @CurrentUserID)
	--			AND NOT EXISTS(SELECT TOP 1 tbl.StatusString FROM #DocTbl Tbl WHERE Tbl.ProjectId = Pro.Id);
	--END

	--Filter lại theo @DocType, @ExternalType, @DocStatus, @PrivateFolder, @Category
    CREATE TABLE #Results (Idx int, ProjectId uniqueidentifier, DocStatus nvarchar(50), DeptID uniqueidentifier);	 
	BEGIN

        SET @Sql = '

        INSERT INTO #Results
			SELECT
                ROW_NUMBER() OVER (ORDER BY Pro.' + @OrderBy + ')
			   ,Pro.Id
			   ,TPro.StatusString
			   ,Pro.DepartmentId
            FROM
				#DocTbl TPro
				INNER JOIN[Task].[Project] Pro ON TPro.ProjectId = Pro.Id ';
	
		 SET @Sql = @Sql + '	

            WHERE
                Pro.IsActive = 1 '; 

		 --SET @Sql = @Sql + @SqlFilter;
        END

        PRINT @Sql;
	EXEC sys.sp_executesql @Sql; 
	 -- lấy số trang
    SET @TotalRecord = (
        SELECT MAX(Idx)
        FROM #Results);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);

	-- lấy dữ liệu phân trang
    IF(@IsCount = 0)

    BEGIN
        SELECT
			--DT.ProjectId ,
			(CASE
               WHEN DT.DocStatus LIKE '%-TRACK%' THEN dbo.Func_GetStringTaskItemStatus(DT.ProjectId, @CurrentUserID)
               ELSE DT.DocStatus
             END)
			+ (CASE WHEN ProF.ProjectId IS NOT NULL THEN '; isfollowed' ELSE '' END)

             AS ProjectTotalStatus,
			--DT.DeptID,
			@TotalPage TotalPage,
            @TotalRecord TotalRecord,
			P.*

        FROM
			 #Results DT
			 left join[Task].[ProjectFollow] ProF ON ProF.UserId = @CurrentUserID
            AND ProF.ProjectId = DT.ProjectId
            INNER JOIN[Task].Project P ON P.Id = DT.ProjectId
		--WHERE
		--	@Page< 0 
		--	OR (
		--	DT.Idx BETWEEN (@Page - 1) * @PageSize + 1 AND(@Page - 1) * @PageSize + @PageSize )
		--WHERE 
		--	dbo.Func_GetStringTaskItemStatus(DT.ProjectId, @CurrentUserID) NOT LIKE '4-TRACKING%'
		ORDER BY

            P.ProjectPriorityId DESC,
            CASE

                WHEN CHARINDEX('overdue',
                    (CASE
                        WHEN DT.DocStatus LIKE '%-TRACK%' THEN dbo.Func_GetStringTaskItemStatus(DT.ProjectId, @CurrentUserID)
                        ELSE DT.DocStatus
                        END)
						+ (CASE WHEN ProF.ProjectId IS NOT NULL THEN '; isfollowed' ELSE '' END)) > 0 
					 THEN 0
				ELSE 1
			END, 
			P.ToDate
        OFFSET(@Page - 1) * @PageSize ROWS

        FETCH NEXT @PageSize ROWS ONLY;
			
	END
    ELSE

    BEGIN
        IF(@TotalRecord IS NULL)

            SET @TotalRecord = 0;
        IF(@TotalPage IS NULL)

            SET @TotalPage = 0;
        SELECT
            CAST(	'00000000-0000-0000-0000-000000000000' as uniqueidentifier ) Id
		   ,'' AS ProjectTotalStatus
           , CAST(	'00000000-0000-0000-0000-000000000000' as uniqueidentifier ) DepartmentId
		   ,@TotalPage TotalPage
           , @TotalRecord TotalRecord; 
	END

    DROP TABLE #Results
	DROP TABLE #DocTbl;
END TRY
BEGIN CATCH
-- Whoops, there was an error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION
-- Raise an error with the details of the exception
 DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

 SELECT @ErrMsg = ERROR_MESSAGE(),
 @ErrSeverity = ERROR_SEVERITY()


    DROP TABLE #Results
	DROP TABLE #DocTbl;
 RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH




GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Select_Projects_With_Filter] 
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
	print @ProTaskSqlTrack
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
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter_By_Folder]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-28
-- Description:	Lây danh sách project theo folder
-- =============================================
CREATE   PROCEDURE [dbo].[SP_Select_Projects_With_Filter_By_Folder] 
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
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter_Kaban]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		nttrung
-- Create date: 2020-07-13
-- Description:	Create new filter store procedure
-- [SP_Select_Projects_With_Filter] '0eae4e5b-56b0-40bd-91e2-06b4b52e2ee8', 0
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_Projects_With_Filter_Kaban] 
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
	@TaskItemPriorityId VARCHAR(10)			= null,
	@TaskItemNatureId int			= -1,
	@ProjectHashtag	NVARCHAR(255)		= '',
	@TaskHashtag	NVARCHAR(255)		= '',
	@IsReport			VARCHAR(10)			= '',
	@IsWeirdo			VARCHAR(10)			= '',
	@Page				INT					= 1,
	@PageSize			INT					= 10,
	@OrderBy	  		NVARCHAR(50)			= 'CreatedDate DESC',
	@IsCount			BIT					= 0,
	@IsFullControl		BIT					= 0
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
								' AND  TItem.TaskItemStatusId = ' + @TaskItemStatusId + ' AND (TItem.IsGroupLabel Is null or TItem.IsGroupLabel = 0 ) ';
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
								' AND TIAssign.TaskType = ' + @TaskType + '';
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
								' AND (TItem.TaskItemPriorityId = ' + @TaskItemPriorityId + ')';
	END

	--natureTask
	IF @TaskItemNatureId >= 0
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.NatureTaskId = ' + @TaskItemNatureId + ')';
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
	print(@ProTaskSqlTrack);
	EXEC sp_executesql @ProTaskSqlTrack;

--	-- Neu lay project
--	IF (@ParentId = '')
--	BEGIN

--		SET @ProSqlTrack = 
--				'INSERT INTO #ProTbl
--				select ProjectId from (SELECT ProjectId
--				FROM #ProTaskTbl) AS T3
--GROUP BY ProjectId'
--				;

--		EXEC sp_executesql @ProSqlTrack;

--		-- Get info Project from #ProTbl
--		SET @SqlTrack = 
--				'INSERT INTO #DocTbl
--				SELECT 
--					Pro.Id AS [Id],
--					Pro.Id As [ProjectId],
--					''00000000-0000-0000-0000-000000000000'' AS [ParentId],
--					Pro.Summary AS [Name], 
--					''project'' AS [Type],
--					ProStatus.Code AS [Status],
--					CASE 
--						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate <= Pro.ToDate THEN ''in-due-date''
--						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate >  Pro.ToDate THEN ''out-of-date''
--						WHEN Pro.ProjectStatusId != 4 AND Pro.ToDate       >= GETDATE()  THEN ''in-due-date''
--					ELSE ''out-of-date'' 
--					END AS [Process],
--					[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
--					Pro.FromDate AS [FromDate],
--					Pro.ToDate AS [ToDate],
--					Pro.ApprovedBy AS [ApprovedBy],
--					Pro.ApprovedBy AS [AssignBy],
--					NULL AS [UserId],
--					Pro.DepartmentId AS [DepartmentId],
--					ProStatus.Name AS [StatusName]
--				FROM #ProTbl TempTbl
--				LEFT JOIN [Task].[Project] Pro ON TempTbl.ProjectId = Pro.Id
--				LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
--				ORDER BY Pro.CreatedDate DESC, Pro.Summary';

--		EXEC sp_executesql @SqlTrack;

--	END
--	-- Neu lay task
--	ELSE
--	BEGIN

--		SET @ProSqlTrack = 
--				'INSERT INTO #ProTbl
--				SELECT DISTINCT ProjectId
--				FROM #ProTaskTbl';

--		EXEC sp_executesql @ProSqlTrack;

--		SET @TaskSqlTrack = 
--				'INSERT INTO #TempTItemTbl
--				SELECT DISTINCT TaskItemId
--				FROM #ProTaskTbl';

--		EXEC sp_executesql @TaskSqlTrack;

--		-- Insert vào @TaskItemId danh sách TaskItemId có quan hệ
--		DECLARE @TaskItemId NVARCHAR(200);
--		DECLARE MY_CURSOR CURSOR
--		LOCAL STATIC READ_ONLY FORWARD_ONLY
--		FOR
--		SELECT TaskItemId FROM #TempTItemTbl

--		OPEN MY_CURSOR
--		FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
--		WHILE @@FETCH_STATUS = 0
--		BEGIN 
--			--Do something with Id here
--			WITH tbParent AS
--			(
--				SELECT Id, ParentId 
--				FROM [Task].[TaskItem]
--				WHERE Id = @TaskItemId  and IsDeleted = 0
				
--				UNION ALL

--				SELECT TItem.Id, TItem.ParentId
--				FROM [Task].[TaskItem] TItem
--				JOIN tbParent 
--				ON TItem.Id = tbParent.ParentId
--				where TItem.IsDeleted = 0
--			)

--			INSERT INTO #TempTwoTItemTbl
--			SELECT Id AS Id
--			FROM tbParent

--			FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
--		END
--		CLOSE MY_CURSOR
--		DEALLOCATE MY_CURSOR

--		-- Đưa TaskItemId đã được lọc duplicate vào trong #TItemTbl
--		INSERT INTO #TItemTbl
--		SELECT DISTINCT *
--		FROM #TempTwoTItemTbl
--		SET @SqlTrack =
--			'INSERT INTO #DocTbl
--				SELECT TItem.Id AS [Id],
--					TItem.ProjectId AS [ProjectId],
--					TItem.ParentId AS [ParentId],
--					TItem.TaskName AS [Name],
--					CASE 
--						WHEN TItem.IsGroupLabel = 1 THEN ''group''
--						ELSE ''''
--						END AS [Type],
--					TIStatus.Code AS [Status],
--					CASE
--						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate <= TItem.ToDate THEN ''in-due-date''
--						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate >  TItem.ToDate THEN ''out-of-date''
--						WHEN TItem.TaskItemStatusId != 4 AND TItem.ToDate       >= GETDATE()    THEN ''in-due-date''
--						ELSE ''out-of-date''
--						END AS [Process],
--					[dbo].[Func_CheckChildrenOfTaskItem](TItem.Id) AS [HasChildren],
--					TItem.FromDate AS [FromDate],
--					TItem.ToDate AS [ToDate],
--					''00000000-0000-0000-0000-000000000000'' AS [ApprovedBy],
--					TItem.AssignBy AS [AssignBy],
--					[dbo].[Func_GetPriorityUserIdByTaskItemId](TItem.Id) AS [UserId],
--					TItem.DepartmentId AS [DepartmentId],
--					TIStatus.Name AS [StatusName]
--				FROM [Task].[TaskItem] AS TItem
--				LEFT JOIN [Task].[TaskItemStatus] AS TIStatus
--				ON TItem.TaskItemStatusId = TIStatus.Id 
--				WHERE TItem.TaskName IS NOT NULL ';

--		IF (EXISTS (SELECT Id FROM [Task].[Project] WHERE Id = @ParentId))
--		BEGIN
--			SET @SqlTrack = @SqlTrack +
--							' AND (TItem.ProjectId = ''' + @ParentId + ''' 
--								AND (TItem.ParentId IS NULL OR TItem.ParentId = ''00000000-0000-0000-0000-000000000000'')) ';
--		END
--		ELSE
--		BEGIN
--			SET @SqlTrack = @SqlTrack + 
--							' AND (TItem.ParentId = ''' + @ParentId + ''')';
--		END

--		SET @SqlTrack = @SqlTrack + 
--				' AND TItem.Id IN (SELECT TaskItemId FROM #TItemTbl)';

	
--		SET @SqlTrack = @SqlTrack +
--						' ORDER BY TItem.[Order], TItem.CreatedDate';
--		Print @SqlTrack;
--		EXEC sp_executesql @SqlTrack;

--	END

--	-- END: AFTER CHANGING

--	-- lấy số trang
--	SET @TotalRecord = (SELECT COUNT(*) FROM #DocTbl);
--	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);

--	-- lấy dữ liệu phân trang
--	IF (@IsCount = 1 AND @ParentId = '')
--	BEGIN
--		IF (@TotalRecord IS NULL)
--			SET @TotalRecord = 0;
--		IF (@TotalPage IS NULL)
--			SET @TotalPage = 0;



--		SELECT Pro.Id AS [Id],
--			Pro.Summary AS [Name], 
--			Pro.Id As [ProjectId],
--			'project' AS [Type],
--			CASE 
--				WHEN ProjectStatusId = 4 THEN 'ProjectStatusId' 
--				ELSE 'not-finish' END AS [Status],
--			[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
--			Pro.FromDate AS [FromDate],
--			Pro.ToDate AS [ToDate],
--			Pro.ApprovedBy AS [ApprovedBy],
--			Pro.DepartmentId AS [DepartmentId],
--			ProStatus.Name AS [StatusName],
--			@TotalPage AS [TotalPage],
--			@TotalRecord AS [TotalRecord]
--		FROM #DocTbl TempPro
--		LEFT JOIN [Task].[Project] Pro ON TempPro.Id = Pro.Id
--		LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
--		ORDER BY Pro.CreatedDate DESC, Pro.Summary
--		OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
--		FETCH NEXT @PageSize ROWS ONLY;

--	END
--	ELSE IF @ParentId = ''
--	BEGIN
--		SELECT 
--			Doc.*,
--			p.PercentFinish AS [PercentFinish],
--			p.UserViews [UserViews],
--			(SELECT Count(ti.Id) FROM Task.TaskItem ti
--				WHERE ti.ProjectId = Doc.Id AND (ti.IsGroupLabel is null Or ti.IsGroupLabel = 0) AND (ti.AssignBy = @CurrentUserId Or (select Count(tia.Id) from Task.TaskItemAssign tia where tia.ProjectId = ti.Id AND tia.AssignTo = @CurrentUserId) > 0)) As CountTask,
--			@TotalPage AS [TotalPage],
--			@TotalRecord AS [TotalRecord]
--		FROM #DocTbl Doc INNER JOIN Task.Project p ON p.Id = Doc.Id
--		ORDER BY p.CreatedDate DESC, p.Summary
--	END
--	ELSE
--	BEGIN
--		SELECT 
--			Doc.*,
--			p.IsGroupLabel,
--			CASE
--				WHEN p.PercentFinish <= 0 THEN (Select top 1 PercentFinish From Task.TaskItemAssign tia where tia.TaskType = 1 AND tia.TaskItemId = Doc.Id)
--				ELSE p.PercentFinish
--			END as PercentFinish,
--			p.Conclusion as Content,
--			(SELECT Count(ti.Id) FROM Task.TaskItem ti
--				WHERE ti.ParentId = Doc.Id) As CountTask,
--			(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
--				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
--			(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
--				WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
--			@TotalPage AS [TotalPage],
--			@TotalRecord AS [TotalRecord]

--		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
--		ORDER BY p.[Order], p.CreatedDate
--	END
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
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter_Schedule]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_Select_Projects_With_Filter_Schedule] 
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
				Pro.IsActive = 1 and TItem.IsDeleted = 0 and TItem.IsGroupLabel =0
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
				Pro.IsActive = 1 and TItem.IsGroupLabel =0' 
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
		-- FromDate Condition
	IF (@StartDate!='' or @EndDate !='' )
	BEGIN
		SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND ( (TIAssign.FromDate <= ''' + @StartDate + '''And TIAssign.ToDate >=''' + @EndDate + ''')
								or  (TItem.FromDate <= ''' + @StartDate + ''' And TItem.ToDate >=''' + @EndDate + ''')
											or  (Pro.FromDate <= ''' + @StartDate + '''And Pro.ToDate >=''' + @EndDate + '''))' ;
										
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
	if (@ParentId <> '')	
	begin
				SET @ProTaskSqlTrack = @ProTaskSqlTrack +
								' AND (TItem.ProjectId = ''' + @ParentId + ''')';
	end
	print(@ProTaskSqlTrack);
	EXEC sp_executesql @ProTaskSqlTrack;

--INSERT INTO #TItemTbl
--		SELECT DISTINCT *
--		FROM #TempTwoTItemTbl

--	-- Neu lay project
--	IF (@ParentId = '')
--	BEGIN

--		SET @ProSqlTrack = 
--				'INSERT INTO #ProTbl
--				select ProjectId from (SELECT ProjectId
--				FROM #ProTaskTbl) AS T3
--GROUP BY ProjectId'
--				;

--		EXEC sp_executesql @ProSqlTrack;

--		-- Get info Project from #ProTbl
--		SET @SqlTrack = 
--				'INSERT INTO #DocTbl
--				SELECT 
--					Pro.Id AS [Id],
--					Pro.Id As [ProjectId],
--					''00000000-0000-0000-0000-000000000000'' AS [ParentId],
--					Pro.Summary AS [Name], 
--					''project'' AS [Type],
--					ProStatus.Code AS [Status],
--					CASE 
--						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate <= Pro.ToDate THEN ''in-due-date''
--						WHEN Pro.ProjectStatusId  = 4 AND Pro.FinishedDate >  Pro.ToDate THEN ''out-of-date''
--						WHEN Pro.ProjectStatusId != 4 AND Pro.ToDate       >= GETDATE()  THEN ''in-due-date''
--					ELSE ''out-of-date'' 
--					END AS [Process],
--					[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
--					Pro.FromDate AS [FromDate],
--					Pro.ToDate AS [ToDate],
--					Pro.ApprovedBy AS [ApprovedBy],
--					Pro.ApprovedBy AS [AssignBy],
--					NULL AS [UserId],
--					Pro.DepartmentId AS [DepartmentId],
--					ProStatus.Name AS [StatusName]
--				FROM #ProTbl TempTbl
--				LEFT JOIN [Task].[Project] Pro ON TempTbl.ProjectId = Pro.Id
--				LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
--				ORDER BY Pro.CreatedDate DESC, Pro.Summary';

--		EXEC sp_executesql @SqlTrack;

--	END
--	-- Neu lay task
--	ELSE
--	BEGIN

--		SET @ProSqlTrack = 
--				'INSERT INTO #ProTbl
--				SELECT DISTINCT ProjectId
--				FROM #ProTaskTbl';

--		EXEC sp_executesql @ProSqlTrack;

--		SET @TaskSqlTrack = 
--				'INSERT INTO #TempTItemTbl
--				SELECT DISTINCT TaskItemId
--				FROM #ProTaskTbl';

--		EXEC sp_executesql @TaskSqlTrack;

--		-- Insert vào @TaskItemId danh sách TaskItemId có quan hệ
--		DECLARE @TaskItemId NVARCHAR(200);
--		DECLARE MY_CURSOR CURSOR
--		LOCAL STATIC READ_ONLY FORWARD_ONLY
--		FOR
--		SELECT TaskItemId FROM #TempTItemTbl

--		OPEN MY_CURSOR
--		FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
--		WHILE @@FETCH_STATUS = 0
--		BEGIN 
--			--Do something with Id here
--			WITH tbParent AS
--			(
--				SELECT Id, ParentId 
--				FROM [Task].[TaskItem]
--				WHERE Id = @TaskItemId  and IsDeleted = 0
				
--				UNION ALL

--				SELECT TItem.Id, TItem.ParentId
--				FROM [Task].[TaskItem] TItem
--				JOIN tbParent 
--				ON TItem.Id = tbParent.ParentId
--				where TItem.IsDeleted = 0
--			)

--			INSERT INTO #TempTwoTItemTbl
--			SELECT Id AS Id
--			FROM tbParent

--			FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
--		END
--		CLOSE MY_CURSOR
--		DEALLOCATE MY_CURSOR

--		-- Đưa TaskItemId đã được lọc duplicate vào trong #TItemTbl
--		INSERT INTO #TItemTbl
--		SELECT DISTINCT *
--		FROM #TempTwoTItemTbl
--		SET @SqlTrack =
--			'INSERT INTO #DocTbl
--				SELECT TItem.Id AS [Id],
--					TItem.ProjectId AS [ProjectId],
--					TItem.ParentId AS [ParentId],
--					TItem.TaskName AS [Name],
--					CASE 
--						WHEN TItem.IsGroupLabel = 1 THEN ''group''
--						ELSE ''''
--						END AS [Type],
--					TIStatus.Code AS [Status],
--					CASE
--						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate <= TItem.ToDate THEN ''in-due-date''
--						WHEN TItem.TaskItemStatusId  = 4 AND TItem.FinishedDate >  TItem.ToDate THEN ''out-of-date''
--						WHEN TItem.TaskItemStatusId != 4 AND TItem.ToDate       >= GETDATE()    THEN ''in-due-date''
--						ELSE ''out-of-date''
--						END AS [Process],
--					[dbo].[Func_CheckChildrenOfTaskItem](TItem.Id) AS [HasChildren],
--					TItem.FromDate AS [FromDate],
--					TItem.ToDate AS [ToDate],
--					''00000000-0000-0000-0000-000000000000'' AS [ApprovedBy],
--					TItem.AssignBy AS [AssignBy],
--					[dbo].[Func_GetPriorityUserIdByTaskItemId](TItem.Id) AS [UserId],
--					TItem.DepartmentId AS [DepartmentId],
--					TIStatus.Name AS [StatusName]
--				FROM [Task].[TaskItem] AS TItem
--				LEFT JOIN [Task].[TaskItemStatus] AS TIStatus
--				ON TItem.TaskItemStatusId = TIStatus.Id 
--				WHERE TItem.TaskName IS NOT NULL ';

--		--IF (EXISTS (SELECT Id FROM [Task].[Project] WHERE Id = @ParentId))
--		--BEGIN
--		--	SET @SqlTrack = @SqlTrack +
--		--					' AND (TItem.ProjectId = ''' + @ParentId + ''' 
--		--						AND (TItem.ParentId IS NULL OR TItem.ParentId = ''00000000-0000-0000-0000-000000000000'')) ';
--		--END
--		--ELSE
--		--BEGIN
--		--	SET @SqlTrack = @SqlTrack + 
--		--					' AND (TItem.ParentId = ''' + @ParentId + ''')';
--		--END
--		if (@ParentId <> '')	
--		begin
--					SET @SqlTrack = @SqlTrack + 
--							' AND (TItem.ProjectId = ''' + @ParentId + ''')';
--		end	

--		SET @SqlTrack = @SqlTrack + 
--				' AND TItem.Id IN (SELECT TaskItemId FROM #TItemTbl)';

	
--		SET @SqlTrack = @SqlTrack +
--						' ORDER BY TItem.[Order], TItem.CreatedDate';
--		Print @SqlTrack;
--		EXEC sp_executesql @SqlTrack;

--	END

	-- END: AFTER CHANGING

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

		if (@ParentId <> '')	
		begin
					SET @SqlTrack = @SqlTrack + 
							' AND (TItem.ProjectId = ''' + @ParentId + ''')';
		end	

		SET @SqlTrack = @SqlTrack + 
				' AND TItem.Id IN (SELECT TaskItemId FROM #ProTaskTbl)';

	
		SET @SqlTrack = @SqlTrack +
						' ORDER BY TItem.[Order], TItem.CreatedDate';
		Print @SqlTrack;
		EXEC sp_executesql @SqlTrack;

	-- lấy số trang
	SET @TotalRecord = (SELECT COUNT(*) FROM #DocTbl);
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);


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
			@TotalRecord AS [TotalRecord]

		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		ORDER BY p.[Order], p.CreatedDate
	-- lấy dữ liệu phân trang
	--IF (@IsCount = 1 AND @ParentId = '')
	--BEGIN
	--	IF (@TotalRecord IS NULL)
	--		SET @TotalRecord = 0;
	--	IF (@TotalPage IS NULL)
	--		SET @TotalPage = 0;



	--	SELECT Pro.Id AS [Id],
	--		Pro.Summary AS [Name], 
	--		Pro.Id As [ProjectId],
	--		'project' AS [Type],
	--		CASE 
	--			WHEN ProjectStatusId = 4 THEN 'ProjectStatusId' 
	--			ELSE 'not-finish' END AS [Status],
	--		[dbo].[Func_CheckChildrenOfProject](Pro.Id) AS [HasChildren],
	--		Pro.FromDate AS [FromDate],
	--		Pro.ToDate AS [ToDate],
	--		Pro.ApprovedBy AS [ApprovedBy],
	--		Pro.DepartmentId AS [DepartmentId],
	--		ProStatus.Name AS [StatusName],
	--		@TotalPage AS [TotalPage],
	--		@TotalRecord AS [TotalRecord]
	--	FROM #DocTbl TempPro
	--	LEFT JOIN [Task].[Project] Pro ON TempPro.Id = Pro.Id
	--	LEFT JOIN [Task].[ProjectStatus] ProStatus ON Pro.ProjectStatusId = ProStatus.Id
	--	ORDER BY Pro.CreatedDate DESC, Pro.Summary
	--	OFFSET     (@Page - 1) * @PageSize ROWS       -- skip 10 rows
	--	FETCH NEXT @PageSize ROWS ONLY;

	--END
	--ELSE IF @ParentId = ''
	--BEGIN
	--	SELECT 
	--		Doc.*,
	--		p.PercentFinish AS [PercentFinish],
	--		p.UserViews [UserViews],
	--		(SELECT Count(ti.Id) FROM Task.TaskItem ti
	--			WHERE ti.ProjectId = Doc.Id AND (ti.IsGroupLabel is null Or ti.IsGroupLabel = 0) AND (ti.AssignBy = @CurrentUserId Or (select Count(tia.Id) from Task.TaskItemAssign tia where tia.ProjectId = ti.Id AND tia.AssignTo = @CurrentUserId) > 0)) As CountTask,
	--		@TotalPage AS [TotalPage],
	--		@TotalRecord AS [TotalRecord]
	--	FROM #DocTbl Doc INNER JOIN Task.Project p ON p.Id = Doc.Id
	--	ORDER BY p.CreatedDate DESC, p.Summary
	--END
	--ELSE
	--BEGIN
	--	SELECT 
	--		Doc.*,
	--		p.IsGroupLabel,
	--		CASE
	--			WHEN p.PercentFinish <= 0 THEN (Select top 1 PercentFinish From Task.TaskItemAssign tia where tia.TaskType = 1 AND tia.TaskItemId = Doc.Id)
	--			ELSE p.PercentFinish
	--		END as PercentFinish,
	--		p.Conclusion as Content,
	--		(SELECT Count(ti.Id) FROM Task.TaskItem ti
	--			WHERE ti.ParentId = Doc.Id) As CountTask,
	--		(SELECT Top 1 tia.AssignTo FROM Task.TaskItemAssign tia
	--			WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignTo,
	--		(SELECT Top 1 tia.DepartmentId FROM Task.TaskItemAssign tia
	--			WHERE tia.TaskItemId = Doc.Id and tia.TaskType = 1) As AssignToDeparmentId,
	--		@TotalPage AS [TotalPage],
	--		@TotalRecord AS [TotalRecord]

	--	FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
	--	ORDER BY p.[Order], p.CreatedDate
	--END
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
/****** Object:  StoredProcedure [dbo].[SP_Select_Projects_With_Filter_Test]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Select_Projects_With_Filter_Test] 
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
/****** Object:  StoredProcedure [dbo].[SP_Select_RecursiveDeptId]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_RecursiveDeptId]
	@ParentId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	  WITH  CTE_Dept
                        AS
                        (
	                        SELECT ID, Name
	                        FROM Departments
	                        WHERE 
		                        ID = @ParentId 
		                        AND IsActive = 1  
		                        AND IsShow = 1
	                        UNION ALL
	                        SELECT D.ID, D.Name
	                        FROM
		                         Departments D
		                        INNER JOIN CTE_Dept CD ON CD.ID = D.ParentID
	                        WHERE
		                        D.IsActive = 1
		                        AND D.IsShow = 1

                        )

                        SELECT ID
                        FROM CTE_Dept
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_RequestTotal_Module_UserNameFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Select_RequestTotal_Module_UserNameFromDateToDate]
	@UserName nvarchar(200),
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
 
	SELECT
		tbModuleOrgUser.ModuleID
	   ,tbModule.Description ModuleName
	   ,SUM(tbModuleOrgUser.RequestTotal) AS 'RequestTotal'
	FROM 
		WebAnalysis_Date_Module_OrgUser tbModuleOrgUser
		LEFT JOIN Modules tbModule ON tbModuleOrgUser.ModuleID = tbModule.ID
	WHERE 
		tbModuleOrgUser.UserName = @UserName
		AND @FromDate <= DATEFROMPARTS(tbModuleOrgUser.Year, tbModuleOrgUser.Month, tbModuleOrgUser.Date)
		AND @ToDate > DATEFROMPARTS(tbModuleOrgUser.Year, tbModuleOrgUser.Month, tbModuleOrgUser.Date)
	GROUP BY tbModuleOrgUser.ModuleID
			,tbModule.Description
	ORDER BY 
		RequestTotal DESC;
	
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_RequestTotal_User_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Select_RequestTotal_User_OrgParentIDFromDateToDate] 
	@OrgParentID uniqueidentifier,
	@FromDate datetime,
	@ToDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		tbUserInfo.UserName
	   ,tbUserInfo.FullName
	   ,SUM(tbOrgUser.RequestTotal) AS 'RequestTotal'
	FROM 
		WebAnalysis_Date_OrgUser tbOrgUser
		INNER JOIN Users tbUserInfo	ON tbOrgUser.UserName = tbUserInfo.UserName
		INNER JOIN Departments tbOrg ON tbOrgUser.OrgID = tbOrg.ID
	WHERE 
		tbOrgUser.OrgID = @OrgParentID
		AND tbOrg.IsActive = 1
		AND @FromDate <= DATEFROMPARTS(tbOrgUser.Year, tbOrgUser.Month, tbOrgUser.Date)
		AND @ToDate > DATEFROMPARTS(tbOrgUser.Year, tbOrgUser.Month, tbOrgUser.Date)
	GROUP BY tbUserInfo.UserName
			,tbUserInfo.FullName;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Task by_Projects_With_Filter]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Select_Task by_Projects_With_Filter] 
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
	@IsFullControl		BIT					= 0
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
				Pro.IsActive = 1 and TItem.IsDeleted = 0
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
				where TItem.IsDeleted = 0
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
	ELSE IF @ParentId = ''
	BEGIN
		SELECT 
			Doc.*,
			p.PercentFinish AS [PercentFinish],
			p.UserViews [UserViews],
			(SELECT Count(ti.Id) FROM Task.TaskItem ti
				WHERE ti.ProjectId = Doc.Id AND (ti.IsGroupLabel is null Or ti.IsGroupLabel = 0) AND (ti.AssignBy = @CurrentUserId Or (select Count(tia.Id) from Task.TaskItemAssign tia where tia.ProjectId = ti.Id AND tia.AssignTo = @CurrentUserId) > 0)) As CountTask,
			@TotalPage AS [TotalPage],
			@TotalRecord AS [TotalRecord]
		FROM #DocTbl Doc INNER JOIN Task.Project p ON p.Id = Doc.Id
		ORDER BY p.CreatedDate DESC, p.Summary
	END
	ELSE
	BEGIN
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
			@TotalRecord AS [TotalRecord]

		FROM #DocTbl Doc INNER JOIN Task.TaskItem p ON p.Id = Doc.Id
		ORDER BY p.[Order], p.CreatedDate
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
/****** Object:  StoredProcedure [dbo].[SP_Select_Task_MultiFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_Select_Task_MultiFilters]
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
	@DepartmentId	NVARCHAR(36)	= '',
	@UserId		NVARCHAR(36)	= '',
	@UserHandoverId NVARCHAR(36) = '',
	@StatusTime int = 0,
	@TaskItemStatusId nvarchar(10) = '',
	@AssignTo nvarchar(36) = '',
	@AssignBy nvarchar (36) ='',
	@IsFullControl int = 0,
	@TaskItemPriorityId VARCHAR(10)			= null,
	@TaskItemNatureId int			= -1,
	@ProjectHashtag	NVARCHAR(255)		= '',
	@TaskHashtag	NVARCHAR(255)		= '',
	@IsReport			VARCHAR(10)			= '',
	@IsWeirdo			VARCHAR(10)			= '',
	@TaskType			VARCHAR(10)			= '',
	@ProjectId nvarchar(36) = ''
AS

--SELECt @CurrentDate = '2019-12-16 08:17', @KeyWord = N'',
--@CurrentUser = dbo.Func_GetUserIDByLoginName('dxg\thaidt'),
--@OrderBy = 'CreatedDate DESC', @IsCount = '0', @Page = '1', 
--@PageSize = '15' ,@TrackStatus = '6' ,@DocType = '2', @UserType = '2'
BEGIN TRY
	 DECLARE @sql nvarchar(max) = ''
	 -- Lay danh project ban dau
	--if @IsFullControl = 1
	--	Set @sql = 'Select Distinct TItem.Id from task.TaskItemAssign TIAssign 
	--				FULL OUTER JOIN Task.TaskItem TItem on TItem.Id = TIAssign.TaskItemId
	--				FULL OUTER JOIN Task.Project Pro on Pro.Id = TItem.ProjectId
	--				where Pro.IsActive = 1 AND (TItem.IsGroupLabel <> 1 OR TItem.IsGroupLabel IS NULL) '
	--else
		Set @sql = 'Select Distinct TItem.Id from task.TaskItemAssign TIAssign 
				FULL OUTER JOIN Task.TaskItem TItem on TItem.Id = TIAssign.TaskItemId
				FULL OUTER JOIN Task.Project Pro on Pro.Id = TItem.ProjectId
				where Pro.IsActive = 1 AND (TItem.IsGroupLabel <> 1 OR TItem.IsGroupLabel IS NULL)
				AND (Pro.ApprovedBy = ''' + @CurrentUser + ''' 
					OR Pro.CreatedBy = ''' + @CurrentUser + '''
					OR Pro.UserViews like N''%' + @CurrentUser + '%''
					OR TItem.AssignBy = ''' + @CurrentUser + '''
					OR (TIAssign.TaskItemId IS NOT NULL 
						AND TIAssign.TaskItemId != ''00000000-0000-0000-0000-000000000000''
						AND (TIAssign.AssignTo = ''' + @CurrentUser + '''
								OR TIAssign.AssignFollow = ''' + @CurrentUser + '''))) AND TItem.TaskItemStatusId <> 3 AND TItem.TaskItemStatusId <> 4 '
	IF (@ProjectId != '')
	BEGIN
		SET @sql = @sql + ' AND Pro.ID = ''' + @ProjectId + ''' '
	END
	-- Keyword Condition
	IF (@Keyword != '') 
	BEGIN
		SET @sql = @sql +
								' AND (Pro.Summary like N''%' + @Keyword + '%''
										OR TItem.TaskName like N''%' + @Keyword + '%'')';
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
		SET @sql = @sql + ' AND  TItem.TaskItemStatusId = ' + @TaskItemStatusId + ' ';
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
		SET @sql = @sql + ' AND TIAssign.TaskType = ' + @TaskType + ' ';
	END

	-- mặc định
	IF (@StatusTime = 0)
	BEGIN
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

	-- Priority
	IF (@TaskItemPriorityId IS NOT NULL)
	BEGIN
		SET @sql = @sql + ' AND (TItem.TaskItemPriorityId = ' + @TaskItemPriorityId + ')';
	END

	--natureTask
	IF @TaskItemNatureId >= 0
	BEGIN
		SET @sql = @sql + ' AND (TItem.NatureTaskId = ' + @TaskItemNatureId + ')';
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
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_TaskItem_Children]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_TaskItem_Children] 
                                @Id uniqueidentifier
                                AS
                                BEGIN
	                                CREATE TABLE #TempTItemTbl (TaskItemId uniqueidentifier);
	                                DECLARE @TaskItemId NVARCHAR(200);
	                                DECLARE MY_CURSOR CURSOR
	                                LOCAL STATIC READ_ONLY FORWARD_ONLY
	                                FOR
	                                Select Id from Task.TaskItem where id = @Id--'34a197bc-925c-4e0f-89f4-3d2bc46161c1'
	                                OPEN MY_CURSOR
	                                FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
	                                WHILE @@FETCH_STATUS = 0
	                                BEGIN TRY
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
			                                ON TItem.ParentId = tbParent.Id
			                                where TItem.IsDeleted = 0
		                                )

		                                INSERT INTO #TempTItemTbl
		                                SELECT Id AS Id
		                                FROM tbParent

		                                FETCH NEXT FROM MY_CURSOR INTO @TaskItemId
		                                CLOSE MY_CURSOR
		                                DEALLOCATE MY_CURSOR
		                                select distinct t.Id from #TempTItemTbl temp inner join Task.TaskItem t on temp.TaskItemId = t.Id
		                                DROP TABLE #TempTItemTbl;
	                                END TRY
	                                BEGIN CATCH
		                                CLOSE MY_CURSOR
		                                DEALLOCATE MY_CURSOR
		                                DROP TABLE #TempTItemTbl;
	                                END CATCH
                                END

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_TaskItems_ByFilters]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[SP_Select_TaskItems_ByFilters]
	@ProjectId uniqueidentifier,
	@UserId uniqueidentifier = null,
	@PageIndex int = 1, 
	@PageSize int = 2,
	@ViewType int = 0, 
	@ViewStatus int = -1,
	@SpecTaskItemId uniqueidentifier = null
AS
--DECLARE 
--	@ProjectId uniqueidentifier = 'D68D1ED3-0EC1-42DD-B4D5-ECD0FEC4C298',
--	@UserId uniqueidentifier = SurePortal_DatXanh.dbo.Func_GetUseridByLoginName('datxanh\vanvd'),
--	@PageIndex int = 1, @PageSize int = 2,
--	@ViewType int = 0, @ViewStatus int = -1,
--	@SpecTaskItemId uniqueidentifier = null;
BEGIN 
	SET NOCOUNT ON; 
	IF (@PageIndex = 0)
		SET @PageIndex = 1;
	IF (@PageSize = 0)
		SET @PageSize = 2;
	DECLARE @TaskPath TABLE (Idx int, TaskItemId uniqueidentifier, OrderPath nvarchar(200), DeptId uniqueidentifier);
	DELETE @TaskPath;

	DECLARE @DeptIds TABLE (Id uniqueidentifier);
	DELETE @DeptIds;
	INSERT INTO @DeptIds
		SELECT UD.DeptID
		FROM UserDepartments UD 
			INNER JOIN Departments D ON D.ID = UD.DeptID AND D.IsActive = 1
		WHERE @UserId = UD.UserID;


	;WITH #TempTaskItems
	AS
	(
		SELECT 
	 		CAST( ROW_NUMBER() OVER(ORDER BY Ti.CreatedDate) AS NVARCHAR(20)) AS DepthLevel,
			Ti.Id AS TaskItemId
			,Ti.DepartmentId
			--,CAST(Ti.TaskName AS NVARCHAR(MAX)) AS TaskName
			--,CAST(' ' AS NVARCHAR(20)) AS PathLevel
		FROM 
			[Task].TaskItem Ti
		WHERE
			Ti.ProjectId = @ProjectId
			AND (
				@SpecTaskItemId = Ti.Id 
				OR 
				(@SpecTaskItemId IS NULL AND Ti.ParentId = Cast(0x0 as uniqueidentifier)) 		
			)
		UNION ALL
		SELECT
			CAST(Temp.DepthLevel + '/' +
				CAST( ROW_NUMBER() OVER(ORDER BY Ti.CreatedDate) AS NVARCHAR(2))  AS NVARCHAR(20))
			AS DepthLevel, 
			Ti.Id AS TaskItemId
			,Ti.DepartmentId
			--,CAST(( '    ' + Temp.PathLevel + Ti.TaskName) as nvarchar(MAX)) AS TaskName
			--,CAST((Temp.PathLevel + ' ') as nvarchar(20)) AS PathLevel
		FROM 
			[Task].TaskItem Ti
			INNER JOIN #TempTaskItems Temp ON Temp.TaskItemId = Ti.ParentId
	)

	INSERT INTO @TaskPath
		SELECT
			ROW_NUMBER() OVER(ORDER BY DepthLevel) AS Idx, TaskItemId, DepthLevel, DepartmentId
		FROM #TempTaskItems
		ORDER BY DepthLevel;

	DECLARE @FinalTemp TABLE (Idx int, TaskItemId uniqueidentifier);
	DELETE @FinalTemp;

	INSERT INTO @FinalTemp
		SELECT ROW_NUMBER() OVER(ORDER BY TB.Idx), TB.TaskItemId
		FROM 
		(
			SELECT DISTINCT 
				TP.Idx,
			  TP.TaskItemId
			FROM 
				@TaskPath TP
				INNER JOIN Task.TaskItem TI ON TI.Id = TP.TaskItemId
				INNER JOIN Task.TaskItemAssign TIA ON TIA.TaskItemId = TP.TaskItemId
			WHERE
				(
					(@ViewType = 0)
					OR
					-- department
					(@ViewType = 1 AND 
						(TI.DepartmentId IN (SELECT Id FROM @DeptIds) 
						OR 
						TIA.DepartmentId IN  (SELECT Id FROM @DeptIds) )
					) 
					OR
					-- user
					(@ViewType = 2 AND 
						(TI.CreatedBy = @UserId OR TI.AssignBy = @UserId OR TIA.AssignTo = @UserId)
					)
				)
				AND
				(
					@ViewStatus = -1
					OR
					(TI.TaskItemStatusId = @ViewStatus OR TIA.TaskItemStatusId = @ViewStatus)
				)
		) TB; 

	DECLARE @TotalRecord int = 0, @TotalPage int = 0;
	SET @TotalRecord = ( SELECT MAX(Idx) FROM @FinalTemp); 
	SET @TotalPage = dbo.Func_GetTotalPage(@TotalRecord, @PageSize);

	SELECT  
		@TotalPage TotalPage,  
		@TotalRecord TotalRecord,
		TI.*
	FROM
		Task.TaskItem TI
		INNER JOIN  @FinalTemp TP ON TP.TaskItemId = TI.Id
	WHERE
		TP.Idx BETWEEN (@PageIndex - 1) * @PageSize + 1 AND (@PageIndex - 1) * @PageSize + @PageSize 
	ORDER BY 
		TP.Idx;
	

END




GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_InDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_InDate]
	@ToDay Date
AS
BEGIN
	SET NOCOUNT ON;
	Declare @Year int, @Month int, @Date int;
	SET @Year = YEAR(@ToDay);
	SET @Month = MONTH(@ToDay);
	SET @Date = DAY(@ToDay);
    -- Insert statements for procedure here
	SELECT
		Year
	   ,Month
	   ,Date
	   ,UserName
	   ,SUM(RequestTotal) AS RequestTotal
	   ,SUM(MobileRequestTotal) AS MobileRequestTotal
	FROM WebAnalysis_Date
	WHERE Year = @Year
	AND Month = @Month
	AND Date = @Date
	GROUP BY Year
			,Month
			,Date
			,UserName;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_InMonth]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_InMonth]
	@Year int,
	@Month int
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT
		Year
	   ,Month
	   ,Date
	   ,SUM(RequestTotal) AS RequestTotal
	   ,SUM(MobileRequestTotal) AS MobileRequestTotal
	FROM WebAnalysis_Date
	WHERE Year = @Year
	AND Month = @Month
	GROUP BY Year
			,Month
			,Date;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_Module_FromDateToDateByUserName]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_Module_FromDateToDateByUserName]
	@FromDate date,
	@ToDate date,
	@UserName nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT
		t1.Year
	   ,t1.Month
	   ,t1.Date
	   ,t1.UserName
	   ,t1.RequestTotal
	   ,t1.ModuleID
	   ,t2.Description ModuleName
	FROM 
		WebAnalysis_Date_Module t1
		LEFT JOIN Modules t2 ON t1.ModuleID = t2.ID
	WHERE 
		@FromDate <= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
		AND @ToDate >= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
		AND UserName = @UserName
	ORDER BY 
		t2.Description;
END



GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_Module_InDateByUserName]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_Module_InDateByUserName]
	@ToDay Date,
	@UserName nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
   Declare @Year int, @Month int, @Date int
		Set @Year=YEAR(@ToDay)
		Set @Month= MONTH(@ToDay)
		Set @Date=DAY(@ToDay) 
    -- Insert statements for procedure here
	SELECT
		t1.Year
	   ,t1.Month
	   ,t1.Date
	   ,t1.UserName
	   ,t1.RequestTotal
	   ,t1.ModuleID
	   ,t2.Description ModuleName
	FROM 
		WebAnalysis_Date_Module t1
		LEFT JOIN Modules t2
		ON t1.ModuleID = t2.ID
	WHERE Year = @Year
	AND Month = @Month
	AND Date = @Date
	AND UserName = @UserName
	ORDER BY t2.Description;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_Org_OrgParentIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_Org_OrgParentIDFromDateToDate]
--DECLARE
	@OrgParentID uniqueidentifier ,
	@FromDate date,
	@ToDay date
AS
BEGIN
	SET NOCOUNT ON;
	IF (@OrgParentID IS NULL OR @OrgParentID = '00000000-0000-0000-0000-000000000000')
	BEGIN
		SELECT
			t1.Year
		   ,t1.Month
		   ,t1.Date
		   ,t1.ParentID
		   ,t1.OrgID
		   ,t2.Name OrgName
		   ,t1.MobileRequestTotal
		   ,t1.RequestTotal
		   ,t1.OnlineTotal
		FROM 
			WebAnalysis_Date_Org t1
			INNER JOIN Departments t2 ON t1.OrgID = t2.ID
		WHERE 
			t1.ParentID = @OrgParentID
			AND t2.IsActive = 1
			AND @FromDate <= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
			AND @ToDay >= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
			AND t1.RequestTotal > 0
		ORDER BY t2.OrderNumber DESC;
	END
	ELSE
	BEGIN
		SELECT
			YEAR(@ToDay) [Year]
		   ,MONTH(@ToDay) [Month]
		   ,DAY(@ToDay) [Day]
		   ,@OrgParentID ParentID
		   ,t1.OrgID
		   ,t2.Name OrgName
		   ,SUM(t1.MobileRequestTotal) AS [MobileRequestTotal]
		   ,SUM(t1.RequestTotal) AS [RequestTotal]
		   ,SUM(t1.OnlineTotal) AS [OnlineTotal]
		FROM 
			WebAnalysis_Date_Org t1
			INNER JOIN Departments t2 ON t1.OrgID = t2.ID
		WHERE 
			t1.ParentID = @OrgParentID
			AND t2.IsActive = 1
			AND @FromDate <= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
			AND @ToDay >= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
			AND t1.RequestTotal > 0
		GROUP BY 
			t1.OrgID
			,t2.Name 
			,t2.OrderNumber
		ORDER BY 
			t2.OrderNumber DESC;
	END
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_Org_OrgParentIDInDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_Org_OrgParentIDInDate]
	@OrgParentID uniqueidentifier,
	@ToDay date
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT
		t1.Year
	   ,t1.Month
	   ,t1.Date
	   ,t1.RequestTotal
	   ,t1.MobileRequestTotal
	   ,t1.OnlineTotal
	   ,t1.OrgID
	   ,t2.Name OrgName
	FROM 
		WebAnalysis_Date_Org t1
		INNER JOIN Departments t2
		ON t1.OrgID = t2.ID
	WHERE 
		t1.ParentID = @OrgParentID
		AND @ToDay = DATEFROMPARTS(Year, Month, Date)
		AND t1.RequestTotal > 0
	ORDER BY 
		t2.OrderNumber;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_OrgUser_OrgIDFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_OrgUser_OrgIDFromDateToDate]
--DECLARE
	@OrgID uniqueidentifier,-- = '90fce8fe-398c-4dcb-b809-68cde1c2e6e0',
	@FromDate date,-- = '2016-10-25',
	@ToDay date-- = '2016-10-25'
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		YEAR(@ToDay) [Year]
	   ,MONTH(@ToDay) [Month]
	   ,DAY(@ToDay) [Date]
	   ,t1.UserName + '_' + t2.FullName UserName
	   ,t2.FullName
	   ,SUM(t1.RequestTotal) AS [RequestTotal]
	   ,SUM(t1.MobileRequestTotal) AS [MobileRequestTotal]
	   ,@OrgID OrgID
	   ,t2.FullName OrgName
	FROM 
		WebAnalysis_Date_OrgUser t1
		INNER JOIN Users t2
		ON t1.UserName = t2.UserName
		--join Orgs t3 on t1.OrgID=t3.OrgID
	WHERE 
		t1.OrgID = @OrgID
		AND @FromDate <= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
		AND @ToDay >= DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
		AND t1.RequestTotal > 0
	GROUP BY t1.UserName
			,t2.FullName--,t1.OrgID,t3.OrgName 
	ORDER BY t1.UserName;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_Date_OrgUser_OrgIDInDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_Date_OrgUser_OrgIDInDate]
	@OrgID uniqueidentifier,
	@ToDay date
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT
		t1.Year
	   ,t1.Month
	   ,t1.Date
	   ,t1.UserName
	   ,t2.FullName
	   ,t1.RequestTotal
	   ,t1.MobileRequestTotal
	   ,t1.OrgID
	   ,t3.Name OrgName
	FROM 
		WebAnalysis_Date_OrgUser t1
		JOIN Users t2
			ON t1.UserName = t2.UserName
		JOIN Departments t3
			ON t1.OrgID = t3.ID
	WHERE 
		t1.OrgID = @OrgID
		AND @ToDay = DATEFROMPARTS(t1.Year, t1.Month, t1.Date)
		AND t1.RequestTotal > 0
	ORDER BY 
		t2.FullName;
END


GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_UserNameFromDateToDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_Select_WebAnalysis_UserNameFromDateToDate]
	@UserName nvarchar(50),
	@FromDate date,
	@ToDate date
AS
BEGIN
	SET NOCOUNT ON;
    
	SELECT
		Url
	   ,COUNT(*) AS Total
	FROM WebAnalysis
	WHERE UserName = @UserName
	AND CONVERT(DATE, RequestDateTime) BETWEEN @FromDate AND @ToDate
	GROUP BY Url;
END



GO
/****** Object:  StoredProcedure [dbo].[SP_Select_WebAnalysis_UserNameInDate]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_Select_WebAnalysis_UserNameInDate]
	@UserName nvarchar(50),
	@ToDay date
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		Url
	   ,COUNT(*) AS Total
	FROM WebAnalysis
	WHERE UserName = @UserName
	AND CONVERT(DATE, RequestDateTime) = @ToDay
	GROUP BY Url;

END


GO
/****** Object:  StoredProcedure [dbo].[SP_StatisticReportUserDeparmentTasks]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_StatisticReportUserDeparmentTasks]
--DECLARE
	@FromDate	datetime,
	@ToDate		datetime,
	@UserId		nvarchar(36) = NULL,
	@DeptIds	nvarchar(max) = NULL,
	@TypeCount	nvarchar(30) = 'number'
AS
--SELECT 
--	@FromDate=N'2020-01-01',
--	@ToDate=N'2020-01-04',
--	@UserId= null, --N'6e457e97-fcaa-4b5c-b6cc-dd2bd9ab3b76',
--	@DeptIds=N'1b5a83ce-78a9-4e28-82c6-a9637b119560',
--	@TypeCount=N'number' 
BEGIN
	SET NOCOUNT ON;

	DECLARE @TotalTask	int = 0,
			@Sql nvarchar(MAX),
			@SqlFilter nvarchar(1000);
	-- tạo bảng danh sách các công của các phòng ban cần query
	CREATE TABLE #TempTrack 
	(
		TaskItemId		 uniqueidentifier,
		ProjectId		 uniqueidentifier,
		TaskItemAssignId uniqueidentifier,
		DeptId			 uniqueidentifier,
		FromDate		 datetime,
		ToDate			 datetime,
		FinishedDate	 datetime,
		TrackStatus		 int
	);
	SET @SqlFilter = '';
	IF ((@UserId IS NOT NULL AND @UserId != '') AND (@DeptIds IS NOT NULL AND @DeptIds != ''))
	BEGIN
	 SET @SqlFilter= 'AND Track.AssignTo = '''+ @UserId +''' AND Track.DepartmentId IN (SELECT items FROM dbo.Func_SplitTextToTable('''+@DeptIds+''', '';'') )';
	END
	ELSE
	IF (@UserId IS NOT NULL AND @UserId != '')
	BEGIN
		SET @SqlFilter= 'AND Track.AssignTo = '''+ @UserId +''' ';	
	END
	ELSE
	IF (@DeptIds IS NOT NULL AND @DeptIds != '')
	BEGIN 
	    SET @SqlFilter= 'AND Track.DepartmentId IN (SELECT items FROM dbo.Func_SplitTextToTable('''+@DeptIds+''', '';'') )';
	END

	SET @Sql = 'INSERT INTO #TempTrack
					SELECT
						Track.TaskItemId,
						Track.ProjectId,
						Track.ID,
						Track.DepartmentId,
						Track.FromDate,
						Track.ToDate,
						Track.FinishedDate,
						Track.TaskItemStatusId
					FROM 
						Task.TaskItemAssign Track
						INNER JOIN Task.Project Pro ON Pro.Id = Track.ProjectId AND Pro.IsActive = 1
					WHERE
						Track.FromDate BETWEEN '''+ CONVERT(NVARCHAR(20), @FromDate, 120) +''' AND '''+ CONVERT(NVARCHAR(20), @ToDate, 120) +''' ' + @SqlFilter;
	EXEC sys.sp_executesql @Sql;
	SELECT @TotalTask = COUNT(DISTINCT TaskItemId) FROM #TempTrack;
	--select * from #TempTrack;
	PRINT N'1. Báo cáo công việc hoàn thành';
	DECLARE @TotalDays Int = DATEDIFF(DAY, @FromDate, @ToDate), @IdxTime int = 1, @MaxTime int = 0;
	DECLARE @TempCountTbl TABLE(TimeLabel nvarchar(20), TotalFinished int, Total int);
	DELETE @TempCountTbl;
	PRINT @TotalDays;
	IF (@TotalDays > 365 OR YEAR(@FromDate) != YEAR(@ToDate))
	BEGIN
		SELECT @IdxTime = YEAR(@FromDate), @MaxTime = YEAR(@ToDate); 
		IF (@TypeCount = 'number')
		BEGIN
			WHILE @IdxTime <= @MaxTime
			BEGIN 		 
				INSERT INTO @TempCountTbl
				VALUES(
					'Y.' + CAST(@IdxTime AS NVARCHAR(4)) ,
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE YEAR(FinishedDate) <= @IdxTime),
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE YEAR(ToDate) <= @IdxTime)); 
				SET @IdxTime = @IdxTime + 1;
			END
		END
	END
	ELSE
	IF (@TotalDays > 90)
	BEGIN 
		SELECT @IdxTime = DATEPART(QUARTER,@FromDate), @MaxTime =  DATEPART(QUARTER,@ToDate); 
		IF (@TypeCount = 'number')
		BEGIN
			WHILE @IdxTime <= @MaxTime
			BEGIN 
				INSERT INTO @TempCountTbl
				VALUES(
					'Q.' + CAST(@IdxTime AS NVARCHAR(2)) ,
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE DATEPART(QUARTER,FinishedDate) <= @IdxTime),
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE DATEPART(QUARTER,ToDate) <= @IdxTime )); 
				SET @IdxTime = @IdxTime + 1;
			END
		END
	END
	ELSE
	IF (@TotalDays > 31)
	BEGIN 
		SELECT @IdxTime = MONTH(@FromDate), @MaxTime = MONTH(@ToDate); 
		IF (@TypeCount = 'number') -- put here take 1 statement, if push in while n-loop statement
		BEGIN 
			WHILE @IdxTime <= @MaxTime
			BEGIN 
				INSERT INTO @TempCountTbl
				VALUES(
					'M.' + CAST(@IdxTime AS NVARCHAR(2)) + '-' + CAST(Year(@FromDate) AS NVARCHAR(4) ) ,
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE MONTH(FinishedDate) <= @IdxTime),
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE MONTH(ToDate) <= @IdxTime)
					); 
				SET @IdxTime = @IdxTime + 1;
			END
		END
	END 
	ELSE
	BEGIN 
		SELECT @IdxTime = 1, @MaxTime = @TotalDays; 
		DECLARE @RunDate date = null; 
		IF (@TypeCount = 'number')
		BEGIN
			WHILE @IdxTime <= @MaxTime
			BEGIN 
				SET @RunDate = DATEADD(DAY, @IdxTime - 1, @FromDate);
				INSERT INTO @TempCountTbl
				VALUES(
					  CONVERT(nvarchar,@RunDate, 103),
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE CAST(FinishedDate AS DATE) <= @RunDate),
					(SELECT COUNT(DISTINCT TaskItemId) FROM #TempTrack WHERE CAST(FromDate AS DATE) <= @RunDate)); 
				SET @IdxTime = @IdxTime + 1;
			END
		END
	END

	SELECT *
	FROM 
		@TempCountTbl
	WHERE TotalFinished >= 0  OR Total >= 0
	

	PRINT N'2. Phân loại công việc chia theo phòng ban';
	IF (@TypeCount = 'number')
	BEGIN
			SELECT  
				DeptId, 
				COUNT(DISTINCT TaskItemId) As TotalTask,
				SUM(DISTINCT (CASE WHEN #TempTrack.FinishedDate IS NOT NULL THEN 1 ELSE 0 END)) AS TotalFinish
			FROM #TempTrack 
			GROUP BY DeptId
		--SELECT *
		--FROM 
		--(
		--	SELECT  
		--		DeptId, 
		--		'TaskFinished' AS TaskType, 
		--		COUNT(DISTINCT TaskItemId) As TotalTask
		--	FROM #TempTrack
		--	WHERE FinishedDate IS NOT NULL
		--	GROUP BY DeptId
		--	UNION
		--	SELECT  
		--		DeptId, 
		--		'TotalTask' AS TaskType, 
		--		COUNT(DISTINCT TaskItemId) As TotalTask
		--	FROM #TempTrack 
		--	GROUP BY DeptId
		--) TB
		--ORDER BY DeptId, TaskType
	END
	
	PRINT N'3. Tình trạng công việc';
	IF (@TypeCount = 'number')
	BEGIN
		SELECT 
			Task.TaskItemStatusId,
			COUNT(Task.Id) AS TotalTask
		FROM Task.TaskItem Task
		WHERE Task.Id IN (SELECT DISTINCT TaskItemId FROM #TempTrack)
		GROUP BY Task.TaskItemStatusId
		ORDER BY Task.TaskItemStatusId;
	END
	PRINT N'4. Tiến độ công việc';
	IF (@TypeCount = 'number')
	BEGIN 
		SELECT 
		SUM(DISTINCT (CASE WHEN Task.FinishedDate IS NOT NULL THEN 1 ELSE 0 END)) AS TotalFinish,
		@TotalTask AS TotalTask,
		SUM(CASE WHEN Task.ToDate < GETDATE() AND (Task.FinishedDate IS NULL OR Task.ToDate < Task.FinishedDate) THEN 1 ELSE 0 END) AS OverDue
		FROM Task.TaskItem Task
		WHERE Task.Id IN (SELECT DISTINCT TaskItemId FROM #TempTrack);
	END
	DROP TABLE #TempTrack;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_TASK_APPRAISE_BY_USER]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TASK_APPRAISE_BY_USER]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@ProjectId uniqueidentifier,
	@StatusId int null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select distinct Id from Task.TaskItem 
	where ProjectId = @ProjectId and AssignBy = @CurrentUserId
	and IsDeleted = 0
	and (@StatusId is null or TaskItemStatusId = @StatusId);
	
END

GO
/****** Object:  StoredProcedure [dbo].[SP_TASK_BY_TASK_PARENT]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TASK_BY_TASK_PARENT]
	@parentId NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	WITH tbChild AS
			(
				SELECT Id
				FROM [Task].[TaskItem]
				WHERE Id = @parentId and IsDeleted = 0
				
				UNION ALL

				SELECT TItem.Id
				FROM [Task].[TaskItem] TItem
				JOIN tbChild 
				ON TItem.ParentId = tbChild.Id where TItem.IsDeleted = 0
			)
	select distinct Id from tbChild
END

GO
/****** Object:  StoredProcedure [dbo].[SP_TASK_PARENT_OF_ASSIGNER]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TASK_PARENT_OF_ASSIGNER]
	-- Add the parameters for the stored procedure here
	@CurrentUserId uniqueidentifier,
	@ProjectId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	create table #temp (id uniqueidentifier);
    insert into #temp
	select distinct TaskItemId from Task.TaskItemAssign 
	where ProjectId = @ProjectId and 
	AssignTo = @CurrentUserId
	;
	WITH tbParent AS
			(
				SELECT Id, ParentId
				FROM [Task].[TaskItem]
				WHERE Id in (select Id from #temp) and IsDeleted = 0
				
				UNION ALL

				SELECT TItem.Id, TItem.ParentId
				FROM [Task].[TaskItem] TItem
				JOIN tbParent 
				ON TItem.Id = tbParent.ParentId
				where TItem.IsDeleted = 0
			)
			select distinct Id from tbParent
	drop table #temp;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]
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
/****** Object:  StoredProcedure [dbo].[SP_Update_Avartar]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Avartar]
	@UserName nvarchar(200),
	@Avartar varbinary(max)
AS
BEGIN
	SET NOCOUNT ON;

    Update Users
	SET Avartar	 = @Avartar
	WHERE UserName = @UserName
END

GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_TASK_RANGE_DATE]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_UPDATE_TASK_RANGE_DATE]
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

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Date]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Date]
	@ToDay Date
AS
BEGIN TRY
	BEGIN TRANSACTION 
	SET NOCOUNT ON;
	Declare @Year int, @Month int, @Date int;
	SET @Year = YEAR(@ToDay);
	SET @Month = MONTH(@ToDay);
	SET @Date = DAY(@ToDay);
	     
	DELETE WebAnalysis_Date
	WHERE Year = @Year
		AND Month = @Month
		AND Date = @Date;

	INSERT WebAnalysis_Date
		SELECT
			YEAR(@ToDay) AS Year
		   ,MONTH(@ToDay) AS Month
		   ,DAY(@ToDay) AS Date
		   ,UserName
		   ,COUNT(*) AS RequestTotal
		   ,SUM(CONVERT(INT, IsMobileDevice)) AS MobileRequestTotal		   
		   ,dbo.Func_GetUserIDByLoginName(t1.UserName) UserID
		FROM 
			WebAnalysis t1
			INNER JOIN ModuleUrl t2
				ON t1.Url LIKE N'%' + t2.Url 
		WHERE 
			DAY(RequestDateTime) = @Date
			AND MONTH(RequestDateTime) = @Month
			AND YEAR(RequestDateTime) = @Year
		GROUP BY UserName;
	COMMIT TRANSACTION
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
END CATCH;

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Date_Module]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Date_Module]
	@ToDay Date
AS
BEGIN TRY
	BEGIN TRANSACTION 
	SET NOCOUNT ON;
	Declare @Year int, @Month int, @Date int;
	SET @Year = YEAR(@ToDay);
	SET @Month = MONTH(@ToDay);
	SET @Date = DAY(@ToDay);
 
	DELETE WebAnalysis_Date_Module
	WHERE Year = @Year
		AND Month = @Month
		AND Date = @Date;

	INSERT WebAnalysis_Date_Module
		SELECT
			YEAR(@ToDay) AS Year
		   ,MONTH(@ToDay) AS Month
		   ,DAY(@ToDay) AS Date
		   ,t1.UserName
		   ,t2.ModuleID
		   ,COUNT(*) AS RequestTotal
		   ,dbo.Func_GetUserIDByLoginName(t1.UserName) UserID
		FROM 
			WebAnalysis t1
			INNER JOIN ModuleUrl t2
			ON t1.Url LIKE N'%' + t2.Url  
		WHERE 
			DAY(RequestDateTime) = @Date
			AND MONTH(RequestDateTime) = @Month
			AND YEAR(RequestDateTime) = @Year
		GROUP BY t1.UserName
				,t2.ModuleID;
COMMIT TRANSACTION
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
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Date_Module_OrgUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Update_WebAnalysis_Date_Module_OrgUser]
	@ToDay Date
AS
BEGIN TRY
	BEGIN TRANSACTION  
	SET NOCOUNT ON;
	
	DELETE WebAnalysis_Date_Module_OrgUser
	WHERE Year = YEAR(@ToDay)
		AND Month = MONTH(@Today)
		AND Date = DAY(@Today);

	INSERT WebAnalysis_Date_Module_OrgUser
		SELECT
			w.Year
		   ,w.Month
		   ,w.Date
		   ,w.UserName
		   ,w.ModuleID
		   ,o.DeptID OrgID
		   ,w.RequestTotal
		   ,o.UserID
		FROM 
			WebAnalysis_Date_Module w
			INNER JOIN UserDepartments o
			ON dbo.Func_GetUserIDByLoginName( w.UserName )= o.UserID
			INNER JOIN Departments og
			ON og.ID = o.DeptID
		WHERE og.IsActive = 1
		AND w.Year = YEAR(@ToDay)
		AND w.Month = MONTH(@Today)
		AND w.Date = DAY(@Today);  

COMMIT TRANSACTION
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
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Date_Org]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Date_Org]
	@ToDay Date
AS
BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON;
	
	Declare @Year int,@Month int,@Day int;
	SET @Year = YEAR(@ToDay);
	SET @Month = MONTH(@Today);
	SET @Day = DAY(@Today);

	DELETE WebAnalysis_Date_Org
	WHERE Year = @Year
		AND Month = @Month
		AND Date = @Day;
	
	SELECT
		YEAR
	   ,Month
	   ,Date
	   ,o.ID OrgID
	   ,o.ParentID
	   ,SUM(RequestTotal) AS UserRequestTotal
	   ,SUM(RequestTotal) AS RequestTotal
	   ,SUM(MobileRequestTotal) AS UserMobileRequestTotal
	   ,SUM(MobileRequestTotal) AS MobileRequestTotal
	   ,COUNT(DISTINCT UserName) AS UserOnlineTotal
	   ,COUNT(DISTINCT UserName) AS OnlineTotal INTO #TempTable
	FROM 
		WebAnalysis_Date_OrgUser w
		INNER JOIN Departments o
		ON w.OrgID = o.ID
	WHERE 
		Year = @Year
		AND Month = @Month
		AND Date = @Day
	GROUP BY YEAR
			,Month
			,Date
			,o.ID
			,o.ParentID;

	INSERT #TempTable
		SELECT
			Year = @Year
		   ,Month = @Month
		   ,Date = @Day
		   ,t1.ID OrgID
		   ,ParentID
		   ,0
		   ,0
		   ,0
		   ,0
		   ,0
		   ,0
		FROM 
			Departments t1
		WHERE NOT EXISTS (SELECT
				OrgID
			FROM #TempTable t2
			WHERE t1.ID = t2.OrgID);
	
	-- lấy râ tất cả node lá
	SELECT
		* INTO #RootList
	FROM #TempTable t1
	WHERE NOT EXISTS (SELECT
			*
		FROM #TempTable t2
		WHERE t1.OrgID = t2.ParentID)
	ORDER BY ParentID;

	DECLARE @IsStop bit = 0;
	WHILE(@IsStop=0)
		BEGIN
			
			-- Lấy ra danh sách cha của nốt lá để chạy tiếp
			SELECT DISTINCT
				ROW_NUMBER() OVER (ORDER BY ParentID) AS Idx
			   ,ParentID INTO #RunOrgs
			FROM #RootList
			GROUP BY ParentID;
			
			DELETE #RootList;
			
			DECLARE @Idx int = 1, @MaxRow int  = 0,@OrgID uniqueidentifier ,@ParentID uniqueidentifier,
			@RequestTotal bigint,@MobileRequestTotal bigint,@OnlineTotal bigint;

			SELECT
				@MaxRow = MAX(Idx)
			FROM #RunOrgs;

			WHILE (@Idx <= @MaxRow)
			BEGIN
				SELECT
					@OrgID = ParentID
				FROM #RunOrgs
				WHERE idx = @Idx;

				SELECT
					@RequestTotal = SUM(RequestTotal)
					,@MobileRequestTotal = SUM(MobileRequestTotal)
					,@OnlineTotal = SUM(OnlineTotal)
				FROM #TempTable
				WHERE ParentID = @OrgID
				GROUP BY ParentID;

				UPDATE #TempTable
				SET RequestTotal = @RequestTotal + UserRequestTotal
					,MobileRequestTotal = @MobileRequestTotal + UserMobileRequestTotal
					,OnlineTotal = @OnlineTotal + UserOnlineTotal
				WHERE OrgID = @OrgID;

				INSERT #RootList
					SELECT
						*
					FROM #TempTable
					WHERE OrgID = @OrgID;

				SET @Idx = @Idx + 1;
					
			END
			
			DROP TABLE #RunOrgs;
			Declare @RowCount int,@RootCount int;
			SET @RowCount = (SELECT
					COUNT(OrgID)
				FROM #RootList);

			SET @RootCount = (SELECT
					COUNT(OrgID)
				FROM #RootList
				WHERE ParentID = '00000000-0000-0000-0000-000000000000');

			IF(@RowCount=@RootCount)
			BEGIN
				SET @IsStop = 1;
			END

		END
	INSERT WebAnalysis_Date_Org
		SELECT
			YEAR
		   ,Month
		   ,Date
		   ,OrgID
		   ,ParentID
		   ,RequestTotal
		   ,MobileRequestTotal
		   ,OnlineTotal
		FROM #TempTable;

	DROP TABLE #TempTable;
	DROP TABLE #RootList;


COMMIT TRANSACTION
END TRY
BEGIN CATCH
	-- Whoops, there was an error
	IF @@TRANCOUNT > 0
	 ROLLBACK TRANSACTION;
	-- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int;

	 SELECT
		 @ErrMsg = ERROR_MESSAGE()
		,@ErrSeverity = ERROR_SEVERITY();
 
	RAISERROR(@ErrMsg, @ErrSeverity, 1);
END CATCH


GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Date_OrgUser]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Date_OrgUser]
	@ToDay Date
AS
BEGIN TRY
	BEGIN TRANSACTION 
	SET NOCOUNT ON;
	
	DELETE WebAnalysis_Date_OrgUser
	WHERE Year = YEAR(@ToDay)
		AND Month = MONTH(@Today)
		AND Date = DAY(@Today);

	INSERT WebAnalysis_Date_OrgUser
		SELECT
			w.Year
		   ,w.Month
		   ,w.Date
		   ,w.UserName
		   ,o.DeptID OrgID
		   ,w.RequestTotal
		   ,w.MobileRequestTotal
		   ,o.UserID
		FROM 
			WebAnalysis_Date w
			INNER JOIN UserDepartments o
				ON dbo.Func_GetUserIDByLoginName( w.UserName) = o.UserID
			INNER JOIN Departments og
				ON og.ID = o.DeptID
		WHERE
			 og.IsActive = 1
			AND w.Year = YEAR(@ToDay)
			AND w.Month = MONTH(@Today)
			AND w.Date = DAY(@Today);

COMMIT TRANSACTION
END TRY
BEGIN CATCH
-- Whoops, there was an error
IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION;
-- Raise an error with the details of the exception
DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int;

 SELECT
	 @ErrMsg = ERROR_MESSAGE()
	,@ErrSeverity = ERROR_SEVERITY();

RAISERROR(@ErrMsg, @ErrSeverity, 1);
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Month]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Month]
	@Year int,
	@Month int
AS
BEGIN TRY
	BEGIN TRANSACTION 
	SET NOCOUNT ON;

	DELETE WebAnalysis_Month
	WHERE Year = @Year
		AND Month = @Month;

	INSERT WebAnalysis_Month
		SELECT
			Year
		   ,Month
		   ,SUM(RequestTotal) AS RequestTotal
		   ,SUM(MobileRequestTotal) AS MobileRequestTotal
		FROM WebAnalysis_Date
		WHERE Year = @Year
		AND Month = @Month
		GROUP BY Year
				,Month;


COMMIT TRANSACTION
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
END CATCH


GO
/****** Object:  StoredProcedure [dbo].[SP_Update_WebAnalysis_Year]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_WebAnalysis_Year]
	@Year int
AS
BEGIN TRY
	BEGIN TRANSACTION 
	SET NOCOUNT ON;
	

    Delete WebAnalysis_Year   WHERE Year=@Year 

	INSERT WebAnalysis_Year
		SELECT
			Year
		   ,SUM(RequestTotal) AS RequestTotal
		   ,SUM(MobileRequestTotal) AS MobileRequestTotal
		FROM WebAnalysis_Month
		WHERE Year = @Year
		GROUP BY Year;

   
COMMIT TRANSACTION
END TRY
BEGIN CATCH
-- Whoops, there was an error
IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION;
-- Raise an error with the details of the exception
DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int;

 SELECT
	 @ErrMsg = ERROR_MESSAGE()
	,@ErrSeverity = ERROR_SEVERITY();

RAISERROR(@ErrMsg, @ErrSeverity, 1);
END CATCH

GO
/****** Object:  StoredProcedure [Task].[SP_Move_Item_In_Table]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Task].[SP_Move_Item_In_Table] 
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
/****** Object:  StoredProcedure [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]    Script Date: 04-05-2021 7:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Task].[SP_TASK_VIEW_BREADCRUMB_WITH_PARENT]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - Daily
1 - Weekly
2 - Monthly
3 - Yearly' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'ScheduleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày bắt đầu của schedule' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày kết thúc của lịch' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số chu kỳ lặp giá trị  1 -> 5
Bao nhiêu ngày/lần
Bao nhiêu tuần/lần
Bao nhiêu tháng/lần
Bao nhiêu năm/lần
' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalFrequently'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Xác định có lặp vào 2 ngày cuối tuần hay không' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalInWeekday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lặp vào thứ nào trong tuần' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalInDayOfWeek'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lặp vào ngày nào trong tháng' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalInDateOfMonth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lặp vào tháng nào của năm' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalInMonthOfYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tuần, Tháng thứ mấy trong tháng/năm' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalOrdinalNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày thứ hai đầu tiên của tháng hoặc của năm' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'Schedule', @level2type=N'COLUMN',@level2name=N'IntervalOrdinalNumberInDayOfWeek'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã hóa' , @level0type=N'SCHEMA',@level0name=N'Core', @level1type=N'TABLE',@level1name=N'UserOtp', @level2type=N'COLUMN',@level2name=N'PIN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'user or group' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AudienceNav', @level2type=N'COLUMN',@level2name=N'AudienceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại sổ. 0: đi, 1: đến' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookDocuments', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sổ đi bên ngoài/nội bộ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookDocuments', @level2type=N'COLUMN',@level2name=N'IsExternal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Thứ tự hiển thị sổ văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookDocuments', @level2type=N'COLUMN',@level2name=N'NoOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sổ văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookDocuments'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: đi, 1: đến' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookTrackingDocuments', @level2type=N'COLUMN',@level2name=N'BookType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: nội bộ, 1: bên ngoài' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BookTrackingDocuments', @level2type=N'COLUMN',@level2name=N'IsExternal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số thứ tự văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'DocNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số ký hiệu văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'SerialNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trích yếu văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nội dung đầy đủ văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'DocDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Người ký văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'SignedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nơi gửi, nơi phát hành văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Sender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nơi nhận văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Receiver'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nơi nhận văn bản nội bộ hệ thống' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'InternalReceiver'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sao gửi văn bản nội bộ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'CarbonCopy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số trang văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Pages'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số bản bộ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Appendix'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tình trạng văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cờ xác định là văn bản đến' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'IsInComing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cờ xác định văn bản nội bộ/bên ngoài' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'IsExternal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày hết hiệu lực văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ExpiredDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày phê duyệt văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Approved'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Người phê duyệt văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ApprovedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày ban hành văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'PublishedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Người ban hành văn bản, chuyển văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'PublishedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày nhận văn bản đến' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ReceivedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'xác định văn bản đã xóa hay không' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'IsActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày bắt đầu xử lý văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'FromDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày kết thúc xử lý văn bản theo kế hoạch' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ToDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày văn bản hoàn thành xử lý' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'FinishedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Độ mật' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Secret'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Độ khẩn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hình thức gửi, lưu dạng 2^...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'SendProvider'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phòng ban soạn thảo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'EditDepartment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bước chạy quy trình đang thực hiện' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'WorkFlowStepID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dữ liệu quy trình chạy theo văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'WorkFlowInfos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Xác văn bản thông báo cho tất cả mọi người' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'IsBroadCast'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trường thông tin mở rộng cho văn bản' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ExtensiveInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phạm vi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Scope'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Khóa đa ngôn ngữ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'NameResourceKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Khóa đa ngôn ngữ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NavNodes', @level2type=N'COLUMN',@level2name=N'NameResourceKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Đường dẫn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NavNodes', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kiểu hiển thị vd: link or folder' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NavNodes', @level2type=N'COLUMN',@level2name=N'ElementType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Thứ tự hiển thị trên web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NavNodes', @level2type=N'COLUMN',@level2name=N'NoOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0,' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SendMailDocuments', @level2type=N'COLUMN',@level2name=N'DocumentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: mới, 1: đã gửi, 2: sai' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SendMailDocuments', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'left,top,right,... vùng binding' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TreeFilterDocuments'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Projects, 1: TaskItem, 2: TaskItemAppraiseHistory, 3: TaskItemProcessHistory' , @level0type=N'SCHEMA',@level0name=N'Task', @level1type=N'TABLE',@level1name=N'Attachment', @level2type=N'COLUMN',@level2name=N'Source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Documents"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 192
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 37
         End
         Begin Table = "StatusDocuments"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 190
               Right = 419
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PriorityDocuments"
            Begin Extent = 
               Top = 6
               Left = 457
               Bottom = 181
               Right = 627
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "SecretDocuments"
            Begin Extent = 
               Top = 6
               Left = 665
               Bottom = 136
               Right = 835
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_ListDocument'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_ListDocument'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "D"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "UD"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "U"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 634
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "JT"
            Begin Extent = 
               Top = 6
               Left = 672
               Bottom = 136
               Right = 842
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserDept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserDept'
GO
USE [master]
GO
ALTER DATABASE [SurePortal_DEV] SET  READ_WRITE 
GO
