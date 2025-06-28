// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class ScribeServiceAdminResreps : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServiceAdminResreps);
  public ScribeServiceAdminResreps() : base() { }

  // ScribeService AdminResreps
  public const string pageName = DepScribeServiceAdminResreps;
  public const string pagePath = pageName;
  public const string bodyTitle = PdpSitePathKey;
  public const string bodyMenu = "_ScribeServiceSpanPageMenu";

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe, // resets DatabaseType, SearchFilter, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin // resets *ClientRequired
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(pageName, pagePath, bodyTitle, bodyMenu);
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // CoreRepo resets NPDSCW/QUDC if/when necessary
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    if (string.IsNullOrEmpty(searchFilter)) { searchFilter = NPDSCD.SearchFilterDfltSrch.EName; }
    if (string.IsNullOrEmpty(serviceTag)) { serviceTag = NPDSCD.ServiceTagDfltSrch; }
    if (string.IsNullOrEmpty(entityType)) { entityType = NPDSCD.EntityTypeDfltSrch.EName; }
    if (string.IsNullOrEmpty(resrepFormat)) { resrepFormat = NPDSCD.ResrepFormatDfltSrch.EName; }
    // SelectFilter constrained to Scribe service
    var entityTag = ESS;
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, entityTag, searchFilter, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
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