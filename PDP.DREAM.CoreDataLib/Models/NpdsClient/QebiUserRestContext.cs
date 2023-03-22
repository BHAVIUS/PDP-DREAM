// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext : NpdsClient, IQebiUserRestContext
{
  // TODO: inherit/implement ??? abstract Microsoft.AspNetCore.Http.HttpContext
  //       or leave as is with constructor dependency injection
  public QebiUserRestContext(HttpContext? httpCntxt)
  {
    if (httpCntxt != null)
    {
      NpdsContext = httpCntxt;
      NpdsRequest = httpCntxt.Request; // sets NpdsReqstQuery and other props
      NpdsResponse = httpCntxt.Response;
      // TODO: refactor the call to ParseQueryCollection for use in both Apis and Apps
      if (NpdsReqstQuery.Count > 0) { ParseNpdsQueryString(NpdsReqstQuery); }
    }
  }

  // content context for NPDS service

  private HttpContext? npdsCntxt = null;
  public HttpContext? NpdsContext
  {
    set { npdsCntxt = value; }
    get { return npdsCntxt; }
  }

  // content context for NPDS request

  private HttpRequest? npdsReqst = null;
  public HttpRequest? NpdsRequest
  {
    set {
      npdsReqst = value;
      if (npdsReqst != null)
      {
        NpdsReqstBaseUrl = $"{npdsReqst.Scheme}://{npdsReqst.Host}{npdsReqst.PathBase}";
        NpdsReqstDisplayUrl = npdsReqst.GetDisplayUrl();
        NpdsReqstEncodedUrl = npdsReqst.GetEncodedUrl();
        NpdsReqstHost = npdsReqst.Host;
        NpdsReqstPath = npdsReqst.Path;
        NpdsReqstPathBase = npdsReqst.PathBase;
        NpdsReqstQuery = npdsReqst.Query;
        NpdsReqstQueryString = npdsReqst.QueryString;
        NpdsReqstScheme = npdsReqst.Scheme;
      }
    }
    get { return npdsReqst; }
  }

  public string? NpdsReqstBaseUrl { get; set; } = string.Empty;
  public string? NpdsReqstDisplayUrl { get; set; } = string.Empty;
  public string? NpdsReqstEncodedUrl { get; set; } = string.Empty;
  public HostString? NpdsReqstHost { get; set; } = null;
  public string? NpdsReqstPath { get; set; } = string.Empty;
  public string? NpdsReqstPathBase { get; set; } = string.Empty;
  public IQueryCollection? NpdsReqstQuery { get; set; } = null;
  public QueryString? NpdsReqstQueryString { get; set; } = null;
  public string? NpdsReqstScheme { get; set; } = string.Empty;

  // TODO: relocate in a core folder such as /nsvo/ or /npds/
  public string? NpdsReqstXmlSchemaUrl { get { return NpdsReqstBaseUrl + "/pub/xsd/"; } }
  public override string? UrlHelp { get { return $"{NpdsReqstHost}?{PdpHelpRouteQueryKey}"; } }
  public override string? UrlDebug { get { return $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}"; } }
  public override string? UrlHelpDebug { get { return $"{NpdsReqstHost}?{PdpHelpRouteQueryKey}&{PdpDebugRouteQueryKey}"; } }


  // content context for NPDS response

  private HttpResponse? npdsRspns = null;
  public HttpResponse? NpdsResponse
  {
    set { npdsRspns = value; }
    get { return npdsRspns; }
  }

} // end class

// end file