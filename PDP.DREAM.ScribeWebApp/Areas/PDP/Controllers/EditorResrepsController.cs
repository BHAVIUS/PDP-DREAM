using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.ScribeRestApi.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeWebApp.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSEDITOR)]
  public class EditorResrepsController : TkgrControllerBase
  {
    public EditorResrepsController(PdpAgentCmsContext userCntxt, ScribeDbsqlContext dataCntxt) : base(userCntxt, dataCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCtxt)
    {
      pdpRestCntxt = new PdpRestContext(oaeCtxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Nexus,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.Editor
      };
      CheckClientAgentSession();
    }

  }

}
