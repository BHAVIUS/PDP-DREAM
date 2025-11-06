// Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class AtlasTkgPageController
{
  public const string eidRecordSnapshotStatus = TkndoElemPrfx + "RecordSnapshotStatus";

  public JsonResult OnPostReadRecordSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadRecordSnapshots);
    NPDSCW.OpenAtlasConnection(); // use PNDC
#if DEBUG
    NPDSCW.DebugAtlasRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RecordSnapshots", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.AtlasDR.ListEditableRecordSnapshots(recordGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseAtlasConnection();
    return jsonData;
  }

} // end class

// end file