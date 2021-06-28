using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class ChangePasswordUxm2 
  {
    public ChangePasswordUxm2() { } // required parameterless constructor
    public ChangePasswordUxm2(string username) { UserName = username; }

    public Guid? UserGuid { get; set; }
    public string? UserName { get; set; }

    [Display(Name = "Security Question")]
    public string? SecurityQuestion { get; set; } 

    [Display(Name = "Security Answer")]
    public string? SecurityAnswer { get; set; }

  }

}
