using System.ComponentModel.DataAnnotations;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class RegisterUserUxm : QebUserUxm, IUserRegister
  {
    [Display(Name = "What is the first prime number greater than the square of 3?"), Required]
    [Compare("NextPrime", ErrorMessage = "Incorrect answer")]
    public string SimpleCaptcha { get; set; } = string.Empty;
    public string NextPrime { get { return thePrime; } }
    private string thePrime = "11";

    public string Message { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool UserRegistered { get; set; }
  }

}
