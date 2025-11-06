// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsReviewerRS, NpdsEditorRS, NpdsAdminRS)]
public class NexusServiceReviewerResreps : NexusTkgPageController
{
  private const string rzrClass = nameof(NexusServiceReviewerResreps);
  public NexusServiceReviewerResreps() : base() { }

  // NexusService ReviewerResreps
  public const string pageName = DepNexusServiceReviewerResreps;
  public const string pagePath = pageName;
  public const string bodyTitle = PdpSitePathKey;
  public const string bodyMenu = "_NexusServiceSpanPageMenu";

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeDiristry, // resets ServicePath
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessReviewer, // resets *ClientRequired
      ResrepFormat = NPDSCD.ResrepFormatNexus, // requires ServicePath
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(pageName, pagePath, bodyTitle, bodyMenu);
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository();
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetNexusRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    this.DebugWraceRazorPage(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string entityType, string resrepFormat)
  {
    if (string.IsNullOrEmpty(serviceType)) { serviceType = NPDSCD.ServiceTypeEnmDflt.EName; }
    if (string.IsNullOrEmpty(serviceTag)) { serviceTag = NPDSCD.ServiceTagDefault; }
    if (string.IsNullOrEmpty(entityType)) { entityType = NPDSCD.EntityTypeEnmDflt.EName; }
    if (string.IsNullOrEmpty(resrepFormat)) { resrepFormat = NPDSCD.ResrepFormatEnmDflt.EName; }
    // SelectFilter constrained to Nexus service
    NPDSCW.ParseNpdsSelectFilter(serviceType, serviceTag, entityType, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetAtlasRepository(true);
    NPDSCW.ResetNexusRepository(true);
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    this.DebugWraceRazorPage(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
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