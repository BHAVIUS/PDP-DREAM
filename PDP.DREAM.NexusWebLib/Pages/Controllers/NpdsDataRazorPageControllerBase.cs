// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class NexusDataRazorPageControllerBase : CoreDataRazorPageControllerBase
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(NexusDataRazorPageControllerBase);

  // Web API REST Context = WRACE for user config settings and web api requests/responses
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // PDP Nexus Data Context (PNDC)
  protected const string PndcKey = nameof(PNDC);
  protected NexusDbsqlContext? pdpNexusDataCntxt = null;
  public NexusDbsqlContext? PNDC
  {
    get { return pdpNexusDataCntxt; }
  }

  public NexusDataRazorPageControllerBase()
  {
    wrace = InitWrace();
  }
  public NexusDataRazorPageControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    wrace = InitWrace();
  }
  public NexusDataRazorPageControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerNexus(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    wrace = InitWrace();
  }

  protected ILogger InitLoggerNexus(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<NexusDataRazorPageControllerBase>();
    return logger;
  }
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

  // TODO: refactor params and rebuild Reset*Repositories wrt Open*Connection
  // protected so not visible as public action for controller routes
  protected void ResetNexusRepository(bool openCnctn = false, bool buildDdlists = false, string? dbcs = "")
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
      if (buildDdlists)
      {
        BuildNexusDropDownLists();
      }
    }
    WRACE.DbciNexus = (INpdsDbsqlContext)pdpNexusDataCntxt;
#if DEBUG
    CatchNullNexus(rzrMethod, rzrClass);
#endif
  }
  protected void OpenNexusConnection(string recordAccess = "", bool resetRepo = false)
  {
    if (!string.IsNullOrEmpty(recordAccess))
    { WRACE.RecordAccessReqst = recordAccess; }
#if DEBUG
    WRACE.DebugClientAccess(nameof(OpenNexusConnection), rzrClass);
#endif
    if (resetRepo) { ResetNexusRepository(); }
    pdpNexusDataCntxt.DbsqlConnect();
  }
  protected void CloseNexusConnection()
  {
    if (pdpNexusDataCntxt != null)
    { pdpNexusDataCntxt.DbsqlDisconnect(); }
  }
  protected override void CloseAllConnections()
  {
    // qebiUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
    pdpNexusDataCntxt?.DbsqlDisconnect();
  }
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuted);
    this.CatchNullWrace(rzrHndlr, rzrClass);
    // TODO: deconflict redundancies between debug methods
    this.DebugWraceData(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    this.CloseAllConnections();
  }

  protected void BuildNexusDropDownLists()
  {
    // using current PNDC, add Nexus lists

#if DEBUG
    var testLists = NPDSCD.NpdsDdlists;
#endif
  }

} // end class

// end file