// ChangeUsernameUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangeUsernameUxm : ConfirmTokenUxm
{
  // required parameterless constructor
  public ChangeUsernameUxm() { InitPath(); }
  public ChangeUsernameUxm(Guid ug) { InitPath(); UserGuid = ug; }
  public ChangeUsernameUxm(string id, string ct) : base(id, ct) { InitPath(); }
  public ChangeUsernameUxm(string id, string ct, Int16 ws) : base(id, ct, ws) { InitPath(); }

  public void InitPath()
  {
    ReturnUrlPath = DepAnonModeResetUsername3;
  }

  [Display(Name = "Current password")]
  public string? OldPassword { get; set; } = ESS;

  [Display(Name = "Old username"), Required]
  [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
  public string? OldUsername { get; set; } = ESS;

  [Display(Name = "New username"), Required]
  [RegularExpression("[a-zA-Z0-9._]{8,32}", ErrorMessage = "Username must be 8 - 32 characters including alphanumeric, period '.' or underscore '_' ")]
  public string? NewUsername { get; set; } = ESS;

  [Display(Name = "Confirm new username")]
  [Compare("NewUsername", ErrorMessage = "The new username and its confirmation do not match.")]
  public string? AltUsername { get; set; } = ESS;

  public bool UsernameChanged { get; set; } = false;

} // end class

// end file