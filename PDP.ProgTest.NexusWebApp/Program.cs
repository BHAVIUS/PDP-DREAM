// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Migrations;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Migrations;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;

namespace PDP.DREAM.NexusWebApp;

// top-level program can be derived from previous combined use of 
//    1) Program.cs with CreateHostBuilder()
//    2) Startup.cs with ConfigureServices() and Configure()
// but then lose visibility of entrypoint and namespace in Class View
// meanwhile the approach below eliminates use of multiple different methods for startup program
// as if a top-level program but retains visibility in Class View

public class Program
{
  public static void Main(string[] args)
  {
    PDPCC = new PdpCodeConfig(typeof(Program));
    // PdpConfigManager in PDP.DREAM.CoreDataLib.Utilities
    PDPSS = new PdpSiteSettings(PDPCC.PdpCodeProjroot);
    NPDSSD = new NpdsServerDefaults(PDPCC.PdpCodeProjroot);

    // local variables prefix pdw for Portal-Doors Website
    // pdwBuilder as Microsoft.AspNetCore.Builder.WebApplicationBuilder
    // pdwServices as Microsoft.Extensions.DependencyInjection.IServiceCollection
    // pdwApp as Microsoft.AspNetCore.Builder.WebApplication

    #region server host configuration with pdwBuilder

    // WebApplicationOptions in Microsoft.AspNetCore.Builder
    var pdwOptions = new WebApplicationOptions
    {
      Args = args,
      ApplicationName = PDPCC.PdpCodeAppnam,
      ContentRootPath = PDPCC.PdpCodeProjroot,
      WebRootPath = PDPCC.PdpCodeWebroot,
    };
    // WebApplicationBuilder in Microsoft.AspNetCore.Builder
    var pdwBuilder = WebApplication.CreateBuilder(pdwOptions);
    // IWebHostEnvironment in Microsoft.AspNetCore.Hosting
    var pdwEnvir = pdwBuilder.Environment;

    #endregion

    #region services container with pdwServices

    // IServiceCollection in Microsoft.Extensions.DependencyInjection
    var pdwServices = pdwBuilder.Services;

    // add utility services
    pdwServices.AddSingleton<IConfiguration>(pdwBuilder.Configuration);
    pdwServices.AddSingleton<ISmsSender, TwilioSmsender>();
    pdwServices.AddSingleton<IEmailSender, MailKitEmailer>();
    pdwServices.AddSingleton<ILoggerFactory, LoggerFactory>();
    pdwServices.AddHttpClient<IGeolocater, BingMapsService>();
    // optional utility service for static file library
    if (!string.IsNullOrEmpty(PDPSS.AppFilepathFileprov))
    {
      IFileProvider fileProv = new PhysicalFileProvider(PDPSS.AppFilepathFileprov);
      pdwServices.AddSingleton<IFileProvider>(fileProv);
    }

    // check create User database
    var dbIsValid = QebSql.CheckConnection(NPDSSD.QebiDbconstr);
    using (var dbContxt = new QebiDbsqlContext())
    {
      if (dbIsValid)
      {
         dbIsValid = SqlServer2022Siaa.CheckDatabase(dbContxt);
      }
      else if (PDPSS.AppUseDevtestFeature) // (if allow dbCreation)
      {
        var coreCrfp = pdwEnvir.ContentRootFileProvider;
        dbIsValid = SqlServer2022Siaa.CreateDatabase(NPDSSD.QebiDbconstr, coreCrfp);
      }
    }

    // check create Core database
    dbIsValid = QebSql.CheckConnection(NPDSSD.CoreDbconstr);
    using (var dbContxt = new CoreDbsqlContext())
    {
      if (dbIsValid)
      {
         dbIsValid = SqlServer2022Core.CheckDatabase(dbContxt);
      }
      else if (PDPSS.AppUseDevtestFeature) // (if allow dbCreation)
      {
        var coreCrfp = pdwEnvir.ContentRootFileProvider;
        dbIsValid = SqlServer2022Core.CreateDatabase(NPDSSD.CoreDbconstr, coreCrfp);
      }
    }
    if (!dbIsValid) { throw new Exception(PDPCC.PdpCodeErrmsg); }

    // check create Nexus database
    dbIsValid = QebSql.CheckConnection(NPDSSD.NexusDbconstr);
    using (var dbContxt = new NexusDbsqlContext())
    {
      if (dbIsValid)
      {
        dbIsValid = SqlServer2022Nexus.CheckDatabase(dbContxt);
      }
      else if (PDPSS.AppUseDevtestFeature) // (if allow dbCreation)
      {
        var coreCrfp = pdwEnvir.ContentRootFileProvider;
        dbIsValid = SqlServer2022Nexus.CreateDatabase(NPDSSD.NexusDbconstr, coreCrfp);
      }
    }
    if (!dbIsValid) { throw new Exception(PDPCC.PdpCodeErrmsg); }

    if (pdwEnvir.IsDevelopment())
    {
      pdwServices.AddDatabaseDeveloperPageExceptionFilter();
    }

    // add site settings and service cache for data contexts
    try
    {
      var hasAppGuid = false;
      var hasServiceCache = false;
      dbIsValid = false;

      // no DI available for NPDS data contexts unless uncommented
      // pdwServices.AddDbContext<QebiDbsqlContext>();
      // pdwServices.AddDbContext<CoreDbsqlContext>();
      // pdwServices.AddDbContext<NexusDbsqlContext>();
      
      // add app site settings
      using (var dbCntxt = new QebiDbsqlContext())
      {
        dbCntxt.DbsqlConnect();
         hasAppGuid = dbCntxt.QebiContextHasAppGuid();
        dbCntxt.DbsqlDisconnect();
      }
      // add service cache
      using (var dbCntxt = new CoreDbsqlContext())
      {
        dbCntxt.DbsqlConnect();
        dbCntxt.LoadNpdsServiceCache();
        var countL2R = NPDSSD.NpdsServiceCache.CountL2R();
        var countR2L = NPDSSD.NpdsServiceCache.CountR2L();
        if ((countL2R > 0) && (countR2L > 0) && (countL2R == countR2L)) { hasServiceCache= true; }
        dbCntxt.DbsqlDisconnect();
      }
      if (hasAppGuid && hasServiceCache) { dbIsValid = true; }
    }
    catch
    {
      dbIsValid = false;
    }
    if (!dbIsValid) { throw new Exception(PDPCC.PdpCodeErrmsg); }

    // configure IIS
    pdwServices.Configure<IISServerOptions>(options => {
      options.AllowSynchronousIO = true;
    });
    // Microsoft.Extensions.DependencyInjection.HttpServiceCollectionExtensions.AddHttpContextAccessor
    // runs the Microsoft.AspNetCore.Http.HttpContextAccessor
    pdwServices.AddHttpContextAccessor();

    // add secure user identity authentication authorization
    if (PDPSS.AppUseSecureUiaa)
    {
      pdwServices.Configure<CookiePolicyOptions>(options => {
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
        options.HttpOnly = HttpOnlyPolicy.Always;
        options.Secure = CookieSecurePolicy.Always;
        options.CheckConsentNeeded = (context => true);
      });
      pdwServices.AddAuthentication(PdpIdentityScheme)
        .AddCookie(PdpIdentityScheme, options => {
          options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          options.SlidingExpiration = true;
          options.LoginPath = DepQebIdentLogin;
          options.AccessDeniedPath = DepQebIdentRequired;
        });
      if (!pdwEnvir.IsDevelopment())
      {
        pdwServices.AddHttpsRedirection(options => {
          options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
          options.HttpsPort = 443;
        });
        pdwServices.Configure<MvcOptions>(options => {
          options.Filters.Add(new RequireHttpsAttribute());
        });
      }
    }
    else
    {
      pdwServices.ConfigureApplicationCookie(options => {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
      });
    }

    // add routing for controllers with views
    pdwServices.AddRouting(options => {
      options.ConstraintMap.Add(NpdsPT, typeof(NpdsPrincipalTagConstraint));
      options.ConstraintMap.Add(NpdsIS, typeof(NpdsInfosetStatusConstraint));
      options.ConstraintMap.Add(NexusST, typeof(DiristryServiceTypesConstraint));
    });

    // add Razor view controllers
    if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Views) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
    {
      // add named-folder in Views for action controllers
      // (for controllers derived from Microsoft.AspNetCore.Mvc.Controller)
      pdwServices.Configure<RazorViewEngineOptions>(options => {
        var viewFiles = DepNpdsViews + RazorViewEngine.ViewExtension;
        options.ViewLocationFormats.Add(viewFiles);
      });
      pdwServices.AddControllersWithViews(options => {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
      }).AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
      });
    }

    // add Razor page controllers
    if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Pages) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
    {
      pdwServices.AddRazorPages(options => {
        if (PDPSS.AppUseDefPathStart)
        {
          // for empty-path route using AppSite default path
          options.Conventions.AddPageRoute(PDPSS.AppSiteDefPath, DepEmptyPath);
        }
        else
        {
          // for empty-path route using AppSite default page
          options.Conventions.AddPageRoute(PDPSS.AppSiteDefPage, DepEmptyPath);
        }
        // TODO: PdpRazorPageRouteAttribute ??? ignored ???
        // for assigning route name and order on pages
        options.Conventions.Add(new PdpRazorPageRouteConvention());
      });
    }

    // add Telerik Kendo UI widgets
    pdwServices.AddKendo();

    #endregion

    #region application pipeline with pdwApp

    // WebApplication in Microsoft.AspNetCore.Builder
    var pdwApp = pdwBuilder.Build();
    pdwApp.QebUseHttpContext();
    pdwApp.UseStatusCodePages();
    if (pdwEnvir.IsDevelopment())
    {
      pdwApp.UseDeveloperExceptionPage();
      // pdwApp.UseMigrationsEndPoint();
      // pdwApp.UsePdpSite404Redirect();
    }
    else
    {
      pdwApp.UseExceptionHandler(DepPdpSiteErrors);
    }

    if (PDPSS.AppUseSecureUiaa)
    {
      if (!pdwEnvir.IsDevelopment()) { pdwApp.UseHsts(); }
      pdwApp.UseHttpsRedirection();
      pdwApp.UseCookiePolicy();
    }

    if (PDPSS.AppUseStaticFiles) // PdpSiteStaticFiles before Routing
    {
      pdwApp.UsePdpSiteStaticFiles();
    }
    pdwApp.UseRouting(); // Routing before Authentication and Authorization

    if (PDPSS.AppUseDebugRouting) // ATTN: use requires setting flag in appsettings.json
    {
      pdwApp.UseWhen(
        context => (context.Request.Query.ContainsKey(PdpDebugRouteQueryKey)
        || context.Request.Query.ContainsKey(PdpHelpRouteQueryKey)
        || context.Request.Query.ContainsKey(PdpHelpRouteHackKey)),
       builder => builder.UseQebRouteDebuggerPage());
    }

    if (PDPSS.AppUseSecureUiaa)
    {
      // Cors after Routing, before Auth; 
      if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Views) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
      { pdwApp.UseCors(); } // requires use of .AddControllersWithViews()
      pdwApp.UseAuthentication();
      pdwApp.UseAuthorization();
    }

    Action<IEndpointRouteBuilder> GetRoutes;
    GetRoutes = (r => {
      r.MapControllers(); // for NPDS REST and TKG JSON controllers
      r.MapRazorPages();  // for Web Apps
    });
    pdwApp.UseEndpoints(GetRoutes);

    pdwApp.Run();

    #endregion

  } // end method

} // end class

// end file