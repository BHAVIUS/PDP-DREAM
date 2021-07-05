using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Xml.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Utilities;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      var target = !source.Any(predicate);
      return target;
    }
  }

  // PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase.CoreDbsqlContext
  public partial class CoreDbsqlContext // : INpdsDataService
  {
    public CoreDbsqlContext(string dbcs)
    {
      if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty in CoreDbsqlContext"); }
      var optionsBuilder = new DbContextOptionsBuilder<CoreDbsqlContext>();
      optionsBuilder.UseSqlServer(dbcs);
      base.OnConfiguring(optionsBuilder);
      OnCreated();
    }

    public virtual void AddDynamicConnection(ref DbContextOptionsBuilder optionsBuilder)
    {
      var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
      if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
    }

    protected readonly PdpSiteSettings pdpSitSets = PdpSiteSettings.GetValues;
    protected readonly NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.GetValues;

    //  PDP NPDS REST Context = PRC
    public PdpRestContext PRC { get { return pdpRestCntxt; } }
    public void SetRestContext(ref PdpRestContext prc) { pdpRestCntxt = prc; } // set by reference
    public void SetRestContext(PdpRestContext prc) { pdpRestCntxt = prc; } // set by value
    protected PdpRestContext pdpRestCntxt;

    // PDP NPDS Data Context = PDC
    public SqlConnection PdcSqlconn { get { return pdpDataCnctn; } } // PDC SQL Data Connection
    protected SqlConnection pdpDataCnctn;
    public SqlCommand PdcSqlcomm { get { return pdpDataCmmnd; } }  // PDC SQL Data Command
    protected SqlCommand pdpDataCmmnd;

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
      catch (Exception ex)
      {
        var inMessage = ex.InnerException.Message;
        return ex.Message + inMessage;
      }
    }

  } // class

} // namespace
