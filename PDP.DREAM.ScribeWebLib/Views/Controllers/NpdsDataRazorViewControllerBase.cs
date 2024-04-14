// NpdsDataRazorViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class ScribeDataRazorViewControllerBase : NexusDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(ScribeDataRazorViewControllerBase);

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = WRACE for user config settings and web api requests
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // PDP Scribe Data Context (PSDC)
  protected const string PsdcKey = nameof(PSDC);
  protected ScribeDbsqlContext? pdpScribeDataCntxt = null;
  public ScribeDbsqlContext? PSDC
  {
    get { return pdpScribeDataCntxt; }
  }

  protected ILogger InitLoggerScribe(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<ScribeDataRazorViewControllerBase>();
    return logger;
  }
  protected void CatchNullScribe(string methodName = "", string className = "")
  {
    PSDC.CatchNullObject(PndcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullScribe)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PSDC DatabaseType: {PSDC.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PSDC DatabaseConstr: {PSDC.NPDSDC.DatabaseConstr}");
  }
  protected void DebugScribeRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugScribeRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{WRACE.SearchFilter}/{WRACE.ServiceTag}/{WRACE.ServiceType}/{WRACE.EntityType}/{WRACE.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"WRACE ScribeDbconstr: {WRACE.DbcnstrScribe}");
    Debug.WriteLine($"WRACE DatabaseConstr: {WRACE.DatabaseConstr}");
    Debug.WriteLine($"PSDC DatabaseType: {PSDC?.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PSDC DatabaseConstr: {PSDC?.NPDSDC.DatabaseConstr}");
  }

  // protected so not visible as public action for controller routes
  protected void ResetScribeRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetScribeRepository);
    CatchNullWrace(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (WRACE.DatabaseType != NPDSCD.DatabaseTypeScribe)
    { WRACE.DatabaseType = NPDSCD.DatabaseTypeScribe; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { WRACE.DbcnstrScribe = dbcs; }
    // reset NPDS data context with current QEB User Rest Context
    pdpScribeDataCntxt = new ScribeDbsqlContext((INpdscwClient)WRACE);
    // open connection if switched on
    if (openCnctn)
    {
      pdpScribeDataCntxt.DbsqlConnect();
    }
#if DEBUG
    CatchNullScribe(rzrMethod, rzrClass);
#endif
  }
  protected void OpenScribeConnection(bool resetRepo = false)
  {
    if (resetRepo) { ResetScribeRepository(false); }
    pdpScribeDataCntxt.DbsqlConnect();
  }
  protected void CloseScribeConnection()
  {
    pdpScribeDataCntxt.DbsqlDisconnect();
  }
  protected override void CloseAllConnections()
  {
    qebiUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
    pdpNexusDataCntxt?.DbsqlDisconnect();
    pdpScribeDataCntxt?.DbsqlDisconnect();
  }

  // ATTN: assure that these controllers create both the
  // public WRACE with private qebiUserRestCntxt
  // public QUDC with private qebiUserDataCntxt

  public ScribeDataRazorViewControllerBase()
  {
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerScribe(lgrFtry);
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerScribe(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    wrace = InitWrace().SetDatabaseType(NPDSCD.DatabaseTypeQEBI);
    qebiUserDataCntxt = new QebiDalContext((INpdscwClient)wrace);
  }


} // end class

// end file
