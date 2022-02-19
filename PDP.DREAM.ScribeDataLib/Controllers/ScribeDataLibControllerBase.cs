// ScribeDataLibControllerBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class ScribeDataLibControllerBase : QebIdentityControllerBase
{
  // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
  // PDP REST Context = PRC for app config settings and web api requests
  // PDP User Context = PUC for secure user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  // PDP Scribe Data Context = PSDC
  protected ScribeDbsqlContext pdpScribeDataCntxt;
  public ScribeDbsqlContext PSDC
  {
    get { return pdpScribeDataCntxt; }
    set { pdpScribeDataCntxt = value; ResetScribeRepository(); }
  }
  // reset repository with current PDP Rest Context and current PDP Data Context
  // protected so not visible as public action for controller routes
  protected void ResetScribeRepository()
  {
    // reset repository with current PDP Rest Context and current PDP Data Context
    if (PRC == null) { throw new NullReferenceException("PDP REST Context"); }
    if (PSDC != null) { pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt); }
  }

  public ScribeDataLibControllerBase()
  {
    qebUserContext = new QebIdentityContext(npdsSrvcDefs.NpdsUserDbconstr);
    pdpScribeDataCntxt = new ScribeDbsqlContext(npdsSrvcDefs.NpdsRegistrarDbconstr);
  }

  public ScribeDataLibControllerBase(QebIdentityContext userCntxt)
  {
    qebUserContext = userCntxt;
    pdpScribeDataCntxt = new ScribeDbsqlContext(npdsSrvcDefs.NpdsRegistrarDbconstr);
  }
  public ScribeDataLibControllerBase(ScribeDbsqlContext npdsCntxt)
  {
    qebUserContext = new QebIdentityContext(npdsSrvcDefs.NpdsUserDbconstr);
    pdpScribeDataCntxt = npdsCntxt;
  }
  public ScribeDataLibControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt)
  {
    qebUserContext = userCntxt;
    pdpScribeDataCntxt = npdsCntxt;
  }
  public ScribeDataLibControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserContext = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpScribeDataCntxt = new ScribeDbsqlContext(npdsSrvcDefs.NpdsRegistrarDbconstr);
  }

  public ScribeDataLibControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebUserContext = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebLogger = InitLogger(lgrFtry);
    pdpScribeDataCntxt = npdsCntxt;
  }

  protected ILogger InitLogger(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<ScribeDataLibControllerBase>();
    return logger;
  }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    // new PdpRestContext() calls ParseQueryCollection
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsConst.DatabaseType.Scribe,
      DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsConst.RecordAccess.User,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
  }

  public bool CheckClientAgentSession()
  {
    var pdpSignin = new PdpIdentityResult();
    var sessionIsIdentified = false;
    var agentIsVerified = false;
    if (PRC.AuthorizedClientIsRequired) // depends on the PRC.RecordAccess setting
    {
      if (OnlineUserIsAuthenticated)
      {
        sessionIsIdentified = PSDC.CheckPdpAgentSession(ref pdpRestCntxt);
      }
      else if (!string.IsNullOrEmpty(PRC.UserName) && !string.IsNullOrEmpty(PRC.PassWord))
      {
        pdpSignin = QebUserSignin(PRC.UserName, PRC.PassWord);
        sessionIsIdentified = PSDC.CheckPdpAgentSession(ref pdpRestCntxt);
      }
      else if (PRC.SessionValueIsRequired && !PRC.SessionGuid.IsInvalid())
      {
        sessionIsIdentified = PSDC.CheckPdpAgentSession(ref pdpRestCntxt);
        pdpSignin = QebUserSignin(PRC.UserGuid, PRC.AgentGuid, PRC.SessionGuid);
      }
      if (sessionIsIdentified)
      {
        agentIsVerified = PRC.ClientIsVerified;
      }
      ResetRecordAccess();
    }
#if DEBUG
    UserGuidDevTest();
#endif
    return agentIsVerified;
  }

} // class
