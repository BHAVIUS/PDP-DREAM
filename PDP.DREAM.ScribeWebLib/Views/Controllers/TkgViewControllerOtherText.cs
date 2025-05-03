// TkgViewControllerOtherText.cs 
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

public partial class TkgsViewControllerBase
{
  private const string eidOtherTextStatus = "span#OtherTextStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectOtherTexts), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeSelectOtherTexts([DataSourceRequest] DataSourceRequest request,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableOtherTexts(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertOtherText), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeUpsertOtherText([DataSourceRequest] DataSourceRequest dsr,
    OtherTextEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditOtherText(nre); nre.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteOtherText), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeDeleteOtherText([DataSourceRequest] DataSourceRequest dsr,
    OtherTextEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteOtherText(nre); nre.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckOtherText), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeCheckOtherText([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    OtherTextEditModel? nre = PSDC.GetEditableOtherTextByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckOtherText(nre); nre.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqOtherText), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeReseqOtherText([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    OtherTextEditModel? nre = PSDC.GetEditableOtherTextByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqOtherText(nre); nre.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file