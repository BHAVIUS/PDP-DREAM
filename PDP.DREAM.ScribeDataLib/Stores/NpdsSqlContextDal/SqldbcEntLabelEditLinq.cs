// SqldbcUilEntLabelEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Linq;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<EntityLabelEditModel> ToEditable(this IQueryable<NexusEntityLabel> query)
  {
    IQueryable<EntityLabelEditModel> rows =
      from r in query
      select new EntityLabelEditModel
      {
        RRFgroupGuid = r.FgroupGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        ManagedByAgentGuid = r.ManagedByAgentGuidRef,
        ManagedByAgentName = r.ManagedByAgentUserName,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuidRef,
        CreatedByAgentName = r.CreatedByAgentUserName,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
        UpdatedByAgentName = r.UpdatedByAgentUserName,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuidRef,
        DeletedByAgentName = r.DeletedByAgentUserName,
        //
        IsResolvable = r.IsResolvable,
        IsPrivate = r.IsPrivate,
        IsGenerating = r.IsGenerating,
        ServiceTypeCode = r.ServiceTypeCodeRef,
        TagToken = r.TagToken,
        LabelUri = r.LabelUri,
        EntityLabel = r.EntityLabel
      };
    return rows;
  }

}

