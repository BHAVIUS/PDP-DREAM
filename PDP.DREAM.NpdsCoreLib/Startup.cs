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

namespace PDP.DREAM.NpdsCoreLib
{
  public class Startup
  {
    private PdpSiteSettings? pdpSitSets = null;
    private NpdsServiceDefaults? npdsSrvcDefs = null;

    public IConfiguration Configuration { get; private set; }
    public IWebHostEnvironment Environment { get; private set; }
    // public IServiceProvider ServiceProvider { get; private set; }

    public Startup(IConfiguration config, IWebHostEnvironment envir)
    {
      // from PDP.DREAM.NpdsRootLib.Utilities
      ConfigManager.Initialize(config);
      Configuration = config;
      Environment = envir;
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
      services.AddSingleton<IConfiguration>(Configuration);
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
        if (!Environment.IsDevelopment())
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
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseHttpContext();
      app.UseStatusCodePages();
      if (Environment.IsDevelopment())
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
        if (!Environment.IsDevelopment()) { app.UseHsts(); }
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
        PdpEndpoints.RegisterPdpArea(r, true);
        // then routes without constraints
        PdpEndpoints.RegisterPdpWebApp(r, "PDP", "NpdsCoreLib", "Index");
        // PdpEndpoints.RegisterPdpRazorBlazor(r, true, false, "/Error");
      });
      app.UseEndpoints(GetRoutes);

    } // method

  } // class

} // namespace
