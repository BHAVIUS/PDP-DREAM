// CoreTestLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class CoreDataLibTestController : CoreDataRestApiControllerBase
{
  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Core,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
    ResetCoreRepository();
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

  // Index/Examples first, then rest alphabetical

  [HttpGet, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public virtual IActionResult MvcErrors()
  {
    PRC.SectionTitle = "PDP/CoreTestLib/MvcErrors";
    var evm = new MvcErrorUxm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
    return View(evm);
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public virtual IActionResult MvcRoutes()
  {
    PRC.SectionTitle = "PDP/CoreTestLib/MvcRoutes";
    return View();
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public virtual IActionResult PrcTest()
  {
    PRC.SectionTitle = "PDP/CoreTestLib/PrcTest";
    return View();
  }

} // class
