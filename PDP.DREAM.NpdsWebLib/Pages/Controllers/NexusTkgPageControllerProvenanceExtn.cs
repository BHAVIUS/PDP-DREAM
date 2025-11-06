// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class NexusTkgPageController
{
  private const string eidProvenanceExtnStatus = TkndoElemPrfx + "ProvenanceExtnStatus";

  public JsonResult OnPostReadProvenanceExtns([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadProvenanceExtns);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenNexusConnection(); // use PNDC
#if DEBUG
    NPDSCW.DebugNexusRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("ProvenanceExtns", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.NexusDR.ListEditableProvenanceExtns(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseNexusConnection();
    return jsonData;
  }

} // end class

// end file