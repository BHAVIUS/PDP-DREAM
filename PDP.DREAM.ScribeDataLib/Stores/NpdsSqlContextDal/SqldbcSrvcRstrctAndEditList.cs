// SqldbcUilSrvcRstrctAndEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // ListEditables //
  public IEnumerable<ServiceRestrictionAndEditModel> ListEditableRestrictionAndsByIGuid(Guid infosetGuid, bool isExcluding)
  {
    IEnumerable<ServiceRestrictionAndEditModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r =>
        (r.InfosetGuidRef == infosetGuid) && (r.IsExcluding == isExcluding))
        .OrderBy(r => r.AndHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndEditModel>();
    }
    return result;
  }
  public IEnumerable<ServiceRestrictionAndEditModel> ListEditableRestrictionAndsByRGuid(Guid recordGuid)
  {
    IEnumerable<ServiceRestrictionAndEditModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r =>
        (r.RecordGuidRef == recordGuid)) // both isExcluding true and false
        .OrderBy(r => r.AndHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndEditModel>();
    }
    return result;
  }

  // ListStorables //

} // end class

// end file