// NexusDataRazorPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorPageControllerBase : CoreDataRazorPageControllerBase
{
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
    set {
      value.CatchNullObject(nameof(pdpNexusDataCntxt), nameof(PNDC), nameof(NexusDataRazorPageControllerBase));
      pdpNexusDataCntxt = value;
      ResetNexusRepository();
    }
    get {
      // if (pdpNexusDataCntxt == null) { pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr); }
      pdpNexusDataCntxt.CatchNullObject(nameof(pdpNexusDataCntxt), nameof(PNDC), nameof(NexusDataRazorPageControllerBase));
      return pdpNexusDataCntxt;
    }
  }

  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository()
  {
    // reset ViewData with current QEB User Rest Context
    QURC.CatchNullObject(nameof(QURC), nameof(ResetNexusRepository), nameof(NexusDataRazorPageControllerBase));
    ViewData[nameof(QURC)] = QURC;
    // reset PDP Nexus Data Context with current QEB User Rest Context
    PNDC.CatchNullObject(nameof(PNDC), nameof(ResetNexusRepository), nameof(NexusDataRazorPageControllerBase));
    pdpNexusDataCntxt.ResetQebiContext(QURC); // resets data connection with current QURC.DbConnectionString
  }

  public NexusDataRazorPageControllerBase()
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }

  public NexusDataRazorPageControllerBase(QebIdentityContext userCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }
  public NexusDataRazorPageControllerBase(NexusDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRazorPageControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRazorPageControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }
  public NexusDataRazorPageControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpNexusDataCntxt = npdsCntxt;
  }

  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC.CatchNullObject(nameof(QURC), nameof(OnPageHandlerExecuting), nameof(NexusDataRazorPageControllerBase));
    // PSR.RazorAreaName = DepNpdsPath;
    // PSR.RazorControllerName = nameof(NexusDataRazorPageControllerBase);
    ViewData[nameof(QURC)] = QURC;
  }

} // end class

// end file