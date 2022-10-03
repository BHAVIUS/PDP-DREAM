// PrcResrepFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // in file NpdsConstants.cs
  // public enum ServerType { NPDS = 1, Nexus, PORTAL, DOORS, Scribe };

  // default values

  public PdpAppConst.NpdsResrepFormat ResrepFormatDeflt { get; }
    = PdpAppStatus.NPDSSD.NpdsDefaultResrepFormat;

  // requested values

  public string ResrepFormatReqst
  {
    set
    {
      reqResrepFormat = value;
      if (!string.IsNullOrEmpty(reqResrepFormat))
      {
        ResrepFormat = ValidateResrepFormat(reqResrepFormat);
      }
    }
    get { return reqResrepFormat; }
  }
  private string reqResrepFormat = string.Empty;

  // validated values

  public PdpAppConst.NpdsResrepFormat ResrepFormat
  {
    set { resrepFormat = ValidateResrepFormat(value); }
    get
    {
      if (resrepFormat == 0)
      { ResrepFormat = ResrepFormatDeflt; }
      return resrepFormat;
    }
  }
  private PdpAppConst.NpdsResrepFormat resrepFormat = default;

  // validators

  private PdpAppConst.NpdsResrepFormat ValidateResrepFormat(string strValue)
  {
    PdpAppConst.NpdsResrepFormat enmValue = PdpEnum<PdpAppConst.NpdsResrepFormat>.ParseString(strValue, PdpAppConst.NpdsResrepFormat.Core);
    return ValidateResrepFormat(enmValue);
  }
  private PdpAppConst.NpdsResrepFormat ValidateResrepFormat(PdpAppConst.NpdsResrepFormat value)
  {
    bool doReset = false;
    if (value == 0) { doReset = true; }
    else
    {
      switch (DatabaseType)
      {
        case PdpAppConst.NpdsDatabaseType.Nexus: // allows Core, PORTAL, DOORS, Nexus
        case PdpAppConst.NpdsDatabaseType.Scribe: // allows Core, PORTAL, DOORS, Nexus
          // do nothing
          break;
        case PdpAppConst.NpdsDatabaseType.PORTAL: // allows Core and PORTAL
          if (!(value == PdpAppConst.NpdsResrepFormat.Core || value == PdpAppConst.NpdsResrepFormat.PORTAL)) { doReset = true; }
          break;
        case PdpAppConst.NpdsDatabaseType.DOORS: // allows Core and DOORS
          if (!(value == PdpAppConst.NpdsResrepFormat.Core || value == PdpAppConst.NpdsResrepFormat.DOORS)) { doReset = true; }
          break;
        case PdpAppConst.NpdsDatabaseType.Core: // allows Core
          if (!(value == PdpAppConst.NpdsResrepFormat.Core)) { doReset = true; }
          break;
        default:
          throw new Exception("Invalid value for NpdsConstants.DatabaseType");
      }
    }
    if (doReset) { value = PdpAppConst.NpdsResrepFormat.Core; }
    return value;
  }

}

