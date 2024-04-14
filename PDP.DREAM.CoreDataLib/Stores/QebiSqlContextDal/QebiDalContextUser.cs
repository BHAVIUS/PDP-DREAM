// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Linq;

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class QebiDbsqlOperators
{
  // User editable to QebiUserUxm
  public static List<QebiUserUxm> ToEditable(this IQueryable<QebiUser> query)
  {
    List<QebiUserUxm>? result = query
      .Include(rec => rec.QebiUserRoles)
      .AsEnumerable().Select(rec => {
        var uxmUser = new QebiUserUxm(
          rec.AppGuid, rec.UserGuid,
          rec.UserIsApproved, rec.UserIsPerson, rec.UserIsAgent,
          rec.FirstName, rec.LastName, rec.UserName, rec.UserAlias, rec.EmailAddress);
        uxmUser.UserRoleNames = rec.QebiUserRoles
        .OrderBy(p => p.RoleName).Select(p => p.RoleName)
        .ToList().JoinListToOrString();
        return uxmUser;
      }).ToList();
    return result;
  }

} // class

public partial class QebiDalContext
{
  public IQueryable<QebiUser> QueryStorableQebiUsers()
  {
    IQueryable<QebiUser> query = this.QebiUsers
      .Where(u => (u.AppGuid == PDPSS.CiaamAppGuid))
      .OrderBy(r => r.UserName);
    return query;
  }
  public List<QebiUser> ListStorableQebiUsers()
  {
    List<QebiUser> result;
    try
    {
      result = this.QueryStorableQebiUsers().ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebiUser>().ToList();
    }
    return result;
  }

  public List<QebiUserUxm> ListEditableQebiUsers()
  {
    List<QebiUserUxm> result;
    try
    {
      result = this.QueryStorableQebiUsers().ToEditable();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserUxm>().ToList();
    }
    return result;
  }

  public RegisterUserUxm RegisterQebiUser(RegisterUserUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    if (uxm.UserGuid.IsEmpty()) { uxm.UserGuid = PdpNewGuid(); }
    var qebUsr = new QebiUser(uxm);

    var errorCode = QebiUserRegister(qebUsr.AppGuid, qebUsr.UserGuid,
      qebUsr.UserName, qebUsr.UserAlias, qebUsr.FirstName, qebUsr.LastName, qebUsr.PhoneNumber,
      qebUsr.EmailAddress, qebUsr.EmailAlternate, qebUsr.WebsiteAddress, qebUsr.Organization,
      qebUsr.SecurityQuestion, qebUsr.SecurityAnswer, qebUsr.SecurityStamp, qebUsr.SecurityToken,
      qebUsr.PasswordHash, PDPSS.AppUseSecureAlias, qebUsr.DateUserCreated, qebUsr.DateTokenExpired);
    if (errorCode < 0) { uxm.Message = $"Error code = {errorCode} while registering user with index {uxm.UserName}"; }
    return uxm;
  }

  public QebiUserUxm ApproveQebiUser(QebiUserUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    if (uxm.UserGuid.IsEmpty()) { uxm.UserGuid = PdpNewGuid(); }

    var errorCode = QebiUserApprove(uxm.AppGuid, uxm.UserGuid, uxm.UserIsApproved);
    if (errorCode < 0) { uxm.Message = $"Error code = {errorCode} while registering user with index {uxm.UserName}"; }
    return uxm;
  }

  public QebiUserUxm EditQebiUser(QebiUserUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    if (uxm.UserGuid.IsEmpty()) { uxm.UserGuid = PdpNewGuid(); }

    if (!string.IsNullOrEmpty(uxm.UserRoleNames))
    {
      // database context roles
      var dbcRoles = GetQebiUserRolesForUserGuid(uxm.UserGuid);
      // user interface model roles
      var uxmRoles = uxm.UserRoleNames.SplitOrStringToList();
      // add uxmRole if does not exist in dbcRoles
      foreach (var roleName in uxmRoles)
      {
        if (!dbcRoles.Contains(roleName))
        {
          var roleGuid = GetQebiRoleGuidByRoleName(roleName);
          var linkGuid = PdpNewGuid();
          if (roleGuid != null)
          {
            QebiLinkEdit(linkGuid, uxm.AppGuid, uxm.UserGuid, roleGuid);
          }
        }
      }
      // delete dbcRole if does not exist in uxmRoles
      foreach (var roleName in dbcRoles)
      {
        if (!uxmRoles.Contains(roleName))
        {
          var roleGuid = GetQebiRoleGuidByRoleName(roleName);
          var linkGuid = GetQebiLinkGuidByUserGuidRoleName(uxm.UserGuid, roleName);
          if (linkGuid != null)
          {
            QebiLinkDelete(linkGuid, uxm.AppGuid, uxm.UserGuid, roleGuid);
          }
        }
      }
    }

    var errorCode = QebiUserEdit(uxm.AppGuid, uxm.UserGuid,
      uxm.FirstName, uxm.LastName, uxm.UserName, uxm.EmailAddress);

    return uxm;
  }

  public QebiUserUxm DeleteQebiUser(QebiUserUxm uxm)
  {
    if (uxm.AppGuid.IsEmpty()) { uxm.AppGuid = PDPSS.CiaamAppGuid; }
    var errorCode = QebiUserDelete(uxm.AppGuid, uxm.UserGuid);
    return uxm;
  }

} // class
