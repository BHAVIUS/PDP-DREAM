﻿// SqldbcUilDescriptionViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<DescriptionViewModel> ListViewableDescriptions(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<DescriptionViewModel> result;
    try
    {
      IQueryable<NexusDescription> qry = this.NexusDescriptions;
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
      result = Enumerable.Empty<DescriptionViewModel>();
    }
    return result;
  }

} // end class

// end file