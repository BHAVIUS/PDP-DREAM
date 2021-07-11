// NpdsMetadataEntityItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsMetadataEntityItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsMetadataEntityItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataEntityNexus
  {
    public NpdsMetadataEntityItem() : base() { this.Initialize(); }
    public NpdsMetadataEntityItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = default(NpdsConst.ResrepFormat))
    {
      // initialize base
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.EntityItemXnam, NpdsConst.EntityListXnam, NpdsConst.EntityKeyXnam);

      // pdsroot.xsd group G_EntityNexusCore
      this.EntityName = new NpdsEntityNameItem(NpdsConst.FieldRule.Permitted);
      this.EntityNature = new NpdsEntityNatureItem(NpdsConst.FieldRule.Permitted);
      this.EntityPrincipalTag = new NpdsEntityPrincipalTagItem(NpdsConst.FieldRule.Required);
      this.EntityCanonicalLabel = new NpdsEntityCanonicalLabelItem(NpdsConst.FieldRule.Required);
      this.EntityAliasLabelSet = new NpdsEntityAliasLabelList(NpdsConst.FieldRule.Permitted);

      // pdsroot.xsd group G_EntityPortal
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.PORTAL)
      {
        this.EntitySupportingTagSet = new NpdsEntitySupportingTagList(NpdsConst.FieldRule.Permitted);
        this.EntitySupportingLabelSet = new NpdsEntitySupportingLabelList(NpdsConst.FieldRule.Permitted);
        this.EntityOtherEntity = new NpdsEntityOtherEntityItem(NpdsConst.FieldRule.Permitted);
        this.EntityContact = new NpdsEntityContactItem(NpdsConst.FieldRule.Permitted);
        this.EntityOwner = new NpdsEntityOwnerItem(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.EntitySupportingTagSet = new NpdsEntitySupportingTagList(NpdsConst.FieldRule.Prohibited);
        this.EntitySupportingLabelSet = new NpdsEntitySupportingLabelList(NpdsConst.FieldRule.Prohibited);
        this.EntityOtherEntity = new NpdsEntityOtherEntityItem(NpdsConst.FieldRule.Prohibited);
        this.EntityContact = new NpdsEntityContactItem(NpdsConst.FieldRule.Prohibited);
        this.EntityOwner = new NpdsEntityOwnerItem(NpdsConst.FieldRule.Prohibited);
      }

      // pdsroot.xsd group G_EntityDoors
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.DOORS)
      {
        this.EntityLocationSet = new NpdsEntityLocationList(NpdsConst.FieldRule.Required);
        this.EntityDescriptionSet = new NpdsEntityDescriptionList(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.EntityLocationSet = new NpdsEntityLocationList(NpdsConst.FieldRule.Prohibited);
        this.EntityDescriptionSet = new NpdsEntityDescriptionList(NpdsConst.FieldRule.Prohibited);
      }
    }

    public NpdsEntityNameItem EntityName { set; get; }
    public NpdsEntityNatureItem EntityNature { set; get; }
    public NpdsEntityCanonicalLabelItem EntityCanonicalLabel { set; get; }
    public NpdsEntityAliasLabelList EntityAliasLabelSet { set; get; }
    public NpdsEntityPrincipalTagItem EntityPrincipalTag { set; get; }
    public NpdsEntitySupportingTagList EntitySupportingTagSet { set; get; }
    public NpdsEntitySupportingLabelList EntitySupportingLabelSet { set; get; }
    public NpdsEntityOtherEntityItem EntityOtherEntity { set; get; }
    public NpdsEntityContactItem EntityContact { set; get; }
    public NpdsEntityOwnerItem EntityOwner { set; get; }
    public NpdsEntityLocationList EntityLocationSet { set; get; }
    public NpdsEntityDescriptionList EntityDescriptionSet { set; get; }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      // TODO: refactor so same paradigm also applies for keys in addition to values???
      //       implies base class must handle both keys and values???
      //       or else treat the "keys" as the important "value"
      //       and everything else in the properties????
      if (ItemHasKey && writer.PRC.ItemDoesArchive)
      {
        // TODO: consider moving to generalized attribute writer in base
        writer.WriteAttributeString(ItemKeyXnam, ItemGuidKey.ToString());
      }
      if (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive)
      {
        if (EntityName.ItemMayExist) { EntityName.WriteXml(writer); }
        if (EntityNature.ItemMayExist) { EntityNature.WriteXml(writer); }
        if (EntityPrincipalTag.ItemMayExist) { EntityPrincipalTag.WriteXml(writer); }
        if (EntityCanonicalLabel.ItemMayExist) { EntityCanonicalLabel.WriteXml(writer); }
        if (EntityAliasLabelSet.ListMayExist) { EntityAliasLabelSet.WriteXml(writer); }
        if (EntitySupportingTagSet.ListMayExist) { EntitySupportingTagSet.WriteXml(writer); }
        if (EntitySupportingLabelSet.ListMayExist) { EntitySupportingLabelSet.WriteXml(writer); }
        if (EntityOtherEntity.ItemMayExist) { EntityOtherEntity.WriteXml(writer); }
        if (EntityOwner.ItemMayExist) { EntityOwner.WriteXml(writer); }
        if (EntityContact.ItemMayExist) { EntityContact.WriteXml(writer); }
        if (EntityLocationSet.ListMayExist) { EntityLocationSet.WriteXml(writer); }
        if (EntityDescriptionSet.ListMayExist) { EntityDescriptionSet.WriteXml(writer); }
      }
      else
      {
        if (EntityName.ItemDoesExist) { EntityName.WriteXml(writer); }
        if (EntityNature.ItemDoesExist) { EntityNature.WriteXml(writer); }
        if (EntityPrincipalTag.ItemDoesExist) { EntityPrincipalTag.WriteXml(writer); }
        if (EntityCanonicalLabel.ItemDoesExist) { EntityCanonicalLabel.WriteXml(writer); }
        if (EntityAliasLabelSet.ListDoesExist) { EntityAliasLabelSet.WriteXml(writer); }
        if (EntitySupportingTagSet.ListDoesExist) { EntitySupportingTagSet.WriteXml(writer); }
        if (EntitySupportingLabelSet.ListDoesExist) { EntitySupportingLabelSet.WriteXml(writer); }
        if (EntityOtherEntity.ItemDoesExist) { EntityOtherEntity.WriteXml(writer); }
        if (EntityOwner.ItemDoesExist) { EntityOwner.WriteXml(writer); }
        if (EntityContact.ItemDoesExist) { EntityContact.WriteXml(writer); }
        if (EntityLocationSet.ListDoesExist) { EntityLocationSet.WriteXml(writer); }
        if (EntityDescriptionSet.ListDoesExist) { EntityDescriptionSet.WriteXml(writer); }
      }
      writer.WriteEndElement();
    }

    public override void ReadXml(XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      reader.MoveToContent();
      if (reader.IsEmptyElement)
      {
        reader.ReadStartElement();
      }
      else if (reader.IsStartElement(ItemXnam))
      {
        if (reader.HasAttributes)
        {
          while (reader.MoveToNextAttribute())
          {
            string attrnam = reader.LocalName;
            string attrval = reader.GetAttribute(attrnam);
            if (!string.IsNullOrEmpty(attrval))
            {
              if (attrnam == ItemKeyXnam)
              {
                // TODO: update / revise / recheck
                // PdsItemKey = ParseToPdsItemKeyType(attrval);
                ItemGuidKey = Guid.Parse(WebUtility.HtmlDecode(attrval));
              }
            }
          }
        }
        reader.Read();

        // pdsroot.xsd group G_EntityNexusCore
        EntityName.ReadXml(reader);
        EntityNature.ReadXml(reader);
        EntityPrincipalTag.ReadXml(reader);
        EntityCanonicalLabel.ReadXml(reader);
        EntityAliasLabelSet.ReadXml(reader);

        // pdsroot.xsd group G_EntityPortal
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.PORTAL)
        {
          EntitySupportingTagSet.ReadXml(reader);
          EntitySupportingLabelSet.ReadXml(reader);
          EntityOtherEntity.ReadXml(reader);
          EntityOwner.ReadXml(reader);
          EntityContact.ReadXml(reader);
        }

        // pdsroot.xsd group G_EntityDoors
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.DOORS)
        {
          EntityLocationSet.ReadXml(reader);
          EntityDescriptionSet.ReadXml(reader);
        }
        reader.Read();
      }
    }

  }

}