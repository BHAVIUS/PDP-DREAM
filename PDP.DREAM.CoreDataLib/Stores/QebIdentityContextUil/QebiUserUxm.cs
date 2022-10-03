// QebiUserUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserUxm : QebiUserUvm, IUserProfileEdit, IFormTaskUxm
{
  public QebiUserUxm()
  {
    if (AppGuid == Guid.Empty) { AppGuid = PDPSS.AppSecureUiaaGuid; }
  }
  public QebiUserUxm(IUserProfileEdit p)
  {
    this.DateLastEdit = p.DateLastEdit;
    this.DateProfileChanged = p.DateProfileChanged;
    this.QebUserNameDisplayed = p.QebUserNameDisplayed;
    this.FirstName = p.FirstName;
    this.LastName = p.LastName;
    this.Organization = p.Organization;
    this.PhoneNumber = p.PhoneNumber;
    this.SecurityAnswer = p.SecurityAnswer;
    this.SecurityQuestion = p.SecurityQuestion;
    this.WebsiteAddress = p.WebsiteAddress;
  }
  public QebiUserUxm(Guid appGuid, Guid usrGuid, string firstName, string lastName,
    string userName, string emailAddress, bool isApproved)
  {
    AppGuid = appGuid; UserGuid = usrGuid; FirstName = firstName; LastName = lastName;
    QebUserName = userName; EmailAddress = emailAddress; UserIsApproved = isApproved;
  }

  // begin IFormTaskUxm
  public string? FormTitle { get; set; } = string.Empty;
  public string? FormMessage { get; set; } = string.Empty;
  public bool FormCompleted { get; set; }
  public bool ErrorOccurred { get; set; }
  public Exception? Error { get; set; } = null;
  // end IFormTaskUxm


  // ATTN: does not update in Telerik controls unless use simple standard property
  [Display(Name = "UserRoles")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? UserRoleNames { get; set; } = string.Empty;

  [Display(Name = "Email (primary)"), Required, EmailAddress]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public override string? EmailAddress { get; set; } = string.Empty;

  [Display(Name = "Email (alternate)"), EmailAddress]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public override string? EmailAlternate
  {
    get { if (string.IsNullOrEmpty(altEmail)) { return EmailAddress; } else { return altEmail; } }
    set { altEmail = value; }
  }
  private string? altEmail = string.Empty;

  [Display(Name = "Security Question")]
  [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 6)]
  public override string? SecurityQuestion { get; set; } = string.Empty;

  [Display(Name = "Security Answer")]
  [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 4)]
  public override string? SecurityAnswer { get; set; } = string.Empty;

  [Display(Name = "Username"), Required]
  // [StringLength(32,  MinimumLength = 8, ErrorMessage = "Username must be from 8 to <=32 characters.")]
  [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
  public override string? QebUserName { get; set; } = string.Empty;

  [Display(Name = "Display (Screen) Name")]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public override string? QebUserNameDisplayed
  {
    get {
      if (string.IsNullOrEmpty(usrNamDisp)) { usrNamDisp = QebUserName; }
      return usrNamDisp;
    }
    set { usrNamDisp = value; }
  }
  private string? usrNamDisp = string.Empty;

  [Display(Name = "First Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public override string? FirstName { get; set; } = string.Empty;

  [Display(Name = "Last Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public override string? LastName { get; set; } = string.Empty;

  [Display(Name = "Telephone")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public override string? PhoneNumber { get; set; } = string.Empty;

  [Display(Name = "Website")]
  [StringLength(256, ErrorMessage = "String must be <=256 characters.")]
  public override string? WebsiteAddress { get; set; } = string.Empty;

  [Display(Name = "Organization")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public override string? Organization { get; set; } = string.Empty;

  [Display(Name = "Password")]
  [StringLength(32, ErrorMessage = "The {0} must be from {2} to <=32 characters.", MinimumLength = 6)]
  public virtual string? PassWord { get; set; } = string.Empty;

  [Display(Name = "Confirm Password")]
  [Compare("PassWord", ErrorMessage = "The Password and its confirmation do not match.")]
  public virtual string? AltPassword { get; set; } = string.Empty;
  public virtual string? PasswordHash { get; set; } = string.Empty;

  private IList<string>? userRoleList = new List<string>() { string.Empty };
  public IList<string>? UserRoleList
  {
    get {
      if (userRoleList == null)
      {
        if (!string.IsNullOrEmpty(UserRoleNames)) { userRoleList = UserRoleNames.SplitOrStringToList(); }
        else { userRoleList = new List<string>() { string.Empty }; }
      }
      return userRoleList;
    }
    set {
      if (value != null)
      {
        userRoleList = value;
        UserRoleNames = userRoleList.JoinListToOrString();
      }
    }
  }

}

// end file