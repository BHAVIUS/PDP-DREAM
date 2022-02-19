// PdpSiteMiddlewareExtensions.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpSiteMiddlewareExtensions
{
  public static IApplicationBuilder UsePdpSite404Redirect(this IApplicationBuilder app)
  {
    app.Use(async (context, next) => {
      await next();
      if (context.Response.StatusCode == 404)
      {
        context.Request.Path = CoreDLC.PdpPathSiteRoutes;
        await next();
      }
    });
    return app;
  }

  public static IApplicationBuilder UsePdpSiteDefaultFile(this IApplicationBuilder app)
  {
    var opt = new DefaultFilesOptions();
    opt.DefaultFileNames.Clear();
    opt.DefaultFileNames.Add("PdpSiteDefault.html");
    app.UseDefaultFiles(opt);
    return app;
  }

  public static IApplicationBuilder UsePdpSiteStaticFiles(this IApplicationBuilder app, PdpSiteSettings sets)
  {
    if (!string.IsNullOrEmpty(sets.AppRqstpathExtdeplib) && !string.IsNullOrEmpty(sets.AppFilepathExtdeplib))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathExtdeplib),
        RequestPath = sets.AppRqstpathExtdeplib
      });
    }
    if (!string.IsNullOrEmpty(sets.AppRqstpathSecuredocs) && !string.IsNullOrEmpty(sets.AppFilepathSecuredocs))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathSecuredocs),
        RequestPath = sets.AppRqstpathSecuredocs
      });
    }
    if (!string.IsNullOrEmpty(sets.AppRqstpathPublicdocs) && !string.IsNullOrEmpty(sets.AppFilepathPublicdocs))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathPublicdocs),
        RequestPath = sets.AppRqstpathPublicdocs
      });
    }
    if (!string.IsNullOrEmpty(sets.AppRqstpathWebimages) && !string.IsNullOrEmpty(sets.AppFilepathWebimages))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathWebimages),
        RequestPath = sets.AppRqstpathWebimages
      });
    }
    if (!string.IsNullOrEmpty(sets.AppRqstpathTestdata) && !string.IsNullOrEmpty(sets.AppFilepathTestdata))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathTestdata),
        RequestPath = sets.AppRqstpathTestdata
      });
    }
    if (!string.IsNullOrEmpty(sets.AppRqstpathFileprov) && !string.IsNullOrEmpty(sets.AppFilepathFileprov))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathFileprov),
        RequestPath = sets.AppRqstpathFileprov
      });
    }
    return app;

  } // method

  public static IServiceCollection AddPdpSiteDbContext<TDbcontxt>
    (this IServiceCollection services, string dbconstr) where TDbcontxt : DbContext
  {
    services.AddDbContext<TDbcontxt>(options => options.UseSqlServer(dbconstr));
    return services;
  }

} // end class

// end file