// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

[RequireHttps, AllowAnonymous]
public class NexusRestApiAnonController : NexusDataRazorViewControllerBase
{
  public NexusRestApiAnonController() : base() { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    NPDSCW = new NpdsClientWrace(oaeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeDiristry,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      LnqFltrRecordAccess = false,
      LnqFltrAuthoritativeOnly = false,
      LnqFltrPublicOnly = true,
      LnqFltrQryStrValues = true,
      LnqFltrNpdsService = true,
      // TODO: extend to settable option for other message formats
      MessageFormat = NPDSCD.MessageFormatXML
    };
    NPDSCW.ResetNexusRepository();
  }

  // ATTN: ServiceType/ServiceTag in path segments and SearchType/SearchTag in querystring parameters

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("nexus/{serviceTag:NpdsPTC}/{entityType:NpdsPTC}/{infosetStatus:NpdsISC}",
    $"Nexus{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceTag, string entityType, string infosetStatus)
  {
    NPDSCW.ParseNpdsSelectFilter(ESS, DdeServiceType.Diristry, serviceTag,
      DdeServiceType.Diristry, serviceTag, entityType, ESS, ESS, infosetStatus);
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("nexus/{serviceTag:NpdsPTC}/{entityTag:NpdsPTC}",
    $"Nexus{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceTag, string entityTag)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Diristry, serviceTag, entityTag);
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("nexus/{serviceTag:NpdsPTC}",
    $"Nexus{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceTag)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Diristry, serviceTag);
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // ATTN: xsdValidate default to true during development, reset to false for release
  protected IActionResult NexusRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;
    bool withFacets = true;
    // stage 1 check of current service
    NPDSCW.ResetNexusRepository(); // NPDSSD.NexusDbconstr
    var rrRecords = NPDSCW.NexusDR.ListStorableResrepRecords(withFacets);
    // stage 2 check of vocabulary service
    if (rrRecords?.Count == 0)
    {
      NPDSCW.ResetNexusRepository(false, false, NPDSCD.EryadDbconstr);
      rrRecords = NPDSCW.NexusDR.ListStorableResrepRecords(withFacets);
    }
    // stage 3 check of NPDS cache service 
    if (rrRecords?.Count == 0)
    {
      NPDSCW.ResetNexusRepository(false, false, NPDSCD.CarpDbconstr);
      rrRecords = NPDSCW.NexusDR.ListStorableResrepRecords(withFacets);
    }

    var messageValidated = false;

    switch (NPDSCW.MessageFormat.EName)
    {
      case DdeMessageFormat.XML:
        // generate XML message from item list
        var rrMessage = new NpdsResrepXmlRoot();
        NPDSCW.NexusRecords = NPDSCW.NexusDR.CreateResrepListXml(rrRecords);
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