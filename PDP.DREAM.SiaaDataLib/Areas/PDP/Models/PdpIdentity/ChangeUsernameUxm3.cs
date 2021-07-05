using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
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
