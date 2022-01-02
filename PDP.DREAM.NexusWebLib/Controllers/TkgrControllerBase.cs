// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusWebLib.Controllers;

public abstract partial class TkgrControllerBase : NexusDataLibControllerBase
{
  public TkgrControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

  // NP is NamePrefix, TS is TemplateSuffix
  protected const string NPtkgr = "PdpTkgr";
  protected const string TSststet = "{serviceType}/{serviceTag}/{entityType?}";
  protected const string TSrg = "{recordGuid}";
  protected const string TSrgil = "{recordGuid}/{isLimited?}";

  [HttpGet]
  [PdpMvcRoute(nameof(NexusNpdsRead), "", TSststet, NPtkgr)]
  public virtual IActionResult NexusNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
    return View(PRC.ViewName); // CoreRead, PortalRead, DoorsRead, NexusRead
  }

} // class
