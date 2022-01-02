// PdpSiteSettings.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;
public class PdpSiteSettings
{
  public static PdpSiteSettings Values { get; } = new PdpSiteSettings();

  // private constructor prevents instantiation outside the class
  private PdpSiteSettings()
  {
    AppRqstpathExtdeplib = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetExtdeplibRqstpath);
    AppFilepathExtdeplib = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetExtdeplibFilepath);

    AppRqstpathPublicdocs = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPublicdocsRqstpath);
    AppFilepathPublicdocs = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPublicdocsFilepath);

    AppRqstpathSecuredocs = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSecuredocsRqstpath);
    AppFilepathSecuredocs = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSecuredocsFilepath);

    AppRqstpathWebimages = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetWebimagesRqstpath);
    AppFilepathWebimages = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetWebimagesFilepath);

    AppRqstpathTestdata = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetTestdataRqstpath);
    AppFilepathTestdata = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetTestdataFilepath);

    AppRqstpathFileprov = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetFileprovRqstpath);
    AppFilepathFileprov = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetFileprovFilepath);

    AppUseDebugRouting = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetDebugRouting, false);
    AppUseDevtestFeature = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetDevtestFeature, false);
    AppNameVersion = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetAppNameVersion);
    AppOwnerName = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerName);
    AppOwnerAcronym = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerAcronym);
    AppOwnerEmail = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerEmail);
    AppHostEmail = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHostEmail);

    AppSiteTitle = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteTitle);
    AppSiteMvcDefArea = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefArea);
    AppSiteMvcDefController = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefController);
    AppSiteMvcDefAction = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefAction);
    AppSiteMvcDefPath = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefPath);
    AppSiteMvcDefLayoutView = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefLayoutView);
    AppSiteMvcDefLayoutPage = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteMvcDefLayoutPage);

    WspldMetatagAuthor = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetMetatagAuthor);
    WspldMetatagKeywords = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetMetatagKeywords);
    WspldMetatagDescription = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetMetatagDescription);

    WspldViewLayout = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetViewLayout);
    WspldPageLayout = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageLayout);
    WspldPageMenu = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageMenu);
    WspldPageTitle = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageTitle);

    WspldHeaderImageLogo = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHeaderImageLogo);
    WspldHeaderTitle = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHeaderTitle);
    WspldHeaderTagLine = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHeaderTagLine);
    WspldHeaderSloganLine = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHeaderSloganLine);

    WspldFooterCopyrightLine = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetFooterCopyrightLine);
    WspldFooterCrosslinkLine = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetFooterCrosslinkLine);
    WspldFooterContactLine = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetFooterContactLine);

    AppSecureUiaaName = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSecUiaaName);
    AppUseSecurityToken = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSecTok, false);
    AppUseSecureUiaa = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSecUiaa, false);
    AppUseSendGrid = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSendGrid, false);
    AppUsePageDefaults = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUsePageDefaults, false);
    AppUseSiteHtmlFile = PdpConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSiteHtmlFile, false);
    AppDatabaseProvider = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetDatabaseProvider);

    ApiKeySendGrid = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeySendGrid);
    ApiKeyBingMaps = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyBingMaps);
    ApiKeyGoogleMaps = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyGoogleMaps);
    ApiKeyIeeeXplore = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyIeeeXplore);
    ApiKeyNlmPubMed = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyNlmPubMed);
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
  public string AppFilepathTestdata { get; init; } = string.Empty;
  public string AppFilepathWebimages { get; init; } = string.Empty;
  public string AppHostEmail { get; init; } = string.Empty;
  public string AppRqstpathExtdeplib { get; init; } = string.Empty;
  public string AppRqstpathFileprov { get; init; } = string.Empty;
  public string AppRqstpathPublicdocs { get; init; } = string.Empty;
  public string AppRqstpathSecuredocs { get; init; } = string.Empty;
  public string AppRqstpathTestdata { get; init; } = string.Empty;
  public string AppRqstpathWebimages { get; init; } = string.Empty;
  public string AppNameVersion { get; init; } = string.Empty;
  public string AppOwnerAcronym { get; init; } = string.Empty;
  public string AppOwnerEmail { get; init; } = string.Empty;
  public string AppOwnerName { get; init; } = string.Empty;
  public string AppSecureUiaaName { get; init; } = string.Empty;
  public string AppSiteMvcDefAction { get; init; } = string.Empty;
  public string AppSiteMvcDefArea { get; init; } = string.Empty;
  public string AppSiteMvcDefController { get; init; } = string.Empty;
  public string AppSiteMvcDefLayoutPage { get; init; } = string.Empty;
  public string AppSiteMvcDefLayoutView { get; init; } = string.Empty;
  public string AppSiteMvcDefPath { get; init; } = string.Empty;
  public string AppSiteTitle { get; init; } = string.Empty;


  // Wspld = Web Site Page Layout Defaults should be settable
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


  // boolean properties in alphabetic order
  public bool AppUseDebugRouting { get; init; } = false;
  public bool AppUseDevtestFeature { get; init; } = false;
  public bool AppUsePageDefaults { get; init; } = false;
  public bool AppUseSecureUiaa { get; init; } = false;
  public bool AppUseSecurityToken { get; init; } = false;
  public bool AppUseSendGrid { get; init; } = false;
  public bool AppUseSiteHtmlFile { get; init; } = false;

  // guid properties in alphabetic order
  public Guid AppSecureUiaaGuid { get; set; } = Guid.Empty; // property retrieved from database must be settable

} // class
