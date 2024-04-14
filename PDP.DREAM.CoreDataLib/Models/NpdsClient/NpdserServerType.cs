// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqServerType = ESS;
  public string? ServerTypeReqst
  {
    set {
      reqServerType = value;
      if (!string.IsNullOrEmpty(reqServerType))
      { serverType = ValidateServerType(reqServerType); }
    }
    get { return reqServerType; }
  }

  // validated values

  private NpdsServerType serverType = NPDSCD.ServerTypeDefault;
  public NpdsServerType ServerType
  {
    set { serverType = ValidateServerType(value); }
    get {
      if (serverType == null)
      { serverType = NPDSCD.ServerTypeDefault; }
      return serverType;
    }
  }

  // validators

  private NpdsServerType ValidateServerType(string strValue)
  {
    // NpdsServerType enmValue = PdpEnumItemList<NpdsServerType>.ParseString(strValue, NPDSCD.ServerTypeDefault);
    // return ValidateServerType(enmValue);
    var enmValue = NPDSCD.ServerTypeDefault;
    return enmValue;
  }
  private NpdsServerType ValidateServerType(NpdsServerType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // ServerType focused on function of NPDS component as server
  public static class DdeServerType
  {
    public const string None = "None"; // 0
    public const string Diristry = "Diristry"; // 1
    public const string Registry = "Registry"; // 2
    public const string Directory = "Directory"; // 3
    public const string Registrar = "Registrar"; // 4
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsServerType ServerTypeNone =
     new NpdsServerType(0, DdeServerType.None, DdeServerType.None);
  public NpdsServerType ServerTypeDiristry =
     new NpdsServerType(1, DdeServerType.Diristry, DdeServerType.Diristry);
  public NpdsServerType ServerTypeRegistry =
     new NpdsServerType(2, DdeServerType.Registry, DdeServerType.Registry);
  public NpdsServerType ServerTypeDirectory =
      new NpdsServerType(3, DdeServerType.Directory, DdeServerType.Directory);
  public NpdsServerType ServerTypeRegistrar =
     new NpdsServerType(4, DdeServerType.Registrar, DdeServerType.Registrar);
  protected void InitServerTypeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsServerType>
    {
      ServerTypeNone,

      ServerTypeDiristry,
      ServerTypeRegistry,
      ServerTypeDirectory,
      ServerTypeRegistrar,
    };
    ServerTypeList = recItms;
    ServerTypeNewItem = recItms[0];
    ServerTypeDefault = recItms[1];
  }

  //public void ResetServerTypeDefault(string enam)
  //{
  //  try
  //  {
  //    ServerTypeDefault = ServerTypeList?
  //       .Where(itm => itm.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitServerTypes();
  //  }
  //}

  // nullable lists
  public List<NpdsServerType>? ServerTypeList { get; set; } = null;
  public List<string>? ServerTypeNames
  {
    get {
      if (ServerTypeList == null) { InitServerTypeEnums(); }
      return ServerTypeList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsServerType ParseServerType(byte ecod)
  {
    NpdsServerType? recItm = null;
    if (ServerTypeList == null) { InitServerTypeEnums(); }
    try
    {
      recItm = ServerTypeList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ServerTypeDefault; }
    return recItm;
  }
  public NpdsServerType ParseServerType(string enam)
  {
    NpdsServerType? recItm = null;
    if (ServerTypeList == null) { InitServerTypeEnums(); }
    try
    {
      recItm = ServerTypeList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ServerTypeDefault; }
    return recItm;
  }
  public NpdsServerType ServerTypeDefault { get; set; }
  public NpdsServerType ServerTypeNewItem { get; set; }

} // end class

// end file