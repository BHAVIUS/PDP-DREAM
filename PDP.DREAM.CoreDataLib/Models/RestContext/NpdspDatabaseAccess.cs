// PrcDatabaseAccess.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsParameters
{
  // in file NpdsConstants.cs
  // public enum DatabaseAccess { AnonReadOnly, AuthReadOnly, AuthReadWrite };

  // default values

  public PdpAppConst.NpdsDatabaseAccess DatabaseAccessDeflt
  { get { return defDatabaseAccess; } }
  private PdpAppConst.NpdsDatabaseAccess defDatabaseAccess = PdpAppStatus.NPDSSD.NpdsDefaultDatabaseAccess;

  // requested values

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
  private string reqDatabaseAccess = string.Empty;

  // validated values

  public PdpAppConst.NpdsDatabaseAccess DatabaseAccess
  {
    set { databaseAccess = ValidateDatabaseAccess(value); }
    get
    {
      if (databaseAccess == 0)
      { DatabaseAccess = DatabaseAccessDeflt; }
      return databaseAccess;
    }
  }
  private PdpAppConst.NpdsDatabaseAccess databaseAccess = default;

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

