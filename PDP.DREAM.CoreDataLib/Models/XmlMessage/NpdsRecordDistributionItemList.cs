// NpdsRecordDistributionItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDistributionItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDistributionItem : ANpdsXsgBaseItem<string>
{
  public NpdsRecordDistributionItem() : base() { this.Initialize(); }
  public NpdsRecordDistributionItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDistributionItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.DistributionItemXnam, PdpAppConst.DistributionListXnam); }

  public string Distribution
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsRecordDistributionList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDistributionList : ANpdsXsgItemList<NpdsRecordDistributionItem>
{
  public NpdsRecordDistributionList() : base() { }
  public NpdsRecordDistributionList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

