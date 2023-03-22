// PdpSiteMiddlewareExtensions.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpSiteMiddlewareExtensions
{
  public static IApplicationBuilder UsePdpSite404Redirect(this IApplicationBuilder app)
  {
    app.Use(async (context, next) => {
      if (context.Response.StatusCode == 404)
      {
        context.Request.Path = DepPdpSiteRoutes;
      }
      await next();
    });
    return app;
  }

  public static IApplicationBuilder UsePdpSiteDefaultPath(this IApplicationBuilder app)
  {
    app.Use(async (context, next) => {
      if (context.Request.Path.Value == "/")
      {
        context.Request.Path = PDPSS.AppSiteDefPath;
      }
      await next();
    });
    return app;
  }
  public static IApplicationBuilder UsePdpSiteDefaultFile(this IApplicationBuilder app)
  {
    var opt = new DefaultFilesOptions();
    opt.DefaultFileNames.Clear();
    opt.DefaultFileNames.Add(PdpSiteDefaultHtml);
    app.UseDefaultFiles(opt);
    return app;
  }
  public static IApplicationBuilder UsePdpSiteStaticFiles(this IApplicationBuilder app, bool useDefaultFile = false)
  {
    if (useDefaultFile) { app.UsePdpSiteDefaultFile(); }
    // app.UseDefaultFiles should precede app.UseStaticFiles
    app.UseStaticFiles(); // for directory configured with WebRootPath in WebApplicationBuilder
    if (!string.IsNullOrEmpty(PDPSS.AppRqstpathExtdeplib) && !string.IsNullOrEmpty(PDPSS.AppFilepathExtdeplib))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(PDPSS.AppFilepathExtdeplib),
        RequestPath = PDPSS.AppRqstpathExtdeplib,
      });
    }
    if (!string.IsNullOrEmpty(PDPSS.AppRqstpathSecuredocs) && !string.IsNullOrEmpty(PDPSS.AppFilepathSecuredocs))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(PDPSS.AppFilepathSecuredocs),
        RequestPath = PDPSS.AppRqstpathSecuredocs,
      });
    }
    if (!string.IsNullOrEmpty(PDPSS.AppRqstpathPublicdocs) && !string.IsNullOrEmpty(PDPSS.AppFilepathPublicdocs))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(PDPSS.AppFilepathPublicdocs),
        RequestPath = PDPSS.AppRqstpathPublicdocs,
      });
    }
    if (!string.IsNullOrEmpty(PDPSS.AppRqstpathFileprov) && !string.IsNullOrEmpty(PDPSS.AppFilepathFileprov))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(PDPSS.AppFilepathFileprov),
        RequestPath = PDPSS.AppRqstpathFileprov,
      });
    }
    return app;
  }

  public static IServiceCollection AddPdpSiteDbContext<TDbcontxt>
    (this IServiceCollection services, string dbconstr) where TDbcontxt : DbContext
  {
    services.AddDbContext<TDbcontxt>(options => options.UseSqlServer(dbconstr));
    return services;
  }

} // end class

// end file