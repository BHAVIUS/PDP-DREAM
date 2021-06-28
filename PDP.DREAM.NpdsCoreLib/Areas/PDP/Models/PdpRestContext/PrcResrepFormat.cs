using System;

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum ServerType { NPDS = 1, Nexus, PORTAL, DOORS, Scribe };

    // default values

    public NpdsConst.ResrepFormat ResrepFormatDeflt { get; }
      = NpdsServiceDefaults.GetValues.NpdsDefaultResrepFormat;

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

    public NpdsConst.ResrepFormat ResrepFormat
    {
      set { resrepFormat = ValidateResrepFormat(value); }
      get
      {
        if (resrepFormat == 0)
        { ResrepFormat = ResrepFormatDeflt; }
        return resrepFormat;
      }
    }
    private NpdsConst.ResrepFormat resrepFormat = default;

    // validators

    private NpdsConst.ResrepFormat ValidateResrepFormat(string strValue)
    {
      NpdsConst.ResrepFormat enmValue = PdpEnum<NpdsConst.ResrepFormat>.Parse(strValue, NpdsConst.ResrepFormat.Core);
      return ValidateResrepFormat(enmValue);
    }
    private NpdsConst.ResrepFormat ValidateResrepFormat(NpdsConst.ResrepFormat value)
    {
      bool doReset = false;
      if (value == 0) { doReset = true; }
      else
      {
        switch (DatabaseType)
        {
          case NpdsConst.DatabaseType.Nexus: // allows Core, PORTAL, DOORS, Nexus
          case NpdsConst.DatabaseType.Scribe: // allows Core, PORTAL, DOORS, Nexus
            // do nothing
            break;
          case NpdsConst.DatabaseType.PORTAL: // allows Core and PORTAL
            if (!(value == NpdsConst.ResrepFormat.Core || value == NpdsConst.ResrepFormat.PORTAL)) { doReset = true; }
            break;
          case NpdsConst.DatabaseType.DOORS: // allows Core and DOORS
            if (!(value == NpdsConst.ResrepFormat.Core || value == NpdsConst.ResrepFormat.DOORS)) { doReset = true; }
            break;
          case NpdsConst.DatabaseType.Core: // allows Core
            if (!(value == NpdsConst.ResrepFormat.Core)) { doReset = true; }
            break;
          default:
            throw new Exception("Invalid value for NpdsConstants.DatabaseType");
        }
      }
      if (doReset) { value = NpdsConst.ResrepFormat.Core; }
      return value;
    }

  }

}
