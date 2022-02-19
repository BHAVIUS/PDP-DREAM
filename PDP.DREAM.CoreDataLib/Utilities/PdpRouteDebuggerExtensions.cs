// PdpRouteDebuggerExtensions.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace PDP.DREAM.CoreDataLib.Utilities;

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
