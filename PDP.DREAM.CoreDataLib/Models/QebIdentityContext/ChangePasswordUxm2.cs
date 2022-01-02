// ChangePasswordUxm2.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
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
