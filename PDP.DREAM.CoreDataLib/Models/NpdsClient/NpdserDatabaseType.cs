// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqDatabaseType = ESS;
  public string? DatabaseTypeReqst
  {
    set {
      reqDatabaseType = value;
      if (!string.IsNullOrEmpty(reqDatabaseType))
      { databaseType = ValidateDatabaseType(reqDatabaseType); }
    }
    get { return reqDatabaseType; }
  }

  // validated value (non-nullable typed)

  private NpdsDatabaseType databaseType = NPDSCD.DatabaseTypeDefault;
  public NpdsDatabaseType DatabaseType
  {
    set { databaseType = ValidateDatabaseType(value); }
    get {
      if (databaseType == null)
      { databaseType = NPDSCD.DatabaseTypeDefault; }
      return databaseType;
    }
  }

  // validators

  private NpdsDatabaseType ValidateDatabaseType(string recName)
  {
    NpdsDatabaseType recItem = NPDSCD.ParseDatabaseType(recName);
    return recItem;
  }
  private NpdsDatabaseType ValidateDatabaseType(NpdsDatabaseType recItem)
  {
    // TODO: code this virtual implementation with AI rules
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // DatabaseType is focused on structure of backend database
  // DatabaseType determines database connection strings
  // DatabaseType determines DatabaseTypes and ResRepFormats

  public static class DdeDatabaseType
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
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsDatabaseType DatabaseTypeNone =
     new NpdsDatabaseType(0, DdeDatabaseType.None, DdeDatabaseType.None);
  public NpdsDatabaseType DatabaseTypeNexus =
     new NpdsDatabaseType(1, DdeDatabaseType.Nexus, DdeDatabaseType.Nexus);
  public NpdsDatabaseType DatabaseTypePORTAL =
     new NpdsDatabaseType(2, DdeDatabaseType.PORTAL, DdeDatabaseType.PORTAL);
  public NpdsDatabaseType DatabaseTypeDOORS =
      new NpdsDatabaseType(3, DdeDatabaseType.DOORS, DdeDatabaseType.DOORS);
  public NpdsDatabaseType DatabaseTypeScribe =
     new NpdsDatabaseType(4, DdeDatabaseType.Scribe, DdeDatabaseType.Scribe);
  public NpdsDatabaseType DatabaseTypeCore =
     new NpdsDatabaseType(5, DdeDatabaseType.Core, DdeDatabaseType.Core);
  public NpdsDatabaseType DatabaseTypeACMS =
      new NpdsDatabaseType(6, DdeDatabaseType.ACMS, DdeDatabaseType.ACMS);
  public NpdsDatabaseType DatabaseTypeQEBI =
     new NpdsDatabaseType(7, DdeDatabaseType.QEBI, DdeDatabaseType.QEBI);
  public NpdsDatabaseType DatabaseTypeVocab =
     new NpdsDatabaseType(8, DdeDatabaseType.Vocab, DdeDatabaseType.Vocab);
  public NpdsDatabaseType DatabaseTypeCache =
      new NpdsDatabaseType(9, DdeDatabaseType.Cache, DdeDatabaseType.Cache);
  public NpdsDatabaseType DatabaseTypeBridge =
     new NpdsDatabaseType(10, DdeDatabaseType.Bridge, DdeDatabaseType.Bridge);

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

  protected void InitDatabaseTypeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsDatabaseType>
    {
      DatabaseTypeNone,
      DatabaseTypeNexus,
      DatabaseTypePORTAL,
      DatabaseTypeDOORS,
      DatabaseTypeScribe,
      DatabaseTypeCore,
      DatabaseTypeACMS,
      DatabaseTypeQEBI,
      DatabaseTypeVocab,
      DatabaseTypeCache,
      DatabaseTypeBridge,
    };
    DatabaseTypeList = recItms;
    DatabaseTypeNewItem = recItms[0];
    DatabaseTypeDefault = recItms[1];
  }

  //public void ResetDatabaseTypeDefault(string enam)
  //{
  //  try
  //  {
  //    DatabaseTypeDefault = DatabaseTypeList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitDatabaseTypes();
  //  }
  //}

  // nullable lists
  public List<NpdsDatabaseType>? DatabaseTypeList { get; set; } = null;
  public List<string>? DatabaseTypeNames
  {
    get {
      if (DatabaseTypeList == null) { InitDatabaseTypeEnums(); }
      return DatabaseTypeList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsDatabaseType ParseDatabaseType(byte ecod)
  {
    NpdsDatabaseType? recItm = null;
    if (DatabaseTypeList == null) { InitDatabaseTypeEnums(); }
    try
    {
      recItm = DatabaseTypeList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = DatabaseTypeDefault; }
    return recItm;
  }
  public NpdsDatabaseType ParseDatabaseType(string enam)
  {
    NpdsDatabaseType? recItm = null;
    if (DatabaseTypeList == null) { InitDatabaseTypeEnums(); }
    try
    {
      recItm = DatabaseTypeList?
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = DatabaseTypeDefault; }
    return recItm;
  }
  public NpdsDatabaseType DatabaseTypeDefault { get; set; }
  public NpdsDatabaseType DatabaseTypeNewItem { get; set; }

} // end class

// end file