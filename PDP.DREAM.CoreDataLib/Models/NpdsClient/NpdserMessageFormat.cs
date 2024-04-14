// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqMessageFormat = ESS;
  public string? MessageFormatReqst
  {
    set {
      reqMessageFormat = value;
      if (!string.IsNullOrEmpty(reqMessageFormat))
      { messageFormat = ValidateMessageFormat(reqMessageFormat); }
    }
    get { return reqMessageFormat; }
  }

  // validated value (non-nullable typed)

  private NpdsMessageFormat messageFormat = NPDSCD.MessageFormatDefault;
  public NpdsMessageFormat MessageFormat
  {
    set { messageFormat = ValidateMessageFormat(value); }
    get {
      if (messageFormat == null)
      { messageFormat = NPDSCD.MessageFormatDefault; }
      return messageFormat;
    }
  }

  // validators

  private NpdsMessageFormat ValidateMessageFormat(string recName)
  {
    NpdsMessageFormat recItem = NPDSCD.ParseMessageFormat(recName);
    return recItem;
  }
  private NpdsMessageFormat ValidateMessageFormat(NpdsMessageFormat value)
  {
    // TODO: code AI rules
    if (string.Equals(value.EName, PdpAppConst.DdeMessageFormat.None, StringComparison.OrdinalIgnoreCase))
    // reset to XML
    { value = NPDSCD.MessageFormatDefault; } // current default
    return value;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{

  // ATTN: distinguish http message format and NPDS message representation
  // TODO: address range of HttpResponseFormats per 
  // http://www.iana.org/assignments/media-types/media-types.xhtml
  //
  // MessageFormat is the Message Format returned
  // from RESTful APIs as the http response to http request
  public static class DdeMessageFormat
  {
    public const string None = "None"; // 0 
    public const string JSON = "JSON"; // 1
    public const string XML = "XML"; // 2
    public const string XHTML = "XHTML"; // 3

  } // end class

} // end class

public partial class NpdsClientDefaults
{
  public NpdsMessageFormat MessageFormatNone =
    new NpdsMessageFormat(0, DdeMessageFormat.None, DdeMessageFormat.None);
  public NpdsMessageFormat MessageFormatJSON =
    new NpdsMessageFormat(1, DdeMessageFormat.JSON, DdeMessageFormat.JSON);
  public NpdsMessageFormat MessageFormatXML =
    new NpdsMessageFormat(2, DdeMessageFormat.XML, DdeMessageFormat.XML);
  public NpdsMessageFormat MessageFormatXHTML =
    new NpdsMessageFormat(3, DdeMessageFormat.XHTML, DdeMessageFormat.XHTML);

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

  protected void InitMessageFormatEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsMessageFormat>
    {
      MessageFormatNone,

      MessageFormatJSON, MessageFormatXML, MessageFormatXHTML,
    };
    MessageFormatList = recItms;
    MessageFormatNewItem = recItms[0];
    MessageFormatDefault = recItms[2];
  }

  //public void ResetMessageFormatDefault(string enam)
  //{
  //  try
  //  {
  //    MessageFormatDefault = MessageFormatList?
  //       .Where(itm => itm.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitMessageFormats();
  //  }
  //}

  // nullable lists
  public List<NpdsMessageFormat>? MessageFormatList { get; set; } = null;
  public List<string>? MessageFormatNames
  {
    get {
      if (MessageFormatList == null) { InitMessageFormatEnums(); }
      return MessageFormatList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsMessageFormat ParseMessageFormat(byte ecod)
  {
    NpdsMessageFormat? recItm = null;
    if (MessageFormatList == null) { InitMessageFormatEnums(); }
    try
    {
      recItm = MessageFormatList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = MessageFormatDefault; }
    return recItm;
  }
  public NpdsMessageFormat ParseMessageFormat(string enam)
  {
    NpdsMessageFormat? recItm = null;
    if (MessageFormatList == null) { InitMessageFormatEnums(); }
    try
    {
      recItm = MessageFormatList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = MessageFormatDefault; }
    return recItm;
  }
  public NpdsMessageFormat MessageFormatDefault { get; set; }
  public NpdsMessageFormat MessageFormatNewItem { get; set; }

} // end class

// end file