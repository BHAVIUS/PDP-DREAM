// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityOtherEntityItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOtherEntityItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityOtherEntityItem() : base()
  { this.Initialize(); }
  public NpdsEntityOtherEntityItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityOtherEntityItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, OtherEntityItemXnam, OtherEntityListXnam); }

  public string? OtherEntity
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

[KnownType(typeof(NpdsEntityOtherEntityList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOtherEntityList : ANpdsXsgItemList<NpdsEntityOtherEntityItem>
{
  public NpdsEntityOtherEntityList() : base() { }
  public NpdsEntityOtherEntityList(NpdsFieldRule rul) : base(rul) { }
}

