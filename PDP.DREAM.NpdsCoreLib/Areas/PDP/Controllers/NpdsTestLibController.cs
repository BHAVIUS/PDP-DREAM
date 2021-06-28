using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NpdsTestLibController : NpdsCoreControllerBase
  {
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public virtual IActionResult MvcError()
    {
      var evm = new MvcErrorUxm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
      return View(evm);
    }
    public IActionResult PrcTest() { return View(PRC); }
    public IActionResult Routes() { return View(PRC); }

  }

}
