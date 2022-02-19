// PdpDataContext.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Data;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Stores;

public class PdpDataContext : DbContext
{
  public PdpDataContext() { }
  public PdpDataContext(string dbcstr)
  {
    DbConstr = dbcstr;
  }
  public PdpDataContext(int dbidx, string dbnam, string dbcstr)
  {
    DbIndex = dbidx; DbName = dbnam; DbConstr = dbcstr;
  }
  public int DbIndex { get; set; } = 0;
  public string DbName { get; set; } = string.Empty;
  public string DbConstr { get; set; } = string.Empty;

  // TODO: move methods below to PDP Data Context Class
  //  then implement PDC property analogous to PRC property

  protected readonly PdpSiteSettings pdpSitSets = PdpSiteSettings.Values;
  protected readonly NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.Values;

  //  PDP NPDS REST Context = PRC
  public PdpRestContext PRC { get { return pdpRestCntxt; } }
  public void SetRestContext(ref PdpRestContext prc) // set by reference
  { pdpRestCntxt = prc; }
  public void SetRestContext(PdpRestContext prc) // set by value
  { pdpRestCntxt = prc; }
  protected PdpRestContext pdpRestCntxt;

  // PDP NPDS Data Context = PDC

  public virtual void UsePrcDefaultConnection(ref DbContextOptionsBuilder optionsBuilder)
  {
    var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
  }

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

  public void OpenSqlConnection()
  {
    if (pdpDataCnctn == null) { pdpDataCnctn = this.Database.GetDbConnection() as SqlConnection; }
    if (pdpDataCnctn == null) { pdpDataCnctn = new SqlConnection(pdpRestCntxt.DbConnectionString); }
    if (pdpDataCnctn.State != ConnectionState.Open) { pdpDataCnctn.Open(); }
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

  public void CloseSqlConnection()
  {
    if (pdpDataCnctn != null) { pdpDataCnctn.Close(); }
  }

  public string StoreChanges()
  {
    try
    {
      this.SaveChanges();
      return string.Empty;
    }
    catch (Exception exc)
    {
      var inMessage = exc.InnerException.Message;
      return exc.Message + inMessage;
    }
  }

} // class
