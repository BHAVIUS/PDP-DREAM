// TkgViewControllerRequestServiceEditor.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  [HttpGet]
   [PdpRazorViewRoute]
  public IActionResult NpdsRequestServiceEditor()
  {
    BuildScribeDropDownLists();
    return View();
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
   [PdpRazorViewRoute]
  public JsonResult ScribeSelectServiceEditorRequests([DataSourceRequest] DataSourceRequest dsr)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableServiceEditorRequests().ToDataSourceResult(dsr);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpRazorViewRoute]
  public JsonResult ScribeUpsertServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = rem.ItemXnam;
    var infosetGuid = PdpGuid.ParseToNonNullable(rem.RRInfosetGuid, Guid.Empty);
    if (infosetGuid == Guid.Empty) // assure presence of valid infosetGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRInfosetGuid)} not valid in {nameof(ScribeUpsertServiceEditorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid) { rem = PSDC.EditServiceEditorRequest(rem); }
    rem.PdpStatusElement = eidResrepRootStatus;
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
   [PdpRazorViewRoute]
  public JsonResult ScribeDeleteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteServiceEditorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file