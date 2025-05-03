// SqldbcUilPortalSnapshotViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<PortalSnapshotViewModel> ListViewablePortalSnapshots(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<PortalSnapshotViewModel> result;
    try
    {
      IQueryable<NexusPortalSnapshot> qry = this.NexusPortalSnapshots;
      if (QURC.ClientHasAdminAccess || QURC.ClientHasEditorAccess)
      { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == QURC.QebAgentGuid)); }
        else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
      }
      result = qry.ToViewable().AsEnumerable().OrderBy(r => r.HasPriority).ToList();
    }
    catch
    {
      result = Enumerable.Empty<PortalSnapshotViewModel>();
    }
    return result;
  }

} // end class

// end file