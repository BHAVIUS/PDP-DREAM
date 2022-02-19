// QebUserUvm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiUserUvm : IUserProfileView
{
  public Guid AppGuidRef { get; set; } = PdpSiteSettings.Values.AppSecureUiaaGuid;
  public Guid UserGuidKey { get; set; }
  public Guid SessionGuidRef { get; set; }

  public virtual string? EmailAddress { get; set; } = string.Empty;
  public virtual string? EmailAlternate { get; set; } = string.Empty;
  public virtual string? SecurityQuestion { get; set; } = string.Empty;
  public virtual string? SecurityAnswer { get; set; } = string.Empty;
  public virtual string? SecurityStamp { get; set; } = string.Empty;
  public virtual string? SecurityToken { get; set; } = string.Empty;
  public virtual string? UserName { get; set; } = string.Empty;
  public virtual string? UserNameDisplayed { get; set; } = string.Empty;
  public virtual string? FirstName { get; set; } = string.Empty;
  public virtual string? LastName { get; set; } = string.Empty;
  public virtual string? PersonName { get; set; } = string.Empty;
  public virtual string? PhoneNumber { get; set; } = string.Empty;
  public virtual string? WebsiteAddress { get; set; } = string.Empty;
  public virtual string? Organization { get; set; } = string.Empty;
  public virtual string? ReturnUrl { get; set; } = string.Empty;
  public virtual string? Message { get; set; } = string.Empty;

  public virtual DateTime? DateUserCreated { get; set; } = null;
  public virtual DateTime? DateTokenExpired { get; set; } = null;
  public virtual DateTime? DateEmailConfirmed { get; set; } = null;
  public virtual DateTime? DatePasswordChanged { get; set; } = null;
  public virtual DateTime? DateProfileChanged { get; set; } = null;
  public virtual DateTime? DateLastEdit { get; set; } = null;
  public virtual DateTime? DateLastLogin { get; set; } = null;
  public virtual DateTime? DateLastLockout { get; set; } = null;

  public virtual bool UserIsApproved { get; set; } = false;
  public virtual bool RememberMe { get; set; } = false;
}
