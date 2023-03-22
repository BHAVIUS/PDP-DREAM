// ContactUserUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ContactUserUxm : IFormTaskUxm, IUserContact
{
  public ContactUserUxm() { } // required zero-param constructor
  public ContactUserUxm(string title) { FormTitle = title; }

  // begin IFormTaskUxm
  public string? FormTitle { get; set; } = string.Empty;
  public string? FormMessage { get; set; } = string.Empty;
  public bool FormCompleted { get; set; } = false;
  public bool ErrorOccurred { get; set; } = false;
  public Exception? Error { get; set; } = null;
  // end IFormTaskUxm

  [Display(Name = "First Name"), Required]
  public string? FirstName { get; set; } = string.Empty;

  [Display(Name = "Last Name"), Required]
  public string? LastName { get; set; } = string.Empty;

  [Display(Name = "Telephone"), Phone, Required]
  public string? PhoneNumber { get; set; } = string.Empty;

  [Display(Name = "Email (primary)"), EmailAddress, Required]
  public string? EmailAddress { get; set; } = string.Empty;

  [Display(Name = "Email (alternate)"), EmailAddress]
  public string? EmailAlternate { get; set; } = string.Empty;

  [Display(Name = "Website"), Url]
  public string? WebsiteAddress { get; set; } = string.Empty;

  [Display(Name = "Organization")]
  public string? Organization { get; set; } = string.Empty;

  [Display(Name = "Message Title"), Required]
  public string? EmailSubject { get; set; } = string.Empty;

  [Display(Name = "Message Text"), Required]
  public string? EmailBody { get; set; } = string.Empty;

} // end class

// end file