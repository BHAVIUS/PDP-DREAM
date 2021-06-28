using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Utilities;

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
        SessionValueIsRequired = false,
        DatabaseType = NpdsConst.DatabaseType.Nexus,
      };
      ResetNexusRepository();
    }

    public override IActionResult Index() { return View(PRC); }
    public IActionResult Examples() { return View(); }
    public IActionResult Routes() { return View(); }

  }

}
