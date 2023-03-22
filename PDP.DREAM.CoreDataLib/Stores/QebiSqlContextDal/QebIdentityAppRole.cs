// QebIdentityAppRole.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityAppRole : DbsqlContextEntity
{
  public QebIdentityAppRole()
  {
    AppGuidRef = OnEntityCreated(AppGuidRef);
  }

} // class

public static partial class QebIdentityAppOperators
{
  public static IQueryable<QebiRoleUxm> ToEditable(this IQueryable<QebIdentityAppRole> query)
  {
    IQueryable<QebiRoleUxm> rows =
      from rec in query
      select new QebiRoleUxm
      {
        AppGuid = rec.AppGuidRef,
        RoleGuid = rec.RoleGuidKey,
        RoleName = rec.RoleName,
        RoleDescription = rec.RoleDescription
      };
    return rows;
  }

} // class

public partial class QebiDbsqlContext
{
  public IEnumerable<QebIdentityAppRole> ListStorableAppRoles(Guid? appGuid = null)
  {
    IEnumerable<QebIdentityAppRole> result;
    if (appGuid == null) { appGuid = PDPSS.AppSecureUiaaGuid; }
    try
    {
      IQueryable<QebIdentityAppRole> query = this.QebIdentityAppRoles
        .Where(r => (r.AppGuidRef == appGuid));
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebIdentityAppRole>();
    }
    return result;
  }

  public IEnumerable<QebiRoleUxm> ListEditableAppRoles(Guid? appGuid = null)
  {
    IEnumerable<QebiRoleUxm> result;
    if (appGuid == null) { appGuid = PDPSS.AppSecureUiaaGuid; }
    try
    {
      IQueryable<QebiRoleUxm> query = this.QebIdentityAppRoles
        .Where(r => (r.AppGuidRef == appGuid)).ToEditable();
      result = query.OrderBy(r => r.RoleName).ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiRoleUxm>();
    }
    return result;
  }

  public Guid? GetAppRoleGuidByRoleName(string roleName, Guid? appGuid = null)
  {
    if (appGuid == null) { appGuid = PDPSS.AppSecureUiaaGuid; }
    Guid? roleGuid = this.QebIdentityAppRoles.Where(r =>
      (r.AppGuidRef == appGuid) && (r.RoleName == roleName))
      .Select(n => n.RoleGuidKey).FirstOrDefault();
    return roleGuid;
  }

  public QebiRoleUxm EditSiaaRole(QebiRoleUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    if (editObj.RoleGuid.IsEmpty()) { editObj.RoleGuid = Guid.NewGuid(); }
    var errorCode = QebIdentityAppRoleEdit(editObj.AppGuid, editObj.RoleGuid,
      editObj.RoleName, editObj.RoleDescription);
    return editObj;
  }

  public QebiRoleUxm DeleteSiaaRole(QebiRoleUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    var errorCode = QebIdentityAppRoleDelete(editObj.AppGuid, editObj.RoleGuid);
    return editObj;
  }

} // end class

// end file