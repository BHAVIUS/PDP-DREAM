// SqldbcUilResrepRootEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Kendo.Mvc.Extensions;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<NexusResrepEditModel?> ToEditable(this IQueryable<INexusResrepRoot> query, Guid agentGuidRef = default)
  {
    IQueryable<NexusResrepEditModel?> rows =
      from r in query
      select new NexusResrepEditModel
      {
        AgentGuid = agentGuidRef,
        RRRecordGuid = r.RecordGuidKey,
        RRInfosetGuid = r.InfosetGuidKey,
        RecordHandle = r.RecordHandle,
        RecordIsDeleted = r.RecordIsDeleted,
        ManagedByAgentGuid = r.RecordManagedByAgentGuidRef,
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
        //
        EntityTypeCode = r.EntityTypeCodeRef,
        EntityTypeName = r.EntityTypeName,
        EntityInitialTag = r.EntityInitialTag,
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
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        RecordDiristryTag = r.RecordDiristryTag,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryTag = r.RecordRegistryTag,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryTag = r.RecordDirectoryTag,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarTag = r.RecordRegistrarTag,
      };
    return rows;
  }

} // end class

// end file