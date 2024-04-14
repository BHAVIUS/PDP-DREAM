// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserUxm : FormTaskUxmBase, IUserProfileEdit
{
  protected void CheckGuids()
  {
    if (AppGuid == EGS) { AppGuid = PDPSS.CiaamAppGuid; }
    if (UserGuid == EGS) { UserGuid = PdpNewGuid(); }
  }
  public QebiUserUxm()
  {
    CheckGuids();
  }
  public QebiUserUxm(IUserProfileEdit p)
  {
    CheckGuids();
    this.DateLastEdit = p.DateLastEdit;
    this.DateProfileChanged = p.DateProfileChanged;
    this.UserName = p.UserName;
    this.UserAlias = p.UserAlias;
    this.FirstName = p.FirstName;
    this.LastName = p.LastName;
    this.PhoneNumber = p.PhoneNumber;
    this.PhoneAlternate = p.PhoneAlternate;
    this.WebsiteAddress = p.WebsiteAddress;
    this.Organization = p.Organization;
    this.SecurityQuestion = p.SecurityQuestion;
    this.SecurityAnswer = p.SecurityAnswer;
  }
  public QebiUserUxm(IUserAdminEdit p)
  {
    CheckGuids();
    this.FirstName = p.FirstName;
    this.LastName = p.LastName;
    this.UserName = p.UserName;
    this.UserAlias = p.UserAlias;
    this.EmailAddress = p.EmailAddress;
    this.EmailAlternate = p.EmailAlternate;
    this.UserIsApproved = p.UserIsApproved;
  }
  public QebiUserUxm(Guid appGuid, Guid usrGuid,
     bool isApproved, bool isPerson, bool isAgent,
    string firstName, string lastName, string userName, string userAlias, string emailAddress)
  {
    AppGuid = appGuid; UserGuid = usrGuid;
    UserIsApproved = isApproved; UserIsPerson = isPerson; UserIsAgent = isAgent;
    FirstName = firstName; LastName = lastName; UserName = userName; UserAlias = userAlias;
    EmailAddress = emailAddress;
  }

  public Guid AppGuid { get; set; } = EGS;
  public Guid UserGuid { get; set; } = EGS;

  public virtual string? ReturnUrl { get; set; } = ESS;
  public virtual string? Message { get; set; } = ESS;

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
  public virtual bool UserIsPerson { get; set; } = false;
  public virtual bool UserIsAgent { get; set; } = false;
  // public short WizardStep { get; set; } = 0;

  public virtual string? SecurityStamp { get; set; } = ESS;
  public virtual string? SecurityToken { get; set; } = ESS;

  // ATTN: does not update in Telerik controls unless use simple standard property
  [Display(Name = "User Roles")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? UserRoleNames { get; set; } = ESS;

  [Display(Name = "Email (preferred)"), EmailAddress, Required]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? EmailAddress { get; set; } = ESS;

  [Display(Name = "Email (alternate)"), EmailAddress]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? EmailAlternate
  {
    get { if (string.IsNullOrEmpty(altEmail)) { return EmailAddress; } else { return altEmail; } }
    set { altEmail = value; }
  }
  private string? altEmail = ESS;

  [Display(Name = "Security Question")]
  [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 6)]
  public string? SecurityQuestion { get; set; } = ESS;

  [Display(Name = "Security Answer")]
  [StringLength(64, ErrorMessage = "The {0} must be from {2} to <=64 characters.", MinimumLength = 4)]
  public string? SecurityAnswer { get; set; } = ESS;

  [Display(Name = "User Name"), Required]
  [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
  public string? UserName { get; set; } = ESS;

  [Display(Name = "Display (Screen) Name")]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? UserAlias
  {
    get {
      if (string.IsNullOrEmpty(usrAlias)) { usrAlias = UserName; }
      return usrAlias;
    }
    set { usrAlias = value; }
  }
  private string? usrAlias = ESS;

  [Display(Name = "First Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? FirstName { get; set; } = ESS;

  [Display(Name = "Last Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? LastName { get; set; } = ESS;

  [Display(Name = "Phone (preferred)"), Phone, Required]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? PhoneNumber { get; set; } = ESS;

  [Display(Name = "Phone (alternate)"), Phone]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? PhoneAlternate { get; set; } = ESS;

  [Display(Name = "Website")]
  [StringLength(256, ErrorMessage = "String must be <=256 characters.")]
  public string? WebsiteAddress { get; set; } = ESS;

  [Display(Name = "Organization")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? Organization { get; set; } = ESS;

  [Display(Name = "Password")]
  [StringLength(32, ErrorMessage = "The {0} must be from {2} to <=32 characters.", MinimumLength = 6)]
  public virtual string? PassWord { get; set; } = ESS;

  [Display(Name = "Confirm Password")]
  [Compare("PassWord", ErrorMessage = "The Password and its confirmation do not match.")]
  public string? AltPassword { get; set; } = ESS;
  public string? PasswordHash { get; set; } = ESS;

} // end class

// end file