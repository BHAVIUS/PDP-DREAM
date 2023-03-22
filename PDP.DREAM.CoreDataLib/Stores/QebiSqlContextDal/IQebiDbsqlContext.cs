// IQebiDbsqlContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public interface IQebiDbsqlContext
{
  DbSet<QebIdentityAppRole> QebIdentityAppRoles { get; set; }
  DbSet<QebIdentityApp> QebIdentityApps { get; set; }
  DbSet<QebIdentityAppUserRole> QebIdentityAppUserRoles { get; set; }
  DbSet<QebIdentityAppUser> QebIdentityAppUsers { get; set; }
  DbSet<QebIdentityAppUserSession> QebIdentityAppUserSessions { get; set; }

  QebiUserUxm ApproveSiaaUser(QebiUserUxm editObj);
  bool CheckQebiSessionUser(ref QebiUserRestContext qurc);
  int CountUsersByUserName(string userName);
  bool QebiContextHasAppGuid();
  QebiRoleUxm DeleteSiaaRole(QebiRoleUxm editObj);
  QebiUserUxm DeleteSiaaUser(QebiUserUxm editObj);
  bool EditQebiSessionUser(ref QebiUserRestContext qurc);
  QebiRoleUxm EditSiaaRole(QebiRoleUxm editObj);
  QebiUserUxm EditSiaaUser(QebiUserUxm editObj);
  QebIdentityApp GetAppByAppName(string appName);
  Guid? GetAppLinkGuidByUserGuidRoleName(Guid userGuid, string roleName);
  Guid? GetAppRoleGuidByRoleName(string roleName, Guid? appGuid = null);
  List<QebIdentityAppRole> GetAppRolesByAppGuid();
  IList<string>? GetAppUserRolesForUserGuid(Guid userGuid);
  List<QebIdentityAppUser> GetAppUsersByAppGuid();
  QebIdentityAppUser GetUserByPassWord(string password);
  QebIdentityAppUser GetUserByPassWordAndAnswer(string password, string answer);
  QebIdentityAppUser GetUserByPassWordAndToken(string password, string token);
  QebIdentityAppUser GetUserByPrincipal(ClaimsPrincipal principal);
  QebIdentityAppUser GetUserByUserGuid(Guid? userguid);
  QebIdentityAppUser GetUserByUserGuid(string? userguid);
  QebIdentityAppUser GetUserByUserName(string username);
  QebIdentityAppUser GetUserByUserNameAndAnswer(string username, string answer);
  QebIdentityAppUser GetUserByUserNameAndToken(string username, string token);
  QebIdentityAppUser GetUserByUserNameAndUserGuid(string? username, Guid? userguid);
  List<string> GetUserRoleNamesByUserGuid(Guid? userguid);
  List<QebIdentityAppUserRole> GetUserRolesByUserGuid(Guid? userguid);
  bool HasChanges();
  IEnumerable<QebiRoleUxm> ListEditableAppRoles(Guid? appGuid = null);
  IEnumerable<QebiUserRoleUxm> ListEditableAppUserRoles(Guid userGuid);
  IEnumerable<QebiUserUxm> ListEditableAppUsers();
  IEnumerable<QebIdentityAppRole> ListStorableAppRoles(Guid? appGuid = null);
  IEnumerable<QebIdentityAppUserRole> ListStorableAppUserRoles(Guid userGuid, Guid? appGuid = null);
  IEnumerable<QebIdentityAppUser> ListStorableAppUsers();
  int? QebiAppUserAgentRoleEdit(Guid? AppGuid, Guid? UserGuid, Guid? AgentGuid, bool? UserIsAgent, bool? AgentIsAuthor, bool? AgentIsEditor, bool? AgentIsAdmin);
  int? QebiAppUserSessionCheck(Guid? AppGuid, bool? SessionValueIsRequired, ref Guid? SessionGuid, ref Guid? UserGuid, ref Guid? AgentGuid, ref string UserNameDisp, ref bool? UserIsAgent, ref bool? AgentIsAuthor, ref bool? AgentIsEditor, ref bool? AgentIsAdmin);
  int? QebiAppUserSessionEdit(Guid? AppGuid, Guid? UserGuid, ref Guid? SessionGuid);
  int? QebiAppUserSessionTimestamp(Guid? UserGuid, Guid? SessionGuid);
  int? QebIdentityAppDelete(Guid? AppGuidKey);
  Task<int?> QebIdentityAppDeleteAsync(Guid? AppGuidKey);
  int? QebIdentityAppEdit(Guid? AppGuidKey, string AppName, string AppDescription);
  Task<int?> QebIdentityAppEditAsync(Guid? AppGuidKey, string AppName, string AppDescription);
  int? QebIdentityAppLinkDelete(Guid? LinkGuidKey, Guid? AppGuidRef);
  Task<int?> QebIdentityAppLinkDeleteAsync(Guid? LinkGuidKey, Guid? AppGuidRef);
  int? QebIdentityAppLinkEdit(Guid? LinkGuidKey, Guid? UserGuidRef, Guid? RoleGuidRef, Guid? AppGuidRef);
  Task<int?> QebIdentityAppLinkEditAsync(Guid? LinkGuidKey, Guid? UserGuidRef, Guid? RoleGuidRef, Guid? AppGuidRef);
  int? QebIdentityAppRoleDelete(Guid? AppGuidRef, Guid? RoleGuidKey);
  Task<int?> QebIdentityAppRoleDeleteAsync(Guid? AppGuidRef, Guid? RoleGuidKey);
  int? QebIdentityAppRoleEdit(Guid? AppGuidRef, Guid? RoleGuidKey, string RoleName, string RoleDescription);
  Task<int?> QebIdentityAppRoleEditAsync(Guid? AppGuidRef, Guid? RoleGuidKey, string RoleName, string RoleDescription);
  int? QebIdentityAppUserApprove(Guid? AppGuidRef, Guid? UserGuidKey, bool? UserIsApproved);
  Task<int?> QebIdentityAppUserApproveAsync(Guid? AppGuidRef, Guid? UserGuidKey, bool? UserIsApproved);
  int? QebIdentityAppUserDelete(Guid? AppGuidRef, Guid? UserGuidKey);
  Task<int?> QebIdentityAppUserDeleteAsync(Guid? AppGuidRef, Guid? UserGuidKey);
  int? QebIdentityAppUserEdit(Guid? AppGuidRef, Guid? UserGuidKey, string FirstName, string LastName, string UserName, string EmailAddress);
  Task<int?> QebIdentityAppUserEditAsync(Guid? AppGuidRef, Guid? UserGuidKey, string FirstName, string LastName, string UserName, string EmailAddress);
  int? QebIdentityAppUserRegister(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserNameDisplayed, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string EmailAlternate, string WebsiteAddress, string Organization, string SecurityQuestion, string SecurityAnswer, string SecurityStamp, string SecurityToken, string PasswordHash, DateTime? DateUserCreated, DateTime? DateTokenExpired);
  Task<int?> QebIdentityAppUserRegisterAsync(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserNameDisplayed, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string EmailAlternate, string WebsiteAddress, string Organization, string SecurityQuestion, string SecurityAnswer, string SecurityStamp, string SecurityToken, string PasswordHash, DateTime? DateUserCreated, DateTime? DateTokenExpired);
  int? QebIdentityAppUserStamp(Guid? AppGuidRef, Guid? UserGuidKey, Guid? SessionGuidRef);
  Task<int?> QebIdentityAppUserStampAsync(Guid? AppGuidRef, Guid? UserGuidKey, Guid? SessionGuidRef);
  int? QebIdentityAppUserUpdateEmail(Guid? AppGuidRef, Guid? UserGuidKey, string EmailAddress, string EmailAlternate, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateEmailConfirmed, DateTime? DateLastEdit, bool? EmailConfirmed);
  Task<int?> QebIdentityAppUserUpdateEmailAsync(Guid? AppGuidRef, Guid? UserGuidKey, string EmailAddress, string EmailAlternate, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateEmailConfirmed, DateTime? DateLastEdit, bool? EmailConfirmed);
  int? QebIdentityAppUserUpdatePassword(Guid? AppGuidRef, Guid? UserGuidKey, string PasswordHash, string SecurityToken, DateTime? DateTokenExpired, DateTime? DatePasswordChanged, DateTime? DateLastEdit);
  Task<int?> QebIdentityAppUserUpdatePasswordAsync(Guid? AppGuidRef, Guid? UserGuidKey, string PasswordHash, string SecurityToken, DateTime? DateTokenExpired, DateTime? DatePasswordChanged, DateTime? DateLastEdit);
  int? QebIdentityAppUserUpdateProfile(Guid? AppGuidRef, Guid? UserGuidKey, string UserNameDisplayed, string FirstName, string LastName, string Organization, string PhoneNumber, string SecurityAnswer, string SecurityQuestion, string WebsiteAddress, DateTime? DateProfileChanged, DateTime? DateLastEdit);
  Task<int?> QebIdentityAppUserUpdateProfileAsync(Guid? AppGuidRef, Guid? UserGuidKey, string UserNameDisplayed, string FirstName, string LastName, string Organization, string PhoneNumber, string SecurityAnswer, string SecurityQuestion, string WebsiteAddress, DateTime? DateProfileChanged, DateTime? DateLastEdit);
  int? QebIdentityAppUserUpdateUsername(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserNameDisplayed, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateUserNameChanged, DateTime? DateLastEdit);
  Task<int?> QebIdentityAppUserUpdateUsernameAsync(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserNameDisplayed, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateUserNameChanged, DateTime? DateLastEdit);
  IQueryable<QebIdentityAppUser> QueryStorableAppUsers();
  RegisterUserUxm RegisterSiaaUser(RegisterUserUxm editObj);

} // end interface

// end file