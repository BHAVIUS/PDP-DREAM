// ChangePasswordUxm2.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

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
