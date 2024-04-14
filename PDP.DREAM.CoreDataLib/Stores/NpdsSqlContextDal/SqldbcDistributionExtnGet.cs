// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public DistributionExtnUxm GetEditableDistributionExtnByKey(Guid guidKey)
  { return QueryStorableDistributionExtnByKey(guidKey).ToEditable().SingleOrDefault(); }
  public DistributionExtnUxm GetEditableDistributionExtnByKey(string guidKey)
  { return GetEditableDistributionExtnByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsDistributionExtn GetStorableDistributionExtnByKey(Guid guidKey)
  { return QueryStorableDistributionExtnByKey(guidKey).SingleOrDefault(); }
  public DoorsDistributionExtn GetStorableDistributionExtnByKey(string guidKey)
  { return GetStorableDistributionExtnByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsDistributionExtn> QueryStorableDistributionExtnByKey(Guid guidKey)
  {
    IQueryable<DoorsDistributionExtn> qry = this.DoorsDistributionExtns;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file