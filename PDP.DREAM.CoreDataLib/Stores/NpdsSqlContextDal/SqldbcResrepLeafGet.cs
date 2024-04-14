// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public CoreResrepLeafUxm? GetEditableResrepLeafByRKey(Guid guidKey)
  { return QueryStorableResrepLeafByRKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<CoreResrepLeafUxm?> GetEditableResrepLeafByRKeyAsync(Guid guidKey)
  { return QueryStorableResrepLeafByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public CoreResrepLeafUxm? GetEditableResrepLeafByIKey(Guid guidKey)
  { return QueryStorableResrepLeafByIKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<CoreResrepLeafUxm?> GetEditableResrepLeafByIKeyAsync(Guid guidKey)
  { return QueryStorableResrepLeafByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public CoreResrepLeafUxm? GetEditableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<ICoreResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableResrepLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableResrepLeafByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefault();
    return row;
  }
  public CoreResrepLeafUxm? GetEditableResrepLeafByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepLeafByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<CoreResrepLeafUxm?> GetEditableResrepLeafByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<ICoreResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableResrepLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableResrepLeafByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefaultAsync();
    return row;
  }
  public Task<CoreResrepLeafUxm?> GetEditableResrepLeafByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepLeafByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }

  // GetStorables //
  public ICoreResrepLeaf GetStorableResrepLeafByRKey(Guid guidKey)
  { return QueryStorableResrepLeafByRKey(guidKey).SingleOrDefault(); }
  public ICoreResrepLeaf GetStorableResrepLeafByIKey(Guid guidKey)
  { return QueryStorableResrepLeafByIKey(guidKey).SingleOrDefault(); }
  public ICoreResrepLeaf GetStorableResrepLeafByDiristryEntityTag(Guid diristryGuid, string entityTag)
  { return QueryStorableResrepLeafByDiristryEntityTag(diristryGuid, entityTag).FirstOrDefault(); }

  public ICoreResrepLeaf GetStorableResrepLeafWithFacets(Guid guidKey)
  {
    IQueryable<ICoreResrepLeaf> query = this.CoreResrepLeafs;
    query = query.Where(r => (r.RecordGuid == guidKey));
    query = query.Select((ICoreResrepLeaf rr) => rr)
      .Include((ICoreResrepLeaf rr) => rr.CoreEntityLabels)
      .Include((ICoreResrepLeaf rr) => rr.PortalSupportingTags)
      .Include((ICoreResrepLeaf rr) => rr.PortalSupportingLabels)
      .Include((ICoreResrepLeaf rr) => rr.PortalCrossReferences)
      .Include((ICoreResrepLeaf rr) => rr.PortalOtherTexts)
      .Include((ICoreResrepLeaf rr) => rr.DoorsLocations)
      .Include((ICoreResrepLeaf rr) => rr.DoorsDescriptions)
      .Include((ICoreResrepLeaf rr) => rr.DoorsProvenances)
      .Include((ICoreResrepLeaf rr) => rr.DoorsDistributions)
      .Include((ICoreResrepLeaf rr) => rr.DoorsFairMetrics);
    ICoreResrepLeaf resrep = query.SingleOrDefault();
    return resrep;
  }

  // QueryStorables //
  public IQueryable<ICoreResrepLeaf> QueryStorableResrepLeafByRKey(Guid guidKey)
  {
    IQueryable<ICoreResrepLeaf> qry = this.CoreResrepLeafs;
    qry = qry.Where(rr => (rr.RecordGuid == guidKey));
    return qry;
  }
  public IQueryable<ICoreResrepLeaf> QueryStorableResrepLeafByIKey(Guid guidKey)
  {
    IQueryable<ICoreResrepLeaf> qry = this.CoreResrepLeafs;
    qry = qry.Where(rr => (rr.InfosetGuid == guidKey));
    return qry;
  }
  public IQueryable<ICoreResrepLeaf> QueryStorableResrepLeafByDiristryEntityTag(Guid diristryGuid, string entityTag)
  {
    IQueryable<ICoreResrepLeaf> qry = this.CoreResrepLeafs;
    qry = qry.Where(rr => (rr.RecordDiristryGuid == diristryGuid) &&
    ((rr.EntityInitialPrincipalTag == entityTag) || (rr.EntityPrincipalTag == entityTag)));
    return qry;
  }

} // end class

// end file