// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

// Telerik Kendo Grid Scribe (TKGS) PageController
public partial class TkgsPageController 
{
  public virtual IActionResult ScribeExportNpdsQuads(NpdsQuadUxm editObj) 
  {
    return Page();
  }

  public virtual IActionResult ScribeImportNpdsQuads(NpdsQuadUxm editObj, IFormFileCollection bcrFormFiles)
  {
    return Page();
  }


} // end class

// end file