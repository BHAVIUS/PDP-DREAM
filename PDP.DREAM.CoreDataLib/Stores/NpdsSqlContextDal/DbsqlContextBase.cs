// PdpDataContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.DbsqlContextBase
public abstract class DbsqlContextBase : DbContext, IDbsqlContext
{

  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    // var dbcs = this.Database.GetDbConnection().ConnectionString; 
    // reference to this.Database not available in OnConfiguring() method
    // if (!builder.IsConfigured) { builder.UseSqlServer(PdpSiteDefaultDbconstr); }
#if DEBUG
    var configured = dbcob.IsConfigured;
    var clientNonNull = (NPDSCP != null);
#endif
    if ((dbcob.IsConfigured) && (NPDSCP != null))
    {
      if (NPDSCP.ClientAppSiaaGuid.IsNullOrEmpty() && !PDPSS.AppSecureUiaaGuid.IsEmpty())
      { NPDSCP.ClientAppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
    }
    else
    {
      throw new Exception("DbContext builder with NPDSCP has not been configured");
    }
  }
  protected DbsqlContextBase() : base() { }
  protected DbsqlContextBase(DbContextOptionsBuilder dbcob) : base() { }
  protected DbsqlContextBase(INpdsClient npdsc) : base()
  {
    NPDSCP = npdsc;
    // if (string.IsNullOrEmpty(NPDSCP.DatabaseConstr)) { NPDSCP.DatabaseConstr = NPDSSD.QebiDbconstr;  }
    var dbcob = new DbContextOptionsBuilder().UseSqlServer(NPDSCP.DatabaseConstr);
    base.OnConfiguring(dbcob);
  }

  //  NPDS Client Properties (NPDSCP)
  protected INpdsClient npdsClient;
  public INpdsClient NPDSCP
  {
    set { npdsClient = value; }
    get { return npdsClient; }
  }

  protected SqlConnection? dbsqlCnctn = null;
  public SqlConnection? DbsqlCnctn
  {
    set { dbsqlCnctn = value; }
    get { return dbsqlCnctn; }
  }
  public string DbsqlErrors { get; set; } = string.Empty;
  public string DbsqlStatus { get; set; } = string.Empty;

  // ATTN: any use of DbsqlConnect should be paired with DbsqlDisconnect
  public SqlConnection? DbsqlConnect()
  {
    try
    {
      dbsqlCnctn = new SqlConnection(NPDSCP.DatabaseConstr);
      if (dbsqlCnctn?.State != ConnectionState.Open) { dbsqlCnctn.Open(); }
      DbsqlStatus = dbsqlCnctn.State.ToString();
      DbsqlErrors = string.Empty;
    }
    catch (SqlException sqlExc)
    {
      DbsqlStatus = dbsqlCnctn.State.ToString();
      DbsqlErrors = QebSql.ParseSqlException(sqlExc);
    }
    return dbsqlCnctn;
  }
  public void DbsqlDisconnect()
  {
    try
    {
      if (dbsqlCnctn == null)
      {
        DbsqlStatus = ConnectionState.Broken.ToString();
      }
      else if (dbsqlCnctn.State != ConnectionState.Closed)
      {
        dbsqlCnctn.Close();
        DbsqlStatus = dbsqlCnctn.State.ToString();
      }
      DbsqlErrors = string.Empty;
    }
    catch (SqlException sqlExc)
    {
      DbsqlStatus = dbsqlCnctn?.State.ToString();
      DbsqlErrors = QebSql.ParseSqlException(sqlExc);
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