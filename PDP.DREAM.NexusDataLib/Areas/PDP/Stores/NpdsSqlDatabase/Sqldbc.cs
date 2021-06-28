using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Xml.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Utilities;

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
