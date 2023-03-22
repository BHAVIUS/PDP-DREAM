// PdpCiDictionary.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

// PDP Case Insensitive Dictionary
public class PdpCiDictionary : Dictionary<string, string>
{
  public PdpCiDictionary() : base(StringComparer.OrdinalIgnoreCase)  
  {
    KeyComparer = StringComparer.OrdinalIgnoreCase;
  }
  public PdpCiDictionary(StringComparer sc) : base(sc) 
  { 
    KeyComparer = sc;
  }
  public StringComparer KeyComparer { get; }

}

// TODO: clone this code functionality to a new Pdp typed dictionary
//public string this[string attribName]
//{
//  get {
//    var name = attribName.ToLower();
//    var value = BcrAttributes.ContainsKey(name) ? BcrAttributes[name] : string.Empty;
//    return value;
//  }
//  set {
//    var name = attribName.ToLower();
//    BcrAttributes[name] = value;
//  }
//}
