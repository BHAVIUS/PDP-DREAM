// SendCodeUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class SendCodeUxm : IFormTaskUxm
{
  // begin IFormTaskUxm
  public string? FormTitle { get; set; } = string.Empty;
  public string? FormMessage { get; set; } = string.Empty;
  public bool FormCompleted { get; set; }
  public bool ErrorOccurred { get; set; }
  public Exception? Error { get; set; } = null;
  // end IFormTaskUxm

  public string? SelectedProvider { get; set; } = string.Empty;
  public ICollection<SelectListItem>? Providers { get; set; }
  public string? ReturnUrl { get; set; } = string.Empty;
  public bool RememberMe { get; set; }

} // end class

// end file