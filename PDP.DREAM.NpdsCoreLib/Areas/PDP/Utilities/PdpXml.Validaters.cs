using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static partial class PdpXml
  {
    public static bool XmlValidate(string xml, string xsd)
    {
      bool isValidXml;
      try
      {
        // alternate approach with 
        // var xsv = new XmlSchemaValidator();

        // build settings
        XmlReaderSettings _xmlReaderSettings = null;
        if (string.IsNullOrEmpty(xsd)) // without XSD schema
        {
          _xmlReaderSettings = new XmlReaderSettings() { ValidationType = ValidationType.None };
        }
        else  // with XSD schema as a string
        {
          StringReader _xsdStream = new StringReader(xsd);
          XmlSchema _xmlSchema = XmlSchema.Read(_xsdStream, null);
          _xmlReaderSettings = new XmlReaderSettings() { ValidationType = ValidationType.Schema };
          _xmlReaderSettings.Schemas.Add(_xmlSchema);
        }
        // build XML reader
        StringReader _xmlStream = new StringReader(xml);
        XmlReader _xmlReader = XmlReader.Create(_xmlStream, _xmlReaderSettings);
        // validate by trying to read through to end
        using (_xmlReader) { while (_xmlReader.Read()) { } }
        // validation succeeded
        isValidXml = true;
      }
      catch
      {
        // validation failed
        isValidXml = false;
      }
      return isValidXml;
    }

    public static bool XmlValidate(string xmlFileUri, XmlSchemaSet schemas, XmlReaderSettings? settings = null)
    {
      bool xmlIsValid;
      // Create an XmlUrlResolver with default credentials.
      var resolver = new XmlUrlResolver();
      resolver.Credentials = CredentialCache.DefaultCredentials;
      if (settings == null)
      {
        settings = new XmlReaderSettings();
        settings.XmlResolver = resolver;
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.AllowXmlAttributes
          | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationEventHandler += new ValidationEventHandler(XmlValidationHandler);
      }
      if (!schemas.IsCompiled)
      {
        if (schemas.Count == 0)
        { // https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlresolver?view=net-5.0
          string appBaseUrl = PdpWebAppHttpContext.BaseUrl;
          var baseUri = new Uri(appBaseUrl);
          var fulluri = resolver.ResolveUri(baseUri, "~/xsd/xmldsig-core-schema-pdp.xsd");
          schemas.Add(null, fulluri.ToString());
          fulluri = resolver.ResolveUri(baseUri, "~/xsd/npdsroot.xsd");
          schemas.Add(null, fulluri.ToString());
        }
        schemas.Compile();
      }
      if (schemas.IsCompiled)
      {
        settings.Schemas.Add(schemas);
      }
      var xmlRdr = XmlReader.Create(xmlFileUri, settings);
      try
      {
        while (xmlRdr.Read()) { }
        xmlIsValid = true;
      }
      catch (XmlException exc)
      {
        Console.WriteLine($"XmlException: {exc.Message}");
        xmlIsValid = false;
      }
      catch (XmlSchemaValidationException exc)
      {
        Console.WriteLine($"XmlSchemaValidationException : {exc.Message}");
        xmlIsValid = false;
      }
      catch (Exception exc)
      {
        Console.WriteLine($"Exception: {exc.Message}");
        xmlIsValid = false;
      }
      return xmlIsValid;
    }

    public static void XmlValidationHandler(object evtsrc, ValidationEventArgs evtargs)
    {
      if (evtargs.Severity == XmlSeverityType.Error)
      {
        Console.WriteLine($"Error: {evtargs.Message}");
      }
      else if (evtargs.Severity == XmlSeverityType.Warning)
      {
        Console.WriteLine($"Warning: {evtargs.Message}");
      }
    }

  }

}
