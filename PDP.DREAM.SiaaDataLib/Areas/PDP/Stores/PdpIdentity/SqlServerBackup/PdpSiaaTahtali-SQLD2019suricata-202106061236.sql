USE [master]
GO
CREATE DATABASE [PdpSiaaTahtali]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PdpSiaaTahtali', FILENAME = N'D:\Microsoft\SQLD2019\Data\PdpSiaaTahtali.mdf' , SIZE = 11136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 7%)
 LOG ON 
( NAME = N'PdpSiaaTahtaliLog', FILENAME = N'D:\Microsoft\SQLD2019\Logs\PdpSiaaTahtaliLog.ldf' , SIZE = 8192KB , MAXSIZE = 125952KB , FILEGROWTH = 7%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PdpSiaaTahtali] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PdpSiaaTahtali].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PdpSiaaTahtali] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ARITHABORT OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [PdpSiaaTahtali] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PdpSiaaTahtali] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PdpSiaaTahtali] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PdpSiaaTahtali] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [PdpSiaaTahtali] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PdpSiaaTahtali] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PdpSiaaTahtali] SET  MULTI_USER 
GO
ALTER DATABASE [PdpSiaaTahtali] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PdpSiaaTahtali] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PdpSiaaTahtali] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PdpSiaaTahtali] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PdpSiaaTahtali] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PdpSiaaTahtali] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PdpSiaaTahtali', N'ON'
GO
ALTER DATABASE [PdpSiaaTahtali] SET QUERY_STORE = OFF
GO
USE [PdpSiaaTahtali]
GO
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
CREATE USER [BUILTIN\Administrators] FOR LOGIN [BUILTIN\Administrators] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetApp](
	[AppGuidKey] [uniqueidentifier] NOT NULL,
	[AppName] [nvarchar](64) NOT NULL,
	[AppDescription] [nvarchar](128) NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetApps_AppGuidKey] PRIMARY KEY CLUSTERED 
(
	[AppGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[QebIdentityApp]
AS
SELECT AppGuidKey, AppName, AppDescription, ConcurrencyStamp
FROM dbo.SiaaNetApp
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetRole](
	[AppGuidRef] [uniqueidentifier] NOT NULL,
	[RoleGuidKey] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](64) NOT NULL,
	[RoleDescription] [nvarchar](128) NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetRoles_RoleGuidKey] PRIMARY KEY CLUSTERED 
(
	[RoleGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[QebIdentityAppRole]
AS
SELECT 
R.AppGuidRef, R.RoleGuidKey, R.RoleName, R.RoleDescription
FROM dbo.SiaaNetRole AS R 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetUser](
	[AppGuidRef] [uniqueidentifier] NOT NULL,
	[UserGuidKey] [uniqueidentifier] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[AgentGuidRef] [uniqueidentifier] NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
	[DateEmailConfirmed] [datetime2](7) NULL,
	[DateLastEdit] [datetime2](7) NULL,
	[DateLastLockout] [datetime2](7) NULL,
	[DateLastLogin] [datetime2](7) NULL,
	[DatePasswordChanged] [datetime2](7) NULL,
	[DateProfileChanged] [datetime2](7) NULL,
	[DateTokenExpired] [datetime2](7) NULL,
	[DateUserCreated] [datetime2](7) NULL,
	[DateUserNameChanged] [datetime2](7) NULL,
	[EmailAddress] [nvarchar](64) NOT NULL,
	[EmailAlternate] [nvarchar](64) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[FirstName] [nvarchar](64) NOT NULL,
	[LastName] [nvarchar](64) NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEndDateUtc] [datetime2](7) NULL,
	[Organization] [nvarchar](128) NOT NULL,
	[PasswordHash] [nvarchar](1024) NOT NULL,
	[PhoneNumber] [nvarchar](64) NOT NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityAnswer] [nvarchar](64) NOT NULL,
	[SecurityQuestion] [nvarchar](64) NOT NULL,
	[SecurityStamp] [nvarchar](64) NOT NULL,
	[SecurityToken] [nvarchar](64) NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserIsApproved] [bit] NOT NULL,
	[UserName] [nvarchar](64) NOT NULL,
	[UserNameDisplayed] [nvarchar](64) NOT NULL,
	[WebsiteAddress] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_SiaaNetUsers_UserGuidKey] PRIMARY KEY CLUSTERED 
(
	[UserGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SiaaNetUsers_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[QebIdentityAppUser]
AS
SELECT 
U.AppGuidRef, U.UserGuidKey, U.AgentGuidRef, U.AccessFailedCount, U.ConcurrencyStamp, 
U.DateEmailConfirmed, U.DateLastEdit, U.DateLastLockout, U.DateLastLogin, U.DatePasswordChanged, 
U.DateProfileChanged, U.DateTokenExpired, U.DateUserCreated, U.DateUserNameChanged, 
U.EmailAddress, U.EmailAlternate, U.EmailConfirmed, U.FirstName, U.LastName, 
U.LockoutEnabled, U.LockoutEnd, U.LockoutEndDateUtc, 
U.Organization, U.PasswordHash, U.PhoneNumber, U.PhoneNumberConfirmed, 
U.SecurityAnswer, U.SecurityQuestion, U.SecurityStamp, U.SecurityToken, U.TwoFactorEnabled, U.UserIsApproved, 
U.UserName, U.UserNameDisplayed, U.WebsiteAddress
FROM dbo.SiaaNetUser AS U
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetUserRoleLink](
	[LinkGuidKey] [uniqueidentifier] NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[RoleGuidRef] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetUserRoles_LinkGuidKey] PRIMARY KEY CLUSTERED 
(
	[LinkGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[QebIdentityAppUserRole]
AS
SELECT L.LinkGuidKey, 
U.AppGuidRef, A.AppName,
L.UserGuidRef, U.UserName,
L.RoleGuidRef, R.RoleName,
L.ConcurrencyStamp
FROM dbo.SiaaNetUserRoleLink AS L 
INNER JOIN dbo.SiaaNetRole AS R ON (R.RoleGuidKey = L.RoleGuidRef) 
INNER JOIN dbo.SiaaNetUser AS U ON (U.UserGuidKey = L.UserGuidRef)
INNER JOIN dbo.SiaaNetApp AS A ON (A.AppGuidKey = U.AppGuidRef)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[QebIdentityUserRoleLink]
AS
SELECT LinkGuidKey, UserGuidRef, RoleGuidRef, ConcurrencyStamp
FROM dbo.SiaaNetUserRoleLink
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsDaylog](
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[OccurredOn] [datetime2](7) NOT NULL,
	[TypeCode] [smallint] NOT NULL,
	[Hours] [tinyint] NOT NULL,
	[Minutes] [smallint] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PdpDaylog] PRIMARY KEY CLUSTERED 
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsDaylogType](
	[CodeKey] [smallint] NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[TypeDescription] [nvarchar](64) NULL,
 CONSTRAINT [PK_PdpDaylogTypes] PRIMARY KEY CLUSTERED 
(
	[CodeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[PdpAgentDaylog2021]
AS
SELECT U.UserGuidKey, U.UserName, 
   D.FgroupGuidKey, D.CreatedOn, D.UpdatedOn, D.OccurredOn, 
   T.CodeKey AS TypeCode, T.TypeName, T.TypeDescription,
   D.[Hours], D.[Minutes], D.[Text]
FROM dbo.AcmsDaylog AS D INNER 
JOIN dbo.AcmsDaylogType AS T ON D.TypeCode = T.CodeKey INNER 
JOIN dbo.SiaaNetUser AS U ON D.UserGuidRef = U.UserGuidKey 
WHERE CONVERT(date, D.OccurredOn) >= CONVERT(date, '20210101')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[QebIdentityAppUserRoleLink]
AS
SELECT LinkGuidKey, UserGuidRef, RoleGuidRef, ConcurrencyStamp
FROM dbo.SiaaNetUserRoleLink
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PdpAgentDaylog]
AS
SELECT U.UserGuidKey, U.UserName, 
   D.FgroupGuidKey, D.CreatedOn, D.UpdatedOn, D.OccurredOn, 
   T.CodeKey AS TypeCode, T.TypeName, T.TypeDescription,
   D.[Hours], D.[Minutes], D.[Text]
FROM dbo.AcmsDaylog AS D INNER 
JOIN dbo.AcmsDaylogType AS T ON D.TypeCode = T.CodeKey INNER 
JOIN dbo.SiaaNetUser AS U ON D.UserGuidRef = U.UserGuidKey
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PdpAgentDaylogType]
AS
SELECT
   T.CodeKey, T.TypeName, T.TypeDescription
FROM dbo.AcmsDaylogType AS T
WHERE T.CodeKey <> 0

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[QebIdentityRole]
AS
SELECT RoleGuidKey, RoleName, RoleDescription, ConcurrencyStamp
FROM dbo.SiaaNetRole
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   VIEW [dbo].[QebIdentityUserRole]
AS
SELECT LinkGuidKey, 
L.RoleGuidRef, R.RoleName,
L.UserGuidRef, U.UserName,
L.ConcurrencyStamp
FROM dbo.SiaaNetUserRoleLink AS L 
INNER JOIN dbo.SiaaNetRole AS R ON R.RoleGuidKey = L.RoleGuidRef 
INNER JOIN dbo.SiaaNetUser AS U ON U.UserGuidKey = L.UserGuidRef
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[QebIdentityUser]
AS
SELECT UserGuidKey, AgentGuidRef, AccessFailedCount, ConcurrencyStamp, 
  DateEmailConfirmed, DateLastEdit, DateLastLockout, DateLastLogin, DatePasswordChanged, 
  DateProfileChanged, DateTokenExpired, DateUserCreated, DateUserNameChanged, 
  EmailAddress, EmailAlternate, EmailConfirmed, FirstName, LastName, 
  LockoutEnabled, LockoutEnd, LockoutEndDateUtc, 
  Organization, PasswordHash, PhoneNumber, PhoneNumberConfirmed, 
  SecurityAnswer, SecurityQuestion, SecurityStamp, SecurityToken, TwoFactorEnabled, UserIsApproved, 
  UserName, UserNameDisplayed, WebsiteAddress
FROM dbo.SiaaNetUser
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsDocument](
	[FgroupGuidKey] [uniqueidentifier] NOT NULL,
	[RecordGuidRef] [uniqueidentifier] NULL,
	[CitationKey] [nvarchar](64) NOT NULL,
	[FilePath] [nvarchar](512) NOT NULL,
	[FileType] [nvarchar](256) NOT NULL,
	[FileName] [nvarchar](128) NULL,
	[DisplayText] [nvarchar](128) NULL,
	[AccessMetadataCount] [int] NOT NULL,
	[AccessFileCount] [int] NOT NULL,
	[ConversionRate] [real] NOT NULL,
 CONSTRAINT [PK_PdpDoc_CitationKey] PRIMARY KEY CLUSTERED 
(
	[CitationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsForum](
	[PostGuidKey] [uniqueidentifier] NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[PostGuidRef] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[Thread] [nvarchar](256) NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PdpForum] PRIMARY KEY CLUSTERED 
(
	[PostGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsGallery](
	[PostGuidKey] [uniqueidentifier] NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[PostGuidRef] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[Thread] [nvarchar](256) NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PdpGallery] PRIMARY KEY CLUSTERED 
(
	[PostGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsTask](
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[TaskGuidKey] [uniqueidentifier] NOT NULL,
	[StartOn] [datetime2](7) NOT NULL,
	[StartTimezone] [ntext] NULL,
	[FinishOn] [datetime2](7) NOT NULL,
	[FinishTimezone] [ntext] NULL,
	[IsAllDay] [bit] NOT NULL,
	[Title] [ntext] NOT NULL,
	[Description] [ntext] NULL,
	[RecurrenceGuidRef] [uniqueidentifier] NULL,
	[RecurrenceRule] [ntext] NULL,
	[RecurrenceException] [ntext] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcmsWeblog](
	[PostGuidKey] [uniqueidentifier] NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PdpWeblog] PRIMARY KEY CLUSTERED 
(
	[PostGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetRoleClaim](
	[ClaimGuidKey] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](128) NOT NULL,
	[ClaimValue] [nvarchar](1048) NOT NULL,
	[RoleGuidRef] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetRoleClaims_ClaimGuidKey] PRIMARY KEY CLUSTERED 
(
	[ClaimGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetUserClaim](
	[ClaimGuidKey] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](128) NOT NULL,
	[ClaimValue] [nvarchar](1024) NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[ClaimGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetUserLogin](
	[LoginGuidKey] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](128) NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiaaNetUserToken](
	[TokenGuidKey] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](1028) NOT NULL,
	[UserGuidRef] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
 CONSTRAINT [PK_SiaaNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[TokenGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PdpDocument] ON [dbo].[AcmsDocument]
(
	[FgroupGuidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SiaaNetRoleClaims_RoleGuidRef] ON [dbo].[SiaaNetRoleClaim]
(
	[RoleGuidRef] ASC,
	[ClaimType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SiaaNetUsers_AgentGuidRef] ON [dbo].[SiaaNetUser]
(
	[AgentGuidRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_SiaaNetUsers_EmailAddress] ON [dbo].[SiaaNetUser]
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserGuid_ClaimType] ON [dbo].[SiaaNetUserClaim]
(
	[UserGuidRef] ASC,
	[ClaimType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SiaaNetUserLogins] ON [dbo].[SiaaNetUserLogin]
(
	[UserGuidRef] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SiaaNetUserRoles] ON [dbo].[SiaaNetUserRoleLink]
(
	[UserGuidRef] ASC,
	[RoleGuidRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SiaaNetUserTokens_UserLoginName] ON [dbo].[SiaaNetUserToken]
(
	[UserGuidRef] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_DateTime]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_OccurredOn]  DEFAULT (getutcdate()) FOR [OccurredOn]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_Type]  DEFAULT ((0)) FOR [TypeCode]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_Hours]  DEFAULT ((0)) FOR [Hours]
GO
ALTER TABLE [dbo].[AcmsDaylog] ADD  CONSTRAINT [DF_PdpDaylog_Minutes]  DEFAULT ((0)) FOR [Minutes]
GO
ALTER TABLE [dbo].[AcmsDocument] ADD  CONSTRAINT [DF_PdpDocument_FgroupGuidKey]  DEFAULT (newid()) FOR [FgroupGuidKey]
GO
ALTER TABLE [dbo].[AcmsDocument] ADD  CONSTRAINT [DF_PdpDocument_FilePath]  DEFAULT ('D:\Inetpub\PDP\BJedoc\') FOR [FilePath]
GO
ALTER TABLE [dbo].[AcmsDocument] ADD  CONSTRAINT [DF_PdpDocument_FileType]  DEFAULT ('application/pdf') FOR [FileType]
GO
ALTER TABLE [dbo].[AcmsDocument] ADD  CONSTRAINT [DF_PdpDocument_AccessCount]  DEFAULT ((0)) FOR [AccessMetadataCount]
GO
ALTER TABLE [dbo].[AcmsDocument] ADD  CONSTRAINT [DF_PdpDocument_AccessFileCount]  DEFAULT ((0)) FOR [AccessFileCount]
GO
ALTER TABLE [dbo].[AcmsForum] ADD  CONSTRAINT [DF_PdpForum_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AcmsForum] ADD  CONSTRAINT [DF_PdpForum_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[AcmsGallery] ADD  CONSTRAINT [DF_PdpGallery_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AcmsGallery] ADD  CONSTRAINT [DF_PdpGallery_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[AcmsTask] ADD  CONSTRAINT [DF_PdpTask_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AcmsTask] ADD  CONSTRAINT [DF_PdpTask_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[AcmsTask] ADD  CONSTRAINT [DF_PdpTask_TaskGuidKey]  DEFAULT (newid()) FOR [TaskGuidKey]
GO
ALTER TABLE [dbo].[AcmsTask] ADD  CONSTRAINT [DF_PdpTask_IsAllDay]  DEFAULT ((0)) FOR [IsAllDay]
GO
ALTER TABLE [dbo].[AcmsWeblog] ADD  CONSTRAINT [DF_PdpWeblog_PostGuidKey]  DEFAULT (newid()) FOR [PostGuidKey]
GO
ALTER TABLE [dbo].[AcmsWeblog] ADD  CONSTRAINT [DF_PdpWeblog_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AcmsWeblog] ADD  CONSTRAINT [DF_PdpWeblog_UpdatedOn]  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[SiaaNetApp] ADD  CONSTRAINT [DF_SiaaNetApps_AppGuidKey]  DEFAULT (newid()) FOR [AppGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetApp] ADD  CONSTRAINT [DF_SiaaNetApp_AppName]  DEFAULT ('') FOR [AppName]
GO
ALTER TABLE [dbo].[SiaaNetApp] ADD  CONSTRAINT [DF_SiaaNetApp_AppDescription]  DEFAULT ('') FOR [AppDescription]
GO
ALTER TABLE [dbo].[SiaaNetRole] ADD  CONSTRAINT [DF_SiaaNetRoles_RoleGuidKey]  DEFAULT (newid()) FOR [RoleGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetRole] ADD  CONSTRAINT [DF_SiaaNetRole_RoleName]  DEFAULT ('') FOR [RoleName]
GO
ALTER TABLE [dbo].[SiaaNetRole] ADD  CONSTRAINT [DF_SiaaNetRole_RoleDescription]  DEFAULT ('') FOR [RoleDescription]
GO
ALTER TABLE [dbo].[SiaaNetRoleClaim] ADD  CONSTRAINT [DF_SiaaNetRoleClaims_ClaimGuidKey]  DEFAULT (newid()) FOR [ClaimGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetRoleClaim] ADD  CONSTRAINT [DF_SiaaNetRoleClaim_ClaimType]  DEFAULT ('') FOR [ClaimType]
GO
ALTER TABLE [dbo].[SiaaNetRoleClaim] ADD  CONSTRAINT [DF_SiaaNetRoleClaim_ClaimValue]  DEFAULT ('') FOR [ClaimValue]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_UserGuidKey]  DEFAULT (newid()) FOR [UserGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_AccessFailedCount]  DEFAULT ((0)) FOR [AccessFailedCount]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_EmailAddress]  DEFAULT ('') FOR [EmailAddress]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_EmailAlternate]  DEFAULT ('') FOR [EmailAlternate]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_EmailConfirmed]  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_FirstName]  DEFAULT ('') FOR [FirstName]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_LastName]  DEFAULT ('') FOR [LastName]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_LockoutEnabled]  DEFAULT ((0)) FOR [LockoutEnabled]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_Organization]  DEFAULT ('') FOR [Organization]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_PasswordHash]  DEFAULT ('') FOR [PasswordHash]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_PhoneNumberConfirmed]  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_SecurityAnswer]  DEFAULT ('') FOR [SecurityAnswer]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_SecurityQuestion]  DEFAULT ('') FOR [SecurityQuestion]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_SecurityStamp]  DEFAULT ('') FOR [SecurityStamp]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_SecurityToken]  DEFAULT ('') FOR [SecurityToken]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_TwoFactorEnabled]  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUsers_UserIsApproved]  DEFAULT ((0)) FOR [UserIsApproved]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_UserNameDisplayed]  DEFAULT ('') FOR [UserNameDisplayed]
GO
ALTER TABLE [dbo].[SiaaNetUser] ADD  CONSTRAINT [DF_SiaaNetUser_WebsiteAddress]  DEFAULT ('') FOR [WebsiteAddress]
GO
ALTER TABLE [dbo].[SiaaNetUserClaim] ADD  CONSTRAINT [DF_SiaaNetUserClaims_ClaimGuidKey]  DEFAULT (newid()) FOR [ClaimGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetUserClaim] ADD  CONSTRAINT [DF_SiaaNetUserClaim_ClaimType]  DEFAULT ('') FOR [ClaimType]
GO
ALTER TABLE [dbo].[SiaaNetUserClaim] ADD  CONSTRAINT [DF_SiaaNetUserClaim_ClaimValue]  DEFAULT ('') FOR [ClaimValue]
GO
ALTER TABLE [dbo].[SiaaNetUserLogin] ADD  CONSTRAINT [DF_SiaaNetUserLogins_LoginGuidKey]  DEFAULT (newid()) FOR [LoginGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetUserLogin] ADD  CONSTRAINT [DF_SiaaNetUserLogin_LoginProvider]  DEFAULT ('') FOR [LoginProvider]
GO
ALTER TABLE [dbo].[SiaaNetUserLogin] ADD  CONSTRAINT [DF_SiaaNetUserLogin_ProviderKey]  DEFAULT ('') FOR [ProviderKey]
GO
ALTER TABLE [dbo].[SiaaNetUserLogin] ADD  CONSTRAINT [DF_SiaaNetUserLogin_ProviderDisplayName]  DEFAULT ('') FOR [ProviderDisplayName]
GO
ALTER TABLE [dbo].[SiaaNetUserRoleLink] ADD  CONSTRAINT [DF_SiaaNetUserRoles_LinkGuidKey]  DEFAULT (newid()) FOR [LinkGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetUserToken] ADD  CONSTRAINT [DF_SiaaNetUserTokens_TokenGuidKey]  DEFAULT (newid()) FOR [TokenGuidKey]
GO
ALTER TABLE [dbo].[SiaaNetUserToken] ADD  CONSTRAINT [DF_SiaaNetUserToken_LoginProvider]  DEFAULT ('') FOR [LoginProvider]
GO
ALTER TABLE [dbo].[SiaaNetUserToken] ADD  CONSTRAINT [DF_SiaaNetUserToken_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[SiaaNetUserToken] ADD  CONSTRAINT [DF_SiaaNetUserToken_Value]  DEFAULT ('') FOR [Value]
GO
ALTER TABLE [dbo].[AcmsDaylog]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpDaylog_AspNetUsers_UserId] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[AcmsDaylog] CHECK CONSTRAINT [FK_PdpDaylog_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AcmsDaylog]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpDaylog_PdpDaylogTypes_CodeKey] FOREIGN KEY([TypeCode])
REFERENCES [dbo].[AcmsDaylogType] ([CodeKey])
GO
ALTER TABLE [dbo].[AcmsDaylog] CHECK CONSTRAINT [FK_PdpDaylog_PdpDaylogTypes_CodeKey]
GO
ALTER TABLE [dbo].[AcmsForum]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpForum_AspNetUsers_UserId] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[AcmsForum] CHECK CONSTRAINT [FK_PdpForum_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AcmsGallery]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpGallery_AspNetUsers_UserId] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[AcmsGallery] CHECK CONSTRAINT [FK_PdpGallery_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AcmsTask]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpTask_PdpTask] FOREIGN KEY([RecurrenceGuidRef])
REFERENCES [dbo].[AcmsTask] ([TaskGuidKey])
GO
ALTER TABLE [dbo].[AcmsTask] CHECK CONSTRAINT [FK_PdpTask_PdpTask]
GO
ALTER TABLE [dbo].[AcmsTask]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpTaskUserGuid_AspNetUserId] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[AcmsTask] CHECK CONSTRAINT [FK_PdpTaskUserGuid_AspNetUserId]
GO
ALTER TABLE [dbo].[AcmsWeblog]  WITH NOCHECK ADD  CONSTRAINT [FK_PdpWeblog_AspNetUsers_UserId] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[AcmsWeblog] CHECK CONSTRAINT [FK_PdpWeblog_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[SiaaNetRole]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetRoles_SiaaNetApps] FOREIGN KEY([AppGuidRef])
REFERENCES [dbo].[SiaaNetApp] ([AppGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetRole] CHECK CONSTRAINT [FK_SiaaNetRoles_SiaaNetApps]
GO
ALTER TABLE [dbo].[SiaaNetRoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetRoleClaims_SiaaNetRoles] FOREIGN KEY([RoleGuidRef])
REFERENCES [dbo].[SiaaNetRole] ([RoleGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetRoleClaim] CHECK CONSTRAINT [FK_SiaaNetRoleClaims_SiaaNetRoles]
GO
ALTER TABLE [dbo].[SiaaNetUserClaim]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetUserClaims_SiaaNetUsers] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetUserClaim] CHECK CONSTRAINT [FK_SiaaNetUserClaims_SiaaNetUsers]
GO
ALTER TABLE [dbo].[SiaaNetUserLogin]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetUserLogins_SiaaNetUsers] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetUserLogin] CHECK CONSTRAINT [FK_SiaaNetUserLogins_SiaaNetUsers]
GO
ALTER TABLE [dbo].[SiaaNetUserRoleLink]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetUserRoles_SiaaNetRoles] FOREIGN KEY([RoleGuidRef])
REFERENCES [dbo].[SiaaNetRole] ([RoleGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetUserRoleLink] CHECK CONSTRAINT [FK_SiaaNetUserRoles_SiaaNetRoles]
GO
ALTER TABLE [dbo].[SiaaNetUserRoleLink]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetUserRoles_SiaaNetUsers] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetUserRoleLink] CHECK CONSTRAINT [FK_SiaaNetUserRoles_SiaaNetUsers]
GO
ALTER TABLE [dbo].[SiaaNetUserToken]  WITH CHECK ADD  CONSTRAINT [FK_SiaaNetUserTokens_SiaaNetUsers] FOREIGN KEY([UserGuidRef])
REFERENCES [dbo].[SiaaNetUser] ([UserGuidKey])
GO
ALTER TABLE [dbo].[SiaaNetUserToken] CHECK CONSTRAINT [FK_SiaaNetUserTokens_SiaaNetUsers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PdpAgentDaylogDelete](
@FgroupGuidKey uniqueidentifier = null,
@UserGuidRef uniqueidentifier = null)

AS BEGIN

	SET NOCOUNT ON;
  	DECLARE @rowCount int = 0, @errorCode int = 0;
	IF (@UserGuidRef IS NULL) RETURN -12;
	IF (@FgroupGuidKey IS NULL) RETURN -13;

    DELETE FROM dbo.AcmsDaylog 
			WHERE (UserGuidRef = @UserGuidRef)
			AND (FgroupGuidKey = @FgroupGuidKey);

	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -14;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PdpAgentDaylogEdit](
@FgroupGuidKey uniqueidentifier = null,
@UserGuidRef uniqueidentifier = null,
@OccurredOn datetime2,
@TypeCode smallint = 0,
@Hours tinyint = 0,
@Minutes smallint = 0,
@Text nvarchar(MAX))

AS BEGIN

	SET NOCOUNT ON;
  	DECLARE @rowCount int = 0, @errorCode int = 0, @utcDate datetime2 = getutcdate();
	IF (@UserGuidRef IS NULL) RETURN -12;
	IF (@FgroupGuidKey IS NULL) SELECT @FgroupGuidKey = newid();

    UPDATE dbo.AcmsDaylog
	    SET OccurredOn = @OccurredOn, TypeCode = @TypeCode, [Hours] = @Hours, [Minutes] = @Minutes, [Text] = @Text, UpdatedOn = @utcDate
		WHERE (FgroupGuidKey = @FgroupGuidKey) AND (UserGuidRef = @UserGuidRef);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);

	IF (@rowCount = 0) OR (@errorCode <> 0) BEGIN	 
		INSERT INTO dbo.AcmsDaylog
			(FgroupGuidKey, UserGuidRef, OccurredOn, TypeCode, [Hours], [Minutes], [Text], CreatedOn, UpdatedOn) 
		VALUES 
			(@FgroupGuidKey, @UserGuidRef, @OccurredOn, @TypeCode, @Hours, @Minutes, @Text, @utcDate, @utcDate);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -14;
	END;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PdpDocumentAccessFile]
(@CitationKey NVARCHAR(64) = '',
@FilePath NVARCHAR(512) = '' OUTPUT,
@FileType NVARCHAR(256) = '' OUTPUT,
@FileName NVARCHAR(128) = '' OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	IF (@CitationKey IS NULL) OR (@CitationKey = '') RETURN -10;
	DECLARE @AccessFileCount INT = -11;

	SELECT @FilePath = FilePath, @FileType = FileType, @FileName = [FileName],
		@AccessFileCount = AccessFileCount + 1
		FROM dbo.AcmsDocument WHERE CitationKey = @CitationKey;

	UPDATE dbo.AcmsDocument SET AccessFileCount = @AccessFileCount,
	 ConversionRate = (CAST(@AccessFileCount as real) / CAST(AccessMetadataCount as real))
		WHERE CitationKey = @CitationKey;

	RETURN @AccessFileCount;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PdpDocumentAccessMetadata]
(@CitationKey NVARCHAR(64) = '',
@FilePath NVARCHAR(512) = '' OUTPUT,
@FileType NVARCHAR(256) = '' OUTPUT,
@FileName NVARCHAR(128) = '' OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	IF (@CitationKey IS NULL) OR (@CitationKey = '') RETURN -10;
	DECLARE @AccessMetadataCount INT = -11;

	SELECT @FilePath = FilePath, @FileType = FileType, @FileName = [FileName],
		@AccessMetadataCount = AccessMetadataCount + 1
		FROM dbo.AcmsDocument WHERE CitationKey = @CitationKey;

	UPDATE dbo.AcmsDocument SET AccessMetadataCount = @AccessMetadataCount, 
      ConversionRate = (CAST(AccessFileCount as real) / CAST(@AccessMetadataCount as real))
	  WHERE CitationKey = @CitationKey;

	RETURN @AccessMetadataCount;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QebIdentityAppDelete](
@AppGuidKey UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF (@AppGuidKey IS NULL) OR (@AppGuidKey = @EmptyGuid) RETURN -11;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();

    DELETE FROM dbo.SiaaNetApp
		WHERE (AppGuidKey = @AppGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QebIdentityAppEdit](
@AppGuidKey UNIQUEIDENTIFIER = NULL,
@AppName NVARCHAR(64) = NULL,
@AppDescription NVARCHAR(128) = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF (@AppGuidKey IS NULL) OR (@AppGuidKey = @EmptyGuid) SELECT @AppGuidKey = NEWID();

    UPDATE dbo.SiaaNetApp
	    SET AppName = @AppName, AppDescription = @AppDescription
		WHERE (AppGuidKey = @AppGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);

	IF (@rowCount = 0) OR (@errorCode <> 0) BEGIN	 
		INSERT INTO dbo.SiaaNetApp
			(AppGuidKey, AppName, AppDescription) 
		VALUES 
			(@AppGuidKey, @AppName, @AppDescription);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	END;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QebIdentityAppRoleDelete](
@AppGuidRef UNIQUEIDENTIFIER = NULL,
@RoleGuidKey UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF (@AppGuidRef IS NULL) OR (@AppGuidRef = @EmptyGuid) RETURN -11;
	IF (@RoleGuidKey IS NULL) OR (@RoleGuidKey = @EmptyGuid) RETURN -12;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();

	-- first delete role users in link table
    DELETE FROM dbo.SiaaNetUserRoleLink
		WHERE (RoleGuidRef = @RoleGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;

	-- then delete role	 
	DELETE FROM dbo.SiaaNetRole
		WHERE (RoleGuidKey = @RoleGuidKey) AND (AppGuidRef = @AppGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -22;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[QebIdentityAppRoleEdit](
@AppGuidRef UNIQUEIDENTIFIER = NULL,
@RoleGuidKey UNIQUEIDENTIFIER = NULL,
@RoleName NVARCHAR(64) = NULL,
@RoleDescription NVARCHAR(128) = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF (@AppGuidRef IS NULL) OR (@AppGuidRef = @EmptyGuid) RETURN -11;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF (@RoleGuidKey IS NULL) OR (@RoleGuidKey = @EmptyGuid) SELECT @RoleGuidKey = NEWID();

    UPDATE dbo.SiaaNetRole
	    SET AppGuidRef = @AppGuidRef, RoleName = @RoleName, RoleDescription = @RoleDescription
		WHERE (RoleGuidKey = @RoleGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);

	IF (@rowCount = 0) OR (@errorCode <> 0) BEGIN	 
		INSERT INTO dbo.SiaaNetRole
			(AppGuidRef, RoleGuidKey, RoleName, RoleDescription) 
		VALUES 
			(@AppGuidRef, @RoleGuidKey, @RoleName, @RoleDescription);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	END;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[QebIdentityAppUserDelete](
@AppGuidRef UNIQUEIDENTIFIER = NULL,
@UserGuidKey UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF (@AppGuidRef IS NULL) OR (@AppGuidRef = @EmptyGuid) RETURN -11;
	IF (@UserGuidKey IS NULL) OR (@UserGuidKey = @EmptyGuid) RETURN -12;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();

	-- first delete user roles in link table and other user linked tables
    DELETE FROM dbo.SiaaNetUserRoleLink
		WHERE (UserGuidRef = @UserGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -21;

    DELETE FROM dbo.SiaaNetUserClaim
		WHERE (UserGuidRef = @UserGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -22;

    DELETE FROM dbo.SiaaNetUserLogin
		WHERE (UserGuidRef = @UserGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -23;

    DELETE FROM dbo.SiaaNetUserToken
		WHERE (UserGuidRef = @UserGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@errorCode <> 0) RETURN -24;

	-- then delete user	 
	DELETE FROM dbo.SiaaNetUser
		WHERE (UserGuidKey = @UserGuidKey) AND (AppGuidRef = @AppGuidRef);
	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
	IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -25;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[QebIdentityAppUserEdit](
@AppGuidRef UNIQUEIDENTIFIER = NULL,
@UserGuidKey UNIQUEIDENTIFIER = NULL,
@UserNameDisplayed NVARCHAR(64) = NULL,
@FirstName NVARCHAR(64) = NULL,
@LastName NVARCHAR(64) = NULL,
@UserName NVARCHAR(64) = NULL,
@EmailAddress NVARCHAR(64))

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
	IF (@AppGuidRef IS NULL) OR (@AppGuidRef = @EmptyGuid) RETURN -11;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF (@UserGuidKey IS NULL) OR (@UserGuidKey = @EmptyGuid) SELECT @UserGuidKey = NEWID();

    UPDATE dbo.SiaaNetUser
	    SET AppGuidRef = @AppGuidRef, UserNameDisplayed = @UserNameDisplayed,
		FirstName = @FirstName, LastName = @LastName, UserName = @UserName, EmailAddress = @EmailAddress
		WHERE (UserGuidKey = @UserGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);

	IF (@rowCount = 0) OR (@errorCode <> 0) BEGIN	 
		INSERT INTO dbo.SiaaNetUser
			(AppGuidRef, UserGuidKey, UserNameDisplayed, 
			FirstName, LastName, UserName, EmailAddress) 
		VALUES 
			(@AppGuidRef, @UserGuidKey, @UserNameDisplayed, 
			@FirstName, @LastName, @UserName, @EmailAddress);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	END;

    RETURN 0; -- no errors

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QebIdentityAppUserRoleLinkEdit](
@LinkGuidKey UNIQUEIDENTIFIER = NULL,
@UserGuidRef UNIQUEIDENTIFIER = NULL,
@RoleGuidRef UNIQUEIDENTIFIER = NULL)

AS BEGIN

	SET NOCOUNT ON;
	DECLARE @EmptyGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000';
	IF (@UserGuidRef IS NULL) OR (@UserGuidRef = @EmptyGuid) RETURN -11;
	IF (@RoleGuidRef IS NULL) OR (@RoleGuidRef = @EmptyGuid) RETURN -12;
  	DECLARE @rowCount INT = 0, @errorCode INT = 0, @utcDate DATETIME2 = GETUTCDATE();
	IF (@LinkGuidKey IS NULL) OR (@LinkGuidKey = @EmptyGuid) SELECT @LinkGuidKey = NEWID();

    UPDATE dbo.SiaaNetUserRoleLink
	    SET UserGuidRef = @UserGuidRef, RoleGuidRef = @RoleGuidRef
		WHERE (LinkGuidKey = @LinkGuidKey);
 	SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);

	IF (@rowCount = 0) OR (@errorCode <> 0) BEGIN	 
		INSERT INTO dbo.SiaaNetUserRoleLink
			(LinkGuidKey, UserGuidRef, RoleGuidRef) 
		VALUES 
			(@LinkGuidKey, @UserGuidRef, @RoleGuidRef);
		SELECT @rowCount = ISNULL(@@ROWCOUNT,0), @errorCode = ISNULL(@@ERROR,0);
		IF (@rowCount <> 1) OR (@errorCode <> 0) RETURN -21;
	END;

    RETURN 0; -- no errors

END
GO
USE [master]
GO
ALTER DATABASE [PdpSiaaTahtali] SET  READ_WRITE 
GO
