// SqldbcUilPortalSnapshotViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public PortalSnapshotViewModel GetViewablePortalSnapshotByKey(Guid guidKey)
  { return QueryStorablePortalSnapshotByKey(guidKey).ToViewable().SingleOrDefault(); }
  public PortalSnapshotViewModel GetViewablePortalSnapshotByKey(string guidKey)
  { return GetViewablePortalSnapshotByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusPortalSnapshot GetStorablePortalSnapshotByKey(Guid guidKey)
  { return QueryStorablePortalSnapshotByKey(guidKey).SingleOrDefault(); }
  public NexusPortalSnapshot GetStorablePortalSnapshotByKey(string guidKey)
  { return GetStorablePortalSnapshotByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusPortalSnapshot> QueryStorablePortalSnapshotByKey(Guid guidKey)
  {
    IQueryable<NexusPortalSnapshot> qry = this.NexusPortalSnapshots;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file