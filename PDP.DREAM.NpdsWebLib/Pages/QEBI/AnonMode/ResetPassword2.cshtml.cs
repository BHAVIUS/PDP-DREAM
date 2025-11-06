// ResetPassword2.cshtml.cs // PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetPassword2 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetPassword2);
  public QebiAnonModeResetPassword2() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeResetPassword2, "Reset Password", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string username)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm2(username);
    var usr = NPDSCW.QebiDR.GetUserByUserName(UXM.UserName);
    UXM.SecurityQuestion = usr.SecurityQuestion;
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
  public ChangePasswordUxm2 UXM { get; set; } = new ChangePasswordUxm2();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid &&
      !string.IsNullOrWhiteSpace(UXM.UserName) && !string.IsNullOrWhiteSpace(UXM.SecurityAnswer))
    {
      var uxm = Controllers.IQebiUser.ResetPasswordWithToken(UXM.UserName, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = Controllers.IQebiUser.NotifyPasswordWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      WraceAddErrors(uxm.FormNote);
    }
    else { WraceAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file