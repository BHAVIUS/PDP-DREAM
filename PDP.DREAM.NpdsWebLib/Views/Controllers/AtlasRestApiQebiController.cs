// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

[RequireHttps, AllowAnonymous]
public class AtlasRestApiQebiController : AtlasDataRazorViewControllerBase
{
  public AtlasRestApiQebiController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    NPDSCW = new NpdsClientWrace(oaeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeArchive,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      LnqFltrPublicOnly = false,
      LnqFltrQryStrValues = true,
      // TODO: extend to settable option for other message formats
      MessageFormat = NPDSCD.MessageFormatXML
    };
    NPDSCW.ResetAtlasRepository();
    //if (!QebRazorAnonList.Contains(oaeCntxt.ActionName()))
    //{
    //  var isReader = CheckAtlasReaderSession();
    //  if (!isReader) { oaeCntxt.Result = Redirect(DepQebIdentRequired); }
    //}
  }

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "atlas/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize]
  [PdpRazorViewRoute("atlas/agents/{iaguid:guid}",
    $"Atlas{nameof(AgentsByGuid)}", false)]
  public IActionResult AgentsByGuid(Guid iaguid)
  {
    throw new NotImplementedException();
    // var msgAtlas = AtlasRestApiMessage();
    //  return msgAtlas;
  }

  // "atlas/resreps/" constrained route for rrguid selected individual resrep
  //    and optional selected subcollection rrlistxnam and subitem rritemguid
  //   TODO: add regex constraint rrlistxnam = NpdsConst.RegexResRepListXnams
  //       examples show "{parameter:regex(theRegexPattern)}" where the RegexPattern is hardcoded
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("atlas/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
    $"Atlas{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgAtlas = AtlasRestApiMessage();
    //  return msgAtlas;
  }

  // ATTN: RESTful API paths must be different and unique across related controllers

} // end class

// end file