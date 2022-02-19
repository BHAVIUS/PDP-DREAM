// ScribeAdminResrepsController.cs 
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

// ATTN: must avoid ambiguous match exceptions, requires use of distinct routing

[Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NpdsAdmin)]
public class ScribeAdminResrepsController : TkgrControllerBase
{
  public ScribeAdminResrepsController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.Admin,
      ClientInAdminModeIsRequired = true,
      SessionValueIsRequired = true
    };
    ResetScribeRepository();
    var isVerified = CheckClientAgentSession();
    if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(ScribeAdminResrepsController)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    pdpRestCntxt.TkgrController = "ScribeAdminResreps";
    pdpRestCntxt.TkgrViewRole = "Admin";
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

  [HttpGet]
  [PdpMvcRoute(nameof(NpdsServiceDefaults), "", "", ScribeWLC.ranpView)]
  public IActionResult NpdsServiceDefaults()
  {
    PRC.ParseNpdsServTagEntity("Nexus", "NPDS-Root", "", "Edit");
    BuildDropDownListsForResrepRoot();
    return View();
  }

  [HttpGet]
  [PdpMvcRoute(nameof(NpdsServiceRestrictions), "", "", ScribeWLC.ranpView)]
  public IActionResult NpdsServiceRestrictions()
  {
    PRC.ParseNpdsServTagEntity("Nexus", "NPDS-Root", "", "Edit");
    BuildDropDownListsForResrepRoot();
    return View();
  }

  [HttpGet]
  [PdpMvcRoute(nameof(ScribeCheckLocations), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public IActionResult ScribeCheckLocations(string serviceType, string serviceTag, string entityType = "")
  {
    ArgumentChecker.CatchNullOrWhite(serviceType); ArgumentChecker.CatchNullOrWhite(serviceTag);
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "Edit");
    foreach (var rrr in PSDC.ListEditableResrepStems())
    {
      var recordGuid = PdpGuid.ParseToNonNullable(rrr.RRRecordGuid, Guid.Empty);
      if (!PdpGuid.IsInvalidGuid(recordGuid))
      {
        foreach (var loc in PSDC.ListEditableLocations(recordGuid))
        {
          if (!string.IsNullOrEmpty(loc.CityLocality)) { PSDC.CheckLocation(loc); }
        }
      }
    }
    return View(PRC.ViewName);
  }

} // end class

// end file