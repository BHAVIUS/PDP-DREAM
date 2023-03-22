// QebiDbsqlContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.QebiDbsqlContext
public partial class QebiDbsqlContext : DbsqlContextBase, INpdsDataService, IQebiDbsqlContext
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSCP == null) { NPDSCP = new NpdsClient(NpdsDatabaseType.SIAA); }
    // if (!string.IsNullOrEmpty(NPDSCP.DatabaseConstr)) { NPDSCP.QebiDbconstr = NPDSCP.DatabaseConstr; }
    if (!dbcob.IsConfigured) { dbcob.UseSqlServer(NPDSCP.DatabaseConstr); }
    if (dbcob.IsConfigured)
    {
      if (NPDSCP.ClientAppSiaaGuid.IsNullOrEmpty() && !PDPSS.AppSecureUiaaGuid.IsEmpty())
      { NPDSCP.ClientAppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
    }
    else
    {
      throw new Exception("DbContext builder with NPDSCP has not been configured");
    }
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public QebiDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public QebiDbsqlContext(DbContextOptionsBuilder<QebiDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public QebiDbsqlContext(INpdsClient npdsc) : base(npdsc) { AssureAppGuid(); }
  public QebiDbsqlContext() : this(new NpdsClient(NpdsDatabaseType.SIAA)) { AssureAppGuid(); }

  protected void AssureAppGuid()
  {
    var guidExists = QebiContextHasAppGuid();
    if (!guidExists) { Debug.WriteLine("QebiDbsqlContext does not have AppSecureUiaaGuid"); }
  }
  public bool QebiContextHasAppGuid()
  {
    var dbcHasAppGuid = false;
    if (PDPSS is null) { return dbcHasAppGuid; }
    var userApp = this.GetAppByAppName(PDPSS.AppSecureUiaaName);
    if (userApp == null) { return dbcHasAppGuid; }
    if (!userApp.AppGuidKey.IsEmpty()) { PDPSS.AppSecureUiaaGuid = userApp.AppGuidKey; }
    if (PDPSS.AppSecureUiaaGuid.IsEmpty()) { return dbcHasAppGuid; }
    if (NPDSCP.ClientAppSiaaGuid.IsNullOrEmpty()) { NPDSCP.ClientAppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
    if (!NPDSCP.ClientAppSiaaGuid.IsNullOrEmpty()) { dbcHasAppGuid = true; }
    return dbcHasAppGuid;
  }

  public QebIdentityApp GetAppByAppName(string appName)
  {
    var qebApp = new QebIdentityApp();
    if (!string.IsNullOrEmpty(appName))
    {
      try
      {
        qebApp = this.QebIdentityApps.SingleOrDefault(a =>
        (a.AppName.ToUpper() == appName.ToUpper()));
      }
      catch (Exception exc)
      {
        qebApp.ConcurrencyStamp = LinqErrorMessage(exc);
      }
    }
    return qebApp;
  }
  public List<QebIdentityAppRole> GetAppRolesByAppGuid()
  {
    var appRoles = new List<QebIdentityAppRole>();
    try
    {
      appRoles = this.QebIdentityAppRoles.Where(r =>
        (r.AppGuidRef == NPDSCP.ClientAppSiaaGuid)).ToList();
    }
    catch (Exception exc)
    {
      var appRole = new QebIdentityAppRole()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      appRoles.Add(appRole);
    }
    return appRoles;
  }
  public List<QebIdentityAppUser> GetAppUsersByAppGuid()
  {
    var appUsers = new List<QebIdentityAppUser>();
    try
    {
      appUsers = this.QebIdentityAppUsers.Where(u =>
        (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid)).ToList();
    }
    catch (Exception exc)
    {
      var appUser = new QebIdentityAppUser()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      appUsers.Add(appUser);
    }
    return appUsers;
  }

  public int CountUsersByUserName(string userName)
  {
    int count;
    count = this.QebIdentityAppUsers.Where(u =>
    (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
    (u.UserName.ToLower() == userName.ToLower())).Count();
    return count;
  }

  public QebIdentityAppUser GetUserByPrincipal(ClaimsPrincipal principal)
  {
    var qebUser = new QebIdentityAppUser();
    if (principal == null) { throw new ArgumentNullException("principal is null in GetUserByPrincipal"); }
    string username = principal.FindFirstValue(QebiClaimTypes.UserName);
    if (!string.IsNullOrWhiteSpace(username))
    {
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
      }
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByPassWord(string password)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByPassWordAndToken(string password, string token)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash) &&
       QebCryptoService.TokenEqualsToken(token, u.SecurityToken)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByPassWordAndAnswer(string password, string answer)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash) &&
       QebCryptoService.TokenEqualsToken(answer, u.SecurityAnswer)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByUserName(string username)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
       ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
       (u.UserName.ToUpper() == username.ToUpper())));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndToken(string username, string token)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.AsEnumerable().SingleOrDefault(u =>
        ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
        (u.UserName.ToUpper() == username.ToUpper()) &&
        QebCryptoService.TokenEqualsToken(token, u.SecurityToken)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndAnswer(string username, string answer)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.AsEnumerable().SingleOrDefault(u =>
        ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
        (u.UserName.ToUpper() == username.ToUpper()) &&
        QebCryptoService.TokenEqualsToken(answer, u.SecurityAnswer)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByUserGuid(string? userguid)
  {
    return GetUserByUserGuid(PdpGuid.ParseToNonNullable(userguid));
  }
  public QebIdentityAppUser GetUserByUserGuid(Guid? userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
        ((u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
        (u.UserGuidKey == userguid)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndUserGuid(string? username, Guid? userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()) &&
         (u.UserGuidKey == userguid));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public List<QebIdentityAppUserRole> GetUserRolesByUserGuid(Guid? userguid)
  {
    var userRoles = new List<QebIdentityAppUserRole>();
    try
    {
      userRoles = this.QebIdentityAppUserRoles.Where(u =>
        (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
        (u.UserGuidRef == userguid)).ToList();
    }
    catch (Exception exc)
    {
      var userRole = new QebIdentityAppUserRole()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      userRoles.Add(userRole);
    }
    return userRoles;
  }
  public List<string> GetUserRoleNamesByUserGuid(Guid? userguid)
  {
    var roleNames = new List<string>();
    roleNames = this.QebIdentityAppUserRoles.Where(u =>
      (u.AppGuidRef == NPDSCP.ClientAppSiaaGuid) &&
      (u.UserGuidRef == userguid))
      .Select(r => r.RoleName).Distinct().ToList();
    return roleNames;
  }

} // end class

// end file