// NpdsConstantsPrcAis.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// PdpConstants for PDP applications
// NpdsConstants for NPDS specification model

public static partial class NpdsConst
{
  // this class contains nothing but constants and enums only
  // if enum value not set in code, it defaults to 0

  // PdpRestContext AI System

  // DatabaseType is focused on structure of backend database
  // DatabaseType determines possible ServiceTypes and ResRepFormats
  public enum DatabaseType { Core = 0, Nexus = 1, PORTAL = 2, DOORS = 3, Scribe = 4 };

  // DatabaseAccess determines service-based read/write access to entire database
  // Anonymous versus Authenticated and ReadOnly versus ReadWrite
  // higher privileges do not exclude lower privileges,
  // eg, AuthReadOnly includes AnonReadOnly, AuthReadWrite includes AuthReadOnly and AnonReadOnly
  public enum DatabaseAccess { None = 0, AnonReadOnly = 1, AuthReadOnly = 2, AuthReadWrite = 3 };

  // RecordAccess determines role-based read/write access privileges to individual records in database
  // Client is anonymous; User/Agent/Author/Editor/Admin are authenticated
  // User is authenticated while Agent is both authenticated and consented
  // Agent can see all transferrable records which may or may not include his own --- TODO: create a transferrable flag !!!
  // Author can only see own records
  // Editor can see all records in registry / directory / diristry if declared Editor for that registry / directory / diristry
  // Admin can see all records in registrar
  public enum RecordAccess { None = 0, Client = 1, User = 2, Agent = 3, Author = 4, Editor = 5, Admin = 6 };

  // Server Network NodeType
  public enum NodeType { None = 0, Forwarding = 1, Caching = 2, Authoritative = 3 }

  // ServerType is focused on overall function of NPDS component
  //   Registrar, Registry, Directory, and
  //   Diristry == 'Directory-Registry' == 'Registry-Directory'
  // ServiceType is focused on function of frontend webservice
  // ServiceType determines possible ResRepFormats returned for
  //    resource representations in response to requests
  // proper-named service with generic-termed server yields NPDS components with
  // Core root, Nexus diristry, PORTAL registry, DOORS directory, Scribe registrar
  public enum ServiceType { Core = 0, Nexus = 1, PORTAL = 2, DOORS = 3, Scribe = 4 };
  public enum ServerType { Root = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 4 };

  // Network SearchScope determines extent of distributed network nodes
  //    searched throughout NPDS;
  // Consider mapping Local/Regional/Global searches for forwarding/caching server
  //    to Answer/Related/Referred sections of NPDS response message;
  // however recall that an authoritative server should return only an
  //    Answer response for Local scope query directed at it without
  //    Related response for Regional scope query or
  //    Referred response for Global scope query
  // ie, Scope for Search over all network nodes
  // [Flags] // why bother with Flags attribute ???
  public enum SearchScope { None = 0, Local = 1, Regional = 2, Global = 3 }

  // NodeSearchFilter determines filter constraints on search
  //   at a given node where search through records filtered
  //   by constraints on the infoset guidrefs  and/or tags
  // ATTN:  so see also tag constraints
  // ie, Filter for Search within a network node
  // [Flags] // why bother with Flags attribute ???
  public enum SearchFilter { None = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 5, AllTags = 10, AllGuids = 11 }

  // FieldRule determines restrictions/conditions on record fields in the different formats
  // Prohibited and Optional are unnamed and unspecified by NPDS standard
  // Permitted and Required are named and specified by NPDS standard
  public enum FieldRule { None = 0, Required = 1, Permitted = 2, Optional = 3, Prohibited = 4 };

  // FieldFormat determines the structured syntax format for content of fields especially
  // on DOORS side for semantic analysis, but also possibly on PORTAL side for lexical analysis (eg, OtherText)
  // and be sure to include our new convention of 0 for none meaning no filter or in this case
  public enum FieldFormat { None = 0, FreeForm = 1, JSON = 2, XML = 3, RDF = 4, OWL = 5, HTML = 10, XHTML = 11 }

  // ResrepFormat is the format for the NPDS resource representation
  // ATTN: note distinction with http message format and NPDS message representation
  public enum ResrepFormat { Core = 0, Nexus = 1, PORTAL = 2, DOORS = 3, Scribe = 4 };

  // MessageFormat is the Message Format returned from RESTful APIs as the http response to http request
  public enum MessageFormat { None = 0, JSON = 1, XML = 2, XHTML = 3 }; // other formats?

  // public enum HttpResponseFormat... 
  // http://www.iana.org/assignments/media-types/media-types.xhtml

  //
  // use explicit integer codes for those fields stored in database records
  // enums corresponding to those stored in database records are meant to be extensible
  // int <= 100 PDP reserved, int >= 101 PDP optional

  public enum InfosetStatus : short
  {
    None = 0, Unknown = 7,
    // generic terms (odds true, evens false)
    Valid = 1, Invalid = 2, SyntaxValid = 3, SyntaxInvalid = 4, SemanticsValid = 5, SemanticsInvalid = 6,
    ConceptValid = 11, ConceptInvalid = 12, AddressValid = 13, AddressInvalid = 14,
    // format-specific terms
    JsonInvalid = 20, JsonValid = 21, JsonValidLax = 22, JsonValidStrict = 23,
    XmlInvalid = 30, XmlValid = 31, XmlValidLax = 32, XmlValidStrict = 33,
    RdfInvalid = 40, RdfValid = 41, RdfValidLax = 42, RdfValidStrict = 43,
    OwlInvalid = 50, OwlValid = 51, OwlValidLax = 52, OwlValidStrict = 53,
    HtmlInvalid = 60, HtmlValid = 61, HtmlValidLax = 62, HtmlValidStrict = 63,
    XhtmlInvalid = 70, XhtmlValid = 71, XhtmlValidLax = 72, XhtmlValidStrict = 73,
    AnyAndAll = 100
  }
  public const InfosetStatus DefaultInfosetStatusItem = InfosetStatus.Unknown;
  public const short DefaultInfosetStatusCode = (short)InfosetStatus.Unknown;

  public enum EntityType : short
  {
    None = 0, Untyped = 7,
    NpdsRoot = 1,
    NpdsRegistrar = 2,
    NpdsRegistry = 3,
    NpdsDirectory = 4,
    NpdsDiristry = 5,
    PortalRoot = 10,
    PortalPrimary = 11,
    PortalSecondary = 12,
    DoorsRoot = 20,
    DoorsPrimary = 21,
    DoorsSecondary = 22,
    NexusRoot = 30,
    NexusPrimary = 31,
    NexusSecondary = 32,
    Organization = 40,
    Person = 50,
    OfflineRealEntity = 60,
    PhysicalObject = 61,
    ChemicalSubstance = 62,
    BiologicalBeing = 63,
    GeographicLocation = 64,
    OnlineVirtualEntity = 70,
    TerminologyItem = 71,
    TaxonomyItem = 72,
    ThesaurusItem = 73,
    Ontology = 74,
    Example = 75,
    Principle = 76,
    DataRecord = 77,
    ComputingTool = 78,
    ComputingService = 79,
    Publication = 80,
    AudioItem = 81,
    ImageItem = 82,
    VideoItem = 83,
    MultiMediaItem = 84,
    OneDimSequence = 90,
    MultiDimSignal = 91,
    MetaResourceEntity = 99,
    AnyAndAll = 100
  }
  public const EntityType DefaultEntityTypeItem = EntityType.Untyped;
  public const short DefaultEntityTypeCode = (short)EntityType.Untyped;

  public const byte DefaultIndex = 1; // used by ServiceRestriction And/Or
  public const byte DefaultPriority = 255;

}
