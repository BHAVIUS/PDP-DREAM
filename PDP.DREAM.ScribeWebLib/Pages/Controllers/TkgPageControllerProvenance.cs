// TkgPageControllerProvenance.cs 
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
  private const string eidProvenanceStatus = "span#ProvenanceStatus";

  public virtual JsonResult OnPostReadProvenances([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableProvenances(recordGuid, isLimited).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteProvenance([DataSourceRequest] DataSourceRequest dsRequest,
    ProvenanceEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.EditProvenance(fgr); fgr.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteProvenance([DataSourceRequest] DataSourceRequest dsRequest,
    ProvenanceEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteProvenance(fgr); fgr.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostCheckProvenance([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    ProvenanceEditModel? fgr = PSDC.GetEditableProvenanceByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckProvenance(fgr); fgr.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReseqProvenance([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    ProvenanceEditModel? fgr = PSDC.GetEditableProvenanceByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.ReseqProvenance(fgr); fgr.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file