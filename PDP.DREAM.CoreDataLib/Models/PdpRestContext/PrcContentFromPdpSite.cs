// PrcContentForService.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// content for PDP Site
public partial class PdpRestContext
{
  // PDP Site Settings (PDPSS)
  private PdpSiteSettings pdpSitSets = PdpSiteSettings.Values;
  public PdpSiteSettings PDPSS { get { return pdpSitSets; } }

  // site related

  private string? appNameVersion = null;
  public string? SiteAppNameVersion
  {
    set { appNameVersion = value; }
    get {
      if (string.IsNullOrEmpty(appNameVersion))
      { appNameVersion = PDPSS.AppNameVersion; }
      return appNameVersion;
    }
  }

  private string? ownerAcronym = null;
  public string? SiteOwnerAcronym
  {
    set { ownerAcronym = value; }
    get {
      if (string.IsNullOrEmpty(ownerAcronym))
      { ownerAcronym = PDPSS.AppOwnerAcronym; }
      return ownerAcronym;
    }
  }

  private string? ownerEmail = null;
  public string? SiteOwnerEmail
  {
    set { ownerEmail = value; }
    get {
      if (string.IsNullOrEmpty(ownerEmail))
      { ownerEmail = PDPSS.AppOwnerEmail; }
      return ownerEmail;
    }
  }

  private string? ownerName = null;
  public string? SiteOwnerName
  {
    set { ownerName = value; }
    get {
      if (string.IsNullOrEmpty(ownerName))
      { ownerName = PDPSS.AppOwnerName; }
      return ownerName;
    }
  }

  private string? siteTitle = null;
  public string? SiteTitle
  {
    set { siteTitle = value; }
    get {
      if (string.IsNullOrEmpty(siteTitle))
      { siteTitle = PDPSS.AppSiteTitle; }
      return siteTitle;
    }
  }

  private string? rzrMvcArea = null;
  public string? RazorMvcArea
  {
    set { rzrMvcArea = value; }
    get {
      if (string.IsNullOrEmpty(rzrMvcArea))
      { rzrMvcArea = PDPSS.AppSiteMvcDefArea; }
      return rzrMvcArea;
    }
  }

  private string? rzrMvcController = null;
  public string? RazorMvcController
  {
    set { rzrMvcController = value; }
    get {
      if (string.IsNullOrEmpty(rzrMvcController))
      { rzrMvcController = PDPSS.AppSiteMvcDefController; }
      return rzrMvcController;
    }
  }

  private string? rzrMvcAction = null;
  public string? RazorMvcAction
  {
    set { rzrMvcAction = value; }
    get {
      if (string.IsNullOrEmpty(rzrMvcAction))
      { rzrMvcAction = PDPSS.AppSiteMvcDefAction; }
      return rzrMvcAction;
    }
  }

  private string? rzrMvcPage = null;
  public string? RazorMvcPage
  {
    set { rzrMvcPage = value; }
    get {
      if (string.IsNullOrEmpty(rzrMvcPage))
      { rzrMvcPage = PDPSS.AppSiteMvcDefPath; }
      return rzrMvcPage;
    }
  }

  // page related

  private string vwLayout = string.Empty;
  public string RazorMvcLayoutView
  {
    set { vwLayout = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(vwLayout))
        { vwLayout = PDPSS.WspldViewLayout; }
        if (string.IsNullOrEmpty(vwLayout))
        { vwLayout = PDPSS.AppSiteMvcDefLayoutView; }
        if (vwLayout == PdpConst.PdpPageDefaultNone)
        { vwLayout = string.Empty; }
      }
      return vwLayout;
    }
  }

  private string pgLayout = string.Empty;
  public string RazorMvcLayoutPage
  {
    set { pgLayout = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(pgLayout))
        { pgLayout = PDPSS.WspldPageLayout; }
        if (string.IsNullOrEmpty(pgLayout))
        { pgLayout = PDPSS.AppSiteMvcDefLayoutPage; }
        if (pgLayout == PdpConst.PdpPageDefaultNone)
        { pgLayout = string.Empty; }
      }
      return pgLayout;
    }
  }

  private string pgMenu = string.Empty;
  public string PageMenu
  {
    set { pgMenu = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(pgMenu))
        { pgMenu = PDPSS.WspldPageMenu; }
        if (pgMenu == PdpConst.PdpPageDefaultNone)
        { pgMenu = string.Empty; }
      }
      return pgMenu;
    }
  }

  private string pgTitle = string.Empty;
  public string PageTitle
  {
    set { pgTitle = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(pgTitle))
        { pgTitle = PDPSS.WspldPageTitle; }
        if (string.IsNullOrEmpty(pgTitle))
        { pgTitle = PDPSS.WspldHeaderTitle; }
        if (string.IsNullOrEmpty(pgTitle))
        { pgTitle = PDPSS.AppSiteTitle; }
        if (string.IsNullOrEmpty(pgTitle))
        { pgTitle = PDPSS.AppOwnerName; }
        if (pgTitle == PdpConst.PdpPageDefaultNone)
        { pgTitle = string.Empty; }
      }
      return pgTitle;
    }
  }

  private string mttAuthor = string.Empty;
  public string MetatagAuthor
  {
    set { mttAuthor = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(mttAuthor))
        { mttAuthor = PDPSS.WspldMetatagAuthor; }
        if (string.IsNullOrEmpty(mttAuthor))
        { mttAuthor = PDPSS.AppOwnerName; }
        if (string.IsNullOrEmpty(mttAuthor))
        { mttAuthor = PdpConst.PdpSiteMetatagAuthor; }
        if (mttAuthor == PdpConst.PdpPageDefaultNone)
        { mttAuthor = string.Empty; }
      }
      return mttAuthor;
    }
  }

  private string mttKeywords = string.Empty;
  public string MetatagKeywords
  {
    set { mttKeywords = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(mttKeywords))
        { mttKeywords = PDPSS.WspldMetatagKeywords; }
        if (string.IsNullOrEmpty(mttKeywords))
        { mttKeywords = PdpConst.PdpSiteMetatagKeywords; }
        if (mttKeywords == PdpConst.PdpPageDefaultNone)
        { mttKeywords = string.Empty; }
      }
      return mttKeywords;
    }
  }

  private string mttDescription = string.Empty;
  public string MetatagDescription
  {
    set { mttDescription = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(mttDescription))
        { mttDescription = PDPSS.WspldMetatagDescription; }
        if (string.IsNullOrEmpty(mttDescription))
        { mttDescription = $"{SiteOwnerAcronym} {ServerType} {PdpConst.PdpSiteMetatagDescription}"; }
        if (mttDescription == PdpConst.PdpPageDefaultNone)
        { mttDescription = string.Empty; }
      }
      return mttDescription;
    }
  }

  private string hdrImageLogo = string.Empty;
  public string HeaderImageLogo
  {
    set { hdrImageLogo = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(hdrImageLogo))
        { hdrImageLogo = PDPSS.WspldHeaderImageLogo; }
        if (string.IsNullOrEmpty(hdrImageLogo))
        { hdrImageLogo = PdpConst.PdpSiteHeaderImageLogo; }
        if (hdrImageLogo == PdpConst.PdpPageDefaultNone)
        { hdrImageLogo = string.Empty; }
      }
      return hdrImageLogo;
    }
  }

  private string hdrTitle = string.Empty;
  public string HeaderTitle
  {
    set { hdrTitle = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(hdrTitle))
        { hdrTitle = PDPSS.WspldHeaderTitle; }
        if (string.IsNullOrEmpty(hdrTitle))
        { hdrTitle = PDPSS.AppSiteTitle; }
        if (string.IsNullOrEmpty(hdrTitle))
        { hdrTitle = PdpConst.PdpSiteHeaderTitle; }
        if (hdrTitle == PdpConst.PdpPageDefaultNone)
        { hdrTitle = string.Empty; }
      }
      return hdrTitle;
    }
  }

  private string hdrTagLine = string.Empty;
  public string HeaderTagLine
  {
    set { hdrTagLine = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(hdrTagLine))
        { hdrTagLine = PDPSS.WspldHeaderTagLine; }
        if (string.IsNullOrEmpty(hdrTagLine))
        { hdrTagLine = PdpConst.PdpSiteHeaderTagLine; }
        if (hdrTagLine == PdpConst.PdpPageDefaultNone)
        { hdrTagLine = string.Empty; }
      }
      return hdrTagLine;
    }
  }

  private string hdrSloganLine = string.Empty;
  public string HeaderSloganLine
  {
    set { hdrSloganLine = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(hdrSloganLine))
        { hdrSloganLine = PDPSS.WspldHeaderSloganLine; }
        if (string.IsNullOrEmpty(hdrSloganLine))
        { hdrSloganLine = PdpConst.PdpSiteHeaderSloganLine; }
        if (hdrSloganLine == PdpConst.PdpPageDefaultNone)
        { hdrSloganLine = string.Empty; }
      }
      return hdrSloganLine;
    }
  }

  private string ftrCopyrightLine = string.Empty;
  public string FooterCopyrightLine
  {
    set { ftrCopyrightLine = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(ftrCopyrightLine))
        { ftrCopyrightLine = PDPSS.WspldFooterCopyrightLine; }
        if (string.IsNullOrEmpty(ftrCopyrightLine))
        { ftrCopyrightLine = PdpConst.PdpSiteFooterCopyrightLine; }
        if (ftrCopyrightLine == PdpConst.PdpPageDefaultNone)
        { ftrCopyrightLine = string.Empty; }
      }
      return ftrCopyrightLine;
    }
  }

  private string ftrCrosslinkLine = string.Empty;
  public string FooterCrosslinkLine
  {
    set { ftrCrosslinkLine = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(ftrCrosslinkLine))
        { ftrCrosslinkLine = PDPSS.WspldFooterCrosslinkLine; }
        if (string.IsNullOrEmpty(ftrCrosslinkLine))
        { ftrCrosslinkLine = PdpConst.PdpSiteFooterCrosslinkLine; }
        if (ftrCrosslinkLine == PdpConst.PdpPageDefaultNone)
        { ftrCrosslinkLine = string.Empty; }
      }
      return ftrCrosslinkLine;
    }
  }

  private string ftrContactLine = string.Empty;
  public string FooterContactLine
  {
    set { ftrContactLine = value; }
    get {
      if (PDPSS.AppUsePageDefaults)
      {
        if (string.IsNullOrEmpty(ftrContactLine))
        { ftrContactLine = PDPSS.WspldFooterContactLine; }
        if (string.IsNullOrEmpty(ftrContactLine))
        { ftrContactLine = PdpConst.PdpSiteFooterContactLine; }
        if (ftrContactLine == PdpConst.PdpPageDefaultNone)
        { ftrContactLine = string.Empty; }
      }
      return ftrContactLine;
    }
  }

  // properties set only in controllers, but not in appsettings.json
  public string SectionTitle { get; set; } = string.Empty;
  public string ViewName { get; set; } = string.Empty;

  // methods

  public string FormatHeaderTitle()
  { return $"<h1>{HeaderTitle}</h1>"; }
  public string FormatSiteTitle(string title = "")
  {
    if (!string.IsNullOrEmpty(title)) { HeaderTitle = title; }
    return FormatHeaderTitle();
  }

  public string FormatHeaderTagLine()
  { return $"<h5>{HeaderTagLine}</h5>"; }
  public string FormatHeaderTagLine(string tagLine = "")
  {
    if (!string.IsNullOrEmpty(tagLine)) { HeaderTagLine = tagLine; }
    return FormatHeaderTagLine();
  }
  public string FormatHeaderSloganLine()
  { return $"<h5>{HeaderSloganLine}</h5>"; }
  public string FormatHeaderSloganLine(string sloganLine = "")
  {
    if (!string.IsNullOrEmpty(sloganLine)) { HeaderSloganLine = sloganLine; }
    return FormatHeaderSloganLine();
  }

  public string FormatPageTitle()
  { return $"<h3>{PageTitle}</h3>"; }
  public string FormatPageTitle(string title = "")
  {
    if (!string.IsNullOrEmpty(title)) { PageTitle = title; }
    return FormatPageTitle();
  }

  public string FormatSectionTitle()
  { return $"<h5>{SectionTitle}</h5>"; }
  public string FormatSectionTitle(string title = "")
  {
    if (!string.IsNullOrEmpty(title)) { SectionTitle = title; }
    return FormatSectionTitle();
  }

} // class
