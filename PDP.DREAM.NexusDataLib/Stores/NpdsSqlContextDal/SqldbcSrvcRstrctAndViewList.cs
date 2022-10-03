// SqldbcUilSrvcRstrctAndViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // ListViewables //
  public IEnumerable<ServiceRestrictionAndViewModel> ListViewableRestrictionAndsByIGuid(Guid infosetGuid, bool isExcluding)
  {
    IEnumerable<ServiceRestrictionAndViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid) && (r.IsExcluding == isExcluding));
      result = qry.ToViewable().AsEnumerable().OrderBy(r => r.HasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndViewModel>();
    }
    return result;
  }
  public IEnumerable<ServiceRestrictionAndViewModel> ListViewableRestrictionAndsByRGuid(Guid recordGuid)
  {
    IEnumerable<ServiceRestrictionAndViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r => (r.RecordGuidRef == recordGuid)); // both isExcluding true and false
      result = qry.ToViewable().AsEnumerable().OrderBy(r => r.HasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionAndViewModel>();
    }
    return result;
  }

  // ListStorables //

}

