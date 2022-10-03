// ChangePasswordUxm3.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangePasswordUxm3 : ConfirmTokenUxm, IFormTaskUxm
{
  // required parameterless constructor
  public ChangePasswordUxm3() { }
  public ChangePasswordUxm3(string id, string ct)
  {
    UserName = id; SecurityToken = ct;
  }

  [Display(Name = "New password")]
  [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
  public string? NewPassword { get; set; } = string.Empty;

  [Display(Name = "Confirm new password")]
  [Compare("NewPassword", ErrorMessage = "The new password and its confirmation do not match.")]
  public string? AltPassword { get; set; } = string.Empty;

} // end class

// end file