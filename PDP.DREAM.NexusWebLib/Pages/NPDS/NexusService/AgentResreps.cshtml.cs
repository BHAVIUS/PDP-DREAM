// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class NexusServiceAgentResreps : TkgnPageController
{
  private const string rzrClass = nameof(NexusServiceAgentResreps);
  public NexusServiceAgentResreps() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeNexus,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessAgent,
      AgentModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusServiceAgentResreps, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NexusWebLibSpanPageMenu", "_NexusServiceSpanPageMenu");
    ResetCoreRepository(); // CoreRepo resets WRACE/QUDC if/when necessary
    var isVerified = CheckNpdsAgentSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    ResetNexusRepository(); // for both OnGet and OnPost incl TKG AJAX callbacks
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
    // SelectFilter constrained to Nexus service
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