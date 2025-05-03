// TkgViewControllerProvenance.cs 
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
  private const string eidProvenanceStatus = "span#ProvenanceStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectProvenances), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeSelectProvenances([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableProvenances(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertProvenance), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeUpsertProvenance([DataSourceRequest] DataSourceRequest dsr, ProvenanceEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditProvenance(nre); nre.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteProvenance), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeDeleteProvenance([DataSourceRequest] DataSourceRequest dsr, ProvenanceEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteProvenance(nre); nre.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckProvenance), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeCheckProvenance([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    ProvenanceEditModel? nre = PSDC.GetEditableProvenanceByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckProvenance(nre); nre.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqProvenance), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult ScribeReseqProvenance([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    ProvenanceEditModel? nre = PSDC.GetEditableProvenanceByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqProvenance(nre); nre.PdpStatusElement = eidProvenanceStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file