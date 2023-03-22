// PdpDictionaryExtensions.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpDictionaryExtensions
{
  public static IDictionary<int, int> AddUpdate(this IDictionary<int, int> iiDict, int itemKey, int itemVal)
  {
    if (iiDict.ContainsKey(itemKey)) { iiDict[itemKey] = itemVal; }
    else { iiDict.Add(itemKey, itemVal); }
    return iiDict;
  }

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
    if (source == null) { throw new ArgumentNullException(nameof(source)); }
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

} // end class

// end file