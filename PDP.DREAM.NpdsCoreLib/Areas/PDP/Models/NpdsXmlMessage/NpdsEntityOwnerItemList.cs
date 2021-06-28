using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityOwnerItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityOwnerItem : ANpdsXsgTypedLabelItem
  {
    public NpdsEntityOwnerItem() : base() { this.Initialize(); }
    public NpdsEntityOwnerItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityOwnerItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.OwnerItemXnam, NpdsConst.OwnerListXnam); }

    public string Owner
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsEntityOwnerList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityOwnerList : ANpdsXsgItemList<NpdsEntityOwnerItem>
  {
    public NpdsEntityOwnerList() : base() { }
    public NpdsEntityOwnerList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
