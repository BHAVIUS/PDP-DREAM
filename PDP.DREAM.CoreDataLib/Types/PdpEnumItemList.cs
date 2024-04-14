// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpEnumItemList<TEnum> where TEnum : notnull
{
  static PdpEnumItemList() { }

  public static readonly Type theEnumType = typeof(TEnum);
  public static readonly Type theBaseType = Enum.GetUnderlyingType(theEnumType);

  public static List<TEnum> PdpEnumList;
  public static Dictionary<byte, string> PdpEnumDict;

  public static bool Exists(TEnum enmValue)
  {
    return Enum.IsDefined(theEnumType, enmValue);
  }
  public static bool Exists(string strValue)
  {
    return Enum.IsDefined(theEnumType, strValue);
  }

  // byte numValue to typed enum
  public static TEnum ParseNumeric(byte numValue)
  {
    return ParseNumeric(numValue, default);
  }
  public static TEnum ParseNumeric(byte numValue, TEnum enmDefault)
  {
    TEnum enmValue;
    TEnum defValue = enmDefault ?? default;
    try { enmValue = (TEnum)Enum.ToObject(theEnumType, numValue); }
    catch { enmValue = defValue; }
    return enmValue;
  }

  // short numValue to typed enum
  public static TEnum ParseNumeric(short numValue)
  {
    return ParseNumeric(numValue, default);
  }
  public static TEnum ParseNumeric(short numValue, TEnum enmDefault)
  {
    TEnum enmValue;
    TEnum defValue = enmDefault ?? default;
    try { enmValue = (TEnum)Enum.ToObject(theEnumType, numValue); }
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
    try { enmValue = (TEnum)Enum.ToObject(theEnumType, numValue); }
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
    try { enmValue = (TEnum)Enum.Parse(theEnumType, strValue, true); }
    catch { enmValue = defValue; }
    return enmValue;
  }

  public static IEnumerable<TEnum> GetValues()
  {
    return Enum.GetValues(theEnumType).Cast<TEnum>();
  }
  public static IList<TEnum> CreateList()
  {
    PdpEnumList = new List<TEnum>();
    foreach (object value in Enum.GetValues(theEnumType))
    {
      PdpEnumList.Add((TEnum)value);
    }
    return PdpEnumList;
  }

  public static Dictionary<byte, string> CreateBytKeyStrValDictionary()
  {
    PdpEnumDict = new Dictionary<byte, string>();
    foreach (Enum enmItem in Enum.GetValues(theEnumType))
    {
      byte key = Convert.ToByte(enmItem);
      string val = enmItem.ToString();
      PdpEnumDict.Add(key, val);
    }
    return PdpEnumDict;
  }

} // end class

// end file