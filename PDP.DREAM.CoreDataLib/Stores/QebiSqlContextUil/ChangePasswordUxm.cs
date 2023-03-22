// ChangePasswordUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangePasswordUxm : ConfirmTokenUxm, IFormTaskUxm
{
  // required parameterless constructor
  public ChangePasswordUxm() { InitPath(); }
  public ChangePasswordUxm(Guid ug) { InitPath(); UserGuid = ug; }
  public ChangePasswordUxm(string id, string ct) : base(id, ct) { InitPath(); }
  public ChangePasswordUxm(string id, string ct, short ws) : base(id, ct, ws) { InitPath(); }

  public void InitPath()
  {
    ReturnUrlPath = "/NPDS/AnonMode/ResetPassword3";
  }

  [Display(Name = "Current username")]
  [DataType(DataType.Text)]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? OldUsername { get; set; } = string.Empty;

  [Display(Name = "New password")]
  [DataType(DataType.Text)]
  [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
  public string? NewPassword { get; set; } = string.Empty;

  [Display(Name = "Confirm new password")]
  [DataType(DataType.Text)]
  [Compare("NewPassword", ErrorMessage = "The new password and its confirmation do not match.")]
  public string? AltPassword { get; set; } = string.Empty;

  public bool PasswordChanged { get; set; } = false;

} // end class

// end file