// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class ScribeWebLibIndex : TkgsPageController
{
  private const string rzrClass = nameof(ScribeWebLibIndex);
  public ScribeWebLibIndex() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeWebLibIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SwlPdpHelpSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for QebiUser
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    // if (!string.IsNullOrEmpty(recordAccess))
    // { NPDSCW.RecordAccessReqst = recordAccess; }
    // SearchFilter properties
    // NPDSCW.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    // PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetCoreRepository(true);
    NPDSCW.ResetScribeRepository(true, true);
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnGet), rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuted), rzrClass);
#endif
  }

  // Other page handlers and properties

} // end class

// end file