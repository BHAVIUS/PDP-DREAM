// SqldbcUilResrepLeafEdit.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusResrepEditModel ToEditable(this NexusResrepLeaf r, Guid agentGuidRef = default)
    {
      var nre = new NexusResrepEditModel()
      {
        AgentGuid = agentGuidRef,
        RRRecordGuid = r.RecordGuidKey,
        RRInfosetGuid = r.InfosetGuidKey,
        RecordHandle = r.RecordHandle,
        RecordIsDeleted = r.RecordIsDeleted,
        ManagedByAgentGuid = r.RecordManagedByAgentGuidRef,
        ManagedByAgentName = r.RecordManagedByUserName,
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
        CreatedByAgentName = r.RecordCreatedByUserName,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
        UpdatedByAgentName = r.RecordUpdatedByUserName,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
        DeletedByAgentName = r.RecordDeletedByUserName,
        //
        EntityTypeCode = r.EntityTypeCodeRef,
        EntityTypeName = r.EntityTypeName,
        // EntityInitialTag = r.EntityInitialTag,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        // EntityOwnerGuid = r.EntityOwnerGuidRef,
        // EntityOwnerLabel = r.EntityOwnerLabel,
        // EntityContactGuid = r.EntityOwnerGuidRef,
        // EntityContactLabel = r.EntityOwnerLabel,
        // EntityOtherGuid = r.EntityOwnerGuidRef,
        // EntityOtherLabel = r.EntityOwnerLabel,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        InfosetPortalStatusCode = r.InfosetPortalStatusCode,
        InfosetPortalStatusName = r.InfosetPortalStatusName,
        InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
        InfosetDoorsStatusName = r.InfosetDoorsStatusName,
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        RecordDiristryName = r.RecordDiristryName,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryName = r.RecordRegistryName,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryName = r.RecordDirectoryName,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarName = r.RecordRegistrarName,
        ResrepEntityStatusCode = r.InfosetResrepEntityStatusCode,
        ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
        ResrepRecordStatusCode = r.InfosetResrepRecordStatusCode,
        ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
        ResrepInfosetStatusCode = r.InfosetResrepInfosetStatusCode,
        ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
        EntityLabelsCount = r.InfosetEntityLabelsCount,
        EntityLabelsStatusCode = r.InfosetEntityLabelsStatusCode,
        EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
        SupportingTagsCount = r.InfosetSupportingTagsCount,
        SupportingTagsStatusCode = r.InfosetSupportingTagsStatusCode,
        SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
        SupportingLabelsCount = r.InfosetSupportingLabelsCount,
        SupportingLabelsStatusCode = r.InfosetSupportingLabelsStatusCode,
        SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
        CrossReferencesCount = r.InfosetCrossReferencesCount,
        CrossReferencesStatusCode = r.InfosetCrossReferencesStatusCode,
        CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
        OtherTextsCount = r.InfosetOtherTextsCount,
        OtherTextsStatusCode = r.InfosetOtherTextsStatusCode,
        OtherTextsStatusName = r.InfosetOtherTextsStatusName,
        LocationsCount = r.InfosetLocationsCount,
        LocationsStatusCode = r.InfosetLocationsStatusCode,
        LocationsStatusName = r.InfosetLocationsStatusName,
        DescriptionsCount = r.InfosetDescriptionsCount,
        DescriptionsStatusCode = r.InfosetDescriptionsStatusCode,
        DescriptionsStatusName = r.InfosetDescriptionsStatusName,
        ProvenancesCount = r.InfosetProvenancesCount,
        ProvenancesStatusCode = r.InfosetProvenancesStatusCode,
        ProvenancesStatusName = r.InfosetProvenancesStatusName,
        DistributionsCount = r.InfosetDistributionsCount,
        DistributionsStatusCode = r.InfosetDistributionsStatusCode,
        DistributionsStatusName = r.InfosetDistributionsStatusName,
        FairMetricsCount = r.InfosetFairMetricsCount,
        FairMetricsStatusCode = r.InfosetFairMetricsStatusCode,
        FairMetricsStatusName = r.InfosetFairMetricsStatusName,
        NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
        NexusSnapshotsStatusCode = r.InfosetNexusSnapshotsStatusCode,
        NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
      };
      return nre;
    }

    public static IQueryable<NexusResrepEditModel> ToEditable(this IQueryable<NexusResrepLeaf> qry, Guid agentGuidRef = default)
    {
      IQueryable<NexusResrepEditModel> rows =
        from r in qry
        select new NexusResrepEditModel
        {
          AgentGuid = agentGuidRef,
          RRRecordGuid = r.RecordGuidKey,
          RRInfosetGuid = r.InfosetGuidKey,
          RecordHandle = r.RecordHandle,
          RecordIsDeleted = r.RecordIsDeleted,
          ManagedByAgentGuid = r.RecordManagedByAgentGuidRef,
          ManagedByAgentName = r.RecordManagedByUserName,
          CreatedOn = r.RecordCreatedOn,
          CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
          CreatedByAgentName = r.RecordCreatedByUserName,
          UpdatedOn = r.RecordUpdatedOn,
          UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
          UpdatedByAgentName = r.RecordUpdatedByUserName,
          DeletedOn = r.RecordDeletedOn,
          DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
          DeletedByAgentName = r.RecordDeletedByUserName,
          //
          EntityTypeCode = r.EntityTypeCodeRef,
          EntityTypeName = r.EntityTypeName,
          // EntityInitialTag = r.EntityInitialTag,
          EntityName = r.EntityName,
          EntityNature = r.EntityNature,
          // EntityOwnerGuid = r.EntityOwnerGuidRef,
          // EntityOwnerLabel = r.EntityOwnerLabel,
          // EntityContactGuid = r.EntityOwnerGuidRef,
          // EntityContactLabel = r.EntityOwnerLabel,
          // EntityOtherGuid = r.EntityOwnerGuidRef,
          // EntityOtherLabel = r.EntityOwnerLabel,
          InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
          InfosetIsAgentShared = r.InfosetIsAgentShared,
          InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
          InfosetIsManagerReleased = r.InfosetIsManagerReleased,
          InfosetPortalStatusCode = r.InfosetPortalStatusCode,
          InfosetPortalStatusName = r.InfosetPortalStatusName,
          InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
          InfosetDoorsStatusName = r.InfosetDoorsStatusName,
          RecordDiristryGuid = r.RecordDiristryGuidRef,
          RecordDiristryName = r.RecordDiristryName,
          RecordRegistryGuid = r.RecordRegistryGuidRef,
          RecordRegistryName = r.RecordRegistryName,
          RecordDirectoryGuid = r.RecordDirectoryGuidRef,
          RecordDirectoryName = r.RecordDirectoryName,
          RecordRegistrarGuid = r.RecordRegistrarGuidRef,
          RecordRegistrarName = r.RecordRegistrarName,
          ResrepEntityStatusCode = r.InfosetResrepEntityStatusCode,
          ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
          ResrepRecordStatusCode = r.InfosetResrepRecordStatusCode,
          ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
          ResrepInfosetStatusCode = r.InfosetResrepInfosetStatusCode,
          ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
          EntityLabelsCount = r.InfosetEntityLabelsCount,
          EntityLabelsStatusCode = r.InfosetEntityLabelsStatusCode,
          EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
          SupportingTagsCount = r.InfosetSupportingTagsCount,
          SupportingTagsStatusCode = r.InfosetSupportingTagsStatusCode,
          SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
          SupportingLabelsCount = r.InfosetSupportingLabelsCount,
          SupportingLabelsStatusCode = r.InfosetSupportingLabelsStatusCode,
          SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
          CrossReferencesCount = r.InfosetCrossReferencesCount,
          CrossReferencesStatusCode = r.InfosetCrossReferencesStatusCode,
          CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
          OtherTextsCount = r.InfosetOtherTextsCount,
          OtherTextsStatusCode = r.InfosetOtherTextsStatusCode,
          OtherTextsStatusName = r.InfosetOtherTextsStatusName,
          LocationsCount = r.InfosetLocationsCount,
          LocationsStatusCode = r.InfosetLocationsStatusCode,
          LocationsStatusName = r.InfosetLocationsStatusName,
          DescriptionsCount = r.InfosetDescriptionsCount,
          DescriptionsStatusCode = r.InfosetDescriptionsStatusCode,
          DescriptionsStatusName = r.InfosetDescriptionsStatusName,
          ProvenancesCount = r.InfosetProvenancesCount,
          ProvenancesStatusCode = r.InfosetProvenancesStatusCode,
          ProvenancesStatusName = r.InfosetProvenancesStatusName,
          DistributionsCount = r.InfosetDistributionsCount,
          DistributionsStatusCode = r.InfosetDistributionsStatusCode,
          DistributionsStatusName = r.InfosetDistributionsStatusName,
          FairMetricsCount = r.InfosetFairMetricsCount,
          FairMetricsStatusCode = r.InfosetFairMetricsStatusCode,
          FairMetricsStatusName = r.InfosetFairMetricsStatusName,
          NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
          NexusSnapshotsStatusCode = r.InfosetNexusSnapshotsStatusCode,
          NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<NexusResrepEditModel> ListEditableResrepLeafs()
    {
      IQueryable<NexusResrepLeaf> query = InitQueryStorableResrepLeaf();
      IEnumerable<NexusResrepEditModel> list = query.ToEditable().ToList();
      return list;
    }

    public IEnumerable<NexusResrepEditModel> ListEditableResrepLeafs(int pageSize, int pageNumber, out int listCount)
    {
      IQueryable<NexusResrepLeaf> query = InitQueryStorableResrepLeaf();
      listCount = (from NexusResrepLeaf rr in query select rr).Count();
      if (pageSize > 0)
      {
        if (pageNumber > 1)
        {
          query = query.Skip(pageSize * (pageNumber - 1));
        }
        query = query.Take(pageSize);
      }
      IEnumerable<NexusResrepEditModel> list = query.ToEditable().ToList();
      return list;
    }

    public IEnumerable<NexusResrepEditModel> ListEditableResrepLeafsWithAgent(Guid agentKey, int pageSize, int pageNumber, out int listCount)
    {
      IQueryable<NexusResrepLeaf> query = InitQueryStorableResrepLeaf();
      listCount = (from NexusResrepLeaf rr in query select rr).Count();
      if (pageSize > 0)
      {
        if (pageNumber > 1)
        {
          query = query.Skip(pageSize * (pageNumber - 1));
        }
        query = query.Take(pageSize);
      }
      IEnumerable<NexusResrepEditModel> list = query.ToEditable(agentKey).ToList();
      return list;
    }

    public IQueryable<NexusResrepLeaf> QueryNexusResrepLeafByRKey(Guid guidKey)
    {
      IQueryable<NexusResrepLeaf> qry = this.NexusResrepLeafs;
      qry = qry.Where(r => (r.RecordGuidKey == guidKey));
      return qry;
    }
    public IQueryable<NexusResrepLeaf> QueryNexusResrepLeafByIKey(Guid guidKey)
    {
      IQueryable<NexusResrepLeaf> qry = this.NexusResrepLeafs;
      qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
      return qry;
    }

    public NexusResrepLeaf GetStorableResrepLeafByRKey(Guid guidKey)
    { return QueryNexusResrepLeafByRKey(guidKey).SingleOrDefault(); }
    public NexusResrepLeaf GetStorableResrepLeafByIKey(Guid guidKey)
    { return QueryNexusResrepLeafByIKey(guidKey).SingleOrDefault(); }

    public NexusResrepEditModel GetEditableResrepLeafByRKey(Guid guidKey)
    { return QueryNexusResrepLeafByRKey(guidKey).ToEditable().SingleOrDefault(); }
    public Task<NexusResrepEditModel?> GetEditableResrepLeafByRKeyAsync(Guid guidKey)
    { return QueryNexusResrepLeafByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
    public NexusResrepEditModel GetEditableResrepLeafByIKey(Guid guidKey)
    { return QueryNexusResrepLeafByIKey(guidKey).ToEditable().SingleOrDefault(); }
    public Task<NexusResrepEditModel?> GetEditableResrepLeafByIKeyAsync(Guid guidKey)
    { return QueryNexusResrepLeafByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
    public NexusResrepEditModel GetEditableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepLeaf> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryNexusResrepLeafByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryNexusResrepLeafByRKey(guidKey);
      }
      NexusResrepEditModel row = qry.ToEditable().SingleOrDefault();
      return row;
    }
    public NexusResrepEditModel GetEditableResrepLeafByKey(string guidKey, bool isInfosetKey = false)
    {
      return GetEditableResrepLeafByKey(PdpGuid.Parse(guidKey), isInfosetKey);
    }
    public Task<NexusResrepEditModel?> GetEditableResrepLeafByKeyAsync(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepLeaf> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryNexusResrepLeafByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryNexusResrepLeafByRKey(guidKey);
      }
      var row = qry.ToEditable().SingleOrDefaultAsync();
      return row;
    }
    public Task<NexusResrepEditModel?> GetEditableResrepLeafByKeyAsync(string guidKey, bool isInfosetKey = false)
    {
      return GetEditableResrepLeafByKeyAsync(PdpGuid.Parse(guidKey), isInfosetKey);
    }

    public NexusResrepEditModel EditResrepLeaf(NexusResrepEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordHandle = editObj.RecordHandle;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var isNewRecord = recordGuid.IsEmpty();
      NexusResrepLeaf storObj;
      if (isNewRecord)
      {
        // insert new record
        recordGuid = Guid.NewGuid();
        storObj = new NexusResrepLeaf()
        {
          RecordCreatedByAgentGuidRef = agentGuid,
          RecordUpdatedByAgentGuidRef = agentGuid,
          RecordManagedByAgentGuidRef = agentGuid,
          RecordGuidKey = recordGuid,
          InfosetGuidKey = Guid.NewGuid()
        };
      }
      else
      {
        // update existing record
        storObj = GetStorableResrepLeafByRKey(recordGuid);
        storObj.RecordUpdatedByAgentGuidRef = agentGuid;
      }

      // begin common insert/update edit
      if (PRC.ClientHasAdminAccess)
      {
        storObj.RecordDiristryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDiristryGuid, PRC.DiristryGuidDeflt); // Nexus
        storObj.RecordRegistryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistryGuid, PRC.RegistryGuidDeflt); // PORTAL
        storObj.RecordDirectoryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDirectoryGuid, PRC.DirectoryGuidDeflt); // DOORS
        storObj.RecordRegistrarGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistrarGuid, PRC.RegistrarGuidDeflt); // Scribe
      }
      else
      {
        // do not allow change from current request settings for non-admin authors
        storObj.RecordDiristryGuidRef = PdpGuid.ParseToNonNullable(PRC.DiristryGuid, PRC.DiristryGuidDeflt); // Nexus
        storObj.RecordRegistryGuidRef = PdpGuid.ParseToNonNullable(PRC.RegistryGuid, PRC.RegistryGuidDeflt); // PORTAL
        storObj.RecordDirectoryGuidRef = PdpGuid.ParseToNonNullable(PRC.DirectoryGuid, PRC.DirectoryGuidDeflt); // DOORS
        storObj.RecordRegistrarGuidRef = PdpGuid.ParseToNonNullable(PRC.RegistrarGuid, PRC.RegistrarGuidDeflt); // Scribe
      }

      // TODO: must redesign/rebuild to address current redundancy in NPDS scheme with diristry = directory + registry
      // ATTN: current scheme only resets diristry to registry if registry = directory and if diristry invalid/empty
      // assure consistency of current scheme until redesigned and rebuilt
      if (PdpGuid.IsInvalidGuid(storObj.RecordDiristryGuidRef) && (storObj.RecordDirectoryGuidRef == storObj.RecordRegistryGuidRef))
      {
        storObj.RecordDiristryGuidRef = storObj.RecordRegistryGuidRef;
      }

      // allowed for Agents
      storObj.EntityNature = editObj.EntityNature;
      if (string.IsNullOrWhiteSpace(editObj.EntityInitialTag))
      {
        if (string.IsNullOrWhiteSpace(editObj.EntityName)) { editObj.EntityInitialTag = PdpRandom.RandGuidString(); }
        else { editObj.EntityInitialTag = editObj.EntityName.CleanPhrase().CreateAcronym(); }
      }
      //  storObj.EntityInitialTag = editObj.EntityInitialTag;

      // not allowed for Agents
      if (PRC.ClientHasAuthorEditorOrAdminAccess)
      {
        storObj.EntityTypeCodeRef = editObj.EntityTypeCode;
        storObj.EntityName = editObj.EntityName;
        //storObj.EntityOwnerLabel = editObj.EntityOwnerLabel;
        //storObj.EntityContactLabel = editObj.EntityContactLabel;
        //storObj.EntityOtherLabel = editObj.EntityOtherLabel;
        storObj.InfosetIsAuthorPrivate = editObj.InfosetIsAuthorPrivate;
        storObj.InfosetIsAgentShared = editObj.InfosetIsAgentShared;
        storObj.InfosetIsUpdaterLimited = editObj.InfosetIsUpdaterLimited;
        storObj.InfosetIsManagerReleased = editObj.InfosetIsManagerReleased;
      }

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeResrepLeafEdit(agentGuid, storObj.InfosetGuidKey, recordGuid,
          storObj.EntityTypeCodeRef, storObj.EntityName, storObj.EntityNature,
          // storObj.EntityOwnerLabel, storObj.EntityContactLabel, storObj.EntityOtherLabel,
          storObj.RecordDiristryGuidRef, storObj.RecordRegistryGuidRef, storObj.RecordDirectoryGuidRef, storObj.RecordRegistrarGuidRef,
          storObj.InfosetIsAuthorPrivate, storObj.InfosetIsAgentShared, storObj.InfosetIsUpdaterLimited, storObj.InfosetIsManagerReleased);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusResrepLeafs.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableResrepLeafByRKey(recordGuid);
      if (editObj == null) { editObj = new NexusResrepEditModel(); }
      // refresh the record handle
      recordHandle = editObj.RecordHandle;
      // update the status message
      if (string.IsNullOrEmpty(errMsg))
      {
        editObj.PdpStatusMessage =
          $"{recordName} record with handle {recordHandle} written to database"; editObj.PdpStatusItemStored = true;
      }
      else { editObj.PdpStatusMessage = errMsg; }
      return editObj;
    }

    public NexusResrepEditModel DeleteResrepLeaf(NexusResrepEditModel editObj, bool byStorProc = true)
    {
      var errorMessage = string.Empty;
      var recordMessage = $" {editObj.ItemXnam} record ";
      var agentGuid = PRC.AgentGuid;
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var isNewRecord = recordGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableResrepLeafByRKey(recordGuid);
        if (storObj == null) { errorMessage = $"Database error while getting {recordMessage}"; }
        else
        {
          recordMessage = $" {recordMessage} with handle {storObj.RecordHandle} ";
          storObj.RecordDeletedByAgentGuidRef = agentGuid;
          storObj.RecordIsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
          if (byStorProc)
          {
            var errorCode = ScribeResrepLeafDelete(
            storObj.RecordDeletedByAgentGuidRef, storObj.RecordGuidKey, storObj.RecordIsDeleted);
            if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
          }
          else
          {
            this.NexusResrepLeafs.Attach(storObj);
            this.NexusResrepLeafs.Remove(storObj);
            errorMessage = StoreChanges();
          }
        }
        // refresh the edit object
        editObj = GetEditableResrepLeafByKey(recordGuid);
        if (editObj == null) { editObj = new NexusResrepEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errorMessage)) { editObj.PdpStatusMessage = $"{recordMessage} deleted from database"; }
        else { editObj.PdpStatusMessage = errorMessage; }
      }
      return editObj;
    }

  }

}