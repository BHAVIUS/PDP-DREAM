// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public LocationUxm? GetEditableLocationByKey(Guid guidKey)
  { return QueryStorableLocationByKey(guidKey).ToEditable().SingleOrDefault(); }
  public LocationUxm? GetEditableLocationByKey(string guidKey)
  { return GetEditableLocationByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsLocation GetStorableLocationByKey(Guid guidKey)
  {
    DoorsLocation dto;
    try { dto = QueryStorableLocationByKey(guidKey).SingleOrDefault(); }
    catch { dto = new DoorsLocation(); }
    return dto;
  }
  public DoorsLocation? GetStorableLocationByKey(string guidKey)
  { return GetStorableLocationByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsLocation> QueryStorableLocationByKey(Guid guidKey)
  {
    IQueryable<DoorsLocation> qry = this.DoorsLocations;
    qry = qry.Where(ss => (ss.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file