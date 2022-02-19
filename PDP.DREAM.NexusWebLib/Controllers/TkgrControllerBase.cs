// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusWebLib.Controllers;

public abstract partial class TkgrControllerBase : NexusDataLibControllerBase
{
  public TkgrControllerBase() { }
  public TkgrControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgrControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgrControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  // NP is NamePrefix, NS is NameSuffix, TS is TemplateSuffix
  public const string NPvsp = "NexusWebLib"; // NP for visual studio project

  [HttpGet]
  [PdpMvcRoute(nameof(NexusNpdsRead), "", CoreDLC.ratsStstet, NPvsp)]
  public virtual IActionResult NexusNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
    // return View(PRC.ViewName); // CoreRead, PortalRead, DoorsRead, NexusRead
    return View("TkgrNexusNexus"); // TkgrNexusCore, TkgrNexusPortal, TkgrNexusDoors, TkgrNexusNexus
  }


  protected Guid? ParseResRepRecordGuid(string recordName, Guid? modelGuid, Guid defaultGuid)
  {
    var parsedGuid = PdpGuid.ParseToNonNullable(modelGuid, defaultGuid);
    if (PdpGuid.IsNullOrEmpty(parsedGuid))
    { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
    return parsedGuid;
  }

}
