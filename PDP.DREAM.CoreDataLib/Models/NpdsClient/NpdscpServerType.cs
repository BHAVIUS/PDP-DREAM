// PrcServerType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqServerType = string.Empty;
  public string ServerTypeReqst
  {
    set
    {
      reqServerType = value;
      if (!string.IsNullOrEmpty(reqServerType))
      {
        ServerType = ValidateServerType(reqServerType);
      }
    }
    get { return reqServerType; }
  }

  // validated values

  private PdpAppConst.NpdsServerType serverType = default;
  public PdpAppConst.NpdsServerType ServerType
  {
    set { serverType = ValidateServerType(value); }
    get
    {
      if (serverType == 0)
      { ServerType = NPDSSD.ServerTypeDefault; }
      return serverType;
    }
  }

  // validators

  private PdpAppConst.NpdsServerType ValidateServerType(string strValue)
  {
    PdpAppConst.NpdsServerType enmValue = PdpEnum<PdpAppConst.NpdsServerType>.ParseString(strValue, NPDSSD.ServerTypeDefault);
    return ValidateServerType(enmValue);
  }
  private PdpAppConst.NpdsServerType ValidateServerType(PdpAppConst.NpdsServerType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

