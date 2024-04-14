// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.QebiDbsqlContext
// public partial class QebiDalContext : PdpDbsqlContextBase, IQebiDalContext
public partial class QebiDalContext : PdpDbsqlContextBase, IQebiDalContext
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  // ATTN: if use Entity Developer with base-derived class, then must delete generated redundant copy of HasChanges()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSDC == null) { NPDSDC = new NpdsClientWrace(NPDSCD.DatabaseTypeQEBI); }
    NPDSDC.DbcontextIsConfigured(dbcob);
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public QebiDalContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public QebiDalContext(DbContextOptionsBuilder<QebiDalContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public QebiDalContext(INpdscwClient npdsc) : base(npdsc) { AssureAppGuid(); }
  public QebiDalContext() : this(new NpdsClientWrace(NPDSCD.DatabaseTypeQEBI)) { AssureAppGuid(); }

  protected void AssureAppGuid()
  {
    var guidExists = QebiContextHasAppGuid();
#if DEBUG
    if (!guidExists) { Debug.WriteLine("QebiDbsqlContext does not have AppSecureUiaaGuid"); }
#endif
  }
  public bool QebiContextHasAppGuid()
  {
    var dbcHasAppGuid = false;
    if (PDPSS is null) { return dbcHasAppGuid; }
    var userApp = this.GetAppByAppName(PDPSS.AppSecureCiaamName);
    if (userApp == null) { return dbcHasAppGuid; }
    if (!userApp.AppGuid.IsEmpty()) { PDPSS.CiaamAppGuid = userApp.AppGuid; }
    if (PDPSS.CiaamAppGuid.IsEmpty()) { return dbcHasAppGuid; }
    if (NPDSDC.CiaamAppGuid.IsNullOrEmpty()) { NPDSDC.CiaamAppGuid = PDPSS.CiaamAppGuid; }
    if (!NPDSDC.CiaamAppGuid.IsNullOrEmpty()) { dbcHasAppGuid = true; }
#if DEBUG
    Debug.WriteLine($"AppSecureCiaamName = '{PDPSS.AppSecureCiaamName}', AppSecureCiaamGuid = '{PDPSS.CiaamAppGuid}', dbcHasAppGuid = {dbcHasAppGuid}");
#endif
    return dbcHasAppGuid;
  }

  public QebiApp GetAppByAppName(string appName)
  {
    var qebApp = new QebiApp();
    if (!string.IsNullOrEmpty(appName))
    {
      try
      {
        qebApp = this.QebiApps.SingleOrDefault(a =>
        (a.AppName.ToUpper() == appName.ToUpper()));
      }
      catch (Exception exc)
      {
        qebApp.ConcurrencyStamp = LinqErrorMessage(exc);
      }
    }
    return qebApp;
  }
  public List<QebiAppRole> GetAppRolesByAppGuid()
  {
    var appRoles = new List<QebiAppRole>();
    try
    {
      appRoles = this.QebiAppRoles.Where(r =>
        (r.AppGuid == NPDSDC.CiaamAppGuid)).ToList();
    }
    catch (Exception exc)
    {
      var appRole = new QebiAppRole()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      appRoles.Add(appRole);
    }
    return appRoles;
  }
  public List<QebiUser> GetAppUsersByAppGuid()
  {
    var appUsers = new List<QebiUser>();
    try
    {
      appUsers = this.QebiUsers.Where(u =>
        (u.AppGuid == NPDSDC.CiaamAppGuid)).ToList();
    }
    catch (Exception exc)
    {
      var appUser = new QebiUser()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      appUsers.Add(appUser);
    }
    return appUsers;
  }

  public int CountUsersByUserName(string userName)
  {
    int count;
    count = this.QebiUsers.Where(u =>
    (u.AppGuid == NPDSDC.CiaamAppGuid) &&
    (u.UserName.ToLower() == userName.ToLower())).Count();
    return count;
  }

  public QebiUser GetUserByPrincipal(QebiRcp principal)
  {
    var qebUser = new QebiUser();
    if (principal == null) { throw new ArgumentNullException("principal is null in GetUserByPrincipal"); }
    string username = principal.FindFirstValue(QebiClaimTypes.UserName);
    if (!string.IsNullOrWhiteSpace(username))
    {
      try
      {
        qebUser = this.QebiUsers.SingleOrDefault(u =>
         (u.AppGuid == NPDSDC.CiaamAppGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
      }
    }
    return qebUser;
  }

  public QebiUser GetUserByPassWord(string password)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebiUser GetUserByPassWordAndToken(string password, string token)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash) &&
       QebCryptoService.TokenEqualsToken(token, u.SecurityToken)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebiUser GetUserByPassWordAndAnswer(string password, string answer)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.AsEnumerable().SingleOrDefault(u =>
       ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
       QebCryptoService.TokenEqualsHash(password, u.PasswordHash) &&
       QebCryptoService.TokenEqualsToken(answer, u.SecurityAnswer)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebiUser GetUserByUserName(string username)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.SingleOrDefault(u =>
       ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
       (u.UserName.ToUpper() == username.ToUpper())));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebiUser GetUserByUserNameAndToken(string username, string token)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.AsEnumerable().SingleOrDefault(u =>
        ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
        (u.UserName.ToUpper() == username.ToUpper()) &&
        QebCryptoService.TokenEqualsToken(token, u.SecurityToken)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebiUser GetUserByUserNameAndAnswer(string username, string answer)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.AsEnumerable().SingleOrDefault(u =>
        ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
        (u.UserName.ToUpper() == username.ToUpper()) &&
        QebCryptoService.TokenEqualsToken(answer, u.SecurityAnswer)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebiUser GetUserByUserGuid(string? userguid)
  {
    return GetUserByUserGuid(PdpGuid.ParseToNonNullable(userguid));
  }
  public QebiUser GetUserByUserGuid(Guid? userguid)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.SingleOrDefault(u =>
        ((u.AppGuid == NPDSDC.CiaamAppGuid) &&
        (u.UserGuid == userguid)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebiUser GetUserByUserNameAndUserGuid(string? username, Guid? userguid)
  {
    var qebUser = new QebiUser();
    try
    {
      qebUser = this.QebiUsers.SingleOrDefault(u =>
         (u.AppGuid == NPDSDC.CiaamAppGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()) &&
         (u.UserGuid == userguid));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public List<QebiUserRole> GetUserRolesByUserGuid(Guid? userguid)
  {
    var userRoles = new List<QebiUserRole>();
    try
    {
      userRoles = this.QebiUserRoles.Where(u =>
        (u.AppGuid == NPDSDC.CiaamAppGuid) &&
        (u.UserGuid == userguid)).ToList();
    }
    catch (Exception exc)
    {
      var userRole = new QebiUserRole()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      userRoles.Add(userRole);
    }
    return userRoles;
  }
  public List<string> GetUserRoleNamesByUserGuid(Guid? userguid)
  {
    var roleNames = new List<string>();
    roleNames = this.QebiUserRoles.Where(u =>
      (u.AppGuid == NPDSDC.CiaamAppGuid) &&
      (u.UserGuid == userguid))
      .Select(r => r.RoleName).Distinct().ToList();
    return roleNames;
  }

} // end class

// end file