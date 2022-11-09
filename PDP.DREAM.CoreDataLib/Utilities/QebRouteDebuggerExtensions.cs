// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class QebRouteDebuggerExtensions
{
  public static IApplicationBuilder UseQebRouteDebuggerPage(this IApplicationBuilder app)
  {
    if (app == null) { throw new ArgumentNullException(nameof(app)); }
    app.UseMiddleware<QebRouteDebuggerMiddleware>();
    return app;
  }

  public static IApplicationBuilder UseQebRouteDebuggerPage(
    this IApplicationBuilder app, QebRouteDebuggerOptions options)
  {
    if (app == null) { throw new ArgumentNullException(nameof(app)); }
    if (options == null) { throw new ArgumentNullException(nameof(options)); }
    app.UseMiddleware<QebRouteDebuggerMiddleware>(Options.Create(options));
    return app;
  }

}
