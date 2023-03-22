// SqldbcUilEntLabelViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public EntityLabelViewModel GetViewableEntityLabelByKey(Guid guidKey)
  { return QueryStorableEntityLabelByKey(guidKey).ToViewable().SingleOrDefault(); }
  public EntityLabelViewModel GetViewableEntityLabelByKey(string guidKey)
  { return GetViewableEntityLabelByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusEntityLabel GetStorableEntityLabelByKey(Guid guidKey)
  { return QueryStorableEntityLabelByKey(guidKey).SingleOrDefault(); }
  public NexusEntityLabel GetStorableEntityLabelByKey(string guidKey)
  { return GetStorableEntityLabelByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusEntityLabel> QueryStorableEntityLabelByKey(Guid guidKey)
  {
    IQueryable<NexusEntityLabel> qry = this.NexusEntityLabels;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file