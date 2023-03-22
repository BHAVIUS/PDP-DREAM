// NpdsInfosetEntailmentNexusItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetEntailmentNexusItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetEntailmentNexusItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsInfosetEntailmentNexusItem() : base() { this.Initialize(); }
  public NpdsInfosetEntailmentNexusItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetEntailmentNexusItem(PdpAppConst.NpdsFieldRule rul, XElement val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.NexusEntailmentItemXnam, PdpAppConst.NexusEntailmentListXnam); }

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
  public NpdsInfosetEntailmentNexusList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
