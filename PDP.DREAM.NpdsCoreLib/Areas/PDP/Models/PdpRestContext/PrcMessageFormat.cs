// PrcMessageFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum MessageFormat { None = 0, JSON = 1, XML = 2, XHTML = 3 } // other formats?

    // default values

    public NpdsConst.MessageFormat MessageFormatDeflt
    {
      get
      {
        if (defMessageFormat == 0)
        { defMessageFormat = NpdsServiceDefaults.GetValues.NpdsDefaultMessageFormat; }
        return defMessageFormat;
      }
    }
    private NpdsConst.MessageFormat defMessageFormat = default;

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

    public NpdsConst.MessageFormat MessageFormat
    {
      set { messageFormat = ValidateMessageFormat(value); }
      get
      {
        if (messageFormat == 0)
        { MessageFormat = MessageFormatDeflt; }
        return messageFormat;
      }
    }
    private NpdsConst.MessageFormat messageFormat = default;

    // validators

    private NpdsConst.MessageFormat ValidateMessageFormat(string strValue)
    {
      NpdsConst.MessageFormat enmValue = PdpEnum<NpdsConst.MessageFormat>.Parse(strValue, NpdsConst.MessageFormat.XML);
      return ValidateMessageFormat(enmValue);
    }
    private NpdsConst.MessageFormat ValidateMessageFormat(NpdsConst.MessageFormat value)
    {
      if (value == NpdsConst.MessageFormat.None) // reset to XML
      { value = NpdsConst.MessageFormat.XML; }
      return value;
    }

  }

}
