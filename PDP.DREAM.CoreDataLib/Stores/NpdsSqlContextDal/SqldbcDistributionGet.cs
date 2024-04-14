// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public DistributionUxm GetEditableDistributionByKey(Guid guidKey)
  { return QueryStorableDistributionByKey(guidKey).ToEditable().SingleOrDefault(); }
  public DistributionUxm GetEditableDistributionByKey(string guidKey)
  { return GetEditableDistributionByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsDistribution GetStorableDistributionByKey(Guid guidKey)
  { return QueryStorableDistributionByKey(guidKey).SingleOrDefault(); }
  public DoorsDistribution GetStorableDistributionByKey(string guidKey)
  { return GetStorableDistributionByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsDistribution> QueryStorableDistributionByKey(Guid guidKey)
  {
    IQueryable<DoorsDistribution> qry = this.DoorsDistributions;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file