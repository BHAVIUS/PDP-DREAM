// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpSiteRoutes
{
  // for use with dotnet Razor ViewEngine and ViewEngineOptions
  // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengineoptions?view=aspnetcore-6.0

  // DREAM EndPoint (DEP) for NPDS
  public const string DepEmptyPath = "";
  public const string DepNpdsPath = "/NPDS";
  public const string DepNpdsFolder = "NPDS";
  public const string DepNpdsViews = "/Views/NPDS/{1}/{0}";

  public const string DepPdpDream = "PDP-DREAM";
  public const string DepCoreWebLib = "CoreWebLib";
  public const string DepNexusWebLib = "NexusWebLib";
  public const string DepScribeWebLib = "ScribeWebLib";
  public const string DepAcmsWebLib = "AcmsWebLib";

  // NPDS paths - initial slash assures path from contentroot
  public const string DepQebIdentRequired = DepNpdsPath + "/AnonMode/AccessDenied";
  public const string DepQebIdentLogin = DepNpdsPath + "/AnonMode/LoginUser";
  public const string DepQebIdentLogout = DepNpdsPath + "/AuthMode/LogoutUser";
  public const string DepPdpSiteErrors = DepNpdsPath + "/CwlHome/DotnetErrors";
  public const string DepPdpSiteRoutes = DepNpdsPath + "/CwlHome/RazorRoutes";

  public const string DepPdpSiteIndex = DepNpdsPath + "/PdpSite/Index";
  public const string DepPdpSiteDesign = DepNpdsPath + "/PdpSite/Design";
  public const string DepPdpSiteDiristries = DepNpdsPath + "/PdpSite/Diristries";
  public const string DepPdpSiteEntities = DepNpdsPath + "/PdpSite/Entities";
  public const string DepPdpSiteInfo = DepNpdsPath + "/PdpSite/Info";
  public const string DepPdpSitePapers = DepNpdsPath + "/PdpSite/Papers";
  public const string DepPdpSitePrivacy = DepNpdsPath + "/PdpSite/Privacy";


  public const string DepDebugModeCodeConfig = DepNpdsPath + "/DebugMode/CodeConfig";
  public const string DepDebugModeDotnetErrors = DepNpdsPath + "/DebugMode/DotnetErrors";
  public const string DepDebugModeQurcProperties = DepNpdsPath + "/DebugMode/QurcProperties";
  public const string DepDebugModeRazorEndpoints = DepNpdsPath + "/DebugMode/RazorEndpoints";
  public const string DepDebugModeRazorFormats = DepNpdsPath + "/DebugMode/RazorFormats";
  public const string DepDebugModeRazorRoutes = DepNpdsPath + "/DebugMode/RazorRoutes";
  public const string DepDebugModeServerDefaults = DepNpdsPath + "/DebugMode/ServerDefaults";
  public const string DepDebugModeSiteSettings = DepNpdsPath + "/DebugMode/SiteSettings";

  public const string DepAnonModeIndex = DepNpdsPath + "/AnonMode/Index";
  public const string DepAnonModeAccessDenied = DepNpdsPath + "/AnonMode/AccessDenied";
  public const string DepAnonModeConfirmEmail = DepNpdsPath + "/AnonMode/ConfirmEmail";
  public const string DepAnonModeContactSite = DepNpdsPath + "/AnonMode/ContactSite";
  public const string DepAnonModeDonateGift = DepNpdsPath + "/AnonMode/DonateGift";
  public const string DepAnonModeLoginUser = DepNpdsPath + "/AnonMode/LoginUser";
  public const string DepAnonModeRegisterUser = DepNpdsPath + "/AnonMode/RegisterUser";
  public const string DepAnonModeResetEmail = DepNpdsPath + "/AnonMode/ResetEmail";
  public const string DepAnonModeResetPassword = DepNpdsPath + "/AnonMode/ResetPassword";
  public const string DepAnonModeResetUsername = DepNpdsPath + "/AnonMode/ResetUsername";

  public const string DepAuthModeIndex = DepNpdsPath + "/AuthMode/Index";
  public const string DepAuthModeChangeEmail = DepNpdsPath + "/AuthMode/ChangeEmail";
  public const string DepAuthModeChangePassword = DepNpdsPath + "/AuthMode/ChangePassword";
  public const string DepAuthModeChangeProfile = DepNpdsPath + "/AuthMode/ChangeProfile";
  public const string DepAuthModeChangeUsername = DepNpdsPath + "/AuthMode/ChangeUsername";
  public const string DepAuthModeCheckSession = DepNpdsPath + "/AuthMode/CheckSession";
  public const string DepAuthModeDisplayProfile = DepNpdsPath + "/AuthMode/DisplayProfile";
  public const string DepAuthModeLogoutUser = DepNpdsPath + "/AuthMode/LogoutUser";

  public const string DepAgentModeIndex = DepNpdsPath + "/AgentMode/Index";
  public const string DepAgentModeCheckSession = DepNpdsPath + "/AgentMode/CheckSession";
  public const string DepAgentModeAddRoleAgent = DepNpdsPath + "/AgentMode/AddRoleAgent";
  public const string DepAgentModeAddRoleAuthor = DepNpdsPath + "/AgentMode/AddRoleAuthor";
  public const string DepAgentModeAddRoleEditor = DepNpdsPath + "/AgentMode/AddRoleEditor";
  public const string DepAgentModeAddRoleAdmin = DepNpdsPath + "/AgentMode/AddRoleAdmin";

  public const string DepAdminModeIndex = DepNpdsPath + "/AdminMode/Index";
  public const string DepAdminModeCheckSession = DepNpdsPath + "/AdminMode/CheckSession";
  public const string DepAdminModeEditSiaaRoles = DepNpdsPath + "/AdminMode/EditSiaaRoles";
  public const string DepAdminModeEditSiaaUsers = DepNpdsPath + "/AdminMode/EditSiaaUsers";
  public const string DepAdminModeViewSiaaRoles = DepNpdsPath + "/AdminMode/ViewSiaaRoles";
  public const string DepAdminModeViewSiaaUsers = DepNpdsPath + "/AdminMode/ViewSiaaUsers";

  // TODO: consider moving from ScribeServer to CoreServer ???
  public const string DepScribeServerAuthorAccess = DepNpdsPath + "/ScribeServer/AuthorAccess";
  public const string DepScribeServerEditorAccess = DepNpdsPath + "/ScribeServer/EditorAccess";
  public const string DepScribeServerServiceDefaults = DepNpdsPath + "/ScribeServer/ServiceDefaults";
  public const string DepScribeServerServiceRestrictions = DepNpdsPath + "/ScribeServer/ServiceRestrictions";
  public const string DepScribeServerServiceStatus = DepNpdsPath + "/ScribeServer/ServiceStatus";


  // CoreServer
  public const string DepCoreServerIndex = DepNpdsPath + "/CoreServer/Index";
  public const string DepCoreServerAnonResreps = DepNpdsPath + "/CoreServer/AnonResreps";
  public const string DepCoreServerUserResreps = DepNpdsPath + "/CoreServer/UserResreps";
  public const string DepCoreServerAgentResreps = DepNpdsPath + "/CoreServer/AgentResreps";
  public const string DepCoreServerAuthorResreps = DepNpdsPath + "/CoreServer/AuthorResreps";
  public const string DepCoreServerEditorResreps = DepNpdsPath + "/CoreServer/EditorResreps";
  public const string DepCoreServerAdminResreps = DepNpdsPath + "/CoreServer/AdminResreps";
  // CwlHome
  public const string DepCwlHomeIndex = DepNpdsPath + "/CwlHome/Index";
  public const string DepCwlHomeAnonHelp = DepNpdsPath + "/CwlHome/AnonHelp";
  public const string DepCwlHomeUserHelp = DepNpdsPath + "/CwlHome/UserHelp";
  public const string DepCwlHomeAgentHelp = DepNpdsPath + "/CwlHome/AgentHelp";
  public const string DepCwlHomeAuthorHelp = DepNpdsPath + "/CwlHome/AuthorHelp";
  public const string DepCwlHomeEditorHelp = DepNpdsPath + "/CwlHome/EditorHelp";
  public const string DepCwlHomeAdminHelp = DepNpdsPath + "/CwlHome/AdminHelp";

  // NexusServer
  public const string DepNexusServer = DepNpdsPath + "/NexusServer";
  public const string DepNexusServerIndex = $"{DepNexusServer}/Index";
  public const string DepNexusServerAnonResreps = $"{DepNexusServer}/AnonResreps";
  public const string DepNexusServerUserResreps = $"{DepNexusServer}/UserResreps";
  public const string DepNexusServerAgentResreps = $"{DepNexusServer}/AgentResreps";
  public const string DepNexusServerAuthorResreps = $"{DepNexusServer}/AuthorResreps";
  public const string DepNexusServerEditorResreps = $"{DepNexusServer}/EditorResreps";
  public const string DepNexusServerAdminResreps = $"{DepNexusServer}/AdminResreps";
   //NwlHome
  public const string DepNwlHome = DepNpdsPath + "/NwlHome";
  public const string DepNwlHomeIndex = $"{DepNwlHome}/Index";
  public const string DepNwlHomeAnonHelp = $"{DepNwlHome}/AnonHelp";
  public const string DepNwlHomeUserHelp = $"{DepNwlHome}/UserHelp";
  public const string DepNwlHomeAgentHelp = $"{DepNwlHome}/AgentHelp";
  public const string DepNwlHomeAuthorHelp = $"{DepNwlHome}/AuthorHelp";
  public const string DepNwlHomeEditorHelp = $"{DepNwlHome}/EditorHelp";
  public const string DepNwlHomeAdminHelp = $"{DepNwlHome}/AdminHelp";

  // ScribeServer
  public const string DepScribeServer = DepNpdsPath + "/ScribeServer";
  public const string DepScribeServerIndex = $"{DepScribeServer}/Index";
  public const string DepScribeServerAgentResreps = $"{DepScribeServer}/AgentResreps";
  public const string DepScribeServerAuthorResreps = $"{DepScribeServer}/AuthorResreps";
  public const string DepScribeServerEditorResreps = $"{DepScribeServer}/EditorResreps";
  public const string DepScribeServerAdminResreps = $"{DepScribeServer}/AdminResreps";
  public const string DepScribeServerExportNpdsQuads = $"{DepScribeServer}/ExportNpdsQuads";
  public const string DepScribeServerImportNpdsQuads = $"{DepScribeServer}/ImportNpdsQuads";
  //SwlHome
  public const string DepSwlHome = DepNpdsPath + "/SwlHome";
  public const string DepSwlHomeIndex = $"{DepSwlHome}/Index";
  public const string DepSwlHomeAgentHelp = $"{DepSwlHome}/AgentHelp";
  public const string DepSwlHomeAuthorHelp = $"{DepSwlHome}/AuthorHelp";
  public const string DepSwlHomeEditorHelp = $"{DepSwlHome}/EditorHelp";
  public const string DepSwlHomeAdminHelp = $"{DepSwlHome}/AdminHelp";

  // AcmsServer
  public const string DepAcmsServer = DepNpdsPath + "/AcmsServer";
  public const string DepAcmsServerIndex = $"{DepAcmsServer}/Index";
  public const string DepAcmsServerConcatDreamExamples = $"{DepAcmsServer}/ConcatDreamExamples";
  public const string DepAcmsServerConcatDreamPrinciples = $"{DepAcmsServer}/ConcatDreamPrinciples";
  public const string DepAcmsServerExportBcrList = $"{DepAcmsServer}/ExportBcrList";
  public const string DepAcmsServerImportBcrItem = $"{DepAcmsServer}/ImportBcrItem";
  public const string DepAcmsServerImportBcrList = $"{DepAcmsServer}/ImportBcrList";
  public const string DepAcmsServerImportBcrSui = $"{DepAcmsServer}/ImportBcrSui";
  //public const string DepAcmsServerImportDreamExample = $"{DepAcmsServer}/ImportDreamExample";
  //public const string DepAcmsServerImportDreamPrinciple = $"{DepAcmsServer}/ImportDreamPrinciple";
  public const string DepAcmsServerImportDreamResrep = $"{DepAcmsServer}/ImportDreamResrep";
  public const string DepAcmsServerMergeToSameResrep = $"{DepAcmsServer}/MergeToSameResrep";
  public const string DepAcmsServerSplitToDifferentResreps = $"{DepAcmsServer}/SplitToDifferentResreps";
  //AwlHome
  public const string DepAwlHome = DepNpdsPath + "/AwlHome";
  public const string DepAwlHomeIndex = $"{DepAwlHome}/Index";
  public const string DepAwlHomeAgentHelp = $"{DepAwlHome}/AgentHelp";
  public const string DepAwlHomeAuthorHelp = $"{DepAwlHome}/AuthorHelp";
  public const string DepAwlHomeEditorHelp = $"{DepAwlHome}/EditorHelp";
  public const string DepAwlHomeAdminHelp = $"{DepAwlHome}/AdminHelp";

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
  public const string depTSrg = "{recordGuid}";
  public const string depTSrgil = "{recordGuid}/{isLimited?}";
 // for TKG core/nexus/scribe webapps
  public const string depTSappfind = "{searchFilter?}/{serviceTag?}/{entityType?}";
  // for restapis
  public const string depTSststet = "{serviceType}/{serviceTag}/{entityType?}"; 
  // keys for Routing ConstraintMap dictionaries
  public const string NexusST = "NexusST"; // Nexus ServiceType constraint
  public const string PortalST = "PortalST"; // PORTAL ServiceType constraint
  public const string DoorsST = "DoorsST"; // DOORS ServiceType constraint
  public const string ScribeST = "ScribeST"; // Scribe ServiceType constraint
  public const string NpdsPT = "NpdsPT"; // PrincipalTag constraint
  public const string NpdsIS = "NpdsIS"; // InfosetStatus constraint

} // end class

// end file