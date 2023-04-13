// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class ScribeServerAuthorAccess : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServerAuthorAccess);
  public ScribeServerAuthorAccess() { }

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
    PSRM = new PdpSiteRazorModel(DepScribeServerAuthorAccess, PdpSitePathKey);
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
  public IActionResult OnGet(string recordAccess)
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullQurc(rzrHndlr, rzrClass);
#endif
    // SearchFilter properties
    if (!string.IsNullOrEmpty(recordAccess) &&  recordAccess.ToLower() != "author") 
    { QURC.RecordAccessReqst = recordAccess; }
    ResetCoreRepository(true);
    ResetScribeRepository(true);
    // build select lists
    BuildCoreDropDownLists();
#if DEBUG
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file