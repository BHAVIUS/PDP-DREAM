// ScribeWebLibController.cs 
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
public class ScribeWebLibController : TkgrControllerBase
{
  public ScribeWebLibController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

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
    var anonActionList = new string[] { nameof(Index), nameof(Help) };
    string actionName = oaeCntxt.ActionName();
    if (!anonActionList.Contains(actionName))
    {
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
    }
    base.OnActionExecuting(oaeCntxt);
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(ScribeWebLibController)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    pdpRestCntxt.TkgrController = "ScribeWebLib";
    pdpRestCntxt.TkgrViewRole = "User";
    ViewData["PRC"] = pdpRestCntxt;
  }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  // Index/Help first then rest alphabetical

} // class

// file