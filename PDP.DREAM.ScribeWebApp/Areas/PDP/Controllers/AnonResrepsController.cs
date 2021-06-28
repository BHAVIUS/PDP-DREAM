using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;
using PDP.DREAM.ScribeRestApi.Controllers;

namespace PDP.DREAM.ScribeWebApp.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, AllowAnonymous]
  public class AnonResrepsController : TkgrControllerBase
  {
    public AnonResrepsController(PdpAgentCmsContext userCntxt, ScribeDbsqlContext dataCntxt) : base(userCntxt, dataCntxt) { }

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
