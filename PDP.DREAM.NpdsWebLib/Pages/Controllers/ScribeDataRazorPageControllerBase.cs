// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class ScribeDataRazorPageControllerBase : NexusDataRazorPageControllerBase
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(ScribeDataRazorPageControllerBase);
 // BingMaps service in controllers, not in dbcontexts
  protected readonly BingMapsService? bingMaps;

  public ScribeDataRazorPageControllerBase()
  {
    bingMaps = new BingMapsService(new HttpClient());
    npdscw = InitClientWrace();
  }
  public ScribeDataRazorPageControllerBase(ILoggerFactory lgrFtry)
  {
    bingMaps = new BingMapsService(new HttpClient());
    qebLogger = InitLoggerScribe(lgrFtry);
    npdscw = InitClientWrace();
  }
  public ScribeDataRazorPageControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    bingMaps = new BingMapsService(new HttpClient());
    qebLogger = InitLoggerScribe(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    npdscw = InitClientWrace();
  }

  protected ILogger InitLoggerScribe(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<ScribeDataRazorPageControllerBase>();
    return logger;
  }

  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuted), rzrClass);
#endif
    NPDSCW.CloseScribeConnection();
  }

} // end class

// end file