// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqEntityType = ESS;
  public string? EntityTypeReqst
  {
    set {
      reqEntityType = value;
      if (!string.IsNullOrEmpty(reqEntityType))
      { entityType = ValidateEntityType(reqEntityType); }
    }
    get { return reqEntityType; }
  }

  // validated value (non-nullable typed)

  private NpdsEntityType entityType = NPDSCD.EntityTypeDefault;
  public NpdsEntityType EntityType
  {
    set { entityType = ValidateEntityType(value); }
    get {
      if (entityType == null)
      { entityType = NPDSCD.EntityTypeDefault; }
      return entityType;
    }
  }

  // validators

  private NpdsEntityType ValidateEntityType(string recName)
  {
    NpdsEntityType recItem = NPDSCD.ParseEntityType(recName);
    return recItem;
  }
  private NpdsEntityType ValidateEntityType(NpdsEntityType recItem)
  {
    // TODO: code AI rules
    return recItem;

  } // end method

}  // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  public static class DdeEntityType
  {
    public const string None = "None"; // 0
    public const string Unknown = "Unknown "; // 99
    public const string AnyAndAll = "AnyAndAll"; // 100

    // generic terms (odds true, evens false)

    public const string NpdsRoot = "NpdsRoot"; // 1
    public const string NpdsRegistrar = "NpdsRegistrar"; // 2
    public const string NpdsRegistry = "NpdsRegistry"; // 3
    public const string NpdsDirectory = "NpdsDirectory"; // 4
    public const string NpdsDiristry = "NpdsDiristry"; // 5
    public const string PortalRoot = "PortalRoot"; // 10
    public const string PortalPrimary = "PortalPrimary"; // 11
    public const string PortalSecondary = "PortalSecondary"; // 12
    public const string DoorsRoot = "DoorsRoot"; // 20
    public const string DoorsPrimary = "DoorsPrimary"; // 21
    public const string DoorsSecondary = "DoorsSecondary"; // 22
    public const string NexusRoot = "NexusRoot"; // 30
    public const string NexusPrimary = "NexusPrimary"; // 31
    public const string NexusSecondary = "NexusSecondary"; // 32
    public const string Organization = "Organization"; // 40
    public const string Person = "Person"; // 50
    public const string OfflineRealEntity = "OfflineRealEntity"; // 60
    public const string PhysicalObject = "PhysicalObject"; // 61
    public const string ChemicalSubstance = "ChemicalSubstance"; // 62
    public const string BiologicalBeing = "BiologicalBeing"; // 63
    public const string GeographicLocation = "GeographicLocation"; // 64
    public const string OnlineVirtualEntity = "OnlineVirtualEntity"; // 70
    public const string TerminologyItem = "TerminologyItem"; // 71
    public const string TaxonomyItem = "TaxonomyItem"; // 72
    public const string ThesaurusItem = "ThesaurusItem"; // 73
    public const string Ontology = "Ontology"; // 74
    public const string Example = "Example"; // 75
    public const string Principle = "Principle"; // 76
    public const string DataRecord = "DataRecord"; // 77
    public const string ComputingTool = "ComputingTool"; // 78
    public const string ComputingService = "ComputingService"; // 79
    public const string Publication = "Publication"; // 80
    public const string AudioItem = "AudioItem"; // 81
    public const string ImageItem = "ImageItem";  // 82
    public const string VideoItem = "VideoItem"; // 83
    public const string MultiMediaItem = "MultiMediaItem"; // 84 
    public const string OneDimSequence = "OneDimSequence"; // 90
    public const string MultiDimSignal = "MultiDimSignal"; // 91
    public const string MetaResourceEntity = "MetaResourceEntity"; // 93

  } // end class

} // end class

public partial class NpdsClientDefaults
{
  public NpdsEntityType EntityTypeNone =
     new NpdsEntityType(0, DdeEntityType.None, DdeEntityType.None);
  public NpdsEntityType EntityTypeNpdsRoot =
     new NpdsEntityType(1, DdeEntityType.NpdsRoot, DdeEntityType.NpdsRoot);
  public NpdsEntityType EntityTypeNpdsRegistrar =
     new NpdsEntityType(2, DdeEntityType.NpdsRegistrar, DdeEntityType.NpdsRegistrar);
  public NpdsEntityType EntityTypeNpdsRegistry =
      new NpdsEntityType(3, DdeEntityType.NpdsRegistry, DdeEntityType.NpdsRegistry);
  public NpdsEntityType EntityTypeNpdsDirectory =
     new NpdsEntityType(4, DdeEntityType.NpdsDirectory, DdeEntityType.NpdsDirectory);
  public NpdsEntityType EntityTypeNpdsDiristry =
     new NpdsEntityType(5, DdeEntityType.NpdsDiristry, DdeEntityType.NpdsDiristry);
  public NpdsEntityType EntityTypeOrganization =
      new NpdsEntityType(40, DdeEntityType.Organization, DdeEntityType.Organization);
  public NpdsEntityType EntityTypePerson =
     new NpdsEntityType(50, DdeEntityType.Person, DdeEntityType.Person);
  public NpdsEntityType EntityTypeOfflineRealEntity =
     new NpdsEntityType(60, DdeEntityType.OfflineRealEntity, DdeEntityType.OfflineRealEntity);
  public NpdsEntityType EntityTypePhysicalObject =
      new NpdsEntityType(61, DdeEntityType.PhysicalObject, DdeEntityType.PhysicalObject);
  public NpdsEntityType EntityTypeChemicalSubstance =
     new NpdsEntityType(62, DdeEntityType.ChemicalSubstance, DdeEntityType.ChemicalSubstance);
  public NpdsEntityType EntityTypeBiologicalBeing =
     new NpdsEntityType(63, DdeEntityType.BiologicalBeing, DdeEntityType.BiologicalBeing);
  public NpdsEntityType EntityTypeGeographicLocation =
     new NpdsEntityType(64, DdeEntityType.GeographicLocation, DdeEntityType.GeographicLocation);
  public NpdsEntityType EntityTypeOnlineVirtualEntity =
     new NpdsEntityType(70, DdeEntityType.OnlineVirtualEntity, DdeEntityType.OnlineVirtualEntity);
  public NpdsEntityType EntityTypeTerminologyItem =
     new NpdsEntityType(71, DdeEntityType.TerminologyItem, DdeEntityType.TerminologyItem);
  public NpdsEntityType EntityTypeTaxonomyItem =
      new NpdsEntityType(72, DdeEntityType.TaxonomyItem, DdeEntityType.TaxonomyItem);
  public NpdsEntityType EntityTypeThesaurusItem =
     new NpdsEntityType(73, DdeEntityType.ThesaurusItem, DdeEntityType.ThesaurusItem);
  public NpdsEntityType EntityTypeOntology =
     new NpdsEntityType(74, DdeEntityType.Ontology, DdeEntityType.Ontology);
  public NpdsEntityType EntityTypeExample =
      new NpdsEntityType(75, DdeEntityType.Example, DdeEntityType.Example);
  public NpdsEntityType EntityTypePrinciple =
     new NpdsEntityType(76, DdeEntityType.Principle, DdeEntityType.Principle);
  public NpdsEntityType EntityTypeDataRecord =
     new NpdsEntityType(77, DdeEntityType.DataRecord, DdeEntityType.DataRecord);
  public NpdsEntityType EntityTypeComputingTool =
      new NpdsEntityType(78, DdeEntityType.ComputingTool, DdeEntityType.ComputingTool);
  public NpdsEntityType EntityTypeComputingService =
     new NpdsEntityType(79, DdeEntityType.ComputingService, DdeEntityType.ComputingService);
  public NpdsEntityType EntityTypePublication =
     new NpdsEntityType(80, DdeEntityType.Publication, DdeEntityType.Publication);
  public NpdsEntityType EntityTypeAudioItem =
     new NpdsEntityType(81, DdeEntityType.AudioItem, DdeEntityType.AudioItem);
  public NpdsEntityType EntityTypeImageItem =
     new NpdsEntityType(82, DdeEntityType.ImageItem, DdeEntityType.ImageItem);
  public NpdsEntityType EntityTypeVideoItem =
     new NpdsEntityType(83, DdeEntityType.VideoItem, DdeEntityType.VideoItem);
  public NpdsEntityType EntityTypeMultiMediaItem =
      new NpdsEntityType(84, DdeEntityType.MultiMediaItem, DdeEntityType.MultiMediaItem);
  public NpdsEntityType EntityTypeOneDimSequence =
     new NpdsEntityType(90, DdeEntityType.OneDimSequence, DdeEntityType.OneDimSequence);
  public NpdsEntityType EntityTypeMultiDimSignal =
     new NpdsEntityType(91, DdeEntityType.MultiDimSignal, DdeEntityType.MultiDimSignal);
  public NpdsEntityType EntityTypeMetaResourceEntity =
      new NpdsEntityType(93, DdeEntityType.MetaResourceEntity, DdeEntityType.MetaResourceEntity);
  public NpdsEntityType EntityTypeUnknown =
     new NpdsEntityType(99, DdeEntityType.Unknown, DdeEntityType.Unknown);
  public NpdsEntityType EntityTypeAnyAndAll =
     new NpdsEntityType(100, DdeEntityType.AnyAndAll, DdeEntityType.AnyAndAll);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitEntityTypeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsEntityType>
    {
      EntityTypeNone, EntityTypeAnyAndAll, EntityTypeUnknown,

      EntityTypeNpdsRoot,
      EntityTypeNpdsRegistrar, EntityTypeNpdsRegistry,
      EntityTypeNpdsDirectory, EntityTypeNpdsDiristry,
      EntityTypeOrganization, EntityTypePerson,
      EntityTypeOfflineRealEntity, EntityTypePhysicalObject, EntityTypeChemicalSubstance,
      EntityTypeBiologicalBeing, EntityTypeGeographicLocation, EntityTypeOnlineVirtualEntity,
      EntityTypeTerminologyItem, EntityTypeTaxonomyItem, EntityTypeThesaurusItem,
      EntityTypeOntology, EntityTypeExample, EntityTypePrinciple,
      EntityTypeDataRecord, EntityTypeComputingTool, EntityTypeComputingService,
      EntityTypePublication, EntityTypeAudioItem, EntityTypeImageItem, EntityTypeVideoItem,
      EntityTypeMultiMediaItem, EntityTypeOneDimSequence, EntityTypeMultiDimSignal,
      EntityTypeMetaResourceEntity,
    };
    EntityTypeList = recItms;
    EntityTypeNewItem = recItms[0];
    EntityTypeDefault = recItms[1];
  }

  //public void ResetEntityTypeDefault(string enam)
  //{
  //  try
  //  {
  //    EntityTypeDefault = EntityTypeList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitEntityTypes();
  //  }
  //}

  // nullable lists
  public List<NpdsEntityType>? EntityTypeList { get; set; } = null;
  public List<string>? EntityTypeNames
  {
    get {
      if (EntityTypeList == null) { InitEntityTypeEnums(); }
      return EntityTypeList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsEntityType ParseEntityType(byte ecod)
  {
    NpdsEntityType? recItm = null;
    if (EntityTypeList == null) { InitEntityTypeEnums(); }
    try
    {
      recItm = EntityTypeList?
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = EntityTypeDefault; }
    return recItm;
  }
  public NpdsEntityType ParseEntityType(string enam)
  {
    NpdsEntityType? recItm = null;
    if (EntityTypeList == null) { InitEntityTypeEnums(); }
    try
    {
      recItm = EntityTypeList?
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = EntityTypeDefault; }
    return recItm;
  }
  public NpdsEntityType EntityTypeDefault { get; set; }
  public NpdsEntityType EntityTypeNewItem { get; set; }

} // end class

// end file