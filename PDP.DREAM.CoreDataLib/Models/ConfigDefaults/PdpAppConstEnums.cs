// PdpAppConstEnums.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // this class contains nothing but constants and enums only
  // if enum value not set in code, it defaults to 0

  public enum AcgtCodeBranch
  {
    AorakiNet6, CervinNet7, GangkharNet8, TahtaliNetP
  }
  public const AcgtCodeBranch AcgtBranchDefault = AcgtCodeBranch.TahtaliNetP;

  public enum AcgtCodeRazor
  {
    Views, Pages, Components, All
  }
  public const AcgtCodeRazor AcgtRazorDefault = AcgtCodeRazor.All;

  public enum AcgtCodeDatabase
  {
    Core, PORTAL, DOORS, Nexus, Scribe, ACMS
  }
  public const AcgtCodeDatabase AcgtDatabaseDefault = AcgtCodeDatabase.Core;

  // enums list names for the configuration properties
  //
  // use prefix PdpSet for PDP site SETtings
  public enum NamesForSiteSettings : short
  {
    // app related
    PdpSetAppnameversion, PdpSetAppwebrootFilepath,
    // web related request and file paths
    PdpSetExtdeplibRqstpath, PdpSetExtdeplibFilepath,
    PdpSetSecuredocsRqstpath, PdpSetSecuredocsFilepath,
    PdpSetPublicdocsRqstpath, PdpSetPublicdocsFilepath,
    PdpSetWebimagesRqstpath, PdpSetWebimagesFilepath,
    PdpSetTestdataRqstpath, PdpSetTestdataFilepath,
    PdpSetFileprovRqstpath, PdpSetFileprovFilepath,
    // dev/test/debug related
    PdpSetDebugRouting, PdpSetDevtestFeature,
    // owner related
    PdpSetOwnerLongName, PdpSetOwnerShortName, PdpSetOwnerCodeName,
    PdpSetOwnerEmail, PdpSetHostEmail, PdpSetSiteTitle,
    // site content related
    PdpSetSiteDefArea, PdpSetSiteDefController, PdpSetSiteDefAction, PdpSetSiteDefView,
    PdpSetSiteDefHtml, PdpSetSiteDefPage, PdpSetSiteDefPath,
    PdpSetPageLayout, PdpSetPageMenu,
    PdpSetViewLayout, PdpSetViewMenu, 
    PdpSetBodyTitle,
    PdpSetHeaderImageLogo, PdpSetHeaderTitle, PdpSetHeaderTagLine, PdpSetHeaderSloganLine,
    PdpSetFooterCopyrightLine, PdpSetFooterCrosslinkLine, PdpSetFooterContactLine,
    PdpSetMetatagAuthor, PdpSetMetatagKeywords, PdpSetMetatagDescription,
    PdpSetSecUiaaName, PdpSetUseSecTok, PdpSetUseSecUiaa,
    PdpSetUseAreaDefault, PdpSetUsePageDefaults, PdpSetUseViewDefaults, 
    PdpSetUsePathDefaults, PdpSetUseDefPathStart, 
    PdpSetUseStaticFiles, PdpSetDatabaseProvider,
    // site services and api keys
    PdpSetUseSendGrid, PdpSetApiKeySendGrid,
    PdpSetApiKeyBingMaps, PdpSetApiKeyGoogleMaps,
    PdpSetApiKeyIeeeXplore, PdpSetApiKeyNlmPubMed
  }
  // use prefix NpdsDef for NPDS service DEFaults
  public enum NamesForServiceDefaults : short
  {
    NpdsDefSearchScope, NpdsDefSearchFilter,
    NpdsDefNodeType, NpdsDefServerType, NpdsDefServiceType,
    NpdsDefDatabaseType, NpdsDefDatabaseAccess, NpdsDefRecordAccess,
    NpdsDefResrepFormat, NpdsDefMessageFormat, NpdsDefQueryFormat,
    NpdsDefEntityType, NpdsDefNewEntityType,
    // NPDS components
    NpdsDefDiristryTag, NpdsDefRegistryTag, NpdsDefDirectoryTag, NpdsDefRegistrarTag
  }
  // use prefix NpdsCon for NPDS service CONstraints
  public enum NamesForServiceConstraints : short
  {
    NpdsConDiristryTag, NpdsConRegistryTag, NpdsConDirectoryTag, NpdsConRegistrarTag
  }

  // NPDS database Connection String Names for use with settings in web.config
  public enum NamesForRequiredDbConnStrings : short
  {
    NpdsSiaaDbserver, NpdsAcmsDbserver, NpdsCoreDbserver,
    NpdsNexusDiristry, NpdsPortalRegistry, NpdsDoorsDirectory, NpdsScribeRegistrar
  }
  public enum NamesForPermittedDbConnStrings : short
  {
    NpdsVocabDbserver, NpdsCacheDbserver,
    NpdsDiristryAuth2Dbserver, NpdsDiristryCacheDbserver, NpdsDiristryVocabDbserver,
    NpdsRegistryAuth2Dbserver, NpdsRegistryCacheDbserver, NpdsRegistryVocabDbserver,
    NpdsDirectoryAuth2Dbserver, NpdsDirectoryCacheDbserver, NpdsDirectoryVocabDbserver
  }
  // TODO:  recode with key/value pairs to allow arbitary naming in web.config;
  //     new property to separate term/thesaurus/ont servers (MeSH, MICAD, etc)
  //    as a group separate from the PORTAL/DOORS/Nexus servers
  public enum NamesForOptionalDbConnStrings : short
  {
    NpdsNlmmeshDbserver, NpdsNlmmicadDbserver
  }

  // ATTN: recall enums cannot be used in attributes so must also
  // use individual const strings above for roles specified in attributes
  public enum NamesForClientRoles : short
  { NpdsAnon, NpdsAuth, NpdsUser, NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin }

}
