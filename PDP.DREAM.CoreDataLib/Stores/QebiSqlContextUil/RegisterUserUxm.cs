// RegisterUserUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class RegisterUserUxm : QebiUserUxm, IUserRegister
{
  public bool UserRegistered { get; set; }

  [Display(Name = "What is the first prime number greater than the square of 3?"), Required]
  [Compare("NextPrime", ErrorMessage = "Incorrect answer")]
  public string? SimpleCaptcha { get; set; } = string.Empty;

  public string NextPrime { get; init; } = "11";

}
