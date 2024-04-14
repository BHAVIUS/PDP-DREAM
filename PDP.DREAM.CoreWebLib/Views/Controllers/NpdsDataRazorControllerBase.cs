// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract partial class CoreDataRazorViewControllerBase : QebiDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view
  private const string rzrClass = nameof(CoreDataRazorViewControllerBase);

  // Web API REST Controller Environment = WRACE for user config settings and web api requests/responses
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // PDP Core Data Context = PCDC
  protected const string PcdcKey = nameof(PCDC);
  protected CoreDbsqlContext? pdpCoreDataCntxt = null;
  public CoreDbsqlContext? PCDC
  {
    get { return pdpCoreDataCntxt; }
  }

  public CoreDataRazorViewControllerBase()
  {
    wrace = InitWrace();
  }
  public CoreDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLoggerCore(lgrFtry);
    wrace = InitWrace();
  }
  public CoreDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLoggerCore(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    wrace = InitWrace();
  }

  protected ILogger InitLoggerCore(ILoggerFactory lgrFtry)
  {
    var logger = lgrFtry.CreateLogger<CoreDataRazorPageControllerBase>();
    return logger;
  }
  protected void CatchNullCore(string methodName = "", string className = "")
  {
    PCDC.CatchNullObject(PcdcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullCore)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PCDC DatabaseType: {PCDC.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PCDC DatabaseConstr: {PCDC.NPDSDC.DatabaseConstr}");
  }
  protected void DebugCoreRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugCoreRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{WRACE.SearchFilter}/{WRACE.ServiceTag}/{WRACE.ServiceType}/{WRACE.EntityType}/{WRACE.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"WRACE CoreDbconstr: {WRACE.DbcnstrCore}");
    Debug.WriteLine($"WRACE DatabaseConstr: {WRACE.DatabaseConstr}");
    Debug.WriteLine($"PCDC DatabaseType: {PCDC?.NPDSDC.DatabaseType}");
    Debug.WriteLine($"PCDC DatabaseConstr: {PCDC?.NPDSDC.DatabaseConstr}");
  }
  protected void DebugResrepModel(ResrepRootModelBase uxm)
  {
    Debug.WriteLine($"ModelStateIsValid: {ModelState.IsValid}");
    Debug.WriteLine($"RRRecordAccess: {uxm.RRRecordAccess}");
    Debug.WriteLine($"NdisElemMsg: {uxm.NdisElemMsg}");
  }

  // protected so not visible as public action for controller routes
  protected void ResetCoreRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetCoreRepository);
    CatchNullWrace(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (WRACE.DatabaseType != NPDSCD.DatabaseTypeCore)
    { WRACE.DatabaseType = NPDSCD.DatabaseTypeCore; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { WRACE.DbcnstrCore = dbcs; }
    // reset NPDS data context with current QEB User Rest Context
    pdpCoreDataCntxt = new CoreDbsqlContext((INpdscwClient)WRACE);
    // open connection if switched on
    if (openCnctn)
    {
      pdpCoreDataCntxt.DbsqlConnect();
    }
    WRACE.DbciCore = (INpdsDbsqlContext)pdpCoreDataCntxt;
#if DEBUG
    CatchNullCore(rzrMethod, rzrClass);
#endif
  }
  protected void OpenCoreConnection(string recordAccess = "", bool resetRepo = false)
  {
    if (!string.IsNullOrEmpty(recordAccess))
    { WRACE.RecordAccessReqst = recordAccess; }
#if DEBUG
    WRACE.DebugClientAccess(nameof(OpenCoreConnection), rzrClass);
#endif
    if (resetRepo) { ResetCoreRepository(); }
    pdpCoreDataCntxt.DbsqlConnect();
  }
  protected void CloseCoreConnection()
  {
    if (pdpCoreDataCntxt != null)
    { pdpCoreDataCntxt.DbsqlDisconnect(); }
  }
  protected virtual void CloseAllConnections()
  {
    pdpCoreDataCntxt?.DbsqlDisconnect();
  }
  public override void OnActionExecuted(ActionExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnActionExecuted);
    this.CatchNullWrace(rzrHndlr, rzrClass);
    // TODO: deconflict redundancies between debug methods
    this.DebugWraceData(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    this.CloseAllConnections();
  }

  public bool CheckNpdsAgentSession()
  {
    var qebSignin = new QebIdentityResult();
    var sessionIsIdentified = false;
    var agentIsVerified = false;
    // AuthorizedClientIsAllowed depends on the PRC.RecordAccess setting
    if (WRACE.AuthorizedClientIsAllowed)
    {
      if (QebRcp.IsAuthenticated)
      {
        sessionIsIdentified = PCDC.CheckSessionNpdsAgent(ref wrace);
      }
      if (sessionIsIdentified)
      {
        agentIsVerified = WRACE.ClientIsVerified;
      }
    }
#if DEBUG
    WraceDevTest();
#endif
    return agentIsVerified;
  }

  public Guid? ParseResRepRecordGuid(string recordName, Guid? modelGuid, Guid defaultGuid)
  {
    var parsedGuid = PdpGuid.ParseToNonNullable(modelGuid, defaultGuid);
    if (PdpGuid.IsNullOrEmpty(parsedGuid))
    { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
    return parsedGuid;
  }
} // end class

// end file