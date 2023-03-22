// PrcDatabaseType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsClient
{
  // requested values

  private string reqDatabaseType = string.Empty;
  public string DatabaseTypeReqst
  {
    set {
      reqDatabaseType = value;
      if (!string.IsNullOrEmpty(reqDatabaseType))
      {
        DatabaseType = ValidateDatabaseType(reqDatabaseType);
      }
    }
    get { return reqDatabaseType; }
  }

  // validated values

  private NpdsDatabaseType databaseType = default;
  public NpdsDatabaseType DatabaseType
  {
    set { databaseType = ValidateDatabaseType(value); }
    get {
      if (databaseType == 0)
      { DatabaseType = NPDSSD.DatabaseTypeDefault; }
      return databaseType;
    }
  }

  // validators

  private NpdsDatabaseType ValidateDatabaseType(string strValue)
  {
    NpdsDatabaseType enmValue;
    switch (strValue.ToLower())
    {
      case "qebi":
        enmValue = NpdsDatabaseType.SIAA; break;
      case "core":
        enmValue = NpdsDatabaseType.Core; break;
      case "nexus":
        enmValue = NpdsDatabaseType.Nexus; break;
      case "portal":
        enmValue = NpdsDatabaseType.PORTAL; break;
      case "doors":
        enmValue = NpdsDatabaseType.DOORS; break;
      case "scribe":
        enmValue = NpdsDatabaseType.Scribe; break;
      case "acms":
        enmValue = NpdsDatabaseType.ACMS; break;
      case "vocab":
        enmValue = NpdsDatabaseType.Vocab; break;
      case "cache":
      default:
        enmValue = NpdsDatabaseType.Cache; break;
    }
    return ValidateDatabaseType(enmValue);
  }
  private NpdsDatabaseType ValidateDatabaseType(NpdsDatabaseType enmValue)
  {
    // TODO: finish coding this virtual implementation with AI rules for enums
    if (!PdpEnum<NpdsDatabaseType>.Exists(enmValue))
    { enmValue = NpdsDatabaseType.Core; }
    return enmValue;
  }

}
