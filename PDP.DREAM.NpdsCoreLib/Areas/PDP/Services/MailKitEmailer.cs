using System;
using System.Diagnostics;
using System.Threading.Tasks;

using PDP.DREAM.NpdsCoreLib.Models;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  public class MailKitEmailer : IEmailSender
  {
    Task IEmailSender.SendEmailAsync(string toEmail, string msgSubject, string msgBody)
    {
      // return Task.CompletedTask;
      var fromEmail = string.Empty;
      return Execute(fromEmail, toEmail, msgSubject, msgBody);
    }
    public Task SendEmailAsync(string fromEmail, string toEmail, string msgSubject, string msgBody)
    {
      return Execute(fromEmail, toEmail, msgSubject, msgBody);
    }
    public Task Execute(string companyEmail, string customerEmail, string msgSubject, string msgBody)
    {
      throw new NotImplementedException();
    }

  }

}
