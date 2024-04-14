// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// Telerik Kendo Grid Core (TKGC) PageController
public partial class TkgcPageController 
{
  public virtual IActionResult CoreExportNpdsQuads(NpdsQuadUxm editObj) 
  {
    return Page();
  }

  public virtual IActionResult CoreImportNpdsQuads(NpdsQuadUxm editObj, IFormFileCollection bcrFormFiles)
  {
    return Page();
  }


} // end class

// end file