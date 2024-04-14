// NpdsInfosetEntailmentNexusItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetEntailmentItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetEntailmentItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsInfosetEntailmentItem() : base() { this.Initialize(); }
  public NpdsInfosetEntailmentItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetEntailmentItem(NpdsFieldRule rul, XElement val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, NexusEntailmentItemXnam, NexusEntailmentListXnam); }

  public XElement Entailment
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsInfosetEntailmentList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetEntailmentList : ANpdsXsgItemList<NpdsInfosetEntailmentItem>
{
  public NpdsInfosetEntailmentList() : base() { }
  public NpdsInfosetEntailmentList(NpdsFieldRule rul) : base(rul) { }
}
