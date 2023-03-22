// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlResponseWriter<T> : ActionResult  //, IDisposable
{
  // ATTN: compare with PdpXmlStringWriter

  public NpdsXmlResponseWriter(T dataTransferObjectToSerialize, QebiUserRestContext pdpRestContext, XmlWriterSettings? xmlWriterSettings = null)
  {
    if (dataTransferObjectToSerialize == null)  // ?? operator cannot be applied to generic types
    {
      throw new ArgumentNullException("dataTransferObjectToSerialize in PdpXmlResponseWriter");
    }
    DTO = dataTransferObjectToSerialize;
    QURC = pdpRestContext ?? throw new ArgumentNullException("pdpRestContext in PdpXmlResponseWriter");
    // TODO: recode the ResponseStatus feature
    // PRC.ResponseStatus = HttpResponseExtensions.ResponseStatus();
    XWS = xmlWriterSettings ?? QebXml.CreateXmlWriterSettings();
  }

  // the Data Transfer Object
  public T DTO { set; get; }

  // the QEB User REST Context
  public QebiUserRestContext QURC { set; get; }

  // the XML Writer and Settings
  public NpdsXmlWrappingWriter NXWW { set; get; }
  public XmlWriterSettings XWS { set; get; }

  // the XML response result

  public override void ExecuteResult(ActionContext ac)
  {
    ac.HttpContext.Response.ContentType = "text/xml";
    NXWW = new NpdsXmlWrappingWriter(QURC, ac.HttpContext.Response.Body, XWS);
    var xs = new XmlSerializer(DTO.GetType());
    xs.Serialize(NXWW, DTO);  // .SerializeAsXElement ???
    ac.HttpContext.Response.Body.Flush();
    // to retain copy in an xml string property may require enabling buffering
    // https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body
  }

}

// end file