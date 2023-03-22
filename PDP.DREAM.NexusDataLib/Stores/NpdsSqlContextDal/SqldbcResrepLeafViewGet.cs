// SqldbcUilResrepLeafViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public NexusResrepViewModel? GetViewableResrepLeafByRKey(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepLeafByIKey(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusLeafByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefault();
    return row;
  }
  public NexusResrepViewModel? GetViewableResrepLeafByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepLeafByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusLeafByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepLeafByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }

  // GetStorables //
  public INexusResrepLeaf GetStorableNexusLeafByRKey(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).SingleOrDefault(); }
  public INexusResrepLeaf GetStorableNexusLeafByIKey(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).SingleOrDefault(); }
  public INexusResrepLeaf GetStorableNexusLeafByDiristryEntityTag(Guid diristryGuid, string entityTag)
  { return QueryStorableNexusLeafByDiristryEntityTag(diristryGuid, entityTag).FirstOrDefault(); }

  public INexusResrepLeaf GetStorableNexusLeafWithFacets(Guid guidKey)
  {
    IQueryable<INexusResrepLeaf> query = this.NexusResrepLeafs;
    query = query.Where(r => (r.RecordGuidKey == guidKey));
    query = query.Select((INexusResrepLeaf rr) => rr)
      .Include((INexusResrepLeaf rr) => rr.NexusEntityLabels)
      .Include((INexusResrepLeaf rr) => rr.NexusSupportingTags)
      .Include((INexusResrepLeaf rr) => rr.NexusSupportingLabels)
      .Include((INexusResrepLeaf rr) => rr.NexusCrossReferences)
      .Include((INexusResrepLeaf rr) => rr.NexusOtherTexts)
      .Include((INexusResrepLeaf rr) => rr.NexusLocations)
      .Include((INexusResrepLeaf rr) => rr.NexusDescriptions)
      .Include((INexusResrepLeaf rr) => rr.NexusProvenances)
      .Include((INexusResrepLeaf rr) => rr.NexusDistributions)
      .Include((INexusResrepLeaf rr) => rr.NexusFairMetrics);
    INexusResrepLeaf resrep = query.SingleOrDefault();
    return resrep;
  }

  // QueryStorables //
  public IQueryable<INexusResrepLeaf> QueryStorableNexusLeafByRKey(Guid guidKey)
  {
    IQueryable<INexusResrepLeaf> qry = this.NexusResrepLeafs;
    qry = qry.Where(rr => (rr.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepLeaf> QueryStorableNexusLeafByIKey(Guid guidKey)
  {
    IQueryable<INexusResrepLeaf> qry = this.NexusResrepLeafs;
    qry = qry.Where(rr => (rr.InfosetGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepLeaf> QueryStorableNexusLeafByDiristryEntityTag(Guid diristryGuid, string entityTag)
  {
    IQueryable<INexusResrepLeaf> qry = this.NexusResrepLeafs;
    qry = qry.Where(rr => (rr.RecordDiristryGuidRef == diristryGuid) &&
    ((rr.EntityInitialTag == entityTag) || (rr.EntityPrincipalTag == entityTag)));
    return qry;
  }

} // end class

// end file