using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

#nullable disable

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  // TODO: consolidate these extensions to eliminate repeated code

  public static class GetRoutesMiddlewareExtensions
  {
    public static IApplicationBuilder UseGetRoutesMiddleware(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
    {
      if (app == null) { throw new ArgumentNullException(nameof(app)); }
      if (configureRoutes == null) throw new ArgumentNullException(nameof(configureRoutes));
      var mrh = new RouteHandler(context =>
      {
        // var routeValues = context.GetRouteData().Values;
        // return context.Response.WriteAsync($"Route values: {string.Join(",", routeValues)}");
        return context.Response.WriteAsync(string.Empty);
      });
      var mrb = new RouteBuilder(app, mrh);
      configureRoutes(mrb);
      var router = mrb.Build();
      return app.UseMiddleware<GetRoutesMiddleware>(router);
    }

    public static IRouter BuildMvcRouter(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
    {
      if (app == null) throw new ArgumentNullException(nameof(app));
      if (configureRoutes == null) throw new ArgumentNullException(nameof(configureRoutes));
      var mrh = new RouteHandler(context =>
      {
        // var routeValues = context.GetRouteData().Values;
        // return context.Response.WriteAsync($"Route values: {string.Join(",", routeValues)}");
        return context.Response.WriteAsync(string.Empty);
      });
      var mrb = new RouteBuilder(app, mrh);
      configureRoutes(mrb);
      return mrb.Build();
    }

    public static IRouteBuilder PdpSetMvcRouteBuilder(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
    {
      if (app == null) throw new ArgumentNullException(nameof(app));
      if (configureRoutes == null) throw new ArgumentNullException(nameof(configureRoutes));
      var mrh = new RouteHandler(context =>
      {
        // var routeValues = context.GetRouteData().Values;
        // return context.Response.WriteAsync($"Route values: {string.Join(",", routeValues)}");
        return context.Response.WriteAsync(string.Empty);
      });
      var mrb = new RouteBuilder(app, mrh);
      configureRoutes(mrb);
      return mrb;
    }

    public static RequestDelegate SetRouteData(this IApplicationBuilder app, RequestDelegate next, IRouter router)
    {
      return async hContext =>
      {
        var rContext = new RouteContext(hContext);
        await router.RouteAsync(rContext);

        if (rContext.Handler != null)
        {
          hContext.Features[typeof(IRoutingFeature)] = new RoutingFeature
          {
            RouteData = rContext.RouteData
          };
        }

        await next(hContext);
      };
    }

  }

  public class GetRoutesMiddleware
  {
    private readonly RequestDelegate next;
    private readonly IRouter _router;

    public GetRoutesMiddleware(RequestDelegate next, IRouter router)
    {
      this.next = next;
      _router = router;
    }

    public async Task Invoke(HttpContext hContext)
    {
      var rContext = new RouteContext(hContext);
      rContext.RouteData.Routers.Add(_router);

      await _router.RouteAsync(rContext);

      if (rContext.Handler != null)
      {
        hContext.Features[typeof(IRoutingFeature)] = new RoutingFeature()
        {
          RouteData = rContext.RouteData,
        };
      }

      // proceed to next...
      await next(hContext);
    }
  }

  public class RoutingFeature : IRoutingFeature
  {
    public RouteData RouteData { get; set; }
  }

}
