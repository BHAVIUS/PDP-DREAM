// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsServerResponseItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsServerResponseItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsServerResponseItem() : base() { this.Initialize(NPDSCD.ResrepFormatDefault); }
  public NpdsServerResponseItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(NpdsResrepFormat rrf)
  {
    base.InitNpdsItem(NpdsFieldRule.Required, ResponseXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    // start ServerResponse element
    writer.WriteStartElement(ItemXnam);
    // Response status
    if (!(string.IsNullOrEmpty(writer.WRACE.ResponseStatus)))
    {
      writer.WriteElementString(ResponseStatusXnam, writer.WRACE.ResponseStatus);
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteStartElement(ResponseStatusXnam);
      writer.WriteEndElement();
    }
    // Response Note
    if (!(string.IsNullOrEmpty(writer.WRACE.ResponseNote)))
    {
      writer.WriteElementString(ResponseNoteXnam, writer.WRACE.ResponseNote);
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteStartElement(ResponseNoteXnam);
      writer.WriteEndElement();
    }
    // Response Answer
    if (writer.WRACE.ResponseAnswer != null)
    {
      writer.WriteStartElement(ResponseAnswerXnam);
      writer.WRACE.ResponseAnswer.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteStartElement(ResponseAnswerXnam);
      writer.WriteEndElement();
    }
    // Response Related
    if (writer.WRACE.ResponseRelated != null)
    {
      writer.WriteStartElement(ResponseRelatedXnam);
      writer.WRACE.ResponseRelated.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteStartElement(ResponseRelatedXnam);
      writer.WriteEndElement();
    }
    // Response Referred
    if (writer.WRACE.ResponseReferred != null)
    {
      writer.WriteStartElement(ResponseReferredXnam);
      writer.WRACE.ResponseReferred.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteStartElement(ResponseReferredXnam);
      writer.WriteEndElement();
    }
    // finish ServerResponse element
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
    reader.ReadToFollowing(ResponseXnam);
    if (reader.IsStartElement(ResponseXnam))
    {
      reader.Read();
      reader.WRACE.ResponseStatus = reader.ReadElementString(ResponseStatusXnam);
      reader.WRACE.ResponseAnswer.ReadXml(reader);
      reader.WRACE.ResponseRelated.ReadXml(reader);
      reader.WRACE.ResponseReferred.ReadXml(reader);
    }

  }

}

