// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public FairMetricUxm GetEditableFairMetricByKey(Guid guidKey)
  { return QueryStorableFairMetricByKey(guidKey).ToEditable().SingleOrDefault(); }
  public FairMetricUxm GetEditableFairMetricByKey(string guidKey)
  { return GetEditableFairMetricByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsFairMetric GetStorableFairMetricByKey(Guid guidKey)
  { return QueryStorableFairMetricByKey(guidKey).SingleOrDefault(); }
  public DoorsFairMetric GetStorableFairMetricByKey(string guidKey)
  { return GetStorableFairMetricByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsFairMetric> QueryStorableFairMetricByKey(Guid guidKey)
  {
    IQueryable<DoorsFairMetric> qry = this.DoorsFairMetrics;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file