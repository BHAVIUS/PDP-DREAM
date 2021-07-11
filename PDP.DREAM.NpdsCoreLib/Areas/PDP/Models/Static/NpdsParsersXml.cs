// NpdsParsersXml.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Xml;
using System.Xml.Linq;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public static partial class NpdsParsers
  {
    // TODO: address differences in character encoding and allowed characters
    //     for any differences between XML and JSON
    public static XElement ParseXml(string value)
    {
      XElement xstr;
      try { xstr = XElement.Parse(value); }
      catch { 
        try {
          var istr = $"<NpdsXmlTag>{value}</NpdsXmlTag>";
          xstr = XElement.Parse(istr); } 
        catch { xstr = XElement.Parse("<NpdsXmlTag> WARNING: valid XML format required </NpdsXmlTag>"); } 
      }
      return xstr;
    }

    public static string ParsePrincipalTag(string value)
    {
      string ptag = XmlConvert.EncodeNmToken(value);
      return ptag;
    }

    public static string ParseSupportingTag(string value)
    {
      string xstr;
      try { xstr = XmlConvert.VerifyXmlChars(value); }
      catch { xstr = XmlConvert.VerifyXmlChars("WARNING: valid XML characters required"); }
      return xstr;
    }

    public static XElement ParseOtherText(string value)
    {
      return ParseXml(value);
    }

    public static XElement ParseLocation(string value)
    {
      return ParseXml(value);
    }
    public static XElement ParseDescription(string value)
    {
      return ParseXml(value);
    }
    public static XElement ParseDistribution(string value)
    {
      return ParseXml(value);
    }
    public static XElement ParseProvenance(string value)
    {
      return ParseXml(value);
    }

  }

}