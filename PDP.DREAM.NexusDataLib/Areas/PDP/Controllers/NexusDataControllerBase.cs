// NexusDataControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Controllers;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NexusDataLib.Controllers
{
  // convention: name abstract controllers with suffix ControllerBase
  public abstract class NexusDataControllerBase : NpdsCoreControllerBase
  {
    // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
    // PDP REST Context = PRC for app config settings and web api requests
    // PDP User Context = PUC for secure user identification, authentication, authorization
    // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
    // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
    // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

   public NexusDbsqlContext PNDC
    {
      get { return pdpNexusDataCntxt; }
      set { pdpNexusDataCntxt = value; ResetNexusRepository(); }
    }
    protected NexusDbsqlContext pdpNexusDataCntxt;
    // reset repository with current PDP Rest Context and current PDP Data Context
    // protected so not visible as public action for controller routes
    protected void ResetNexusRepository()
    {
      if (pdpRestCntxt == null) { throw new NullReferenceException("PDP REST Context"); }
      if (pdpNexusDataCntxt != null) { pdpNexusDataCntxt.SetRestContext(ref pdpRestCntxt); }
    }

    public NexusDataControllerBase()
    {
      pdpNexusDataCntxt = new NexusDbsqlContext(npdsSrvcDefs.NpdsDiristryDbconstr);
    }

    public NexusDataControllerBase(NexusDbsqlContext npdsCntxt)
    {
      pdpNexusDataCntxt = npdsCntxt;
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

  } // class

} // namespace
