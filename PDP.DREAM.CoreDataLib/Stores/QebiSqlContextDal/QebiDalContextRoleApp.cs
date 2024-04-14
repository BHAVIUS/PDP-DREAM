// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class QebiDbsqlOperators
{
  public static IQueryable<QebiAppRoleUxm> ToEditable(this IQueryable<QebiAppRole> query)
  {
    IQueryable<QebiAppRoleUxm> result =
      from rec in query
      select new QebiAppRoleUxm
      {
        AppGuid = rec.AppGuid,
        RoleGuid = rec.RoleGuid,
        RoleCode = rec.RoleCode,
        RoleName = rec.RoleName,
        RoleDescription = rec.RoleDescription,
        AppRoleComment = rec.AppRoleComment,
      };
    return result;
  }

} // class

public partial class QebiDalContext
{
  public List<QebiAppRole> ListStorableQebiRoles(Guid? appGuid = null)
  {
    List<QebiAppRole> result;
    if (appGuid == null) { appGuid = PDPSS.CiaamAppGuid; }
    try
    {
      IQueryable<QebiAppRole> query = this.QebiAppRoles
        .Where(r => (r.AppGuid == appGuid));
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiAppRole>().ToList();
    }
    return result;
  }

  public List<QebiAppRoleUxm> ListEditableQebiRoles(Guid? appGuid = null)
  {
    List<QebiAppRoleUxm> result;
    if (appGuid == null) { appGuid = PDPSS.CiaamAppGuid; }
    try
    {
      IQueryable<QebiAppRoleUxm> query = this.QebiAppRoles
        .Where(r => (r.AppGuid == appGuid)).ToEditable();
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiAppRoleUxm>().ToList();
    }
    return result;
  }

  public Guid? GetQebiRoleGuidByRoleName(string roleName, Guid? appGuid = null)
  {
    if (appGuid == null) { appGuid = PDPSS.CiaamAppGuid; }
    Guid? roleGuid = this.QebiAppRoles.Where(r =>
      (r.AppGuid == appGuid) && (r.RoleName == roleName))
      .Select(n => n.RoleGuid).FirstOrDefault();
    return roleGuid;
  }

  public Byte? GetQebiRoleCodeByRoleName(string roleName, Guid? appGuid = null)
  {
    if (appGuid == null) { appGuid = PDPSS.CiaamAppGuid; }
    Byte? roleCode = this.QebiAppRoles.Where(r =>
      (r.AppGuid == appGuid) && (r.RoleName == roleName))
      .Select(n => n.RoleCode).FirstOrDefault();
    return roleCode;
  }

  public QebiAppRoleUxm EditQebiAppRole(QebiAppRoleUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    if (uxm.RoleGuid.IsEmpty()) { uxm.RoleGuid = PdpNewGuid(); }
    var errorCode = QebiAppRoleEdit(uxm.AppGuid, uxm.RoleGuid, uxm.RoleCode, uxm.AppRoleComment);
    return uxm;
  }

  public QebiAppRoleUxm DeleteQebiAppRole(QebiAppRoleUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    var errorCode = QebiAppRoleDelete(uxm.AppGuid, uxm.RoleGuid);
    return uxm;
  }

} // end class

// end file