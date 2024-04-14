// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class PdpSiteInfoModel
{
  private string appDispName = ESS;
  public string SiteAppName
  {
    set { appDispName = value; }
    get {
      if (string.IsNullOrEmpty(appDispName))
      { appDispName = PDPSS.AppDisplayName; }
      if (string.IsNullOrEmpty(appDispName))
      { appDispName = PDPCC.PdpCodeAppdesc; }
      return appDispName;
    }
  }

  private string appDispVers = ESS;
  public string SiteAppVersion
  {
    set { appDispVers = value; }
    get {
      if (string.IsNullOrEmpty(appDispVers))
      { appDispVers = PDPSS.AppDisplayVersion; }
      if (string.IsNullOrEmpty(appDispVers))
      { appDispVers = $"({PDPCC.PdpCodeBranch} {PDPCC.PdpCodeAppvers})"; }
      return appDispVers;
    }
  }

  private bool appUseExtn = PDPSS.AppUseExtensions;
  public bool SiteAppUseExtensions
  {
    set { appUseExtn = value;}
    get { return appUseExtn; }
  }

  private string ownerCodeName = ESS;
  public string SiteOwnerCodeName
  {
    set { ownerCodeName = value; }
    get {
      if (string.IsNullOrEmpty(ownerCodeName))
      { ownerCodeName = PDPSS.AppCodeNameLong; }
      return ownerCodeName;
    }
  }

  private string ownerEmail = ESS;
  public string SiteOwnerEmail
  {
    set { ownerEmail = value; }
    get {
      if (string.IsNullOrEmpty(ownerEmail))
      { ownerEmail = PDPSS.AppOwnerEmail; }
      return ownerEmail;
    }
  }

  private string ownerName = ESS;
  public string SiteOwnerName
  {
    set { ownerName = value; }
    get {
      if (string.IsNullOrEmpty(ownerName))
      { ownerName = PDPSS.AppOwnerNameLong; }
      return ownerName;
    }
  }

  private string ownerServer = ESS;
  public string SiteOwnerDefServerType
  {
    set { ownerServer = value; }
    get {
      if (string.IsNullOrEmpty(ownerServer))
      { ownerServer = NPDSCD.ServerTypeDefault.ToString(); }
      return ownerServer;
    }
  }

  // for use in PdpSiteInfoModel and PdpSitePageLayout
  private string siteTitle = ESS;
  public string SiteTitle
  {
    set { siteTitle = value; }
    get {
      if (string.IsNullOrEmpty(siteTitle))
      { siteTitle = PDPSS.AppSiteDefTitle; }
      return siteTitle;
    }
  }

  // for use in PdpSiteInfoModel and PdpSitePageLayout
  private string sitePage = ESS;
  public string SitePage
  {
    set { sitePage = value; }
    get {
      if (string.IsNullOrEmpty(sitePage))
      { sitePage = PDPSS.AppSiteDefPage; }
      return sitePage;
    }
  }

  private string mttAuthor = ESS;
  protected void DefaultMetatagAuthor()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PDPSS.WspldMetatagAuthor; }
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PDPSS.AppOwnerNameLong; }
      if (string.IsNullOrEmpty(mttAuthor))
      { mttAuthor = PdpSiteMetatagAuthor; }
      if (mttAuthor == PdpSiteNoneKey)
      { mttAuthor = ESS; }
    }
  }
  public string MetatagAuthor
  {
    set { mttAuthor = value; DefaultMetatagAuthor(); }
    get { DefaultMetatagAuthor(); return mttAuthor; }
  }

  private string mttKeywords = ESS;
  protected void DefaultMetatagKeywords()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttKeywords))
      { mttKeywords = PDPSS.WspldMetatagKeywords; }
      if (string.IsNullOrEmpty(mttKeywords))
      { mttKeywords = PdpSiteMetatagKeywords; }
      if (mttKeywords == PdpSiteNoneKey)
      { mttKeywords = ESS; }
    }
  }
  public string MetatagKeywords
  {
    set { mttKeywords = value; DefaultMetatagKeywords(); }
    get { DefaultMetatagKeywords(); return mttKeywords; }
  }

  private string mttDescription = ESS;
  protected void DefaultMetatagDescription()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(mttDescription))
      { mttDescription = PDPSS.WspldMetatagDescription; }
      if (string.IsNullOrEmpty(mttDescription))
      { mttDescription = $"{SiteOwnerCodeName} {SiteOwnerDefServerType} {PdpSiteMetatagDescription}"; }
      if (mttDescription == PdpSiteNoneKey)
      { mttDescription = ESS; }
    }
  }
  public string MetatagDescription
  {
    set { mttDescription = value; DefaultMetatagDescription(); }
    get { DefaultMetatagDescription(); return mttDescription; }
  }

  private string hdrImageLogo = ESS;
  protected void DefaultHeaderImageLogo()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrImageLogo))
      { hdrImageLogo = PDPSS.WspldHeaderImageLogo; }
      if (string.IsNullOrEmpty(hdrImageLogo))
      { hdrImageLogo = PdpSiteHeaderImageLogo; }
      if (hdrImageLogo == PdpSiteNoneKey)
      { hdrImageLogo = ESS; }
    }
  }
  public string HeaderImageLogo
  {
    set { hdrImageLogo = value; DefaultHeaderImageLogo(); }
    get { DefaultHeaderImageLogo(); return hdrImageLogo; }
  }

  private string hdrTitle = ESS;
  protected void DefaultHeaderTitle()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PDPSS.WspldHeaderTitle; }
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PDPSS.AppSiteDefTitle; }
      if (string.IsNullOrEmpty(hdrTitle))
      { hdrTitle = PdpSiteHeaderTitle; }
      if (hdrTitle == PdpSiteNoneKey)
      { hdrTitle = ESS; }
    }
  }
  public string HeaderTitle
  {
    set { hdrTitle = value; DefaultHeaderTitle(); }
    get { DefaultHeaderTitle(); return hdrTitle; }
  }

  private string hdrTagLine = ESS;
  protected void DefaultHeaderTagLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrTagLine))
      { hdrTagLine = PDPSS.WspldHeaderTagLine; }
      if (string.IsNullOrEmpty(hdrTagLine))
      { hdrTagLine = PdpSiteHeaderTagLine; }
      if (hdrTagLine == PdpSiteNoneKey)
      { hdrTagLine = ESS; }
    }
  }
  public string HeaderTagLine
  {
    set { hdrTagLine = value; DefaultHeaderTagLine(); }
    get { DefaultHeaderTagLine(); return hdrTagLine; }
  }

  private string hdrSloganLine = ESS;
  protected void DefaultHeaderSloganLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(hdrSloganLine))
      { hdrSloganLine = PDPSS.WspldHeaderSloganLine; }
      if (string.IsNullOrEmpty(hdrSloganLine))
      { hdrSloganLine = PdpSiteHeaderSloganLine; }
      if (hdrSloganLine == PdpSiteNoneKey)
      { hdrSloganLine = ESS; }
    }
  }
  public string HeaderSloganLine
  {
    set { hdrSloganLine = value; DefaultHeaderSloganLine(); }
    get { DefaultHeaderSloganLine(); return hdrSloganLine; }
  }

  private string ftrCopyrightLine = ESS;
  protected void DefaultFooterCopyrightLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCopyrightLine))
      { ftrCopyrightLine = PDPSS.WspldFooterCopyrightLine; }
      if (string.IsNullOrEmpty(ftrCopyrightLine))
      { ftrCopyrightLine = PdpSiteFooterCopyrightLine; }
      if (ftrCopyrightLine == PdpSiteNoneKey)
      { ftrCopyrightLine = ESS; }
    }
  }
  public string FooterCopyrightLine
  {
    set { ftrCopyrightLine = value; DefaultFooterCopyrightLine(); }
    get { DefaultFooterCopyrightLine(); return ftrCopyrightLine; }
  }

  private string ftrCodebuildLine = ESS;
  protected void DefaultFooterCodebuildLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCodebuildLine))
      { ftrCodebuildLine = PDPSS.WspldFooterCodebuildLine; }
      if (string.IsNullOrEmpty(ftrCodebuildLine))
      { ftrCodebuildLine = PdpSiteFooterCodebuildLine; }
      if (ftrCodebuildLine == PdpSiteNoneKey)
      { ftrCodebuildLine = ESS; }
    }
  }
  public string FooterCodebuildLine
  {
    set { ftrCodebuildLine = value; DefaultFooterCodebuildLine(); }
    get { DefaultFooterCodebuildLine(); return ftrCodebuildLine; }
  }

  private string ftrCrosslinkLine = ESS;
  protected void DefaultFooterCrosslinkLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrCrosslinkLine))
      { ftrCrosslinkLine = PDPSS.WspldFooterCrosslinkLine; }
      if (string.IsNullOrEmpty(ftrCrosslinkLine))
      { ftrCrosslinkLine = PdpSiteFooterCrosslinkLine; }
      if (ftrCrosslinkLine == PdpSiteNoneKey)
      { ftrCrosslinkLine = ESS; }
    }
  }
  public string FooterCrosslinkLine
  {
    set { ftrCrosslinkLine = value; DefaultFooterCrosslinkLine(); }
    get { DefaultFooterCrosslinkLine(); return ftrCrosslinkLine; }
  }

  private string ftrContactLine = ESS;
  protected void DefaultFooterContactLine()
  {
    if (PDPSS.AppUsePageDefaults)
    {
      if (string.IsNullOrEmpty(ftrContactLine))
      { ftrContactLine = PDPSS.WspldFooterContactLine; }
      if (string.IsNullOrEmpty(ftrContactLine))
      { ftrContactLine = PdpSiteFooterContactLine; }
      if (ftrContactLine == PdpSiteNoneKey)
      { ftrContactLine = ESS; }
    }
  }
  public string FooterContactLine
  {
    set { ftrContactLine = value; DefaultFooterContactLine(); }
    get { DefaultFooterContactLine(); return ftrContactLine; }
  }

} // end class

// end file