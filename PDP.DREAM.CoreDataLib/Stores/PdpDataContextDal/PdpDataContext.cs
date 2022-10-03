// PdpDataContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Data;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public class PdpDataContext : DbContext
{
  // PDP Data Context = PDPDC
  public PdpDataContext()
  {
    PdpdcInitialize();
  }
  public PdpDataContext(string dbcstr)
  {
    pdpDataCnstr = dbcstr;
    PdpdcInitialize();
  }
  public PdpDataContext(SqlConnection dbconn)
  {
    pdpDataCnctn = dbconn;
    PdpdcInitialize();
  }

  protected virtual void PdpdcInitialize()
  {
    AppSiaaGuid = PdpGuid.ParseToNonNullable(AppSiaaGuid, PDPSS.AppSecureUiaaGuid);
  }
  public Guid AppSiaaGuid { get; set; }
  public bool DataContextHasSchema()
  {
    var dbHasSchema = false;
    if (this.PdcSqlconn == null) { return dbHasSchema; }
    if (!this.Database.CanConnect()) { return dbHasSchema; }
    var schemaDataTable = this.PdcSqlconn.GetSchema();
    if (schemaDataTable != null) { dbHasSchema = true; }
    return dbHasSchema;
  }

  //  QEB User REST Context = QURC 
  protected QebUserRestContext qebUserRestCntxt;
  public QebUserRestContext QURC { get { return qebUserRestCntxt; } }
  public void ResetRestContext(QebUserRestContext qurc)
  { qebUserRestCntxt = qurc; }

  public virtual void UsePdpdbDefaultConnection(ref DbContextOptionsBuilder optionsBuilder)
  {
    var dbcs = qebUserRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
  }

  public string PdcSqlcstr // PDC SQL Connection String
  {     get { return pdpDataCnstr; }  }
  protected string pdpDataCnstr = string.Empty;
  public SqlConnection PdcSqlconn // PDC SQL Data Connection
  {
    get {
      OpenSqlConnection();
      return pdpDataCnctn;
    }
  }
  protected SqlConnection? pdpDataCnctn = null;
  public SqlCommand PdcSqlcomm // PDC SQL Data Command
  { get { return pdpDataCmmnd; } }
  protected SqlCommand? pdpDataCmmnd = null;

  // static methods should have no dependencies on instances
  public static bool CheckConnection(string dbCnstr)
  {
    var dbcValid = false;
    var dbConn = new SqlConnection(dbCnstr);
    try
    {
      var dbServer = dbConn.DataSource;
      var dbName = dbConn.Database;
      dbConn.Open();
      dbConn.Close();
      dbcValid = true;
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"{ex.Number} = {ex.Message}\n");
      throw;
    }
    return dbcValid;
  }
  public static SqlConnection CreateConnection(string dbCnstr)
  {
    var dbConn = new SqlConnection(dbCnstr);
    return dbConn;
  }

  public string OpenSqlConnection()
  {
    try
    {
      if (pdpDataCnctn == null) { pdpDataCnctn = this.Database.GetDbConnection() as SqlConnection; }
      if (pdpDataCnctn == null) { pdpDataCnctn = new SqlConnection(qebUserRestCntxt.DbConnectionString); }
      if (pdpDataCnctn.State != ConnectionState.Open) { pdpDataCnctn.Open(); }
      return string.Empty;
    }
    catch (SqlException exc)
    {
      var inMessage = exc.InnerException.Message;
      return exc.Message + inMessage;
    }
  }

  public void OpenSqlCommand(string namStorProc)
  {
    if (string.IsNullOrEmpty(namStorProc) || (pdpDataCnctn == null)) { throw new NoNullAllowedException(); }
    pdpDataCmmnd = new SqlCommand(namStorProc, pdpDataCnctn)
    {
      CommandType = CommandType.StoredProcedure
    };
    // add int return value to the command
    PdpSql.AddRetVal(ref pdpDataCmmnd);
  }

  public string CloseSqlConnection()
  {
    try
    {
      if (pdpDataCnctn != null) { pdpDataCnctn.Close(); }
      return string.Empty;
    }
    catch (SqlException exc)
    {
      var inMessage = exc.InnerException.Message;
      return exc.Message + inMessage;
    }
  }

  public string StoreChanges()
  {
    try
    {
      this.SaveChanges();
      return string.Empty;
    }
    catch (SqlException exc)
    {
      var inMessage = exc.InnerException.Message;
      return exc.Message + inMessage;
    }
  }

} // end class

// end file