// TkgViewControllerResrepRootStemLeaf.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSststet)]
  public JsonResult ScribeSelectResrepRoots([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    var resreps = PSDC.ListEditableResrepRoots(request, out int numResreps);
    var result = new DataSourceResult() { Data = resreps, Total = numResreps };
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSststet)]
  public JsonResult ScribeUpsertResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { nre = PSDC.EditResrepRoot(nre); nre.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSststet)]
  public JsonResult ScribeDeleteResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType);
    ResetScribeRepository();  // use PSDC
    if (ModelState.IsValid) { nre = PSDC.DeleteResrepRoot(nre); nre.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSrg)]
  public ActionResult<string?> ScribeCheckResrepRoot(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepRootByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSrg)]
  public ActionResult<string?> ScribeCheckResrepStem(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepStemByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSrg)]
  public ActionResult<string?> ScribeCheckResrepLeaf(Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSrg)]
  public JsonResult ScribeRefreshResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre.PdpStatusElement = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} refreshed from database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put for Rest, Post for Ajax
  [PdpRazorViewRoute( depTSrg)]
  public JsonResult ScribeValidateResrepStatus([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByRKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre = PSDC.ValidateUpdateResrepLeaf(nre);
      nre.PdpStatusElement = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} validated in database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpPost]
  [PdpRazorViewRoute( depTSrg)]
  public JsonResult ScribeArchiveResrepSnapshot([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    QURC.ArchiveFormat = true;
    ResetScribeRepository();  // use PSDC
    NexusResrepEditModel? nre = PSDC.GetEditableResrepLeafByKey(recordGuid);
    if (nre?.RRRecordGuid == recordGuid)
    {
      nre = PSDC.ArchiveResrepRecord(nre);
      nre.PdpStatusElement = eidResrepRootStatus;
      nre.PdpStatusMessage = $"{nre.ItemXnam} record with handle {nre.RecordHandle} archived in database";
    }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file