// PrcDatabaseAccess.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // ATTN: recall different patterns when property is enum vs when property is string
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum DatabaseAccess { AnonReadOnly, AuthReadOnly, AuthReadWrite };

    // default values

    public NpdsConst.DatabaseAccess DatabaseAccessDeflt
    { get { return defDatabaseAccess; } }
    private NpdsConst.DatabaseAccess defDatabaseAccess = NpdsServiceDefaults.GetValues.NpdsDefaultDatabaseAccess;

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

    public NpdsConst.DatabaseAccess DatabaseAccess
    {
      set { databaseAccess = ValidateDatabaseAccess(value); }
      get
      {
        if (databaseAccess == 0)
        { DatabaseAccess = DatabaseAccessDeflt; }
        return databaseAccess;
      }
    }
    private NpdsConst.DatabaseAccess databaseAccess = default;

    // validators

    private NpdsConst.DatabaseAccess ValidateDatabaseAccess(string strValue)
    {
      NpdsConst.DatabaseAccess enmValue = PdpEnum<NpdsConst.DatabaseAccess>.Parse(strValue, NpdsConst.DatabaseAccess.AnonReadOnly);
      return ValidateDatabaseAccess(enmValue);
    }
    private NpdsConst.DatabaseAccess ValidateDatabaseAccess(NpdsConst.DatabaseAccess value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
