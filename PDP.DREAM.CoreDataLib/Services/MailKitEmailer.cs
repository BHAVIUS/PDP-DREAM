// MailKitEmailer.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Services
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
