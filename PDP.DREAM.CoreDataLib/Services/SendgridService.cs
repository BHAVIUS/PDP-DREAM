// SendgridService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Diagnostics;

using PDP.DREAM.CoreDataLib.Models;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace PDP.DREAM.CoreDataLib.Services
{
  public static class SendgridService
  {
    public static bool SendEmail(string fromEmail, string toEmail, string subject, string body)
    {
      var result = SendEmail(fromEmail, toEmail, subject, body, false, false);
      return result;
    }
    public static bool SendEmail(string companyEmail, string customerEmail, string msgSubject, string msgBody, bool isHtml, bool encode)
    {
      var result = false;
      try
      {
        string adminEmail, adminApiKey;
        adminApiKey = PdpAppStatus.PDPSS.ApiKeySendGrid;
        adminEmail = PdpAppStatus.PDPSS.AppHostEmail;
        if (string.IsNullOrWhiteSpace(companyEmail)) { companyEmail = PdpAppStatus.PDPSS.AppOwnerEmail; }
        if (string.IsNullOrWhiteSpace(customerEmail)) { customerEmail = PdpAppStatus.PDPSS.AppOwnerEmail; }

        var companyAddress = new EmailAddress(companyEmail);
        var customerAddress = new EmailAddress(customerEmail);
        var adminAddress = new EmailAddress(adminEmail);
        // var msgMail = MailHelper.CreateSingleEmail(companyAddress, customerAddress, msgSubject, msgBody, msgBody);
        var msgMail = new SendGridMessage();
        //msgMail.MailSettings = new MailSettings();
        //msgMail.MailSettings.SandboxMode = new SandboxMode();
        //msgMail.MailSettings.SandboxMode.Enable = false;
        //msgMail.SetSandBoxMode(false);

        msgMail.SetFrom(adminAddress);
        msgMail.SetReplyTo(companyAddress);
        msgMail.AddTo(customerAddress);
        msgMail.AddCc(companyAddress); // combo up to here did work
                                       // msgMail.AddBcc(adminAddress); // adding Bcc caused a BadRequest
        msgMail.SetSubject(msgSubject);
        if (isHtml)
        {
          // do encoding ??
          msgMail.AddContent(MimeType.Html, msgBody);
          msgMail.HtmlContent = msgBody;
        }
        else
        {
          msgMail.AddContent(MimeType.Text, msgBody);
          msgMail.PlainTextContent = msgBody;
        }

        var sgc = new SendGridClient(adminApiKey);
        var request = sgc.SendEmailAsync(msgMail);
        var response = request.GetAwaiter().GetResult();
        var headers = response.Headers;
        var body = response.Body;
        var status = response.StatusCode;

        if (status == System.Net.HttpStatusCode.Accepted) { result = true; }
      }
      catch (Exception err)
      {
        Debug.WriteLine(err.Message);
        if (err.InnerException != null) { Debug.WriteLine(err.InnerException.Message); }
      }
      return result;
      // end SendEmail
    }
    // end SendgridService
  }
  // end namespace
}