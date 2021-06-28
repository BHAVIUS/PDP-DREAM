using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, AllowAnonymous]
  public class AnonScribeTkgrController : TkgrControllerBase
  {
    public AnonScribeTkgrController(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Nexus,
        DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
        RecordAccess = NpdsConst.RecordAccess.Client,
        ClientInUserModeIsRequired = false,
        SessionValueIsRequired = false
      };

    }

  } // class

} // namespace
