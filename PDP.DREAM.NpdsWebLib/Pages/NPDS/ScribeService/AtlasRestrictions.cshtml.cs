// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class ScribeServiceAtlasRestrictions : ScribeTkgPageController
{
  private const string rzrClass = nameof(ScribeServiceAtlasRestrictions);
  public ScribeServiceAtlasRestrictions() :base() { }

  // ScribeService Atlas Restrictions
  public const string pageName = DepScribeServiceAtlasRestrictions;
  public const string pagePath = pageName;
  public const string bodyTitle = PdpSitePathKey;
  public const string bodyMenu = "_ScribeServiceSpanPageMenu";

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeRegistrar, // resets ServicePath
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin, // resets *ClientRequired
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
    NPDSCW.ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    this.DebugWraceRazorPage(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string entityType, string resrepFormat)
  {
    if (string.IsNullOrEmpty(serviceType)) { serviceType = NPDSCD.SearchTypeRegistrar.EName; }
    if (string.IsNullOrEmpty(serviceTag)) { serviceTag = PdpNpdsRootTag; }
    if (string.IsNullOrEmpty(entityType)) { entityType = NPDSCD.EntityTypeEnmDflt.EName; }
    if (string.IsNullOrEmpty(resrepFormat)) { resrepFormat = NPDSCD.ResrepFormatEnmDflt.EName; }
    // SelectFilter constrained to Scribe service
    NPDSCW.ParseNpdsSelectFilter(serviceType, serviceTag, entityType, resrepFormat);
    PSRM.NpdsRazorBodyTitle(NPDSCW.ServiceTitle);
    NPDSCW.ResetAtlasRepository(true);
    NPDSCW.ResetScribeRepository(true, true);
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