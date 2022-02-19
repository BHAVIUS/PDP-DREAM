// PdpPrcXmlStringWriter.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Xml;
using System.Xml.Serialization;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Utilities
{
  public class PdpPrcXmlStringWriter<T>
  {
    // ATTN: compare with PdpXmlResponseWriter

    public PdpPrcXmlStringWriter(T dataTransferObjectToSerialize, PdpRestContext pdpRestContext, XmlWriterSettings? xmlWriterSettings = null)
    {
      if (dataTransferObjectToSerialize == null)  // ?? operator cannot be applied to generic types
      {
        throw new ArgumentNullException("dataTransferObjectToSerialize in PdpXmlStringWriter");
      }
      DTO = dataTransferObjectToSerialize;
      PRC = pdpRestContext ?? throw new ArgumentNullException("pdpRestContext in PdpXmlStringWriter");
      // TODO: recode the ResponseStatus feature
      // PRC.ResponseStatus = HttpResponseExtensions.ResponseStatus();
      XWS = xmlWriterSettings ?? PdpXml.CreateXmlWriterSettings();
    }

    // the Data Transfer Object
    public T DTO { set; get; }

    // the PDP REST Context
    public PdpRestContext PRC { set; get; }

    // the XML Writer and Settings
    public PdpPrcXmlWrappingWriter PXW { set; get; }
    public XmlWriterSettings XWS { set; get; }

    // the XML string result

    public string XML
    {
      get
      {
        if (string.IsNullOrEmpty(xml)) { BuildXmlString(); }
        return xml;
      }
    }
    private string xml;

    public void BuildXmlString()
    {
      var sb = new EncodedStringWriter();
      PXW = new PdpPrcXmlWrappingWriter(PRC, sb, XWS);
      var xs = new XmlSerializer(DTO.GetType());
      xs.Serialize(PXW, DTO);  // .SerializeAsXElement ???
      xml = sb.ToString();  // retain copy in xml property
    }

  }

}
