// TkgPageControllerOtherText.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageControllerBase
{
  private const string eidOtherTextStatus = "span#OtherTextStatus";

  public virtual JsonResult OnPostReadOtherTexts([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableOtherTexts(recordGuid, isLimited).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteOtherText([DataSourceRequest] DataSourceRequest dsRequest,
    OtherTextEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.EditOtherText(fgr); fgr.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteOtherText([DataSourceRequest] DataSourceRequest dsRequest,
    OtherTextEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteOtherText(fgr); fgr.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostCheckOtherText([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    OtherTextEditModel? fgr = PSDC.GetEditableOtherTextByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckOtherText(fgr); fgr.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReseqOtherText([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    OtherTextEditModel? fgr = PSDC.GetEditableOtherTextByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.ReseqOtherText(fgr); fgr.PdpStatusElement = eidOtherTextStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file