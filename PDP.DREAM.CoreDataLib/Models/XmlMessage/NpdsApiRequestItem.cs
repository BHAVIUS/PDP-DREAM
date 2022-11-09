// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsClientRequestItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsClientRequestItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsClientRequestItem() : base() { this.Initialize(); }
  public NpdsClientRequestItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(NpdsResrepFormat rrf = default(NpdsResrepFormat))
  {
    base.InitNpdsItem(NpdsFieldRule.Required, RequestXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    // start ClientRequest element
    writer.WriteStartElement(ItemXnam); 
    if (writer.QURC.EchoFormat)
    {
      // Request URL
      if (!(string.IsNullOrEmpty(writer.QURC.NpdsReqstEncodedUrl)))
      {
        writer.WriteElementString(RequestUrlXnam, writer.QURC.NpdsReqstEncodedUrl);
      }
      else if (writer.QURC.VerboseFormat)
      {
        writer.WriteStartElement(RequestUrlXnam);
        writer.WriteEndElement();
      }
      // Request Note
      if (!(string.IsNullOrEmpty(writer.QURC.RequestNote)))
      {
        writer.WriteElementString(RequestNoteXnam, writer.QURC.RequestNote);
      }
      else if (writer.QURC.VerboseFormat)
      {
        writer.WriteStartElement(RequestNoteXnam);
        writer.WriteEndElement();
      }
      // Request Question
      if (!(string.IsNullOrEmpty(writer.QURC.RequestQuestion)))
      {
        writer.WriteElementString(RequestQuestionXnam, writer.QURC.RequestQuestion);
      }
      else if (writer.QURC.VerboseFormat)
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
      reader.QURC.NpdsReqstEncodedUrl = reader.ReadElementString(RequestUrlXnam);
      reader.QURC.RequestNote = reader.ReadElementString(RequestNoteXnam);
      reader.QURC.RequestQuestion = reader.ReadElementString(RequestQuestionXnam);
    }

  }

}

