// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class QebHttpContextExtensions
{
  public static IApplicationBuilder QebUseHttpContext(this IApplicationBuilder app)
  {
    QebHttpContextAccessor.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
    return app;
  }

  public static Endpoint QebGetHttpEndpoint(this HttpContext httpContext)
  {
    if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }
    Endpoint ep = httpContext.GetEndpoint();
    return ep;
  }

}

// TODO: recode QebHttpContextAccessor to inherit from IHttpContextAccessor
// use DI and a factory pattern to support a call like
// pdwServices.AddScoped<IHttpContextAccessor, QebHttpContextAccessor>();

public class QebHttpContextAccessor
{
  // https://stackoverflow.com/questions/43526630/how-can-i-get-the-baseurl-of-my-site-in-asp-net-core
  // https://stackoverflow.com/questions/1288046/how-can-i-get-my-webapps-base-url-in-asp-net-mvc
  public static string BaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";
  public static HttpContext Current => httpCtxtAccessor.HttpContext;

  private static IHttpContextAccessor httpCtxtAccessor;
  internal static void Configure(IHttpContextAccessor accessor)
  {
    httpCtxtAccessor = accessor;
  }

}
