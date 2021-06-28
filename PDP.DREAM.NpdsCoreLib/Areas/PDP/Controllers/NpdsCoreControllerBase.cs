using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  // convention: name abstract controllers with suffix ControllerBase
  public abstract class NpdsCoreControllerBase : PdpPrcControllerBase
  {
    protected readonly PdpSiteSettings pdpSiteSets = PdpSiteSettings.GetValues;
    protected readonly NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.GetValues;

    // 3 main contexts: REST Context, User Identity, and NPDS Data/Metadata
    // PDP REST Context = PRC for app config settings and web api requests
    // PDP User Context = PUC for secure user identification, authentication, authorization
    // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
    // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
    // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

    public CoreDbsqlContext PCDC
    {
      get { return pdpCoreDataCntxt; }
      set { pdpCoreDataCntxt = value; ResetCoreRepository(); }
    }
    protected CoreDbsqlContext pdpCoreDataCntxt;
    // reset repository with current PDP Rest Context and current PDP Data Context
    // protected so not visible as public action for controller routes
    protected void ResetCoreRepository()
    {
      if (pdpRestCntxt == null) { throw new NullReferenceException("PDP REST Context"); }
      if (pdpCoreDataCntxt != null) { pdpCoreDataCntxt.SetRestContext(ref pdpRestCntxt); }
    }

    public NpdsCoreControllerBase()
    {
      pdpCoreDataCntxt = new CoreDbsqlContext(npdsSrvcDefs.NpdsCoreDbconstr);
    }
    public NpdsCoreControllerBase(CoreDbsqlContext coreCntxt)
    {
      pdpCoreDataCntxt = coreCntxt;
    }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpHttpCntxt = oaeCntxt.HttpContext;
      pdpHttpReqst = pdpHttpCntxt.Request;
      pdpRestCntxt = new PdpRestContext(pdpHttpReqst)  // calls ParseQueryCollection on new()
      {
        DatabaseType = NpdsConst.DatabaseType.Core,
        DatabaseAccess = NpdsConst.DatabaseAccess.AnonReadOnly,
        RecordAccess = NpdsConst.RecordAccess.Client
      };
    }
    public virtual IActionResult Index() { return View(); }
    public virtual IActionResult Help() { return View(); }
    public virtual IActionResult Privacy() { return View(); }

  }

}
