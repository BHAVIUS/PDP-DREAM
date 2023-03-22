// PdpXml.Serializers.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities
{
  public static partial class QebXml
  {
    // TODO: build a full set of serializers/deserializers to/from strings and XElements for NPDS REST Context
    // TODO: check performance differences between them

    public static XElement ConvertXmlElementToXElement(XmlElement xmlElm)
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.AppendChild(xmlDoc.ImportNode(xmlElm, true));
      return XElement.Parse(xmlDoc.InnerXml);
    }

    public static XElement XmlelSerialize(this XmlSerializer xs, object o)
    {
      // this extension method from Hanselman blog Posted 2007-08-28
      // http://www.hanselman.com/blog/MixingXmlSerializersWithXElementsAndLINQToXML.aspx
      XDocument d = new XDocument();
      using (XmlWriter w = d.CreateWriter())
      {
        xs.Serialize(w, o);
      }
      XElement e = d.Root;
      e.Remove();
      return e;
    }

    public static string XmldcSerialize(IXmlSerializable serObj, XmlWriterSettings? xws = null)
    {
      // using DataContractSerializer
      if (xws == null) { xws = CreateXmlWriterSettings(); }
      EncodedStringWriter sb = new EncodedStringWriter(xws.Encoding);
      XmlWriter xw = XmlWriter.Create(sb, xws);
      // Dim xdw As XmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xw)
      DataContractSerializer dcs = new DataContractSerializer(serObj.GetType());
      // WriteObject takes Stream, XmlWriter, or XmlDictionaryWriter
      dcs.WriteObject(xw, serObj);
      // dcs.WriteObject(xdw, serObj)
      // xdw.Flush()
      xw.Flush();
      return sb.ToString();
    }

    public static string XmlSerialize(IXmlSerializable serObj, XmlWriterSettings? xws = null)
    {
      if (xws == null) { xws = CreateXmlWriterSettings(); }
      EncodedStringWriter sb = new EncodedStringWriter(xws.Encoding);
      using (XmlWriter xw = XmlWriter.Create(sb, xws))
      {
        serObj.WriteXml(xw);
        xw.Flush();
      }
      return sb.ToString();
    }

    public static T XmlDeserialize<T>(string xmlStr, T serObj) where T : class, IXmlSerializable, new()
    {
      XmlDocument xd = new XmlDocument();
      xd.LoadXml(xmlStr);
      using (XmlNodeReader xnr = new XmlNodeReader(xd))
      {
        serObj.ReadXml(xnr);
      }
      return serObj;
    }

    public static string PdpSerialize(QebiUserRestContext qurc, IXmlSerializable ixs, XmlWriterSettings? xws = null)
    {
      if (qurc == null) { throw new ArgumentNullException("qurc in SerializeToString"); }
      if (ixs == null) { throw new ArgumentNullException("ixs in SerializeToString"); }
      if (xws == null) { xws = CreateXmlWriterSettings(); }
      // ATTN: StringBuilder fixed encoding of UTF-16 overrides any encoding carried by xws
      // so cannot use framework StringBuilder and instead must use enhanced variation
      // EncodedStringBuilder : StringWriter : TextWriter so esb is a TextWriter
      EncodedStringWriter esb = null;
      NpdsXmlWrappingWriter pxw = null;
      XmlSerializer xsz = null;
      var xml = string.Empty;
      try
      {
        esb = new EncodedStringWriter(xws.Encoding);
        pxw = new NpdsXmlWrappingWriter(qurc, esb, xws); // create the writer with the settings
        xsz = new XmlSerializer(ixs.GetType()); // create the serializer with the object type
        xsz.Serialize(pxw, ixs); // serialize with the writer and the object value
        pxw.Flush(); // flush the writer
        xml = esb.ToString(); // convert to string
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
      }
      finally
      {
        if (esb != null) { esb.Dispose(); }
        if (pxw != null) { pxw.Dispose(); }
      }
      return xml;
    }

    public static T PdpDeserialize<T>(QebiUserRestContext qurc, string xmlStr, T serObj) where T : class, IXmlSerializable, new()
    {
      XmlDocument xd = new XmlDocument();
      xd.LoadXml(xmlStr);
      using (var xnr = new XmlNodeReader(xd))
      {
        using (var pxr = new NpdsXmlWrappingReader(qurc, xnr))
        {
          serObj.ReadXml(pxr);
        }
      }
      return serObj;
    }

    public static bool RoundTrip<T>(T serObj1, T serObj2, string pairName = "", XmlWriterSettings? xws = null) where T : class, IXmlSerializable, new()
    {
      if (xws == null) { xws = CreateXmlWriterSettings(); }
      bool pairAreEquiv = false;
      string str = XmlSerialize(serObj1, xws);
      serObj2 = XmlDeserialize(str, serObj2);
      pairAreEquiv = serObj1.Equals(serObj2);

      Debug.WriteLine(str);
      Debug.WriteLine(pairName + " String Pair Equivalence = " + pairAreEquiv.ToString());

      return pairAreEquiv;
    }

    public static bool RoundTripX2<T>(QebiUserRestContext qurc, T serObj1, T serObj2, string pairName = "",
      XmlWriterSettings? xws = null) where T : class, IXmlSerializable, new()
    {
      if (xws == null) { xws = CreateXmlWriterSettings(); }
      var strcomp = StringComparison.Ordinal;
      bool pairAreEquiv = false;
      string str1, str2;

      // XML serialized strings may have HTML encoded entities so str1 and str2 may have entities
      // XML deserialized strings should not have any HTML encoded entities so strings in serObj1 and serObj2 should not have any encoded entities

      if (qurc == null)
      {
        str1 = XmlSerialize(serObj1, xws);
        Debug.WriteLine(str1);

        serObj2 = XmlDeserialize(str1, serObj2);
        str2 = XmlSerialize(serObj2, xws);
        Debug.WriteLine(str2);
      }
      else
      {
        str1 = PdpSerialize(qurc, serObj1, xws);
        Debug.WriteLine(str1);

        serObj2 = PdpDeserialize(qurc, str1, serObj2);
        str2 = PdpSerialize(qurc, serObj2, xws);
        Debug.WriteLine(str2);
      }

      pairAreEquiv = string.Equals(str1, str2, strcomp);
      Debug.WriteLine(pairName + " String Pair Equivalence = " + pairAreEquiv.ToString());

      return pairAreEquiv;
    }

  }

}
