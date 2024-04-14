// NpdsRecordCreatedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCreatedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCreatedByItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordCreatedByItem() : base()
  { this.Initialize(); }
  public NpdsRecordCreatedByItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordCreatedByItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, CreatedByItemXnam, CreatedByListXnam); }

  public string? CreatedBy
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

}

