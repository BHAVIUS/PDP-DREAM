// PdpEnum.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;

#nullable disable

namespace PDP.DREAM.NpdsCoreLib.Types
{
  public static class PdpEnum<T>
  {
    public static T ToEnum(short numValue)
    {
      return ToEnum(numValue, default);
    }
    public static T ToEnum(short numValue, T enmDefault)
    {
      T enmValue;
      T defValue = enmDefault ?? default;
      try { enmValue = (T)Enum.ToObject(typeof(T), numValue); }
      catch { enmValue = defValue; }
      return enmValue;
    }

    public static T Parse(string strValue)
    {
      return Parse(strValue, default);
    }
    public static T Parse(string strValue, T enmDefault)
    {
      T enmValue;
      T defValue = enmDefault ?? default;
      try { enmValue = (T)Enum.Parse(typeof(T), strValue, true); }
      catch { enmValue = defValue; }
      return enmValue;
    }

    public static IList<T> GetTextNames()
    {
      IList<T> list = new List<T>();
      foreach (object value in Enum.GetValues(typeof(T)))
      {
        list.Add((T)value);
      }
      return list;
    }

    public static Dictionary<string, string> GetDictionary()
    {
      var dict = new Dictionary<string, string>();
      Type eTyp = typeof(T);
      Type uTyp = Enum.GetUnderlyingType(eTyp);
      foreach (object enm in Enum.GetValues(eTyp))
      {
        string val = enm.ToString();
        string key = Convert.ChangeType(enm, uTyp).ToString();
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

}
