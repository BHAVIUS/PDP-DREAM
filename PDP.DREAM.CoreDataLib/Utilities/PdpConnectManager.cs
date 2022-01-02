// PdpConnectManager.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Data;
using System.Text;

using Microsoft.Data.SqlClient;

namespace PDP.DREAM.CoreDataLib.Utilities;

public class PdpConnectManager
{
  // term "pipe" and name SqlPipe used for the SqlConnection
  public enum NewStringType : int
  {
    WebConfigNameOnly = 1,
    AppConfigNameOnly = 2,
    ConnectionString = 3
  }

  // SqlConnection string
  private string strPipe = "";
  public string SqlPipeString
  {
    get {
      return strPipe;
    }
    set {
      strPipe = ParseConnectionString(value);
    }
  }

  private bool isEncrypted = false;
  public bool IsEncrypted
  {
    get { return isEncrypted; }
  }

  // SqlConnection object
  private SqlConnection? sqlPipe;
  public SqlConnection? SqlPipe
  {
    get { return sqlPipe; }
  }

  private string sqlStatus = "";
  public string SqlPipeStatus
  {
    get { return sqlStatus; }
  }

  private string sqlErrors = "";
  public string SqlPipeErrors
  {
    get { return sqlErrors; }
  }

  // TODO: recode for new configuration in AspNetCore
  public PdpConnectManager(string strIn, NewStringType strInTyp = NewStringType.WebConfigNameOnly)
  {
    // do not allow default null or empty string for strIn
    if (string.IsNullOrEmpty(strIn))
    {
      throw new ArgumentNullException("Error with null/empty strIn for PdpConnectManager");
    }
    try
    {
      switch (strInTyp)
      {
        case NewStringType.WebConfigNameOnly:
          strPipe = ParseConnectionString(PdpConfigManager.ParseAppDbConnString(strIn));
          break;
        case NewStringType.AppConfigNameOnly:
          strPipe = ParseConnectionString(PdpConfigManager.ParseAppStringSetting(strIn));
          break;
        case NewStringType.ConnectionString:
          strPipe = ParseConnectionString(strIn);
          break;
      }
    }
    catch (Exception exc)
    {
      throw new Exception("PdpConnectManager error with connection string", exc);
    }
  }

  public string ParseConnectionString(string value)
  {
    var scsb = new SqlConnectionStringBuilder(value);
    isEncrypted = scsb.Encrypt;
    return scsb.ToString();
  }

  public void Connect()
  {
    if (sqlPipe == null)
    {
      sqlPipe = new SqlConnection(strPipe);
    }
    if (sqlPipe.State == ConnectionState.Closed)
    {
      try
      {
        sqlPipe.Open();
        sqlStatus = "Connection Opened.";
      }
      catch (SqlException sqlExc)
      {
        StringBuilder sbErrors = new StringBuilder("");
        foreach (SqlError sqlErr in sqlExc.Errors)
        {
          sbErrors.AppendLine(sqlErr.ToString());
          sbErrors.AppendLine(string.Format("Class: {0}; Error: {1}; Line: {2}.<br>", sqlErr.Class, sqlErr.Number, sqlErr.LineNumber));
          sbErrors.AppendLine(string.Format("Reported by {0} while connected to {1}.</p>", sqlErr.Source, sqlErr.Server));
        }
        sqlErrors = sbErrors.ToString();
        sqlStatus = "Connection Not Opened.";
      }
    }
    else if (sqlPipe.State != ConnectionState.Open)
    {
      sqlStatus = sqlPipe.State.ToString();
    }
  }

  public void Disconnect()
  {
    if (sqlPipe != null)
    {
      if (!(sqlPipe.State == ConnectionState.Closed))
      {
        sqlPipe.Close();
      }
      //TODO: answer this question re Dispose
      //				sqlCon.Dispose()
      sqlPipe = null;
    }
  }

}
