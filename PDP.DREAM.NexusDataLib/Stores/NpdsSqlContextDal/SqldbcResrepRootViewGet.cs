// SqldbcUilResrepRootViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public NexusResrepViewModel? GetViewableResrepRootByRKey(Guid guidKey)
  { return QueryStorableNexusRootByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusRootByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepRootByIKey(Guid guidKey)
  { return QueryStorableNexusRootByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusRootByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepRootByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusRootByRKey(guidKey);
    }
    var rr = qry.ToViewable().SingleOrDefault();
    return rr;
  }
  public NexusResrepViewModel? GetViewableResrepRootByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepRootByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusRootByRKey(guidKey);
    }
    var rr = qry.ToViewable().SingleOrDefaultAsync();
    return rr;
  }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepRootByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }

  // GetStorables //
  public INexusResrepRoot GetStorableNexusRootByRKey(Guid guidKey)
  { return QueryStorableNexusRootByRKey(guidKey).SingleOrDefault(); }
  public INexusResrepRoot GetStorableNexusRootByIKey(Guid guidKey)
  { return QueryStorableNexusRootByIKey(guidKey).SingleOrDefault(); }
  public INexusResrepRoot GetStorableNexusRootByDiristryEntityTag(Guid diristryGuid, string entityTag)
  { return QueryStorableNexusRootByDiristryEntityTag(diristryGuid, entityTag).FirstOrDefault(); }

  public INexusResrepRoot GetStorableNexusRootWithFacets(Guid guidKey)
  {
    IQueryable<INexusResrepRoot> query = this.NexusResrepLeafs;
    query = query.Where(r => (r.RecordGuidKey == guidKey));
    query = query.Select((INexusResrepRoot rr) => rr)
      .Include((INexusResrepRoot rr) => rr.NexusEntityLabels)
      .Include((INexusResrepRoot rr) => rr.NexusSupportingTags)
      .Include((INexusResrepRoot rr) => rr.NexusSupportingLabels)
      .Include((INexusResrepRoot rr) => rr.NexusCrossReferences)
      .Include((INexusResrepRoot rr) => rr.NexusOtherTexts)
      .Include((INexusResrepRoot rr) => rr.NexusLocations)
      .Include((INexusResrepRoot rr) => rr.NexusDescriptions)
      .Include((INexusResrepRoot rr) => rr.NexusProvenances)
      .Include((INexusResrepRoot rr) => rr.NexusDistributions)
      .Include((INexusResrepRoot rr) => rr.NexusFairMetrics);
    INexusResrepRoot resrep = query.SingleOrDefault();
    return resrep;
  }

  // QueryStorables //
  public IQueryable<INexusResrepRoot> QueryStorableNexusRootByRKey(Guid guidKey)
  {
    IQueryable<INexusResrepRoot> qry = this.NexusResrepRoots;
    qry = qry.Where(rr => (rr.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepRoot> QueryStorableNexusRootByIKey(Guid guidKey)
  {
    IQueryable<INexusResrepRoot> qry = this.NexusResrepRoots;
    qry = qry.Where(rr => (rr.InfosetGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepRoot> QueryStorableNexusRootByDiristryEntityTag(Guid diristryGuid, string entityTag)
  {
    IQueryable<INexusResrepRoot> qry = this.NexusResrepRoots;
    qry = qry.Where(rr => (rr.RecordDiristryGuidRef == diristryGuid) &&
    ((rr.EntityInitialTag == entityTag) || (rr.EntityPrincipalTag == entityTag)));
    return qry;
  }

} // end class

// end file