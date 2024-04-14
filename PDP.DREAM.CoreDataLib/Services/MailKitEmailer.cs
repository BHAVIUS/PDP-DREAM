// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Services;

public class MailKitEmailer : IEmailSender
{
  // https://www.nuget.org/packages/MailKit/#readme-body-tab
  // https://github.com/jstedfast/MailKit
  // http://www.mimekit.net/

  Task IEmailSender.SendEmailAsync(string toEmail, string msgSubject, string msgBody)
  {
    // return Task.CompletedTask;
    var fromEmail = ESS;
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
