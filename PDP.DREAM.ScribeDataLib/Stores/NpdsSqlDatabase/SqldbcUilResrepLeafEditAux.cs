// SqldbcUilResrepLeafEditAux.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Data.SqlClient;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Models;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public NexusResrepEditModel ValidateUpdateResrepLeaf(NexusResrepEditModel editObj)
  {
    var errCod = 0; var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = PRC.AgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    NexusResrepLeaf storObj;
    if (isNewRecord)
    {
      // cannot validate a non-existent record
      errMsg = "Record not validated: missing record identifier.";
    }
    else
    {
      // validate existing record
      try
      {
        storObj = GetStorableResrepLeafByRKey(recordGuid);
        storObj.RecordUpdatedByAgentGuidRef = agentGuid;
        // ResRep parent
        storObj.InfosetResrepEntityStatusCode = ValidateResrepEntity(ref editObj);
        storObj.InfosetResrepRecordStatusCode = ValidateResrepRecord(ref editObj);
        storObj.InfosetResrepInfosetStatusCode = ValidateResrepInfoset(ref editObj);
        // PORTAL children
        storObj.InfosetEntityLabelsStatusCode = CheckEntityLabels(ref editObj);
        storObj.InfosetSupportingTagsStatusCode = CheckSupportingTags(ref editObj);
        storObj.InfosetSupportingLabelsStatusCode = CheckSupportingLabels(ref editObj);
        storObj.InfosetCrossReferencesStatusCode = CheckCrossReferences(ref editObj);
        storObj.InfosetOtherTextsStatusCode = CheckOtherTexts(ref editObj);
        // DOORS children
        storObj.InfosetLocationsStatusCode = CheckLocations(ref editObj);
        storObj.InfosetDescriptionsStatusCode = CheckDescriptions(ref editObj);
        storObj.InfosetProvenancesStatusCode = CheckProvenances(ref editObj);
        storObj.InfosetDistributionsStatusCode = CheckDistributions(ref editObj);
        storObj.InfosetFairMetricsStatusCode = CheckFairMetrics(ref editObj);
        storObj.InfosetNexusSnapshotsStatusCode = CheckNexusSnapshots(ref editObj);
        // PORTAL-DOORS status (ATTN: note interdependencies/updates)
        storObj.InfosetPortalStatusCode = ValidatePortalStatus(ref editObj);
        storObj.InfosetDoorsStatusCode = ValidateDoorsStatus(ref editObj);
        // update Scribe database record
        errCod = (int)ScribeResrepStatusCountsUpdate(agentGuid, recordGuid,
          storObj.InfosetResrepEntityStatusCode, storObj.InfosetResrepRecordStatusCode, storObj.InfosetResrepInfosetStatusCode,
          storObj.InfosetEntityLabelsStatusCode, storObj.InfosetSupportingTagsStatusCode, storObj.InfosetSupportingLabelsStatusCode, storObj.InfosetCrossReferencesStatusCode, storObj.InfosetOtherTextsStatusCode,
          storObj.InfosetLocationsStatusCode, storObj.InfosetDescriptionsStatusCode, storObj.InfosetProvenancesStatusCode, storObj.InfosetDistributionsStatusCode,
          storObj.InfosetFairMetricsStatusCode, storObj.InfosetNexusSnapshotsStatusCode, storObj.InfosetPortalStatusCode, storObj.InfosetDoorsStatusCode);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
        else
        {
          var portalStatus = PdpEnum<NpdsConst.InfosetStatus>.ToEnum(storObj.InfosetPortalStatusCode).ToString();
          var doorsStatus = PdpEnum<NpdsConst.InfosetStatus>.ToEnum(storObj.InfosetDoorsStatusCode).ToString();
          errMsg = $"Record status at PORTAL = {portalStatus} and at DOORS = {doorsStatus}";
        }
      }
      catch (Exception error) when (error is SqlException)
      {
        errMsg = $"Record not validated: server database error {error.Message}";
      }
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

  public NexusResrepEditModel RequestReleaseResrepRecord(NexusResrepEditModel editObj)
  {
    var errMsg = string.Empty;
    var reqrel = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = PRC.AgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    if (!isNewRecord) // check existing record
    {
      var errCod = ScribeResrepAuthorRequestEdit(agentGuid, agentGuid, infosetGuid, recordGuid, null, false, false);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing {recordName} record to database"; }

      if (string.IsNullOrEmpty(errMsg))
      {
        reqrel = ((editObj.ManagedByAgentGuid == PRC.AgentGuid) ? "released" : "requested");
        editObj.PdpStatusMessage = $"Authorship for record {recordHandle} has been {reqrel}";
        editObj.PdpStatusName = $"<span class='pdpStatusValid'>{reqrel}</span>";
      }
      else
      {
        editObj.PdpStatusMessage = errMsg;
        editObj.PdpStatusName = "<span class='pdpStatusInvalid'>error</span>";
      }
    }
    return editObj;
  }

  public NexusResrepEditModel ArchiveResrepRecord(NexusResrepEditModel editObj)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = PRC.AgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    if (isNewRecord)
    {
      errMsg = "Record not archived: missing record handle.";
    }
    else  // validate existing record
    {
      var archObj = new NexusSnapshotEditModel()
      {
        RRRecordGuid = recordGuid
      };
      try
      {
        archObj = EditSnapshot(archObj);
        errMsg = "been archived successfully.";
      }
      catch
      {
        errMsg = "not been archived; a server database error occurred.";
      }
    }
    editObj.PdpStatusMessage = $"{recordName} record with handle {recordHandle} has {errMsg}";
    return editObj;
  }


  //  ResRep Entity Metadata in parent, not children
  public virtual short ValidateResrepEntity(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepEntity(ref rrr);
  }
  public virtual short ValidateResrepEntity(ref NexusResrepEditModel rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    IEnumerable<string> suppStrings = (new List<string>() { rrr.EntityName, rrr.EntityNature }).Select(st => st.ToLower());
    var statusCode = CheckSupportingStrings(suppStrings, registryGuid);
    rrr.ResrepEntityStatusCode = statusCode;
    return statusCode;
  }

  //  ResRep Record Metadata in parent, not children
  public virtual short ValidateResrepRecord(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepRecord(ref rrr);
  }
  public virtual short ValidateResrepRecord(ref NexusResrepEditModel rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusCode = (short)NpdsConst.InfosetStatus.None;
    rrr.ResrepRecordStatusCode = statusCode;
    return statusCode;
  }

  //  ResRep Infoset Metadata in parent, not children
  public virtual short ValidateResrepInfoset(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepInfoset(ref rrr);
  }
  public virtual short ValidateResrepInfoset(ref NexusResrepEditModel rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusCode = (short)NpdsConst.InfosetStatus.None;
    rrr.ResrepInfosetStatusCode = statusCode;
    return statusCode;
  }

  //  ResRep metadata in Nexus parent for PORTAL status
  public virtual short ValidatePortalStatus(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidatePortalStatus(ref rrr);
  }
  public virtual short ValidatePortalStatus(ref NexusResrepEditModel rrr)
  {
    var statusEnum = NpdsConst.InfosetStatus.Invalid;
    var resc = (NpdsConst.InfosetStatus)rrr.ResrepEntityStatusCode;
    var stsc = (NpdsConst.InfosetStatus)rrr.SupportingTagsStatusCode;
    var slsc = (NpdsConst.InfosetStatus)rrr.SupportingLabelsStatusCode;
    if ((rrr.EntityLabelsCount > 0) && ((resc == NpdsConst.InfosetStatus.ConceptValid) ||
      (stsc == NpdsConst.InfosetStatus.ConceptValid) || (slsc == NpdsConst.InfosetStatus.ConceptValid)))
    { statusEnum = NpdsConst.InfosetStatus.Valid; }
    var statusCode = (short)statusEnum;
    rrr.InfosetPortalStatusCode = statusCode;
    return statusCode;
  }

  //  ResRep metadata in Nexus parent for DOORS status
  public virtual short ValidateDoorsStatus(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateDoorsStatus(ref rrr);
  }
  public virtual short ValidateDoorsStatus(ref NexusResrepEditModel rrr)
  { // old "generous" approach to DOORS validity
    // validate presence of at least one confirmed address
    //  as either a validated UrlWebAddress, EmailAddress, StreetAddress
    //  or at least one EntityLabel URI that is resolveable as URL
    //  but only for entities that are not components (not NPDS network node servers)
    //var statusEnum = NpdsConstants.InfosetStatus.Invalid;
    //var etc = rrr.EntityTypeCode;
    //var lsc = (NpdsConstants.InfosetStatus)rrr.LocationsStatusCode;
    //if ((rrr.EntityLabelsCount > 0) &&
    //  (lsc == NpdsConstants.InfosetStatus.AddressValid))
    //{ statusEnum = NpdsConstants.InfosetStatus.Valid; }
    //// TODO: use EntityTypeIsComponent flag
    //if ((statusEnum != NpdsConstants.InfosetStatus.Valid) && (etc == 0 || etc >= 60))
    //{
    //  var recordGuid = (Guid)rrr.RRRecordGuid;
    //  var elabels = ListEditableEntityLabels(recordGuid);
    //  foreach (EntityLabelEditModel el in elabels)
    //  {
    //    if (!string.IsNullOrEmpty(el.EntityLabel) && (el.IsResolvable == true) && (el.EntityLabel.UrlIsValid() == true))
    //    {
    //      statusEnum = NpdsConstants.InfosetStatus.Valid;
    //      break;
    //    }
    //  }
    //}
    //
    // currrent "strict" approach to DOORS validity
    var statusEnum = NpdsConst.InfosetStatus.Invalid;
    var lsc = (NpdsConst.InfosetStatus)rrr.LocationsStatusCode;
    if (lsc == NpdsConst.InfosetStatus.AddressValid)
    { statusEnum = NpdsConst.InfosetStatus.Valid; }
    var statusCode = (short)statusEnum;
    rrr.InfosetDoorsStatusCode = statusCode;
    return statusCode;
  }

  // private method for use by ValidateSupportingTags and ValidateSupportingLabels
  private short CheckSupportingStrings(string ss, Guid registryGuid)
  {
    var sss = new List<string>() { ss };
    return CheckSupportingStrings(sss, registryGuid);
  }
  private short CheckSupportingStrings(IEnumerable<string> sss, Guid registryGuid)
  {
    var statusCode = NpdsConst.InfosetStatus.None;
    var restrctAnds = ListViewableRestrictionAnds(registryGuid);
    int restrctCount = restrctAnds.Count();
    bool stringsAreValid = false;
    if (restrctCount > 0)
    {
      bool andsAreValid = true;
      // Concept Validity requires that all restrictionAnd concepts are true
      // each restrictionAnd concept is true if at least one of its restrictionOr concepts are true
      // assume the Ands are true and exit as soon as first And is false
      foreach (RestrictionAndViewModel rAnd in restrctAnds)
      {
        // select the Ors for the current And
        var restrctAndOrs = ListViewableRestrictionOrsByAnd(rAnd.RestrictionAndGuidKey);
        // assume the Ors are false and exit as soon as first Or is true
        bool orsAreValid = false;
        foreach (RestrictionOrViewModel rOr in restrctAndOrs)
        {
          var restrct = rOr.Restriction.ToLower();
          foreach (string s in sss)
          {
            if (s.Contains(restrct)) { orsAreValid = true; }
            if (orsAreValid == true) { break; }
          }
          if (orsAreValid == true) { break; }
        }
        // if all of the Ors for a given insufficient And are false, then that And must be false
        // otherwise ignore and continue testing other Ands
        if (orsAreValid == false && rAnd.RestrictionIsSufficient == false) { andsAreValid = false; }
        // if current And is false and IsSufficient is false then unnecessary to test other Ands
        // if current And is true and IsSufficient is true then unnecessary to test other Ands
        if ((andsAreValid == false && rAnd.RestrictionIsSufficient == false) //
          || (andsAreValid == true && rAnd.RestrictionIsSufficient == true)) { break; }
      }
      // if (all of the Ands are true) or (one of the IsSufficient Ands are true) then consider the tags valid
      if (andsAreValid == true) { stringsAreValid = true; }
    }
    // if no restrictions, then define by default as ConceptValid
    if (stringsAreValid == true) { statusCode = NpdsConst.InfosetStatus.ConceptValid; }
    else { statusCode = NpdsConst.InfosetStatus.ConceptInvalid; }
    return (short)statusCode;
  }
  private short CheckSupportingStrings(IEnumerable<string> sss, Guid registryGuid, bool isLabel)
  {
    var restrctAnds = ListViewableRestrictionAnds(registryGuid);
    var restrctOrs = ListViewableRestrictionOrs(registryGuid, isLabel);
    return CheckSupportingStrings(sss, restrctAnds, restrctOrs);
  }
  private short CheckSupportingStrings(IEnumerable<string> sss, IEnumerable<RestrictionAndViewModel> restrctAnds, IEnumerable<RestrictionOrViewModel> restrctOrs)
  {
    var statusCode = NpdsConst.InfosetStatus.ConceptInvalid;
    int restrctCount = restrctAnds.Count();
    bool stringsAreValid = false;
    if (restrctCount > 0)
    {
      // Concept Validity requires that all restrictionAnd concepts are true
      // each restrictionAnd concept is true if at least one of its restrictionOr concepts are true
      // assume the Ands are true and exit as soon as first And is false
      bool andsAreValid = true;
      foreach (RestrictionAndViewModel rAnd in restrctAnds)
      {
        // select the Ors for the current And
        var restrctAndOrs = restrctOrs.Where(rOr => (rOr.RestrictionAndGuidRef == rAnd.RestrictionAndGuidKey));
        // assume the Ors are false and exit as soon as first Or is true
        bool orsAreValid = false;
        foreach (RestrictionOrViewModel rOr in restrctAndOrs)
        {
          var restrct = rOr.Restriction.ToLower();
          if (sss.Any(restrct.Contains)) { orsAreValid = true; }
          if (orsAreValid == true) { break; }
        }
        // if all of the Ors for a given And are false, then that And must be false
        if (orsAreValid == false) { andsAreValid = false; }
        // if current And is false then unnecessary to test other Ands
        // if And is true and marked as IsSufficient then unnecessary to test other Ands
        if ((andsAreValid == false) || (andsAreValid == true && rAnd.RestrictionIsSufficient == true)) { break; }
      }
      // if (all of the Ands are true) or (one of the IsSufficient Ands are true) then consider the tags valid
      if (andsAreValid == true) { stringsAreValid = true; }
    }
    // if no restrictions, then define by default as ConceptValid
    if ((restrctCount == 0) || (stringsAreValid == true)) { statusCode = NpdsConst.InfosetStatus.ConceptValid; }
    return (short)statusCode;
  }

}
