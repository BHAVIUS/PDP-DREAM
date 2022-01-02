// SqldbContext.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext : CoreDbsqlContext
{
  // OnConfiguring method with DbContextOptionsBuilder required for EntityFrameworkCore
  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    if (!builder.IsConfigured ||
        (!builder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
         !builder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
    {
      builder.UseSqlServer(NpdsServiceDefaults.Values.NpdsDiristryDbconstr);
    }
  }
  protected void OnInitiating(string? dbcs = null)
  {
    if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.Values.NpdsDiristryDbconstr; }
    var builder = new DbContextOptionsBuilder();
    builder.UseSqlServer(dbcs);
    OnConfiguring(builder);
    OnCreated();
  }
  protected void OnInitiating(DbContextOptions<NexusDbsqlContext> dbco)
  {
    // TODO: pass dbco to DbContext base, not the Nexus/Core base  ???
    OnCreated();
  }

  // constructor with DbContextOptions required for EntityFrameworkCore
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

} // class
