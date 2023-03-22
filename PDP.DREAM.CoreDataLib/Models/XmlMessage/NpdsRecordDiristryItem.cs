// NpdsRecordDiristryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDiristryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDiristryItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordDiristryItem() : base() { this.Initialize(); }
  public NpdsRecordDiristryItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDiristryItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.DiristryItemXnam, PdpAppConst.DiristryListXnam); }

  public string Diristry
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

