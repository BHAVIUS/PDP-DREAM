// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class AtlasTkgPageController
{
  public const string eidRestrictionOrStatus = TkndoElemPrfx + "ServiceRestrictionOrStatus";

  public JsonResult OnPostReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid rstrctAndGuid, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Archive, serviceTag);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenAtlasConnection(); // use PCDC
#if DEBUG
    NPDSCW.DebugAtlasRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", "RRRecordGuid invalid."); }
    else if (rstrctAndGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", "RestrictionAndGuid invalid."); }
    else
    {
      dsResult = NPDSCW.AtlasDR.ListEditableRestrictionOrsByAGuid(rstrctAndGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseAtlasConnection();
    return jsonData;
  }



} // end class

// end file