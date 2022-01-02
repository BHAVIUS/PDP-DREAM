// RegisterUserUxm.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.CoreDataLib.Models;

public class RegisterUserUxm : QebiUserUxm, IUserRegister
{
  public bool UserRegistered { get; set; }

  [Display(Name = "What is the first prime number greater than the square of 3?"), Required]
  [Compare("NextPrime", ErrorMessage = "Incorrect answer")]
  public string SimpleCaptcha { get; set; } = string.Empty;

  private string nxtPrime = "11";
  public string NextPrime { get { return nxtPrime; } }

}
