// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientDefaults : PdpConfigManager
{
  public NpdsClientDefaults() : base()
  {
    base.Configure(); // configures pdpSiteConfig in PdpConfigManager
    pdpSiteConfig.CatchNullObject(nameof(pdpSiteConfig), nameof(NpdsClientDefaults));

    InitSearchScopeEnums();
    SearchScopeDefault = ParseSearchScope(ParseAppStringSetting(NpdsDef.SearchScope));

    InitSearchFilterEnums();
    SearchFilterDefault = ParseSearchFilter(ParseAppStringSetting(NpdsDef.SearchFilter));

    InitNodeTypeEnums();
    NodeTypeDefault = ParseNodeType(ParseAppStringSetting(NpdsDef.NodeType));

    InitServerTypeEnums();
    ServerTypeDefault = ParseServerType(ParseAppStringSetting(NpdsDef.ServerType));

    InitServiceTypeEnums();
    ServiceTypeDefault = ParseServiceType(ParseAppStringSetting(NpdsDef.ServiceType));

    InitDatabaseTypeEnums();
    DatabaseTypeDefault = ParseDatabaseType(ParseAppStringSetting(NpdsDef.DatabaseType));

    InitDatabaseAccessEnums();
    DatabaseAccessDefault = ParseDatabaseAccess(ParseAppStringSetting(NpdsDef.DatabaseAccess));

    InitRecordAccessEnums();
    RecordAccessDefault = ParseRecordAccess(ParseAppStringSetting(NpdsDef.RecordAccess));

    InitEntityTypeEnums();
    EntityTypeDefault = ParseEntityType(ParseAppStringSetting(NpdsDef.EntityType));

    InitInfosetStatusEnums();
    InfosetStatusDefault = ParseInfosetStatus(ParseAppStringSetting(NpdsDef.InfosetStatus));

    InitFieldFormatEnums();
    FieldFormatDefault = ParseFieldFormat(ParseAppStringSetting(NpdsDef.FieldFormat));

    InitResrepFormatEnums();
    ResrepFormatDefault = ParseResrepFormat(ParseAppStringSetting(NpdsDef.ResrepFormat));

    InitMessageFormatEnums();
    MessageFormatDefault = ParseMessageFormat(ParseAppStringSetting(NpdsDef.MessageFormat));

    // intended for use as service initial defaults for new records
    DiristryTagDefault = ParseAppStringSetting(NpdsDef.DiristryTag); // Nexus
    RegistryTagDefault = ParseAppStringSetting(NpdsDef.RegistryTag); // PORTAL
    DirectoryTagDefault = ParseAppStringSetting(NpdsDef.DirectoryTag); // DOORS
    RegistrarTagDefault = ParseAppStringSetting(NpdsDef.RegistrarTag); // Scribe
    ServiceTagDefault = ParseAppStringSetting(NpdsDef.ServiceTag); // Core

    // intended for use as service restriction constraints on scope of the authority of an authoritative server
    DiristryTagConstraint = ParseAppStringSetting(NpdsCon.DiristryTag); // Nexus
    RegistryTagConstraint = ParseAppStringSetting(NpdsCon.RegistryTag); // PORTAL
    DirectoryTagConstraint = ParseAppStringSetting(NpdsCon.DirectoryTag); // DOORS
    RegistrarTagConstraint = ParseAppStringSetting(NpdsCon.RegistrarTag); // Scribe

    // required database connection strings
    CiaamDbconstr = ParseAppDbConnString(NpdsDbcsReq.CiaamDbserver);
    CoreDbconstr = ParseAppDbConnString(NpdsDbcsReq.CoreDbserver);
    NexusDbconstr = ParseAppDbConnString(NpdsDbcsReq.NexusDiristry);
    PortalDbconstr = ParseAppDbConnString(NpdsDbcsReq.PortalRegistry);
    DoorsDbconstr = ParseAppDbConnString(NpdsDbcsReq.DoorsDirectory);
    ScribeDbconstr = ParseAppDbConnString(NpdsDbcsReq.ScribeRegistrar);

    // permitted database connection strings
    AcmsDbconstr = ParseAppDbConnString(NpdsDbcsPer.AcmsDbserver);
    BridgeDbconstr = ParseAppDbConnString(NpdsDbcsPer.BridgeDbserver);
    VocabDbconstr = ParseAppDbConnString(NpdsDbcsPer.VocabDbserver);
    CacheDbconstr = ParseAppDbConnString(NpdsDbcsPer.CacheDbserver);
  }

  public string CiaamDbconstr { get; init; }
  public string CoreDbconstr { get; init; }
  public string NexusDbconstr { get; init; }
  public string PortalDbconstr { get; init; }
  public string DoorsDbconstr { get; init; }
  public string ScribeDbconstr { get; init; }

  public string AcmsDbconstr { get; init; }
  public string BridgeDbconstr { get; init; }
  public string VocabDbconstr { get; init; }
  public string CacheDbconstr { get; init; }

  // cache for tag-guid conversion
  public PdpTagGuidDictionary NpdsServiceCache { set; get; } = new PdpTagGuidDictionary();
  // TODO: create method to refresh NpdsServiceCache independent of controller chain
  public PdpDropDownLists NpdsDdlists { set; get; } = new PdpDropDownLists();
  // TODO: create method to refresh NpdsDdlists independent of controller chain
  public string UtcNowYear { set; get; } = DateTime.UtcNow.Year.ToString();

  // diristry/registry/directory/registrar defaults with tags and guids

  public string ServiceTagDefault { get; set; }

  public string DiristryTagDefault { get; set; }
  public Guid DiristryGuidDefault
  { get { return NpdsServiceCache.GetByTag(DiristryTagDefault); } }

  public string RegistryTagDefault { get; set; }
  public Guid RegistryGuidDefault
  { get { return NpdsServiceCache.GetByTag(RegistryTagDefault); } }

  public string DirectoryTagDefault { get; set; }
  public Guid DirectoryGuidDefault
  { get { return NpdsServiceCache.GetByTag(DirectoryTagDefault); } }

  public string RegistrarTagDefault { get; set; }
  public Guid RegistrarGuidDefault
  { get { return NpdsServiceCache.GetByTag(RegistrarTagDefault); } }

  // diristry/registry/directory/registrar constraints with tags only

  public string DiristryTagConstraint { get; set; }
  public string RegistryTagConstraint { get; set; }
  public string DirectoryTagConstraint { get; set; }
  public string RegistrarTagConstraint { get; set; }


  public void DebugNpdsGuids(Guid diristryGuid, Guid registryGuid, Guid directoryGuid, Guid registrarGuid)
  {
    var diristryTag = NpdsServiceCache.GetByGuid(diristryGuid);
    var registryTag = NpdsServiceCache.GetByGuid(registryGuid);
    var directoryTag = NpdsServiceCache.GetByGuid(directoryGuid);
    var registrarTag = NpdsServiceCache.GetByGuid(registrarGuid);
    Debug.WriteLine($"Nexus: {diristryTag} = {diristryGuid}");
    Debug.WriteLine($"PORTAL: {registryTag} = {registryGuid}");
    Debug.WriteLine($"DOORS: {directoryTag} = {directoryGuid}");
    Debug.WriteLine($"Scribe: {registrarTag} = {registrarGuid};");
  }

} // end class

// end file