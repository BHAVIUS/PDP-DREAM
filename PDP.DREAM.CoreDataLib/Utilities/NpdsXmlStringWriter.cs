// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlStringWriter<Tdto>
{
  // Tdto is generic Type of the Data Transfer Object
  // ATTN: compare with PdpXmlResponseWriter

  public NpdsXmlStringWriter(Tdto dtoToSerialize, NpdsClientWrace pdpRestContext, XmlWriterSettings? xmlWriterSettings = null)
  {
    if (dtoToSerialize == null)  // ?? operator cannot be applied to generic types
    {
      throw new ArgumentNullException("dataTransferObjectToSerialize in PdpXmlStringWriter");
    }
    DTO = dtoToSerialize;
    WRACE = pdpRestContext ?? throw new ArgumentNullException("pdpRestContext in PdpXmlStringWriter");
    // TODO: recode the ResponseStatus feature
    // PRC.ResponseStatus = HttpResponseExtensions.ResponseStatus();
    XWS = xmlWriterSettings ?? QebXml.CreateXmlWriterSettings();
  }

  // Data Transfer Object
  public Tdto DTO { set; get; }

  // QEBI User REST Context
  public NpdsClientWrace WRACE { set; get; }

  // XML Writer and Settings
  public NpdsXmlWrappingWriter NXWW { set; get; }
  public XmlWriterSettings XWS { set; get; }

  // XML string result

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
    var esw = new EncodedStringWriter();
    NXWW = new NpdsXmlWrappingWriter(WRACE, esw, XWS);
    var xs = new XmlSerializer(DTO.GetType());
    xs.Serialize(NXWW, DTO);  // .SerializeAsXElement ???
    xml = esw.ToString();  // retain copy in xml property
  }

}

// end file