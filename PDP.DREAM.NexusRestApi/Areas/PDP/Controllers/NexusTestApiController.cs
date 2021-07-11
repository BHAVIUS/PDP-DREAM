// NexusTestApiController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NexusTestApiController : NexusDataControllerBase
  {
    public NexusTestApiController(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

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
    public IActionResult PrcTest()
    {
      PRC.SectionTitle = "PDP/NexusTestApi/PrcTest";
      return View();
    }


    [HttpGet]
    public IActionResult MvcRoutes()
    {
      PRC.SectionTitle = "PDP/NexusTestApi/MvcRoutes";
      return View();
    }

  } // class

} // namespace
