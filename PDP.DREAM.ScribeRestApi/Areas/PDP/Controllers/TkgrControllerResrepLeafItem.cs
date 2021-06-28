using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public ActionResult<string> SummarizeScribeResrepLeaf(Guid recordGuid)
    {
      ResetScribeRepository();  // use PSDC
      NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(recordGuid);
      var result = string.Empty;
      if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
      return result;
    }

    [HttpPost] // Post for Rest, Post for Ajax
    public JsonResult EditScribeResrepLeaf([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      if (ModelState.IsValid) { nre = PSDC.EditResrepLeaf(nre); nre.PdpStatusName = eidResrepLeafStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeResrepLeaf([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteResrepLeaf(nre); nre.PdpStatusName = eidResrepLeafStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult RefreshScribeResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository();  // use PSDC
      NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(id);
      if (nre?.RRRecordGuid == id)
      {
        nre.PdpStatusName = eidResrepLeafStatus;
        nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} refreshed from database";
      }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult ValidateScribeResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository();  // use PSDC
      NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(id);
      if (nre?.RRRecordGuid == id)
      {
        nre = PSDC.ValidateUpdateResrepLeaf(nre);
        nre.PdpStatusName = eidResrepLeafStatus;
        nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} validated in database";
      }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult ArchiveScribeResrepSnapshot([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      PRC.ArchiveFormatReqst = true;
      ResetScribeRepository();  // use PSDC
      NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(id);
      if (nre?.RRRecordGuid == id)
      {
        nre = PSDC.ArchiveResrepRecord(nre);
        nre.PdpStatusName = eidResrepLeafStatus;
        nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} archived in database";
      }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  } // class

} // namespace
