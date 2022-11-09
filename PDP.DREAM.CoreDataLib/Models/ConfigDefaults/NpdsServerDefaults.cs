// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class NpdsServerDefaults : PdpConfigManager
{
  public NpdsServerDefaults(string projCodedir) : base(projCodedir)
  {
    if (pdpSiteCnfgMngr == null) { throw new NullReferenceException(); }

    // TODO: recode Enums and the Enum parsing to assure that error thrown for invalid arguments

    defSearchScope = PdpEnum<NpdsSearchScope>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefSearchScope));
    defSearchFilter = PdpEnum<NpdsSearchFilter>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefSearchFilter));
    defNodeType = PdpEnum<NpdsNodeType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefNodeType));
    sitDefServerType = PdpEnum<NpdsServerType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefServerType));
    defServiceType = PdpEnum<NpdsServiceType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefServiceType));
    defDatabaseType = PdpEnum<NpdsDatabaseType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefDatabaseType));
    defDatabaseAccess = PdpEnum<NpdsDatabaseAccess>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefDatabaseAccess));
    defRecordAccess = PdpEnum<NpdsRecordAccess>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefRecordAccess));
    var repFmt = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefResrepFormat);
    defResrepFormat = PdpEnum<NpdsResrepFormat>.ParseString(repFmt);
    var msgFmt = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefMessageFormat);
    defMessageFormat = PdpEnum<NpdsMessageFormat>.ParseString(msgFmt);
    defEntityType = PdpEnum<NpdsEntityType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefEntityType));
    defNewEntityType = PdpEnum<NpdsEntityType>.ParseString(ParseAppStringSetting(NamesForServiceDefaults.NpdsDefNewEntityType));

    // intended for use as service initial defaults for new records
    defDiristryTag = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefDiristryTag);
    defRegistryTag = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefRegistryTag);
    defDirectoryTag = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefDirectoryTag);
    defRegistrarTag = ParseAppStringSetting(NamesForServiceDefaults.NpdsDefRegistrarTag);

    // intended for use as service restriction constraints on scope of the authority of an authoritative server
    conRegistrarTag = ParseAppStringSetting(NamesForServiceConstraints.NpdsConRegistrarTag);
    conRegistryTag = ParseAppStringSetting(NamesForServiceConstraints.NpdsConRegistryTag);
    conDirectoryTag = ParseAppStringSetting(NamesForServiceConstraints.NpdsConDirectoryTag);
    conDiristryTag = ParseAppStringSetting(NamesForServiceConstraints.NpdsConDiristryTag);

    // required database connection strings
    userDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsUserDbserver);
    agentDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsAgentDbserver);
    coreDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsCoreDbserver);
    diristryDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsNexusDiristry);
    registryDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsPortalRegistry);
    directoryDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsDoorsDirectory);
    registrarDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsScribeRegistrar);

    // permitted datatbase connection strings
    portalAuth1Dbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsRegistryAuth1Dbserver);
    doorsAuth1Dbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsDirectoryAuth1Dbserver);
    nexusAuth1Dbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsDiristryAuth1Dbserver);
    portalCacheDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsRegistryCacheDbserver);
    doorsCacheDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsDirectoryCacheDbserver);
    nexusCacheDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsDiristryCacheDbserver);

    // optional database connection strings
    nlmmeshDbcs = ParseAppDbConnString(NamesForOptionalDbConnStrings.NpdsNlmmeshDbserver);
    nlmmicadDbcs = ParseAppDbConnString(NamesForOptionalDbConnStrings.NpdsNlmmicadDbserver);
  }

  // NPDS service connection strings
  public string NpdsUserDbconstr
  { get { return userDbcs; } }
  private string userDbcs;
  public string NpdsAgentDbconstr
  { get { return agentDbcs; } }
  private string agentDbcs;
  public string NpdsCoreDbconstr
  { get { return coreDbcs; } }
  private string coreDbcs;
  public string NpdsDiristryDbconstr
  { get { return diristryDbcs; } }
  private string diristryDbcs;
  public string NpdsRegistryDbconstr
  { get { return registryDbcs; } }
  private string registryDbcs;
  public string NpdsDirectoryDbconstr
  { get { return directoryDbcs; } }
  private string directoryDbcs;
  public string NpdsRegistrarDbconstr
  { get { return registrarDbcs; } }
  private string registrarDbcs;

  // NPDS PORTAL DataBase Connection String

  public string NpdsPortalAuth1Dbconstr
  { get { return portalAuth1Dbcs; } }
  private string portalAuth1Dbcs;

  public string NpdsPortalCacheDbconstr
  { get { return portalCacheDbcs; } }
  private string portalCacheDbcs;

  // NPDS DOORS DataBase Connection String

  public string NpdsDoorsAuth1Dbconstr
  { get { return doorsAuth1Dbcs; } }
  private string doorsAuth1Dbcs;

  public string NpdsDoorsCacheDbconstr
  { get { return doorsCacheDbcs; } }
  private string doorsCacheDbcs;

  // NPDS Nexus DataBase Connection Strings
  public string NpdsNexusAuth1Dbconstr
  { get { return nexusAuth1Dbcs; } }
  private string nexusAuth1Dbcs;

  public string NpdsNexusCacheDbconstr
  { get { return nexusCacheDbcs; } }
  private string nexusCacheDbcs;

  // NPDS NLM MeSH and MICAD DataBase Connection Strings
  public string NpdsNlmmeshDbconstr
  { get { return nlmmeshDbcs; } }
  private string nlmmeshDbcs;
  public string NpdsNlmmicadDbconstr
  { get { return nlmmicadDbcs; } }
  private string nlmmicadDbcs;


  public NpdsSearchFilter NpdsDefaultSearchFilter
  { get { return defSearchFilter; } }
  private NpdsSearchFilter defSearchFilter;

  public NpdsSearchScope NpdsDefaultSearchScope
  { get { return defSearchScope; } }
  private NpdsSearchScope defSearchScope;

  public NpdsNodeType NpdsDefaultNodeType
  { get { return defNodeType; } }
  private NpdsNodeType defNodeType;

  public NpdsServiceType NpdsDefaultServiceType
  { get { return defServiceType; } }
  private NpdsServiceType defServiceType;

  public NpdsServerType NpdsDefaultServerType
  { get { return sitDefServerType; } }
  private NpdsServerType sitDefServerType;

  public NpdsDatabaseType NpdsDefaultDatabaseType
  { get { return defDatabaseType; } }
  private NpdsDatabaseType defDatabaseType;

  public NpdsDatabaseAccess NpdsDefaultDatabaseAccess
  { get { return defDatabaseAccess; } }
  private NpdsDatabaseAccess defDatabaseAccess;

  public NpdsRecordAccess NpdsDefaultRecordAccess
  { get { return defRecordAccess; } }
  private NpdsRecordAccess defRecordAccess;

  public NpdsResrepFormat NpdsDefaultResrepFormat
  { get { return defResrepFormat; } }
  private NpdsResrepFormat defResrepFormat;

  public NpdsMessageFormat NpdsDefaultMessageFormat
  { get { return defMessageFormat; } }
  private NpdsMessageFormat defMessageFormat;

  public bool NpdsDefaultQueryFormat
  { get { return defQueryFormat; } }
  private bool defQueryFormat = false;

  // for use as filter when searching/selecting records
  public NpdsEntityType NpdsDefaultEntityType
  { get { return defEntityType; } }
  private NpdsEntityType defEntityType;

  // for use as default when creating new records
  public NpdsEntityType NpdsDefaultNewEntityType
  { get { return defNewEntityType; } }
  private NpdsEntityType defNewEntityType;

  // NPDS cyberinfrastructure root service tag
  public string NpdsRootServiceTag { get { return NpdsRoot; } }

  // cache for tag-guid conversion
  public PdpTagGuidDictionary NpdsServiceCache { set; get; } = new PdpTagGuidDictionary();

  // diristry/registry/directory/registrar defaults with tags and guids

  public string NpdsDefaultDiristryTag
  { get { return defDiristryTag; } }
  private string defDiristryTag;

  public Guid NpdsDefaultDiristryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultDiristryTag); } }

  public string NpdsDefaultRegistryTag
  { get { return defRegistryTag; } }
  private string defRegistryTag;

  public Guid NpdsDefaultRegistryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultRegistryTag); } }

  public string NpdsDefaultDirectoryTag
  { get { return defDirectoryTag; } }
  private string defDirectoryTag;

  public Guid NpdsDefaultDirectoryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultDirectoryTag); } }

  public string NpdsDefaultRegistrarTag
  { get { return defRegistrarTag; } }
  private string defRegistrarTag;

  public Guid NpdsDefaultRegistrarGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultRegistrarTag); } }

  // diristry/registry/directory/registrar constraints with tags only

  public string NpdsConstraintDiristryTag
  { get { return conDiristryTag; } }
  private string conDiristryTag;

  public string NpdsConstraintRegistryTag
  { get { return conRegistryTag; } }
  private string conRegistryTag;

  public string NpdsConstraintDirectoryTag
  { get { return conDirectoryTag; } }
  private string conDirectoryTag;

  public string NpdsConstraintRegistrarTag
  { get { return conRegistrarTag; } }
  private string conRegistrarTag;

} // end class

// end file