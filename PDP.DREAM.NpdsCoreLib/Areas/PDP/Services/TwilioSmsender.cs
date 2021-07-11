// TwilioSmsender.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  public class TwilioSmsender : ISmsSender
  {
    Task ISmsSender.SendSmsAsync(string number, string message)
    {
      throw new NotImplementedException();
    }

  }

}
