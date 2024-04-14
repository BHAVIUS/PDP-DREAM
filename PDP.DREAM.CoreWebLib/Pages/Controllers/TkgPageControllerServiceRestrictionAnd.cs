// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{
  public const string eidRestrictionAndStatus = TkndoElemPrfx + "ServiceRestrictionAndStatus";

  // ATTN: current implementation does not allow use of parameter isLimited
  public JsonResult OnPostReadServiceRestrictionAnds([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
    OpenCoreConnection(); // use PCDC
#if DEBUG
    DebugCoreRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("RestrictionAnd", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PCDC.ListEditableRestrictionAndsByRGuid(recordGuid)
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
    CloseCoreConnection();
    return jsonData;
  }


} // end class

// end file