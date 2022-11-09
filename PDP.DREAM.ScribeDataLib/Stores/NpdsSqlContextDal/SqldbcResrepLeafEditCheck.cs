// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Data.SqlClient;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public NexusResrepEditModel ValidateUpdateResrepLeaf(NexusResrepEditModel editObj)
  {
    var errCod = 0;
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = QURC.QebAgentGuid;
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
        storObj = (NexusResrepLeaf)GetStorableNexusLeafByRKey(recordGuid);
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
        storObj.InfosetNexusSnapshotsStatusCode = CheckSnapshots(ref editObj);
        // PORTAL-DOORS status (ATTN: note interdependencies/updates)
        storObj.InfosetPortalStatusCode = ValidatePortalStatus(ref editObj);
        storObj.InfosetDoorsStatusCode = ValidateDoorsStatus(ref editObj);
        // update Scribe database record
        errCod = (int)ScribeResrepStatusCountsUpdate(agentGuid, recordGuid,
          storObj.InfosetResrepEntityStatusCode, storObj.InfosetResrepRecordStatusCode,
          storObj.InfosetResrepInfosetStatusCode, storObj.InfosetEntityLabelsStatusCode,
          storObj.InfosetSupportingTagsStatusCode, storObj.InfosetSupportingLabelsStatusCode,
          storObj.InfosetCrossReferencesStatusCode, storObj.InfosetOtherTextsStatusCode,
          storObj.InfosetLocationsStatusCode, storObj.InfosetDescriptionsStatusCode,
          storObj.InfosetProvenancesStatusCode, storObj.InfosetDistributionsStatusCode,
          storObj.InfosetFairMetricsStatusCode, storObj.InfosetNexusSnapshotsStatusCode,
          storObj.InfosetPortalStatusCode, storObj.InfosetDoorsStatusCode);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
        else
        {
          var portalStatus = PdpEnum<PdpAppConst.NpdsInfosetStatus>.ParseNumeric(storObj.InfosetPortalStatusCode).ToString();
          var doorsStatus = PdpEnum<PdpAppConst.NpdsInfosetStatus>.ParseNumeric(storObj.InfosetDoorsStatusCode).ToString();
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
    var statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
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
    var statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
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
    var statusEnum = PdpAppConst.NpdsInfosetStatus.Invalid;
    var resc = (PdpAppConst.NpdsInfosetStatus)rrr.ResrepEntityStatusCode;
    var stsc = (PdpAppConst.NpdsInfosetStatus)rrr.SupportingTagsStatusCode;
    var slsc = (PdpAppConst.NpdsInfosetStatus)rrr.SupportingLabelsStatusCode;
    if ((rrr.EntityLabelsCount > 0) && ((resc == PdpAppConst.NpdsInfosetStatus.ConceptValid) ||
      (stsc == PdpAppConst.NpdsInfosetStatus.ConceptValid) || (slsc == PdpAppConst.NpdsInfosetStatus.ConceptValid)))
    { statusEnum = PdpAppConst.NpdsInfosetStatus.Valid; }
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
    var statusEnum = PdpAppConst.NpdsInfosetStatus.Invalid;
    var lsc = (PdpAppConst.NpdsInfosetStatus)rrr.LocationsStatusCode;
    if (lsc == PdpAppConst.NpdsInfosetStatus.AddressValid)
    { statusEnum = PdpAppConst.NpdsInfosetStatus.Valid; }
    var statusCode = (short)statusEnum;
    rrr.InfosetDoorsStatusCode = statusCode;
    return statusCode;
  }

  // private method for use by ValidateSupportingTags and ValidateSupportingLabels
  // TODO: extend checks for concept-validation other PORTAL infosubsets
  // TODO: extend checks for concept-validation to other DOORS infosubsets
  private short CheckSupportingStrings(string ss, Guid registryGuid)
  {
    var sss = new List<string>() { ss };
    return CheckSupportingStrings(sss, registryGuid);
  }
  private short CheckSupportingStrings(IEnumerable<string> sss, Guid registryGuid)
  {
    NpdsInfosetStatus statusEnum = NpdsInfosetStatus.None;
    bool includeConceptsPresent = CheckPresenceRestrictionStrings(sss, registryGuid, false);
    bool excludeConceptsPresent = CheckPresenceRestrictionStrings(sss, registryGuid, true);
    // if no restrictions, then define by default as ConceptValid
    if (includeConceptsPresent && !excludeConceptsPresent)
    { statusEnum = NpdsInfosetStatus.ConceptValid; }
    else
    { statusEnum = NpdsInfosetStatus.ConceptInvalid; }
    var statusCode = (short)statusEnum;
    return statusCode;
  }

  private bool CheckPresenceRestrictionStrings(IEnumerable<string> sss, Guid registryGuid, bool isExcluding)
  {
    bool stringsArePresent = false;
    IEnumerable<ServiceRestrictionAndEditModel>? restrctAnds = ListEditableRestrictionAndsByIGuid(registryGuid, isExcluding);
    int restrctCount = restrctAnds.Count();
    if (restrctCount > 0)
    {
      bool andsArePresent = true;
      // Concept Validity requires that all restrictionAnd concepts are true
      // each restrictionAnd concept is true if at least one of its restrictionOr concepts are true
      // assume the Ands are true and exit as soon as first And is false
      foreach (ServiceRestrictionAndEditModel rAnd in restrctAnds)
      {
        // select the Ors for the current And
        var rAndGuid = (Guid)rAnd.RRFgroupGuid;
        var restrctAndOrs = ListEditableRestrictionOrsByAndGuid(rAndGuid);
        // assume the Ors are false and exit as soon as first Or is true
        bool orsArePresent = false;
        foreach (ServiceRestrictionOrEditModel rOr in restrctAndOrs)
        {
          var restrct = rOr.RestrictionValue.ToLower();
          foreach (string s in sss)
          {
            if (s.Contains(restrct)) { orsArePresent = true; }
            if (orsArePresent == true) { break; }
          }
          if (orsArePresent == true) { break; }
        }
        // if all of the Ors for a given And are false, then that And must be false
        // otherwise ignore and continue testing other Ands
        if (orsArePresent == false) { andsArePresent = false; }
        // if current And is true and IsSufficient is true then unnecessary to test other Ands
        if (andsArePresent == true && rAnd.IsSufficient == true) { break; }
      }
      // if (all of the Ands are true) or (one of the IsSufficient Ands are true)
      // then consider the strings present
      if (andsArePresent == true) { stringsArePresent = true; }
    }
    return stringsArePresent;
  }

}
