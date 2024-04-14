// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract partial class WraceRazorPageControllerBase : PageModel
{
  // prefix rzr from RaZoR page
  private const string rzrClass = nameof(WraceRazorPageControllerBase);

  // Web API REST Controller Environment = WRACE for user config settings and web api requests/responses
  // QEB User Data Context = QUDC for user identification, authentication, authorization
  // PDP Core Data Context = PCDC for Core data repositories of data/metadata records
  // PDP Nexus Data Context = PNDC for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context = PCDC for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context = PADC for ACMS data repositories of data/metadata records

  protected void DebugRazorPage(string rzrHandlerName = "", string rzrCntrllrName = "")
  {
    CatchNullWrace(rzrHandlerName, rzrCntrllrName);
    WRACE.DebugClientAccess(rzrHandlerName, rzrCntrllrName);
    WRACE.DebugNpdsSelectFilter(rzrHandlerName, rzrCntrllrName);
    PSRM.DebugRazorPageStrings(rzrHandlerName, rzrCntrllrName);
  }

  [BindProperty]
  public PdpSiteRazorModel PSRM { get; set; } = new PdpSiteRazorModel();
  protected const string PsrmKey = nameof(PSRM);

  protected IEmailSender qebEmailSender;
  protected ISmsSender qebSmsSender;
  protected ILogger qebLogger;

  // Web REST API Controller Environment (WRACE)
  // reset on each request to each controller
  protected const string WraceKey = nameof(WRACE);
  protected NpdsClientWrace? wrace;
  public NpdsClientWrace WRACE
  {
    set {
      if (value == null)
      { throw new ArgumentNullException("ArgNullException when attempting to set WRACE "); }
      wrace = value;
    }
    get {
      if (wrace == null)
      { wrace = InitWrace(); }
      return wrace;
    }
  }

  // Web REST API Controller Environment (WRACE) for user config settings and web api requests
  // QEB User Data Context (QUDC) for user identification, authentication, authorization
  // PDP Core Data Context (PCDC) for Core data repositories of data/metadata records
  // PDP Nexus Data Context (PNDC) for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context (PCDC) for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context (PADC) for ACMS data repositories of data/metadata records

#if DEBUG
  // TODO: add DatabaseType inspection to the CatchNull* and Debug*Repo methods
  protected void CatchNullWrace(string methodName = "", string className = "")
  {
    WRACE.CatchNullObject(WraceKey, methodName, className);
  }
  protected virtual void DebugWraceData(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(CatchNullWrace)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"WRACE Dbconstr QEBI: {WRACE.DbcnstrQebi}");
    Debug.WriteLine($"WRACE Dbconstr Core: {WRACE.DbcnstrCore}");
    Debug.WriteLine($"WRACE Dbconstr Nexus: {WRACE.DbcnstrNexus}");
    Debug.WriteLine($"WRACE Dbconstr Scribe: {WRACE.DbcnstrScribe}");
    Debug.WriteLine($"WRACE Dbconstr ACMS: {WRACE.DbcnstrAcms}");
    Debug.WriteLine($"WRACE DatabaseConstr: {WRACE.DatabaseConstr}");
  }
#endif

  protected NpdsClientWrace InitWrace()
  {
    var baseUrl = QebHttpContextAccessor.BaseUrl;
    var httpRqst = QebHttpContextAccessor.Current.Request;
    return InitWrace(httpRqst);
  }
  protected NpdsClientWrace InitWrace(IHttpContextAccessor contextAccessor)
  {
    HttpContext? httpCntxt = contextAccessor.HttpContext;
    HttpRequest? httpRqst = httpCntxt.Request;
    return InitWrace(httpRqst);
  }
  protected NpdsClientWrace InitWrace(HttpRequest httpRqst)
  {
    httpRqst.CatchNullObject(nameof(httpRqst), nameof(InitWrace), rzrClass);
    return new NpdsClientWrace(HttpContext);
  }
  protected virtual ILogger InitLogger<TLogger>(ILoggerFactory? lgrFtry)
  {
    if (lgrFtry == null) { lgrFtry = new LoggerFactory(); }
    qebLogger = lgrFtry.CreateLogger<TLogger>();
    return qebLogger;
  }

  protected void WraceUxmAddErrors(string error)
  {
    ModelState.AddModelError("", error);
  }
  protected void WraceUxmAddErrors(string[] errors)
  {
    foreach (var error in errors)
    {
      ModelState.AddModelError("", error);
    }
  }

  protected void WraceDevTest()
  {
    // test-dev-debug
    if (PdpGuid.IsInvalidGuid(WRACE.QebiUserGuid)) 
    { WRACE.ServiceError += "UserGuid is null, empty, or invalid"; }
    if (PdpGuid.IsInvalidGuid(WRACE.NpdsAgentGuid)) 
    { WRACE.ServiceError += "AgentGuid is null, empty, or invalid"; }
    if (PdpGuid.IsInvalidGuid(WRACE.NpdsAgentInfosetGuid)) 
    { WRACE.ServiceError += "AgentInfosetGuid is null, empty, or invalid"; }
    if (PdpGuid.IsInvalidGuid(WRACE.CiaamSessionGuid)) 
    { WRACE.ServiceError += "AgentSessionGuid is null, empty, or invalid"; }

    // display output
    Debug.WriteLine($"UserModeClientRequired: {WRACE.UserModeClientRequired}");
    Debug.WriteLine($"SessionClientRequired: {WRACE.SessionClientRequired}");
    Debug.WriteLine($"ClientIsAuthenticated: {WRACE.ClientIsAuthenticated}");
    Debug.WriteLine($"ClientUserName: {WRACE.CiaamUserName}; ClientPassWord: {WRACE.CiaamPassWord};");
    Debug.WriteLine($"ClientIsUser: {WRACE.ClientIsUser}; ClientUserGuid: {WRACE.QebiUserGuid};");
    Debug.WriteLine($"ClientIsAgent: {WRACE.ClientIsAgent}; ClientAgentGuid: {WRACE.NpdsAgentGuid};");
  }

  protected void AddErrors(IEnumerable<QebIdentityError> errorList)
  {
    foreach (var error in errorList)
    {
      ModelState.AddModelError("PdpIdentity", error.Message);
    }
  }

  protected bool IsTokenDateValid(DateTime? tokenDate)
  {
    var current = DateTime.UtcNow;
    var expired = Convert.ToDateTime(tokenDate ?? DateTime.MinValue);
    var isValid = (DateTime.Compare(current, expired) < 0);
    return isValid;
  }

  protected bool IsUrlLocalToLoginDomain(string testUrl)
  {
    if (Url.IsLocalUrl(testUrl) && (testUrl.Length > 1) && testUrl.StartsWith("/") && !testUrl.StartsWith("//") && !testUrl.StartsWith("/\\"))
    { return true; }
    else
    { return false; }
  }

  protected string ArgCheckReturnUrl(string theUrl)
  {
    if ((theUrl == null) || !IsUrlLocalToLoginDomain(theUrl))
    { theUrl = Url.Content(DepPdpSiteInfo); }
    return theUrl;
  }

  public string AppendKeys(string pathKeys, string ssnKey, string agtKey, string usrKey)
  {
    if (pathKeys == null) { pathKeys = ESS; }
    if (!pathKeys.Contains(QuestionChar)) { pathKeys = pathKeys + QuestionChar; }
    if (!PdpGuid.IsInvalidGuid(ssnKey)) { pathKeys = pathKeys + AndChar + QskSessionKey + EqualChar + ssnKey; }
    if (!PdpGuid.IsInvalidGuid(agtKey)) { pathKeys = pathKeys + AndChar + QskAgentKey + EqualChar + agtKey; }
    if (!PdpGuid.IsInvalidGuid(usrKey)) { pathKeys = pathKeys + AndChar + QskUserKey + EqualChar + usrKey; }
    return pathKeys;
  }
  public string AppendKeys(string pathKeys, Guid ssnKey, Guid agtKey, Guid usrKey)
  {
    return AppendKeys(pathKeys, ssnKey.ToString(), agtKey.ToString(), usrKey.ToString());
  }

} // end class

// end file