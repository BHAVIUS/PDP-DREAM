// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetUsername3 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetUsername3);
  public AnonModeResetUsername3() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AnonReadOnly),
      RecordAccess = NPDSCD.RecordAccessAnon,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeResetUsername3, $"{PDPSS.AppOwnerNameShort}: Reset Username");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm3(id, ct);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  //  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  //  {
  //#if DEBUG
  //    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
  //    DebugQurcData(exeCntxt.Result);
  //#endif
  //  }

  // Other page handlers and properties

  [BindProperty]
  public ChangeUsernameUxm3 UXM { get; set; } = new ChangeUsernameUxm3();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.PassWord) &&
      !string.IsNullOrWhiteSpace(UXM.SecurityToken) && !string.IsNullOrWhiteSpace(UXM.NewUsername))
    {
      var uxm = IQebiUser.ChangeUsernameWithToken(UXM.PassWord, UXM.SecurityToken, UXM.NewUsername);
      if (uxm.UsernameChanged) { UXM.FormCompleted = true; }
      if (!string.IsNullOrEmpty(uxm.FormMessage)) { WraceUxmAddErrors(uxm.FormMessage); }
    }
    return Page();
  }

} // end class

// end file