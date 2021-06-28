using System;
using System.Collections.Generic;
using System.Linq;
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
