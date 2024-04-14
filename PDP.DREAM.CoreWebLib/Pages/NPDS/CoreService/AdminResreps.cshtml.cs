// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class CoreServiceAdminResreps : TkgcPageController
{
  private const string rzrClass = nameof(CoreServiceAdminResreps);
  public CoreServiceAdminResreps() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepCoreServiceAdminResreps, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_CoreServiceSpanPageMenu");
    //ResetQebiRepository();
    //var isUserVerified = CheckQebiUserSession();
    //if (!isUserVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    ResetCoreRepository(); // required for TKG Ajax post callbacks
    var isAgentVerified = CheckNpdsAgentSession();
    if (!isAgentVerified) { RedirectToPage(DepAgentModeAddRoleAgent); }
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
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    // SelectFilter constrained to Scribe service
    var entityTag = ESS;
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Core, serviceTag, entityType, entityTag, searchFilter);
    PSRM.NpdsRazorBodyTitle(WRACE.ServiceTitle);
    ResetCoreRepository(true);
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