// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntitySupportingLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingLabelItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntitySupportingLabelItem() : base()
  { this.Initialize(); }
  public NpdsEntitySupportingLabelItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntitySupportingLabelItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, SupportingLabelItemXnam, SupportingLabelListXnam); }

  public Uri? SupportingLabel
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
          writer.WriteAttributeString(ItemIsPrincipalXnam, IsPrincipal.ToString().ToLower());
          writer.WriteAttributeString(ItemIsPrivateXnam, IsPrivate.ToString().ToLower());
          writer.WriteAttributeString(ItemIsResolvableXnam, IsResolvable.ToString().ToLower());
        }
        // write content for element
        if (SupportingLabel != null)
        { writer.WriteValue(SupportingLabel.AbsoluteUri); }
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

} // end class

[KnownType(typeof(NpdsEntitySupportingLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingLabelList : ANpdsXsgItemList<NpdsEntitySupportingLabelItem>
{
  public NpdsEntitySupportingLabelList() : base() { }
  public NpdsEntitySupportingLabelList(NpdsFieldRule rul) : base(rul) { }
} // end class

// end file
