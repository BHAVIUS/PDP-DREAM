// NpdsApiServerPortalItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsPortalServerItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsPortalServerItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsPortalServerItem() : base() { this.Initialize(); }
    public NpdsPortalServerItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = NpdsConst.ResrepFormat.PORTAL)
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.PortalServerXnam);
    }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      if (writer.PRC.PortalRecords != null)
      {
        writer.PRC.PortalRecords.WriteXml(writer);
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