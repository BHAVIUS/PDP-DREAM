// NpdsResrepXmlRoot.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [XmlRoot(ElementName = NpdsConst.NpdsRootXnam, Namespace = NpdsConst.NpdsNamespace)]
  public class NpdsResrepXmlRoot : IXmlSerializable
  {
    public NpdsResrepXmlRoot() { }

    public string Version { set; get; } = NpdsConst.NpdsVersion;

    public void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;

      // document root element begin (not necessary when using XmlRoot attribute above)
      // writer.WriteStartDocument(); // causes error
      // writer.WriteStartElement(NpdsConstants.NpdsRootXnam);
      // writer.WriteAttributeString("xmlns", NpdsConstants.NpdsNamespace); // causes error
      // writer.WriteAttributeString("", "xmlns", "", NpdsConstants.NpdsNamespace); // causes error
      writer.WriteAttributeString(NpdsConst.NpdsVersionXnam, NpdsConst.NpdsVersion);
      writer.WriteComment(NpdsConst.NpdsCopyright);

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
      var reader = (PdpPrcXmlWrappingReader)xReader;

      reader.MoveToContent();
      if (reader.IsEmptyElement)
      {
        reader.ReadStartElement();
      }
      else if (reader.IsStartElement(NpdsConst.NpdsRootXnam))
      {
        if (reader.HasAttributes)
        {
          while (reader.MoveToNextAttribute())
          {
            string attrnam = reader.LocalName;
            string attrval = reader.GetAttribute(attrnam);
            if (!string.IsNullOrEmpty(attrval))
            {
              if (attrnam == NpdsConst.NpdsVersionXnam)
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

}