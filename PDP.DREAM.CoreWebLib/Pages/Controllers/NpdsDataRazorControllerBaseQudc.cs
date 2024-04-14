// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract partial class QebiDataRazorPageControllerBase : WraceRazorPageControllerBase, IQebiUser
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(QebiDataRazorPageControllerBase);

  // Web API REST Controller Environment = WRACE for user config settings and web api requests/responses
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PCDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // QEBI User Data Context = QUDC
  protected const string QudcKey = nameof(QUDC);
  protected QebiDalContext? qebiUserDataCntxt = null;
  public QebiDalContext? QUDC
  {
    get { return qebiUserDataCntxt; }
  }

#if DEBUG
  protected void CatchNullQebi(string methodName = "", string className = "")
  {
    QUDC.CatchNullObject(QudcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullQebi)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"QUDC DatabaseType: {QUDC.NPDSDC.DatabaseType}");
    Debug.WriteLine($"QUDC DatabaseConstr: {QUDC.NPDSDC.DatabaseConstr}");
  }
  protected void DebugQebiRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugQebiRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{WRACE.SearchFilter}/{WRACE.ServiceTag}/{WRACE.ServiceType}/{WRACE.EntityType}/{WRACE.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"WRACE CoreDbconstr: {WRACE.DbcnstrCore}");
    Debug.WriteLine($"WRACE DatabaseConstr: {WRACE.DatabaseConstr}");
    Debug.WriteLine($"QUDC DatabaseType: {QUDC.NPDSDC.DatabaseType}");
    Debug.WriteLine($"QUDC DatabaseConstr: {QUDC.NPDSDC.DatabaseConstr}");
  }
#endif

  public void ResetQebiRepository(bool openCnctn = true, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetQebiRepository);
    CatchNullWrace(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (WRACE.DatabaseType != NPDSCD.DatabaseTypeQEBI)
    { WRACE.DatabaseType = NPDSCD.DatabaseTypeQEBI; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { WRACE.DbcnstrQebi = dbcs; }
    // reset NPDS data context with current INpdsClient from WRACE
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)WRACE);
    // open connection if switched on
    if (openCnctn)
    { qebiUserDataCntxt.DbsqlConnect(); }
    // update NPDS data context in WRACE 
    WRACE.DbciQebi = (INpdsDbsqlContext)qebiUserDataCntxt;
#if DEBUG
    CatchNullQebi(rzrMethod, rzrClass);
#endif
  }
  public void OpenQebiConnection(bool resetRepo = false)
  {
    if (resetRepo) { ResetQebiRepository(false); }
    qebiUserDataCntxt.DbsqlConnect();
  }
  public void CloseQebiConnection()
  {
    qebiUserDataCntxt.DbsqlDisconnect();
  }

  public bool AddQebiRoleByRoleName(string roleName)
  {
    var roleAdded = false;
    var userGuid = QebRcp.UserGuid;
    var userRoles = QUDC.GetQebiUserRolesForUserGuid(userGuid);
    if (!userRoles.Contains(roleName))
    {
      Guid? roleGuid = QUDC.GetQebiRoleGuidByRoleName(roleName);
      if (!roleGuid.IsNullOrEmpty())
      {
        var linkGuid = PdpNewGuid();
        var appGuid = PDPSS.CiaamAppGuid;
        var errorCode = QUDC.QebiLinkEdit(linkGuid, appGuid, userGuid, roleGuid);
        if (errorCode == 0) { roleAdded = true; }
      }
    }
    return roleAdded;
  }

  public bool CheckQebiUserSession(bool userIsAgent = false)
  {
    var qebSignin = new QebIdentityResult();
    var usrSsnValid = false;
    if (QebRcp.IsAuthenticated)
    {
#if DEBUG
      var usrModeReq = wrace.UserModeClientRequired;
      var ssnReq = wrace.SessionClientRequired;
#endif
      usrSsnValid = QUDC.CheckSessionQebiUser(ref wrace);
#if DEBUG
      usrModeReq = wrace.UserModeClientRequired;
      ssnReq = wrace.SessionClientRequired;
      var isAuth = wrace.ClientIsAuthenticated;
      var isUser = wrace.ClientIsUser;
#endif
    }
    else if (!string.IsNullOrEmpty(WRACE.CiaamUserName) && !string.IsNullOrEmpty(WRACE.CiaamPassWord))
    {
      qebSignin = QebUserSignin(WRACE.CiaamUserName, WRACE.CiaamPassWord);
      usrSsnValid = QUDC.CheckSessionQebiUser(ref wrace);
    }
#if DEBUG
    WraceDevTest();
#endif
    return usrSsnValid;
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
    if (qebUser.ConcurrencyStamp == PdpInvalidToken || qebUser.UserGuid.IsInvalid())
    { qebSignin.Failed = true; return qebSignin; }
    var userNameValid = (qebUser.UserName.ToLower() == userName.ToLower());
    if (!userNameValid) { qebSignin.Failed = true; return qebSignin; }
    var passWordValid = QebCryptoService.TokenEqualsHash(passWord, qebUser.PasswordHash);
    if (!passWordValid) { qebSignin.Failed = true; return qebSignin; }
    var userRoles = QUDC.GetUserRoleNamesByUserGuid(qebUser.UserGuid);
    var result = await QebiExtensions.SigninUserAsync(HttpContext,
      qebUser.UserAlias, qebUser.EmailAddress,
      userName, qebUser.UserGuid, userRoles);
    return result;
  }

  public QebIdentityResult QebUserSignin(
    string? userAlias, string? userEmail, string? userName,
    Guid? userGuid, List<string> userRoles)
  {
    return QebUserSigninAsync(userAlias, userEmail, userName,
      userGuid, userRoles).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebUserSigninAsync(
    string? userAlias, string? userEmail, string? userName,
    Guid? userGuid, List<string> userRoles)
  {
    var result = await QebiExtensions.SigninUserAsync(HttpContext,
      userAlias, userEmail, userName, userGuid, userRoles);
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

  // Type QebiRcp = QEBI RoleClaimsPrincipal (RCP)
  // wrapper for HttpContext.User available in 
  // Microsoft.AspNetCore.Mvc.ControllerBase as property User
  private QebiRcp? qebRcp = null;
  public QebiRcp QebRcp
  {
    get {
      if (qebRcp == null)
      {
        qebRcp = new QebiRcp(User);
        qebRcp.UpdateWrace(ref wrace);
      }
      return qebRcp;
    }
  }

  protected ActionResult RedirectToLocal(string returnUrl, string pathIdentProfile, string pathUserIndex)
  {
    if (IsUrlLocalToLoginDomain(returnUrl))
    {
      return Redirect(returnUrl);
    }
    else
    {
      if (QebRcp.IsAuthenticated) { return Redirect(pathIdentProfile); }
      else { return Redirect(pathUserIndex); }
    }
  }

} // end class

// end file