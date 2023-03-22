// TkgPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

// Telerik Kendo Grid Scribe (TKGS) PageController
public partial class TkgsPageController 
{
  public virtual IActionResult ScribeExportNpdsQuads(NpdsQuadEditModel editObj) 
  {
    return Page();
  }

  public virtual IActionResult ScribeImportNpdsQuads(NpdsQuadEditModel editObj, IFormFileCollection bcrFormFiles)
  {
    return Page();
  }


} // end class

// end file