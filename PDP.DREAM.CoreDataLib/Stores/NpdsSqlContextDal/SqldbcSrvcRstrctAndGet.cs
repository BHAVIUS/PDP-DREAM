// SqldbcUilSrvcRstrctAndEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //

  public ServiceRestrictionAndUxm GetEditableRestrictionAndByKey(Guid guidKey)
  { return QueryRestrictionAndByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceRestrictionAndUxm GetEditableRestrictionAndByKey(string guidKey)
  { return GetEditableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public CoreServiceRestrictionAnd GetStorableRestrictionAndByKey(Guid guidKey)
  { return QueryRestrictionAndByKey(guidKey).SingleOrDefault(); }
  public CoreServiceRestrictionAnd GetStorableRestrictionAndByKey(string guidKey)
  { return GetStorableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<CoreServiceRestrictionAnd> QueryRestrictionAndByKey(Guid guidKey)
  {
    IQueryable<CoreServiceRestrictionAnd> qry = this.CoreServiceRestrictionAnds;
    qry = qry.Where(r => (r.RestrictionAndGuidKey == guidKey));
    return qry;
  }

} // end class

// end file