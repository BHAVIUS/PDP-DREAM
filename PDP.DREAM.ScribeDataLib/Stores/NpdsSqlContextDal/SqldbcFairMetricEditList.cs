// SqldbcUilFairMetricEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public IEnumerable<FairMetricEditModel> ListEditableFairMetrics(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<FairMetricEditModel> result;
    try
    {
      IQueryable<NexusFairMetric> qry = this.NexusFairMetrics;
      if (NPDSCP.ClientHasAdminAccess || NPDSCP.ClientHasEditorAccess)
      { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited)
        {
          qry = qry.Where(ss => (ss.RecordGuidRef == guidKey) &&
            (ss.IsDeleted == false) && (ss.UpdatedByAgentGuidRef == NPDSCP.ClientAgentGuid));
        }
        else
        {
          qry = qry.Where(ss => (ss.RecordGuidRef == guidKey) &&
            (ss.IsDeleted == false));
        }
      }
      result = qry.ToEditable().AsEnumerable()
        .OrderBy(ss => ss.HasPriority).ThenByDescending(ss => ss.UpdatedOn)
        .ToList();
    }
    catch
    {
      result = Enumerable.Empty<FairMetricEditModel>();
    }
    return result;
  }

} // end class

// end file