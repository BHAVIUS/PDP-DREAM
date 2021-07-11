// EncodedStringBuilder.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.IO;
using System.Text;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  // CT modified code
  // original from Robert McClaws FunWithCoding.net
  // http://weblogs.asp.net/rmclaws/archive/2003/10/19/32534.aspx
  ///<summary>Implements a TextWriter for writing information to a string. The information is stored in an underlying StringBuilder.</summary>
  public class EncodedStringBuilder : StringWriter
  {
    public EncodedStringBuilder() : base(new StringBuilder()) { txtEncoding = Encoding.UTF8; }
    public EncodedStringBuilder(Encoding enc) : base(new StringBuilder()) { txtEncoding = enc; }
    public EncodedStringBuilder(StringBuilder sb, Encoding enc) : base(sb) { txtEncoding = enc; }

    public override Encoding Encoding
    { get { return txtEncoding; } }
    private Encoding txtEncoding;
  }

}
