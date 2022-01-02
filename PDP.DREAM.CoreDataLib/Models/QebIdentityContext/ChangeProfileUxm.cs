// ChangeProfileUxm.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangeProfileUxm : QebiUserUvm, IUserProfileEdit
  {
    // required parameterless constructor
    public ChangeProfileUxm() { }
    public ChangeProfileUxm(IUserProfileEdit p)
    {
      this.DateLastEdit = p.DateLastEdit;
      this.DateProfileChanged = p.DateProfileChanged;
      this.UserNameDisplayed = p.UserNameDisplayed;
      this.FirstName = p.FirstName;
      this.LastName = p.LastName;
      this.Organization = p.Organization;
      this.PhoneNumber = p.PhoneNumber;
      this.SecurityAnswer = p.SecurityAnswer;
      this.SecurityQuestion = p.SecurityQuestion;
      this.WebsiteAddress = p.WebsiteAddress;
    }

    [Display(Name = "First Name"), Required]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string? FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name"), Required]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string? LastName { get; set; } = string.Empty;

    [Display(Name = "Display Name")]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string? UserNameDisplayed { get; set; } = string.Empty;

    [Display(Name = "Telephone")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public override string? PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Website")]
    [StringLength(256, ErrorMessage = "String must be <=256 characters.")]
    public override string? WebsiteAddress { get; set; } = string.Empty;

    [Display(Name = "Organization")]
    [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
    public override string? Organization { get; set; } = string.Empty;

    [Display(Name = "Password"), Required]
    [StringLength(32, ErrorMessage = "The {0} must be from {2} to <=32 characters.", MinimumLength = 6)]
    public string? PassWord { get; set; } = string.Empty;

    // do not require use in current ChangeProfile form
    public string? AltPassword { get; set; } = string.Empty;
    public string? PasswordHash { get; set; } = string.Empty;

    public bool ProfileChanged { get; set; } = false;

  }

}