// CoreDataRazorPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public abstract partial class CoreDataRazorPageControllerBase : PageModel, ISiaaUser
{
  // prefix rzr from RaZoR page class
  private const string rzrClass = nameof(CoreDataRazorPageControllerBase);

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
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

  protected string YearNow = DateTime.Now.Year.ToString();
  protected void CatchNullCore(string methodName = "", string className = "")
  {
    PCDC.CatchNullObject(PcdcKey, methodName, className);
    Debug.WriteLine($"{nameof(CatchNullCore)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PCDC DatabaseType: {PCDC.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PCDC DatabaseConstr: {PCDC.NPDSCP.DatabaseConstr}");
  }
  protected void DebugCoreRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugCoreRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{QURC.SearchFilter}/{QURC.ServiceTag}/{QURC.EntityType}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"QURC CoreDbconstr: {QURC.CoreDbconstr}");
    Debug.WriteLine($"QURC DatabaseConstr: {QURC.DatabaseConstr}");
    Debug.WriteLine($"PCDC DatabaseType: {PCDC?.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PCDC DatabaseConstr: {PCDC?.NPDSCP.DatabaseConstr}");
  }

  // protected so not visible as public action for controller routes
  protected void ResetCoreRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetCoreRepository);
    CatchNullQurc(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (QURC.DatabaseType != NpdsDatabaseType.Core)
    { QURC.DatabaseType = NpdsDatabaseType.Core; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { QURC.CoreDbconstr = dbcs; }
    // reset viewdata with current QEB User Rest Context
    // ViewData[QurcKey] = QURC; // required for Views, but not for Pages
    // reset NPDS data context with current QEB User Rest Context
    pdpCoreDataCntxt = new CoreDbsqlContext((INpdsClient)QURC);
    // open connection if switched on
    if (openCnctn)
    { pdpCoreDataCntxt.DbsqlConnect(); }
#if DEBUG
    CatchNullCore(rzrMethod, rzrClass);
#endif
  }
  protected void OpenCoreConnection(bool resetRepo = false)
  {
    if (resetRepo) { ResetCoreRepository(false); }
    pdpCoreDataCntxt.DbsqlConnect();
  }
  protected void CloseCoreConnection()
  {
    pdpCoreDataCntxt.DbsqlDisconnect();
  }
  protected virtual void CloseAllConnections()
  {
    qebiUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
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

  public CoreDataRazorPageControllerBase()
  {
    qebiUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebiUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebiUserRestCntxt);
  }
  public CoreDataRazorPageControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLogger<CoreDataRazorPageControllerBase>(lgrFtry);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    qebiUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebiUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebiUserRestCntxt);
  }

  // TODO: migrate dropdownlists from Scribe*ControllerBase to Core*ControllerBase
  // via analogous core version then transition from scribe version to core version

  protected UilDropDownLists UilDdlists;
  protected IList<EntityTypeListItem> EntityTypeSelectList;
  protected IList<FieldFormatListItem> FieldFormatSelectList;
  protected IList<SelectListItem>? CoreDiristryListMvc;
  protected void BuildCoreDropDownLists()
  {
    // TODO: re-eval conventions for select item lists
    //  currently suffix "Mvc" associated with those lists that are IList<SelectListItem>?
    UilDdlists = new UilDropDownLists()
    {
      EntityTypeList = PCDC.GetEntityTypeSelectList(),
      FieldFormatList = PCDC.GetFieldFormatSelectList(),
      CoreDiristryListMvc = PCDC.GetCoreDiristrySelectListMvc(),
    };
    // TODO: use of fields or use of ViewData ? which approach works better ?
    EntityTypeSelectList = UilDdlists.EntityTypeList;
    FieldFormatSelectList = UilDdlists.FieldFormatList;
    CoreDiristryListMvc = UilDdlists.CoreDiristryListMvc;
    // TODO: use of fields or use of ViewData ? which approach works better ?
    ViewData[nameof(UilDropDownLists.EntityTypeList)] = UilDdlists.EntityTypeList;
    ViewData[nameof(UilDropDownLists.FieldFormatList)] = UilDdlists.FieldFormatList;
    ViewData[nameof(UilDropDownLists.CoreDiristryListMvc)] = UilDdlists.CoreDiristryListMvc;

    if (QURC.ClientHasScribeEditAccess)
    {
      UilDdlists.CoreDiristryList = PCDC.GetCoreDiristrySelectList();
      ViewData[nameof(UilDropDownLists.CoreDiristryList)] = UilDdlists.CoreDiristryList;
      UilDdlists.CoreDiristryListMvc = PCDC.GetCoreDiristrySelectListMvc();
      ViewData[nameof(UilDropDownLists.CoreDiristryListMvc)] = UilDdlists.CoreDiristryListMvc;
      UilDdlists.RegcDiristryListMvc = PCDC.GetRegistrarDiristriesSelectListMvc();
      ViewData[nameof(UilDropDownLists.RegcDiristryListMvc)] = UilDdlists.RegcDiristryListMvc;
    }

    if (QURC.ClientHasEditorOrAdminAccess)
    {
      UilDdlists.CoreRegistryList = PCDC.GetCoreRegistrySelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistryList)] = UilDdlists.CoreRegistryList;
      UilDdlists.CoreDirectoryList = PCDC.GetCoreDirectorySelectList();
      ViewData[nameof(UilDropDownLists.CoreDirectoryList)] = UilDdlists.CoreDirectoryList;
      UilDdlists.CoreRegistrarList = PCDC.GetCoreRegistrarSelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistrarList)] = UilDdlists.CoreRegistrarList;
    }

    if (QURC.ClientHasAdminAccess)
    {
      UilDdlists.InfosetPortalStatusList = PCDC.GetInfosetPortalStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetPortalStatusList)] = UilDdlists.InfosetPortalStatusList;
      UilDdlists.InfosetDoorsStatusList = PCDC.GetInfosetDoorsStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetDoorsStatusList)] = UilDdlists.InfosetDoorsStatusList;
    }

  }

} // end class

// end file