// SqldbContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext : CoreDbsqlContext
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
      optionsBuilder.UseSqlServer(NPDSSD.NpdsDiristryDbconstr);
    }
  }

  protected void OnInitiatingPdpdc(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsDiristryDbconstr; }
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<NexusDbsqlContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(SqlConnection? dbconn)
  {
    if (dbconn == null) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<NexusDbsqlContext>();
    builder.UseSqlServer(dbconn);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiatingPdpdc(DbContextOptions<NexusDbsqlContext> dbco)
  {
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public NexusDbsqlContext(DbContextOptions<NexusDbsqlContext> dbco) : base() 
  {
    OnInitiatingPdpdc(dbco);
  }
  public NexusDbsqlContext(SqlConnection? dbconn) : base()
  {
    OnInitiatingPdpdc(dbconn);
  }
  public NexusDbsqlContext(string dbcs) : base() 
  {
    OnInitiatingPdpdc(dbcs);
  }
  public NexusDbsqlContext() : base() 
  {
    OnInitiatingPdpdc();
  }

  public override void UsePdpdbDefaultConnection(ref DbContextOptionsBuilder builder)
  {
    var dbcs = qebUserRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsDiristryDbconstr; } // for Nexus service
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NPDSSD.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { builder.UseSqlServer(dbcs); }
  }

} // end class

// end file
