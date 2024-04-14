// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class ScribeServiceImportNpdsQuads : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServiceImportNpdsQuads);
  public ScribeServiceImportNpdsQuads() { }

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
    PSRM = new PdpSiteRazorModel(DepScribeServiceImportNpdsQuads, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeWebLibSpanPageMenu", "_ScribeServiceSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckNpdsAgentSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    ResetScribeRepository(); // for both OnGet and OnPost
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
    CatchNullScribe(rzrHndlr, rzrClass);
#endif
    if (!string.IsNullOrEmpty(recordAccess))
    { WRACE.RecordAccessReqst = recordAccess; }
    // SelectFilter constrained to Scribe service
    // WRACE.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag, entityType, searchFilter);
    // PSRM.NpdsRazorBodyTitle(WRACE.ServiceTitle);
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
  public IActionResult OnPost(NpdsQuadUxm UXM, IFormFileCollection bcrFormFiles)
  {
#if DEBUG
    CatchNullWrace(nameof(OnPost), rzrClass);
    PSRM.DebugRazorPageStrings();
    var recAccss = UXM.RRRecordAccess;
#endif
    if (ModelState.IsValid)
    {
      return ScribeImportNpdsQuads(UXM, bcrFormFiles);
    }
    else
    {
      ModelState.AddModelError("", "Invalid model.");
    }
    return Page();
  }

} // end class

// end file