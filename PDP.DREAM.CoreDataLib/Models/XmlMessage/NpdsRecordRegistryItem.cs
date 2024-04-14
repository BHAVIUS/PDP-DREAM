// NpdsRecordRegistryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistryItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordRegistryItem() : base()
  { this.Initialize(); }
  public NpdsRecordRegistryItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordRegistryItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, RegistryItemXnam, RegistryListXnam); }

  public string? Registry
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

// end file