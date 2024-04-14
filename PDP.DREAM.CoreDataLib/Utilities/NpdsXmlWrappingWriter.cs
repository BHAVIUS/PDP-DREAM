// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlWrappingWriter : XmlWriter
{
  public NpdsXmlWrappingWriter(XmlWriter writer) { baseXmlWriter = writer; }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, XmlWriter writer)
  { WRACE = context; baseXmlWriter = writer; }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, TextWriter writer)
  { WRACE = context; baseXmlWriter = Create(writer); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, Stream outputStream)
  { WRACE = context; baseXmlWriter = Create(outputStream); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, StringBuilder outputString)
  { WRACE = context; baseXmlWriter = Create(outputString); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, string outputFileName)
  { WRACE = context; baseXmlWriter = Create(outputFileName); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, XmlWriter writer, XmlWriterSettings settings)
  { WRACE = context; baseXmlWriter = Create(writer, settings); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, TextWriter writer, XmlWriterSettings settings)
  { WRACE = context; baseXmlWriter = Create(writer); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, Stream outputStream, XmlWriterSettings settings)
  { WRACE = context; baseXmlWriter = Create(outputStream, settings); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, StringBuilder outputString, XmlWriterSettings settings)
  { WRACE = context; baseXmlWriter = Create(outputString, settings); }
  public NpdsXmlWrappingWriter(NpdsClientWrace context, string outputFileName, XmlWriterSettings settings)
  { WRACE = context; baseXmlWriter = Create(outputFileName, settings); }

  public NpdsClientWrace WRACE { get; set; }

  private XmlWriter baseXmlWriter;
  protected XmlWriter BaseXmlWriter
  {
    get {
      baseXmlWriter.CatchNullObject(nameof(baseXmlWriter), nameof(BaseXmlWriter), nameof(NpdsXmlWrappingWriter));
      return baseXmlWriter;
    }
    set {
      value.CatchNullObject(nameof(baseXmlWriter), nameof(BaseXmlWriter), nameof(NpdsXmlWrappingWriter));
      baseXmlWriter = value;
    }
  }

  public override void Close() { baseXmlWriter.Close(); }
  protected override void Dispose(bool disposing)
  {
    if (this.WriteState != WriteState.Closed) { this.Close(); }
    ((IDisposable)baseXmlWriter).Dispose();
  }

  public override void Flush() { BaseXmlWriter.Flush(); }
  public override Task FlushAsync() { return BaseXmlWriter.FlushAsync(); }
  public override string LookupPrefix(string ns) { return BaseXmlWriter.LookupPrefix(ns); }
  public override void WriteBase64(byte[] buffer, int index, int count) { BaseXmlWriter.WriteBase64(buffer, index, count); }
  public override void WriteCData(string text) { BaseXmlWriter.WriteCData(text); }
  public override void WriteCharEntity(char ch) { BaseXmlWriter.WriteCharEntity(ch); }
  public override void WriteChars(char[] buffer, int index, int count) { BaseXmlWriter.WriteChars(buffer, index, count); }
  public override void WriteComment(string text) { BaseXmlWriter.WriteComment(text); }
  public override void WriteDocType(string name, string pubid, string sysid, string subset) { BaseXmlWriter.WriteDocType(name, pubid, sysid, subset); }
  public override void WriteEndAttribute() { BaseXmlWriter.WriteEndAttribute(); }
  public override void WriteEndDocument() { BaseXmlWriter.WriteEndDocument(); }
  public override void WriteEndElement() { BaseXmlWriter.WriteEndElement(); }
  public override void WriteEntityRef(string name) { BaseXmlWriter.WriteEntityRef(name); }
  public override void WriteFullEndElement() { BaseXmlWriter.WriteFullEndElement(); }
  public override void WriteProcessingInstruction(string name, string text) { BaseXmlWriter.WriteProcessingInstruction(name, text); }
  public override void WriteRaw(string data) { BaseXmlWriter.WriteRaw(data); }
  public override void WriteRaw(char[] buffer, int index, int count) { BaseXmlWriter.WriteRaw(buffer, index, count); }
  public override void WriteStartAttribute(string prefix, string localName, string ns) { BaseXmlWriter.WriteStartAttribute(prefix, localName, ns); }
  public override void WriteStartDocument() { BaseXmlWriter.WriteStartDocument(); }
  public override void WriteStartDocument(bool standalone) { BaseXmlWriter.WriteStartDocument(standalone); }
  public override void WriteStartElement(string prefix, string localName, string ns) { BaseXmlWriter.WriteStartElement(prefix, localName, ns); }
  public override void WriteString(string text) { BaseXmlWriter.WriteString(text); }
  public override void WriteSurrogateCharEntity(char lowChar, char highChar) { BaseXmlWriter.WriteSurrogateCharEntity(lowChar, highChar); }
  public override void WriteValue(bool value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(DateTime value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(decimal value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(double value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(int value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(long value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(object value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(float value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteValue(string value) { BaseXmlWriter.WriteValue(value); }
  public override void WriteWhitespace(string ws) { BaseXmlWriter.WriteWhitespace(ws); }

  public override XmlWriterSettings Settings
  {
    get { return BaseXmlWriter.Settings; }
  }

  public override WriteState WriteState
  {
    get { return BaseXmlWriter.WriteState; }
  }

  public override string XmlLang
  {
    get { return BaseXmlWriter.XmlLang; }
  }

  public override XmlSpace XmlSpace
  {
    get { return BaseXmlWriter.XmlSpace; }
  }

} // end class

// end file