// Sqldbc.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;

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

  // PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase.ScribeDbsqlContext
  public partial class ScribeDbsqlContext : NexusDbsqlContext
  {
    public ScribeDbsqlContext(string dbcs) : base()
    {
      if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty in ScribeDbsqlContext"); }
      var optionsBuilder = new DbContextOptionsBuilder<ScribeDbsqlContext>();
      optionsBuilder.UseSqlServer(dbcs);
      base.OnConfiguring(optionsBuilder);
      OnCreated();
    }

    public override void AddDynamicConnection(ref DbContextOptionsBuilder optionsBuilder)
    {
      var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsRegistrarDbconstr; } // for Scribe service
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
      if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
    }

    public ScribeDbsqlContext(BingMapsService service) { bingMaps = service; }
    private readonly BingMapsService bingMaps;

  } // class

} // namespace
