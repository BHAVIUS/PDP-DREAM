using System.ComponentModel.DataAnnotations;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class QebUserUxm : QebUserUvm, IUserProfileEdit
  {
    public QebUserUxm() { }
    public QebUserUxm(IUserProfileEdit p)
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

    [Display(Name = "Email (primary)"), Required, EmailAddress]
    [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
    public override string EmailAddress { get; set; } = string.Empty;

    [Display(Name = "Email (alternate)"), EmailAddress]
    [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
    public override string EmailAlternate
    {
      get { if (string.IsNullOrEmpty(altEmail)) { return EmailAddress; } else { return altEmail; } }
      set { altEmail = value; }
    }
    private string altEmail = string.Empty;

    [Display(Name = "Security Question"), Required]
    [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 6)]
    public override string SecurityQuestion { get; set; } = string.Empty;

    [Display(Name = "Security Answer"), Required]
    [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 4)]
    public override string SecurityAnswer { get; set; } = string.Empty;

    [Display(Name = "Username"), Required]
    // [StringLength(32,  MinimumLength = 8, ErrorMessage = "Username must be from 8 to <=32 characters.")]
    [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
    public override string UserName { get; set; } = string.Empty;

    [Display(Name = "Display (Screen) Name")]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string UserNameDisplayed
    {
      get
      {
        if (string.IsNullOrEmpty(usrNamDisp)) { usrNamDisp = UserName; }
        return usrNamDisp;
      }
      set { usrNamDisp = value; }
    }
    private string usrNamDisp = string.Empty;

    [Display(Name = "First Name"), Required]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name"), Required]
    [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
    public override string LastName { get; set; } = string.Empty;

    [Display(Name = "Telephone")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public override string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Website")]
    [StringLength(256, ErrorMessage = "String must be <=256 characters.")]
    public override string WebsiteAddress { get; set; } = string.Empty;

    [Display(Name = "Organization")]
    [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
    public override string Organization { get; set; } = string.Empty;

    [Display(Name = "Password"), Required]
    [StringLength(32, ErrorMessage = "The {0} must be from {2} to <=32 characters.", MinimumLength = 6)]
    public virtual string PassWord { get; set; } = string.Empty;

    [Display(Name = "Confirm Password")]
    [Compare("PassWord", ErrorMessage = "The Password and its confirmation do not match.")]
    public virtual string AltPassword { get; set; } = string.Empty;
    public virtual string PasswordHash { get; set; } = string.Empty;

  }

}