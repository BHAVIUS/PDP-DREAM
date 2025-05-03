// TkgPageControllerResrepRootStemLeaf.cs 
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
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceType, string serviceTag, string entityType, string recordAccess)
  {
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    ResetScribeRepository();  // use PSDC
    var resreps = PSDC.ListEditableResrepRoots(dsRequest, out int numResreps);
    var dsResult = new DataSourceResult() { Data = resreps, Total = numResreps };
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteResrepRoot([DataSourceRequest] DataSourceRequest dsRequest,
    NexusResrepEditModel rrr, string serviceType, string serviceTag, string entityType, string recordAccess)
  {
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { rrr = PSDC.EditResrepRoot(rrr); rrr.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteResrepRoot([DataSourceRequest] DataSourceRequest dsRequest,
    NexusResrepEditModel rrr, string serviceType, string serviceTag, string entityType, string recordAccess)
  {
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { rrr = PSDC.DeleteResrepRoot(rrr); rrr.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual ContentResult OnGetCheckResrepRoot(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepRootByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    return htmCntnt;
  }

  public virtual ContentResult OnGetCheckResrepStem(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepStemByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    return htmCntnt;
  }

  public virtual ContentResult OnGetCheckResrepLeaf(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    return htmCntnt;
  }

  public virtual JsonResult OnPostRefreshResrepStatus([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} refreshed from database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostValidateResrepStatus([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ValidateUpdateResrepLeaf(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} validated in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReqRelResrepRecord([DataSourceRequest] DataSourceRequest dsRequest,
  Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.RequestReleaseResrepRecord(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      // rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} validated in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostArchiveResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    QURC.ArchiveFormatReqst = true;
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ArchiveResrepRecord(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} archived in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file