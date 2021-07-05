using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class ChangeUsernameUxm1 
  {
    public ChangeUsernameUxm1() { } // required parameterless constructor

    [Display(Name = "Current password")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.", MinimumLength = 6)]
    public string? PassWord { get; set; }

  }

}
