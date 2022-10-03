// SqldbcUilDoorsSnapshotViewGet.cs 
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
  public DoorsSnapshotViewModel GetViewableDoorsSnapshotByKey(Guid guidKey)
  { return QueryStorableDoorsSnapshotByKey(guidKey).ToViewable().SingleOrDefault(); }
  public DoorsSnapshotViewModel GetViewableDoorsSnapshotByKey(string guidKey)
  { return GetViewableDoorsSnapshotByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusDoorsSnapshot GetStorableDoorsSnapshotByKey(Guid guidKey)
  { return QueryStorableDoorsSnapshotByKey(guidKey).SingleOrDefault(); }
  public NexusDoorsSnapshot GetStorableDoorsSnapshotByKey(string guidKey)
  { return GetStorableDoorsSnapshotByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusDoorsSnapshot> QueryStorableDoorsSnapshotByKey(Guid guidKey)
  {
    IQueryable<NexusDoorsSnapshot> qry = this.NexusDoorsSnapshots;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file