// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// PDP dream software APPlication STATUS
public static partial class PdpAppStatus
{
  // NPDS ClientDefaults (NPDSCD)
  public static NpdsClientDefaults NPDSCD { get; set; }

  // Web Rest Api Controller Environment (WRACE)
  // see instance property of type NpdsClientWrace in CoreDataRazorPageControllerBase

  // NPDS Database Context (NPDSDC)
  // see instance property of type INpdsdcClient in PdpDbsqlContextBase  

  // PDP CodeConfig (PDPCC)
  public static PdpCodeConfig PDPCC { get; set; }

  // PDP SiteSettings (PDPSS)
  public static PdpSiteSettings PDPSS { get; set; }

  // PDP WebSiteBuilder (PDPWSB)  WebApplicationBuilder : IHostApplicationBuilder (explicit)
  // , IApplicationBuilderMS, IMvcBuilder (not explicit)
  public static WebApplicationBuilder PDPWSB { get; set; }

  // PDP WebSiteApp (PDPWSA)
  public static WebApplication PDPWSA { get; set; }

  public static void ConfigPdpSettings(Type appType, string appRoot, string[]? appArgs)
  {
    // application configuration with appType, appArgs, appOpts
    PDPCC = new PdpCodeConfig(appType, appRoot);  // in PDP.DREAM.CoreDataLib.Models
    // PdpConfigManager for PdpSiteSettings and NpdsServerDefaults
    PDPSS = new PdpSiteSettings(); // in PDP.DREAM.CoreDataLib.Models
    NPDSCD = new NpdsClientDefaults(); // in PDP.DREAM.CoreDataLib.Models
    // WebApplicationOptions in Microsoft.AspNetCore.Builder
    var appOpts = new WebApplicationOptions
    {
      Args = appArgs,
      // ApplicationName must be assembly name, not app display name
      ApplicationName = PDPCC.PdpCodeAsmnam,
      ContentRootPath = PDPCC.PdpCodePrgroot,
      WebRootPath = PDPSS.AppFilepathWebroot,
    };
    // WebApplicationBuilder in Microsoft.AspNetCore.Builder
    PDPWSB = WebApplication.CreateBuilder(appOpts);
  }

  public static void AddPdpUtilityServices()
  {
    PDPWSB.Services.AddSingleton<IConfiguration>(PDPWSB.Configuration);
    PDPWSB.Services.AddSingleton<ISmsSender, TwilioSmsender>();
    PDPWSB.Services.AddSingleton<IEmailSender, MailKitEmailer>();
    PDPWSB.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
    PDPWSB.Services.AddHttpClient<IGeolocater, BingMapsService>();
    // optional utility service for static file library
    if (!string.IsNullOrEmpty(PDPSS.AppFilepathFileprov))
    {
      IFileProvider fileProv = new PhysicalFileProvider(PDPSS.AppFilepathFileprov);
      PDPWSB.Services.AddSingleton<IFileProvider>(fileProv);
    }
    // add Telerik Kendo UI widgets
    PDPWSB.Services.AddKendo();
  }

  // add PDP site settings and NPDS service cache for data contexts
  public static void AddPdpNpdsDataServices()
  {
    if (PDPWSB.Environment.IsDevelopment())
    {
      PDPWSB.Services.AddDatabaseDeveloperPageExceptionFilter();
    }

    // configure IIS
    PDPWSB.Services.Configure<IISServerOptions>(options => {
      options.AllowSynchronousIO = true;
    });
    // Microsoft.Extensions.DependencyInjection.HttpServiceCollectionExtensions.AddHttpContextAccessor
    // runs the Microsoft.AspNetCore.Http.HttpContextAccessor
    PDPWSB.Services.AddHttpContextAccessor();

    // add secure user identity authentication authorization
    if (PDPSS.AppUseSecureCiaam)
    {
      PDPWSB.Services.Configure<CookiePolicyOptions>(options => {
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
        options.HttpOnly = HttpOnlyPolicy.Always;
        options.Secure = CookieSecurePolicy.Always;
        options.CheckConsentNeeded = (context => true);
      });
      PDPWSB.Services.AddAuthentication(PdpIdentityScheme)
        .AddCookie(PdpIdentityScheme, (Action<Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions>)(options => {
          options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          options.SlidingExpiration = true;
          options.LoginPath = DepAnonModeLoginUser;
          options.AccessDeniedPath = PdpSiteRoutes.DepAnonModeAccessDenied;
        }));
      if (!PDPWSB.Environment.IsDevelopment())
      {
        PDPWSB.Services.AddHttpsRedirection(options => {
          options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
          options.HttpsPort = 443;
        });
        PDPWSB.Services.Configure<MvcOptions>(options => {
          options.Filters.Add(new RequireHttpsAttribute());
        });
      }
    }
    else
    {
      PDPWSB.Services.ConfigureApplicationCookie(options => {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
      });
    }

    // add routing for controllers
    PDPWSB.Services.AddRouting(options => {
      options.ConstraintMap.Add(NpdsPTC, typeof(NpdsPrincipalTagConstraint));
      options.ConstraintMap.Add(NpdsISC, typeof(NpdsInfosetStatusConstraint));
      options.ConstraintMap.Add(NexusSTC, typeof(DiristryServiceTypesConstraint));
      options.ConstraintMap.Add(ScribeSTC, typeof(RegistrarServiceTypesConstraint));
    });

    // add Razor view controllers
    if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Apis) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
    {
      // add named-folder in Views for action controllers
      // (for controllers derived from Microsoft.AspNetCore.Mvc.Controller)
      // PDPWSB.Services.Configure<RazorViewEngineOptions>(options => {
      //   var viewFiles = DepNpdsViews + RazorViewEngine.ViewExtension;
      //   options.ViewLocationFormats.Add(viewFiles);
      // });
      // AddControllers() if apis only
      // AddControllersWithViews() if MVC action views

      PDPWSB.Services.AddControllers(options => {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
      }).AddJsonOptions(options => {
        // https://docs.telerik.com/aspnet-core/installation/json-serialization
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
      });

      if (PDPWSB.Environment.IsDevelopment())
      {
        PDPWSB.Services.AddRazorPages().AddRazorRuntimeCompilation();
        if (PDPSS.AppUseSwagger)
        {
          // Learn more about configuring Swagger/OpenAPI
          //   at https://aka.ms/aspnetcore/swashbuckle
          // https://stackoverflow.com/questions/71932980/what-is-addendpointsapiexplorer-in-asp-net-core-6/71933535#71933535
          // pdwServices.AddEndpointsApiExplorer(); // required for minimal APIs
          PDPWSB.Services.AddSwaggerGen();
        }
      }
    }
    // add Razor pages
    if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Pages) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
    {
      PDPWSB.Services.AddRazorPages(options => {
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
        // TODO: PdpRazorPageRouteAttribute ignored
        // for assigning route name and order on pages
        options.Conventions.Add(new PdpRazorPageRouteConvention());
      });
    }
    // add Blazor components 
    if ((PDPCC.PdpCodeRazor == AcgtCodeRazor.Components) || (PDPCC.PdpCodeRazor == AcgtCodeRazor.All))
    {
      PDPWSB.Services.AddServerSideBlazor();
      PDPWSB.Services.AddRazorComponents().AddInteractiveServerComponents();
      PDPWSB.Services.AddTelerikBlazor();
    }
  }

  public static void BuildPdpSite()
  {
    // application pipeline with PDPWSA

    // WebApplication in Microsoft.AspNetCore.Builder
    PDPWSA = PDPWSB.Build();
    PDPWSA.QebUseHttpContext();
    PDPWSA.UseStatusCodePages();
    if (PDPWSB.Environment.IsDevelopment())
    {
      PDPWSA.UseDeveloperExceptionPage();
      // pdwApp.UseMigrationsEndPoint();
      // pdwApp.UsePdpSite404Redirect();
      if (PDPSS.AppUseSwagger)
      {
        PDPWSA.UseSwagger();
        // Swagger UI at https://localhost:<port>/swagger
        // generated swagger doc at https://localhost:<port>/swagger/v1/swagger.json  
        // https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger
        // OpenAPI v3 at https://spec.openapis.org/oas/v3.1.0
        PDPWSA.UseSwaggerUI();
      }
    }
    else
    {
      PDPWSA.UseExceptionHandler(DepDebugModeDotnetErrors);
    }

    if (PDPSS.AppUseSecureCiaam)
    {
      // Hsts before HttpsRedirection
      PDPWSA.UseHsts();
      PDPWSA.UseHttpsRedirection();
      // CookiePolicy before Routing
      PDPWSA.UseCookiePolicy();
    }

    if (PDPSS.AppUseStaticFiles) // StaticFiles before Routing
    {
      PDPWSA.UsePdpSiteStaticFiles();
    }
    PDPWSA.UseAntiforgery();
    PDPWSA.UseRouting(); // Routing before Authentication and Authorization

    if (PDPSS.AppUseDebugRouting) // ATTN: use requires setting flag in appsettings.json
    {
      PDPWSA.UseWhen(context => (
        context.Request.Query.ContainsKey(PdpDebugRouteQueryKey) ||
        context.Request.Query.ContainsKey(PdpHelpRouteQueryKey) ||
        context.Request.Query.ContainsKey(PdpHelpRouteHackKey)),
       builder => builder.UseQebRouteDebuggerPage());
    }

    if (PDPSS.AppUseSecureCiaam)
    {
      // Cors after Routing, before Auth; 
      PDPWSA.UseCors();
      PDPWSA.UseAuthentication();
      PDPWSA.UseAuthorization();
    }

    Action<IEndpointRouteBuilder> GetRoutes;
    GetRoutes = (r => {
      // for NPDS REST and TKG JSON controllers
      r.MapControllers();
      // for Web Apps with Razor Pages
      r.MapRazorPages();
    });
    PDPWSA.UseEndpoints(GetRoutes);
  }

  public static void RunPdpSite()
  {
    BuildPdpSite();
    PDPWSA.Run();
  }

  // add PDP site settings and NPDS service defaults cache for data contexts
  public static bool AddPdpNpdsCacheService(bool addDbcs = false)
  {
    if (addDbcs)
    {
      PDPWSB.Services.AddDbContext<QebiDalContext>();
      PDPWSB.Services.AddDbContext<CoreDbsqlContext>();
    }
    var hasAppGuid = false;
    var hasServiceCache = false;
    var dbIsValid = false;
    try
    {
      // add app site settings
      using (var dbCntxt = new QebiDalContext())
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
        var countL2R = NPDSCD.NpdsServiceCache.CountL2R();
        var countR2L = NPDSCD.NpdsServiceCache.CountR2L();
#if DEBUG
        // TODO: debug.writeline output to display service cache
        //   var testGuidByTag = NPDSSD.NpdsServiceCache.GetByTag("Guardians2023");
#endif
        if ((countL2R > 0) && (countR2L > 0) && (countL2R == countR2L)) { hasServiceCache = true; }
        dbCntxt.DbsqlDisconnect();
      }
      if (hasAppGuid && hasServiceCache) { dbIsValid = true; }
    }
    catch
    {
      dbIsValid = false;
    }
    if (!dbIsValid)
    {
      Debug.WriteLine($" startup hasAppGuid {hasAppGuid}, hasServiceCache {hasServiceCache}, dbIsValid {dbIsValid}");
      throw new Exception(PDPCC.PdpCodeErrmsg);
    }
    return dbIsValid;
  }

  // for use when called from other methods with arguments to parameters
  // arguments from calling methods are passed to parameters declared in receiving methods
  public static void ThrowNullEmptyException(string variableName, string methodName, string className)
  {
    var errorMessage = $"Null or empty variable {variableName} in method {methodName}";
    if (!string.IsNullOrEmpty(className)) { errorMessage += $" of class {className}"; }
    throw new NullReferenceException(errorMessage);
  }

} // end class

// end file