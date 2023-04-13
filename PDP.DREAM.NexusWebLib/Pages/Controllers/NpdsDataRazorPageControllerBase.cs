// NpdsDataRazorPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public abstract class NexusDataRazorPageControllerBase : CoreDataRazorPageControllerBase
{
  // prefix rzr from RaZoR page class
  private const string rzrClass = nameof(NexusDataRazorPageControllerBase);

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
    // ViewData[QurcKey] = QURC; // required for Views, but not for Pages
    // reset NPDS data context with current QEB User Rest Context
    pdpNexusDataCntxt = new NexusDbsqlContext((INpdsClient)QURC);
    // open connection if switched on
    if (openCnctn)
    { pdpNexusDataCntxt.DbsqlConnect(); }
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
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuted);
    this.CatchNullQurc(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    this.DebugQurcData(exeCntxt.Result);
#endif
    this.CloseAllConnections();
  }

  // ATTN: assure that these controllers create both the
  // public QURC with private qebUserRestCntxt
  // public QUDC with private qebUserDataCntxt

  public NexusDataRazorPageControllerBase()
  {
    qebiUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebiUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebiUserRestCntxt);
  }
  public NexusDataRazorPageControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLogger<NexusDataRazorPageControllerBase>(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebiUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebiUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebiUserRestCntxt);
  }

} // end class

// end file