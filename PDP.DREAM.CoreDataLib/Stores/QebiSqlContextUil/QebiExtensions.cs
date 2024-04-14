// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static class QebiExtensions
{
  // using System.Security.Principal.IPrincipal
  // private IPrincipal pdpPrincipal; // with readonly Identity (type IIdentity) and IsInRole (type bool) for current user
  // using System.Security.Principal.IIdentity
  // private IIdentity pdpIdentity; // with readonly AuthenticationType (string), IsAuthenticated (bool), Name (string) for current user

  public static string GetUserAlias(this QebiRcp principal)
  {
    string userAlias;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for UserAlias"); }
    userAlias = principal.FindFirstValue(QebiClaimTypes.UserAlias);
    return userAlias;
  }

  public static string GetUserEmail(this QebiRcp principal)
  {
    string userAlias;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for UserEmail"); }
    userAlias = principal.FindFirstValue(QebiClaimTypes.UserEmail);
    return userAlias;
  }

  public static string GetUserName(this QebiRcp principal)
  {
    string userName;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for UserName"); }
    userName = principal.FindFirstValue(QebiClaimTypes.UserName);
    return userName;
  }

  public static Guid GetUserGuid(this QebiRcp principal)
  {
    Guid userGuid;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for UserGuid"); }
    var strGuid = principal.FindFirstValue(QebiClaimTypes.UserGuid);
    Guid.TryParse(strGuid, out userGuid);
    return userGuid;
  }

  public static Guid GetAgentGuid(this QebiRcp principal)
  {
    Guid agentGuid;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for AgentGuid"); }
    var strGuid = principal.FindFirstValue(QebiClaimTypes.AgentGuid);
    Guid.TryParse(strGuid, out agentGuid);
    return agentGuid;
  }

  public static Guid GetSessionGuid(this QebiRcp principal)
  {
    Guid sessionGuid;
    if (principal == null) { throw new ArgumentNullException("QEBI RoleClaimsPrincipal is null for SessionGuid"); }
    var strGuid = principal.FindFirstValue(QebiClaimTypes.SessionGuid);
    Guid.TryParse(strGuid, out sessionGuid);
    return sessionGuid;
  }

  public static async Task SignoutUserAsync(this HttpContext cntxt, bool useAwaiter = false)
  {
    var authMethod = PdpIdentityScheme;
    await cntxt.SignOutAsync(authMethod).ConfigureAwait(useAwaiter);
    return;
  }
  //public static async Task SignoutUserAsync(this HttpContext cntxt, string authMethod, bool useAwaiter = false)
  //{
  //  if (string.IsNullOrEmpty(authMethod)) { authMethod = PdpIdentityScheme; }
  //  await cntxt.SignOutAsync(authMethod).ConfigureAwait(useAwaiter);
  //  return;
  //}

  public static async Task<QebIdentityResult> SigninUserAsync(this HttpContext cntxt,
    string? userAlias, string? userEmail, string? userName,
    Guid? userGuid, List<string> userRoles, bool useAwaiter = false)
  {
    var authProps = new AuthenticationProperties();
    var authMethod = PdpIdentityScheme;
    var authPrincipal = CreateUserPrincipal(userAlias, userEmail, userName,
      userGuid.Value, userRoles, authMethod);
    await cntxt.SignInAsync(authMethod, authPrincipal, authProps).ConfigureAwait(useAwaiter);
    var result = authPrincipal.CheckUserPrincipal(); // recheck current online user
    return result;
  }

  public static QebiRcp CreateUserPrincipal(
    string? userAlias, string? userEmail, string? userName,
    Guid? userGuid, List<string> userRoles, string authMethod)
  {
    if (string.IsNullOrEmpty(authMethod)) { authMethod = PdpIdentityScheme; }
    // TODO: migrate to QebiClaimTypes only
    var authMethodClaim = new Claim(ClaimTypes.AuthenticationMethod, authMethod);
    // var principalClaim = new Claim(ClaimTypes.Name, userName);
    var userNameClaim = new Claim(QebiClaimTypes.UserName, userName);
    var userAliasClaim = new Claim(QebiClaimTypes.UserAlias, userAlias);
    var userEmailClaim = new Claim(QebiClaimTypes.UserEmail, userEmail);
    var userGuidClaim = new Claim(QebiClaimTypes.UserGuid, userGuid.ToString());

    var allClaims = new List<Claim>
      {
        authMethodClaim,
        userNameClaim, userAliasClaim, userEmailClaim, userGuidClaim,
      };

    if ((userRoles != null) && (userRoles.Count > 0))
    {
      foreach (var userRole in userRoles)
      {
        var userRoleClaim = new Claim(ClaimTypes.Role, userRole);
        allClaims.Add(userRoleClaim);
      }
    }
    var userIdentity = new ClaimsIdentity(allClaims, authMethod);
    var userPrincipal = new QebiRcp(userIdentity);
    return userPrincipal;
  }

  public static QebIdentityResult CheckUserPrincipal(this QebiRcp userPrincipal)
  {
    var result = new QebIdentityResult();
    if (userPrincipal.Identity.IsAuthenticated) { result.Succeeded = true; }
    else { result.Failed = true; }
    return result;
  }

}
