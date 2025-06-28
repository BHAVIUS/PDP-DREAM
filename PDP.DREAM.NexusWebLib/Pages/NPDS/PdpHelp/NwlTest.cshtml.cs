// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class NexusWebLibTest : TkgnPageController
{
  private const string rzrClass = nameof(NexusWebLibTest);
  public NexusWebLibTest() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeNexus, // resets SearchFilter and DatabaseType
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin // resets *ClientRequired
    };
    NPDSCW.ResrepFormat = NPDSCD.ResrepFormatCore; // requires DatabaseType
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusWebLibTest, PdpSitePathKey);
    // ATTN: cannot use Telerik dropdown menus as headerMenu when displayed in the *DebugPageMenu
    PSRM.InitRazorPageMenus("_NexusWebLibDebugPageMenu");
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
    // SelectFilter constrained to Nexus service
    var entityTag = ESS;
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Nexus, serviceTag, entityType, entityTag, searchFilter, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetCoreRepository(true);
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