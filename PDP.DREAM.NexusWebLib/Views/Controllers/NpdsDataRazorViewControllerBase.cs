// NexusDataRazorViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorViewControllerBase : CoreDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(NexusDataRazorViewControllerBase);

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // PDP Nexus Data Context (PNDC)
  protected const string PndcKey = nameof(PNDC);
  protected NexusDbsqlContext? pdpNexusDataCntxt;
  public NexusDbsqlContext? PNDC
  {
    get { return pdpNexusDataCntxt; }
  }

  protected void CatchNullNexus(string methodName = "", string className = "")
  {
    PNDC.CatchNullObject(PndcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullNexus)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PNDC DatabaseType: {PNDC.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PNDC DatabaseConstr: {PNDC.NPDSCP.DatabaseConstr}");
  }
  protected void DebugNexusRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugNexusRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{QURC.SearchFilter}/{QURC.ServiceTag}/{QURC.ServiceType}/{QURC.EntityType}/{QURC.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"QURC NexusDbconstr: {QURC.NexusDbconstr}");
    Debug.WriteLine($"QURC DatabaseConstr: {QURC.DatabaseConstr}");
    Debug.WriteLine($"PNDC DatabaseType: {PNDC?.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PNDC DatabaseConstr: {PNDC?.NPDSCP.DatabaseConstr}");
  }

  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetNexusRepository);
    CatchNullQurc(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (QURC.DatabaseType != NpdsDatabaseType.Nexus)
    { QURC.DatabaseType = NpdsDatabaseType.Nexus; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { QURC.NexusDbconstr = dbcs; }
    // reset viewdata with current QEB User Rest Context
    ViewData[QurcKey] = QURC; // TODO: where used? where required? for Views not Pages?
    // reset NPDS data context with current QEB User Rest Context
    pdpNexusDataCntxt = new NexusDbsqlContext((INpdsClient)QURC);
    // open connection if switched on
    if (openCnctn)
    {
      pdpNexusDataCntxt.DbsqlConnect();
    }
#if DEBUG
    CatchNullNexus(rzrMethod, rzrClass);
#endif
  }
  protected void OpenNexusConnection(bool resetRepo = false)
  {
    if (resetRepo) { ResetNexusRepository(false); }
    pdpNexusDataCntxt.DbsqlConnect();
  }
  protected void CloseNexusConnection()
  {
    pdpNexusDataCntxt.DbsqlDisconnect();
  }
  protected virtual void CloseAllConnections()
  {
    qebUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
    pdpNexusDataCntxt?.DbsqlDisconnect();
  }

  public NexusDataRazorViewControllerBase()
  {
    qebUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebUserRestCntxt);
  }

  //public NexusDataRazorViewControllerBase(QebiDbsqlContext userCntxt)
  //{
  //  qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
  //  qebUserRestCntxt = InitRestContext();
  //  qebUserDataCntxt = userCntxt;
  //  pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NexusDbconstr);
  //}
  //public NexusDataRazorViewControllerBase(NexusDbsqlContext npdsCntxt)
  //{
  //  qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
  //  qebUserRestCntxt = InitRestContext();
  //  qebUserDataCntxt = new QebiDbsqlContext();
  //  pdpNexusDataCntxt = npdsCntxt;
  //}
  //public NexusDataRazorViewControllerBase(QebiDbsqlContext userCntxt, NexusDbsqlContext npdsCntxt)
  //{
  //  qebLogger = InitLogger<NexusDataRazorViewControllerBase>();
  //  qebUserRestCntxt = InitRestContext();
  //  qebUserDataCntxt = userCntxt;
  //  pdpNexusDataCntxt = npdsCntxt;
  //}
  //public NexusDataRazorViewControllerBase(QebiDbsqlContext userCntxt,
  //  IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  //{
  //  qebLogger = InitLogger<NexusDataRazorViewControllerBase>(lgrFtry);
  //  qebUserRestCntxt = InitRestContext();
  //  qebUserDataCntxt = userCntxt;
  //  qebEmailSender = emlSndr;
  //  qebSmsSender = smsSndr;
  //  pdpNexusDataCntxt = new NexusDbsqlContext(NPDSSD.NexusDbconstr);
  //}
  //public NexusDataRazorViewControllerBase(QebiDbsqlContext userCntxt, NexusDbsqlContext npdsCntxt,
  //  IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  //{
  //  qebLogger = InitLogger<NexusDataRazorViewControllerBase>(lgrFtry);
  //  qebUserRestCntxt = InitRestContext();
  //  qebUserDataCntxt = userCntxt;
  //  qebEmailSender = emlSndr;
  //  qebSmsSender = smsSndr;
  //  pdpNexusDataCntxt = npdsCntxt;
  //}

} // end class

// end file