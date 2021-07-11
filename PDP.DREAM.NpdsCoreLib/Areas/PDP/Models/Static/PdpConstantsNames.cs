// PdpConstantsNames.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public static partial class PdpConst
  {
    // this class contains nothing but constants and enums only
    // if enum value not set in code, it defaults to 0

    // enums list names for the configuration properties, not the actual values of the properties;

    // use prefix PdpSet for PDP site SETtings
    public enum NamesForSiteSettings
    {
      PdpSetExtdeplibFilepath, PdpSetWebimagesFilepath, PdpSetPublicdocsFilepath,
      PdpSetSecuredocsFilepath, PdpSetTestdataFilepath,
      PdpSetDebugRouting, PdpSetDevtestFeature, PdpSetAppNameVersion,
      PdpSetOwnerName, PdpSetOwnerAcronym, PdpSetOwnerEmail, PdpSetHostEmail,
      PdpSetPageLayout, PdpSetPageMenu, PdpSetPageTitle, PdpSetSiteTitle,
      PdpSetSecUiaaName, PdpSetUseSecTok, PdpSetUseSecUiaa,
      PdpSetUseSendGrid, PdpSetUseSiteHtmlFile, PdpSetDatabaseProvider,
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

    // TODO: move path constants to static file for PdpDefaultRoutes
    // TODO: add other path constants for PdpAnon and PdpdAuth controllers
    //
    // initial slash assures path from web app root
    public const string PdpPathUserIndex = "/PDP/User/Index";
    public const string PdpPathAgentIndex = "/PDP/Agent/Index";
    public const string PdpPathIdentRequired = "/PDP/Anon/AccessDenied";
    public const string PdpPathIdentLogin = "/PDP/Anon/LoginUser";
    public const string PdpPathIdentLogout = "/PDP/Auth/LogoutUser";
    public const string PdpPathIdentProfile = "/PDP/Auth/DisplayProfile";
    public const string PdpPathIdentManage = "/PDP/Admin/Index";
    public const string PdpPathSiteIndex = "/PDP/Site/Index";
    public const string PdpPathSiteInfo = "/PDP/Site/Info";
    public const string PdpPathSiteHelp = "/PDP/Site/Help";
    public const string PdpPathSiteTest = "/PDP/NpdsTestLib/PrcTest";
    public const string PdpPathSiteError = "/PDP/NpdsTestLib/MvcError";
    public const string PdpPathSiteRoutes = "/PDP/NpdsTestLib/Routes";

    //PDP area MVC default constants
    public const string PdpMvcArea = "PDP";
    public const string PdpMvcController = "Site";
    public const string PdpMvcAltController = "AnonResreps";
    public const string PdpMvcAction = "Info";
    public const string PdpMvcAltAction = "Help";
    public const string PdpMvcUser = "User";
    public const string PdpMvcAgent = "Agent";

    // TODO: eliminate redundancy with NpdsServiceDefaults
    public const string PdpIdentityDbConnName = "NpdsUserDbserver";
    // Used for cookie session authorization
    public const string PdpIdentityScheme = "PDP-SIAA-Cookies";
    // Used for XSRF protection when adding external logins
    public const string PdpIdentityXsrfKey = "XsrfId";
    // Used for resetting stampes with invalid error message
    public const string PdpInvalidToken = "INVALID";

    // used for RouteDebugger Utility
    public const string PdpDebugRouteQueryKey = "debug";
    public const string PdpHelpRouteQueryKey = "help";
    public const string PdpHelpRouteHackKey = "??";

  }

}
