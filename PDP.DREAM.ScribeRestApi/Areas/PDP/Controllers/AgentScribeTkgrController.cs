using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSAGENT)]
  public class AgentScribeTkgrController : TkgrControllerBase
  {
    public AgentScribeTkgrController(PdpAgentCmsContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Scribe,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.Agent,
        ClientInAgentModeIsRequired = true,
        SessionValueIsRequired = true
      };
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
    }

  }

}
