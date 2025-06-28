// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthorRS, NpdsReviewerRS, NpdsEditorRS, NpdsAdminRS)]
public class ScribeServiceReviewerAccess : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServiceReviewerAccess);
  public ScribeServiceReviewerAccess() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe, // resets DatabaseType, SearchFilter, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
    };
    NPDSCW.ResrepFormat = NPDSCD.ResrepFormatCore; // requires DatabaseType
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServiceReviewerAccess, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeServiceSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for Index and QebiUser
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
  public IActionResult OnGet(string recordAccess = "")
  {
    if (!string.IsNullOrEmpty(recordAccess))
    { NPDSCW.RecordAccessReqst = recordAccess; }
    // SelectFilter constrained to Scribe service
    // var entityTag = ESS;
    // NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, entityTag, searchFilter);
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