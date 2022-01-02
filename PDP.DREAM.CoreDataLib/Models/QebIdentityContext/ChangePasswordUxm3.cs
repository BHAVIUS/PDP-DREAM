// ChangePasswordUxm3.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangePasswordUxm3
  {
    public ChangePasswordUxm3() { } // required parameterless constructor
    public ChangePasswordUxm3(string id, string ct)
    {
      UserName = id; SecurityToken = ct;
    }

    [Display(Name = "Current username")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public string? UserName { get; set; }

    [Display(Name = "Security token")]
    public string? SecurityToken { get; set; }

    [Display(Name = "New password")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string? NewPassword { get; set; }

    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and its confirmation do not match.")]
    public string? AltPassword { get; set; }

  }

}
