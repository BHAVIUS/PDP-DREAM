// NexusDataLibControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRestApiControllerBase : CoreDataRazorViewControllerBase
{
  // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
  // PDP REST Context = PRC for app config settings and web api requests
  // PDP User Context = PUC for secure user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  // PDP Nexus Data Context = PNDC
  protected NexusDbsqlContext pdpNexusDataCntxt;
  public NexusDbsqlContext PNDC
  {
    get { return pdpNexusDataCntxt; }
    set { pdpNexusDataCntxt = value; ResetNexusRepository(); }
  }
  // reset repository with current PDP Rest Context and current PDP Data Context
  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository()
  {
    // reset repository with current PDP Rest Context and current PDP Data Context
    if (QURC == null) { throw new NullReferenceException("PDP REST Context"); }
    if (PNDC != null) { pdpNexusDataCntxt.ResetRestContext(QURC); }
  }

  public NexusDataRestApiControllerBase()
  {
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }

  public NexusDataRestApiControllerBase(QebIdentityContext userCntxt)
  {
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }
  public NexusDataRestApiControllerBase(NexusDbsqlContext npdsCntxt)
  {
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRestApiControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt)
  {
    qebUserDataCntxt = userCntxt;
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataRestApiControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NpdsDiristryDbconstr);
  }

  public NexusDataRestApiControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpNexusDataCntxt = npdsCntxt;
  }

  protected ILogger InitLogger(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataRestApiControllerBase>();
    return logger;
  }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // new PdpRestContext() calls ParseQueryCollection
    qebUserRestCntxt = new QebUserRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsDatabaseType.Nexus,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      QebSessionValueIsRequired = false
    };
  }

} // end class

// end file