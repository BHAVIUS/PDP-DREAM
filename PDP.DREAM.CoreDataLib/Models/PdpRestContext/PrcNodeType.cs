// PrcNodeType.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  // ATTN: recall different patterns when property is enum vs when property is string
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum NodeType { Authoritative = 1, Caching, Forwarding }

    // default values

    public NpdsConst.NodeType NodeTypeDeflt
    { get { return defNodeType; } }
    private NpdsConst.NodeType defNodeType = NpdsServiceDefaults.Values.NpdsDefaultNodeType;

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

    public NpdsConst.NodeType NodeType
    {
      set { nodeType = ValidateNodeType(value); }
      get
      {
        if (nodeType == 0)
        { NodeType = NodeTypeDeflt; }
        return nodeType;
      }
    }
    private NpdsConst.NodeType nodeType = default;

    // validators

    private NpdsConst.NodeType ValidateNodeType(string strValue)
    {
      NpdsConst.NodeType enmValue = PdpEnum<NpdsConst.NodeType>.Parse(strValue, NpdsConst.NodeType.Authoritative);
      return ValidateNodeType(enmValue);
    }
    private NpdsConst.NodeType ValidateNodeType(NpdsConst.NodeType value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
