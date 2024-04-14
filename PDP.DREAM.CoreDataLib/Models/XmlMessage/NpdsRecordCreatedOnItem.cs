// NpdsRecordCreatedOnItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCreatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCreatedOnItem : ANpdsXsgBaseItem<DateTime>
{
  public NpdsRecordCreatedOnItem() : base() { this.Initialize(); }
  public NpdsRecordCreatedOnItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordCreatedOnItem(NpdsFieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, CreatedOnItemXnam, CreatedOnListXnam); }

  public DateTime CreatedOn
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}
