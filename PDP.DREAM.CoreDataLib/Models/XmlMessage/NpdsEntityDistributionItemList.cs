// NpdsEntityDistributionItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityDistributionItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityDistributionItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityDistributionItem() : base() { this.Initialize(); }
  public NpdsEntityDistributionItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityDistributionItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DistributionItemXnam, DistributionListXnam); }

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
  public NpdsEntityDistributionList(NpdsFieldRule rul) : base(rul) { }
}

