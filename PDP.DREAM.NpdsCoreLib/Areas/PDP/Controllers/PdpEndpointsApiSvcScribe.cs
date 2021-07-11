// PdpEndpointsApiSvcScribe.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  public static partial class PdpEndpoints
  {
    // for use with non-anonymous and required authenticated ScribeRest controller

    public static void RegisterForScribeReadWriteSvc(IEndpointRouteBuilder routes, string apiController = "ScribeRestApi")
    {
      // both "agents" and "resreps" should be considered reserved keywords
      //    that cannot be used as ServiceTags in second segment of routes

      // "scribe/agents/" constrained route for iaguid selected individual agent
      routes.MapControllerRoute(
        name: "ScribeAgentsByGuid",
        pattern: "scribe/agents/{iaguid}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "AgentsByGuid" },
        constraints: new
        {
          aguid = NpdsConst.RegexGuid
        }
      );

      // "scribe/resreps/" constrained route for rrguid selected individual resrep
      //    and optional selected subcollection rrlistxnam and subitem rritemguid
      routes.MapControllerRoute(
        name: "ScribeResrepsByGuid",
        pattern: "scribe/resreps/{rrguid}/{rrlistxnam?}/{rritemguid?}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByGuid" },
        constraints: new
        {
          rrguid = NpdsConst.RegexGuid,
          rrlistxnam = NpdsConst.RegexResRepListXnams,
          rritemguid = NpdsConst.RegexGuid
        }
      );

      // 4-segment route with entityType selected collection of resreps
      routes.MapControllerRoute(
        name: "ScribeResrepsByEntityType",
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
      );

      // 3-segment route with entityTag selected collection of resreps
      routes.MapControllerRoute(
        name: "ScribeResrepsByEntityTag",
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
      );

      // 2-segment route with serviceTag selected collection of resreps
      routes.MapControllerRoute(
        name: "ScribeResrepsByServiceTag",
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
      );

    }

  }

}