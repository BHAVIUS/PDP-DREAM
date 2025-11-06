// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class PdpHelpTestScribe : ScribeTkgPageController
{
  private const string rzrClass = nameof(PdpHelpTestScribe);
  public PdpHelpTestScribe() : base() { }

  // ScribeService AdminResreps
  public const string pageName = DepPdpHelpTestScribe;
  public const string pagePath = pageName;
  public const string bodyTitle = PdpSitePathKey;
  // ATTN: cannot use Telerik dropdown menus as headerMenu when displayed in the *DebugPageMenu
  public const string bodyMenu = "_ScribeWebLibDebugPageMenu";

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeRegistrar, // resets ServicePath
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin, // resets *ClientRequired
      ResrepFormat = NPDSCD.ResrepFormatScribe, // requires ServicePath
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(pageName, pagePath, bodyTitle, bodyMenu);
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository();
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string searchType, string serviceTag, string entityType, string resrepFormat)
  {
    if (string.IsNullOrEmpty(searchType)) { searchType = NPDSCD.SearchTypeEnmDflt.EName; }
    if (string.IsNullOrEmpty(serviceTag)) { serviceTag = NPDSCD.SearchTagDefault; }
    if (string.IsNullOrEmpty(entityType)) { entityType = NPDSCD.EntityTypeEnmDflt.EName; }
    if (string.IsNullOrEmpty(resrepFormat)) { resrepFormat = NPDSCD.ResrepFormatEnmDflt.EName; }
    // SelectFilter constrained to Scribe service
    NPDSCW.ParseNpdsSelectFilter(searchType, serviceTag, entityType, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
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