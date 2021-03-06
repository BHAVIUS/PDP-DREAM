// AuthController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), Authorize]
public partial class AuthScribeController : UserScribeController
{
  public AuthScribeController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // calls ParseQueryCollection on new()
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.User,
      ClientInUserModeIsRequired = true,
      SessionValueIsRequired = true
    };
    pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt);
    // TODO: build analogous CheckClientUserSession
    var isVerified = CheckClientAgentSession();
    if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

} // class
