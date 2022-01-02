// PdpRestContext.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
{
  // PDP REST Context (PRC)
  public PdpRestContext PRC { get { return this; } }

  // default parameterless constructor not generated
  public PdpRestContext(HttpRequest? httpReqst)
  {
    if (httpReqst != null)
    {
      NpdsRequest = httpReqst;
      NpdsContext = httpReqst.HttpContext;
      NpdsResponse = httpReqst.HttpContext.Response;
      ParseQueryCollection(NpdsReqstQuery);
    }
  }

  public ClaimsPrincipal OnlineUser { get; set; }

  public string? TkgrArea { get; set; }
  public string? TkgrController { get; set; }
  public string? TkgrViewRole { get; set; }

  public string? UrlHelp { get { return $"{NpdsReqstHost}?{PdpConst.PdpHelpRouteQueryKey}"; } }
  public string? UrlDebug { get { return $"{NpdsReqstHost}?{PdpConst.PdpDebugRouteQueryKey}"; } }
  public string? UrlHelpDebug { get { return $"{NpdsReqstHost}?{PdpConst.PdpDebugRouteQueryKey}&{PdpConst.PdpDebugRouteQueryKey}"; } }

  // code is possibly int or enum
  public string? StatusCode { get; set; }
  // short text for simple display
  public string? StatusName { get; set; }
  // long text for XHTML display of full message
  public string? StatusXhtml { get; set; }

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

} // class
