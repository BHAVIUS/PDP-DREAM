// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<CoreResrepRootUxm?> ToEditable(this IQueryable<ICoreResrepRoot> query, Guid? agentGuidRef = default)
  {
    IQueryable<CoreResrepRootUxm?> rows =
      from r in query
      select new CoreResrepRootUxm
      {
        AgentGuid = agentGuidRef,
        RRRecordGuid = r.RecordGuid,
        RRInfosetGuid = r.InfosetGuid,
        RecordHandle = r.RecordHandle,
        RecordIsDeleted = r.RecordIsDeleted,
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuid,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuid,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuid,
        //
        AuthoredByAgentGuid = r.RecordAuthoredByAgentGuid,
        ReviewedByAgentGuid = r.RecordReviewedByAgentGuid,
        EditedByAgentGuid = r.RecordEditedByAgentGuid,
        ManagedByAgentGuid = r.RecordManagedByAgentGuid,
        //
        EntityTypeCode = r.EntityTypeCode,
        EntityTypeName = r.EntityTypeName,
        EntityInitPrinTag = r.EntityInitialPrincipalTag,
        EntityPrincipalTag = r.EntityPrincipalTag,
        EntityCanonicalLabel = r.EntityCanonicalLabel,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        InfosetPortalStatusCode = r.InfosetPortalStatusCode,
        InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
        RecordDiristryGuid = r.RecordDiristryGuid,
        RecordDiristryTag = r.RecordDiristryTag,
        RecordRegistryGuid = r.RecordRegistryGuid,
        RecordRegistryTag = r.RecordRegistryTag,
        RecordDirectoryGuid = r.RecordDirectoryGuid,
        RecordDirectoryTag = r.RecordDirectoryTag,
        RecordRegistrarGuid = r.RecordRegistrarGuid,
        RecordRegistrarTag = r.RecordRegistrarTag,
      };
    return rows;
  }

} // end class

// end file