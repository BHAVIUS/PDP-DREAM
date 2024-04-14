// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCrossReferenceItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCrossReferenceItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordCrossReferenceItem() : base()
  { this.Initialize(); }
  public NpdsRecordCrossReferenceItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordCrossReferenceItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, CrossReferenceItemXnam, CrossReferenceListXnam); }

  public string? CrossReference
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

[KnownType(typeof(NpdsRecordCrossReferenceList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCrossReferenceList : ANpdsXsgItemList<NpdsRecordCrossReferenceItem>
{
  public NpdsRecordCrossReferenceList() : base() { }
  public NpdsRecordCrossReferenceList(NpdsFieldRule rul) : base(rul) { }
}

