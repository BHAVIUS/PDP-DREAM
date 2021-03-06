// NpdsEntityOtherEntityItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsEntityOtherEntityItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityOtherEntityItem : ANpdsXsgTypedLabelItem
  {
    public NpdsEntityOtherEntityItem() : base() { this.Initialize(); }
    public NpdsEntityOtherEntityItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityOtherEntityItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.OtherEntityItemXnam, NpdsConst.OtherEntityListXnam); }

    public string OtherEntity
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsEntityOtherEntityList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityOtherEntityList : ANpdsXsgItemList<NpdsEntityOtherEntityItem>
  {
    public NpdsEntityOtherEntityList() : base() { }
    public NpdsEntityOtherEntityList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
