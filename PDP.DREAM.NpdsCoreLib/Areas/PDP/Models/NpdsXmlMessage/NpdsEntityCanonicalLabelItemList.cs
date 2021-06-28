﻿using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityCanonicalLabelItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityCanonicalLabelItem : ANpdsXsgTypedLabelItem
  {
    public NpdsEntityCanonicalLabelItem() : base() { this.Initialize(); }
    public NpdsEntityCanonicalLabelItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityCanonicalLabelItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.CanonicalLabelItemXnam, NpdsConst.CanonicalLabelListXnam); }

    public string CanonicalLabel
    {
      get { return ItemValue; }
      set { if (value != null) { ItemValue = value; } }
    }

  }

  [KnownType(typeof(NpdsEntityCanonicalLabelList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityCanonicalLabelList : ANpdsXsgItemList<NpdsEntityCanonicalLabelItem>
  {
    public NpdsEntityCanonicalLabelList() : base() { }
    public NpdsEntityCanonicalLabelList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
