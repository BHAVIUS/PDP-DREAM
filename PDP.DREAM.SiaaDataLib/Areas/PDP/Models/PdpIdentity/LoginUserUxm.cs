// LoginUserUxm.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class LoginUserUxm : RestTaskUxm
  {
    [Required, Display(Name = "Username")]
    [DataType(DataType.Text)]
    public string UserName { get; set; } = string.Empty;

    [Required, Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string PassWord { get; set; } = string.Empty;

    [Display(Name = "Remember?")]
    public bool RememberMe { get; set; } = false;

    [Display(Name = "Recover?")]
    public bool RecoverMe { get; set; } = false;

    public DateTime? DateLastLogin { get; set; } = null;

    public string ReturnUrl { get; set; } = string.Empty;

    public string QueryString { get; set; } = string.Empty;

    public bool UserLoginOk { get; set; } = false;

  }

}
