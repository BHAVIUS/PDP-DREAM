// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpSiteRazorModel
{
  // constructors

  // TODO: add a RazorRedirect property to PSRM
  // TODO: for use with BhaDocs, BhaviDocs and BrainiacsDocs
  //   must rebuild unified collections of documents for versatile flexible extensible use
  // TODO: compare property name used in LaTeX templates "edocptag, edocrhan, edocalab"
  // TODO: extend for object with properties matching those used in LaTeX templates
  // TODO: make WRACE properties for "edocptag, edocrhan, edocalab"
  // TODO: then handle appropriately for Razor Pages and Razor Views

  public PdpSiteRazorModel()
  {
    RazorPageName = "";
    RazorBodyTitle = "";
  }
  public PdpSiteRazorModel(string pageName)
  {
    RazorPageName = pageName;
    RazorBodyTitle = "";
  }
  public PdpSiteRazorModel(string pageName, string bodyTitle)
  {
    RazorPageName = pageName;
    RazorBodyTitle = bodyTitle;
  }

  // properties

  public PdpSiteInfoModel PdpSiteInfo { get; set; } = new PdpSiteInfoModel();


  public string RazorBodyTitle
  {
    set { rzrBodyTitle = value; DefaultRazorBodyTitle(); }
    get { DefaultRazorBodyTitle(); return rzrBodyTitle; }
  }

  private string rzrBodyTitle = ESS;
  protected void DefaultRazorBodyTitle()
  {
    if (PDPSS.AppUsePageDefaults || PDPSS.AppUseViewDefaults)
    {
      if ((rzrBodyTitle == "path") && PDPSS.AppUsePathDefaults)
      {
        if (PDPSS.AppUsePageDefaults) { rzrBodyTitle = RazorPagePath; }
        else if (PDPSS.AppUseViewDefaults) { rzrBodyTitle = RazorViewPath; }
        else { rzrBodyTitle = ESS; }
      }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.WspldBodyTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.AppSiteDefTitle; }
      if (string.IsNullOrEmpty(rzrBodyTitle))
      { rzrBodyTitle = PDPSS.AppOwnerNameLong; }
      if (rzrBodyTitle == PdpSiteNoneKey)
      { rzrBodyTitle = ESS; }
    }
  }

  // for use in header with <partial name="@RazorHeaderPart" /> 
  public string RazorHeaderPart { get; set; } = ESS;
  public string RazorHeaderMenu
  {
    set { rzrPageMenu = value; DefaultRazorPageMenu(); }
    get { DefaultRazorPageMenu(); return rzrPageMenu; }
  }

  // for use in body with <partial name="@RazorBodyPart" /> 
  public string RazorBodyPart { get; set; } = ESS;
  public string RazorBodyMenu { get; set; } = ESS;
  // generic container for arbitrary content used in body
  public string RazorBodyContent { get; set; } = ESS;

  // for use in footer with <partial name="@RazorFooterPart" /> 
  public string RazorFooterPart { set; get; } = ESS;
  public string RazorFooterMenu { set; get; } = ESS;

  // methods

  public string NpdsRazorBodyTitle(string? serviceTitle)
  {
    if (string.IsNullOrEmpty(serviceTitle))
    { serviceTitle = NPDSCD.ServiceTypeDefault.ToString(); }
    rzrBodyTitle = $"{RazorPageName} from {serviceTitle}";
    return rzrBodyTitle;
  }

  public string FormatHeaderTitle(string headerTitle = "")
  {
    var htmlString = ESS;
    if (string.IsNullOrEmpty(headerTitle)) { headerTitle = PdpSiteInfo.HeaderTitle; }
    if (!string.IsNullOrWhiteSpace(headerTitle)) { htmlString = $"<h1>{headerTitle}</h1>"; }
    return htmlString;
  }

  public string FormatHeaderTagLine(string tagLine = "")
  {
    var htmlString = ESS;
    if (string.IsNullOrEmpty(tagLine)) { tagLine = PdpSiteInfo.HeaderTagLine; }
    if (!string.IsNullOrWhiteSpace(tagLine)) { htmlString = $"<h5>{tagLine}</h5>"; }
    return htmlString;
  }

  public string FormatHeaderSloganLine(string sloganLine = "")
  {
    var htmlString = ESS;
    if (string.IsNullOrEmpty(sloganLine)) { sloganLine = PdpSiteInfo.HeaderSloganLine; }
    if (!string.IsNullOrWhiteSpace(sloganLine)) { htmlString = $"<h5>{sloganLine}</h5>"; }
    return htmlString;
  }

  public string FormatBodyTitle(string bodyTitle = "")
  {
    var htmlString = ESS;
    if (string.IsNullOrEmpty(bodyTitle)) { bodyTitle = RazorBodyTitle; }
    if (!string.IsNullOrWhiteSpace(bodyTitle)) { htmlString = $"<h3 class='pdpBodyTitle'>{bodyTitle}</h3>"; }
    return htmlString;
  }

  public string FormatSectionTitle(string sectionTitle = "")
  {
    var htmlString = ESS;
    if (string.IsNullOrEmpty(sectionTitle)) { sectionTitle = "SectionTitle"; }
    // TODO: update css with pdpSectionTitle ??? maintain copy of CSS in CoreWebLib
    if (!string.IsNullOrWhiteSpace(sectionTitle)) { htmlString = $"<h5 class='pdpSectionTitle'>{sectionTitle}</h5>"; }
    return htmlString;
  }

} // end class

// end file