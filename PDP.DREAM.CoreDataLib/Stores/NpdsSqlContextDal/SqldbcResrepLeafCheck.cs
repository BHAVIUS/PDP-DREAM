// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public CoreResrepLeafUxm ValidateUpdateResrepLeaf(CoreResrepLeafUxm editObj)
  {
    var errCod = 0;
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var isNewRecord = recordGuid.IsEmpty();
    CoreResrepLeaf storObj;
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
        storObj = (CoreResrepLeaf)GetStorableResrepLeafByRKey(recordGuid);
        storObj.RecordUpdatedByAgentGuid = agentGuid;
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
          var portalStatusEnum = NPDSCD.ParseInfosetStatus((byte)storObj.InfosetPortalStatusCode);
          var doorsStatusEnum = NPDSCD.ParseInfosetStatus((byte)storObj.InfosetDoorsStatusCode);
          errMsg = $"Record status at PORTAL = {portalStatusEnum.EName} and at DOORS = {doorsStatusEnum.EName}";
        }
      }
      catch (Exception error) when (error is SqlException)
      {
        errMsg = $"Record not validated: server database error {error.Message}";
      }
    }
    // refresh the edit object
    editObj = GetEditableResrepLeafByRKey(recordGuid);
    if (editObj == null) { editObj = new CoreResrepLeafUxm(); }
    // refresh the record handle
    recordHandle = editObj.RecordHandle;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg =
        $"{recordName} record with handle {recordHandle} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  //  ResRep Entity Metadata in parent, not children
  public virtual byte ValidateResrepEntity(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepEntity(ref rrr);
  }
  public virtual byte ValidateResrepEntity(ref CoreResrepLeafUxm rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    IEnumerable<string> suppStrings = (new List<string>() { rrr.EntityName, rrr.EntityNature }).Select(st => st.ToLower());
    var statusCode = CheckSupportingStrings(suppStrings, registryGuid);
    rrr.ResrepEntityStatusCode = statusCode;
    return statusCode;
  }

  //  ResRep Record Metadata in parent, not children
  public virtual byte ValidateResrepRecord(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepRecord(ref rrr);
  }
  public virtual byte ValidateResrepRecord(ref CoreResrepLeafUxm rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusEnum = NPDSCD.InfosetStatusNone;
    rrr.ResrepInfosetStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  //  ResRep Infoset Metadata in parent, not children
  public virtual byte ValidateResrepInfoset(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateResrepInfoset(ref rrr);
  }
  public virtual byte ValidateResrepInfoset(ref CoreResrepLeafUxm rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusEnum = NPDSCD.InfosetStatusNone;
    rrr.ResrepInfosetStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  //  ResRep metadata in Nexus parent for PORTAL status
  public virtual byte ValidatePortalStatus(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidatePortalStatus(ref rrr);
  }
  public virtual byte ValidatePortalStatus(ref CoreResrepLeafUxm rrr)
  {
    var statusEnum = NPDSCD.InfosetStatusInvalid;
    var resc = NPDSCD.ParseInfosetStatus(rrr.ResrepEntityStatusCode);
    var stsc = NPDSCD.ParseInfosetStatus(rrr.SupportingTagsStatusCode);
    var slsc = NPDSCD.ParseInfosetStatus(rrr.SupportingLabelsStatusCode);
    if ((rrr.EntityLabelsCount > 0) && ((resc == NPDSCD.InfosetStatusConceptValid) ||
      (stsc == NPDSCD.InfosetStatusConceptValid) || (slsc == NPDSCD.InfosetStatusConceptValid)))
    { statusEnum = NPDSCD.InfosetStatusValid; }
    rrr.InfosetPortalStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  //  ResRep metadata in Nexus parent for DOORS status
  public virtual byte ValidateDoorsStatus(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return ValidateDoorsStatus(ref rrr);
  }
  public virtual byte ValidateDoorsStatus(ref CoreResrepLeafUxm rrr)
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
    var statusEnum = NPDSCD.InfosetStatusInvalid;
    var lsc = NPDSCD.ParseInfosetStatus(rrr.LocationsStatusCode);
    if (lsc == NPDSCD.InfosetStatusAddressValid)
    { statusEnum = NPDSCD.InfosetStatusValid; }
    rrr.InfosetDoorsStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  // private method for use by ValidateSupportingTags and ValidateSupportingLabels
  // TODO: extend checks for concept-validation other PORTAL infosubsets
  // TODO: extend checks for concept-validation to other DOORS infosubsets
  private byte CheckSupportingStrings(string ss, Guid registryGuid)
  {
    var sss = new List<string>() { ss };
    return CheckSupportingStrings(sss, registryGuid);
  }
  private byte CheckSupportingStrings(IEnumerable<string> sss, Guid registryGuid)
  {
    NpdsInfosetStatus statusEnum = NPDSCD.InfosetStatusNone;
    bool includeConceptsPresent = CheckPresenceRestrictionStrings(sss, registryGuid, false);
    bool excludeConceptsPresent = CheckPresenceRestrictionStrings(sss, registryGuid, true);
    // if no restrictions, then define by default as ConceptValid
    if (includeConceptsPresent && !excludeConceptsPresent)
    { statusEnum = NPDSCD.InfosetStatusConceptValid; }
    else
    { statusEnum = NPDSCD.InfosetStatusConceptInvalid; }
    return statusEnum.ECode;
  }

  private bool CheckPresenceRestrictionStrings(IEnumerable<string> sss, Guid registryGuid, bool isExcluding)
  {
    bool stringsArePresent = false;
    IEnumerable<ServiceRestrictionAndUxm>? restrctAnds = ListEditableRestrictionAndsByIGuid(registryGuid, isExcluding);
    int restrctCount = restrctAnds.Count();
    if (restrctCount > 0)
    {
      bool andsArePresent = true;
      // Concept Validity requires that all restrictionAnd concepts are true
      // each restrictionAnd concept is true if at least one of its restrictionOr concepts are true
      // assume the Ands are true and exit as soon as first And is false
      foreach (ServiceRestrictionAndUxm rAnd in restrctAnds)
      {
        // select the Ors for the current And
        var rAndGuid = (Guid)rAnd.RRFgroupGuid;
        var restrctAndOrs = ListEditableRestrictionOrsByAndGuid(rAndGuid);
        // assume the Ors are false and exit as soon as first Or is true
        bool orsArePresent = false;
        foreach (ServiceRestrictionOrUxm rOr in restrctAndOrs)
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
