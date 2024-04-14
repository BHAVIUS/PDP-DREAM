// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthor)]
public class AuthorModeHelp : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AuthorModeHelp);
  public AuthorModeHelp() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAuthor,
      AuthorModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepAuthorModeHelp, $"{PDPSS.AppOwnerNameShort}: Author Help");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AuthorModeSpanPageMenu");
    ResetQebiRepository();
    var isUserVerified = CheckQebiUserSession();
    if (!isUserVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    //ResetCoreRepository(); // required for TKG Ajax post callbacks
    //var isAgentVerified = CheckNpdsAgentSession();
    //if (!isAgentVerified) { RedirectToPage(DepAgentModeAddRoleAgent); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file