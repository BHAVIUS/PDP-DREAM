// NpdsEntityDistributionItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityDistributionItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityDistributionItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityDistributionItem() : base() { this.Initialize(); }
  public NpdsEntityDistributionItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityDistributionItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.DistributionItemXnam, PdpAppConst.DistributionListXnam); }

  public string Distribution
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsEntityDistributionList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityDistributionList : ANpdsXsgItemList<NpdsEntityDistributionItem>
{
  public NpdsEntityDistributionList() : base() { }
  public NpdsEntityDistributionList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

