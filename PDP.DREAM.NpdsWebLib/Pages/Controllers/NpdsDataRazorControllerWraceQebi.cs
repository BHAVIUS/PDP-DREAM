// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract partial class QebiDataRazorPageControllerBase : WraceRazorPageControllerBase, IQebiUser
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(QebiDataRazorPageControllerBase);

  // Type QebiRoleClaimsPrincipal = QEBI RoleClaimsPrincipal (RCP)
  // wrapper for HttpContext.User available in 
  // Microsoft.AspNetCore.Mvc.ControllerBase as property User
  private QebiRoleClaimsPrincipal? qebiRcp = null;
  public QebiRoleClaimsPrincipal QebiRcp
  {
    get {
      if (qebiRcp == null)
      {
        qebiRcp = new QebiRoleClaimsPrincipal(User);
        qebiRcp.UpdateWrace(ref npdscw);
      }
      return qebiRcp;
    }
  }

  public bool AddQebiRoleByRoleName(string roleName)
  {
    var roleAdded = false;
    var userGuid = QebiRcp.UserGuid;
    var userRoles = NPDSCW.QebiDR.GetQebiUserRolesForUserGuid(userGuid);
    // idempotent add role only if not already present
    if (!userRoles.Contains(roleName))
    {

      Guid? roleGuid = NPDSCW.QebiDR.GetQebiRoleGuidByRoleName(roleName);
      if (!roleGuid.IsNullOrEmpty())
      {
        var linkGuid = PdpNewGuid();
        var appGuid = PDPSS.CiaamAppGuid;
        var errorCode = NPDSCW.QebiDR.QebiLinkEdit(linkGuid, appGuid, userGuid, roleGuid);
        if (errorCode == 0) { roleAdded = true; }
      }
    }
    return roleAdded;
  }

  public bool CheckQebiUserClient(bool userIsAgent = false)
  {
    var qebSignin = new QebIdentityResult();
    var usrSsnValid = false;
    if (QebiRcp.IsAuthenticated)
    {
      usrSsnValid = NPDSCW.CheckSessionQebiUser();
    }
    else if (!string.IsNullOrEmpty(NPDSCW.CiaamUserName) && !string.IsNullOrEmpty(NPDSCW.CiaamPassWord))
    {
      qebSignin = QebiUserSignin(NPDSCW.CiaamUserName, NPDSCW.CiaamPassWord);
      usrSsnValid = NPDSCW.CheckSessionQebiUser();
    }
#if DEBUG
    NPDSCW.DebugClientAccess(nameof(CheckQebiUserClient), rzrClass);
#endif
    return usrSsnValid;
  }

  public QebIdentityResult QebiUserSignin(string? userName, string? passWord)
  {
    return QebiUserSigninAsync(userName, passWord).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebiUserSigninAsync(string? userName, string? passWord)
  {
    var qebSignin = new QebIdentityResult();
    var qebUser = NPDSCW.QebiDR.GetUserByUserName(userName);
    if ((qebUser == null) || string.IsNullOrWhiteSpace(qebUser.UserName) || string.IsNullOrWhiteSpace(qebUser.PasswordHash))
    { qebSignin.Failed = true; return qebSignin; }
    if (qebUser.ConcurrencyStamp == PdpInvalidToken || qebUser.UserGuid.IsInvalid())
    { qebSignin.Failed = true; return qebSignin; }
    var userNameValid = (qebUser.UserName.ToLower() == userName.ToLower());
    if (!userNameValid) { qebSignin.Failed = true; return qebSignin; }
    var passWordValid = QebCryptoService.TokenEqualsHash(passWord, qebUser.PasswordHash);
    if (!passWordValid) { qebSignin.Failed = true; return qebSignin; }
    var userRoles = NPDSCW.QebiDR.GetUserRoleNamesByUserGuid(qebUser.UserGuid);
    var result = await QebiExtensions.SigninUserAsync(HttpContext,
     qebUser.RandAlias, qebUser.UserAlias, qebUser.EmailAddress, userName, qebUser.UserGuid, userRoles);
    return result;
  }

  public QebIdentityResult QebiUserSignin(
    string? randAlias, string? userAlias, string? userEmail, string? userName, Guid? userGuid, List<string> userRoles)
  {
    return QebiUserSigninAsync(randAlias, userAlias, userEmail, userName, userGuid, userRoles).GetAwaiter().GetResult();
  }
  public async Task<QebIdentityResult> QebiUserSigninAsync(
    string? randAlias, string? userAlias, string? userEmail, string? userName, Guid? userGuid, List<string> userRoles)
  {
    var result = await QebiExtensions.SigninUserAsync(HttpContext,
     randAlias, userAlias, userEmail, userName, userGuid, userRoles);
    return result;
  }

  public void QebiUserSignout()
  {
    QebiUserSignoutAsync();
    return;
  }
  public async void QebiUserSignoutAsync()
  {
    await QebiExtensions.SignoutUserAsync(HttpContext);
    return;
  }

  protected ActionResult RedirectToLocal(string returnUrl, string pathIdentProfile, string pathUserIndex)
  {
    if (IsUrlLocalToLoginDomain(returnUrl))
    {
      return Redirect(returnUrl);
    }
    else
    {
      if (QebiRcp.IsAuthenticated) { return Redirect(pathIdentProfile); }
      else { return Redirect(pathUserIndex); }
    }
  }

} // end class

// end file