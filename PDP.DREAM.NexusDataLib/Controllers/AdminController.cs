// AdminController.cs 
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

namespace PDP.DREAM.NexusDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NpdsAdmin)]
public partial class AdminNexusController : UserNexusController
{
  public AdminNexusController(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
    : base(userCntxt, emlSndr, smsSndr, lgrFtry) { }

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
    // TODO: build analogous CheckClientUserSession
    // var isVerified = CheckClientAgentSession();
    // if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
  }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

} // end class

// end file