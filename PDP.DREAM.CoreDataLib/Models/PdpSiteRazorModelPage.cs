// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Diagnostics;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Utilities.PdpStringFrasFormFile;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpSiteRazorModel
{
  private string rzrPageLayout = string.Empty;
  protected void DefaultRazorPageLayout()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(rzrPageLayout))
      { rzrPageLayout = PDPSS.WspldPageLayout; }
      if (rzrPageLayout == PdpSiteNoneKey)
      { rzrPageLayout = string.Empty; }
    }
  }

  private string rzrPageMenu = string.Empty;
  protected void DefaultRazorPageMenu()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(rzrPageMenu))
      { rzrPageMenu = PDPSS.WspldPageMenu; }
      if (rzrPageMenu == PdpSiteNoneKey)
      { rzrPageMenu = string.Empty; }
    }
  }

  private string rzrPageName = string.Empty;
  protected void DefaultRazorPageName()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(rzrPageName))
      { rzrPageName = PDPSS.AppSiteDefPage; }
      if (PDPSS.AppUsePathDefaults)
      { rzrPageName = rzrPageName.AssureInitialSlash(); }
    }
  }

  private string rzrPagePath = string.Empty;
  protected void DefaultRazorPagePath()
  {
    if (PDPSS.AppUsePathDefaults)
    {
      if (string.IsNullOrEmpty(rzrPagePath))
      {
        // assuming convention that
        // page names have been expressed as valid url page paths
        rzrPagePath = RazorPageName;
      }
      if (string.IsNullOrEmpty(rzrPagePath))
      {
        rzrPagePath = PDPSS.AppSiteDefPath;
      }
      rzrPagePath = rzrPagePath.AssureInitialSlash();
    }
  }

  private string rzrPageTitle = string.Empty;
  protected void DefaultRazorPageTitle()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if ((rzrPageTitle == "path") && PDPSS.AppUsePathDefaults)
      { rzrPageTitle = RazorPagePath; }
      if (string.IsNullOrEmpty(rzrPageTitle))
      { rzrPageTitle = PDPSS.WspldPageTitle; }
      if (string.IsNullOrEmpty(rzrPageTitle))
      { rzrPageTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(rzrPageTitle))
      { rzrPageTitle = PDPSS.AppSiteTitle; }
      if (string.IsNullOrEmpty(rzrPageTitle))
      { rzrPageTitle = PDPSS.AppOwnerLongName; }
      if (rzrPageTitle == PdpSiteNoneKey)
      { rzrPageTitle = string.Empty; }
    }
  }

  public string RazorPageLayout
  {
    set { rzrPageLayout = value; DefaultRazorPageLayout(); }
    get { DefaultRazorPageLayout(); return rzrPageLayout; }
  }

  public string RazorPageName
  {
    set { rzrPageName = value; DefaultRazorPageName(); }
    get { DefaultRazorPageName(); return rzrPageName; }
  }

  public string RazorPagePath
  {
    set { rzrPagePath = value; DefaultRazorPagePath(); }
    get { DefaultRazorPagePath(); return rzrPagePath; }
  }

  public string RazorBodyTitle
  {
    set { rzrPageTitle = value; DefaultRazorPageTitle(); }
    get { DefaultRazorPageTitle(); return rzrPageTitle; }
  }

  public string NpdsRazorBodyTitle(string? serviceTitle)
  {
    if (string.IsNullOrEmpty(serviceTitle))
    { serviceTitle = NPDSSD.NpdsDefaultServiceType.ToString(); }
    rzrPageTitle = $"{RazorPageName} from {serviceTitle}";
    return rzrPageTitle;
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

  public virtual void InitRazorBodyStrings(string bodyName, string bodyTitle, string bodyMenu)
  {
    RazorBodyPart = bodyName;
    RazorBodyTitle = bodyTitle;
    RazorBodyMenu = bodyMenu;
  }

  public virtual void InitRazorPageMenus(string footerMenu, string headerMenu, string bodyMenu, bool useBodyDefault)
  {
    RazorFooterMenu = footerMenu;
    RazorHeaderMenu = headerMenu;
    RazorBodyMenu = bodyMenu;
    if (useBodyDefault && string.IsNullOrEmpty(RazorBodyMenu))
    {
      if (!string.IsNullOrEmpty(headerMenu)) { RazorBodyMenu = headerMenu; }
      else if (!string.IsNullOrEmpty(footerMenu)) { RazorBodyMenu = footerMenu; }
    }
  }
  public virtual void InitRazorPageMenus(string footerMenu, string headerMenu, bool useBodyDefault = false)
  {
    InitRazorPageMenus(footerMenu, headerMenu, "", useBodyDefault);
  }
  public virtual void InitRazorPageMenus(string footerMenu, bool useBodyDefault = true)
  {
    InitRazorPageMenus(footerMenu, "", "", useBodyDefault);
  }

  public virtual void DebugRazorPageStrings()
  {
    Debug.WriteLine($"PageName = {RazorPageName}; PagePath = {RazorPagePath}");
    Debug.WriteLine($"HeaderPart = {RazorHeaderPart}; HeaderMenu = {RazorHeaderMenu}");
    Debug.WriteLine($"FooterPart = {RazorFooterPart}; FooterMenu = {RazorFooterMenu}");
    Debug.WriteLine($"BodyPart = {RazorBodyPart}; BodyMenu = {RazorBodyMenu}; BodyTitle = {RazorBodyTitle}");
  }

} // end class

// end file