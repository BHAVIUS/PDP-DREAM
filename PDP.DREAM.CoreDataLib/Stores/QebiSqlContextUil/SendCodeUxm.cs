// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class SendCodeUxm : FormTaskUxmBase
{
  public string? SelectedProvider { get; set; } = ESS;
  public ICollection<SelectListItem>? Providers { get; set; }
  public string? ReturnUrl { get; set; } = ESS;
  public bool RememberMe { get; set; }

} // end class

// end file