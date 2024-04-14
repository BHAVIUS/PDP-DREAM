// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ContactSiteUxm : FormTaskUxmBase, IEmailMessage
{
  public ContactSiteUxm() { } // required zero-param constructor
  public ContactSiteUxm(string title) { FormTitle = title; }

  public string? UserName { get; set; } = ESS;

  [Display(Name = "First Name"), Required]
  public string? FirstName { get; set; } = ESS;

  [Display(Name = "Last Name"), Required]
  public string? LastName { get; set; } = ESS;

  [Display(Name = "Phone (preferred))"), Phone, Required]
  public string? PhoneNumber { get; set; } = ESS;

  [Display(Name = "Phone (alternate)"), Phone]
  public string? PhoneAlternate { get; set; } = ESS;

  [Display(Name = "Email (preferred)"), EmailAddress, Required]
  public string? EmailAddress { get; set; } = ESS;

  [Display(Name = "Email (alternate)"), EmailAddress]
  public string? EmailAlternate { get; set; } = ESS;

  [Display(Name = "Website"), Url]
  public string? WebsiteAddress { get; set; } = ESS;

  [Display(Name = "Organization")]
  public string? Organization { get; set; } = ESS;

  [Display(Name = "Message Title"), Required]
  public string? EmailSubject { get; set; } = ESS;

  [Display(Name = "Message Text"), Required]
  public string? EmailBody { get; set; } = ESS;

} // end class

// end file