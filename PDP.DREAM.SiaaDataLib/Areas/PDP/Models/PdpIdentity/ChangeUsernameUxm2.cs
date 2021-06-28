using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class ChangeUsernameUxm2 
  {
    public ChangeUsernameUxm2() { } // required parameterless constructor
    public ChangeUsernameUxm2(string password) { PassWord = password; }

    public Guid? UserGuid { get; set; }
    public string? PassWord { get; set; }

    [Display(Name = "Security Question")]
    public string? SecurityQuestion { get; set; } 

    [Display(Name = "Security Answer")]
    public string? SecurityAnswer { get; set; }

  }

}
