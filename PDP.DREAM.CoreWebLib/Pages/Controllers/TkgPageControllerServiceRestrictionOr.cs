// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{
  public const string eidRestrictionOrStatus = TkndoElemPrfx + "ServiceRestrictionOrStatus";

  // ATTN: current implementation does not allow use of parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrsByAndGuid
  public JsonResult OnPostReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
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
      { ModelState.AddModelError("RestrictionOr", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PCDC.ListEditableRestrictionOrsByRGuid(recordGuid)
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

  // ATTN: current implementation does not allow use of parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrs
  public JsonResult OnPostReadServiceRestrictionOrsByAndGuid([DataSourceRequest] DataSourceRequest dsRequest,
     Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    OpenCoreConnection(); // use PCDC
    if (rstrctAndGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", $"{nameof(rstrctAndGuid)} invalid"); }
    var records = PCDC.ListEditableRestrictionOrsByAndGuid(rstrctAndGuid);
    var dsResult = records.ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseCoreConnection();
    return jsonData;
  }


} // end class

// end file