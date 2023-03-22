// TkgPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// Telerik Kendo Grid Nexus (TKGN) PageController
public partial class TkgnPageController : NexusDataRazorPageControllerBase
{
  private const string rzrClass = nameof(TkgnPageController);
  public TkgnPageController() { }
  public TkgnPageController(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

} // end class

// end file