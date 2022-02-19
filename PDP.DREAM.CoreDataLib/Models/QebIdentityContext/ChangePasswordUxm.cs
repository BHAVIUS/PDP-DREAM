// ChangePasswordUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangePasswordUxm : ConfirmTokenUxm
  {
    // required parameterless constructor
    public ChangePasswordUxm() { }
    public ChangePasswordUxm(Guid ug) { UserGuid = ug; }
    public ChangePasswordUxm(string id) : base(id) { }
    public ChangePasswordUxm(string id, string ct) : base(id, ct) { }
    public ChangePasswordUxm(string id, string ct, Int16 ws) : base(id, ct, ws) { }

    [Display(Name = "Current username")]
    [DataType(DataType.Text)]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public string OldUsername { get; set; } = string.Empty;

    [Display(Name = "New password")]
    [DataType(DataType.Text)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string NewPassword { get; set; } = string.Empty;

    [Display(Name = "Confirm new password")]
    [DataType(DataType.Text)]
    [Compare("NewPassword", ErrorMessage = "The new password and its confirmation do not match.")]
    public string AltPassword { get; set; } = string.Empty;

    public bool PasswordChanged { get; set; } = false;
  }

}
