// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class ScribeServiceCccServiceRestrictions : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServiceCccServiceRestrictions);
  public ScribeServiceCccServiceRestrictions() :base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe, // resets DatabaseType, SearchFilter, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin // resets *ClientRequired
    };
    // searchFilter, serviceTag, entityType, resrepFormat overridden
    NPDSCW.SearchFilter = NPDSCD.SearchFilterDiristry; // NpdsSearchFilter.Services
    NPDSCW.ServiceTag = PdpNpdsRootTag;
    NPDSCW.EntityType = NPDSCD.EntityTypeNpdsDiristry;
    NPDSCW.ResrepFormat = NPDSCD.ResrepFormatCore; // requires DatabaseType
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServiceCccServiceRestrictions, PdpSitePathKey);
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
  public IActionResult OnGet(string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    if (string.IsNullOrEmpty(searchFilter)) { searchFilter = NPDSCW.SearchFilter.EName; }
    if (string.IsNullOrEmpty(serviceTag)) { serviceTag = NPDSCW.ServiceTag; }
    if (string.IsNullOrEmpty(entityType)) { entityType = NPDSCW.EntityType.EName; }
    if (string.IsNullOrEmpty(resrepFormat)) { resrepFormat = NPDSCW.ResrepFormat.EName; }
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