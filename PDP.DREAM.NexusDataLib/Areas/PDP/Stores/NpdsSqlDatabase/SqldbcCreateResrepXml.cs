// SqldbcCreateResrepXml.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
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
    // convention: use local var rr = Resource Representation

    public NpdsResrepItem? ResrepItemMel { set; get; }

    public NpdsResrepList? ResrepListMel { set; get; }

    public NpdsResrepXmlRoot? GetResrepsXmlMessage()
    {
      NpdsResrepXmlRoot? pdsMsg = null;
      if ((PRC == null) || string.IsNullOrEmpty(PRC.ServiceType.ToString()))
      {
        ResrepListMel = null;
      }
      else
      {
        var rrStems = ListStorableResrepStemsWithFacets();
        if (rrStems != null)
        {
          ResrepListMel = CreateResrepListXml(rrStems);
          PRC.NexusRecords = ResrepListMel;
          pdsMsg = new NpdsResrepXmlRoot();
        }
      }
      return pdsMsg;
    }

    public NpdsResrepList CreateResrepListXml(IEnumerable<NexusResrepStem> rrlist)
    {

      try
      {
        ResrepListMel = new NpdsResrepList(NpdsConst.FieldRule.Required);
        foreach (NexusResrepStem rr in rrlist)
        {
          var rrItem = CreateResrepItemXml(rr);
          ResrepListMel.Add(rrItem);
        }

      }
      catch (Exception ex)
      {
        ResrepListMel = null;
        PRC.ResponseNote = ex.Message;
      }
      return ResrepListMel;
    }

    public NpdsResrepItem CreateResrepItemXml(NexusResrepStem rr)
    {
      // Data Transfer Object for Resource Representation
      ResrepItemMel = new NpdsResrepItem(pdpRestCntxt.ResrepFormat, rr.RecordGuidKey);

      // Resource Representation Level 1 EntityMetadata

      if (ResrepItemMel.EntityMetadata.ItemMayExist)
      {
        // Core

        var ent = ResrepItemMel.EntityMetadata;

        if ((ent.EntityName.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityName)))
        { ent.EntityName.Name = rr.EntityName; }
        if ((ent.EntityNature.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityNature)))
        { ent.EntityNature.Nature = rr.EntityNature; }
        if ((ent.EntityPrincipalTag.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityPrincipalTag)))
        { ent.EntityPrincipalTag.PrincipalTag = rr.EntityPrincipalTag; }

        if ((ent.EntityCanonicalLabel.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityCanonicalLabel)))
        {
          ent.EntityCanonicalLabel.EntityTypedLabel = rr.EntityCanonicalLabel;
          ent.EntityCanonicalLabel.EntityTypeName = rr.EntityTypeName;
        }

        // Count > 1 implies at least one CanonicalLabel and at least one AliasLabel
        if ((ent.EntityAliasLabelSet.ListMayExist) && (rr.NexusEntityLabels.Count > 1))
        {
          foreach (NexusEntityLabel source in rr.NexusEntityLabels
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

        if ((ent.EntitySupportingTagSet.ListMayExist) && (rr.NexusSupportingTags.Count > 0))
        {
          foreach (NexusSupportingTag source in rr.NexusSupportingTags.OrderBy((NexusSupportingTag o) => o.HasPriority))
          {
            var target = new NpdsEntitySupportingTagItem(ent.EntitySupportingTagSet.ListRule);
            if (!string.IsNullOrEmpty(source.SupportingTag)) { target.SupportingTag = source.SupportingTag; }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            ent.EntitySupportingTagSet.Add(target);
          }
        }

        if ((ent.EntitySupportingLabelSet.ListMayExist) && (rr.NexusSupportingLabels.Count > 0))
        {
          foreach (NexusSupportingLabel source in rr.NexusSupportingLabels.OrderBy((NexusSupportingLabel o) => o.HasPriority))
          {
            var target = new NpdsEntitySupportingLabelItem(ent.EntitySupportingLabelSet.ListRule);
            if (!string.IsNullOrEmpty(source.SupportingLabel)) { target.SupportingLabel = source.SupportingLabel.ToUri(); }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            ent.EntitySupportingLabelSet.Add(target);
          }
        }

        if ((ent.EntityOtherEntity.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityOtherLabel)))
        { ent.EntityOtherEntity.OtherEntity = rr.EntityOtherLabel; }
        if ((ent.EntityContact.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityContactLabel)))
        { ent.EntityContact.Contact = rr.EntityContactLabel; }
        if ((ent.EntityOwner.ItemMayExist) && (!string.IsNullOrEmpty(rr.EntityOwnerLabel)))
        { ent.EntityOwner.Owner = rr.EntityOwnerLabel; }

        // DOORS

        if ((ent.EntityLocationSet.ListMayExist) && (rr.NexusLocations.Count > 0))
        {
          foreach (NexusLocation source in rr.NexusLocations.OrderBy((NexusLocation o) => o.HasPriority))
          {
            var target = new NpdsEntityLocationItem(ent.EntityLocationSet.ListRule);
            if (!string.IsNullOrEmpty(source.Location)) { target.Location = source.Location; }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            ent.EntityLocationSet.Add(target);
          }
        }

        if ((ent.EntityDescriptionSet.ListMayExist) && (rr.NexusDescriptions.Count > 0))
        {
          foreach (NexusDescription source in rr.NexusDescriptions.OrderBy((NexusDescription o) => o.HasPriority))
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

        rec.RecordHandle = rr.RecordHandle;

        if (rec.RecordCreatedOn.ItemMayExist && rr.RecordCreatedOn.HasValue)
        { rec.RecordCreatedOn.CreatedOn = rr.RecordCreatedOn.Value; }

        if (rec.RecordUpdatedOn.ItemMayExist && (rr.RecordUpdatedOn.HasValue))
        { rec.RecordUpdatedOn.UpdatedOn = rr.RecordUpdatedOn.Value; }

        // TODO: include RecordCreatedByAgent, RecordUpdatedByAgent, RecordManagedByAgent
        //      for RecordCreatedBy, RecordUpdatedBy, RecordManagedBy 

        if (rec.RecordDiristry.ItemMayExist && (!string.IsNullOrEmpty(rr.RecordDiristryLabel)))
        { rec.RecordDiristry.Diristry = rr.RecordDiristryLabel; }

        if (rec.RecordRegistry.ItemMayExist && (!string.IsNullOrEmpty(rr.RecordRegistryLabel)))
        { rec.RecordRegistry.Registry = rr.RecordRegistryLabel; }

        if (rec.RecordDirectory.ItemMayExist && (!string.IsNullOrEmpty(rr.RecordDirectoryLabel)))
        { rec.RecordDirectory.Directory = rr.RecordDirectoryLabel; }

        if (rec.RecordRegistrar.ItemMayExist && (!string.IsNullOrEmpty(rr.RecordRegistrarLabel)))
        { rec.RecordRegistrar.Registrar = rr.RecordRegistrarLabel; }

        if (rec.RecordRegistrant.ItemMayExist && (!string.IsNullOrEmpty(rr.RecordRegistrantLabel)))
        { rec.RecordRegistrant.Registrant = rr.RecordRegistrantLabel; }

        //if (rec.RecordSignatureList.ListMayExist && (rr.RecordSignature != null))
        //{ rec.RecordSignatureList = XElement.Parse(rr.RecordSignature); }

        if ((rec.RecordCrossReferenceList.ListMayExist) && (rr.NexusCrossReferences.Count > 0))
        {
          foreach (NexusCrossReference source in rr.NexusCrossReferences.OrderBy((NexusCrossReference o) => o.HasPriority))
          {
            var target = new NpdsRecordCrossReferenceItem(rec.RecordCrossReferenceList.ListRule);
            if (!string.IsNullOrEmpty(source.CrossReference)) { target.CrossReference = source.CrossReference.ToUri(); }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            rec.RecordCrossReferenceList.Add(target);
          }
        }

        if ((rec.RecordOtherTextList.ListMayExist) && (rr.NexusOtherTexts.Count > 0))
        {
          foreach (NexusOtherText source in rr.NexusOtherTexts.OrderBy((NexusOtherText o) => o.HasPriority))
          {
            var target = new NpdsRecordOtherTextItem(rec.RecordOtherTextList.ListRule);
            if (!string.IsNullOrEmpty(source.OtherText)) { target.OtherText = source.OtherText; }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            rec.RecordOtherTextList.Add(target);
          }
        }

        if ((rec.RecordProvenanceList.ListMayExist) && (rr.NexusProvenances.Count > 0))
        {
          foreach (NexusProvenance source in rr.NexusProvenances.OrderBy((NexusProvenance o) => o.HasPriority))
          {
            var target = new NpdsRecordProvenanceItem(rec.RecordProvenanceList.ListRule);
            if (!string.IsNullOrEmpty(source.Provenance)) { target.Provenance = source.Provenance; }
            target.IsPrincipal = source.IsPrincipal;
            target.ItemIndexKeys.Priority = source.HasPriority;
            rec.RecordProvenanceList.Add(target);
          }
        }

        if ((rec.RecordDistributionList.ListMayExist) && (rr.NexusDistributions.Count > 0))
        {
          foreach (NexusDistribution source in rr.NexusDistributions.OrderBy((NexusDistribution o) => o.HasPriority))
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

        inf.InfosetGuid = rr.InfosetGuidKey;
        inf.InfosetIsAuthorPrivate = rr.InfosetIsAuthorPrivate;

        // PORTAL

        if (inf.InfosetPortalValidation.ItemMayExist)
        {
          inf.InfosetPortalValidation.InfosetStatus = rr.InfosetPortalStatusName;
          inf.InfosetPortalValidation.InfosetTestedOn = rr.InfosetPortalStatusTestedOn;
        }

        // DOORS

        if (inf.InfosetDoorsValidation.ItemMayExist)
        {
          inf.InfosetDoorsValidation.InfosetStatus = rr.InfosetDoorsStatusName;
          inf.InfosetDoorsValidation.InfosetTestedOn = rr.InfosetDoorsStatusTestedOn;
        }

        // Nexus

        if ((inf.InfosetNexusEntailment.ItemMayExist) && (rr.InfosetEntailment != null))
        { inf.InfosetNexusEntailment.Entailment = XElement.Parse(rr.InfosetEntailment); }
      }

      return ResrepItemMel;
    }

  } // class

} // namespace
