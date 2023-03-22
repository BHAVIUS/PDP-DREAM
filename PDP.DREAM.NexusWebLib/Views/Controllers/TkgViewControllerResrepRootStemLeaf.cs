// TkgViewControllerResrepRootStemLeaf.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnViewController
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSststet)]
  public JsonResult NexusSelectResrepRoots([DataSourceRequest] DataSourceRequest request,
    string serviceType, string serviceTag, string entityType, string userRole)
  {
    QURC.ParseNpdsSelectFilterForView(serviceType, serviceTag, entityType);
    ResetNexusRepository();  // use PNDC
    var resreps = PNDC.ListViewableResrepRoots(request, out int numResreps);
    var result = new DataSourceResult() { Data = resreps, Total = numResreps };
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrg)]
  public ActionResult<string?> NexusCheckResrepRoot(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepRootByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrg)]
  public ActionResult<string?> NexusCheckResrepStem(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepStemByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrg)]
  public ActionResult<string?> NexusCheckResrepLeaf(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepLeafByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

} // end class

// end file