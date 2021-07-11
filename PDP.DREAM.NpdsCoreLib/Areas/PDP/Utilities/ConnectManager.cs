// ConnectManager.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Data;
using System.Text;

using Microsoft.Data.SqlClient;

#nullable disable

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public class ConnectManager
  {

    public enum NewStringType : int
    {
      WebConfigNameOnly = 1,
      AppConfigNameOnly = 2,
      ConnectionString = 3
    }

    private string strCon = ""; // Connection string
    public string ConnectionString
    {
      get
      {
        return strCon;
      }
      set
      {
        strCon = BuildConnectionString(value);
      }
    }

    private SqlConnection sqlCon; // Connection object
    public SqlConnection SqlPipe
    {
      get
      {
        return sqlCon;
      }
    }

    public string ConnectionStatus
    {
      get
      {
        return conStatus;
      }
    }
    private string conStatus;

    public string ConnectionErrors
    {
      get
      {
        return conErrors;
      }
    }
    private string conErrors;

    // TODO: recode for new configuration in AspNetCore
    public ConnectManager(string strIn, NewStringType strInTyp = NewStringType.WebConfigNameOnly)
    {
      // do not allow default null or empty string for strIn
      if (string.IsNullOrEmpty(strIn))
      {
        throw new Exception("PDP.DREAM.NpdsCoreLib.DataTools.Connection Error with null/empty strIn for ConnectionManager");
      }
      try
      {
        switch (strInTyp)
        {
          case NewStringType.WebConfigNameOnly:
            strCon = BuildConnectionString(ConfigManager.ParseAppDBConnString(strIn));
            break;
          case NewStringType.AppConfigNameOnly:
            strCon = BuildConnectionString(ConfigManager.ParseAppStringSetting(strIn));
            break;
          case NewStringType.ConnectionString:
            strCon = strIn;
            break;
        }
      }
      catch (Exception exc)
      {
        throw new Exception("PDP.DREAM.NpdsCoreLib.DataTools.Connection Error", exc);
      }
    }

    public string BuildConnectionString(string value)
    {
      SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(value);
      //scsb.IntegratedSecurity = True
      //scsb.PersistSecurityInfo = False
      //scsb.MultipleActiveResultSets = True
      //scsb.Pooling = True
      //scsb.Enlist = True
      //scsb.MinPoolSize = 2
      //scsb.MaxPoolSize = 100
      return scsb.ToString();
    }

    public void Connect()
    {
      if (sqlCon == null)
      {
        sqlCon = new SqlConnection(strCon);
      }
      if (sqlCon.State == ConnectionState.Closed)
      {
        try
        {
          sqlCon.Open();
          conStatus = "Connection Opened.";
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
          conErrors = sbErrors.ToString();
          conStatus = "Connection Not Opened.";
        }
      }
      else if (sqlCon.State != ConnectionState.Open)
      {
        conStatus = sqlCon.State.ToString();
      }
    }

    public void Disconnect()
    {
      if (sqlCon != null)
      {
        if (!(sqlCon.State == ConnectionState.Closed))
        {
          sqlCon.Close();
        }
        //TODO: answer this question re Dispose
        //				sqlCon.Dispose()
        sqlCon = null;
      }
    }

  }

}