// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetUsername2 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetUsername2);
  public AnonModeResetUsername2() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepAnonModeResetUsername2, $"{PDPSS.AppOwnerNameShort}: Reset Username");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string password)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm2(password);
    var usr = QUDC.GetUserByPassWord(UXM.PassWord);
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
      WraceUxmAddErrors(uxm.FormMessage);
    }
    else { WraceUxmAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file