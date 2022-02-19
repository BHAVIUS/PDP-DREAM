// NpdsEntityLocationItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsEntityLocationItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityLocationItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityLocationItem() : base() { this.Initialize(); }
    public NpdsEntityLocationItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityLocationItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.LocationItemXnam, NpdsConst.LocationListXnam); }

    public string Location
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }

  } // class

  [KnownType(typeof(NpdsEntityLocationList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityLocationList : ANpdsXsgItemList<NpdsEntityLocationItem>
  {
    public NpdsEntityLocationList() : base() { }
    public NpdsEntityLocationList(NpdsConst.FieldRule rul) : base(rul) { }

  } // class

} //  namespace
