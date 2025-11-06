// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthorRS, NpdsReviewerRS, NpdsEditorRS, NpdsAdminRS)]
public class ScribeServiceReviewerAccess : ScribeTkgPageController
{
  private const string rzrClass = nameof(ScribeServiceReviewerAccess);
  public ScribeServiceReviewerAccess() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeRegistrar, // resets ServicePath, SearchType, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
    };
    NPDSCW.ResrepFormat = NPDSCD.ResrepFormatAtlas; // requires ServicePath
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServiceReviewerAccess, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeServiceSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for Index and QebiUser
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentClient();
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
    // NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, entityTag, searchType);
    // PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetAtlasRepository(true);
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