using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // default parameterless constructor not generated
    public PdpRestContext(HttpRequest? httpReqst)
    {
      if (httpReqst != null)
      {
        PdpRequest = httpReqst; // see file PdpOptionsForRequest
        ParseQueryCollection(PdpReqstQuery);
      }
    }

    public PdpRestContext PRC
    {
      get { return this; }
    }

    public ClaimsPrincipal OnlineUser { get; set; }
    public string? HelpUrl { get; set; }
    public string? DebugUrl { get; set; }
    public string? HelpDebugUrl { get; set; }

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

  }

}
