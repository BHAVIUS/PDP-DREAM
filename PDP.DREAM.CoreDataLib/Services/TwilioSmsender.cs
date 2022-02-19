// TwilioSmsender.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Threading.Tasks;

namespace PDP.DREAM.CoreDataLib.Services
{
  public class TwilioSmsender : ISmsSender
  {
    Task ISmsSender.SendSmsAsync(string number, string message)
    {
      throw new NotImplementedException();
    }

  }

}
