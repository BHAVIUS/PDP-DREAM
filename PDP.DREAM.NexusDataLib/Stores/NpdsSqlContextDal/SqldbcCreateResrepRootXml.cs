// SqldbcCreateResrepXml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
}

// TODO: rebuild interface INpdsDataService
public partial class NexusDbsqlContext // : INpdsDataService
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
    if ((NPDSCP == null) || string.IsNullOrEmpty(NPDSCP.ServiceType.ToString()))
    {
      ResrepListMel = null;
    }
    else
    {
      var rrStems = ListStorableNexusRootsWithFacets();
      if (rrStems != null)
      {
        ResrepListMel = CreateNexusResrepListXml(rrStems);
        NPDSCP.NexusRecords = ResrepListMel;
        pdsMsg = new NpdsResrepXmlRoot();
      }
    }
    return pdsMsg;
  }

  public NpdsResrepList CreateNexusResrepListXml(IEnumerable<INexusResrepRoot> rrlist)
  {

    try
    {
      ResrepListMel = new NpdsResrepList(PdpAppConst.NpdsFieldRule.Required);
      foreach (INexusResrepRoot rr in rrlist)
      {
        var rrItem = CreateNexusResrepItemXml(rr);
        ResrepListMel.Add(rrItem);
      }

    }
    catch (Exception ex)
    {
      ResrepListMel = null;
      NPDSCP.ResponseNote = ex.Message;
    }
    return ResrepListMel;
  }

  public NpdsResrepItem CreateNexusResrepItemXml(INexusResrepRoot rrr)
  {
    // retrieve the full leaf item from the root item
    INexusResrepLeaf rrl = GetStorableNexusLeafWithFacets(rrr.RecordGuidKey);

    // Data Transfer Object for Resource Representation
    NpdsResrepFormat rrFormat = NPDSCP.ResrepFormat;
    ResrepItemMel = new NpdsResrepItem(rrFormat, rrl.RecordGuidKey);

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
        ent.EntityCanonicalLabel.EntityTypedLabel = rrl.EntityCanonicalLabel;
        ent.EntityCanonicalLabel.EntityTypeName = rrl.EntityTypeName;
      }

      // Count > 1 implies at least one CanonicalLabel and at least one AliasLabel
      if ((ent.EntityAliasLabelSet.ListMayExist) && (rrl.NexusEntityLabels.Count > 1))
      {
        foreach (NexusEntityLabel source in rrl.NexusEntityLabels
          .Where((NexusEntityLabel o) => (o.IsPrincipal == false)).OrderBy((NexusEntityLabel o) => o.HasPriority))
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

      if ((ent.EntitySupportingTagSet.ListMayExist) && (rrl.NexusSupportingTags.Count > 0))
      {
        foreach (NexusSupportingTag source in rrl.NexusSupportingTags.OrderBy((NexusSupportingTag o) => o.HasPriority))
        {
          var target = new NpdsEntitySupportingTagItem(ent.EntitySupportingTagSet.ListRule);
          if (!string.IsNullOrEmpty(source.SupportingTag)) { target.SupportingTag = source.SupportingTag; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntitySupportingTagSet.Add(target);
        }
      }

      if ((ent.EntitySupportingLabelSet.ListMayExist) && (rrl.NexusSupportingLabels.Count > 0))
      {
        foreach (NexusSupportingLabel source in rrl.NexusSupportingLabels.OrderBy((NexusSupportingLabel o) => o.HasPriority))
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

      if ((ent.EntityLocationSet.ListMayExist) && (rrl.NexusLocations.Count > 0))
      {
        foreach (NexusLocation source in rrl.NexusLocations.OrderBy((NexusLocation o) => o.HasPriority))
        {
          var target = new NpdsEntityLocationItem(ent.EntityLocationSet.ListRule);
          if (!string.IsNullOrEmpty(source.Location)) { target.Location = source.Location; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntityLocationSet.Add(target);
        }
      }

      if ((ent.EntityDescriptionSet.ListMayExist) && (rrl.NexusDescriptions.Count > 0))
      {
        foreach (NexusDescription source in rrl.NexusDescriptions.OrderBy((NexusDescription o) => o.HasPriority))
        {
          var target = new NpdsEntityDescriptionItem(ent.EntityDescriptionSet.ListRule);
          if (!string.IsNullOrEmpty(source.Description)) { target.Description = source.Description; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          ent.EntityDescriptionSet.Add(target);
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

      if ((rec.RecordCrossReferenceList.ListMayExist) && (rrl.NexusCrossReferences.Count > 0))
      {
        foreach (NexusCrossReference source in rrl.NexusCrossReferences.OrderBy((NexusCrossReference o) => o.HasPriority))
        {
          var target = new NpdsRecordCrossReferenceItem(rec.RecordCrossReferenceList.ListRule);
          if (!string.IsNullOrEmpty(source.CrossReference)) { target.CrossReference = source.CrossReference.ToUri(); }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordCrossReferenceList.Add(target);
        }
      }

      if ((rec.RecordOtherTextList.ListMayExist) && (rrl.NexusOtherTexts.Count > 0))
      {
        foreach (NexusOtherText source in rrl.NexusOtherTexts.OrderBy((NexusOtherText o) => o.HasPriority))
        {
          var target = new NpdsRecordOtherTextItem(rec.RecordOtherTextList.ListRule);
          if (!string.IsNullOrEmpty(source.OtherText)) { target.OtherText = source.OtherText; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordOtherTextList.Add(target);
        }
      }

      if ((rec.RecordProvenanceList.ListMayExist) && (rrl.NexusProvenances.Count > 0))
      {
        foreach (NexusProvenance source in rrl.NexusProvenances.OrderBy((NexusProvenance o) => o.HasPriority))
        {
          var target = new NpdsRecordProvenanceItem(rec.RecordProvenanceList.ListRule);
          if (!string.IsNullOrEmpty(source.Provenance)) { target.Provenance = source.Provenance; }
          target.IsPrincipal = source.IsPrincipal;
          target.ItemIndexKeys.Priority = source.HasPriority;
          rec.RecordProvenanceList.Add(target);
        }
      }

      if ((rec.RecordDistributionList.ListMayExist) && (rrl.NexusDistributions.Count > 0))
      {
        foreach (NexusDistribution source in rrl.NexusDistributions.OrderBy((NexusDistribution o) => o.HasPriority))
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

      inf.InfosetGuid = rrl.InfosetGuidKey;
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