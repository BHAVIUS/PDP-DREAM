// NpdsRecordUpdatedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordUpdatedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordUpdatedByItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordUpdatedByItem() : base()
  { this.Initialize(); }
  public NpdsRecordUpdatedByItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordUpdatedByItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, UpdatedByItemXnam, UpdatedByListXnam); }

  public string? UpdatedBy
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

// end file