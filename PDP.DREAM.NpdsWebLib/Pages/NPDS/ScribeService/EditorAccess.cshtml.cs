// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsEditorRS, NpdsAdminRS)]
public class ScribeServiceEditorAccess : ScribeTkgPageController
{
  private const string rzrClass = nameof(ScribeServiceEditorAccess);
  public ScribeServiceEditorAccess() : base() { }

  // ScribeService EditorAccess
  public const string pageName = DepScribeServiceEditorAccess;
  public const string pagePath = pageName;
  public const string bodyTitle = PdpSitePathKey;
  public const string bodyMenu = "_ScribeServiceSpanPageMenu";

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeRegistrar, // resets ServicePath
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessEditor, // resets *ClientRequired
      ResrepFormat = NPDSCD.ResrepFormatNexus, // requires ServicePath
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(pageName, pagePath, bodyTitle, bodyMenu);
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository();
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
    NPDSCW.ResetScribeRepository(); // required for OnGet, OnPost, TKG Ajax post callbacks
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    this.DebugWraceRazorPage(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string recordAccess = "")
  {
    if (!string.IsNullOrEmpty(recordAccess))
    { NPDSCW.RecordAccessReqst = recordAccess; }
    NPDSCW.ResetAtlasRepository(true);
    NPDSCW.ResetScribeRepository(true, true);
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    this.DebugWraceRazorPage(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuted), rzrClass);
#endif
  }

  // Other page handlers and properties

} // end class

// end file