// NpdsServerDefaults.cs
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class NpdsServerDefaults : PdpConfigManager
{
  public NpdsServerDefaults() : base()
  {
    base.Configure(); // configures pdpSiteConfig in PdpConfigManager
    pdpSiteConfig.CatchNullObject(nameof(pdpSiteConfig), nameof(NpdsServerDefaults));

    // TODO: recode Enums and the Enum parsing to assure error thrown for invalid arguments

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
    qebiDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsSiaaDbserver);
    coreDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsCoreDbserver);
    nexusDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsNexusDiristry);
    portalDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsPortalRegistry);
    doorsDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsDoorsDirectory);
    scribeDbcs = ParseAppDbConnString(NamesForRequiredDbConnStrings.NpdsScribeRegistrar);

    // permitted database connection strings
    acmsDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsAcmsDbserver);
    bridgeDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsBridgeDbserver);
    vocabDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsVocabDbserver);
    cacheDbcs = ParseAppDbConnString(NamesForPermittedDbConnStrings.NpdsCacheDbserver);
  }

  private string qebiDbcs = string.Empty;
  public string QebiDbconstr
  { get { return qebiDbcs; } }

  private string coreDbcs = string.Empty;
  public string CoreDbconstr
  { get { return coreDbcs; } }

  private string nexusDbcs = string.Empty;
  public string NexusDbconstr
  { get { return nexusDbcs; } }

  private string portalDbcs = string.Empty;
  public string PortalDbconstr
  { get { return portalDbcs; } }

  private string doorsDbcs = string.Empty;
  public string DoorsDbconstr
  { get { return doorsDbcs; } }

  private string scribeDbcs = string.Empty;
  public string ScribeDbconstr
  { get { return scribeDbcs; } }

  private string acmsDbcs = string.Empty;
  public string AcmsDbconstr
  { get { return acmsDbcs; } }

  private string bridgeDbcs = string.Empty;
  public string BridgeDbconstr
  { get { return bridgeDbcs; } }

  private string vocabDbcs = string.Empty;
  public string VocabDbconstr
  { get { return vocabDbcs; } }

  private string cacheDbcs = string.Empty;
  public string CacheDbconstr
  { get { return cacheDbcs; } }

  // NpdsClient related defaults

  private NpdsSearchFilter defSearchFilter;
  public NpdsSearchFilter SearchFilterDefault
  { get { return defSearchFilter; } }

  private NpdsSearchScope defSearchScope;
  public NpdsSearchScope SearchScopeDefault
  { get { return defSearchScope; } }

  private NpdsNodeType defNodeType;
  public NpdsNodeType NodeTypeDefault
  { get { return defNodeType; } }

  private NpdsServiceType defServiceType;
  public NpdsServiceType ServiceTypeDefault
  { get { return defServiceType; } }

  private NpdsServerType sitDefServerType;
  public NpdsServerType ServerTypeDefault
  { get { return sitDefServerType; } }

  private NpdsDatabaseType defDatabaseType;
  public NpdsDatabaseType DatabaseTypeDefault
  { get { return defDatabaseType; } }

  private NpdsDatabaseAccess defDatabaseAccess;
  public NpdsDatabaseAccess DatabaseAccessDefault
  { get { return defDatabaseAccess; } }

  private NpdsRecordAccess defRecordAccess;
  public NpdsRecordAccess RecordAccessDefault
  { get { return defRecordAccess; } }

  private NpdsResrepFormat defResrepFormat;
  public NpdsResrepFormat ResrepFormatDefault
  { get { return defResrepFormat; } }

  private NpdsMessageFormat defMessageFormat;
  public NpdsMessageFormat MessageFormatDefault
  { get { return defMessageFormat; } }

  private bool defQueryFormat = false;
  public bool QueryFormatDefault
  { get { return defQueryFormat; } }

  // for use as filter when searching/selecting records
  private NpdsEntityType defEntityType;
  public NpdsEntityType EntityTypeDefault
  { get { return defEntityType; } }

  // for use as default when creating new records
  private NpdsEntityType defNewEntityType;
  public NpdsEntityType NewEntityTypeDefault
  { get { return defNewEntityType; } }

  // NPDS cyberinfrastructure root service tag
  public string NpdsRootServiceTag { get { return NpdsRoot; } }

  // cache for tag-guid conversion
  public PdpTagGuidDictionary NpdsServiceCache { set; get; } = new PdpTagGuidDictionary();

  // diristry/registry/directory/registrar defaults with tags and guids

  public string DiristryTagDefault
  { get { return defDiristryTag; } }
  private string defDiristryTag = string.Empty;

  public Guid DiristryGuidDefault
  { get { return NpdsServiceCache.GetByTag(DiristryTagDefault); } }

  public string RegistryTagDefault
  { get { return defRegistryTag; } }
  private string defRegistryTag = string.Empty;

  public Guid RegistryGuidDefault
  { get { return NpdsServiceCache.GetByTag(RegistryTagDefault); } }

  public string DirectoryTagDefault
  { get { return defDirectoryTag; } }
  private string defDirectoryTag = string.Empty;

  public Guid DirectoryGuidDefault
  { get { return NpdsServiceCache.GetByTag(DirectoryTagDefault); } }

  public string RegistrarTagDefault
  { get { return defRegistrarTag; } }
  private string defRegistrarTag = string.Empty;

  public Guid RegistrarGuidDefault
  { get { return NpdsServiceCache.GetByTag(RegistrarTagDefault); } }

  // diristry/registry/directory/registrar constraints with tags only

  public string DiristryTagConstraint
  { get { return conDiristryTag; } }
  private string conDiristryTag = string.Empty;

  public string RegistryTagConstraint
  { get { return conRegistryTag; } }
  private string conRegistryTag = string.Empty;

  public string DirectoryTagConstraint
  { get { return conDirectoryTag; } }
  private string conDirectoryTag = string.Empty;

  public string RegistrarTagConstraint
  { get { return conRegistrarTag; } }
  private string conRegistrarTag = string.Empty;

} // end class

// end file