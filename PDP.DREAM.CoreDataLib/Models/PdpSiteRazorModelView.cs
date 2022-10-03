// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

using PDP.DREAM.CoreDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Utilities.PdpStringFrasFormFile;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpSiteRazorModel
{
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
  public string RazorViewName
  {
    set { rzrViewName = value; DefaultRazorViewName(); }
    get { DefaultRazorViewName(); return rzrViewName; }
  }

  public string RazorViewPath { get; set; } = string.Empty;

  // properties with defaults from Wspld* series

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
  public string RazorViewLayout
  {
    set { rzrViewLayout = value; DefaultRazorViewLayout(); }
    get { DefaultRazorViewLayout(); return rzrViewLayout; }
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
  public string RazorViewMenu
  {
    set { rzrViewMenu = value; DefaultRazorViewMenu(); }
    get { DefaultRazorViewMenu(); return rzrViewMenu; }
  }


  private string rzrViewTitle = string.Empty;
  protected void DefaultRazorViewTitle()
  {
    if (PDPSS.AppUseViewDefaults)
    {
      if ((rzrViewTitle == "path") && PDPSS.AppUsePathDefaults)
      { rzrViewTitle = RazorViewPath; }
      if (string.IsNullOrEmpty(rzrViewTitle))
      { rzrViewTitle = PDPSS.WspldViewTitle; }
      if (string.IsNullOrEmpty(rzrViewTitle))
      { rzrViewTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(rzrViewTitle))
      { rzrViewTitle = PDPSS.AppSiteTitle; }
      if (string.IsNullOrEmpty(rzrViewTitle))
      { rzrViewTitle = PDPSS.AppOwnerLongName; }
      if (rzrViewTitle == PdpSiteNoneKey)
      { rzrViewTitle = string.Empty; }
    }
  }
  public string RazorViewTitle
  {
    set { rzrViewTitle = value; DefaultRazorViewTitle(); }
    get { DefaultRazorViewTitle(); return rzrViewTitle; }
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
    Debug.WriteLine($"viewName = {RazorViewName}; viewMenu = {RazorViewMenu}; viewTitle = {RazorViewTitle}");
    Debug.WriteLine($"BodyPart = {RazorBodyPart}; BodyMenu = {RazorBodyMenu}; BodyTitle = {RazorBodyTitle}");
  }
} // end class

// end file