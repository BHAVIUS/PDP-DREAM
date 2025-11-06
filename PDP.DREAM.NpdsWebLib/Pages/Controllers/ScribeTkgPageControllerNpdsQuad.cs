// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

// Telerik Kendo Grid Scribe (TKGS) PageController
public partial class ScribeTkgPageController 
{
  public IActionResult ScribeExportNpdsQuads(NpdsQuadUxm uxm) 
  {
    return Page();
  }

  public IActionResult ScribeImportNpdsQuads(NpdsQuadUxm uxm, IFormFileCollection bcrFormFiles)
  {
    return Page();
  }

} // end class

// end file