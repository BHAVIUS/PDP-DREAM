﻿// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlValidater
{
  public NpdsXmlValidater(QebiUserRestContext qurc, XmlReaderSettings? xrs = null)
  {
    if (qurc == null) 
    { throw new ArgumentNullException("qurc in PdpPrcXmlValidater"); }
    QURC = qurc;
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
  public QebiUserRestContext QURC { set; get; }

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
    var xsdBaseUrl = QURC.NpdsReqstXmlSchemaUrl;
    // TODO: recode path with relative path
    if (string.IsNullOrEmpty(xsdBaseUrl))
    { QURC.ResponseNote = "xsdBaseUrl is null or empty; "; }

    // Add the NPDS schema to the collection.
    // null on target namespace URI defaults to use of target namespace defined in schema
    // but that null default does not appear to be working as of 7/5/2017 ????
    // string xsigTNS = "http://www.w3.org/2000/09/xmldsig#";
    // XSS.Add(xsigTNS, xsdBaseUrl + "xmldsig-core-schema-pdp.xsd");
    // string npdsTNS = "http://npds.portaldoors.org/nsvo/npdsystem#";
    // XSS.Add(npdsTNS, xsdBaseUrl + "npdsroot.xsd");
    XSS.Add("http://npds.portaldoors.org/nsvo/npdsystem#", xsdBaseUrl + "npdsroot.xsd");
    try { XSS.Compile(); }
    catch (Exception er) { QURC.ResponseNote += er.Message; }

    if (!XSS.IsCompiled)
    {
      QURC.ResponseNote = "XML Schema set did not compile. ";
    }
    else
    {
      QURC.ResponseNote = "XML Schema set compiled with schemas: ";
      if (QURC.VerboseFormat)
      {
        // list target namespace for all schemas in set
        foreach (XmlSchema schema in XSS.Schemas())
        {
          QURC.ResponseNote = $"SourceUri = {schema.SourceUri} with TargetNamespace = {schema.TargetNamespace}; ";
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
            QURC.ResponseNote = veaMsg;
          }
        };

      // Create the XmlReader object.
      // note that xsi location attributes not necessary in the xml document tested
      // because of way current flags are set on xrSettings.ValidationFlags

      var sr = new StringReader(xmlDocument);
      var xr = XmlReader.Create(sr, XRS);
      var ppxwr = new NpdsXmlWrappingReader(QURC, xr, XRS);

      // Parse the file.
      try
      {
        while (ppxwr.Read()) { };
        if (xmlErrors == 0 && xmlWarnings == 0) { isValidNpdsXml = true; }
        QURC.ResponseNote = $"NPDS XML Schema Validity: {isValidNpdsXml} with {xmlErrors} errors and {xmlWarnings} warnings.";
      }
      catch (XmlSchemaException er)
      {
        QURC.ResponseNote = "XSD error: " + er.Message;
      }
      catch (XmlException er)
      {
        QURC.ResponseNote = "XML error: " + er.Message;
      }
      catch (Exception er)
      {
        QURC.ResponseNote = "Exception: " + er.Message;
      }
      // finally { }
    }
    return isValidNpdsXml;
  }

}

