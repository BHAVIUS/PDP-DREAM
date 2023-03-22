// SqldbcUilDistributionViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public DistributionViewModel GetViewableDistributionByKey(Guid guidKey)
  { return QueryStorableDistributionByKey(guidKey).ToViewable().SingleOrDefault(); }
  public DistributionViewModel GetViewableDistributionByKey(string guidKey)
  { return GetViewableDistributionByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusDistribution GetStorableDistributionByKey(Guid guidKey)
  { return QueryStorableDistributionByKey(guidKey).SingleOrDefault(); }
  public NexusDistribution GetStorableDistributionByKey(string guidKey)
  { return GetStorableDistributionByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusDistribution> QueryStorableDistributionByKey(Guid guidKey)
  {
    IQueryable<NexusDistribution> qry = this.NexusDistributions;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file