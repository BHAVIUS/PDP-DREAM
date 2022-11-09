// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class CoreDataRazorViewControllerBase : Controller, ISiaaUser
{
  private const string rzrCntrllr = nameof(CoreDataRazorViewControllerBase);

  [BindProperty]
  public PdpSiteRazorModel PSR { get; set; } = new PdpSiteRazorModel();
  protected const string PsrKey = nameof(PSR);

  // ranp = Route App NamePrefix
  public const string CwlRanpView = "CoreWebLibView"; // for MVC controller-action views

  protected IEmailSender qebEmailSender;
  protected ISmsSender qebSmsSender;
  protected ILogger qebLogger;

  // QEB User REST Context = QURC
  // reset on each request to each controller
  protected QebUserRestContext? qebUserRestCntxt = null;
  public QebUserRestContext QURC
  {
    set {
      //if (value == null) { qebUserRestCntxt = InitRestContext(); }
      //else { qebUserRestCntxt = value; }
      if (value == null) { throw new ArgumentNullException(nameof(value)); }
      qebUserRestCntxt = value;
    }
    get {
      if (qebUserRestCntxt == null) { qebUserRestCntxt = InitRestContext(); }
      return qebUserRestCntxt;
    }
  }

  // QEB User Data Context = QUDC
  protected QebIdentityContext qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
  public QebIdentityContext QUDC { get { return qebUserDataCntxt; } set { qebUserDataCntxt = value; } }

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
  protected const string QurcKey = nameof(QURC);
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  protected const string QudcKey = nameof(QUDC);
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  protected const string PcdcKey = nameof(PCDC);
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // protected const string PndcKey = nameof(PNDC);
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // protected const string PsdcKey = nameof(PSDC);

  // PDP Core Data Context = PCDC
  protected CoreDbsqlContext pdpCoreDataCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
  public CoreDbsqlContext PCDC
  {
    set { pdpCoreDataCntxt = value; ResetCoreRepository(); }
    get {
      if (pdpCoreDataCntxt == null) { pdpCoreDataCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr); }
      return pdpCoreDataCntxt;
    }
  }

  // protected so not visible as public action for controller routes
  protected void ResetCoreRepository()
  {
#if DEBUG
    CatchNullQurc(nameof(ResetCoreRepository), rzrCntrllr);
    if (PCDC == null) { PCDC.CatchNullObject(PcdcKey, nameof(ResetCoreRepository), rzrCntrllr); }
#endif
    // reset ViewData with current QEB User Rest Context
    ViewData[QurcKey] = QURC;
    // reset repository with current PDP Core Data Context
    pdpCoreDataCntxt.ResetQebiContext(QURC);
  }

  public CoreDataRazorViewControllerBase()
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpCoreDataCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
  }

  public CoreDataRazorViewControllerBase(QebIdentityContext userCntxt)
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpCoreDataCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
  }
  public CoreDataRazorViewControllerBase(CoreDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpCoreDataCntxt = npdsCntxt;
  }
  public CoreDataRazorViewControllerBase(QebIdentityContext userCntxt, CoreDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpCoreDataCntxt = npdsCntxt;
  }
  public CoreDataRazorViewControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpCoreDataCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
  }
  public CoreDataRazorViewControllerBase(QebIdentityContext userCntxt, CoreDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpCoreDataCntxt = npdsCntxt;
  }

  protected UilDropDownLists UilDdlists;
  protected IList<EntityTypeListItem> EntityTypeSelectList;
  protected IList<FieldFormatListItem> FieldFormatSelectList;
  protected IList<SelectListItem>? CoreDiristryListMvc;

  // migrate from Scribe*ControllerBase to Core*ControllerBase
  protected void BuildCoreDropDownLists()
  {
    // TODO: re-eval conventions for select item lists
    //  currently suffix "Mvc" associated with those lists that are IList<SelectListItem>?
    UilDdlists = new UilDropDownLists()
    {
      EntityTypeList = PCDC.GetEntityTypeSelectList(),
      FieldFormatList = PCDC.GetFieldFormatSelectList(),
      CoreDiristryListMvc = PCDC.GetCoreDiristrySelectListMvc(),
    };
    // TODO: use of fields or use of ViewData ? which approach works better ?
    EntityTypeSelectList = UilDdlists.EntityTypeList;
    FieldFormatSelectList = UilDdlists.FieldFormatList;
    CoreDiristryListMvc = UilDdlists.CoreDiristryListMvc;
    // TODO: use of fields or use of ViewData ? which approach works better ?
    ViewData[nameof(UilDropDownLists.EntityTypeList)] = UilDdlists.EntityTypeList;
    ViewData[nameof(UilDropDownLists.FieldFormatList)] = UilDdlists.FieldFormatList;
    ViewData[nameof(UilDropDownLists.CoreDiristryListMvc)] = UilDdlists.CoreDiristryListMvc;

    if (QURC.ClientHasScribeEditAccess)
    {
      UilDdlists.CoreDiristryList = PCDC.GetCoreDiristrySelectList();
      ViewData[nameof(UilDropDownLists.CoreDiristryList)] = UilDdlists.CoreDiristryList;
      UilDdlists.CoreDiristryListMvc = PCDC.GetCoreDiristrySelectListMvc();
      ViewData[nameof(UilDropDownLists.CoreDiristryListMvc)] = UilDdlists.CoreDiristryListMvc;
      UilDdlists.RegcDiristryListMvc = PCDC.GetRegistrarDiristriesSelectListMvc();
      ViewData[nameof(UilDropDownLists.RegcDiristryListMvc)] = UilDdlists.RegcDiristryListMvc;
    }

    if (QURC.ClientHasEditorOrAdminAccess)
    {
      UilDdlists.CoreRegistryList = PCDC.GetCoreRegistrySelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistryList)] = UilDdlists.CoreRegistryList;
      UilDdlists.CoreDirectoryList = PCDC.GetCoreDirectorySelectList();
      ViewData[nameof(UilDropDownLists.CoreDirectoryList)] = UilDdlists.CoreDirectoryList;
      UilDdlists.CoreRegistrarList = PCDC.GetCoreRegistrarSelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistrarList)] = UilDdlists.CoreRegistrarList;
    }

    if (QURC.ClientHasAdminAccess)
    {
      UilDdlists.InfosetPortalStatusList = PCDC.GetInfosetPortalStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetPortalStatusList)] = UilDdlists.InfosetPortalStatusList;
      UilDdlists.InfosetDoorsStatusList = PCDC.GetInfosetDoorsStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetDoorsStatusList)] = UilDdlists.InfosetDoorsStatusList;
    }

  }

  protected QebUserRestContext InitRestContext()
  {
    var qurc = new QebUserRestContext(HttpContext);
    return qurc;
  }

  protected virtual ILogger InitLogger<TLogger>(ILoggerFactory? lgrFtry = null)
  {
    if (lgrFtry == null) { lgrFtry = new LoggerFactory(); }
    qebLogger = lgrFtry.CreateLogger<TLogger>();
    return qebLogger;
  }

  protected virtual void CatchNullQurc(string methodName, string className)
  {
    QURC.CatchNullObject(QurcKey, methodName, className);
  }
  protected virtual void DebugQurcData(object thing)
  {
    if (thing is ViewResult)
    {
      var pageResult = (ViewResult)thing;
      var qurcData = pageResult.ViewData[QurcKey];
    }
  }

  public override void OnActionExecuting(ActionExecutingContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnActionExecuting), rzrCntrllr);
#endif
    ResetCoreRepository();
    ViewData[PsrKey] = PSR;
  }
  public override void OnActionExecuted(ActionExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnActionExecuted), rzrCntrllr);
    if (exeCntxt.Result is ViewResult)
    {
      var result = (ViewResult)exeCntxt.Result;
      var qurcData = result.ViewData[QurcKey];
    }
#endif
  }

  protected NamesForIdentityRoles npdsUsrRolRequired = NamesForIdentityRoles.NpdsAnon;
  public NamesForIdentityRoles NpdsUserRoleRequired
  {
    set { npdsUsrRolRequired = value; }
    get { return npdsUsrRolRequired; }
  }

  public bool CheckCoreUserRole(HttpRequest httpReqst,
    NpdsDatabaseType dbType = NpdsDatabaseType.Core, NpdsDatabaseAccess dbAccess = NpdsDatabaseAccess.AnonReadOnly)
  {
    // QebUserRestContext in PDP.DREAM.CoreDataLib.Models
    // new QebUserRestContext() calls ParseQueryCollection
    var roleIsVerified = false;
    switch (NpdsUserRoleRequired)
    {
      case NamesForIdentityRoles.NpdsAdmin:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Admin,
          AdminModeClientRequired = true,
          QebSessionValueIsRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForIdentityRoles.NpdsEditor:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Editor,
          EditorModeClientRequired = true,
          QebSessionValueIsRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForIdentityRoles.NpdsAuthor:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Author,
          AuthorModeClientRequired = true,
          QebSessionValueIsRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForIdentityRoles.NpdsAgent:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Agent,
          AgentModeClientRequired = true,
          QebSessionValueIsRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForIdentityRoles.NpdsUser:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AuthUser,
          UserModeClientRequired = true,
          QebSessionValueIsRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForIdentityRoles.NpdsAuth:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AnonUser,
          AuthenticatedClientRequired = true,
          QebSessionValueIsRequired = true
        };
        if (OnlineUserIsAuthenticated) { roleIsVerified = true; }
        break;
      case NamesForIdentityRoles.NpdsAnon:
        QURC = new QebUserRestContext(HttpContext)
        {
          NpdsUserRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AnonUser,
          AuthenticatedClientRequired = false,
          QebSessionValueIsRequired = false
        };
        break;
      default:
        throw new Exception("Invalid User Role");
    }
    ResetCoreRepository();
#if DEBUG
    var appName = PSR.PdpSiteInfo.SiteAppNameVersion;
    var userRole = QURC.NpdsUserRole.ToString();
    var userName = QURC.QebUserNameDisplayed;
#endif
    return roleIsVerified;
  }

  public bool CheckCoreUserSession(bool userIsAgent = false)
  {
    var qebSignin = new QebIdentityResult();
    var sessionIsIdentified = false;
    var userIsVerified = false;
    if (QURC.AuthorizedClientIsRequired) // depends on the PRC.RecordAccess setting
    {
      if (OnlineUserIsAuthenticated)
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (!string.IsNullOrEmpty(QURC.QebUserName) && !string.IsNullOrEmpty(QURC.QebPassWord))
      {
        qebSignin = QebUserSignin(QURC.QebUserName, QURC.QebPassWord);
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (QURC.QebSessionValueIsRequired && !QURC.QebSessionGuid.IsInvalid())
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
        qebSignin = QebUserSignin(QURC.QebUserGuid, QURC.QebAgentGuid, QURC.QebSessionGuid);
      }
      if (sessionIsIdentified)
      {
        userIsVerified = QURC.ClientIsVerified;
      }
    }
#if DEBUG
    UserGuidDevTest();
#endif
    return userIsVerified;
  }

  public bool CheckCoreAgentSession()
  {
    var qebSignin = new QebIdentityResult();
    var sessionIsIdentified = false;
    var agentIsVerified = false;
    if (QURC.AuthorizedClientIsRequired) // depends on the PRC.RecordAccess setting
    {
      if (OnlineUserIsAuthenticated)
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (!string.IsNullOrEmpty(QURC.QebUserName) && !string.IsNullOrEmpty(QURC.QebPassWord))
      {
        qebSignin = QebUserSignin(QURC.QebUserName, QURC.QebPassWord);
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (QURC.QebSessionValueIsRequired && !QURC.QebSessionGuid.IsInvalid())
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
        qebSignin = QebUserSignin(QURC.QebUserGuid, QURC.QebAgentGuid, QURC.QebSessionGuid);
      }
      if (sessionIsIdentified)
      {
        agentIsVerified = QURC.ClientIsVerified;
      }
    }
#if DEBUG
    UserGuidDevTest();
#endif
    return agentIsVerified;
  }

  public Guid? ParseResRepRecordGuid(string recordName, Guid? modelGuid, Guid defaultGuid)
  {
    var parsedGuid = PdpGuid.ParseToNonNullable(modelGuid, defaultGuid);
    if (PdpGuid.IsNullOrEmpty(parsedGuid))
    { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
    return parsedGuid;
  }

  protected void PdpPrcMvcAddErrors(string error)
  {
    ModelState.AddModelError("", error);
  }
  protected void PdpPrcMvcAddErrors(string[] errors)
  {
    foreach (var error in errors)
    {
      ModelState.AddModelError("", error);
    }
  }

  protected void UserGuidDevTest()
  {
    // test-dev-debug from Microsoft ASP.NET ClaimsPrincipal (prefix mac)
    var macUsrName = QebUserName;
    if (string.IsNullOrWhiteSpace(macUsrName)) { QURC.ServiceError += "QebUserName is null, empty, or whitespace"; }
    var macUsrGuid = QebUserGuid;
    if (PdpGuid.IsInvalidGuid(macUsrGuid)) { QURC.ServiceError += "QebUserGuid is null, empty, or invalid"; }

    // test-dev-debug from QEB User REST Context (prefix qeb)
    var qebUsrGuid = QURC.QebUserGuid;
    if (PdpGuid.IsInvalidGuid(qebUsrGuid)) { QURC.ServiceError += "QebUserGuid is null, empty, or invalid"; }
    var qebAgtGuid = QURC.QebAgentGuid;
    if (PdpGuid.IsInvalidGuid(qebAgtGuid)) { QURC.ServiceError += "QebAgentGuid is null, empty, or invalid"; }
    var qebInfGuid = QURC.QebAgentInfosetGuid;
    if (PdpGuid.IsInvalidGuid(qebInfGuid)) { QURC.ServiceError += "QebAgentInfosetGuid is null, empty, or invalid"; }
    var qebSsnGuid = QURC.QebSessionGuid;
    if (PdpGuid.IsInvalidGuid(qebSsnGuid)) { QURC.ServiceError += "QebAgentSessionGuid is null, empty, or invalid"; }

    // error check reports for QEB User REST Context
    if ((macUsrGuid == Guid.Empty) && (qebUsrGuid == Guid.Empty)) { QURC.ServiceError += "both ClaimsPrincipal and RestContext UserGuids are null or empty"; }
    if (macUsrGuid != qebUsrGuid) { QURC.ServiceError += "UserGuids from ClaimsPrincipal and RestContext are not equal"; }
  }

  // PROPERTIES
  // TODO: migrate to interface typed version using System.Security.Claims.IPrincipal;
  // private IPrincipal pdpPrincipal; // with readonly Identity (type IIdentity) and IsInRole (type bool) for current user
  // using System.Security.Principal.IIdentity;
  // private IIdentity pdpIdentity; // with readonly AuthenticationType (string), IsAuthenticated (bool), Name (string) for current user
  // see extensions in PDP.DREAM.NpdsRootLib.Models.AncIdentity.PdpUserExtensions

  // READONLY PROPERTIES
  public ClaimsPrincipal OnlineUser
  { // wrapper for HttpContext.User
    get {
      // if (onlinUsr == null) { onlinUsr = HttpContext.User; }
      // now available in Microsoft.AspNetCore.Mvc.ControllerBase as property User
      if (onlinUsr == null) { onlinUsr = User; }
      return onlinUsr;
    }
  }
  private ClaimsPrincipal? onlinUsr = null;

  private bool onlinUsrIdent = false;
  public bool OnlineUserIsIdentified
  {
    get {
      if (OnlineUser == null) { onlinUsrIdent = false; }
      else if (OnlineUser.Identities != null)
      {
        onlinUsrIdent = OnlineUser.Identities
        .Any(i => (i.AuthenticationType == PdpIdentityScheme));

      }
      return onlinUsrIdent;
    }
  }

  private bool onlinUsrAuth = false;
  public bool OnlineUserIsAuthenticated
  {
    get {
      onlinUsrAuth = OnlineUser.Identity.IsAuthenticated;
      QURC.ClientIsAuthenticated = onlinUsrAuth;
      QURC.NpdsUserRole = npdsUsrRolRequired;
      QURC.QebUserName = QebUserName;
      QURC.QebUserGuid = QebUserGuid;
      QURC.QebAgentGuid = QebAgentGuid;
      QURC.QebSessionGuid = QebSessionGuid;
      return onlinUsrAuth;
    }
  }

  public string OnlineUserName
  {
    get { return QebUserName; }
  }
  private string qebUsrName = "";
  protected string QebUserName
  {
    get {
      if (string.IsNullOrEmpty(qebUsrName))
      {
        qebUsrName = QebiUserExtensions.GetUserName(OnlineUser);
        QURC.QebUserName = qebUsrName;
      }
      return qebUsrName;
    }
  }

  public Guid OnlineUserGuid
  {
    get { return QebUserGuid; }
  }
  private Guid qebUsrGuid = Guid.Empty;
  protected Guid QebUserGuid
  {
    get {
      if (qebUsrGuid.IsEmpty())
      {
        qebUsrGuid = QebiUserExtensions.GetUserGuid(OnlineUser);
        QURC.QebUserGuid = qebUsrGuid;
      }
      return qebUsrGuid;
    }
  }

  private QebIdentityAppUser? qebUsr = null;
  protected QebIdentityAppUser QebUser
  {
    get {
      return qebUsr;
    }
    set {
      qebUsr = value;
      qebUsrGuid = qebUsr.UserGuidKey;
      QURC.QebUserGuid = qebUsrGuid;
      qebUsrName = qebUsr.UserName;
      QURC.QebUserName = qebUsrName;
      qebSsnGuid = qebUsr.SessionGuidRef ?? Guid.Empty;
      QURC.QebSessionGuid = qebSsnGuid;
    }
  }

  private Guid qebAgtGuid = Guid.Empty;
  public Guid QebAgentGuid
  {
    get {
      if (qebAgtGuid.IsEmpty())
      {
        qebAgtGuid = QebiUserExtensions.GetAgentGuid(OnlineUser);
        QURC.QebAgentGuid = qebAgtGuid;
      }
      return qebAgtGuid;
    }
  }

  private Guid qebSsnGuid = Guid.Empty;
  public Guid QebSessionGuid
  {
    get {
      if (qebSsnGuid.IsEmpty())
      {
        qebSsnGuid = QebiUserExtensions.GetSessionGuid(OnlineUser);
        QURC.QebSessionGuid = qebSsnGuid;
      }
      return qebSsnGuid;
    }
  }

  public QebIdentityResult QebUserSignin(string userName, string passWord)
  {
    return QebUserSigninAsync(userName, passWord).GetAwaiter().GetResult();
  }

  public async Task<QebIdentityResult> QebUserSigninAsync(string userName, string passWord)
  {
    var qebSignin = new QebIdentityResult();
    var qebUser = QUDC.GetUserByUserName(userName);
    if ((qebUser == null) || string.IsNullOrWhiteSpace(qebUser.UserName) || string.IsNullOrWhiteSpace(qebUser.PasswordHash))
    { qebSignin.Failed = true; return qebSignin; }
    if (qebUser.ConcurrencyStamp == PdpInvalidToken || qebUser.UserGuidKey.IsInvalid())
    { qebSignin.Failed = true; return qebSignin; }
    var userNameOk = (qebUser.UserName.ToLower() == userName.ToLower());
    if (!userNameOk) { qebSignin.Failed = true; return qebSignin; }
    // var passWordOk = PdpCryptoService.VerifyHashedToken(qebUser.PasswordHash, passWord);
    var passWordOk = QebCryptoService.TokenEqualsHash(passWord, qebUser.PasswordHash);
    if (!passWordOk) { qebSignin.Failed = true; return qebSignin; }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(qebUser.UserGuidKey);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, userName, qebUser.UserGuidKey, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }

  public QebIdentityResult QebUserSignin(string userName, Guid userGuid)
  {
    return QebUserSigninAsync(userName, userGuid).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(string userName, Guid userGuid)
  {
    if (string.IsNullOrWhiteSpace(userName) || userGuid.IsEmpty())
    { throw new ArgumentNullException("userName or userGuid is null empty or invalid in QebUserSignin"); }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, userName, userGuid, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }
  public QebIdentityResult QebUserSignin(Guid userGuid, Guid agentGuid, Guid sessionGuid)
  {
    return QebUserSigninAsync(userGuid, agentGuid, sessionGuid).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(Guid userGuid, Guid agentGuid, Guid sessionGuid)
  {
    if (userGuid.IsInvalid() || agentGuid.IsInvalid() || sessionGuid.IsInvalid())
    { throw new ArgumentNullException("userGuid, agentGuid, or sessionGuid is invalid in QebUserSignin"); }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, string.Empty, userGuid, agentGuid, sessionGuid, userRoles);
    return result;
  }
  public QebIdentityResult QebUserSignin(string userName, Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles)
  {
    return QebUserSigninAsync(userName, userGuid, agentGuid, sessionGuid, userRoles).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(string userName, Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles)
  {
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, userName, userGuid, agentGuid, sessionGuid, userRoles);
    return result;
  }

  public void QebUserSignout()
  {
    QebUserSignoutAsync();
    return;
  }
  public async void QebUserSignoutAsync()
  {
    await QebiUserExtensions.SignoutUserAsync(HttpContext);
    return;
  }

  // HELPER METHODS

  protected void AddErrors(IEnumerable<QebIdentityError> errorList)
  {
    foreach (var error in errorList)
    {
      ModelState.AddModelError("PdpIdentity", error.Message);
    }
  }

  protected bool IsTokenDateValid(DateTime? tokenDate)
  {
    var current = DateTime.UtcNow;
    var expired = Convert.ToDateTime(tokenDate ?? DateTime.MinValue);
    var isValid = (DateTime.Compare(current, expired) < 0);
    return isValid;
  }

  protected ActionResult RedirectToLocal(string returnUrl, string pathIdentProfile, string pathUserIndex)
  {
    if (IsUrlLocalToLoginDomain(returnUrl))
    {
      return Redirect(returnUrl);
    }
    else
    {
      if (OnlineUserIsAuthenticated) { return Redirect(pathIdentProfile); }
      else { return Redirect(pathUserIndex); }
    }
  }

  protected bool IsUrlLocalToLoginDomain(string testUrl)
  {
    if (Url.IsLocalUrl(testUrl) && (testUrl.Length > 1) && testUrl.StartsWith("/") && !testUrl.StartsWith("//") && !testUrl.StartsWith("/\\"))
    { return true; }
    else
    { return false; }
  }

  protected string ArgCheckReturnUrl(string theUrl)
  {
    if ((theUrl == null) || !IsUrlLocalToLoginDomain(theUrl))
    { theUrl = Url.Content(DepPdpSiteInfo); }
    return theUrl;
  }

  public string AppendKeys(string pathKeys, Guid ssnKey, Guid agtKey, Guid usrKey)
  {
    return AppendKeys(pathKeys, ssnKey.ToString(), agtKey.ToString(), usrKey.ToString());
  }
  public string AppendKeys(string pathKeys, string ssnKey, string agtKey, string usrKey)
  {
    if (pathKeys == null) { pathKeys = string.Empty; }
    if (!pathKeys.Contains(QuestionChar)) { pathKeys = pathKeys + QuestionChar; }
    if (!PdpGuid.IsInvalidGuid(ssnKey)) { pathKeys = pathKeys + AndChar + QskSessionKey + EqualChar + ssnKey; }
    if (!PdpGuid.IsInvalidGuid(agtKey)) { pathKeys = pathKeys + AndChar + QskAgentKey + EqualChar + agtKey; }
    if (!PdpGuid.IsInvalidGuid(usrKey)) { pathKeys = pathKeys + AndChar + QskUserKey + EqualChar + usrKey; }
    return pathKeys;
  }

  protected bool AddRoleUser(Guid? roleGuid)
  {
    var usrRoleAdded = false;
    var appGuid = PDPSS.AppSecureUiaaGuid;
    var errorCode = QUDC.QebIdentityAppLinkEdit(PdpGuid.NewGuid(), QebUserGuid, roleGuid, appGuid);
    if (errorCode == 0) { usrRoleAdded = true; }
    return usrRoleAdded;
  }

} // end class

// end file