// CoreDataLibController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class CoreDataLibController : CoreDataRestApiControllerBase
{
  private readonly ILogger<CoreDataLibController>? libLogger = null;

  public CoreDataLibController(ILogger<CoreDataLibController> logger)
  {
    libLogger = logger;
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  // Index/Help first, then rest alphabetical

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Diristries() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Entities() { return View(); }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public JsonResult SelectEntityTypes([DataSourceRequest] DataSourceRequest request)
  {
    ResetCoreRepository(); // use PCDC
    DataSourceResult result = PCDC.ListViewableEntityTypes().ToDataSourceResult(request);
    return Json(result);
  }

} // class
