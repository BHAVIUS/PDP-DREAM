// ChangeUsernameUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangeUsernameUxm : ConfirmTokenUxm
  {
    // required parameterless constructor
    public ChangeUsernameUxm() { }
    public ChangeUsernameUxm(Guid ug) { UserGuid = ug; }
    public ChangeUsernameUxm(string id) : base(id) { }
    public ChangeUsernameUxm(string id, string ct) : base(id, ct) { }
    public ChangeUsernameUxm(string id, string ct, Int16 ws) : base(id, ct, ws) { }

    [Display(Name = "Current password")]
    public string OldPassword { get; set; } = string.Empty;

    [Display(Name = "Old username"), Required]
    [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
    public string OldUsername { get; set; } = string.Empty;

    [Display(Name = "New username"), Required]
    [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
    public string NewUsername { get; set; } = string.Empty;

    [Display(Name = "Confirm new username")]
    [Compare("NewUsername", ErrorMessage = "The new username and its confirmation do not match.")]
    public string AltUsername { get; set; } = string.Empty;

    public bool UsernameChanged { get; set; } = false;
  }

}
