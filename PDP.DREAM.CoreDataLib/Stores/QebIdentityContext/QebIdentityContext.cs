// QebIdentityContext.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityContext : PdpDataContext, IQebIdentityContext
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured ||
        (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      optionsBuilder.UseSqlServer(NpdsServiceDefaults.Values.NpdsUserDbconstr);
    }
  }

  protected void OnInitiating(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsUserDbconstr; }
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<QebIdentityContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiating(DbContextOptions<QebIdentityContext> dbco)
  {
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public QebIdentityContext(DbContextOptions<QebIdentityContext> dbco) : base() { OnInitiating(dbco); }
  public QebIdentityContext(string dbcs) : base() { OnInitiating(dbcs); }
  public QebIdentityContext() : base() { OnInitiating(); }

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
        qebApp.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
      }
    }
    return qebApp;
  }
  public List<QebIdentityAppRole> GetAppRolesByAppGuid(Guid? appGuid = null)
  {
    if (appGuid == null) { appGuid = PdpSiteSettings.Values.AppSecureUiaaGuid; }
    var appRoles = new List<QebIdentityAppRole>();
    if (appGuid != null)
    {
      try
      {
        appRoles = this.QebIdentityAppRoles.Where(r =>
          (r.AppGuidRef == appGuid)).ToList();
      }
      catch (Exception exc)
      {
        var appRole = new QebIdentityAppRole()
        { ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc) };
        appRoles.Add(appRole);
      }
    }
    return appRoles;
  }
  public List<QebIdentityAppUser> GetAppUsersByAppGuid(Guid? appGuid = null)
  {
    if (appGuid == null) { appGuid = PdpSiteSettings.Values.AppSecureUiaaGuid; }
    var appUsers = new List<QebIdentityAppUser>();
    if (appGuid != null)
    {
      try
      {
        appUsers = this.QebIdentityAppUsers.Where(u =>
          (u.AppGuidRef == appGuid)).ToList();
      }
      catch (Exception exc)
      {
        var appUser = new QebIdentityAppUser()
        { ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc) };
        appUsers.Add(appUser);
      }
    }
    return appUsers;
  }

  public int CountUsersByUserName(string userName)
  {
    int count;
    count = this.QebIdentityAppUsers.Where(u =>
    (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
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
         (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
      }
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByPassWord(string password)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
       (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
       (u.PasswordHash == PdpCryptoService.HashToken(password)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByPassWordAndToken(string password, string token)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
       ((u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
       PdpCryptoService.VerifyHashedToken(u.PasswordHash, password) &&
       PdpCryptoService.TokenEqualsToken(u.SecurityToken, token)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByUserName(string username, Guid? appGuid = null)
  {
    var qebUser = new QebIdentityAppUser();
    if (appGuid == null) { appGuid = PdpSiteSettings.Values.AppSecureUiaaGuid; }
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
       (u.AppGuidRef == appGuid) && (u.UserName.ToUpper() == username.ToUpper()));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndToken(string username, string token)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
        (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
        (u.UserName.ToUpper() == username.ToUpper() &&
        PdpCryptoService.TokenEqualsToken(u.SecurityToken, token)));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public QebIdentityAppUser GetUserByUserGuid(string userguid)
  {
    return GetUserByUserGuid(PdpGuid.Parse(userguid));
  }
  public QebIdentityAppUser GetUserByUserGuid(Guid userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
        (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
        (u.UserGuidKey == userguid));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }
  public QebIdentityAppUser GetUserByUserNameAndUserGuid(string username, Guid userguid)
  {
    var qebUser = new QebIdentityAppUser();
    try
    {
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()) && (u.UserGuidKey == userguid));
    }
    catch (Exception exc)
    {
      qebUser.ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc);
    }
    return qebUser;
  }

  public List<QebIdentityAppUserRole> GetUserRolesByUserGuid(Guid userguid)
  {
    var userRoles = new List<QebIdentityAppUserRole>();
    try
    {
      userRoles = this.QebIdentityAppUserRoles.Where(u =>
        (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
        (u.UserGuidRef == userguid)).ToList();
    }
    catch (Exception exc)
    {
      var userRole = new QebIdentityAppUserRole()
      { ConcurrencyStamp = PdpStatus.LinqErrorMessage(exc) };
      userRoles.Add(userRole);
    }
    return userRoles;
  }
  public List<string> GetUserRoleNamesByUserGuid(Guid userguid)
  {
    var roleNames = new List<string>();
    roleNames = this.QebIdentityAppUserRoles.Where(u =>
      (u.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
      (u.UserGuidRef == userguid))
      .Select(r => r.RoleName).Distinct().ToList();
    return roleNames;
  }

} // end class
