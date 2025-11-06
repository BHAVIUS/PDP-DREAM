// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class PdpHelpTextNexus : NexusTkgPageController
{
  private const string rzrClass = nameof(PdpHelpTextNexus);
  public PdpHelpTextNexus() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeDiristry, // resets SearchType and ServicePath
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin, // resets *ClientRequired
      ResrepFormat = NPDSCD.ResrepFormatNexus, // requires ServicePath
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepPdpHelpTestNexus, PdpSitePathKey);
    // ATTN: cannot use Telerik dropdown menus as headerMenu when displayed in the *DebugPageMenu
    PSRM.InitRazorPageMenus("_NexusWebLibDebugPageMenu");
    NPDSCW.ResetQebiRepository(); // for Index and QebiUser
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetNexusRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string searchType, string searchTag, string entityType, string resrepFormat)
  {
    // SelectFilter constrained to Nexus service
    var entityTag = ESS;
    // ParseNpdsSelectFilter(string serviceType, string serviceTag, string searchType, string searchTag, string entityTag, string resrepFormat)
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Diristry, serviceTag, searchType, searchTag, entityTag, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetAtlasRepository(true);
    NPDSCW.ResetNexusRepository(true, true);
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
