// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

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

  public virtual void InitRazorPageMenus(string footerMenu, string bodyMenu, string headerMenu, bool useBodyDefault)
  {
    if (!string.IsNullOrEmpty(footerMenu))
    { RazorFooterMenu = footerMenu; }
    if (!string.IsNullOrEmpty(bodyMenu))
    { RazorBodyMenu = bodyMenu; }
    if (!string.IsNullOrEmpty(headerMenu))
    { RazorHeaderMenu = headerMenu; }
    // useBodyDefault only if also RazorBodyMenu is empty
    if (useBodyDefault && string.IsNullOrEmpty(RazorBodyMenu))
    {
      // prioritize footerMenu over headerMenu as default for bodyMenu 
      if (!string.IsNullOrEmpty(footerMenu)) { RazorBodyMenu = footerMenu; }
      else if (!string.IsNullOrEmpty(headerMenu)) { RazorBodyMenu = headerMenu; }
    }
  }
  public virtual void InitRazorPageMenus(string footerMenu, string bodyMenu, string headerMenu)
  {
    InitRazorPageMenus(footerMenu, bodyMenu, headerMenu, false);
  }
  public virtual void InitRazorPageMenus(string footerMenu, string bodyMenu)
  {
    InitRazorPageMenus(footerMenu, bodyMenu, "", false);
  }
  public virtual void InitRazorPageMenus(string footerMenu, bool useBodyDefault = true)
  {
    InitRazorPageMenus(footerMenu, "", "", useBodyDefault);
  }

  public virtual void DebugRazorPageStrings(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugRazorPageStrings)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"PageName = {RazorPageName}; PagePath = {RazorPagePath}");
    Debug.WriteLine($"HeaderPart = {RazorHeaderPart}; HeaderMenu = {RazorHeaderMenu}");
    Debug.WriteLine($"FooterPart = {RazorFooterPart}; FooterMenu = {RazorFooterMenu}");
    Debug.WriteLine($"BodyPart = {RazorBodyPart}; BodyMenu = {RazorBodyMenu}; BodyTitle = {RazorBodyTitle}");
  }

} // end class

// end file