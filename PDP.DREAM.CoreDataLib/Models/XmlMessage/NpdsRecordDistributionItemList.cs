// NpdsRecordDistributionItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDistributionItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDistributionItem : ANpdsXsgBaseItem<string>
{
  public NpdsRecordDistributionItem() : base() { this.Initialize(); }
  public NpdsRecordDistributionItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDistributionItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DistributionItemXnam, DistributionListXnam); }

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
  public NpdsRecordDistributionList(NpdsFieldRule rul) : base(rul) { }
}

