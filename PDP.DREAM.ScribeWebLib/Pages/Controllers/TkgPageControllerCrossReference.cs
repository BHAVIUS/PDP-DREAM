// TkgPageControllerCrossReference.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text.RegularExpressions;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageControllerBase
{
  private const string eidCrossReferenceStatus = "span#CrossReferenceStatus";

  public virtual JsonResult OnPostReadCrossReferences([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableCrossReferences(recordGuid, isLimited).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    CrossReferenceEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(fgr.CrossReference))
    {
      var rgx = new Regex(PdpAppConst.RegexCrossReference);
      var isMatch = rgx.IsMatch(fgr.CrossReference);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, $"String is not a valid {fgr.ItemXnam}."); }
    }
    if (ModelState.IsValid) { fgr = PSDC.EditCrossReference(fgr); fgr.PdpStatusElement = eidCrossReferenceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    CrossReferenceEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteCrossReference(fgr); fgr.PdpStatusElement = eidCrossReferenceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostCheckCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    CrossReferenceEditModel? fgr = PSDC.GetEditableCrossReferenceByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckCrossReference(fgr); fgr.PdpStatusElement = eidCrossReferenceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReseqCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    CrossReferenceEditModel? fgr = PSDC.GetEditableCrossReferenceByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.ReseqCrossReference(fgr); fgr.PdpStatusElement = eidCrossReferenceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file