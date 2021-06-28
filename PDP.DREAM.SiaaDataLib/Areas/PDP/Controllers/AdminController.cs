using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSADMIN)]
  public partial class AdminController : SiaaDataControllerBase
  {
    public AdminController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpHttpCntxt = oaeCntxt.HttpContext;
      pdpHttpReqst = pdpHttpCntxt.Request;
      pdpRestCntxt = new PdpRestContext(pdpHttpReqst)  // calls ParseQueryCollection on new()
      {
        DatabaseType = NpdsConst.DatabaseType.Scribe,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.Admin,
        ClientInAdminModeIsRequired = true,
        SessionValueIsRequired = true
      };
      pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt);
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
    }

    [HttpGet]
    public IActionResult Index() { return View(); }


  }

}
