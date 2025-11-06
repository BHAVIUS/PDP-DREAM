// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

// Nexus Telerik Kendo Grid (TKG) PageController
public partial class NexusTkgPageController : NexusDataRazorPageControllerBase
{
  private const string rzrClass = nameof(NexusTkgPageController);
  public NexusTkgPageController() : base() { }

  // ATTN: using the OnGet handler in this controller instead
  //  of derived controllers yields multiple matches on route handler

} // end class

// end file