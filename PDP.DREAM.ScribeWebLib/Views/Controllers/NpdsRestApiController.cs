// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PDP.DREAM.ScribeWebLib.Controllers;

[RequireHttps, Authorize]
public class ScribeRestApiController : ScribeDataRazorViewControllerBase
{
  public ScribeRestApiController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    WRACE = new NpdsClientWrace(oaeCntxt.HttpContext)
    {
      DatabaseType = NPDSCD.DatabaseTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    ResetScribeRepository();
    var isValid = CheckNpdsAgentSession();
    if (!isValid) { oaeCntxt.Result = Redirect(DepAnonModeAccessDenied); }
  }

  // TODO: all routes for Scribe must be disambiguated from routes for Nexus, PORTAL, DOORS, Core
  // TODO: must disambiguate the serviceType from the searchFilter

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "scribe/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize]
  // [PdpRazorViewRoute("{serviceType:ScribeSTC}/agents/{iaguid:guid}",
  [PdpRazorViewRoute("scribe/agents/{iaguid:guid}",
    $"Scribe{nameof(AgentsByGuid)}", false)]
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
  // [PdpRazorViewRoute("{serviceType:ScribeSTC}/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
  [PdpRazorViewRoute("scribe/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
    $"Scribe{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgScribe = ScribeRestApiMessage();
    //  return msgScribe;
  }

  //public void ParseNpdsSelectFilter(string recordAccess,
  //string serviceType, string serviceVersion, string serviceTag,
  //string entityType, string entityVersion, string entityTag,
  //string infosetStatus, string searchFilter)

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, Authorize]
  // [PdpRazorViewRoute("{serviceType:ScribeSTC}/{serviceTag:NpdsPTC}/{entityType:NpdsPTC}/{infosetStatus:NpdsISC}",
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}/{entityType:NpdsPTC}/{infosetStatus:NpdsISC}",
    $"Scribe{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    var searchFilter = NPDSCD.SearchFilterDiristry.ToString();
    WRACE.ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      entityType, "", "",
      infosetStatus, searchFilter, "");
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, Authorize]
  // [PdpRazorViewRoute("{serviceType:ScribeSTC}/{serviceTag:NpdsPTC}/{entityTag:NpdsPTC}/{entityVersion?}",
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}/{entityTag:NpdsPTC}/{entityVersion?}",
    $"Scribe{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    var searchFilter = NPDSCD.SearchFilterDiristry.ToString();
    WRACE.ParseNpdsSelectFilter(searchFilter, serviceTag, serviceType, entityTag, entityVersion);
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, Authorize]
  // [PdpRazorViewRoute("{serviceType:ScribeSTC}/{serviceTag:NpdsPTC}",
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}",
    $"Scribe{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    var searchFilter = NPDSCD.SearchFilterDiristry.ToString();
    WRACE.ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      "", "", "",
      "", searchFilter, "");
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  protected IActionResult ScribeRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;
    // stage 1 check of current service
    ResetScribeRepository(); // NPDSSD.ScribeDbconstr
    var rrRoots = PSDC.ListStorableResrepRootsWithFacets();
    // stage 2 check of vocabulary service
    if (rrRoots?.Count == 0)
    {
      ResetScribeRepository(false, NPDSCD.VocabDbconstr);
      rrRoots = PSDC.ListStorableResrepRootsWithFacets();
    }
    // generate XML message from item list
    var rrListXml = PSDC.CreateCoreResrepListXml(rrRoots);
    // PRC.ResponseAnswer = rrListXml; // alternative format response
    WRACE.NexusRecords = rrListXml;

    var messageValidated = false;

    switch (WRACE.MessageFormat.EName)
    {
      case DdeMessageFormat.XML:
        var rrMessage = new NpdsResrepXmlRoot();
        // xsdValidate enables local override independent of PRC.CheckFormat value
        if (WRACE.CheckFormat || xsdValidate)
        {
          var pxsWriter = new NpdsXmlStringWriter<NpdsResrepXmlRoot>(rrMessage, WRACE);
          var xmlMessage = pxsWriter.XML;
          var pxsValidater = new NpdsXmlValidater(WRACE);
          messageValidated = pxsValidater.ValidateNpdsXmlMessage(xmlMessage);
        }
        response = new NpdsXmlResponseWriter<NpdsResrepXmlRoot>(rrMessage, WRACE);
        break;
      case DdeMessageFormat.JSON:
      // TODO: code analogous JSON writer
      //     or else develop single serializing text writer
      //     that can handle both XML and JSON
      //     that would provide complete control 
      // response = new PdpXmlResponseWriter<NpdsResrepJsonRoot>(records, PRC, XWS);
      case DdeMessageFormat.XHTML:
      default:
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in ScribeRestApiMessage");
    }

    return response;
  }

} // end class

// end file