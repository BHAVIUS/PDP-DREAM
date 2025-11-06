// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeContactSite : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeContactSite);
  public QebiAnonModeContactSite() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeContactSite, "Contact Site", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
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
      var subj = PDPSS.AppOwnerNameLong + " Contact " + name;
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
      var mail = UXM.EmailAddress ?? PDPSS.AppHostEmail;
      UXM.FormCompleted = QebNotifyService.SendEmail(mail, subj, body.ToString());
      if (!UXM.FormCompleted) { WraceAddErrors("Your message could not be sent. Please try again later."); }
    }
    else { WraceAddErrors("Submitted form not valid. "); }
    return Page();
  }

} // end class

// end file