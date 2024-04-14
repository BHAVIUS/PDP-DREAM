// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpSiteRoutes
{
  // for use with dotnet Razor ViewEngine and ViewEngineOptions
  // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengineoptions?view=aspnetcore-6.0

  // DREAM EndPoint (DEP) for QEBI and NPDS
  public const string DepEmptyPath = "";
  public const string DepQebiPath = "/QEBI";
  public const string DepNpdsPath = "/NPDS";
  public const string DepQebiFolder = "QEBI";
  public const string DepNpdsFolder = "NPDS";
  public const string DepNpdsViews = "/Views/NPDS/{1}/{0}";

  public const string DepPdpDream = "PDP-DREAM";
  public const string DepCoreWebLib = "CoreWebLib";
  public const string DepNexusWebLib = "NexusWebLib";
  public const string DepScribeWebLib = "ScribeWebLib";
  public const string DepAcmsWebLib = "AcmsWebLib";

  // QEBI paths - initial slash assures path from contentroot

  // non-identified AnonMode
  public const string DepAnonModeIndex = DepQebiPath + "/AnonMode/Index";
  public const string DepAnonModeAccessDenied = DepQebiPath + "/AnonMode/AccessDenied";
  public const string DepAnonModeConfirmEmail = DepQebiPath + "/AnonMode/ConfirmEmail";
  public const string DepAnonModeContactSite = DepQebiPath + "/AnonMode/ContactSite";
  public const string DepAnonModeDonateGift = DepQebiPath + "/AnonMode/DonateGift";
  public const string DepAnonModeLoginUser = DepQebiPath + "/AnonMode/LoginUser";
  public const string DepAnonModeRegisterUser = DepQebiPath + "/AnonMode/RegisterUser";
  public const string DepAnonModeResetEmail = DepQebiPath + "/AnonMode/ResetEmail";
  public const string DepAnonModeResetPassword1 = DepQebiPath + "/AnonMode/ResetPassword1";
  public const string DepAnonModeResetPassword2 = DepQebiPath + "/AnonMode/ResetPassword2";
  public const string DepAnonModeResetPassword3 = DepQebiPath + "/AnonMode/ResetPassword3";
  public const string DepAnonModeResetUsername = DepQebiPath + "/AnonMode/ResetUsername";
  public const string DepAnonModeResetUsername1 = DepQebiPath + "/AnonMode/ResetUsername1";
  public const string DepAnonModeResetUsername2 = DepQebiPath + "/AnonMode/ResetUsername2";
  public const string DepAnonModeResetUsername3 = DepQebiPath + "/AnonMode/ResetUsername3";

  // identified UserMode (vs AuthMode) to avoid confusion with AuthorMode
  public const string DepUserModeIndex = DepQebiPath + "/UserMode/Index";
  public const string DepUserModeChangeEmail = DepQebiPath + "/UserMode/ChangeEmail";
  public const string DepUserModeChangePassword = DepQebiPath + "/UserMode/ChangePassword";
  public const string DepUserModeChangeProfile = DepQebiPath + "/UserMode/ChangeProfile";
  public const string DepUserModeChangeUsername = DepQebiPath + "/UserMode/ChangeUsername";
  public const string DepUserModeCheckQebiUser = DepQebiPath + "/UserMode/CheckQebiUser";
  public const string DepUserModeContactSite = DepQebiPath + "/UserMode/ContactSite";
  public const string DepUserModeDisplayProfile = DepQebiPath + "/UserMode/DisplayProfile";
  public const string DepUserModeLogoutUser = DepQebiPath + "/UserMode/LogoutUser";

  public const string DepGuruModeIndex = DepQebiPath + "/GuruMode/Index";
  public const string DepGuruModeCheckQebiUser = DepQebiPath + "/GuruMode/CheckQebiUser";
  public const string DepGuruModeEditQebiRoles = DepQebiPath + "/GuruMode/EditQebiRoles";
  public const string DepGuruModeEditQebiUsers = DepQebiPath + "/GuruMode/EditQebiUsers";
  public const string DepGuruModeViewQebiRoles = DepQebiPath + "/GuruMode/ViewQebiRoles";
  public const string DepGuruModeViewQebiUsers = DepQebiPath + "/GuruMode/ViewQebiUsers";

  public const string DepAgentModeIndex = DepNpdsPath + "/AgentMode/Index";
  public const string DepAgentModeHelp = DepNpdsPath + "/AgentMode/Help";
  public const string DepAgentModeCheckQebiUser = DepNpdsPath + "/AgentMode/CheckQebiUser";
  public const string DepAgentModeCheckNpdsAgent = DepNpdsPath + "/AgentMode/CheckNpdsAgent";
  public const string DepAgentModeAddRoleAgent = DepNpdsPath + "/AgentMode/AddRoleAgent";
  public const string DepAgentModeAddRoleAuthor = DepNpdsPath + "/AgentMode/AddRoleAuthor";
  public const string DepAgentModeAddRoleReviewer = DepNpdsPath + "/AgentMode/AddRoleReviewer";
  public const string DepAgentModeAddRoleEditor = DepNpdsPath + "/AgentMode/AddRoleEditor";
  public const string DepAgentModeAddRoleAdmin = DepNpdsPath + "/AgentMode/AddRoleAdmin";

  public const string DepAuthorModeIndex = DepNpdsPath + "/AuthorMode/Index";
  public const string DepAuthorModeHelp = DepNpdsPath + "/AuthorMode/Help";
  public const string DepAuthorModeCheckQebiUser = DepNpdsPath + "/AuthorMode/CheckQebiUser";
  public const string DepAuthorModeCheckNpdsAgent = DepNpdsPath + "/AuthorMode/CheckNpdsAgent";

  public const string DepReviewerModeIndex = DepNpdsPath + "/ReviewerMode/Index";
  public const string DepReviewerModeHelp = DepNpdsPath + "/ReviewerMode/Help";
  public const string DepReviewerModeCheckQebiUser = DepNpdsPath + "/ReviewerMode/CheckQebiUser";
  public const string DepReviewerModeCheckNpdsAgent = DepNpdsPath + "/ReviewerMode/CheckNpdsAgent";

  public const string DepEditorModeIndex = DepNpdsPath + "/EditorMode/Index";
  public const string DepEditorModeHelp = DepNpdsPath + "/EditorMode/Help";
  public const string DepEditorModeCheckQebiUser = DepNpdsPath + "/EditorMode/CheckQebiUser";
  public const string DepEditorModeCheckNpdsAgent = DepNpdsPath + "/EditorMode/CheckNpdsAgent";

  public const string DepAdminModeIndex = DepNpdsPath + "/AdminMode/Index";
  public const string DepAdminModeHelp = DepNpdsPath + "/AdminMode/Help";
  public const string DepAdminModeCheckQebiUser = DepNpdsPath + "/AdminMode/CheckQebiUser";
  public const string DepAdminModeCheckNpdsAgent = DepNpdsPath + "/AdminMode/CheckNpdsAgent";
  public const string DepAdminModeEditQebiRoles = DepNpdsPath + "/AdminMode/EditQebiRoles";
  public const string DepAdminModeEditQebiUsers = DepNpdsPath + "/AdminMode/EditQebiUsers";
  public const string DepAdminModeViewQebiRoles = DepNpdsPath + "/AdminMode/ViewQebiRoles";
  public const string DepAdminModeViewQebiUsers = DepNpdsPath + "/AdminMode/ViewQebiUsers";

  public static string DepServiceResreps(this NpdsServiceType serviceType, string recordAccess)
  {
    return $"{DepNpdsPath}/{serviceType.ToString()}Service/{recordAccess}Resreps";
  }

  // NPDS paths - initial slash assures path from contentroot

  public const string DepPdpSiteIndex = DepNpdsPath + "/PdpSite/Index";
  public const string DepPdpSiteDesign = DepNpdsPath + "/PdpSite/Design";
  public const string DepPdpSiteDiristries = DepNpdsPath + "/PdpSite/Diristries";
  public const string DepPdpSiteEntities = DepNpdsPath + "/PdpSite/Entities";
  public const string DepPdpSiteInfo = DepNpdsPath + "/PdpSite/Info";
  public const string DepPdpSitePapers = DepNpdsPath + "/PdpSite/Papers";
  public const string DepPdpSitePrivacy = DepNpdsPath + "/PdpSite/Privacy";

  public const string DepDebugModeIndex = DepNpdsPath + "/DebugMode/Index";
  public const string DepDebugModeCodeConfig = DepNpdsPath + "/DebugMode/CodeConfig";
  public const string DepDebugModeDotnetErrors = DepNpdsPath + "/DebugMode/DotnetErrors";
  public const string DepDebugModeWarcProperties = DepNpdsPath + "/DebugMode/WarcProperties";
  public const string DepDebugModeRazorEndpoints = DepNpdsPath + "/DebugMode/RazorEndpoints";
  public const string DepDebugModeRazorFormats = DepNpdsPath + "/DebugMode/RazorFormats";
  public const string DepDebugModeRazorRoutes = DepNpdsPath + "/DebugMode/RazorRoutes";
  public const string DepDebugModeServerDefaults = DepNpdsPath + "/DebugMode/ServerDefaults";
  public const string DepDebugModeSiteSettings = DepNpdsPath + "/DebugMode/SiteSettings";

  // CoreService
  public const string DepCoreService = DepNpdsPath + "/CoreService";
  public const string DepCoreServiceIndex = $"{DepCoreService}/Index";
  public const string DepCoreServiceAnonResreps = $"{DepCoreService}/AnonResreps";
  public const string DepCoreServiceUserResreps = $"{DepCoreService}/UserResreps";
  public const string DepCoreServiceAgentResreps = $"{DepCoreService}/AgentResreps";
  public const string DepCoreServiceAuthorResreps = $"{DepCoreService}/AuthorResreps";
  public const string DepCoreServiceReviewerResreps = $"{DepCoreService}/ReviewerResreps";
  public const string DepCoreServiceEditorResreps = $"{DepCoreService}/EditorResreps";
  public const string DepCoreServiceAdminResreps = $"{DepCoreService}/AdminResreps";
  // CwlHome
  public const string DepCwlHome = DepNpdsPath + "/CwlHome";
  public const string DepCwlHomeIndex = $"{DepCwlHome}/Index";
  public const string DepCwlHomeDevTest = $"{DepCwlHome}/DevTest";

  // NexusService
  public const string DepNexusService = DepNpdsPath + "/NexusService";
  public const string DepNexusServiceIndex = $"{DepNexusService}/Index";
  public const string DepNexusServiceAnonResreps = $"{DepNexusService}/AnonResreps";
  public const string DepNexusServiceUserResreps = $"{DepNexusService}/UserResreps";
  public const string DepNexusServiceAgentResreps = $"{DepNexusService}/AgentResreps";
  public const string DepNexusServiceAuthorResreps = $"{DepNexusService}/AuthorResreps";
  public const string DepNexusServiceReviewerResreps = $"{DepNexusService}/ReviewerResreps";
  public const string DepNexusServiceEditorResreps = $"{DepNexusService}/EditorResreps";
  public const string DepNexusServiceAdminResreps = $"{DepNexusService}/AdminResreps";
  //NwlHome
  public const string DepNwlHome = DepNpdsPath + "/NwlHome";
  public const string DepNwlHomeIndex = $"{DepNwlHome}/Index";
  public const string DepNwlHomeDevTest = $"{DepNwlHome}/DevTest";

  // ScribeService
  public const string DepScribeService = DepNpdsPath + "/ScribeService";
  public const string DepScribeServiceIndex = $"{DepScribeService}/Index";
  public const string DepScribeServiceAgentResreps = $"{DepScribeService}/AgentResreps";
  public const string DepScribeServiceAuthorResreps = $"{DepScribeService}/AuthorResreps";
  public const string DepScribeServiceReviewerResreps = $"{DepScribeService}/ReviewerResreps";
  public const string DepScribeServiceEditorResreps = $"{DepScribeService}/EditorResreps";
  public const string DepScribeServiceAdminResreps = $"{DepScribeService}/AdminResreps";
  public const string DepScribeServiceAdminResrepsNexus = $"{DepScribeService}/AdminResrepsNexus";
  public const string DepScribeServiceAdminResrepsPortal = $"{DepScribeService}/AdminResrepsPortal";
  public const string DepScribeServiceAdminResrepsDoors = $"{DepScribeService}/AdminResrepsDoors";
  public const string DepScribeServiceExportNpdsQuads = $"{DepScribeService}/ExportNpdsQuads";
  public const string DepScribeServiceImportNpdsQuads = $"{DepScribeService}/ImportNpdsQuads";
  public const string DepScribeServiceAuthorAccess = $"{DepScribeService}/AuthorAccess";
  public const string DepScribeServiceReviewerAccess = $"{DepScribeService}/ReviewerAccess";
  public const string DepScribeServiceEditorAccess = $"{DepScribeService}/EditorAccess";
  public const string DepScribeServiceCccResrepServices = $"{DepScribeService}/CccResrepServices";
  public const string DepScribeServiceCccEntityLabels = $"{DepScribeService}/CccEntityLabels";
  public const string DepScribeServiceCccRestrictions = $"{DepScribeService}/CccRestrictions";
  public const string DepScribeServiceCccResrepStatus = $"{DepScribeService}/CccResrepStatus";
  //SwlHome
  public const string DepSwlHome = DepNpdsPath + "/SwlHome";
  public const string DepSwlHomeIndex = $"{DepSwlHome}/Index";
  public const string DepSwlHomeDevTest = $"{DepSwlHome}/DevTest";

  // AcmsService
  public const string DepAcmsService = DepNpdsPath + "/AcmsService";
  public const string DepAcmsServiceIndex = $"{DepAcmsService}/Index";
  public const string DepAcmsServiceAgentResreps = $"{DepAcmsService}/AgentResreps";
  public const string DepAcmsServiceAuthorResreps = $"{DepAcmsService}/AuthorResreps";
  public const string DepAcmsServiceReviewerResreps = $"{DepAcmsService}/ReviewerResreps";
  public const string DepAcmsServiceEditorResreps = $"{DepAcmsService}/EditorResreps";
  public const string DepAcmsServiceAdminResreps = $"{DepAcmsService}/AdminResreps";
  public const string DepAcmsServiceConcatDreamExamples = $"{DepAcmsService}/ConcatDreamExamples";
  public const string DepAcmsServiceConcatDreamPrinciples = $"{DepAcmsService}/ConcatDreamPrinciples";
  public const string DepAcmsServiceExportBcrList = $"{DepAcmsService}/ExportBcrList";
  public const string DepAcmsServiceImportBcrItem = $"{DepAcmsService}/ImportBcrItem";
  public const string DepAcmsServiceImportBcrList = $"{DepAcmsService}/ImportBcrList";
  public const string DepAcmsServiceImportBcrSui = $"{DepAcmsService}/ImportBcrSui";
  public const string DepAcmsServiceImportDreamResrep = $"{DepAcmsService}/ImportDreamResrep";
  public const string DepAcmsServiceMergeToSameResrep = $"{DepAcmsService}/MergeToSameResrep";
  public const string DepAcmsServiceMoveToDifferentDiristry = $"{DepAcmsService}/MoveToDifferentDiristry";
  public const string DepAcmsServiceSplitToDifferentResreps = $"{DepAcmsService}/SplitToDifferentResreps";
  public const string DepAcmsServiceSubmitBcrItem = $"{DepAcmsService}/SubmitBcrItem";
  //AwlHome
  public const string DepAwlHome = DepNpdsPath + "/AwlHome";
  public const string DepAwlHomeIndex = $"{DepAwlHome}/Index";
  public const string DepAwlHomeDevTest = $"{DepAwlHome}/DevTest";

  // ran = Route App Name for dep = DREAM EndPoint
  public const string DepRanRazorApi = "PdpDreamApi";
  public const string DepRanRazorPage = "PdpDreamPage";
  public const string DepRanRazorView = "PdpDreamView";
  // rao = Route App Order for dep = DREAM EndPoint
  public const int DepRaoRestApi = -3; // for REST apis (with Controller-based controllers)
  public const int DepRaoRazorPage = -2; // for Razor pages (with PageModel-based controllers)
  public const int DepRaoRazorView = -1; // for Razor views (with Controller-based controllers)
  // new for Net 6 with migration from convention routing to attribute routing
  // string constants for use in RestApi and WebApp controllers
  // NP is NamePrefix, NS is NameSuffix, TS is TemplateSuffix
  // TODO: check NP/NS deprecated/deleted
  // TS is TemplateSuffix
  public const string depTSrg = "{recordGuid}";
  public const string depTSrgil = "{recordGuid}/{isLimited?}";
  // for TKG core/nexus/scribe webapps
  public const string depTSappfind = "{searchFilter?}/{serviceTag?}/{entityType?}";
  // for restapis
  public const string depTSststet = "{serviceType}/{serviceTag}/{entityType?}";
  // keys for Routing ConstraintMap dictionaries
  public const string NexusSTC = "NexusSTC"; // Nexus ServiceType Constraint
  public const string PortalSTC = "PortalSTC"; // PORTAL ServiceType Constraint
  public const string DoorsSTC = "DoorsSTC"; // DOORS ServiceType Constraint
  public const string ScribeSTC = "ScribeSTC"; // Scribe ServiceType Constraint
  public const string NpdsPTC = "NpdsPTC"; // PrincipalTag Constraint
  public const string NpdsISC = "NpdsISC"; // InfosetStatus Constraint

} // end class

// end file