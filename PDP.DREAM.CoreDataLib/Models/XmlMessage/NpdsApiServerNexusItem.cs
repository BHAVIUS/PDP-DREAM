// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsNexusServerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsNexusServerItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsNexusServerItem() : base() { this.Initialize(); }
  public NpdsNexusServerItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = PdpAppConst.NpdsResrepFormat.Nexus)
  {
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.NexusServerXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (writer.QURC.NexusRecords != null)
    {
      writer.QURC.NexusRecords.WriteXml(writer);
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

