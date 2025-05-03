// IQebIdentity.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models;

public interface IUserIdent
{
  // Ids internal to the membership system
  Guid QebUserGuid { get; set; }
  Guid QebSessionGuid { get; set; }
  Guid QebApplicationGuid { get; }

  // Ids external to the membership system
  Guid QebAgentGuid { get; set; }
  Guid QebAgentInfosetGuid { get; set; }
}
public interface IUserSecurityView
{
  string? QebUserNameDisplayed { get; set; }
  string? SecurityQuestion { get; set; }
  string? SecurityAnswer { get; set; }
  string? SecurityToken { get; set; }
  bool UserIsApproved { get; set; }
}
public interface IUserSecurityEdit : IUserLogin, IUserSecurityView
{
  string? AltPassword { get; set; }
  string? PasswordHash { get; set; }
}

public interface IUserCore
{
  string? FirstName { get; set; }
  string? LastName { get; set; }
  string? PhoneNumber { get; set; }
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
public interface IUserLogin
{
  string? QebUserName { get; set; }
  string? PassWord { get; set; }
  string? ReturnUrl { get; set; }
  bool RememberMe { get; set; }
}

// compound interfaces

public interface IUserContact : IUserCore
{
  string? EmailSubject { get; set; }
  string? EmailBody { get; set; }
}

public interface IUserRegister : IUserSecurityEdit, IUserCore, IUserDates { }
public interface IUserProfileView : IUserSecurityView, IUserCore, IUserDates { }
public interface IUserProfileEdit : IUserSecurityEdit, IUserCore, IUserDates { }

// end file