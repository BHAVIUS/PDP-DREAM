// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordOtherTextItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordOtherTextItem : ANpdsXsgBaseItem<string?>
{
  public NpdsRecordOtherTextItem() : base()
  { this.Initialize(); }
  public NpdsRecordOtherTextItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsRecordOtherTextItem(NpdsFieldRule rul, string? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, OtherTextItemXnam, OtherTextListXnam); }

  public string? OtherText
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

  public void ParseOtherText(string? value)
  {
    throw new NotImplementedException();
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
        if (OtherText != null)
        { writer.WriteValue(OtherText); }
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

} // end class

[KnownType(typeof(NpdsRecordOtherTextList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordOtherTextList : ANpdsXsgItemList<NpdsRecordOtherTextItem>
{
  public NpdsRecordOtherTextList() : base() { }
  public NpdsRecordOtherTextList(NpdsFieldRule rul) : base(rul) { }
}

