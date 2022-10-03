// SmtpmailService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text.Encodings.Web;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Services;

public static class SmtpmailService
{
  public static bool SendEmail(string fromEmail, string toEmail, string subject, string body)
  {
    var response = SendEmail(fromEmail, toEmail, subject, body, false, false);
    return response;
  }
  public static bool SendEmail(string companyEmail, string customerEmail, string msgSubject, string msgBody, bool isHtml, bool encode)
  {
    try
    {
      string adminEmail, adminUsername, adminPassword, smtpIpadrstring;
      int smtpPortnumber;
      bool useSsl, useDfltCred;
      
      // smtpIpadrstring = "smtpIpadrstring";
      smtpIpadrstring = "208.91.196.40"; // smtpout.secureserver.net 465 with SSL
      // smtpPortnumber = 25;
      smtpPortnumber = 465;
      // useSsl = false;
      useSsl = true;
      useDfltCred = false;
      // adminUsername = "adminUsername";
      adminUsername = "admin@akeakamai.net";
      // adminPassword = "adminPassword";
      adminPassword = "91sipapu19";

      adminEmail = PdpAppStatus.PDPSS.AppHostEmail;
      companyEmail = (companyEmail ?? PdpAppStatus.PDPSS.AppOwnerEmail);
      customerEmail = (customerEmail ?? adminEmail);

      var adminAddress = new MailAddress(adminEmail);
      var companyAddress = new MailAddress(companyEmail);
      var customerAddress = new MailAddress(customerEmail);

      using (var msg = new MailMessage())
      {
        msg.From = adminAddress;
        msg.To.Add(customerAddress);
        msg.CC.Add(companyAddress);
        // msg.Bcc.Add(adminAddress);
        if (isHtml || encode)
        {
          msg.Subject = HtmlEncoder.Default.Encode(msgSubject);
          msg.Body = HtmlEncoder.Default.Encode(msgBody);
          msg.IsBodyHtml = true;
        }
        else
        {
          msg.Subject = msgSubject;
          msg.Body = msgBody;
          msg.IsBodyHtml = false;
        }

        using (var clt = new SmtpClient(smtpIpadrstring, smtpPortnumber))
        {
          clt.DeliveryMethod = SmtpDeliveryMethod.Network;
          clt.Timeout = 11111; // default is 100000 millisecs (= 100 secs)
          clt.Host = smtpIpadrstring;
          clt.Port = smtpPortnumber;
          clt.EnableSsl = useSsl;
          clt.UseDefaultCredentials = useDfltCred;
          if (!useDfltCred)
          {
            var crd = new System.Net.NetworkCredential(adminUsername, adminPassword);
            clt.Credentials = crd;  // web transport client with credentials
          }
          clt.Send(msg);
        }
      }
      return true;
    }
    catch (SmtpException err)
    {
      Debug.WriteLine(err.Message);
      if (err.InnerException != null) { Debug.WriteLine(err.InnerException.Message); }
      return false;
    }
  }
}

public class WebmailMessage
{
  public WebmailMessage() { }

  public string CCList { get; set; } = string.Empty;
  public string FromEmail { get; set; } = string.Empty;
  public string FromName { get; set; } = string.Empty;
  public bool IsHtml { get; set; } = false;
  public string MessageBody { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string ToList { get; set; } = string.Empty;

  public bool SendEmail()
  {
    return false;
  }

  public static string BuildMessageBody(string userName, string Password, string filePath)
  {
    string text = string.Empty;
    FileInfo fi = new FileInfo(filePath);
    if (fi.Exists)
    {
      using (StreamReader sr = fi.OpenText())
      {
        text = sr.ReadToEnd();
      }
      text = text.Replace("%UserName%", userName);
      text = text.Replace("%Password%", Password);
    }
    return text;
  }

}

