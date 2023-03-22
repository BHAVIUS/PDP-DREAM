// TkgViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// Telerik Kendo Grid View Controller
public partial class TkgnViewController : NexusDataRazorViewControllerBase
{
  public TkgnViewController() { }

  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute(depTSststet)]
  public virtual IActionResult NexusNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType, "View");
    // return View("TkgrNexusReadResrepsTkgp"); // TkgrNexusCore, TkgrNexusPortal, TkgrNexusDoors, TkgrNexusNexus
    return View(); // NexusNpdsRead
  }

} // end class

// end file