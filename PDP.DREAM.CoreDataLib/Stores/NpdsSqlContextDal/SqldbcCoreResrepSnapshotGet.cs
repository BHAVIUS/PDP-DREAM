// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public ResrepSnapshotUxm GetEditableSnapshotByKey(Guid guidKey)
  { return QueryStorableSnapshotByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ResrepSnapshotUxm GetEditableSnapshotByKey(string guidKey)
  { return GetEditableSnapshotByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public CoreResrepSnapshot GetStorableSnapshotByKey(Guid guidKey)
  { return QueryStorableSnapshotByKey(guidKey).SingleOrDefault(); }
  public CoreResrepSnapshot GetStorableSnapshotByKey(string guidKey)
  { return GetStorableSnapshotByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<CoreResrepSnapshot> QueryStorableSnapshotByKey(Guid guidKey)
  {
    IQueryable<CoreResrepSnapshot> qry = this.CoreResrepSnapshots;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file