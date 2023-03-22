// PdpNullableType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpNullableType<TValueType>
{

  public PdpNullableType()
  {
    HasDefaultValue = false;
    HasValue = false;
  }

  public PdpNullableType(TValueType parValue)
  {
    HasDefaultValue = false;
    Value = parValue;
  }

  public PdpNullableType(TValueType parValue, TValueType parDefaultValue)
  {
    // assign DefaultValue first
    DefaultValue = parDefaultValue;
    // then Value assignment can check for DefaultValue
    Value = parValue;
  }


  private bool pnHasValue = false;
  public bool HasValue
  {
    get
    {
      return pnHasValue;
    }
    set
    {
      pnHasValue = value;
    }
  }

  private bool pnHasDefaultValue = false;
  public bool HasDefaultValue
  {
    get
    {
      return pnHasDefaultValue;
    }
    set
    {
      pnHasDefaultValue = value;
    }
  }

  private TValueType pnValue;
  public virtual TValueType Value
  {
    get
    {
      return pnValue;
    }
    set
    {
      if ((value == null) || Convert.IsDBNull(value))
      {
        pnHasValue = false;
      }
      else
      {
        pnValue = value;
        pnHasValue = true;
      }
    }
  }

  private TValueType pnDefaultValue;
  public virtual TValueType DefaultValue
  {
    get
    {
        return pnDefaultValue;
    }
    set
    {
      if ((value == null) || Convert.IsDBNull(value))
      {
        pnHasDefaultValue = false;
      }
      else
      {
        pnDefaultValue = value;
        pnHasDefaultValue = true;
      }
    }
  }

  public TValueType GetValueOrDefault()
  {
    if (HasValue)
    {
      return Value;
    }
    else if (HasDefaultValue)
    {
      return DefaultValue;
    }
    else
    {
      return default(TValueType);
    }
  }

  public TValueType GetValueOrDefault(TValueType parDefaultValue)
  {
    if (HasValue)
    {
      return Value;
    }
    else
    {
      DefaultValue = parDefaultValue;
      if (HasDefaultValue)
      {
        return DefaultValue;
      }
      else
      {
        throw new Exception("PdpNullableType has no DefaultValue. Set the DefaultValue property first or call the GetValueOrDefault function with an argument.");
      }
    }
  }

  // DBNull and IsDBNull are in the System namespace
  public static DBNull NullValue = DBNull.Value;

  public static implicit operator PdpNullableType<TValueType>(TValueType theType)
  {
    return (new PdpNullableType<TValueType>(theType));
  }

  public static explicit operator TValueType(PdpNullableType<TValueType> theType)
  {
    return theType.Value;
  }
}
