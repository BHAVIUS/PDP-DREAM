// NpdsRecordProvenanceItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordProvenanceItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordProvenanceItem : ANpdsXsgBaseItem<string>
{
  public NpdsRecordProvenanceItem() : base() { this.Initialize(); }
  public NpdsRecordProvenanceItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordProvenanceItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.ProvenanceItemXnam, PdpAppConst.ProvenanceListXnam); }

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
  public NpdsRecordProvenanceList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

