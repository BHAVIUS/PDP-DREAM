// Startup.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PDP.DREAM.NpdsCoreLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public class StartPdpSite
  {
    public StartPdpSite AppWebSite { get { return this; } }
    public IConfiguration AppConfiguration { get; private set; }
    public IWebHostEnvironment AppEnvironment { get; private set; }
    public IServiceCollection AppServices { get; private set; }
    public PdpSiteSettings PdpSiteSettings { get { return pdpSitSets; } }
    private PdpSiteSettings? pdpSitSets = null;
    public NpdsServiceDefaults NpdsServiceDefaults { get { return npdsSrvcDefs; } }
    private NpdsServiceDefaults? npdsSrvcDefs = null;

    public StartPdpSite(IConfiguration config, IWebHostEnvironment envir)
    {
      // ConfigManager in PDP.DREAM.NpdsRootLib.Utilities
      ConfigManager.Initialize(config);
      AppConfiguration = config; 
      AppEnvironment = envir;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      // add constants (after ConfigManager has been initialized)
      pdpSitSets = PdpSiteSettings.GetValues;
      npdsSrvcDefs = NpdsServiceDefaults.GetValues;

      // add database contexts
      services.AddDbContext<CoreDbsqlContext>(options =>
        options.UseSqlServer(npdsSrvcDefs.NpdsCoreDbconstr));
      services.AddDatabaseDeveloperPageExceptionFilter();

      // add cached settings 
      using (var dataCntxt = new CoreDbsqlContext(npdsSrvcDefs.NpdsCoreDbconstr))
      {
        dataCntxt.LoadNpdsServiceCache();
      }

      // configure IIS
      services.Configure<IISServerOptions>(options =>
      {
        options.AllowSynchronousIO = true;
      });
      services.AddHttpContextAccessor();

      // add utility services
      services.AddSingleton<ISmsSender, TwilioSmsender>();
      services.AddSingleton<IEmailSender, MailKitEmailer>();
      services.AddHttpClient<BingMapsService>();

      // add secure user identity authentication authorization
      if (pdpSitSets.AppUseSecureUiaa)
      {
        services.Configure<CookiePolicyOptions>(options =>
        {
          options.MinimumSameSitePolicy = SameSiteMode.Strict;
          options.HttpOnly = HttpOnlyPolicy.Always;
          options.Secure = CookieSecurePolicy.Always;
          options.CheckConsentNeeded = (context => true);
        });
        services.ConfigureApplicationCookie(options =>
        {
          options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          options.SlidingExpiration = true;
        });
        if (!AppEnvironment.IsDevelopment())
        {
          services.AddHttpsRedirection(options =>
          {
            options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
            options.HttpsPort = 443;
          });
          services.Configure<MvcOptions>(options =>
          {
            options.Filters.Add(new RequireHttpsAttribute());
          });
        }
      }

      // add routing for controllers with views
      services.AddRouting();
      services.AddControllersWithViews()
        .AddRazorRuntimeCompilation()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.PropertyNamingPolicy = null;
          options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });
      // services.AddRazorPages();

      // add Telerik Kendo UI
      services.AddKendo();

      AppServices = services;
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseHttpContext();
      app.UseStatusCodePages();
      if (AppEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        // app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler(PdpConst.PdpPathSiteError);
      }

      if (pdpSitSets.AppUseSecureUiaa)
      {
        if (!AppEnvironment.IsDevelopment()) { app.UseHsts(); }
        app.UseHttpsRedirection();
        app.UseCookiePolicy();
      }

      if (pdpSitSets.AppUseSiteHtmlFile)
      {
        app.UsePdpSiteDefaultFile(); // DefaultFile before StaticFiles
      }
      app.UseStaticFiles();    // wwwroot folder StaticFiles
      app.UsePdpSiteStaticFiles(pdpSitSets); // StaticFiles before Routing
      app.UseRouting(); // Routing before Authentication and Authorization

      if (pdpSitSets.AppUseDebugRouting) // ATTN: use requires setting flag in appsettings.json
      {
        app.UseWhen(
          context => (context.Request.Query.ContainsKey(PdpConst.PdpDebugRouteQueryKey)
          || context.Request.Query.ContainsKey(PdpConst.PdpHelpRouteQueryKey)
          || context.Request.Query.ContainsKey(PdpConst.PdpHelpRouteHackKey)),
         builder => builder.UsePdpRouteDebuggerPage());
      }

      if (pdpSitSets.AppUseSecureUiaa)
      {
        app.UseCors();  // after Routing, before Auth
        app.UseAuthentication();
        app.UseAuthorization();
      }

      Action<IEndpointRouteBuilder> GetRoutes;
      GetRoutes = (r =>
      {
        // first routes with constraints
        PdpEndpoints.RegisterPdpArea(r);
        // then routes without constraints
        PdpEndpoints.RegisterPdpWebApp(r, "PDP", "Site", "Info");
        // PdpEndpoints.RegisterPdpRazorBlazor(r, true, false, "/Error");
      });
      app.UseEndpoints(GetRoutes);

    } // method

  } // class

} // namespace
