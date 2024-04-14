// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class QebiDbsqlOperators
{
  // Admin editable to QebiUserUzm
  public static IList<QebiUserUzm> AdminToEditable(this IQueryable<QebiUser> query)
  {
    IList<QebiUserUzm>? result = query
      .Include(usr => usr.QebiUserRoles)
      .AsEnumerable().Select(usr => {
        var uzm = new QebiUserUzm(
          usr.AppGuid, usr.UserGuid, 
          usr.UserIsApproved, usr.UserIsPerson, usr.UserIsAgent,
          usr.FirstName, usr.LastName, usr.UserName, usr.UserAlias, 
          usr.EmailAddress, usr.EmailAlternate,
          usr.SecurityQuestion, usr.SecurityAnswer);
        uzm.UserRoleNames = usr.QebiUserRoles
        .OrderBy(p => p.RoleName).Select(p => p.RoleName)
        .ToList().JoinListToOrString();
        return uzm;
      }).ToList();
    return result;
  }

} // class

public partial class QebiDalContext
{
  public IQueryable<QebiUser> AdminQueryStorableQebiUsers()
  {
    IQueryable<QebiUser> query = this.QebiUsers
      .Where(u => (u.AppGuid == PDPSS.CiaamAppGuid))
      .OrderBy(r => r.UserName);
    return query;
  }
  public IEnumerable<QebiUser> AdminListStorableQebiUsers()
  {
    IEnumerable<QebiUser> result;
    try
    {
      result = this.AdminQueryStorableQebiUsers().ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUser>();
    }
    return result;
  }

  public IEnumerable<QebiUserUzm> AdminListEditableQebiUsers()
  {
    IEnumerable<QebiUserUzm> result;
    try
    {
      result = this.AdminQueryStorableQebiUsers().AdminToEditable();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserUzm>();
    }
    return result;
  }


  public QebiUserUzm AdminApproveQebiUser(QebiUserUzm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.CiaamAppGuid; }
    if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = PdpNewGuid(); }

    var errorCode = QebiUserApprove(editObj.AppGuid, editObj.UserGuid, editObj.UserIsApproved);
    if (errorCode < 0) { editObj.Message = $"Error code = {errorCode} while registering user with index {editObj.UserName}"; }
    return editObj;
  }

  public QebiUserUzm AdminEditQebiUser(QebiUserUzm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.CiaamAppGuid; }
    if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = PdpNewGuid(); }

    // ATTN: not currently using (dependent on) QebiUserUxm property UserRoleList
    // TODO: deprecate property UserRoleList

    if (!string.IsNullOrEmpty(editObj.UserRoleNames))
    {
      // database context roles
      var dbcRoles = GetQebiUserRolesForUserGuid(editObj.UserGuid);
      // user interface model roles
      var uxmRoles = editObj.UserRoleNames.SplitOrStringToList();
      // add uxmRole if does not exist in dbcRoles
      foreach (var roleName in uxmRoles)
      {
        if (!dbcRoles.Contains(roleName))
        {
          var roleGuid = GetQebiRoleGuidByRoleName(roleName);
          var linkGuid = PdpNewGuid();
          if (roleGuid != null)
          {
            var errCod = QebiLinkEdit(linkGuid, editObj.AppGuid, editObj.UserGuid, roleGuid);
          }
        }
      }
      // delete dbcRole if does not exist in uxmRoles
      foreach (var roleName in dbcRoles)
      {
        if (!uxmRoles.Contains(roleName))
        {
          var roleGuid = GetQebiRoleGuidByRoleName(roleName);
          var linkGuid = GetQebiLinkGuidByUserGuidRoleName(editObj.UserGuid, roleName);
          if (linkGuid != null)
          {
            var errCod = QebiLinkDelete(linkGuid, editObj.AppGuid, editObj.UserGuid, roleGuid);
          }
        }
      }
    }

    var errorCode = QebiUserEdit(editObj.AppGuid, editObj.UserGuid,
      editObj.FirstName, editObj.LastName, editObj.UserName, editObj.EmailAddress);

    return editObj;
  }

  public QebiUserUzm AdminDeleteQebiUser(QebiUserUzm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.CiaamAppGuid; }
    var errorCode = QebiUserDelete(editObj.AppGuid, editObj.UserGuid);
    return editObj;
  }

} // class
