// NpdsRecordDiristryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDiristryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDiristryItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordDiristryItem() : base() { this.Initialize(); }
  public NpdsRecordDiristryItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDiristryItem(NpdsFieldRule rul, Uri? val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DiristryItemXnam, DiristryListXnam); }

  public string? Diristry
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

