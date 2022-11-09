// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsServerResponseItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsServerResponseItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsServerResponseItem() : base() { this.Initialize(); }
  public NpdsServerResponseItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = PdpAppConst.NpdsResrepFormat.Nexus)
  {
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.ResponseXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    // start ServerResponse element
    writer.WriteStartElement(ItemXnam);
    // Response status
    if (!(string.IsNullOrEmpty(writer.QURC.ResponseStatus)))
    {
      writer.WriteElementString(PdpAppConst.ResponseStatusXnam, writer.QURC.ResponseStatus);
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteStartElement(PdpAppConst.ResponseStatusXnam);
      writer.WriteEndElement();
    }
    // Response Note
    if (!(string.IsNullOrEmpty(writer.QURC.ResponseNote)))
    {
      writer.WriteElementString(PdpAppConst.ResponseNoteXnam, writer.QURC.ResponseNote);
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteStartElement(PdpAppConst.ResponseNoteXnam);
      writer.WriteEndElement();
    }
    // Response Answer
    if (writer.QURC.ResponseAnswer != null)
    {
      writer.WriteStartElement(PdpAppConst.ResponseAnswerXnam);
      writer.QURC.ResponseAnswer.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteStartElement(PdpAppConst.ResponseAnswerXnam);
      writer.WriteEndElement();
    }
    // Response Related
    if (writer.QURC.ResponseRelated != null)
    {
      writer.WriteStartElement(PdpAppConst.ResponseRelatedXnam);
      writer.QURC.ResponseRelated.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteStartElement(PdpAppConst.ResponseRelatedXnam);
      writer.WriteEndElement();
    }
    // Response Referred
    if (writer.QURC.ResponseReferred != null)
    {
      writer.WriteStartElement(PdpAppConst.ResponseReferredXnam);
      writer.QURC.ResponseReferred.WriteXml(writer);
      writer.WriteEndElement();
    }
    else if (writer.QURC.VerboseFormat)
    {
      writer.WriteStartElement(PdpAppConst.ResponseReferredXnam);
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
    reader.ReadToFollowing(PdpAppConst.ResponseXnam);
    if (reader.IsStartElement(PdpAppConst.ResponseXnam))
    {
      reader.Read();
      reader.QURC.ResponseStatus = reader.ReadElementString(PdpAppConst.ResponseStatusXnam);
      reader.QURC.ResponseAnswer.ReadXml(reader);
      reader.QURC.ResponseRelated.ReadXml(reader);
      reader.QURC.ResponseReferred.ReadXml(reader);
    }

  }

}

