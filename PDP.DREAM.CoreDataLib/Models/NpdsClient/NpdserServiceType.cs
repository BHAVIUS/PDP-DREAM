// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqServiceType = ESS;
  public string? ServiceTypeReqst
  {
    set {
      reqServiceType = value;
      if (!string.IsNullOrEmpty(reqServiceType))
      { serviceType = ValidateServiceType(reqServiceType); }
    }
    get { return reqServiceType; }
  }

  // validated value (non-nullable typed)

  private NpdsServiceType serviceType = NPDSCD.ServiceTypeDefault;
  public NpdsServiceType ServiceType
  {
    set { serviceType = ValidateServiceType(value); }
    get {
      if (serviceType == null)
      { serviceType = NPDSCD.ServiceTypeDefault; }
      return serviceType;
    }
  }

  // validators

  private NpdsServiceType ValidateServiceType(string recName)
  {
    NpdsServiceType recItem = NPDSCD.ParseServiceType(recName);
    return recItem;
  }
  private NpdsServiceType ValidateServiceType(NpdsServiceType recItem)
  {
    // ATTN: see also ValidateSearchFilter()
    switch (recItem.EName)
    {
      case DdeServiceType.Nexus:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeNexus; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterDiristry; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatNexus; }
        break;
      case DdeServiceType.PORTAL:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypePORTAL; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterRegistry; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatPORTAL; }
        break;
      case DdeServiceType.DOORS:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeDOORS; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterDirectory; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatDOORS; }
        break;
      case DdeServiceType.Scribe:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeScribe; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterRegistrar; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatNexus; }
        break;
      case DdeServiceType.Core:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeCore; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterServices; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatCore; }
        break;
      case DdeServiceType.Cache:
      case DdeServiceType.Vocab:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeNexus; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterDiristry; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatNexus; }
        break;
      case DdeServiceType.ACMS:
      case DdeServiceType.Bridge:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeScribe; }
        if (SearchFilter == NPDSCD.SearchFilterNone)
        { SearchFilter = NPDSCD.SearchFilterDiristry; }
        if (ResrepFormat == NPDSCD.ResrepFormatNone)
        { ResrepFormat = NPDSCD.ResrepFormatNexus; }
        break;
      case DdeServiceType.QEBI:
      case DdeServiceType.Schema:
        if (DatabaseType == NPDSCD.DatabaseTypeNone)
        { DatabaseType = NPDSCD.DatabaseTypeQEBI; }
        SearchFilter = NPDSCD.SearchFilterNone;
        ResrepFormat = NPDSCD.ResrepFormatNone;
        break;
      default:
        throw new Exception($"{nameof(ValidateServiceType)} switch case not implemented for ServiceType {recItem.EName}");
    }
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // ServiceType focused on function of frontend webservices supported by the server
  // ServiceType determines possible ResRepFormats returned for
  //    resource representations in response to requests
  // proper-named service with generic-termed server yields NPDS components with
  // Core root, Nexus diristry, PORTAL registry, DOORS directory, Scribe registrar
  // compare ServiceType to ServerType and DatabaseType
  // TODO: allow for non-database service types, eg,
  //  converter utilities independent of OS file system or DB data system 
  //
  //  search for CCCC records via EntityType 
  //  ie those for NpdsRoot, NpdsRegistrar, NpdsRegistry, NpdsDirectory, NpdsDiristry
  //  ie, those with CodeKey < 40 for component server/services
  //  those with CodeKey = 40 for organization constituents
  //   those with CodeKey = 50 for person constituents

  public static class DdeServiceType
  {
    public const string None = "None"; // 0
    public const string Nexus = "Nexus"; // 1
    public const string PORTAL = "PORTAL"; // 2
    public const string DOORS = "DOORS"; // 3
    public const string Scribe = "Scribe"; // 4
    public const string Core = "Core"; // 5
    public const string ACMS = "ACMS"; // 6
    public const string QEBI = "QEBI"; // 7
    public const string Vocab = "Vocab"; // 8
    public const string Cache = "Cache"; // 9
    public const string Bridge = "Bridge"; // 10
    public const string Schema = "Schema"; // 11
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsServiceType ServiceTypeNone =
     new NpdsServiceType(0, DdeServiceType.None, DdeServiceType.None);
  public NpdsServiceType ServiceTypeNexus =
     new NpdsServiceType(1, DdeServiceType.Nexus, DdeServiceType.Nexus);
  public NpdsServiceType ServiceTypePORTAL =
     new NpdsServiceType(2, DdeServiceType.PORTAL, DdeServiceType.PORTAL);
  public NpdsServiceType ServiceTypeDOORS =
      new NpdsServiceType(3, DdeServiceType.DOORS, DdeServiceType.DOORS);
  public NpdsServiceType ServiceTypeScribe =
     new NpdsServiceType(4, DdeServiceType.Scribe, DdeServiceType.Scribe);
  public NpdsServiceType ServiceTypeCore =
     new NpdsServiceType(5, DdeServiceType.Core, DdeServiceType.Core);
  public NpdsServiceType ServiceTypeACMS =
      new NpdsServiceType(6, DdeServiceType.ACMS, DdeServiceType.ACMS);
  public NpdsServiceType ServiceTypeQEBI =
     new NpdsServiceType(7, DdeServiceType.QEBI, DdeServiceType.QEBI);
  public NpdsServiceType ServiceTypeVocab =
     new NpdsServiceType(8, DdeServiceType.Vocab, DdeServiceType.Vocab);
  public NpdsServiceType ServiceTypeCache =
      new NpdsServiceType(9, DdeServiceType.Cache, DdeServiceType.Cache);
  public NpdsServiceType ServiceTypeBridge =
     new NpdsServiceType(10, DdeServiceType.Bridge, DdeServiceType.Bridge);
  public NpdsServiceType ServiceTypeSchema =
     new NpdsServiceType(11, DdeServiceType.Schema, DdeServiceType.Schema);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitServiceTypeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsServiceType>
    {
      ServiceTypeNone,
      ServiceTypeNexus,
      ServiceTypePORTAL,
      ServiceTypeDOORS,
      ServiceTypeScribe,
      ServiceTypeCore,
      ServiceTypeACMS,
      ServiceTypeQEBI,
      ServiceTypeVocab,
      ServiceTypeCache,
      ServiceTypeBridge,
      ServiceTypeSchema,
    };
    ServiceTypeList = recItms;
    ServiceTypeNewItem = recItms[0];
    ServiceTypeDefault = recItms[1];
  }

  // nullable lists
  public List<NpdsServiceType>? ServiceTypeList { get; set; } = null;
  public List<string>? ServiceTypeNames
  {
    get {
      if (ServiceTypeList == null) { InitServiceTypeEnums(); }
      return ServiceTypeList.Select(itm => itm.EName).ToList<string>();
    }
  }

  // non-nullable items
  public NpdsServiceType ParseServiceType(byte ecod)
  {
    NpdsServiceType? recItm = null;
    if (ServiceTypeList == null) { InitServiceTypeEnums(); }
    try
    {
      recItm = ServiceTypeList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ServiceTypeDefault; }
    return recItm;
  }
  public NpdsServiceType ParseServiceType(string enam)
  {
    NpdsServiceType? recItm = null;
    if (ServiceTypeList == null) { InitServiceTypeEnums(); }
    try
    {
      recItm = ServiceTypeList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ServiceTypeDefault; }
    return recItm;
  }
  public NpdsServiceType ServiceTypeDefault { get; set; }
  public NpdsServiceType ServiceTypeNewItem { get; set; }

} // end class

// end file