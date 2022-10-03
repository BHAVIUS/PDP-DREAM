// NpdsConstRegex.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // for use with dotnet Razor ViewEngine and ViewEngineOptions
  // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengineoptions?view=aspnetcore-6.0

  // DREAM EndPoint (DEP) for NPDS
  public const string DepNpdsPath = "/NPDS";
  public const string DepNpdsFolder = "NPDS";
  public const string DepNpdsViews = "/Views/NPDS/{1}/{0}";

  // NPDS paths - initial slash assures path from contentroot
  public const string DepPdpSiteErrors = "/NPDS/CoreDataLib/MvcErrors";
  public const string DepPdpSiteRoutes = "/NPDS/CoreDataLib/MvcRoutes";
  public const string DepQebIdentRequired = "/NPDS/AnonCore/AccessDenied";
  public const string DepQebIdentLogin = "/NPDS/AnonCore/LoginUser";
  public const string DepQebIdentLogout = "/NPDS/AuthCore/LogoutUser";

   public const string DepPdpSiteDesign = DepNpdsPath + "/PdpSite/Design";
  public const string DepPdpSiteDiristries = DepNpdsPath + "/PdpSite/Diristries";
  public const string DepPdpSiteEntities = DepNpdsPath + "/PdpSite/Entities";
  public const string DepPdpSiteIndex = DepNpdsPath + "/PdpSite/Index";
  public const string DepPdpSiteInfo = DepNpdsPath + "/PdpSite/Info";
  public const string DepPdpSitePapers = DepNpdsPath + "/PdpSite/Papers";
  public const string DepPdpSitePrivacy = DepNpdsPath + "/PdpSite/Privacy";

  public const string DepAnonCoreConfirmEmail = "/NPDS/AnonCore/ConfirmEmail";
  public const string DepAnonCoreContactSite = "/NPDS/AnonCore/ContactSite";
  public const string DepAnonCoreDonateGift = "/NPDS/AnonCore/DonateGift";
  public const string DepAnonCoreIndex = "/NPDS/AnonCore/Index";
  public const string DepAnonCoreLoginUser = "/NPDS/AnonCore/LoginUser";
  public const string DepAnonCoreRegisterUser = "/NPDS/AnonCore/RegisterUser";

  public const string DepAuthCoreAddRoleAgent = "/NPDS/AuthCore/AddRoleAgent";
  public const string DepAuthCoreChangeEmail = "/NPDS/AuthCore/ChangeEmail";
  public const string DepAuthCoreChangePassword = "/NPDS/AuthCore/ChangePassword";
  public const string DepAuthCoreChangeProfile = "/NPDS/AuthCore/ChangeProfile";
  public const string DepAuthCoreChangeUsername = "/NPDS/AuthCore/ChangeUsername";
  public const string DepAuthCoreCheckSession = "/NPDS/AuthCore/CheckSession";
  public const string DepAuthCoreDisplayProfile = "/NPDS/AuthCore/DisplayProfile";
  public const string DepAuthCoreIndex = "/NPDS/AuthCore/Index";
  public const string DepAuthCoreLogoutUser = "/NPDS/AuthCore/LogoutUser";

  public const string DepAgentCoreIndex = "/NPDS/AgentCore/Index";
  public const string DepAgentCoreCheckSession = "/NPDS/AgentCore/CheckSession";
  public const string DepAgentCoreAddRoleAuthor = "/NPDS/AgentCore/AddRoleAuthor";
  public const string DepAgentCoreAddRoleEditor = "/NPDS/AgentCore/AddRoleEditor";
  public const string DepAgentCoreAddRoleAdmin = "/NPDS/AgentCore/AddRoleAdmin";


  // TODO: consider moving from ScribeServer to CoreServer
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
  // public const string DepScribeServerAnonResreps = DepNpds + "/ScribeServer/AnonResreps";
  // public const string DepScribeServerUserResreps = DepNpds + "/ScribeServer/UserResreps";
  public const string DepScribeServerAgentResreps = DepNpdsPath + "/ScribeServer/AgentResreps";
  public const string DepScribeServerAuthorResreps = DepNpdsPath + "/ScribeServer/AuthorResreps";
  public const string DepScribeServerEditorResreps = DepNpdsPath + "/ScribeServer/EditorResreps";
  public const string DepScribeServerAdminResreps = DepNpdsPath + "/ScribeServer/AdminResreps";

  // AcmsServer
  public const string DepAcmsServerAgentResreps = DepNpdsPath + "/AcmsServer/AgentResreps";
  public const string DepAcmsServerAuthorResreps = DepNpdsPath + "/AcmsServer/AuthorResreps";
  public const string DepAcmsServerEditorResreps = DepNpdsPath + "/AcmsServer/EditorResreps";
  public const string DepAcmsServerAdminResreps = DepNpdsPath + "/AcmsServer/AdminResreps";

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
  public const string NpdsPT = "NpdsPT"; // PrincipalTag constraint
  public const string NpdsIS = "NpdsIS"; // InfosetStatus constraint

} // end class

// end file