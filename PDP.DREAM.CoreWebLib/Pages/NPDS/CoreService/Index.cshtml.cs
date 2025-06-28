// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class CoreServiceIndex : TkgcPageController
{
  private const string rzrClass = nameof(CoreServiceIndex);
  public CoreServiceIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAgent,
      AuthenticatedClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepCoreServiceIndex, "NPDS CoreService Index", true);
    PSRM.InitRazorPageMenus("_CoreServiceSpanPageMenu");
    NPDSCW.ResetCoreRepository();
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepqAnonModeAccessDenied); }
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess = "")
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
#endif
    // TODO: fix hack on RecordAccess roles
    // create parser for RecordAccess that limits to specified roles
    if (!string.IsNullOrEmpty(recordAccess))
    { NPDSCW.RecordAccessReqst = recordAccess; }
    // SearchFilter properties
    // NPDSCW.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetCoreRepository(true, true, ESS);
#if DEBUG
    NPDSCW.DebugCoreRepo(rzrHndlr, rzrClass);
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