using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NexusRestApi.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NexusWebApp.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class AnonResrepsController : TkgrControllerBase
  {
    public AnonResrepsController(NexusDbsqlContext dataCntxt) : base(dataCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Nexus,
        DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
        RecordAccess = NpdsConst.RecordAccess.Client
      };

    }

  }

}
