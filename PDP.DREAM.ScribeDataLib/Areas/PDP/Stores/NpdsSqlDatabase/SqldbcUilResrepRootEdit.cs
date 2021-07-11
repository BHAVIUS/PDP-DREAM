// SqldbcUilResrepRootEdit.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusResrepEditModel ToEditable(this NexusResrepRoot r, Guid agentGuidRef = default)
    {
      var nre = new NexusResrepEditModel()
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
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        RecordDiristryName = r.RecordDiristryTag,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryName = r.RecordRegistryTag,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryName = r.RecordDirectoryTag,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarName = r.RecordRegistrarTag
      };
      return nre;
    }

    public static IQueryable<NexusResrepEditModel> ToEditable(this IQueryable<NexusResrepRoot> qry, Guid agentGuidRef = default)
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
          EntityName = r.EntityName,
          EntityNature = r.EntityNature,
          InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
          InfosetIsAgentShared = r.InfosetIsAgentShared,
          InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
          InfosetIsManagerReleased = r.InfosetIsManagerReleased,
          RecordDiristryGuid = r.RecordDiristryGuidRef,
          RecordDiristryName = r.RecordDiristryTag,
          RecordRegistryGuid = r.RecordRegistryGuidRef,
          RecordRegistryName = r.RecordRegistryTag,
          RecordDirectoryGuid = r.RecordDirectoryGuidRef,
          RecordDirectoryName = r.RecordDirectoryTag,
          RecordRegistrarGuid = r.RecordRegistrarGuidRef,
          RecordRegistrarName = r.RecordRegistrarTag
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {

    public IEnumerable<NexusResrepEditModel> ListEditableResrepRoots(DataSourceRequest dsRequest, out int listCount)
    {
      var pageSize = dsRequest.PageSize;
      var pageNumber = dsRequest.Page;
      IQueryable<NexusResrepRoot> query = InitQueryStorableResrepRoot();
      foreach (FilterDescriptor filterDescriptor in dsRequest.Filters)
      {
        var filterMember = filterDescriptor.Member;
        var filterOperator = filterDescriptor.Operator;
        var filterValue = filterDescriptor.Value;
        var filterValueConverted = filterDescriptor.ConvertedValue;
        switch (filterMember)
        {
          case "RecordHandle":
            if (filterOperator == FilterOperator.IsEqualTo)
            { query = query.Where(rr => rr.RecordHandle.Equals((string)filterValue)); }
            else
            { query = query.Where(rr => rr.RecordHandle.Contains((string)filterValue)); }
            break;
          case "EntityTypeCode":
            if (filterOperator == FilterOperator.IsEqualTo)
            { query = query.Where(rr => rr.EntityTypeName.Equals((string)filterValue)); }
            else
            { query = query.Where(rr => rr.EntityTypeName.Contains((string)filterValue)); }
            break;
          case "EntityName":
            if (filterOperator == FilterOperator.IsEqualTo)
            { query = query.Where(rr => rr.EntityName.Equals((string)filterValue)); }
            else
            { query = query.Where(rr => rr.EntityName.Contains((string)filterValue)); }
            break;
          case "EntityNature":
            if (filterOperator == FilterOperator.IsEqualTo)
            { query = query.Where(rr => rr.EntityNature.Equals((string)filterValue)); }
            else
            { query = query.Where(rr => rr.EntityNature.Contains((string)filterValue)); }
            break;
          case "UpdatedOn":
            if (filterOperator == FilterOperator.IsEqualTo)
            { query = query.Where(rr => (rr.RecordUpdatedOn.Value == (DateTime)filterValue)); }
            else if (filterOperator == FilterOperator.IsGreaterThanOrEqualTo)
            { query = query.Where(rr => (rr.RecordUpdatedOn.Value >= (DateTime)filterValue)); }
            else if (filterOperator == FilterOperator.IsLessThanOrEqualTo)
            { query = query.Where(rr => (rr.RecordUpdatedOn.Value <= (DateTime)filterValue)); }
            break;
          default:
            break;
        }
      }
      listCount = (from NexusResrepRoot rr in query select rr).Count();
      if (listCount > 0)
      {
        if (dsRequest.Sorts.Count > 0)
        {
          var sortMember = dsRequest.Sorts[0].Member;
          var sortDirection = dsRequest.Sorts[0].SortDirection;
          switch (sortMember)
          {
            case "RecordHandle":
              if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
              { query = query.OrderBy(rr => rr.RecordHandle); }
              else
              { query = query.OrderByDescending(rr => rr.RecordHandle); }
              break;
            case "EntityTypeCode":
              if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
              { query = query.OrderBy(rr => rr.EntityTypeName); }
              else
              { query = query.OrderByDescending(rr => rr.EntityTypeName); }
              break;
            case "EntityName":
              if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
              { query = query.OrderBy(rr => rr.EntityName); }
              else
              { query = query.OrderByDescending(rr => rr.EntityName); }
              break;
            case "EntityNature":
              if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
              { query = query.OrderBy(rr => rr.EntityNature); }
              else
              { query = query.OrderByDescending(rr => rr.EntityNature); }
              break;
            case "UpdatedOn":
              if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
              { query = query.OrderBy(rr => rr.RecordUpdatedOn); }
              else
              { query = query.OrderByDescending(rr => rr.RecordUpdatedOn); }
              break;
            default:
              break;
          }
        }
        if (pageSize > 0)
        {
          if (pageNumber > 1)
          {
            query = query.Skip(pageSize * (pageNumber - 1));
          }
          query = query.Take(pageSize);
        }
      }
      var agentGuid = PRC.AgentGuid;
      IEnumerable<NexusResrepEditModel> list = query.ToEditable(agentGuid).ToList();
      return list;
    }

    public NexusResrepEditModel GetEditableResrepRootByRKey(Guid guidKey)
    { return QueryStorableResrepRootByRKey(guidKey).ToEditable().SingleOrDefault(); }
    public Task<NexusResrepEditModel?> GetEditableResrepRootByRKeyAsync(Guid guidKey)
    { return QueryStorableResrepRootByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
    public NexusResrepEditModel GetEditableResrepRootByIKey(Guid guidKey)
    { return QueryStorableResrepRootByIKey(guidKey).ToEditable().SingleOrDefault(); }
    public Task<NexusResrepEditModel?> GetEditableResrepRootByIKeyAsync(Guid guidKey)
    { return QueryStorableResrepRootByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
    public NexusResrepEditModel GetEditableResrepRootByKey(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepRoot> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryStorableResrepRootByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryStorableResrepRootByRKey(guidKey);
      }
      NexusResrepEditModel row = qry.ToEditable().SingleOrDefault();
      return row;
    }
    public NexusResrepEditModel GetEditableResrepRootByKey(string guidKey, bool isInfosetKey = false)
    {
      return GetEditableResrepRootByKey(PdpGuid.Parse(guidKey), isInfosetKey);
    }
    public Task<NexusResrepEditModel?> GetEditableResrepRootByKeyAsync(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepRoot> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryStorableResrepRootByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryStorableResrepRootByRKey(guidKey);
      }
      var row = qry.ToEditable().SingleOrDefaultAsync();
      return row;
    }
    public Task<NexusResrepEditModel?> GetEditableResrepRootByKeyAsync(string guidKey, bool isInfosetKey = false)
    {
      return GetEditableResrepRootByKeyAsync(PdpGuid.Parse(guidKey), isInfosetKey);
    }

    public NexusResrepEditModel EditResrepRoot(NexusResrepEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordHandle = editObj.RecordHandle;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var isNewRecord = recordGuid.IsEmpty();
      NexusResrepRoot storObj;
      if (isNewRecord)
      {
        // insert new record
        recordGuid = PdpGuid.NewGuid();
        infosetGuid = PdpGuid.NewGuid();
        storObj = new NexusResrepRoot()
        {
          RecordCreatedByAgentGuidRef = agentGuid,
          RecordUpdatedByAgentGuidRef = agentGuid,
          RecordManagedByAgentGuidRef = agentGuid,
          RecordGuidKey = recordGuid,
          InfosetGuidKey = infosetGuid
        };
      }
      else
      {
        // update existing record
        storObj = GetStorableResrepRootByRKey(recordGuid);
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

      // max chars for gridcol display and database store
      // EntityTag 32 / 64 
      // EntityName 64 / 256
      // EntityNature 128 / 1024

      // allowed for Agents
      storObj.EntityNature = editObj.EntityNature.ParseLeft(1024);
      if (string.IsNullOrWhiteSpace(editObj.EntityInitialTag))
      {
        if (string.IsNullOrWhiteSpace(editObj.EntityName)) { editObj.EntityInitialTag = PdpRandom.RandGuidString(); }
        else { editObj.EntityInitialTag = editObj.EntityName.CleanPhrase().CreateAcronym(); }
      }
      storObj.EntityInitialTag = editObj.EntityInitialTag.ParseLeft(64);

      // not allowed for Agents
      if (PRC.ClientHasAuthorEditorOrAdminAccess)
      {
        storObj.EntityTypeCodeRef = editObj.EntityTypeCode;
        storObj.EntityName = editObj.EntityName.ParseLeft(256);
        storObj.InfosetIsAuthorPrivate = editObj.InfosetIsAuthorPrivate;
        storObj.InfosetIsAgentShared = editObj.InfosetIsAgentShared;
        storObj.InfosetIsUpdaterLimited = editObj.InfosetIsUpdaterLimited;
        storObj.InfosetIsManagerReleased = editObj.InfosetIsManagerReleased;
      }

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeResrepRootEdit(agentGuid, storObj.InfosetGuidKey, recordGuid,
          storObj.EntityTypeCodeRef, storObj.EntityInitialTag, storObj.EntityName, storObj.EntityNature,
          storObj.RecordDiristryGuidRef, storObj.RecordRegistryGuidRef, storObj.RecordDirectoryGuidRef, storObj.RecordRegistrarGuidRef,
          storObj.InfosetIsAuthorPrivate, storObj.InfosetIsAgentShared, storObj.InfosetIsUpdaterLimited, storObj.InfosetIsManagerReleased);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusResrepRoots.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableResrepRootByRKey(recordGuid);
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

    public NexusResrepEditModel DeleteResrepRoot(NexusResrepEditModel editObj, bool byStorProc = true)
    {
      var errorMessage = string.Empty;
      var recordMessage = $" {editObj.ItemXnam} record ";
      var agentGuid = PRC.AgentGuid;
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var isNewRecord = recordGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableResrepRootByRKey(recordGuid);
        if (storObj == null) { errorMessage = $"Database error while getting {recordMessage}"; }
        else
        {
          recordMessage = $" {recordMessage} with handle {storObj.RecordHandle} ";
          storObj.RecordDeletedByAgentGuidRef = agentGuid;
          storObj.RecordIsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
          if (byStorProc)
          {
            var errorCode = ScribeResrepRootDelete(
            storObj.RecordDeletedByAgentGuidRef, storObj.RecordGuidKey, storObj.RecordIsDeleted);
            if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
          }
          else
          {
            this.NexusResrepRoots.Attach(storObj);
            this.NexusResrepRoots.Remove(storObj);
            errorMessage = StoreChanges();
          }
        }
        // refresh the edit object
        editObj = GetEditableResrepRootByKey(recordGuid);
        if (editObj == null) { editObj = new NexusResrepEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errorMessage)) { editObj.PdpStatusMessage = $"{recordMessage} deleted from database"; }
        else { editObj.PdpStatusMessage = errorMessage; }
      }
      return editObj;
    }

  }

}