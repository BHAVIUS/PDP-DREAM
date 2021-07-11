// PdpPrcXmlValidaters.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Schema;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public class PdpPrcXmlValidater
  {
    public PdpPrcXmlValidater(PdpRestContext prc, XmlReaderSettings? xrs = null)
    {
      PRC = prc ?? new PdpRestContext(null);
      XRS = xrs ?? PdpXml.CreateXmlReaderSettings(true);
      // Create the XmlSchemaSet class.
      // note that default XmlResolver for resolving external entities, schema locations,
      //  and xs:include or xs:import elements is the XmlUrlResolver with no user credentials
      var resolver = new XmlUrlResolver();
      resolver.Credentials = CredentialCache.DefaultCredentials;
      XRS.XmlResolver = resolver;
      XSS = new XmlSchemaSet();
      XSS.XmlResolver = resolver;
    }

    // the PDP REST Context
    public PdpRestContext PRC { set; get; }

    // the XML Reader and Settings
    public PdpPrcXmlWrappingReader PXR { set; get; }
    public XmlReaderSettings XRS { set; get; }
    public XmlSchemaSet XSS { get; set; }

    public bool ValidateNpdsXmlMessage(string xmlDocument)
    {
      // TODO finish coding and testing this method 
      var xmlErrors = 0;
      var xmlWarnings = 0;
      var isValidNpdsXml = false;
      var xsdBaseUrl = PRC.PdpReqstXmlSchemaUrl;
      // TODO: recode path with relative path
      if (string.IsNullOrEmpty(xsdBaseUrl))
      { PRC.ResponseNote = "xsdBaseUrl is null or empty; "; }

      // Add the NPDS schema to the collection.
      // null on target namespace URI defaults to use of target namespace defined in schema
      // but that null default does not appear to be working as of 7/5/2017 ????
      // string xsigTNS = "http://www.w3.org/2000/09/xmldsig#";
      // XSS.Add(xsigTNS, xsdBaseUrl + "xmldsig-core-schema-pdp.xsd");
      // string npdsTNS = "http://npds.portaldoors.org/nsvo/npdsystem#";
      // XSS.Add(npdsTNS, xsdBaseUrl + "npdsroot.xsd");
      XSS.Add("http://npds.portaldoors.org/nsvo/npdsystem#", xsdBaseUrl + "npdsroot.xsd");
      try { XSS.Compile(); }
      catch (Exception er) { PRC.ResponseNote += er.Message; }

      if (!XSS.IsCompiled)
      {
        PRC.ResponseNote = "XML Schema set did not compile. ";
      }
      else
      {
        PRC.ResponseNote = "XML Schema set compiled with schemas: ";
        if (PRC.VerboseFormat)
        {
          // list target namespace for all schemas in set
          foreach (XmlSchema schema in XSS.Schemas())
          {
            PRC.ResponseNote = $"SourceUri = {schema.SourceUri} with TargetNamespace = {schema.TargetNamespace}; ";
          }
        }
        // XRS.Schemas.Add(targetNamespace, schemaUri) and 3 other overloads
        XRS.Schemas = XSS;
        XRS.ValidationEventHandler +=
          (object sender, ValidationEventArgs veArgs) =>
            {
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
                PRC.ResponseNote = veaMsg;
              }
            };

        // Create the XmlReader object.
        // note that xsi location attributes not necessary in the xml document tested
        // because of way current flags are set on xrSettings.ValidationFlags

        var sr = new StringReader(xmlDocument);
        var xr = XmlReader.Create(sr, XRS);
        var ppxwr = new PdpPrcXmlWrappingReader(PRC, xr, XRS);

        // Parse the file.
        try
        {
          while (ppxwr.Read()) { };
          if (xmlErrors == 0 && xmlWarnings == 0) { isValidNpdsXml = true; }
          PRC.ResponseNote = $"NPDS XML Schema Validity: {isValidNpdsXml} with {xmlErrors} errors and {xmlWarnings} warnings.";
        }
        catch (XmlSchemaException er)
        {
          PRC.ResponseNote = "XSD error: " + er.Message;
        }
        catch (XmlException er)
        {
          PRC.ResponseNote = "XML error: " + er.Message;
        }
        catch (Exception er)
        {
          PRC.ResponseNote = "Exception: " + er.Message;
        }
        // finally { }
      }
      return isValidNpdsXml;
    }

  }

}
