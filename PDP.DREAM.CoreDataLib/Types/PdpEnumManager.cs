// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpEnumManager : IComparable
{
  private readonly byte enmBytCode;
  private readonly string enmStrName;
  private readonly string enmStrDesc;

  protected PdpEnumManager() { }

  protected PdpEnumManager(byte eCode, string eName, string eDesc = "")
  {
    enmBytCode = eCode; // byte code
    enmStrName = eName; // string name
    enmStrDesc = eDesc; // string description
  }

  public PdpEnumManager Selected { get; set; }

  public byte EnmCode
  {
    get { return enmBytCode; }
  }
  public string EnmName
  {
    get { return enmStrName; }
  }
  public string EnmDesc
  {
    get { return enmStrDesc; }
  }
  public override string ToString()
  {
    return enmStrName;
  }

  public static IEnumerable<TPdpEnum> GetAllByLoop<TPdpEnum>()
    where TPdpEnum : PdpEnumManager, new()
  {
    var enmType = typeof(TPdpEnum);
    var enmFields = enmType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

    foreach (var enmFldInfo in enmFields)
    {
      var instance = new TPdpEnum();
      var locatedValue = enmFldInfo.GetValue(instance) as TPdpEnum;

      if (locatedValue != null)
      {
        yield return locatedValue;
      }
    }
  }

  public static IEnumerable<TPdpEnum> GetAllByLinq<TPdpEnum>()
   where TPdpEnum : PdpEnumManager, new()
  {
    return typeof(TPdpEnum)
     .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<TPdpEnum>();
  }

  public override bool Equals(object other)
  {
    var enmOther = other as PdpEnumManager;
    if (enmOther == null) { return false; }
    var typOther = enmOther.GetType();
    var codOther = enmOther.EnmCode;
    var typMatch = GetType().Equals(typOther);
    var codMatch = enmBytCode.Equals(codOther);
    return (typMatch && codMatch);
  }

  public override int GetHashCode()
  {
    return enmBytCode.GetHashCode();
  }

  public static int MathAbsDiff(PdpEnumManager itm1, PdpEnumManager itm2)
  {
    var absDiff = Math.Abs(itm1.EnmCode - itm2.EnmCode);
    return absDiff;
  }

  public static TPdpEnum GetByCode<TPdpEnum>(byte eCode)
    where TPdpEnum : PdpEnumManager, new()
  {
    var selected = enmParse<TPdpEnum, byte>(eCode, "code", obj => (obj.EnmCode == eCode));
    return selected;
  }

  public static TPdpEnum GetByName<TPdpEnum>(string eName)
    where TPdpEnum : PdpEnumManager, new()
  {
    var selected = enmParse<TPdpEnum, string>(eName, "name", obj => (obj.EnmName == eName));
    return selected;
  }

  private static TPdpEnum enmParse<TPdpEnum, TParse>(TParse parseObj, string parseTyp, Func<TPdpEnum, bool> predicate)
    where TPdpEnum : PdpEnumManager, new()
  {
    var found = GetAllByLoop<TPdpEnum>().FirstOrDefault(predicate);
    if (found == null)
    {
      var errorMsg = string.Format("'{0}' invalid {1} for {2}", parseObj, parseTyp, typeof(TPdpEnum));
      throw new ApplicationException(errorMsg);
    }
    return found;
  }

  // CompareTo sort order should return byte with:
  //     A value that indicates the relative order of the objects being compared. The
  //     return value has these meanings:
  //     Value – Meaning
  //     Less than zero – This instance precedes obj in the sort order.
  //     Zero – This instance occurs in the same position in the sort order as obj.
  //     Greater than zero – This instance follows obj in the sort order.

  // interface required implementation of CompareTo()
  public int CompareTo(object other)
  {
    return EnmCode.CompareTo(((PdpEnumManager)other).EnmCode);
  }
  public int CodeCompareTo(object other)
  {
    return EnmCode.CompareTo(((PdpEnumManager)other).EnmCode);
  }
  public int NameCompareTo(object other)
  {
    return EnmName.CompareTo(((PdpEnumManager)other).EnmName);
  }

} // end class

// end file