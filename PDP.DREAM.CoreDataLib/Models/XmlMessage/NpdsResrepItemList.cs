// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: pattern discrepancy ?!? 
//  higher level objects NexusResrep/EntityMetadata/RecordMetadata/InfosetMetadata
//     have input ResrepFormat, but lower level objects have input FieldRule ?!?
//   what should be injected? should only ResrepFormat be carried through tree instead of FieldRule? or leave as is?
[KnownType(typeof(NpdsResrepItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsResrepItem : ANpdsXsgBaseItem<Object>, INpdsResrep
{
  public NpdsResrepItem() : base() { this.Initialize(); }
  public NpdsResrepItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }
  public NpdsResrepItem(PdpAppConst.NpdsResrepFormat rrf, Guid? key) : base(typeof(Guid?), key) { this.Initialize(rrf); }

  protected void Initialize(PdpAppConst.NpdsResrepFormat rrf = default(PdpAppConst.NpdsResrepFormat))
  {
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.NexusResrepItemXnam, PdpAppConst.NexusResrepListXnam, PdpAppConst.ResrepKeyXnam);
    this.EntityMetadata = new NpdsMetadataEntityItem(rrf);
    this.RecordMetadata = new NpdsMetadataRecordItem(rrf);
    this.InfosetMetadata = new NpdsMetadataInfosetItem(rrf);
  }

  public bool ResrepIsAuthorPrivate { get; set; } = false;
  public bool ResrepIsAgentShared { get; set; } = false;
  public bool ResrepIsUpdaterLimited { get; set; } = false;
  public bool ResrepIsManagerReleased { get; set; } = false;


  private NpdsMetadataEntityItem entMeta;
  public NpdsMetadataEntityItem EntityMetadata
  {
    get { return entMeta; }
    set { entMeta = value; }
  }

  private NpdsMetadataRecordItem recMeta;
  public NpdsMetadataRecordItem RecordMetadata
  {
    get { return recMeta; }
    set { recMeta = value; }
  }

  private NpdsMetadataInfosetItem infMeta;
  public NpdsMetadataInfosetItem InfosetMetadata
  {
    get { return infMeta; }
    set { infMeta = value; }
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    writer.WriteAttributeString(PdpAppConst.ItemIsAuthorPrivateXnam, ResrepIsAuthorPrivate.ToString().ToLower());
    writer.WriteAttributeString(PdpAppConst.ItemIsAgentSharedXnam, ResrepIsAgentShared.ToString().ToLower());
    writer.WriteAttributeString(PdpAppConst.ItemIsUpdaterLimitedXnam, ResrepIsUpdaterLimited.ToString().ToLower());
    writer.WriteAttributeString(PdpAppConst.ItemIsManagerReleasedXnam, ResrepIsManagerReleased.ToString().ToLower());
    EntityMetadata.WriteXml(writer);
    RecordMetadata.WriteXml(writer);
    InfosetMetadata.WriteXml(writer);
    writer.WriteEndElement();
  }

  public override void ReadXml(XmlReader reader)
  {
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
          string attrNam = reader.LocalName;
          string attrVal = reader.GetAttribute(attrNam);
          switch (attrNam)
          {
            case PdpAppConst.ItemIsAuthorPrivateXnam:
              ResrepIsAuthorPrivate = Convert.ToBoolean(attrVal);
              break;
            case PdpAppConst.ItemIsAgentSharedXnam:
              ResrepIsAgentShared = Convert.ToBoolean(attrVal);
              break;
            case PdpAppConst.ItemIsUpdaterLimitedXnam:
              ResrepIsUpdaterLimited = Convert.ToBoolean(attrVal);
              break;
            case PdpAppConst.ItemIsManagerReleasedXnam:
              ResrepIsManagerReleased = Convert.ToBoolean(attrVal);
              break;
            default:
              break;
          }
        }
      }
      reader.Read();
      EntityMetadata.ReadXml(reader);
      RecordMetadata.ReadXml(reader);
      InfosetMetadata.ReadXml(reader);
      reader.Read();
    }
  }

}

[KnownType(typeof(NpdsResrepList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsResrepList : ANpdsXsgItemList<NpdsResrepItem>
{
  public NpdsResrepList() : base() { }
  public NpdsResrepList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

