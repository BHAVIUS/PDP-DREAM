// TkgrControllerSupportingTag.cs 
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
  private const string eidSupportingTagStatus = "span#SupportingTagStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectSupportingTags), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeSelectSupportingTags([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableSupportingTags(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertSupportingTag), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertSupportingTag([DataSourceRequest] DataSourceRequest dsr, SupportingTagEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(nre.SupportingTag))
    {
      var rgx = new Regex(NpdsConst.RegexSupportingTag);
      var isMatch = rgx.IsMatch(nre.SupportingTag);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, $"String is not a valid {nre.ItemXnam}."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditSupportingTag(nre); nre.PdpStatusName = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteSupportingTag), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteSupportingTag([DataSourceRequest] DataSourceRequest dsr, SupportingTagEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteSupportingTag(nre); nre.PdpStatusName = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckSupportingTag), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeCheckSupportingTag([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    SupportingTagEditModel? nre = PSDC.GetEditableSupportingTagByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckSupportingTag(nre); nre.PdpStatusName = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
