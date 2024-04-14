// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public interface IQebiDalContext
{
  DbSet<QebiApp> QebiApps { get; set; }
  DbSet<QebiAppRole> QebiAppRoles { get; set; }
  DbSet<QebiUser> QebiUsers { get; set; }
  DbSet<QebiUserRole> QebiUserRoles { get; set; }
  DbSet<QebiUserSession> QebiUserSessions { get; set; }

  QebiUserUxm ApproveQebiUser(QebiUserUxm editObj);
  bool CheckSessionQebiUser(ref NpdsClientWrace wrace);
  int CountUsersByUserName(string userName);
  bool QebiContextHasAppGuid();
  QebiAppRoleUxm DeleteQebiAppRole(QebiAppRoleUxm editObj);
  QebiUserUxm DeleteQebiUser(QebiUserUxm editObj);
  bool EditSessionQebiUser(ref NpdsClientWrace wrace);
  QebiAppRoleUxm EditQebiAppRole(QebiAppRoleUxm editObj);
  QebiUserUxm EditQebiUser(QebiUserUxm editObj);
  QebiApp GetAppByAppName(string appName);
  Guid? GetQebiLinkGuidByUserGuidRoleName(Guid userGuid, string roleName);
  Guid? GetQebiRoleGuidByRoleName(string roleName, Guid? appGuid = null);
  List<QebiAppRole> GetAppRolesByAppGuid();
  List<string>? GetQebiUserRolesForUserGuid(Guid userGuid);
  List<QebiUser> GetAppUsersByAppGuid();
  QebiUser GetUserByPassWord(string password);
  QebiUser GetUserByPassWordAndAnswer(string password, string answer);
  QebiUser GetUserByPassWordAndToken(string password, string token);
  QebiUser GetUserByPrincipal(QebiRcp principal);
  QebiUser GetUserByUserGuid(Guid? userguid);
  QebiUser GetUserByUserGuid(string? userguid);
  QebiUser GetUserByUserName(string username);
  QebiUser GetUserByUserNameAndAnswer(string username, string answer);
  QebiUser GetUserByUserNameAndToken(string username, string token);
  QebiUser GetUserByUserNameAndUserGuid(string? username, Guid? userguid);
  List<string> GetUserRoleNamesByUserGuid(Guid? userguid);
  List<QebiUserRole> GetUserRolesByUserGuid(Guid? userguid);
  bool HasChanges();
  List<QebiAppRoleUxm> ListEditableQebiRoles(Guid? appGuid = null);
  List<QebiUserRoleUxm> ListEditableQebiUserRoles(Guid userGuid);
  List<QebiUserUxm> ListEditableQebiUsers();
  List<QebiAppRole> ListStorableQebiRoles(Guid? appGuid = null);
  List<QebiUserRole> ListStorableQebiUserRoles(Guid userGuid, Guid? appGuid = null);
  List<QebiUser> ListStorableQebiUsers();

  int? QebiUserSessionTimestamp(Guid? UserGuid); // TODO: check use/refs
  int? QebiAppDelete(Guid? AppGuidKey);
  int? QebiAppEdit(Guid? AppGuidKey, string AppName, string AppDescription);
  int? QebiLinkDelete(Guid? LinkGuidKey, Guid? AppGuidRef, Guid? UserGuidRef, Guid? RoleGuidRef);
  int? QebiLinkEdit(Guid? LinkGuidKey, Guid? AppGuidRef, Guid? UserGuidRef, Guid? RoleGuidRef);
  int? QebiAppRoleDelete(Guid? AppGuidRef, Guid? RoleGuidKey);
  int? QebiAppRoleEdit(Guid? AppGuidRef, Guid? RoleGuidKey, byte? RoleCodeRef, string Comment);
  int? QebiUserApprove(Guid? AppGuidRef, Guid? UserGuidKey, bool? UserIsApproved);
  int? QebiUserDelete(Guid? AppGuidRef, Guid? UserGuidKey);
  int? QebiUserEdit(Guid? AppGuidRef, Guid? UserGuidKey, string FirstName, string LastName, string UserName, string EmailAddress);
  int? QebiUserRegister(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserAlias, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string EmailAlternate, string WebsiteAddress, string Organization, string SecurityQuestion, string SecurityAnswer, string SecurityStamp, string SecurityToken, string PasswordHash, bool? AliasIsLocked, DateTime? DateUserCreated, DateTime? DateTokenExpired);
  int? QebiUserStamp(Guid? AppGuidRef, Guid? UserGuidKey); // TODO: check use/refs
  int? QebiUserUpdateEmail(Guid? AppGuidRef, Guid? UserGuidKey, string EmailAddress, string EmailAlternate, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateEmailConfirmed, DateTime? DateLastEdit, bool? EmailConfirmed);
  int? QebiUserUpdatePassword(Guid? AppGuidRef, Guid? UserGuidKey, string PasswordHash, string SecurityToken, DateTime? DateTokenExpired, DateTime? DatePasswordChanged, DateTime? DateLastEdit);
  int? QebiUserUpdateProfile(Guid? AppGuidRef, Guid? UserGuidKey, string UserAlias, string FirstName, string LastName, string Organization, string PhoneNumber, string PhoneAlternate, string SecurityQuestion, string SecurityAnswer, string WebsiteAddress, bool? AliasIsLocked, DateTime? DateProfileChanged, DateTime? DateLastEdit);
  int? QebiUserUpdateUsername(Guid? AppGuidRef, Guid? UserGuidKey, string UserName, string UserAlias, string SecurityToken, DateTime? DateTokenExpired, DateTime? DateUserNameChanged, DateTime? DateLastEdit);
  IQueryable<QebiUser> QueryStorableQebiUsers();
  RegisterUserUxm RegisterQebiUser(RegisterUserUxm editObj);

} // end interface

// end file