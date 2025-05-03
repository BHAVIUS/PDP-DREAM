// PrcNodeType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsParameters
{
  // in file NpdsConstants.cs
  // public enum NodeType { Authoritative = 1, Caching, Forwarding }

  // default values

  public PdpAppConst.NpdsNodeType NodeTypeDeflt
  { get { return defNodeType; } }
  private PdpAppConst.NpdsNodeType defNodeType = PdpAppStatus.NPDSSD.NpdsDefaultNodeType;

  // requested values

  public string NodeTypeReqst
  {
    set
    {
      reqNodeType = value;
      if (!string.IsNullOrEmpty(reqNodeType))
      {
        NodeType = ValidateNodeType(reqNodeType);
      }
    }
    get { return reqNodeType; }
  }
  private string reqNodeType = string.Empty;

  // validated values

  public PdpAppConst.NpdsNodeType NodeType
  {
    set { nodeType = ValidateNodeType(value); }
    get
    {
      if (nodeType == 0)
      { NodeType = NodeTypeDeflt; }
      return nodeType;
    }
  }
  private PdpAppConst.NpdsNodeType nodeType = default;

  // validators

  private PdpAppConst.NpdsNodeType ValidateNodeType(string strValue)
  {
    PdpAppConst.NpdsNodeType enmValue = PdpEnum<PdpAppConst.NpdsNodeType>.ParseString(strValue, PdpAppConst.NpdsNodeType.Authoritative);
    return ValidateNodeType(enmValue);
  }
  private PdpAppConst.NpdsNodeType ValidateNodeType(PdpAppConst.NpdsNodeType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

