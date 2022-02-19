// TkgrControllerSupportingLabel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text.RegularExpressions;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidSupportingLabelStatus = "span#SupportingLabelStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectSupportingLabels), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeSelectSupportingLabels([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableSupportingLabels(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertSupportingLabel), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertSupportingLabel([DataSourceRequest] DataSourceRequest dsr, SupportingLabelEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(nre.SupportingLabel))
    {
      var rgx = new Regex(NpdsConst.RegexSupportingLabel);
      var isMatch = rgx.IsMatch(nre.SupportingLabel);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, $"String is not a valid {nre.ItemXnam}."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteSupportingLabel), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteSupportingLabel([DataSourceRequest] DataSourceRequest dsr, SupportingLabelEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckSupportingLabel), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeCheckSupportingLabel([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    SupportingLabelEditModel? nre = PSDC.GetEditableSupportingLabelByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
