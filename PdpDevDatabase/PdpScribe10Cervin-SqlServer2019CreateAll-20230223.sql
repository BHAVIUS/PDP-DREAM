USE [master]
GO
/****** Object:  Database [PdpScribe10Cervin]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE DATABASE [PdpScribe10Cervin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PdpScribe10Cervin', FILENAME = N'D:\Microsoft\SQLD2019\Data\PdpScribe10Cervin.mdf' , SIZE = 65152KB , MAXSIZE = UNLIMITED, FILEGROWTH = 7%)
 LOG ON 
( NAME = N'PdpScribe10CervinLog', FILENAME = N'D:\Microsoft\SQLD2019\Logs\PdpScribe10CervinLog.ldf' , SIZE = 1024KB , MAXSIZE = 125952KB , FILEGROWTH = 7%)
 COLLATE Latin1_General_100_CI_AS
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PdpScribe10Cervin] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PdpScribe10Cervin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PdpScribe10Cervin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ARITHABORT OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [PdpScribe10Cervin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PdpScribe10Cervin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PdpScribe10Cervin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [PdpScribe10Cervin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PdpScribe10Cervin] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [PdpScribe10Cervin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PdpScribe10Cervin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PdpScribe10Cervin] SET  MULTI_USER 
GO
ALTER DATABASE [PdpScribe10Cervin] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [PdpScribe10Cervin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PdpScribe10Cervin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PdpScribe10Cervin] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PdpScribe10Cervin] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PdpScribe10Cervin] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PdpScribe10Cervin', N'ON'
GO
ALTER DATABASE [PdpScribe10Cervin] SET QUERY_STORE = OFF
GO
USE [PdpScribe10Cervin]
GO
/****** Object:  User [NT AUTHORITY\NETWORK SERVICE]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [BUILTIN\Administrators]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE USER [BUILTIN\Administrators] FOR LOGIN [BUILTIN\Administrators]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_owner] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_datareader] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [BUILTIN\Administrators]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [BUILTIN\Administrators]
GO
/****** Object:  UserDefinedFunction [dbo].[CoreConvertPhraseToToken]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CoreConvertPhraseToToken]
( @phrase nvarchar(384) ) RETURNS nvarchar(128)
AS BEGIN

	-- token to be used as tag for alias labels
	
	DECLARE @token nvarchar(128), @charcode tinyint;
	
	SET @token = RTRIM(LTRIM(@phrase));
	-- delete any periods, colons, semicolons, commas, apostrophes, quotes
	SET @token = REPLACE(@token,'.','');
	SET @token = REPLACE(@token,':','');
	SET @token = REPLACE(@token,';','');
	SET @token = REPLACE(@token,',','');
	SET @token = REPLACE(@token,'''','');
	SET @token = REPLACE(@token,'"','');
	-- replace any space with underscore
	SET @token = REPLACE(@token,' ','_');
	-- if first char is digit then prefix with char 'N'
	SET @charcode = ASCII(SUBSTRING(@token,1,1));
	IF (48 <= @charcode) AND (@charcode <= 57)	SET @token = 'N' + @token;

	RETURN @token;

END;
GO
/****** Object:  UserDefinedFunction [dbo].[CoreExtractAcronymFromPhrase]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CoreExtractAcronymFromPhrase]
( @phrase nvarchar(384) ) RETURNS nvarchar(128)
AS BEGIN

	DECLARE @curchar char, @idxchar int, @acronym nvarchar(128), @len int, @pos int, @regex nvarchar(16);
	
	SET @len = LEN(@phrase);
	-- SET @regex = '[A-Z]';
	SET @pos = 1;
	SET @acronym = '';

	WHILE (@len > 0) AND (@pos <= @len) BEGIN
		SET @curchar = SUBSTRING(@phrase,@pos,1);
		SET @idxchar = PATINDEX('[ABCDEFGHIJKLMNOPQRSTUVWXYZ]',@curchar COLLATE Latin1_General_CS_AS);
		IF (@idxchar = 1) SET @acronym = @acronym + @curchar;
		SET @pos = @pos + 1;
	END;
	
	-- TODO: test for zero length from caps and extract acronym from first letter of each word 
	
	IF (LEN(@acronym) > 128) SET @acronym = LEFT(@acronym,128);

	RETURN @acronym;

END;
GO
/****** Object:  Table [dbo].[NpdsCoreFieldFormatEnum]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreFieldFormatEnum](
	[CodeKey] [smallint] NOT NULL,
	[FormatName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[FormatDescription] [nvarchar](128) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_CoreFieldFormatEnum_CodeKey] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CoreFieldFormatItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreFieldFormatItem] 
AS 
SELECT CodeKey, FormatName, FormatDescription 
FROM dbo.NpdsCoreFieldFormatEnum

GO
/****** Object:  Table [dbo].[NpdsCoreInfosetTypeEnum]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreInfosetTypeEnum](
	[CodeKey] [smallint] NOT NULL,
	[TypeName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[TypeDescription] [nvarchar](128) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_CoreInfosetTypeEnum_CodeKey] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CoreInfosetTypeItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreInfosetTypeItem] 
AS 
SELECT CodeKey, TypeName, TypeDescription 
FROM dbo.NpdsCoreInfosetTypeEnum

GO
/****** Object:  Table [dbo].[NpdsCoreEntityTypeEnum]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreEntityTypeEnum](
	[CodeKey] [smallint] NOT NULL,
	[TypeName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[TypeDescription] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[TypeIsComponent] [bit] NOT NULL,
	[TypeIsConstituent] [bit] NOT NULL,
	[TypeEditedByAgent] [bit] NOT NULL,
	[TypeEditedByAuthor] [bit] NOT NULL,
	[TypeEditedByEditor] [bit] NOT NULL,
	[TypeEditedByAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_CoreEntityTypeEnum_CodeKey] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusEntityTypeItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityTypeItem] 
AS 
SELECT CodeKey, TypeName, TypeDescription, TypeIsComponent, TypeIsConstituent,
TypeEditedByAgent, TypeEditedByAuthor, TypeEditedByEditor, TypeEditedByAdmin
FROM dbo.NpdsCoreEntityTypeEnum

GO
/****** Object:  Table [dbo].[NpdsCoreInfosetStatusEnum]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreInfosetStatusEnum](
	[CodeKey] [smallint] NOT NULL,
	[StatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[StatusDescription] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[StatusEditedByAgent] [bit] NOT NULL,
	[StatusEditedByAuthor] [bit] NOT NULL,
	[StatusEditedByEditor] [bit] NOT NULL,
	[StatusEditedByAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_CoreInfosetStatusEnum_CodeKey] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusInfosetStatusItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusInfosetStatusItem] 
AS 
SELECT CodeKey, StatusName, StatusDescription, 
StatusEditedByAgent, StatusEditedByAuthor, StatusEditedByEditor, StatusEditedByAdmin
FROM dbo.NpdsCoreInfosetStatusEnum

GO
/****** Object:  View [dbo].[NexusInfosetTypeItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusInfosetTypeItem] 
AS 
SELECT CodeKey, TypeName, TypeDescription 
FROM dbo.NpdsCoreInfosetTypeEnum

GO
/****** Object:  Table [dbo].[NpdsCoreEntityLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreEntityLabel](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[TagToken] [nvarchar](64) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[LabelUri] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[ServiceTypeCodeRef] [smallint] NOT NULL,
	[EntityLabel] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
 CONSTRAINT [PK_CoreEntityLabel_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_CoreEntityLabel_EntityLabel] UNIQUE NONCLUSTERED 
(
	[EntityLabel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NpdsCoreServiceTypeEnum]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreServiceTypeEnum](
	[CodeKey] [smallint] NOT NULL,
	[TypeName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[TypeDescription] [nvarchar](128) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[DefaultGeneratingLabel] [nvarchar](128) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_NpdsCoreServiceTypeEnum] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NpdsCoreAudit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreAudit](
	[InfosetTypeCodeRef] [smallint] NOT NULL,
	[FieldFormatCodeRef] [smallint] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NULL,
	[FgroupGuidRef] [uniqueidentifier] NULL,
	[AuditGuidKey] [uniqueidentifier] NOT NULL,
	[HasAccess] [bit] NOT NULL,
	[HasIndex] [smallint] NOT NULL,
	[HasPriority] [smallint] NOT NULL,
	[HasToken] [nvarchar](128) COLLATE Latin1_General_100_CI_AS NULL,
	[HasVersion] [timestamp] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[IsCached] [bit] NOT NULL,
	[IsConceptLabel] [bit] NOT NULL,
	[IsConcise] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsExcluding] [bit] NOT NULL,
	[IsGenerating] [bit] NOT NULL,
	[IsLimited] [bit] NOT NULL,
	[IsMarked] [bit] NOT NULL,
	[IsResolvable] [bit] NOT NULL,
	[IsPrincipal] [bit] NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[IsSufficient] [bit] NOT NULL,
	[IsWordPhrase] [bit] NOT NULL,
	[ArchivedOn] [datetime2](7) NULL,
	[CachedOn] [datetime2](7) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[DeletedOn] [datetime2](7) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[ArchivedByAgentGuidRef] [uniqueidentifier] NULL,
	[CachedByAgentGuidRef] [uniqueidentifier] NULL,
	[CreatedByAgentGuidRef] [uniqueidentifier] NULL,
	[DeletedByAgentGuidRef] [uniqueidentifier] NULL,
	[ManagedByAgentGuidRef] [uniqueidentifier] NULL,
	[UpdatedByAgentGuidRef] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CoreAudit_AuditGuidKey] PRIMARY KEY CLUSTERED 
(
	[AuditGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NpdsResrepRoot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsResrepRoot](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[EntityTypeCodeRef] [smallint] NOT NULL,
	[EntityInitialTag] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[EntityName] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[EntityNature] [nvarchar](1024) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[EntityOwnerInfosetGuidRef] [uniqueidentifier] NULL,
	[EntityContactInfosetGuidRef] [uniqueidentifier] NULL,
	[EntityOtherInfosetGuidRef] [uniqueidentifier] NULL,
	[RecordGuidKey] [uniqueidentifier] NOT NULL,
	[RecordHandle] [char](9) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[RecordSignature] [xml] NULL,
	[RecordDiristryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RecordDiristryTag] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NULL,
	[RecordRegistryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RecordRegistryTag] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NULL,
	[RecordDirectoryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RecordDirectoryTag] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NULL,
	[RecordRegistrarInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RecordRegistrarTag] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NULL,
	[RecordRegistrantInfosetGuidRef] [uniqueidentifier] NULL,
	[InfosetGuidKey] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[InfosetIsAuthorPrivate] [bit] NOT NULL,
	[InfosetIsAgentShared] [bit] NOT NULL,
	[InfosetIsUpdaterLimited] [bit] NOT NULL,
	[InfosetIsManagerReleased] [bit] NOT NULL,
	[InfosetEntailment] [xml] NULL,
	[InfosetResrepEntityStatusCode] [smallint] NOT NULL,
	[InfosetResrepEntityStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetResrepEntityTestedOn] [datetime2](7) NULL,
	[InfosetResrepRecordStatusCode] [smallint] NOT NULL,
	[InfosetResrepRecordStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetResrepRecordTestedOn] [datetime2](7) NULL,
	[InfosetResrepInfosetStatusCode] [smallint] NOT NULL,
	[InfosetResrepInfosetStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetResrepInfosetTestedOn] [datetime2](7) NULL,
	[InfosetEntityLabelsCount] [int] NOT NULL,
	[InfosetEntityLabelsStatusCode] [smallint] NOT NULL,
	[InfosetEntityLabelsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetSupportingTagsCount] [int] NOT NULL,
	[InfosetSupportingTagsStatusCode] [smallint] NOT NULL,
	[InfosetSupportingTagsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetSupportingLabelsCount] [int] NOT NULL,
	[InfosetSupportingLabelsStatusCode] [smallint] NOT NULL,
	[InfosetSupportingLabelsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetCrossReferencesCount] [int] NOT NULL,
	[InfosetCrossReferencesStatusCode] [smallint] NOT NULL,
	[InfosetCrossReferencesStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetOtherTextsCount] [int] NOT NULL,
	[InfosetOtherTextsStatusCode] [smallint] NOT NULL,
	[InfosetOtherTextsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetLocationsCount] [int] NOT NULL,
	[InfosetLocationsStatusCode] [smallint] NOT NULL,
	[InfosetLocationsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetDescriptionsCount] [int] NOT NULL,
	[InfosetDescriptionsStatusCode] [smallint] NOT NULL,
	[InfosetDescriptionsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetProvenancesCount] [int] NOT NULL,
	[InfosetProvenancesStatusCode] [smallint] NOT NULL,
	[InfosetProvenancesStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetDistributionsCount] [int] NOT NULL,
	[InfosetDistributionsStatusCode] [smallint] NOT NULL,
	[InfosetDistributionsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetFairMetricsCount] [int] NOT NULL,
	[InfosetFairMetricsStatusCode] [smallint] NOT NULL,
	[InfosetFairMetricsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetPortalStatusCode] [smallint] NOT NULL,
	[InfosetPortalStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetPortalStatusTestedOn] [datetime2](7) NULL,
	[InfosetPortalSnapshotsCount] [int] NOT NULL,
	[InfosetPortalSnapshotsStatusCode] [smallint] NOT NULL,
	[InfosetPortalSnapshotsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetDoorsStatusCode] [smallint] NOT NULL,
	[InfosetDoorsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetDoorsStatusTestedOn] [datetime2](7) NULL,
	[InfosetDoorsSnapshotsCount] [int] NOT NULL,
	[InfosetDoorsSnapshotsStatusCode] [smallint] NOT NULL,
	[InfosetDoorsSnapshotsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetNexusStatusCode] [smallint] NOT NULL,
	[InfosetNexusStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
	[InfosetNexusStatusTestedOn] [datetime2](7) NULL,
	[InfosetNexusSnapshotsCount] [int] NOT NULL,
	[InfosetNexusSnapshotsStatusCode] [smallint] NOT NULL,
	[InfosetNexusSnapshotsStatusName] [nvarchar](32) COLLATE Latin1_General_100_CI_AS NULL,
 CONSTRAINT [PK_NpdsResrepRoot_RecordGuidKey] PRIMARY KEY CLUSTERED 
(
	[RecordGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_NpdsResrepRoot_InfosetGuidKey] UNIQUE NONCLUSTERED 
(
	[InfosetGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_NpdsResrepRoot_RecordHandle] UNIQUE NONCLUSTERED 
(
	[RecordHandle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusEntityPrincipalTag]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityPrincipalTag] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	PRT.InfosetGuidKey AS InfosetGuidRef, CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	PRT.EntityName, PRT.EntityTypeCodeRef, ET.TypeName AS EntityTypeName,
	CRT.EntityLabel AS EntityCanonicalLabel, CRT.TagToken AS EntityPrincipalTag, CRT.LabelUri, 
	CRT.ServiceTypeCodeRef, ST.TypeName AS ServiceTypeName,
	(SELECT CASE CAT.IsResolvable WHEN 1 THEN CRT.EntityLabel WHEN 0 THEN '' END) AS LabelUrl,
	CAT.IsGenerating, CAT.IsResolvable, CAT.IsPrivate,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal 
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) AND (CAT.IsPrincipal = 1) AND (CAT.IsDeleted = 0) INNER
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (PRT.EntityTypeCodeRef = ET.CodeKey) INNER 
JOIN dbo.NpdsCoreServiceTypeEnum AS ST ON (CRT.ServiceTypeCodeRef = ST.CodeKey) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) 
GO
/****** Object:  View [dbo].[NexusEntityNameTypeTag]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityNameTypeTag] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, PRT.InfosetGuidKey AS InfosetGuidRef, 
	PRT.EntityName, PRT.EntityTypeCodeRef, ET.TypeName AS EntityTypeName,
	CRT.EntityLabel AS EntityCanonicalLabel, CRT.TagToken AS EntityPrincipalTag
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) AND (CAT.IsPrincipal = 1) AND (CAT.IsDeleted = 0) INNER
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (PRT.EntityTypeCodeRef = ET.CodeKey) 
GO
/****** Object:  Table [dbo].[NpdsCoreSessionAgent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreSessionAgent](
	[AgentGuidKey] [uniqueidentifier] NOT NULL,
	[AgentInfosetGuidRef] [uniqueidentifier] NULL,
	[AgentIsAuthor] [bit] NOT NULL,
	[AgentIsEditor] [bit] NOT NULL,
	[AgentIsAdmin] [bit] NOT NULL,
	[SessionGuidKey] [uniqueidentifier] NOT NULL,
	[SessionDateCreated] [datetime2](7) NULL,
	[SessionDateAccessed] [datetime2](7) NULL,
	[SessionDateExpired] [datetime2](7) NULL,
	[IdentitySystemGuidRef] [uniqueidentifier] NULL,
	[IdentityApplicationGuidRef] [uniqueidentifier] NULL,
	[IdentityUserGuidRef] [uniqueidentifier] NOT NULL,
	[IdentityUserNameDisplayed] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_CoreSessionAgent_AgentGuidKey] PRIMARY KEY CLUSTERED 
(
	[AgentGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CoreSessionAgent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreSessionAgent]
AS
SELECT 
	SA.AgentGuidKey, SA.AgentInfosetGuidRef,
	SA.AgentIsAuthor, SA.AgentIsEditor, SA.AgentIsAdmin,
    SA.IdentityUserGuidRef, SA.IdentityUserNameDisplayed AS IdentityUserName,
	SA.IdentitySystemGuidRef, 
	SA.SessionGuidKey, SA.SessionDateCreated, SA.SessionDateAccessed
FROM dbo.NpdsCoreSessionAgent AS SA 

GO
/****** Object:  View [dbo].[NexusEntityLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[NexusEntityLabel] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	PRT.InfosetGuidKey AS InfosetGuidRef, CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	PRT.EntityName, PRT.EntityTypeCodeRef, ET.TypeName AS EntityTypeName,
	CRT.EntityLabel, CRT.TagToken, CRT.LabelUri, CRT.ServiceTypeCodeRef, ST.TypeName AS ServiceTypeName,
	(SELECT CASE CAT.IsResolvable WHEN 1 THEN CRT.EntityLabel WHEN 0 THEN '' END) AS LabelUrl,
	CAT.IsGenerating, CAT.IsResolvable, CAT.IsPrivate,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (PRT.EntityTypeCodeRef = ET.CodeKey) INNER 
JOIN dbo.NpdsCoreServiceTypeEnum AS ST ON (CRT.ServiceTypeCodeRef = ST.CodeKey) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusEntityNameTagLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityNameTagLabel] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.AuditGuidRef, CRT.RecordGuidRef, PRT.InfosetGuidKey AS InfosetGuidRef, 
	PRT.EntityName, CRT.EntityLabel AS EntityCanonicalLabel, CRT.TagToken AS EntityPrincipalTag
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) AND (CAT.IsPrincipal = 1) AND (CAT.IsDeleted = 0)
GO
/****** Object:  View [dbo].[NexusResrepRoot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusResrepRoot]
AS
SELECT RR.EntityTypeCodeRef, TC.TypeName AS EntityTypeName, 
	TC.TypeIsComponent AS EntityTypeIsComponent, TC.TypeIsConstituent AS EntityTypeIsConstituent,
	TC.TypeEditedByAgent AS EntityTypeEditedByAgent, TC.TypeEditedByAuthor AS EntityTypeEditedByAuthor, 
	TC.TypeEditedByEditor AS EntityTypeEditedByEditor,  TC.TypeEditedByAdmin AS EntityTypeEditedByAdmin,
	RR.EntityInitialTag, RR.EntityName, RR.EntityNature, ettRR.EntityPrincipalTag, ettRR.EntityCanonicalLabel, 
	RR.AuditGuidRef, RR.RecordGuidKey, RR.RecordHandle, 
	ART.IsDeleted AS RecordIsDeleted, ART.IsCached AS RecordIsCached,
	RR.RecordDiristryInfosetGuidRef AS RecordDiristryGuidRef, RR.RecordDiristryTag,	
	RR.RecordRegistryInfosetGuidRef AS RecordRegistryGuidRef, RR.RecordRegistryTag,	
	RR.RecordDirectoryInfosetGuidRef AS RecordDirectoryGuidRef, RR.RecordDirectoryTag,
	RR.RecordRegistrarInfosetGuidRef AS RecordRegistrarGuidRef, RR.RecordRegistrarTag,
	ART.CreatedByAgentGuidRef AS RecordCreatedByAgentGuidRef, ART.CreatedOn AS RecordCreatedOn,
	ART.UpdatedByAgentGuidRef AS RecordUpdatedByAgentGuidRef, ART.UpdatedOn AS RecordUpdatedOn,
	ART.DeletedByAgentGuidRef AS RecordDeletedByAgentGuidRef, ART.DeletedOn AS RecordDeletedOn, 
	ART.ManagedByAgentGuidRef AS RecordManagedByAgentGuidRef,
	RR.InfosetGuidKey, 
	RR.InfosetIsAuthorPrivate, RR.InfosetIsAgentShared, RR.InfosetIsUpdaterLimited, RR.InfosetIsManagerReleased,
	RR.InfosetPortalStatusTestedOn, RR.InfosetPortalStatusCode, RR.InfosetPortalStatusName,
	RR.InfosetDoorsStatusTestedOn, RR.InfosetDoorsStatusCode, RR.InfosetDoorsStatusName
FROM dbo.NpdsResrepRoot AS RR 
INNER JOIN dbo.NpdsCoreAudit AS ART ON (ART.AuditGuidKey = RR.AuditGuidRef) 
INNER JOIN dbo.NpdsCoreEntityTypeEnum AS TC ON (RR.EntityTypeCodeRef = TC.CodeKey)
INNER JOIN dbo.NexusEntityNameTagLabel AS ettRR ON (RR.InfosetGuidKey = ettRR.InfosetGuidRef) 
GO
/****** Object:  Table [dbo].[NpdsServiceDefault]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsServiceDefault](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[DiristryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RegistryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[DirectoryInfosetGuidRef] [uniqueidentifier] NOT NULL,
	[RegistrarInfosetGuidRef] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ServiceDefault_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CoreServiceDefault]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[CoreServiceDefault]
AS
SELECT DISTINCT RR.EntityName AS ServiceName, 
	RR.EntityNature AS ServiceNature,
	RR.RecordGuidKey AS ServiceRGuid,
	RR.InfosetGuidKey AS ServiceIGuid, 
	ET.CodeKey AS ServiceTCode,
	ET.TypeName AS ServiceTName,
	EL.TagToken AS ServicePTag,
	SD.DiristryInfosetGuidRef AS DefDiristryGuid,
	SD.RegistryInfosetGuidRef AS DefRegistryGuid,
	SD.DirectoryInfosetGuidRef AS DefDirectoryGuid,
	SD.RegistrarInfosetGuidRef AS DefRegistrarGuid
FROM dbo.NpdsResrepRoot AS RR INNER 
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (ET.CodeKey = RR.EntityTypeCodeRef) INNER 
JOIN dbo.NpdsCoreEntityLabel AS EL ON (EL.RecordGuidRef = RR.RecordGuidKey) INNER 
JOIN dbo.NpdsCoreAudit AS LA ON (LA.AuditGuidKey = EL.AuditGuidRef) INNER 
JOIN dbo.NpdsServiceDefault AS SD ON (SD.RecordGuidRef = RR.RecordGuidKey) INNER 
JOIN dbo.NpdsCoreAudit AS DA ON (DA.AuditGuidKey = SD.AuditGuidRef)
WHERE (ET.TypeIsComponent = 1) AND (LA.IsPrincipal = 1) AND (DA.IsPrincipal = 1);
GO
/****** Object:  View [dbo].[NexusCoreServiceDefault]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[NexusCoreServiceDefault] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT  CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	CAT.InfosetTypeCodeRef, ITT.TypeName AS InfosetTypeName,
	CRT.DiristryInfosetGuidRef, NR.ServiceName AS DiristryName, 
	CRT.RegistryInfosetGuidRef, PR.ServiceName AS RegistryName,
	CRT.DirectoryInfosetGuidRef, DR.ServiceName AS DirectoryName,
	CRT.RegistrarInfosetGuidRef,  SR.ServiceName AS RegistrarName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsServiceDefault AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS ITT ON (ITT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreServiceDefault AS NR ON (NR.ServiceIGuid = CRT.DiristryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS PR ON (PR.ServiceIGuid = CRT.RegistryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS DR ON (DR.ServiceIGuid = CRT.DirectoryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS SR ON (SR.ServiceIGuid = CRT.RegistrarInfosetGuidRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsDoorsFairMetric]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsDoorsFairMetric](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[M_InvalidOldClaim] [smallint] NOT NULL,
	[Q_ValidOldClaim] [smallint] NOT NULL,
	[P_InvalidNewClaim] [smallint] NOT NULL,
	[N_ValidNewClaim] [smallint] NOT NULL,
	[FAIR1] [real] NOT NULL,
	[FAIR2] [real] NOT NULL,
	[FAIR3] [real] NOT NULL,
	[FAIR4] [real] NOT NULL,
 CONSTRAINT [PK_DoorsFairMetric_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusFairMetric]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusFairMetric] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT  CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CRT.M_InvalidOldClaim, CRT.Q_ValidOldClaim, CRT.P_InvalidNewClaim, CRT.N_ValidNewClaim,
	CRT.FAIR1, CRT.FAIR2, CRT.FAIR3, CRT.FAIR4,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsDoorsFairMetric AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[CoreServerService]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[CoreServerService]
AS
SELECT DISTINCT R.EntityName AS ServiceName, 
	R.EntityNature AS ServiceNature,
	R.RecordGuidKey AS ServiceRGuid,
	R.InfosetGuidKey AS ServiceIGuid, 
	T.CodeKey AS ServiceTCode,
	T.TypeName AS ServiceTName,
	L.TagToken AS ServicePTag
FROM dbo.NpdsResrepRoot AS R 
INNER JOIN dbo.NpdsCoreEntityTypeEnum AS T ON (T.CodeKey = R.EntityTypeCodeRef)
INNER JOIN dbo.NpdsCoreEntityLabel AS L ON (L.RecordGuidRef = R.RecordGuidKey)
INNER JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
WHERE (T.TypeIsComponent = 1) AND (A.IsPrincipal = 1);
GO
/****** Object:  View [dbo].[NexusEntityGeneratingLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityGeneratingLabel]
AS
SELECT RR.RecordGuidKey, 
RR.RecordRegistryInfosetGuidRef AS RegistryGuidRef,
rgyRR.EntityName AS RegistryName, 
rgyRRtl.EntityLabel AS RegistryGeneratingLabel
FROM dbo.NpdsResrepRoot AS RR /** selected Resrep **/
INNER JOIN dbo.NpdsResrepRoot AS rgyRR /** its Registry Resrep **/
ON (RR.RecordRegistryInfosetGuidRef = rgyRR.InfosetGuidKey) 
INNER JOIN dbo.NexusEntityLabel As rgyRRtl /** the generating EntityLabel for the Registry Resrep **/
ON (rgyRR.RecordGuidKey = rgyRRtl.RecordGuidRef) AND (rgyRRtl.IsGenerating = 1)
GO
/****** Object:  View [dbo].[NexusEntityGeneratingLabelDirectory]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusEntityGeneratingLabelDirectory]
AS
SELECT RR.RecordGuidKey, 
RR.RecordDirectoryInfosetGuidRef AS DirectoryIGuidRef,
CRR.EntityName AS DirectoryName, 
CEL.TagToken AS DirectoryTagToken,
CEL.EntityLabel AS DirectoryGeneratingLabel
FROM dbo.NpdsResrepRoot AS RR /** selected ResrepRecord **/
INNER JOIN dbo.NpdsResrepRoot AS CRR /** its directory Component ResrepRecord **/
ON (RR.RecordDirectoryInfosetGuidRef = CRR.InfosetGuidKey) 
INNER JOIN dbo.NexusEntityLabel As CEL /** the Component's generating EntityLabel ***/
ON (CRR.RecordGuidKey = CEL.RecordGuidRef) AND (CEL.IsGenerating = 1)
GO
/****** Object:  View [dbo].[NexusEntityGeneratingLabelDiristry]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusEntityGeneratingLabelDiristry]
AS
SELECT RR.RecordGuidKey, 
RR.RecordRegistryInfosetGuidRef AS DiristryIGuidRef,
CRR.EntityName AS DiristryName, 
CEL.TagToken AS DiristryTagToken,
CEL.EntityLabel AS DiristryGeneratingLabel
FROM dbo.NpdsResrepRoot AS RR /** selected ResrepRecord **/
INNER JOIN dbo.NpdsResrepRoot AS CRR /** its registry Component ResrepRecord **/
ON (RR.RecordRegistryInfosetGuidRef = CRR.InfosetGuidKey) 
INNER JOIN dbo.NexusEntityLabel As CEL /** the Component's generating EntityLabel ***/
ON (CRR.RecordGuidKey = CEL.RecordGuidRef) AND (CEL.IsGenerating = 1)
GO
/****** Object:  View [dbo].[NexusEntityGeneratingLabelRegistrar]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[NexusEntityGeneratingLabelRegistrar]
AS
SELECT RR.RecordGuidKey, 
RR.RecordRegistrarInfosetGuidRef AS RegistrarIGuidRef,
CRR.EntityName AS RegistrarName, 
CEL.TagToken AS RegistrarTagToken,
CEL.EntityLabel AS RegistrarGeneratingLabel
FROM dbo.NpdsResrepRoot AS RR /** selected ResrepRecord **/
INNER JOIN dbo.NpdsResrepRoot AS CRR /** its registrar Component ResrepRecord **/
ON (RR.RecordRegistrarInfosetGuidRef = CRR.InfosetGuidKey) 
INNER JOIN dbo.NexusEntityLabel As CEL /** the Component's generating EntityLabel ***/
ON (CRR.RecordGuidKey = CEL.RecordGuidRef) AND (CEL.IsGenerating = 1)
GO
/****** Object:  View [dbo].[NexusEntityGeneratingLabelRegistry]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusEntityGeneratingLabelRegistry]
AS
SELECT RR.RecordGuidKey, 
RR.RecordRegistryInfosetGuidRef AS RegistryIGuidRef,
CRR.EntityName AS RegistryName, 
CEL.TagToken AS RegistryTagToken,
CEL.EntityLabel AS RegistryGeneratingLabel
FROM dbo.NpdsResrepRoot AS RR /** selected ResrepRecord **/
INNER JOIN dbo.NpdsResrepRoot AS CRR /** its registry Component ResrepRecord **/
ON (RR.RecordRegistryInfosetGuidRef = CRR.InfosetGuidKey) 
INNER JOIN dbo.NexusEntityLabel As CEL /** the Component's generating EntityLabel ***/
ON (CRR.RecordGuidKey = CEL.RecordGuidRef) AND (CEL.IsGenerating = 1)
GO
/****** Object:  Table [dbo].[NpdsResrepAccessAuth]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsResrepAccessAuth](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[AccessRequestedForAgentGuidRef] [uniqueidentifier] NULL,
	[AccessApprovedByAgentGuidRef] [uniqueidentifier] NULL,
	[RequestIsApproved] [bit] NOT NULL,
	[RequestIsDenied] [bit] NOT NULL,
	[AuthorHasResrepAccess] [bit] NOT NULL,
 CONSTRAINT [PK_ResrepAccessAuth_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusResrepAuthorAccess]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusResrepAuthorAccess]
AS
SELECT       
RR.FgroupGuidKey AS AccessGuidKey, RR.RecordGuidRef, 
RR.AccessRequestedForAgentGuidRef, RR.AccessApprovedByAgentGuidRef, 
RR.RequestIsApproved, RR.RequestIsDenied, RR.AuthorHasResrepAccess
FROM dbo.NpdsResrepAccessAuth AS RR

GO
/****** Object:  View [dbo].[NexusEntityCanonicalLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusEntityCanonicalLabel] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	PRT.InfosetGuidKey AS InfosetGuidRef, CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	PRT.EntityName, PRT.EntityTypeCodeRef, ET.TypeName AS EntityTypeName,
	CRT.EntityLabel AS EntityCanonicalLabel, CRT.TagToken AS EntityPrincipalTag, CRT.LabelUri, 
	CRT.ServiceTypeCodeRef, ST.TypeName AS ServiceTypeName,
	(SELECT CASE CAT.IsResolvable WHEN 1 THEN CRT.EntityLabel WHEN 0 THEN '' END) AS LabelUrl,
	CAT.IsGenerating, CAT.IsResolvable, CAT.IsPrivate,
	CAT.HasIndex, CAT.HasPriority,  CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) AND (CAT.IsPrincipal = 1) AND (CAT.IsDeleted = 0) INNER
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (PRT.EntityTypeCodeRef = ET.CodeKey) INNER 
JOIN dbo.NpdsCoreServiceTypeEnum AS ST ON (CRT.ServiceTypeCodeRef = ST.CodeKey) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusResrepAuthorAudit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusResrepAuthorAudit]
AS
SELECT  
RR.InfosetGuidKey AS InfosetGuidRef,
SS.RecordGuidRef, 
SS.FgroupGuidKey AS AccessGuidKey,  
SS.AccessRequestedForAgentGuidRef, SS.AccessApprovedByAgentGuidRef, 
SS.RequestIsApproved, SS.RequestIsDenied, SS.AuthorHasResrepAccess, 
SS.AuditGuidRef,
AA.HasIndex, AA.IsDeleted,
AA.CreatedOn, AA.CreatedByAgentGuidRef, 
AA.UpdatedOn, AA.UpdatedByAgentGuidRef, 
AA.DeletedOn, AA.DeletedByAgentGuidRef
FROM dbo.NpdsResrepAccessAuth AS SS INNER
JOIN dbo.NpdsResrepRoot AS RR ON (RR.RecordGuidKey = SS.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS AA ON (AA.AuditGuidKey = SS.AuditGuidRef)

GO
/****** Object:  View [dbo].[NexusResrepAuthorRequest]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[NexusResrepAuthorRequest]
AS
SELECT RR.RecordGuidKey AS ResrepRGuidRef, RR.InfosetGuidKey AS ResrepIGuidRef, 
RR.RecordHandle AS ResrepRecordHandle, RR.EntityName AS ResrepEntityName, 
RRCC.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
SS.FgroupGuidKey, 
SS.AccessRequestedForAgentGuidRef, RA.IdentityUserName AS AccessRequestedForAgentUserName,
SS.AccessApprovedByAgentGuidRef, AA.IdentityUserName AS AccessApprovedByAgentUserName,
SS.RequestIsApproved, SS.RequestIsDenied, SS.AuthorHasResrepAccess, 
SSCC.HasIndex, SSCC.IsDeleted,
SSCC.CreatedOn, SSCC.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
SSCC.UpdatedOn, SSCC.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
SSCC.DeletedOn, SSCC.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS RR -- current selected resource
INNER JOIN dbo.NpdsCoreAudit AS RRCC ON (RRCC.AuditGuidKey = RR.AuditGuidRef)
-- access request for agent's access to resource
INNER JOIN dbo.NpdsResrepAccessAuth AS SS ON (SS.RecordGuidRef = RR.RecordGuidKey) 
INNER JOIN dbo.NpdsCoreAudit AS SSCC ON (SSCC.AuditGuidKey = SS.AuditGuidRef)
-- Requesting Agent RA for resource in SS
INNER JOIN dbo.CoreSessionAgent AS RA ON (RA.AgentGuidKey = SS.AccessRequestedForAgentGuidRef)
-- current Managing Agent MA for resource in RR
INNER JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = RRCC.ManagedByAgentGuidRef)
-- Creating Agent CA for request in SS
INNER JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = SSCC.CreatedByAgentGuidRef)
-- Updating Agent UA for request in SS
INNER JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = SSCC.UpdatedByAgentGuidRef) LEFT
-- Approving Agent AA for request in SS
OUTER JOIN dbo.CoreSessionAgent AS AA ON (AA.AgentGuidKey = SS.AccessApprovedByAgentGuidRef) LEFT
-- Deleting Agent DA for request in SS
OUTER JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = SSCC.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsServiceAccessAuth]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsServiceAccessAuth](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[AccessRequestedForAgentGuidRef] [uniqueidentifier] NULL,
	[AccessApprovedByAgentGuidRef] [uniqueidentifier] NULL,
	[RequestIsApproved] [bit] NOT NULL,
	[RequestIsDenied] [bit] NOT NULL,
	[EditorHasServiceAccess] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceAccessAuth_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusServiceEditorAccess]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusServiceEditorAccess]
AS
SELECT      
RR.InfosetGuidKey AS InfosetGuidRef,
SS.RecordGuidRef,
SS.FgroupGuidKey AS AccessGuidKey,  
SS.AccessRequestedForAgentGuidRef, SS.AccessApprovedByAgentGuidRef,
SS.RequestIsApproved, SS.RequestIsDenied, SS.EditorHasServiceAccess
FROM dbo.NpdsServiceAccessAuth AS SS INNER
JOIN dbo.NpdsResrepRoot AS RR ON (RR.RecordGuidKey = SS.RecordGuidRef)
GO
/****** Object:  View [dbo].[NexusServiceEditorAudit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[NexusServiceEditorAudit]
AS
SELECT       
RR.InfosetGuidKey AS InfosetGuidRef,
SS.RecordGuidRef,
SS.FgroupGuidKey AS AccessGuidKey,   
SS.AccessRequestedForAgentGuidRef, SS.AccessApprovedByAgentGuidRef,
SS.RequestIsApproved, SS.RequestIsDenied, SS.EditorHasServiceAccess,
SS.AuditGuidRef,
AA.HasIndex, AA.IsDeleted,
AA.CreatedOn, AA.CreatedByAgentGuidRef, 
AA.UpdatedOn, AA.UpdatedByAgentGuidRef, 
AA.DeletedOn, AA.DeletedByAgentGuidRef
FROM dbo.NpdsServiceAccessAuth AS SS INNER
JOIN dbo.NpdsResrepRoot AS RR ON (RR.RecordGuidKey = SS.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS AA ON (AA.AuditGuidKey = SS.AuditGuidRef)
GO
/****** Object:  View [dbo].[NexusServiceEditorRequest]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusServiceEditorRequest]
AS
SELECT RR.RecordGuidKey AS ResrepRGuidRef, RR.InfosetGuidKey AS ResrepIGuidRef, 
RR.RecordHandle AS ResrepRecordHandle, RR.EntityName AS ResrepEntityName, 
RRCC.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
SS.FgroupGuidKey, 
SS.AccessRequestedForAgentGuidRef, RA.IdentityUserName AS AccessRequestedForAgentUserName,
SS.AccessApprovedByAgentGuidRef, AA.IdentityUserName AS AccessApprovedByAgentUserName,
SS.RequestIsApproved, SS.RequestIsDenied, SS.EditorHasServiceAccess, 
SSCC.HasIndex, SSCC.IsDeleted,
SSCC.CreatedOn, SSCC.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
SSCC.UpdatedOn, SSCC.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
SSCC.DeletedOn, SSCC.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS RR -- current selected resource
INNER JOIN dbo.NpdsCoreAudit AS RRCC ON (RRCC.AuditGuidKey = RR.AuditGuidRef)
-- access request for agent's access to resource
INNER JOIN dbo.NpdsServiceAccessAuth AS SS ON (SS.RecordGuidRef = RR.RecordGuidKey) 
INNER JOIN dbo.NpdsCoreAudit AS SSCC ON (SSCC.AuditGuidKey = SS.AuditGuidRef)
-- Requesting Agent RA for resource in SS
INNER JOIN dbo.CoreSessionAgent AS RA ON (RA.AgentGuidKey = SS.AccessRequestedForAgentGuidRef)
-- current Managing Agent MA for resource in RR
INNER JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = RRCC.ManagedByAgentGuidRef)
-- Creating Agent CA for request in SS
INNER JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = SSCC.CreatedByAgentGuidRef)
-- Updating Agent UA for request in SS
INNER JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = SSCC.UpdatedByAgentGuidRef) LEFT
-- Approving Agent AA for request in SS
OUTER JOIN dbo.CoreSessionAgent AS AA ON (AA.AgentGuidKey = SS.AccessApprovedByAgentGuidRef) LEFT
-- Deleting Agent DA for request in SS
OUTER JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = SSCC.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusServiceCoreDefault]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusServiceCoreDefault] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT  CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	CAT.InfosetTypeCodeRef, ITT.TypeName AS InfosetTypeName,
	CRT.DiristryInfosetGuidRef, NR.ServiceName AS DiristryName, 
	CRT.RegistryInfosetGuidRef, PR.ServiceName AS RegistryName,
	CRT.DirectoryInfosetGuidRef, DR.ServiceName AS DirectoryName,
	CRT.RegistrarInfosetGuidRef,  SR.ServiceName AS RegistrarName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsServiceDefault AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS ITT ON (ITT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreServiceDefault AS NR ON (NR.ServiceIGuid = CRT.DiristryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS PR ON (PR.ServiceIGuid = CRT.RegistryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS DR ON (DR.ServiceIGuid = CRT.DirectoryInfosetGuidRef) INNER
JOIN dbo.CoreServiceDefault AS SR ON (SR.ServiceIGuid = CRT.RegistrarInfosetGuidRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsServiceRestrictionAnd]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsServiceRestrictionAnd](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[RestrictionAndGuidKey] [uniqueidentifier] NOT NULL,
	[RestrictionName] [nvarchar](64) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_ServiceRestrictionAnd_RestrictionAndGuidKey] PRIMARY KEY CLUSTERED 
(
	[RestrictionAndGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusServiceRestrictionAnd]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusServiceRestrictionAnd]
AS -- SRA ServiceRestrictionAnd, CCA CoreComponentAudit, RRR ResRepRecord
SELECT 
RRR.EntityName AS ServiceName, 
RRR.EntityCanonicalLabel AS ServiceLabel, 
RRR.InfosetGuidRef AS ServiceIGuid,
RRR.RecordGuidRef AS ServiceRGuid,
RRR.InfosetGuidRef, RRR.RecordGuidRef, 
SRA.RestrictionAndGuidKey, SRA.RestrictionName, 
CCA.HasIndex AS AndHasIndex, CCA.HasPriority AS AndHasPriority, 
CCA.IsExcluding, CCA.IsSufficient, CCA.IsDeleted, 
CCA.CreatedOn, CCA.CreatedByAgentGuidRef AS CreatedByAgentGuid, 
CCA.UpdatedOn, CCA.UpdatedByAgentGuidRef AS UpdatedByAgentGuid, 
CCA.DeletedOn, CCA.DeletedByAgentGuidRef AS DeletedByAgentGuid
FROM dbo.NpdsServiceRestrictionAnd AS SRA INNER 
JOIN dbo.NpdsCoreAudit AS CCA ON (SRA.AuditGuidRef = CCA.AuditGuidKey) INNER
JOIN dbo.NexusEntityCanonicalLabel AS RRR ON (SRA.RecordGuidRef = RRR.RecordGuidRef) 

GO
/****** Object:  Table [dbo].[NpdsServiceRestrictionOr]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsServiceRestrictionOr](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RestrictionAndGuidRef] [uniqueidentifier] NOT NULL,
	[RestrictionOrGuidKey] [uniqueidentifier] NOT NULL,
	[RestrictionValue] [nvarchar](256) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_ServiceRestrictionOr_RestrictionOrGuidKey] PRIMARY KEY CLUSTERED 
(
	[RestrictionOrGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusServiceRestrictionOr]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusServiceRestrictionOr]
AS -- SRO ServiceRestrictionOr, SRA ServiceRestrictionAnd, RRR ResRepRecord,
-- CCAO CoreComponentAuditOR, CCAA CoreComponentAuditAnd
SELECT 
RRR.EntityName AS ServiceName, 
RRR.EntityCanonicalLabel AS ServiceLabel, 
RRR.InfosetGuidRef AS ServiceIGuid,
RRR.RecordGuidRef AS ServiceRGuid,
RRR.InfosetGuidRef, RRR.RecordGuidRef, 
SRO.RestrictionAndGuidRef, SRA.RestrictionName,
  CCAA.HasIndex AS AndHasIndex, CCAA.HasPriority AS AndHasPriority, 
  CCAA.IsExcluding, CCAA.IsSufficient, 
SRO.RestrictionOrGuidKey, SRO.RestrictionValue,
	CCAO.HasIndex AS OrHasIndex, CCAO.HasPriority AS OrHasPriority,  
  CCAO.IsWordPhrase, CCAO.IsConceptLabel, CCAO.IsDeleted, 
CCAO.CreatedOn, CCAO.CreatedByAgentGuidRef AS CreatedByAgentGuid, 
CCAO.UpdatedOn, CCAO.UpdatedByAgentGuidRef AS UpdatedByAgentGuid, 
CCAO.DeletedOn, CCAO.DeletedByAgentGuidRef AS DeletedByAgentGuid
FROM dbo.NpdsServiceRestrictionOr AS SRO INNER 
JOIN dbo.NpdsCoreAudit AS CCAO ON (CCAO.AuditGuidKey = SRO.AuditGuidRef) INNER
JOIN dbo.NpdsServiceRestrictionAnd AS SRA ON (SRA.RestrictionAndGuidKey = SRO.RestrictionAndGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CCAA ON (CCAA.AuditGuidKey = SRA.AuditGuidRef) INNER
JOIN dbo.NexusEntityCanonicalLabel AS RRR ON (RRR.RecordGuidRef = SRA.RecordGuidRef) 

GO
/****** Object:  Table [dbo].[NpdsPortalCrossReference]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsPortalCrossReference](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[CrossReference] [nvarchar](256) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_PortalCrossReference_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusCrossReference]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusCrossReference] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.CrossReference,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	 CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal, 
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsPortalCrossReference AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsDoorsDescription]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsDoorsDescription](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_DoorsDescription_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusDescription]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[NexusDescription] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.[Description],
	CAT.InfosetTypeCodeRef, CIT.TypeName AS InfosetTypeName,
	CAT.FieldFormatCodeRef, CIF.FormatName AS FieldFormatName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsDoorsDescription AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS CIT ON (CIT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.NpdsCoreFieldFormatEnum AS CIF ON (CIF.CodeKey = CAT.FieldFormatCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsDoorsDistribution]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsDoorsDistribution](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[Distribution] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_DoorsDistribution_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusDistribution]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusDistribution] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.[Distribution],
	CAT.InfosetTypeCodeRef, CIT.TypeName AS InfosetTypeName,
	CAT.FieldFormatCodeRef, CIF.FormatName AS FieldFormatName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsDoorsDistribution AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS CIT ON (CIT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.NpdsCoreFieldFormatEnum AS CIF ON (CIF.CodeKey = CAT.FieldFormatCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusEntityAliasLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusEntityAliasLabel] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, 
	PRT.InfosetGuidKey AS InfosetGuidRef, CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	PRT.EntityName, PRT.EntityTypeCodeRef, ET.TypeName AS EntityTypeName,
	CRT.EntityLabel AS EntityAliasLabel, CRT.TagToken AS EntityAliasTag, CRT.LabelUri, 
	CRT.ServiceTypeCodeRef, ST.TypeName AS ServiceTypeName,
	(SELECT CASE CAT.IsResolvable WHEN 1 THEN CRT.EntityLabel WHEN 0 THEN '' END) AS LabelUrl,
	CAT.IsGenerating, CAT.IsResolvable, CAT.IsPrivate,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreEntityLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) AND (CAT.IsPrincipal = 0) AND (CAT.IsDeleted = 0) INNER
JOIN dbo.NpdsCoreEntityTypeEnum AS ET ON (PRT.EntityTypeCodeRef = ET.CodeKey) INNER 
JOIN dbo.NpdsCoreServiceTypeEnum AS ST ON (CRT.ServiceTypeCodeRef = ST.CodeKey) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsDoorsLocation]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsDoorsLocation](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[Location] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[DisplayText] [nvarchar](64) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[DisplayImageUrl] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[UrlWebAddress] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[UrlWebAddressValidated] [datetime2](7) NULL,
	[EmailAddress] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[EmailAddressValidated] [datetime2](7) NULL,
	[StreetAddress] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[StreetAddressValidated] [datetime2](7) NULL,
	[ExtendedAddress] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[FormattedAddress] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[CityLocality] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[StateRegion] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[Country] [nvarchar](128) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[PostalCode] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[Telephone] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[GeocodeType] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[GeocodeConfidence] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
 CONSTRAINT [PK_DoorsLocation_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusLocation]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NexusLocation]
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT  CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.[Location], 
  CAT.InfosetTypeCodeRef, CIT.TypeName AS InfosetTypeName,
	CAT.FieldFormatCodeRef, CIF.FormatName AS FieldFormatName,
	CRT.DisplayText, CRT.DisplayImageUrl,
	CRT.UrlWebAddress, CRT.UrlWebAddressValidated, CRT.EmailAddress, CRT.EmailAddressValidated,
	CRT.StreetAddress, CRT.StreetAddressValidated, CRT.ExtendedAddress, CRT.FormattedAddress,
	CRT.CityLocality, CRT.StateRegion, CRT.Country, CRT.PostalCode, CRT.Telephone, 
	CRT.GeocodeType, CRT.GeocodeConfidence, CRT.Latitude, CRT.Longitude,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef,  MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsDoorsLocation AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS CIT ON (CIT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.NpdsCoreFieldFormatEnum AS CIF ON (CIF.CodeKey = CAT.FieldFormatCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsPortalOtherText]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsPortalOtherText](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[OtherText] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_PortalOtherText_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusOtherText]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusOtherText] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.OtherText,
	CAT.InfosetTypeCodeRef, CIT.TypeName AS InfosetTypeName,
	CAT.FieldFormatCodeRef, CIF.FormatName AS FieldFormatName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsPortalOtherText AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS CIT ON (CIT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.NpdsCoreFieldFormatEnum AS CIF ON (CIF.CodeKey = CAT.FieldFormatCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsDoorsProvenance]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsDoorsProvenance](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[Provenance] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_DoorsProvenance_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusProvenance]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusProvenance] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.[Provenance],
	CAT.InfosetTypeCodeRef, CIT.TypeName AS InfosetTypeName,
	CAT.FieldFormatCodeRef, CIF.FormatName AS FieldFormatName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsDoorsProvenance AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS CIT ON (CIT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.NpdsCoreFieldFormatEnum AS CIF ON (CIF.CodeKey = CAT.FieldFormatCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsPortalSupportingLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsPortalSupportingLabel](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[SupportingLabel] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
 CONSTRAINT [PK_PortalSupportingLabel_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusSupportingLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusSupportingLabel] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.SupportingLabel,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsPortalSupportingLabel AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsPortalSupportingTag]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsPortalSupportingTag](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[SupportingTag] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NOT NULL,
 CONSTRAINT [PK_PortalSupportingTag_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusSupportingTag]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusSupportingTag] 
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.SupportingTag,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,  
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsPortalSupportingTag AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusResrepLeaf]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[NexusResrepLeaf]
AS
SELECT RR.EntityTypeCodeRef, TC.TypeName AS EntityTypeName, 
	TC.TypeIsComponent AS EntityTypeIsComponent, TC.TypeIsConstituent AS EntityTypeIsConstituent,
	TC.TypeEditedByAgent AS EntityTypeEditedByAgent, TC.TypeEditedByAuthor AS EntityTypeEditedByAuthor, 
	TC.TypeEditedByEditor AS EntityTypeEditedByEditor,  TC.TypeEditedByAdmin AS EntityTypeEditedByAdmin,
	RR.EntityInitialTag, RR.EntityName, RR.EntityNature, ettRR.EntityPrincipalTag, ettRR.EntityCanonicalLabel, 
	RR.EntityOtherInfosetGuidRef AS EntityOtherGuidRef,				
	othRR.EntityName AS EntityOtherName,   othRR.EntityPrincipalTag AS EntityOtherTag,      othRR.EntityCanonicalLabel AS EntityOtherLabel,
	RR.EntityOwnerInfosetGuidRef As EntityOwnerGuidRef,				
	onrRR.EntityName AS EntityOwnerName,   onrRR.EntityPrincipalTag AS EntityOwnerTag,      onrRR.EntityCanonicalLabel AS EntityOwnerLabel,   
	RR.EntityContactInfosetGuidRef AS EntityContactGuidRef,			
	ctcRR.EntityName AS EntityContactName, ctcRR.EntityPrincipalTag AS EntityContactTag,    ctcRR.EntityCanonicalLabel AS EntityContactLabel,   
	RR.RecordRegistrantInfosetGuidRef AS RecordRegistrantGuidRef,	
	rgtRR.EntityName AS RecordRegistrantName, rgtRR.EntityPrincipalTag AS RecordRegistrantTag, rgtRR.EntityCanonicalLabel AS RecordRegistrantLabel,
	RR.AuditGuidRef, RR.RecordGuidKey, RR.RecordHandle, RR.RecordSignature, 
	ART.IsCached AS RecordIsCached, ART.CachedOn AS RecordCachedOn, 
	ART.IsDeleted AS RecordIsDeleted,  ART.ArchivedOn AS RecordArchivedOn,
	RR.RecordDiristryInfosetGuidRef AS RecordDiristryGuidRef,	drsRR.EntityName AS RecordDiristryName,   drsRR.EntityPrincipalTag AS RecordDiristryTag,   drsRR.EntityCanonicalLabel AS RecordDiristryLabel,   
	RR.RecordRegistryInfosetGuidRef AS RecordRegistryGuidRef,	rgsRR.EntityName AS RecordRegistryName,   rgsRR.EntityPrincipalTag AS RecordRegistryTag,   rgsRR.EntityCanonicalLabel AS RecordRegistryLabel,   
	RR.RecordDirectoryInfosetGuidRef AS RecordDirectoryGuidRef,	drcRR.EntityName AS RecordDirectoryName,  drcRR.EntityPrincipalTag AS RecordDirectoryTag,  drcRR.EntityCanonicalLabel AS RecordDirectoryLabel,
	RR.RecordRegistrarInfosetGuidRef AS RecordRegistrarGuidRef, rgrRR.EntityName AS RecordRegistrarName,  rgrRR.EntityPrincipalTag AS RecordRegistrarTag,  rgrRR.EntityCanonicalLabel AS RecordRegistrarLabel,
	ART.CreatedByAgentGuidRef AS RecordCreatedByAgentGuidRef, ART.CreatedOn AS RecordCreatedOn, UAC.IdentityUserName As RecordCreatedByUserName, 
	ART.UpdatedByAgentGuidRef AS RecordUpdatedByAgentGuidRef, ART.UpdatedOn AS RecordUpdatedOn, UAU.IdentityUserName As RecordUpdatedByUserName, 
	ART.DeletedByAgentGuidRef AS RecordDeletedByAgentGuidRef, ART.DeletedOn AS RecordDeletedOn, UAD.IdentityUserName AS RecordDeletedByUserName, 
	ART.ManagedByAgentGuidRef AS RecordManagedByAgentGuidRef, UAM.IdentityUserName As RecordManagedByUserName, 
	RR.InfosetGuidKey, RR.InfosetEntailment,
	RR.InfosetIsAuthorPrivate, RR.InfosetIsAgentShared, RR.InfosetIsUpdaterLimited, RR.InfosetIsManagerReleased,
	RR.InfosetPortalStatusTestedOn, RR.InfosetPortalStatusCode, RR.InfosetPortalStatusName,
	RR.InfosetDoorsStatusTestedOn, RR.InfosetDoorsStatusCode, RR.InfosetDoorsStatusName,
	RR.InfosetResrepEntityTestedOn, RR.InfosetResrepEntityStatusCode, RR.InfosetResrepEntityStatusName,
	RR.InfosetResrepRecordTestedOn, RR.InfosetResrepRecordStatusCode, RR.InfosetResrepRecordStatusName,
	RR.InfosetResrepInfosetTestedOn, RR.InfosetResrepInfosetStatusCode, RR.InfosetResrepInfosetStatusName,
	RR.InfosetEntityLabelsCount, RR.InfosetEntityLabelsStatusCode, RR.InfosetEntityLabelsStatusName,
	RR.InfosetSupportingTagsCount, RR.InfosetSupportingTagsStatusCode, RR.InfosetSupportingTagsStatusName,
	RR.InfosetSupportingLabelsCount, RR.InfosetSupportingLabelsStatusCode, RR.InfosetSupportingLabelsStatusName,
	RR.InfosetCrossReferencesCount, RR.InfosetCrossReferencesStatusCode, RR.InfosetCrossReferencesStatusName,
	RR.InfosetOtherTextsCount, RR.InfosetOtherTextsStatusCode, RR.InfosetOtherTextsStatusName,
	RR.InfosetLocationsCount, RR.InfosetLocationsStatusCode,  RR.InfosetLocationsStatusName,
	RR.InfosetDescriptionsCount, RR.InfosetDescriptionsStatusCode, RR.InfosetDescriptionsStatusName,
	RR.InfosetProvenancesCount, RR.InfosetProvenancesStatusCode, RR.InfosetProvenancesStatusName,
	RR.InfosetDistributionsCount, RR.InfosetDistributionsStatusCode, RR.InfosetDistributionsStatusName,
	RR.InfosetFairMetricsCount, RR.InfosetFairMetricsStatusCode, RR.InfosetFairMetricsStatusName,
	RR.InfosetNexusSnapshotsCount, RR.InfosetNexusSnapshotsStatusCode, RR.InfosetNexusSnapshotsStatusName
FROM dbo.NpdsResrepRoot AS RR 
INNER JOIN dbo.NpdsCoreAudit AS ART ON (ART.AuditGuidKey = RR.AuditGuidRef) 
INNER JOIN dbo.NpdsCoreEntityTypeEnum AS TC ON (RR.EntityTypeCodeRef = TC.CodeKey) 
INNER JOIN dbo.NexusEntityNameTagLabel AS ettRR ON (RR.InfosetGuidKey = ettRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS drsRR ON (RR.RecordDiristryInfosetGuidRef   = drsRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS rgsRR ON (RR.RecordRegistryInfosetGuidRef   = rgsRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS drcRR ON (RR.RecordDirectoryInfosetGuidRef  = drcRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS rgrRR ON (RR.RecordRegistrarInfosetGuidRef  = rgrRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAM ON (ART.ManagedByAgentGuidRef = UAM.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAC ON (ART.CreatedByAgentGuidRef = UAC.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAU ON (ART.UpdatedByAgentGuidRef = UAU.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAD ON (ART.DeletedByAgentGuidRef = UAD.AgentGuidKey)
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS rgtRR ON (RR.RecordRegistrantInfosetGuidRef = rgtRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS ctcRR ON (RR.EntityContactInfosetGuidRef    = ctcRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS onrRR ON (RR.EntityOwnerInfosetGuidRef      = onrRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS othRR ON (RR.EntityOtherInfosetGuidRef      = othRR.InfosetGuidRef)
GO
/****** Object:  View [dbo].[CoreEntityTypeItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreEntityTypeItem] 
AS 
SELECT CodeKey, TypeName, TypeDescription, TypeIsComponent, TypeIsConstituent,
TypeEditedByAgent, TypeEditedByAuthor, TypeEditedByEditor, TypeEditedByAdmin
FROM dbo.NpdsCoreEntityTypeEnum

GO
/****** Object:  View [dbo].[NexusResrepStem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusResrepStem]
AS
SELECT RR.EntityTypeCodeRef, TC.TypeName AS EntityTypeName, 
	TC.TypeIsComponent AS EntityTypeIsComponent, TC.TypeIsConstituent AS EntityTypeIsConstituent,
	TC.TypeEditedByAgent AS EntityTypeEditedByAgent, TC.TypeEditedByAuthor AS EntityTypeEditedByAuthor, 
	TC.TypeEditedByEditor AS EntityTypeEditedByEditor,  TC.TypeEditedByAdmin AS EntityTypeEditedByAdmin,
	RR.EntityInitialTag, RR.EntityName, RR.EntityNature, ettRR.EntityPrincipalTag, ettRR.EntityCanonicalLabel, 
	RR.EntityOtherInfosetGuidRef AS EntityOtherGuidRef,				
	othRR.EntityName AS EntityOtherName,   othRR.EntityPrincipalTag AS EntityOtherTag,      othRR.EntityCanonicalLabel AS EntityOtherLabel,
	RR.EntityOwnerInfosetGuidRef As EntityOwnerGuidRef,				
	onrRR.EntityName AS EntityOwnerName,   onrRR.EntityPrincipalTag AS EntityOwnerTag,      onrRR.EntityCanonicalLabel AS EntityOwnerLabel,   
	RR.EntityContactInfosetGuidRef AS EntityContactGuidRef,			
	ctcRR.EntityName AS EntityContactName, ctcRR.EntityPrincipalTag AS EntityContactTag,    ctcRR.EntityCanonicalLabel AS EntityContactLabel,   
	RR.RecordRegistrantInfosetGuidRef AS RecordRegistrantGuidRef,	
	rgtRR.EntityName AS RecordRegistrantName, rgtRR.EntityPrincipalTag AS RecordRegistrantTag, rgtRR.EntityCanonicalLabel AS RecordRegistrantLabel,
	RR.AuditGuidRef, RR.RecordGuidKey, RR.RecordHandle, RR.RecordSignature, 
	ART.IsCached AS RecordIsCached, ART.CachedOn AS RecordCachedOn,
	ART.IsDeleted AS RecordIsDeleted,  ART.ArchivedOn AS RecordArchivedOn,
	RR.RecordDiristryInfosetGuidRef AS RecordDiristryGuidRef,	drsRR.EntityName AS RecordDiristryName,   drsRR.EntityPrincipalTag AS RecordDiristryTag,   drsRR.EntityCanonicalLabel AS RecordDiristryLabel,   
	RR.RecordRegistryInfosetGuidRef AS RecordRegistryGuidRef,	rgsRR.EntityName AS RecordRegistryName,   rgsRR.EntityPrincipalTag AS RecordRegistryTag,   rgsRR.EntityCanonicalLabel AS RecordRegistryLabel,   
	RR.RecordDirectoryInfosetGuidRef AS RecordDirectoryGuidRef,	drcRR.EntityName AS RecordDirectoryName,  drcRR.EntityPrincipalTag AS RecordDirectoryTag,  drcRR.EntityCanonicalLabel AS RecordDirectoryLabel,
	RR.RecordRegistrarInfosetGuidRef AS RecordRegistrarGuidRef, rgrRR.EntityName AS RecordRegistrarName,  rgrRR.EntityPrincipalTag AS RecordRegistrarTag,  rgrRR.EntityCanonicalLabel AS RecordRegistrarLabel,
	ART.CreatedByAgentGuidRef AS RecordCreatedByAgentGuidRef, ART.CreatedOn AS RecordCreatedOn, UAC.IdentityUserName As RecordCreatedByUserName, 
	ART.UpdatedByAgentGuidRef AS RecordUpdatedByAgentGuidRef, ART.UpdatedOn AS RecordUpdatedOn, UAU.IdentityUserName As RecordUpdatedByUserName, 
	ART.DeletedByAgentGuidRef AS RecordDeletedByAgentGuidRef, ART.DeletedOn AS RecordDeletedOn, UAD.IdentityUserName AS RecordDeletedByUserName, 
	ART.ManagedByAgentGuidRef AS RecordManagedByAgentGuidRef, UAM.IdentityUserName As RecordManagedByUserName, 
	RR.InfosetGuidKey, RR.InfosetEntailment,
	RR.InfosetIsAuthorPrivate, RR.InfosetIsAgentShared, RR.InfosetIsUpdaterLimited, RR.InfosetIsManagerReleased,
	RR.InfosetPortalStatusTestedOn, RR.InfosetPortalStatusCode, RR.InfosetPortalStatusName,
	RR.InfosetDoorsStatusTestedOn, RR.InfosetDoorsStatusCode, RR.InfosetDoorsStatusName
FROM dbo.NpdsResrepRoot AS RR 
INNER JOIN dbo.NpdsCoreAudit AS ART ON (ART.AuditGuidKey = RR.AuditGuidRef) 
INNER JOIN dbo.NpdsCoreEntityTypeEnum AS TC ON (RR.EntityTypeCodeRef = TC.CodeKey) 
INNER JOIN dbo.NexusEntityNameTagLabel AS ettRR ON (RR.InfosetGuidKey = ettRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS drsRR ON (RR.RecordDiristryInfosetGuidRef   = drsRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS rgsRR ON (RR.RecordRegistryInfosetGuidRef   = rgsRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS drcRR ON (RR.RecordDirectoryInfosetGuidRef  = drcRR.InfosetGuidRef) 
INNER JOIN dbo.NexusEntityNameTagLabel AS rgrRR ON (RR.RecordRegistrarInfosetGuidRef  = rgrRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAM ON (ART.ManagedByAgentGuidRef = UAM.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAC ON (ART.CreatedByAgentGuidRef = UAC.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAU ON (ART.UpdatedByAgentGuidRef = UAU.AgentGuidKey) 
LEFT OUTER JOIN dbo.CoreSessionAgent AS UAD ON (ART.DeletedByAgentGuidRef = UAD.AgentGuidKey)
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS rgtRR ON (RR.RecordRegistrantInfosetGuidRef = rgtRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS ctcRR ON (RR.EntityContactInfosetGuidRef    = ctcRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS onrRR ON (RR.EntityOwnerInfosetGuidRef      = onrRR.InfosetGuidRef) 
LEFT OUTER JOIN dbo.NexusEntityNameTagLabel AS othRR ON (RR.EntityOtherInfosetGuidRef      = othRR.InfosetGuidRef)
GO
/****** Object:  View [dbo].[CoreInfosetStatusItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreInfosetStatusItem] 
AS 
SELECT CodeKey, StatusName, StatusDescription, 
StatusEditedByAgent, StatusEditedByAuthor, StatusEditedByEditor, StatusEditedByAdmin
FROM dbo.NpdsCoreInfosetStatusEnum

GO
/****** Object:  View [dbo].[CoreServiceTypeItem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CoreServiceTypeItem] 
AS 
SELECT CodeKey, TypeName, TypeDescription, DefaultGeneratingLabel
FROM dbo.NpdsCoreServiceTypeEnum

GO
/****** Object:  View [dbo].[CoreSessionAgentServiceAccess]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CoreSessionAgentServiceAccess]
AS
SELECT 
	SA.AgentGuidKey, SA.AgentInfosetGuidRef,
	SA.AgentIsAuthor, SA.AgentIsEditor, SA.AgentIsAdmin,
    SA.IdentityUserGuidRef, SA.IdentityUserNameDisplayed AS IdentityUserName,
	SA.IdentitySystemGuidRef, SA.[Version],
	SA.SessionGuidKey, SA.SessionDateCreated, SA.SessionDateAccessed,
	CA.EditorHasServiceAccess
FROM dbo.NpdsCoreSessionAgent AS SA LEFT OUTER
JOIN dbo.NpdsServiceAccessAuth AS CA ON (CA.AccessRequestedForAgentGuidRef = SA.AgentGuidKey)

GO
/****** Object:  View [dbo].[CoreAudit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CoreAudit]
AS
SELECT *
FROM dbo.NpdsCoreAudit
GO
/****** Object:  View [dbo].[CoreServiceRestrictionAnd]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CoreServiceRestrictionAnd]
AS -- SRA ServiceRestrictionAnd, CCA CoreComponentAudit, RRR ResRepRecord
SELECT 
RRR.EntityName AS ServiceName, 
RRR.EntityCanonicalLabel AS ServiceLabel, 
RRR.InfosetGuidRef AS ServiceIGuid,
RRR.RecordGuidRef AS ServiceRGuid,
RRR.InfosetGuidRef, RRR.RecordGuidRef, 
SRA.RestrictionAndGuidKey, SRA.AuditGuidRef, SRA.RestrictionName, 
CCA.HasIndex, CCA.HasPriority, CCA.IsExcluding, CCA.IsSufficient, CCA.IsDeleted, 
CCA.CreatedOn, CCA.CreatedByAgentGuidRef AS CreatedByAgentGuid, 
CCA.UpdatedOn, CCA.UpdatedByAgentGuidRef AS UpdatedByAgentGuid, 
CCA.DeletedOn, CCA.DeletedByAgentGuidRef AS DeletedByAgentGuid
FROM dbo.NpdsServiceRestrictionAnd AS SRA INNER 
JOIN dbo.NpdsCoreAudit AS CCA ON (SRA.AuditGuidRef = CCA.AuditGuidKey) INNER
JOIN dbo.NexusEntityCanonicalLabel AS RRR ON (SRA.RecordGuidRef = RRR.RecordGuidRef) 

GO
/****** Object:  View [dbo].[CoreServiceRestrictionOr]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CoreServiceRestrictionOr]
AS -- CRO ComponentRestrictionOr, CRA ComponentRestrictionAnd, CCA CoreComponentAudit, RRR ResRepRecord
SELECT 
RRR.EntityName AS ServiceName, RRR.EntityCanonicalLabel AS ServiceLabel, 
RRR.RecordGuidRef, RRR.InfosetGuidRef, 
CRO.RestrictionAndGuidRef, CRA.AuditGuidRef AS AndAuditGuidRef, CCAA.HasIndex AS AndHasIndex, CRA.RestrictionName,
CRO.RestrictionOrGuidKey, CRO.AuditGuidRef AS OrAuditGuidRef, CCAO.HasIndex AS OrHasIndex, CRO.RestrictionValue, 
CCAO.IsWordPhrase, CCAO.IsConceptLabel, CCAO.IsDeleted, 
CCAO.CreatedOn, CCAO.CreatedByAgentGuidRef AS CreatedByAgentGuid, 
CCAO.UpdatedOn, CCAO.UpdatedByAgentGuidRef AS UpdatedByAgentGuid, 
CCAO.DeletedOn, CCAO.DeletedByAgentGuidRef AS DeletedByAgentGuid
FROM dbo.NpdsServiceRestrictionOr AS CRO INNER 
JOIN dbo.NpdsCoreAudit AS CCAO ON (CCAO.AuditGuidKey = CRO.AuditGuidRef) INNER
JOIN dbo.NpdsServiceRestrictionAnd AS CRA ON (CRA.RestrictionAndGuidKey = CRO.RestrictionAndGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CCAA ON (CCAA.AuditGuidKey = CRO.AuditGuidRef) INNER
JOIN dbo.NexusEntityCanonicalLabel AS RRR ON (RRR.RecordGuidRef = CRA.RecordGuidRef) 

GO
/****** Object:  Table [dbo].[NpdsResrepSnapshot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsResrepSnapshot](
	[AuditGuidRef] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NOT NULL,
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[ResrepSnapshotJson] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
	[ResrepSnapshotXml] [nvarchar](max) COLLATE Latin1_General_100_CI_AS NOT NULL,
 CONSTRAINT [PK_ResrepSnapshot_InternalGuidKey] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[NexusNexusSnapshot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[NexusNexusSnapshot] -- TODO: finish rebuilding implementation
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.ResrepSnapshotXml,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsResrepSnapshot AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusResrepGpsOrganization]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusResrepGpsOrganization]
AS
SELECT RR.RecordGuidKey, RR.InfosetGuidKey, RR.EntityName, RR.EntityNature, 
LL.Latitude, LL.Longitude, LL.Telephone
FROM dbo.NpdsResrepRoot as RR 
INNER JOIN dbo.NpdsCoreAudit AS RA ON  (RR.AuditGuidRef = RA.AuditGuidKey)
INNER JOIN dbo.NpdsDoorsLocation as LL ON (LL.RecordGuidRef = RR.RecordGuidKey)
INNER JOIN dbo.NpdsCoreAudit AS LA ON  (LL.AuditGuidRef = LA.AuditGuidKey)
WHERE (RR.EntityTypeCodeRef = 40) -- Organization
AND (LL.Latitude IS NOT NULL) AND (LL.Longitude IS NOT NULL) 
AND (RA.IsDeleted = 0) AND (LA.IsDeleted = 0)
GO
/****** Object:  View [dbo].[NexusPortalSnapshot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusPortalSnapshot] -- TODO: finish rebuilding implementation
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.ResrepSnapshotXml,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsResrepSnapshot AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  View [dbo].[NexusResrepGpsPerson]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[NexusResrepGpsPerson]
AS
SELECT RR.RecordGuidKey, RR.InfosetGuidKey, RR.EntityName, RR.EntityNature, 
LL.Latitude, LL.Longitude, LL.Telephone
FROM dbo.NpdsResrepRoot as RR 
INNER JOIN dbo.NpdsCoreAudit AS RA ON  (RR.AuditGuidRef = RA.AuditGuidKey)
INNER JOIN dbo.NpdsDoorsLocation as LL ON (LL.RecordGuidRef = RR.RecordGuidKey)
INNER JOIN dbo.NpdsCoreAudit AS LA ON  (LL.AuditGuidRef = LA.AuditGuidKey)
WHERE (RR.EntityTypeCodeRef = 50) -- Person
AND (LL.Latitude IS NOT NULL) AND (LL.Longitude IS NOT NULL) 
AND (RA.IsDeleted = 0) AND (LA.IsDeleted = 0)
GO
/****** Object:  View [dbo].[NexusDoorsSnapshot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[NexusDoorsSnapshot] -- TODO: finish rebuilding implementation
AS --  PRT = Parent Record Table, PAT = Parent Audit Table, CRT = Child Record Table, CAT = Child Audit Table
SELECT CRT.FgroupGuidKey, CRT.RecordGuidRef, CRT.AuditGuidRef, CRT.ResrepSnapshotXml,
	CAT.InfosetTypeCodeRef, IT.TypeName AS InfosetTypeName,
	CAT.HasIndex, CAT.HasPriority, CAT.IsDeleted, CAT.IsMarked, CAT.IsPrincipal,   
	PAT.ManagedByAgentGuidRef, MA.IdentityUserName AS ManagedByAgentUserName,
	CAT.CreatedOn, CAT.CreatedByAgentGuidRef, CA.IdentityUserName AS CreatedByAgentUserName,
	CAT.UpdatedOn, CAT.UpdatedByAgentGuidRef, UA.IdentityUserName AS UpdatedByAgentUserName,
	CAT.DeletedOn, CAT.DeletedByAgentGuidRef, DA.IdentityUserName AS DeletedByAgentUserName
FROM dbo.NpdsResrepRoot AS PRT INNER
JOIN dbo.NpdsCoreAudit AS PAT ON (PAT.AuditGuidKey = PRT.AuditGuidRef) INNER
JOIN dbo.NpdsResrepSnapshot AS CRT ON (PRT.RecordGuidKey = CRT.RecordGuidRef) INNER
JOIN dbo.NpdsCoreAudit AS CAT ON (CAT.AuditGuidKey = CRT.AuditGuidRef) INNER
JOIN dbo.NpdsCoreInfosetTypeEnum AS IT ON (IT.CodeKey = CAT.InfosetTypeCodeRef) INNER
JOIN dbo.CoreSessionAgent AS MA ON (MA.AgentGuidKey = PAT.ManagedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS CA ON (CA.AgentGuidKey = CAT.CreatedByAgentGuidRef) INNER
JOIN dbo.CoreSessionAgent AS UA ON (UA.AgentGuidKey = CAT.UpdatedByAgentGuidRef) LEFT OUTER 
JOIN dbo.CoreSessionAgent AS DA ON (DA.AgentGuidKey = CAT.DeletedByAgentGuidRef)
GO
/****** Object:  Table [dbo].[NpdsCoreUserIdentitySystem]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NpdsCoreUserIdentitySystem](
	[SystemIidKey] [int] NOT NULL,
	[SystemGuidKey] [uniqueidentifier] NOT NULL,
	[SystemName] [nvarchar](64) COLLATE Latin1_General_100_CI_AI NOT NULL,
	[SystemDescription] [nvarchar](256) COLLATE Latin1_General_100_CI_AI NULL,
	[ProviderName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NULL,
	[DatabaseName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NULL,
	[SchemaName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NULL,
	[ObjectQualifierName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NULL,
	[ApplicationName] [nvarchar](32) COLLATE Latin1_General_100_CI_AI NULL,
	[Comment] [nvarchar](256) COLLATE Latin1_General_100_CI_AS NULL,
 CONSTRAINT [PK_AUserIdentitySystemGuidKey] PRIMARY KEY CLUSTERED 
(
	[SystemGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_AUserAccountSystemGuidKey]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE NONCLUSTERED INDEX [IX_AUserAccountSystemGuidKey] ON [dbo].[NpdsCoreUserIdentitySystem]
(
	[SystemGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AUserAccountSystemName]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE NONCLUSTERED INDEX [IX_AUserAccountSystemName] ON [dbo].[NpdsCoreUserIdentitySystem]
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NAccess_AgentGuidRef]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE NONCLUSTERED INDEX [IX_NAccess_AgentGuidRef] ON [dbo].[NpdsResrepAccessAuth]
(
	[AccessRequestedForAgentGuidRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NpdsResrepRoot_EntityTypeCode]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE NONCLUSTERED INDEX [IX_NpdsResrepRoot_EntityTypeCode] ON [dbo].[NpdsResrepRoot]
(
	[EntityTypeCodeRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NpdsResrepRoot_InfosetStatus]    Script Date: 2/23/2023 4:50:52 PM ******/
CREATE NONCLUSTERED INDEX [IX_NpdsResrepRoot_InfosetStatus] ON [dbo].[NpdsResrepRoot]
(
	[InfosetPortalStatusCode] ASC,
	[InfosetDoorsStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_InfosetTypeCodeRef]  DEFAULT ((0)) FOR [InfosetTypeCodeRef]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_NpdsCoreAudit_InfosetFormatCodeRef]  DEFAULT ((0)) FOR [FieldFormatCodeRef]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_AuditGuidKey]  DEFAULT (newid()) FOR [AuditGuidKey]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_HasAccess]  DEFAULT ((0)) FOR [HasAccess]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_HasIndex]  DEFAULT ((0)) FOR [HasIndex]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_HasPriority]  DEFAULT ((0)) FOR [HasPriority]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsCached]  DEFAULT ((0)) FOR [IsCached]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsConceptLabel]  DEFAULT ((0)) FOR [IsConceptLabel]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsConcise]  DEFAULT ((0)) FOR [IsConcise]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_NpdsCoreAudit_IsExclusionConcept]  DEFAULT ((0)) FOR [IsExcluding]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsGenerating]  DEFAULT ((0)) FOR [IsGenerating]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsLimited]  DEFAULT ((0)) FOR [IsLimited]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsMarked]  DEFAULT ((0)) FOR [IsMarked]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsResolvable]  DEFAULT ((0)) FOR [IsResolvable]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsPrincipal]  DEFAULT ((0)) FOR [IsPrincipal]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsPrivate]  DEFAULT ((0)) FOR [IsPrivate]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsSufficient]  DEFAULT ((0)) FOR [IsSufficient]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_IsWordPhrase]  DEFAULT ((0)) FOR [IsWordPhrase]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[NpdsCoreAudit] ADD  CONSTRAINT [DF_CoreAudit_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[NpdsCoreEntityLabel] ADD  CONSTRAINT [DF_NpdsCoreEntityLabel_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsCoreEntityLabel] ADD  CONSTRAINT [DF_CoreEntityLabel_TagToken]  DEFAULT ('') FOR [TagToken]
GO
ALTER TABLE [dbo].[NpdsCoreEntityLabel] ADD  CONSTRAINT [DF_CoreEntityLabel_LabelUri]  DEFAULT ('') FOR [LabelUri]
GO
ALTER TABLE [dbo].[NpdsCoreEntityLabel] ADD  CONSTRAINT [DF_CoreEntityLabel_ServiceType]  DEFAULT ((0)) FOR [ServiceTypeCodeRef]
GO
ALTER TABLE [dbo].[NpdsCoreEntityLabel] ADD  CONSTRAINT [DF_CoreEntityLabel_EntityLabel]  DEFAULT ('http://'+CONVERT([nvarchar](256),newid(),(0))) FOR [EntityLabel]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreEntityTypeEnum_TypeDescription]  DEFAULT ('') FOR [TypeDescription]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeIsComponent]  DEFAULT ((0)) FOR [TypeIsComponent]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeIsConstituent]  DEFAULT ((0)) FOR [TypeIsConstituent]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeEditedByAgent]  DEFAULT ((0)) FOR [TypeEditedByAgent]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeEditedByAuthor]  DEFAULT ((0)) FOR [TypeEditedByAuthor]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeEditedByEditor]  DEFAULT ((0)) FOR [TypeEditedByEditor]
GO
ALTER TABLE [dbo].[NpdsCoreEntityTypeEnum] ADD  CONSTRAINT [DF_CoreEntityTypeEnum_TypeEditedByAdmin]  DEFAULT ((1)) FOR [TypeEditedByAdmin]
GO
ALTER TABLE [dbo].[NpdsCoreFieldFormatEnum] ADD  CONSTRAINT [DF_NpdsCoreFieldFormatEnum_FormatName]  DEFAULT ('') FOR [FormatName]
GO
ALTER TABLE [dbo].[NpdsCoreFieldFormatEnum] ADD  CONSTRAINT [DF_NpdsCoreFieldFormatEnum_FormatDescription]  DEFAULT ('') FOR [FormatDescription]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetStatusEnum] ADD  CONSTRAINT [DF_NpdsCoreInfosetStatusEnum_StatusDescription]  DEFAULT ('') FOR [StatusDescription]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetStatusEnum] ADD  CONSTRAINT [DF_CoreInfosetStatusEnum_StatusEditedByAgent]  DEFAULT ((0)) FOR [StatusEditedByAgent]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetStatusEnum] ADD  CONSTRAINT [DF_CoreInfosetStatusEnum_StatusEditedByAuthor]  DEFAULT ((0)) FOR [StatusEditedByAuthor]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetStatusEnum] ADD  CONSTRAINT [DF_CoreInfosetStatusEnum_StatusEditedByEditor]  DEFAULT ((0)) FOR [StatusEditedByEditor]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetStatusEnum] ADD  CONSTRAINT [DF_CoreInfosetStatusEnum_StatusEditedByAdmin]  DEFAULT ((1)) FOR [StatusEditedByAdmin]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreInfosetTypeEnum_TypeName]  DEFAULT ('') FOR [TypeName]
GO
ALTER TABLE [dbo].[NpdsCoreInfosetTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreInfosetTypeEnum_TypeDescription]  DEFAULT ('') FOR [TypeDescription]
GO
ALTER TABLE [dbo].[NpdsCoreServiceTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreServiceTypeEnum_TypeName]  DEFAULT ('') FOR [TypeName]
GO
ALTER TABLE [dbo].[NpdsCoreServiceTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreServiceTypeEnum_TypeDescription]  DEFAULT ('') FOR [TypeDescription]
GO
ALTER TABLE [dbo].[NpdsCoreServiceTypeEnum] ADD  CONSTRAINT [DF_NpdsCoreServiceTypeEnum_DefaultGeneratingLabel]  DEFAULT ('') FOR [DefaultGeneratingLabel]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NAgent_InternalGuidKey]  DEFAULT (newid()) FOR [AgentGuidKey]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NAgentIsAuthor]  DEFAULT ((1)) FOR [AgentIsAuthor]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NAgentIsEditor]  DEFAULT ((0)) FOR [AgentIsEditor]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NAgentIsAdmin]  DEFAULT ((0)) FOR [AgentIsAdmin]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NAgent_SessionGuidKey]  DEFAULT (newid()) FOR [SessionGuidKey]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NpdsCoreSessionAgent_IdentitySystemGuidRef]  DEFAULT ('337937A6-DD25-4883-A588-675DD755FC43') FOR [IdentitySystemGuidRef]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] ADD  CONSTRAINT [DF_NpdsCoreSessionAgent_IdentityUserNameDisplayed]  DEFAULT ('') FOR [IdentityUserNameDisplayed]
GO
ALTER TABLE [dbo].[NpdsCoreUserIdentitySystem] ADD  CONSTRAINT [DF_AUserAccountSystemGuidKey]  DEFAULT (newid()) FOR [SystemGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsDescription] ADD  CONSTRAINT [DF_NpdsDoorsDescription_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsDescription] ADD  CONSTRAINT [DF_DoorsDescription_Description]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[NpdsDoorsDistribution] ADD  CONSTRAINT [DF_NpdsDoorsDistribution_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsDistribution] ADD  CONSTRAINT [DF_DoorsDistribution_Distribution]  DEFAULT ('') FOR [Distribution]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_NpdsDoorsFairMetric_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_M_InvalidOldClaim]  DEFAULT ((0)) FOR [M_InvalidOldClaim]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_Q_ValidOldClaim]  DEFAULT ((0)) FOR [Q_ValidOldClaim]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_P_InvalidNewClaim]  DEFAULT ((0)) FOR [P_InvalidNewClaim]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_N_ValidNewClaim]  DEFAULT ((0)) FOR [N_ValidNewClaim]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_FAIR1]  DEFAULT ((0)) FOR [FAIR1]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_FAIR2]  DEFAULT ((0)) FOR [FAIR2]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_FAIR3]  DEFAULT ((0)) FOR [FAIR3]
GO
ALTER TABLE [dbo].[NpdsDoorsFairMetric] ADD  CONSTRAINT [DF_DoorsFairMetric_FAIR4]  DEFAULT ((0)) FOR [FAIR4]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_DoorsLocation_Location]  DEFAULT ('') FOR [Location]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_DisplayText]  DEFAULT ('') FOR [DisplayText]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_DisplayImageUrl]  DEFAULT ('') FOR [DisplayImageUrl]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_UrlWebAddress]  DEFAULT ('') FOR [UrlWebAddress]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_EmailAddress]  DEFAULT ('') FOR [EmailAddress]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_StreetAddress]  DEFAULT ('') FOR [StreetAddress]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_ExtendedAddress]  DEFAULT ('') FOR [ExtendedAddress]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_FormattedAddress]  DEFAULT ('') FOR [FormattedAddress]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_CityLocality]  DEFAULT ('') FOR [CityLocality]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_StateRegion]  DEFAULT ('') FOR [StateRegion]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_Country]  DEFAULT ('') FOR [Country]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_PostalCode]  DEFAULT ('') FOR [PostalCode]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_Telephone]  DEFAULT ('') FOR [Telephone]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_GeocodeType]  DEFAULT ('') FOR [GeocodeType]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_GeocodeConfidence]  DEFAULT ('') FOR [GeocodeConfidence]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_Latitude]  DEFAULT ((0)) FOR [Latitude]
GO
ALTER TABLE [dbo].[NpdsDoorsLocation] ADD  CONSTRAINT [DF_NpdsDoorsLocation_Longitude]  DEFAULT ((0)) FOR [Longitude]
GO
ALTER TABLE [dbo].[NpdsDoorsProvenance] ADD  CONSTRAINT [DF_NpdsDoorsProvenance_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsDoorsProvenance] ADD  CONSTRAINT [DF_DoorsProvenance_Provenance]  DEFAULT ('') FOR [Provenance]
GO
ALTER TABLE [dbo].[NpdsPortalCrossReference] ADD  CONSTRAINT [DF_NpdsPortalCrossReference_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsPortalCrossReference] ADD  CONSTRAINT [DF_PortalCrossReference_CrossReference]  DEFAULT ('') FOR [CrossReference]
GO
ALTER TABLE [dbo].[NpdsPortalOtherText] ADD  CONSTRAINT [DF_NpdsPortalOtherText_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsPortalOtherText] ADD  CONSTRAINT [DF_PortalOtherText_OtherText]  DEFAULT ('') FOR [OtherText]
GO
ALTER TABLE [dbo].[NpdsPortalSupportingLabel] ADD  CONSTRAINT [DF_NpdsPortalSupportingLabel_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsPortalSupportingLabel] ADD  CONSTRAINT [DF_PortalSupportingLabel_SupportingLabel]  DEFAULT ('') FOR [SupportingLabel]
GO
ALTER TABLE [dbo].[NpdsPortalSupportingTag] ADD  CONSTRAINT [DF_NpdsPortalSupportingTag_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsPortalSupportingTag] ADD  CONSTRAINT [DF_PortalSupportingTag_SupportingTag]  DEFAULT ('') FOR [SupportingTag]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] ADD  CONSTRAINT [DF_NpdsResrepAccessAuth_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] ADD  CONSTRAINT [DF_ResrepAccessAuth_RequestIsApproved]  DEFAULT ((0)) FOR [RequestIsApproved]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] ADD  CONSTRAINT [DF_ResrepAccessAuth_RequestIsDenied]  DEFAULT ((0)) FOR [RequestIsDenied]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] ADD  CONSTRAINT [DF_ResrepAccessAuth_AgentHasRecordAccess]  DEFAULT ((0)) FOR [AuthorHasResrepAccess]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_EntityTypeCode]  DEFAULT ((0)) FOR [EntityTypeCodeRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_EntityInitialTag]  DEFAULT ('') FOR [EntityInitialTag]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_EntityName]  DEFAULT ('') FOR [EntityName]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_EntityNature]  DEFAULT ('') FOR [EntityNature]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_RecordGuidKey]  DEFAULT (newid()) FOR [RecordGuidKey]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetGuidKey]  DEFAULT (newid()) FOR [InfosetGuidKey]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetIsAuthorPrivate]  DEFAULT ((0)) FOR [InfosetIsAuthorPrivate]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetIsAgentPublic]  DEFAULT ((0)) FOR [InfosetIsAgentShared]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetIsUpdaterLimited]  DEFAULT ((0)) FOR [InfosetIsUpdaterLimited]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetIsManagerReleased]  DEFAULT ((0)) FOR [InfosetIsManagerReleased]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetResrepEntityStatusCode]  DEFAULT ((0)) FOR [InfosetResrepEntityStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetResrepRecordStatusCode]  DEFAULT ((0)) FOR [InfosetResrepRecordStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetResrepInfosetStatusCode]  DEFAULT ((0)) FOR [InfosetResrepInfosetStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetEntityLabelsCount]  DEFAULT ((0)) FOR [InfosetEntityLabelsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusEntityLabels]  DEFAULT ((0)) FOR [InfosetEntityLabelsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetSupportingTagsCount]  DEFAULT ((0)) FOR [InfosetSupportingTagsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusSupportingTags]  DEFAULT ((0)) FOR [InfosetSupportingTagsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetSupportingLabelsCount]  DEFAULT ((0)) FOR [InfosetSupportingLabelsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusSupportingLabels]  DEFAULT ((0)) FOR [InfosetSupportingLabelsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetCrossReferencesCount]  DEFAULT ((0)) FOR [InfosetCrossReferencesCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusCrossReferences]  DEFAULT ((0)) FOR [InfosetCrossReferencesStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetOtherTextsCount]  DEFAULT ((0)) FOR [InfosetOtherTextsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusOtherTexts]  DEFAULT ((0)) FOR [InfosetOtherTextsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetLocationsCount]  DEFAULT ((0)) FOR [InfosetLocationsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusLocations]  DEFAULT ((0)) FOR [InfosetLocationsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetDescriptionsCount]  DEFAULT ((0)) FOR [InfosetDescriptionsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusDescriptions]  DEFAULT ((0)) FOR [InfosetDescriptionsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetProvenancesCount]  DEFAULT ((0)) FOR [InfosetProvenancesCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusProvenances]  DEFAULT ((0)) FOR [InfosetProvenancesStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetDistributionsCount]  DEFAULT ((0)) FOR [InfosetDistributionsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusDistributions]  DEFAULT ((0)) FOR [InfosetDistributionsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetFairMetricsCount]  DEFAULT ((0)) FOR [InfosetFairMetricsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusFairMetrics]  DEFAULT ((0)) FOR [InfosetFairMetricsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetPortalStatusCode]  DEFAULT ((0)) FOR [InfosetPortalStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetPortalSnapshotsCount]  DEFAULT ((0)) FOR [InfosetPortalSnapshotsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetPortalSnapshotsStatusCode]  DEFAULT ((0)) FOR [InfosetPortalSnapshotsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetDoorsStatusCode]  DEFAULT ((0)) FOR [InfosetDoorsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetDoorsSnapshotsCount]  DEFAULT ((0)) FOR [InfosetDoorsSnapshotsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetDoorsSnapshotsStatusCode]  DEFAULT ((0)) FOR [InfosetDoorsSnapshotsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetNexusStatusCode]  DEFAULT ((0)) FOR [InfosetNexusStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetSnapshotsCount]  DEFAULT ((0)) FOR [InfosetNexusSnapshotsCount]
GO
ALTER TABLE [dbo].[NpdsResrepRoot] ADD  CONSTRAINT [DF_NpdsResrepRoot_InfosetStatusSnapshots]  DEFAULT ((0)) FOR [InfosetNexusSnapshotsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepSnapshot] ADD  CONSTRAINT [DF_NpdsResrepSnapshot_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsResrepSnapshot] ADD  CONSTRAINT [DF_ResrepSnapshot_ResrepSnapshotJson]  DEFAULT ('') FOR [ResrepSnapshotJson]
GO
ALTER TABLE [dbo].[NpdsResrepSnapshot] ADD  CONSTRAINT [DF_ResrepSnapshot_ResrepSnapshotXml]  DEFAULT ('') FOR [ResrepSnapshotXml]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] ADD  CONSTRAINT [DF_NpdsServiceAccessAuth_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] ADD  CONSTRAINT [DF_ServiceAccessAuth_RequestIsApproved]  DEFAULT ((0)) FOR [RequestIsApproved]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] ADD  CONSTRAINT [DF_ServiceAccessAuth_RequestIsDenied]  DEFAULT ((0)) FOR [RequestIsDenied]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] ADD  CONSTRAINT [DF_ServiceAccessAuth_EditorAccessIsApproved]  DEFAULT ((0)) FOR [EditorHasServiceAccess]
GO
ALTER TABLE [dbo].[NpdsServiceDefault] ADD  CONSTRAINT [DF_NpdsServiceDefault_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionAnd] ADD  CONSTRAINT [DF_ServiceRestrictionAnd_RestrictionAndGuidKey]  DEFAULT (newid()) FOR [RestrictionAndGuidKey]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionAnd] ADD  CONSTRAINT [DF_ServiceRestrictionAnd_RestrictionName]  DEFAULT ('') FOR [RestrictionName]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr] ADD  CONSTRAINT [DF_ServiceRestrictionOr_AuditGuidRef]  DEFAULT (newid()) FOR [AuditGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr] ADD  CONSTRAINT [DF_ServiceRestrictionOr_RestrictionOrGuidKey]  DEFAULT (newid()) FOR [RestrictionOrGuidKey]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr] ADD  CONSTRAINT [DF_ServiceRestrictionOr_RestrictionValue]  DEFAULT ('') FOR [RestrictionValue]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent]  WITH NOCHECK ADD  CONSTRAINT [FK_CoreSessionAgent_AgentInfosetGuidRef] FOREIGN KEY([AgentInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] CHECK CONSTRAINT [FK_CoreSessionAgent_AgentInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent]  WITH NOCHECK ADD  CONSTRAINT [FK_CoreSessionAgent_IdentitySystemGuidRef] FOREIGN KEY([IdentitySystemGuidRef])
REFERENCES [dbo].[NpdsCoreUserIdentitySystem] ([SystemGuidKey])
GO
ALTER TABLE [dbo].[NpdsCoreSessionAgent] CHECK CONSTRAINT [FK_CoreSessionAgent_IdentitySystemGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth]  WITH NOCHECK ADD  CONSTRAINT [FK_ResrepAccessAuth_ApprovedByAgentGuidRef] FOREIGN KEY([AccessApprovedByAgentGuidRef])
REFERENCES [dbo].[NpdsCoreSessionAgent] ([AgentGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] CHECK CONSTRAINT [FK_ResrepAccessAuth_ApprovedByAgentGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth]  WITH NOCHECK ADD  CONSTRAINT [FK_ResrepAccessAuth_RequestedForAgentGuidRef] FOREIGN KEY([AccessRequestedForAgentGuidRef])
REFERENCES [dbo].[NpdsCoreSessionAgent] ([AgentGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepAccessAuth] CHECK CONSTRAINT [FK_ResrepAccessAuth_RequestedForAgentGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH CHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_AuditGuidRef] FOREIGN KEY([AuditGuidRef])
REFERENCES [dbo].[NpdsCoreAudit] ([AuditGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_AuditGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_EntityContactInfosetGuidRef] FOREIGN KEY([EntityContactInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_EntityContactInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_EntityOtherInfosetGuidRef] FOREIGN KEY([EntityOtherInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_EntityOtherInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_EntityOwnerInfosetGuidRef] FOREIGN KEY([EntityOwnerInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_EntityOwnerInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_EntityTypeCodeRef] FOREIGN KEY([EntityTypeCodeRef])
REFERENCES [dbo].[NpdsCoreEntityTypeEnum] ([CodeKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_EntityTypeCodeRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_InfosetDoorsStatusCode] FOREIGN KEY([InfosetDoorsStatusCode])
REFERENCES [dbo].[NpdsCoreInfosetStatusEnum] ([CodeKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_InfosetDoorsStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_InfosetPortalStatusCode] FOREIGN KEY([InfosetPortalStatusCode])
REFERENCES [dbo].[NpdsCoreInfosetStatusEnum] ([CodeKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_InfosetPortalStatusCode]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_RecordDirectoryInfosetGuidRef] FOREIGN KEY([RecordDirectoryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_RecordDirectoryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH CHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_RecordDiristryInfosetGuidRef] FOREIGN KEY([RecordDiristryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_RecordDiristryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_RecordRegistrantInfosetGuidRef] FOREIGN KEY([RecordRegistrantInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_RecordRegistrantInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_RecordRegistrarInfosetGuidRef] FOREIGN KEY([RecordRegistrarInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_RecordRegistrarInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsResrepRoot]  WITH NOCHECK ADD  CONSTRAINT [FK_NpdsResrepRoot_RecordRegistryInfosetGuidRef] FOREIGN KEY([RecordRegistryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsResrepRoot] CHECK CONSTRAINT [FK_NpdsResrepRoot_RecordRegistryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceAccessAuth_ApprovedByAgentGuidRef] FOREIGN KEY([AccessApprovedByAgentGuidRef])
REFERENCES [dbo].[NpdsCoreSessionAgent] ([AgentGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] CHECK CONSTRAINT [FK_ServiceAccessAuth_ApprovedByAgentGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceAccessAuth_RequestedForAgentGuidRef] FOREIGN KEY([AccessRequestedForAgentGuidRef])
REFERENCES [dbo].[NpdsCoreSessionAgent] ([AgentGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceAccessAuth] CHECK CONSTRAINT [FK_ServiceAccessAuth_RequestedForAgentGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceDefault]  WITH CHECK ADD  CONSTRAINT [FK_ServiceDefault_DirectoryInfosetGuidRef] FOREIGN KEY([DirectoryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceDefault] CHECK CONSTRAINT [FK_ServiceDefault_DirectoryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceDefault]  WITH CHECK ADD  CONSTRAINT [FK_ServiceDefault_DiristryInfosetGuidRef] FOREIGN KEY([DiristryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceDefault] CHECK CONSTRAINT [FK_ServiceDefault_DiristryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceDefault]  WITH CHECK ADD  CONSTRAINT [FK_ServiceDefault_RegistrarInfosetGuidRef] FOREIGN KEY([RegistrarInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceDefault] CHECK CONSTRAINT [FK_ServiceDefault_RegistrarInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceDefault]  WITH CHECK ADD  CONSTRAINT [FK_ServiceDefault_RegistryInfosetGuidRef] FOREIGN KEY([RegistryInfosetGuidRef])
REFERENCES [dbo].[NpdsResrepRoot] ([InfosetGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceDefault] CHECK CONSTRAINT [FK_ServiceDefault_RegistryInfosetGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRestrictionOr_AuditGuidRef] FOREIGN KEY([AuditGuidRef])
REFERENCES [dbo].[NpdsCoreAudit] ([AuditGuidKey])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr] CHECK CONSTRAINT [FK_ServiceRestrictionOr_AuditGuidRef]
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRestrictionOr_RestrictionAndGuidRef] FOREIGN KEY([RestrictionAndGuidRef])
REFERENCES [dbo].[NpdsServiceRestrictionAnd] ([RestrictionAndGuidKey])
GO
ALTER TABLE [dbo].[NpdsServiceRestrictionOr] CHECK CONSTRAINT [FK_ServiceRestrictionOr_RestrictionAndGuidRef]
GO
/****** Object:  StoredProcedure [dbo].[CoreDefaultServices]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CoreDefaultServices](
@ServicePTag NVARCHAR(128),
@DiristryGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@RegistryGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@DirectoryGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@RegistrarGuid UNIQUEIDENTIFIER = NULL OUTPUT)
AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount INT = 0, @errorCode INT = 0;

	SELECT 
    @DiristryGuid = DefDiristryGuid, 
    @RegistryGuid = DefRegistryGuid, 
    @DirectoryGuid = DefDirectoryGuid, 
    @RegistrarGuid = DefRegistrarGuid
    FROM dbo.CoreServiceDefault
		WHERE ServicePTag = @ServicePTag;
  
  IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -11;
	IF (@RegistrarGuid IS NULL) RETURN -12;

  IF (@DiristryGuid IS NULL) SET @DiristryGuid = @EmptyGuid;
	IF (@RegistryGuid IS NULL) SET @RegistryGuid = @EmptyGuid;
	IF (@DirectoryGuid IS NULL) SET @DirectoryGuid = @EmptyGuid;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[CoreRandomCharHandleGenerate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CoreRandomCharHandleGenerate]
(@Handle char(9) output)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @nc tinyint, @rows tinyint, @loops tinyint;
	SET @nc = 9;

	EXEC dbo.CoreRandomHexStrVarCharGenerate @nc, @Handle output;
	SELECT @rows = COUNT(*) FROM dbo.NpdsResrepRoot WHERE (RecordHandle = @Handle);
	-- (@rows > 0) implies @Handle already exists and thus is duplicate or replicate
	SET @loops = 1;
	WHILE (@rows > 0) AND (@loops < 100) BEGIN
		EXEC dbo.CoreRandomHexStrVarCharGenerate @nc, @Handle output;	
		SELECT @rows = COUNT(*) FROM dbo.NpdsResrepRoot WHERE (RecordHandle = @Handle);
		SET @loops = @loops + 1;	
	END;
	-- (@rows = 0) implies new random @Handle is unique 
	-- (also indicates "no error" return value)
	RETURN @rows;

END
GO
/****** Object:  StoredProcedure [dbo].[CoreRandomHexStrVarCharGenerate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CoreRandomHexStrVarCharGenerate]
(@NumChars tinyint = 9,
 @HexStr varchar(12) output)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF (@NumChars < 1) OR (@NumChars > 12) RETURN -1;
	
	-- TODO: recode to delete four hyphens in newid and allow up to (@NumChars > 32)

	SET @HexStr = CHAR(ROUND(25*RAND()+65,0)) + RIGHT(CAST(NEWID() AS char(36)), (@NumChars-1));
	
	RETURN ISNULL(@@ROWCOUNT,0);

END

-- test with T-SQL code:
--		declare @nc tinyint, @res varchar(36), @ret int;
--		set @nc = 12;
--		exec @ret = CoreRandomHexStrVarCharGenerate @nc, @res output;
--		select @ret, @res;
GO
/****** Object:  StoredProcedure [dbo].[CoreRandomIndexGenerate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CoreRandomIndexGenerate]
(@InfosetTypeCodeRef SMALLINT = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@RandIndex SMALLINT = 0 OUTPUT)
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF (@InfosetTypeCodeRef IS NULL) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;

	DECLARE @loval SMALLINT = 0, @hival SMALLINT = 255, 
    @rows SMALLINT = 0, @loops SMALLINT = 1, @maxloops SMALLINT = 100;

	-- EXEC dbo.CoreRandomIntegerGenerate @loval, @hival, @RandIndex output;	
	SET @RandIndex = FLOOR(RAND()*256);	
	SELECT @rows = COUNT(*) FROM NpdsCoreAudit WHERE (InfosetTypeCodeRef = @InfosetTypeCodeRef) 
		AND (RecordGuidRef = @RecordGuidRef) AND (HasIndex = @RandIndex);	
	-- (@rows > 0) implies @HasIndex already exists and thus is duplicate or replicate
	WHILE (@rows > 0) AND (@loops < @maxloops) BEGIN
		-- EXEC dbo.CoreRandomIntegerGenerate @loval, @hival, @RandIndex output;	
		SET @RandIndex = FLOOR(RAND()*256);	
		SELECT @rows = COUNT(*) FROM NpdsCoreAudit WHERE (InfosetTypeCodeRef = @InfosetTypeCodeRef) 
			AND (RecordGuidRef = @RecordGuidRef) AND (HasIndex = @RandIndex);	
		SET @loops = @loops + 1;	
	END;
	-- (@rows = 0) implies new random @HasIndex is unique 
	-- (also indicates "no error" return value)
	RETURN @rows;

END;
GO
/****** Object:  StoredProcedure [dbo].[CoreRandomIntegerGenerate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CoreRandomIntegerGenerate]
(@loval INTEGER = 0,
@hival INTEGER = 100,
@randval INTEGER = 0 OUTPUT)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT @randval = FLOOR(RAND()*(@hival-@loval+1))+@loval;
	RETURN ISNULL(@@ROWCOUNT,0);

END
GO
/****** Object:  StoredProcedure [dbo].[CoreSessionAgentAddRole]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CoreSessionAgentAddRole] (
@UserGuid UNIQUEIDENTIFIER = NULL,
@AgentGuid UNIQUEIDENTIFIER = NULL,
@AgentIsAuthor bit = 0,
@AgentIsEditor bit = 0,
@AgentIsAdmin bit = 0
) AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF ((@UserGuid IS NULL) OR (@UserGuid = @EmptyGuid)) RETURN -11;
	IF ((@AgentGuid IS NULL) OR (@AgentGuid = @EmptyGuid)) RETURN -12;	
		
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	DECLARE @dateExpired DATETIME2 =  DATEADD(HOUR, 8, @utcDate);

	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreSessionAgent
		WHERE (IdentityUserGuidRef = @UserGuid) AND (AgentGuidKey = @AgentGuid);
 	SELECT  @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

		UPDATE dbo.NpdsCoreSessionAgent 
		   SET AgentIsAuthor = @AgentIsAuthor, AgentIsEditor = @AgentIsEditor, AgentIsAdmin = @AgentIsAdmin
		   WHERE  (IdentityUserGuidRef = @UserGuid) AND (AgentGuidKey = @AgentGuid);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -32;

    RETURN 0; -- no errors

END

GO
/****** Object:  StoredProcedure [dbo].[CoreSessionAgentCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CoreSessionAgentCheck] (
@IdentityAppGuid UNIQUEIDENTIFIER = NULL,
@SessionValueIsRequired BIT = 0,
@SessionGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@UserGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@AgentGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@AgentInfosetGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@AgentUserNameDisp NVARCHAR(64) = '' OUTPUT,
@AgentIsAuthor BIT = 0 OUTPUT,
@AgentIsEditor BIT = 0 OUTPUT,
@AgentIsAdmin BIT = 0 OUTPUT
) AS BEGIN

	SET NOCOUNT ON;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	DECLARE @AppGuidExists BIT = 1, @UserGuidExists BIT= 1, 
		@AgentGuidExists BIT = 1, @SessionGuidExists BIT = 1;

	IF ((@IdentityAppGuid IS NULL) OR (@IdentityAppGuid = @EmptyGuid)) SET @AppGuidExists = 0;
	IF ((@SessionGuid IS NULL) OR (@SessionGuid = @EmptyGuid)) SET @SessionGuidExists = 0;
	IF ((@UserGuid IS NULL) OR (@UserGuid = @EmptyGuid)) SET @UserGuidExists = 0;
	IF ((@AgentGuid IS NULL) OR (@AgentGuid = @EmptyGuid)) SET @AgentGuidExists = 0;

	IF (@AppGuidExists = 0) RETURN -11;
	IF ((@AgentGuidExists = 0) AND (@UserGuidExists = 0) AND (@SessionGuidExists = 0)) RETURN -12;
	IF ((@SessionGuidExists = 0) AND (@SessionValueIsRequired = 1)) RETURN -13;

	IF (@AgentGuidExists = 0) AND (@SessionGuidExists = 1) BEGIN -- get AgentGuid via SessionGuid and AppGuid
		SELECT @AgentGuid = AgentGuidKey FROM dbo.NpdsCoreSessionAgent
			WHERE (SessionGuidKey = @SessionGuid) AND (IdentityApplicationGuidRef = @IdentityAppGuid);
		IF ((@AgentGuid IS NULL) OR (@AgentGuid = @EmptyGuid)) RETURN -14;
	END;

	IF (@AgentGuidExists = 0) AND (@UserGuidExists = 1) BEGIN -- get AgentGuid via UserGuid and AppGuid
		SELECT @AgentGuid = AgentGuidKey FROM dbo.NpdsCoreSessionAgent
			WHERE (IdentityUserGuidRef = @UserGuid) AND (IdentityApplicationGuidRef = @IdentityAppGuid);
		IF ((@AgentGuid IS NULL) OR (@AgentGuid = @EmptyGuid)) RETURN -15;
	END;

	IF (@SessionValueIsRequired = 1) BEGIN -- require known non-expired session value

		UPDATE dbo.NpdsCoreSessionAgent SET SessionDateAccessed = @utcDate
			WHERE (AgentGuidKey = @AgentGuid) AND (SessionGuidKey = @SessionGuid)
			AND (@utcDate < SessionDateExpired); -- current time before expiry date
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;

		SELECT @UserGuid = IdentityUserGuidRef, 
			@AgentInfosetGuid = AgentInfosetGuidRef, @AgentUserNameDisp = IdentityUserNameDisplayed,
			@AgentIsAuthor = AgentIsAuthor, @AgentIsEditor = AgentIsEditor, @AgentIsAdmin = AgentIsAdmin
			FROM dbo.NpdsCoreSessionAgent 
			WHERE (AgentGuidKey = @AgentGuid) AND (SessionGuidKey = @SessionGuid) 
			AND (@utcDate < SessionDateExpired); -- current time before expiry date
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

	END ELSE BEGIN -- require known agent value only (without session value)

		SELECT @UserGuid = IdentityUserGuidRef, @SessionGuid = SessionGuidKey,
			@AgentInfosetGuid = AgentInfosetGuidRef, @AgentUserNameDisp = IdentityUserNameDisplayed,
			@AgentIsAuthor = AgentIsAuthor, @AgentIsEditor = AgentIsEditor, @AgentIsAdmin = AgentIsAdmin
			FROM dbo.NpdsCoreSessionAgent WHERE (AgentGuidKey = @AgentGuid);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;

	END;

    RETURN 0; -- no errors

END
GO
/****** Object:  StoredProcedure [dbo].[CoreSessionAgentEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CoreSessionAgentEdit] (
@IdentityAppGuid UNIQUEIDENTIFIER = NULL,
@IdentityUserGuid UNIQUEIDENTIFIER = NULL,
@IdentityUserNameDisp nvarchar(64) = NULL,
@AgentGuid UNIQUEIDENTIFIER = NULL OUTPUT,
@SessionGuid UNIQUEIDENTIFIER = NULL OUTPUT
) AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF ((@IdentityAppGuid IS NULL) OR (@IdentityAppGuid = @EmptyGuid)) RETURN -11;
	IF ((@IdentityUserGuid IS NULL) OR (@IdentityUserGuid = @EmptyGuid)) RETURN -12;	
	IF ((@IdentityUserNameDisp IS NULL) OR (@IdentityUserNameDisp = '')) RETURN -13;	
	IF ((@SessionGuid IS NULL) OR (@SessionGuid = @EmptyGuid)) SELECT @SessionGuid = NEWID(); 
		
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	DECLARE @dateExpired DATETIME2 =  DATEADD(HOUR, 8, @utcDate);

	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreSessionAgent
		WHERE (IdentityUserGuidRef = @IdentityUserGuid);
	IF (@rowCount > 1) BEGIN
		SELECT @AgentGuid = AgentGuidKey FROM dbo.NpdsCoreSessionAgent
			WHERE (IdentityUserGuidRef = @IdentityUserGuid) 
			AND (IdentityApplicationGuidRef = @IdentityAppGuid);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	END ELSE IF (@rowCount = 1) BEGIN
		SELECT @AgentGuid = AgentGuidKey FROM dbo.NpdsCoreSessionAgent
			WHERE (IdentityUserGuidRef = @IdentityUserGuid);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		SET @AgentGuid = NULL;
	END;

	IF (@AgentGuid IS NULL) BEGIN -- record does not exist
		SELECT @AgentGuid = NEWID();
		INSERT INTO dbo.NpdsCoreSessionAgent 
			(AgentGuidKey, IdentityApplicationGuidRef, IdentityUserGuidRef, IdentityUserNameDisplayed,  
			 SessionGuidKey, SessionDateCreated, SessionDateAccessed, SessionDateExpired) 
		VALUES 
			(@AgentGuid, @IdentityAppGuid, @IdentityUserGuid, @IdentityUserNameDisp,
			 @SessionGuid, @utcDate, @utcDate, @dateExpired);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;
	END ELSE BEGIN -- record exists but requires session reset/refresh
		UPDATE dbo.NpdsCoreSessionAgent 
		   SET IdentityUserNameDisplayed = @IdentityUserNameDisp,
		   SessionGuidKey = @SessionGuid, SessionDateCreated = @utcDate, 
		   SessionDateAccessed = @utcDate, SessionDateExpired = @dateExpired
		   WHERE  (IdentityUserGuidRef = @IdentityUserGuid) AND (AgentGuidKey = @AgentGuid);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -32;
	END;

    RETURN 0; -- no errors

END

GO
/****** Object:  StoredProcedure [dbo].[ScribeConcatRecordsDocByDescription]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeConcatRecordsDocByDescription]
(@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@DiristryTag NVARCHAR(64) = 'PDP-DREAM',
@DocumentEntityType NVARCHAR(32) = 'Bibliography',
@RecordEntityType NVARCHAR(32) = 'Publication',
@RecordCount INT = 0 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
  	IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  	IF (@DocumentEntityType IS NULL) OR (@DocumentEntityType = '') RETURN -12;
  	IF (@RecordEntityType IS NULL) OR (@RecordEntityType = '') RETURN -13;

	-- find most recent record for a @DocumentEntityType in the diristry
	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NEWID(),
		@DocumentRecordGuid UNIQUEIDENTIFIER, @DocumentInfosetGuid UNIQUEIDENTIFIER;
	SELECT TOP 1
		@DocumentRecordGuid = RecordGuidKey, @DocumentInfosetGuid = InfosetGuidKey 
		FROM dbo.NexusResrepRoot
		WHERE (RecordDiristryTag = @DiristryTag) AND (EntityTypeName = @DocumentEntityType)
		ORDER BY RecordUpdatedOn DESC;
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

	DECLARE @RecordGuidRef UNIQUEIDENTIFIER, 
		@Description NVARCHAR(MAX) = '', @DocumentText NVARCHAR(MAX) = '';

	-- concatenate all descriptions from records of @RecordEntityType in the diristry
	DECLARE records CURSOR FOR
		SELECT RecordGuidKey FROM dbo.NexusResrepRoot
		WHERE (RecordDiristryTag = @DiristryTag) AND (EntityTypeName = @RecordEntityType)
		ORDER BY EntityName;

	OPEN records;
	FETCH records INTO @RecordGuidRef;

	WHILE (@@FETCH_STATUS=0) BEGIN
		
		SELECT @Description = [Description]
			FROM dbo.NexusDescription
			WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1);		
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount = 1) AND (@errorCode = 0) 
			SELECT @RecordCount = @RecordCount + 1;
		IF (@Description IS NOT NULL) AND (@Description <> '') 
			SELECT @DocumentText = @DocumentText + @Description;

		FETCH records INTO @RecordGuidRef;
	
	END;

	CLOSE records;
	DEALLOCATE records;

	EXEC @errorCode = dbo.ScribeDescriptionEdit 
		@AgentGuidRef, @DocumentInfosetGuid, @DocumentRecordGuid, @FgroupGuidKey, 
		0, 0, 0, 1, @DocumentText;
	IF (@errorCode <> 0) RETURN @errorCode-100;
	
	RETURN 0; -- no errors

END
GO
/****** Object:  StoredProcedure [dbo].[ScribeConcatRecordsDocByOtherText]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeConcatRecordsDocByOtherText]
(@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@DiristryTag NVARCHAR(64) = 'PDP-DREAM',
@DocumentEntityType NVARCHAR(32) = 'Bibliography',
@RecordEntityType NVARCHAR(32) = 'Publication',
@RecordCount INT = 0 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
  	IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  	IF (@DocumentEntityType IS NULL) OR (@DocumentEntityType = '') RETURN -12;
  	IF (@RecordEntityType IS NULL) OR (@RecordEntityType = '') RETURN -13;

	-- find most recent record for a @DocumentEntityType in the diristry
	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NEWID(),
		@DocumentRecordGuid UNIQUEIDENTIFIER, @DocumentInfosetGuid UNIQUEIDENTIFIER;
	SELECT TOP 1
		@DocumentRecordGuid = RecordGuidKey, @DocumentInfosetGuid = InfosetGuidKey 
		FROM dbo.NexusResrepRoot
		WHERE (RecordDiristryTag = @DiristryTag) AND (EntityTypeName = @DocumentEntityType)
		ORDER BY RecordUpdatedOn DESC;
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

	DECLARE @RecordGuidRef UNIQUEIDENTIFIER, 
		@OtherText NVARCHAR(MAX) = '', @DocumentText NVARCHAR(MAX) = '';

	-- concatenate all descriptions from records of @RecordEntityType in the diristry
	DECLARE records CURSOR FOR
		SELECT RecordGuidKey FROM dbo.NexusResrepRoot
		WHERE (RecordDiristryTag = @DiristryTag) AND (EntityTypeName = @RecordEntityType)
		ORDER BY EntityName;

	OPEN records;
	FETCH records INTO @RecordGuidRef;

	WHILE (@@FETCH_STATUS=0) BEGIN
		
		SELECT @OtherText = OtherText
			FROM dbo.NexusOtherText
			WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1);		
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount = 1) AND (@errorCode = 0) 
			SELECT @RecordCount = @RecordCount + 1;
		IF (@OtherText IS NOT NULL) AND (@OtherText <> '') 
			SELECT @DocumentText = @DocumentText + @OtherText;

		FETCH records INTO @RecordGuidRef;
	
	END;

	CLOSE records;
	DEALLOCATE records;

	EXEC @errorCode = dbo.ScribeOtherTextEdit 
		@AgentGuidRef, @DocumentInfosetGuid, @DocumentRecordGuid, @FgroupGuidKey, 
		0, 0, 0, 1, @DocumentText;
	IF (@errorCode <> 0) RETURN @errorCode-100;
	
	RETURN 0; -- no errors

END
GO
/****** Object:  StoredProcedure [dbo].[ScribeCoreDefaultServiceCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeCoreDefaultServiceCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusCoreServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusCoreServiceDefault 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusCoreServiceDefault
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusCoreServiceDefault 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusCoreServiceDefault
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusCoreServiceDefault 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCoreDefaultServiceDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeCoreDefaultServiceDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsServiceDefault 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeCoreDefaultServiceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCoreDefaultServiceEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeCoreDefaultServiceEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@DiristryIGuidRef UNIQUEIDENTIFIER = NULL,
@RegistryIGuidRef UNIQUEIDENTIFIER = NULL,
@DirectoryIGuidRef UNIQUEIDENTIFIER = NULL,
@RegistrarIGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 12; -- CoreServiceDefault
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			@utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
	 
 	IF (@rowCount = 0) BEGIN	 
	   INSERT INTO dbo.NpdsServiceDefault
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, 
			 DiristryInfosetGuidRef, RegistryInfosetGuidRef, DirectoryInfosetGuidRef, RegistrarInfosetGuidRef) 
		VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, 
			@DiristryIGuidRef, @RegistryIGuidRef, @DirectoryIGuidRef, @RegistrarIGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsServiceDefault SET  
			DiristryInfosetGuidRef = @DiristryIGuidRef, RegistryInfosetGuidRef = @RegistryIGuidRef, 
			DirectoryInfosetGuidRef = @DirectoryIGuidRef, RegistrarInfosetGuidRef = @RegistrarIGuidRef
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;


	EXEC @errorCode = dbo.ScribeCoreDefaultServiceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;

	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCoreDefaultServiceReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeCoreDefaultServiceReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 32; -- DoorsDescription
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- descRecords for Description Records
  DECLARE descRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusCoreServiceDefault  
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN descRecords;
  FETCH descRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH descRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE descRecords;
	DEALLOCATE descRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCrossReferenceCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeCrossReferenceCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusCrossReference 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusCrossReference 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusCrossReference
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusCrossReference 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusCrossReference
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusCrossReference 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusCrossReference WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetCrossReferencesCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCrossReferenceClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeCrossReferenceClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @CrossReference NVARCHAR(256) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, CrossReference 
		FROM dbo.NexusCrossReference WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @CrossReference;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeCrossReferenceEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @CrossReference;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @CrossReference;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeCrossReferenceDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeCrossReferenceDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  	IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalCrossReference
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsPortalCrossReference 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeCrossReferenceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END
GO
/****** Object:  StoredProcedure [dbo].[ScribeCrossReferenceEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeCrossReferenceEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@CrossReference NVARCHAR(256))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = getutcdate();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 23; -- PortalCrossReference
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalCrossReference 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- do not allow replicate values (case insensitive) except when updating current
    DECLARE @CrossReferenceLower nvarchar(256);
	SET @CrossReferenceLower = LOWER(@CrossReference);
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalCrossReference AS P
		INNER JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = P.AuditGuidRef)
		WHERE (P.RecordGuidRef = @RecordGuidRef) AND (P.FgroupGuidKey <> @FgroupGuidKey)
		AND (A.IsDeleted = 0) AND (LOWER(P.CrossReference) = @CrossReferenceLower);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount > 0) OR (@errorCode <> 0) RETURN -13;	

	-- audit table first with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalCrossReference 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsPortalCrossReference
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, CrossReference) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @CrossReference);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsPortalCrossReference SET CrossReference = @CrossReference
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeCrossReferenceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END
GO
/****** Object:  StoredProcedure [dbo].[ScribeCrossReferenceReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ScribeCrossReferenceReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 23; -- PortalCrossReference
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- crefRecords for CrossReference Records
  DECLARE crefRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusCrossReference 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN crefRecords;
  FETCH crefRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH crefRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE crefRecords;
	DEALLOCATE crefRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDdlistEntityTypeCodes]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ScribeDdlistEntityTypeCodes]
(@ForAuthor bit = 0,
 @ForEditor bit = 0,
 @ForAdmin bit = 0)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF (@ForAuthor = 1)
		SELECT * FROM dbo.NpdsCoreEntityTypeEnum
			WHERE (TypeEditedByAuthor = 1) 
			ORDER BY TypeName;
			
	ELSE IF (@ForEditor = 1)
		SELECT * FROM dbo.NpdsCoreEntityTypeEnum
			WHERE (TypeEditedByEditor = 1) 
			ORDER BY TypeName;

	ELSE IF (@ForAdmin = 1)	
		SELECT * FROM dbo.NpdsCoreEntityTypeEnum
			WHERE (TypeEditedByAdmin = 1) 
			ORDER BY TypeName;
	
	ELSE	
		SELECT * FROM dbo.NpdsCoreEntityTypeEnum
			ORDER BY TypeName;
		
	RETURN ISNULL(@@ROWCOUNT,0);
	
END
GO
/****** Object:  StoredProcedure [dbo].[ScribeDdlistInfosetStatusCodes]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ScribeDdlistInfosetStatusCodes]
(@ForAuthor bit = 0,
 @ForEditor bit = 0,
 @ForAdmin bit = 0)
AS
BEGIN

	SET NOCOUNT ON;

	IF (@ForAuthor = 1)
		SELECT * FROM dbo.NpdsCoreInfosetStatusEnum
			WHERE (StatusEditedByAuthor = 1) 
			ORDER BY StatusName;
		
	ELSE IF (@ForEditor = 1)
		SELECT * FROM dbo.NpdsCoreInfosetStatusEnum
			WHERE (StatusEditedByEditor = 1) 
			ORDER BY StatusName;

	ELSE IF (@ForAdmin = 1)	
		SELECT * FROM dbo.NpdsCoreInfosetStatusEnum
			WHERE (StatusEditedByAdmin = 1) 
			ORDER BY StatusName;

	ELSE	
		SELECT * FROM dbo.NpdsCoreInfosetStatusEnum
			ORDER BY StatusName;

	RETURN ISNULL(@@ROWCOUNT,0);
	
END
GO
/****** Object:  StoredProcedure [dbo].[ScribeDdlistRegistrarDiristries]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeDdlistRegistrarDiristries](
@ForAuthor bit = 0,
@ForEditor bit = 0,
@ForAdmin bit = 0,
@SiteDefaultRegistrarGuid uniqueidentifier)
AS
BEGIN

	SET NOCOUNT ON;
	
	/* access privilege should go from lowest to highest */
	    
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF (@SiteDefaultRegistrarGuid IS NULL) OR (@SiteDefaultRegistrarGuid = @EmptyGuid) RETURN -11;
		
	IF (@ForAuthor = 1)
		SELECT *
			FROM dbo.CoreServerService AS C INNER JOIN dbo.NpdsServiceDefault AS D 
			ON (C.ServiceIGuid = D.DiristryInfosetGuidRef)
			WHERE (D.RegistrarInfosetGuidRef = @SiteDefaultRegistrarGuid) 
			AND (C.ServiceTCode = 31)
			ORDER BY C.ServiceName;
			
	ELSE IF (@ForEditor = 1)
		SELECT *
			FROM dbo.CoreServerService AS C INNER JOIN dbo.NpdsServiceDefault AS D 
			ON (C.ServiceIGuid = D.DiristryInfosetGuidRef)
			WHERE (D.RegistrarInfosetGuidRef = @SiteDefaultRegistrarGuid) 
			AND (C.ServiceTCode = 31 OR C.ServiceTCode = 32)
			ORDER BY C.ServiceName;
			
	ELSE IF (@ForAdmin = 1)	BEGIN
			SELECT *
			FROM dbo.CoreServerService AS C
			ORDER BY C.ServiceName;
				
	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDdlistServices]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDdlistServices](
@ForAuthor bit = 0,
@ForEditor bit = 0,
@ForAdmin bit = 0)
AS
BEGIN

	SET NOCOUNT ON;
	
	/* access privilege should go from lowest to highest */
		
	IF (@ForAuthor = 1)
		SELECT *
			FROM dbo.CoreServerService AS C 
			WHERE (C.ServiceTCode = 11) OR (C.ServiceTCode = 21) OR (C.ServiceTCode = 31)
			ORDER BY C.ServiceName;
			
	ELSE IF (@ForEditor = 1)
		SELECT *
			FROM dbo.CoreServerService AS C 
			WHERE (C.ServiceTCode = 11) OR (C.ServiceTCode = 21) OR (C.ServiceTCode = 31)
			   OR (C.ServiceTCode = 12) OR (C.ServiceTCode = 22) OR (C.ServiceTCode = 32)
			ORDER BY C.ServiceName;
			
	ELSE IF (@ForAdmin = 1)	BEGIN
			SELECT *
			FROM dbo.CoreServerService AS C
			ORDER BY C.ServiceName;
				
	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDescriptionCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDescriptionCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusDescription 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusDescription 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusDescription
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusDescription 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusDescription
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusDescription 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusDescription WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetDescriptionsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDescriptionClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeDescriptionClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @Description NVARCHAR(MAX) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, [Description] 
		FROM dbo.NexusDescription WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Description;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeDescriptionEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @Description;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Description;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDescriptionDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeDescriptionDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsDescription
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsDoorsDescription 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeDescriptionCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDescriptionEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDescriptionEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@FieldFormatCodeRef SMALLINT = 0,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@Description NVARCHAR(MAX))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 32; -- DoorsDescription
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsDescription 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, FieldFormatCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @FieldFormatCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET FieldFormatCodeRef = @FieldFormatCodeRef,
        HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsDoorsDescription 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsDoorsDescription
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, [Description]) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @Description);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsDoorsDescription SET [Description] = @Description
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeDescriptionCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDescriptionReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDescriptionReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 32; -- DoorsDescription
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- descRecords for Description Records
  DECLARE descRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusDescription 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN descRecords;
  FETCH descRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH descRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE descRecords;
	DEALLOCATE descRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDistributionCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDistributionCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusDistribution 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusDistribution 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusDistribution
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusDistribution 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusDistribution
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusDistribution 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusDistribution WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetDistributionsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDistributionClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeDistributionClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @Distribution NVARCHAR(MAX) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, [Distribution]
		FROM dbo.NexusDistribution WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Distribution;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeDistributionEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @Distribution;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Distribution;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDistributionDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeDistributionDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsDistribution
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsDoorsDistribution 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeDistributionCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDistributionEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeDistributionEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@FieldFormatCodeRef SMALLINT = 0,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@Distribution NVARCHAR(MAX))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 34; -- DoorsDistribution
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsDistribution 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, FieldFormatCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @FieldFormatCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET FieldFormatCodeRef = @FieldFormatCodeRef,
        HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsDoorsDistribution 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsDoorsDistribution
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, [Distribution]) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @Distribution);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsDoorsDistribution SET [Distribution] = @Distribution
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeDistributionCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeDistributionReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeDistributionReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 34; -- DoorsDistribution
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- distRecords for Distribution Records
  DECLARE distRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusDistribution 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN distRecords;
  FETCH distRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH distRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE distRecords;
	DEALLOCATE distRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelAssureAliasLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeEntityLabelAssureAliasLabel](
@AgentGuidRef uniqueidentifier = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@DiristryGuidRef uniqueidentifier = NULL,
@RegistryGuidRef uniqueidentifier = NULL,
@DirectoryGuidRef uniqueidentifier = NULL,
@RegistrarGuidRef uniqueidentifier = NULL,
@EntityTypeCode SMALLINT = NULL)
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@DiristryGuidRef IS NULL) SET @DiristryGuidRef = @EmptyGuid;
	IF (@RegistryGuidRef IS NULL) SET @RegistryGuidRef = @EmptyGuid;
	IF (@DirectoryGuidRef IS NULL) SET @DirectoryGuidRef = @EmptyGuid;
	IF (@RegistrarGuidRef IS NULL) SET @RegistrarGuidRef = @EmptyGuid;
	IF ((@DiristryGuidRef = @EmptyGuid) AND (@RegistryGuidRef = @EmptyGuid) AND
		(@DirectoryGuidRef = @EmptyGuid) AND (@RegistrarGuidRef = @EmptyGuid)) RETURN -13;
	IF ((@EntityTypeCode IS NULL) OR (@EntityTypeCode < 0)) SET @EntityTypeCode = 0;
	
	DECLARE @rows int = 0, @loops int = 0, @MaxLoops tinyint = 111, @NumChars tinyint = 11;
	DECLARE @GeneratingLabel nvarchar(256) = '', @TestLabel nvarchar(256) = '', @RandomTagToken char(11);

	DECLARE @FgroupGuidKey uniqueidentifier = NULL,
		@HasPriority SMALLINT = 0, @IsMarked bit = 0, @IsRestricted bit = 0, @IsPrincipal bit = 0,
		@ServiceTypeCode SMALLINT = 1, @IsResolvable bit = 1, @IsPrivate bit = 0, @IsGenerating bit = 0;


	SELECT @HasPriority = 251, @ServiceTypeCode = 1; -- Nexus Diristry
	-- assure that resource record has an alias label with random tag for nexus service	
	SELECT @rows = COUNT(EntityAliasLabel)
		FROM dbo.NexusEntityAliasLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (HasPriority = @HasPriority) AND (IsDeleted = 0)					
	IF (@rows = 0) BEGIN
		SELECT @GeneratingLabel = DiristryGeneratingLabel
			FROM dbo.NexusEntityGeneratingLabelDiristry WHERE (DiristryIGuidRef = @DiristryGuidRef);
		IF (@GeneratingLabel IS NOT NULL) AND (@GeneratingLabel <> '') BEGIN
			SELECT @loops = 0, @rows  = 1;
			WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @RandomTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @RandomTagToken;
				EXEC @rows = dbo.ScribeEntityLabelExistsInDiristry @DiristryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @RandomTagToken, @GeneratingLabel, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;

	SELECT @HasPriority = 252, @ServiceTypeCode = 2; -- PORTAL Registry
	-- assure that resource record has an alias label with random tag for portal service	
	SELECT @rows = COUNT(EntityAliasLabel)
		FROM dbo.NexusEntityAliasLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (HasPriority = @HasPriority) AND (IsDeleted = 0)					
	IF (@rows = 0) BEGIN
		SELECT @GeneratingLabel = RegistryGeneratingLabel
			FROM dbo.NexusEntityGeneratingLabelRegistry WHERE (RegistryIGuidRef = @RegistryGuidRef);
		IF (@GeneratingLabel IS NOT NULL) AND (@GeneratingLabel <> '') BEGIN
			IF (@GeneratingLabel LIKE '%/nexus/%') SELECT @GeneratingLabel = REPLACE(@GeneratingLabel,'/nexus/','/portal/');
			SELECT @loops = 0, @rows  = 1;
			WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @RandomTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @RandomTagToken;
				EXEC @rows = dbo.ScribeEntityLabelExistsInRegistry @RegistryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @RandomTagToken, @GeneratingLabel, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;

	SELECT @HasPriority = 253, @ServiceTypeCode = 3; -- DOORS Directory
	-- assure that resource record has an alias label with random tag for doors service
	SELECT @rows = COUNT(EntityAliasLabel)
		FROM dbo.NexusEntityAliasLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (HasPriority = @HasPriority) AND (IsDeleted = 0)					
	IF (@rows = 0) BEGIN
		SELECT @GeneratingLabel = DirectoryGeneratingLabel
			FROM dbo.NexusEntityGeneratingLabelDirectory WHERE (DirectoryIGuidRef = @DirectoryGuidRef);
		IF (@GeneratingLabel IS NOT NULL) AND (@GeneratingLabel <> '') BEGIN
			IF (@GeneratingLabel LIKE '%/nexus/%') SELECT @GeneratingLabel = REPLACE(@GeneratingLabel,'/nexus/','/doors/');
			SELECT @loops = 0, @rows  = 1;
			WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @RandomTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @RandomTagToken;
				EXEC @rows = dbo.ScribeEntityLabelExistsInDirectory @DirectoryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @RandomTagToken, @GeneratingLabel, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;

	SELECT @HasPriority = 254, @ServiceTypeCode = 4; -- Scribe Registrar
	-- assure that resource record has an alias label with random tag for scribe service
	SELECT @rows = COUNT(EntityAliasLabel)
		FROM dbo.NexusEntityAliasLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (HasPriority = @HasPriority) AND (IsDeleted = 0)					
	IF (@rows = 0) BEGIN
		SELECT @GeneratingLabel = RegistrarGeneratingLabel
			FROM dbo.NexusEntityGeneratingLabelRegistrar WHERE (RegistrarIGuidRef = @RegistrarGuidRef);
		IF (@GeneratingLabel IS NOT NULL) AND (@GeneratingLabel <> '') BEGIN
			SELECT @loops = 0, @rows  = 1;
			WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @RandomTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @RandomTagToken;
				EXEC @rows = dbo.ScribeEntityLabelExistsInRegistrar @RegistrarGuidRef, @TestLabel, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @RandomTagToken, @GeneratingLabel, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelAssureCanonicalLabel]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeEntityLabelAssureCanonicalLabel](
@AgentGuidRef uniqueidentifier = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@DiristryGuidRef uniqueidentifier = NULL,
@RegistryGuidRef uniqueidentifier = NULL,
@DirectoryGuidRef uniqueidentifier = NULL,
@RegistrarGuidRef uniqueidentifier = NULL,
@ServiceTypeCode tinyint = NULL,
@EntityTag nvarchar(64) = NULL,
@EntityName nvarchar(256) = NULL,
@EntityTypeCode SMALLINT = NULL)
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@DiristryGuidRef IS NULL) SET @DiristryGuidRef = @EmptyGuid;
	IF (@RegistryGuidRef IS NULL) SET @RegistryGuidRef = @EmptyGuid;
	IF (@DirectoryGuidRef IS NULL) SET @DirectoryGuidRef = @EmptyGuid;
	IF (@RegistrarGuidRef IS NULL) SET @RegistrarGuidRef = @EmptyGuid;
	IF ((@DiristryGuidRef = @EmptyGuid) AND (@RegistryGuidRef = @EmptyGuid) AND
		(@DirectoryGuidRef = @EmptyGuid) AND (@RegistrarGuidRef = @EmptyGuid)) RETURN -13;
	IF ((@EntityTypeCode IS NULL) OR (@EntityTypeCode < 0)) SET @EntityTypeCode = 0;
	
	DECLARE @rows int = 0, @loops int = 0, @MaxLoops tinyint = 111, @NumChars tinyint = 11;
	DECLARE @GeneratingLabel nvarchar(256) = '', @RandomTagToken char(11);
	DECLARE @PhraseTagToken nvarchar(64) = '', @AmendedTagToken nvarchar(64) = '';

	DECLARE @FgroupGuidKey uniqueidentifier = NULL, @LabelUri nvarchar(128) = '', 
		@HasPriority SMALLINT = 0, @IsMarked bit = 0, @IsRestricted bit = 1, @IsPrincipal bit = 1, 
		@IsResolvable bit = 1, @IsPrivate bit = 0, @IsGenerating bit = 0;

	-- assure that resource record has CoreDefaultService based on current NPDS services
	EXEC @rows = dbo.ScribeCoreDefaultServiceEdit 
			@AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
			@HasPriority, @IsMarked, @IsPrincipal, 
			@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef;
			
	-- EntityCanonicalLabel is the EntityLabel marked IsPrincipal
	-- assure that resource record has a canonical label with acronym/phrase principal tag

	SELECT @rows = COUNT(EntityCanonicalLabel) 
		FROM dbo.NexusEntityCanonicalLabel WHERE (RecordGuidRef = @RecordGuidRef);	

	-- first attempt to create canonical label from nonempty @EntityTag
	IF ((@rows = 0) AND (@EntityTag IS NOT NULL)) BEGIN
		SELECT @PhraseTagToken = @EntityTag; 
		EXEC @rows = dbo.ScribeEntityRegTagExists @PhraseTagToken, @RegistryGuidRef, @RecordGuidRef;	
		IF (@rows = 0) BEGIN
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @PhraseTagToken, @LabelUri, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END ELSE BEGIN
			SELECT @loops = 0, @rows  = 1;
			WHILE  (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				SELECT @AmendedTagToken = @PhraseTagToken + CAST(@loops AS nvarchar(128));
				EXEC @rows = dbo.ScribeEntityRegTagExists @AmendedTagToken, @RegistryGuidRef, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @AmendedTagToken, @LabelUri, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;

	SELECT @rows = COUNT(EntityCanonicalLabel) 
		FROM dbo.NexusEntityCanonicalLabel WHERE (RecordGuidRef = @RecordGuidRef);	
					
	-- then attempt to create canonical label from nonempty @EntityName
	IF ((@rows = 0) AND (@EntityName IS NOT NULL)) BEGIN
		IF ((71 <= @EntityTypeCode) AND (@EntityTypeCode <= 73)) BEGIN
			SELECT @PhraseTagToken = LOWER(dbo.CoreConvertPhraseToToken(@EntityName)); 
		END ELSE BEGIN 
			SELECT @PhraseTagToken = dbo.CoreExtractAcronymFromPhrase(@EntityName);
		END;
		EXEC @rows = dbo.ScribeEntityRegTagExists @PhraseTagToken, @RegistryGuidRef, @RecordGuidRef;	
		IF (@rows = 0) BEGIN 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @PhraseTagToken, @LabelUri, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END ELSE BEGIN
			IF (LEN(@PhraseTagToken) > 126) SET @PhraseTagToken = LEFT(@PhraseTagToken,126);
			SELECT @loops = 0, @rows  = 1;
			WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
				SELECT @loops = @loops + 1;
				SELECT @AmendedTagToken = @PhraseTagToken + CAST(@loops AS nvarchar(128));
				EXEC @rows = dbo.ScribeEntityRegTagExists @AmendedTagToken, @RegistryGuidRef, @RecordGuidRef;	
			END;
			IF (@rows = 0) 
			EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
				@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
				@ServiceTypeCode, @AmendedTagToken, @LabelUri, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
		END;
	END;	

	-- assure that resource record has a canonical label with random tag 
	--    when acronym principal tag not possible from either EntityTag or EntityName
	SELECT @rows = COUNT(EntityCanonicalLabel) 
		FROM dbo.NexusEntityCanonicalLabel WHERE (RecordGuidRef = @RecordGuidRef);				
	IF (@rows = 0) BEGIN
		SELECT @loops = 0, @rows  = 1; -- initialize for loop
		WHILE (@rows > 0) AND (@loops < @MaxLoops) BEGIN
			SET @loops = @loops + 1;
			EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @RandomTagToken output;
			EXEC @rows = dbo.ScribeEntityRegTagExists @RandomTagToken, @RegistryGuidRef, @RecordGuidRef;	
		END;
		IF (@rows = 0) 
		EXEC @rows = dbo.ScribeEntityLabelEdit @AgentGuidRef, @InfosetGuidRef, @RecordGuidRef, @FgroupGuidKey, 
			@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
			@ServiceTypeCode, @RandomTagToken, @LabelUri, @HasPriority, @IsMarked, @IsPrincipal, @IsResolvable, @IsPrivate, @IsGenerating;
	END;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeEntityLabelCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusEntityLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusEntityLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusEntityLabel
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusEntityLabel 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusEntityLabel
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusEntityLabel 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusEntityLabel WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetEntityLabelsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeEntityLabelDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsCoreEntityLabel
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsCoreEntityLabel 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeEntityLabelCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeEntityLabelEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@DiristryGuidRef UNIQUEIDENTIFIER = NULL,
@RegistryGuidRef UNIQUEIDENTIFIER = NULL,
@DirectoryGuidRef UNIQUEIDENTIFIER = NULL,
@RegistrarGuidRef UNIQUEIDENTIFIER = NULL,
@ServiceTypeCode SMALLINT = 2,
@TagToken NVARCHAR(64) = '',
@LabelUri NVARCHAR(128) = '',
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@IsResolvable BIT = 0,
@IsPrivate BIT = 0,
@IsGenerating BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @newRecord BIT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SELECT @newRecord = 1, @FgroupGuidKey = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 11; -- CoreEntityLabel
	DECLARE @HasIndex SMALLINT; -- random unique index

	IF (@DiristryGuidRef IS NULL) SET @DiristryGuidRef = @EmptyGuid;
	IF (@RegistryGuidRef IS NULL) SET @RegistryGuidRef = @EmptyGuid;
	IF (@DirectoryGuidRef IS NULL) SET @DirectoryGuidRef = @EmptyGuid;
	IF (@RegistrarGuidRef IS NULL) SET @RegistrarGuidRef = @EmptyGuid;

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsCoreEntityLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- problems accessing view instead of table without transactions
	IF ((@DiristryGuidRef = @EmptyGuid) OR (@RegistryGuidRef = @EmptyGuid) OR (@DirectoryGuidRef = @EmptyGuid) OR (@RegistrarGuidRef = @EmptyGuid)) BEGIN
		SELECT @DiristryGuidRef = RecordDiristryInfosetGuidRef, @RegistryGuidRef = RecordRegistryInfosetGuidRef,
			@DirectoryGuidRef = RecordDirectoryInfosetGuidRef, @RegistrarGuidRef = RecordRegistrarInfosetGuidRef
			FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -13;
	END;

	-- Parse and generate the current @EntityLabel
	DECLARE @ParsedTagToken nvarchar(64) = '', @ParsedLabelUri nvarchar(128) = '', @ParsedEntityLabel nvarchar(256) = '';
	EXECUTE @errorCode = dbo.ScribeEntityLabelParse @RecordGuidRef, @IsGenerating, @ServiceTypeCode, 
		@DiristryGuidRef, @RegistryGuidRef, @DirectoryGuidRef, @RegistrarGuidRef,
		@TagToken, @LabelUri, @ParsedTagToken output, @ParsedLabelUri output, @ParsedEntityLabel output;
	IF ((@errorCode <> 0) OR (LEN(@ParsedEntityLabel) = 0)) RETURN (@errorCode-100);

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 IsResolvable, IsPrivate, IsGenerating,  
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @IsResolvable, @IsPrivate, @IsGenerating, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
				IsResolvable = @IsResolvable, IsPrivate = @IsPrivate, IsGenerating = @IsGenerating,
				UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreEntityLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsCoreEntityLabel
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, EntityLabel,
			ServiceTypeCodeRef, TagToken, LabelUri)
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @ParsedEntityLabel,
			@ServiceTypeCode, @ParsedTagToken, @ParsedLabelUri);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsCoreEntityLabel SET EntityLabel = @ParsedEntityLabel,
			ServiceTypeCodeRef = @ServiceTypeCode, TagToken = @ParsedTagToken, LabelUri = @ParsedLabelUri
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeEntityLabelCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelExistsInDirectory]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeEntityLabelExistsInDirectory](
@RecordDirectoryGuidRef uniqueidentifier = NULL,
@EntityLabel nvarchar(256) = '',
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF ((@RecordDirectoryGuidRef IS NULL) OR (@RecordDirectoryGuidRef = @EmptyGuid)) RETURN -11;	
	IF ((@EntityLabel IS NULL) OR (@EntityLabel = '')) RETURN -12;
	
	IF (@RecordGuidRef IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0)
			AND (L.FgroupGuidKey <> @FgroupGuidKey);

	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelExistsInDiristry]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityLabelExistsInDiristry](
@RecordDiristryGuidRef uniqueidentifier = NULL,
@EntityLabel nvarchar(256) = '',
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF ((@RecordDiristryGuidRef IS NULL) OR (@RecordDiristryGuidRef = @EmptyGuid)) RETURN -11;	
	IF ((@EntityLabel IS NULL) OR (@EntityLabel = '')) RETURN -12;
	
	IF (@RecordGuidRef IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0)
			AND (L.FgroupGuidKey <> @FgroupGuidKey);

	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelExistsInNpdsRoot]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityLabelExistsInNpdsRoot](
@EntityLabel nvarchar(256) = '',
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	DECLARE @NpdsRootGuid uniqueidentifier = 'c6eca1c6-b7e4-4855-980e-5878fe72b78f';
	IF ((@EntityLabel IS NULL) OR (@EntityLabel = '')) RETURN -12;
	
	IF (@RecordGuidRef IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistrarInfosetGuidRef = @NpdsRootGuid)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistrarInfosetGuidRef = @NpdsRootGuid)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0)
			AND (L.FgroupGuidKey <> @FgroupGuidKey);

	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelExistsInRegistrar]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityLabelExistsInRegistrar](
@RecordRegistrarGuidRef uniqueidentifier = NULL,
@EntityLabel nvarchar(256) = '',
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF ((@RecordRegistrarGuidRef IS NULL) OR (@RecordRegistrarGuidRef = @EmptyGuid)) RETURN -11;	
	IF ((@EntityLabel IS NULL) OR (@EntityLabel = '')) RETURN -12;
	
	IF (@RecordGuidRef IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0)
			AND (L.FgroupGuidKey <> @FgroupGuidKey);

	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelExistsInRegistry]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityLabelExistsInRegistry](
@RecordRegistryGuidRef uniqueidentifier = NULL,
@EntityLabel nvarchar(256) = '',
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF ((@RecordRegistryGuidRef IS NULL) OR (@RecordRegistryGuidRef = @EmptyGuid)) RETURN -11;	
	IF ((@EntityLabel IS NULL) OR (@EntityLabel = '')) RETURN -12;
	
	IF (@RecordGuidRef IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT L.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS L ON (R.RecordGuidKey = L.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = L.AuditGuidRef)
			WHERE (R.RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef)
			AND (L.EntityLabel = LOWER(@EntityLabel)) AND (A.IsDeleted = 0)
			AND (L.FgroupGuidKey <> @FgroupGuidKey);

	END;

	RETURN ISNULL(@@ROWCOUNT,0);

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelParse]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeEntityLabelParse] (
@RecordGuidRef uniqueidentifier = null,
@IsGenerating bit = 0,
@ServiceTypeCode SMALLINT = 1,
@RecordDiristryGuidRef uniqueidentifier = null,
@RecordRegistryGuidRef uniqueidentifier = null,
@RecordDirectoryGuidRef uniqueidentifier = null,
@RecordRegistrarGuidRef uniqueidentifier = null,
@TagToken nvarchar(64) = '',
@LabelUri nvarchar(128) = '',
@ParsedTagToken nvarchar(64) output,
@ParsedLabelUri nvarchar(128) output,
@ParsedEntityLabel nvarchar(256) output)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -11;
  DECLARE @loopCount int = 0, @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = getutcdate();
	DECLARE @GeneratingLabel nvarchar(384) = '', @TestLabel nvarchar(384) = '', @NumChars tinyint  = 11;
  SELECT @ParsedTagToken = COALESCE(LTRIM(RTRIM(@TagToken)),'');
  SELECT @ParsedLabelUri = LOWER(COALESCE(LTRIM(RTRIM(@LabelUri)),''));		
	-- failsafe to prevent nulls in uniquekey column, always guarantees an EntityLabel
	SELECT @ParsedEntityLabel = (CONVERT(nvarchar(384),'http://',0)+CONVERT(nvarchar(384),newid(),0));

	-- LabelUri may be empty but if nonempty then must match a regex for NPDS ServiceType path segment
	-- check nonempty LabelUri for NPDS ServiceType path segment and NPDS segment missing, then reset to ''
	IF (@ParsedLabelUri <> '') BEGIN
		IF (@ServiceTypeCode = 1)  BEGIN -- nexus
			IF NOT (@ParsedLabelUri LIKE '%nexus/%') SELECT @ParsedLabelUri = ''; 
		END ELSE IF (@ServiceTypeCode = 2)  BEGIN -- portal
			IF NOT (@ParsedLabelUri LIKE '%portal/%') SELECT @ParsedLabelUri = ''; 
		END ELSE IF (@ServiceTypeCode = 3)  BEGIN -- doors
			IF NOT (@ParsedLabelUri LIKE '%doors/%') SELECT @ParsedLabelUri = ''; 
		END ELSE IF (@ServiceTypeCode = 4)  BEGIN -- scribe
			IF NOT (@ParsedLabelUri LIKE '%scribe/%') SELECT @ParsedLabelUri = ''; 
		END ELSE RETURN -12; -- invalid @ServiceTypeCode
	END;

	-- if empty ParsedLabelUri then retrieve the DefaultGeneratingLabel
	IF (@ParsedLabelUri = '') BEGIN -- empty LabelUri
		IF (@ServiceTypeCode = 1)  BEGIN -- nexus
			SELECT @GeneratingLabel = DiristryGeneratingLabel 
				FROM dbo.NexusEntityGeneratingLabelDiristry WHERE RecordGuidKey = @RecordGuidRef;
			SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
			IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
		END ELSE IF (@ServiceTypeCode = 2)  BEGIN -- portal
			SELECT @GeneratingLabel = RegistryGeneratingLabel	
				FROM dbo.NexusEntityGeneratingLabelRegistry WHERE RecordGuidKey = @RecordGuidRef;
			SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
			IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
		END ELSE IF (@ServiceTypeCode = 3)  BEGIN -- doors
			SELECT @GeneratingLabel = DirectoryGeneratingLabel 
				FROM dbo.NexusEntityGeneratingLabelDirectory WHERE RecordGuidKey = @RecordGuidRef;
			SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
			IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -24;
		END ELSE IF (@ServiceTypeCode = 4)  BEGIN -- scribe
			SELECT @GeneratingLabel = RegistrarGeneratingLabel	
				FROM dbo.NexusEntityGeneratingLabelRegistrar WHERE RecordGuidKey = @RecordGuidRef;
			SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
			IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
		END ELSE RETURN -26; -- invalid @ServiceTypeCode		
	END ELSE BEGIN -- nonempty LabelUri
		SELECT @GeneratingLabel = @ParsedLabelUri; 
	END;
    IF (RIGHT(@GeneratingLabel,1) <> '/') SELECT @GeneratingLabel = @GeneratingLabel + '/';
	IF (LEN(@GeneratingLabel) = 0) RETURN -27;
	-- check for empty ParsedTagToken because should NOT be empty
	IF (@ParsedTagToken = '') BEGIN
		SELECT @loopCount = 0, @rowCount  = 1;
		IF (@ServiceTypeCode = 1)  BEGIN -- nexus
			WHILE (@rowCount > 0) AND (@loopCount < 100) BEGIN
				SELECT @loopCount = @loopCount + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @ParsedTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @ParsedTagToken;
				EXEC @rowCount = dbo.ScribeEntityLabelExistsInDiristry @RecordDiristryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
		END ELSE IF (@ServiceTypeCode = 2)  BEGIN -- portal
			WHILE (@rowCount > 0) AND (@loopCount < 100) BEGIN
				SELECT @loopCount = @loopCount + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @ParsedTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @ParsedTagToken;
				EXEC @rowCount = dbo.ScribeEntityLabelExistsInRegistry @RecordRegistryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
		END ELSE IF (@ServiceTypeCode = 3)  BEGIN -- doors
			WHILE (@rowCount > 0) AND (@loopCount < 100) BEGIN
				SELECT @loopCount = @loopCount + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @ParsedTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @ParsedTagToken;
				EXEC @rowCount = dbo.ScribeEntityLabelExistsInDirectory @RecordDirectoryGuidRef, @TestLabel, @RecordGuidRef;	
			END;
		END ELSE IF (@ServiceTypeCode = 4)  BEGIN -- scribe
			WHILE (@rowCount > 0) AND (@loopCount < 100) BEGIN
				SELECT @loopCount = @loopCount + 1;
				EXEC dbo.CoreRandomHexStrVarCharGenerate @NumChars, @ParsedTagToken output;
				SELECT @TestLabel = @GeneratingLabel + @ParsedTagToken;
				EXEC @rowCount = dbo.ScribeEntityLabelExistsInRegistrar @RecordRegistrarGuidRef, @TestLabel, @RecordGuidRef;	
			END;
		END ELSE RETURN -28; -- invalid @ServiceTypeCode
	END;
	IF (LEN(@ParsedTagToken) = 0) RETURN -29;

	SELECT @TestLabel = LOWER(@GeneratingLabel + @ParsedTagToken);
	IF (LEN(@TestLabel) = 0) RETURN -30;

	SELECT @ParsedEntityLabel = @TestLabel;

	RETURN 0; -- no error

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeEntityLabelReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 11; -- CoreEntityLabel
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- elabRecords for EntityLabel Records
  DECLARE elabRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusEntityLabel 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN elabRecords;
  FETCH elabRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH elabRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE elabRecords;
	DEALLOCATE elabRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityLabelToInfosetGuid]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityLabelToInfosetGuid]
(@EntityLabel nvarchar(256),
 @InfosetGuid uniqueidentifier = null output)
AS
BEGIN

	SET NOCOUNT ON;

	IF (@EntityLabel IS NULL) RETURN -1;
	
	SET @EntityLabel = LOWER(RTRIM(LTRIM(@EntityLabel)));
	
	SELECT @InfosetGuid = InfosetGuidRef 
		FROM dbo.NexusEntityLabel WHERE (EntityLabel = @EntityLabel);

	RETURN 0; -- no errors
			
END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeEntityRegTagExists]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeEntityRegTagExists](
@EntityPrincipalTag nvarchar(64),
@RecordRegistryGuidRef uniqueidentifier = NULL,
@RecordGuidKey uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL)
AS BEGIN
	
	SET NOCOUNT ON;
	
	-- @EntityPrincipalTag must be non-null to test for its existence
	IF ((@EntityPrincipalTag IS NULL) OR (@EntityPrincipalTag = '')) RETURN -11;
	-- @RecordRegistryGuid and @ResourceIid must not be simultaneously null
	IF (@RecordRegistryGuidRef IS NULL) BEGIN
		IF (@RecordGuidKey IS NULL) BEGIN
			RETURN -12;
		END ELSE BEGIN
			SELECT @RecordRegistryGuidRef = RecordRegistryInfosetGuidRef
				FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidKey);
			IF (@RecordRegistryGuidRef IS NULL) RETURN -13;
		END;	
	END;
	
	-- @EntityPrincipalTag and @RecordRegistryGuid should now both be non-null
	
	IF (@RecordGuidKey IS NULL) OR (@FgroupGuidKey IS NULL) BEGIN
		/* self does not yet exist; check all */
		SELECT T.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS T ON (R.RecordGuidKey = T.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = T.AuditGuidRef)
			WHERE (R.RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef)
			AND (LOWER(T.TagToken) = LOWER(@EntityPrincipalTag)) AND (A.IsDeleted = 0);

		RETURN ISNULL(@@ROWCOUNT,0);

	END ELSE BEGIN
		/* self already does exist; check all except self */
		SELECT T.FgroupGuidKey	
			FROM dbo.NpdsResrepRoot AS R INNER
			JOIN dbo.NpdsCoreEntityLabel AS T ON (R.RecordGuidKey = T.RecordGuidRef) INNER
			JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = T.AuditGuidRef)
			WHERE (R.RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef)
			AND (LOWER(T.TagToken) = LOWER(@EntityPrincipalTag)) AND (A.IsDeleted = 0)
			AND (T.FgroupGuidKey <> @FgroupGuidKey);

		RETURN ISNULL(@@ROWCOUNT,0);

	END

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeFairMetricCalculate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeFairMetricCalculate]
(@RecordGuidRef uniqueidentifier,
@FgroupGuidKey uniqueidentifier)
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
  DECLARE @rowCount int = 0, @errorCode int = 0;
  IF (@RecordGuidRef IS NULL) RETURN -11;
  IF (@FgroupGuidKey IS NULL) RETURN -12;

	DECLARE @Misquoted real = 0, @Quoted real = 0, @Plagiarized real = 0, @Novel real = 0, @Similar real = 0, @Reported real = 0;
	DECLARE @FAIR1 real = 0, @FAIR2 real = 0, @FAIR3 real = 0, @FAIR4 real = 0;

    -- Insert statements for procedure here
	SELECT @Misquoted = CAST(M_InvalidOldClaim AS real), @Quoted = CAST(Q_ValidOldClaim AS real),
			@Plagiarized = CAST(P_InvalidNewClaim AS real), @Novel = CAST(N_ValidNewClaim AS real)
		FROM dbo.NpdsDoorsFairMetric WHERE (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	IF (@Misquoted < 0) OR (@Quoted < 0) OR (@Plagiarized < 0) OR (@Novel < 0) RETURN -22;
	SET @Similar = (@Misquoted + @Quoted + @Plagiarized);
	SET @Reported = (@Misquoted + @Quoted + @Plagiarized + @Novel);

	IF (@Similar > 0) BEGIN
	  SET @FAIR1 = (@Quoted/@Similar);
	  SET @FAIR2 = (@Quoted - @Misquoted)/@Similar;
	  SET @FAIR3 = (@Quoted - @Plagiarized)/@Similar;
	END;
	IF (@Reported > 0) BEGIN
	  SET @FAIR4 = (@Quoted - @Novel)/@Reported;
	END;

	UPDATE dbo.NpdsDoorsFairMetric
	    SET FAIR1 = @FAIR1, FAIR2 = @FAIR2, FAIR3 = @FAIR3, FAIR4 = @FAIR4
		WHERE (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;
		
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeFairMetricCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeFairMetricCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusFairMetric 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusFairMetric 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusFairMetric
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusFairMetric 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusFairMetric
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusFairMetric 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusFairMetric WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetFairMetricsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeFairMetricDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeFairMetricDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsFairMetric
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsDoorsFairMetric 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeFairMetricCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeFairMetricEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeFairMetricEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@Mcount SMALLINT = 0,
@Qcount SMALLINT = 0,
@Pcount SMALLINT = 0,
@Ncount SMALLINT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 35; -- DoorsFairMetric
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsFairMetric 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsDoorsFairMetric 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
	 
 	IF (@rowCount = 0) BEGIN	 
	   INSERT INTO dbo.NpdsDoorsFairMetric
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, 
			M_InvalidOldClaim, Q_ValidOldClaim, P_InvalidNewClaim, N_ValidNewClaim) 
		VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, 
			@Mcount, @Qcount, @Pcount, @Ncount);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsDoorsFairMetric SET  
			M_InvalidOldClaim = @Mcount, Q_ValidOldClaim = @Qcount, 
			P_InvalidNewClaim = @Pcount, N_ValidNewClaim = @Ncount
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeFairMetricCalculate @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -30;

	EXEC @errorCode = dbo.ScribeFairMetricCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;

	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeFairMetricReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeFairMetricReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 35; -- DoorsFairMetric
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- fmetRecords for FairMetric Records
  DECLARE fmetRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusFairMetric 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN fmetRecords;
  FETCH fmetRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH fmetRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE fmetRecords;
	DEALLOCATE fmetRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeLocationCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeLocationCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusLocation 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusLocation 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusLocation
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusLocation 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusLocation
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusLocation 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusLocation WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetLocationsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeLocationClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeLocationClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @Location NVARCHAR(MAX) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, [Location]
		FROM dbo.NexusLocation WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Location;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeLocationEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @Location;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Location;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeLocationDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeLocationDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsLocation
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsDoorsLocation 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeLocationCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeLocationEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeLocationEdit](
@AgentGuidRef uniqueidentifier = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@FgroupGuidKey uniqueidentifier = NULL,
@FieldFormatCodeRef SMALLINT = 0,
@HasPriority SMALLINT = 0,
@IsMarked bit = 0,
@IsPrincipal bit = 0,
@Location nvarchar(max) = null,
@DisplayText nvarchar(64) = null,
@DisplayImageUrl nvarchar(256) = null,
@UrlWebAddress nvarchar(256) = null,
@UrlWebAddressValidated datetime2 = null,
@EmailAddress nvarchar(128) = null,
@EmailAddressValidated datetime2 = null,
@StreetAddress nvarchar(128) = null,
@StreetAddressValidated datetime2 = null,
@ExtendedAddress nvarchar(128) = null,
@FormattedAddress nvarchar(128) = null,
@CityLocality nvarchar(128) = null,
@StateRegion nvarchar(128) = null,
@Country nvarchar(128) = null,
@PostalCode nvarchar(32) = null,
@Telephone nvarchar(32) = null,
@GeocodeType nvarchar(32) = null,
@GeocodeConfidence nvarchar(32) = null,
@Latitude float = null,
@Longitude float = null)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 31; -- DoorsLocation
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsLocation 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, FieldFormatCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @FieldFormatCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET FieldFormatCodeRef = @FieldFormatCodeRef,
        HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsDoorsLocation 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
	    INSERT INTO dbo.NpdsDoorsLocation
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, [Location],
		     DisplayText, DisplayImageUrl, UrlWebAddress, UrlWebAddressValidated, EmailAddress, EmailAddressValidated, StreetAddress, StreetAddressValidated,
			 ExtendedAddress, FormattedAddress, CityLocality, StateRegion, Country, PostalCode, Telephone, GeocodeType, GeocodeConfidence, Latitude, Longitude) 
		VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @Location,
		     @DisplayText, @DisplayImageUrl, @UrlWebAddress, @UrlWebAddressValidated, @EmailAddress, @EmailAddressValidated, @StreetAddress, @StreetAddressValidated,
			 @ExtendedAddress, @FormattedAddress, @CityLocality, @StateRegion, @Country, @PostalCode, @Telephone, @GeocodeType, @GeocodeConfidence, @Latitude, @Longitude); 
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN	
		UPDATE dbo.NpdsDoorsLocation SET [Location] = @Location,
			DisplayText = @DisplayText, DisplayImageUrl = @DisplayImageUrl,
			UrlWebAddress = @UrlWebAddress, UrlWebAddressValidated = @UrlWebAddressValidated,
			EmailAddress = @EmailAddress, EmailAddressValidated = @EmailAddressValidated,
			StreetAddress = @StreetAddress, StreetAddressValidated = @StreetAddressValidated,
			ExtendedAddress = @ExtendedAddress, FormattedAddress = @FormattedAddress,
			CityLocality = @CityLocality, StateRegion = @StateRegion,
			Country = @Country, PostalCode = @PostalCode, Telephone = @Telephone,
			GeocodeType = @GeocodeType, GeocodeConfidence = @GeocodeConfidence, 
			Latitude = @Latitude, Longitude = @Longitude
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;
	
	EXEC @errorCode = dbo.ScribeLocationCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

    RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeLocationReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeLocationReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 31; -- DoorsLocation
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- loctRecords for Location Records
  DECLARE loctRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusLocation 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN loctRecords;
  FETCH loctRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH loctRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE loctRecords;
	DEALLOCATE loctRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeOtherTextCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusOtherText 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusOtherText 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusOtherText
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusOtherText 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusOtherText
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusOtherText 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusOtherText WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetOtherTextsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeOtherTextClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 24; -- PortalOtherText
  DECLARE @FieldFormatCodeRef SMALLINT = 0;
	DECLARE @HasPriority SMALLINT = 0; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT = 0; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @OtherText NVARCHAR(MAX) = '';

	DECLARE clone CURSOR FOR 
		SELECT FieldFormatCodeRef, HasPriority, IsMarked, IsPrincipal, OtherText 
		FROM dbo.NexusOtherText WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @FieldFormatCodeRef, @HasPriority, @IsMarked, @IsPrincipal, @OtherText;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeOtherTextEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL, @FieldFormatCodeRef,
			@HasPriority, @IsMarked, @IsPrincipal, @OtherText;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @FieldFormatCodeRef, @HasPriority, @IsMarked, @IsPrincipal, @OtherText;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeOtherTextDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalOtherText
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsPortalOtherText 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeOtherTextCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeOtherTextEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@FieldFormatCodeRef SMALLINT = 0,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@OtherText NVARCHAR(MAX))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 24; -- PortalOtherText
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalOtherText 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, FieldFormatCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @FieldFormatCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET FieldFormatCodeRef = @FieldFormatCodeRef,
        HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsPortalOtherText 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsPortalOtherText
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, OtherText) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @OtherText);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsPortalOtherText SET OtherText = @OtherText
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeOtherTextCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeOtherTextReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 24; -- PortalOtherText
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- otxtRecords for OtherText Records
  DECLARE otxtRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusOtherText 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN otxtRecords;
  FETCH otxtRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH otxtRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE otxtRecords;
	DEALLOCATE otxtRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeOtherTextReseqAll]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeOtherTextReseqAll](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@DiristryGuidRef UNIQUEIDENTIFIER = NULL,
@MaxPriority SMALLINT = 0 OUTPUT
) AS BEGIN

	SET NOCOUNT ON;

	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
 	DECLARE @rowCount INT = 0, @errorCode INT = 0, @recordCount INT = 0; 
	DECLARE @InfosetGuidKey UNIQUEIDENTIFIER, @RecordGuidKey UNIQUEIDENTIFIER, @HasPriority SMALLINT = 0; 
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;	
  IF (@DiristryGuidRef IS NULL) OR (@DiristryGuidRef = @EmptyGuid) RETURN -12;	

  -- all records in given diristry (TODO: impose admin constraint)
  -- rrRecords for ResrepRoot Records
	DECLARE rrRecords CURSOR FOR
		SELECT InfosetGuidKey, RecordGuidKey
		FROM dbo.NpdsResrepRoot WHERE (RecordDiristryInfosetGuidRef = @DiristryGuidRef)
    ORDER BY RecordHandle;

	OPEN rrRecords;
	FETCH rrRecords INTO @InfosetGuidKey, @RecordGuidKey;

	WHILE (@@FETCH_STATUS=0) BEGIN
		
    EXECUTE @errorCode = dbo.ScribeOtherTextReseq @AgentGuidRef, @InfosetGuidKey, @RecordGuidKey, @HasPriority;
    IF (@errorCode = 0)	SET @recordCount = @recordCount + 1;
    IF (@HasPriority > @MaxPriority) SET @MaxPriority = @HasPriority;

	  FETCH rrRecords INTO @InfosetGuidKey, @RecordGuidKey;
	
	END

	CLOSE rrRecords;
	DEALLOCATE rrRecords;
	
	RETURN @recordCount;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeProvenanceCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeProvenanceCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusProvenance 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusProvenance 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusProvenance
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusProvenance 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusProvenance
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusProvenance 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusProvenance WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetProvenancesCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeProvenanceClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeProvenanceClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @Provenance NVARCHAR(MAX) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, Provenance
		FROM dbo.NexusProvenance WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Provenance;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeProvenanceEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @Provenance;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @Provenance;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeProvenanceDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeProvenanceDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsProvenance
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsDoorsProvenance 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeProvenanceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeProvenanceEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeProvenanceEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@FieldFormatCodeRef SMALLINT = 0,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@Provenance NVARCHAR(MAX))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 33; -- DoorsProvenance
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsDoorsProvenance 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, FieldFormatCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @FieldFormatCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET FieldFormatCodeRef = @FieldFormatCodeRef,
        HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsDoorsProvenance 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsDoorsProvenance
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, Provenance) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @Provenance);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsDoorsProvenance SET Provenance = @Provenance
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeProvenanceCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeProvenanceReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeProvenanceReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 33; -- DoorsProvenance
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- provRecords for Provenance Records
  DECLARE provRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusProvenance 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN provRecords;
  FETCH provRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH provRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE provRecords;
	DEALLOCATE provRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepAuthorRequestDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeResrepAuthorRequestDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepAccessAuth
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsResrepAccessAuth
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;
 
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepAuthorRequestEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepAuthorRequestEdit](
@AccessRequestedForAgentGuidRef UNIQUEIDENTIFIER = NULL,
@AccessRequestedByAgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@RequestIsApproved BIT = 0,
@RequestIsDenied BIT = 0)

AS BEGIN

	SET NOCOUNT ON; 
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';	
	-- admin or editor may create (assign) requests for agents 
	IF ((@AccessRequestedByAgentGuidRef IS NULL) OR (@AccessRequestedByAgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid)) RETURN -12;
	DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 7; -- ResrepAuthorRequest
	DECLARE @HasIndex TINYINT; -- random unique index
	IF ((@AccessRequestedForAgentGuidRef IS NULL) OR (@AccessRequestedForAgentGuidRef = @EmptyGuid)) 
		SET @AccessRequestedForAgentGuidRef = @AccessRequestedByAgentGuidRef;

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepAccessAuth 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	DECLARE @AuthorHasResrepAccess bit = 0;
	-- approval has lower priority than denial
	IF (@RequestIsApproved = 1) SET @AuthorHasResrepAccess = 1;
	-- denial has higher priority than approval
	IF (@RequestIsDenied = 1) SET @AuthorHasResrepAccess = 0;

	DECLARE @ManagingAgentGuidRef uniqueidentifier = NULL, @NewManagingAgentGuidRef uniqueidentifier = NULL;
	DECLARE @InfosetIsManagerReleased bit = 0, @InfosetIsManagerChanged bit = 0;
	SELECT @ManagingAgentGuidRef = RecordManagedByAgentGuidRef, @InfosetIsManagerReleased = InfosetIsManagerReleased
		FROM dbo.NexusResrepRoot WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -14;
	IF ((@ManagingAgentGuidRef IS NULL) OR (@ManagingAgentGuidRef = @EmptyGuid)) RETURN -15;

	-- assign new ManagingAgent
	IF ((@RequestIsApproved = 1) OR (@InfosetIsManagerReleased = 1)) AND (@RequestIsDenied = 0) 
		AND (@AccessRequestedForAgentGuidRef <> @ManagingAgentGuidRef) BEGIN 
	    SET @AuthorHasResrepAccess = 1;
		SET @NewManagingAgentGuidRef = @AccessRequestedForAgentGuidRef;
	END;
	IF ((@NewManagingAgentGuidRef IS NOT NULL) AND (@NewManagingAgentGuidRef <> @ManagingAgentGuidRef)) BEGIN
	   SET @InfosetIsManagerChanged = 1;
	END;

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, @HasIndex,
			@utcDate, @AccessRequestedByAgentGuidRef, @utcDate, @AccessRequestedByAgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AccessRequestedByAgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepAccessAuth 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsResrepAccessAuth
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, AccessRequestedForAgentGuidRef) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @AccessRequestedForAgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsResrepAccessAuth SET 
			RequestIsApproved = @RequestIsApproved, 
			RequestIsDenied = @RequestIsDenied,
			AuthorHasResrepAccess = @AuthorHasResrepAccess,
			AccessApprovedByAgentGuidRef = @AccessRequestedByAgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	-- update resrep audit record only if managing agent changed
	IF (@InfosetIsManagerChanged = 1) BEGIN
		SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot 
			WHERE (RecordGuidKey = @RecordGuidRef); 
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -27;
		UPDATE dbo.NpdsCoreAudit 
			SET ManagedByAgentGuidRef = @NewManagingAgentGuidRef,
				UpdatedByAgentGuidRef = @AccessRequestedByAgentGuidRef,
				UpdatedOn = @utcDate
			WHERE (AuditGuidKey = @AuditGuidRef);		
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -28;
	END;
    
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepCountRecordsCreatedByAgent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepCountRecordsCreatedByAgent]
(@NpdsAgentGuid uniqueidentifier)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @count int;

	SELECT @count = COUNT(RecordGuidKey) FROM dbo.NexusResrepStem
		WHERE (RecordCreatedByAgentGuidRef = @NpdsAgentGuid);
	
	RETURN @count;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepCountRecordsManagedByAgent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepCountRecordsManagedByAgent]
(@NpdsAgentGuid uniqueidentifier)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @count int;

	SELECT @count = COUNT(RecordGuidKey) FROM dbo.NexusResrepStem
		WHERE (RecordManagedByAgentGuidRef = @NpdsAgentGuid);
	
	RETURN @count;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepCountRecordsUpdatedByAgent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepCountRecordsUpdatedByAgent]
(@NpdsAgentGuid uniqueidentifier)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @count int;

	SELECT @count = COUNT(RecordGuidKey) FROM dbo.NexusResrepStem
		WHERE (RecordUpdatedByAgentGuidRef = @NpdsAgentGuid);
	
	RETURN @count;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepEditMergeToSame]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepEditMergeToSame](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordHandleToRetain CHAR(9) = '',
@RecordHandleToDelete CHAR(9) = '')
AS
BEGIN

	SET NOCOUNT ON; 
	-- compare with ScribeResrepRootEdit and ScribeResrepEditSplitToDifferent
	-- ScribeResrepEditSplitToDifferent is a one-step process
	--   with a clone operation of the input record to a new record
	--   for which the input record will not be modified
	-- ScribeResrepMergeToSame is a two-step process 
	--   with a clone operation of the second input record to the first input record
	--   followed by a delete operation on the second input record
	--   for which the first input record will be modified
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount int = 0, @errorCode int = 0, @newRecord bit = 0, @utcDate datetime2 = GETUTCDATE();
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordHandleToRetain IS NULL) OR (@RecordHandleToRetain = '')) RETURN -12;
	IF ((@RecordHandleToDelete IS NULL) OR (@RecordHandleToRetain = '')) RETURN -13;
	DECLARE @InfosetTypeCodeRef SMALLINT = 48; -- NexusResrep
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @RecordGuidKeyRetain UNIQUEIDENTIFIER = NULL, @AuditGuidRefRetain UNIQUEIDENTIFIER = NULL;
	SELECT @RecordGuidKeyRetain = RecordGuidKey, @AuditGuidRefRetain = AuditGuidRef 
		FROM dbo.NpdsResrepRoot WHERE (RecordHandle = @RecordHandleToRetain);
	IF ((@RecordGuidKeyRetain IS NULL) OR (@RecordGuidKeyRetain = @EmptyGuid)) RETURN -14;
	IF ((@AuditGuidRefRetain IS NULL) OR (@AuditGuidRefRetain = @EmptyGuid)) SET @AuditGuidRefRetain = NEWID();	

	DECLARE @RecordGuidKeyDelete UNIQUEIDENTIFIER = NULL, @AuditGuidRefDelete UNIQUEIDENTIFIER = NULL;
	SELECT @RecordGuidKeyDelete = RecordGuidKey, @AuditGuidRefDelete = AuditGuidRef 
		FROM dbo.NpdsResrepRoot WHERE (RecordHandle = @RecordHandleToDelete);
	IF ((@RecordGuidKeyDelete IS NULL) OR (@RecordGuidKeyDelete = @EmptyGuid)) RETURN -15;

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidKeyRetain) AND (AuditGuidKey = @AuditGuidRefRetain);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -25;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidKeyRetain, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			 ManagedByAgentGuidRef, CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidKeyRetain, @AuditGuidRefRetain, @HasIndex,
			 @AgentGuidRef, @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRefRetain);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -27;
	END;

	-- resrep record (main) table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepRoot
		WHERE (RecordGuidKey = @RecordGuidKeyRetain) AND (AuditGuidRef = @AuditGuidRefRetain);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount = 0) OR (@errorCode <> 0) RETURN -28;	

	UPDATE dbo.NpdsPortalSupportingTag 
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -31;	

	UPDATE dbo.NpdsPortalSupportingLabel
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -32;	

	UPDATE dbo.NpdsPortalCrossReference
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -33;	

	UPDATE dbo.NpdsPortalOtherText
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -34;	

	UPDATE dbo.NpdsDoorsLocation
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -35;	

	UPDATE dbo.NpdsDoorsDescription
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -36;	

	UPDATE dbo.NpdsDoorsProvenance
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -37;	

	UPDATE dbo.NpdsDoorsDistribution
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -38;	

	UPDATE dbo.NpdsDoorsFairMetric
		SET RecordGuidRef = @RecordGuidKeyRetain WHERE RecordGuidRef = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -39;	

	DELETE FROM dbo.NpdsResrepRoot
		WHERE RecordGuidKey = @RecordGuidKeyDelete;
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -41;	

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepEditServiceTags]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepEditServiceTags](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidKey UNIQUEIDENTIFIER = NULL,
@AuditGuidRef UNIQUEIDENTIFIER = NULL)
AS
BEGIN

	SET NOCOUNT ON; 
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @newRecord BIT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordGuidKey IS NULL) OR (@RecordGuidKey = @EmptyGuid)) RETURN -12;
	IF ((@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid)) RETURN -13;

	-- recover NPDS service guids from record guid and audit guid
	DECLARE @RecordDiristryGuidRef UNIQUEIDENTIFIER, @RecordRegistryGuidRef UNIQUEIDENTIFIER,
		@RecordDirectoryGuidRef UNIQUEIDENTIFIER, @RecordRegistrarGuidRef UNIQUEIDENTIFIER;
	SELECT @RecordDiristryGuidRef = RecordDiristryInfosetGuidRef, @RecordRegistryGuidRef = RecordRegistryInfosetGuidRef,
		@RecordDirectoryGuidRef = RecordDirectoryInfosetGuidRef, @RecordRegistrarGuidRef = RecordRegistrarInfosetGuidRef
		FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidKey) AND (AuditGuidRef = @AuditGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

	-- recover NPDS tags from NPDS guids
	DECLARE @RecordDiristryTag nvarchar(64), @RecordRegistryTag nvarchar(64), 
		@RecordDirectoryTag nvarchar(64), @RecordRegistrarTag nvarchar(64);

	SELECT @RecordDiristryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordDiristryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;

	SELECT @RecordRegistryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordRegistryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -24;

	SELECT @RecordDirectoryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordDirectoryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;

	SELECT @RecordRegistrarTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordRegistrarGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;

	-- update audit table with audit key
	UPDATE dbo.NpdsCoreAudit
		SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
		WHERE (AuditGuidKey = @AuditGuidRef);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN (@errorCode-1000);

	-- update root table with record key
	UPDATE dbo.NpdsResrepRoot
	    SET RecordDiristryTag = @RecordDiristryTag, RecordRegistryTag = @RecordRegistryTag,
			RecordDirectoryTag = @RecordDirectoryTag, RecordRegistrarTag = @RecordRegistrarTag
		WHERE (RecordGuidKey = @RecordGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF ((@rowCount <> 1) OR (@errorCode <> 0)) RETURN (@errorCode-2000); 

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepEditSplitToDifferent]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepEditSplitToDifferent](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordHandleToSplit CHAR(9) = '')
AS
BEGIN

	SET NOCOUNT ON; 
	-- compare with ScribeResrepRootEdit and ScribeResrepEditMergeToSame
	-- ScribeResrepEditSplitToDifferent is a one-step process
	--   with a clone operation of the input record to a new record
	--   for which the input record will not be modified
	-- ScribeResrepMergeToSame is a two-step process 
	--   with a clone operation of the second input record to the first input record
	--   followed by a delete operation on the second input record
	--   for which the first input record will be modified
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordHandleToSplit IS NULL) OR (@RecordHandleToSplit = '')) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @newRecord bit = 1, @utcDate datetime2 = GETUTCDATE();
	DECLARE @OldRecordGuidKey UNIQUEIDENTIFIER,  @OldInfosetGuidKey UNIQUEIDENTIFIER;  
	DECLARE @EntityTypeCode SMALLINT = 0;  -- default to unknown
	DECLARE @EntityInitialTag NVARCHAR(64) = '';
	DECLARE @EntityName NVARCHAR(256) = ''; 
	DECLARE @EntityNature NVARCHAR(1024) = '';
	DECLARE @InfosetTypeCodeRef SMALLINT = 48; -- NexusResrep
  DECLARE @ServiceTypeCode SMALLINT = 1; -- default to Nexus service
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @EntityOwnerGuidRef uniqueidentifier, @EntityContactGuidRef uniqueidentifier, @EntityOtherGuidRef uniqueidentifier; 
	DECLARE @RecordDiristryGuidRef uniqueidentifier, @RecordRegistryGuidRef uniqueidentifier, @RecordDirectoryGuidRef UNIQUEIDENTIFIER, @RecordRegistrarGuidRef UNIQUEIDENTIFIER; 
	DECLARE @InfosetIsAuthorPrivate BIT = 0, @InfosetIsAgentShared BIT = 0, @InfosetIsUpdaterLimited BIT = 0, @InfosetIsManagerReleased BIT = 0;
	DECLARE @NewRecordGuidKey UNIQUEIDENTIFIER = NEWID(), @NewInfosetGuidKey UNIQUEIDENTIFIER = NEWID(), @NewAuditGuidRef UNIQUEIDENTIFIER = NEWID();
	DECLARE @RecordDiristryTag nvarchar(128), @RecordRegistryTag nvarchar(128), @RecordDirectoryTag nvarchar(128), @RecordRegistrarTag nvarchar(128);

	-- retrieve old record to split

	SELECT @OldRecordGuidKey = RecordGuidKey, @OldInfosetGuidKey = InfosetGuidKey,
		@EntityTypeCode = EntityTypeCodeRef, @EntityInitialTag = EntityInitialTag, @EntityName = EntityName, @EntityNature = EntityNature,
		@EntityOwnerGuidRef = EntityOwnerInfosetGuidRef, @EntityContactGuidRef = EntityContactInfosetGuidRef, @EntityOtherGuidRef = EntityOtherInfosetGuidRef, 
		@RecordDiristryGuidRef = RecordDiristryInfosetGuidRef, @RecordRegistryGuidRef = RecordRegistryInfosetGuidRef, 
		@RecordDirectoryGuidRef = RecordDirectoryInfosetGuidRef, @RecordRegistrarGuidRef = RecordRegistrarInfosetGuidRef 		
		FROM dbo.NpdsResrepRoot WHERE (RecordHandle = @RecordHandleToSplit);

	-- recover NPDS tags from NPDS guids

	SELECT @RecordDiristryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordDiristryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;

	SELECT @RecordRegistryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordRegistryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

	SELECT @RecordDirectoryTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordDirectoryGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;

	SELECT @RecordRegistrarTag = EntityPrincipalTag 
		FROM dbo.NexusEntityNameTypeTag WHERE (InfosetGuidRef = @RecordRegistrarGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -24;

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @NewRecordGuidKey) AND (AuditGuidKey = @NewAuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -25;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @NewRecordGuidKey, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			 ManagedByAgentGuidRef, CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @NewRecordGuidKey, @NewAuditGuidRef, @HasIndex,
			 @AgentGuidRef, @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @NewAuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -27;
	END;

	-- resrep record (main) table with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepRoot
		WHERE (RecordGuidKey = @NewRecordGuidKey) AND (AuditGuidRef = @NewAuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -28;	
		
   -- insert new record           
	IF (@rowCount = 0) BEGIN

		-- BEGIN TRANSACTION ResRepRec
		-- create random character handle for new record	
		DECLARE @NewRecordHandle char(9);
		EXECUTE @errorCode = dbo.CoreRandomCharHandleGenerate @NewRecordHandle output;
		IF (@errorCode <> 0) RETURN (@errorCode-600);
		-- create new record
	   INSERT INTO dbo.NpdsResrepRoot
		(RecordGuidKey, AuditGuidRef, RecordHandle, InfosetGuidKey, 
		 EntityTypeCodeRef, EntityInitialTag, EntityName, EntityNature, EntityOtherInfosetGuidRef,
		 RecordDiristryInfosetGuidRef, RecordRegistryInfosetGuidRef, RecordDirectoryInfosetGuidRef, RecordRegistrarInfosetGuidRef, 
		 RecordDiristryTag, RecordRegistryTag, RecordDirectoryTag, RecordRegistrarTag,
		 InfosetIsAuthorPrivate, InfosetIsAgentShared, InfosetIsUpdaterLimited, InfosetIsManagerReleased)
		VALUES 
		(@NewRecordGuidKey, @NewAuditGuidRef, @NewRecordHandle, @NewInfosetGuidKey, 
		 @EntityTypeCode, @EntityInitialTag, @EntityName, @EntityNature, @EntityOtherGuidRef,
		 @RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
		 @RecordDiristryTag, @RecordRegistryTag, @RecordDirectoryTag, @RecordRegistrarTag,
		 @InfosetIsAuthorPrivate, @InfosetIsAgentShared, @InfosetIsUpdaterLimited, @InfosetIsManagerReleased);
		 SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		 IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN (@errorCode-700);
		 -- COMMIT TRANSACTION ResRepRec
		 				
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureCanonicalLabel 
			@AgentGuidRef, @NewInfosetGuidKey, @NewRecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
			@ServiceTypeCode, @EntityInitialTag, @EntityName, @EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-800);
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureAliasLabel 
			@AgentGuidRef, @NewInfosetGuidKey, @NewRecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef,
			@EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-900);

    -- update existing record
	END ELSE BEGIN
		UPDATE dbo.NpdsResrepRoot
	    SET EntityTypeCodeRef = @EntityTypeCode, 
			EntityName = @EntityName, EntityNature = @EntityNature, 
			EntityOtherInfosetGuidRef = @EntityOtherGuidRef,
			RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef, RecordDiristryTag = @RecordDiristryTag,
			RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef, RecordRegistryTag = @RecordRegistryTag,
			RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef, RecordDirectoryTag = @RecordDirectoryTag,
			RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef, RecordRegistrarTag = @RecordRegistrarTag,
			InfosetIsAuthorPrivate = @InfosetIsAuthorPrivate,
			InfosetIsAgentShared = @InfosetIsAgentShared,
			InfosetIsUpdaterLimited = @InfosetIsUpdaterLimited,
			InfosetIsManagerReleased = @InfosetIsManagerReleased
		WHERE (RecordGuidKey = @NewRecordGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF ((@rowCount <> 1) OR (@errorCode <> 0)) RETURN (@errorCode-1000); 
	END;

	EXECUTE @errorCode = dbo.ScribeSupportingTagClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-1000;	

	EXECUTE @errorCode = dbo.ScribeSupportingLabelClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-2000;	

	EXECUTE @errorCode = dbo.ScribeCrossReferenceClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-3000;	

	EXECUTE @errorCode = dbo.ScribeOtherTextClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-4000;	

	EXECUTE @errorCode = dbo.ScribeLocationClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-5000;	

	EXECUTE @errorCode = dbo.ScribeDescriptionClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-6000;	

	EXECUTE @errorCode = dbo.ScribeProvenanceClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-7000;	

	EXECUTE @errorCode = dbo.ScribeDistributionClone @AgentGuidRef, @OldRecordGuidKey, @NewRecordGuidKey;
	IF (@errorCode <> 0) RETURN @errorCode-8000;	

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepLeafDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepLeafDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)
AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot
		WHERE RecordGuidKey = @RecordGuidRef;
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN -- real "hard" delete
		DELETE FROM dbo.NpdsResrepRoot
			WHERE RecordGuidKey = @RecordGuidRef;
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN -- virtual "soft" delete
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			 DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepLeafEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepLeafEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidKey UNIQUEIDENTIFIER = NULL,
@RecordGuidKey UNIQUEIDENTIFIER = NULL,
@EntityTypeCode SMALLINT = 0, 
@EntityName NVARCHAR(256) = '', 
@EntityNature NVARCHAR(1024) = '',
@RecordDiristryGuidRef UNIQUEIDENTIFIER = NULL,
@RecordRegistryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordDirectoryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordRegistrarGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetIsAuthorPrivate BIT = 0,
@InfosetIsAgentShared BIT = 0,
@InfosetIsUpdaterLimited BIT = 0,
@InfosetIsManagerReleased BIT = 0)
AS
BEGIN

	SET NOCOUNT ON; 
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount int = 0, @errorCode int = 0, @newRecord bit = 0, @utcDate datetime2 = GETUTCDATE();
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordDiristryGuidRef IS NULL) OR (@RecordDiristryGuidRef = @EmptyGuid)) RETURN -12;
	IF ((@RecordRegistryGuidRef IS NULL) OR (@RecordRegistryGuidRef = @EmptyGuid)) RETURN -13;
	IF ((@RecordDirectoryGuidRef IS NULL) OR (@RecordDirectoryGuidRef = @EmptyGuid)) RETURN -14;
	IF ((@RecordRegistrarGuidRef IS NULL) OR (@RecordRegistrarGuidRef = @EmptyGuid)) RETURN -15;
	IF ((@InfosetGuidKey IS NULL) OR (@InfosetGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @InfosetGuidKey = NEWID();
	IF ((@RecordGuidKey IS NULL) OR (@RecordGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @RecordGuidKey = NEWID();
	IF (@EntityTypeCode IS NULL) SET @EntityTypeCode = 0;  -- default to unknown
	DECLARE @InfosetTypeCodeRef SMALLINT = 48; -- NexusResrep
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- check for non-null empty or blank strings or strings with double spaces
	IF (@EntityName IS NULL) SET @EntityName = ''; ELSE SET @EntityName = REPLACE(RTRIM(LTRIM(@EntityName)),'  ',' ');
	IF (@EntityNature IS NULL) SET @EntityNature = ''; ELSE SET @EntityNature = REPLACE(RTRIM(LTRIM(@EntityNature)),'  ',' ');

	-- local vars
  DECLARE @ServiceTypeCode SMALLINT = 1; -- default to Nexus service
	DECLARE @EntityOwnerGuidRef uniqueidentifier, @EntityContactGuidRef uniqueidentifier, @EntityOtherGuidRef uniqueidentifier; 
	DECLARE	@OldRegistryGuidRef uniqueidentifier, @UpdateLabels bit;

	-- assure consistency from registry+directory to matching diristry and vice versa
	IF (@RecordDirectoryGuidRef = @RecordRegistryGuidRef) BEGIN
		SET @RecordDiristryGuidRef = @RecordRegistryGuidRef;
	END ELSE BEGIN
		SET @RecordRegistryGuidRef = @RecordDiristryGuidRef;
		SET @RecordDirectoryGuidRef = @RecordDiristryGuidRef;
	END;

	/***
	-- check for change in registry
	SELECT @OldRegistryGuidRef = RecordRegistryInfosetGuidRef 
		FROM dbo.NpdsNexusResrepRecord WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@OldRegistryGuidRef = @RecordRegistryGuidRef) SET @UpdateLabels = 0; ELSE SET @UpdateLabels = 1;
	
	-- TODO: check prior existence of PTags under change of registry ?
	-- otherwise must either prevent the registry change or delete conflicting PTags	
	-- check for ability to move labels from old to new registry	
	IF (@UpdateLabels = 1) BEGIN
		SET @rowCount = 0;
		EXECUTE @errorCode = dbo.ScribeEntityLabelsCheck9 @RecordGuidKey, @RecordRegistryGuidRef, @rowCount;
		IF (@errorCode <> 0) RETURN (@errorCode-500);
		-- prevent the registry change
		IF (@rowCount > 0) BEGIN
			SET @UpdateLabels = 0;
			SET @RecordRegistryGuidRef = @OldRegistryGuidRef;
		END;
	END;
	***/

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidKey) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidKey, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			 ManagedByAgentGuidRef, CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidKey,@AuditGuidRef, @HasIndex,
			 @AgentGuidRef, @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- resrep record (main) table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepRoot
		WHERE (RecordGuidKey = @RecordGuidKey) AND (AuditGuidRef = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
		
   -- insert new record           
	IF (@rowCount = 0) BEGIN

		-- BEGIN TRANSACTION ResRepRec
		-- create random character handle for new record	
		DECLARE @RecordHandle char(9);
		EXECUTE @errorCode = dbo.CoreRandomCharHandleGenerate @RecordHandle output;
		IF (@errorCode <> 0) RETURN (@errorCode-600);
		-- create new record
	   INSERT INTO dbo.NpdsResrepRoot
		(RecordGuidKey, AuditGuidRef,
		 RecordHandle, InfosetGuidKey, EntityTypeCodeRef, EntityName, EntityNature, EntityOtherInfosetGuidRef,
		 RecordDiristryInfosetGuidRef, RecordRegistryInfosetGuidRef, RecordDirectoryInfosetGuidRef, RecordRegistrarInfosetGuidRef, 
		 InfosetIsAuthorPrivate, InfosetIsAgentShared, InfosetIsUpdaterLimited, InfosetIsManagerReleased)
		VALUES 
		(@RecordGuidKey, @AuditGuidRef,
		 @RecordHandle, @InfosetGuidKey, @EntityTypeCode, @EntityName, @EntityNature, @EntityOtherGuidRef,
		 @RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
		 @InfosetIsAuthorPrivate, @InfosetIsAgentShared, @InfosetIsUpdaterLimited, @InfosetIsManagerReleased);
		 SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		 IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN (@errorCode-700);
		 -- COMMIT TRANSACTION ResRepRec
		 				

    -- update existing record
	END ELSE BEGIN
		UPDATE dbo.NpdsResrepRoot
	    SET EntityTypeCodeRef = @EntityTypeCode, 
			EntityName = @EntityName, EntityNature = @EntityNature, 
			EntityOtherInfosetGuidRef = @EntityOtherGuidRef,
			RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef,
			RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef, 
			RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef, 
			RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef, 
			InfosetIsAuthorPrivate = @InfosetIsAuthorPrivate,
			InfosetIsAgentShared = @InfosetIsAgentShared,
			InfosetIsUpdaterLimited = @InfosetIsUpdaterLimited,
			InfosetIsManagerReleased = @InfosetIsManagerReleased
		WHERE (RecordGuidKey = @RecordGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF ((@rowCount <> 1) OR (@errorCode <> 0)) SELECT @newRecord = 1; 
	END;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepRootDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeResrepRootDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)
AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot
		WHERE RecordGuidKey = @RecordGuidRef;
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN -- real "hard" delete
		DELETE FROM dbo.NpdsResrepRoot
			WHERE RecordGuidKey = @RecordGuidRef;
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN -- virtual "soft" delete
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			 DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepRootEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepRootEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidKey UNIQUEIDENTIFIER = NULL,
@RecordGuidKey UNIQUEIDENTIFIER = NULL,
@EntityTypeCode SMALLINT = 0, 
@EntityInitialTag NVARCHAR(64) = '',
@EntityName NVARCHAR(256) = '', 
@EntityNature NVARCHAR(1024) = '',
@RecordDiristryGuidRef UNIQUEIDENTIFIER = NULL,
@RecordRegistryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordDirectoryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordRegistrarGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetIsAuthorPrivate BIT = 0,
@InfosetIsAgentShared BIT = 0,
@InfosetIsUpdaterLimited BIT = 0,
@InfosetIsManagerReleased BIT = 0)
AS
BEGIN

	SET NOCOUNT ON; 
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount int = 0, @errorCode int = 0, @newRecord bit = 0, @utcDate datetime2 = GETUTCDATE();
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordDiristryGuidRef IS NULL) OR (@RecordDiristryGuidRef = @EmptyGuid)) RETURN -12;
	IF ((@RecordRegistryGuidRef IS NULL) OR (@RecordRegistryGuidRef = @EmptyGuid)) RETURN -13;
	IF ((@RecordDirectoryGuidRef IS NULL) OR (@RecordDirectoryGuidRef = @EmptyGuid)) RETURN -14;
	IF ((@RecordRegistrarGuidRef IS NULL) OR (@RecordRegistrarGuidRef = @EmptyGuid)) RETURN -15;
	IF ((@InfosetGuidKey IS NULL) OR (@InfosetGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @InfosetGuidKey = NEWID();
	IF ((@RecordGuidKey IS NULL) OR (@RecordGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @RecordGuidKey = NEWID();
	IF (@EntityTypeCode IS NULL) SET @EntityTypeCode = 0;  -- default to unknown
	DECLARE @InfosetTypeCodeRef SMALLINT = 48; -- NexusResrep
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- check for non-null empty or blank strings or strings with double spaces
	IF (@EntityInitialTag IS NULL) SET @EntityInitialTag = ''; ELSE SET @EntityInitialTag = REPLACE(RTRIM(LTRIM(@EntityInitialTag)),'  ',' ');
	IF (@EntityName IS NULL) SET @EntityName = ''; ELSE SET @EntityName = REPLACE(RTRIM(LTRIM(@EntityName)),'  ',' ');
	IF (@EntityNature IS NULL) SET @EntityNature = ''; ELSE SET @EntityNature = REPLACE(RTRIM(LTRIM(@EntityNature)),'  ',' ');

	-- local vars
  DECLARE @ServiceTypeCode SMALLINT = 1; -- default to Nexus service
	DECLARE @EntityOwnerGuidRef uniqueidentifier, @EntityContactGuidRef uniqueidentifier, @EntityOtherGuidRef uniqueidentifier; 
	DECLARE	@OldRegistryGuidRef uniqueidentifier, @UpdateLabels bit;

	-- assure consistency from registry+directory to matching diristry and vice versa
	IF (@RecordDirectoryGuidRef = @RecordRegistryGuidRef) BEGIN
		SET @RecordDiristryGuidRef = @RecordRegistryGuidRef;
	END ELSE BEGIN
		SET @RecordRegistryGuidRef = @RecordDiristryGuidRef;
		SET @RecordDirectoryGuidRef = @RecordDiristryGuidRef;
	END;

	/***
	-- check for change in registry
	SELECT @OldRegistryGuidRef = RecordRegistryInfosetGuidRef 
		FROM dbo.NpdsNexusResrepRecord WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@OldRegistryGuidRef = @RecordRegistryGuidRef) SET @UpdateLabels = 0; ELSE SET @UpdateLabels = 1;
	
	-- TODO: check prior existence of PTags under change of registry ?
	-- otherwise must either prevent the registry change or delete conflicting PTags	
	-- check for ability to move labels from old to new registry	
	IF (@UpdateLabels = 1) BEGIN
		SET @rowCount = 0;
		EXECUTE @errorCode = dbo.ScribeEntityLabelsCheck9 @RecordGuidKey, @RecordRegistryGuidRef, @rowCount;
		IF (@errorCode <> 0) RETURN (@errorCode-500);
		-- prevent the registry change
		IF (@rowCount > 0) BEGIN
			SET @UpdateLabels = 0;
			SET @RecordRegistryGuidRef = @OldRegistryGuidRef;
		END;
	END;
	***/

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidKey) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidKey, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			 ManagedByAgentGuidRef, CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidKey,@AuditGuidRef, @HasIndex,
			 @AgentGuidRef, @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- resrep record (main) table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepRoot
		WHERE (RecordGuidKey = @RecordGuidKey) AND (AuditGuidRef = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
		
   -- insert new record           
	IF (@rowCount = 0) BEGIN

		-- BEGIN TRANSACTION ResRepRec
		-- create random character handle for new record	
		DECLARE @RecordHandle char(9);
		EXECUTE @errorCode = dbo.CoreRandomCharHandleGenerate @RecordHandle output;
		IF (@errorCode <> 0) RETURN (@errorCode-600);
		-- create new record
	   INSERT INTO dbo.NpdsResrepRoot
		(RecordGuidKey, AuditGuidRef, RecordHandle,
		 InfosetGuidKey, EntityTypeCodeRef, EntityInitialTag, EntityName, EntityNature, EntityOtherInfosetGuidRef,
		 RecordDiristryInfosetGuidRef, RecordRegistryInfosetGuidRef, RecordDirectoryInfosetGuidRef, RecordRegistrarInfosetGuidRef, 
		 InfosetIsAuthorPrivate, InfosetIsAgentShared, InfosetIsUpdaterLimited, InfosetIsManagerReleased)
		VALUES 
		(@RecordGuidKey, @AuditGuidRef,	 @RecordHandle,
		 @InfosetGuidKey, @EntityTypeCode, @EntityInitialTag, @EntityName, @EntityNature, @EntityOtherGuidRef,
		 @RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
		 @InfosetIsAuthorPrivate, @InfosetIsAgentShared, @InfosetIsUpdaterLimited, @InfosetIsManagerReleased);
		 SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		 IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN (@errorCode-700);
		 -- COMMIT TRANSACTION ResRepRec
		 				
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureCanonicalLabel 
			@AgentGuidRef, @InfosetGuidKey, @RecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
			@ServiceTypeCode, @EntityInitialTag, @EntityName, @EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-800);
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureAliasLabel 
			@AgentGuidRef, @InfosetGuidKey, @RecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef,
			@EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-900);

    -- update existing record
	END ELSE BEGIN
		UPDATE dbo.NpdsResrepRoot
	    SET EntityTypeCodeRef = @EntityTypeCode, 
			EntityName = @EntityName, EntityNature = @EntityNature, 
			EntityOtherInfosetGuidRef = @EntityOtherGuidRef,
			RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef,
			RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef, 
			RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef, 
			RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef, 
			InfosetIsAuthorPrivate = @InfosetIsAuthorPrivate,
			InfosetIsAgentShared = @InfosetIsAgentShared,
			InfosetIsUpdaterLimited = @InfosetIsUpdaterLimited,
			InfosetIsManagerReleased = @InfosetIsManagerReleased
		WHERE (RecordGuidKey = @RecordGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF ((@rowCount <> 1) OR (@errorCode <> 0)) RETURN (@errorCode-1000); 
	END;

	EXECUTE @errorCode = dbo.ScribeResrepEditServiceTags @AgentGuidRef, @RecordGuidKey, @AuditGuidRef;
	IF (@errorCode <> 0) RETURN (@errorCode-1100);

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepSnapshotCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepSnapshotCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusNexusSnapshot 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusNexusSnapshot 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusNexusSnapshot
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusNexusSnapshot 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusNexusSnapshot
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusNexusSnapshot 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same Resource
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusNexusSnapshot WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetNexusSnapshotsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepSnapshotDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepSnapshotDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepSnapshot
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsResrepSnapshot
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeResrepSnapshotCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepSnapshotEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeResrepSnapshotEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@ResrepSnapshotXml NVARCHAR(MAX) = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	-- TODO: expose InfosetTypeCode in API, also ResrepSnapshotJson
	DECLARE @InfosetTypeCodeRef SMALLINT = 49; -- NexusSnapshot
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepSnapshot 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepSnapshot
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsResrepSnapshot
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, ResrepSnapshotXml) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @ResrepSnapshotXml);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsResrepSnapshot SET ResrepSnapshotXml = @ResrepSnapshotXml
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeResrepSnapshotCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepStatusCountsUpdate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeResrepStatusCountsUpdate](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@ResrepEntityStatusCode SMALLINT = 0,
@ResrepRecordStatusCode SMALLINT = 0,
@ResrepInfosetStatusCode SMALLINT = 0,
@EntityLabelsStatusCode SMALLINT = 0,
@SupportingTagsStatusCode SMALLINT = 0,
@SupportingLabelsStatusCode SMALLINT = 0,
@CrossReferencesStatusCode SMALLINT = 0,
@OtherTextsStatusCode SMALLINT = 0,
@LocationsStatusCode SMALLINT = 0,
@DescriptionsStatusCode SMALLINT = 0,
@ProvenancesStatusCode SMALLINT = 0,
@DistributionsStatusCode SMALLINT = 0,
@FairMetricsStatusCode SMALLINT = 0,
@NexusSnapshotsStatusCode SMALLINT = 0,
@InfosetPortalStatusCode SMALLINT = 0,
@InfosetDoorsStatusCode SMALLINT = 0)
AS
BEGIN

	SET NOCOUNT ON;  -- vars and pars
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid)) RETURN -12;
	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	DECLARE @StatusAtPortal SMALLINT = 0, @StatusAtDoors SMALLINT = 0;
	DECLARE @CountEntityLabels INT, @CountSupportingTags INT, @CountSupportingLabels INT, @CountCrossReferences INT, @CountOtherTexts INT;
	DECLARE @CountLocations INT, @CountDescriptions INT, @CountProvenances INT, @CountDistributions INT, @CountFairMetrics INT, @CountNexusSnapshots INT;

	DECLARE @ResrepEntityStatusName nvarchar(32), @ResrepRecordStatusName nvarchar(32),@ResrepInfosetStatusName nvarchar(32);
	DECLARE @EntityLabelsStatusName nvarchar(32), @SupportingTagsStatusName nvarchar(32), @SupportingLabelsStatusName nvarchar(32), 
		@CrossReferencesStatusName nvarchar(32), @OtherTextsStatusName nvarchar(32);
	DECLARE @LocationsStatusName nvarchar(32), @DescriptionsStatusName nvarchar(32), @ProvenancesStatusName nvarchar(32), 
		@DistributionsStatusName nvarchar(32), @FairMetricsStatusName nvarchar(32), @NexusSnapshotsStatusName nvarchar(32);
	DECLARE @InfosetPortalStatusName nvarchar(32), @InfosetDoorsStatusName nvarchar(32);

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot 
		WHERE (RecordGuidKey = @RecordGuidRef);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) RETURN -14;	

	-- reset StatusNames from StatusCodes

	SELECT @ResrepEntityStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @ResrepEntityStatusCode);
	SELECT @ResrepRecordStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @ResrepRecordStatusCode);
	SELECT @ResrepInfosetStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @ResrepInfosetStatusCode);

	SELECT @EntityLabelsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @EntityLabelsStatusCode);
	SELECT @SupportingTagsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @SupportingTagsStatusCode);
	SELECT @SupportingLabelsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @SupportingLabelsStatusCode);
	SELECT @CrossReferencesStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @CrossReferencesStatusCode);
	SELECT @OtherTextsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @OtherTextsStatusCode);

	SELECT @LocationsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @LocationsStatusCode);
	SELECT @DescriptionsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @DescriptionsStatusCode);
	SELECT @ProvenancesStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @ProvenancesStatusCode);
	SELECT @DistributionsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @DistributionsStatusCode);
	SELECT @FairMetricsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @FairMetricsStatusCode);
	SELECT @NexusSnapshotsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @NexusSnapshotsStatusCode);

	SELECT @InfosetPortalStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @InfosetPortalStatusCode);
	SELECT @InfosetDoorsStatusName = StatusName FROM dbo.NpdsCoreInfosetStatusEnum WHERE (CodeKey = @InfosetDoorsStatusCode);

	-- main method
	
	SELECT @CountEntityLabels = COUNT(*) FROM dbo.NexusEntityLabel WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountSupportingTags = COUNT(*) FROM dbo.NexusSupportingTag WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountSupportingLabels = COUNT(*) FROM dbo.NexusSupportingLabel WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountCrossReferences = COUNT(*) FROM dbo.NexusCrossReference WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountOtherTexts = COUNT(*) FROM dbo.NexusOtherText WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountLocations = COUNT(*) FROM dbo.NexusLocation WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountDescriptions = COUNT(*) FROM dbo.NexusDescription WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountProvenances = COUNT(*) FROM dbo.NexusProvenance WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountDistributions = COUNT(*) FROM dbo.NexusDistribution WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountFairMetrics = COUNT(*) FROM dbo.NexusFairMetric WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
	SELECT @CountNexusSnapshots = COUNT(*) FROM dbo.NexusNexusSnapshot WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);

	UPDATE dbo.NpdsResrepRoot SET
		InfosetResrepEntityStatusCode = @ResrepEntityStatusCode, InfosetResrepEntityStatusName = @ResrepEntityStatusName,
		InfosetResrepRecordStatusCode = @ResrepRecordStatusCode, InfosetResrepRecordStatusName = @ResrepRecordStatusName,
		InfosetResrepInfosetStatusCode = @ResrepInfosetStatusCode, InfosetResrepInfosetStatusName = @ResrepInfosetStatusName,
	InfosetEntityLabelsCount = @CountEntityLabels, 
	InfosetEntityLabelsStatusCode = @EntityLabelsStatusCode, InfosetEntityLabelsStatusName = @EntityLabelsStatusName,
	InfosetSupportingTagsCount = @CountSupportingTags, 
	InfosetSupportingTagsStatusCode = @SupportingTagsStatusCode, InfosetSupportingTagsStatusName = @SupportingTagsStatusName,
	InfosetSupportingLabelsCount = @CountSupportingLabels, 
	InfosetSupportingLabelsStatusCode = @SupportingLabelsStatusCode, InfosetSupportingLabelsStatusName = @SupportingLabelsStatusName,
	InfosetCrossReferencesCount = @CountCrossReferences, 
	InfosetCrossReferencesStatusCode = @CrossReferencesStatusCode, InfosetCrossReferencesStatusName = @CrossReferencesStatusName,
	InfosetOtherTextsCount = @CountOtherTexts, 
	InfosetOtherTextsStatusCode = @OtherTextsStatusCode, InfosetOtherTextsStatusName = @OtherTextsStatusName,
	InfosetLocationsCount = @CountLocations, 
	InfosetLocationsStatusCode = @LocationsStatusCode, InfosetLocationsStatusName = @LocationsStatusName,
	InfosetDescriptionsCount = @CountDescriptions, 
	InfosetDescriptionsStatusCode = @DescriptionsStatusCode, InfosetDescriptionsStatusName = @DescriptionsStatusName,
	InfosetProvenancesCount = @CountProvenances, 
	InfosetProvenancesStatusCode = @ProvenancesStatusCode, InfosetProvenancesStatusName = @ProvenancesStatusName,
	InfosetDistributionsCount = @CountDistributions, 
	InfosetDistributionsStatusCode = @DistributionsStatusCode, InfosetDistributionsStatusName = @DistributionsStatusName,
	InfosetFairMetricsCount = @CountFairMetrics, 
	InfosetFairMetricsStatusCode = @FairMetricsStatusCode, InfosetFairMetricsStatusName = @FairMetricsStatusName,
	InfosetNexusSnapshotsCount = @CountNexusSnapshots, 
	InfosetNexusSnapshotsStatusCode = @NexusSnapshotsStatusCode, InfosetNexusSnapshotsStatusName = @NexusSnapshotsStatusName,
		InfosetPortalStatusTestedOn = @utcDate,
		InfosetPortalStatusCode = @InfosetPortalStatusCode, InfosetPortalStatusName = @InfosetPortalStatusName, 
		InfosetDoorsStatusTestedOn = @utcDate,
		InfosetDoorsStatusCode = @InfosetDoorsStatusCode, InfosetDoorsStatusName = @InfosetDoorsStatusName
		WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;

	UPDATE dbo.NpdsCoreAudit SET
		UpdatedByAgentGuidRef = @AgentGuidRef, UpdatedOn = @utcDate
		WHERE (AuditGuidKey = @AuditGuidRef)
		
	-- no error return
	RETURN 0;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepStemDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepStemDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)
AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot
		WHERE RecordGuidKey = @RecordGuidRef;
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN -- real "hard" delete
		DELETE FROM dbo.NpdsResrepRoot
			WHERE RecordGuidKey = @RecordGuidRef;
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN -- virtual "soft" delete
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			 DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepStemEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepStemEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidKey UNIQUEIDENTIFIER = NULL,
@RecordGuidKey UNIQUEIDENTIFIER = NULL,
@EntityTypeCode SMALLINT = 0, 
@EntityInitialTag NVARCHAR(64) = '',
@EntityName NVARCHAR(256) = '', 
@EntityNature NVARCHAR(1024) = '',
@RecordDiristryGuidRef UNIQUEIDENTIFIER = NULL,
@RecordRegistryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordDirectoryGuidRef UNIQUEIDENTIFIER = NULL, 
@RecordRegistrarGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetIsAuthorPrivate BIT = 0,
@InfosetIsAgentShared BIT = 0,
@InfosetIsUpdaterLimited BIT = 0,
@InfosetIsManagerReleased BIT = 0)
AS
BEGIN

	SET NOCOUNT ON; 
		
	-- check the guidkeys and codes
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  DECLARE @rowCount int = 0, @errorCode int = 0, @newRecord bit = 0, @utcDate datetime2 = GETUTCDATE();
	IF ((@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@RecordDiristryGuidRef IS NULL) OR (@RecordDiristryGuidRef = @EmptyGuid)) RETURN -12;
	IF ((@RecordRegistryGuidRef IS NULL) OR (@RecordRegistryGuidRef = @EmptyGuid)) RETURN -13;
	IF ((@RecordDirectoryGuidRef IS NULL) OR (@RecordDirectoryGuidRef = @EmptyGuid)) RETURN -14;
	IF ((@RecordRegistrarGuidRef IS NULL) OR (@RecordRegistrarGuidRef = @EmptyGuid)) RETURN -15;
	IF ((@InfosetGuidKey IS NULL) OR (@InfosetGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @InfosetGuidKey = NEWID();
	IF ((@RecordGuidKey IS NULL) OR (@RecordGuidKey = @EmptyGuid)) SELECT @newRecord = 1, @RecordGuidKey = NEWID();
	IF (@EntityTypeCode IS NULL) SET @EntityTypeCode = 0;  -- default to unknown
	DECLARE @InfosetTypeCodeRef SMALLINT = 48; -- NexusResrep
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- check for non-null empty or blank strings or strings with double spaces
	IF (@EntityInitialTag IS NULL) SET @EntityInitialTag = ''; ELSE SET @EntityInitialTag = REPLACE(RTRIM(LTRIM(@EntityInitialTag)),'  ',' ');
	IF (@EntityName IS NULL) SET @EntityName = ''; ELSE SET @EntityName = REPLACE(RTRIM(LTRIM(@EntityName)),'  ',' ');
	IF (@EntityNature IS NULL) SET @EntityNature = ''; ELSE SET @EntityNature = REPLACE(RTRIM(LTRIM(@EntityNature)),'  ',' ');

	-- local vars
  DECLARE @ServiceTypeCode SMALLINT = 1; -- default to Nexus service
	DECLARE @EntityOwnerGuidRef uniqueidentifier, @EntityContactGuidRef uniqueidentifier, @EntityOtherGuidRef uniqueidentifier; 
	DECLARE	@OldRegistryGuidRef uniqueidentifier, @UpdateLabels bit;

	-- assure consistency from registry+directory to matching diristry and vice versa
	IF (@RecordDirectoryGuidRef = @RecordRegistryGuidRef) BEGIN
		SET @RecordDiristryGuidRef = @RecordRegistryGuidRef;
	END ELSE BEGIN
		SET @RecordRegistryGuidRef = @RecordDiristryGuidRef;
		SET @RecordDirectoryGuidRef = @RecordDiristryGuidRef;
	END;

	/***
	-- check for change in registry
	SELECT @OldRegistryGuidRef = RecordRegistryInfosetGuidRef 
		FROM dbo.NpdsNexusResrepRecord WHERE (RecordGuidKey = @RecordGuidKey);
	IF (@OldRegistryGuidRef = @RecordRegistryGuidRef) SET @UpdateLabels = 0; ELSE SET @UpdateLabels = 1;
	
	-- TODO: check prior existence of PTags under change of registry ?
	-- otherwise must either prevent the registry change or delete conflicting PTags	
	-- check for ability to move labels from old to new registry	
	IF (@UpdateLabels = 1) BEGIN
		SET @rowCount = 0;
		EXECUTE @errorCode = dbo.ScribeEntityLabelsCheck9 @RecordGuidKey, @RecordRegistryGuidRef, @rowCount;
		IF (@errorCode <> 0) RETURN (@errorCode-500);
		-- prevent the registry change
		IF (@rowCount > 0) BEGIN
			SET @UpdateLabels = 0;
			SET @RecordRegistryGuidRef = @OldRegistryGuidRef;
		END;
	END;
	***/

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidKey) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidKey, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			 ManagedByAgentGuidRef, CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidKey,@AuditGuidRef, @HasIndex,
			 @AgentGuidRef, @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- resrep record (main) table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsResrepRoot
		WHERE (RecordGuidKey = @RecordGuidKey) AND (AuditGuidRef = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
		
   -- insert new record           
	IF (@rowCount = 0) BEGIN

		-- BEGIN TRANSACTION ResRepRec
		-- create random character handle for new record	
		DECLARE @RecordHandle char(9);
		EXECUTE @errorCode = dbo.CoreRandomCharHandleGenerate @RecordHandle output;
		IF (@errorCode <> 0) RETURN (@errorCode-600);
		-- create new record
	   INSERT INTO dbo.NpdsResrepRoot
		(RecordGuidKey, AuditGuidRef, RecordHandle,
		 InfosetGuidKey, EntityTypeCodeRef, EntityInitialTag, EntityName, EntityNature, EntityOtherInfosetGuidRef,
		 RecordDiristryInfosetGuidRef, RecordRegistryInfosetGuidRef, RecordDirectoryInfosetGuidRef, RecordRegistrarInfosetGuidRef, 
		 InfosetIsAuthorPrivate, InfosetIsAgentShared, InfosetIsUpdaterLimited, InfosetIsManagerReleased)
		VALUES 
		(@RecordGuidKey, @AuditGuidRef,	 @RecordHandle,
		 @InfosetGuidKey, @EntityTypeCode, @EntityInitialTag, @EntityName, @EntityNature, @EntityOtherGuidRef,
		 @RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
		 @InfosetIsAuthorPrivate, @InfosetIsAgentShared, @InfosetIsUpdaterLimited, @InfosetIsManagerReleased);
		 SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		 IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN (@errorCode-700);
		 -- COMMIT TRANSACTION ResRepRec
		 				
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureCanonicalLabel 
			@AgentGuidRef, @InfosetGuidKey, @RecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef, 
			@ServiceTypeCode, @EntityInitialTag, @EntityName, @EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-800);
		EXECUTE @errorCode = dbo.ScribeEntityLabelAssureAliasLabel 
			@AgentGuidRef, @InfosetGuidKey, @RecordGuidKey, 
			@RecordDiristryGuidRef, @RecordRegistryGuidRef, @RecordDirectoryGuidRef, @RecordRegistrarGuidRef,
			@EntityTypeCode;
		IF (@errorCode <> 0) RETURN (@errorCode-900);

    -- update existing record
	END ELSE BEGIN
		UPDATE dbo.NpdsResrepRoot
	    SET EntityTypeCodeRef = @EntityTypeCode, 
			EntityName = @EntityName, EntityNature = @EntityNature, 
			EntityOtherInfosetGuidRef = @EntityOtherGuidRef,
			RecordDiristryInfosetGuidRef = @RecordDiristryGuidRef,
			RecordRegistryInfosetGuidRef = @RecordRegistryGuidRef, 
			RecordDirectoryInfosetGuidRef = @RecordDirectoryGuidRef, 
			RecordRegistrarInfosetGuidRef = @RecordRegistrarGuidRef, 
			InfosetIsAuthorPrivate = @InfosetIsAuthorPrivate,
			InfosetIsAgentShared = @InfosetIsAgentShared,
			InfosetIsUpdaterLimited = @InfosetIsUpdaterLimited,
			InfosetIsManagerReleased = @InfosetIsManagerReleased
		WHERE (RecordGuidKey = @RecordGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF ((@rowCount <> 1) OR (@errorCode <> 0)) RETURN (@errorCode-1000); 
	END;

	EXECUTE @errorCode = dbo.ScribeResrepEditServiceTags @AgentGuidRef, @RecordGuidKey, @AuditGuidRef;
	IF (@errorCode <> 0) RETURN (@errorCode-1100);

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeResrepTimestamp]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeResrepTimestamp](
@RecordGuidRef UNIQUEIDENTIFIER,
@RecordCached BIT = 0,
@RecordArchived BIT = 0)
AS
BEGIN

	-- compare with ScribeResrepAuditTimestamp
	-- TODO: deprecate ScribeResrepTimestamp, migrate to ScribeResrepAuditTimestamp

	SET NOCOUNT ON;
	
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER, @utcDate DATETIME2 = GETUTCDATE();
	
	--IF (@RecordCached = 1) AND (@RecordArchived = 1) BEGIN
  
	--    UPDATE dbo.NpdsResrepRoot
	--	    SET RecordUpdatedOn = @utcDate, RecordCachedOn = @utcDate, RecordArchivedOn = @utcDate
	--		WHERE (ResourceGuidKey = @RecordGuidRef);
			
	--END ELSE IF (@RecordCached = 1) BEGIN
  
	--    UPDATE dbo.NpdsResrepRoot
	--	    SET RecordUpdatedOn = @utcDate, RecordCachedOn = @utcDate
	--		WHERE (ResourceGuidKey = @RecordGuidRef);
			
	--END ELSE IF (@RecordArchived = 1) BEGIN
  
	--    UPDATE dbo.NpdsResrepRoot
	--	    SET RecordUpdatedOn = @utcDate, RecordArchivedOn = @utcDate
	--		WHERE (ResourceGuidKey = @RecordGuidRef);
				
	--END 
  
	SELECT @AuditGuidRef = AuditGuidRef 
		FROM dbo.NpdsResrepRoot WHERE (RecordGuidKey = @RecordGuidRef);
	UPDATE dbo.NpdsCoreAudit
	    SET UpdatedOn = @utcDate WHERE (AuditGuidKey = @AuditGuidRef);

	IF (ISNULL(@@ROWCOUNT,0) <> 1) RETURN -11 
	ELSE RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceCoreDefaultCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeServiceCoreDefaultCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusServiceCoreDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusServiceCoreDefault 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusServiceCoreDefault
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusServiceCoreDefault 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusServiceCoreDefault
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusServiceCoreDefault 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceCoreDefaultDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeServiceCoreDefaultDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsServiceDefault 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeServiceCoreDefaultCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceCoreDefaultEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeServiceCoreDefaultEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@DiristryIGuidRef UNIQUEIDENTIFIER = NULL,
@RegistryIGuidRef UNIQUEIDENTIFIER = NULL,
@DirectoryIGuidRef UNIQUEIDENTIFIER = NULL,
@RegistrarIGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 12; -- CoreServiceDefault
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			@utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsServiceDefault
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	
	 
 	IF (@rowCount = 0) BEGIN	 
	   INSERT INTO dbo.NpdsServiceDefault
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, 
			 DiristryInfosetGuidRef, RegistryInfosetGuidRef, DirectoryInfosetGuidRef, RegistrarInfosetGuidRef) 
		VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, 
			@DiristryIGuidRef, @RegistryIGuidRef, @DirectoryIGuidRef, @RegistrarIGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsServiceDefault SET  
			DiristryInfosetGuidRef = @DiristryIGuidRef, RegistryInfosetGuidRef = @RegistryIGuidRef, 
			DirectoryInfosetGuidRef = @DirectoryIGuidRef, RegistrarInfosetGuidRef = @RegistrarIGuidRef
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;


	EXEC @errorCode = dbo.ScribeServiceCoreDefaultCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;

	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;
	
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceEditorRequestDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeServiceEditorRequestDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceAccessAuth
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsServiceAccessAuth
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;
 
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceEditorRequestEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeServiceEditorRequestEdit](
@AccessRequestedForAgentGuidRef UNIQUEIDENTIFIER = NULL,
@AccessRequestedByAgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef  UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@RequestIsApproved BIT = 0,
@RequestIsDenied BIT = 0)

AS BEGIN

	SET NOCOUNT ON; 
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';	
	-- admin or editor may create (assign) requests for agents 
	IF ((@AccessRequestedByAgentGuidRef IS NULL) OR (@AccessRequestedByAgentGuidRef = @EmptyGuid)) RETURN -11;
	IF ((@InfosetGuidRef IS NULL) OR (@InfosetGuidRef = @EmptyGuid)) RETURN -12;
	DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = NEWID();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 8; -- ServiceEditorRequest
	DECLARE @HasIndex SMALLINT; -- random unique index
	IF ((@AccessRequestedForAgentGuidRef IS NULL) OR (@AccessRequestedForAgentGuidRef = @EmptyGuid)) 
		SET @AccessRequestedForAgentGuidRef = @AccessRequestedByAgentGuidRef;

	IF ((@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid)) BEGIN
		SELECT @RecordGuidRef = RecordGuidKey FROM dbo.NpdsResrepRoot
			WHERE (InfosetGuidKey = @InfosetGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -13;
	END;

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceAccessAuth 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	DECLARE @RequestedForAgentIsAuthor bit = 0, @RequestedForAgentIsEditor bit = 0, @EditorHasServiceAccess bit = 0;
	-- approval has lower priority than denial
	IF (@RequestIsApproved = 1) SET @EditorHasServiceAccess = 1;
	-- denial has higher priority than approval
	IF (@RequestIsDenied = 1) SET @EditorHasServiceAccess = 0;

	-- check status of RequestedForAgent
	SELECT @RequestedForAgentIsAuthor = AgentIsAuthor, @RequestedForAgentIsEditor = AgentIsEditor 
		FROM dbo.NpdsCoreSessionAgent WHERE (AgentGuidKey = @AccessRequestedForAgentGuidRef);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -14;

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, HasIndex,
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, @HasIndex,
			@utcDate, @AccessRequestedByAgentGuidRef, @utcDate, @AccessRequestedByAgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET  UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AccessRequestedByAgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsServiceAccessAuth 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsServiceAccessAuth
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, AccessRequestedForAgentGuidRef) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @AccessRequestedForAgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsServiceAccessAuth SET 
			RequestIsApproved = @RequestIsApproved, 
			RequestIsDenied = @RequestIsDenied,
			EditorHasServiceAccess = @EditorHasServiceAccess,
			AccessApprovedByAgentGuidRef = @AccessRequestedByAgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;	
		IF (@EditorHasServiceAccess = 1) AND (@RequestedForAgentIsAuthor = 0 OR @RequestedForAgentIsEditor = 0) BEGIN
			UPDATE dbo.NpdsCoreSessionAgent SET
				AgentIsAuthor = 1, AgentIsEditor = 1
				WHERE (AgentGuidKey = @AccessRequestedForAgentGuidRef);
 			SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
			IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -27;				
		END;
	END;
   
	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionAndDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeServiceRestrictionAndDelete](
@AgentGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@RestrictionAndGuidKey uniqueidentifier = NULL,
@IsRealDelete bit = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid)  RETURN -12;
	IF (@RestrictionAndGuidKey IS NULL) OR (@RestrictionAndGuidKey = @EmptyGuid)  RETURN -13;
	DECLARE @rowCount int = 0, @errorCode int = 0;
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceRestrictionAnd
		WHERE (RecordGuidRef = @RecordGuidRef) AND (RestrictionAndGuidKey = @RestrictionAndGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN
		DELETE FROM dbo.NpdsServiceRestrictionOr
			WHERE (RestrictionAndGuidRef = @RestrictionAndGuidKey);
	    DELETE FROM dbo.NpdsServiceRestrictionAnd
			WHERE (RestrictionAndGuidKey = @RestrictionAndGuidKey);
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = GETUTCDATE(), DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
	END;
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -15;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -16;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionAndEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeServiceRestrictionAndEdit](
@AgentGuidRef uniqueidentifier = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@RestrictionAndGuidKey uniqueidentifier = NULL,
@RestrictionAndPriority SMALLINT = 0,
@RestrictionName nvarchar(64) = '',
@IsExcluding bit = 0,
@IsSufficient bit = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@RestrictionAndGuidKey IS NULL) OR (@RestrictionAndGuidKey = @EmptyGuid) SET @RestrictionAndGuidKey = NEWID();		 
	DECLARE @InfosetTypeCodeRef SMALLINT = 14; -- CoreRestrictionAnd
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceRestrictionAnd 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (RestrictionAndGuidKey = @RestrictionAndGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsExcluding, IsSufficient,
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @RestrictionAndPriority, @IsExcluding, @IsSufficient,
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @RestrictionAndPriority, IsExcluding = @IsExcluding, IsSufficient = @IsSufficient,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	
	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsServiceRestrictionAnd 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (RestrictionAndGuidKey = @RestrictionAndGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsServiceRestrictionAnd
			(RecordGuidRef, RestrictionAndGuidKey, AuditGuidRef, RestrictionName) 
			VALUES 
			(@RecordGuidRef, @RestrictionAndGuidKey, @AuditGuidRef, @RestrictionName);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsServiceRestrictionAnd SET RestrictionName = @RestrictionName
			WHERE (RecordGuidRef = @RecordGuidRef) AND (RestrictionAndGuidKey = @RestrictionAndGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;
	
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

    RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionOrDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeServiceRestrictionOrDelete](
@AgentGuidRef uniqueidentifier = NULL,
@ResourceGuidRef UNIQUEIDENTIFIER = NULL,
@RestrictionAndGuidRef uniqueidentifier = NULL,
@RestrictionOrGuidKey uniqueidentifier = NULL,
@IsRealDelete bit = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid)  RETURN -11;
	IF (@RestrictionAndGuidRef IS NULL) OR (@RestrictionAndGuidRef = @EmptyGuid)  RETURN -12;
	IF (@RestrictionOrGuidKey IS NULL) OR (@RestrictionOrGuidKey = @EmptyGuid) RETURN -13;
	DECLARE @rowCount int = 0, @errorCode int = 0;
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceRestrictionOr
		WHERE (RestrictionAndGuidRef = @RestrictionAndGuidRef) AND (RestrictionOrGuidKey = @RestrictionOrGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsServiceRestrictionOr
			WHERE (RestrictionOrGuidKey = @RestrictionOrGuidKey);
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = GETUTCDATE(), DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
	END;
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -15;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @ResourceGuidRef;
	IF (@errorCode <> 0) RETURN -16;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionOrEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeServiceRestrictionOrEdit](
@AgentGuidRef uniqueidentifier = NULL,
@InfosetGuidRef uniqueidentifier = NULL,
@RecordGuidRef uniqueidentifier = NULL,
@RestrictionAndGuidRef uniqueidentifier = NULL,
@RestrictionOrGuidKey uniqueidentifier = NULL,
@RestrictionOrPriority SMALLINT = 0,
@RestrictionValue nvarchar(256) = '',
@IsWordPhrase bit = 0,
@IsConceptLabel bit = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@RestrictionAndGuidRef IS NULL) OR (@RestrictionAndGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	IF (@RestrictionOrGuidKey IS NULL) OR (@RestrictionOrGuidKey = @EmptyGuid) SET @RestrictionOrGuidKey = NEWID();		 
	DECLARE @InfosetTypeCodeRef SMALLINT = 15; -- CoreRestrictionOr
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsServiceRestrictionOr 
		WHERE (RestrictionAndGuidRef = @RestrictionAndGuidRef) AND (RestrictionOrGuidKey = @RestrictionOrGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- audit table first with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsWordPhrase, IsConceptLabel,
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @RestrictionOrPriority, @IsWordPhrase, @IsConceptLabel,
			@utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @RestrictionOrPriority,
			IsWordPhrase = @IsWordPhrase, IsConceptLabel = @IsConceptLabel, 
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	
	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = COUNT(*) FROM dbo.NpdsServiceRestrictionOr 
		WHERE (RestrictionAndGuidRef = @RestrictionAndGuidRef) AND (RestrictionOrGuidKey = @RestrictionOrGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsServiceRestrictionOr
			(RestrictionOrGuidKey, RestrictionAndGuidRef, AuditGuidRef, RestrictionValue) 
			VALUES 
			(@RestrictionOrGuidKey, @RestrictionAndGuidRef, @AuditGuidRef, @RestrictionValue);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsServiceRestrictionOr SET RestrictionValue = @RestrictionValue
			WHERE (RestrictionAndGuidRef = @RestrictionAndGuidRef) AND (RestrictionOrGuidKey = @RestrictionOrGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;
	
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

    RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionXml]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeServiceRestrictionXml]
(@ServiceRGuid uniqueidentifier,
 @RestrictionXml xml output)
AS
BEGIN

	SET NOCOUNT ON;

	SET @RestrictionXml = 
	(SELECT COUNT(*) AS '@Count', 
	
		(SELECT COUNT(*) AS '@Count',	

			(SELECT 
				AndHasIndex AS "@AndIndex",
				OrHasIndex AS "@OrIndex", 
				RestrictionValue AS "text()"
			FROM dbo.NexusServiceRestrictionOr
			WHERE (RecordGuidRef = @ServiceRGuid) AND (IsConceptLabel = 0) AND (IsWordPhrase = 0)
			ORDER BY AndHasIndex, OrHasIndex
			FOR XML PATH('WordStem'), TYPE),		
			
			(SELECT 
				AndHasIndex AS "@AndIndex",
				OrHasIndex AS "@OrIndex", 
				RestrictionValue AS "text()"
			FROM dbo.NexusServiceRestrictionOr 
			WHERE (RecordGuidRef = @ServiceRGuid) AND (IsConceptLabel = 0) AND (IsWordPhrase = 1)
			ORDER BY AndHasIndex, OrHasIndex
			FOR XML PATH('WordPhrase'), TYPE)
				
		FROM dbo.NexusServiceRestrictionOr
		WHERE (RecordGuidRef = @ServiceRGuid) AND (IsConceptLabel = 0)
		FOR XML PATH('ForSupportingTags'), TYPE),
	
		(SELECT COUNT(*) AS '@Count',
		
			(SELECT 
				AndHasIndex AS "@AndIndex",
				OrHasIndex AS "@OrIndex", 
				RestrictionValue AS "text()"
			FROM dbo.NexusServiceRestrictionOr
			WHERE (RecordGuidRef = @ServiceRGuid) AND (IsConceptLabel = 1)
			ORDER BY AndHasIndex, OrHasIndex
			FOR XML PATH('ConceptLabel'), TYPE)
			
		FROM dbo.NexusServiceRestrictionOr
		WHERE (RecordGuidRef = @ServiceRGuid) AND (IsConceptLabel = 1)
		FOR XML PATH('ForSupportingLabels'), TYPE)	
	
	FROM dbo.NexusServiceRestrictionOr
	WHERE (RecordGuidRef = @ServiceRGuid)
	FOR XML PATH('ServiceRestrictions'), TYPE);
	
END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionXmlCursor]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeServiceRestrictionXmlCursor](
@NpdsAgentGuid uniqueidentifier = null)
AS
BEGIN

	SET NOCOUNT ON;

	IF (@NpdsAgentGuid IS NULL) RETURN -12;

	DECLARE @CompRGuid uniqueidentifier, @CompIGuid uniqueidentifier, @RegXml xml, 
	   @RestrictionsWithNullRefs int, @ComponentsUpdated int;
	SET @RestrictionsWithNullRefs = 0;
	SET @ComponentsUpdated = 0;

	-- TODO: generalize for all components (registries, directories, and diristries)
	-- TODO: requires EntityType specific restriction XML for EntityType specific fields
	DECLARE registries CURSOR FOR
		SELECT RecordGuidKey, InfosetGuidKey FROM dbo.NpdsResrepRoot
		WHERE EntityTypeCodeRef = 3 OR EntityTypeCodeRef = 5 OR EntityTypeCodeRef = 11 OR EntityTypeCodeRef = 31 
		ORDER BY EntityName;

	OPEN registries
	FETCH registries INTO @CompRGuid, @CompIGuid;

	WHILE (@@FETCH_STATUS=0) BEGIN
		
		EXEC dbo.ScribeServiceRestrictionXmlUpdate @CompRGuid, @NpdsAgentGuid;		
		SET @ComponentsUpdated = @ComponentsUpdated + 1;
		FETCH registries INTO @CompRGuid, @CompIGuid;
	
	END

	CLOSE registries;
	DEALLOCATE registries;
	
	RETURN @ComponentsUpdated;

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeServiceRestrictionXmlUpdate]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeServiceRestrictionXmlUpdate](
@ServiceRGuid uniqueidentifier = null,
@NpdsAgentGuid uniqueidentifier = null)
AS 
BEGIN

	SET NOCOUNT ON;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
	
	IF (@ServiceRGuid IS NULL) RETURN -11;
	IF (@NpdsAgentGuid IS NULL) RETURN -12;

	DECLARE @restrictXML xml, @otherText nvarchar(max), @FgroupGuidKey UNIQUEIDENTIFIER, @AuditGuidRef UNIQUEIDENTIFIER;	
	EXEC dbo.ScribeServiceRestrictionXml @ServiceRGuid, @restrictXML output;
	SET @otherText = convert(nvarchar(max),@restrictXML);

	-- note constraint of 'AND [Priority] = 0 and IsPrincipal = 1'
	SELECT @FgroupGuidKey = FgroupGuidKey, @AuditGuidRef = AuditGuidRef 
		FROM dbo.NexusOtherText
		WHERE RecordGuidRef = @ServiceRGuid AND HasPriority = 0 and IsPrincipal = 1;

	EXEC @errorCode = dbo.ScribeOtherTextEdit @NpdsAgentGuid, @ServiceRGuid, @FgroupGuidKey, @AuditGuidRef,
		0, 1, 1, 1, @otherText;
 	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingLabelCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeSupportingLabelCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusSupportingLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusSupportingLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusSupportingLabel
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusSupportingLabel 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusSupportingLabel
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusSupportingLabel 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusSupportingLabel WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetSupportingLabelsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingLabelClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeSupportingLabelClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @SupportingLabel NVARCHAR(256) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, SupportingLabel 
		FROM dbo.NexusSupportingLabel WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @SupportingLabel;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeSupportingTagEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @SupportingLabel;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @SupportingLabel;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingLabelDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeSupportingLabelDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalSupportingLabel
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsPortalSupportingLabel 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeSupportingLabelCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingLabelEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeSupportingLabelEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@SupportingLabel NVARCHAR(256))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = getutcdate();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 22; -- PortalSupportingLabel
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalSupportingLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- do not allow replicate values (case insensitive) except when updating current
    DECLARE @SupportingLabelLower nvarchar(256);
	SET @SupportingLabelLower = LOWER(@SupportingLabel);
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalSupportingLabel AS P
		INNER JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = P.AuditGuidRef)
		WHERE (P.RecordGuidRef = @RecordGuidRef) AND (P.FgroupGuidKey <> @FgroupGuidKey)
		AND (A.IsDeleted = 0) AND (LOWER(P.SupportingLabel) = @SupportingLabelLower);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount > 0) OR (@errorCode <> 0) RETURN -13;	

	-- audit table first with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN	 
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			 HasIndex, HasPriority, IsMarked, IsPrincipal, 
			 CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalSupportingLabel 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsPortalSupportingLabel
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, SupportingLabel) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @SupportingLabel);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsPortalSupportingLabel SET SupportingLabel = @SupportingLabel
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeSupportingLabelCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingLabelReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScribeSupportingLabelReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 22; -- PortalSupportingLabel
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- slabRecords for SupportingLabel Records
  DECLARE slabRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusSupportingLabel 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN slabRecords;
  FETCH slabRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	  WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) RETURN @rowCount;
	IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH slabRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE slabRecords;
	DEALLOCATE slabRecords;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingTagCheck]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScribeSupportingTagCheck](
@RecordGuidRef UNIQUEIDENTIFIER,
@FgroupGuidKey UNIQUEIDENTIFIER)
AS
BEGIN

	SET NOCOUNT ON;	
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE(), @AuditGuidRef UNIQUEIDENTIFIER;
	DECLARE @PrincipalGuidKey UNIQUEIDENTIFIER, @InfosetTypeCodeRef SMALLINT, 
		@CurrentIsPrincipal BIT, @CurrentIsDeleted BIT, @CurrentPriority SMALLINT;
		
	SELECT @AuditGuidRef = AuditGuidRef, @CurrentPriority = HasPriority, 
		@CurrentIsPrincipal = IsPrincipal, @CurrentIsDeleted = IsDeleted
		FROM dbo.NexusSupportingTag 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
		 		
	SELECT @rowCount = COUNT(*) 
		FROM dbo.NexusSupportingTag 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0);	
	
	-- assure at least one principal record assumed to be the current record if not deleted
	IF (@rowCount = 0) AND (@CurrentIsDeleted = 0) BEGIN 
		
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;		 
	
	-- assure at least one principal record assumed to be the current record if deleted
	END ELSE IF (@rowCount = 0) AND (@CurrentIsDeleted = 1) BEGIN 
	
		-- find the record to keep as principal
		SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusSupportingTag
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusSupportingTag 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- set that record as principal
		UPDATE dbo.NpdsCoreAudit
			SET IsPrincipal = 1
			WHERE (AuditGuidKey = @AuditGuidRef);
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;		 

	-- assure no more than one principal record if more than one
	END ELSE IF (@rowCount > 1) BEGIN 
	
		-- find the record to keep as principal
		IF (@CurrentIsPrincipal = 1) BEGIN
			SET @PrincipalGuidKey = @FgroupGuidKey;			
		END ELSE BEGIN		
			SELECT TOP(1) @PrincipalGuidKey = FgroupGuidKey
				FROM dbo.NexusSupportingTag
				WHERE (RecordGuidRef = @RecordGuidRef) AND (IsPrincipal = 1) AND (IsDeleted = 0)
				ORDER BY HasPriority, FgroupGuidKey; 					
		END;
		SELECT @AuditGuidRef = AuditGuidRef, @InfosetTypeCodeRef = InfosetTypeCodeRef
			FROM dbo.NexusSupportingTag 
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @PrincipalGuidKey);
		-- reset the other records as non-principal
		UPDATE dbo.NpdsCoreAudit 
			SET IsPrincipal = 0
			WHERE (RecordGuidRef = @RecordGuidRef)  -- for same ResRep
			AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) -- for same InfosetType
			AND (AuditGuidKey <> @AuditGuidRef)   -- but not the principal record / audit record
			AND (IsPrincipal = 1); -- any record for which true must be reset to false
 	    SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;		 
	
	END; -- ELSE IF (@rowCount = 1) DO NOTHING 

  DECLARE @CountRecords INT = 0;
  SELECT @CountRecords = COUNT(*) FROM dbo.NexusSupportingTag WHERE (RecordGuidRef = @RecordGuidRef) AND (IsDeleted = 0);
 	UPDATE dbo.NpdsResrepRoot SET InfosetSupportingTagsCount = @CountRecords WHERE (RecordGuidKey = @RecordGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -31;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingTagClone]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeSupportingTagClone](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@OldRecordGuidRef UNIQUEIDENTIFIER = NULL,
@NewRecordGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@OldRecordGuidRef IS NULL) OR (@OldRecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@NewRecordGuidRef IS NULL) OR (@NewRecordGuidRef = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate datetime2 = getutcdate();

	DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasPriority SMALLINT; -- nonrandom nonunique index
	DECLARE @HasIndex SMALLINT; -- random unique index
	DECLARE @IsMarked BIT = 0;
	DECLARE @IsPrincipal BIT = 0; 
	DECLARE @SupportingTag NVARCHAR(256) = '';

	DECLARE clone CURSOR FOR 
		SELECT HasPriority, IsMarked, IsPrincipal, SupportingTag 
		FROM dbo.NexusSupportingTag WHERE (RecordGuidRef = @OldRecordGuidRef);
	OPEN clone;
	FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @SupportingTag;

	WHILE (@@fetch_status = 0) BEGIN
		EXEC @errorCode = dbo.ScribeSupportingTagEdit 
			@AgentGuidRef, NULL, @NewRecordGuidRef, NULL,
			@HasPriority, @IsMarked, @IsPrincipal, @SupportingTag;
		IF (@errorCode <> 0) RETURN @errorCode-1000;
		FETCH NEXT FROM clone INTO @HasPriority, @IsMarked, @IsPrincipal, @SupportingTag;
	END;

	CLOSE clone;
	DEALLOCATE clone;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingTagDelete]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ScribeSupportingTagDelete](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@IsRealDelete BIT = 0)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) RETURN -13;
  DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = getutcdate();
	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;

	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalSupportingTag
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -14;

 	IF (@IsRealDelete = 1) BEGIN
	    DELETE FROM dbo.NpdsPortalSupportingTag 
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	    DELETE FROM dbo.NpdsCoreAudit
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit SET IsDeleted = 1,
			DeletedOn = @utcDate, DeletedByAgentGuidRef = @AgentGuidRef
			WHERE (RecordGuidRef = @RecordGuidRef)
			AND (AuditGuidKey = @AuditGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	EXEC @errorCode = dbo.ScribeSupportingTagCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingTagEdit]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeSupportingTagEdit](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@FgroupGuidKey UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0,
@IsMarked BIT = 0,
@IsPrincipal BIT = 0,
@SupportingTag NVARCHAR(256))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
	IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = getutcdate();
	IF (@FgroupGuidKey IS NULL) OR (@FgroupGuidKey = @EmptyGuid) SET @FgroupGuidKey = newid();	
	DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
	DECLARE @HasIndex SMALLINT; -- random unique index

	DECLARE @AuditGuidRef UNIQUEIDENTIFIER = NULL;
	SELECT @AuditGuidRef = AuditGuidRef FROM dbo.NpdsPortalSupportingTag 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
	IF (@AuditGuidRef IS NULL) OR (@AuditGuidRef = @EmptyGuid) SET @AuditGuidRef = NEWID();	

	-- do not allow replicate values (case insensitive) except when updating current
    DECLARE @SupportingTagLower nvarchar(256);
	SET @SupportingTagLower = LOWER(@SupportingTag);
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalSupportingTag AS P
		INNER JOIN dbo.NpdsCoreAudit AS A ON (A.AuditGuidKey = P.AuditGuidRef)
		WHERE (P.RecordGuidRef = @RecordGuidRef) AND (P.FgroupGuidKey <> @FgroupGuidKey)
		AND (A.IsDeleted = 0) AND (LOWER(P.SupportingTag) = @SupportingTagLower);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount > 0) OR (@errorCode <> 0) RETURN -13;	

	-- audit table first with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsCoreAudit
		WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;
		 
 	IF (@rowCount = 0) BEGIN
	   EXECUTE dbo.CoreRandomIndexGenerate @InfosetTypeCodeRef, @RecordGuidRef, @HasIndex OUTPUT;
	   INSERT INTO dbo.NpdsCoreAudit
			(InfosetTypeCodeRef, RecordGuidRef, AuditGuidKey, 
			HasIndex, HasPriority, IsMarked, IsPrincipal, 
			CreatedOn, CreatedByAgentGuidRef, UpdatedOn, UpdatedByAgentGuidRef) 
		VALUES 
			(@InfosetTypeCodeRef, @RecordGuidRef, @AuditGuidRef, 
			 @HasIndex, @HasPriority, @IsMarked, @IsPrincipal, 
			 @utcDate, @AgentGuidRef, @utcDate, @AgentGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;
	END ELSE BEGIN
		UPDATE dbo.NpdsCoreAudit
			SET HasPriority = @HasPriority, IsMarked = @IsMarked, IsPrincipal = @IsPrincipal,
			UpdatedOn = @utcDate, UpdatedByAgentGuidRef = @AgentGuidRef
			WHERE (AuditGuidKey = @AuditGuidRef);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -23;
	END;

	-- bridge link table second with foreign key pointing to audit table with primary key
	SELECT @rowCount = Count(*) FROM dbo.NpdsPortalSupportingTag 
		WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 	SELECT @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;	

	IF (@rowCount = 0) BEGIN	 
 	   INSERT INTO dbo.NpdsPortalSupportingTag
			(FgroupGuidKey, RecordGuidRef, AuditGuidRef, SupportingTag) 
			VALUES 
			(@FgroupGuidKey, @RecordGuidRef, @AuditGuidRef, @SupportingTag);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;
	END ELSE BEGIN
	    UPDATE dbo.NpdsPortalSupportingTag SET SupportingTag = @SupportingTag
			WHERE (RecordGuidRef = @RecordGuidRef) AND (FgroupGuidKey = @FgroupGuidKey);
 		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -26;		 
	END;

	EXEC @errorCode = dbo.ScribeSupportingTagCheck @RecordGuidRef, @FgroupGuidKey;
	IF (@errorCode <> 0) RETURN -31;
		
	EXEC @errorCode = dbo.ScribeResrepTimestamp @RecordGuidRef;
	IF (@errorCode <> 0) RETURN -32;

	RETURN 0; -- no errors

END;
GO
/****** Object:  StoredProcedure [dbo].[ScribeSupportingTagReseq]    Script Date: 2/23/2023 4:50:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScribeSupportingTagReseq](
@AgentGuidRef UNIQUEIDENTIFIER = NULL,
@InfosetGuidRef UNIQUEIDENTIFIER = NULL,
@RecordGuidRef UNIQUEIDENTIFIER = NULL,
@HasPriority SMALLINT = 0 OUTPUT)

AS BEGIN

  SET NOCOUNT ON;
  DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
  IF (@AgentGuidRef IS NULL) OR (@AgentGuidRef = @EmptyGuid) RETURN -11;
  IF (@RecordGuidRef IS NULL) OR (@RecordGuidRef = @EmptyGuid) RETURN -12;
  DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = GETUTCDATE();
  -- TODO: make an input parameter so that this method can be generalized to all 10 infosubsets
  DECLARE @InfosetTypeCodeRef SMALLINT = 21; -- PortalSupportingTag
  DECLARE @FgroupGuidKey UNIQUEIDENTIFIER = NULL, @AuditGuidRef UNIQUEIDENTIFIER = NULL;
  SET @HasPriority = 0; -- impose 0 init until recode validation checks

  -- stagRecords for SupportingTag Records
  DECLARE stagRecords CURSOR FOR
    SELECT AuditGuidRef
    FROM dbo.NexusSupportingTag 
    WHERE (RecordGuidRef = @RecordGuidRef) 	-- AND (ManagedByAgentGuidRef = @AgentGuidRef)
	  AND (InfosetTypeCodeRef = @InfosetTypeCodeRef) AND (HasPriority < 250)
    ORDER BY UpdatedOn DESC;
    
  OPEN stagRecords;
  FETCH stagRecords INTO @AuditGuidRef; 	

  WHILE (@@FETCH_STATUS=0) BEGIN

    SET @HasPriority = @HasPriority + 1;
		
	  UPDATE dbo.NpdsCoreAudit SET HasPriority = @HasPriority
	    WHERE (RecordGuidRef = @RecordGuidRef) AND (AuditGuidKey = @AuditGuidRef);	
	  SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	  IF (@rowCount <> 1) RETURN @rowCount;
	  IF (@errorCode <> 0) RETURN @errorCode;
			
    FETCH stagRecords INTO @AuditGuidRef; 	
	
  END;

	CLOSE stagRecords;
	DEALLOCATE stagRecords;

	RETURN 0; -- no errors

END;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ResourceGuid cannot be unique index if nulls and replicates are allowed necessary for mapping different membership identities to same ResourceGuid linking to same ResourceRecord of ResourceType person or ResourceType agentrole' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NpdsCoreSessionAgent', @level2type=N'COLUMN',@level2name=N'AgentInfosetGuidRef'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ATTN: must modify storprocs aspnet_Users_CreateUser and aspnet_Users_DeleteUser to create/delete record here when record in table aspnet_Users is created/deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NpdsCoreSessionAgent', @level2type=N'COLUMN',@level2name=N'IdentityUserGuidRef'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'deprecated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NpdsCoreUserIdentitySystem', @level2type=N'COLUMN',@level2name=N'SystemIidKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'only used for entities of type Infoset when resource describes another independent resource' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NpdsResrepRoot', @level2type=N'COLUMN',@level2name=N'EntityOtherInfosetGuidRef'
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
         Begin Table = "NpdsCoreAudit"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 401
               Right = 517
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CoreAudit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CoreAudit'
GO
USE [master]
GO
ALTER DATABASE [PdpScribe10Cervin] SET  READ_WRITE 
GO
