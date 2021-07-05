using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), Authorize]
  public partial class AuthController : UserController
  {
    public AuthController(ScribeDbsqlContext npdsCntxt, QebIdentityContext userCntxt,
      IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
      : base(npdsCntxt, userCntxt, emlSndr, smsSndr, lgrFtry) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpHttpCntxt = oaeCntxt.HttpContext;
      pdpHttpReqst = pdpHttpCntxt.Request;
      pdpRestCntxt = new PdpRestContext(pdpHttpReqst)  // calls ParseQueryCollection on new()
      {
        DatabaseType = NpdsConst.DatabaseType.Scribe,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.User,
        ClientInUserModeIsRequired = true,
        SessionValueIsRequired = true
      };
      pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt);
      // TODO: build analogous CheckClientUserSession
      // var isVerified = CheckClientAgentSession();
      // if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
    }

    [HttpGet]
    public IActionResult Index() { return View(); }

  } // class

} // namespace
