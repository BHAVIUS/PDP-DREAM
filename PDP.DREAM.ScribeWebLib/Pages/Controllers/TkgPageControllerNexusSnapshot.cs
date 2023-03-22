// TkgPageControllerNexusSnapshot.cs 
// Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidSnapshotStatus = "span#SnapshotStatus";

  public virtual JsonResult OnPostReadNexusSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadNexusSnapshots);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("NexusSnapshots", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PSDC.ListEditableSnapshots(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest);
      }
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // ATTN: no updates for archived records, just insert and delete (with either soft delete or hard delete)
  //  noting that a soft delete is an update but only at T-SQL level not the application level
  //     and that a hard delete is also a true delete at T-SQL level

  public virtual JsonResult OnPostWriteSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
   NexusSnapshotEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.EditSnapshot(fgr); fgr.PdpStatusElement = eidSnapshotStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
   NexusSnapshotEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteSnapshot(fgr); fgr.PdpStatusElement = eidSnapshotStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    NexusSnapshotEditModel? fgr = PSDC.GetEditableSnapshotByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckSnapshot(fgr); fgr.PdpStatusElement = eidSnapshotStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file