// INpdsClient.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// IQebiUserRestContext : INpdsClient : IQebiClient
public interface INpdsClient : IQebiClient
{
  Guid? ClientAgentInfosetGuid { get; set; }

  bool ItemCanBeAccessed { get; }
  bool ItemCanBeVerbosed { get; }
  bool ItemDoesArchive { get; }
  bool ItemDoesVerbose { get; }
  bool ItemIsConcise { get; set; }
  bool ItemIsPrivate { get; set; }

  // QueryString Key Values for search parameters
  // TODO: refactor to a dictionary of strings
  string? QskLexLabAny { get; set; }
  string? QskLexLabCan { get; set; }
  string? QskLexLabAls { get; set; }
  string? QskLexLabSup { get; set; }
  string? QskLexTagAny { get; set; }
  string? QskLexTagSup { get; set; }
  string? QskLexOText { get; set; }

 
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
  string? QebiDbconstr { get; set; }
  string? CoreDbconstr { get; set; }
  string? NexusDbconstr { get; set; }
  string? PortalDbconstr { get; set; }
  string? DoorsDbconstr { get; set; }
  string? ScribeDbconstr { get; set; }
  string? AcmsDbconstr { get; set; }
  string? VocabDbconstr { get; set; }
  string? CacheDbconstr { get; set; }


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

  NpdsDatabaseAccess DatabaseAccess { get; set; }
  NpdsDatabaseType DatabaseType { get; set; }
  NpdsEntityType EntityType { get; set; }
  //  NpdsFieldFormat FieldFormat { get; set; }
  //  NpdsFieldRule FieldRule { get; set; }
  NpdsInfosetStatus InfosetStatus { get; set; }
  NpdsMessageFormat MessageFormat { get; set; }
  NpdsNodeType NodeType { get; set; }
  NpdsRecordAccess RecordAccess { get; set; }
  NpdsResrepFormat ResrepFormat { get; set; }
  NpdsSearchFilter SearchFilter { get; set; }
  NpdsSearchScope SearchScope { get; set; }
  NpdsServerType ServerType { get; set; }
  NpdsServiceType ServiceType { get; set; }

  string? StatusName { get; set; }
  string? StatusNote { get; set; }
  string? StatusXhtml { get; set; }
  string? ServiceError { get; set; }
  string? ServiceNote { get; set; }

  string? UrlDebug { get; set; }
  string? UrlHelp { get; set; }
  string? UrlHelpDebug { get; set; }
  
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

} // end interface

// end file