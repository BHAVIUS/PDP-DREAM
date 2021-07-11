// Sqldbc.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;

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

  // PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase.NexusDbsqlContext
  public partial class NexusDbsqlContext : CoreDbsqlContext
  {
    public NexusDbsqlContext(string dbcs) : base()
    {
      if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty in NexusDbsqlContext"); }
      var optionsBuilder = new DbContextOptionsBuilder<NexusDbsqlContext>();
      optionsBuilder.UseSqlServer(dbcs);
      base.OnConfiguring(optionsBuilder);
      OnCreated();
    }

    public override void AddDynamicConnection(ref DbContextOptionsBuilder optionsBuilder)
    {
      var dbcs = pdpRestCntxt?.DbConnectionString; // assume dynamic selection on each request
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsDiristryDbconstr; } // for Nexus service
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
      if (!string.IsNullOrEmpty(dbcs)) { optionsBuilder.UseSqlServer(dbcs); }
    }

  } // class

} // namespace
