using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSAUTHOR)]
  public class ScribeRestApiController : SiaaDataControllerBase
  {
    public ScribeRestApiController(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

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
      var isVerified = CheckClientAgentSession();
      if (!isVerified) { oaeCntxt.Result = Redirect(PdpConst.PdpPathIdentRequired); }
    }

    // routes for this ScribeRestApiController found in
    // PDP.DREAM.NpdsCoreLib.Controllers.PdpEndpoints.RegisterForScribeReadWriteSvc

    public IActionResult ResrepsByEntityType(string serviceType, string serviceTag, string entityType, string infosetStatus)
    {
      var es = string.Empty;
      PRC.ParseUrlSegments(serviceType, serviceTag, es, es, entityType, infosetStatus);
      ResetScribeRepository();
      var msgScribe = ScribeApiMessage();
      return msgScribe;
    }

    public IActionResult ResrepsByEntityTag(string serviceType, string serviceTag, string entityTag, string entityVersion = "")
    {
      PRC.ParseUrlSegments(serviceType, serviceTag, entityTag, entityVersion);
      ResetScribeRepository();
      var msgScribe = ScribeApiMessage();
      return msgScribe;
    }

    public IActionResult ResrepsByQuerystr(string serviceType, string serviceTag)
    {
      PRC.ParseUrlSegments(serviceType, serviceTag);
      ResetScribeRepository();
      var msgScribe = ScribeApiMessage();
      return msgScribe;
    }

    public IActionResult ScribeApiMessage(bool xsdValidate = false)
    {
      IActionResult response = null;

      var rrStems = PSDC.ListStorableResrepStemsWithFacets();
      var rrListXml = PSDC.CreateResrepListXml(rrStems);
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
