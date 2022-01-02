// NpdsApiRequestItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsClientRequestItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsClientRequestItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsClientRequestItem() : base() { this.Initialize(); }
    public NpdsClientRequestItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    private void Initialize(NpdsConst.ResrepFormat rrf = default(NpdsConst.ResrepFormat))
    {
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.RequestXnam);
    }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      // start ClientRequest element
      writer.WriteStartElement(ItemXnam); 
      if (writer.PRC.EchoFormat)
      {
        // Request URL
        if (!(string.IsNullOrEmpty(writer.PRC.NpdsReqstEncodedUrl)))
        {
          writer.WriteElementString(NpdsConst.RequestUrlXnam, writer.PRC.NpdsReqstEncodedUrl);
        }
        else if (writer.PRC.VerboseFormat)
        {
          writer.WriteStartElement(NpdsConst.RequestUrlXnam);
          writer.WriteEndElement();
        }
        // Request Note
        if (!(string.IsNullOrEmpty(writer.PRC.RequestNote)))
        {
          writer.WriteElementString(NpdsConst.RequestNoteXnam, writer.PRC.RequestNote);
        }
        else if (writer.PRC.VerboseFormat)
        {
          writer.WriteStartElement(NpdsConst.RequestNoteXnam);
          writer.WriteEndElement();
        }
        // Request Question
        if (!(string.IsNullOrEmpty(writer.PRC.RequestQuestion)))
        {
          writer.WriteElementString(NpdsConst.RequestQuestionXnam, writer.PRC.RequestQuestion);
        }
        else if (writer.PRC.VerboseFormat)
        {
          writer.WriteStartElement(NpdsConst.RequestQuestionXnam);
          writer.WriteEndElement();
        }
      }
      // finish ClientRequest element
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
      reader.ReadToFollowing(NpdsConst.RequestXnam);
      if (reader.IsStartElement(NpdsConst.RequestXnam))
      {
        reader.Read();
        reader.PRC.NpdsReqstEncodedUrl = reader.ReadElementString(NpdsConst.RequestUrlXnam);
        reader.PRC.RequestNote = reader.ReadElementString(NpdsConst.RequestNoteXnam);
        reader.PRC.RequestQuestion = reader.ReadElementString(NpdsConst.RequestQuestionXnam);
      }

    }

  }

}