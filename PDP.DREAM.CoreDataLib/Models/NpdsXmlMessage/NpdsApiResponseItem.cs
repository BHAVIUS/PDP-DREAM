// NpdsApiResponseItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsServerResponseItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsServerResponseItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsServerResponseItem() : base() { this.Initialize(); }
    public NpdsServerResponseItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = NpdsConst.ResrepFormat.Nexus)
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.ResponseXnam);
    }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      // start ServerResponse element
      writer.WriteStartElement(ItemXnam);
      // Response status
      if (!(string.IsNullOrEmpty(writer.PRC.ResponseStatus)))
      {
        writer.WriteElementString(NpdsConst.ResponseStatusXnam, writer.PRC.ResponseStatus);
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteStartElement(NpdsConst.ResponseStatusXnam);
        writer.WriteEndElement();
      }
      // Response Note
      if (!(string.IsNullOrEmpty(writer.PRC.ResponseNote)))
      {
        writer.WriteElementString(NpdsConst.ResponseNoteXnam, writer.PRC.ResponseNote);
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteStartElement(NpdsConst.ResponseNoteXnam);
        writer.WriteEndElement();
      }
      // Response Answer
      if (writer.PRC.ResponseAnswer != null)
      {
        writer.WriteStartElement(NpdsConst.ResponseAnswerXnam);
        writer.PRC.ResponseAnswer.WriteXml(writer);
        writer.WriteEndElement();
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteStartElement(NpdsConst.ResponseAnswerXnam);
        writer.WriteEndElement();
      }
      // Response Related
      if (writer.PRC.ResponseRelated != null)
      {
        writer.WriteStartElement(NpdsConst.ResponseRelatedXnam);
        writer.PRC.ResponseRelated.WriteXml(writer);
        writer.WriteEndElement();
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteStartElement(NpdsConst.ResponseRelatedXnam);
        writer.WriteEndElement();
      }
      // Response Referred
      if (writer.PRC.ResponseReferred != null)
      {
        writer.WriteStartElement(NpdsConst.ResponseReferredXnam);
        writer.PRC.ResponseReferred.WriteXml(writer);
        writer.WriteEndElement();
      }
      else if (writer.PRC.VerboseFormat)
      {
        writer.WriteStartElement(NpdsConst.ResponseReferredXnam);
        writer.WriteEndElement();
      }
      // finish ServerResponse element
      writer.WriteEndElement();  
    }

    public override void ReadXml(XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      reader.MoveToContent();
      if (reader.IsEmptyElement)
      {
        reader.ReadStartElement();
      }
      reader.ReadToFollowing(NpdsConst.ResponseXnam);
      if (reader.IsStartElement(NpdsConst.ResponseXnam))
      {
        reader.Read();
        reader.PRC.ResponseStatus = reader.ReadElementString(NpdsConst.ResponseStatusXnam);
        reader.PRC.ResponseAnswer.ReadXml(reader);
        reader.PRC.ResponseRelated.ReadXml(reader);
        reader.PRC.ResponseReferred.ReadXml(reader);
      }

    }

  }

}