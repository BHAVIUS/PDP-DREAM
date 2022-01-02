// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public abstract partial class TkgrControllerBase : ScribeDataLibControllerBase
{
  public TkgrControllerBase() { }
  public TkgrControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgrControllerBase(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgrControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  // NP is NamePrefix, NS is NameSuffix, TS is TemplateSuffix
  public const string NPmvc  = "ScribeWebLib";
  public const string NSget = "Get"; // may include post for Get/Post
  public const string NSput = "Put"; // may include post for Put/Post
  public const string NSdel = "Del"; // may include post for Delete/Post
  public const string NSpost = "Post"; // for post only
  public const string TSststet = "{serviceType}/{serviceTag}/{entityType?}";
  public const string TSrg = "{recordGuid}";
  public const string TSrgil = "{recordGuid}/{isLimited?}";

  [HttpGet]
  [PdpMvcRoute(nameof(ScribeNpdsRead), "", TSststet, NPmvc)]
  public virtual IActionResult ScribeNpdsRead(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
    // return View(PRC.ViewName); // CoreRead, PortalRead, DoorsRead, NexusRead
    return View("TkgrScribeNexusRead"); // CoreRead, PortalRead, DoorsRead, NexusRead
  }

  [HttpGet]
  [PdpMvcRoute(nameof(ScribeNpdsWrite), "", TSststet, NPmvc)]
  public virtual IActionResult ScribeNpdsWrite(string serviceType, string serviceTag, string entityType = "")
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "Edit");
    BuildDropDownListsForResrepRoot();
    // return View(PRC.ViewName); // CoreWrite, PortalWrite, DoorsWrite, NexusWrite
    return View("TkgrScribeNexusWrite"); // CoreWrite, PortalWrite, DoorsWrite, NexusWrite
  }

  protected Guid? ParseResRepRecordGuid(string recordName, Guid? modelGuid, Guid defaultGuid)
  {
    var parsedGuid = PdpGuid.ParseToNonNullable(modelGuid, defaultGuid);
    if (PdpGuid.IsNullOrEmpty(parsedGuid))
    { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
    return parsedGuid;
  }

} // class
