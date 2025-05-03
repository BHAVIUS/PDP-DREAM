// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpSiteRazorModel
{
  // constructors

  // TODO: compare property name used in LaTeX templates "edocptag, edocrhan, edocalab"
  // TODO: extend for object with properties matching those used in LaTeX templates
  // TODO: make QURC properties for "edocptag, edocrhan, edocalab"
  // TODO: then handle appropriately for Razor Pages and Razor Views

  public PdpSiteRazorModel()
  {
    RazorPageName = "";
    RazorBodyTitle = "";
    // TODO: create analogue for RazorViewStrings
  }
  public PdpSiteRazorModel(string edocName)
  {
    RazorPageName = edocName;
    RazorBodyTitle = "";
    // TODO: create analogue for RazorViewStrings
  }
  public PdpSiteRazorModel(string edocName, string edocTitle)
  {
    RazorPageName = edocName;
    RazorBodyTitle = edocTitle;
    // TODO: create analogue for RazorViewStrings
  }

  // properties

  public PdpSiteInfoModel PdpSiteInfo { get; set; } = new PdpSiteInfoModel();


  public string RazorBodyTitle
  {
    set { rzrBodyTitle = value; DefaultRazorBodyTitle(); }
    get { DefaultRazorBodyTitle(); return rzrBodyTitle; }
  }

  private string rzrBodyTitle = string.Empty;
  protected void DefaultRazorBodyTitle()
  {
    if (PDPSS.AppUsePageDefaults || PDPSS.AppUseViewDefaults)
    {
      if ((rzrBodyTitle == "path") && PDPSS.AppUsePathDefaults)
      {
        if (PDPSS.AppUsePageDefaults) { rzrBodyTitle = RazorPagePath; }
        else if (PDPSS.AppUseViewDefaults) { rzrBodyTitle = RazorViewPath; }
        else { rzrBodyTitle = string.Empty; }
      }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.WspldBodyTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.AppSiteTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.AppOwnerLongName; }
      if (rzrBodyTitle == PdpSiteNoneKey)
      { rzrBodyTitle = string.Empty; }
    }
  }

  public string RazorHeaderPart { get; set; } = string.Empty;
  public string RazorHeaderMenu
  {
    set { rzrPageMenu = value; DefaultRazorPageMenu(); }
    get { DefaultRazorPageMenu(); return rzrPageMenu; }
  }

  public string RazorBodyPart { get; set; } = string.Empty;
  public string RazorBodyMenu { get; set; } = string.Empty;
  public string RazorFooterPart { set; get; } = string.Empty;
  public string RazorFooterMenu { set; get; } = string.Empty;

  // methods
  public virtual void InitRazorBodyStrings(string bodyName, string bodyTitle, string bodyMenu)
  {
    RazorBodyPart = bodyName;
    RazorBodyTitle = bodyTitle;
    RazorBodyMenu = bodyMenu;
  }

  public string NpdsRazorBodyTitle(string? serviceTitle)
  {
    if (string.IsNullOrEmpty(serviceTitle))
    { serviceTitle = NPDSSD.NpdsDefaultServiceType.ToString(); }
    rzrBodyTitle = $"{RazorPageName} from {serviceTitle}";
    return rzrBodyTitle;
  }

  public string FormatHeaderTitle(string headerTitle = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(headerTitle)) { headerTitle = PdpSiteInfo.HeaderTitle; }
    if (!string.IsNullOrWhiteSpace(headerTitle)) { htmlString = $"<h1>{headerTitle}</h1>"; }
    return htmlString;
  }

  public string FormatHeaderTagLine(string tagLine = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(tagLine)) { tagLine = PdpSiteInfo.HeaderTagLine; }
    if (!string.IsNullOrWhiteSpace(tagLine)) { htmlString = $"<h5>{tagLine}</h5>"; }
    return htmlString;
  }

  public string FormatHeaderSloganLine(string sloganLine = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(sloganLine)) { sloganLine = PdpSiteInfo.HeaderSloganLine; }
    if (!string.IsNullOrWhiteSpace(sloganLine)) { htmlString = $"<h5>{sloganLine}</h5>"; }
    return htmlString;
  }

  public string FormatBodyTitle(string bodyTitle = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(bodyTitle)) { bodyTitle = RazorBodyTitle; }
    if (!string.IsNullOrWhiteSpace(bodyTitle)) { htmlString = $"<h3 class='pdpBodyTitle'>{bodyTitle}</h3>"; }
    return htmlString;
  }

} // end class

// end file