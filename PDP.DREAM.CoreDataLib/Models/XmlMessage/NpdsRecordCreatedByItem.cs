// NpdsRecordCreatedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCreatedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCreatedByItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordCreatedByItem() : base() { this.Initialize(); }
  public NpdsRecordCreatedByItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordCreatedByItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.CreatedByItemXnam, PdpAppConst.CreatedByListXnam); }

  public string CreatedBy
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

