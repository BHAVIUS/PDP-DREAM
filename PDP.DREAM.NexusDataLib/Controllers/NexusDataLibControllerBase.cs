// NexusDataControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataLibControllerBase : QebIdentityControllerBase
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
    if (PRC == null) { throw new NullReferenceException("PDP REST Context"); }
    if (PNDC != null) { pdpNexusDataCntxt.SetRestContext(ref pdpRestCntxt); }
  }

  public NexusDataLibControllerBase()
  {
    qebUserContext = new QebIdentityContext(npdsSrvcDefs.NpdsUserDbconstr);
    pdpNexusDataCntxt = new NexusDbsqlContext(npdsSrvcDefs.NpdsDiristryDbconstr);
  }

  public NexusDataLibControllerBase(QebIdentityContext userCntxt)
  {
    qebUserContext = userCntxt;
    pdpNexusDataCntxt = new NexusDbsqlContext(npdsSrvcDefs.NpdsDiristryDbconstr);
  }
  public NexusDataLibControllerBase(NexusDbsqlContext npdsCntxt)
  {
    qebUserContext = new QebIdentityContext(npdsSrvcDefs.NpdsUserDbconstr);
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataLibControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt)
  {
    qebUserContext = userCntxt;
    pdpNexusDataCntxt = npdsCntxt;
  }
  public NexusDataLibControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserContext = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpNexusDataCntxt = new NexusDbsqlContext(npdsSrvcDefs.NpdsDiristryDbconstr);
  }

  public NexusDataLibControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserContext = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpNexusDataCntxt = npdsCntxt;
  }

  protected ILogger InitLogger(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataLibControllerBase>();
    return logger;
  }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // new PdpRestContext() calls ParseQueryCollection
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Nexus,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
  }

} // class
