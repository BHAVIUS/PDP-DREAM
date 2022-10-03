// PrcServerType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // in file NpdsConstantsPrcAis.cs
  // public enum ServerType { Root = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 4 };

  // default values

  public PdpAppConst.NpdsServerType ServerTypeDeflt { get; }
   = PdpAppStatus.NPDSSD.NpdsDefaultServerType;

  // requested values

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
  private string reqServerType = string.Empty;

  // validated values

  public PdpAppConst.NpdsServerType ServerType
  {
    set { serverType = ValidateServerType(value); }
    get
    {
      if (serverType == 0)
      { ServerType = ServerTypeDeflt; }
      return serverType;
    }
  }
  private PdpAppConst.NpdsServerType serverType = default;

  // validators

  private PdpAppConst.NpdsServerType ValidateServerType(string strValue)
  {
    PdpAppConst.NpdsServerType enmValue = PdpEnum<PdpAppConst.NpdsServerType>.ParseString(strValue, PdpAppConst.NpdsServerType.Diristry);
    return ValidateServerType(enmValue);
  }
  private PdpAppConst.NpdsServerType ValidateServerType(PdpAppConst.NpdsServerType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

