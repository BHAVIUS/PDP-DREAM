// SqldbcUilSrvcRstrctOrEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // ListEditables //
  public IEnumerable<ServiceRestrictionOrEditModel> ListEditableRestrictionOrsByIGuid(Guid infosetGuid, bool isExcluding)
  {
    // list by ResrepInfosetGuid
    IEnumerable<ServiceRestrictionOrEditModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.InfosetGuidRef == infosetGuid) && (r.IsExcluding == isExcluding))
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrEditModel>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrEditModel> ListEditableRestrictionOrsByRGuid(Guid recordGuid)
  {
    // list by ResrepRecordGuid
    IEnumerable<ServiceRestrictionOrEditModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.RecordGuidRef == recordGuid)) // both isExcluding true and false
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrEditModel>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrEditModel> ListEditableRestrictionOrsByAndGuid(Guid rstrctAndGuid)
  {
    // list by ServiceRestrictionAndGuid
    IEnumerable<ServiceRestrictionOrEditModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r =>
        (r.RestrictionAndGuidRef == rstrctAndGuid))
        .OrderBy(r => r.AndHasPriority).ThenBy(r => r.OrHasPriority);
      result = qry.ToEditable().AsEnumerable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrEditModel>();
    }
    return result;
  }

  // ListStorables //

} // end class

// end file