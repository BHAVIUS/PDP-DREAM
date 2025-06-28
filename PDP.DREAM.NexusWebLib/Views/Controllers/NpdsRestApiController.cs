// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

[RequireHttps, AllowAnonymous]
public class NexusRestApiController : NexusDataRazorViewControllerBase
{
  public NexusRestApiController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    NPDSCW = new NpdsClientWrace(oaeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeNexus,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      // TODO: extend to settable option for other message formats
      MessageFormat = NPDSCD.MessageFormatXML
    };
    NPDSCW.ResetNexusRepository();
    // TODO: split this controller into two controllers 
    // one for anonymous and one for authorized
    //if (!QebRazorAnonList.Contains(oaeCntxt.ActionName()))
    //{
    //  var isUser = CheckCoreUserSession();
    //  if (!isUser) { oaeCntxt.Result = Redirect(DepQebIdentRequired); }
    //}
  }

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "nexus/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize]
  [PdpRazorViewRoute("{serviceType:NexusSTC}/agents/{iaguid:guid}",
    $"Nexus{nameof(AgentsByGuid)}", false)]
  public IActionResult AgentsByGuid(Guid iaguid)
  {
    throw new NotImplementedException();
    // var msgNexus = NexusRestApiMessage();
    //  return msgNexus;
  }

  // "nexus/resreps/" constrained route for rrguid selected individual resrep
  //    and optional selected subcollection rrlistxnam and subitem rritemguid
  //   TODO: add regex constraint rrlistxnam = NpdsConst.RegexResRepListXnams
  //       examples show "{parameter:regex(theRegexPattern)}" where the RegexPattern is hardcoded
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusSTC}/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
    $"Nexus{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgNexus = NexusRestApiMessage();
    //  return msgNexus;
  }

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusSTC}/{serviceTag:NpdsPTC}/{entityType:NpdsPTC}/{infosetStatus:NpdsISC}",
    $"Nexus{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    NPDSCW.ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      entityType, "", "",
      infosetStatus, "", "");
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusSTC}/{serviceTag:NpdsPTC}/{entityTag:NpdsPTC}/{entityVersion?}",
    $"Nexus{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    NPDSCW.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, entityVersion);
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusSTC}/{serviceTag:NpdsPTC}",
    $"Nexus{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    // var searchFilter = NPDSCD.SearchFilterDiristry.ToString();
    // TODO: assure that searchFilter reset by serviceType
    NPDSCW.ParseNpdsSelectFilter(serviceType, serviceTag, "", "", "");
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  protected IActionResult NexusRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;
    // stage 1 check of current service
    NPDSCW.ResetNexusRepository(); // NPDSSD.NexusDbconstr
    var rrRoots = NPDSCW.PNDC.ListStorableResrepRoots();
    // stage 2 check of vocabulary service
    if (rrRoots?.Count == 0)
    {
      NPDSCW.ResetNexusRepository(false, false, NPDSCD.VocabDbconstr);
      rrRoots = NPDSCW.PNDC.ListStorableResrepRoots();
    }
    // stage 3 check of NPDS cache service 
    if (rrRoots?.Count == 0)
    {
      NPDSCW.ResetNexusRepository(false, false, NPDSCD.CacheDbconstr);
      rrRoots = NPDSCW.PNDC.ListStorableResrepRoots();
    }

    var messageValidated = false;

    switch (NPDSCW.MessageFormat.EName)
    {
      case DdeMessageFormat.XML:
        // generate XML message from item list
        var rrMessage = new NpdsResrepXmlRoot();
        NPDSCW.NexusRecords = NPDSCW.PNDC.CreateResrepListXml(rrRoots);
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
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in NexusRestApiMessage");
    }

    return response;
  }

} // end class

// end file