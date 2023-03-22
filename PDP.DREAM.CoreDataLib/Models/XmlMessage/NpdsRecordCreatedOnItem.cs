// NpdsRecordCreatedOnItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCreatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCreatedOnItem : ANpdsXsgBaseItem<DateTime>
{
  public NpdsRecordCreatedOnItem() : base() { this.Initialize(); }
  public NpdsRecordCreatedOnItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordCreatedOnItem(PdpAppConst.NpdsFieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.CreatedOnItemXnam, PdpAppConst.CreatedOnListXnam); }

  public DateTime CreatedOn
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}
