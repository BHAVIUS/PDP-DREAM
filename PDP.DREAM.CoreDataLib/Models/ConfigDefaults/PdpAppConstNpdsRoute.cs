// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
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

  public const string DepCwlHomeIndex = DepNpdsPath + "/CwlHome/Index";
  public const string DepCwlHomeCodeConfig = DepNpdsPath + "/CwlHome/CodeConfig";
  public const string DepCwlHomeDotnetErrors = DepNpdsPath + "/CwlHome/DotnetErrors";
  public const string DepCwlHomeQurcProperties = DepNpdsPath + "/CwlHome/QurcProperties";
  public const string DepCwlHomeRazorEndpoints = DepNpdsPath + "/CwlHome/RazorEndpoints";
  public const string DepCwlHomeRazorFormats = DepNpdsPath + "/CwlHome/RazorFormats";
  public const string DepCwlHomeRazorRoutes = DepNpdsPath + "/CwlHome/RazorRoutes";
  public const string DepCwlHomeServerDefaults = DepNpdsPath + "/CwlHome/ServerDefaults";
  public const string DepCwlHomeSiteSettings = DepNpdsPath + "/CwlHome/SiteSettings";

  public const string DepAnonModeIndex = DepNpdsPath + "/AnonMode/Index";
  public const string DepAnonModeConfirmEmail = DepNpdsPath + "/AnonMode/ConfirmEmail";
  public const string DepAnonModeContactSite = DepNpdsPath + "/AnonMode/ContactSite";
  public const string DepAnonModeDonateGift = DepNpdsPath + "/AnonMode/DonateGift";
  public const string DepAnonModeLoginUser = DepNpdsPath + "/AnonMode/LoginUser";
  public const string DepAnonModeRegisterUser = DepNpdsPath + "/AnonMode/RegisterUser";

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

  // NexusServer
  public const string DepNexusServerIndex = DepNpdsPath + "/NexusServer/Index";
  public const string DepNexusServerAnonResreps = DepNpdsPath + "/NexusServer/AnonResreps";
  public const string DepNexusServerUserResreps = DepNpdsPath + "/NexusServer/UserResreps";
  public const string DepNexusServerAgentResreps = DepNpdsPath + "/NexusServer/AgentResreps";
  public const string DepNexusServerAuthorResreps = DepNpdsPath + "/NexusServer/AuthorResreps";
  public const string DepNexusServerEditorResreps = DepNpdsPath + "/NexusServer/EditorResreps";
  public const string DepNexusServerAdminResreps = DepNpdsPath + "/NexusServer/AdminResreps";

  // ScribeServer
  public const string DepScribeServerIndex = DepNpdsPath + "/ScribeServer/Index";
  public const string DepScribeServerAgentResreps = DepNpdsPath + "/ScribeServer/AgentResreps";
  public const string DepScribeServerAuthorResreps = DepNpdsPath + "/ScribeServer/AuthorResreps";
  public const string DepScribeServerEditorResreps = DepNpdsPath + "/ScribeServer/EditorResreps";
  public const string DepScribeServerAdminResreps = DepNpdsPath + "/ScribeServer/AdminResreps";

  // AcmsServer
  public const string DepAcmsServerAgentResreps = DepNpdsPath + "/AcmsServer/AgentResreps";
  public const string DepAcmsServerAuthorResreps = DepNpdsPath + "/AcmsServer/AuthorResreps";
  public const string DepAcmsServerEditorResreps = DepNpdsPath + "/AcmsServer/EditorResreps";
  public const string DepAcmsServerAdminResreps = DepNpdsPath + "/AcmsServer/AdminResreps";

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
  public const string NPtkgr = "PdpTkgr";
  public const string NSget = "Get"; // may include post for Get/Post
  public const string NSput = "Put"; // may include post for Put/Post
  public const string NSdel = "Del"; // may include post for Delete/Post
  public const string NSpost = "Post"; // for post only
  public const string TSststet = "{serviceType}/{serviceTag}/{entityType?}";
  public const string TSrg = "{recordGuid}";
  public const string TSrgil = "{recordGuid}/{isLimited?}";

  // keys for Routing ConstraintMap dictionaries
  public const string NexusST = "NexusST"; // Nexus ServiceType constraint
  public const string PortalST = "PortalST"; // PORTAL ServiceType constraint
  public const string DoorsST = "DoorsST"; // DOORS ServiceType constraint
  public const string ScribeST = "ScribeST"; // Scribe ServiceType constraint
  public const string NpdsPT = "NpdsPT"; // PrincipalTag constraint
  public const string NpdsIS = "NpdsIS"; // InfosetStatus constraint

} // end class

// end file