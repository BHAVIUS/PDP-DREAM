// PrcMessageFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // in file NpdsConstants.cs
  // public enum MessageFormat { None = 0, JSON = 1, XML = 2, XHTML = 3 } // other formats?

  // default values

  public PdpAppConst.NpdsMessageFormat MessageFormatDeflt
  {
    get
    {
      if (defMessageFormat == 0)
      { defMessageFormat = PdpAppStatus.NPDSSD.NpdsDefaultMessageFormat; }
      return defMessageFormat;
    }
  }
  private PdpAppConst.NpdsMessageFormat defMessageFormat = default;

  // requested values

  public string MessageFormatReqst
  {
    set
    {
      reqMessageFormat = value;
      if (!string.IsNullOrEmpty(reqMessageFormat))
      {
        MessageFormat = ValidateMessageFormat(reqMessageFormat);
      }
    }
    get { return reqMessageFormat; }
  }
  private string reqMessageFormat = string.Empty;

  // validated values

  public PdpAppConst.NpdsMessageFormat MessageFormat
  {
    set { messageFormat = ValidateMessageFormat(value); }
    get
    {
      if (messageFormat == 0)
      { MessageFormat = MessageFormatDeflt; }
      return messageFormat;
    }
  }
  private PdpAppConst.NpdsMessageFormat messageFormat = default;

  // validators

  private PdpAppConst.NpdsMessageFormat ValidateMessageFormat(string strValue)
  {
    PdpAppConst.NpdsMessageFormat enmValue = PdpEnum<PdpAppConst.NpdsMessageFormat>.ParseString(strValue, PdpAppConst.NpdsMessageFormat.XML);
    return ValidateMessageFormat(enmValue);
  }
  private PdpAppConst.NpdsMessageFormat ValidateMessageFormat(PdpAppConst.NpdsMessageFormat value)
  {
    if (value == PdpAppConst.NpdsMessageFormat.None) // reset to XML
    { value = PdpAppConst.NpdsMessageFormat.XML; }
    return value;
  }

}

