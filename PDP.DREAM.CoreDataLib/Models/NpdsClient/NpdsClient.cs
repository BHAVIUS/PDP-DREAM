// NpdsClient.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient : INpdsClient
{
  public NpdsClient() { }
  public NpdsClient(NpdsDatabaseType dbType)
  { DatabaseType = dbType; }
  public NpdsClient(NpdsDatabaseType dbType, string dbCnstr)
  { DatabaseType = dbType; DatabaseConstr = dbCnstr; }

  public string? QebiDbconstr { get; set; } = NPDSSD.QebiDbconstr;
  public string? CoreDbconstr { get; set; } = NPDSSD.CoreDbconstr;
  public string? NexusDbconstr { get; set; } = NPDSSD.NexusDbconstr;
  public string? PortalDbconstr { get; set; } = NPDSSD.PortalDbconstr;
  public string? DoorsDbconstr { get; set; } = NPDSSD.DoorsDbconstr;
  public string? ScribeDbconstr { get; set; } = NPDSSD.ScribeDbconstr;
  public string? AcmsDbconstr { get; set; } = NPDSSD.AcmsDbconstr;
  public string? VocabDbconstr { get; set; } = NPDSSD.VocabDbconstr;
  public string? CacheDbconstr { get; set; } = NPDSSD.CacheDbconstr;

  public Guid? ClientAgentGuid { get; set; } = Guid.Empty;
  public Guid? ClientAgentInfosetGuid { get; set; } = Guid.Empty;
  public Guid? ClientAppSiaaGuid { get; set; } = Guid.Empty;
  public Guid? ClientRoleGuid { get; set; } = Guid.Empty;
  public Guid? ClientUserGuid { get; set; } = Guid.Empty;
  public Guid? ClientSessionGuid { get; set; } = Guid.Empty;

  public string? ClientPassWord { get; set; } = string.Empty;
  public string? ClientUserName { get; set; } = string.Empty;
  public string? ClientUserNameDisplayed { get; set; } = string.Empty;

  public bool ClientIsUser { get; set; } = false;
  public bool ClientIsAgent { get; set; } = false;
  public bool ClientIsAuthor { get; set; } = false;
  public bool ClientIsEditor { get; set; } = false;
  public bool ClientIsAdmin { get; set; } = false;

  public bool ClientHasUserAccess
  { get { return (UserModeClientRequired && ClientIsUser); } }
  public bool ClientHasAgentAccess
  { get { return (AgentModeClientRequired && ClientIsAgent); } }
  public bool ClientHasAuthorAccess
  { get { return (AuthorModeClientRequired && ClientIsAuthor); } }
  public bool ClientHasEditorAccess
  { get { return (EditorModeClientRequired && ClientIsEditor); } }
  public bool ClientHasAdminAccess
  { get { return (AdminModeClientRequired && ClientIsAdmin); } }
  public bool ClientHasEditorOrAdminAccess
  { get { return (ClientHasEditorAccess || ClientHasAdminAccess); } }
  public bool ClientHasAuthorOrEditorAccess
  { get { return (ClientHasAuthorAccess || ClientHasEditorAccess); } }
  public bool ClientHasScribeEditAccess
  { get { return (ClientHasAuthorAccess || ClientHasEditorAccess || ClientHasAdminAccess); } }

  public bool SessionClientRequired { get; set; } = false;
  public bool AuthenticatedClientRequired { get; set; } = false;
  public bool UserModeClientRequired { get; set; } = false;
  public bool AgentModeClientRequired { get; set; } = false;
  public bool AuthorModeClientRequired { get; set; } = false;
  public bool EditorModeClientRequired { get; set; } = false;
  public bool AdminModeClientRequired { get; set; } = false;
  public bool AuthorizedClientIsRequired
  {
    get {
      return (AuthenticatedClientRequired || UserModeClientRequired || AgentModeClientRequired ||
        AuthorModeClientRequired || EditorModeClientRequired || AdminModeClientRequired);
    }
  }

  public bool ClientIsAuthenticated { get; set; } = false;
  public bool ClientIsVerified
  {
    get { return (ClientIsAuthenticated && ClientIsAuthorized); }
  }

  private bool clientIsAuthorized = false;
  public bool ClientIsAuthorized
  {
    get {
      switch (RecordAccess)
      {
        case NpdsRecordAccess.Admin:
          clientIsAuthorized = ClientHasAdminAccess;
          break;
        case NpdsRecordAccess.Editor:
          clientIsAuthorized = ClientHasEditorAccess;
          break;
        case NpdsRecordAccess.Author:
          clientIsAuthorized = ClientHasAuthorAccess;
          break;
        case NpdsRecordAccess.Agent:
          clientIsAuthorized = ClientHasAgentAccess;
          break;
        case NpdsRecordAccess.AuthUser:
          clientIsAuthorized = ClientHasUserAccess;
          break;
        default:
          break;
      }
      return clientIsAuthorized;
    }
  }
  public bool ClientIsAnonymous
  {
    get { return (!ClientIsAuthorized); }
  }


  // QueryString Key Values for search parameters
  // TODO: refactor to a dictionary of strings
  public string? QskLexLabAny { get; set; } = string.Empty;
  public string? QskLexLabCan { get; set; } = string.Empty;
  public string? QskLexLabAls { get; set; } = string.Empty;
  public string? QskLexLabSup { get; set; } = string.Empty;
  public string? QskLexTagAny { get; set; } = string.Empty;
  public string? QskLexTagSup { get; set; } = string.Empty;
  public string? QskLexOText { get; set; } = string.Empty;

  public virtual string? UrlDebug { get; set; } = string.Empty;
  public virtual string? UrlHelp { get; set; } = string.Empty;
  public virtual string? UrlHelpDebug { get; set; } = string.Empty;


  // list item properties
  public bool ItemIsPrivate { get; set; } = false;
  public bool ItemIsConcise { get; set; } = false;
  public bool ItemCanBeAccessed
  { get { return (!ItemIsPrivate || ClientIsAuthorized); } }
  public bool ItemCanBeVerbosed // "verbosed" is coined term meaning "displayed verbosely" (CT 2011/10/15)
  { get { return (!ItemIsConcise || ClientIsAuthorized); } }
  public bool ItemDoesArchive
  { get { return (ItemCanBeAccessed && ArchiveFormat); } }
  public bool ItemDoesVerbose
  { get { return (ItemCanBeVerbosed && VerboseFormat); } }


  // debug methods
  public virtual void DebugClientAccess(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugClientAccess)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"Username = '{ClientUserName}'; Userguid = '{ClientUserGuid}'; RecordAccess = {RecordAccess};");
    Debug.WriteLine($"{nameof(UserModeClientRequired)} = {UserModeClientRequired}; {nameof(ClientHasUserAccess)} = {ClientHasUserAccess};");
    Debug.WriteLine($"{nameof(AgentModeClientRequired)} = {AgentModeClientRequired}; {nameof(ClientHasAgentAccess)} = {ClientHasAgentAccess};");
    Debug.WriteLine($"{nameof(AuthorModeClientRequired)} = {AuthorModeClientRequired}; {nameof(ClientHasAuthorAccess)} = {ClientHasAuthorAccess};");
    Debug.WriteLine($"{nameof(EditorModeClientRequired)} = {EditorModeClientRequired}; {nameof(ClientHasEditorAccess)} = {ClientHasEditorAccess};");
    Debug.WriteLine($"{nameof(AdminModeClientRequired)} = {AdminModeClientRequired}; {nameof(ClientHasAdminAccess)} = {ClientHasAdminAccess};");
    Debug.WriteLine($"{nameof(ClientHasScribeEditAccess)} = {ClientHasScribeEditAccess};");
  }

  public virtual void DebugNpdsParams(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugNpdsParams)} called from Class = '{className}'; Method = '{methodName}';");
    // SearchFilter should default to corresponding ServiceType
    // eg, SearchFilter = Diristry for ServiceType = Nexus
    // SearchFilter may be different for each ServiceType,
    // eg, SearchFilter = Registry for ServiceType = Scribe
    Debug.WriteLine($"ServiceType = {ServiceType}; RecordAccess = {RecordAccess};");
    Debug.WriteLine($"ResrepParamsPath = /{SearchFilter}/{ServiceTag}/{EntityType}");
    Debug.WriteLine($"with DiristryTag = '{DiristryTag}' and DiristryGuid = '{DiristryGuid}'");
    Debug.WriteLine($"with RegistryTag = '{RegistryTag}' and RegistryGuid = '{RegistryGuid}'");
    Debug.WriteLine($"with DirectoryTag = '{DirectoryTag}' and DirectoryGuid = '{DirectoryGuid}'");
    Debug.WriteLine($"with RegistrarTag = '{RegistrarTag}' and RegistrarGuid = '{RegistrarGuid}'");
  }

} // end class

// end file