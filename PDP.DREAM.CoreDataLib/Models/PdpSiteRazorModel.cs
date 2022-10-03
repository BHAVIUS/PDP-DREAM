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

  // methods

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
    if (!string.IsNullOrWhiteSpace(bodyTitle)) { htmlString = $"<h5>{bodyTitle}</h5>"; }
    return htmlString;
  }

  // Razor Page vs Razor View

  public string FormatPageTitle(string pageTitle = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(pageTitle)) { pageTitle = RazorBodyTitle; }
    if (!string.IsNullOrWhiteSpace(pageTitle)) { htmlString = $"<h3>{pageTitle}</h3>"; }
    return htmlString;
  }

  public string FormatViewTitle(string viewTitle = "")
  {
    var htmlString = string.Empty;
    if (string.IsNullOrEmpty(viewTitle)) { viewTitle = RazorViewTitle; }
    if (!string.IsNullOrWhiteSpace(viewTitle)) { htmlString = $"<h3>{viewTitle}</h3>"; }
    return htmlString;
  }

} // end class

// end file