// ChangeUsernameUxm1.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ChangeUsernameUxm1 
  {
    public ChangeUsernameUxm1() { } // required parameterless constructor

    [Display(Name = "Current password")]
    [StringLength(32, ErrorMessage = "String must be <=32 characters.", MinimumLength = 6)]
    public string? PassWord { get; set; }

  }

}
