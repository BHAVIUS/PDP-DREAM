// QebIdentityAppContext.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
  public partial class QebIdentityContext : IQebIdentityContext
  {
    public QebIdentityContext(string dbcs)
    {
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsUserDbconstr; }
      if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty in QebIdentityContext"); }
      var optionsBuilder = new DbContextOptionsBuilder<QebIdentityContext>();
      optionsBuilder.UseSqlServer(dbcs);
      base.OnConfiguring(optionsBuilder);
      OnCreated();
    }

    public string StoreChanges()
    {
      try
      {
        this.SaveChanges();
        return string.Empty;
      }
      catch (Exception ex)
      {
        var inMessage = ex.InnerException.Message;
        return ex.Message + inMessage;
      }
    }

    public int CountUsersByUserName(string userName)
    {
      int count;
      count = this.QebIdentityAppUsers.Where(u =>
      (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
      (u.UserName.ToLower() == userName.ToLower())).Count();
      return count;
    }

    public QebIdentityAppUser GetUserByPrincipal(ClaimsPrincipal principal)
    {
      if (principal == null) { throw new ArgumentNullException("principal is null in GetUserByPrincipal"); }
      string username = principal.FindFirstValue(SiaaClaimTypes.UserName);
      var qebUser = new QebIdentityAppUser();
      if (!string.IsNullOrWhiteSpace(username))
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()));
      }
      return qebUser;
    }

    public QebIdentityAppUser GetUserByPassWord(string password)
    {
      var qebUser = new QebIdentityAppUser();
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
         (u.PasswordHash == PdpCryptoService.HashToken(password)));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = "LINQ-Error: " + exc.Message;
      }
      return qebUser;
    }

    public QebIdentityAppUser GetUserByPassWordAndToken(string password, string token)
    {
      var qebUser = new QebIdentityAppUser();
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         ((u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
         PdpCryptoService.VerifyHashedToken(u.PasswordHash, password) &&
         PdpCryptoService.TokenEqualsToken(u.SecurityToken, token)));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = "LINQ-Error: " + exc.Message;
      }
      return qebUser;
    }

    public QebIdentityAppUser GetUserByUserName(string username)
    {
      var qebUser = new QebIdentityAppUser();
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = "LINQ-Error: " + exc.Message;
      }
      return qebUser;
    }
    public QebIdentityAppUser GetUserByUserNameAndToken(string username, string token)
    {
      var qebUser = new QebIdentityAppUser();
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
          (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
          (u.UserName.ToUpper() == username.ToUpper() &&
          PdpCryptoService.TokenEqualsToken(u.SecurityToken, token)));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = "LINQ-Error: " + exc.Message;
      }
      return qebUser;
    }

    public QebIdentityAppUser GetUserByUserGuid(Guid userguid)
    {
      var qebUser = new QebIdentityAppUser();
      try
      {
        qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
          (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
          (u.UserGuidKey == userguid));
      }
      catch (Exception exc)
      {
        qebUser.ConcurrencyStamp = "LINQ-Error: " + exc.Message;
      }
      return qebUser;
    }
    public QebIdentityAppUser GetUserByUserGuid(string userguid)
    {
      return GetUserByUserGuid(PdpGuid.Parse(userguid));
    }
    public QebIdentityAppUser GetUserByUserNameAndUserGuid(string username, Guid userguid)
    {
      var qebUser = new QebIdentityAppUser();
      qebUser = this.QebIdentityAppUsers.SingleOrDefault(u =>
         (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
         (u.UserName.ToUpper() == username.ToUpper()) && (u.UserGuidKey == userguid));
      return qebUser;
    }

    public List<QebIdentityAppUserRole> GetUserRolesByUserGuid(Guid userguid)
    {
      var userRoles = new List<QebIdentityAppUserRole>();
      try
      {
        userRoles = this.QebIdentityAppUserRoles.Where(u =>
          (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
          (u.UserGuidRef == userguid)).ToList();
      }
      catch (Exception exc)
      {
        var userRole = new QebIdentityAppUserRole() { ConcurrencyStamp = "LINQ-Error: " + exc.Message };
        userRoles.Add(userRole);
      }
      return userRoles;
    }
    public List<string> GetUserRoleNamesByUserGuid(Guid userguid)
    {
      var roleNames = new List<string>();
      roleNames = this.QebIdentityAppUserRoles.Where(u =>
        (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
        (u.UserGuidRef == userguid))
        .Select(r => r.RoleName).Distinct().ToList();
      return roleNames;
    }

  } // end class

} // end namespace
