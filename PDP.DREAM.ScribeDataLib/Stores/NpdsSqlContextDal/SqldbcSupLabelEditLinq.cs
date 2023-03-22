// SqldbcUilSupLabelEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static SupportingLabelEditModel ToEditable(this NexusSupportingLabel r)
  {
    var nre = new SupportingLabelEditModel()
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
      SupportingLabel = r.SupportingLabel
    };
    return nre;
  }

  public static IQueryable<SupportingLabelEditModel> ToEditable(this IQueryable<NexusSupportingLabel> query)
  {
    IQueryable<SupportingLabelEditModel> rows =
      from r in query
      select new SupportingLabelEditModel
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
        SupportingLabel = r.SupportingLabel
      };
    return rows;
  }

}
