// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlStringWriter<T>
{
  // ATTN: compare with PdpXmlResponseWriter

  public NpdsXmlStringWriter(T dataTransferObjectToSerialize, QebiUserRestContext pdpRestContext, XmlWriterSettings? xmlWriterSettings = null)
  {
    if (dataTransferObjectToSerialize == null)  // ?? operator cannot be applied to generic types
    {
      throw new ArgumentNullException("dataTransferObjectToSerialize in PdpXmlStringWriter");
    }
    DTO = dataTransferObjectToSerialize;
    QURC = pdpRestContext ?? throw new ArgumentNullException("pdpRestContext in PdpXmlStringWriter");
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

  // the XML string result

  public string XML
  {
    get {
      if (string.IsNullOrEmpty(xml)) { BuildXmlString(); }
      return xml;
    }
  }
  private string xml;

  public void BuildXmlString()
  {
    var sb = new EncodedStringWriter();
    NXWW = new NpdsXmlWrappingWriter(QURC, sb, XWS);
    var xs = new XmlSerializer(DTO.GetType());
    xs.Serialize(NXWW, DTO);  // .SerializeAsXElement ???
    xml = sb.ToString();  // retain copy in xml property
  }

}

// end file