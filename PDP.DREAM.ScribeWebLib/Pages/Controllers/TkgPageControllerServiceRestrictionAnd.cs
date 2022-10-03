// TkgPageControllerServiceRestrictionAnd.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageControllerBase
{
  private const string eidRestrictionAndStatus = "span#ServiceRestrictionAndStatus";

  public virtual JsonResult OnPostReadServiceRestrictionAnds([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionAnd", "RRRecordGuid invalid."); }
    DataSourceResult dsResult = PSDC.ListEditableRestrictionAndsByRGuid(recordGuid).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteServiceRestrictionAnd([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionAndEditModel fgr)
  {
    ResetScribeRepository(); // use PSDC
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditRestrictionAnd(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceRestrictionAnd([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionAndEditModel fgr)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = fgr.ItemXnam;
    if (ModelState.IsValid) { fgr = PSDC.DeleteRestrictionAnd(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file