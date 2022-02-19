// ScribeAnonResrepsController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class ScribeAnonResrepsController : TkgrControllerBase
{
  public ScribeAnonResrepsController(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
    ResetScribeRepository();
    // var isVerified = CheckClientAnonSession();
    // if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(ScribeAnonResrepsController)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    pdpRestCntxt.TkgrController = "ScribeAnonResreps";
    pdpRestCntxt.TkgrViewRole = "Anon";
    ViewData["PRC"] = pdpRestCntxt;
  }

  [HttpGet]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  [HttpGet]
  [PdpMvcRoute(ScribeWLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Examples() { return View(); }

} // end class

// end file