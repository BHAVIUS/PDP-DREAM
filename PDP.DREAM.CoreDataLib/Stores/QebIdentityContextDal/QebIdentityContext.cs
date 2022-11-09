// QebIdentityContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityContext : PdpDataContext, IQebIdentityContext
{
  // partial void OnCreated for use with Devart Entity Developer generated code
  partial void OnCreated()
  {
    if (base.AppSiaaGuid.IsEmpty()) { base.AppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
  }

  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  // partial void CustomizeConfiguration for use with Devart Entity Developer generated code
  partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured ||
        (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      optionsBuilder.UseSqlServer(NPDSSD.NpdsUserDbconstr);
    }
  }

  protected void OnInitiatingPdpdc(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsUserDbconstr; } // for User Service
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<QebIdentityContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(SqlConnection? dbconn)
  {
    if (dbconn == null) { throw new NullReferenceException("dbconn null or empty"); }
    var builder = new DbContextOptionsBuilder<CoreDbsqlContext>();
    builder.UseSqlServer(dbconn);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(DbContextOptions<QebIdentityContext> dbco)
  {
    if (dbco == null) { throw new NullReferenceException("dbco null or empty"); }
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public QebIdentityContext(DbContextOptions<QebIdentityContext> dbco) : base()
  {
    OnInitiatingPdpdc(dbco);
  }
  // constructor with typed Microsoft.Data.SqlClient.SqlConnection
  public QebIdentityContext(SqlConnection? dbconn) : base()
  {
    OnInitiatingPdpdc(dbconn);
  }
  // constructor with string intended for the database connection
  public QebIdentityContext(string dbcs) : base()
  {
    OnInitiatingPdpdc(dbcs);
  }
  // constructor without any parameter
  public QebIdentityContext() : base()
  {
    OnInitiatingPdpdc("");
  }

  public bool DataContextHasAppGuid()
  {
    var dbHasAppGuid = false;
    if (PDPSS is null) { return dbHasAppGuid; }
    var userApp = this.GetAppByAppName(PDPSS.AppSecureUiaaName);
    if (userApp == null) { return dbHasAppGuid; }
    if (!userApp.AppGuidKey.IsEmpty()) { PDPSS.AppSecureUiaaGuid = userApp.AppGuidKey; }
    if (PDPSS.AppSecureUiaaGuid.IsEmpty()) { return dbHasAppGuid; }
    if (AppSiaaGuid.IsEmpty()) { AppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
    if (!AppSiaaGuid.IsEmpty()) { dbHasAppGuid = true; }
    return dbHasAppGuid;
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
        (r.AppGuidRef == AppSiaaGuid)).ToList();
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
        (u.AppGuidRef == AppSiaaGuid)).ToList();
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
    (u.AppGuidRef == AppSiaaGuid) && (u.UserName.ToLower() == userName.ToLower())).Count();
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
         (u.AppGuidRef == AppSiaaGuid) && (u.UserName.ToUpper() == username.ToUpper()));
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
       ((u.AppGuidRef == AppSiaaGuid) &&
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
       ((u.AppGuidRef == AppSiaaGuid) &&
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
       ((u.AppGuidRef == AppSiaaGuid) &&
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
       ((u.AppGuidRef == AppSiaaGuid) &&
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
        ((u.AppGuidRef == AppSiaaGuid) &&
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
        ((u.AppGuidRef == AppSiaaGuid) &&
        (u.UserName.ToUpper() == username.ToUpper()) &&
        QebCryptoService.TokenEqualsToken(answer, u.SecurityAnswer)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByUserGuid(string userguid)
  {
    return GetUserByUserGuid(PdpGuid.ParseToNonNullable(userguid));
  }
  public QebIdentityAppUser GetUserByUserGuid(Guid userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
        ((u.AppGuidRef == AppSiaaGuid) &&
        (u.UserGuidKey == userguid)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndUserGuid(string username, Guid userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == AppSiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()) &&
         (u.UserGuidKey == userguid));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public List<QebIdentityAppUserRole> GetUserRolesByUserGuid(Guid userguid)
  {
    var userRoles = new List<QebIdentityAppUserRole>();
    try
    {
      userRoles = this.QebIdentityAppUserRoles.Where(u =>
        (u.AppGuidRef == AppSiaaGuid) && (u.UserGuidRef == userguid)).ToList();
    }
    catch (Exception exc)
    {
      var userRole = new QebIdentityAppUserRole()
      { ConcurrencyStamp = LinqErrorMessage(exc) };
      userRoles.Add(userRole);
    }
    return userRoles;
  }
  public List<string> GetUserRoleNamesByUserGuid(Guid userguid)
  {
    var roleNames = new List<string>();
    roleNames = this.QebIdentityAppUserRoles.Where(u =>
      (u.AppGuidRef == AppSiaaGuid) && (u.UserGuidRef == userguid))
      .Select(r => r.RoleName).Distinct().ToList();
    return roleNames;
  }

} // end class

// end file