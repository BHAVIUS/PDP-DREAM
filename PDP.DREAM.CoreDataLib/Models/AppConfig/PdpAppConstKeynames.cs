// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // keynames for the configuration properties with
  // name/value pairs in application configuration settings

  // use PdpSet for PDP site SETtings
  public static class PdpSet
  {
    // app related
    public const string AppDisplayName = "PdpSetAppDisplayName";
    public const string AppDisplayVersion = "PdpSetAppDisplayVersion";
    public const string AppCodeNameLongLong = "PdpSetAppCodeNameLongLong";
    public const string AppCodeNameLongShort = "PdpSetAppCodeNameLongShort";
    public const string AppwebrootFilepath = "PdpSetAppwebrootFilepath";
    // web related request and file paths
    public const string ExtdeplibRqstpath = "PdpSetExtdeplibRqstpath";
    public const string ExtdeplibFilepath = "PdpSetExtdeplibFilepath";
    public const string SecuredocsRqstpath = "PdpSetSecuredocsRqstpath";
    public const string SecuredocsFilepath = "PdpSetSecuredocsFilepath";
    public const string PublicdocsRqstpath = "PdpSetPublicdocsRqstpath";
    public const string PublicdocsFilepath = "PdpSetPublicdocsFilepath";
    public const string WebimagesRqstpath = "PdpSetWebimagesRqstpath";
    public const string WebimagesFilepath = "PdpSetWebimagesFilepath";
    public const string TestdataRqstpath = "PdpSetTestdataRqstpath";
    public const string TestdataFilepath = "PdpSetTestdataFilepath";
    public const string FileprovRqstpath = "PdpSetFileprovRqstpath";
    public const string FileprovFilepath = "PdpSetFileprovFilepath";
    // dev/test/debug related
    public const string DebugRouting = "PdpSetDebugRouting";
    public const string DevtestFeature = "PdpSetDevtestFeature";
    // owner related
    public const string OwnerNameLong = "PdpSetOwnerNameLong";
    public const string OwnerNameShort = "PdpSetOwnerNameShort";
    public const string OwnerEmail = "PdpSetOwnerEmail";
    // site content related
    public const string HostEmail = "PdpSetHostEmail";
    public const string SiteDefTitle = "PdpSetSiteDefTitle";
    public const string SiteDefController = "PdpSetSiteDefController";
    public const string SiteDefAction = "PdpSetSiteDefAction";
    public const string SiteDefHtml = "PdpSetSiteDefHtml";
    public const string SiteDefPage = "PdpSetSiteDefPage";
    public const string SiteDefPath = "PdpSetSiteDefPath";
    public const string PageLayout = "PdpSetPageLayout";
    public const string PageMenu = "PdpSetPageMenu";
    public const string BodyTitle = "PdpSetBodyTitle";
    public const string HeaderImageLogo = "PdpSetHeaderImageLogo";
    public const string HeaderTitle = "PdpSetHeaderTitle";
    public const string HeaderTagLine = "PdpSetHeaderTagLine";
    public const string HeaderSloganLine = "PdpSetHeaderSloganLine";
    public const string FooterCopyrightLine = "PdpSetFooterCopyrightLine";
    public const string FooterCrosslinkLine = "PdpSetFooterCrosslinkLine";
    public const string FooterContactLine = "PdpSetFooterContactLine";
    public const string MetatagAuthor = "PdpSetMetatagAuthor";
    public const string MetatagKeywords = "PdpSetMetatagKeywords";
    public const string MetatagDescription = "PdpSetMetatagDescription";
    public const string SecCiaamName = "PdpSetSecCiaamName";
    public const string UseSecCiaam = "PdpSetUseSecCiaam";
    public const string UseSecAlias = "PdpSetUseSecAlias";
    public const string UseSecToken = "PdpSetUseSecToken";
    public const string UseExtensions = "PdpSetUseExtensions";
    public const string UsePageDefaults = "PdpSetUsePageDefaults";
    public const string UsePathDefaults = "PdpSetUsePathDefaults";
    public const string UseDefPathStart = "PdpSetUseDefPathStart";
    public const string UseStaticFiles = "PdpSetUseStaticFiles";
    public const string DatabaseProvider = "PdpSetDatabaseProvider";
    // site services and api keys
    public const string UseSendGrid = "PdpSetUseSendGrid";
    public const string ApiKeySendGrid = "PdpSetApiKeySendGrid";
    public const string UseSwagger = "PdpSetUseSwagger";
    public const string ApiKeyBingMaps = "PdpSetApiKeyBingMaps";
    public const string ApiKeyGoogleMaps = "PdpSetApiKeyGoogleMaps";
    public const string ApiKeyIeeeXplore = "PdpSetApiKeyIeeeXplore";
    public const string ApiKeyNlmPubMed = "PdpSetApiKeyNlmPubMed";
  }

  // use prefix NpdsDef for NPDS service DEFaults
  public static class NpdsDef
  {
    public const string SearchScope = "NpdsDefSearchScope";
    public const string SearchFilter = "NpdsDefSearchFilter";
    public const string NodeType = "NpdsDefNodeType";
    public const string ServerType = "NpdsDefServerType";
    public const string ServiceType = "NpdsDefServiceType";
    public const string DatabaseType = "NpdsDefDatabaseType";
    public const string DatabaseAccess = "NpdsDefDatabaseAccess";
    public const string RecordAccess = "NpdsDefRecordAccess";
    public const string ResrepFormat = "NpdsDefResrepFormat";
    public const string MessageFormat = "NpdsDefMessageFormat";
    public const string FieldFormat = "NpdsDefFieldFormat";
    public const string QueryFormat = "NpdsDefQueryFormat";
    public const string EntityType = "NpdsDefEntityType";
    public const string InfosetStatus = "NpdsDefInfosetStatus";
    public const string DiristryTag = "NpdsDefDiristryTag";  // Nexus
    public const string RegistryTag = "NpdsDefRegistryTag";  // PORTAL
    public const string DirectoryTag = "NpdsDefDirectoryTag";  // DOORS
    public const string RegistrarTag = "NpdsDefRegistrarTag";  // Scribe
    public const string ServiceTag = "NpdsDefServiceTag";  // Core
  }
  // use prefix NpdsCon for NPDS service CONstraints
  public static class NpdsCon
  {
    public const string DiristryTag = "NpdsConDiristryTag";
    public const string RegistryTag = "NpdsConRegistryTag";
    public const string DirectoryTag = "NpdsConDirectoryTag";
    public const string RegistrarTag = "NpdsConRegistrarTag";
  }

  // NPDS DataBase Connection String keynames for use with settings in web.config
  public static class NpdsDbcsReq // REQuired
  {
    public const string CiaamDbserver = "NpdsCiaamDbserver";
    public const string CoreDbserver = "NpdsCoreDbserver";
    public const string NexusDiristry = "NpdsNexusDiristry";
    public const string PortalRegistry = "NpdsPortalRegistry";
    public const string DoorsDirectory = "NpdsDoorsDirectory";
    public const string ScribeRegistrar = "NpdsScribeRegistrar";
  }
  public static class NpdsDbcsPer // PERmitted
  {
    public const string AcmsDbserver = "NpdsAcmsDbserver";
    public const string BridgeDbserver = "NpdsBridgeDbserver";
    public const string VocabDbserver = "NpdsVocabDbserver";
    public const string CacheDbserver = "NpdsCacheDbserver";
    public const string DiristryAuth2Dbserver = "NpdsDiristryAuth2Dbserver";
    public const string DiristryCacheDbserver = "NpdsDiristryCacheDbserver";
    public const string DiristryVocabDbserver = "NpdsDiristryVocabDbserver";
    public const string RegistryAuth2Dbserver = "NpdsRegistryAuth2Dbserver";
    public const string RegistryCacheDbserver = "NpdsRegistryCacheDbserver";
    public const string RegistryVocabDbserver = "NpdsRegistryVocabDbserver";
    public const string DirectoryAuth2Dbserver = "NpdsDirectoryAuth2Dbserver";
    public const string DirectoryCacheDbserver = "NpdsDirectoryCacheDbserver";
    public const string DirectoryVocabDbserver = "NpdsDirectoryVocabDbserver";
  }

  // TODO:  rebuild with key/value pairs to allow arbitary naming in web.config;
  //     new property to separate term/thesaurus/ont servers (MeSH, MICAD, etc)
  //    as a group separate from the PORTAL/DOORS/Nexus servers
  public static class NpdsDbcsExt // EXTended
  {
    public const string NlmmeshDbserver = "NpdsNlmmeshDbserver";
    public const string NlmmicadDbserver = "NpdsNlmmicadDbserver";
  }

}

