// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Newtonsoft.Json.Linq;
using System;
using Telerik.SvgIcons;

namespace PDP.DREAM.CoreDataLib.Models;

// NPDS Client Wrace (Npdscw*)
public partial class NpdsClientWrace : INpdscwClient
{
  public NpdsClientWrace() : this(NPDSCD.DatabaseTypeNone) { }
  public NpdsClientWrace(NpdsDatabaseType dbType) { DatabaseType = dbType; }
  public NpdsClientWrace(HttpContext? httpCntxt) : this(NPDSCD.DatabaseTypeNone)
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

  // context for NPDS request

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

  public string? NpdsReqstBaseUrl { get; set; } = ESS;
  public string? NpdsReqstDisplayUrl { get; set; } = ESS;
  public string? NpdsReqstEncodedUrl { get; set; } = ESS;
  public HostString? NpdsReqstHost { get; set; } = null;
  public string? NpdsReqstPath { get; set; } = ESS;
  public string? NpdsReqstPathBase { get; set; } = ESS;
  public IQueryCollection? NpdsReqstQuery { get; set; } = null;
  public QueryString? NpdsReqstQueryString { get; set; } = null;
  public string? NpdsReqstScheme { get; set; } = ESS;
  public string? NpdsReqstXmlSchemaUrl { get { return NpdsReqstBaseUrl + NpdsXsdPath; } }

  private string? urlHelp = ESS;
  public string? UrlHelp
  {
    set { urlHelp = value; }
    get {
      if (string.IsNullOrEmpty(urlHelp))
      { urlHelp = $"{NpdsReqstHost}?{PdpHelpRouteQueryKey}"; }
      return urlHelp;
    }
  }
  private string? urlDebug = ESS;
  public string? UrlDebug
  {
    set { urlDebug = value; }
    get {
      if (string.IsNullOrEmpty(urlDebug))
      { urlDebug = $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}"; }
      return urlDebug;
    }
  }

  // content for NPDS request
  public string? RequestQuestion { get; set; } = ESS;
  public string? RequestNote { get; set; } = ESS;


  // context for NPDS response

  private HttpResponse? npdsRspns = null;
  public HttpResponse? NpdsResponse
  {
    set { npdsRspns = value; }
    get { return npdsRspns; }
  }

    // content for NPDS response

  private string? respNote = ESS;
  public string? ResponseNote
  {
    set {
      var sb = new StringBuilder(ResponseNote);
      sb.AppendLine(value);
      respNote = sb.ToString();
    }
    get {
      if (string.IsNullOrEmpty(respNote))
      {
        if (!string.IsNullOrEmpty(ServiceNote)) { respNote = ServiceNote; };
      }
      return respNote;
    }
  }
  public string? ResponseStatus { get; set; } = ESS;
  public string? ResponseHeader { get; set; } = ESS;

  public NpdsResrepList? ResponseAnswer { set; get; }
  public NpdsResrepList? ResponseRelated { set; get; }
  public NpdsResrepList? ResponseReferred { set; get; }
  public string? NpdsStatusMessage { get; set; } = ESS;
 
  // list item status properties
  public string? StatusName { get; set; } = ESS;
  // short text for simple display
  public string? StatusNote { get; set; } = ESS;
  // long text for XHTML display of full message
  public string? StatusXhtml { get; set; } = ESS;
  public string? ServiceError { set; get; } = ESS;
  public string? ServiceNote { set; get; } = ESS;

  public NpdsResrepList? CoreRecords { set; get; }
  public NpdsResrepList? PortalRecords { set; get; }
  public NpdsResrepList? DoorsRecords { set; get; }
  public NpdsResrepList? NexusRecords { set; get; }

} // end class

// end file