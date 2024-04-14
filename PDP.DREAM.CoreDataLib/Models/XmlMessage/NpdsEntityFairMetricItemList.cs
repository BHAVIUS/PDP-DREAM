// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityFairMetricItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityFairMetricItem : ANpdsXsgBaseItem<string?>
{
  public NpdsEntityFairMetricItem() : base()
  { this.Initialize(); }
  public NpdsEntityFairMetricItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityFairMetricItem(NpdsFieldRule rul, string? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, FairMetricItemXnam, FairMetricListXnam); }

  public string? FairMetric
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    if (ItemHasValue || writer.WRACE.ItemDoesVerbose)
    {
      // write open tag for element
      writer.WriteStartElement(ItemXnam);
      if (ItemHasValue && writer.WRACE.ItemCanBeAccessed)
      {
        // write attributes for element
        if (ItemIndexKeys.Priority.HasValue && (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive))
        {
          writer.WriteAttributeString(ItemFacetPriorityXnam, ItemIndexKeys.Priority.ToString());
        }
        if (writer.WRACE.ItemDoesArchive)
        {
          if (ItemIndexKeys.GuidForeignKey.HasValue)
          { writer.WriteAttributeString(ItemForeignKeyXnam, ItemIndexKeys.GuidForeignKey.ToString()); }
          if (ItemIndexKeys.GuidPrimaryKey.HasValue)
          { writer.WriteAttributeString(ItemPrimaryKeyXnam, ItemIndexKeys.GuidPrimaryKey.ToString()); }
        }
        // write content for element
        if (FairMetric != null)
        { writer.WriteValue(FairMetric); }
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

}

[KnownType(typeof(NpdsEntityFairMetricList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityFairMetricList : ANpdsXsgItemList<NpdsEntityFairMetricItem>
{
  public NpdsEntityFairMetricList() : base() { }
  public NpdsEntityFairMetricList(NpdsFieldRule rul) : base(rul) { }
}
