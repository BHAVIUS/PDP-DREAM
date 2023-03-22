// SqldbcUilLocationEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public IEnumerable<LocationEditModel> ListEditableLocations(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<LocationEditModel> result;
    try
    {
      IQueryable<NexusLocation> qry = this.NexusLocations;
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
      result = Enumerable.Empty<LocationEditModel>();
    }
    return result;
  }

} // end class

// end file