// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class ScribeServiceExportNpdsQuads : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServiceExportNpdsQuads);
  public ScribeServiceExportNpdsQuads() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAuthor,
      AuthenticatedClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServiceExportNpdsQuads, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeWebLibSpanPageMenu", "_ScribeServiceSpanPageMenu");
    //ResetQebiRepository();
    //var isUserVerified = CheckQebiUserSession();
    //if (!isUserVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    ResetCoreRepository();
    var isAgentVerified = CheckNpdsAgentSession();
    if (!isAgentVerified) { RedirectToPage(DepAgentModeAddRoleAgent); }
    ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess = "")
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    if (!string.IsNullOrEmpty(recordAccess))
    { WRACE.RecordAccessReqst = recordAccess; }
    // SelectFilter constrained to Scribe service
    // var entityTag = ESS;
    // WRACE.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, searchFilter);
    // PSRM.NpdsRazorBodyTitle(WRACE.ServiceTitle);
    ResetCoreRepository(true);
    ResetScribeRepository(true, true);
    UXM = new NpdsQuadUxm();
    UXM.RRRecordAccess = WRACE.RecordAccess.ToString();
#if DEBUG
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

  public NpdsQuadUxm UXM { get; set; } = new NpdsQuadUxm();

  // maintain this Razor page handler as wrapper so that
  // the BibCitRefLib method can remain independent of the frontend client
  public IActionResult OnPost(NpdsQuadUxm UXM)
  {
#if DEBUG
    CatchNullWrace(nameof(OnPost), rzrClass);
    PSRM.DebugRazorPageStrings();
    var recAccss = UXM.RRRecordAccess;
#endif
    if (ModelState.IsValid)
    {
      return ScribeExportNpdsQuads(UXM);
    }
    else
    {
      ModelState.AddModelError("", "Invalid model.");
    }
    return Page();
  }

} // end class

// end file