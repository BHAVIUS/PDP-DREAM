// QebIdentityAppUserRole.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityAppUserRole
{
  partial void OnCreated()
  {
    AppGuidRef = PdpGuid.ParseToNonNullable(AppGuidRef, PdpSiteSettings.Values.AppSecureUiaaGuid);
  }
  public QebIdentityAppUserRole()
  {
    OnCreated();
  }

} // class

public static partial class QebIdentityAppOperators
{
  public static IQueryable<QebiUserRoleUxm> ToEditable(this IQueryable<QebIdentityAppUserRole> query)
  {
    IQueryable<QebiUserRoleUxm> rows =
      from rec in query
      select new QebiUserRoleUxm
      {
        AppGuid = rec.AppGuidRef,
        UserGuid = rec.UserGuidRef,
        UserName = rec.UserName,
        RoleGuid = rec.RoleGuidRef,
        RoleName = rec.RoleName,
        RoleDescription = rec.RoleDescription
      };
    return rows;
  }

} // class

public partial class QebIdentityContext
{
  public IEnumerable<QebIdentityAppUserRole> ListStorableAppUserRoles(Guid userGuid)
  {
    IEnumerable<QebIdentityAppUserRole> result;
    try
    {
      IQueryable<QebIdentityAppUserRole> query = this.QebIdentityAppUserRoles
        .Where(r =>
        (r.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
        (r.UserGuidRef == userGuid));
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebIdentityAppUserRole>();
    }
    return result;
  }

  public IEnumerable<QebiUserRoleUxm> ListEditableAppUserRoles(Guid userGuid)
  {
    IEnumerable<QebiUserRoleUxm> result;
    try
    {
      IQueryable<QebiUserRoleUxm> query = this.QebIdentityAppUserRoles
        .Where(r =>
        (r.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
        (r.UserGuidRef == userGuid)).ToEditable();
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserRoleUxm>();
    }
    return result;
  }

  public List<string>? GetAppUserRolesForUserGuid(Guid userGuid)
  {
    List<string>? userRoles = this.QebIdentityAppUserRoles
      .Where(r =>
      (r.AppGuidRef == PdpSiteSettings.Values.AppSecureUiaaGuid) &&
      (r.UserGuidRef == userGuid)).Select(n => n.RoleName).ToList();
    return userRoles;
  }

} // class
