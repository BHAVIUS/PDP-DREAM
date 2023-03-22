// TkgPageControllerResrepRootStemLeaf.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(true);  // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<NexusResrepEditModel?> resreps; int numResreps;
      resreps = PSDC.ListEditableResrepRoots(dsRequest, out numResreps);
      dsResult = new DataSourceResult() { Data = resreps, Total = numResreps };
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
    NexusResrepEditModel rrr, string searchFilter, string serviceTag, string entityType)
  {
    QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection();  // use PSDC
#if DEBUG
    DebugScribeRepo(nameof(OnPostReadResrepRoots));
    QURC.DebugNpdsParams();
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (ModelState.IsValid)
      {
        rrr = PSDC.EditResrepRoot(rrr);
      }
      rrr.PdpStatusElement = eidResrepRootStatus;
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
    NexusResrepEditModel rrr, string searchFilter, string serviceTag, string entityType)
  {
    QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection();  // use PSDC
#if DEBUG
    DebugScribeRepo(nameof(OnPostReadResrepRoots));
    QURC.DebugNpdsParams();
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (ModelState.IsValid)
      {
        using (var psdc = new ScribeDbsqlContext((INpdsClient)QURC))
        { rrr = psdc.DeleteResrepRoot(rrr); }
      }
      rrr.PdpStatusElement = eidResrepRootStatus;
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
  public virtual ContentResult OnGetCheckResrepRoot(Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepRootByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    CloseScribeConnection();
    return htmCntnt;
  }
  // Telerik .LoadcontentFrom() requires Get handler
  public virtual ContentResult OnGetCheckResrepStem(Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepStemByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    CloseScribeConnection();
    return htmCntnt;
  }
  // Telerik .LoadcontentFrom() requires Get handler
  public virtual ContentResult OnGetCheckResrepLeaf(Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    var dsResult = string.Empty;
    if (rrr?.RRRecordGuid == recordGuid) { dsResult = rrr.NexusStatusSummary; }
    var htmCntnt = Content(dsResult);
    CloseScribeConnection();
    return htmCntnt;
  }

  public virtual JsonResult OnPostRefreshResrepStatus([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    OpenScribeConnection();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} refreshed from database";
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
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ValidateUpdateResrepLeaf(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} validated in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReqRelResrepRecord([DataSourceRequest] DataSourceRequest dsRequest,
  Guid recordGuid)
  {
    OpenScribeConnection(); // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.RequestReleaseResrepRecord(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      // rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} validated in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostArchiveResrepSnapshot([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid)
  {
    QURC.ArchiveFormat = true;
    OpenScribeConnection();  // use PSDC
    NexusResrepEditModel? rrr = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr = PSDC.ArchiveResrepRecord(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      rrr.PdpStatusElement = eidResrepLeafStatus;
      rrr.PdpStatusMessage = $"{rrr.ItemXnam} record with handle {rrr.RecordHandle} archived in database";
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file