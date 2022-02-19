// ScribeWebLibTestController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

[Area(PdpConst.PdpMvcArea), RequireHttps, AllowAnonymous]
public class ScribeWebLibTestController : TkgrControllerBase
{
  public ScribeWebLibTestController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.User,
      ClientInUserModeIsRequired = true,
      SessionValueIsRequired = true
    };
    ResetScribeRepository();
    var anonActionList = new string[] { nameof(Index), nameof(Examples), nameof(AnonWeb) };
    string actionName = oaeCntxt.ActionName();
    if (!anonActionList.Contains(actionName))
    {
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
    }
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(ScribeWebLibTestController)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    pdpRestCntxt.TkgrController = "ScribeWebLibTest";
    pdpRestCntxt.TkgrViewRole = "User";
    ViewData["PRC"] = pdpRestCntxt;
  }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAllAuthRoles)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

  // Index/Examples first, then rest alphabetical

  [HttpGet, Authorize(Roles = PdpConst.NpdsAdmin)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AdminWeb() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAgent)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AgentWeb() { return View(PRC); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AnonWeb() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsAuthor)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AuthorWeb() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsEditor)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult EditorWeb() { return View(PRC); }

  [HttpGet, Authorize(Roles = PdpConst.NpdsUser)]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult UserWeb() { return View(PRC); }

} // end class

// end file