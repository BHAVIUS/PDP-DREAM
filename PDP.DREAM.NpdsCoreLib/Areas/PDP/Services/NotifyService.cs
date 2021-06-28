using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  // static classes cannot implement interfaces
  public static class NotifyService
  {

    // TODO: consider MailKit https://www.mailkit.com/
    // TODO: consider MailGun https://www.mailgun.com/
    // TODO: consider Postal https://github.com/postalhq/postal

    public static bool SendEmail(string toEmailAddress, string msgSubject, string msgBody)
    {
      bool emailSent;
      if (PdpSiteSettings.GetValues.AppUseSendGrid)
      {
        emailSent = SendgridService.SendEmail(PdpSiteSettings.GetValues.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
      }
      else
      {
        emailSent = SmtpmailService.SendEmail(PdpSiteSettings.GetValues.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
      }
      return emailSent;
    }

  }

}