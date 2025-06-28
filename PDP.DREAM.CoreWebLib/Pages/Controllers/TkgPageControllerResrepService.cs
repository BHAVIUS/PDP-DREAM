// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{
  public const string eidResrepServiceStatus = TkndoElemPrfx + "ResrepServiceStatus";

  public JsonResult OnPostReadResrepServices([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadResrepServices);
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Core, serviceTag);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenCoreConnection(); // use PCDC
#if DEBUG
    NPDSCW.DebugCoreRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("ResrepServices", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PCDC.ListEditableResrepServices(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseCoreConnection();
    return jsonData;
  }

} // end class

// end file