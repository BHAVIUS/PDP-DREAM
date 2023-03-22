// QebIdentityAppUser.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityAppUser : DbsqlContextEntity
{
  public QebIdentityAppUser()
  {
    AppGuidRef = OnEntityCreated(AppGuidRef);
  }

  public QebIdentityAppUser(IUserRegister m)
  {
    RegisterProfileModel(m);
  }

  public void RegisterProfileModel(IUserRegister m)
  {
    this.AppGuidRef = PDPSS.AppSecureUiaaGuid;
    this.UserGuidKey = Guid.NewGuid();
    this.UserName = m.QebUserName;
    if (string.IsNullOrEmpty(m.QebUserNameDisplayed)) { this.UserNameDisplayed = m.QebUserName; }
    else { this.UserNameDisplayed = m.QebUserNameDisplayed; }
    this.FirstName = m.FirstName;
    this.LastName = m.LastName;
    this.PhoneNumber = m.PhoneNumber;
    this.EmailAddress = m.EmailAddress;
    if (string.IsNullOrEmpty(m.EmailAlternate)) { this.EmailAlternate = m.EmailAddress; }
    else { this.EmailAlternate = m.EmailAlternate; }
    this.WebsiteAddress = m.WebsiteAddress;
    this.Organization = m.Organization;
    this.SecurityQuestion = m.SecurityQuestion;
    this.SecurityAnswer = m.SecurityAnswer;
    this.SecurityStamp = Guid.NewGuid().ToString();
    this.SecurityToken = QebCryptoService.GenerateToken();
    this.PasswordHash = QebCryptoService.HashToken(m.PassWord);
    this.DateUserCreated = DateTime.UtcNow;
    this.DateTokenExpired = DateTime.UtcNow.AddHours(24);
    // TODO: recode to check options on startup for confirmation requirements
    // else initialize true to simplify and handle case when email confirmation not required
    this.UserIsApproved = false;
  }

  public ChangeProfileUxm GetChangeProfileModel()
  {
    var m = new ChangeProfileUxm();
    m.DateLastEdit = this.DateLastEdit;
    m.DateProfileChanged = this.DateProfileChanged;
    m.QebUserNameDisplayed = this.UserNameDisplayed;
    m.FirstName = this.FirstName;
    m.LastName = this.LastName;
    m.Organization = this.Organization;
    m.PhoneNumber = this.PhoneNumber;
    m.SecurityAnswer = this.SecurityAnswer;
    m.SecurityQuestion = this.SecurityQuestion;
    m.WebsiteAddress = this.WebsiteAddress;
    return m;
  }

  public void SetChangeProfileModel(IUserProfileEdit m)
  {
    this.DateLastEdit = DateTime.UtcNow;
    this.DateProfileChanged = DateTime.UtcNow;
    this.UserNameDisplayed = m.QebUserNameDisplayed ?? "";
    this.FirstName = m.FirstName ?? "";
    this.LastName = m.LastName ?? "";
    this.Organization = m.Organization ?? "";
    this.PhoneNumber = m.PhoneNumber ?? "";
    this.SecurityAnswer = m.SecurityAnswer ?? "";
    this.SecurityQuestion = m.SecurityQuestion ?? "";
    this.WebsiteAddress = m.WebsiteAddress ?? "";
  }

} // class

public static partial class QebIdentityAppOperators
{
  public static IEnumerable<QebiUserUxm> ToEditable(this IQueryable<QebIdentityAppUser> query)
  {
    IEnumerable<QebiUserUxm>? rows = query
      .Include(rec => rec.QebIdentityAppUserRoles)
      .AsEnumerable().Select(rec => {
        var uxmUser = new QebiUserUxm(rec.AppGuidRef, rec.UserGuidKey,
          rec.FirstName, rec.LastName, rec.UserName, rec.EmailAddress, rec.UserIsApproved);
        uxmUser.UserRoleNames = rec.QebIdentityAppUserRoles
        .OrderBy(p => p.RoleName).Select(p => p.RoleName).ToList().JoinListToOrString();
        return uxmUser;
      }).ToList();
    return rows;
  }

} // class

public partial class QebiDbsqlContext
{
  public IQueryable<QebIdentityAppUser> QueryStorableAppUsers()
  {
    IQueryable<QebIdentityAppUser> query = this.QebIdentityAppUsers
      .Where(u => (u.AppGuidRef == PDPSS.AppSecureUiaaGuid))
      .OrderBy(r => r.UserName);
    return query;
  }
  public IEnumerable<QebIdentityAppUser> ListStorableAppUsers()
  {
    IEnumerable<QebIdentityAppUser> result;
    try
    {
      result = this.QueryStorableAppUsers().ToList();
    }
    catch
    {
      result = Enumerable.Empty<QebIdentityAppUser>();
    }
    return result;
  }

  public IEnumerable<QebiUserUxm> ListEditableAppUsers()
  {
    IEnumerable<QebiUserUxm> result;
    try
    {
      result = this.QueryStorableAppUsers().ToEditable();
    }
    catch
    {
      result = Enumerable.Empty<QebiUserUxm>();
    }
    return result;
  }

  // ATTN: note differences between AppGuid and AppGuidRef, between UserGuid and UserGuidKey
  // TODO: reconcile these differences to simplify for consistency across RegisterUserUxm, QebiUserUxm, QebIdentityAppUser

  public RegisterUserUxm RegisterSiaaUser(RegisterUserUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = Guid.NewGuid(); }
    var qebUser = new QebIdentityAppUser(editObj);

    var errorCode = QebIdentityAppUserRegister(qebUser.AppGuidRef, qebUser.UserGuidKey,
      qebUser.UserName, qebUser.UserNameDisplayed, qebUser.FirstName, qebUser.LastName, qebUser.PhoneNumber,
      qebUser.EmailAddress, qebUser.EmailAlternate, qebUser.WebsiteAddress, qebUser.Organization,
      qebUser.SecurityQuestion, qebUser.SecurityAnswer, qebUser.SecurityStamp, qebUser.SecurityToken,
      qebUser.PasswordHash, qebUser.DateUserCreated, qebUser.DateTokenExpired);
    if (errorCode < 0) { editObj.Message = $"Error code = {errorCode} while registering user with index {editObj.QebUserName}"; }
    return editObj;
  }

  public QebiUserUxm ApproveSiaaUser(QebiUserUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = Guid.NewGuid(); }

    var errorCode = QebIdentityAppUserApprove(editObj.AppGuid, editObj.UserGuid, editObj.UserIsApproved);
    if (errorCode < 0) { editObj.Message = $"Error code = {errorCode} while registering user with index {editObj.QebUserName}"; }
    return editObj;
  }

  public QebiUserUxm EditSiaaUser(QebiUserUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = Guid.NewGuid(); }

    // ATTN: not currently using (dependent on) QebiUserUxm property UserRoleList
    // TODO: deprecate property UserRoleList

    if (!string.IsNullOrEmpty(editObj.UserRoleNames))
    {
      // database context roles
      var dbcRoles = GetAppUserRolesForUserGuid(editObj.UserGuid);
      // user interface model roles
      var uxmRoles = editObj.UserRoleNames.SplitOrStringToList();
      // add uxmRole if does not exist in dbcRoles
      foreach (var roleName in uxmRoles)
      {
        if (!dbcRoles.Contains(roleName))
        {
          var linkGuid = Guid.NewGuid();
          var roleGuid = GetAppRoleGuidByRoleName(roleName);
          if (roleGuid != null)
          {
            QebIdentityAppLinkEdit(linkGuid, editObj.UserGuid, roleGuid, editObj.AppGuid);
          }
        }
      }
      // delete dbcRole if does not exist in uxmRoles
      foreach (var roleName in dbcRoles)
      {
        if (!uxmRoles.Contains(roleName))
        {
          var linkGuid = GetAppLinkGuidByUserGuidRoleName(editObj.UserGuid, roleName);
          if (linkGuid != null)
          {
            QebIdentityAppLinkDelete(linkGuid, editObj.AppGuid);
          }
        }
      }
    }

    var errorCode = QebIdentityAppUserEdit(editObj.AppGuid, editObj.UserGuid,
      editObj.FirstName, editObj.LastName, editObj.QebUserName, editObj.EmailAddress);

    return editObj;
  }

  public QebiUserUxm DeleteSiaaUser(QebiUserUxm editObj)
  {
    if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PDPSS.AppSecureUiaaGuid; }
    var errorCode = QebIdentityAppUserDelete(editObj.AppGuid, editObj.UserGuid);
    return editObj;
  }

} // class
