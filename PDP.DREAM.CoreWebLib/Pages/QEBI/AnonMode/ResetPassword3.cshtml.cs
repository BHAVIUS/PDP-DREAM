// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetPassword3 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetPassword3);
  public QebiAnonModeResetPassword3() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeResetPassword3, "Reset Password", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm3(id, ct);
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
  public ChangePasswordUxm3 UXM { get; set; } = new ChangePasswordUxm3();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName) &&
      !string.IsNullOrWhiteSpace(UXM.SecurityToken) && !string.IsNullOrWhiteSpace(UXM.NewPassword))
    {
      var uxm = IQebiUser.ChangePasswordWithToken(UXM.UserName, UXM.SecurityToken, UXM.NewPassword);
      if (uxm.PasswordChanged) { UXM.FormCompleted = true; }
      if (!string.IsNullOrEmpty(uxm.FormNote)) { WraceAddErrors(uxm.FormNote); }
    }
    return Page();
  }

} // end class

// end file