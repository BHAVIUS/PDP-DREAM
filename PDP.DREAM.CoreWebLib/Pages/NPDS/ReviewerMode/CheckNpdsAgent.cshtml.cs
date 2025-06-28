// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsReviewerRS)]
public class NpdsReviewerModeCheckNpdsAgent : TkgcPageController
{
  private const string rzrClass = nameof(NpdsReviewerModeCheckNpdsAgent);
  public NpdsReviewerModeCheckNpdsAgent() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessReviewer,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepnReviewerModeCheckNpdsAgent, "Check NPDS Reviewer", true);
    PSRM.InitRazorPageMenus("_NpdsReviewerModeSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for QebiUser
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess)
  {
    if (!string.IsNullOrEmpty(recordAccess)) { NPDSCW.RecordAccessReqst = recordAccess; }
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