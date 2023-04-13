// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class NpdsXmlWrappingReader : XmlReader, IXmlLineInfo
{
  public NpdsXmlWrappingReader(XmlReader reader) { baseXmlReader = reader; }
  public NpdsXmlWrappingReader(QebiUserRestContext context, XmlReader reader)
  { QURC = context; baseXmlReader = reader; }
  public NpdsXmlWrappingReader(QebiUserRestContext context, TextReader reader)
  { QURC = context; baseXmlReader = Create(reader); }
  public NpdsXmlWrappingReader(QebiUserRestContext context, Stream inputStream)
  { QURC = context; baseXmlReader = Create(inputStream); }
  public NpdsXmlWrappingReader(QebiUserRestContext context, XmlReader reader, XmlReaderSettings settings)
  { QURC = context; baseXmlReader = Create(reader, settings); }
  public NpdsXmlWrappingReader(QebiUserRestContext context, TextReader reader, XmlReaderSettings settings)
  { QURC = context; baseXmlReader = Create(reader, settings); }
  public NpdsXmlWrappingReader(QebiUserRestContext context, Stream inputStream, XmlReaderSettings settings)
  { QURC = context; baseXmlReader = Create(inputStream, settings); }
  public NpdsXmlWrappingReader(QebiUserRestContext context, string inputUri, XmlReaderSettings settings)
  { QURC = context; baseXmlReader = Create(inputUri, settings); }

  public QebiUserRestContext QURC { get; set; }

  private XmlReader baseXmlReader;
  protected XmlReader BaseXmlReader
  {
    get {
      baseXmlReader.CatchNullObject(nameof(baseXmlReader), nameof(BaseXmlReader), nameof(NpdsXmlWrappingReader));
      return baseXmlReader;
    }
    set {
      value.CatchNullObject(nameof(baseXmlReader), nameof(BaseXmlReader), nameof(NpdsXmlWrappingReader));
      baseXmlReader = value;
    }
  }

  public override bool CanReadBinaryContent { get { return BaseXmlReader.CanReadBinaryContent; } }
  public override bool CanReadValueChunk { get { return BaseXmlReader.CanReadValueChunk; } }
  public override bool CanResolveEntity { get { return BaseXmlReader.CanResolveEntity; } }

  public override void Close() { baseXmlReader.Close(); }
  protected override void Dispose(bool disposing)
  {
    if (this.ReadState != ReadState.Closed) { this.Close(); }
    ((IDisposable)baseXmlReader).Dispose();
  }

  public override bool Read() { return BaseXmlReader.Read(); }
  public override string GetAttribute(int i) { return BaseXmlReader.GetAttribute(i); }
  public override string GetAttribute(string name) { return BaseXmlReader.GetAttribute(name); }
  public override string GetAttribute(string localName, string namespaceURI) { { return BaseXmlReader.GetAttribute(localName, namespaceURI); } }
  public override string LookupNamespace(string prefix) { return BaseXmlReader.LookupNamespace(prefix); }
  public override void MoveToAttribute(int i) { BaseXmlReader.MoveToAttribute(i); }
  public override bool MoveToAttribute(string name) { return BaseXmlReader.MoveToAttribute(name); }
  public override bool MoveToAttribute(string localName, string namespaceURI) { return BaseXmlReader.MoveToAttribute(localName, namespaceURI); }
  public override bool MoveToElement() { return BaseXmlReader.MoveToElement(); }
  public override bool MoveToFirstAttribute() { return BaseXmlReader.MoveToFirstAttribute(); }
  public override bool MoveToNextAttribute() { return BaseXmlReader.MoveToNextAttribute(); }
  public override bool ReadAttributeValue() { return BaseXmlReader.ReadAttributeValue(); }
  public override void ResolveEntity() { BaseXmlReader.ResolveEntity(); }
  public override int AttributeCount { get { return BaseXmlReader.AttributeCount; } }
  public override string BaseURI { get { return BaseXmlReader.BaseURI; } }
  public override int Depth { get { return BaseXmlReader.Depth; } }
  public override bool EOF { get { return BaseXmlReader.EOF; } }
  public override bool HasValue { get { return BaseXmlReader.HasValue; } }
  public override bool IsDefault { get { return BaseXmlReader.IsDefault; } }
  public override bool IsEmptyElement { get { return BaseXmlReader.IsEmptyElement; } }
  public override string this[int i] { get { return BaseXmlReader[i]; } }
  public override string this[string name] { get { return BaseXmlReader[name]; } }
  public override string this[string name, string namespaceURI] { get { return BaseXmlReader[name, namespaceURI]; } }
  public override string LocalName { get { return BaseXmlReader.LocalName; } }
  public override string Name { get { return BaseXmlReader.Name; } }
  public override string NamespaceURI { get { return BaseXmlReader.NamespaceURI; } }
  public override XmlNameTable NameTable { get { return BaseXmlReader.NameTable; } }
  public override XmlNodeType NodeType { get { return BaseXmlReader.NodeType; } }
  public override string Prefix { get { return BaseXmlReader.Prefix; } }
  public override char QuoteChar { get { return BaseXmlReader.QuoteChar; } }
  public override ReadState ReadState { get { return BaseXmlReader.ReadState; } }
  public override string Value { get { return BaseXmlReader.Value; } }
  public override string XmlLang { get { return BaseXmlReader.XmlLang; } }
  public override XmlSpace XmlSpace { get { return BaseXmlReader.XmlSpace; } }
  public override int ReadValueChunk(char[] buffer, int index, int count) { return BaseXmlReader.ReadValueChunk(buffer, index, count); }

  public bool HasLineInfo()
  {
    IXmlLineInfo info = BaseXmlReader as IXmlLineInfo;
    if (info != null)
    {
      return info.HasLineInfo();
    }

    return false;
  }
  public int LineNumber
  {
    get {
      IXmlLineInfo info = BaseXmlReader as IXmlLineInfo;
      if (info != null)
      {
        return info.LineNumber;
      }

      return 0;
    }
  }
  public int LinePosition
  {
    get {
      IXmlLineInfo info = BaseXmlReader as IXmlLineInfo;
      if (info != null)
      {
        return info.LinePosition;
      }

      return 0;
    }
  }

} // end class

// end file