// NpdsRecordRegistryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistryItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordRegistryItem() : base() { this.Initialize(); }
  public NpdsRecordRegistryItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordRegistryItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.RegistryItemXnam, PdpAppConst.RegistryListXnam); }

  public string Registry
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

