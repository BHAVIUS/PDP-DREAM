// PdpSiteMiddlewareExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.NpdsCoreLib.Models;

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
