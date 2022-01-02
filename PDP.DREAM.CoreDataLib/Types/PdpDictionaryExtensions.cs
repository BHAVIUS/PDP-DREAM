// PdpDictionaryExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Routing;

namespace PDP.DREAM.CoreDataLib.Types;

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
