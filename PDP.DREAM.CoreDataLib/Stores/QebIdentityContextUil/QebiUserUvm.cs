// QebiUserUvm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiUserUvm : PdpDataEntity, IUserProfileView
{
  public QebiUserUvm()
  {
    if (AppGuid == Guid.Empty) { AppGuid = PDPSS.AppSecureUiaaGuid; }
  }

  public Guid AppGuid { get; set; } = Guid.Empty;
  public Guid UserGuid { get; set; } = Guid.Empty;
  public Guid SessionGuid { get; set; } = Guid.Empty;

  public virtual string? EmailAddress { get; set; } = string.Empty;
  public virtual string? EmailAlternate { get; set; } = string.Empty;
  public virtual string? SecurityQuestion { get; set; } = string.Empty;
  public virtual string? SecurityAnswer { get; set; } = string.Empty;
  public virtual string? SecurityStamp { get; set; } = string.Empty;
  public virtual string? SecurityToken { get; set; } = string.Empty;
  public virtual string? QebUserName { get; set; } = string.Empty;
  public virtual string? QebUserNameDisplayed { get; set; } = string.Empty;
  public virtual string? FirstName { get; set; } = string.Empty;
  public virtual string? LastName { get; set; } = string.Empty;
  public virtual string? PersonName { get; set; } = string.Empty;
  public virtual string? PhoneNumber { get; set; } = string.Empty;
  public virtual string? WebsiteAddress { get; set; } = string.Empty;
  public virtual string? Organization { get; set; } = string.Empty;
  public virtual string? ReturnUrl { get; set; } = string.Empty;
  public virtual string? Message { get; set; } = string.Empty;

  public virtual DateTime? DateEmailConfirmed { get; set; } = null;
  public virtual DateTime? DateLastEdit { get; set; } = null;
  public virtual DateTime? DateLastLogin { get; set; } = null;
  public virtual DateTime? DateLastLockout { get; set; } = null;
  public virtual DateTime? DatePasswordChanged { get; set; } = null;
  public virtual DateTime? DateProfileChanged { get; set; } = null;
  public virtual DateTime? DateTokenExpired { get; set; } = null;
  public virtual DateTime? DateUserCreated { get; set; } = null;

  public virtual bool RememberMe { get; set; } = false;
  // public bool RequireASQ { get; set; } = false;
  public virtual bool RequireSecTok { get; set; } = false;
  public virtual bool UserIsApproved { get; set; } = false;
  // public short WizardStep { get; set; } = 0;

} // end class

// end file