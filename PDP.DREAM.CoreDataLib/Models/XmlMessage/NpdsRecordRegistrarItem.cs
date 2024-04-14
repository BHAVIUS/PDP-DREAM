// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistrarItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistrarItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordRegistrarItem() : base()
  { this.Initialize(); }
  public NpdsRecordRegistrarItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordRegistrarItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, RegistrarItemXnam, RegistrarListXnam); }

  public string? Registrar
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

// end file