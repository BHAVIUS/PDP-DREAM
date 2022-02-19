// QebIdentityControllerBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class QebIdentityControllerBase : CoreDataRestApiControllerBase
{
  // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
  // PDP REST Context = PRC for app config settings and web api requests
  // PDP User Context = PUC for secure user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  protected IEmailSender qebEmailSender;
  protected ISmsSender qebSmsSender;
  protected ILogger qebLogger;

  // QEB User Context = QUC
  // QEB Identity users/roles in QEB Identity context (via SQL views)
  protected QebIdentityContext qebUserContext;
  public QebIdentityContext QUC { get { return qebUserContext; } set { qebUserContext = value; } }


  public QebIdentityControllerBase()
  {
    qebUserContext = new QebIdentityContext(npdsSrvcDefs.NpdsUserDbconstr);
  }

  public QebIdentityControllerBase(QebIdentityContext userCntxt)
  {
    qebUserContext = userCntxt;
  }
  public QebIdentityControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserContext = userCntxt;
    qebEmailSender = emlSndr; qebSmsSender = smsSndr; qebLogger = lgrFtry.CreateLogger<QebIdentityControllerBase>();
  }


  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // new PdpRestContext() calls ParseQueryCollection
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Nexus,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
  }

  public void ResetRecordAccess()
  {
    if (PRC.ClientHasAdminAccess) { PRC.RecordAccess = NpdsConst.RecordAccess.Admin; }
    else if (PRC.ClientHasEditorAccess) { PRC.RecordAccess = NpdsConst.RecordAccess.Editor; }
    else if (PRC.ClientHasAuthorAccess) { PRC.RecordAccess = NpdsConst.RecordAccess.Author; }
    else if (PRC.ClientHasAgentAccess) { PRC.RecordAccess = NpdsConst.RecordAccess.Agent; }
    else if (PRC.ClientHasUserAccess) { PRC.RecordAccess = NpdsConst.RecordAccess.User; }
  }

  protected void UserGuidDevTest()
  {
    // test-dev-debug from ClaimsPrincipal
    var pdpUsrName = QebUserName;
    if (string.IsNullOrWhiteSpace(pdpUsrName)) { PRC.ServiceError += "PDP: PdpUserName is null, empty, or whitespace"; }
    var pdpUsrGuid = QebUserGuid;
    if (PdpGuid.IsInvalidGuid(pdpUsrGuid)) { PRC.ServiceError += "PDP: PdpUserGuid is null, empty, or invalid"; }
    // test-dev-debug from PDP REST Context
    var prcUsrGuid = PRC.UserGuid;
    if (PdpGuid.IsInvalidGuid(prcUsrGuid)) { PRC.ServiceError += "PDP: PrcUserGuid is null, empty, or invalid"; }
    var prcAgtGuid = PRC.AgentGuid;
    if (PdpGuid.IsInvalidGuid(prcAgtGuid)) { PRC.ServiceError += "PDP: PrcAgentGuid is null, empty, or invalid"; }
    var prcInfGuid = PRC.AgentInfosetGuid;
    if (PdpGuid.IsInvalidGuid(prcInfGuid)) { PRC.ServiceError += "PDP: PrcAgentInfosetGuid is null, empty, or invalid"; }
    var prcSsnGuid = PRC.SessionGuid;
    if (PdpGuid.IsInvalidGuid(prcSsnGuid)) { PRC.ServiceError += "PDP: PrcAgentSessionGuid is null, empty, or invalid"; }
    // error check reports to PDP REST Context
    if ((pdpUsrGuid == Guid.Empty) && (prcUsrGuid == Guid.Empty)) { PRC.ServiceError += "PDP: both ClaimsPrincipal and RestContext UserGuids are null or empty"; }
    if (pdpUsrGuid != prcUsrGuid) { PRC.ServiceError += "PDP: UserGuids from ClaimsPrincipal and RestContext are not equal"; }
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

  public bool OnlineUserIsIdentified
  {
    get {
      if (OnlineUser == null) { onlinUsrIdent = false; }
      else if (OnlineUser.Identities != null)
      {
        onlinUsrIdent = OnlineUser.Identities
        .Any(i => (i.AuthenticationType == PdpConst.PdpIdentityScheme));

      }
      return onlinUsrIdent;
    }
  }
  private bool onlinUsrIdent = false;

  public bool OnlineUserIsAuthenticated
  {
    get {
      onlinUsrAuth = OnlineUser.Identity.IsAuthenticated;
      PRC.ClientIsAuthenticated = onlinUsrAuth;
      PRC.UserName = QebUserName;
      PRC.UserGuid = QebUserGuid;
      PRC.AgentGuid = QebAgentGuid;
      PRC.SessionGuid = QebSessionGuid;
      return onlinUsrAuth;
    }
  }
  private bool onlinUsrAuth = false;

  public string OnlineUserName
  {
    get { return QebUserName; }
  }
  protected string QebUserName
  {
    get {
      if (string.IsNullOrEmpty(qebUsrName))
      {
        qebUsrName = QebiUserExtensions.GetUserName(OnlineUser);
        PRC.UserName = qebUsrName;
      }
      return qebUsrName;
    }
  }
  private string qebUsrName = "";

  public Guid OnlineUserGuid
  {
    get { return QebUserGuid; }
  }
  protected Guid QebUserGuid
  {
    get {
      if (qebUsrGuid.IsEmpty())
      {
        qebUsrGuid = QebiUserExtensions.GetUserGuid(OnlineUser);
        PRC.UserGuid = qebUsrGuid;
      }
      return qebUsrGuid;
    }
  }
  private Guid qebUsrGuid = Guid.Empty;


  protected QebIdentityAppUser QebUser
  {
    get {
      return qebUsr;
    }
    set {
      qebUsr = value;
      qebUsrGuid = qebUsr.UserGuidKey;
      PRC.UserGuid = qebUsrGuid;
      qebUsrName = qebUsr.UserName;
      PRC.UserName = qebUsrName;
      qebSsnGuid = qebUsr.SessionGuidRef ?? Guid.Empty;
      PRC.SessionGuid = qebSsnGuid;
    }
  }
  private QebIdentityAppUser? qebUsr = null;

  public Guid QebAgentGuid
  {
    get {
      if (qebAgtGuid.IsEmpty())
      {
        qebAgtGuid = QebiUserExtensions.GetAgentGuid(OnlineUser);
        PRC.AgentGuid = qebAgtGuid;
      }
      return qebAgtGuid;
    }
  }
  private Guid qebAgtGuid = Guid.Empty;

  public Guid QebSessionGuid
  {
    get {
      if (qebSsnGuid.IsEmpty())
      {
        qebSsnGuid = QebiUserExtensions.GetSessionGuid(OnlineUser);
        PRC.SessionGuid = qebSsnGuid;
      }
      return qebSsnGuid;
    }
  }
  private Guid qebSsnGuid = Guid.Empty;

  public PdpIdentityResult QebUserSignin(string userName, string passWord)
  {
    return QebUserSigninAsync(userName, passWord).GetAwaiter().GetResult();
  }

  public async Task<PdpIdentityResult> QebUserSigninAsync(string userName, string passWord)
  {
    var pdpSignin = new PdpIdentityResult();
    var qebUser = QUC.GetUserByUserName(userName);
    if ((qebUser == null) || string.IsNullOrWhiteSpace(qebUser.UserName) || string.IsNullOrWhiteSpace(qebUser.PasswordHash))
    { pdpSignin.Failed = true; return pdpSignin; }
    if (qebUser.ConcurrencyStamp == PdpConst.PdpInvalidToken || qebUser.UserGuidKey.IsInvalid())
    { pdpSignin.Failed = true; return pdpSignin; }
    var userNameOk = (qebUser.UserName.ToLower() == userName.ToLower());
    if (!userNameOk) { pdpSignin.Failed = true; return pdpSignin; }
    var passWordOk = PdpCryptoService.VerifyHashedToken(qebUser.PasswordHash, passWord);
    if (!passWordOk) { pdpSignin.Failed = true; return pdpSignin; }
    var userRoles = QUC.GetUserRoleNamesByUserGuid(qebUser.UserGuidKey);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, userName, qebUser.UserGuidKey, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }

  public PdpIdentityResult QebUserSignin(string userName, Guid userGuid)
  {
    return QebUserSigninAsync(userName, userGuid).GetAwaiter().GetResult();
  }
  public async Task<PdpIdentityResult> QebUserSigninAsync(string userName, Guid userGuid)
  {
    if (string.IsNullOrWhiteSpace(userName) || userGuid.IsEmpty())
    { throw new ArgumentNullException("userName or userGuid is null empty or invalid in QebUserSignin"); }
    var userRoles = QUC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, userName, userGuid, Guid.Empty, Guid.Empty, userRoles);
    return result;
  }
  public PdpIdentityResult QebUserSignin(Guid userGuid, Guid agentGuid, Guid sessionGuid)
  {
    return QebUserSigninAsync(userGuid, agentGuid, sessionGuid).GetAwaiter().GetResult();
  }
  public async Task<PdpIdentityResult> QebUserSigninAsync(Guid userGuid, Guid agentGuid, Guid sessionGuid)
  {
    if (userGuid.IsInvalid() || agentGuid.IsInvalid() || sessionGuid.IsInvalid())
    { throw new ArgumentNullException("userGuid, agentGuid, or sessionGuid is invalid in QebUserSignin"); }
    var userRoles = QUC.GetUserRoleNamesByUserGuid(userGuid);
    var result = await QebiUserExtensions.SigninUserAsync(HttpContext, string.Empty, userGuid, agentGuid, sessionGuid, userRoles);
    return result;
  }
  public async Task<PdpIdentityResult> QebUserSigninAsync(string userName, Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles)
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

  protected void AddErrors(IEnumerable<PdpIdentityError> errorList)
  {
    foreach (var error in errorList)
    {
      ModelState.AddModelError("PdpIdentity", error.Message);
    }
  }

  protected bool IsTokenValid(DateTime? token)
  {
    var current = DateTime.UtcNow;
    var expired = Convert.ToDateTime(token ?? DateTime.MinValue);
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

  protected string LinkToConfirmToken(string id, string ct, string namAction, string namController, string namArea = "")
  {
    if (namArea == null) { namArea = PdpConst.PdpMvcArea; }
    // TODO: compare PrcOptionsForRequest and refactor/encapsulate standard parameters for request url
    var req = HttpContext.Request;
    var prot = req.Scheme;
    var site = req.Host;
    var path = req.Path;
    if (string.IsNullOrEmpty(namArea))
    {
      path = $"/{namController}/{namAction}";
    }
    else
    {
      path = $"/{namArea}/{namController}/{namAction}";
    }
    var link = $"{prot}://{site}{path}?id={id}&ct={ct}";
    return link;
  }

  public string AppendKeys(string pathKeys, Guid ssnKey, Guid agtKey, Guid usrKey)
  {
    return AppendKeys(pathKeys, ssnKey.ToString(), agtKey.ToString(), usrKey.ToString());
  }
  public string AppendKeys(string pathKeys, string ssnKey, string agtKey, string usrKey)
  {
    if (pathKeys == null) { pathKeys = string.Empty; }
    if (!pathKeys.Contains(QuestionChar)) { pathKeys = pathKeys + QuestionChar; }
    if (!PdpGuid.IsInvalidGuid(ssnKey)) { pathKeys = pathKeys + AndChar + NpdsConst.QskSessionKey + EqualChar + ssnKey; }
    if (!PdpGuid.IsInvalidGuid(agtKey)) { pathKeys = pathKeys + AndChar + NpdsConst.QskAgentKey + EqualChar + agtKey; }
    if (!PdpGuid.IsInvalidGuid(usrKey)) { pathKeys = pathKeys + AndChar + NpdsConst.QskUserKey + EqualChar + usrKey; }
    return pathKeys;
  }

} // class
