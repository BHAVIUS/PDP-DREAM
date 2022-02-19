// ScribeRestLibTestController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeRestLib.Controllers;

[Area(PdpConst.PdpMvcArea), RequireHttps, AllowAnonymous]
public class ScribeRestLibTestController : ScribeDataLibControllerBase
{
  public ScribeRestLibTestController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.User,
      ClientInUserModeIsRequired = true,
      SessionValueIsRequired = true
    };
    ResetScribeRepository();
    var anonActionList = new string[] { nameof(Index), nameof(Examples), nameof(AnonApi) };
    string actionName = oaeCntxt.ActionName();
    if (!anonActionList.Contains(actionName))
    {
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
    }
    base.OnActionExecuting(oaeCntxt);
  }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAllAuthRoles)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

  // Index/Examples first, then rest alphabetical

  [HttpGet, Authorize(Roles = PdpConst.NpdsAdmin)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AdminApi() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAgent)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AgentApi() { return View(PRC); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AnonApi() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAuthor)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AuthorApi() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsEditor)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult EditorApi() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsUser)]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult UserApi() { return View(PRC); }

} // end class

// end file