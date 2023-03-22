// TkgViewControllerFairMetric.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnViewController
{
  private const string eidFairMetricStatus = "span#FairMetricStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult NexusSelectFairMetrics([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

} // end class

// end file