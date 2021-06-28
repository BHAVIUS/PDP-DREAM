using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.ScribeDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class ScribeDataLibController : ScribeDataControllerBase
  {
    private readonly ILogger<ScribeDataLibController>? libLogger = null;

    public ScribeDataLibController(ILogger<ScribeDataLibController> logger)
    {
      libLogger = logger;
    }

  } // class

} // namespace