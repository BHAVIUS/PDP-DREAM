// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class ScribeDataRazorViewControllerBase : NexusDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(ScribeDataRazorViewControllerBase);
 // BingMaps service in controllers, not in dbcontexts;
 // TODO: required by Scribe, not currently used by Nexus, what about for DOORS?
 // TODO: alternative geolocator service
  protected readonly BingMapsService? bingMaps;

  public ScribeDataRazorViewControllerBase()
  {
    bingMaps = new BingMapsService(new HttpClient());
    npdscw = InitClientWrace();
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    bingMaps = new BingMapsService(new HttpClient());
    qebLogger = InitLoggerScribe(lgrFtry);
    npdscw = InitClientWrace();
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    bingMaps = new BingMapsService(new HttpClient());
    qebLogger = InitLoggerScribe(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    npdscw = InitClientWrace();
  }

  protected ILogger InitLoggerScribe(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<ScribeDataRazorViewControllerBase>();
    return logger;
  }

} // end class

// end file
