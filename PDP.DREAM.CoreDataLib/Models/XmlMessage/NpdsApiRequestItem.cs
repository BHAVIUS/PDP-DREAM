// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsClientRequestItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsClientRequestItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsClientRequestItem() : base() { this.Initialize(NPDSCD.ResrepFormatDefault); }
  public NpdsClientRequestItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(NpdsResrepFormat rrf)
  {
    base.InitNpdsItem(NpdsFieldRule.Required, RequestXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    // start ClientRequest element
    writer.WriteStartElement(ItemXnam); 
    if (writer.WRACE.EchoFormat)
    {
      // Request URL
      if (!(string.IsNullOrEmpty(writer.WRACE.NpdsReqstEncodedUrl)))
      {
        writer.WriteElementString(RequestUrlXnam, writer.WRACE.NpdsReqstEncodedUrl);
      }
      else if (writer.WRACE.VerboseFormat)
      {
        writer.WriteStartElement(RequestUrlXnam);
        writer.WriteEndElement();
      }
      // Request Note
      if (!(string.IsNullOrEmpty(writer.WRACE.RequestNote)))
      {
        writer.WriteElementString(RequestNoteXnam, writer.WRACE.RequestNote);
      }
      else if (writer.WRACE.VerboseFormat)
      {
        writer.WriteStartElement(RequestNoteXnam);
        writer.WriteEndElement();
      }
      // Request Question
      if (!(string.IsNullOrEmpty(writer.WRACE.RequestQuestion)))
      {
        writer.WriteElementString(RequestQuestionXnam, writer.WRACE.RequestQuestion);
      }
      else if (writer.WRACE.VerboseFormat)
      {
        writer.WriteStartElement(RequestQuestionXnam);
        writer.WriteEndElement();
      }
    }
    // finish ClientRequest element
    writer.WriteEndElement(); 
  }

  public override void ReadXml(XmlReader xReader)
  {
    var reader = (NpdsXmlWrappingReader)xReader;
    reader.MoveToContent();
    if (reader.IsEmptyElement)
    {
      reader.ReadStartElement();
    }
    reader.ReadToFollowing(RequestXnam);
    if (reader.IsStartElement(RequestXnam))
    {
      reader.Read();
      reader.WRACE.NpdsReqstEncodedUrl = reader.ReadElementString(RequestUrlXnam);
      reader.WRACE.RequestNote = reader.ReadElementString(RequestNoteXnam);
      reader.WRACE.RequestQuestion = reader.ReadElementString(RequestQuestionXnam);
    }

  }

}

