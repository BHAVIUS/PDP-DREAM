// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// NPDS Client Wrace (Npdscw*)
public interface INpdscwClient
{
  // NPDS Client WRACE properties in the NpdsClient folder organized
  // into group of files Npdser* for Enum constrained Record properties (er) 
  // another group of files Npdsrv* for Regex constrained Value Properties (rv)
  // another group of files Npdsdv* for simple Data Value properties (dv) 
  // where enum record properties are PdpEnumerator class typed
  // and data value properties are simple bool, string, number, or guid typed
  
    NpdsClientRole CiaamClientRole { get; set; }

  string? CiaamUserAlias { get; set; }
  string? CiaamUserEmail { get; set; }
  string? CiaamUserName { get; set; }
  string? CiaamPassWord { get; set; }

  Byte? CiaamRoleCode { get; set; }
  Guid? CiaamAppGuid { get; set; }
  Guid? CiaamRoleGuid { get; set; }
  Guid? CiaamSessionGuid { get; set; } // TBD ?

  Guid? QebiUserGuid { get; set; }
  Guid? NpdsAgentGuid { get; set; }
  Guid? BridgePersonGuid { get; set; }
  Guid? NpdsAgentInfosetGuid { get; set; }

  bool AdminModeClientRequired { get; set; }
  bool AgentModeClientRequired { get; set; }
  bool AuthenticatedClientRequired { get; set; }
  bool AuthorModeClientRequired { get; set; }
  bool AuthorizedClientIsAllowed { get; }
  bool EditorModeClientRequired { get; set; }
  bool ReviewerModeClientRequired { get; set; }
  bool SessionClientRequired { get; set; }
  bool UserModeClientRequired { get; set; }

  bool ClientIsAnonymous { get; }
  bool ClientIsAuthenticated { get; set; }
  bool ClientIsAuthorized { get; }
  bool ClientIsVerified { get; }

  bool ClientIsUser { get; set; }
  bool ClientIsAgent { get; set; }
  bool ClientIsAuthor { get; set; }
  bool ClientIsReviewer { get; set; }
  bool ClientIsEditor { get; set; }
  bool ClientIsAdmin { get; set; }

  bool ClientHasUserMode { get; }
  bool ClientHasAgentMode { get; }
  bool ClientHasAuthorMode { get; }
  bool ClientHasReviewerMode { get; }
  bool ClientHasEditorMode { get; }
  bool ClientHasAdminMode { get; }

  bool DbcontextIsConfigured(DbContextOptionsBuilder dbcob);

  bool ItemCanBeAccessed { get; }
  bool ItemCanBeVerbosed { get; }
  bool ItemDoesArchive { get; }
  bool ItemDoesVerbose { get; }
  bool ItemIsConcise { get; set; }
  bool ItemIsPrivate { get; set; }

  // QueryString Key Values for search parameters in LINQ queries
  // TODO: refactor to a dictionary of strings
  string? LnqLexLabAny { get; set; }
  string? LnqLexLabCan { get; set; }
  string? LnqLexLabAls { get; set; }
  string? LnqLexLabSup { get; set; }
  string? LnqLexTagAny { get; set; }
  string? LnqLexTagSup { get; set; }
  string? LnqLexOText { get; set; }


  bool ArchiveFormat { get; set; }
  bool CheckFormat { get; set; }
  bool EchoFormat { get; set; }
  bool QueryFormat { get; set; }
  bool VerboseFormat { get; set; }

  int PageListCount { get; set; }

  Guid? DirectoryGuid { get; set; }
  Guid? DiristryGuid { get; set; }
  Guid? RegistrarGuid { get; set; }
  Guid? RegistryGuid { get; set; }


  string? DatabaseConstr { get; set; }
  string? DbcnstrQebi { get; set; }
  string? DbcnstrCore { get; set; }
  string? DbcnstrNexus { get; set; }
  string? DbcnstrPortal { get; set; }
  string? DbcnstrDoors { get; set; }
  string? DbcnstrScribe { get; set; }
  string? DbcnstrAcms { get; set; }
  string? DbcnstrVocab { get; set; }
  string? DbcnstrCache { get; set; }


  string? DirectoryTag { get; set; }
  string? DiristryTag { get; set; }
  string? EntityName { get; set; }
  string? EntityNature { get; set; }
  string? EntityTag { get; set; }
  string? EntityVersion { get; set; }
  string? RegistrarTag { get; set; }
  string? RegistryTag { get; set; }
  string? ServiceTag { get; set; }
  string? ServiceTitle { get; set; }

  NpdsDatabaseAccess? DatabaseAccess { get; set; }
  NpdsDatabaseType? DatabaseType { get; set; }
  NpdsEntityType? EntityType { get; set; }
  NpdsInfosetStatus? InfosetStatus { get; set; }
  NpdsMessageFormat? MessageFormat { get; set; }
  NpdsNodeType? NodeType { get; set; }
  NpdsRecordAccess? RecordAccess { get; set; }
  NpdsResrepFormat? ResrepFormat { get; set; }
  NpdsSearchFilter? SearchFilter { get; set; }
  NpdsSearchScope? SearchScope { get; set; }
  NpdsServerType? ServerType { get; set; }
  NpdsServiceType? ServiceType { get; set; }

  bool LnqFltrAuthoritativeOnly { get; set; }
  bool LnqFltrPublicOnly { get; set; }
  bool LnqFltrRecordAccess { get; set; }
  bool LnqFltrNpdsService { get; set; }
  bool LnqFltrQryStrValues { get; set; }

  string? StatusName { get; set; }
  string? StatusNote { get; set; }
  string? StatusXhtml { get; set; }
  string? ServiceError { get; set; }
  string? ServiceNote { get; set; }

  string? UrlDebug { get; set; }
  string? UrlHelp { get; set; }

  string? RequestQuestion { get; set; }
  string? RequestNote { get; set; }

  string? ResponseNote { get; set; }
  string? ResponseStatus { get; set; }
  string? ResponseHeader { get; set; }

  NpdsResrepList? ResponseAnswer { set; get; }
  NpdsResrepList? ResponseRelated { set; get; }
  NpdsResrepList? ResponseReferred { set; get; }

  NpdsResrepList? CoreRecords { set; get; }
  NpdsResrepList? PortalRecords { set; get; }
  NpdsResrepList? DoorsRecords { set; get; }
  NpdsResrepList? NexusRecords { set; get; }

  bool ClientCanAgentWrite { get; }
  bool ClientCanAuthorWrite { get; }
  bool ClientCanEditorWrite { get; }
  bool ClientCanAdminWrite { get; }

  void ParseNpdsQueryString(IQueryCollection? qryStrCol);
  void ParseNpdsSelectFilter();
  void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityTag);
  void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityType, string entityTag, string searchFilter, string resrepFormat);
  void DebugNpdsSelectFilter(string methodName = "", string className = "");
  void DebugClientAccess(string methodName = "", string className = "");


  // properties below migrated from Web api rest client
  HttpContext? NpdsContext { get; set; }
  string? NpdsReqstBaseUrl { get; set; }
  string? NpdsReqstDisplayUrl { get; set; }
  string? NpdsReqstEncodedUrl { get; set; }
  HostString? NpdsReqstHost { get; set; }
  string? NpdsReqstPath { get; set; }
  string? NpdsReqstPathBase { get; set; }
  IQueryCollection? NpdsReqstQuery { get; set; }
  QueryString? NpdsReqstQueryString { get; set; }
  string? NpdsReqstScheme { get; set; }
  string? NpdsReqstXmlSchemaUrl { get; }
  HttpRequest? NpdsRequest { get; set; }
  HttpResponse? NpdsResponse { get; set; }

  INpdsDbsqlContext? DbciQebi { get; set; }
  INpdsDbsqlContext? DbciCore { get; set; }
  INpdsDbsqlContext? DbciNexus { get; set; }
  INpdsDbsqlContext? DbciPortal { get; set; }
  INpdsDbsqlContext? DbciDoors { get; set; }
  INpdsDbsqlContext? DbciScribe { get; set; }
  INpdsDbsqlContext? DbciAcms { get; set; }
  INpdsDbsqlContext? DbciBridge { get; set; }
  INpdsDbsqlContext? DbciVocab { get; set; }
  INpdsDbsqlContext? DbciCache { get; set; }

} // end interface

// end file