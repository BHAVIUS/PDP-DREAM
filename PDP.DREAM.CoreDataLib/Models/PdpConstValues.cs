// PdpConstValues.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpConst
{
  // this class contains nothing but constants and enums only
  // if enum value not set in code, it defaults to 0

  // Messages/Notices
  public const string NLMMESHNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MeSH Descriptor Records contained in the NPDS Resource Representations. No modifications have been made to the original source Descriptor Records from NLM MeSH other than incorporation within the NPDS Message wrapper.";
  public const string NLMMICADNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MICAD metadata records contained in the NPDS Resource Representations. No modifications have been made to the original record metadata from NLM MICAD other than incorporation within the NPDS Message wrapper.";

  //PDP area MVC default constants
  public const string PdpMvcArea = "NPDS";
  public const string PdpMvcController = "Site";
  public const string PdpMvcAltController = "CoreDataLib";
  public const string PdpMvcAction = "Info";
  public const string PdpMvcAltAction = "Help";
  public const string PdpMvcUser = "User";
  public const string PdpMvcAgent = "Agent";

  // TODO: eliminate redundancy with NpdsServiceDefaults
  public const string PdpIdentityDbConnName = "NpdsUserDbserver";
  // Used for cookie session authorization
  public const string PdpIdentityScheme = "PDP-SIAA-Cookies";
  // Used for XSRF protection when adding external logins
  public const string PdpIdentityXsrfKey = "XsrfId";
  // Used for resetting stampes with invalid error message
  public const string PdpInvalidToken = "INVALID";

  // used for RouteDebugger Utility
  public const string PdpDebugRouteQueryKey = "debug";
  public const string PdpHelpRouteQueryKey = "help";
  public const string PdpHelpRouteHackKey = "??";

  // used by PdpRestContext properties as defaults for PdpSiteSettings
  public const string PdpSiteMetatagAuthor = "PORTAL-DOORS Project";
  public const string PdpSiteMetatagKeywords = "PDP, NPDS, semantic web and grid, information and knowledge engineering";
  public const string PdpSiteMetatagDescription = "for the NPDS Cyberinfrastructure";
  public const string PdpSiteHeaderImageLogo = "src='/PdpSiteLogo20101123.png' alt='PORTAL-DOORS Project Logo Image'";
  public const string PdpSiteHeaderTitle = "";
  public const string PdpSiteHeaderTagLine = "PORTAL-DOORS Project (<a href='http://www.portaldoors.org/' target='_blank'>PDP</a>) for the";
  public const string PdpSiteHeaderSloganLine = "Nexus-PORTAL-DOORS-Scribe (<a href='http://www.npdslinks.org/' target='_blank'>NPDS</a>) Cyberinfrastructure";
  public const string PdpSiteFooterCopyrightLine = "PDP websites &copy; 2007 - 2021 <a href='http://www.portaldoors.org/' target='_blank'>PORTAL-DOORS Project (PDP)</a>.<br />";
  public const string PdpSiteFooterCrosslinkLine = "";
  public const string PdpSiteFooterContactLine = "8 Gilly Flower Street, Ladera Ranch CA 92694 USA. Tel:+1(949)481-3121.<br />";
  public const string PdpPageDefaultNone = "none";

  // NPDS User, Agent, Author, Editor, Admin roles
  public const string NPDSUSER = "NpdsUser";
  public const string NPDSAGENT = "NpdsAgent";
  public const string NPDSAUTHOR = "NpdsAuthor";
  public const string NPDSEDITOR = "NpdsEditor";
  public const string NPDSADMIN = "NpdsAdmin";
  // syntax must be valid for MVC Authorize(Roles=) attribute
  public const string NPDSAllAuthRoles = "NpdsAuthor, NpdsEditor, NpdsAdmin";

} // class
