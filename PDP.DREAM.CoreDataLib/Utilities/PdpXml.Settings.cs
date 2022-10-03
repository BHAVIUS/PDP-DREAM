// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class PdpXml
{

  public static XmlReaderSettings CreateXmlReaderSettings(bool xsdValidate = false)
  {
    var settings = new XmlReaderSettings()
    {
      CheckCharacters = true,
      DtdProcessing = DtdProcessing.Prohibit,
      IgnoreComments = false,
      IgnoreProcessingInstructions = false,
      IgnoreWhitespace = false,
      MaxCharactersFromEntities = 0, // no limits
      MaxCharactersInDocument = 0, // no limits
    };
    if (xsdValidate)
    {
      settings.ConformanceLevel = ConformanceLevel.Document;
      settings.ValidationFlags = (XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings
        | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes);
      settings.ValidationType = ValidationType.Schema;
    }
    else
    {
      settings.ValidationType = ValidationType.None;
    }
    return settings;
  }

  public static XmlWriterSettings CreateXmlWriterSettings()
  {
    var xws = new XmlWriterSettings()
    {
      CheckCharacters = true,
      // if CloseOutput false 
      // then does not close the underlying stream when the writer is close
      CloseOutput = true,
      ConformanceLevel = ConformanceLevel.Document,
      Encoding = Encoding.UTF8,
      Indent = false,
      NamespaceHandling = NamespaceHandling.OmitDuplicates,
      NewLineHandling = NewLineHandling.None,
      NewLineOnAttributes = false,
      OmitXmlDeclaration = false,
      WriteEndDocumentOnClose = true
    };
    return xws;
  }

  public static XDocument ReadXmlFromFileToXDocument(string xmlFileName)
  {
    // 1. Get the stream for the file located on the local hard drive.
    Stream xStrm = File.Open(xmlFileName, FileMode.Open);
    // 2. Create a new instance of the XmlSerializer class.
    XmlSerializer serializer = new XmlSerializer(typeof(XDocument));
    // 3. Create a new instance of the StreamReader class.
    StreamReader xRdr = new StreamReader(xStrm);
    // 4. Deserialize the data
    XDocument xDoc = (XDocument)(serializer.Deserialize(xRdr));
    return xDoc;
  }

  public static XmlNodeList ReadXmlFromFileToXmlNodeList(string xmlFileName)
  {
    // 1. Get the stream for the file located on the local hard drive.
    Stream xmlStream = File.Open(xmlFileName, FileMode.Open);
    // 2. Create a new instance of the XmlSerializer class.
    XmlSerializer serializer = new XmlSerializer(typeof(XmlNodeList));
    // 3. Create a new instance of the StreamReader class.
    StreamReader reader = new StreamReader(xmlStream);
    // 4. Deserialize the data
    XmlNodeList list = (XmlNodeList)(serializer.Deserialize(reader));
    return list;
  }


  // TODO: deprecate and replace by current ASP.NET Core encoder for XML
  public static string EncodeXml(string inXml)
  {
    string outXml = string.Empty;

    if (!string.IsNullOrWhiteSpace(inXml))
    {
      // outXml = inXml.Replace("\"", "&quot;");
      outXml = inXml.Replace("'", "&apos;");
      outXml = outXml.Replace("<", "&lt;");
      outXml = outXml.Replace(">", "&gt;");
      // TODO: what about pre-existing &amp; ???
      // is it necessary to decode before encoding ????
      outXml = outXml.Replace("&", "&amp;");
    }
    return outXml;
  }

  public static string FormatXml(Stream inStrm)
  {
    // read from input stream
    XmlTextReader xreader = new XmlTextReader(inStrm);
    xreader.WhitespaceHandling = WhitespaceHandling.None;
    // write to output stream
    MemoryStream outStrm = new MemoryStream();
    XmlTextWriter xwriter = new XmlTextWriter(outStrm, Encoding.UTF8);
    xwriter.Formatting = Formatting.Indented;
    // complete the read/write cycle
    while (!xreader.EOF)
    {
      xwriter.WriteNode(xreader, true);
    }
    xwriter.Flush();
    // reset MemoryStream and read it to string
    outStrm.Position = 0;
    StreamReader sreader = new StreamReader(outStrm);
    return sreader.ReadToEnd().ToString();
  }

  public static void XmlSerializeToConsole(IXmlSerializable serObj, bool needPause = false)
  {
    using (XmlTextWriter xtw = new XmlTextWriter(Console.Out))
    {
      serObj.WriteXml(xtw);
      xtw.Flush();
    }
    ConsoleContinue(needPause, true);
  }

}