// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsMetadataInfosetItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsMetadataInfosetItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataInfosetNexus
{
  public NpdsMetadataInfosetItem() : base() { this.Initialize(NPDSCD.ResrepFormatDefault); }
  public NpdsMetadataInfosetItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  // initialize only in this private method, not with the private fields of properties
  private void Initialize(NpdsResrepFormat rrf)
  {
    // initialize base
    base.InitNpdsItem(NpdsFieldRule.Required, InfosetItemXnam, InfosetListXnam, InfosetKeyXnam);

    // pdsroot.xsd group G_InfosetNexusCore

    // pdsroot.xsd group G_InfosetPortal
    if (rrf == NPDSCD.ResrepFormatNexus || rrf == NPDSCD.ResrepFormatPORTAL)
    {
      this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(NpdsFieldRule.Permitted);
    }
    else
    {
      this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(NpdsFieldRule.Prohibited);
    }

    // pdsroot.xsd group G_InfosetDoors
    if (rrf == NPDSCD.ResrepFormatNexus || rrf == NPDSCD.ResrepFormatDOORS)
    {
      this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(NpdsFieldRule.Permitted);
      this.InfosetNexusEntailment = new NpdsInfosetEntailmentItem(NpdsFieldRule.Permitted);
    }
    else
    {
      this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(NpdsFieldRule.Prohibited);
      this.InfosetNexusEntailment = new NpdsInfosetEntailmentItem(NpdsFieldRule.Prohibited);
    }
  }

  public Guid? InfosetGuid
  {
    get { return ItemGuidKey; }
    set { ItemGuidKey = value; }
  }

  public string? InfosetHandle
  {
    get { return ItemHandleKey; }
    set { ItemHandleKey = value; }
  }

  public bool InfosetIsAuthorPrivate { set; get; } = false;
  public bool InfosetIsAgentShared { set; get; } = false;
  public bool InfosetIsUpdaterLimited { set; get; } = false;
  public bool InfosetIsManagerReleased { set; get; } = false;
  public bool InfosetIsConcise { set; get; } = false;
  public NpdsInfosetEntailmentItem InfosetNexusEntailment { set; get; }
  public NpdsInfosetValidationPortalItem InfosetPortalValidation { set; get; }
  public NpdsInfosetValidationDoorsItem InfosetDoorsValidation { set; get; }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (ItemHasKey && writer.WRACE.ItemDoesArchive)
    {
      writer.WriteAttributeString(ItemKeyXnam, InfosetGuid.ToString());
    }
    if (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive)
    {
      if (InfosetPortalValidation.ItemMayExist) { InfosetPortalValidation.WriteXml(writer); }
      if (InfosetDoorsValidation.ItemMayExist) { InfosetDoorsValidation.WriteXml(writer); }
      if (InfosetNexusEntailment.ItemMayExist) { InfosetNexusEntailment.WriteXml(writer); }
    }
    else
    {
      if (InfosetPortalValidation.ItemDoesExist) { InfosetPortalValidation.WriteXml(writer); }
      if (InfosetDoorsValidation.ItemDoesExist) { InfosetDoorsValidation.WriteXml(writer); }
      if (InfosetNexusEntailment.ItemDoesExist) { InfosetNexusEntailment.WriteXml(writer); }
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
              ItemGuidKey = new Guid(WebUtility.HtmlDecode(attrval));
            }
          }
        }
      }
      reader.Read();

      // pdsroot.xsd group G_InfosetNexus

      // pdsroot.xsd group G_InfosetPortal
      if (reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatNexus ||
          reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatPORTAL)
      {
        InfosetPortalValidation.ReadXml(reader);
      }

      // pdsroot.xsd group G_InfosetDoors
      if (reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatNexus ||
          reader.WRACE.ResrepFormat == NPDSCD.ResrepFormatDOORS)
      {
        InfosetDoorsValidation.ReadXml(reader);
        InfosetNexusEntailment.ReadXml(reader);
      }

      reader.Read();
    }
  }

}

