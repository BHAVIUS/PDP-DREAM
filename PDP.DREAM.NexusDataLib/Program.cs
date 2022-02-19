// Program.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NexusDataLib.Migrations;
using PDP.DREAM.NexusDataLib.Stores;

// top-level program can be derived from previous combined use of 
//    1) Program.cs with CreateHostBuilder()
//    2) Startup.cs with ConfigureServices() and Configure()
// but then lose visibility of entrypoint and namespace in Class View
// meanwhile the approach below eliminates use of multiple different methods for startup program
// as if a top-level program but retains visibility in Class View

namespace PDP.DREAM.NexusDataLib;

public class Program
{
  public static void Main(string[] args)
  {
    // local variables prefix pdw for Portal-Doors Website
    // pdwBuilder as Microsoft.AspNetCore.Builder.WebApplicationBuilder
    // pdwServices as Microsoft.Extensions.DependencyInjection.IServiceCollection
    // pdwApp as Microsoft.AspNetCore.Builder.WebApplication

    #region server host configuration with pdwBuilder

    // WebApplicationOptions (Microsoft.AspNetCore.Builder)
    var pdwOptions = new WebApplicationOptions
    {
      Args = args,
      // EnvironmentName = Environments.Development,
      ContentRootPath = Directory.GetCurrentDirectory()
    };
    // WebApplicationBuilder (Microsoft.AspNetCore.Builder)
    var pdwBuilder = WebApplication.CreateBuilder(pdwOptions);
    // IWebHostEnvironment (Microsoft.AspNetCore.Hosting)
    var pdwEnvir = pdwBuilder.Environment;
    // ConfigurationManager (Microsoft.Extensions.Configuration)
    var pdwConfig = pdwBuilder.Configuration;
    // PdpConfigManager (PDP.DREAM.CoreDataLib.Utilities)
    PdpConfigManager.Initialize(pdwConfig);

    #endregion

    #region services container with pdwServices

    // IServiceCollection (Microsoft.Extensions.DependencyInjection)
    var pdwServices = pdwBuilder.Services;

    // add utility services
    pdwServices.AddSingleton<IConfiguration>(pdwConfig);
    pdwServices.AddSingleton<ISmsSender, TwilioSmsender>();
    pdwServices.AddSingleton<IEmailSender, MailKitEmailer>();
    pdwServices.AddSingleton<ILoggerFactory, LoggerFactory>();
    pdwServices.AddHttpClient<IGeolocater, BingMapsService>();

    // check/create User database
    var dbConstr = NpdsServiceDefaults.Values.NpdsUserDbconstr;
    var dbIsValid = SqlServer2019.CheckConnection(dbConstr);
    if (dbIsValid)
    {
      dbIsValid = SqlServer2019.CheckDatabase(dbConstr);
    }
    else // (if allow dbCreation)
    {
      var coreCrfp = pdwEnvir.ContentRootFileProvider;
      dbIsValid = SqlServer2019.CreateDatabase(dbConstr, coreCrfp);
    }

    // check/create Nexus database
    dbConstr = NpdsServiceDefaults.Values.NpdsDiristryDbconstr;
    dbIsValid = SqlServer2019.CheckConnection(dbConstr);
    if (dbIsValid)
    {
      dbIsValid = SqlServer2019.CheckDatabase(dbConstr);
    }
    else // (if allow dbCreation)
    {
      var coreCrfp = pdwEnvir.ContentRootFileProvider;
      dbIsValid = SqlServer2019.CreateDatabase(dbConstr, coreCrfp);
    }

    if (dbIsValid)
    {
      // add database contexts
      pdwServices.AddDbContext<CoreDbsqlContext>();
      pdwServices.AddDbContext<QebIdentityContext>();
      pdwServices.AddDbContext<NexusDbsqlContext>();
      if (pdwEnvir.IsDevelopment())
      {
        pdwServices.AddDatabaseDeveloperPageExceptionFilter();
      }
      // add cached settings 
      using (var dataCntxt = new CoreDbsqlContext())
      {
        dataCntxt.LoadNpdsServiceCache();
      }
      using (var userCntxt = new QebIdentityContext())
      {
        var userApp = userCntxt.GetAppByAppName(PdpSiteSettings.Values.AppSecureUiaaName);
        if (userApp?.AppGuidKey == null) { PdpSiteSettings.Values.AppSecureUiaaGuid = Guid.Empty; }
        else { PdpSiteSettings.Values.AppSecureUiaaGuid = userApp.AppGuidKey; }
      }
    }
    else
    {
      throw new Exception("ERROR: Missing database and/or database not created.");
    }

    // configure IIS
    pdwServices.Configure<IISServerOptions>(options => {
      options.AllowSynchronousIO = true;
    });
    pdwServices.AddHttpContextAccessor();

    // add file provider service for static file library
    if (!string.IsNullOrEmpty(PdpSiteSettings.Values.AppFilepathFileprov))
    {
      IFileProvider fileProv = new PhysicalFileProvider(PdpSiteSettings.Values.AppFilepathFileprov);
      pdwServices.AddSingleton<IFileProvider>(fileProv);
    }

    // add secure user identity authentication authorization
    if (PdpSiteSettings.Values.AppUseSecureUiaa)
    {
      pdwServices.Configure<CookiePolicyOptions>(options => {
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
        options.HttpOnly = HttpOnlyPolicy.Always;
        options.Secure = CookieSecurePolicy.Always;
        options.CheckConsentNeeded = (context => true);
      });
      pdwServices.AddAuthentication(PdpConst.PdpIdentityScheme)
        .AddCookie(PdpConst.PdpIdentityScheme, options => {
          options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          options.SlidingExpiration = true;
          options.LoginPath = NexusDLC.PdpPathIdentLogin;
          options.AccessDeniedPath = NexusDLC.PdpPathIdentRequired;
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
      options.ConstraintMap.Add("NpdsPT", typeof(NpdsPrincipalTagConstraint));
      options.ConstraintMap.Add("NpdsIS", typeof(NpdsInfosetStatusConstraint));
    });
    pdwServices.AddControllersWithViews()
      // .AddRazorRuntimeCompilation()
      .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
      });
    pdwServices.AddRazorPages(options => {
      options.Conventions.AddPageRoute(CoreDLC.PdpPathSiteInfo, "");
      options.Conventions.Add(new PdpMvcPageRouteConvention());
    });
    pdwServices.AddKendo(); // Telerik Kendo UI

    #endregion

    #region application pipeline with pdwApp

    // WebApplication (Microsoft.AspNetCore.Builder)
    var pdwApp = pdwBuilder.Build();

    pdwApp.UseHttpContext();
    pdwApp.UseStatusCodePages();
    if (pdwEnvir.IsDevelopment())
    {
      pdwApp.UseDeveloperExceptionPage();
      // pdwApp.UseMigrationsEndPoint();
      pdwApp.UsePdpSite404Redirect();
    }
    else
    {
      pdwApp.UseExceptionHandler(CoreDLC.PdpPathSiteErrors);
    }

    if (PdpSiteSettings.Values.AppUseSecureUiaa)
    {
      if (!pdwEnvir.IsDevelopment()) { pdwApp.UseHsts(); }
      pdwApp.UseHttpsRedirection();
      pdwApp.UseCookiePolicy();
    }

    if (PdpSiteSettings.Values.AppUseSiteHtmlFile)
    {
      pdwApp.UsePdpSiteDefaultFile(); // DefaultFile before StaticFiles
    }
    pdwApp.UseStaticFiles(); // wwwroot folder StaticFiles
    pdwApp.UsePdpSiteStaticFiles(PdpSiteSettings.Values); // StaticFiles before Routing
    pdwApp.UseRouting(); // Routing before Authentication and Authorization

    if (PdpSiteSettings.Values.AppUseDebugRouting) // ATTN: use requires setting flag in appsettings.json
    {
      pdwApp.UseWhen(
        context => (context.Request.Query.ContainsKey(PdpConst.PdpDebugRouteQueryKey)
        || context.Request.Query.ContainsKey(PdpConst.PdpHelpRouteQueryKey)
        || context.Request.Query.ContainsKey(PdpConst.PdpHelpRouteHackKey)),
       builder => builder.UsePdpRouteDebuggerPage());
    }

    if (PdpSiteSettings.Values.AppUseSecureUiaa)
    {
      pdwApp.UseCors(); // after Routing, before Auth
      pdwApp.UseAuthentication();
      pdwApp.UseAuthorization();
    }

    Action<IEndpointRouteBuilder> GetRoutes;
    GetRoutes = (r => {
      r.MapControllers();
      // r.MapRazorPages();
    });
    pdwApp.UseEndpoints(GetRoutes);

    pdwApp.Run();

    #endregion

  } // method

} // class
