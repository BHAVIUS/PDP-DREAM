// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// TODO: refactor/rename/rebuild to align better with
//   pattern used for QebiDalContext and IQebiDalContext
public abstract class PdpDbsqlContextBase : DbContext, INpdsDbsqlContext
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    // var dbcs = this.Database.GetDbConnection().ConnectionString; 
    // reference to this.Database not available in OnConfiguring() method
#if DEBUG
    var configured = dbcob.IsConfigured;
    var clientNonNull = (NPDSDC != null);
#endif
    if ((dbcob.IsConfigured) && (NPDSDC != null))
    {
      if (NPDSDC.CiaamAppGuid.IsNullOrEmpty() && !PDPSS.CiaamAppGuid.IsEmpty())
      { NPDSDC.CiaamAppGuid = PDPSS.CiaamAppGuid; }
    }
    else
    {
      throw new Exception("DbContext builder with NPDSCP has not been configured");
    }
  }

  protected PdpDbsqlContextBase(INpdscwClient cnpds) : base()
  {
    NPDSDC = cnpds;
    InitDbsqlContextOptions();
  }
  protected PdpDbsqlContextBase(NpdsDatabaseType dbType)
  {
    NPDSDC = new NpdsClientWrace(dbType);
    InitDbsqlContextOptions();
  }
  protected PdpDbsqlContextBase(DbContextOptionsBuilder dbcob) : base()
  {
    NPDSDC = new NpdsClientWrace();
    InitDbsqlContextOptions();
  }
  protected void InitDbsqlContextOptions()
  {
    var dbcob = new DbContextOptionsBuilder().UseSqlServer(NPDSDC.DatabaseConstr);
    base.OnConfiguring(dbcob);
    dbsqlDatabase = new DatabaseFacade(this);
  }

  // NPDS Database Context (NPDSDC) client
  protected INpdscwClient npdsdcClient;
  public INpdscwClient NPDSDC
  {
    set { npdsdcClient = value; }
    get { return npdsdcClient; }
  }

  public DbContext DbsqlContext { get { return this; } }

  // TODO: need unit test lib for all features of DbsqlDatabase
  protected DatabaseFacade dbsqlDatabase;
  public DatabaseFacade DbsqlDatabase
  { get { return dbsqlDatabase; } }

  protected SqlConnection? dbsqlCnctn = null;
  public SqlConnection? DbsqlCnctn
  {
    set { dbsqlCnctn = value; }
    get { return dbsqlCnctn; }
  }
  public bool IsNullCnctn { get { return (dbsqlCnctn == null); } }
  public string DbsqlErrors { get; set; } = ESS;
  public string DbsqlStatus { get; set; } = ESS;

  // ATTN: use of DbsqlConnect should be paired with DbsqlDisconnect
  public SqlConnection? DbsqlConnect()
  {
    try
    {
      dbsqlCnctn = new SqlConnection(NPDSDC.DatabaseConstr);
      if (dbsqlCnctn?.State != ConnectionState.Open) { dbsqlCnctn.Open(); }
      DbsqlStatus = dbsqlCnctn.State.ToString();
      DbsqlErrors = ESS;
    }
    catch (SqlException sqlExc)
    {
      DbsqlStatus = dbsqlCnctn.State.ToString();
      DbsqlErrors = QebSqlLinq.ParseSqlException(sqlExc);
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
      DbsqlErrors = ESS;
    }
    catch (SqlException sqlExc)
    {
      DbsqlStatus = dbsqlCnctn?.State.ToString();
      DbsqlErrors = QebSqlLinq.ParseSqlException(sqlExc);
    }
    finally
    {
      if (dbsqlCnctn != null)
      {
        dbsqlCnctn.Dispose();
        dbsqlCnctn = null;
      }
    }
  }

  public string StoreChanges()
  {
    try
    {
      this.SaveChanges();
      return ESS;
    }
    catch (SqlException exc)
    {
      var inMessage = exc.InnerException.Message;
      return exc.Message + inMessage;
    }
  }

} // end class

// end file