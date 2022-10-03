// SqldbcUilSrvcRstrctOrViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<ServiceRestrictionOrViewModel> ListViewableRestrictionOrsByAnd(Guid? andGuidKey)
  {
    IEnumerable<ServiceRestrictionOrViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r => (r.RestrictionAndGuidRef == andGuidKey));
      result = qry.ToViewable().AsEnumerable()
        .OrderBy(r => r.RestrictionOrHasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrViewModel>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrViewModel> ListViewableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel)
  {
    IEnumerable<ServiceRestrictionOrViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid) && (r.IsConceptLabel == isLabel));
      result = qry.ToViewable().AsEnumerable()
        .OrderBy(r => r.RestrictionAndHasPriority).ThenBy(r => r.RestrictionOrHasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrViewModel>();
    }
    return result;
  }

  public IEnumerable<ServiceRestrictionOrViewModel> ListViewableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel, bool isPhrase)
  {
    IEnumerable<ServiceRestrictionOrViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid) && (r.IsConceptLabel == isLabel) && (r.IsWordPhrase == isPhrase));
      result = qry.OrderBy(r => r.AndHasIndex).ThenBy(r => r.OrHasIndex).ToViewable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceRestrictionOrViewModel>();
    }
    return result;
  }

}
