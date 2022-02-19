// ScribeRestLibController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeRestLib.Controllers;

[Area(PdpConst.PdpMvcArea), RequireHttps, AllowAnonymous]
public class ScribeRestLibController : ScribeDataLibControllerBase
{
  public ScribeRestLibController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    PRC = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.User,
      ClientInUserModeIsRequired = true,
      SessionValueIsRequired = true
    };
    ResetScribeRepository();
    var anonActionList = new string[] { nameof(Index), nameof(Help) };
    string actionName = oaeCntxt.ActionName();
    if (!anonActionList.Contains(actionName))
    {
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(ScribeDLC.PdpPathIdentRequired); }
    }
    base.OnActionExecuting(oaeCntxt);
  }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet, AllowAnonymous]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help() { return View(); }

  // both "agents" and "resreps" should be considered reserved keywords
  //    that cannot be used as ServiceTags in second segment of routes

  // "scribe/agents/" constrained route for iaguid selected individual agent
  [HttpGet, Authorize()]
  [PdpMvcRoute("scribe/agents/{iaguid:guid}", "PdpScribeAgentsByGuid", false)]
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
  [HttpGet, Authorize()]
  [PdpMvcRoute("scribe/resreps/{rrguid:guid}/{rrlistxnam?}/{rritemguid:guid?}", "PdpScribeResrepsByGuid", false)]
  public IActionResult ResrepsByGuid(Guid rrguid, string? rrlistxnam = "", Guid? rritemguid = null)
  {
    throw new NotImplementedException();
    // var msgScribe = ScribeRestApiMessage();
    //  return msgScribe;
  }

  // 4-segment route with entityType selected collection of resreps
  [HttpGet, Authorize()]
  [PdpMvcRoute("scribe/{serviceTag:NpdsPT}/{entityType:NpdsPT}/{infosetStatus:NpdsIS}", "PdpScribeResrepsByEntityType", false)]
  public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
  {
    var es = string.Empty;
    PRC.ParseUrlSegments(serviceType, serviceTag, es, es, entityType, infosetStatus);
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 3-segment route with entityTag selected collection of resreps
  [HttpGet, Authorize()]
  [PdpMvcRoute("scribe/{serviceTag:NpdsPT}/{entityTag:NpdsPT}/{entityVersion?}", "PdpScribeResrepsByEntityTag", false)]
  public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
  {
    PRC.ParseUrlSegments(serviceType, serviceTag, entityTag, entityVersion);
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  // 2-segment route with serviceTag selected collection of resreps
  [HttpGet, Authorize()]
  [PdpMvcRoute("scribe/{serviceTag:NpdsPT}", "PdpScribeResrepsByServiceTag", false)]
  public IActionResult ResrepsByServiceTag(string serviceType, string serviceTag)
  {
    PRC.ParseUrlSegments(serviceType, serviceTag);
    ResetScribeRepository();
    var msgScribe = ScribeRestApiMessage();
    return msgScribe;
  }

  protected IActionResult ScribeRestApiMessage(bool xsdValidate = false)
  {
    IActionResult response = null;

    var rrStems = PSDC.ListStorableResrepStemsWithFacets();
    var rrListXml = PSDC.CreateResrepListXml(rrStems);
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
        throw new NotSupportedException("MessageFormat invalid or not yet implemented in ScribeRestApiMessage");
    }

    return response;
  }

} // end class

// end file
