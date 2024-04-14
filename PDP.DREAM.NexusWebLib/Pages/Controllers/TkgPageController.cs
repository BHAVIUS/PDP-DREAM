// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// Telerik Kendo Grid Nexus (TKGN) PageController
public partial class TkgnPageController : NexusDataRazorPageControllerBase
{
  private const string rzrClass = nameof(TkgnPageController);
  public TkgnPageController() : base() { }

  // ATTN: using the OnGet handler in this controller instead
  //  of derived controllers yields multiple matches on route handler

} // end class

// end file