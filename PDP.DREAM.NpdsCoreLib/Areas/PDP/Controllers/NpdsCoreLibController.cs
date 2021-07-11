// NpdsCoreLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NpdsCoreLibController : NpdsCoreControllerBase
  {
    private readonly ILogger<NpdsCoreLibController> _logger;

    public NpdsCoreLibController(ILogger<NpdsCoreLibController> logger)
    {
      _logger = logger;
    }

    public IActionResult Diristries() { return View(); }
    public IActionResult Entities() { return View(); }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectEntityTypesForView([DataSourceRequest] DataSourceRequest request)
    {
      ResetCoreRepository(); // use PCDC
      DataSourceResult result = PCDC.ListViewableEntityTypes().ToDataSourceResult(request);
      return Json(result);
    }

  }

}
