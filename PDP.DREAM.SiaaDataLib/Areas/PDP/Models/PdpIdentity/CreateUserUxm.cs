using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class CreateUserUxm
  {
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
  }

}
