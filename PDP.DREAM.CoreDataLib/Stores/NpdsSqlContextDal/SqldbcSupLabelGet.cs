// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public SupportingLabelUxm GetEditableSupportingLabelByKey(Guid guidKey)
  { return QueryStorableSupportingLabelByKey(guidKey).ToEditable().SingleOrDefault(); }
  public SupportingLabelUxm GetEditableSupportingLabelByKey(string guidKey)
  { return GetEditableSupportingLabelByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public PortalSupportingLabel GetStorableSupportingLabelByKey(Guid guidKey)
  { return QueryStorableSupportingLabelByKey(guidKey).SingleOrDefault(); }
  public PortalSupportingLabel GetStorableSupportingLabelByKey(string guidKey)
  { return GetStorableSupportingLabelByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<PortalSupportingLabel> QueryStorableSupportingLabelByKey(Guid guidKey)
  {
    IQueryable<PortalSupportingLabel> qry = this.PortalSupportingLabels;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file