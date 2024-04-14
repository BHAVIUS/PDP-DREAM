// NpdsRecordUpdatedOnItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordUpdatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordUpdatedOnItem : ANpdsXsgBaseItem<DateTime>
{
  public NpdsRecordUpdatedOnItem() : base() { this.Initialize(); }
  public NpdsRecordUpdatedOnItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordUpdatedOnItem(NpdsFieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, UpdatedOnItemXnam, UpdatedOnListXnam); }

  public DateTime UpdatedOn
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}
