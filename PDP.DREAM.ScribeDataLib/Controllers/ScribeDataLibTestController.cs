// ScribeTestLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

} // class
