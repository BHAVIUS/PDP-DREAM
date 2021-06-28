using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps]
  public class ScribeTestApiController : SiaaDataControllerBase
  {
    public ScribeTestApiController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Scribe,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.User,
        ClientInUserModeIsRequired = true,
        SessionValueIsRequired = true
      };
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
    }

    [HttpGet]
    public override IActionResult Index() { return View(); }
    [HttpGet]
    public IActionResult RestUser() { return View(PRC); }
    [HttpGet]
    public IActionResult RestAgent() { return View(PRC); }
    [HttpGet]
    public IActionResult RestAuthor() { return View(PRC); }
    [HttpGet]
    public IActionResult RestEditor() { return View(PRC); }
    [HttpGet]
    public IActionResult RestAdmin() { return View(PRC); }
    [HttpGet]
    public IActionResult Routes() { return View(); }
    [HttpGet]
    public IActionResult Examples() { return View(); }

  } // class

} // namespace
