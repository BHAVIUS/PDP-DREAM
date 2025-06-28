// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorViewControllerBase : CoreDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(NexusDataRazorViewControllerBase);

  public NexusDataRazorViewControllerBase()
  {
    npdscw = InitClientWrace();
  }
  public NexusDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    npdscw = InitClientWrace();
  }
  public NexusDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    npdscw = InitClientWrace();
  }

  protected ILogger InitLoggerNexus(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataRazorViewControllerBase>();
    return logger;
  }

} // end class

// end file