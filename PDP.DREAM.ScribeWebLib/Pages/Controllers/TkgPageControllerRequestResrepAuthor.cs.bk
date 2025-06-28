// TkgViewControllerRequestResrepAuthor.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  [HttpGet]
   [PdpRazorViewRoute]
  public IActionResult NpdsRequestResrepAuthor()
  {
    BuildScribeDropDownLists();
    return View();
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
   [PdpRazorViewRoute]
  public JsonResult ScribeSelectResrepAuthorRequests([DataSourceRequest] DataSourceRequest dsr)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableResrepAuthorRequests().ToDataSourceResult(dsr);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
   [PdpRazorViewRoute]
  public JsonResult ScribeUpsertResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsr, ResrepAuthorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = rem.ItemXnam;
    var recordGuid = PdpGuid.ParseToNonNullable(rem.RRRecordGuid, Guid.Empty);
    if (recordGuid == Guid.Empty) // assure presence of valid resouceGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRRecordGuid)} not valid in {nameof(ScribeUpsertResrepAuthorRequest)}";
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
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpRazorViewRoute]
  public JsonResult ScribeDeleteResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsr, ResrepAuthorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteResrepAuthorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpPost]
   [PdpRazorViewRoute( depTSrg)]
  public JsonResult ScribeReqRelNpdsResRepRecord([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    NexusResrepEditModel? rvm = PSDC.GetEditableResrepStemByKey(recordGuid);
    if ((rvm != null) && (rvm.RRRecordGuid == recordGuid)) { rvm = PSDC.RequestReleaseResrepRecord(rvm); }
    DataSourceResult result = (new[] { rvm }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file