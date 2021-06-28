using System;

using PDP.DREAM.NpdsCoreLib.Utilities;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public class PdpSiteSettings
  {
    public static PdpSiteSettings GetValues { get; } = new PdpSiteSettings();

    // private constructor prevents instantiation outside the class
    private PdpSiteSettings()
    {
      AppFilepathExtdeplib = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetExtdeplibFilepath);
      AppFilepathWebimages = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetWebimagesFilepath);
      AppFilepathPublicdocs = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPublicdocsFilepath);
      AppFilepathSecuredocs = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSecuredocsFilepath);
      AppFilepathTestdata = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetTestdataFilepath);
      AppUseDebugRouting = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetDebugRouting, false);
      AppUseDevtestFeature = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetDevtestFeature, false);
      AppNameVersion = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetAppNameVersion);
      AppOwnerName = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerName);
      AppOwnerAcronym = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerAcronym);
      AppOwnerEmail = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetOwnerEmail);
      AppHostEmail = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetHostEmail);
      AppPageLayout = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageLayout);
      AppPageMenu = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageMenu);
      AppPageTitle = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetPageTitle);
      AppSiteTitle = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSiteTitle);
      AppSecureUiaaName = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetSecUiaaName);
      AppUseSecurityToken = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSecTok, false);
      AppUseSecureUiaa = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSecUiaa, false);
      AppUseSendGrid = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSendGrid, false);
      AppUseSiteHtmlFile = ConfigManager.ParseAppBooleanSetting(PdpConst.NamesForSiteSettings.PdpSetUseSiteHtmlFile, false);
      AppDatabaseProvider = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetDatabaseProvider);
      ApiKeySendGrid = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeySendGrid);
      ApiKeyBingMaps = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyBingMaps);
      ApiKeyGoogleMaps = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyGoogleMaps);
      ApiKeyIeeeXplore = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyIeeeXplore);
      ApiKeyNlmPubMed = ConfigManager.ParseAppStringSetting(PdpConst.NamesForSiteSettings.PdpSetApiKeyNlmPubMed);
    }

    public string ApiKeyBingMaps { get; init; } = string.Empty;
    public string ApiKeyGoogleMaps { get; init; } = string.Empty;
    public string ApiKeyIeeeXplore { get; init; } = string.Empty;
    public string ApiKeyNlmPubMed { get; init; } = string.Empty;
    public string ApiKeySendGrid { get; init; } = string.Empty;


    public string AppDatabaseProvider { get; init; } = string.Empty;
    public string AppFilepathExtdeplib { get; init; } = string.Empty;
    public string AppFilepathPublicdocs { get; init; } = string.Empty;
    public string AppFilepathSecuredocs { get; init; } = string.Empty;
    public string AppFilepathTestdata { get; init; } = string.Empty;
    public string AppFilepathWebimages { get; init; } = string.Empty;
    public string AppHostEmail { get; init; } = string.Empty;
    public string AppSecureUiaaName { get; init; } = string.Empty;
    public string AppNameVersion { get; init; } = string.Empty;
    public string AppOwnerAcronym { get; init; } = string.Empty;
    public string AppOwnerEmail { get; init; } = string.Empty;
    public string AppOwnerName { get; init; } = string.Empty;
    public string AppPageLayout { get; init; } = string.Empty;
    public string AppPageMenu { get; init; } = string.Empty;
    public string AppPageTitle { get; init; } = string.Empty;
    public string AppSiteTitle { get; init; } = string.Empty;

    public bool AppUseDebugRouting { get; init; } = false;
    public bool AppUseDevtestFeature { get; init; } = false;
    public bool AppUseSecureUiaa { get; init; } = false;
    public bool AppUseSecurityToken { get; init; } = false;
    public bool AppUseSendGrid { get; init; } = false;
    public bool AppUseSiteHtmlFile { get; init; } = false;

    // properties retrieved from database must be settable
    public Guid AppSecureUiaaGuid { get; set; } = Guid.Empty;
  }

}