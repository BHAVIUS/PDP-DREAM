// ChangeUsernameUxm3.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangeUsernameUxm3 : ConfirmTokenUxm, IFormTaskUxm
{
  // required parameterless constructor
  public ChangeUsernameUxm3() { }
  public ChangeUsernameUxm3(string id, string ct)
  {
    PassWord = id; SecurityToken = ct;
  }

  [Display(Name = "New username")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public string? NewUsername { get; set; }

  [Display(Name = "Confirm new username")]
  [Compare("NewUsername", ErrorMessage = "The new password and its confirmation do not match.")]
  public string? AltUsername { get; set; }

} // end clas

// end file