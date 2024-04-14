// NpdsDataRazorViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorViewControllerBase : CoreDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(NexusDataRazorViewControllerBase);

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = WRACE for user config settings and web api requests
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

  protected ILogger InitLoggerNexus(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataRazorViewControllerBase>();
    return logger;
  }
#if DEBUG
  protected void CatchNullNexus(string methodName = "", string className = "")
  {
    PNDC.CatchNullObject(PndcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullNexus)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PNDC DatabaseType: {PNDC.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PNDC DatabaseConstr: {PNDC.NPDSDC.DatabaseConstr}");
  }
  protected void DebugNexusRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugNexusRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{WRACE.SearchFilter}/{WRACE.ServiceTag}/{WRACE.ServiceType}/{WRACE.EntityType}/{WRACE.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"WRACE NexusDbconstr: {WRACE.DbcnstrNexus}");
    Debug.WriteLine($"WRACE DatabaseConstr: {WRACE.DatabaseConstr}");
    Debug.WriteLine($"PNDC DatabaseType: {PNDC?.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PNDC DatabaseConstr: {PNDC?.NPDSDC.DatabaseConstr}");
  }
#endif

  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetNexusRepository);
    CatchNullWrace(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (WRACE.DatabaseType != NPDSCD.DatabaseTypeNexus)
    { WRACE.DatabaseType = NPDSCD.DatabaseTypeNexus; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { WRACE.DbcnstrNexus = dbcs; }
    // reset NPDS data context with current QEB User Rest Context
    pdpNexusDataCntxt = new NexusDbsqlContext((INpdscwClient)WRACE);
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
  protected override void CloseAllConnections()
  {
    qebiUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
    pdpNexusDataCntxt?.DbsqlDisconnect();
  }

  // ATTN: assure that these controllers create both the
  // public WRACE with private qebiUserRestCntxt
  // public QUDC with private qebiUserDataCntxt

  public NexusDataRazorViewControllerBase()
  {
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }
  public NexusDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }
  public NexusDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }

} // end class

// end file