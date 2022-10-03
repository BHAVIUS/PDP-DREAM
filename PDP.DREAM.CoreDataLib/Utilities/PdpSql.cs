// PdpSql.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Data.SqlClient;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class GlobalConstants
{
  public const string ESS = ""; // Empty String Symbol
  public const int NIS = -1; // Null Integer Symbol
  public const int ZIS = 0; // Zero Integer Symbol
}

public static class PdpSql
{
  private static string lstErr;
  public static string LastError
  { get { return lstErr; } }

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

} // end class

// end file