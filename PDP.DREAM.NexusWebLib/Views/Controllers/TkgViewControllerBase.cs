// TkgViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.NexusWebLib.Controllers;

// Telerik Kendo Grid View ControllerBase
public abstract partial class TkgnViewControllerBase : NexusDataRazorViewControllerBase
{
  public TkgnViewControllerBase() { }
  public TkgnViewControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgnViewControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgnViewControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute(TSststet)]
  public virtual IActionResult NexusNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType, "View");
    return View("TkgrNexusNexusRead"); // TkgrNexusCore, TkgrNexusPortal, TkgrNexusDoors, TkgrNexusNexus
  }

} // end class

// end file