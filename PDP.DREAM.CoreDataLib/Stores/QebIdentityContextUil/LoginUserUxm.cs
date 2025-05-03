// LoginUserUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class LoginUserUxm : IFormTaskUxm
  {
    // begin IFormTaskUxm
    public string? FormTitle { get; set; } = string.Empty;
    public string? FormMessage { get; set; } = string.Empty;
    public bool FormCompleted { get; set; }
    public bool ErrorOccurred { get; set; }
    public Exception? Error { get; set; } = null;
    // end IFormTaskUxm

    [Required, Display(Name = "Username")]
    [DataType(DataType.Text)] // non-nullable
    public string UserName { get; set; } = string.Empty;

    [Required, Display(Name = "Password")]
    [DataType(DataType.Password)] // non-nullable
    public string PassWord { get; set; } = string.Empty;

    [Display(Name = "Remember?")]
    public bool RememberMe { get; set; } = false;

    [Display(Name = "Recover?")]
    public bool RecoverMe { get; set; } = false;

    public DateTime? DateLastLogin { get; set; } = null;

    public string? ReturnUrl { get; set; } = string.Empty;

    public string? QueryString { get; set; } = string.Empty;

    public bool UserLoginOk { get; set; } = false;

  }

}
