// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class PdpSiteInfoModel
{

  private string appNameVersion = string.Empty;
  public string SiteAppNameVersion
  {
    set { appNameVersion = value; }
    get {
      if (string.IsNullOrEmpty(appNameVersion))
      { appNameVersion = PDPSS.AppNameVersion; }
      return appNameVersion;
    }
  }

  private string ownerAcronym = string.Empty;
  public string SiteOwnerAcronym
  {
    set { ownerAcronym = value; }
    get {
      if (string.IsNullOrEmpty(ownerAcronym))
      { ownerAcronym = PDPSS.AppOwnerCodeName; }
      return ownerAcronym;
    }
  }

  private string ownerEmail = string.Empty;
  public string SiteOwnerEmail
  {
    set { ownerEmail = value; }
    get {
      if (string.IsNullOrEmpty(ownerEmail))
      { ownerEmail = PDPSS.AppOwnerEmail; }
      return ownerEmail;
    }
  }

  private string ownerName = string.Empty;
  public string SiteOwnerName
  {
    set { ownerName = value; }
    get {
      if (string.IsNullOrEmpty(ownerName))
      { ownerName = PDPSS.AppOwnerLongName; }
      return ownerName;
    }
  }

  private string ownerServer = string.Empty;
  public string SiteOwnerDefServerType
  {
    set { ownerServer = value; }
    get {
      if (string.IsNullOrEmpty(ownerServer))
      { ownerServer = NPDSSD.NpdsDefaultServerType.ToString(); }
      return ownerServer;
    }
  }

  private string siteTitle = string.Empty;
  public string SiteTitle
  {
    set { siteTitle = value; }
    get {
      if (string.IsNullOrEmpty(siteTitle))
      { siteTitle = PDPSS.AppSiteTitle; }
      return siteTitle;
    }
  }

  private string mttAuthor = string.Empty;
  protected void DefaultMetatagAuthor()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PDPSS.WspldMetatagAuthor; }
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PDPSS.AppOwnerLongName; }
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PdpSiteMetatagAuthor; }
      if (mttAuthor == PdpSiteNoneKey)
      { mttAuthor = string.Empty; }
    }
  }
  public string MetatagAuthor
  {
    set { mttAuthor = value; DefaultMetatagAuthor(); }
    get { DefaultMetatagAuthor(); return mttAuthor; }
  }

  private string mttKeywords = string.Empty;
  protected void DefaultMetatagKeywords()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttKeywords))
      { mttKeywords = PDPSS.WspldMetatagKeywords; }
      if (string.IsNullOrEmpty(mttKeywords))
      { mttKeywords = PdpSiteMetatagKeywords; }
      if (mttKeywords == PdpSiteNoneKey)
      { mttKeywords = string.Empty; }
    }
  }
  public string MetatagKeywords
  {
    set { mttKeywords = value; DefaultMetatagKeywords(); }
    get { DefaultMetatagKeywords(); return mttKeywords; }
  }

  private string mttDescription = string.Empty;
  protected void DefaultMetatagDescription()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttDescription))
      { mttDescription = PDPSS.WspldMetatagDescription; }
      if (string.IsNullOrEmpty(mttDescription))
      { mttDescription = $"{SiteOwnerAcronym} {SiteOwnerDefServerType} {PdpSiteMetatagDescription}"; }
      if (mttDescription == PdpSiteNoneKey)
      { mttDescription = string.Empty; }
    }
  }
  public string MetatagDescription
  {
    set { mttDescription = value; DefaultMetatagDescription(); }
    get { DefaultMetatagDescription(); return mttDescription; }
  }

  private string hdrImageLogo = string.Empty;
  protected void DefaultHeaderImageLogo()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrImageLogo))
      { hdrImageLogo = PDPSS.WspldHeaderImageLogo; }
      if (string.IsNullOrEmpty(hdrImageLogo))
      { hdrImageLogo = PdpSiteHeaderImageLogo; }
      if (hdrImageLogo == PdpSiteNoneKey)
      { hdrImageLogo = string.Empty; }
    }
  }
  public string HeaderImageLogo
  {
    set { hdrImageLogo = value; DefaultHeaderImageLogo(); }
    get { DefaultHeaderImageLogo(); return hdrImageLogo; }
  }

  private string hdrTitle = string.Empty;
  protected void DefaultHeaderTitle()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PDPSS.AppSiteTitle; }
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PdpSiteHeaderTitle; }
      if (hdrTitle == PdpSiteNoneKey)
      { hdrTitle = string.Empty; }
    }
  }
  public string HeaderTitle
  {
    set { hdrTitle = value; DefaultHeaderTitle(); }
    get { DefaultHeaderTitle(); return hdrTitle; }
  }

  private string hdrTagLine = string.Empty;
  protected void DefaultHeaderTagLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrTagLine))
      { hdrTagLine = PDPSS.WspldHeaderTagLine; }
      if (string.IsNullOrEmpty(hdrTagLine))
      { hdrTagLine = PdpSiteHeaderTagLine; }
      if (hdrTagLine == PdpSiteNoneKey)
      { hdrTagLine = string.Empty; }
    }
  }
  public string HeaderTagLine
  {
    set { hdrTagLine = value; DefaultHeaderTagLine(); }
    get { DefaultHeaderTagLine(); return hdrTagLine; }
  }

  private string hdrSloganLine = string.Empty;
  protected void DefaultHeaderSloganLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrSloganLine))
      { hdrSloganLine = PDPSS.WspldHeaderSloganLine; }
      if (string.IsNullOrEmpty(hdrSloganLine))
      { hdrSloganLine = PdpSiteHeaderSloganLine; }
      if (hdrSloganLine == PdpSiteNoneKey)
      { hdrSloganLine = string.Empty; }
    }
  }
  public string HeaderSloganLine
  {
    set { hdrSloganLine = value; DefaultHeaderSloganLine(); }
    get { DefaultHeaderSloganLine(); return hdrSloganLine; }
  }

  private string ftrCopyrightLine = string.Empty;
  protected void DefaultFooterCopyrightLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCopyrightLine))
      { ftrCopyrightLine = PDPSS.WspldFooterCopyrightLine; }
      if (string.IsNullOrEmpty(ftrCopyrightLine))
      { ftrCopyrightLine = PdpSiteFooterCopyrightLine; }
      if (ftrCopyrightLine == PdpSiteNoneKey)
      { ftrCopyrightLine = string.Empty; }
    }
  }
  public string FooterCopyrightLine
  {
    set { ftrCopyrightLine = value; DefaultFooterCopyrightLine(); }
    get { DefaultFooterCopyrightLine(); return ftrCopyrightLine; }
  }

  private string ftrCodebuildLine = string.Empty;
  protected void DefaultFooterCodebuildLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCodebuildLine))
      { ftrCodebuildLine = PDPSS.WspldFooterCodebuildLine; }
      if (string.IsNullOrEmpty(ftrCodebuildLine))
      { ftrCodebuildLine = PdpSiteFooterCodebuildLine; }
      if (ftrCodebuildLine == PdpSiteNoneKey)
      { ftrCodebuildLine = string.Empty; }
    }
  }
  public string FooterCodebuildLine
  {
    set { ftrCodebuildLine = value; DefaultFooterCodebuildLine(); }
    get { DefaultFooterCodebuildLine(); return ftrCodebuildLine; }
  }

  private string ftrCrosslinkLine = string.Empty;
  protected void DefaultFooterCrosslinkLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCrosslinkLine))
      { ftrCrosslinkLine = PDPSS.WspldFooterCrosslinkLine; }
      if (string.IsNullOrEmpty(ftrCrosslinkLine))
      { ftrCrosslinkLine = PdpSiteFooterCrosslinkLine; }
      if (ftrCrosslinkLine == PdpSiteNoneKey)
      { ftrCrosslinkLine = string.Empty; }
    }
  }
  public string FooterCrosslinkLine
  {
    set { ftrCrosslinkLine = value; DefaultFooterCrosslinkLine(); }
    get { DefaultFooterCrosslinkLine(); return ftrCrosslinkLine; }
  }

  private string ftrContactLine = string.Empty;
  protected void DefaultFooterContactLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrContactLine))
      { ftrContactLine = PDPSS.WspldFooterContactLine; }
      if (string.IsNullOrEmpty(ftrContactLine))
      { ftrContactLine = PdpSiteFooterContactLine; }
      if (ftrContactLine == PdpSiteNoneKey)
      { ftrContactLine = string.Empty; }
    }
  }
  public string FooterContactLine
  {
    set { ftrContactLine = value; DefaultFooterContactLine(); }
    get { DefaultFooterContactLine(); return ftrContactLine; }
  }

} // end class

// end file