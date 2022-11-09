// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Stores;
using PDP.DREAM.ScribeWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAuthor)]
public class ScribeServerAuthorResreps : TkgsPageControllerBase
{
  private const string rzrCntrllr = nameof(ScribeServerAuthorResreps);
  public ScribeServerAuthorResreps(QebIdentityContext userCntxt,
    ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Author,
      AuthorModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel(DepScribeServerAuthorResreps, PdpSitePathKey);
    PSR.InitRazorPageMenus("_ScribeWebLibSpanPageMenu");
    ResetScribeRepository(true);
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
#if DEBUG
    QURC.DebugClientAccess();
    PSR.DebugRazorPageStrings();
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string entityType)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    // SelectFilter properties
    var recordAccess = NpdsRecordAccess.Author.ToString();
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    PSR.NpdsRazorBodyTitle(QURC.ServiceTitle);
    ResetScribeRepository(true);
#if DEBUG
    QURC.DebugClientAccess();
    PSR.DebugRazorPageStrings();
#endif
    ViewData[QurcKey] = QURC;
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrCntrllr);
    DebugQurcData(exeCntxt.Result);
    QURC.DebugClientAccess();
#endif
  }

} // end class

// end file