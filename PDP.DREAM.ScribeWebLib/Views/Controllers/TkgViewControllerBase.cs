// TkgViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

// Telerik Kendo Grid Scribe ViewControllerBase
public abstract partial class TkgsViewControllerBase : ScribeDataRazorViewControllerBase
{

  // TKGR controller extends ScribeDataRestApi controller with additional methods in partial class files
  public TkgsViewControllerBase() { }
  public TkgsViewControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgsViewControllerBase(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgsViewControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  [HttpGet, PdpRazorViewRoute(TSststet)]
  // [PdpMvcRoute(nameof(ScribeNpdsRead), "", PdpRatsStstet, SrlRanpView)]
  // [PdpMvcRoute(SrlRanpView, nameof(TkgrControllerBase), nameof(ScribeNpdsRead), PdpRaoView, PdpMvcArea, PdpRatsStstet)]
  // [PdpRazorViewRoute( TSststet)]
  public virtual IActionResult ScribeNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType, "View");
    return View("TkgrScribeNexusRead"); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
  }

  [HttpGet, PdpRazorViewRoute(TSststet)]
  // [PdpMvcRoute(nameof(ScribeNpdsWrite), "", PdpRatsStstet, SrlRanpView)]
  // [PdpMvcRoute(SrlRanpView, nameof(TkgrControllerBase), nameof(ScribeNpdsWrite), PdpRaoView, PdpMvcArea, PdpRatsStstet)]
  // [PdpRazorViewRoute( TSststet)]
  public virtual IActionResult ScribeNpdsWrite(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType, "Edit");
    BuildScribeDropDownLists();
    return View("TkgrScribeNexusWrite"); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
  }

  [HttpGet, PdpRazorViewRoute]
  // [PdpMvcRoute(nameof(NpdsServerServiceDefaults), "", "", SrlRanpView)]
  // [PdpMvcRoute(SrlRanpView, nameof(TkgrControllerBase), nameof(NpdsServerServiceDefaults), PdpRaoView, PdpMvcArea)]
  // [PdpRazorViewRoute]
  public IActionResult NpdsServerServiceDefaults()
  {
    QURC.ParseNpdsSelectFilterForView("Nexus", "NPDS-Root", "", "Edit", "Service Defaults for");
    BuildScribeDropDownLists();
    return View();
  }

  [HttpGet, PdpRazorViewRoute]
  // [PdpMvcRoute(nameof(NpdsServerServiceRestrictions), "", "", SrlRanpView)]
  // [PdpMvcRoute(SrlRanpView, nameof(TkgrControllerBase), nameof(NpdsServerServiceRestrictions), PdpRaoView, PdpMvcArea)]
  // [PdpRazorViewRoute]
  public IActionResult NpdsServerServiceRestrictions()
  {
    QURC.ParseNpdsSelectFilterForView("Nexus", "NPDS-Root", "", "Edit", "Service Restrictions for");
    BuildScribeDropDownLists();
    return View();
  }

} // end class

// end file