// IMessageServices.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  public interface ISmsSender
  {
    Task SendSmsAsync(string number, string message);
  }

  public interface IEmailSender
  {
    Task SendEmailAsync(string email, string subject, string message);
  }

}
