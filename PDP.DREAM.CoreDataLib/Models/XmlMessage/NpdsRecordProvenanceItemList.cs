// NpdsRecordProvenanceItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordProvenanceItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordProvenanceItem : ANpdsXsgBaseItem<string>
{
  public NpdsRecordProvenanceItem() : base() { this.Initialize(); }
  public NpdsRecordProvenanceItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordProvenanceItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, ProvenanceItemXnam, ProvenanceListXnam); }

  public string Provenance
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsRecordProvenanceList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordProvenanceList : ANpdsXsgItemList<NpdsRecordProvenanceItem>
{
  public NpdsRecordProvenanceList() : base() { }
  public NpdsRecordProvenanceList(NpdsFieldRule rul) : base(rul) { }
}

