// QebIdentityAppUserRole.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityAppUserRole : PdpDataEntity
{
  public QebIdentityAppUserRole()
  {
    AppGuidRef = OnEntityCreated(AppGuidRef);
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
  public IEnumerable<QebIdentityAppUserRole> ListStorableAppUserRoles(Guid userGuid, Guid? appGuid = null)
  {
    IEnumerable<QebIdentityAppUserRole> result;
    if (appGuid == null) { appGuid = PDPSS.AppSecureUiaaGuid; }
    try
    {
      IQueryable<QebIdentityAppUserRole> query = this.QebIdentityAppUserRoles
        .Where(r =>
        (r.AppGuidRef == appGuid) &&
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
        (r.AppGuidRef == PDPSS.AppSecureUiaaGuid) &&
        (r.UserGuidRef == userGuid)).ToEditable();
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserRoleUxm>();
    }
    return result;
  }

  public IList<string>? GetAppUserRolesForUserGuid(Guid userGuid)
  {
    IList<string>? userRoles = this.QebIdentityAppUserRoles
      .Where(r =>
      (r.AppGuidRef == PDPSS.AppSecureUiaaGuid) &&
      (r.UserGuidRef == userGuid)).Select(n => n.RoleName).ToList();
    return userRoles;
  }

  public Guid? GetAppLinkGuidByUserGuidRoleName(Guid userGuid, string roleName)
  {
    Guid? linkGuid = this.QebIdentityAppUserRoles
      .Where(r =>
      (r.AppGuidRef == PDPSS.AppSecureUiaaGuid) && (r.RoleName == roleName) &&
      (r.UserGuidRef == userGuid)).Select(g => g.LinkGuidKey).FirstOrDefault();
    return linkGuid;
  }

}
