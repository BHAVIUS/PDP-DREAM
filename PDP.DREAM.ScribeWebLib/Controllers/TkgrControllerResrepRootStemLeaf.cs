// TkgrControllerResrepRootStemLeaf.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

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
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectResrepRoots), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public JsonResult ScribeSelectResrepRoots([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    var resreps = PSDC.ListEditableResrepRoots(request, out int numResreps);
    var result = new DataSourceResult() { Data = resreps, Total = numResreps };
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertResrepRoot), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { nre = PSDC.EditResrepRoot(nre); nre.PdpStatusName = eidResrepRootStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteResrepRoot), "", CoreDLC.ratsStstet, ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { nre = PSDC.DeleteResrepRoot(nre); nre.PdpStatusName = eidResrepRootStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckResrepRoot), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public ActionResult<string?> ScribeCheckResrepRoot(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepRootByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckResrepStem), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public ActionResult<string?> ScribeCheckResrepStem(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepStemByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckResrepLeaf), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public ActionResult<string?> ScribeCheckResrepLeaf(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeRefreshResrepStatus), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeRefreshResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre.PdpStatusName = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} refreshed from database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeValidateResrepStatus), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeValidateResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre = PSDC.ValidateUpdateResrepLeaf(nre);
      nre.PdpStatusName = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} validated in database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpPost]
  [PdpMvcRoute(nameof(ScribeArchiveResrepSnapshot), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeArchiveResrepSnapshot([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    PRC.ArchiveFormatReqst = true;
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre = PSDC.ArchiveResrepRecord(nre);
      nre.PdpStatusName = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} archived in database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
