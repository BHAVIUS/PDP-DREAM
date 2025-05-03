// SqldbContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Net.Http;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext : NexusDbsqlContext
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
      optionsBuilder.UseSqlServer(NPDSSD.NpdsRegistrarDbconstr);
    }
  }
  // ATTN: default connection method for database only in absence of HttpContext
  //  cannot use an HttpRequest dependent dynamic selection here which instead 
  //  must be refactored to a page/view/request dependent reset of the DB connection
  protected void OnInitiatingPdpdc(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsRegistrarDbconstr; } // for Scribe service
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<ScribeDbsqlContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(SqlConnection? dbconn)
  {
    if (dbconn == null) { throw new NullReferenceException("dbconn null or empty"); }
    var builder = new DbContextOptionsBuilder<ScribeDbsqlContext>();
    builder.UseSqlServer(dbconn);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(DbContextOptions<ScribeDbsqlContext> dbco)
  {
    if (dbco == null) { throw new NullReferenceException("dbco null or empty"); }
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public ScribeDbsqlContext(DbContextOptions<ScribeDbsqlContext> dbco) : base()
  {
    OnInitiatingPdpdc(dbco);
    bingMaps = new BingMapsService(new HttpClient());
  }
  // constructor with typed Microsoft.Data.SqlClient.SqlConnection
  public ScribeDbsqlContext(SqlConnection? dbconn) : base()
  {
    OnInitiatingPdpdc(dbconn);
    bingMaps = new BingMapsService(new HttpClient());
  }
  // constructor with string intended for the database connection
  public ScribeDbsqlContext(string dbcs) : base()
  {
    OnInitiatingPdpdc(dbcs);
    bingMaps = new BingMapsService(new HttpClient());
  }
  // constructor without any parameter
  public ScribeDbsqlContext() : base()
  {
    OnInitiatingPdpdc("");
    bingMaps = new BingMapsService(new HttpClient());
  }

  protected readonly BingMapsService? bingMaps;

} // end class

// end file