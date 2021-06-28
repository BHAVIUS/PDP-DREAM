using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  public static partial class PdpEndpoints
  {
    public const string TKGAC = "AnonResreps|AnonNexus|AnonScribeTkgr|AgentResreps|AgentScribeTkgr|AuthorResreps|AuthorScribeTkgr|EditorResreps|EditorScribeTkgr|AdminResreps|AdminScribeTkgr";

    // public static void RegisterPdpArea(IEndpointRouteBuilder routes, string namArea, string namController, string namAction, bool setAreaDefaults = false)
    public static void RegisterPdpArea(IEndpointRouteBuilder routes, bool setAreaDefaults = false,
      string namController = PdpConst.PdpMvcController, string namAction = PdpConst.PdpMvcAction)
    {
      object? pdpAreaDefault = null;
      object? pdpAreaCatchall = null;
      object? pdpAreaConstraint = null;
      if (setAreaDefaults)
      {
        pdpAreaDefault = new { controller = namController, action = namAction };
        pdpAreaCatchall = new { controller = namController, action = PdpConst.PdpMvcAltAction };
        pdpAreaConstraint = new { area = PdpConst.PdpMvcArea };
      }

      routes.MapAreaControllerRoute(
        name: "PdpAreaNpdsResreps",
        areaName: PdpConst.PdpMvcArea,
        pattern: "PDP/{controller}/{action}/{serviceType}/{serviceTag}/{entityType?}",
        // do not use default on area
        defaults: new { controller = "AnonResreps", action = "NpdsView", serviceType = "Nexus", serviceTag = "PDP-Nexus" },
        // do not use constraint on action
        constraints: new { area = PdpConst.PdpMvcArea, controller = TKGAC, serviceType = NpdsConst.RegexReadWriteServiceTypeToken }
      );
      routes.MapAreaControllerRoute(
        name: "PdpAreaDefaultWithId",
        areaName: PdpConst.PdpMvcArea,
        pattern: "PDP/{controller}/{action}/{id}",
        // do not use default on area
        defaults: pdpAreaDefault,
        // do not use constraints on action or controller
        constraints: pdpAreaConstraint
      );

      routes.MapAreaControllerRoute(
        name: "PdpAreaDefault",
        areaName: PdpConst.PdpMvcArea,
        pattern: "PDP/{controller}/{action}",
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
        pattern: "PDP/{*catchall}",
        defaults: pdpAreaCatchall,
        constraints: pdpAreaConstraint,
        dataTokens: new
        {
          help = string.Empty,
          examples = string.Empty
        }
      );
    }

    public static void RegisterPdpWebApp(IEndpointRouteBuilder routes,
     string defArea = "", string defController = "", string defAction = "", bool mapAttribRoutes = false)
    {
      if (string.IsNullOrEmpty(defArea))
      {
        routes.MapControllerRoute(
          name: "PdpWebAppDefault",
          pattern: "{controller}/{action}/{id?}",
          defaults: new { controller = defController, action = defAction }
          );
      }
      else
      {
        routes.MapControllerRoute(
          name: "PdpWebAppDefault",
          pattern: "{area}/{controller}/{action}/{id?}",
          defaults: new { area = defArea, controller = defController, action = defAction }
          );
      }
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
