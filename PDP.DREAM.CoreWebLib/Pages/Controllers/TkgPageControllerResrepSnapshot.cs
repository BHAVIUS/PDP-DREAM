// Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{
  public const string eidResrepSnapshotStatus = TkndoElemPrfx + "ResrepSnapshotStatus";

  public JsonResult OnPostReadResrepSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadResrepSnapshots);
    OpenCoreConnection(); // use PNDC
#if DEBUG
    DebugCoreRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("ResrepSnapshots", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PCDC.ListEditableResrepSnapshots(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
      }
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseCoreConnection();
    return jsonData;
  }

} // end class

// end file