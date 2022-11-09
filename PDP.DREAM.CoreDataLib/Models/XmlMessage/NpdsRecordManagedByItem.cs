// NpdsRecordManagedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordManagedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordManagedByItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordManagedByItem() : base() { this.Initialize(); }
  public NpdsRecordManagedByItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordManagedByItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.ManagedByItemXnam, PdpAppConst.ManagedByListXnam); }

  public string ManagedBy
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

