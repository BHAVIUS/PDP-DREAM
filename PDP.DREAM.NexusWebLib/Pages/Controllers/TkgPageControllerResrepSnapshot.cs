// Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  private const string eidResrepSnapshotStatus = TkndoElemPrfx + "ResrepSnapshotStatus";

  public JsonResult OnPostReadResrepSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadResrepSnapshots);
    NPDSCW.OpenNexusConnection(); // use PNDC
#if DEBUG
    NPDSCW.DebugNexusRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("ResrepSnapshots", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PNDC.ListEditableResrepSnapshots(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseNexusConnection();
    return jsonData;
  }

} // end class

// end file