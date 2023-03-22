// SqldbcUilSrvcRstrctOrEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //

  public ServiceRestrictionOrEditModel GetEditableRestrictionOrByKey(Guid guidKey)
  { return QueryScribeRestrictionOrByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceRestrictionOrEditModel GetEditableRestrictionOrByKey(string guidKey)
  { return GetEditableRestrictionOrByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public NexusServiceRestrictionOr GetStorableRestrictionOrByKey(Guid guidKey)
  { return QueryScribeRestrictionOrByKey(guidKey).SingleOrDefault(); }
  public NexusServiceRestrictionOr GetStorableRestrictionOrByKey(string guidKey)
  { return GetStorableRestrictionOrByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<NexusServiceRestrictionOr> QueryScribeRestrictionOrByKey(Guid guidKey)
  {
    IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
    qry = qry.Where(r => (r.RestrictionOrGuidKey == guidKey));
    return qry;
  }
 
} // end class

// end file