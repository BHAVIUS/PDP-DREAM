// PdpPrcXmlWrappingWriter.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.IO;
using System.Text;
using System.Xml;

using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class PdpPrcXmlWrappingWriter : XmlWrappingWriter
  {
    public PdpPrcXmlWrappingWriter(PdpRestContext context, XmlWriter writer)
      : base(writer)
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, TextWriter writer)
      : base(XmlWriter.Create(writer))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, Stream outputStream)
      : base(XmlWriter.Create(outputStream))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, StringBuilder outputString)
      : base(XmlWriter.Create(outputString))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, string outputFileName)
      : base(XmlWriter.Create(outputFileName))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, XmlWriter writer, XmlWriterSettings settings)
      : base(XmlWriter.Create(writer, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, TextWriter writer, XmlWriterSettings settings)
      : base(XmlWriter.Create(writer, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, Stream outputStream, XmlWriterSettings settings)
      : base(XmlWriter.Create(outputStream, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, StringBuilder outputString, XmlWriterSettings settings)
      : base(XmlWriter.Create(outputString, settings))
    { PRC = context; }
    public PdpPrcXmlWrappingWriter(PdpRestContext context, string outputFileName, XmlWriterSettings settings)
      : base(XmlWriter.Create(outputFileName, settings))
    { PRC = context; }

    public PdpRestContext PRC { get; set; }

  }

}
