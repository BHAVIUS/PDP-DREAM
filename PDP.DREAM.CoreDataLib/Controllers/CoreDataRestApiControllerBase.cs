// NpdsCoreControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.CoreDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class CoreDataRestApiControllerBase : PdpPrcRestApiControllerBase
{
  // PDP Site Settings (PDPSS) and NPDS Service Defaults (NPDSSD)

  protected readonly PdpSiteSettings pdpSiteSets = PdpSiteSettings.Values;
  public PdpSiteSettings PDPSS { get { return pdpSiteSets; } }

  protected readonly NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.Values;
  public NpdsServiceDefaults NPDSSD { get { return npdsSrvcDefs; } }

  // data contexts: REST Context, User Identity, and NPDS Data/Metadata
  // PDP REST Context = PRC for app config settings and web api requests
  // PDP User Context = PUC for secure user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  protected CoreDbsqlContext pdpCoreDataCntxt;
  public CoreDbsqlContext PCDC
  {
    get { return pdpCoreDataCntxt; }
    set { pdpCoreDataCntxt = value; ResetCoreRepository(); }
  }

  // reset repository with current PDP Rest Context and current PDP Data Context
  // protected so not visible as public action for controller routes
  protected void ResetCoreRepository()
  {
    if (pdpRestCntxt == null) { throw new NullReferenceException("PDP REST Context"); }
    if (pdpCoreDataCntxt != null) { pdpCoreDataCntxt.SetRestContext(ref pdpRestCntxt); }
  }

  public CoreDataRestApiControllerBase()
  {
    pdpCoreDataCntxt = new CoreDbsqlContext(npdsSrvcDefs.NpdsCoreDbconstr);
  }
  public CoreDataRestApiControllerBase(CoreDbsqlContext coreCntxt)
  {
    pdpCoreDataCntxt = coreCntxt;
  }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request)  // calls ParseQueryCollection on new()
    {
      DatabaseType = NpdsConst.DatabaseType.Core,
      DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsConst.RecordAccess.Client,
      ClientInUserModeIsRequired = false,
      SessionValueIsRequired = false
    };
    ResetCoreRepository();
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(CoreDataRestApiControllerBase)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    ViewData["PRC"] = pdpRestCntxt;
  }

} // class
