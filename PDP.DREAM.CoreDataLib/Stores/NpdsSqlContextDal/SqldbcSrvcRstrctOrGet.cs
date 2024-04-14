// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //

  public ServiceRestrictionOrUxm GetEditableRestrictionOrByKey(Guid guidKey)
  { return QueryScribeRestrictionOrByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceRestrictionOrUxm GetEditableRestrictionOrByKey(string guidKey)
  { return GetEditableRestrictionOrByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public CoreServiceRestrictionOr GetStorableRestrictionOrByKey(Guid guidKey)
  { return QueryScribeRestrictionOrByKey(guidKey).SingleOrDefault(); }
  public CoreServiceRestrictionOr GetStorableRestrictionOrByKey(string guidKey)
  { return GetStorableRestrictionOrByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<CoreServiceRestrictionOr> QueryScribeRestrictionOrByKey(Guid guidKey)
  {
    IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
    qry = qry.Where(r => (r.RestrictionOrGuidKey == guidKey));
    return qry;
  }

} // end class

// end file