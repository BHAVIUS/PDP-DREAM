// PdpNullableStruct.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public struct PdpNullableStruct<T> where T : struct
{

  public PdpNullableStruct(T par) : this()
  {
    Value = par;
  }

  private bool sHasVal;
  public bool HasValue
  {
    get
    {
      return sHasVal;
    }
  }

  private T sVal;
  public T Value
  {
    get
    {
      return sVal;
    }
    set
    {
      sVal = value;
      if (Convert.IsDBNull(sVal))
      {
        sHasVal = false;
      }
      else
      {
        sHasVal = true;
      }
    }
  }

  public T GetValueOrDefault(T theDefaultValue)
  {
    if (HasValue)
    {
      return Value;
    }
    else
    {
      return theDefaultValue;
    }
  }

  // DBNull and IsDBNull are in the System namespace
  public static DBNull NullValue = DBNull.Value;

  public static implicit operator PdpNullableStruct<T>(T theValue)
  {
    return (new PdpNullableStruct<T>(theValue));
  }

  public static explicit operator T(PdpNullableStruct<T> theValue)
  {
    return theValue.Value;
  }

}

