﻿// PrcServerType.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstantsPrcAis.cs
    // public enum ServerType { Root = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 4 };

    // default values

    public NpdsConst.ServerType ServerTypeDeflt { get; }
     = NpdsServiceDefaults.GetValues.NpdsDefaultServerType;

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
