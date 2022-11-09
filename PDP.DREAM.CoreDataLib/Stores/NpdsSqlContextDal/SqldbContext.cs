// SqldbContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext : PdpDataContext, INpdsDataService
{
  // partial void OnCreated for use with Devart Entity Developer generated code
  partial void OnCreated()
  {
    if (base.AppSiaaGuid.IsEmpty()) { base.AppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
  }

  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  // partial void CustomizeConfiguration for use with Devart Entity Developer generated code
  partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured ||
        (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      optionsBuilder.UseSqlServer(NPDSSD.NpdsCoreDbconstr);
    }
  }

  protected void OnInitiatingPdpdc(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsCoreDbconstr; } // for Core service
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<CoreDbsqlContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(SqlConnection? dbconn)
  {
    if (dbconn == null) { throw new NullReferenceException("dbconn null or empty"); }
    var builder = new DbContextOptionsBuilder<CoreDbsqlContext>();
    builder.UseSqlServer(dbconn);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(DbContextOptions<CoreDbsqlContext> dbco)
  {
    if (dbco == null) { throw new NullReferenceException("dbco null or empty"); }
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public CoreDbsqlContext(DbContextOptions<CoreDbsqlContext> dbco) : base()
  {
    OnInitiatingPdpdc(dbco);
  }
  // constructor with typed Microsoft.Data.SqlClient.SqlConnection
  public CoreDbsqlContext(SqlConnection? dbconn) : base()
  {
    OnInitiatingPdpdc(dbconn);
  }
  // constructor with string intended for the database connection
  public CoreDbsqlContext(string dbcs) : base()
  {
    OnInitiatingPdpdc(dbcs);
  }
  // constructor without any parameter
  public CoreDbsqlContext() : base()
  {
    OnInitiatingPdpdc("");
  }
 
} // end class

// end file