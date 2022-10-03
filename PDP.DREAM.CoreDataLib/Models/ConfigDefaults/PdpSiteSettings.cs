// PdpSiteSettings.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;

using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Models;

public class PdpSiteSettings : PdpConfigManager // , IPdpSiteSettings
{
  public PdpSiteSettings(string projCodedir) : base(projCodedir)
  {
    if (pdpSiteCnfgMngr == null) { throw new NullReferenceException(); }

    AppFilepathWebroot = ParseAppStringSetting(NamesForSiteSettings.PdpSetAppwebrootFilepath,
 @$"{Directory.GetCurrentDirectory()}\wwwroot");

    AppRqstpathExtdeplib = ParseAppStringSetting(NamesForSiteSettings.PdpSetExtdeplibRqstpath);
    AppFilepathExtdeplib = ParseAppStringSetting(NamesForSiteSettings.PdpSetExtdeplibFilepath);

    AppRqstpathPublicdocs = ParseAppStringSetting(NamesForSiteSettings.PdpSetPublicdocsRqstpath);
    AppFilepathPublicdocs = ParseAppStringSetting(NamesForSiteSettings.PdpSetPublicdocsFilepath);

    AppRqstpathSecuredocs = ParseAppStringSetting(NamesForSiteSettings.PdpSetSecuredocsRqstpath);
    AppFilepathSecuredocs = ParseAppStringSetting(NamesForSiteSettings.PdpSetSecuredocsFilepath);

    AppRqstpathFileprov = ParseAppStringSetting(NamesForSiteSettings.PdpSetFileprovRqstpath);
    AppFilepathFileprov = ParseAppStringSetting(NamesForSiteSettings.PdpSetFileprovFilepath);

    AppUseDebugRouting = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetDebugRouting, false);
    AppUseDevtestFeature = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetDevtestFeature, false);
    AppNameVersion = ParseAppStringSetting(NamesForSiteSettings.PdpSetAppnameversion);
    AppOwnerLongName = ParseAppStringSetting(NamesForSiteSettings.PdpSetOwnerLongName);
    AppOwnerShortName = ParseAppStringSetting(NamesForSiteSettings.PdpSetOwnerShortName);
    AppOwnerCodeName = ParseAppStringSetting(NamesForSiteSettings.PdpSetOwnerCodeName);
    AppOwnerEmail = ParseAppStringSetting(NamesForSiteSettings.PdpSetOwnerEmail);
    AppHostEmail = ParseAppStringSetting(NamesForSiteSettings.PdpSetHostEmail);

    AppSiteTitle = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteTitle);
    AppSiteDefHtml = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefHtml);
    AppSiteDefPath = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefPath);
    AppSiteDefController = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefController);
    AppSiteDefAction = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefAction);
    AppSiteDefView = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefView);
    AppSiteDefPage = ParseAppStringSetting(NamesForSiteSettings.PdpSetSiteDefPage);

    WspldMetatagAuthor = ParseAppStringSetting(NamesForSiteSettings.PdpSetMetatagAuthor);
    WspldMetatagKeywords = ParseAppStringSetting(NamesForSiteSettings.PdpSetMetatagKeywords);
    WspldMetatagDescription = ParseAppStringSetting(NamesForSiteSettings.PdpSetMetatagDescription);

    WspldPageLayout = ParseAppStringSetting(NamesForSiteSettings.PdpSetPageLayout);
    WspldPageMenu = ParseAppStringSetting(NamesForSiteSettings.PdpSetPageMenu);
    WspldPageTitle = ParseAppStringSetting(NamesForSiteSettings.PdpSetPageTitle);
    WspldViewLayout = ParseAppStringSetting(NamesForSiteSettings.PdpSetViewLayout);
    WspldViewMenu = ParseAppStringSetting(NamesForSiteSettings.PdpSetViewMenu);
    WspldViewTitle = ParseAppStringSetting(NamesForSiteSettings.PdpSetViewTitle);

    WspldHeaderImageLogo = ParseAppStringSetting(NamesForSiteSettings.PdpSetHeaderImageLogo);
    WspldHeaderTitle = ParseAppStringSetting(NamesForSiteSettings.PdpSetHeaderTitle);
    WspldHeaderTagLine = ParseAppStringSetting(NamesForSiteSettings.PdpSetHeaderTagLine);
    WspldHeaderSloganLine = ParseAppStringSetting(NamesForSiteSettings.PdpSetHeaderSloganLine);

    WspldFooterCodebuildLine = $"Code Branch {PDPCC.PdpCodeBranch} Version {PDPCC.PdpCodeAsmver} Date {PDPCC.PdpCodeAsmdat.ToLocalTime()}<br />";
    WspldFooterCopyrightLine = ParseAppStringSetting(NamesForSiteSettings.PdpSetFooterCopyrightLine);
    WspldFooterCrosslinkLine = ParseAppStringSetting(NamesForSiteSettings.PdpSetFooterCrosslinkLine);
    WspldFooterContactLine = ParseAppStringSetting(NamesForSiteSettings.PdpSetFooterContactLine);

    AppSecureUiaaName = ParseAppStringSetting(NamesForSiteSettings.PdpSetSecUiaaName);
    AppUseSecurityToken = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseSecTok, false);
    AppUseSecureUiaa = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseSecUiaa, false);
    AppUsePageDefaults = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUsePageDefaults, false);
    AppUseViewDefaults = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseViewDefaults, false);
    AppUsePathDefaults = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUsePathDefaults, false);
    AppUseDefPathStart = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseDefPathStart, false);
    AppUseStaticFiles = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseStaticFiles, false);
    AppUseSendGrid = ParseAppBooleanSetting(NamesForSiteSettings.PdpSetUseSendGrid, false);
    AppDatabaseProvider = ParseAppStringSetting(NamesForSiteSettings.PdpSetDatabaseProvider);

    ApiKeySendGrid = ParseAppStringSetting(NamesForSiteSettings.PdpSetApiKeySendGrid);
    ApiKeyBingMaps = ParseAppStringSetting(NamesForSiteSettings.PdpSetApiKeyBingMaps);
    ApiKeyGoogleMaps = ParseAppStringSetting(NamesForSiteSettings.PdpSetApiKeyGoogleMaps);
    ApiKeyIeeeXplore = ParseAppStringSetting(NamesForSiteSettings.PdpSetApiKeyIeeeXplore);
    ApiKeyNlmPubMed = ParseAppStringSetting(NamesForSiteSettings.PdpSetApiKeyNlmPubMed);
  }

  // string properties in alphabetic order
  public string ApiKeyBingMaps { get; init; } = string.Empty;
  public string ApiKeyGoogleMaps { get; init; } = string.Empty;
  public string ApiKeyIeeeXplore { get; init; } = string.Empty;
  public string ApiKeyNlmPubMed { get; init; } = string.Empty;
  public string ApiKeySendGrid { get; init; } = string.Empty;
  public string AppDatabaseProvider { get; init; } = string.Empty;
  public string AppFilepathExtdeplib { get; init; } = string.Empty;
  public string AppFilepathFileprov { get; init; } = string.Empty;
  public string AppFilepathPublicdocs { get; init; } = string.Empty;
  public string AppFilepathSecuredocs { get; init; } = string.Empty;
  public string AppFilepathWebroot { get; init; } = string.Empty;
  public string AppHostEmail { get; init; } = string.Empty;
  public string AppRqstpathExtdeplib { get; init; } = string.Empty;
  public string AppRqstpathFileprov { get; init; } = string.Empty;
  public string AppRqstpathPublicdocs { get; init; } = string.Empty;
  public string AppRqstpathSecuredocs { get; init; } = string.Empty;
  public string AppRqstpathTestdata { get; init; } = string.Empty;
  public string AppRqstpathWebimages { get; init; } = string.Empty;
  public string AppNameVersion { get; init; } = string.Empty;
  public string AppOwnerCodeName { get; init; } = string.Empty;
  public string AppOwnerEmail { get; init; } = string.Empty;
  public string AppOwnerLongName { get; init; } = string.Empty;
  public string AppOwnerShortName { get; init; } = string.Empty;
  public string AppSecureUiaaName { get; init; } = string.Empty;
  public string AppSiteDefAction { get; init; } = string.Empty;
  public string AppSiteDefController { get; init; } = string.Empty;
  public string AppSiteDefHtml { get; init; } = string.Empty;
  public string AppSiteDefPage { get; init; } = string.Empty;
  public string AppSiteDefPath { get; init; } = string.Empty;
  public string AppSiteDefView { get; init; } = string.Empty;
  public string AppSiteTitle { get; init; } = string.Empty;


  // Wspld = Web Site Page Layout Defaults should be settable
  public string WspldFooterCodebuildLine { get; set; } = string.Empty;
  public string WspldFooterContactLine { get; set; } = string.Empty;
  public string WspldFooterCopyrightLine { get; set; } = string.Empty;
  public string WspldFooterCrosslinkLine { get; set; } = string.Empty;
  public string WspldHeaderImageLogo { get; set; } = string.Empty;
  public string WspldHeaderSloganLine { get; set; } = string.Empty;
  public string WspldHeaderTagLine { get; set; } = string.Empty;
  public string WspldHeaderTitle { get; set; } = string.Empty;
  public string WspldMetatagAuthor { get; set; } = string.Empty;
  public string WspldMetatagDescription { get; set; } = string.Empty;
  public string WspldMetatagKeywords { get; set; } = string.Empty;
  public string WspldPageLayout { get; set; } = string.Empty;
  public string WspldPageMenu { get; set; } = string.Empty;
  public string WspldPageTitle { get; set; } = string.Empty;
  public string WspldViewLayout { get; set; } = string.Empty;
  public string WspldViewMenu { get; set; } = string.Empty;
  public string WspldViewTitle { get; set; } = string.Empty;


  // boolean properties in alphabetic order
  public bool AppUseDebugRouting { get; init; } = false;
  public bool AppUseDefPathStart { get; init; } = false;
  public bool AppUseDevtestFeature { get; init; } = false;
  public bool AppUsePageDefaults { get; init; } = false;
  public bool AppUsePathDefaults { get; init; } = false;
  public bool AppUseSecureUiaa { get; init; } = false;
  public bool AppUseSecurityToken { get; init; } = false;
  public bool AppUseSendGrid { get; init; } = false;
  public bool AppUseStaticFiles { get; init; } = false;
  public bool AppUseViewDefaults { get; init; } = false;

  // guid properties in alphabetic order
  // AppSecureUiaaGuid retrieved from database must be settable
  public Guid AppSecureUiaaGuid { get; set; } = Guid.Empty;

} // end class

// end file