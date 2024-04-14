// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqInfosetStatus = ESS;
  public string? InfosetStatusReqst
  {
    set {
      reqInfosetStatus = value;
      if (!string.IsNullOrEmpty(reqInfosetStatus))
      { infosetStatus = ValidateInfosetStatus(reqInfosetStatus); }
    }
    get { return reqInfosetStatus; }
  }

  // validated value (non-nullable typed)

  private NpdsInfosetStatus infosetStatus = NPDSCD.InfosetStatusDefault;
  public NpdsInfosetStatus InfosetStatus
  {
    set { infosetStatus = ValidateInfosetStatus(value); }
    get {
      if (infosetStatus == null)
      { infosetStatus = NPDSCD.InfosetStatusDefault; }
      return infosetStatus;
    }
  }

  // validators

  private NpdsInfosetStatus ValidateInfosetStatus(string recName)
  {
    NpdsInfosetStatus recItem = NPDSCD.ParseInfosetStatus(recName);
    return recItem;
  }
  private NpdsInfosetStatus ValidateInfosetStatus(NpdsInfosetStatus recItem)
  {
    // TODO: code AI rules
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  public static class DdeInfosetStatus
  {
    public const string None = "None"; // 0
    public const string Unknown = "Unknown "; // 99
    public const string AnyAndAll = "AnyAndAll"; // 100

    // generic terms (odds true, evens false)

    public const string Valid = "Valid"; // 1, 
    public const string Invalid = "Invalid"; // 2, 
    public const string SyntaxValid = "SyntaxValid"; // 3, 
    public const string SyntaxInvalid = "SyntaxInvalid"; // 4, 
    public const string SemanticsValid = "SemanticsValid"; // 5, 
    public const string SemanticsInvalid = "SemanticsInvalid"; // 6,
    public const string ConceptValid = "ConceptValid "; // 11, 
    public const string ConceptInvalid = "ConceptInvalid "; // 12, 
    public const string AddressValid = "AddressValid "; // 13, 
    public const string AddressInvalid = "AddressInvalid "; // 14,
    // format-specific terms
    public const string JsonInvalid = "JsonInvalid "; // 20, 
    public const string JsonValid = "JsonValid "; // 21, 
    public const string JsonValidLax = "JsonValidLax "; // 22, 
    public const string JsonValidStrict = "JsonValidStrict "; // 23,
    public const string XmlInvalid = "XmlInvalid "; // 30, 
    public const string XmlValid = "XmlValid "; // 31, 
    public const string XmlValidLax = "XmlValidLax "; // 32, 
    public const string XmlValidStrict = "XmlValidStrict "; // 33,
    public const string RdfInvalid = "RdfInvalid "; // 40, 
    public const string RdfValid = "RdfValid "; // 41, 
    public const string RdfValidLax = "RdfValidLax "; // 42, 
    public const string RdfValidStrict = "RdfValidStrict "; // 43,
    public const string OwlInvalid = "OwlInvalid "; // 50, 
    public const string OwlValid = "OwlValid "; // 51, 
    public const string OwlValidLax = "OwlValidLax "; // 52, 
    public const string OwlValidStrict = "OwlValidStrict "; // 53,
    public const string HtmlInvalid = "HtmlInvalid "; // 60, 
    public const string HtmlValid = "HtmlValid "; // 61, 
    public const string HtmlValidLax = "HtmlValidLax "; // 62, 
    public const string HtmlValidStrict = "HtmlValidStrict "; // 63,
    public const string XhtmlInvalid = "XhtmlInvalid "; // 70, 
    public const string XhtmlValid = "XhtmlValid "; // 71, 
    public const string XhtmlValidLax = "XhtmlValidLax "; // 72, 
    public const string XhtmlValidStrict = "XhtmlValidStrict "; // 73,

  } // end class

} // end class

public partial class NpdsClientDefaults
{
  public NpdsInfosetStatus InfosetStatusNone =
     new NpdsInfosetStatus(0, DdeInfosetStatus.None, DdeInfosetStatus.None);
  public NpdsInfosetStatus InfosetStatusValid =
     new NpdsInfosetStatus(1, DdeInfosetStatus.Valid, DdeInfosetStatus.Valid);
  public NpdsInfosetStatus InfosetStatusInvalid =
     new NpdsInfosetStatus(2, DdeInfosetStatus.Invalid, DdeInfosetStatus.Invalid);
  public NpdsInfosetStatus InfosetStatusSyntaxValid =
      new NpdsInfosetStatus(3, DdeInfosetStatus.SyntaxValid, DdeInfosetStatus.SyntaxValid);
  public NpdsInfosetStatus InfosetStatusSyntaxInvalid =
     new NpdsInfosetStatus(4, DdeInfosetStatus.SyntaxInvalid, DdeInfosetStatus.SyntaxInvalid);
  public NpdsInfosetStatus InfosetStatusSemanticsValid =
     new NpdsInfosetStatus(5, DdeInfosetStatus.SemanticsValid, DdeInfosetStatus.SemanticsValid);
  public NpdsInfosetStatus InfosetStatusSemanticsInvalid =
      new NpdsInfosetStatus(6, DdeInfosetStatus.SemanticsInvalid, DdeInfosetStatus.SemanticsInvalid);

  public NpdsInfosetStatus InfosetStatusConceptValid =
     new NpdsInfosetStatus(11, DdeInfosetStatus.ConceptValid, DdeInfosetStatus.ConceptValid);
  public NpdsInfosetStatus InfosetStatusConceptInvalid =
     new NpdsInfosetStatus(12, DdeInfosetStatus.ConceptInvalid, DdeInfosetStatus.ConceptInvalid);
  public NpdsInfosetStatus InfosetStatusAddressValid =
      new NpdsInfosetStatus(13, DdeInfosetStatus.AddressValid, DdeInfosetStatus.AddressValid);
  public NpdsInfosetStatus InfosetStatusAddressInvalid =
     new NpdsInfosetStatus(14, DdeInfosetStatus.AddressInvalid, DdeInfosetStatus.AddressInvalid);

  public NpdsInfosetStatus InfosetStatusJsonInvalid =
     new NpdsInfosetStatus(20, DdeInfosetStatus.JsonInvalid, DdeInfosetStatus.JsonInvalid);
  public NpdsInfosetStatus InfosetStatusJsonValid =
     new NpdsInfosetStatus(21, DdeInfosetStatus.JsonValid, DdeInfosetStatus.JsonValid);
  public NpdsInfosetStatus InfosetStatusJsonValidLax =
     new NpdsInfosetStatus(22, DdeInfosetStatus.JsonValidLax, DdeInfosetStatus.JsonValidLax);
  public NpdsInfosetStatus InfosetStatusJsonValidStrict =
     new NpdsInfosetStatus(23, DdeInfosetStatus.JsonValidStrict, DdeInfosetStatus.JsonValidStrict);

  public NpdsInfosetStatus InfosetStatusXmlInvalid =
   new NpdsInfosetStatus(30, DdeInfosetStatus.XmlInvalid, DdeInfosetStatus.XmlInvalid);
  public NpdsInfosetStatus InfosetStatusXmlValid =
     new NpdsInfosetStatus(31, DdeInfosetStatus.XmlValid, DdeInfosetStatus.XmlValid);
  public NpdsInfosetStatus InfosetStatusXmlValidLax =
     new NpdsInfosetStatus(32, DdeInfosetStatus.XmlValidLax, DdeInfosetStatus.XmlValidLax);
  public NpdsInfosetStatus InfosetStatusXmlValidStrict =
     new NpdsInfosetStatus(33, DdeInfosetStatus.XmlValidStrict, DdeInfosetStatus.XmlValidStrict);

  public NpdsInfosetStatus InfosetStatusRdfInvalid =
   new NpdsInfosetStatus(40, DdeInfosetStatus.RdfInvalid, DdeInfosetStatus.RdfInvalid);
  public NpdsInfosetStatus InfosetStatusRdfValid =
     new NpdsInfosetStatus(41, DdeInfosetStatus.RdfValid, DdeInfosetStatus.RdfValid);
  public NpdsInfosetStatus InfosetStatusRdfValidLax =
     new NpdsInfosetStatus(42, DdeInfosetStatus.RdfValidLax, DdeInfosetStatus.RdfValidLax);
  public NpdsInfosetStatus InfosetStatusRdfValidStrict =
     new NpdsInfosetStatus(43, DdeInfosetStatus.RdfValidStrict, DdeInfosetStatus.RdfValidStrict);

  public NpdsInfosetStatus InfosetStatusOwlInvalid =
   new NpdsInfosetStatus(50, DdeInfosetStatus.OwlInvalid, DdeInfosetStatus.OwlInvalid);
  public NpdsInfosetStatus InfosetStatusOwlValid =
     new NpdsInfosetStatus(51, DdeInfosetStatus.OwlValid, DdeInfosetStatus.OwlValid);
  public NpdsInfosetStatus InfosetStatusOwlValidLax =
     new NpdsInfosetStatus(52, DdeInfosetStatus.OwlValidLax, DdeInfosetStatus.OwlValidLax);
  public NpdsInfosetStatus InfosetStatusOwlValidStrict =
     new NpdsInfosetStatus(53, DdeInfosetStatus.OwlValidStrict, DdeInfosetStatus.OwlValidStrict);

  public NpdsInfosetStatus InfosetStatusHtmlInvalid =
    new NpdsInfosetStatus(60, DdeInfosetStatus.HtmlInvalid, DdeInfosetStatus.HtmlInvalid);
  public NpdsInfosetStatus InfosetStatusHtmlValid =
     new NpdsInfosetStatus(61, DdeInfosetStatus.HtmlValid, DdeInfosetStatus.HtmlValid);
  public NpdsInfosetStatus InfosetStatusHtmlValidLax =
     new NpdsInfosetStatus(62, DdeInfosetStatus.HtmlValidLax, DdeInfosetStatus.HtmlValidLax);
  public NpdsInfosetStatus InfosetStatusHtmlValidStrict =
     new NpdsInfosetStatus(63, DdeInfosetStatus.HtmlValidStrict, DdeInfosetStatus.HtmlValidStrict);

  public NpdsInfosetStatus InfosetStatusXhtmlInvalid =
    new NpdsInfosetStatus(60, DdeInfosetStatus.XhtmlInvalid, DdeInfosetStatus.XhtmlInvalid);
  public NpdsInfosetStatus InfosetStatusXhtmlValid =
     new NpdsInfosetStatus(61, DdeInfosetStatus.XhtmlValid, DdeInfosetStatus.XhtmlValid);
  public NpdsInfosetStatus InfosetStatusXhtmlValidLax =
     new NpdsInfosetStatus(62, DdeInfosetStatus.XhtmlValidLax, DdeInfosetStatus.XhtmlValidLax);
  public NpdsInfosetStatus InfosetStatusXhtmlValidStrict =
     new NpdsInfosetStatus(63, DdeInfosetStatus.XhtmlValidStrict, DdeInfosetStatus.XhtmlValidStrict);

  public NpdsInfosetStatus InfosetStatusUnknown =
     new NpdsInfosetStatus(99, DdeInfosetStatus.Unknown, DdeInfosetStatus.Unknown);
  public NpdsInfosetStatus InfosetStatusAnyAndAll =
     new NpdsInfosetStatus(100, DdeInfosetStatus.AnyAndAll, DdeInfosetStatus.AnyAndAll);

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

  protected void InitInfosetStatusEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsInfosetStatus>
    {
      InfosetStatusNone, InfosetStatusAnyAndAll, InfosetStatusUnknown,

      InfosetStatusInvalid, InfosetStatusValid,
      InfosetStatusSyntaxInvalid, InfosetStatusSyntaxValid,
      InfosetStatusSemanticsInvalid, InfosetStatusSemanticsValid,
      InfosetStatusConceptInvalid, InfosetStatusConceptValid,
      InfosetStatusAddressInvalid, InfosetStatusAddressValid,

      InfosetStatusJsonInvalid, InfosetStatusJsonValid, InfosetStatusJsonValidLax, InfosetStatusJsonValidStrict,
      InfosetStatusXmlInvalid, InfosetStatusXmlValid, InfosetStatusXmlValidLax, InfosetStatusXmlValidStrict,
      InfosetStatusRdfInvalid, InfosetStatusRdfValid, InfosetStatusRdfValidLax, InfosetStatusRdfValidStrict,
      InfosetStatusOwlInvalid, InfosetStatusOwlValid, InfosetStatusOwlValidLax, InfosetStatusOwlValidStrict,
      InfosetStatusHtmlInvalid, InfosetStatusHtmlValid, InfosetStatusHtmlValidLax, InfosetStatusHtmlValidStrict,
      InfosetStatusXhtmlInvalid, InfosetStatusXhtmlValid, InfosetStatusXhtmlValidLax, InfosetStatusXhtmlValidStrict,
    };
    InfosetStatusList = recItms;
    InfosetStatusNewItem = recItms[0];
    InfosetStatusDefault = recItms[1];
  }

  //public void ResetInfosetStatusDefault(string enam)
  //{
  //  try
  //  {
  //    InfosetStatusDefault = InfosetStatusList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitInfosetStatusEnums();
  //  }
  //}

  // nullable lists
  public List<NpdsInfosetStatus>? InfosetStatusList { get; set; } = null;
  public List<string>? InfosetStatusNames
  {
    get {
      if (InfosetStatusList == null) { InitInfosetStatusEnums(); }
      return InfosetStatusList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsInfosetStatus ParseInfosetStatus(byte ecod)
  {
    NpdsInfosetStatus? recItm = null;
    if (InfosetStatusList == null) { InitInfosetStatusEnums(); }
    try
    {
      recItm = InfosetStatusList?
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = InfosetStatusDefault; }
    return recItm;
  }
  public NpdsInfosetStatus ParseInfosetStatus(string enam)
  {
    NpdsInfosetStatus? recItm = null;
    try
    {
      recItm = InfosetStatusList?
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = InfosetStatusDefault; }
    return recItm;
  }
  public NpdsInfosetStatus InfosetStatusDefault { get; set; }
  public NpdsInfosetStatus InfosetStatusNewItem { get; set; }

} // end class

// end file