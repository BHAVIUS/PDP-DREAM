// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext : IQebUserRestContext
{
  // do not allow zero-parameter constructor
  // require HttpRequest for one-parameter constructor
  public QebUserRestContext(HttpRequest httpReqst)
  {
    if (httpReqst != null)
    {
      NpdsRequest = httpReqst;
      NpdsContext = httpReqst.HttpContext;
      NpdsResponse = httpReqst.HttpContext.Response;
      ParseQueryCollection(NpdsReqstQuery);
    }
  }


  public string? UrlHelp { get { return $"{NpdsReqstHost}?{PdpHelpRouteQueryKey}"; } }
  public string? UrlDebug { get { return $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}"; } }
  public string? UrlHelpDebug { get { return $"{NpdsReqstHost}?{PdpDebugRouteQueryKey}&{PdpDebugRouteQueryKey}"; } }


  // code is possibly int or enum
  public string? StatusCode { get; set; } = string.Empty;
  // short text for simple display
  public string? StatusName { get; set; } = string.Empty;
  // long text for XHTML display of full message
  public string? StatusXhtml { get; set; } = string.Empty;


  // list item properties

  public bool ItemIsPrivate { get; set; } = false;

  public bool ItemIsConcise { get; set; } = false;

  public bool ItemCanBeAccessed
  { get { return (!ItemIsPrivate || ClientIsAuthorized); } }

  public bool ItemCanBeVerbosed // "verbosed" is coined term meaning "displayed verbosely" (CT 2011/10/15)
  { get { return (!ItemIsConcise || ClientIsAuthorized); } }

  public bool ItemDoesArchive
  { get { return (ItemCanBeAccessed && ArchiveFormat); } }

  public bool ItemDoesVerbose
  { get { return (ItemCanBeVerbosed && VerboseFormat); } }

} // end class

// end file