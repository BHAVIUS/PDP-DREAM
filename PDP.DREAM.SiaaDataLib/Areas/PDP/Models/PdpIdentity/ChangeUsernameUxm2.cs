// ChangeUsernameUxm2.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class ChangeUsernameUxm2 
  {
    public ChangeUsernameUxm2() { } // required parameterless constructor
    public ChangeUsernameUxm2(string password) { PassWord = password; }

    public Guid? UserGuid { get; set; }

    [StringLength(32, ErrorMessage = "String must be <=32 characters.", MinimumLength = 6)]
    public string? PassWord { get; set; }

    [Display(Name = "Security Question")]
    public string? SecurityQuestion { get; set; } 

    [Display(Name = "Security Answer")]
    public string? SecurityAnswer { get; set; }

  }

}
