// ChangePasswordUxm1.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models
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
