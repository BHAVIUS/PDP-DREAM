// SqldbcUilProvenanceViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<ProvenanceViewModel> ListViewableProvenances(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<ProvenanceViewModel> result;
    try
    {
      IQueryable<NexusProvenance> qry = this.NexusProvenances;
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
      result = Enumerable.Empty<ProvenanceViewModel>();
    }
    return result;
  }

} // end class

// end file

