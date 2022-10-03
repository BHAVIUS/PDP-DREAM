// NpdsEntityProvenanceItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityProvenanceItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityProvenanceItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityProvenanceItem() : base() { this.Initialize(); }
  public NpdsEntityProvenanceItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityProvenanceItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.ProvenanceItemXnam, PdpAppConst.ProvenanceListXnam); }

  public string Provenance
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsEntityProvenanceList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityProvenanceList : ANpdsXsgItemList<NpdsEntityProvenanceItem>
{
  public NpdsEntityProvenanceList() : base() { }
  public NpdsEntityProvenanceList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

