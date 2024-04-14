// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public interface IUserCore
{
  string? UserName { get; set; }
  string? FirstName { get; set; }
  string? LastName { get; set; }
  string? PhoneNumber { get; set; }
  string? PhoneAlternate { get; set; }
  string? EmailAddress { get; set; }
  string? EmailAlternate { get; set; }
  string? WebsiteAddress { get; set; }
  string? Organization { get; set; }
}

public interface IUserDates
{
  DateTime? DateUserCreated { get; set; }
  DateTime? DateTokenExpired { get; set; }
  DateTime? DateEmailConfirmed { get; set; }
  DateTime? DatePasswordChanged { get; set; }
  DateTime? DateProfileChanged { get; set; }
  DateTime? DateLastEdit { get; set; }
  DateTime? DateLastLogin { get; set; }
}

public interface IUserSecurityView : IUserCore, IUserDates
{
  Guid AppGuid { get; set; }
  Guid UserGuid { get; set; }

  string? UserAlias { get; set; }

  bool UserIsApproved { get; set; }
  bool UserIsPerson { get; set; }
  bool UserIsAgent { get; set; }
}

public interface IUserSecurityEdit : IUserSecurityView
{
  string? PassWord { get; set; }
  string? AltPassword { get; set; }
  string? PasswordHash { get; set; }
  string? ReturnUrl { get; set; }

  string? SecurityQuestion { get; set; }
  string? SecurityAnswer { get; set; }
  string? SecurityStamp { get; set; }
  string? SecurityToken { get; set; }

  bool RememberMe { get; set; }
  bool RequireSecTok { get; set; }
}

public interface IEmailMessage : IUserCore
{
  string? EmailSubject { get; set; }
  string? EmailBody { get; set; }
}

public interface IUserProfileEdit
{
  Guid UserGuid { get; set; }

  DateTime? DateLastEdit { get; set; }
  DateTime? DateProfileChanged { get; set; }

  string? UserName { get; set; }
  string? UserAlias { get; set; }
  string? UserRoleNames { get; set; }
  string? FirstName { get; set; }
  string? LastName { get; set; }
  string? Organization { get; set; }
  string? PhoneNumber { get; set; }
  string? PhoneAlternate { get; set; }
  string? WebsiteAddress { get; set; }
  string? SecurityQuestion { get; set; }
  string? SecurityAnswer { get; set; }
  string? PassWord { get; set; }
  string? SecurityToken { get; set; }
}

public interface IUserAdminEdit
{
  Guid UserGuid { get; set; }
  string? UserName { get; set; }
  string? UserAlias { get; set; }
  string? UserRoleNames { get; set; }
  string? EmailAddress { get; set; }
  string? EmailAlternate { get; set; }
  string? FirstName { get; set; }
  string? LastName { get; set; }
  string? SecurityQuestion { get; set; }
  string? SecurityAnswer { get; set; }
  string? Message { get; set; }
  bool UserIsApproved { get; set; }
  bool UserIsPerson { get; set; }
  bool UserIsAgent { get; set; }
}

// end file