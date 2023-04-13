// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class ScribeServerExportNpdsQuads : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServerExportNpdsQuads);
  public ScribeServerExportNpdsQuads() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Author,
      AuthenticatedClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServerExportNpdsQuads, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeWebLibSpanPageMenu", "_ScribeServerSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
    ResetScribeRepository(); // for both OnGet and OnPost
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess = "")
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullQurc(rzrHndlr, rzrClass);
#endif
    if (!string.IsNullOrEmpty(recordAccess))
    { QURC.RecordAccessReqst = recordAccess; }
    // SearchFilter properties
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    // PSRM.NpdsRazorBodyTitle(QURC.ServiceTitle);
    ResetCoreRepository(true);
    ResetScribeRepository(true);
    // build select lists
    BuildCoreDropDownLists();
    UXM = new NpdsQuadEditModel();
    UXM.RRRecordAccess = QURC.RecordAccess.ToString();
#if DEBUG
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

  public NpdsQuadEditModel UXM { get; set; } = new NpdsQuadEditModel();

  // maintain this Razor page handler as wrapper so that
  // the BibCitRefLib method can remain independent of the frontend client
  public IActionResult OnPost(NpdsQuadEditModel UXM)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPost), rzrClass);
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