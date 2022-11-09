// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: inherit/implement ??? abstract Microsoft.AspNetCore.Http.HttpContext
public partial class QebUserRestContext : NpdsParameters 
{
  public QebUserRestContext(HttpContext httpCntxt)
  {
    if (httpCntxt != null)
    {
      NpdsContext = httpCntxt;
      NpdsRequest = httpCntxt.Request;
      NpdsResponse = httpCntxt.Response;
      // TODO: refactor the call to ParseQueryCollection for use in both Apis and Apps
      if (NpdsReqstQuery.Count > 0) { ParseQueryCollection(NpdsReqstQuery); }
    }
  }

  public string? UrlHelp { get { return $"{NpdsReqstHost}?{PdpHelpRouteQueryKey}"; } }
  public string? UrlDebug { get { return $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}"; } }
  public string? UrlHelpDebug { get { return $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}&{PdpDebugRouteQueryKey}"; } }

} // end class

// end file