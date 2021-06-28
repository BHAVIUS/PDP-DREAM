using System;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using PDP.DREAM.NpdsCoreLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class PdpSiteMiddlewareExtensions
  {
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
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathExtdeplib),
        RequestPath = "/lib"
      });
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathSecuredocs),
        RequestPath = "/sec"
      });
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathPublicdocs),
        RequestPath = "/pub"
      });
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathWebimages),
        RequestPath = "/pub/img"
      });
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(sets.AppFilepathTestdata),
        RequestPath = "/pub/dat"
      });
      return app;

    } // method

  } // class

} // namespace
