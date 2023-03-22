// PrcNodeType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsClient
{
  // requested values

  private string reqNodeType = string.Empty;
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

  // validated values

  private PdpAppConst.NpdsNodeType nodeType = default;
  public PdpAppConst.NpdsNodeType NodeType
  {
    set { nodeType = ValidateNodeType(value); }
    get
    {
      if (nodeType == 0)
      { NodeType = NPDSSD.NodeTypeDefault; }
      return nodeType;
    }
  }

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

