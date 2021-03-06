// NexusTestApiController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusRestLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class NexusRestLibTestController : NexusDataLibControllerBase
{
  public NexusRestLibTestController(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Nexus,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
    ResetNexusRepository();
  }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

} // end class

// end file