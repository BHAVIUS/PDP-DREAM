// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public abstract partial class TkgrControllerBase : ScribeDataLibControllerBase
{
  public TkgrControllerBase() { }
  public TkgrControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgrControllerBase(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgrControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }


  [HttpGet]
  [PdpMvcRoute(nameof(ScribeNpdsRead), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public virtual IActionResult ScribeNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
    // return View(PRC.ViewName); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
    return View("TkgrScribeNexusRead"); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
  }

  [HttpGet]
  [PdpMvcRoute(nameof(ScribeNpdsWrite), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public virtual IActionResult ScribeNpdsWrite(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "Edit");
    BuildDropDownListsForResrepRoot();
    // return View(PRC.ViewName); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
    return View("TkgrScribeNexusWrite"); // TkgrScribeCore, TkgrScribePortal, TkgrScribeDoors, TkgrScribeNexus
  }

  protected Guid? ParseResRepRecordGuid(string recordName, Guid? modelGuid, Guid defaultGuid)
  {
    var parsedGuid = PdpGuid.ParseToNonNullable(modelGuid, defaultGuid);
    if (PdpGuid.IsNullOrEmpty(parsedGuid))
    { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
    return parsedGuid;
  }

} // class
