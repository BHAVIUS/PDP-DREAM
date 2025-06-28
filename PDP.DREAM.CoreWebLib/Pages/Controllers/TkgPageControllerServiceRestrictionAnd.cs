// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{
  public const string eidRestrictionAndStatus = TkndoElemPrfx + "ServiceRestrictionAndStatus";

  public JsonResult OnPostReadServiceRestrictionAnds([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
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
    { ModelState.AddModelError("RestrictionAnd", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PCDC.ListEditableRestrictionAndsByRGuid(recordGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseCoreConnection();
    return jsonData;
  }


} // end class

// end file