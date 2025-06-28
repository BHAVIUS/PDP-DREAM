// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetUsername2 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetUsername2);
  public QebiAnonModeResetUsername2() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeResetUsername2, "Reset Username", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string password)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm2(password);
    var usr = NPDSCW.QUDC.GetUserByPassWord(UXM.PassWord);
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
  public ChangeUsernameUxm2 UXM { get; set; } = new ChangeUsernameUxm2();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid &&
      !string.IsNullOrWhiteSpace(UXM.PassWord) && !string.IsNullOrWhiteSpace(UXM.SecurityAnswer))
    {
      var uxm = IQebiUser.ResetUsernameWithToken(UXM.PassWord, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = IQebiUser.NotifyUsernameWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      WraceAddErrors(uxm.FormNote);
    }
    else { WraceAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file