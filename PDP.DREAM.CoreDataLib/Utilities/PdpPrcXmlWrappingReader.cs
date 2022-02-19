// PdpPrcXmlWrappingReader.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.IO;
using System.Xml;

using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class PdpPrcXmlWrappingReader : XmlWrappingReader
  {
    public PdpPrcXmlWrappingReader(PdpRestContext context, XmlReader reader)
      : base(reader)
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, TextReader reader)
      : base(XmlReader.Create(reader))
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, Stream inputStream)
      : base(XmlReader.Create(inputStream))
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, XmlReader reader, XmlReaderSettings settings)
      : base(XmlReader.Create(reader, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, TextReader reader, XmlReaderSettings settings)
      : base(XmlReader.Create(reader, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, Stream inputStream, XmlReaderSettings settings)
      : base(XmlReader.Create(inputStream, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingReader(PdpRestContext context, string inputUri, XmlReaderSettings settings)
      : base(XmlReader.Create(inputUri, settings))
    { PRC = context; }

    public PdpRestContext PRC { get; set; }

  }

}
