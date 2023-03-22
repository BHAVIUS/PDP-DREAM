// SqldbcUilSrvcRstrctAndEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //

  public ServiceRestrictionAndEditModel GetEditableRestrictionAndByKey(Guid guidKey)
  { return QueryRestrictionAndByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceRestrictionAndEditModel GetEditableRestrictionAndByKey(string guidKey)
  { return GetEditableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(Guid guidKey)
  { return QueryRestrictionAndByKey(guidKey).SingleOrDefault(); }
  public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(string guidKey)
  { return GetStorableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<NexusServiceRestrictionAnd> QueryRestrictionAndByKey(Guid guidKey)
  {
    IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
    qry = qry.Where(r => (r.RestrictionAndGuidKey == guidKey));
    return qry;
  }

} // end class

// end file