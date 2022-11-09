// EncodedStringBuilder.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class EncodedStringWriter : StringWriter // : TextWriter
{
  public EncodedStringWriter() : base(new StringBuilder()) { }
  public EncodedStringWriter(Encoding enc) : base(new StringBuilder()) { strEncoding = enc; }
  public EncodedStringWriter(StringBuilder sb, Encoding enc) : base(sb) { strEncoding = enc; }

  private Encoding strEncoding = Encoding.UTF8;
  public override Encoding Encoding
  { get { return strEncoding; } }

}
