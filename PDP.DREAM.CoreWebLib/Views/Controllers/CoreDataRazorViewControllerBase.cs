// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class CoreDataRazorViewControllerBase : Controller, ISiaaUser
{
  private const string rzrClass = nameof(CoreDataRazorViewControllerBase);

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
  protected QebiUserRestContext? qebUserRestCntxt = null;
  public QebiUserRestContext QURC
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
  protected QebiDbsqlContext qebUserDataCntxt = new QebiDbsqlContext();
  public QebiDbsqlContext QUDC { get { return qebUserDataCntxt; } set { qebUserDataCntxt = value; } }

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
  protected CoreDbsqlContext? pdpCoreDataCntxt = null;
  public CoreDbsqlContext? PCDC
  {
    set { pdpCoreDataCntxt = value; ResetCoreRepository(); }
    get {
      if (pdpCoreDataCntxt == null) { pdpCoreDataCntxt = new CoreDbsqlContext(); }
      return pdpCoreDataCntxt;
    }
  }

  // protected so not visible as public action for controller routes
  protected void ResetCoreRepository()
  {
#if DEBUG
    QURC.CatchNullObject(QurcKey, nameof(ResetCoreRepository), rzrClass);
    PCDC.CatchNullObject(PcdcKey, nameof(ResetCoreRepository), rzrClass);
#endif
    // reset ViewData with current QEB User Rest Context
    ViewData[QurcKey] = QURC;
    // reset repository with current QEB User Rest Context
    pdpCoreDataCntxt = new CoreDbsqlContext((INpdsClient)QURC);
  }

  public CoreDataRazorViewControllerBase()
  {
    qebLogger = InitLogger<CoreDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebiDbsqlContext();
    pdpCoreDataCntxt = new CoreDbsqlContext();
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

  protected QebiUserRestContext InitRestContext()
  {
    var qurc = new QebiUserRestContext(HttpContext);
    return qurc;
  }

  protected virtual ILogger InitLogger<TLogger>(ILoggerFactory? lgrFtry = null)
  {
    if (lgrFtry == null) { lgrFtry = new LoggerFactory(); }
    qebLogger = lgrFtry.CreateLogger<TLogger>();
    return qebLogger;
  }

  // TODO: migrate to the set/get with .CatchNullObject extension on values
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
    CatchNullQurc(nameof(OnActionExecuting), rzrClass);
#endif
    ResetCoreRepository();
    ViewData[PsrKey] = PSR;
  }
  public override void OnActionExecuted(ActionExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnActionExecuted), rzrClass);
    if (exeCntxt.Result is ViewResult)
    {
      var result = (ViewResult)exeCntxt.Result;
      var qurcData = result.ViewData[QurcKey];
    }
#endif
  }

  protected NamesForClientRoles npdsUsrRolRequired = NamesForClientRoles.NpdsAnon;
  public NamesForClientRoles NpdsUserRoleRequired
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
      case NamesForClientRoles.NpdsAdmin:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Admin,
          AdminModeClientRequired = true,
          SessionClientRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForClientRoles.NpdsEditor:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Editor,
          EditorModeClientRequired = true,
          SessionClientRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForClientRoles.NpdsAuthor:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Author,
          AuthorModeClientRequired = true,
          SessionClientRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForClientRoles.NpdsAgent:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.Agent,
          AgentModeClientRequired = true,
          SessionClientRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForClientRoles.NpdsUser:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AuthUser,
          UserModeClientRequired = true,
          SessionClientRequired = true
        };
        roleIsVerified = CheckCoreUserSession();
        break;
      case NamesForClientRoles.NpdsAuth:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AnonUser,
          AuthenticatedClientRequired = true,
          SessionClientRequired = true
        };
        if (OnlineUserIsAuthenticated) { roleIsVerified = true; }
        break;
      case NamesForClientRoles.NpdsAnon:
        QURC = new QebiUserRestContext(HttpContext)
        {
          ClientRole = NpdsUserRoleRequired,
          DatabaseType = dbType,
          DatabaseAccess = dbAccess,
          RecordAccess = NpdsRecordAccess.AnonUser,
          AuthenticatedClientRequired = false,
          SessionClientRequired = false
        };
        break;
      default:
        throw new Exception("Invalid User Role");
    }
    ResetCoreRepository();
#if DEBUG
    var appName = PSR.PdpSiteInfo.SiteAppNameVersion;
    var userRole = QURC.ClientRole.ToString();
    var userName = QURC.ClientUserNameDisplayed;
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
      else if (!string.IsNullOrEmpty(QURC.ClientUserName) && !string.IsNullOrEmpty(QURC.ClientPassWord))
      {
        qebSignin = QebUserSignin(QURC.ClientUserName, QURC.ClientPassWord);
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (QURC.SessionClientRequired && !QURC.ClientSessionGuid.IsInvalid())
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
        qebSignin = QebUserSignin(QURC.ClientUserGuid, QURC.ClientAgentGuid, QURC.ClientSessionGuid);
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
      else if (!string.IsNullOrEmpty(QURC.ClientUserName) && !string.IsNullOrEmpty(QURC.ClientPassWord))
      {
        qebSignin = QebUserSignin(QURC.ClientUserName, QURC.ClientPassWord);
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
      }
      else if (QURC.SessionClientRequired && !QURC.ClientSessionGuid.IsInvalid())
      {
        sessionIsIdentified = PCDC.CheckCoreSessionAgent(ref qebUserRestCntxt);
        qebSignin = QebUserSignin(QURC.ClientUserGuid, QURC.ClientAgentGuid, QURC.ClientSessionGuid);
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
    var qebUsrGuid = QURC.ClientUserGuid;
    if (PdpGuid.IsInvalidGuid(qebUsrGuid)) { QURC.ServiceError += "QebUserGuid is null, empty, or invalid"; }
    var qebAgtGuid = QURC.ClientAgentGuid;
    if (PdpGuid.IsInvalidGuid(qebAgtGuid)) { QURC.ServiceError += "QebAgentGuid is null, empty, or invalid"; }
    var qebInfGuid = QURC.ClientAgentInfosetGuid;
    if (PdpGuid.IsInvalidGuid(qebInfGuid)) { QURC.ServiceError += "QebAgentInfosetGuid is null, empty, or invalid"; }
    var qebSsnGuid = QURC.ClientSessionGuid;
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
      QURC.ClientRole = npdsUsrRolRequired;
      QURC.ClientUserName = QebUserName;
      QURC.ClientUserGuid = QebUserGuid;
      QURC.ClientAgentGuid = QebAgentGuid;
      QURC.ClientSessionGuid = QebSessionGuid;
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
        qebUsrName = QebiExtensions.GetUserName(OnlineUser);
        QURC.ClientUserName = qebUsrName;
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
        qebUsrGuid = QebiExtensions.GetUserGuid(OnlineUser);
        QURC.ClientUserGuid = qebUsrGuid;
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
      QURC.ClientUserGuid = qebUsrGuid;
      qebUsrName = qebUsr.UserName;
      QURC.ClientUserName = qebUsrName;
      qebSsnGuid = qebUsr.SessionGuidRef ?? Guid.Empty;
      QURC.ClientSessionGuid = qebSsnGuid;
    }
  }

  private Guid qebAgtGuid = Guid.Empty;
  public Guid QebAgentGuid
  {
    get {
      if (qebAgtGuid.IsEmpty())
      {
        qebAgtGuid = QebiExtensions.GetAgentGuid(OnlineUser);
        QURC.ClientAgentGuid = qebAgtGuid;
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
        qebSsnGuid = QebiExtensions.GetSessionGuid(OnlineUser);
        QURC.ClientSessionGuid = qebSsnGuid;
      }
      return qebSsnGuid;
    }
  }

  public QebIdentityResult QebUserSignin(string? userName, string? passWord)
  {
    return QebUserSigninAsync(userName, passWord).GetAwaiter().GetResult();
  }

  public async Task<QebIdentityResult> QebUserSigninAsync(string? userName, string? passWord)
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
    var result = await QebiExtensions.SigninUserAsync(HttpContext, userName, qebUser.UserGuidKey, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }

  public QebIdentityResult QebUserSignin(string? userName, Guid? userGuid)
  {
    return QebUserSigninAsync(userName, userGuid).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(string? userName, Guid? userGuid)
  {
    if (string.IsNullOrWhiteSpace(userName) || userGuid.IsNullOrEmpty())
    { throw new ArgumentNullException("userName or userGuid is null empty or invalid in QebUserSignin"); }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiExtensions.SigninUserAsync(HttpContext, userName, userGuid, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }
  public QebIdentityResult QebUserSignin(Guid? userGuid, Guid? agentGuid, Guid? sessionGuid)
  {
    return QebUserSigninAsync(userGuid, agentGuid, sessionGuid).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(Guid? userGuid, Guid? agentGuid, Guid? sessionGuid)
  {
    if (userGuid.IsInvalid() || agentGuid.IsInvalid() || sessionGuid.IsInvalid())
    { throw new ArgumentNullException("userGuid, agentGuid, or sessionGuid is invalid in QebUserSignin"); }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiExtensions.SigninUserAsync(HttpContext, string.Empty, userGuid, agentGuid, sessionGuid, userRoles);
    return result;
  }
  public QebIdentityResult QebUserSignin(string userName, Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles)
  {
    return QebUserSigninAsync(userName, userGuid, agentGuid, sessionGuid, userRoles).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(string userName, Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles)
  {
    var result = await QebiExtensions.SigninUserAsync(HttpContext, userName, userGuid, agentGuid, sessionGuid, userRoles);
    return result;
  }

  public void QebUserSignout()
  {
    QebUserSignoutAsync();
    return;
  }
  public async void QebUserSignoutAsync()
  {
    await QebiExtensions.SignoutUserAsync(HttpContext);
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