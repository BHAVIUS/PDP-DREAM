// NexusTestLibController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class NexusDataLibTestController : NexusDataLibControllerBase
{
  private readonly ILogger<NexusDataLibTestController>? libLogger = null;
  public NexusDataLibTestController(ILogger<NexusDataLibTestController> logger)
  {
    libLogger = logger;
  }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

  // Index/Examples first, then rest alphabetical

  [HttpGet, Authorize(Roles = PdpConst.NpdsAdmin)]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAdmin() { return View(PRC); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAnon() { return View(PRC); }

  [HttpGet, Authorize]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAuth() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsUser)]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidUser() { return View(PRC); }

} // end class

// end file