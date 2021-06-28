using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class AnonNexusTkgrController : TkgrControllerBase
  {
    public AnonNexusTkgrController(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

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
