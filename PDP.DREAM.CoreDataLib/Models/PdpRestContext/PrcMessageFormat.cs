// PrcMessageFormat.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
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
        { defMessageFormat = NpdsServiceDefaults.Values.NpdsDefaultMessageFormat; }
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
