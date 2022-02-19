// PdpEndpointsDefault.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Controllers;

public static partial class PdpEndpoints
{
  public const string TKGAC = "NexusJsonTkgr|AnonResreps|AgentResreps|AgentScribeTkgr|AuthorResreps|AuthorScribeTkgr|EditorResreps|EditorScribeTkgr|AdminResreps|AdminScribeTkgr";

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
      pattern: PdpConst.PdpMvcArea + "/{controller}/{action}/{serviceType}/{serviceTag}/{entityType?}",
      // do not use constraint on action
      constraints: new { area = PdpConst.PdpMvcArea, controller = TKGAC, serviceType = NpdsConst.RegexReadWriteServiceTypeToken }
    );
    routes.MapAreaControllerRoute(
      name: "PdpAreaGenericWithId",
      areaName: PdpConst.PdpMvcArea,
      pattern: PdpConst.PdpMvcArea + "/{controller}/{action}/{id}",
      defaults: pdpAreaDefault,
      constraints: pdpAreaConstraint
    );
    routes.MapAreaControllerRoute(
      name: "PdpAreaGenericWithoutId",
      areaName: PdpConst.PdpMvcArea,
      pattern: PdpConst.PdpMvcArea + "/{controller}/{action}",
      defaults: pdpAreaDefault,
      constraints: pdpAreaConstraint,
      dataTokens: new {
        help = "Area default route for NPDS area",
        examples = "'NPDS', 'NPDS/Site', 'NPDS/Site/Info'"
      }
    );
    routes.MapAreaControllerRoute(
      name: "PdpAreaCatchall",
      areaName: PdpConst.PdpMvcArea,
      pattern: PdpConst.PdpMvcArea + "/{*catchall}",
      defaults: new { area = PdpConst.PdpMvcArea, controller = PdpConst.PdpMvcAltController, action = PdpConst.PdpMvcAltAction },
      constraints: pdpAreaConstraint,
      dataTokens: new {
        help = string.Empty,
        examples = string.Empty
      }
    );
  }

  public static void RegisterPdpWebApp(IEndpointRouteBuilder routes, bool mapAttribRoutes = false,
    string defArea = "", string defController = "", string defAction = "")
  {
    // add routes by attribute if switched on for MVC actions and Razor pages
    if (mapAttribRoutes)
    {
      routes.MapControllers();
      routes.MapRazorPages();
    }

    // add routes by convention only if
    //   at least either area/controller non-empty or controller/action non-empty
    if (!string.IsNullOrEmpty(defArea) && !string.IsNullOrEmpty(defController))
    {
      defAction ??= string.Empty;
      routes.MapControllerRoute(
        name: "PdpWebAppDefault",
        pattern: "{area}/{controller}/{action}/{id?}",
        defaults: new { area = defArea, controller = defController, action = defAction }
      );
      routes.MapControllerRoute(
        name: "PdpWebAppCatchall",
        pattern: "{**path}",
        defaults: new { area = defArea, controller = defController, action = defAction }
      );
    }
    else if (!string.IsNullOrEmpty(defController) && !string.IsNullOrEmpty(defAction))
    {
      routes.MapControllerRoute(
        name: "PdpWebAppDefault",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = defController, action = defAction }
      );
      routes.MapControllerRoute(
        name: "PdpWebAppCatchall",
        pattern: "{**path}",
        defaults: new { controller = defController, action = defAction }
      );
    }
    else
    {
      defArea = PdpConst.PdpMvcArea;
      defController = PdpConst.PdpMvcController;
      defAction = PdpConst.PdpMvcAction;
      routes.MapControllerRoute(
        name: "PdpWebAppCatchall",
        pattern: "{**path}",
        defaults: new { area = defArea, controller = defController, action = defAction }
      );
    }

  }

  public static void RegisterPdpRazorBlazor(IEndpointRouteBuilder routes,
    bool razorPages = false, bool blazorHub = false, string fallbackPage = "")
  {
    if (razorPages) { routes.MapRazorPages(); }
    if (blazorHub) { routes.MapBlazorHub(); }
    if (!string.IsNullOrEmpty(fallbackPage)) { routes.MapFallbackToPage(fallbackPage); }

  } // method

} // class

