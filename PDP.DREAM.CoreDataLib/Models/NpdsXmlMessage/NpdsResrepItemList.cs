// NpdsResrepItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  // TODO: pattern discrepancy ?!? 
  //  higher level objects NexusResrep/EntityMetadata/RecordMetadata/InfosetMetadata
  //     have input ResrepFormat, but lower level objects have input FieldRule ?!?
  //   what should be injected? should only ResrepFormat be carried through tree instead of FieldRule? or leave as is?
  [KnownType(typeof(NpdsResrepItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsResrepItem : ANpdsXsgBaseItem<Object>, INpdsResrep
  {
    public NpdsResrepItem() : base() { this.Initialize(); }
    public NpdsResrepItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }
    public NpdsResrepItem(NpdsConst.ResrepFormat rrf, Guid? key) : base(typeof(Guid?), key) { this.Initialize(rrf); }

    protected void Initialize(NpdsConst.ResrepFormat rrf = default(NpdsConst.ResrepFormat))
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.NexusResrepItemXnam, NpdsConst.NexusResrepListXnam, NpdsConst.ResrepKeyXnam);
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
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      writer.WriteAttributeString(NpdsConst.ItemIsAuthorPrivateXnam, ResrepIsAuthorPrivate.ToString().ToLower());
      writer.WriteAttributeString(NpdsConst.ItemIsAgentSharedXnam, ResrepIsAgentShared.ToString().ToLower());
      writer.WriteAttributeString(NpdsConst.ItemIsUpdaterLimitedXnam, ResrepIsUpdaterLimited.ToString().ToLower());
      writer.WriteAttributeString(NpdsConst.ItemIsManagerReleasedXnam, ResrepIsManagerReleased.ToString().ToLower());
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
              case NpdsConst.ItemIsAuthorPrivateXnam:
                ResrepIsAuthorPrivate = Convert.ToBoolean(attrVal);
                break;
              case NpdsConst.ItemIsAgentSharedXnam:
                ResrepIsAgentShared = Convert.ToBoolean(attrVal);
                break;
              case NpdsConst.ItemIsUpdaterLimitedXnam:
                ResrepIsUpdaterLimited = Convert.ToBoolean(attrVal);
                break;
              case NpdsConst.ItemIsManagerReleasedXnam:
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
    public NpdsResrepList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}