// SqldbcUilNexusSnapshotViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public NexusSnapshotViewModel GetViewableSnapshotByKey(Guid guidKey)
  { return QueryStorableNexusSnapshotByKey(guidKey).ToViewable().SingleOrDefault(); }
  public NexusSnapshotViewModel GetViewableSnapshotByKey(string guidKey)
  { return GetViewableSnapshotByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusNexusSnapshot GetStorableSnapshotByKey(Guid guidKey)
  { return QueryStorableNexusSnapshotByKey(guidKey).SingleOrDefault(); }
  public NexusNexusSnapshot GetStorableSnapshotByKey(string guidKey)
  { return GetStorableSnapshotByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusNexusSnapshot> QueryStorableNexusSnapshotByKey(Guid guidKey)
  {
    IQueryable<NexusNexusSnapshot> qry = this.NexusNexusSnapshots;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file