// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class PdpSiteSettings : PdpConfigManager
{
  public PdpSiteSettings() : base()
  {
    base.Configure(); // configures pdpSiteConfig in PdpConfigManager
    pdpSiteConfig.CatchNullObject(nameof(pdpSiteConfig), nameof(PdpSiteSettings));

    AppFilepathWebroot = ParseAppStringSetting(PdpSet.AppwebrootFilepath,
      @$"{PDPCC.PdpCodePrgroot}\{PdpSiteDefaultWebroot}");

    AppRqstpathExtdeplib = ParseAppStringSetting(PdpSet.ExtdeplibRqstpath);
    AppFilepathExtdeplib = ParseAppStringSetting(PdpSet.ExtdeplibFilepath);

    AppRqstpathPublicdocs = ParseAppStringSetting(PdpSet.PublicdocsRqstpath);
    AppFilepathPublicdocs = ParseAppStringSetting(PdpSet.PublicdocsFilepath);

    AppRqstpathSecuredocs = ParseAppStringSetting(PdpSet.SecuredocsRqstpath);
    AppFilepathSecuredocs = ParseAppStringSetting(PdpSet.SecuredocsFilepath);

    AppRqstpathFileprov = ParseAppStringSetting(PdpSet.FileprovRqstpath);
    AppFilepathFileprov = ParseAppStringSetting(PdpSet.FileprovFilepath);

    AppUseDebugRouting = ParseAppBooleanSetting(PdpSet.DebugRouting, false);
    AppUseDevtestFeature = ParseAppBooleanSetting(PdpSet.DevtestFeature, false);

    AppDisplayName = ParseAppStringSetting(PdpSet.AppDisplayName);
    AppDisplayVersion = ParseAppStringSetting(PdpSet.AppDisplayVersion);
    AppCodeNameShort = ParseAppStringSetting(PdpSet.AppCodeNameLongShort);
    AppCodeNameLong = ParseAppStringSetting(PdpSet.AppCodeNameLongLong);

    AppOwnerNameLong = ParseAppStringSetting(PdpSet.OwnerNameLong);
    AppOwnerNameShort = ParseAppStringSetting(PdpSet.OwnerNameShort);
    AppOwnerEmail = ParseAppStringSetting(PdpSet.OwnerEmail);
    AppHostEmail = ParseAppStringSetting(PdpSet.HostEmail);

    AppSiteDefTitle = ParseAppStringSetting(PdpSet.SiteDefTitle);
    AppSiteDefHtml = ParseAppStringSetting(PdpSet.SiteDefHtml);
    AppSiteDefPath = ParseAppStringSetting(PdpSet.SiteDefPath);
    AppSiteDefController = ParseAppStringSetting(PdpSet.SiteDefController);
    AppSiteDefAction = ParseAppStringSetting(PdpSet.SiteDefAction);
    AppSiteDefPage = ParseAppStringSetting(PdpSet.SiteDefPage);

    AppSecureCiaamName = ParseAppStringSetting(PdpSet.SecCiaamName);
    AppUseSecureCiaam = ParseAppBooleanSetting(PdpSet.UseSecCiaam, false);
    AppUseSecureAlias = ParseAppBooleanSetting(PdpSet.UseSecAlias, false);
    AppUseSecurityToken = ParseAppBooleanSetting(PdpSet.UseSecToken, false);
    AppUseExtensions = ParseAppBooleanSetting(PdpSet.UseExtensions, false);
    AppUsePageDefaults = ParseAppBooleanSetting(PdpSet.UsePageDefaults, false);
    AppUsePathDefaults = ParseAppBooleanSetting(PdpSet.UsePathDefaults, false);
    AppUseDefPathStart = ParseAppBooleanSetting(PdpSet.UseDefPathStart, false);
    AppUseStaticFiles = ParseAppBooleanSetting(PdpSet.UseStaticFiles, false);
    AppUseSendGrid = ParseAppBooleanSetting(PdpSet.UseSendGrid, false);
    AppUseSwagger = ParseAppBooleanSetting(PdpSet.UseSwagger, false);
    AppDatabaseProvider = ParseAppStringSetting(PdpSet.DatabaseProvider);

    ApiKeySendGrid = ParseAppStringSetting(PdpSet.ApiKeySendGrid);
    ApiKeyBingMaps = ParseAppStringSetting(PdpSet.ApiKeyBingMaps);
    ApiKeyGoogleMaps = ParseAppStringSetting(PdpSet.ApiKeyGoogleMaps);
    ApiKeyIeeeXplore = ParseAppStringSetting(PdpSet.ApiKeyIeeeXplore);
    ApiKeyNlmPubMed = ParseAppStringSetting(PdpSet.ApiKeyNlmPubMed);

    WspldMetatagAuthor = ParseAppStringSetting(PdpSet.MetatagAuthor);
    WspldMetatagKeywords = ParseAppStringSetting(PdpSet.MetatagKeywords);
    WspldMetatagDescription = ParseAppStringSetting(PdpSet.MetatagDescription);

    WspldPageLayout = ParseAppStringSetting(PdpSet.PageLayout);
    WspldPageMenu = ParseAppStringSetting(PdpSet.PageMenu);
    WspldBodyTitle = ParseAppStringSetting(PdpSet.BodyTitle);

    WspldHeaderImageLogo = ParseAppStringSetting(PdpSet.HeaderImageLogo);
    WspldHeaderTitle = ParseAppStringSetting(PdpSet.HeaderTitle);
    WspldHeaderTagLine = ParseAppStringSetting(PdpSet.HeaderTagLine);
    WspldHeaderSloganLine = ParseAppStringSetting(PdpSet.HeaderSloganLine);

    WspldFooterCopyrightLine = ParseAppStringSetting(PdpSet.FooterCopyrightLine);
    WspldFooterCrosslinkLine = ParseAppStringSetting(PdpSet.FooterCrosslinkLine);
    WspldFooterContactLine = ParseAppStringSetting(PdpSet.FooterContactLine);

    WspldFooterCodebuildLine = PDPCC?.PdpCodeBldstr ?? ESS;
  }

  // boolean properties in alphabetic order
  public bool AppUseDebugRouting { get; init; } = false;
  public bool AppUseDefPathStart { get; init; } = false;
  public bool AppUseDevtestFeature { get; init; } = false;
  public bool AppUseExtensions { get; init; } = false;
  public bool AppUsePageDefaults { get; init; } = false;
  public bool AppUsePathDefaults { get; init; } = false;
  public bool AppUseSecureAlias { get; init; } = false;
  public bool AppUseSecureCiaam { get; init; } = false;
  public bool AppUseSecurityToken { get; init; } = false;
  public bool AppUseSendGrid { get; init; } = false;
  public bool AppUseSwagger { get; init; } = false;
  public bool AppUseStaticFiles { get; init; } = false;

  // string properties in alphabetic order
  public string ApiKeyBingMaps { get; init; } = ESS;
  public string ApiKeyGoogleMaps { get; init; } = ESS;
  public string ApiKeyIeeeXplore { get; init; } = ESS;
  public string ApiKeyNlmPubMed { get; init; } = ESS;
  public string ApiKeySendGrid { get; init; } = ESS;
  public string AppCodeNameLong { get; init; } = ESS;
  public string AppCodeNameShort { get; init; } = ESS;
  public string AppDisplayName { get; init; } = ESS;
  public string AppDisplayVersion { get; init; } = ESS;
  public string AppDatabaseProvider { get; init; } = ESS;
  public string AppFilepathExtdeplib { get; init; } = ESS;
  public string AppFilepathFileprov { get; init; } = ESS;
  public string AppFilepathPublicdocs { get; init; } = ESS;
  public string AppFilepathSecuredocs { get; init; } = ESS;
  public string AppFilepathWebroot { get; init; } = ESS;
  public string AppHostEmail { get; init; } = ESS;
  public string AppRqstpathExtdeplib { get; init; } = ESS;
  public string AppRqstpathFileprov { get; init; } = ESS;
  public string AppRqstpathPublicdocs { get; init; } = ESS;
  public string AppRqstpathSecuredocs { get; init; } = ESS;
  public string AppRqstpathTestdata { get; init; } = ESS;
  public string AppRqstpathWebimages { get; init; } = ESS;
  public string AppOwnerEmail { get; init; } = ESS;
  public string AppOwnerNameLong { get; init; } = ESS;
  public string AppOwnerNameShort { get; init; } = ESS;
  public string AppSecureCiaamName { get; init; } = ESS;
  public string AppSiteDefAction { get; init; } = ESS;
  public string AppSiteDefController { get; init; } = ESS;
  public string AppSiteDefHtml { get; init; } = ESS;
  public string AppSiteDefPage { get; init; } = ESS;
  public string AppSiteDefPath { get; init; } = ESS;
  public string AppSiteDefTitle { get; init; } = ESS;

  // Wspld = Web Site Razor Page Layout Defaults should be settable
  public string WspldBodyTitle { get; set; } = ESS;
  public string WspldFooterCodebuildLine { get; set; } = ESS;
  public string WspldFooterContactLine { get; set; } = ESS;
  public string WspldFooterCopyrightLine { get; set; } = ESS;
  public string WspldFooterCrosslinkLine { get; set; } = ESS;
  public string WspldHeaderImageLogo { get; set; } = ESS;
  public string WspldHeaderSloganLine { get; set; } = ESS;
  public string WspldHeaderTagLine { get; set; } = ESS;
  public string WspldHeaderTitle { get; set; } = ESS;
  public string WspldMetatagAuthor { get; set; } = ESS;
  public string WspldMetatagDescription { get; set; } = ESS;
  public string WspldMetatagKeywords { get; set; } = ESS;
  public string WspldPageLayout { get; set; } = ESS;
  public string WspldPageMenu { get; set; } = ESS;

  // TODO: deprecate the view layout, view menu, and related code
  public bool AppUseViewDefaults { get; init; } = false;
  public string AppSiteDefView { get; init; } = ESS;
  public string WspldViewLayout { get; set; } = ESS;
  public string WspldViewMenu { get; set; } = ESS;

  // guid properties in alphabetic order
  // CiaamAppGuid retrieved from database must be settable
  public Guid CiaamAppGuid { get; set; } = EGS;

} // end class

// end file