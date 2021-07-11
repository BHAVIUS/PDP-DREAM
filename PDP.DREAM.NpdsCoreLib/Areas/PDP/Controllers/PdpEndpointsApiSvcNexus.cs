// PdpEndpointsApiSvcNexus.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  public static partial class PdpEndpoints
  {
    // for use with anonymous (or optionally authenticated) NexusRest controller

    public static void RegisterForNexusReadOnlySvc(IEndpointRouteBuilder routes, string apiController = "NexusRestApi")
    {
      // both "agents" and "resreps" should be considered reserved keywords
      //    that cannot be used as ServiceTags in second segment of routes

      // "nexus/agents/" constrained route for iaguid selected individual agent
      var routeName = "NexusAgentsByGuid";
      routes.MapControllerRoute(
        name: routeName,
        pattern: "nexus/agents/{iaguid}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "AgentsByGuid" },
        constraints: new
        {
          iaguid = NpdsConst.RegexGuid
        }
      ).WithDisplayName(routeName);
      // next line adds an extra SuppressLinkGeneration metadata item, does NOT default to false
     // .WithMetadata(new SuppressLinkGenerationMetadata());

      // "nexus/resreps/" constrained route for rrguid selected individual resrep
      //    and optional selected subcollection rrlistxnam and subitem rritemguid
      routeName = "NexusResrepsByGuid";
      routes.MapControllerRoute(
        name: routeName,
        pattern: "nexus/resreps/{rrguid}/{rrlistxnam?}/{rritemguid?}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByGuid" },
        constraints: new
        {
          rrguid = NpdsConst.RegexGuid,
          rrlistxnam = NpdsConst.RegexResRepListXnams,
          rritemguid = NpdsConst.RegexGuid
        }
      ).WithDisplayName(routeName);
      // .WithMetadata(new SuppressLinkGenerationMetadata());

      // 4-segment route with entityType selected collection of resreps
      routeName = "NexusResrepsByEntityType";
      var endpointName = new EndpointNameMetadata( "NexusResrepsByEntityTypeEndpoint");
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}/{entityType}/{infosetStatus}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByEntityType" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadOnlyServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken,
          entityType = NpdsConst.RegexRequiredTagToken,
          infosetStatus = NpdsConst.RegexInfosetStatusToken
        },
        dataTokens: new
        {
          help = "4-segment route '{serviceType}/{serviceTag}/{entityType}/{infosetStatus}' for NPDS resource representation metadata records by entityType and infosetStatus with required serviceType and serviceTag",
          examples = "'/Nexus/GeneScene/Organization/AnyAndAll', '/PORTAL/NLMMeSH/ThesaurusItem/AnyAndAll', 'DOORS/GTG-DOORS/Person/AnyAndAll'"
        }
      ).WithDisplayName(routeName).WithMetadata(endpointName);
      // .WithMetadata(new SuppressLinkGenerationMetadata());

      // 3-segment route with entityTag selected collection of resreps
      routeName = "NexusResrepsByEntityTag";
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}/{entityTag}/{entityVersion?}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByEntityTag" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadOnlyServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken,
          entityTag = NpdsConst.RegexRequiredTagToken
        },
        dataTokens: new
        {
          help = "3- or 4-segment route '{serviceType}/{serviceTag}/{entityTag}/{entityVersion?}' for NPDS resource representation metadata records by entityTag with required serviceType and serviceTag",
          examples = "'/Nexus/Beacon/ELIDA', '/Nexus/BrainWatch/ABA', '/Nexus/CTGaming/WATTGaming', '/Nexus/Eywa/IBC-ET', '/Nexus/Gaia/TNC', '/Nexus/GeneScene/OMIM', '/Nexus/HELPME/AHRQ', '/Nexus/ManRay/ABNM', '/Nexus/Osler/AHIC'"
        }
      ).WithDisplayName(routeName);
      // .WithMetadata(new SuppressLinkGenerationMetadata());

      // 2-segment route with serviceTag selected collection of resreps
      routeName = "NexusResrepsByServiceTag";
      routes.MapControllerRoute(
        name: routeName,
        pattern: "{serviceType}/{serviceTag}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = apiController, action = "ResrepsByQuerystr" },
        constraints: new
        {
          serviceType = NpdsConst.RegexReadOnlyServiceTypeToken,
          serviceTag = NpdsConst.RegexRequiredTagToken
        },
        dataTokens: new
        {
          help = "2-segment route '{serviceType}/{serviceTag}' for NPDS resource representation metadata records by optional querystr with required serviceType and serviceTag",
          examples = "'/Nexus/Beacon?etag=ELIDA', '/PORTAL/MeSH2015?enam=calcimycin', '/DOORS/BHA-DOORS?enat=American', '/Nexus/DaVinci?enam=semantic'"
        }
      ).WithDisplayName(routeName);
      // .WithMetadata(new SuppressLinkGenerationMetadata());
    }

  }

}
