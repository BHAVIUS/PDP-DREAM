using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidSnapshotStatus = "span#SnapshotStatus";

     [HttpGet, HttpPost]
    public JsonResult SelectScribeSnapshotsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableSnapshots(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost]
    public JsonResult SelectScribeSnapshotsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableSnapshots(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    // ATTN: no updates for archived records, just insert and delete (with either soft delete or hard delete)
    //  noting that a soft delete is an update but only at T-SQL level not the application level
    //     and that a hard delete is also a true delete at T-SQL level

    [HttpPut, HttpPost]
    public JsonResult EditScribeSnapshot([DataSourceRequest] DataSourceRequest dsr, NexusSnapshotEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository();  // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty)) // assure presence of valid recordGuid
      { ModelState.AddModelError(recordName, "Guid is null, not a valid recordGuid."); }
      // TODO: redundant check at controller action level for XML parsing validation
      if (ModelState.IsValid) { nre = PSDC.EditSnapshot(nre); nre.PdpStatusName = eidSnapshotStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost]
    public JsonResult DeleteScribeSnapshot([DataSourceRequest] DataSourceRequest dsr, NexusSnapshotEditModel nre)
    {
      ResetScribeRepository();  // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteSnapshot(nre); nre.PdpStatusName = eidSnapshotStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
