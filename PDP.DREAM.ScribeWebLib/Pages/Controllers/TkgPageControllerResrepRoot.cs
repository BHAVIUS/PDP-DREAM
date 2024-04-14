// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public const string eidResrepStatus = TkndoElemPrfx + "ResrepRootStatus";
  public const string eidAuthorLink = TkndoLinkPrfx + "Author-";
  public const string eidReviewerLink = TkndoLinkPrfx + "Reviewer-";
  public const string eidEditorLink = TkndoLinkPrfx + "Editor-";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, "", searchFilter, resrepFormat);
    OpenScribeConnection();  // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<CoreResrepRootUxm?> resreps; int numResreps;
      resreps = PSDC.ListEditableResrepRoots(dsRequest, out numResreps);
      dsResult = new DataSourceResult() { Data = resreps, Total = numResreps };
#if DEBUG
      Debug.WriteLine($"Resreps Page Count = {resreps.Count}, Total Count = {numResreps}.");
#endif
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

  public virtual JsonResult OnPostWriteResrepRoot([DataSourceRequest] DataSourceRequest dsRequest,
    CoreResrepRootUxm rrr, string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, "", searchFilter, resrepFormat);
    OpenScribeConnection();  // use PSDC
#if DEBUG
    DebugScribeRepo(nameof(OnPostWriteResrepRoot));
    WRACE.DebugNpdsSelectFilter();
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (ModelState.IsValid)
      {
        rrr = PSDC.EditResrepRoot(rrr);
      }
      rrr.NdisElemId = eidResrepStatus;
      if (string.IsNullOrEmpty(rrr.NdisElemMsg)) { rrr.NdisElemMsg = "NDIS message not set in DAL"; }
      dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
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

  public virtual JsonResult OnPostDeleteResrepRoot([DataSourceRequest] DataSourceRequest dsRequest,
    CoreResrepRootUxm rrr, string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, "", searchFilter, resrepFormat);
    OpenScribeConnection();  // use PSDC
#if DEBUG
    DebugScribeRepo(nameof(OnPostDeleteResrepRoot));
    WRACE.DebugNpdsSelectFilter();
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (ModelState.IsValid)
      {
        using (var psdc = new ScribeDbsqlContext((INpdscwClient)WRACE))
        { rrr = psdc.DeleteResrepRoot(rrr); }
      }
      rrr.NdisElemId = eidResrepStatus;
      dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
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

  // Telerik .LoadcontentFrom() requires Get handler
  public virtual ContentResult OnGetCheckResrepLeaf(Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    CoreResrepLeafUxm? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    var dsResult = ESS;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.ResrepStatusSummary; }
    var htmCntnt = Content(dsResult);
    CloseScribeConnection();
    return htmCntnt;
  }

  public virtual JsonResult OnPostRefreshResrepStatus([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    CoreResrepLeafUxm? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.NdisElemId = eidResrepStatus;
      rrr.NdisElemMsg = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} refreshed from database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostValidateResrepStatus([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    CoreResrepLeafUxm? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ValidateUpdateResrepLeaf(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.NdisElemId = eidResrepStatus;
      rrr.NdisElemMsg = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} validated in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostArchiveResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    WRACE.ArchiveFormat = true;
    OpenScribeConnection();  // use PSDC
    CoreResrepRootUxm? rrr = PSDC.GetEditableResrepRootByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ArchiveResrepRecord(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.NdisElemId = eidResrepStatus;
      rrr.NdisElemMsg = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} archived in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file