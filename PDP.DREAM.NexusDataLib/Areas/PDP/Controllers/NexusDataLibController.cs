using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NexusDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NexusDataLibController : NexusDataControllerBase
  {
    private readonly ILogger<NexusDataLibController>? libLogger = null;

    public NexusDataLibController(ILogger<NexusDataLibController> logger)
    {
      libLogger = logger;
    }

  } // class

} // namespace
