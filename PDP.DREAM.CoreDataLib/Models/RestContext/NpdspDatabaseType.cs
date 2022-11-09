// PrcDatabaseType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsParameters
{
  // in file NpdsConstantsPrcAis.cs
  // in class PDP.DREAM.CoreDataLib.Models.NpdsConstants
  // public enum DatabaseType { Nexus = 1, PORTAL, DOORS };

  // default values

  public PdpAppConst.NpdsDatabaseType DatabaseTypeDeflt { get; }
    = PdpAppStatus.NPDSSD.NpdsDefaultDatabaseType;

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

  public PdpAppConst.NpdsDatabaseType DatabaseType
  {
    set { databaseType = ValidateDatabaseType(value); }
    get
    {
      if (databaseType == 0)
      { DatabaseType = DatabaseTypeDeflt; }
      return databaseType;
    }
  }
  private PdpAppConst.NpdsDatabaseType databaseType = default;

  // validators

  private PdpAppConst.NpdsDatabaseType ValidateDatabaseType(string strValue)
  {
    PdpAppConst.NpdsDatabaseType enmValue;
    switch (strValue.ToUpper())
    {
      case "NEXUS":
        enmValue = PdpAppConst.NpdsDatabaseType.Nexus; break;
      case "PORTAL":
        enmValue = PdpAppConst.NpdsDatabaseType.PORTAL; break;
      case "DOORS":
        enmValue = PdpAppConst.NpdsDatabaseType.DOORS; break;
      default:
        throw new Exception("Invalid value for NpdsConstants.DatabaseType");
    }
    return ValidateDatabaseType(enmValue);
  }
  private PdpAppConst.NpdsDatabaseType ValidateDatabaseType(PdpAppConst.NpdsDatabaseType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

