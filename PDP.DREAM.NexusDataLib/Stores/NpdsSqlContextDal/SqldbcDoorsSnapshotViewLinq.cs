// SqldbcUilDoorsSnapshotViewLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  // TODO: rebuild Nexus service to allow for Nexus service reads of
  //  NexusResrepSnapshot, PortalResrepSnapshot, DoorsResrepSnapshot
  // TODO: rebuild Nexus datastore to allow dataviews for
  //  NexusResrepSnapshot, PortalResrepSnapshot, DoorsResrepSnapshot
  public static IQueryable<DoorsSnapshotViewModel> ToViewable(this IQueryable<NexusDoorsSnapshot> query)
  {
    throw new NotImplementedException();  

    //IQueryable<DoorsSnapshotViewModel> rows =
    //  from r in query
    //  select new NexusSnapshotViewModel
    //  {
    //    RRFgroupGuid = r.FgroupGuidKey,
    //    RRRecordGuid = r.RecordGuidRef,
    //    HasIndex = r.HasIndex,
    //    HasPriority = r.HasPriority,
    //    IsMarked = r.IsMarked,
    //    IsPrincipal = r.IsPrincipal,
    //    IsDeleted = r.IsDeleted,
    //    ManagedByAgentGuid = r.ManagedByAgentGuidRef,
    //    ManagedByAgentName = r.ManagedByAgentUserName,
    //    CreatedOn = r.CreatedOn,
    //    CreatedByAgentGuid = r.CreatedByAgentGuidRef,
    //    CreatedByAgentName = r.CreatedByAgentUserName,
    //    UpdatedOn = r.UpdatedOn,
    //    UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
    //    UpdatedByAgentName = r.UpdatedByAgentUserName,
    //    DeletedOn = r.DeletedOn,
    //    DeletedByAgentGuid = r.DeletedByAgentGuidRef,
    //    DeletedByAgentName = r.DeletedByAgentUserName,
    //    //
    //    DoorsSnapshot = r.ResrepSnapshotXml
    //  };
    //return rows;
  }

} // end class

// end file