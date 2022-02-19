// NpdsEntityNameItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsEntityNameItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityNameItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityNameItem() : base() { this.Initialize(); }
    public NpdsEntityNameItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityNameItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.NameItemXnam, NpdsConst.NameListXnam); }

    public string Name
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsEntityNameList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityNameList : ANpdsXsgItemList<NpdsEntityNameItem>
  {
    public NpdsEntityNameList() : base() { }
    public NpdsEntityNameList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}