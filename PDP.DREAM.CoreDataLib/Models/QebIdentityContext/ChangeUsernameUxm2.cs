// ChangeUsernameUxm2.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
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
