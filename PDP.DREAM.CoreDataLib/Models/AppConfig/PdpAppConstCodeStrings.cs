// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // TODO: migrate future use of
  // PdpCodeDefaultAppversion to PdpAssemblyInfo
  public const string PdpCodeDefaultAppversion = "11.4.14";
  public const string MSASPNETENVVAR = "ASPNETCORE_ENVIRONMENT";
  public const string XunitEnvirname = "XunitDevTestDebug";
  public const string PdpDateTimeSortFormat = "yyyyMMddHHmmss";

  // TODO: eliminate redundancy with NpdsServiceDefaults
  public const string PdpIdentityDbconnam = "NpdsCiaamDbserver";
  // Used for cookie session authorization
  public const string PdpIdentityScheme = "NpdsCiaamCookies";
  // Used for XSRF protection when adding external logins
  public const string PdpIdentityXsrfKey = "XsrfId";
  // Used for resetting stamps with invalid error message
  public const string PdpInvalidToken = "INVALID";

  // used for RouteDebugger Utility
  public const string PdpDebugRouteQueryKey = "debug";
  public const string PdpHelpRouteQueryKey = "help";
  public const string PdpHelpRouteHackKey = "??";

  // PDP Site Defaults
  public const string PdpSiteNoneKey = "none";
  public const string PdpSitePathKey = "path";
  public const string PdpSiteDefaultWebroot = "wwwroot";
  public const string PdpSiteDefaultHtml = "PdpSiteDefault.html";
  public const string PdpSiteDefaultDbconstr = @"Server=.\SQLS2022;Database=PdpDevTestDebug;";

  // used by QEB User REST Context properties as defaults for PdpSiteSettings
  public const string PdpSiteMetatagAuthor = "PORTAL-DOORS Project";
  public const string PdpSiteMetatagKeywords = "PDP, NPDS, semantic web and grid, information and knowledge engineering";
  public const string PdpSiteMetatagDescription = "for the NPDS Cyberinfrastructure";
  public const string PdpSiteHeaderImageLogo = "src='/PdpSiteLogo20101123.png' alt='PORTAL-DOORS Project Logo Image'";
  public const string PdpSiteHeaderTitle = "";
  public const string PdpSiteHeaderTagLine = "PORTAL-DOORS Project (<a href='https://www.portaldoors.org/' target='_blank'>PDP</a>) for the<br />Nexus-PORTAL-DOORS-Scribe (<a href='https://www.npdslinks.org/' target='_blank'>NPDS</a>) Cyberinfrastructure";
  public const string PdpSiteHeaderSloganLine = "";
  public const string PdpSiteFooterCopyrightLine = "PDP websites and content &copy; 2006-2024 <a href='https://www.portaldoors.org/' target='_blank'>PORTAL-DOORS Project (PDP)</a>.<br />";
  public const string PdpSiteFooterCodebuildLine = "";
  public const string PdpSiteFooterCrosslinkLine = "";
  public const string PdpSiteFooterContactLine = "";

  public const string QebiAnon = "PdpAppAnon";
  public const string QebiUser = "PdpAppUser";
  public const string QebiGuru = "PdpAppGuru";

  // NPDS User, Agent, Author, Editor, Admin roles
  public const string NpdsAnon = "NpdsAnon";
  public const string NpdsUser = "NpdsUser";
  public const string NpdsAgent = "NpdsAgent";
  public const string NpdsAuthor = "NpdsAuthor";
  public const string NpdsReviewer = "NpdsReviewer";
  public const string NpdsEditor = "NpdsEditor";
  public const string NpdsAdmin = "NpdsAdmin";
  // TODO: move to a file with NPDS serverTags
  public const string NpdsRoot = "NPDS-Root";

  // for use with Razor and Telerik Kendo controls (Tkndo from Telerik KeNDO)
  // see also ResrepRootModelBase
  public const string TkndoToken = "kendoToken"; // security token for postbacks
  public const string TkndoElemPrfx = "button#"; // NdisElemId prefix for NdisElemMsg json content in Kendo toolbars
  public const string TkndoLinkPrfx = "span#"; // NdisLinkId prefix for NdisLinkMsg json content in Kendo gridcells 
  public const string UtcdtFormat = "{0:yyyy-MM-dd HH:mm}";
  public const string Numf3Format = "{0:n3}";

  // Messages/Notices
  public const string NLMMESHNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MeSH Descriptor Records contained in the NPDS Resource Representations. No modifications have been made to the original source Descriptor Records from NLM MeSH other than incorporation within the NPDS Message wrapper.";
  public const string NLMMICADNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MICAD metadata records contained in the NPDS Resource Representations. No modifications have been made to the original record metadata from NLM MICAD other than incorporation within the NPDS Message wrapper.";

} // end class

// end file