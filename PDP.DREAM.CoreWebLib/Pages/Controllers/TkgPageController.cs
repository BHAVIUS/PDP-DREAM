// TkgPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// Telerik Kendo Grid Core (TKGC) PageController
public partial class TkgcPageController : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(TkgcPageController);
  public TkgcPageController() { }
  public TkgcPageController(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

} // end class

// end file