// PrcResrepFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqResrepFormat = string.Empty;
  public string ResrepFormatReqst
  {
    set {
      reqResrepFormat = value;
      if (!string.IsNullOrEmpty(reqResrepFormat))
      {
        ResrepFormat = ValidateResrepFormat(reqResrepFormat);
      }
    }
    get { return reqResrepFormat; }
  }

  // validated values

  private NpdsResrepFormat resrepFormat = default;
  public NpdsResrepFormat ResrepFormat
  {
    set { resrepFormat = ValidateResrepFormat(value); }
    get {
      if (resrepFormat == 0)
      { ResrepFormat = NPDSSD.ResrepFormatDefault; }
      return resrepFormat;
    }
  }

  // validators

  private NpdsResrepFormat ValidateResrepFormat(string strValue)
  {
    NpdsResrepFormat enmValue = PdpEnum<NpdsResrepFormat>.ParseString(strValue, NpdsResrepFormat.Core);
    return ValidateResrepFormat(enmValue);
  }
  private NpdsResrepFormat ValidateResrepFormat(NpdsResrepFormat value)
  {
    bool doReset = false;
    if (value == 0) { doReset = true; }
    else
    {
      switch (DatabaseType)
      {
        case NpdsDatabaseType.Nexus: // allows Core, PORTAL, DOORS, Nexus
        case NpdsDatabaseType.Scribe: // allows Core, PORTAL, DOORS, Nexus
          // do nothing
          break;
        case NpdsDatabaseType.PORTAL: // allows Core and PORTAL
          if (!(value == NpdsResrepFormat.Core || value == NpdsResrepFormat.PORTAL)) { doReset = true; }
          break;
        case NpdsDatabaseType.DOORS: // allows Core and DOORS
          if (!(value == NpdsResrepFormat.Core || value == NpdsResrepFormat.DOORS)) { doReset = true; }
          break;
        case NpdsDatabaseType.Core: // allows Core
          if (!(value == NpdsResrepFormat.Core)) { doReset = true; }
          break;
        default:
          throw new Exception("Invalid value for NpdsConstants.DatabaseType");
      }
    }
    if (doReset) { value = NpdsResrepFormat.Core; }
    return value;
  }

}  // end class

// end file