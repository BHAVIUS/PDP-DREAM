// ChangePasswordUxm3.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangePasswordUxm3 : ConfirmTokenUxm
{
  // required parameterless constructor
  public ChangePasswordUxm3() { }
  public ChangePasswordUxm3(string id, string ct)
  {
    UserName = id; SecurityToken = ct;
  }

  [Display(Name = "New password")]
  [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
  public string? NewPassword { get; set; } = ESS;

  [Display(Name = "Confirm new password")]
  [Compare("NewPassword", ErrorMessage = "The new password and its confirmation do not match.")]
  public string? AltPassword { get; set; } = ESS;

} // end class

// end file