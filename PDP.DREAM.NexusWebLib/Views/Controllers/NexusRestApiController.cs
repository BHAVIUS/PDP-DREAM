// NexusRestApiController.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.NexusWebLib.Controllers;

[RequireHttps, AllowAnonymous]
public class NexusRestApiController : NexusDataRazorViewControllerBase
{
  public NexusRestApiController(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // do NOT call base.OnActionExecuting(oaeCntxt);
    QURC = new QebUserRestContext(oaeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Nexus,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      QebSessionValueIsRequired = false
    };
    ResetNexusRepository();
    // TODO: split this controller into two controllers 
    // one for anonymous and one for authorized
    //if (!QebRazorAnonList.Contains(oaeCntxt.ActionName()))
    //{
    //  var isVerified = CheckCoreUserSession();
    //  if (!isVerified) { oaeCntxt.Result = Redirect(DepQebIdentRequired); }
    //}
  }

  //
  // Index/Help/Examples first, then rest alphabetical
  //
  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "nexus/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize]
  [PdpRazorViewRoute("nexus/agents/{iaguid:guid}",
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
  [PdpRazorViewRoute("nexus/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}",
    $"Nexus{nameof(ResrepsByGuid)}", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgNexus = NexusRestApiMessage();
    //  return msgNexus;
  }

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusST}/{serviceTag:NpdsPT}/{entityType:NpdsPT}/{infosetStatus:NpdsIS}",
    $"Nexus{nameof(ResrepsByEntityType)}", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    var es = string.Empty;
    QURC.ParseNpdsUrlSegments(serviceType, serviceTag, es, es, entityType, infosetStatus);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusST}/{serviceTag:NpdsPT}/{entityTag:NpdsPT}/{entityVersion?}",
    $"Nexus{nameof(ResrepsByEntityTag)}", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    QURC.ParseNpdsUrlSegments(serviceType, serviceTag, entityTag, entityVersion);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, AllowAnonymous]
  [PdpRazorViewRoute("{serviceType:NexusST}/{serviceTag:NpdsPT}",
    $"Nexus{nameof(ResrepsByServiceTag)}", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    QURC.ParseNpdsUrlSegments(serviceType, serviceTag);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  protected IActionResult NexusRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;

    var rrStems = PNDC.ListStorableNexusStemsWithFacets();
    var rrListXml = PNDC.CreateResrepListXml(rrStems);
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
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in NexusRestApiMessage");
    }

    return response;
  }

} // end class

// end file