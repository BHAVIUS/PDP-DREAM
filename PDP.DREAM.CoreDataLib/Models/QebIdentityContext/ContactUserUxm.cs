// ContactUserUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models;

public class ContactUserUxm : RestTaskUxm, IUserContact
{
  [Display(Name = "First Name"), Required]
  public string FirstName { get; set; } = string.Empty;

  [Display(Name = "Last Name"), Required]
  public string LastName { get; set; } = string.Empty;

  [Display(Name = "Telephone"), Phone]
  public string PhoneNumber { get; set; } = string.Empty;

  [Required, Display(Name = "Email (primary)"), EmailAddress]
  public string EmailAddress { get; set; } = string.Empty;

  [Display(Name = "Email (alternate)"), EmailAddress]
  public string EmailAlternate { get; set; } = string.Empty;

  [Display(Name = "Website"), Url]
  public string WebsiteAddress { get; set; } = string.Empty;

  [Display(Name = "Organization")]
  public string Organization { get; set; } = string.Empty;

  [Display(Name = "Message Title"), Required]
  public string EmailSubject { get; set; } = string.Empty;

  [Display(Name = "Message Text"), Required]
  public string EmailBody { get; set; } = string.Empty;

  public override void UpdateRestContext(PdpRestContext prc)
  {
    prc.FirstName = this.FirstName;
    prc.LastName = this.LastName;
    prc.PhoneNumber = this.PhoneNumber;
    prc.EmailAddress = this.EmailAddress;
    prc.EmailAlternate = this.EmailAlternate;
    prc.WebsiteAddress = this.WebsiteAddress;
    prc.Organization = this.Organization;
    prc.EmailSubject = this.EmailSubject;
    prc.EmailBody = this.EmailBody;
  }

}
