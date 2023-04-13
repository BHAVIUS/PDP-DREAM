// ScribeRestApiController.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

[RequireHttps, Authorize]
public class ScribeRestApiController : ScribeDataRazorViewControllerBase
{
  public ScribeRestApiController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    QURC = new QebiUserRestContext(oaeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    ResetScribeRepository();
    // TODO: split this controller into two controllers 
    // one for anonymous and one for authorized, 
    // then deprecate the QebRazorAnonList
    // if (!QebRazorAnonList.Contains(oaeCntxt.ActionName()))
    // {
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { oaeCntxt.Result = Redirect(DepQebIdentRequired); }
    // }
  }

  //
  // Index/Help/Examples first, then rest alphabetical
  //
  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "scribe/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize]
  [PdpRazorViewRoute("scribe/agents/{iaguid:guid}", $"Scribe{nameof(AgentsByGuid)}", false)]
  public IActionResult AgentsByGuid(Guid iaguid)
  {
    throw new NotImplementedException();
    // var msgScribe = ScribeRestApiMessage();
    //  return msgScribe;
  }

  // "scribe/resreps/" constrained route for rrguid selected individual resrep
  //    and optional selected subcollection rrlistxnam and subitem rritemguid
  //   TODO: add regex constraint rrlistxnam = NpdsConst.RegexResRepListXnams
  //       examples show "{parameter:regex(theRegexPattern)}" where the RegexPattern is hardcoded
  [HttpGet, Authorize]
  [PdpRazorViewRoute("scribe/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}", $"Scribe{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgScribe = ScribeRestApiMessage();
    //  return msgScribe;
  }

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, Authorize]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPT}/{entityType:NpdsPT}/{infosetStatus:NpdsIS}", $"Scribe{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    var es = string.Empty;
    // TODO: hardcoded strings must be updated
    QURC.ParseNpdsService("diristry", serviceTag, "scribe", es, es, entityType, infosetStatus);
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, Authorize]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPT}/{entityTag:NpdsPT}/{entityVersion?}", $"Scribe{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    // TODO: hardcoded strings must be updated
    QURC.ParseNpdsService("diristry", serviceTag, "scribe", entityTag, entityVersion);
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, Authorize]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPT}", $"Scribe{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    // TODO: hardcoded strings must be updated
    QURC.ParseNpdsService("diristry", serviceTag, "scribe");
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  protected IActionResult ScribeRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;

    var rrRoots = PSDC.ListStorableNexusRootsWithFacets();
    var rrListXml = PSDC.CreateNexusResrepListXml(rrRoots);
    // PRC.ResponseAnswer = rrListXml; // alternative format response
    QURC.NexusRecords = rrListXml;

    var messageValidated = false;

    switch (QURC.MessageFormat)
    {
      case PdpAppConst.NpdsMessageFormat.XML:
        var rrMessage = new NpdsResrepXmlRoot();
        // xsdValidate enables local override independent of PRC.CheckFormat value
        if (QURC.CheckFormat || xsdValidate)
        {
          var pxsWriter = new NpdsXmlStringWriter<NpdsResrepXmlRoot>(rrMessage, QURC);
          var xmlMessage = pxsWriter.XML;
          var pxsValidater = new NpdsXmlValidater(QURC);
          messageValidated = pxsValidater.ValidateNpdsXmlMessage(xmlMessage);
        }
        response = new NpdsXmlResponseWriter<NpdsResrepXmlRoot>(rrMessage, QURC);
        break;
      case PdpAppConst.NpdsMessageFormat.JSON:
      // TODO: code analogous JSON writer
      //     or else develop single serializing text writer
      //     that can handle both XML and JSON
      //     that would provide complete control 
      // response = new PdpXmlResponseWriter<NpdsResrepJsonRoot>(records, PRC, XWS);
      case PdpAppConst.NpdsMessageFormat.XHTML:
      default:
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in ScribeRestApiMessage");
    }

    return response;
  }

} // end class

// end file