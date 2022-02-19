// NpdsApiServerNexusItem.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsNexusServerItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsNexusServerItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsNexusServerItem() : base() { this.Initialize(); }
    public NpdsNexusServerItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = NpdsConst.ResrepFormat.Nexus)
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.NexusServerXnam);
    }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      if (writer.PRC.NexusRecords != null)
      {
        writer.PRC.NexusRecords.WriteXml(writer);
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteAttributeString(NpdsConst.ListCountXnam, "0");
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
      reader.ReadToFollowing(NpdsConst.ResponseXnam);
      if (reader.IsStartElement(NpdsConst.ResponseXnam))
      {
        reader.Read();
        reader.PRC.ResponseStatus = reader.ReadElementString(NpdsConst.ResponseStatusXnam);
        reader.PRC.ResponseAnswer.ReadXml(reader);
        reader.PRC.ResponseRelated.ReadXml(reader);
        reader.PRC.ResponseReferred.ReadXml(reader);
      }

    }

  }

}