// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsSchemaLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqNodeType = ESS;
  public string? NodeTypeReqst
  {
    set {
      reqNodeType = value;
      if (!string.IsNullOrEmpty(reqNodeType))
      { nodeType = ValidateNodeType(reqNodeType); }
    }
    get { return reqNodeType; }
  }

  // validated values

  private NpdsNodeType? nodeType = NPDSCD.NodeTypeDefault;
  public NpdsNodeType? NodeType
  {
    set { nodeType = ValidateNodeType(value); }
    get {
      if (nodeType == null)
      { nodeType = NPDSCD.NodeTypeDefault; }
      return nodeType;
    }
  }

  // validators

  private NpdsNodeType ValidateNodeType(string strValue)
  {
    // NpdsNodeType enmValue = PdpEnumItemList<NpdsNodeType>.ParseString(strValue, NpdsNodeType.Authoritative);
    // return ValidateNodeType(enmValue);
    var enmValue = NPDSCD.NodeTypeDefault;
    return enmValue;
  }
  private NpdsNodeType ValidateNodeType(NpdsNodeType enmValue)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return enmValue;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

// TODO: Network NodeType must be reconciled
//   with ServerType and ServiceType

public static partial class PdpAppConst
{
  // Network NodeType for authoritative vs non-authoritative
  public static class DdeNodeType
  {
    public const string None = "None"; // 0
    public const string Forwarding = "Forwarding"; // 1
    public const string Caching = "Caching"; // 2
    public const string Authoritative = "Authoritative"; // 3
  };

}

public partial class NpdsClientDefaults
{
  public NpdsNodeType NodeTypeNone =
    new NpdsNodeType(0, DdeNodeType.None, "Non-authoritative");
  public NpdsNodeType NodeTypeForwarding =
    new NpdsNodeType(1, DdeNodeType.Forwarding, DdeNodeType.Forwarding);
  public NpdsNodeType NodeTypeCaching =
    new NpdsNodeType(2, DdeNodeType.Caching, DdeNodeType.Caching);
  public NpdsNodeType NodeTypeAuthoritative =
    new NpdsNodeType(3, DdeNodeType.Authoritative, DdeNodeType.Authoritative);

  protected void InitNodeTypeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsNodeType>
    {
      NodeTypeNone,

      NodeTypeForwarding, NodeTypeCaching, NodeTypeAuthoritative,
    };
    NodeTypeList = recItms;
    NodeTypeNewItem = recItms[0];
    NodeTypeDefault = recItms[1];
  }

  public List<string>? NodeTypeNames
  {
    get {
      if (NodeTypeList == null) { InitNodeTypeEnums(); }
      return NodeTypeList.Select(itm => itm.EName).ToList<string>();
    }
  }
  public NpdsNodeType? ParseNodeType(string recNam)
  {
    NpdsNodeType? recItm = null;
    if (NodeTypeList == null) { InitNodeTypeEnums(); }
    try
    {
      recItm = NodeTypeList?
         .Where(r => r.EName.ToLower() == recNam.ToLower())
         .FirstOrDefault();
    }
    catch { }
    if (recItm == null) { recItm = NodeTypeDefault; }
    return recItm;
  }

  public List<NpdsNodeType>? NodeTypeList { get; set; } = null;
  public NpdsNodeType? NodeTypeNewItem { get; set; } = null;
  public NpdsNodeType? NodeTypeDefault { get; set; } = null;

} // end class

// end file
