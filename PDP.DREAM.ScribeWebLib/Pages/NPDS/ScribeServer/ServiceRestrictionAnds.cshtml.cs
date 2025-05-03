// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Stores;
using PDP.DREAM.ScribeWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAdmin)]
public class ScribeServerServiceRestrictionAnds : TkgsPageControllerBase
{
  private const string rzrCntrllr = nameof(ScribeServerServiceRestrictionAnds);
  public ScribeServerServiceRestrictionAnds(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Admin,
      AdminModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel("/NPDS/ScribeServer/ServiceRestrictionAnds",  PdpSitePathKey);
    ResetScribeRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
    // serviceType, serviceTag, entityType overridden 
    QURC.ServiceType = NpdsServiceType.Nexus;
    QURC.ServiceTag = NpdsRoot;
    QURC.EntityType = NpdsEntityType.AnyAndAll;
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string entityType, string recordAccess)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    // select filter properties
    serviceType = QURC.ServiceType.ToString();
    serviceTag = QURC.ServiceTag;
    entityType = QURC.EntityType.ToString();
    if (string.IsNullOrWhiteSpace(recordAccess)) { recordAccess = NpdsRecordAccess.Admin.ToString(); }
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    ResetScribeRepository(true);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrCntrllr);
    DebugQurcData(exeCntxt.Result);
#endif
  }

} // end class

// end file