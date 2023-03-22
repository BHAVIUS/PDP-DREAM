// ResetUsername2.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetUsername2 : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetUsername2);
  public AnonModeResetUsername2(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel("/NPDS/AnonMode/ResetUsername2",
      $"{PDPSS.AppOwnerShortName}: Reset Username");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string password)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm2(password);
    var usr = QUDC.GetUserByPassWord(UXM.PassWord);
    UXM.SecurityQuestion = usr.SecurityQuestion;
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  // Other page handlers and properties

  [BindProperty]
  public ChangeUsernameUxm2 UXM { get; set; } = new ChangeUsernameUxm2();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid &&
      !string.IsNullOrWhiteSpace(UXM.PassWord) && !string.IsNullOrWhiteSpace(UXM.SecurityAnswer))
    {
      var uxm = ISiaaUser.ResetUsernameWithToken(UXM.PassWord, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = ISiaaUser.NotifyUsernameWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      PdpPrcMvcAddErrors(uxm.FormMessage);
    }
    else { PdpPrcMvcAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file