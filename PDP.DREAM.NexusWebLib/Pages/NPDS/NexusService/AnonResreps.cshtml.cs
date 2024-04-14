// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class NexusServiceAnonResreps : TkgnPageController
{
  private const string rzrClass = nameof(NexusServiceAnonResreps);
  public NexusServiceAnonResreps() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeNexus,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      AuthenticatedClientRequired = false,
      SessionClientRequired = false
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusServiceAnonResreps, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NexusServiceSpanPageMenu");
    ResetCoreRepository();
    ResetNexusRepository(); // for both OnGet and OnPost
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string searchFilter, string serviceTag, string entityType)
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
#endif
    // ServiceType constrained to Nexus service
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Nexus, serviceTag, entityType, "", searchFilter);
    PSRM.NpdsRazorBodyTitle(WRACE.ServiceTitle);
    ResetCoreRepository(true);
    ResetNexusRepository(true);
#if DEBUG
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file