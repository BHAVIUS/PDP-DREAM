using System;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public interface IUserIdent
  {
    // Ids internal to the membership system
    Guid UserGuid { get; set; }
    Guid SessionGuid { get; set; }
    Guid ApplicationGuid { get; }

    // Ids external to the membership system
    Guid AgentGuid { get; set; }
    Guid AgentInfosetGuid { get; set; }
  }
  public interface IUserSecurityView
  {
    string? SecurityQuestion { get; set; }
    string? SecurityAnswer { get; set; }
    string? SecurityToken { get; set; }
    bool UserIsApproved { get; set; }
  }
  public interface IUserSecurityEdit : IUserLogin, IUserSecurityView
  {
    string? UserNameDisplayed { get; set; }
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
    string? UserName { get; set; }
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

  public interface IUserStore : IUserIdent, IUserSecurityView, IUserCore, IUserDates
  {
    // extension interface for additional profile fields
    // may also serve as base for other models
  }
  public interface IUserProfileView : IUserSecurityView, IUserCore, IUserDates { }
  public interface IUserProfileEdit : IUserSecurityEdit, IUserCore, IUserDates { }

  // IPdpNetUser serves as umbrella interface for user forms during transition
  // TODO: eliminate this interface after transition to new asp.net core/standard
  // TODO: or else devise alternative that maintains adequate security with generic typing on PdpRestContext
  public interface IPdpNetUser : IUserIdent, IUserLogin, IUserContact, IUserProfileEdit { }

}
