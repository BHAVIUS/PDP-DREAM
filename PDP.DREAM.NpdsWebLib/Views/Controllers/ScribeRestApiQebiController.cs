// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

[RequireHttps, PdpAuthorizeRoles(NpdsAgentRS, NpdsAdminRS)]
public class ScribeRestApiController : ScribeDataRazorViewControllerBase
{
  public ScribeRestApiController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    NPDSCW = new NpdsClientWrace(oaeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeRegistrar, // resets ServicePath, SearchType, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAgent, // resets *ClientRequired
      // TODO: extend to settable option for other message formats
      MessageFormat = NPDSCD.MessageFormatXML
    };
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserClient();
    if (!isUser) { oaeCntxt.Result = Redirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository();
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { oaeCntxt.Result = Redirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetScribeRepository();
  }

  // TODO: all routes for Scribe must be disambiguated from routes for Nexus, PORTAL, DOORS, Atlas
  // TODO: must disambiguate the serviceType from the searchType

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "scribe/agents/" constrained route for iaguid selected individual agent
  [HttpGet, PdpAuthorizeRoles(NpdsAdminRS)]
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
  [HttpGet, PdpAuthorizeRoles(NpdsAdminRS)]
  [PdpRazorViewRoute("scribe/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
    $"Scribe{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgScribe = ScribeRestApiMessage();
    //  return msgScribe;
  }

  // 10 params
  // public void ParseNpdsSelectFilter(string recordAccess,
  //   string serviceType, string serviceVersion, string serviceTag,
  //   string entityType, string entityVersion, string entityTag,
  //   string infosetStatus, string searchType, string resrepFormat)

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, PdpAuthorizeRoles(NpdsAgentRS)]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}/{entityType:NpdsPTC}/{infosetStatus:NpdsISC}",
      $"Scribe{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceTag, string entityType, string infosetStatus)
  {
    NPDSCW.ParseNpdsSelectFilter(ESS, DdeServiceType.Registrar, serviceTag, ESS, ESS, entityType, ESS, ESS, infosetStatus);
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, PdpAuthorizeRoles(NpdsAgentRS)]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}/{entityTag:NpdsPTC}",
      $"Scribe{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceTag, string entityTag)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Registrar, serviceTag, entityTag);
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, PdpAuthorizeRoles(NpdsAgentRS)]
  [PdpRazorViewRoute("scribe/{serviceTag:NpdsPTC}",
      $"Scribe{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceTag)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Registrar, serviceTag);
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  protected IActionResult ScribeRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;
    // stage 1 check of current service
    NPDSCW.ResetScribeRepository(); // NPDSSD.ScribeDbconstr
    var rrRecords = NPDSCW.ScribeDR.ListStorableResrepRecords();
    // stage 2 check of vocabulary service
    if (rrRecords?.Count == 0)
    {
      NPDSCW.ResetScribeRepository(false, false, NPDSCD.EryadDbconstr);
      rrRecords = NPDSCW.ScribeDR.ListStorableResrepRecords();
    }
    // stage 3 check of NPDS cache service 
    if (rrRecords?.Count == 0)
    {
      NPDSCW.ResetScribeRepository(false, false, NPDSCD.CarpDbconstr);
      rrRecords = NPDSCW.ScribeDR.ListStorableResrepRecords();
    }

    var messageValidated = false;

    switch (NPDSCW.MessageFormat.EName)
    {
      case DdeMessageFormat.XML:
        // generate XML message from item list
        var rrMessage = new NpdsResrepXmlRoot();
        NPDSCW.NexusRecords = NPDSCW.ScribeDR.CreateResrepListXml(rrRecords);
        // xsdValidate enables local override independent of PRC.CheckFormat value
        if (NPDSCW.CheckFormat || xsdValidate)
        {
          var pxsWriter = new NpdsXmlStringWriter<NpdsResrepXmlRoot>(rrMessage, NPDSCW);
          var xmlMessage = pxsWriter.XML;
          var pxsValidater = new NpdsXmlValidater(NPDSCW);
          messageValidated = pxsValidater.ValidateNpdsXmlMessage(xmlMessage);
        }
        response = new NpdsXmlResponseWriter<NpdsResrepXmlRoot>(rrMessage, NPDSCW);
        break;
      case DdeMessageFormat.XHTML:
      // TODO: code analogous XHTML writer
      case DdeMessageFormat.JSON:
      // TODO: code analogous JSON writer
      //   else develop single serializing text writer
      //   that can handle both XML and JSON that would provide complete control 
      // response = new PdpXmlResponseWriter<NpdsResrepJsonRoot>(records, PRC, XWS);
      default:
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in ScribeRestApiMessage");
    }

    return response;
  }

} // end class

// end file
