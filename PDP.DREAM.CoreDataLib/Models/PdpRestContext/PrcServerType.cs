// PrcServerType.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstantsPrcAis.cs
    // public enum ServerType { Root = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 4 };

    // default values

    public NpdsConst.ServerType ServerTypeDeflt { get; }
     = NpdsServiceDefaults.Values.NpdsDefaultServerType;

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

    public NpdsConst.ServerType ServerType
    {
      set { serverType = ValidateServerType(value); }
      get
      {
        if (serverType == 0)
        { ServerType = ServerTypeDeflt; }
        return serverType;
      }
    }
    private NpdsConst.ServerType serverType = default;

    // validators

    private NpdsConst.ServerType ValidateServerType(string strValue)
    {
      NpdsConst.ServerType enmValue = PdpEnum<NpdsConst.ServerType>.Parse(strValue, NpdsConst.ServerType.Diristry);
      return ValidateServerType(enmValue);
    }
    private NpdsConst.ServerType ValidateServerType(NpdsConst.ServerType value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
