﻿// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsPortalServerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsPortalServerItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsPortalServerItem() : base() { this.Initialize(); }
  public NpdsPortalServerItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = PdpAppConst.NpdsResrepFormat.PORTAL)
  {
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.PortalServerXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (writer.QURC.PortalRecords != null)
    {
      writer.QURC.PortalRecords.WriteXml(writer);
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

