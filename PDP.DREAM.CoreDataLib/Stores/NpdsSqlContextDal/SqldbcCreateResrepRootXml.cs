// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
}

// TODO: rebuild interface INpdsDataService
public partial class CoreDbsqlContext // : INpdsDataService
{
  // DBL for database layer, UIL for user interface layer, MEL for message exchange layer
  //
  // Data Transfer Objects for MVC View/EditModels user interaction
  // use suffix with acronym "uil" for user interaction/interface layer
  // compare elsewhere use of UVM and UXM vs ViewModel and EditModel
  // Data Transfer Objects for XML Request/Response data exchange
  // use suffix with acronym "mel" for message exchange layer
  // or use specific suffix XML and JSON for corresponding objects
  // convention: use local var rr or resrep for ResourceRepresentation

  public NpdsResrepItem? ResrepItemMel { set; get; }

  public NpdsResrepList? ResrepListMel { set; get; }

  public NpdsResrepXmlRoot? GetResrepsXmlMessage()
  {
    NpdsResrepXmlRoot? pdsMsg = null;
    if ((NPDSDC == null) || string.IsNullOrEmpty(NPDSDC.ServiceType.ToString()))
    {
      ResrepListMel = null;
    }
    else
    {
      var rrStems = ListStorableResrepRootsWithFacets();
      if (rrStems != null)
      {
        ResrepListMel = CreateCoreResrepListXml(rrStems);
        NPDSDC.CoreRecords = ResrepListMel;
        pdsMsg = new NpdsResrepXmlRoot();
      }
    }
    return pdsMsg;
  }

  public NpdsResrepList CreateCoreResrepListXml(IEnumerable<ICoreResrepRoot> rrlist)
  {

    try
    {
      ResrepListMel = new NpdsResrepList(NpdsFieldRule.Required);
      foreach (ICoreResrepRoot rr in rrlist)
      {
        var rrItem = CreateCoreResrepItemXml(rr);
        ResrepListMel.Add(rrItem);
      }

    }
    catch (Exception ex)
    {
      ResrepListMel = null;
      NPDSDC.ResponseNote = ex.Message;
    }
    return ResrepListMel;
  }

  public NpdsResrepItem CreateCoreResrepItemXml(ICoreResrepRoot rrr)
  {
    // retrieve the full leaf item from the root item
    ICoreResrepLeaf rrl = GetStorableResrepLeafWithFacets(rrr.RecordGuid);

    // Data Transfer Object for Resource Representation
    NpdsResrepFormat rrFormat = NPDSDC.ResrepFormat;
    ResrepItemMel = new NpdsResrepItem(rrFormat, rrl.RecordGuid);

    // Resource Representation Level 1 EntityMetadata

    if (ResrepItemMel.EntityMetadata.ItemMayExist)
    {
      // Core

      var ent = ResrepItemMel.EntityMetadata;

      if ((ent.EntityName.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityName)))
      { ent.EntityName.Name = rrl.EntityName; }
      if ((ent.EntityNature.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityNature)))
      { ent.EntityNature.Nature = rrl.EntityNature; }
      if ((ent.EntityPrincipalTag.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityPrincipalTag)))
      { ent.EntityPrincipalTag.PrincipalTag = rrl.EntityPrincipalTag; }

      if ((ent.EntityCanonicalLabel.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityCanonicalLabel)))
      {
        ent.EntityCanonicalLabel.CanonicalLabel = rrl.EntityCanonicalLabel;
        ent.EntityCanonicalLabel.EntityTypeName = rrl.EntityTypeName;
      }

      // Count > 1 implies at least one CanonicalLabel and at least one AliasLabel
      if ((ent.EntityAliasLabelSet.ListMayExist) && (rrl.CoreEntityLabels.Count > 1))
      {
        foreach (CoreEntityLabel source in rrl.CoreEntityLabels
          .Where((CoreEntityLabel o) => (o.IsPrincipal == false)).OrderBy((CoreEntityLabel o) => o.HasPriority))
        {
          var target = new NpdsEntityAliasLabelItem(ent.EntityAliasLabelSet.ListRule);
          target.TagToken = source.TagToken;
          target.LabelUri = source.LabelUri;
          target.AliasLabel = source.EntityLabel;
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          target.IsPrivate = source.IsPrivate;
          target.IsResolvable = source.IsResolvable;
          ent.EntityAliasLabelSet.Add(target);
        }
      }

      // PORTAL

      if ((ent.EntitySupportingTagSet.ListMayExist) && (rrl.PortalSupportingTags.Count > 0))
      {
        foreach (PortalSupportingTag source in rrl.PortalSupportingTags.OrderBy((PortalSupportingTag o) => o.HasPriority))
        {
          var target = new NpdsEntitySupportingTagItem(ent.EntitySupportingTagSet.ListRule);
          if (!string.IsNullOrEmpty(source.SupportingTag)) { target.SupportingTag = source.SupportingTag; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntitySupportingTagSet.Add(target);
        }
      }

      if ((ent.EntitySupportingLabelSet.ListMayExist) && (rrl.PortalSupportingLabels.Count > 0))
      {
        foreach (PortalSupportingLabel source in rrl.PortalSupportingLabels.OrderBy((PortalSupportingLabel o) => o.HasPriority))
        {
          var target = new NpdsEntitySupportingLabelItem(ent.EntitySupportingLabelSet.ListRule);
          if (!string.IsNullOrEmpty(source.SupportingLabel)) { target.SupportingLabel = source.SupportingLabel.ToUri(); }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntitySupportingLabelSet.Add(target);
        }
      }

      if ((ent.EntityOtherEntity.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityOtherLabel)))
      { ent.EntityOtherEntity.OtherEntity = rrl.EntityOtherLabel; }
      if ((ent.EntityContact.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityContactLabel)))
      { ent.EntityContact.Contact = rrl.EntityContactLabel; }
      if ((ent.EntityOwner.ItemMayExist) && (!string.IsNullOrEmpty(rrl.EntityOwnerLabel)))
      { ent.EntityOwner.Owner = rrl.EntityOwnerLabel; }

      // DOORS

      if ((ent.EntityLocationSet.ListMayExist) && (rrl.DoorsLocations.Count > 0))
      {
        foreach (DoorsLocation source in rrl.DoorsLocations.OrderBy((DoorsLocation o) => o.HasPriority))
        {
          var target = new NpdsEntityLocationItem(ent.EntityLocationSet.ListRule);
          if (!string.IsNullOrEmpty(source.Location)) { target.Location = source.Location; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntityLocationSet.Add(target);
        }
      }

      if ((ent.EntityDescriptionSet.ListMayExist) && (rrl.DoorsDescriptions.Count > 0))
      {
        foreach (DoorsDescription source in rrl.DoorsDescriptions.OrderBy((DoorsDescription o) => o.HasPriority))
        {
          var target = new NpdsEntityDescriptionItem(ent.EntityDescriptionSet.ListRule);
          if (!string.IsNullOrEmpty(source.Description)) { target.Description = source.Description; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntityDescriptionSet.Add(target);
        }
      }

      if ((ent.EntityFairMetricSet.ListMayExist) && (rrl.DoorsFairMetrics.Count > 0))
      {
        foreach (DoorsFairMetric source in rrl.DoorsFairMetrics.OrderBy((DoorsFairMetric o) => o.HasPriority))
        {
          var target = new NpdsEntityFairMetricItem(ent.EntityFairMetricSet.ListRule);
          target.FairMetric = $"F1 = {source.FAIR1Q}, F2  = {source.FAIR2M}, F3 = {source.FAIR3P}, F4  = {source.FAIR4N}";
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntityFairMetricSet.Add(target);
        }
      }

    }

    // Resource Representation Level 2 RecordMetadata

    if (ResrepItemMel.RecordMetadata.ItemMayExist)
    {
      var rec = ResrepItemMel.RecordMetadata;

      rec.RecordHandle = rrl.RecordHandle;

      if (rec.RecordCreatedOn.ItemMayExist && rrl.RecordCreatedOn.HasValue)
      { rec.RecordCreatedOn.CreatedOn = rrl.RecordCreatedOn.Value; }

      if (rec.RecordUpdatedOn.ItemMayExist && (rrl.RecordUpdatedOn.HasValue))
      { rec.RecordUpdatedOn.UpdatedOn = rrl.RecordUpdatedOn.Value; }

      // TODO: include RecordCreatedByAgent, RecordUpdatedByAgent, RecordManagedByAgent
      //      for RecordCreatedBy, RecordUpdatedBy, RecordManagedBy 

      if (rec.RecordDiristry.ItemMayExist && (!string.IsNullOrEmpty(rrl.RecordDiristryLabel)))
      { rec.RecordDiristry.Diristry = rrl.RecordDiristryLabel; }

      if (rec.RecordRegistry.ItemMayExist && (!string.IsNullOrEmpty(rrl.RecordRegistryLabel)))
      { rec.RecordRegistry.Registry = rrl.RecordRegistryLabel; }

      if (rec.RecordDirectory.ItemMayExist && (!string.IsNullOrEmpty(rrl.RecordDirectoryLabel)))
      { rec.RecordDirectory.Directory = rrl.RecordDirectoryLabel; }

      if (rec.RecordRegistrar.ItemMayExist && (!string.IsNullOrEmpty(rrl.RecordRegistrarLabel)))
      { rec.RecordRegistrar.Registrar = rrl.RecordRegistrarLabel; }

      if (rec.RecordRegistrant.ItemMayExist && (!string.IsNullOrEmpty(rrl.RecordRegistrantLabel)))
      { rec.RecordRegistrant.Registrant = rrl.RecordRegistrantLabel; }

      //if (rec.RecordSignatureList.ListMayExist && (rr.RecordSignature != null))
      //{ rec.RecordSignatureList = XElement.Parse(rr.RecordSignature); }

      if ((rec.RecordCrossReferenceList.ListMayExist) && (rrl.PortalCrossReferences.Count > 0))
      {
        foreach (PortalCrossReference source in rrl.PortalCrossReferences.OrderBy((PortalCrossReference o) => o.HasPriority))
        {
          var target = new NpdsRecordCrossReferenceItem(rec.RecordCrossReferenceList.ListRule);
          if (!string.IsNullOrEmpty(source.CrossReference)) { target.CrossReference = source.CrossReference; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordCrossReferenceList.Add(target);
        }
      }

      if ((rec.RecordOtherTextList.ListMayExist) && (rrl.PortalOtherTexts.Count > 0))
      {
        foreach (PortalOtherText source in rrl.PortalOtherTexts.OrderBy((PortalOtherText o) => o.HasPriority))
        {
          var target = new NpdsRecordOtherTextItem(rec.RecordOtherTextList.ListRule);
          if (!string.IsNullOrEmpty(source.OtherText)) { target.OtherText = source.OtherText; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordOtherTextList.Add(target);
        }
      }

      if ((rec.RecordProvenanceList.ListMayExist) && (rrl.DoorsProvenances.Count > 0))
      {
        foreach (DoorsProvenance source in rrl.DoorsProvenances.OrderBy((DoorsProvenance o) => o.HasPriority))
        {
          var target = new NpdsRecordProvenanceItem(rec.RecordProvenanceList.ListRule);
          if (!string.IsNullOrEmpty(source.Provenance)) { target.Provenance = source.Provenance; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordProvenanceList.Add(target);
        }
      }

      if ((rec.RecordDistributionList.ListMayExist) && (rrl.DoorsDistributions.Count > 0))
      {
        foreach (DoorsDistribution source in rrl.DoorsDistributions.OrderBy((DoorsDistribution o) => o.HasPriority))
        {
          var target = new NpdsRecordDistributionItem(rec.RecordOtherTextList.ListRule);
          if (!string.IsNullOrEmpty(source.Distribution)) { target.Distribution = source.Distribution; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordDistributionList.Add(target);
        }
      }

    }

    // Resource Representation Level 3 InfosetMetadata

    if (ResrepItemMel.InfosetMetadata.ItemMayExist)
    {
      var inf = ResrepItemMel.InfosetMetadata;

      inf.InfosetGuid = rrl.InfosetGuid;
      inf.InfosetIsAuthorPrivate = rrl.InfosetIsAuthorPrivate;

      // PORTAL

      if (inf.InfosetPortalValidation.ItemMayExist)
      {
        inf.InfosetPortalValidation.InfosetStatus = rrl.InfosetPortalStatusName;
        inf.InfosetPortalValidation.InfosetTestedOn = rrl.InfosetPortalStatusTestedOn;
      }

      // DOORS

      if (inf.InfosetDoorsValidation.ItemMayExist)
      {
        inf.InfosetDoorsValidation.InfosetStatus = rrl.InfosetDoorsStatusName;
        inf.InfosetDoorsValidation.InfosetTestedOn = rrl.InfosetDoorsStatusTestedOn;
      }

      // Nexus

      if ((inf.InfosetNexusEntailment.ItemMayExist) && (rrl.InfosetEntailment != null))
      { inf.InfosetNexusEntailment.Entailment = XElement.Parse(rrl.InfosetEntailment); }
    }

    return ResrepItemMel;
  }

} // end class

// end file