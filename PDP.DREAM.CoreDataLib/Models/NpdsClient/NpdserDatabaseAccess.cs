// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqDatabaseAccess = ESS;
  public string? DatabaseAccessReqst
  {
    set {
      reqDatabaseAccess = value;
      if (!string.IsNullOrEmpty(reqDatabaseAccess))
      { databaseAccess = ValidateDatabaseAccess(reqDatabaseAccess); }
    }
    get { return reqDatabaseAccess; }
  }

  // validated value (non-nullable typed)

  private NpdsDatabaseAccess databaseAccess = NPDSCD.DatabaseAccessDefault;
  public NpdsDatabaseAccess DatabaseAccess
  {
    set { databaseAccess = ValidateDatabaseAccess(value); }
    get {
      if (databaseAccess == null)
      { databaseAccess = NPDSCD.DatabaseAccessDefault; }
      return databaseAccess;
    }
  }

  // validators

  private NpdsDatabaseAccess ValidateDatabaseAccess(string strName)
  {
    NpdsDatabaseAccess databaseAccess = NPDSCD.ParseDatabaseAccess(strName);
    return databaseAccess;
  }
  private NpdsDatabaseAccess ValidateDatabaseAccess(NpdsDatabaseAccess recItem)
  {
    // TODO: code this virtual implementation with AI rules
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // DatabaseAccess determines service-based read/write access to entire database
  // Anonymous versus Authenticated and ReadOnly versus ReadWrite
  // higher privileges do not exclude lower privileges,
  public static class DdeDatabaseAccess
  {
    public const string None = "None"; // 0
    public const string AnonReadOnly = "AnonReadOnly"; // 1
    public const string AuthReadOnly = "AuthReadOnly"; // 2
    public const string AuthReadWrite = "AuthReadWrite"; // 3
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsDatabaseAccess DatabaseAccessNone =
     new NpdsDatabaseAccess(0, DdeDatabaseAccess.None, DdeDatabaseAccess.None);
  public NpdsDatabaseAccess DatabaseAccessAnonReadOnly =
     new NpdsDatabaseAccess(1, DdeDatabaseAccess.AnonReadOnly, DdeDatabaseAccess.AnonReadOnly);
  public NpdsDatabaseAccess DatabaseAccessAuthReadOnly =
     new NpdsDatabaseAccess(2, DdeDatabaseAccess.AuthReadOnly, DdeDatabaseAccess.AuthReadOnly);
  public NpdsDatabaseAccess DatabaseAccessAuthReadWrite =
      new NpdsDatabaseAccess(3, DdeDatabaseAccess.AuthReadWrite, DdeDatabaseAccess.AuthReadWrite);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitDatabaseAccessEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsDatabaseAccess>
    {
      DatabaseAccessNone,
      DatabaseAccessAnonReadOnly,
      DatabaseAccessAuthReadOnly,
      DatabaseAccessAuthReadWrite,
    };
    DatabaseAccessList = recItms;
    DatabaseAccessNewItem = recItms[0];
    DatabaseAccessDefault = recItms[1];
  }

  //public void ResetDatabaseAccessDefault(string enam)
  //{
  //  try
  //  {
  //    DatabaseAccessDefault = DatabaseAccessList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitDatabaseAccesss();
  //  }
  //}

  // nullable lists
  public List<NpdsDatabaseAccess>? DatabaseAccessList { get; set; } = null;
  public List<string>? DatabaseAccessNames
  {
    get {
      if (DatabaseAccessList == null) { InitDatabaseAccessEnums(); }
      return DatabaseAccessList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsDatabaseAccess ParseDatabaseAccess(byte ecod)
  {
    NpdsDatabaseAccess? recItm = null;
    if (DatabaseAccessList == null) { InitDatabaseAccessEnums(); }
    try
    {
      recItm = DatabaseAccessList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = DatabaseAccessDefault; }
    return recItm;
  }
  public NpdsDatabaseAccess ParseDatabaseAccess(string enam)
  {
    NpdsDatabaseAccess? recItm = null;
    if (DatabaseAccessList == null) { InitDatabaseAccessEnums(); }
    try
    {
      recItm = DatabaseAccessList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = DatabaseAccessDefault; }
    return recItm;
  }
  public NpdsDatabaseAccess DatabaseAccessDefault { get; set; }
  public NpdsDatabaseAccess DatabaseAccessNewItem { get; set; }

} // end class

// end file