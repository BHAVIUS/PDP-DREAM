// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsMetadataRecordItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsMetadataRecordItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataRecordNexus
{
  public NpdsMetadataRecordItem() : base() { this.Initialize(NPDSCD.ResrepFormatDefault); }
  public NpdsMetadataRecordItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(NpdsResrepFormat rrf)
  {
    // initialize base
    base.InitNpdsItem(NpdsFieldRule.Required, RecordItemXnam, RecordListXnam, RecordKeyXnam);

    // pdsroot.xsd group G_RecordNexusCore
    this.RecordCreatedBy = new NpdsRecordCreatedByItem(NpdsFieldRule.Permitted);
    this.RecordCreatedOn = new NpdsRecordCreatedOnItem(NpdsFieldRule.Permitted);
    this.RecordUpdatedBy = new NpdsRecordUpdatedByItem(NpdsFieldRule.Permitted);
    this.RecordUpdatedOn = new NpdsRecordUpdatedOnItem(NpdsFieldRule.Permitted);
    this.RecordManagedBy = new NpdsRecordManagedByItem(NpdsFieldRule.Permitted);
    this.RecordDiristry = new NpdsRecordDiristryItem(NpdsFieldRule.Permitted);
    this.RecordRegistry = new NpdsRecordRegistryItem(NpdsFieldRule.Permitted);
    this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsFieldRule.Permitted);
    this.RecordRegistrar = new NpdsRecordRegistrarItem(NpdsFieldRule.Required);
    this.RecordRegistrant = new NpdsRecordRegistrantItem(NpdsFieldRule.Permitted);
    this.RecordSignatureList = new NpdsRecordSignatureList(NpdsFieldRule.Permitted);

    // pdsroot.xsd group G_RecordPortal
    if (rrf == NPDSCD.ResrepFormatNexus || rrf == NPDSCD.ResrepFormatPORTAL)
    {
      this.RecordRegistry = new NpdsRecordRegistryItem(NpdsFieldRule.Required);
      this.RecordCrossReferenceList = new NpdsRecordCrossReferenceList(NpdsFieldRule.Permitted);
      this.RecordOtherTextList = new NpdsRecordOtherTextList(NpdsFieldRule.Permitted);
    }
    else
    {
      this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsFieldRule.Prohibited);
      this.RecordCrossReferenceList = new NpdsRecordCrossReferenceList(NpdsFieldRule.Prohibited);
      this.RecordOtherTextList = new NpdsRecordOtherTextList(NpdsFieldRule.Prohibited);
    }
    // pdsroot.xsd group G_RecordDoors
    if (rrf == NPDSCD.ResrepFormatNexus || rrf == NPDSCD.ResrepFormatDOORS)
    {
      this.RecordDirectory = new NpdsRecordDirectoryItem(NpdsFieldRule.Required);
      this.RecordProvenanceList = new NpdsRecordProvenanceList(NpdsFieldRule.Permitted);
      this.RecordDistributionList = new NpdsRecordDistributionList(NpdsFieldRule.Permitted);
    }
    else
    {
      this.RecordRegistry = new NpdsRecordRegistryItem(NpdsFieldRule.Prohibited);
      this.RecordProvenanceList = new NpdsRecordProvenanceList(NpdsFieldRule.Prohibited);
      this.RecordDistributionList = new NpdsRecordDistributionList(NpdsFieldRule.Prohibited);
    }
    if (rrf == NPDSCD.ResrepFormatNexus)
    {
      this.RecordDiristry = new NpdsRecordDiristryItem(NpdsFieldRule.Required);
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
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (ItemHasKey && writer.WRACE.ItemDoesArchive)
    {
      writer.WriteAttributeString(ItemKeyXnam, RecordHandle.ToString());
    }
    if (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive)
    {
      if (RecordCreatedBy.ItemMayExist && writer.WRACE.ItemDoesArchive) { RecordCreatedBy.WriteXml(writer); }
      if (RecordCreatedOn.ItemMayExist) { writer.WriteElementString(RecordCreatedOnXnam, RecordCreatedOn.CreatedOn.ToUniversalTime().ToString(UnivDateTimeFormat)); }
      if (RecordUpdatedBy.ItemMayExist && writer.WRACE.ItemDoesArchive) { RecordUpdatedBy.WriteXml(writer); }
      if (RecordUpdatedOn.ItemMayExist) { writer.WriteElementString(RecordUpdatedOnXnam, RecordUpdatedOn.UpdatedOn.ToUniversalTime().ToString(UnivDateTimeFormat)); }
      if (RecordManagedBy.ItemMayExist && writer.WRACE.ItemDoesArchive) { RecordManagedBy.WriteXml(writer); }
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
      if (RecordCreatedOn.ItemDoesExist) { writer.WriteElementString(RecordCreatedOnXnam, RecordCreatedOn.CreatedOn.ToUniversalTime().ToString(UnivDateTimeFormat)); }
      if (RecordUpdatedOn.ItemDoesExist) { writer.WriteElementString(RecordUpdatedOnXnam, RecordUpdatedOn.UpdatedOn.ToUniversalTime().ToString(UnivDateTimeFormat)); }
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
              ItemHandleKey = attrval;
            }
          }
        }
      }
      reader.Read();

      // pdsroot.xsd group G_RecordNexus
      RecordRegistrar.ReadXml(reader);
      RecordRegistrant.ReadXml(reader);
      RecordCreatedBy.ReadXml(reader);
      RecordCreatedOn.CreatedOn = Convert.ToDateTime(reader.ReadElementString(RecordCreatedOnXnam));
      RecordUpdatedBy.ReadXml(reader);
      RecordUpdatedOn.UpdatedOn = Convert.ToDateTime(reader.ReadElementString(RecordUpdatedOnXnam));
      RecordManagedBy.ReadXml(reader);
      RecordSignatureItem.ReadXml(reader);
      RecordDistributionList.ReadXml(reader);
      RecordProvenanceList.ReadXml(reader);

      // pdsroot.xsd group G_RecordPortal
      if (reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatNexus ||
          reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatPORTAL)
      {
        RecordDirectory.ReadXml(reader);
      }

      // pdsroot.xsd group G_RecordDoors
      if (reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatNexus ||
          reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatDOORS)
      {
        RecordRegistry.ReadXml(reader);
      }

      reader.Read();
    }
  }
}
