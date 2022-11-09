// PdpDataContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public class PdpDataContext : DbContext
{
  protected readonly IHttpContextAccessor hcAccessor;

  // PDP Data Context = PDPDC

  // TODO: build data contexts without http context
  // the data context should require injection of any dependencies
  // from http context without being directly dependent on the http context
  // so there should be an independent mediator for any required parameters
  // then can use with either desktop app or web app
  // so QURC needs to be split and refactored

  public PdpDataContext()
  {
    hcAccessor = new HttpContextAccessor();
    PdpdcInitialize();
  }
  public PdpDataContext(IHttpContextAccessor hca)
  {
    hcAccessor = hca;
    PdpdcInitialize();
  }
  public PdpDataContext(QebUserRestContext qurc, IHttpContextAccessor hca)
  {
    hcAccessor = hca;
    PdpdcInitialize(qurc);
  }

  protected virtual void PdpdcInitialize(QebUserRestContext? qurc = null)
  {
    if (qurc != null)
    {
      qebUserRestCntxt = qurc;
    }
    else if (PdcSqlSiteContext != null)
    {
      qebUserRestCntxt = new QebUserRestContext(PdcSqlSiteContext);
    }
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
  public QebUserRestContext QURC
  {
    set {
      value.CatchNullObject(nameof(qebUserRestCntxt), nameof(QURC), nameof(PdpDataContext));
      qebUserRestCntxt = value;
    }
    get {
      if ((qebUserRestCntxt == null) && (PdcSqlSiteContext != null))
      {
        qebUserRestCntxt = new QebUserRestContext(PdcSqlSiteContext);
      }
      qebUserRestCntxt.CatchNullObject(nameof(qebUserRestCntxt), nameof(QURC), nameof(PdpDataContext));
      return qebUserRestCntxt;
    }
  }
  // TODO: update to allow for request-dependent connection string
  //  check for change of the dbcs and update the connection if different
  // TODO: an alternative would be to deprecate this void method
  //  and replace with a new constructor that takes the QURC
  // thereby always creating a new instance and
  // eliminating the old vs new connection string comparison
  public void ResetQebiContext(QebUserRestContext qurc)
  {
    qebUserRestCntxt = qurc;
    var dbcs = qebUserRestCntxt.DbConnectionString;
    if (!string.Equals(dbcs, pdpDataCnstr, StringComparison.OrdinalIgnoreCase))
    {
      pdpDataCnstr = dbcs;
      CloseSqlConnection(); // close current connection
      OpenSqlConnection(); // re-open new connection
    }
  }

  public HttpContext PdcSqlSiteContext
  {
    get { return hcAccessor?.HttpContext; }
  }
  public HttpRequest PdcSqlSiteRequest
  {
    get { return hcAccessor?.HttpContext?.Request; }
  }
  public HttpResponse PdcSqlSiteResponse
  {
    get { return hcAccessor?.HttpContext?.Response; }
  }

  public string PdcSqlcstr // PDC SQL Connection String
  { get { return pdpDataCnstr; } }
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
      if (pdpDataCnctn == null) { pdpDataCnctn = new SqlConnection(pdpDataCnstr); }
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
    QebSql.AddRetVal(ref pdpDataCmmnd);
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