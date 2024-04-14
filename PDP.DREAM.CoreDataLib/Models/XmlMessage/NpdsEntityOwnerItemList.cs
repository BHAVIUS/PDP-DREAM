// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityOwnerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOwnerItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityOwnerItem() : base()
  { this.Initialize(); }
  public NpdsEntityOwnerItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityOwnerItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, OwnerItemXnam, OwnerListXnam); }

  public string? Owner
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

[KnownType(typeof(NpdsEntityOwnerList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOwnerList : ANpdsXsgItemList<NpdsEntityOwnerItem>
{
  public NpdsEntityOwnerList() : base() { }
  public NpdsEntityOwnerList(NpdsFieldRule rul) : base(rul) { }
}

