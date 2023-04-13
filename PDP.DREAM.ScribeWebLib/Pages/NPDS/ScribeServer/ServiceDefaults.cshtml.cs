﻿// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsEditor, NpdsAdmin)]
public class ScribeServerServiceDefaults : TkgsPageController
{
  private const string rzrClass = nameof(ScribeServerServiceDefaults);
  public ScribeServerServiceDefaults() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Admin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    // searchFilter, serviceTag, entityType overridden
    QURC.SearchFilter = NpdsSearchFilter.Diristry;
    QURC.ServiceTag = NpdsRoot;
    QURC.EntityType = NpdsEntityType.AnyAndAll;
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepScribeServerServiceDefaults, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeWebLibSpanPageMenu", "_ScribeServerSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
    ResetScribeRepository(); // for both OnGet and OnPost
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess = "")
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullQurc(rzrHndlr, rzrClass);
#endif
    // SearchFilter properties
    QURC.ParseNpdsResrepFilter(QURC.SearchFilter.ToString(), QURC.ServiceTag, QURC.EntityType.ToString());
    // TODO: fix this hack on RecordAccess roles
    // create parser for RecordAccess that limits to specified roles
    if (!string.IsNullOrWhiteSpace(recordAccess))
    {
      // default above is admin, TODO: allow author or not ???
      recordAccess = recordAccess.ToLower();
      if ((recordAccess == "editor") || (recordAccess == "author"))
      { QURC.RecordAccessReqst = recordAccess; }
    }
    PSRM.NpdsRazorBodyTitle(QURC.ServiceTitle);
    ResetCoreRepository(true);
    ResetScribeRepository(true);
    // build select lists
    BuildCoreDropDownLists();
#if DEBUG
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file