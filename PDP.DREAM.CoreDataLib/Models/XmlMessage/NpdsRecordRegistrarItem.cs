// NpdsRecordRegistrarItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistrarItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistrarItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordRegistrarItem() : base() { this.Initialize(); }
  public NpdsRecordRegistrarItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordRegistrarItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.RegistrarItemXnam, PdpAppConst.RegistrarListXnam); }

  public string Registrar
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

