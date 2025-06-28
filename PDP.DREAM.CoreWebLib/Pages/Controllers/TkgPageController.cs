// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// Telerik Kendo Grid Core (TKGC) PageController
public partial class TkgcPageController : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(TkgcPageController);
  public TkgcPageController() : base() { }

  // ATTN: using the OnGet handler in this controller instead
  //  of derived controllers yields multiple matches on route handler

} // end class

// end file