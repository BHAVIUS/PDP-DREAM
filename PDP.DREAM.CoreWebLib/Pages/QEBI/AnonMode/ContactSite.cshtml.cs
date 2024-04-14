// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeContactSite : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeContactSite);
  public AnonModeContactSite() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepAnonModeContactSite, $"{PDPSS.AppOwnerNameShort}: Contact Site");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string pageTitle = "")
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(pageTitle))
    { PSRM.RazorBodyTitle = pageTitle; }
    UXM = new ContactSiteUxm(PSRM.RazorBodyTitle);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  [BindProperty]
  public ContactSiteUxm UXM { get; set; } = new ContactSiteUxm();

  public IActionResult OnPost()
  {
    if (!string.IsNullOrWhiteSpace(UXM.FormTitle))
    { PSRM.RazorBodyTitle = UXM.FormTitle; }
    if (ModelState.IsValid)
    {
      var name = UXM.FirstName + " " + UXM.LastName;
      var subj = PdpAppStatus.PDPSS.AppOwnerNameLong + " Contact " + name;
      var body = new StringBuilder();
      body.AppendLine("Name: " + name);
      body.AppendLine();
      body.AppendLine("Phone: " + UXM.PhoneNumber);
      body.AppendLine("Email: " + UXM.EmailAddress);
      body.AppendLine("Website: " + UXM.WebsiteAddress);
      body.AppendLine("Organization: " + UXM.Organization);
      body.AppendLine();
      body.AppendLine("Subject: " + UXM.EmailSubject);
      body.AppendLine("Message: " + UXM.EmailBody);
      body.AppendLine();
      var mail = UXM.EmailAddress ?? PdpAppStatus.PDPSS.AppHostEmail;
      UXM.FormCompleted = QebNotifyService.SendEmail(mail, subj, body.ToString());
      if (!UXM.FormCompleted) { WraceUxmAddErrors("Your message could not be sent. Please try again later."); }
    }
    else { WraceUxmAddErrors("Submitted form not valid. "); }
    return Page();
  }

} // end class

// end file