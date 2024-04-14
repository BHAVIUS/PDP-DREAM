// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class RegisterUserUxm : QebiUserUxm, IUserSecurityEdit
{
  public bool UserRegistered { get; set; } = false;

  [Display(Name = "What is the first prime number greater than the square of 3?"), Required]
  [Compare("NextPrime", ErrorMessage = "Incorrect answer")]
  public string? SimpleCaptcha { get; set; } = ESS;

  public string NextPrime { get; init; } = "11";

}
