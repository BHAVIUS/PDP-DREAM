// NpdsEntityContactItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityContactItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityContactItem : ANpdsXsgTypedLabelItem
{
  public NpdsEntityContactItem() : base() { this.Initialize(); }
  public NpdsEntityContactItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityContactItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.ContactItemXnam, PdpAppConst.ContactListXnam); }

  public string Contact
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsEntityContactList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityContactList : ANpdsXsgItemList<NpdsEntityContactItem>
{
  public NpdsEntityContactList() : base() { }
  public NpdsEntityContactList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

