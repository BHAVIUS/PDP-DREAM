// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class NexusServerIndex : TkgnPageController
{
  private const string rzrClass = nameof(NexusServerIndex);
  public NexusServerIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Nexus,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepNexusServerIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NexusServerSpanPageMenu");
    ResetCoreRepository();
    ResetNexusRepository();
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
    ResetNexusRepository(true);
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