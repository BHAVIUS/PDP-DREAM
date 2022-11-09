// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.NexusWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsEditor)]
public class NexusServerEditorResreps : TkgnPageControllerBase
{
  private const string rzrCntrllr = nameof(NexusServerEditorResreps);
  public NexusServerEditorResreps(QebIdentityContext userCntxt,
    NexusDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Nexus,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadOnly,
      RecordAccess = NpdsRecordAccess.Editor,
      EditorModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel(DepNexusServerEditorResreps, PdpSitePathKey);
    PSR.InitRazorPageMenus("_NexusWebLibSpanPageMenu");
    ResetNexusRepository();
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
    var recordAccess = NpdsRecordAccess.Editor.ToString();
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    PSR.NpdsRazorBodyTitle(QURC.ServiceTitle);
    ResetNexusRepository();
#if DEBUG
    QURC.DebugClientAccess();
    PSR.DebugRazorPageStrings();
#endif
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