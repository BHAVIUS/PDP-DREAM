using System;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // ATTN: recall different patterns when property is enum vs when property is string
  public partial class PdpRestContext
  {
    // in file NpdsConstantsPrcAis.cs
    // in class PDP.DREAM.NpdsCoreLib.Models.NpdsConstants
    // public enum DatabaseType { Nexus = 1, PORTAL, DOORS };

    // default values

    public NpdsConst.DatabaseType DatabaseTypeDeflt { get; }
      = NpdsServiceDefaults.GetValues.NpdsDefaultDatabaseType;

    // requested values

    public string DatabaseTypeReqst
    {
      set
      {
        reqDatabaseType = value;
        if (!string.IsNullOrEmpty(reqDatabaseType))
        {
          DatabaseType = ValidateDatabaseType(reqDatabaseType);
        }
      }
      get { return reqDatabaseType; }
    }
    private string reqDatabaseType = string.Empty;

    // validated values

    public NpdsConst.DatabaseType DatabaseType
    {
      set { databaseType = ValidateDatabaseType(value); }
      get
      {
        if (databaseType == 0)
        { DatabaseType = DatabaseTypeDeflt; }
        return databaseType;
      }
    }
    private NpdsConst.DatabaseType databaseType = default;

    // validators

    private NpdsConst.DatabaseType ValidateDatabaseType(string strValue)
    {
      NpdsConst.DatabaseType enmValue;
      switch (strValue.ToUpper())
      {
        case "NEXUS":
          enmValue = NpdsConst.DatabaseType.Nexus; break;
        case "PORTAL":
          enmValue = NpdsConst.DatabaseType.PORTAL; break;
        case "DOORS":
          enmValue = NpdsConst.DatabaseType.DOORS; break;
        default:
          throw new Exception("Invalid value for NpdsConstants.DatabaseType");
      }
      return ValidateDatabaseType(enmValue);
    }
    private NpdsConst.DatabaseType ValidateDatabaseType(NpdsConst.DatabaseType value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
