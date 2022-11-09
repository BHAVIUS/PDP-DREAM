// NpdsEntityOwnerItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityOwnerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOwnerItem : ANpdsXsgTypedLabelItem
{
  public NpdsEntityOwnerItem() : base() { this.Initialize(); }
  public NpdsEntityOwnerItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityOwnerItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.OwnerItemXnam, PdpAppConst.OwnerListXnam); }

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
  public NpdsEntityOwnerList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

