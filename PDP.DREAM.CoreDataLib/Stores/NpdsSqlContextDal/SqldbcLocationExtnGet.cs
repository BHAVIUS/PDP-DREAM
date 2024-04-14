// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public LocationExtnUxm? GetEditableLocationExtnByKey(Guid guidKey)
  { return QueryStorableLocationExtnByKey(guidKey).ToEditable().SingleOrDefault(); }
  public LocationExtnUxm? GetEditableLocationExtnByKey(string guidKey)
  { return GetEditableLocationExtnByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsLocationExtn GetStorableLocationExtnByKey(Guid guidKey)
  {
    DoorsLocationExtn dto;
    try { dto = QueryStorableLocationExtnByKey(guidKey).SingleOrDefault(); }
    catch { dto = new DoorsLocationExtn(); }
    return dto;
  }
  public DoorsLocationExtn GetStorableLocationExtnByKey(string guidKey)
  { return GetStorableLocationExtnByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsLocationExtn> QueryStorableLocationExtnByKey(Guid guidKey)
  {
    IQueryable<DoorsLocationExtn> qry = this.DoorsLocationExtns;
    qry = qry.Where(ss => (ss.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file