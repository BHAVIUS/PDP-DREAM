// NexusDataRazorViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorViewControllerBase : CoreDataRazorViewControllerBase
{
  // ranp = Route App NamePrefix
  // public const string NdlRanpView = "NexusDataLibView"; // by MVC view
  // public const string NrlRanpView = "NexusRestLibView"; // by MVC view
  public const string NwlRanpView = "NexusWebLibView"; // by MVC view

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  // PDP Nexus Data Context = PNDC
  protected NexusDbsqlContext pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  public NexusDbsqlContext PNDC
  {
    set { pdpNexusDataCntxt = value; ResetNexusRepository(); }
    get {
      if (pdpNexusDataCntxt == null) { pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr); }
      return pdpNexusDataCntxt;
    }
  }

  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository()
  {
    // reset repository with current QEB User Rest Context
    QURC.CatchNullObject(nameof(QURC), nameof(ResetNexusRepository), nameof(NexusDataRazorViewControllerBase));
    ViewData[nameof(QURC)] = QURC;
    // reset repository with current PDP Nexus Data Context
    PNDC.CatchNullObject(nameof(PNDC), nameof(ResetNexusRepository), nameof(NexusDataRazorViewControllerBase));
    pdpNexusDataCntxt.ResetQebiContext(QURC);
  }

  public NexusDataRazorViewControllerBase()
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }

  public NexusDataRazorViewControllerBase(QebIdentityContext userCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }
  public NexusDataRazorViewControllerBase(NexusDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRazorViewControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRazorViewControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }
  public NexusDataRazorViewControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<NexusDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpNexusDataCntxt = npdsCntxt;
  }


} // end class

// end file