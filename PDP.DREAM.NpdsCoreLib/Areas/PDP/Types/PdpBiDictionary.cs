// PdpBiDictionary.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Routing;

namespace PDP.DREAM.NpdsCoreLib.Types
{
  public class PdpBiDictionary<TLeft, TRight>
    where TLeft : notnull
    where TRight : notnull
  {
    IDictionary<TLeft, TRight> leftToRight;
    IDictionary<TRight, TLeft> rightToLeft;

    public PdpBiDictionary()
    {
      if ((typeof(TLeft) == typeof(string)) && (typeof(TRight) == typeof(string)))
      {
        leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      }
      else if (typeof(TLeft) == typeof(string))
      {
        leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<string, TRight>(StringComparer.OrdinalIgnoreCase);
        rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<TRight, string>();
      }
      else if (typeof(TRight) == typeof(string))
      {
        leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<TLeft, string>();
        rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<string, TLeft>(StringComparer.OrdinalIgnoreCase);
      }
      else
      {
        leftToRight = new Dictionary<TLeft, TRight>();
        rightToLeft = new Dictionary<TRight, TLeft>();
      }
    }

    public void Add(TLeft left, TRight right)
    {
      if (leftToRight.ContainsKey(left))
      { throw new ArgumentException("Duplicate left in PdpBiDictionary"); }
      else if (rightToLeft.ContainsKey(right))
      { throw new ArgumentException("Duplicate right in PdpBiDictionary"); }

      leftToRight.Add(left, right);
      rightToLeft.Add(right, left);
    }

    public bool TryGetByLeft(TLeft left, out TRight right)
    { return leftToRight.TryGetValue(left, out right); }

    public bool TryGetByRight(TRight right, out TLeft left)
    { return rightToLeft.TryGetValue(right, out left); }

    public TRight GetByLeft(TLeft left)
    {
      if (left == null) return default(TRight);
      else return leftToRight[left];
    }

    public TLeft GetByRight(TRight right)
    {
      if (right == null) return default(TLeft);
      else return rightToLeft[right];
    }

  } // class

  public static class PdpDictionaryExtensions
  {
    public static IDictionary<string, string> ToDictionary<T>(this IDictionary<string, T> source)
    {
      if (source == null) { throw new ArgumentNullException(nameof(source)); }
      var dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, T> item in source)
      {
        dictionary.Add(item.Key, item.Value.ToString());
      }
      return dictionary;
    }

    public static IDictionary<string, string> ToDictionary(this IDictionary<string, IRouteConstraint> source)
    {
      if (source == null) { throw new ArgumentNullException(nameof(source)); }
      var dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, IRouteConstraint> item in source)
      {
        dictionary.Add(item.Key, item.Value.ToString());
      }
      return dictionary;
    }

    public static IDictionary<string, object> ToDictionary(this object source)
    {
      return source.ToDictionary<object>();
    }

    public static IDictionary<string, T> ToDictionary<T>(this object source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      var dictionary = new Dictionary<string, T>();

      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
      {
        AddPropertyToDictionary(property, source, dictionary);
      }
      return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
    {
      object value = property.GetValue(source);
      if (value is DateTime)
      {
        value = string.Format("{0:dd/MM/yyyy}", value);
        dictionary.Add(property.Name, (T)value);
      }
      else if (value is T)
      {
        dictionary.Add(property.Name, (T)value);
      }
    }

  } // class

} // namespace
