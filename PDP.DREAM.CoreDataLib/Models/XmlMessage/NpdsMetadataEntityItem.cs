// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsMetadataEntityItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsMetadataEntityItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataEntityNexus
{
  public NpdsMetadataEntityItem() : base() { this.Initialize(); }
  public NpdsMetadataEntityItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = default(PdpAppConst.NpdsResrepFormat))
  {
    // initialize base
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.EntityItemXnam, PdpAppConst.EntityListXnam, PdpAppConst.EntityKeyXnam);

    // pdsroot.xsd group G_EntityNexusCore
    this.EntityName = new NpdsEntityNameItem(PdpAppConst.NpdsFieldRule.Permitted);
    this.EntityNature = new NpdsEntityNatureItem(PdpAppConst.NpdsFieldRule.Permitted);
    this.EntityPrincipalTag = new NpdsEntityPrincipalTagItem(PdpAppConst.NpdsFieldRule.Required);
    this.EntityCanonicalLabel = new NpdsEntityCanonicalLabelItem(PdpAppConst.NpdsFieldRule.Required);
    this.EntityAliasLabelSet = new NpdsEntityAliasLabelList(PdpAppConst.NpdsFieldRule.Permitted);

    // pdsroot.xsd group G_EntityPortal
    if (rrf == PdpAppConst.NpdsResrepFormat.Nexus || rrf == PdpAppConst.NpdsResrepFormat.PORTAL)
    {
      this.EntitySupportingTagSet = new NpdsEntitySupportingTagList(PdpAppConst.NpdsFieldRule.Permitted);
      this.EntitySupportingLabelSet = new NpdsEntitySupportingLabelList(PdpAppConst.NpdsFieldRule.Permitted);
      this.EntityOtherEntity = new NpdsEntityOtherEntityItem(PdpAppConst.NpdsFieldRule.Permitted);
      this.EntityContact = new NpdsEntityContactItem(PdpAppConst.NpdsFieldRule.Permitted);
      this.EntityOwner = new NpdsEntityOwnerItem(PdpAppConst.NpdsFieldRule.Permitted);
    }
    else
    {
      this.EntitySupportingTagSet = new NpdsEntitySupportingTagList(PdpAppConst.NpdsFieldRule.Prohibited);
      this.EntitySupportingLabelSet = new NpdsEntitySupportingLabelList(PdpAppConst.NpdsFieldRule.Prohibited);
      this.EntityOtherEntity = new NpdsEntityOtherEntityItem(PdpAppConst.NpdsFieldRule.Prohibited);
      this.EntityContact = new NpdsEntityContactItem(PdpAppConst.NpdsFieldRule.Prohibited);
      this.EntityOwner = new NpdsEntityOwnerItem(PdpAppConst.NpdsFieldRule.Prohibited);
    }

    // pdsroot.xsd group G_EntityDoors
    if (rrf == PdpAppConst.NpdsResrepFormat.Nexus || rrf == PdpAppConst.NpdsResrepFormat.DOORS)
    {
      this.EntityLocationSet = new NpdsEntityLocationList(PdpAppConst.NpdsFieldRule.Required);
      this.EntityDescriptionSet = new NpdsEntityDescriptionList(PdpAppConst.NpdsFieldRule.Permitted);
    }
    else
    {
      this.EntityLocationSet = new NpdsEntityLocationList(PdpAppConst.NpdsFieldRule.Prohibited);
      this.EntityDescriptionSet = new NpdsEntityDescriptionList(PdpAppConst.NpdsFieldRule.Prohibited);
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
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    // TODO: refactor so same paradigm also applies for keys in addition to values???
    //       implies base class must handle both keys and values???
    //       or else treat the "keys" as the important "value"
    //       and everything else in the properties????
    if (ItemHasKey && writer.QURC.ItemDoesArchive)
    {
      // TODO: consider moving to generalized attribute writer in base
      writer.WriteAttributeString(ItemKeyXnam, ItemGuidKey.ToString());
    }
    if (writer.QURC.ItemDoesVerbose || writer.QURC.ItemDoesArchive)
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
    var reader = (NpdsXmlWrappingReader)xReader;
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
      if (reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.Nexus ||
          reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.PORTAL)
      {
        EntitySupportingTagSet.ReadXml(reader);
        EntitySupportingLabelSet.ReadXml(reader);
        EntityOtherEntity.ReadXml(reader);
        EntityOwner.ReadXml(reader);
        EntityContact.ReadXml(reader);
      }

      // pdsroot.xsd group G_EntityDoors
      if (reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.Nexus ||
          reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.DOORS)
      {
        EntityLocationSet.ReadXml(reader);
        EntityDescriptionSet.ReadXml(reader);
      }
      reader.Read();
    }
  }

}

