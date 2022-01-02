// NpdsMetadataRecordItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsMetadataRecordItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsMetadataRecordItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataRecordNexus
  {
    public NpdsMetadataRecordItem() : base() { this.Initialize(); }
    public NpdsMetadataRecordItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = default(NpdsConst.ResrepFormat))
    {
      // initialize base
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.RecordItemXnam, NpdsConst.RecordListXnam, NpdsConst.RecordKeyXnam);

      // pdsroot.xsd group G_RecordNexusCore
      this.RecordCreatedBy = new NpdsRecordCreatedByItem(NpdsConst.FieldRule.Permitted);
      this.RecordCreatedOn = new NpdsRecordCreatedOnItem(NpdsConst.FieldRule.Permitted);
      this.RecordUpdatedBy = new NpdsRecordUpdatedByItem(NpdsConst.FieldRule.Permitted);
      this.RecordUpdatedOn = new NpdsRecordUpdatedOnItem(NpdsConst.FieldRule.Permitted);
      this.RecordManagedBy = new NpdsRecordManagedByItem(NpdsConst.FieldRule.Permitted);
      this.RecordDiristry = new NpdsRecordDiristryItem(NpdsConst.FieldRule.Permitted);
      this.RecordRegistry = new NpdsRecordRegistryItem(NpdsConst.FieldRule.Permitted);
      this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsConst.FieldRule.Permitted);
      this.RecordRegistrar = new NpdsRecordRegistrarItem(NpdsConst.FieldRule.Required);
      this.RecordRegistrant = new NpdsRecordRegistrantItem(NpdsConst.FieldRule.Permitted);
      this.RecordSignatureList = new NpdsRecordSignatureList(NpdsConst.FieldRule.Permitted);

      // pdsroot.xsd group G_RecordPortal
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.PORTAL)
      {
        this.RecordRegistry = new NpdsRecordRegistryItem(NpdsConst.FieldRule.Required);
        this.RecordCrossReferenceList = new NpdsRecordCrossReferenceList(NpdsConst.FieldRule.Permitted);
        this.RecordOtherTextList = new NpdsRecordOtherTextList(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsConst.FieldRule.Prohibited);
        this.RecordCrossReferenceList = new NpdsRecordCrossReferenceList(NpdsConst.FieldRule.Prohibited);
        this.RecordOtherTextList = new NpdsRecordOtherTextList(NpdsConst.FieldRule.Prohibited);
      }
      // pdsroot.xsd group G_RecordDoors
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.DOORS)
      {
        this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsConst.FieldRule.Required);
        this.RecordProvenanceList = new NpdsRecordProvenanceList(NpdsConst.FieldRule.Permitted);
        this.RecordDistributionList = new NpdsRecordDistributionList(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.RecordRegistry = new NpdsRecordRegistryItem(NpdsConst.FieldRule.Prohibited);
        this.RecordProvenanceList = new NpdsRecordProvenanceList(NpdsConst.FieldRule.Prohibited);
        this.RecordDistributionList = new NpdsRecordDistributionList(NpdsConst.FieldRule.Prohibited);
      }
      if (rrf == NpdsConst.ResrepFormat.Nexus)
      {
        this.RecordDiristry = new NpdsRecordDiristryItem(NpdsConst.FieldRule.Required);
      }
    }

    public Guid? RecordGuid
    {
      get { return ItemGuidKey; }
      set { ItemGuidKey = value; }
    }

    public string? RecordHandle
    {
      get { return ItemHandleKey; }
      set { ItemHandleKey = value; }
    }

    public NpdsRecordDiristryItem RecordDiristry { set; get; }
    public NpdsRecordRegistryItem RecordRegistry { set; get; }
    public NpdsRecordDirectoryItem RecordDirectory { set; get; }
    public NpdsRecordRegistrarItem RecordRegistrar { set; get; }

    public NpdsRecordRegistrantItem RecordRegistrant { set; get; }
    public NpdsRecordCreatedByItem RecordCreatedBy { set; get; }
    public NpdsRecordCreatedOnItem RecordCreatedOn { set; get; }
    public NpdsRecordUpdatedByItem RecordUpdatedBy { set; get; }
    public NpdsRecordUpdatedOnItem RecordUpdatedOn { set; get; }
    public NpdsRecordManagedByItem RecordManagedBy { set; get; }

    public NpdsRecordSignatureItem? RecordSignatureItem { set; get; }
    public NpdsRecordSignatureList? RecordSignatureList { set; get; }
    public NpdsRecordCrossReferenceItem? RecordCrossReferenceItem { set; get; }
    public NpdsRecordCrossReferenceList? RecordCrossReferenceList { set; get; }
    public NpdsRecordOtherTextItem? RecordOtherTextItem { set; get; }
    public NpdsRecordOtherTextList? RecordOtherTextList { set; get; }
    public NpdsRecordProvenanceItem? RecordProvenanceItem { set; get; }
    public NpdsRecordProvenanceList? RecordProvenanceList { set; get; }
    public NpdsRecordDistributionItem? RecordDistributionItem { set; get; }
    public NpdsRecordDistributionList? RecordDistributionList { set; get; }


    //Private dtFrmStr As String = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"
    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      if (ItemHasKey && writer.PRC.ItemDoesArchive)
      {
        writer.WriteAttributeString(ItemKeyXnam, RecordHandle.ToString());
      }
      if (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive)
      {
        if (RecordCreatedBy.ItemMayExist && writer.PRC.ItemDoesArchive) { RecordCreatedBy.WriteXml(writer); }
        if (RecordCreatedOn.ItemMayExist) { writer.WriteElementString(NpdsConst.RecordCreatedOnXnam, RecordCreatedOn.CreatedOn.ToUniversalTime().ToString(NpdsConst.UnivDateTimeFormat)); }
        if (RecordUpdatedBy.ItemMayExist && writer.PRC.ItemDoesArchive) { RecordUpdatedBy.WriteXml(writer); }
        if (RecordUpdatedOn.ItemMayExist) { writer.WriteElementString(NpdsConst.RecordUpdatedOnXname, RecordUpdatedOn.UpdatedOn.ToUniversalTime().ToString(NpdsConst.UnivDateTimeFormat)); }
        if (RecordManagedBy.ItemMayExist && writer.PRC.ItemDoesArchive) { RecordManagedBy.WriteXml(writer); }
        if (RecordDiristry.ItemMayExist) { RecordDiristry.WriteXml(writer); }
        if (RecordRegistry.ItemMayExist) { RecordRegistry.WriteXml(writer); }
        if (RecordDirectory.ItemMayExist) { RecordDirectory.WriteXml(writer); }
        if (RecordRegistrar.ItemMayExist) { RecordRegistrar.WriteXml(writer); }
        if (RecordRegistrant.ItemMayExist) { RecordRegistrant.WriteXml(writer); }
        if (RecordSignatureList?.ListMayExist ?? false) { RecordSignatureList.WriteXml(writer); }
        if (RecordCrossReferenceList?.ListMayExist ?? false) { RecordCrossReferenceList.WriteXml(writer); }
        if (RecordOtherTextList?.ListMayExist ?? false) { RecordOtherTextList.WriteXml(writer); }
        if (RecordProvenanceList?.ListMayExist ?? false) { RecordProvenanceList.WriteXml(writer); }
        if (RecordDistributionList?.ListMayExist ?? false) { RecordDistributionList.WriteXml(writer); }
      }
      else
      {
        if (RecordCreatedOn.ItemDoesExist) { writer.WriteElementString(NpdsConst.RecordCreatedOnXnam, RecordCreatedOn.CreatedOn.ToUniversalTime().ToString(NpdsConst.UnivDateTimeFormat)); }
        if (RecordUpdatedOn.ItemDoesExist) { writer.WriteElementString(NpdsConst.RecordUpdatedOnXname, RecordUpdatedOn.UpdatedOn.ToUniversalTime().ToString(NpdsConst.UnivDateTimeFormat)); }
        if (RecordDiristry.ItemDoesExist) { RecordDiristry.WriteXml(writer); }
        if (RecordRegistry.ItemDoesExist) { RecordRegistry.WriteXml(writer); }
        if (RecordDirectory.ItemDoesExist) { RecordDirectory.WriteXml(writer); }
        if (RecordRegistrar.ItemDoesExist) { RecordRegistrar.WriteXml(writer); }
        if (RecordRegistrant.ItemDoesExist) { RecordRegistrant.WriteXml(writer); }
        if (RecordSignatureList?.ListDoesExist ?? false) { RecordSignatureList.WriteXml(writer); }
        if (RecordCrossReferenceList?.ListDoesExist ?? false) { RecordCrossReferenceList.WriteXml(writer); }
        if (RecordOtherTextList?.ListDoesExist ?? false) { RecordOtherTextList.WriteXml(writer); }
        if (RecordProvenanceList?.ListDoesExist ?? false) { RecordProvenanceList.WriteXml(writer); }
        if (RecordDistributionList?.ListDoesExist ?? false) { RecordDistributionList.WriteXml(writer); }
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
                ItemHandleKey = attrval;
              }
            }
          }
        }
        reader.Read();

        // pdsroot.xsd group G_RecordNexusCore
        RecordRegistrar.ReadXml(reader);
        RecordRegistrant.ReadXml(reader);
        RecordCreatedBy.ReadXml(reader);
        RecordCreatedOn.CreatedOn = Convert.ToDateTime(reader.ReadElementString(NpdsConst.RecordCreatedOnXnam));
        RecordUpdatedBy.ReadXml(reader);
        RecordUpdatedOn.UpdatedOn = Convert.ToDateTime(reader.ReadElementString(NpdsConst.RecordUpdatedOnXname));
        RecordManagedBy.ReadXml(reader);
        RecordSignatureItem.ReadXml(reader);
        RecordDistributionList.ReadXml(reader);
        RecordProvenanceList.ReadXml(reader);

        // pdsroot.xsd group G_RecordPortal
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.PORTAL)
        {
          RecordDirectory.ReadXml(reader);
        }

        // pdsroot.xsd group G_RecordDoors
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.DOORS)
        {
          RecordRegistry.ReadXml(reader);
        }

        reader.Read();
      }
    }
  }
}