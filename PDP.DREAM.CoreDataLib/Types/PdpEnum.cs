// PdpEnum.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

// ATTN: recall C# enum does not support derived class
// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
// https://ardalis.com/enum-alternatives-in-c/
// https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/

public static class PdpEnum<TEnum>
  where TEnum : notnull
{
  public static readonly Type GetEnumType = typeof(TEnum);
  public static readonly Type GetBaseType = Enum.GetUnderlyingType(GetEnumType);

  public static IList<TEnum> PdpEnumList;
  public static Dictionary<TEnum, string> PdpEnumDict;

  // byte numValue to typed enum
  //public static TEnum ParseNumeric(byte numValue)
  //{
  //  return ParseNumeric(numValue, default);
  //}
  //public static TEnum ParseNumeric(byte numValue, TEnum enmDefault)
  //{
  //  TEnum enmValue;
  //  TEnum defValue = enmDefault ?? default;
  //  try { enmValue = (TEnum)Enum.ToObject(typeof(TEnum), numValue); }
  //  catch { enmValue = defValue; }
  //  return enmValue;
  //}

  // short numValue to typed enum
  public static TEnum ParseNumeric(short numValue)
  {
    return ParseNumeric(numValue, default);
  }
  public static TEnum ParseNumeric(short numValue, TEnum enmDefault)
  {
    TEnum enmValue;
    TEnum defValue = enmDefault ?? default;
    try { enmValue = (TEnum)Enum.ToObject(typeof(TEnum), numValue); }
    catch { enmValue = defValue; }
    return enmValue;
  }

  // int numValue to typed enum
  public static TEnum ParseNumeric(int numValue)
  {
    return ParseNumeric(numValue, default);
  }
  public static TEnum ParseNumeric(int numValue, TEnum enmDefault)
  {
    TEnum enmValue;
    TEnum defValue = enmDefault ?? default;
    try { enmValue = (TEnum)Enum.ToObject(typeof(TEnum), numValue); }
    catch { enmValue = defValue; }
    return enmValue;
  }

  // string strValue to typed enum
  public static TEnum ParseString(string strValue)
  {
    return ParseString(strValue, default);
  }
  public static TEnum ParseString(string strValue, TEnum enmDefault)
  {
    TEnum enmValue;
    TEnum defValue = enmDefault ?? default;
    try { enmValue = (TEnum)Enum.Parse(typeof(TEnum), strValue, true); }
    catch { enmValue = defValue; }
    return enmValue;
  }

  public static IList<TEnum> CreateList()
  {
    PdpEnumList = new List<TEnum>();
    foreach (object value in Enum.GetValues(typeof(TEnum)))
    {
      PdpEnumList.Add((TEnum)value);
    }
    return PdpEnumList;
  }

  public static Dictionary<TEnum,string> CreateEnumDictionary()
  {
    PdpEnumDict = new Dictionary<TEnum,string>();
    foreach (TEnum enmItem in Enum.GetValues(typeof(TEnum)))
    {
      var key = enmItem;
      var val = enmItem.ToString();
      PdpEnumDict.Add(key, val);
    }
    return PdpEnumDict;
  }

  public static Dictionary<short, string> CreateShortKeyDictionary()
  {
    var dict = new Dictionary<short, string>();
    foreach (object enmItem in Enum.GetValues(typeof(TEnum)))
    {
      var key = (short)enmItem;
      var val = enmItem.ToString();
      dict.Add(key, val);
    }
    return dict;
  }
  public static Dictionary<string, string> CreateStringKeyDictionary()
  {
    var dict = new Dictionary<string, string>();
    foreach (object enmItem in Enum.GetValues(GetEnumType))
    {
      var key = Convert.ChangeType(enmItem, GetBaseType).ToString();
      var val = enmItem.ToString();
      dict.Add(key, val);
    }
    return dict;
  }

  //public static SelectList GetEnumSelectList(T someSelectedValue)
  //{
  //  var listItems = from T itm in Enum.GetValues(typeof(T))
  //                  select new { Value = (int)itm., Text = itm.ToString() };
  //  if (someSelectedValue == null)
  //  { return new SelectList(listItems, "Value", "Text"); }
  //  else
  //  { return new SelectList(listItems, "Value", "Text", someSelectedValue); }
  //}

  //// MVC SelectListItem has 3 properties: bool Selected, string Text, string Value

  //public static IEnumerable<SelectListItem> GetEnumTextValueSelectList()
  //{ return (Enum.GetValues(typeof(T)).Cast<T>().Select(enu => new SelectListItem() { Text = enu.ToString(), Value = enu.ToString() })).ToList(); }

}
