﻿// NpdsDataRazorViewControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public abstract class ScribeDataRazorViewControllerBase : NexusDataRazorViewControllerBase
{
  // prefix rzr from RaZoR view class
  private const string rzrClass = nameof(ScribeDataRazorViewControllerBase);

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  // PDP Scribe Data Context (PSDC)
  protected const string PsdcKey = nameof(PSDC);
  protected ScribeDbsqlContext? pdpScribeDataCntxt;
  public ScribeDbsqlContext? PSDC
  { get { return pdpScribeDataCntxt; } }

  protected void CatchNullScribe(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(CatchNullScribe)} called from Class = '{className}'; Method = '{methodName}';");
    PSDC.CatchNullObject(PndcKey, methodName, className);
    Debug.WriteLine($"PSDC DatabaseType: {PSDC.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PSDC DatabaseConstr: {PSDC.NPDSCP.DatabaseConstr}");
  }
  protected void DebugScribeRepo(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugScribeRepo)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"/{QURC.SearchFilter}/{QURC.ServiceTag}/{QURC.ServiceType}/{QURC.EntityType}/{QURC.RecordAccess}");
    Debug.WriteLine($"with database connection strings in method {methodName}");
    Debug.WriteLine($"QURC ScribeDbconstr: {QURC.ScribeDbconstr}");
    Debug.WriteLine($"QURC DatabaseConstr: {QURC.DatabaseConstr}");
    Debug.WriteLine($"PSDC DatabaseType: {PSDC?.NPDSCP.DatabaseType}");
    Debug.WriteLine($"PSDC DatabaseConstr: {PSDC?.NPDSCP.DatabaseConstr}");
  }

  // protected so not visible as public action for controller routes
  protected void ResetScribeRepository(bool openCnctn = false, string? dbcs = "")
  {
#if DEBUG
    var rzrMethod = nameof(ResetNexusRepository);
    CatchNullQurc(rzrMethod, rzrClass);
#endif
    // assure correct DatabaseType
    if (QURC.DatabaseType != NpdsDatabaseType.Scribe)
    { QURC.DatabaseType = NpdsDatabaseType.Scribe; }
    // override DatabaseConstr if dbcs input
    if (!string.IsNullOrEmpty(dbcs))
    { QURC.ScribeDbconstr = dbcs; }
    // reset viewdata with current QEB User Rest Context
    ViewData[QurcKey] = QURC; // TODO: where used? where required? for Views not Pages?
    // reset NPDS data context with current QEB User Rest Context
    pdpScribeDataCntxt = new ScribeDbsqlContext((INpdsClient)QURC);
    // open connection if switched on
    if (openCnctn)
    {
      pdpScribeDataCntxt.DbsqlConnect();
      // TODO: migrate/reconcile with BuildCoreDropDownLists()
      // ? else refactor to pages only where needed
      BuildScribeDropDownLists();
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
    qebUserDataCntxt?.DbsqlDisconnect();
    pdpCoreDataCntxt?.DbsqlDisconnect();
    pdpNexusDataCntxt?.DbsqlDisconnect();
    pdpScribeDataCntxt?.DbsqlDisconnect();
  }
  public override void OnActionExecuted(ActionExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnActionExecuted);
    this.CatchNullQurc(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    this.DebugQurcData(exeCntxt.Result);
#endif
    this.CloseAllConnections();
  }

  public ScribeDataRazorViewControllerBase()
  {
    qebUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebUserRestCntxt);
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebUserRestCntxt);
  }
  public ScribeDataRazorViewControllerBase(ILoggerFactory lgrFtry, IEmailSender emlSndr, ISmsSender smsSndr)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext().SetDatabaseType(NpdsDatabaseType.SIAA);
    qebUserDataCntxt = new QebiDbsqlContext((INpdsClient)qebUserRestCntxt);
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
  }




  // TODO: migrate from Scribe*ControllerBase to Core*ControllerBase
  // via analogous core version then transition from scribe version to core version
  protected void BuildScribeDropDownLists()
  {
    var rrddl = new UilDropDownLists()
    {
      EntityTypeList = PSDC.GetEntityTypeSelectList(),
      FieldFormatList = PSDC.GetFieldFormatSelectList(),
    };
    ViewData[nameof(UilDropDownLists.EntityTypeList)] = rrddl.EntityTypeList;
    ViewData[nameof(UilDropDownLists.FieldFormatList)] = rrddl.FieldFormatList;

    if (QURC.ClientHasScribeEditAccess)
    {
      rrddl.CoreDiristryList = PSDC.GetCoreDiristrySelectList();
      ViewData[nameof(UilDropDownLists.CoreDiristryList)] = rrddl.CoreDiristryList;
      rrddl.CoreDiristryListMvc = PSDC.GetCoreDiristrySelectListMvc();
      ViewData[nameof(UilDropDownLists.CoreDiristryListMvc)] = rrddl.CoreDiristryListMvc;
      rrddl.RegcDiristryListMvc = PSDC.GetRegistrarDiristriesSelectListMvc();
      ViewData[nameof(UilDropDownLists.RegcDiristryListMvc)] = rrddl.RegcDiristryListMvc;
      // ATTN: SupportingLabelList available in Scribe but not in Core
      // TODO: rebuild with default list from the defined problem domain for the specialty diristry
      rrddl.SupportingLabelList = PSDC.GetItemsForSupportingLabelSelectList();
      ViewData[nameof(UilDropDownLists.SupportingLabelList)] = rrddl.SupportingLabelList;
    }

    if (QURC.ClientHasEditorOrAdminAccess)
    {
      rrddl.CoreRegistryList = PSDC.GetCoreRegistrySelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistryList)] = rrddl.CoreRegistryList;
      rrddl.CoreDirectoryList = PSDC.GetCoreDirectorySelectList();
      ViewData[nameof(UilDropDownLists.CoreDirectoryList)] = rrddl.CoreDirectoryList;
      rrddl.CoreRegistrarList = PSDC.GetCoreRegistrarSelectList();
      ViewData[nameof(UilDropDownLists.CoreRegistrarList)] = rrddl.CoreRegistrarList;
    }

    if (QURC.ClientHasAdminAccess)
    {
      rrddl.InfosetPortalStatusList = PSDC.GetInfosetPortalStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetPortalStatusList)] = rrddl.InfosetPortalStatusList;
      rrddl.InfosetDoorsStatusList = PSDC.GetInfosetDoorsStatusSelectList();
      ViewData[nameof(UilDropDownLists.InfosetDoorsStatusList)] = rrddl.InfosetDoorsStatusList;
    }

    // ATTN: SupportingLabelList available in Scribe but not in Core
    // TODO: rebuild with default list from the defined problem domain for the specialty diristry
    // UilDdlists.SupportingLabelList = PSDC.GetItemsForSupportingLabelSelectList();
    // ViewData[nameof(UilDropDownLists.SupportingLabelList)] = UilDdlists.SupportingLabelList;

  }

} // end class

// end file
