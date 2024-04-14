// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // ListEditables //

  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByAnd(Guid? andGuidKey)
  {
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r => (r.RestrictionAndGuidRef == andGuidKey));
      result = qry.ToEditable().AsEnumerable()
        .OrderBy(r => r.RestrictionOrHasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByIGuid(Guid infosetGuid, bool isExcluding)
  {
    // list by ResrepInfosetGuid
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.InfosetGuid == infosetGuid) && (r.IsExcluding == isExcluding))
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByRGuid(Guid recordGuid)
  {
    // list by ResrepRecordGuid
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.RecordGuid == recordGuid)) // both isExcluding true and false
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByAndGuid(Guid rstrctAndGuid)
  {
    // list by ServiceRestrictionAndGuid
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.RestrictionAndGuidRef == rstrctAndGuid))
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }


  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel)
  {
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r => (r.InfosetGuid == infosetGuid) && (r.IsConceptLabel == isLabel));
      result = qry.ToEditable().AsEnumerable()
        .OrderBy(r => r.RestrictionAndHasPriority).ThenBy(r => r.RestrictionOrHasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrUxm> ListEditableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel, bool isPhrase)
  {
    IEnumerable<ServiceRestrictionOrUxm> result;
    try
    {
      IQueryable<CoreServiceRestrictionOr> qry = this.CoreServiceRestrictionOrs;
      qry = qry.Where(r => (r.InfosetGuid == infosetGuid) && (r.IsConceptLabel == isLabel) && (r.IsWordPhrase == isPhrase));
      result = qry.OrderBy(r => r.AndHasIndex).ThenBy(r => r.OrHasIndex).ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrUxm>();
    }
    return result;
  }
  // ListStorables //

} // end class

// end file