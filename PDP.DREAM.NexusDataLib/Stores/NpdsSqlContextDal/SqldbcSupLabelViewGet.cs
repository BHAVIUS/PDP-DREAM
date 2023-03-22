// SqldbcUilSupLabelViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public SupportingLabelViewModel GetViewableSupportingLabelByKey(Guid guidKey)
  { return QueryStorableSupportingLabelByKey(guidKey).ToViewable().SingleOrDefault(); }
  public SupportingLabelViewModel GetViewableSupportingLabelByKey(string guidKey)
  { return GetViewableSupportingLabelByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusSupportingLabel GetStorableSupportingLabelByKey(Guid guidKey)
  { return QueryStorableSupportingLabelByKey(guidKey).SingleOrDefault(); }
  public NexusSupportingLabel GetStorableSupportingLabelByKey(string guidKey)
  { return GetStorableSupportingLabelByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusSupportingLabel> QueryStorableSupportingLabelByKey(Guid guidKey)
  {
    IQueryable<NexusSupportingLabel> qry = this.NexusSupportingLabels;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file