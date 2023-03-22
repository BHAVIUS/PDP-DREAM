// PrcDatabaseAccess.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsClient
{
  // requested values

  private string reqDatabaseAccess = string.Empty;
  public string DatabaseAccessReqst
  {
    set
    {
      reqDatabaseAccess = value;
      if (!string.IsNullOrEmpty(reqDatabaseAccess))
      {
        DatabaseAccess = ValidateDatabaseAccess(reqDatabaseAccess);
      }
    }
    get { return reqDatabaseAccess; }
  }

  // validated values

  private PdpAppConst.NpdsDatabaseAccess databaseAccess = default;
  public PdpAppConst.NpdsDatabaseAccess DatabaseAccess
  {
    set { databaseAccess = ValidateDatabaseAccess(value); }
    get
    {
      if (databaseAccess == 0)
      { DatabaseAccess = NPDSSD.DatabaseAccessDefault; }
      return databaseAccess;
    }
  }

  // validators

  private PdpAppConst.NpdsDatabaseAccess ValidateDatabaseAccess(string strValue)
  {
    PdpAppConst.NpdsDatabaseAccess enmValue = PdpEnum<PdpAppConst.NpdsDatabaseAccess>.ParseString(strValue, PdpAppConst.NpdsDatabaseAccess.AnonReadOnly);
    return ValidateDatabaseAccess(enmValue);
  }
  private PdpAppConst.NpdsDatabaseAccess ValidateDatabaseAccess(PdpAppConst.NpdsDatabaseAccess value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

