// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorPageControllerBase : CoreDataRazorPageControllerBase
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(NexusDataRazorPageControllerBase);

  public NexusDataRazorPageControllerBase()
  {
    npdscw = InitClientWrace();
  }
  public NexusDataRazorPageControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    npdscw = InitClientWrace();
  }
  public NexusDataRazorPageControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    npdscw = InitClientWrace();
  }

  protected ILogger InitLoggerNexus(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataRazorPageControllerBase>();
    return logger;
  }

  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuted), rzrClass);
#endif
    NPDSCW.CloseNexusConnection();
  }

} // end class

// end file