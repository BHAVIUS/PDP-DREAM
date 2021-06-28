using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class ChangePasswordUxm1 
  {
    public ChangePasswordUxm1() { } // required parameterless constructor

    [Display(Name = "Current username")]
    [DataType(DataType.Text)]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
    public string? UserName { get; set; }

  }

}
