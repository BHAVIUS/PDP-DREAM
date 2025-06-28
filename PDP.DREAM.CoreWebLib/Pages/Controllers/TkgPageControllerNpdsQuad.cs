// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// Telerik Kendo Grid Core (TKGC) PageController
public partial class TkgcPageController 
{
  public IActionResult CoreExportNpdsQuads(NpdsQuadUxm uxm) 
  {
    return Page();
  }

  public IActionResult CoreImportNpdsQuads(NpdsQuadUxm uxm, IFormFileCollection bcrFormFiles)
  {
    return Page();
  }


} // end class

// end file