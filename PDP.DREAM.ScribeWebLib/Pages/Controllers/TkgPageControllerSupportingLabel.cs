// TkgPageControllerSupportingLabel.cs 
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
  private const string eidSupportingLabelStatus = "span#SupportingLabelStatus";

  public virtual JsonResult OnPostReadSupportingLabels([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableSupportingLabels(recordGuid, isLimited).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteSupportingLabel([DataSourceRequest] DataSourceRequest dsRequest,
   SupportingLabelEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(fgr.SupportingLabel))
    {
      var rgx = new Regex(PdpAppConst.RegexSupportingLabel);
      var isMatch = rgx.IsMatch(fgr.SupportingLabel);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, $"String is not a valid {fgr.ItemXnam}."); }
    }
    if (ModelState.IsValid) { fgr = PSDC.EditSupportingLabel(fgr); fgr.PdpStatusElement = eidSupportingLabelStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteSupportingLabel([DataSourceRequest] DataSourceRequest dsRequest,
   SupportingLabelEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteSupportingLabel(fgr); fgr.PdpStatusElement = eidSupportingLabelStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostCheckSupportingLabel([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    SupportingLabelEditModel? fgr = PSDC.GetEditableSupportingLabelByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckSupportingLabel(fgr); fgr.PdpStatusElement = eidSupportingLabelStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReseqSupportingLabel([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    SupportingLabelEditModel? fgr = PSDC.GetEditableSupportingLabelByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.ReseqSupportingLabel(fgr); fgr.PdpStatusElement = eidSupportingLabelStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file