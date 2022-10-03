// SqldbcUilFairMetricViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public FairMetricViewModel GetViewableFairMetricByKey(Guid guidKey)
  { return QueryStorableFairMetricByKey(guidKey).ToViewable().SingleOrDefault(); }
  public FairMetricViewModel GetViewableFairMetricByKey(string guidKey)
  { return GetViewableFairMetricByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusFairMetric GetStorableFairMetricByKey(Guid guidKey)
  { return QueryStorableFairMetricByKey(guidKey).SingleOrDefault(); }
  public NexusFairMetric GetStorableFairMetricByKey(string guidKey)
  { return GetStorableFairMetricByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusFairMetric> QueryStorableFairMetricByKey(Guid guidKey)
  {
    IQueryable<NexusFairMetric> qry = this.NexusFairMetrics;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file