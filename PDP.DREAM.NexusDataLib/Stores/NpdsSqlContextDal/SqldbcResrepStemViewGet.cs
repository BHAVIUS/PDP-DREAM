// SqldbcUilResrepStemViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public NexusResrepViewModel? GetViewableResrepStemByRKey(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepStemByIKey(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepStemByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepStem> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusStemByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusStemByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefault();
    return row;
  }
  public NexusResrepViewModel? GetViewableResrepStemByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepStemByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepStem> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusStemByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusStemByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepStemByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }

  // GetStorables //
  public INexusResrepStem GetStorableNexusStemByRKey(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).SingleOrDefault(); }
  public INexusResrepStem GetStorableNexusStemByIKey(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).SingleOrDefault(); }
  public INexusResrepStem GetStorableNexusStemByDiristryEntityTag(Guid diristryGuid, string entityTag)
  { return QueryStorableNexusLeafByDiristryEntityTag(diristryGuid, entityTag).FirstOrDefault(); }

  public INexusResrepStem GetStorableNexusStemWithFacets(Guid guidKey)
  {
    IQueryable<INexusResrepStem> query = this.NexusResrepLeafs;
    query = query.Where(r => (r.RecordGuidKey == guidKey));
    query = query.Select((INexusResrepStem rr) => rr)
      .Include((INexusResrepStem rr) => rr.NexusEntityLabels)
      .Include((INexusResrepStem rr) => rr.NexusSupportingTags)
      .Include((INexusResrepStem rr) => rr.NexusSupportingLabels)
      .Include((INexusResrepStem rr) => rr.NexusCrossReferences)
      .Include((INexusResrepStem rr) => rr.NexusOtherTexts)
      .Include((INexusResrepStem rr) => rr.NexusLocations)
      .Include((INexusResrepStem rr) => rr.NexusDescriptions)
      .Include((INexusResrepStem rr) => rr.NexusProvenances)
      .Include((INexusResrepStem rr) => rr.NexusDistributions)
      .Include((INexusResrepStem rr) => rr.NexusFairMetrics);
    INexusResrepStem resrep = query.SingleOrDefault();
    return resrep;
  }

  // QueryStorables //
  public IQueryable<INexusResrepStem> QueryStorableNexusStemByRKey(Guid guidKey)
  {
    IQueryable<INexusResrepStem> qry = this.NexusResrepStems;
    qry = qry.Where(rr => (rr.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepStem> QueryStorableNexusStemByIKey(Guid guidKey)
  {
    IQueryable<INexusResrepStem> qry = this.NexusResrepStems;
    qry = qry.Where(rr => (rr.InfosetGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepStem> QueryStorableNexusStemByDiristryEntityTag(Guid diristryGuid, string entityTag)
  {
    IQueryable<INexusResrepStem> qry = this.NexusResrepStems;
    qry = qry.Where(rr => (rr.RecordDiristryGuidRef == diristryGuid) &&
    ((rr.EntityInitialTag == entityTag) || (rr.EntityPrincipalTag == entityTag)));
    return qry;
  }

} // end class

// end file