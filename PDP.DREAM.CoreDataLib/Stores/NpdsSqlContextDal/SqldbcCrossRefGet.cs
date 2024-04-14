// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public CrossReferenceUxm GetEditableCrossReferenceByKey(Guid guidKey)
  { return QueryStorableCrossReferenceByKey(guidKey).ToEditable().SingleOrDefault(); }
  public CrossReferenceUxm GetEditableCrossReferenceByKey(string guidKey)
  { return GetEditableCrossReferenceByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public PortalCrossReference GetStorableCrossReferenceByKey(Guid guidKey)
  { return QueryStorableCrossReferenceByKey(guidKey).SingleOrDefault(); }
  public PortalCrossReference GetStorableCrossReferenceByKey(string guidKey)
  { return GetStorableCrossReferenceByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<PortalCrossReference> QueryStorableCrossReferenceByKey(Guid guidKey)
  {
    IQueryable<PortalCrossReference> qry = this.PortalCrossReferences;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file