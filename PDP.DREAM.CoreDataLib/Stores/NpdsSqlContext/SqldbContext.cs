// SqldbContext.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext : PdpDataContext, INpdsDataService
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    if (!builder.IsConfigured ||
        (!builder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !builder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      builder.UseSqlServer(NpdsServiceDefaults.Values.NpdsCoreDbconstr);
    }
  }
  protected void OnInitiating(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; }
    var builder = new DbContextOptionsBuilder();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiating(DbContextOptions<CoreDbsqlContext> dbco)
  {
    OnCreated();
  }

  // constructor with typed DbContextOptions required for EntityFrameworkCore
  public CoreDbsqlContext(DbContextOptions<CoreDbsqlContext> dbco) : base()
  {
    OnInitiating(dbco);
  }
  public CoreDbsqlContext(string dbcs) : base()
  {
    OnInitiating(dbcs);
  }
  public CoreDbsqlContext() : base()
  {
    OnInitiating();
  }

  public virtual void UsePrcDefaultConnection(ref DbContextOptionsBuilder builder)
  {
    var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsCoreDbconstr; } // for Core service
    if (!string.IsNullOrEmpty(dbcs)) { builder.UseSqlServer(dbcs); }
  }


} // class
