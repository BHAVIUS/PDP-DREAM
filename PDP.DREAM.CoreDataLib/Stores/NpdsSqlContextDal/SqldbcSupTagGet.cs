// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public SupportingTagUxm GetEditableSupportingTagByKey(Guid guidKey)
  { return QueryStorableSupportingTagByKey(guidKey).ToEditable().SingleOrDefault(); }
  public SupportingTagUxm GetEditableSupportingTagByKey(string guidKey)
  { return GetEditableSupportingTagByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public PortalSupportingTag GetStorableSupportingTagByKey(Guid guidKey)
  { return QueryStorableSupportingTagByKey(guidKey).SingleOrDefault(); }
  public PortalSupportingTag GetStorableSupportingTagByKey(string guidKey)
  { return GetStorableSupportingTagByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<PortalSupportingTag> QueryStorableSupportingTagByKey(Guid guidKey)
  {
    IQueryable<PortalSupportingTag> qry = this.PortalSupportingTags;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file