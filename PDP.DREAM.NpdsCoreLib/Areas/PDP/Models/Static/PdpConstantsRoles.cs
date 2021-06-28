namespace PDP.DREAM.NpdsCoreLib.Models
{
  public static partial class PdpConst
  {
    // this class contains nothing but constants and enums only
    // if enum value not set in code, it defaults to 0


    // Messages/Notices
    public const string NLMMESHNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MeSH Descriptor Records contained in the NPDS Resource Representations. No modifications have been made to the original source Descriptor Records from NLM MeSH other than incorporation within the NPDS Message wrapper.";
    public const string NLMMICADNOTICE = "US National Library of Medicine is the creator, maintainer, and provider of the NLM MICAD metadata records contained in the NPDS Resource Representations. No modifications have been made to the original record metadata from NLM MICAD other than incorporation within the NPDS Message wrapper.";

    // NPDS database Connection String Names for use with settings in web.config
    public enum NamesForRequiredDbConnStrings
    {
      NpdsUserDbserver, NpdsAgentDbserver, NpdsCoreDbserver,
      NpdsNexusDiristry, NpdsPortalRegistry, NpdsDoorsDirectory, NpdsScribeRegistrar
    }
    public enum NamesForPermittedDbConnStrings
    {
      NpdsRegistryAuth1Dbserver, NpdsRegistryAuth2Dbserver, NpdsRegistryCacheDbserver,
      NpdsDirectoryAuth1Dbserver, NpdsDirectoryAuth2Dbserver, NpdsDirectoryCacheDbserver,
      NpdsDiristryAuth1Dbserver, NpdsDiristryAuth2Dbserver, NpdsDiristryCacheDbserver
    }
    // TODO:  recode with key/value pairs to allow arbitary naming in web.config;
    //     new property to separate term/thesaurus/ont servers (MeSH, MICAD, etc)
    //    as a group separate from the PORTAL/DOORS/Nexus servers
    public enum NamesForOptionalDbConnStrings
    {
      // TODO: generalize to eliminate years from Nlmmesh and Nlmmicad servers
      NpdsNlmmeshDbserver, NpdsNlmmicadDbserver,
      // TODO: registrars are temporary transitional hack only, eliminate these options
      NpdsBhaRegistrarDbserver, NpdsGtgRegistrarDbserver, NpdsPdpRegistrarDbserver
    }

    // TODO: shouldn't these user roles be better associated with PDP instead of NPDS?
    // TODO: update these NPDS roles for ASP.NET Identity roles
    // TODO: fix discrepancy on NpdsUser as role for ASP.NET
    // TODO: reconcile after transition to NPDS
    public const string NPDSUSER = "NpdsUser";
    public const string NPDSAGENT = "NpdsAgent";
    public const string NPDSAUTHOR = "NpdsAuthor";
    public const string NPDSEDITOR = "NpdsEditor";
    public const string NPDSADMIN = "NpdsAdmin";
    // syntax must be valid for MVC Authorize(Roles=) attribute
    public const string NPDSAllAuthRoles = "NpdsAuthor, NpdsEditor, NpdsAdmin"; 
    // ATTN: recall enums cannot be used in attributes so must also
    // use individual const strings above for roles specified in attributes
    public enum IdentityRoleNames
    { NpdsUser = 1, NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin }

  }

}