using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), Authorize]
  public partial class AgentController : SiaaDataControllerBase
  {
    public AgentController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }
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
      // that calls OnlineUserIsAuthenticated 
      if (OnlineUserIsAuthenticated)
      {
        // var isVerified = CheckClientAgentSession();
        // if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
      }

    }

    [HttpGet]
    public IActionResult Index() { return View(); }

    [HttpGet]
    public IActionResult Help() { return View(); }


    [HttpGet, Authorize(Roles = PdpConst.NPDSAGENT)]
    public IActionResult CheckSession()
    {
      bool agentSessionCreated = false, agentSessionIdentified = false;
      var prc = PRC;
      agentSessionIdentified = PSDC.CheckPdpAgentSession(ref pdpRestCntxt);
      if (!agentSessionIdentified)
      {
        agentSessionCreated = PSDC.EditPdpAgentSession(ref pdpRestCntxt);
        if (agentSessionCreated)
        {
          agentSessionIdentified = PSDC.CheckPdpAgentSession(ref pdpRestCntxt);
        }
      }
      // use both view model approaches for CheckSession View
      return View(pdpRestCntxt);
    }

    private void AddRoleUser(Guid roleGuid)
    {
      var appGuid = PdpSiteSettings.GetValues.AppSecureUiaaGuid;
      var errorCode = QUC.QebIdentityAppLinkEdit(Guid.NewGuid(), QebUserGuid, roleGuid, appGuid);
    }

  } // class

} // namespace
