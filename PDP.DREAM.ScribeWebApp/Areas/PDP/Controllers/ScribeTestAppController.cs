// ScribeTestAppController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.ScribeRestApi.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeWebApp.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSAUTHOR)]
  public class ScribeTestAppController : AuthorScribeTkgrController
  {
    public ScribeTestAppController(ScribeDbsqlContext npdsCntxt, PdpAgentCmsContext userCntxt) : base(npdsCntxt, userCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Scribe,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.Author,
        ClientInAuthorModeIsRequired = true,
        SessionValueIsRequired = true
      };
      ResetScribeRepository();
      CheckClientAgentSession();
    }

    [HttpGet]
    public IActionResult PrcTest()
    {
      PRC.SectionTitle = "PDP/ScribeTestApp/PrcTest";
      return View();
    }


    [HttpGet]
    public IActionResult MvcRoutes()
    {
      PRC.SectionTitle = "PDP/ScribeTestApp/MvcRoutes";
      return View();
    }

  } // class

} // namespace
