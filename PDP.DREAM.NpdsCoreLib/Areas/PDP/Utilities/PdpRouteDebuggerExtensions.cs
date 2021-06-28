using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class PdpRouteDebuggerExtensions
  {
    public static IApplicationBuilder UsePdpRouteDebuggerPage(this IApplicationBuilder app)
    {
      if (app == null) { throw new ArgumentNullException(nameof(app)); }
      app.UseMiddleware<PdpRouteDebuggerMiddleware>();
      return app;
    }

    public static IApplicationBuilder UsePdpRouteDebuggerPage(
      this IApplicationBuilder app, PdpRouteDebuggerOptions options)
    {
      if (app == null) { throw new ArgumentNullException(nameof(app)); }
      if (options == null) { throw new ArgumentNullException(nameof(options)); }
      app.UseMiddleware<PdpRouteDebuggerMiddleware>(Options.Create(options));
      return app;
    }

  }

}
