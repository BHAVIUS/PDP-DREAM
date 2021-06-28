using System.ComponentModel.DataAnnotations;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
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

}