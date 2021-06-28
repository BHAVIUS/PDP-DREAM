using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsCoreServerItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsCoreServerItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsCoreServerItem() : base() { this.Initialize(); }
    public NpdsCoreServerItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = NpdsConst.ResrepFormat.Core)
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.CoreServerXnam);
    }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      if (writer.PRC.CoreRecords != null)
      {
        writer.PRC.CoreRecords.WriteXml(writer);
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