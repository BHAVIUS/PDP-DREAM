// PdpEndpointsDefault.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  public static partial class PdpEndpoints
  {
    public const string TKGAC = "NexusJsonTkgr|AnonResreps|AgentResreps|AgentScribeTkgr|AuthorResreps|AuthorScribeTkgr|EditorResreps|EditorScribeTkgr|AdminResreps|AdminScribeTkgr";

    // public static void RegisterPdpArea(IEndpointRouteBuilder routes, string namArea, string namController, string namAction, bool setAreaDefaults = false)
    public static void RegisterPdpArea(IEndpointRouteBuilder routes, bool setAreaDefaults = true,
      string namController = PdpConst.PdpMvcController, string namAction = PdpConst.PdpMvcAction)
    {
      object? pdpAreaDefault = null;
      object? pdpAreaConstraint = null;
      if (setAreaDefaults)
      {
        pdpAreaDefault = new { area = PdpConst.PdpMvcArea, controller = namController, action = namAction };
        pdpAreaConstraint = new { area = PdpConst.PdpMvcArea }; // do not use constraints on action or controller
      }

      routes.MapAreaControllerRoute(
        name: "PdpAreaNpdsResreps",
        areaName: PdpConst.PdpMvcArea,
        pattern: "{area:exists}/{controller}/{action}/{serviceType}/{serviceTag}/{entityType?}",
        // do not use constraint on action
        constraints: new { area = PdpConst.PdpMvcArea, controller = TKGAC, serviceType = NpdsConst.RegexReadWriteServiceTypeToken }
      );
      routes.MapAreaControllerRoute(
        name: "PdpAreaGenericWithId",
        areaName: PdpConst.PdpMvcArea,
        pattern: "{area:exists}/{controller}/{action}/{id}",
        defaults: pdpAreaDefault,
        constraints: pdpAreaConstraint
      );
      routes.MapAreaControllerRoute(
        name: "PdpAreaGenericWithoutId",
        areaName: PdpConst.PdpMvcArea,
        pattern: "{area:exists}/{controller}/{action}",
        defaults: pdpAreaDefault,
        constraints: pdpAreaConstraint,
        dataTokens: new
        {
          help = "Area default route for PDP area",
          examples = "'PDP', 'PDP/Site', 'PDP/Site/Info'"
        }
      );
      routes.MapAreaControllerRoute(
        name: "PdpAreaCatchall",
        areaName: PdpConst.PdpMvcArea,
        pattern: "{area:exists}/{*catchall}",
        defaults: new { area = PdpConst.PdpMvcArea, controller = PdpConst.PdpMvcAltController, action = PdpConst.PdpMvcAltAction },
        constraints: pdpAreaConstraint,
        dataTokens: new
        {
          help = string.Empty,
          examples = string.Empty
        }
      );
      routes.MapFallbackToAreaController("{*:nonfile}",
        PdpConst.PdpMvcAction, PdpConst.PdpMvcController, PdpConst.PdpMvcArea);
    }

    public static void RegisterPdpWebApp(IEndpointRouteBuilder routes, string defArea = "",
      string defController = "", string defAction = "", bool mapAttribRoutes = false)
    {
      defArea ??= string.Empty;
      defController ??= string.Empty;
      defAction ??= string.Empty;
      routes.MapControllerRoute(
        name: "PdpWebAppDefault",
        pattern: "{area}/{controller}/{action}/{id?}",
        defaults: new { area = defArea, controller = defController, action = defAction }
        );
      if (mapAttribRoutes) { routes.MapControllers(); }
    }

    public static void RegisterPdpRazorBlazor(IEndpointRouteBuilder routes,
      bool razorPages = false, bool blazorHub = false, string fallbackPage = "")
    {
      if (razorPages) { routes.MapRazorPages(); }
      if (blazorHub) { routes.MapBlazorHub(); }
      if (!string.IsNullOrEmpty(fallbackPage)) { routes.MapFallbackToPage(fallbackPage); }

    } // method

  } // class

} // namespace
