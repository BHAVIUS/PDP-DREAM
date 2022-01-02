// PdpConstNames.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpConst
{
  // this class contains nothing but constants and enums only
  // if enum value not set in code, it defaults to 0

  // enums list names for the configuration properties, not the actual values of the properties;

  // use prefix PdpSet for PDP site SETtings
  public enum NamesForSiteSettings
  {
    PdpSetExtdeplibRqstpath, PdpSetExtdeplibFilepath,
    PdpSetSecuredocsRqstpath, PdpSetSecuredocsFilepath,
    PdpSetPublicdocsRqstpath, PdpSetPublicdocsFilepath,
    PdpSetWebimagesRqstpath, PdpSetWebimagesFilepath,
    PdpSetTestdataRqstpath, PdpSetTestdataFilepath,
    PdpSetFileprovRqstpath, PdpSetFileprovFilepath,
    PdpSetDebugRouting, PdpSetDevtestFeature, PdpSetAppNameVersion,
    PdpSetOwnerName, PdpSetOwnerAcronym, PdpSetOwnerEmail, PdpSetHostEmail, PdpSetSiteTitle,
    PdpSetSiteMvcDefArea, PdpSetSiteMvcDefController, PdpSetSiteMvcDefAction, PdpSetSiteMvcDefPath,
    PdpSetSiteMvcDefLayoutView, PdpSetSiteMvcDefLayoutPage,
    PdpSetViewLayout, PdpSetPageLayout, PdpSetPageMenu, PdpSetPageTitle,
    PdpSetHeaderImageLogo, PdpSetHeaderTitle, PdpSetHeaderTagLine, PdpSetHeaderSloganLine,
    PdpSetFooterCopyrightLine, PdpSetFooterCrosslinkLine, PdpSetFooterContactLine,
    PdpSetMetatagAuthor, PdpSetMetatagKeywords, PdpSetMetatagDescription,
    PdpSetSecUiaaName, PdpSetUseSecTok, PdpSetUseSecUiaa, PdpSetUseSendGrid,
    PdpSetUsePageDefaults, PdpSetUseSiteHtmlFile, PdpSetDatabaseProvider,
    PdpSetApiKeySendGrid, PdpSetApiKeyBingMaps, PdpSetApiKeyGoogleMaps,
    PdpSetApiKeyIeeeXplore, PdpSetApiKeyNlmPubMed
  }
  // use prefix NpdsDef for NPDS service DEFaults
  public enum NamesForServiceDefaults
  {
    NpdsDefSearchScope, NpdsDefSearchFilter, NpdsDefNodeType, NpdsDefServerType, NpdsDefServiceType,
    NpdsDefDatabaseType, NpdsDefDatabaseAccess, NpdsDefRecordAccess,
    NpdsDefResrepFormat, NpdsDefMessageFormat, NpdsDefQueryFormat,
    NpdsDefEntityType, NpdsDefNewEntityType,
    // NPDS components
    NpdsDefDiristryTag, NpdsDefRegistryTag, NpdsDefDirectoryTag, NpdsDefRegistrarTag
  }
  // use prefix NpdsCon for NPDS service CONstraints
  public enum NamesForServiceConstraints
  {
    NpdsConDiristryTag, NpdsConRegistryTag, NpdsConDirectoryTag, NpdsConRegistrarTag
  }

  // NPDS database Connection String Names for use with settings in web.config
  public enum NamesForRequiredDbConnStrings
  {
    NpdsUserDbserver, NpdsAgentDbserver, NpdsCoreDbserver,
    NpdsNexusDiristry, NpdsPortalRegistry, NpdsDoorsDirectory, NpdsScribeRegistrar
  }
  public enum NamesForPermittedDbConnStrings
  {
    NpdsRegistryAuth1Dbserver, NpdsRegistryAuth2Dbserver, NpdsRegistryCacheDbserver,
    NpdsDirectoryAuth1Dbserver, NpdsDirectoryAuth2Dbserver, NpdsDirectoryCacheDbserver,
    NpdsDiristryAuth1Dbserver, NpdsDiristryAuth2Dbserver, NpdsDiristryCacheDbserver
  }
  // TODO:  recode with key/value pairs to allow arbitary naming in web.config;
  //     new property to separate term/thesaurus/ont servers (MeSH, MICAD, etc)
  //    as a group separate from the PORTAL/DOORS/Nexus servers
  public enum NamesForOptionalDbConnStrings
  {
    // TODO: generalize to eliminate years from Nlmmesh and Nlmmicad servers
    NpdsNlmmeshDbserver, NpdsNlmmicadDbserver,
    // TODO: registrars are temporary transitional hack only, eliminate these options
    NpdsBhaRegistrarDbserver, NpdsGtgRegistrarDbserver, NpdsPdpRegistrarDbserver
  }

  // ATTN: recall enums cannot be used in attributes so must also
  // use individual const strings above for roles specified in attributes
  public enum IdentityRoleNames
  { NpdsUser = 1, NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin }

}
