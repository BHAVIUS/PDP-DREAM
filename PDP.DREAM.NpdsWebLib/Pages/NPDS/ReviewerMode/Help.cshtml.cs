// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsReviewerRS)]
public class NpdsReviewerModeHelp : AtlasTkgPageController
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
    var isUserVerified = CheckQebiUserClient();
    if (!isUserVerified) { LocalRedirect(DepqAnonModeAccessDenied); }
    //ResetAtlasRepository(); // required for TKG Ajax post callbacks
    //var isAgentVerified = CheckNpdsAgentClient();
    //if (!isAgentVerified) { LocalRedirect(DepAgentModeAddRoleAgent); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file