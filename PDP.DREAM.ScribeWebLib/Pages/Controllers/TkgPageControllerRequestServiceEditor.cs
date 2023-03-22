// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public virtual JsonResult OnPostReadServiceEditorRequests([DataSourceRequest] DataSourceRequest dsRequest)
  {
    var rzrHndlr = nameof(OnPostReadServiceEditorRequests);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
        dsResult = PSDC.ListEditableServiceEditorRequests()
        .ToDataSourceResult(dsRequest);
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

  public virtual JsonResult OnPostWriteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceEditorRequestEditModel rem, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    var recordName = rem.ItemXnam;
    var infosetGuid = PdpGuid.ParseToNonNullable(rem.RRInfosetGuid, Guid.Empty);
    if (infosetGuid == Guid.Empty) // assure presence of valid infosetGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRInfosetGuid)} not valid in {nameof(OnPostWriteServiceEditorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid) { rem = PSDC.EditServiceEditorRequest(rem); }
    rem.PdpStatusElement = eidResrepRootStatus;
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceEditorRequestEditModel rem, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteServiceEditorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file