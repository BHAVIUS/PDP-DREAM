// SqldbContext.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext : PdpDataContext, INpdsDataService
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured ||
        (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      optionsBuilder.UseSqlServer(NpdsServiceDefaults.Values.NpdsCoreDbconstr);
    }
  }

  protected void OnInitiating(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; }
    if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty"); }
    var builder = new DbContextOptionsBuilder<CoreDbsqlContext>();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiating(DbContextOptions<CoreDbsqlContext> dbco)
  {
    var builder = new DbContextOptionsBuilder(dbco);
    OnConfiguring(builder);
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public CoreDbsqlContext(DbContextOptions<CoreDbsqlContext> dbco) : base() { OnInitiating(dbco); }
  public CoreDbsqlContext(string dbcs) : base() { OnInitiating(dbcs); }
  public CoreDbsqlContext() : base() { OnInitiating(); }

  public override void UsePrcDefaultConnection(ref DbContextOptionsBuilder optionsBuilder)
  {
    var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
  }

}
