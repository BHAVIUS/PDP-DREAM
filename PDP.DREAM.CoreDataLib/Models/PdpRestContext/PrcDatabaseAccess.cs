// PrcDatabaseAccess.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  // ATTN: recall different patterns when property is enum vs when property is string
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum DatabaseAccess { AnonReadOnly, AuthReadOnly, AuthReadWrite };

    // default values

    public NpdsConst.DatabaseAccess DatabaseAccessDeflt
    { get { return defDatabaseAccess; } }
    private NpdsConst.DatabaseAccess defDatabaseAccess = NpdsServiceDefaults.Values.NpdsDefaultDatabaseAccess;

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
