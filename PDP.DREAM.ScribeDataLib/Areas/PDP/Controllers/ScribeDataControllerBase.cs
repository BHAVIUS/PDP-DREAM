// ScribeDataControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeDataLib.Controllers
{
  // convention: name abstract controllers with suffix ControllerBase
  public abstract class ScribeDataControllerBase : NpdsCoreControllerBase
  {
    // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
    // PDP REST Context = PRC for app config settings and web api requests
    // PDP User Context = PUC for secure user identification, authentication, authorization
    // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
    // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
    // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

    public ScribeDbsqlContext PSDC
    {
      get { return pdpScribeDataCntxt; }
      set { pdpScribeDataCntxt = value; ResetScribeRepository(); }
    }
    protected ScribeDbsqlContext pdpScribeDataCntxt;
    // reset repository with current PDP Rest Context and current PDP Data Context
    // protected so not visible as public action for controller routes
    protected void ResetScribeRepository()
    {
      if (pdpRestCntxt == null) { throw new NullReferenceException("PDP REST Context"); }
      if (pdpScribeDataCntxt != null) { pdpScribeDataCntxt.SetRestContext(ref pdpRestCntxt); }
    }

    public ScribeDataControllerBase()
    {
      pdpScribeDataCntxt = new ScribeDbsqlContext(npdsSrvcDefs.NpdsRegistrarDbconstr);
    }

    public ScribeDataControllerBase(ScribeDbsqlContext npdsCntxt)
    {
      pdpScribeDataCntxt = npdsCntxt;
    }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpHttpCntxt = oaeCntxt.HttpContext;
      pdpHttpReqst = pdpHttpCntxt.Request;
      pdpRestCntxt = new PdpRestContext(pdpHttpReqst)  // calls ParseQueryCollection on new()
      {
        DatabaseType = NpdsConst.DatabaseType.Nexus,
        DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
        RecordAccess = NpdsConst.RecordAccess.Client
      };
    }

  }

}
