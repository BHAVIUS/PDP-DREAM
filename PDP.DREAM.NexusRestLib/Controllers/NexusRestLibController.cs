// NexusRestLibController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusRestLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class NexusRestLibController : NexusDataLibControllerBase
{
  public NexusRestLibController(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Nexus,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
    ResetNexusRepository();
  }

  [HttpGet]
  [PdpMvcRoute(NexusRLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(NexusRLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "nexus/agents/" constrained route for iaguid selected individual agent
  [HttpGet]
  [PdpMvcRoute("nexus/agents/{iaguid:guid}", "PdpNexusAgentsByGuid", false)]
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
  [HttpGet]
  [PdpMvcRoute("nexus/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}", "PdpNexusResrepsByGuid", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgNexus = NexusRestApiMessage();
    //  return msgNexus;
  }

  // 4-segment route with entityType selected collection of resreps
  [HttpGet]
  [PdpMvcRoute("nexus/{serviceTag:NpdsPT}/{entityType:NpdsPT}/{infosetStatus:NpdsIS}", "PdpNexusResrepsByEntityType", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    var es = string.Empty;
    PRC.ParseUrlSegments(serviceType, serviceTag, es, es, entityType, infosetStatus);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet]
  [PdpMvcRoute("nexus/{serviceTag:NpdsPT}/{entityTag:NpdsPT}/{entityVersion?}", "PdpNexusResrepsByEntityTag", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    PRC.ParseUrlSegments(serviceType, serviceTag, entityTag, entityVersion);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet]
  [PdpMvcRoute("nexus/{serviceTag:NpdsPT}", "PdpNexusResrepsByServiceTag", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    PRC.ParseUrlSegments(serviceType, serviceTag);
    ResetNexusRepository();
    var msgNexus = NexusRestApiMessage();
    return msgNexus;
  }

  protected IActionResult NexusRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;

    var rrStems = PNDC.ListStorableResrepStemsWithFacets();
    var rrListXml = PNDC.CreateResrepListXml(rrStems);
    // PRC.ResponseAnswer = rrListXml; // alternative format response
    PRC.NexusRecords = rrListXml;

    var messageValidated = false;

    switch (PRC.MessageFormat)
    {
      case NpdsConst.MessageFormat.XML:
        var rrMessage = new NpdsResrepXmlRoot();
        // xsdValidate enables local override independent of PRC.CheckFormat value
        if (PRC.CheckFormat || xsdValidate)
        {
          var pxsWriter = new PdpPrcXmlStringWriter<NpdsResrepXmlRoot>(rrMessage, PRC);
          var xmlMessage = pxsWriter.XML;
          var pxsValidater = new PdpPrcXmlValidater(PRC);
          messageValidated = pxsValidater.ValidateNpdsXmlMessage(xmlMessage);
        }
        response = new PdpPrcXmlResponseWriter<NpdsResrepXmlRoot>(rrMessage, PRC);
        break;
      case NpdsConst.MessageFormat.JSON:
      // TODO: code analogous JSON writer
      //     or else develop single serializing text writer
      //     that can handle both XML and JSON
      //     that would provide complete control 
      // response = new PdpXmlResponseWriter<NpdsResrepJsonRoot>(records, PRC, XWS);
      case NpdsConst.MessageFormat.XHTML:
      default:
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in NexusRestApiMessage");
    }

    return response;
  }

} // end class

// end file