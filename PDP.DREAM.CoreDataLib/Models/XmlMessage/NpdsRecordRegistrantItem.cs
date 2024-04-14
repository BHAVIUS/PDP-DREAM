// NpdsRecordRegistrantItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistrantItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistrantItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordRegistrantItem() : base()
  { this.Initialize(); }
  public NpdsRecordRegistrantItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordRegistrantItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, RegistrantItemXnam, RegistrantListXnam); }

  public string? Registrant
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

// end file