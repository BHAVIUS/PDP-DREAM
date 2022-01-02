// PdpEndpointsApiSvcScribe.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Controllers
{
  public static partial class PdpEndpoints
  {
    // for use with non-anonymous and required authenticated ScribeRest controller

    public static void RegisterForScribeReadWriteSvc(IEndpointRouteBuilder routes, string apiController = "ScribeRestApi")
    {
      var routeName = string.Empty;
      var endpointName = new EndpointNameMetadata(routeName);

      // both "agents" and "resreps" should be considered reserved keywords
      //    that cannot be used as ServiceTags in second segment of routes

      // "scribe/agents/" constrained route for iaguid selected individual agent
      routeName = "ScribeAgentsByGuid";
      endpointName = new EndpointNameMetadata(routeName);
      routes.MapControllerRoute(
        name: routeName,
        pattern: "scribe/agents/{iaguid}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "AgentsByGuid" },
        constraints: new
        {
          iaguid = NpdsConst.RegexGuid
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);

      // "scribe/resreps/" constrained route for rrguid selected individual resrep
      //    and optional selected subcollection rrlistxnam and subitem rritemguid
      routeName = "ScribeResrepsByGuid";
      endpointName = new EndpointNameMetadata(routeName);
      routes.MapControllerRoute(
        name: routeName,
        pattern: "scribe/resreps/{rrguid}/{rrlistxnam?}/{rritemguid?}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByGuid" },
        constraints: new
        {
          rrguid = NpdsConst.RegexGuid,
          rrlistxnam = NpdsConst.RegexResRepListXnams,
          rritemguid = NpdsConst.RegexGuid
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);

      // 4-segment route with entityType selected collection of resreps
      routeName = "ScribeResrepsByEntityType";
      endpointName = new EndpointNameMetadata(routeName);
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}/{entityType}/{infosetStatus}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByEntityType" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadWriteServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken,
          entityType = NpdsConst.RegexRequiredTagToken,
          infosetStatus = NpdsConst.RegexInfosetStatusToken
        },
        dataTokens: new
        {
          help = "4-segment route '{serviceType}/{serviceTag}/{entityType}/{infosetStatus}' for NPDS resource representation metadata records by entityType and infosetStatus with required serviceType and serviceTag",
          examples = "'/Nexus/GeneScene/Organization/AnyAndAll', '/PORTAL/NLMMeSH/ThesaurusItem/AnyAndAll', 'DOORS/GTG-DOORS/Person/Any'"
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);

      // 3-segment route with entityTag selected collection of resreps
      routeName = "ScribeResrepsByEntityTag";
      endpointName = new EndpointNameMetadata(routeName);
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}/{entityTag}/{entityVersion?}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByEntityTag" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadWriteServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken,
          entityTag = NpdsConst.RegexRequiredTagToken
        },
        dataTokens: new
        {
          help = "3- or 4-segment route '{serviceType}/{serviceTag}/{entityTag}/{entityVersion?}' for NPDS resource representation metadata records by entityTag with required serviceType and serviceTag",
          examples = "'/Nexus/Beacon/ELIDA', '/Nexus/BrainWatch/ABA', '/Nexus/CTGaming/WATTGaming', '/Nexus/Eywa/IBC-ET', '/Nexus/Gaia/TNC', '/Nexus/GeneScene/OMIM', '/Nexus/HELPME/AHRQ', '/Nexus/ManRay/ABNM', '/Nexus/Osler/AHIC'"
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);

      // 2-segment route with serviceTag selected collection of resreps
      routeName = "ScribeResrepsByServiceTag";
      endpointName = new EndpointNameMetadata(routeName);
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByQuerystr" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadWriteServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken
        },
        dataTokens: new
        {
          help = "2-segment route '{serviceType}/{serviceTag}' for NPDS resource representation metadata records by optional querystr with required serviceType and serviceTag",
          examples = "'/Nexus/Beacon?etag=ELIDA', '/PORTAL/MeSH2015?enam=calcimycin', '/DOORS/BHA-DOORS?enat=American', '/Nexus/DaVinci?enam=semantic'"
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);

    }

  }

}