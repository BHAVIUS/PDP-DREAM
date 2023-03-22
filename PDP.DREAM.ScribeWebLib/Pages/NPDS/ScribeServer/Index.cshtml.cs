// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAgent)]
public class ScribeServerIndex : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServerIndex);
  public ScribeServerIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Agent,
      AuthenticatedClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServerIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeServerSpanPageMenu");
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
  public IActionResult OnGet(string searchFilter, string serviceTag, string entityType)
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullQurc(rzrHndlr, rzrClass);
#endif
    // SelectFilter properties
    QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    PSRM.NpdsRazorBodyTitle(QURC.ServiceTitle);
    ResetScribeRepository(true);
#if DEBUG
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

} // end class

// end file