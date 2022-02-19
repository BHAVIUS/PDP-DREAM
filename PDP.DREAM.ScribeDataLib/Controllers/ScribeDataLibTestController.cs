// ScribeTestLibController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.ScribeDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class ScribeDataLibTestController : ScribeDataLibControllerBase
{
  private readonly ILogger<ScribeDataLibTestController>? libLogger = null;
  public ScribeDataLibTestController(ILogger<ScribeDataLibTestController> logger)
  {
    libLogger = logger;
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

  // Index/Examples first, then rest alphabetical

  [HttpGet, Authorize(Roles = PdpConst.NpdsAdmin)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAdmin() { return View(PRC); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAnon() { return View(PRC); }

  [HttpGet, Authorize]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidAuth() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsUser)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult SidUser() { return View(PRC); }

} // class
