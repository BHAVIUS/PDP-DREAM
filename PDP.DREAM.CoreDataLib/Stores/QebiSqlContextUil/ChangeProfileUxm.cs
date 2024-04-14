// ChangeProfileUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangeProfileUxm : FormTaskUxmBase, IUserProfileEdit
{
  // required parameterless constructor
  public ChangeProfileUxm() { }
  public ChangeProfileUxm(IUserProfileEdit p)
  {
    this.DateLastEdit = p.DateLastEdit;
    this.DateProfileChanged = p.DateProfileChanged;
    this.UserName = p.UserName;
    this.UserAlias = p.UserAlias;
    this.UserRoleNames = p.UserRoleNames;
    this.FirstName = p.FirstName;
    this.LastName = p.LastName;
    this.Organization = p.Organization;
    this.PhoneNumber = p.PhoneNumber;
    this.PhoneAlternate = p.PhoneAlternate;
    this.SecurityAnswer = p.SecurityAnswer;
    this.SecurityQuestion = p.SecurityQuestion;
    this.WebsiteAddress = p.WebsiteAddress;
  }

  public Guid UserGuid { get; set; } = EGS;
  public string? UserName { get; set; } = ESS;

  public DateTime? DateLastEdit { get; set; } = null;
  public DateTime? DateProfileChanged { get; set; } = null;

  public string? SecurityToken { get; set; } = ESS;
  public string? SecurityQuestion { get; set; } = ESS;
  public string? SecurityAnswer { get; set; } = ESS;

  [Display(Name = "First Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? FirstName { get; set; } = ESS;

  [Display(Name = "Last Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? LastName { get; set; } = ESS;

  [Display(Name = "User Alias")]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? UserAlias { get; set; } = ESS;

  [Display(Name = "User Roles")]
  public string? UserRoleNames { get; set; } = ESS;

  [Display(Name = "Phone Number")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? PhoneNumber { get; set; } = ESS;

  [Display(Name = "Phone Alternate")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? PhoneAlternate { get; set; } = ESS;

  [Display(Name = "Website")]
  [StringLength(256, ErrorMessage = "String must be <=256 characters.")]
  public string? WebsiteAddress { get; set; } = ESS;

  [Display(Name = "Organization")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? Organization { get; set; } = ESS;

  [Display(Name = "Password"), Required]
  [StringLength(32, ErrorMessage = "The {0} must be from {2} to <=32 characters.", MinimumLength = 6)]
  public string? PassWord { get; set; } = ESS;

}