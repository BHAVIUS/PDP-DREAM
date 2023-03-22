// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[XmlRoot(ElementName = PdpAppConst.NpdsRootXnam, Namespace = PdpAppConst.NpdsNamespace)]
public class NpdsResrepXmlRoot : IXmlSerializable
{
  public NpdsResrepXmlRoot() { }

  public string Version { set; get; } = PdpAppConst.NpdsVersion;

  public void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;

    // document root element begin (not necessary when using XmlRoot attribute above)
    // writer.WriteStartDocument(); // causes error
    // writer.WriteStartElement(NpdsConstants.NpdsRootXnam);
    // writer.WriteAttributeString("xmlns", NpdsConstants.NpdsNamespace); // causes error
    // writer.WriteAttributeString("", "xmlns", "", NpdsConstants.NpdsNamespace); // causes error
    writer.WriteAttributeString(PdpAppConst.NpdsVersionXnam, PdpAppConst.NpdsVersion);
    writer.WriteComment(PdpAppConst.NpdsCopyright);

    (new NpdsClientRequestItem()).WriteXml(writer);
    (new NpdsServerResponseItem()).WriteXml(writer);
    (new NpdsCoreServerItem()).WriteXml(writer);
    (new NpdsPortalServerItem()).WriteXml(writer);
    (new NpdsDoorsServerItem()).WriteXml(writer);
    (new NpdsNexusServerItem()).WriteXml(writer);

    // document root element end
    // writer.WriteEndElement();
    // writer.WriteEndDocument(); // causes error
    // writer.Close(); // causes error
  }

  public void ReadXml(XmlReader xReader)
  {
    var reader = (NpdsXmlWrappingReader)xReader;

    reader.MoveToContent();
    if (reader.IsEmptyElement)
    {
      reader.ReadStartElement();
    }
    else if (reader.IsStartElement(PdpAppConst.NpdsRootXnam))
    {
      if (reader.HasAttributes)
      {
        while (reader.MoveToNextAttribute())
        {
          string attrnam = reader.LocalName;
          string attrval = reader.GetAttribute(attrnam);
          if (!string.IsNullOrEmpty(attrval))
          {
            if (attrnam == PdpAppConst.NpdsVersionXnam)
            { this.Version = attrval; }
          }
        }
      }
    }

  }

  public XmlSchema GetSchema()
  {
    XmlSchema xs = null;
    return xs;
  }

}

