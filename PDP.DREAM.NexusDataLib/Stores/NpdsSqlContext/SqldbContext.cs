// SqldbContext.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

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

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext : CoreDbsqlContext
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured ||
        (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      optionsBuilder.UseSqlServer(NpdsServiceDefaults.Values.NpdsDiristryDbconstr);
    }
  }

  protected void OnInitiating(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsDiristryDbconstr; }
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<NexusDbsqlContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiating(DbContextOptions<NexusDbsqlContext> dbco)
  {
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public NexusDbsqlContext(DbContextOptions<NexusDbsqlContext> dbco) : base() 
  {
    OnInitiating(dbco);
  }
  public NexusDbsqlContext(string dbcs) : base() 
  {
    OnInitiating(dbcs);
  }
  public NexusDbsqlContext() : base() 
  {
    OnInitiating();
  }

  public override void UsePrcDefaultConnection(ref DbContextOptionsBuilder builder)
  {
    var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsDiristryDbconstr; } // for Nexus service
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { builder.UseSqlServer(dbcs); }
  }

}
