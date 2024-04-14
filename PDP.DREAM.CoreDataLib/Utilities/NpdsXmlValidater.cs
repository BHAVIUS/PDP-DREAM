// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlValidater
{
  public NpdsXmlValidater(NpdsClientWrace wrace, XmlReaderSettings? xrs = null)
  {
    if (wrace == null)
    { throw new ArgumentNullException("wrace in PdpPrcXmlValidater"); }
    WRACE = wrace;
    XRS = xrs ?? QebXml.CreateXmlReaderSettings(true);
    // Create the XmlSchemaSet class.
    // note that default XmlResolver for resolving external entities, schema locations,
    //  and xs:include or xs:import elements is the XmlUrlResolver with no user credentials
    var resolver = new XmlUrlResolver();
    resolver.Credentials = CredentialCache.DefaultCredentials;
    XRS.XmlResolver = resolver;
    XSS = new XmlSchemaSet();
    XSS.XmlResolver = resolver;
  }

  // the QEB User REST Context
  public NpdsClientWrace WRACE { set; get; }

  // the XML Reader and Settings
  public NpdsXmlWrappingReader NXWR { set; get; }
  public XmlReaderSettings XRS { set; get; }
  public XmlSchemaSet XSS { get; set; }

  public bool ValidateNpdsXmlMessage(string xmlDocument)
  {
    // TODO finish coding and testing this method 
    var xmlErrors = 0;
    var xmlWarnings = 0;
    var isValidNpdsXml = false;
    var xsdBaseUrl = WRACE.NpdsReqstXmlSchemaUrl;
    // var newLine = Environment.NewLine;

    // TODO: recode path with relative path
    if (string.IsNullOrEmpty(xsdBaseUrl))
    { WRACE.ResponseNote = "xsdBaseUrl is null or empty; "; }

    // Add the NPDS schema to the collection.
    // null on target namespace URI defaults to use of target namespace defined in schema
    // but that null default does not appear to be working as of 7/5/2017 ????
    // string xsigTNS = "http://www.w3.org/2000/09/xmldsig#";
    // XSS.Add(xsigTNS, xsdBaseUrl + "xmldsig-core-schema-pdp.xsd");
    // XSS.Add(NpdsNamespace, xsdBaseUrl + NpdsXmlSchema);
    XSS.Add(NpdsNamespace, xsdBaseUrl + NpdsXmlSchema);
    try { XSS.Compile(); }
    catch (Exception er) { WRACE.ResponseNote += er.Message; }

    if (!XSS.IsCompiled)
    {
      WRACE.ResponseNote = "XML Schema set did not compile. ";
    }
    else
    {
      WRACE.ResponseNote = $"XML Schema set compiled with schemas: ";
      if (WRACE.VerboseFormat)
      {
        // list target namespace for all schemas in set
        foreach (XmlSchema schema in XSS.Schemas())
        {
          WRACE.ResponseNote = $"SourceUri = {schema.SourceUri}; ";
          WRACE.ResponseNote = $"TargetNamespace = {schema.TargetNamespace}; ";
        }
      }
      // XRS.Schemas.Add(targetNamespace, schemaUri) and 3 other overloads
      XRS.Schemas = XSS;
      XRS.ValidationEventHandler +=
        (object sender, ValidationEventArgs veArgs) => {
          string veaMsg = "";
          if (veArgs.Severity == XmlSeverityType.Error)
          {
            xmlErrors += 1;
            veaMsg = $"XML ERROR: {veArgs.Message}; ";
          }
          else if (veArgs.Severity == XmlSeverityType.Warning)
          {
            xmlWarnings += 1;
            veaMsg = $"XML WARNING: {veArgs.Message}; ";
          }
          if (!string.IsNullOrEmpty(veaMsg))
          {
            WRACE.ResponseNote = veaMsg;
          }
        };

      // Create the XmlReader object.
      // note that xsi location attributes not necessary in the xml document tested
      // because of way current flags are set on xrSettings.ValidationFlags

      var sr = new StringReader(xmlDocument);
      var xr = XmlReader.Create(sr, XRS);
      var ppxwr = new NpdsXmlWrappingReader(WRACE, xr, XRS);

      // Parse the file.
      try
      {
        while (ppxwr.Read()) { };
        if (xmlErrors == 0 && xmlWarnings == 0) { isValidNpdsXml = true; }
        WRACE.ResponseNote = $"NPDS XML Schema Validity: {isValidNpdsXml} with {xmlErrors} errors and {xmlWarnings} warnings.";
      }
      catch (XmlSchemaException er)
      {
        WRACE.ResponseNote = "XSD error: " + er.Message;
      }
      catch (XmlException er)
      {
        WRACE.ResponseNote = "XML error: " + er.Message;
      }
      catch (Exception er)
      {
        WRACE.ResponseNote = "Exception: " + er.Message;
      }
      // finally { }
    }
    return isValidNpdsXml;
  }

}

