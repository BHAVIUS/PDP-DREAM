// PdpPrcXmlWrappingReader.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.IO;
using System.Xml;

using PDP.DREAM.NpdsCoreLib.Utilities;

namespace PDP.DREAM.NpdsCoreLib.Models
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
