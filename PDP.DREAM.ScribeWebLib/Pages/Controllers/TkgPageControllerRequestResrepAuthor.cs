// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public virtual JsonResult OnPostReadResrepAuthorRequests([DataSourceRequest] DataSourceRequest dsRequest)
  {
    OpenScribeConnection(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableResrepAuthorRequests().ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostWriteResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepAuthorRequestEditModel rem)
  {
    OpenScribeConnection(); // use PSDC
    var recordName = rem.ItemXnam;
    var recordGuid = PdpGuid.ParseToNonNullable(rem.RRRecordGuid, Guid.Empty);
    if (recordGuid == Guid.Empty) // assure presence of valid resouceGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRRecordGuid)} not valid in {nameof(OnPostWriteResrepAuthorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid)
    {
      // TODO: reactivate the archiving before reassigning the author
      // NexusResrepEditModel rem = PDC.GetEditableScribeResrepRecordByKey((Guid)rem.ResrepRGuid);
      // PDC.ArchiveScribeResrepRecord(rem);
      rem = PSDC.EditResrepAuthorRequest(rem);
    }
    rem.PdpStatusElement = eidResrepRootStatus;
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    CloseScribeConnection();
    return new JsonResult(dsResult);
  }

  public virtual JsonResult OnPostDeleteResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepAuthorRequestEditModel rem)
  {
    OpenScribeConnection(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteResrepAuthorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file