// MailKitEmailer.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Threading.Tasks;

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
