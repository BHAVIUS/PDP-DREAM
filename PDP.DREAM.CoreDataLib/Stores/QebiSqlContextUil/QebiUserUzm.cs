// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserUzm : IUserAdminEdit
{
  protected void CheckGuids()
  {
    if (AppGuid == EGS) { AppGuid = PDPSS.CiaamAppGuid; }
    if (UserGuid == EGS) { UserGuid = PdpNewGuid(); }
    // if (AgentGuid == EGS) { AgentGuid = PdpNewGuid(); }
  }
  public QebiUserUzm()
  {
    CheckGuids();
  }
  public QebiUserUzm(Guid appGuid, Guid usrGuid, 
    bool usrIsApproved, bool usrIsPerson, bool usrIsAgent,
    string firstName, string lastName, string userName, string userAlias,
    string emailAddress, string emailAlternate, 
    string secQuestion, string secAnswer)
  {
    AppGuid = appGuid; UserGuid = usrGuid; 
    UserIsApproved = usrIsApproved; UserIsPerson = usrIsPerson; UserIsAgent = usrIsAgent;
    FirstName = firstName; LastName = lastName; UserName = userName; UserAlias = userAlias;
    EmailAddress = emailAddress; EmailAlternate = emailAlternate;
    SecurityQuestion = secQuestion; SecurityAnswer = secAnswer; 
  }

  public Guid AppGuid { get; set; } = EGS;
  public Guid UserGuid { get; set; } = EGS;
 // public Guid AgentGuid { get; set; } = EGS;

  // ATTN: does not update in Telerik controls unless use simple standard property
  [Display(Name = "UserRoles")]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? UserRoleNames { get; set; } = ESS;

  [Display(Name = "Email (preferred)"), EmailAddress, Required]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? EmailAddress { get; set; } = ESS;

  [Display(Name = "Email (alternate)"), EmailAddress, Required]
  [StringLength(128, ErrorMessage = "String must be <=128 characters.")]
  public string? EmailAlternate { get; set; } = ESS;

  [Display(Name = "User Name"), Required]
  [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
  public string? UserName { get; set; } = ESS;

  [Display(Name = "User Alias"), Required]
  [RegularExpression("[a-zA-Z0-9._]{6,16}", ErrorMessage = "Username must be 6 - 16 characters including alphanumeric, period '.' or underscore '_' ")]
  public string? UserAlias { get; set; } = ESS;

  [Display(Name = "First Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? FirstName { get; set; } = ESS;

  [Display(Name = "Last Name"), Required]
  [StringLength(64, ErrorMessage = "String must be <=64 characters.")]
  public string? LastName { get; set; } = ESS;

  public string? SecurityQuestion { get; set; } = ESS;
  public string? SecurityAnswer { get; set; } = ESS;

  public string? Message { get; set; } = ESS;

  public bool UserIsApproved { get; set; } = false;
  public bool UserIsPerson { get; set; } = false;
  public bool UserIsAgent { get; set; } = false;

} // end class

// end file