// ChangeUsernameUxm3.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangeUsernameUxm3
  {
    public ChangeUsernameUxm3() { } // required parameterless constructor
    public ChangeUsernameUxm3(string id, string ct)
    {
      PassWord = id; SecurityToken = ct;
    }

    [Display(Name = "Current password")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string? PassWord { get; set; }

    [Display(Name = "Security token")]
    public string? SecurityToken { get; set; }

    [Display(Name = "New username")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public string? NewUsername { get; set; }

    [Display(Name = "Confirm new username")]
    [Compare("NewUsername", ErrorMessage = "The new password and its confirmation do not match.")]
    public string? AltUsername { get; set; }

  }

}
