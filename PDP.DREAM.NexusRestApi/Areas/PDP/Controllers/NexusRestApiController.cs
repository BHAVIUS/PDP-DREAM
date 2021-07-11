// NexusRestApiController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NexusRestApiController : NexusDataControllerBase
  {
    public NexusRestApiController(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

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

    // routes for this NexusRestApiController found in
    // PDP.DREAM.NpdsCoreLib.Controllers.PdpEndpoints.RegisterForNexusReadOnlySvc

    public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
    {
      var es = string.Empty;
      PRC.ParseUrlSegments(serviceType, serviceTag, es, es, entityType, infosetStatus);
      ResetNexusRepository();
      var msgNexus = NexusRestApiMessage();
      return msgNexus;
    }

    public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
    {
      PRC.ParseUrlSegments(serviceType, serviceTag, entityTag, entityVersion);
      ResetNexusRepository();
      var msgNexus = NexusRestApiMessage();
      return msgNexus;
    }

    public IActionResult ResrepsByQuerystr(string serviceType, string serviceTag)
    {
      PRC.ParseUrlSegments(serviceType, serviceTag);
      ResetNexusRepository();
      var msgNexus = NexusRestApiMessage();
      return msgNexus;
    }

    public IActionResult NexusRestApiMessage(bool xsdValidate = false)
    {
      IActionResult response = null;

      var rrStems = PNDC.ListStorableResrepStemsWithFacets();
      var rrListXml = PNDC.CreateResrepListXml(rrStems);
      // PRC.ResponseAnswer = rrListXml; // alternative format response
      PRC.NexusRecords = rrListXml; // for test demo

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

  } // class

} // namespace
