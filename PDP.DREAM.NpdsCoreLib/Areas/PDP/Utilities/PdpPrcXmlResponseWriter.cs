// PdpPrcXmlResponseWriter.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public class PdpPrcXmlResponseWriter<T> : ActionResult  //, IDisposable
  {
  	// ATTN: compare with PdpXmlStringWriter
	
    public PdpPrcXmlResponseWriter(T dataTransferObjectToSerialize, PdpRestContext pdpRestContext, XmlWriterSettings? xmlWriterSettings = null)
    {
      if (dataTransferObjectToSerialize == null)  // ?? operator cannot be applied to generic types
      {
        throw new ArgumentNullException("dataTransferObjectToSerialize in PdpXmlResponseWriter");
      }
      DTO = dataTransferObjectToSerialize;
      PRC = pdpRestContext ?? throw new ArgumentNullException("pdpRestContext in PdpXmlResponseWriter");
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

    // the XML response result
	
    public override void ExecuteResult(ActionContext ac)
    {
      ac.HttpContext.Response.ContentType = "text/xml";
      PXW = new PdpPrcXmlWrappingWriter(PRC, ac.HttpContext.Response.Body, XWS);
      var xs = new XmlSerializer(DTO.GetType());
      xs.Serialize(PXW, DTO);  // .SerializeAsXElement ???
      ac.HttpContext.Response.Body.Flush();
      // to retain copy in an xml string property may require enabling buffering
      // https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body
    }

  }

}
