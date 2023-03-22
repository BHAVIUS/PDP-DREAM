// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class GlobalConstants
{
  public const string ESS = ""; // Empty String Symbol
  public const int NIS = -1; // Null Integer Symbol
  public const int ZIS = 0; // Zero Integer Symbol
}

public static class QebSql
{
  public static string ParseSqlDbcString(string dbcs)
  {
    // SQL Server Database Connection String
    // parse via Microsoft.Data.SqlClient.SqlConnectionStringBuilder
    var strBldr = new SqlConnectionStringBuilder(dbcs);
    // TODO: address these hard-coded properties for new SqlClient in NET 7
    // ATTN: values are hardcoded in C# for now in NET 6
    // TODO: address security issues on SQL Connection strings
    strBldr.Encrypt = false;
    strBldr.IntegratedSecurity = true;
    strBldr.MultipleActiveResultSets = true;
    strBldr.TrustServerCertificate = true;
    dbcs = (strBldr.ToString() ?? string.Empty);
    return dbcs;
  }

  private static string lstErr;
  public static string LastError
  { get { return lstErr; } }

  public static SqlCommand? OpenSqlCommand(string? namStorProc, SqlConnection? dbsqlCnctn)
  {
    if (string.IsNullOrEmpty(namStorProc) || (dbsqlCnctn == null))
    { throw new NoNullAllowedException(); }
    var dbsqlCmmnd = new SqlCommand(namStorProc, dbsqlCnctn)
    { CommandType = CommandType.StoredProcedure };
    // add int return value to the command
    QebSql.AddRetVal(ref dbsqlCmmnd);
    return dbsqlCmmnd;
  }

  public static void AddParameter(ref SqlCommand sqlCom, SqlDbType parTyp, int parSiz, string parNam, object? parVal)
  {
    var sqlPar = new SqlParameter(parNam, parTyp, parSiz);
    sqlPar.Direction = ParameterDirection.Input;
    if (parVal != null) { sqlPar.Value = parVal; }
    sqlCom.Parameters.Add(sqlPar);
  }

  public static void AddParameter(ref SqlCommand sqlCom, SqlDbType parTyp, int parSiz, string parNam, ParameterDirection parDir, object? parVal = null)
  {
    var sqlPar = new SqlParameter(parNam, parTyp, parSiz);
    sqlPar.Direction = parDir;
    if (parVal != null) { sqlPar.Value = parVal; }
    sqlCom.Parameters.Add(sqlPar);
  }

  public static void AddRetVal(ref SqlCommand sqlCom)
  {
    AddParameter(ref sqlCom, SqlDbType.Int, 4, "RETURN_VALUE", ParameterDirection.ReturnValue);
  }
  public static int GetRetVal(ref SqlCommand sqlCom)
  {
    return GetInt(ref sqlCom, "RETURN_VALUE");
  }

  public static int ExecuteCommand(ref SqlCommand sqlCom)
  {
    sqlCom.ExecuteNonQuery();
    return GetInt(ref sqlCom, "RETURN_VALUE");
  }
  public static int ExecuteCommand(ref SqlCommand sqlCom, ref DataTable sqlDT)
  {
    SqlDataAdapter sqlDA = null;
    sqlDA = new SqlDataAdapter(sqlCom);
    return sqlDA.Fill(sqlDT);
  }

  public static void CloseReader(ref SqlDataReader sqlRdr)
  {
    if (sqlRdr != null)
    {
      if (!(sqlRdr.IsClosed)) { sqlRdr.Close(); }
    }
  }

  public static Guid GetGuid(ref SqlCommand sqlCom, string strFld)
  {
    Guid theGuid = new Guid();
    try
    {
      theGuid = new Guid(sqlCom.Parameters[strFld].Value.ToString());
    }
    catch (Exception exc)
    {
      lstErr = exc.ToString();
      theGuid = Guid.Empty;
    }
    return theGuid;
  }
  public static SqlGuid GetSqlGuid(ref SqlCommand sqlCom, string strFld)
  {
    SqlGuid sqlUid = new SqlGuid();
    try
    {
      sqlUid = (Guid)(sqlCom.Parameters[strFld].Value);
    }
    catch (FormatException exc)
    {
      lstErr = exc.ToString();
      sqlUid = SqlGuid.Null;
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      sqlUid = SqlGuid.Null;
    }
    return sqlUid;
  }

  //***************************************************************************************************************************

  public static SqlDateTime GetDateTime(ref SqlDataReader sqlRdr, int intOrd)
  {
    SqlDateTime sqlDtm = new SqlDateTime();
    if (sqlRdr.IsDBNull(intOrd))
    {
      sqlDtm = SqlDateTime.Null;
    }
    else
    {
      sqlDtm = sqlRdr.GetSqlDateTime(intOrd);
    }
    return sqlDtm;
  }
  public static SqlDateTime GetDateTime(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetDateTime(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static SqlDateTime GetDateTime(ref SqlCommand sqlCom, string strFld)
  {
    SqlDateTime sqlDtm = new SqlDateTime();
    try
    {
      sqlDtm = Convert.ToDateTime(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      sqlDtm = SqlDateTime.Null;
    }
    return sqlDtm;
  }

  public static string GetChar(ref SqlDataReader sqlRdr, int intOrd)
  {
    string strVal = null;
    if (sqlRdr.IsDBNull(intOrd))
    {
      strVal = "";
    }
    else
    {
      strVal = sqlRdr.GetString(intOrd).Trim(' ');
    }
    return strVal;
  }
  public static string GetChar(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetChar(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static string GetChar(ref SqlCommand sqlCom, string strFld)
  {
    string strVal = null;
    try
    {
      strVal = Convert.ToString(sqlCom.Parameters[strFld].Value).Trim(' ');
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      strVal = "";
    }
    return strVal;
  }
  public static string GetChar(ref DataRow sqlRow, int intOrd)
  {
    string strVal = null;
    try
    {
      strVal = Convert.ToString(sqlRow[intOrd]).Trim(' ');
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      strVal = "";
    }
    return strVal;
  }

  public static decimal GetMoney(ref SqlDataReader sqlRdr, int intOrd)
  {
    decimal decVal = 0M;
    if (sqlRdr.IsDBNull(intOrd))
    {
      decVal = 0M;
    }
    else
    {
      decVal = sqlRdr.GetDecimal(intOrd);
    }
    return decVal;
  }
  public static decimal GetMoney(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetMoney(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static decimal GetMoney(ref SqlCommand sqlCom, string strFld)
  {
    decimal decVal = 0M;
    try
    {
      decVal = Convert.ToDecimal(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      decVal = 0M;
    }
    return decVal;
  }
  public static decimal GetMoney(ref DataRow sqlRow, int intOrd)
  {
    decimal decVal = 0M;
    try
    {
      decVal = Convert.ToDecimal(sqlRow[intOrd]);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      decVal = 0M;
    }
    return decVal;
  }

  public static int GetInt(ref SqlDataReader sqlRdr, int intOrd)
  {
    int intVal = 0;
    if (sqlRdr.IsDBNull(intOrd))
    {
      intVal = GlobalConstants.NIS;
    }
    else
    {
      intVal = sqlRdr.GetInt32(intOrd);
    }
    return intVal;
  }
  public static int GetInt(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetInt(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static int GetInt(ref SqlCommand sqlCom, string strFld)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }
  public static int GetInt(ref DataRow sqlRow, int intOrd)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlRow[intOrd]);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }

  public static int GetSmallInt(ref SqlDataReader sqlRdr, int intOrd)
  {
    int intVal = 0;
    if (sqlRdr.IsDBNull(intOrd))
    {
      intVal = GlobalConstants.NIS;
    }
    else
    {
      intVal = sqlRdr.GetInt16(intOrd);
    }
    return intVal;
  }
  public static int GetSmallInt(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetSmallInt(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static int GetSmallInt(ref SqlCommand sqlCom, string strFld)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }
  public static int GetSmallInt(ref DataRow sqlRow, int intOrd)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlRow[intOrd]);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }

  public static int GetTinyInt(ref SqlDataReader sqlRdr, int intOrd)
  {
    int intVal = 0;
    if (sqlRdr.IsDBNull(intOrd))
    {
      intVal = GlobalConstants.NIS;
    }
    else
    {
      intVal = sqlRdr.GetByte(intOrd);
    }
    return intVal;
  }
  public static int GetTinyInt(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetTinyInt(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static int GetTinyInt(ref SqlCommand sqlCom, string strFld)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }
  public static int GetTinyInt(ref DataRow sqlRow, int intOrd)
  {
    int intVal = 0;
    try
    {
      intVal = Convert.ToInt32(sqlRow[intOrd]);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      intVal = GlobalConstants.NIS;
    }
    return intVal;
  }

  public static bool GetBit(ref SqlDataReader sqlRdr, int intOrd)
  {
    bool bitVal = false;
    if (sqlRdr.IsDBNull(intOrd))
    {
      bitVal = false;
    }
    else
    {
      bitVal = sqlRdr.GetBoolean(intOrd);
    }
    return bitVal;
  }
  public static bool GetBit(ref SqlDataReader sqlRdr, string strFld)
  {
    return GetBit(ref sqlRdr, sqlRdr.GetOrdinal(strFld));
  }
  public static bool GetBit(ref SqlCommand sqlCom, string strFld)
  {
    bool bitVal = false;
    try
    {
      bitVal = Convert.ToBoolean(sqlCom.Parameters[strFld].Value);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      bitVal = false;
    }
    return bitVal;
  }
  public static bool GetBit(ref DataRow sqlRow, int intOrd)
  {
    bool bitVal = false;
    try
    {
      bitVal = Convert.ToBoolean(sqlRow[intOrd]);
    }
    catch (InvalidCastException exc)
    {
      lstErr = exc.ToString();
      bitVal = false;
    }
    return bitVal;
  }

  public static IList<string> SplitSqlScript(string sqlScript, string rgxSplit = "")
  {
    // Split by "GO" statements
    if (string.IsNullOrEmpty(rgxSplit)) { rgxSplit = @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$"; }
    var optSplit = (RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
    var sqlCmnds = Regex.Split(sqlScript, rgxSplit, optSplit).ToList();
    var numRemoved = sqlCmnds.RemoveAll(sql => string.IsNullOrWhiteSpace(sql));
    return sqlCmnds;
  }

  public static string ExecuteNonQuerySqlScript(string dbConstr, string sqlScript, bool doSplit = false, string rgxSplit = "")
  {
    IList<string> sqlTexts;
    if (doSplit == true) { sqlTexts = SplitSqlScript(sqlScript, rgxSplit); }
    else { sqlTexts = new[] { sqlScript }; }
    return ExecuteNonQuerySqlScript(dbConstr, sqlTexts);
  }

  public static string ExecuteNonQuerySqlScript(string dbConstr, IList<string> sqlTexts)
  {
    string errorMessage = string.Empty;
    try
    {
      using (var sqlCnctn = new SqlConnection(dbConstr))
      {
        if (sqlCnctn.State != ConnectionState.Open) { sqlCnctn.Open(); }
        using (var sqlCmmnd = new SqlCommand())
        {
          sqlCmmnd.Connection = sqlCnctn;
          foreach (var sqlTxt in sqlTexts)
          {
            if (!string.IsNullOrEmpty(sqlTxt))
            {
              sqlCmmnd.CommandText = sqlTxt;
              sqlCmmnd.ExecuteNonQuery();
            }
          }
        }
        sqlCnctn.Close();
      }
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    return errorMessage;
  }

  public static string ParseSqlException(SqlException sqlExc)
  {
    var sbErrors = new StringBuilder("");
    foreach (SqlError sqlErr in sqlExc.Errors)
    {
      sbErrors.AppendLine(sqlErr.ToString());
      sbErrors.AppendLine(string.Format("Class: {0}; Error: {1}; Line: {2}.<br>", sqlErr.Class, sqlErr.Number, sqlErr.LineNumber));
      sbErrors.AppendLine(string.Format("Reported by {0} while connected to {1}.</p>", sqlErr.Source, sqlErr.Server));
    }
    return sbErrors.ToString();
  }

  public static SqlConnection? CreateConnection(string? cnstr)
  {
    var cnctn = new SqlConnection(cnstr);
    return cnctn;
  }
  public static SqlConnection? OpenConnection(string? cnstr = null)
  {
    SqlConnection? cnctn = null;
    try
    {
      if (string.IsNullOrEmpty(cnstr)) { cnstr = PdpSiteDefaultDbconstr; }
      if (!string.IsNullOrEmpty(cnstr)) { cnctn = new SqlConnection(cnstr); }
      if (cnctn.State != ConnectionState.Open) { cnctn.Open(); }
    }
    catch (SqlException exc)
    {
      Debug.WriteLine(ParseSqlException(exc));
      cnctn = new SqlConnection();
    }
    return cnctn;
  }
  public static bool CloseConnection(SqlConnection? cnctn)
  {
    bool dbcClosed = false;
    try
    {

      if (cnctn?.State != ConnectionState.Closed)
      { cnctn.Close(); dbcClosed = true; }
    }
    catch (SqlException exc)
    {
      Debug.WriteLine(ParseSqlException(exc));
    }
    return dbcClosed;
  }
  public static bool CheckConnection(string? cnstr)
  {
    var dbcValid = false;
    try
    {
      using (var cnctn = new SqlConnection(cnstr))
      {
        var dbServer = cnctn.DataSource;
        var dbName = cnctn.Database;
        cnctn.Open();
        cnctn.Close();
      }
      dbcValid = true;
    }
    catch (SqlException ex)
    {
      Debug.WriteLine($"{ex.Number} = {ex.Message}\n");
      throw;
    }
    return dbcValid;
  }


  // ATTN: do not use
  // TODO: solve problem of triggering the call to OnConfiguring
  public static string? ContextCnstr(this DbContext? cntxt)
  {
    // ATTN: when type is DbContext
    // calling Database facade triggers call to OnConfiguring method
    // TODO: how to avoid this problem??? change type to DbsqlContextBase ???
    // still calls the OnConfiguring method!!!
    return cntxt.Database.GetConnectionString(); 
  }
  // TODO: solve problem of triggering the call to OnConfiguring
  public static SqlConnection? ContextCnctn(this DbContext? cntxt)
  {
    // ATTN: when type is DbContext
    // calling Database facade triggers call to OnConfiguring method
    // TODO: how to avoid this problem??? change type to DbsqlContextBase ???
    // still calls the OnConfiguring method!!!
    return (SqlConnection?)cntxt.Database.GetDbConnection(); 
  }

  public static bool ContextHasSchema(this DbContext? cntxt)
  {
    var dbHasSchema = false;
    if (cntxt != null)
    {
      if (!cntxt.Database.CanConnect()) { return dbHasSchema; }
      var dbsqlCnctn = cntxt.Database.GetDbConnection();
      if (dbsqlCnctn == null) { return dbHasSchema; }
      if (dbsqlCnctn.State != ConnectionState.Open) { dbsqlCnctn.Open(); }
      var schemaDataTable = dbsqlCnctn.GetSchema();
      if (schemaDataTable != null) { dbHasSchema = true; }
      if (dbsqlCnctn.State != ConnectionState.Closed) { dbsqlCnctn.Close(); }
    }
    return dbHasSchema;
  }
  public static bool ContextHasChanges(this DbContext? cntxt)
  {
    bool hasChanges = false;
    if (cntxt != null)
    {
      hasChanges = cntxt.ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
    }
    return hasChanges;
  }


} // end class

// end file