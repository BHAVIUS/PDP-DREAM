// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public const string eidResrepSnapshotStatus = TkndoElemPrfx + "ResrepSnapshotStatus";

  public JsonResult OnPostReadResrepSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadResrepSnapshots);
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenScribeConnection(); // use PSDC
#if DEBUG
    NPDSCW.DebugScribeRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("ResrepSnapshots", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableResrepSnapshots(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // ATTN: no updates for archived records, just insert and delete (with either soft delete or hard delete)
  // ATTN: a soft delete is an update but only at T-SQL level not the application level
  // ATTN: a hard delete is also a true delete at T-SQL level both application and database levels

  public JsonResult OnPostWriteResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepSnapshotUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditResrepSnapshot(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidResrepSnapshotStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostCheckResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    ResrepSnapshotUxm? fgr = NPDSCW.PSDC.GetEditableResrepSnapshotByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckResrepSnapshot(fgr); }
    fgr.NdisElemId = eidResrepSnapshotStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostReseqResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    ResrepSnapshotUxm? fgr = NPDSCW.PSDC.GetEditableResrepSnapshotByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.ReseqResrepSnapshot(fgr); }
    fgr.NdisElemId = eidResrepSnapshotStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostDeleteResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepSnapshotUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteResrepSnapshot(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidResrepSnapshotStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file
