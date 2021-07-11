// PrcContentForService.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // options for NPDS service
  public partial class PdpRestContext
  {
    // service related

    public NpdsServiceDefaults SrvcDefs { get { return npdsSrvcDefs; } }
    private NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.GetValues;

    public string? ServiceError { set; get; }

    public string? ServiceNote { set; get; }

    // site related

    public PdpSiteSettings SiteSets { get { return pdpSitSets; } }
    private PdpSiteSettings pdpSitSets = PdpSiteSettings.GetValues;

    public string? SiteAppNameVersion
    {
      set { nameVersion = value; }
      get
      {
        if (string.IsNullOrEmpty(nameVersion))
        { nameVersion = SiteSets.AppNameVersion; }
        return nameVersion;
      }
    }
    private string? nameVersion = null;

    public string? SiteOwnerAcronym
    {
      set { ownerAcronym = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerAcronym))
        { ownerAcronym = SiteSets.AppOwnerAcronym; }
        return ownerAcronym;
      }
    }
    private string? ownerAcronym = null;

    public string? SiteOwnerEmail
    {
      set { ownerEmail = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerEmail))
        { ownerEmail = SiteSets.AppOwnerEmail; }
        return ownerEmail;
      }
    }
    private string? ownerEmail = null;

    public string? SiteOwnerName
    {
      set { ownerName = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerName))
        { ownerName = SiteSets.AppOwnerName; }
        return ownerName;
      }
    }
    private string? ownerName = null;

    public string? SiteTitle
    {
      set { siteTitle = value; }
      get
      {
        if (string.IsNullOrEmpty(siteTitle))
        { siteTitle = SiteSets.AppSiteTitle; }
        return siteTitle;
      }
    }
    private string? siteTitle = null;

    public string? SiteDescription
    {
      set { siteDesc = value; }
      get
      {
        if (string.IsNullOrEmpty(siteDesc))
        { siteDesc = SiteOwnerAcronym + " " + ServerType; }
        return siteDesc;
      }
    }
    private string? siteDesc = string.Empty;

    // page related

    public string? PageLayout
    {
      set { pageLayout = value; }
      get
      {
        if (string.IsNullOrEmpty(pageLayout))
        { pageLayout = SiteSets.AppPageLayout; }
        return pageLayout;
      }
    }
    private string? pageLayout = null;

    public string? PageMenu
    {
      set { pageMenu = value; }
      get
      {
        if (string.IsNullOrEmpty(pageMenu))
        { pageMenu = SiteSets.AppPageMenu; }
        return pageMenu;
      }
    }
    private string? pageMenu = null;

    public string? PageTitle
    {
      set { pageTitle = value; }
      get
      {
        if (string.IsNullOrEmpty(pageTitle))
        { pageTitle = SiteSets.AppPageTitle; }
        return pageTitle;
      }
    }
    private string? pageTitle = null;

    public string SectionTitle { get; set; } = string.Empty;
    public string ViewName { get; set; } = string.Empty;

    // methods

    public string FormatSiteTitle()
    { return $"<h1>{SiteTitle}</h1>"; }
    public string FormatSiteTitle(string title = "")
    {
      if (!string.IsNullOrEmpty(title)) { SiteTitle = title; }
      return FormatSiteTitle();
    }

    public string FormatPageTitle()
    { return $"<h2>{PageTitle}</h2>"; }
    public string FormatPageTitle(string title = "")
    {
      if (!string.IsNullOrEmpty(title)) { PageTitle = title; }
      return FormatPageTitle();
    }

    public string FormatSectionTitle()
    { return $"<h4>{SectionTitle}</h4>"; }
    public string FormatSectionTitle(string title = "")
    {
      if (!string.IsNullOrEmpty(title)) { SectionTitle = title; }
      return FormatSectionTitle();
    }

  }

}
