// LoginUserUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public class LoginUserUxm : FormTaskUxmBase
  {
    [Required, Display(Name = "Username")]
    [DataType(DataType.Text)] // non-nullable
    public string UserName { get; set; } = ESS;

    [Required, Display(Name = "Password")]
    [DataType(DataType.Password)] // non-nullable
    public string PassWord { get; set; } = ESS;

    [Display(Name = "Remember?")]
    public bool RememberMe { get; set; } = false;

    [Display(Name = "Recover?")]
    public bool RecoverMe { get; set; } = false;

    public DateTime? DateLastLogin { get; set; } = null;

    public string? ReturnUrl { get; set; } = PDPSS.AppSiteDefPath;

    public string? QueryString { get; set; } = ESS;

    public bool UserLoginOk { get; set; } = false;

  }

}
