// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // ListEditables //
  public IEnumerable<ServiceRestrictionAndUxm> ListEditableRestrictionAndsByIGuid(Guid infosetGuid, bool isExcluding)
  {
    IEnumerable<ServiceRestrictionAndUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionAnd> qry = this.CoreServiceRestrictionAnds;
      qry = qry.Where(r =>
        (r.InfosetGuid == infosetGuid) && (r.IsExcluding == isExcluding))
        .OrderBy(r => r.AndHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndUxm>();
    }
    return result;
  }
  public IEnumerable<ServiceRestrictionAndUxm> ListEditableRestrictionAndsByRGuid(Guid recordGuid)
  {
    IEnumerable<ServiceRestrictionAndUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionAnd> qry = this.CoreServiceRestrictionAnds;
      qry = qry.Where(r =>
        (r.RecordGuid == recordGuid)) // both isExcluding true and false
        .OrderBy(r => r.AndHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndUxm>();
    }
    return result;
  }

  // ListStorables //

} // end class

// end file