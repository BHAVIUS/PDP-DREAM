// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsCoreServerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsCoreServerItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsCoreServerItem() : base() { this.Initialize(); }
  public NpdsCoreServerItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = PdpAppConst.NpdsResrepFormat.Core)
  {
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.CoreServerXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (writer.QURC.CoreRecords != null)
    {
      writer.QURC.CoreRecords.WriteXml(writer);
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteAttributeString(PdpAppConst.ListCountXnam, "0");
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
    reader.ReadToFollowing(PdpAppConst.ResponseXnam);
    if (reader.IsStartElement(PdpAppConst.ResponseXnam))
    {
      reader.Read();
      reader.QURC.ResponseStatus = reader.ReadElementString(PdpAppConst.ResponseStatusXnam);
      reader.QURC.ResponseAnswer.ReadXml(reader);
      reader.QURC.ResponseRelated.ReadXml(reader);
      reader.QURC.ResponseReferred.ReadXml(reader);
    }

  }

}

