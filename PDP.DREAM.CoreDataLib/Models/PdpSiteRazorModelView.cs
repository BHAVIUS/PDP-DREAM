// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpSiteRazorModel
{
  private string rzrViewLayout = string.Empty;
  protected void DefaultRazorViewLayout()
  {
    if (PDPSS.AppUseViewDefaults)
    {
      if (string.IsNullOrEmpty(rzrViewLayout))
      { rzrViewLayout = PDPSS.WspldViewLayout; }
      if (rzrViewLayout == PdpSiteNoneKey)
      { rzrViewLayout = string.Empty; }
    }
  }

  private string rzrViewMenu = string.Empty;
  protected void DefaultRazorViewMenu()
  {
    if (PDPSS.AppUseViewDefaults)
    {
      if (string.IsNullOrEmpty(rzrViewMenu))
      { rzrViewMenu = PDPSS.WspldViewMenu; }
      if (rzrViewMenu == PdpSiteNoneKey)
      { rzrViewMenu = string.Empty; }
    }
  }

  private string rzrViewName = string.Empty;
  protected void DefaultRazorViewName()
  {
    if (PDPSS.AppUseViewDefaults)
    {
      if (string.IsNullOrEmpty(rzrViewName))
      { rzrViewName = PDPSS.AppSiteDefView; }
      if (PDPSS.AppUsePathDefaults)
      { rzrViewName = rzrViewName.AssureInitialSlash(); }
    }
  }

  private string rzrViewPath = string.Empty;
  protected void DefaultRazorViewPath()
  {
    if (PDPSS.AppUsePathDefaults)
    {
      if (string.IsNullOrEmpty(rzrViewPath))
      {
        // assuming convention that
        // view names have been expressed as valid url view paths
        rzrViewPath = RazorViewName;
      }
      if (string.IsNullOrEmpty(rzrPagePath))
      {
        rzrViewPath = PDPSS.AppSiteDefPath;
      }
      rzrViewPath = rzrViewPath.AssureInitialSlash();
    }
  }

  public string RazorViewLayout
  {
    set { rzrViewLayout = value; DefaultRazorViewLayout(); }
    get { DefaultRazorViewLayout(); return rzrViewLayout; }
  }

  public string RazorViewName
  {
    set { rzrViewName = value; DefaultRazorViewName(); }
    get { DefaultRazorViewName(); return rzrViewName; }
  }

  public string RazorViewPath
  {
    set { rzrViewPath = value; DefaultRazorViewPath(); }
    get { DefaultRazorViewPath(); return rzrViewPath; }
  }

  public virtual void InitRazorViewMenus(string footerMenu, string headerMenu, string bodyMenu, bool useBodyDefault)
  {
    RazorFooterMenu = footerMenu;
    RazorHeaderMenu = headerMenu;
    RazorBodyMenu = bodyMenu;
  }

  private string rzrController = string.Empty;
  public string RazorControllerName
  {
    set { rzrController = value; }
    get {
      if (string.IsNullOrEmpty(rzrController))
      { rzrController = PDPSS.AppSiteDefController; }
      return rzrController;
    }
  }

  private string rzrAction = string.Empty;
  public string RazorActionName
  {
    set { rzrAction = value; }
    get {
      if (string.IsNullOrEmpty(rzrAction))
      { rzrAction = PDPSS.AppSiteDefAction; }
      return rzrAction;
    }
  }

  public virtual void DebugRazorViewStrings()
  {
    Debug.WriteLine($"viewName = {RazorViewName}; viewPath = {RazorViewPath}");
    Debug.WriteLine($"HeaderPart = {RazorHeaderPart}; HeaderMenu = {RazorHeaderMenu}");
    Debug.WriteLine($"FooterPart = {RazorFooterPart}; FooterMenu = {RazorFooterMenu}");
    Debug.WriteLine($"BodyPart = {RazorBodyPart}; BodyMenu = {RazorBodyMenu}; BodyTitle = {RazorBodyTitle}");
  }

} // end class

// end file