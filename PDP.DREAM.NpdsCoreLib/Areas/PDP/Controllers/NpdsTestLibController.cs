// NpdsTestLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NpdsTestLibController : NpdsCoreControllerBase
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
    public virtual IActionResult PrcTest()
    {
      PRC.SectionTitle = "PDP/NpdsTestLib/PrcTest";
      return View();
    }


	[HttpGet]
    public virtual IActionResult MvcRoutes()
    {
      PRC.SectionTitle = "PDP/NpdsTestLib/MvcRoutes";
      return View();
    }

    [HttpGet, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public virtual IActionResult MvcErrors()
    {
      var evm = new MvcErrorUxm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
      return View(evm);
    }

  } // class

} // namespace
