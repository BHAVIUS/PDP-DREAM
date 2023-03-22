// SqldbcUilSupTagViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public SupportingTagViewModel GetViewableSupportingTagByKey(Guid guidKey)
  { return QueryStorableSupportingTagByKey(guidKey).ToViewable().SingleOrDefault(); }
  public SupportingTagViewModel GetViewableSupportingTagByKey(string guidKey)
  { return GetViewableSupportingTagByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusSupportingTag GetStorableSupportingTagByKey(Guid guidKey)
  { return QueryStorableSupportingTagByKey(guidKey).SingleOrDefault(); }
  public NexusSupportingTag GetStorableSupportingTagByKey(string guidKey)
  { return GetStorableSupportingTagByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusSupportingTag> QueryStorableSupportingTagByKey(Guid guidKey)
  {
    IQueryable<NexusSupportingTag> qry = this.NexusSupportingTags;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file