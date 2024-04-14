// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntitySupportingTagItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingTagItem : ANpdsXsgBaseItem<string?>
{
  public NpdsEntitySupportingTagItem() : base()
  { this.Initialize(); }
  public NpdsEntitySupportingTagItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntitySupportingTagItem(NpdsFieldRule rul, string? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, SupportingTagItemXnam, SupportingTagListXnam); }

  public string? SupportingTag
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

  public void ParseSupportingTag(string? value)
  {
    ItemValue = NpdsParsers.ParseSupportingTag(value);
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
        if (SupportingTag != null)
        { writer.WriteValue(SupportingTag); }
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

} // end class

[KnownType(typeof(NpdsEntitySupportingTagList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingTagList : ANpdsXsgItemList<NpdsEntitySupportingTagItem>
{
  public NpdsEntitySupportingTagList() : base() { }
  public NpdsEntitySupportingTagList(NpdsFieldRule rul) : base(rul) { }
}

