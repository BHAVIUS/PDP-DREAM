﻿// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.NexusWebLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.ScribeWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class ScribeDataRazorViewControllerBase : NexusDataRazorViewControllerBase
{
  private const string rzrCntrllr = nameof(ScribeDataRazorViewControllerBase);

  // ranp = Route App NamePrefix
  // public const string SdlRanpView = "ScribeDataLibView"; // by Razor view for ScribeDataLib
  // public const string SrlRanpView = "ScribeRestLibView"; //  by Razor view for ScribeRestLib
  // public const string SwlRanpView = "ScribeWebLibView"; // by Razor view for ScribeWebLib

  // data contexts: QEB User REST/Data, PDP NPDS Data/Metadata
  // QEB User REST Context = QURC for user config settings and web api requests
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PSDC for Scribe data repositories of data/metadata records

  // PDP Scribe Data Context = PSDC
  protected ScribeDbsqlContext pdpScribeDataCntxt;
  public ScribeDbsqlContext PSDC
  {
    set {
      if (value == null) { pdpScribeDataCntxt = new ScribeDbsqlContext(NPDSSD.NpdsRegistrarDbconstr); }
      else { pdpScribeDataCntxt = value; }
      ResetScribeRepository();
    }
    get {
      if (pdpScribeDataCntxt == null) { pdpScribeDataCntxt = new ScribeDbsqlContext(NPDSSD.NpdsRegistrarDbconstr); }
      return pdpScribeDataCntxt;
    }
  }

  // TODO: migrate BuildDropDownListsForResrepRoot to Pages and Views controllers
  // protected so not visible as public action for controller routes
  protected void ResetScribeRepository(bool dropDownLists = false)
  {
    // reset repository with current QEB User Rest Context
    if (QURC == null) { NullRefException(nameof(QURC), nameof(ResetScribeRepository), nameof(ScribeDataRazorViewControllerBase)); }
    ViewData[QurcKey] = QURC;
    // reset repository with current PDP Scribe Data Context
    if (PSDC == null) { NullRefException(nameof(PSDC), nameof(ResetScribeRepository), nameof(ScribeDataRazorViewControllerBase)); }
    PSDC.ResetRestContext(QURC);
    // reset dropdownlists in ViewData if needed for front-end client
    if (dropDownLists)
    {
      BuildScribeDropDownLists();
    }
  }

  public ScribeDataRazorViewControllerBase()
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpScribeDataCntxt = new ScribeDbsqlContext(NPDSSD.NpdsRegistrarDbconstr);
  }

  public ScribeDataRazorViewControllerBase(QebIdentityContext userCntxt)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpScribeDataCntxt = new ScribeDbsqlContext(NPDSSD.NpdsRegistrarDbconstr);
  }
  public ScribeDataRazorViewControllerBase(ScribeDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = new QebIdentityContext(NPDSSD.NpdsUserDbconstr);
    pdpScribeDataCntxt = npdsCntxt;
  }
  public ScribeDataRazorViewControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>();
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    pdpScribeDataCntxt = npdsCntxt;
  }
  public ScribeDataRazorViewControllerBase(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpScribeDataCntxt = new ScribeDbsqlContext(NPDSSD.NpdsRegistrarDbconstr);
  }
  public ScribeDataRazorViewControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
  {
    qebLogger = InitLogger<ScribeDataRazorViewControllerBase>(lgrFtry);
    qebUserRestCntxt = InitRestContext();
    qebUserDataCntxt = userCntxt;
    qebEmailSender = emlSndr;
    qebSmsSender = smsSndr;
    pdpScribeDataCntxt = npdsCntxt;
  }

  // migrate from Scribe*ControllerBase to Core*ControllerBase
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

    // TODO: must get working for Core before migrating to Scribe
    // ATTN: which lists are build in a manner dependent on authenticated user login
    // BuildCoreDropDownLists();
    //
    // ATTN: SupportingLabelList available in Scribe but not in Core
    // TODO: rebuild with default list from the defined problem domain for the specialty diristry
    // UilDdlists.SupportingLabelList = PSDC.GetItemsForSupportingLabelSelectList();
    // ViewData[nameof(UilDropDownLists.SupportingLabelList)] = UilDdlists.SupportingLabelList;

  }

} // end class

// end file
