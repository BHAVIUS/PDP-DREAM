// NexusDataLibController.cs 
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
public class NexusDataLibController : NexusDataLibControllerBase
{
  private readonly ILogger<NexusDataLibController>? libLogger = null;
  public NexusDataLibController(ILogger<NexusDataLibController> logger)
  {
    libLogger = logger;
  }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  // Index/Help first, then rest alphabetical

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

} // class
