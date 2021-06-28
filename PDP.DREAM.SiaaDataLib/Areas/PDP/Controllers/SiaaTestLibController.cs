using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps]
  public class SiaaTestLibController : SiaaDataControllerBase
  {
    public SiaaTestLibController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

    [HttpGet]
    public override IActionResult Index() { return View(); }
    [HttpGet]
    public IActionResult Anon() { return View(PRC); }
    [HttpGet]
    public IActionResult Auth() { return View(PRC); }
    [HttpGet]
    public IActionResult Agent() { return View(PRC); }
    [HttpGet]
    public IActionResult Routes() { return View(); }

  } // class

} // namespace
