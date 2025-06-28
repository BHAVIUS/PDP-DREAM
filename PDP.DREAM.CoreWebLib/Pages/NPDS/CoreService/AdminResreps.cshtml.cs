// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class CoreServiceAdminResreps : TkgcPageController
{
  private const string rzrClass = nameof(CoreServiceAdminResreps);
  public CoreServiceAdminResreps() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepCoreServiceAdminResreps, "NPDS CoreService AdminResreps", true);
    PSRM.InitRazorPageMenus("_CoreServiceSpanPageMenu");
    NPDSCW.ResetQebiRepository();  // for QebiUser
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // for NpdsAgent and TKG Ajax post callbacks
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string searchFilter, string serviceTag, string entityType)
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    // SelectFilter constrained to Scribe service
    var entityTag = ESS;
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Core, serviceTag, entityType, entityTag, searchFilter);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetCoreRepository(true);
#if DEBUG
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file