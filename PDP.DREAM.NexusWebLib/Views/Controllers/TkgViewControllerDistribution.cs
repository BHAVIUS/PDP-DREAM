// TkgViewControllerDistribution.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnViewController
{
  private const string eidDistributionStatus = "span#DistributionStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult NexusSelectDistributions([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableDistributions(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

} // end class

// end file