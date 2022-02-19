// IMessageServices.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Threading.Tasks;

namespace PDP.DREAM.CoreDataLib.Services;

public interface ISmsSender
{
  Task SendSmsAsync(string number, string message);
}

public interface IEmailSender
{
  Task SendEmailAsync(string email, string subject, string message);
}
