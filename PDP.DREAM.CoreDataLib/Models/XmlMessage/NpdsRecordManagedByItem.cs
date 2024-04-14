// NpdsRecordManagedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordManagedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordManagedByItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordManagedByItem() : base()
  { this.Initialize(); }
  public NpdsRecordManagedByItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordManagedByItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, ManagedByItemXnam, ManagedByListXnam); }

  public string? ManagedBy
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

