using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

// see assembly System.Xml.ReaderWriter class XmlWriter
// TODO: needs updating for current version of XmlWriter

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public abstract class XmlWrappingWriter : XmlWriter
  {
    XmlWriter baseWriter;

    public XmlWrappingWriter(XmlWriter baseWriter)
    {
      ArgumentChecker.ArgNotNull(baseWriter, "baseWriter");

      this.baseWriter = baseWriter;
    }

    protected XmlWriter BaseWriter
    {
      get { return this.baseWriter; }
      set
      {
        ArgumentChecker.ArgNotNull(value, "value");
        this.baseWriter = value;
      }
    }

    public override void Close() { this.baseWriter.Close(); }

    protected override void Dispose(bool disposing)
    {
      if (WriteState != WriteState.Closed) { Close(); }
      ((IDisposable)this.baseWriter).Dispose();
    }

    public override void Flush() { this.baseWriter.Flush(); }

    public override Task FlushAsync() { return this.baseWriter.FlushAsync(); }

    public override string LookupPrefix(string ns) { return this.baseWriter.LookupPrefix(ns); }

    public override void WriteBase64(byte[] buffer, int index, int count) { this.baseWriter.WriteBase64(buffer, index, count); }

    public override void WriteCData(string text) { this.baseWriter.WriteCData(text); }

    public override void WriteCharEntity(char ch) { this.baseWriter.WriteCharEntity(ch); }

    public override void WriteChars(char[] buffer, int index, int count) { this.baseWriter.WriteChars(buffer, index, count); }

    public override void WriteComment(string text) { this.baseWriter.WriteComment(text); }

    public override void WriteDocType(string name, string pubid, string sysid, string subset) { this.baseWriter.WriteDocType(name, pubid, sysid, subset); }

    public override void WriteEndAttribute() { this.baseWriter.WriteEndAttribute(); }

    public override void WriteEndDocument() { this.baseWriter.WriteEndDocument(); }

    public override void WriteEndElement() { this.baseWriter.WriteEndElement(); }

    public override void WriteEntityRef(string name) { this.baseWriter.WriteEntityRef(name); }

    public override void WriteFullEndElement() { this.baseWriter.WriteFullEndElement(); }

    public override void WriteProcessingInstruction(string name, string text) { this.baseWriter.WriteProcessingInstruction(name, text); }

    public override void WriteRaw(string data) { this.baseWriter.WriteRaw(data); }

    public override void WriteRaw(char[] buffer, int index, int count) { this.baseWriter.WriteRaw(buffer, index, count); }

    public override void WriteStartAttribute(string prefix, string localName, string ns) { this.baseWriter.WriteStartAttribute(prefix, localName, ns); }

    public override void WriteStartDocument() { this.baseWriter.WriteStartDocument(); }

    public override void WriteStartDocument(bool standalone) { this.baseWriter.WriteStartDocument(standalone); }

    public override void WriteStartElement(string prefix, string localName, string ns) { this.baseWriter.WriteStartElement(prefix, localName, ns); }

    public override void WriteString(string text) { this.baseWriter.WriteString(text); }

    public override void WriteSurrogateCharEntity(char lowChar, char highChar) { this.baseWriter.WriteSurrogateCharEntity(lowChar, highChar); }

    public override void WriteValue(bool value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(DateTime value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(decimal value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(double value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(int value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(long value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(object value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(float value) { this.baseWriter.WriteValue(value); }

    public override void WriteValue(string value) { this.baseWriter.WriteValue(value); }

    public override void WriteWhitespace(string ws) { this.baseWriter.WriteWhitespace(ws); }

    public override XmlWriterSettings Settings
    {
      get { return this.baseWriter.Settings; }
    }

    public override WriteState WriteState
    {
      get { return this.baseWriter.WriteState; }
    }

    public override string XmlLang
    {
      get { return this.baseWriter.XmlLang; }
    }

    public override XmlSpace XmlSpace
    {
      get { return this.baseWriter.XmlSpace; }
    }

  }

}
