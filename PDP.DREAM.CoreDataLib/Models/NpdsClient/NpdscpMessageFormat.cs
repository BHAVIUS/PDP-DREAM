// PrcMessageFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqMessageFormat = string.Empty;
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

  // validated values

  private PdpAppConst.NpdsMessageFormat messageFormat = default;
  public PdpAppConst.NpdsMessageFormat MessageFormat
  {
    set { messageFormat = ValidateMessageFormat(value); }
    get
    {
      if (messageFormat == 0)
      { MessageFormat = NPDSSD.MessageFormatDefault; }
      return messageFormat;
    }
  }

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

