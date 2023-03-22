// TkgViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

// Telerik Kendo Grid Scribe ViewController
public partial class TkgsViewController : ScribeDataRazorViewControllerBase
{

   public TkgsViewController() { }
 
  [HttpGet, PdpRazorViewRoute(depTSststet)]
   public virtual IActionResult ScribeNpdsWrite(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType, "Edit");
    BuildScribeDropDownLists();
    // return View("TkgrScribeNexusWrite"); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
    return View(); // ScribeNpdsWrite
  }

  // [HttpGet, PdpRazorViewRoute]
  //  public IActionResult NpdsServerServiceDefaults()
  // {
  //   QURC.ParseNpdsSelectFilterForView("Nexus", "NPDS-Root", "", "Edit", "Service Defaults for");
  //   BuildScribeDropDownLists();
  //   return View();
  // }
  //
  // [HttpGet, PdpRazorViewRoute]
  //  public IActionResult NpdsServerServiceRestrictions()
  // {
  //   QURC.ParseNpdsSelectFilterForView("Nexus", "NPDS-Root", "", "Edit", "Service Restrictions for");
  //   BuildScribeDropDownLists();
  //   return View();
  // }

} // end class

// end file