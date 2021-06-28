using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class SiaaDataLibController : SiaaDataControllerBase
  {
    private readonly ILogger<SiaaDataLibController> _logger;

    public SiaaDataLibController(ILogger<SiaaDataLibController> logger)
    {
      _logger = logger;
    }

  } // class

} // namespace
