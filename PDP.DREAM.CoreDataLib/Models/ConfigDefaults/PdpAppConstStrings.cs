// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // this class contains nothing but constants and enums only
  // if enum value not set in code, it defaults to 0

  // Messages/Notices
  public const string NLMMESHNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MeSH Descriptor Records contained in the NPDS Resource Representations. No modifications have been made to the original source Descriptor Records from NLM MeSH other than incorporation within the NPDS Message wrapper.";
  public const string NLMMICADNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MICAD metadata records contained in the NPDS Resource Representations. No modifications have been made to the original record metadata from NLM MICAD other than incorporation within the NPDS Message wrapper.";

  // TODO: eliminate redundancy with NpdsServiceDefaults
  public const string PdpIdentityDbConnName = "NpdsUserDbserver";
  // Used for cookie session authorization
  public const string PdpIdentityScheme = "PDP-SIAA-Cookies";
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

  // used by QEB User REST Context properties as defaults for PdpSiteSettings
  public const string PdpSiteMetatagAuthor = "PORTAL-DOORS Project";
  public const string PdpSiteMetatagKeywords = "PDP, NPDS, semantic web and grid, information and knowledge engineering";
  public const string PdpSiteMetatagDescription = "for the NPDS Cyberinfrastructure";
  public const string PdpSiteHeaderImageLogo = "src='/PdpSiteLogo20101123.png' alt='PORTAL-DOORS Project Logo Image'";
  public const string PdpSiteHeaderTitle = "";
  public const string PdpSiteHeaderTagLine = "PORTAL-DOORS Project (<a href='http://www.portaldoors.org/' target='_blank'>PDP</a>) for the";
  public const string PdpSiteHeaderSloganLine = "Nexus-PORTAL-DOORS-Scribe (<a href='http://www.npdslinks.org/' target='_blank'>NPDS</a>) Cyberinfrastructure";
  public const string PdpSiteFooterCopyrightLine = "PDP websites and content &copy; 2007 - 2022 <a href='http://www.portaldoors.org/' target='_blank'>PORTAL-DOORS Project (PDP)</a>.<br />";
  public const string PdpSiteFooterCodebuildLine = "";
  public const string PdpSiteFooterCrosslinkLine = "";
  public const string PdpSiteFooterContactLine = "";

  // TODO: move to a file with NPDS serverTags
  public const string NpdsRoot = "NPDS-Root";
  // NPDS User, Agent, Author, Editor, Admin roles
  public const string NpdsAnon = "NpdsAnon";
  public const string NpdsAuth = "NpdsAuth";
  public const string NpdsUser = "NpdsUser";
  public const string NpdsAgent = "NpdsAgent";
  public const string NpdsAuthor = "NpdsAuthor";
  public const string NpdsEditor = "NpdsEditor";
  public const string NpdsAdmin = "NpdsAdmin";
  // syntax must be valid for MVC Authorize(Roles=) attribute
  public const string NpdsAllAuthRoles = "NpdsUser, NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin";

  // for use with Razor and Telerik Kendo controls (Tkgrd from Telerik Kendo GRiD)
  public const string TkgrdToken = "kendoToken";
  public const string UtcdtFormat = "{0:yyyy-MM-dd HH:mm}";
  public const string Numf3Format = "{0:n3}";
} // end class

// end file