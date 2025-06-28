// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsReviewerRS)]
public class NpdsReviewerModeHelp : TkgcPageController
{
  private const string rzrClass = nameof(NpdsReviewerModeHelp);
  public NpdsReviewerModeHelp() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessReviewer,
      ReviewerModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepnReviewerModeHelp, "Reviewer Help", true);
    PSRM.InitRazorPageMenus("_NpdsReviewerModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
    var isUserVerified = CheckQebiUserSession();
    if (!isUserVerified) { LocalRedirect(DepqAnonModeAccessDenied); }
    //ResetCoreRepository(); // required for TKG Ajax post callbacks
    //var isAgentVerified = CheckNpdsAgentSession();
    //if (!isAgentVerified) { LocalRedirect(DepAgentModeAddRoleAgent); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file