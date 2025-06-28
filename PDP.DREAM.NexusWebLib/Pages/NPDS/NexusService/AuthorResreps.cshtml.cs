// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthorRS, NpdsEditorRS, NpdsAdminRS)]
public class NexusServiceAuthorResreps : TkgnPageController
{
  private const string rzrClass = nameof(NexusServiceAuthorResreps);
  public NexusServiceAuthorResreps() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeNexus, // resets DatabaseType, SearchFilter, ServiceTag
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessAuthor // resets *ClientRequired
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusServiceAuthorResreps, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NexusServiceSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for Index and QebiUser
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetNexusRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
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
    // SelectFilter constrained to Nexus service
    var entityTag = ESS;
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Nexus, serviceTag, entityType, entityTag, searchFilter, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetCoreRepository(true);
    NPDSCW.ResetNexusRepository(true);
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