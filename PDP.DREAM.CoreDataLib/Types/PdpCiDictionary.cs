// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
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
//    var value = BcrAttributes.ContainsKey(name) ? BcrAttributes[name] : ESS;
//    return value;
//  }
//  set {
//    var name = attribName.ToLower();
//    BcrAttributes[name] = value;
//  }
//}

  // properties for collection of entity-attribute name-value pairs used by various reference/resource formats
  //
  // TODO: refactor using alternate structure based on the enums for each format
  // see migration started in BabbleNewt

  // TODO: clone this code functionality to PdpCiDictionary
  // alias 'this' to BcrEntityAttribs (base.Attributes) for null-defended getter/setter
  //public string this[string attribName]
  //{
  //  get {
  //    var name = attribName.ToLower();
  //    var value = base.BcrAttributes.ContainsKey(name) ? base.BcrAttributes[name] : ESS;
  //    return value;
  //  }
  //  set {
  //    var name = attribName.ToLower();
  //    base.BcrAttributes[name] = value;
  //  }
  //}

  //public void AddEntityAttrib(string type, string content)
  //{
  //  type = type.ToLower();

  //  bool dupeType, dupeCont;
  //  dupeCont = BcrAttributes.ContainsValue(content);
  //  dupeType = BcrAttributes.ContainsKey(type);

  //  if (!dupeType)
  //  {
  //    BcrAttributes.Add(type, content);
  //  }
  //  else if (dupeType && !dupeCont)
  //  {
  //    BcrAttributes[type] += " " + content;
  //  }
  //}

