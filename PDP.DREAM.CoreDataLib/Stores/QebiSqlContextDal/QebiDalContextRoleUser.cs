// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class QebiDbsqlOperators
{
  public static IQueryable<QebiUserRoleUxm> ToEditable(this IQueryable<QebiUserRole> query)
  {
    IQueryable<QebiUserRoleUxm> rows =
      from rec in query
      select new QebiUserRoleUxm
      {
        AppGuid = rec.AppGuid,
        UserGuid = rec.UserGuid,
        UserName = rec.UserName,
        RoleGuid = rec.RoleGuid,
        RoleName = rec.RoleName,
        RoleDescription = rec.RoleDescription
      };
    return rows;
  }

} // class

public partial class QebiDalContext
{
  public List<QebiUserRole> ListStorableQebiUserRoles(Guid userGuid, Guid? appGuid = null)
  {
    List<QebiUserRole> result;
    if (appGuid == null) { appGuid = PDPSS.CiaamAppGuid; }
    try
    {
      IQueryable<QebiUserRole> query = this.QebiUserRoles
        .Where(r =>
        (r.AppGuid == appGuid) &&
        (r.UserGuid == userGuid));
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserRole>().ToList();
    }
    return result;
  }

  public List<QebiUserRoleUxm> ListEditableQebiUserRoles(Guid userGuid)
  {
    List<QebiUserRoleUxm> result;
    try
    {
      IQueryable<QebiUserRoleUxm> query = this.QebiUserRoles
        .Where(r =>
        (r.AppGuid == PDPSS.CiaamAppGuid) &&
        (r.UserGuid == userGuid)).ToEditable();
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserRoleUxm>().ToList();
    }
    return result;
  }

  public List<string>? GetQebiUserRolesForUserGuid(Guid userGuid)
  {
    List<string>? userRoles = this.QebiUserRoles
      .Where(r =>
      (r.AppGuid == PDPSS.CiaamAppGuid) &&
      (r.UserGuid == userGuid)).Select(n => n.RoleName).ToList();
    return userRoles;
  }

  public Guid? GetQebiLinkGuidByUserGuidRoleName(Guid userGuid, string roleName)
  {
    Guid? linkGuid = this.QebiUserRoles
      .Where(r =>
      (r.AppGuid == PDPSS.CiaamAppGuid) && (r.RoleName == roleName) &&
      (r.UserGuid == userGuid)).Select(g => g.LinkGuid).FirstOrDefault();
    return linkGuid;
  }

}
