// ScribeAdminController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSADMIN)]
public partial class AdminScribeController : UserScribeController
{
  public AdminScribeController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)  // calls ParseQueryCollection on new()
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.Admin,
      ClientInAdminModeIsRequired = true,
      SessionValueIsRequired = true
    };
    pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt);
    // TODO: build analogous CheckClientUserSession
    // that calls OnlineUserIsAuthenticated 
    if (OnlineUserIsAuthenticated)
    {
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
    }

  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

} // class
