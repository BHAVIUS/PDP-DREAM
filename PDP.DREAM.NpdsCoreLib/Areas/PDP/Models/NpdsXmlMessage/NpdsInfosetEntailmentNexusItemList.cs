﻿using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsInfosetEntailmentNexusItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetEntailmentNexusItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsInfosetEntailmentNexusItem() : base() { this.Initialize(); }
    public NpdsInfosetEntailmentNexusItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsInfosetEntailmentNexusItem(NpdsConst.FieldRule rul, XElement val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.NexusEntailmentItemXnam, NpdsConst.NexusEntailmentListXnam); }

    public XElement Entailment
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsInfosetEntailmentNexusList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetEntailmentNexusList : ANpdsXsgItemList<NpdsInfosetEntailmentNexusItem>
  {
    public NpdsInfosetEntailmentNexusList() : base() { }
    public NpdsInfosetEntailmentNexusList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}