namespace PDP.DREAM.NpdsCoreLib.Models
{
  // options for NPDS service
  public partial class PdpRestContext
  {
    // service related
    public string? ServiceError { set; get; }

    public string? ServiceNote { set; get; }

    // site related

    public string SiteAppNameVersion
    {
      set { nameVersion = value; }
      get
      {
        if (string.IsNullOrEmpty(nameVersion))
        { nameVersion = PdpSiteSettings.GetValues.AppNameVersion; }
        return nameVersion;
      }
    }
    private string nameVersion = PdpSiteSettings.GetValues.AppNameVersion;

    public string SiteOwnerAcronym
    {
      set { ownerAcronym = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerAcronym))
        { ownerAcronym = PdpSiteSettings.GetValues.AppOwnerAcronym; }
        return ownerAcronym;
      }
    }
    private string ownerAcronym = PdpSiteSettings.GetValues.AppOwnerAcronym;

    public string SiteOwnerEmail
    {
      set { ownerEmail = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerEmail))
        { ownerEmail = PdpSiteSettings.GetValues.AppOwnerEmail; }
        return ownerEmail;
      }
    }
    private string ownerEmail = PdpSiteSettings.GetValues.AppOwnerEmail;

    public string SiteOwnerName
    {
      set { ownerName = value; }
      get
      {
        if (string.IsNullOrEmpty(ownerName))
        { ownerName = PdpSiteSettings.GetValues.AppOwnerName; }
        return ownerName;
      }
    }
    private string ownerName = PdpSiteSettings.GetValues.AppOwnerName;

    public string SiteTitle
    {
      set { siteTitle = value; }
      get
      {
        if (string.IsNullOrEmpty(siteTitle))
        { siteTitle = PdpSiteSettings.GetValues.AppSiteTitle; }
        return siteTitle;
      }
    }
    private string siteTitle = PdpSiteSettings.GetValues.AppSiteTitle;

    public string SiteDescription
    {
      set { siteDesc = value; }
      get
      {
        if (string.IsNullOrEmpty(siteDesc))
        { siteDesc = SiteOwnerAcronym + " " + ServerType; }
        return siteDesc;
      }
    }
    private string siteDesc = string.Empty;

    // page related

    public string PageLayout
    {
      set { pageLayout = value; }
      get
      {
        if (string.IsNullOrEmpty(pageLayout))
        { pageLayout = PdpSiteSettings.GetValues.AppPageLayout; }
        return pageLayout;
      }
    }
    private string pageLayout = PdpSiteSettings.GetValues.AppPageLayout;

    public string PageMenu
    {
      set { pageMenu = value; }
      get
      {
        if (string.IsNullOrEmpty(pageMenu))
        { pageMenu = PdpSiteSettings.GetValues.AppPageMenu; }
        return pageMenu;
      }
    }
    private string pageMenu = PdpSiteSettings.GetValues.AppPageMenu;

    public string PageTitle
    {
      set { pageTitle = value; }
      get
      {
        if (string.IsNullOrEmpty(pageTitle))
        { pageTitle = PdpSiteSettings.GetValues.AppPageTitle; }
        return pageTitle;
      }
    }
    private string pageTitle = PdpSiteSettings.GetValues.AppPageTitle;

    public string? ViewName { get; set; }

    // methods

    public string FormatPageTitle()
    { return "<h3>" + PageTitle + "</h3>"; }
    public string FormatPageTitle(string pt = "")
    {
      if (!string.IsNullOrEmpty(pt)) { PageTitle = pt; }
      return FormatPageTitle();
    }

    public string FormatSiteTitle()
    { return "<h1>" + SiteTitle + "</h1>"; }
    public string FormatSiteTitle(string st = "")
    {
      if (!string.IsNullOrEmpty(st)) { SiteTitle = st; }
      return FormatSiteTitle();
    }

  }

}
