// ResetPassword2.cshtml.cs // PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetPassword2 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetPassword2);
  public AnonModeResetPassword2() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepAnonModeResetPassword2, $"{PDPSS.AppOwnerNameShort}: Reset Password");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string username)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm2(username);
    var usr = QUDC.GetUserByUserName(UXM.UserName);
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
      var uxm = IQebiUser.ResetPasswordWithToken(UXM.UserName, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = IQebiUser.NotifyPasswordWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      WraceUxmAddErrors(uxm.FormMessage);
    }
    else { WraceUxmAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file