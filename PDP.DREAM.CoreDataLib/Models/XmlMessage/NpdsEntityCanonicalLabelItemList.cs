// NpdsEntityCanonicalLabelItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityCanonicalLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityCanonicalLabelItem : ANpdsXsgTypedLabelItem
{
  public NpdsEntityCanonicalLabelItem() : base() { this.Initialize(); }
  public NpdsEntityCanonicalLabelItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityCanonicalLabelItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.CanonicalLabelItemXnam, PdpAppConst.CanonicalLabelListXnam); }

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
  public NpdsEntityCanonicalLabelList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

