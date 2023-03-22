// SqldbcUilLocationViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<LocationViewModel> ListViewableLocations(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<LocationViewModel> result;
    try
    {
      IQueryable<NexusLocation> qry = this.NexusLocations;
      if (NPDSCP.ClientHasAdminAccess || NPDSCP.ClientHasEditorAccess)
      { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == NPDSCP.ClientAgentGuid)); }
        else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
      }
      result = qry.ToViewable().AsEnumerable().OrderBy(r => r.HasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<LocationViewModel>();
    }
    return result;
  }

} // end class

// end file