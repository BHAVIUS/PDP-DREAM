// HttpContextExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HttpContextExtensions
  {
    public static Endpoint GetCurrentEndpoint(this HttpContext httpContext)
    {
      if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }
      Endpoint ep = httpContext.GetEndpoint();
      return ep;
    }

    public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
    {
      PdpWebAppHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
      return app;
    }
  }

  public static class PdpWebAppHttpContext
  {
    // https://stackoverflow.com/questions/43526630/how-can-i-get-the-baseurl-of-my-site-in-asp-net-core
    // https://stackoverflow.com/questions/1288046/how-can-i-get-my-webapps-base-url-in-asp-net-mvc
    public static string BaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";
    public static HttpContext Current => httpCtxtAccessor.HttpContext;

    internal static void Configure(IHttpContextAccessor accessor)
    {
      httpCtxtAccessor = accessor;
    }
    private static IHttpContextAccessor httpCtxtAccessor;
  }

}
