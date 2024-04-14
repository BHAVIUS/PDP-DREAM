// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public CoreResrepRootUxm? GetEditableResrepRootByRKey(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<CoreResrepRootUxm?> GetEditableResrepRootByRKeyAsync(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public CoreResrepRootUxm? GetEditableResrepRootByIKey(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<CoreResrepRootUxm?> GetEditableResrepRootByIKeyAsync(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public CoreResrepRootUxm? GetEditableResrepRootByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<ICoreResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableResrepRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableResrepRootByRKey(guidKey);
    }
    var rr = qry.ToEditable().SingleOrDefault();
    return rr;
  }
  public CoreResrepRootUxm? GetEditableResrepRootByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepRootByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<CoreResrepRootUxm?> GetEditableResrepRootByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<ICoreResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableResrepRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableResrepRootByRKey(guidKey);
    }
    var rr = qry.ToEditable().SingleOrDefaultAsync();
    return rr;
  }
  public Task<CoreResrepRootUxm?> GetEditableResrepRootByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepRootByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }

  // GetStorables //
  public ICoreResrepRoot GetStorableResrepRootByRKey(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).SingleOrDefault(); }
  public ICoreResrepRoot GetStorableResrepRootByIKey(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).SingleOrDefault(); }
  public ICoreResrepRoot GetStorableResrepRootByEntityTagAndDrstryIKey(Guid? diristryGuid, string entityTag)
  { return QueryStorableResrepRootByEntityTagAndDrstryIKey(diristryGuid, entityTag).FirstOrDefault(); }

  public ICoreResrepRoot GetStorableResrepRootWithFacets(Guid guidKey)
  {
    IQueryable<ICoreResrepRoot> query = this.CoreResrepRoots;
    query = query.Where(r => (r.RecordGuid == guidKey));
    query = query.Select((ICoreResrepRoot rr) => rr)
      .Include((ICoreResrepRoot rr) => rr.CoreEntityLabels)
      .Include((ICoreResrepRoot rr) => rr.PortalSupportingTags)
      .Include((ICoreResrepRoot rr) => rr.PortalSupportingLabels)
      .Include((ICoreResrepRoot rr) => rr.PortalCrossReferences)
      .Include((ICoreResrepRoot rr) => rr.PortalOtherTexts)
      .Include((ICoreResrepRoot rr) => rr.DoorsLocations)
      .Include((ICoreResrepRoot rr) => rr.DoorsDescriptions)
      .Include((ICoreResrepRoot rr) => rr.DoorsProvenances)
      .Include((ICoreResrepRoot rr) => rr.DoorsDistributions)
      .Include((ICoreResrepRoot rr) => rr.DoorsFairMetrics);
    ICoreResrepRoot resrep = query.SingleOrDefault();
    return resrep;
  }

  // QueryStorables //
  public IQueryable<ICoreResrepRoot> QueryStorableResrepRootByRKey(Guid guidKey)
  {
    IQueryable<ICoreResrepRoot> qry = this.CoreResrepRoots;
    qry = qry.Where(rr => (rr.RecordGuid == guidKey));
    return qry;
  }
  public IQueryable<ICoreResrepRoot> QueryStorableResrepRootByIKey(Guid guidKey)
  {
    IQueryable<ICoreResrepRoot> qry = this.CoreResrepRoots;
    qry = qry.Where(rr => (rr.InfosetGuid == guidKey));
    return qry;
  }
  public IQueryable<ICoreResrepRoot> QueryStorableResrepRootByEntityTagAndDrstryIKey(Guid? diristryGuid, string entityTag)
  {
    IQueryable<ICoreResrepRoot> qry = this.CoreResrepRoots;
    qry = qry.Where(rr => (rr.RecordDiristryGuid == diristryGuid) &&
    ((rr.EntityInitialPrincipalTag == entityTag) || (rr.EntityPrincipalTag == entityTag)));
    return qry;
  }

} // end class

// end file