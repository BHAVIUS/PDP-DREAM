// NpdsEntityContactItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityContactItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityContactItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityContactItem() : base()
  { this.Initialize(); }
  public NpdsEntityContactItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityContactItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, ContactItemXnam, ContactListXnam); }

  public string? Contact
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

[KnownType(typeof(NpdsEntityContactList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityContactList : ANpdsXsgItemList<NpdsEntityContactItem>
{
  public NpdsEntityContactList() : base() { }
  public NpdsEntityContactList(NpdsFieldRule rul) : base(rul) { }
}

