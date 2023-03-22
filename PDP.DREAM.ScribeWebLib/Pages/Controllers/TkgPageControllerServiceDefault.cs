// TkgPageControllerServiceDefault.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidServiceDefaultStatus = "span#ServiceDefaultStatus";

  public virtual JsonResult OnPostReadServiceDefaults([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceDefaults);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
    { ModelState.AddModelError("ServiceDefaults", "RRRecordGuid invalid."); }
      else
      {
     dsResult = PSDC.ListEditableServiceDefaults(recordGuid, isLimited)
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
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostWriteServiceDefault([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceDefaultEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditServiceDefault(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidServiceDefaultStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceDefault([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceDefaultEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteServiceDefault(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidServiceDefaultStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckServiceDefault([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    ServiceDefaultEditModel? fgr = PSDC.GetEditableServiceDefaultByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.CheckServiceDefault(fgr); fgr.PdpStatusElement = eidServiceDefaultStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReseqServiceDefault([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    ServiceDefaultEditModel? fgr = PSDC.GetEditableServiceDefaultByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.ReseqServiceDefault(fgr); fgr.PdpStatusElement = eidServiceDefaultStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file